using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms;
using KWizCom.ResourceTranslator.Controls;
using KWizCom.ResourceTranslator.Utilities;

namespace KWizCom.ResourceTranslator.Forms
{
    public partial class MainForm : AccountValidationFormBase
    {
        #region Members

        private bool m_translationInProgress = false;
        private TranslationBatchJob m_batchJob;

        #endregion

        #region Properties

        public bool HasValidLanguageServiceCredentials
        {
            get
            {
                return Utilities.FormUtility.HasValidAppId || Utilities.FormUtility.HasValidMTSCredentials;
            }
        }

        public bool AllFilesReadyToTranslate
        {
            get
            {
                return listBox1.Items.Count > 0 && !((from ResourceFile r in listBox1.Items
                                                      where r.IsReadyToTranslate == false
                                                      select r).Count() > 0);
            }
        }

        public bool HasValidOutputPath
        {
            get
            {
                return !string.IsNullOrEmpty(txtOutputPath.Text) && Directory.Exists(txtOutputPath.Text);
            }
        }

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Properties.UserSettings.Default.UpgradeSettings)
            {
                Properties.UserSettings.Default.Upgrade();
                Properties.UserSettings.Default.UpgradeSettings = false;
                Properties.UserSettings.Default.Save();
                Properties.UserSettings.Default.Reload();
            }

            LogUtility.Start();

            this.Enabled = false;
            System.Windows.Forms.Timer delayLoadTimer = new System.Windows.Forms.Timer();
            delayLoadTimer.Tick += new EventHandler(delayLoadTimer_Tick);
            delayLoadTimer.Interval = 250;
            delayLoadTimer.Enabled = true;           
        }

        #endregion

        #region Event Handlers

        private void delayLoadTimer_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Enabled = false;

            //check saved settings
            AccountSettings accountSettings = new AccountSettings()
            {
                UseBing = Properties.UserSettings.Default.UseBing,
                UseMTS = Properties.UserSettings.Default.UseMTS,
                MTSClientId = Properties.UserSettings.Default.MTSClientId,
                MTSClientSecret = Properties.UserSettings.Default.MTSClientSecret,
                BingAppId = Properties.UserSettings.Default.BingAppId
            };

            Utilities.FormUtility.ValidateSettingsUsingModalDialog(this, ValidateSavedSettings_WorkEventHandler, accountSettings);

            if (!HasValidLanguageServiceCredentials)
            {
                using (SettingsForm settingsForm = new SettingsForm())
                {
                    settingsForm.Owner = this;
                    settingsForm.ShowInTaskbar = false;
                    settingsForm.ShowDialog(this);
                }
            }

            txtOutputPath.Text = openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SetupDisplay();
            txtOutputPath.TextChanged += new EventHandler(txtOutputPath_TextChanged);
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                ValidateResourceFiles(droppedFiles);
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string[] filenames = openFileDialog1.FileNames;

            ValidateResourceFiles(filenames);
        }

        private void btnBrowseResx_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            SetupDisplay();
        }

        private void btnBrowseOutputFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputPath.Text = folderBrowserDialog1.SelectedPath;
            }

            SetupDisplay();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            if (HasValidLanguageServiceCredentials && HasValidOutputPath & AllFilesReadyToTranslate)
            {
                var totalEntriesToTranslate = (from ResourceFile r in listBox1.Items
                                               select r).Sum(w => w.EntriesToTranslate.Count);

                var totalCharactersToTranslate = (from ResourceFile r in listBox1.Items
                                                  select r).Sum(w => w.EntriesToTranslateCharacterCount);


                DialogResult result = MessageBoxEx.Show(this, string.Format("You are about to translate {0} entries made up of {1} characters.\n\nContinue?", totalEntriesToTranslate, totalCharactersToTranslate), "Translation Confirmation", MessageBoxButtons.OKCancel);

                if (result != System.Windows.Forms.DialogResult.OK) return;

                UseWaitCursor = true;
                m_translationInProgress = true;
                SetupDisplay();

                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Starting translation...";

                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 0;

                m_batchJob = new TranslationBatchJob();

                m_batchJob.OutputPath = !txtOutputPath.Text.EndsWith(@"\") ? txtOutputPath.Text + "\\" : txtOutputPath.Text;
                m_batchJob.ResourceFiles = (from ResourceFile file in listBox1.Items
                                            select file as ResourceFile).ToList();

                bwTranslator.RunWorkerAsync(m_batchJob);
            }
        }

        private void bwTranslator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = ((TranslationBatchJob)e.UserState).StatusMessage;
        }

        private void bwTranslator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBoxEx.Show(this, "Translation Complete", "Translation Complete");

            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = ((TranslationBatchJob)e.Result).OutputPath;
            prc.Start();

            toolStripStatusLabel1.Visible = false;
            toolStripProgressBar1.Visible = false;

            UseWaitCursor = false;
            m_translationInProgress = false;
            SetupDisplay();
        }

        private void bwTranslator_DoWork(object sender, DoWorkEventArgs e)
        {
            var job = e.Argument as TranslationBatchJob;
            var outputPath = job.OutputPath;

            var totalEntriesToInclude = 0;

            for (int i = 0; i < job.ResourceFiles.Count; i++)
            {
                totalEntriesToInclude = totalEntriesToInclude + job.ResourceFiles[i].AllEntriesToIncludeCount;
            }

            for (int i = 0; i < job.ResourceFiles.Count; i++)
            {
                var resourceFile = job.ResourceFiles[i];

                double percentage = ((double)i / (double)job.ResourceFiles.Count) * 100.0;
                var toLanguage = Utilities.FormUtility.CultureDataSource[resourceFile.ToLanguage];
                var fromLanguage = Utilities.FormUtility.CultureDataSource[resourceFile.FromLanguage];

                job.StatusMessage = string.Format("Translating {0} from {1} to {2}", Path.GetFileName(resourceFile.Filename), fromLanguage, toLanguage);
                bwTranslator.ReportProgress((int)percentage, job);

                string outputFilename = outputPath + resourceFile.OutputFilename;

                TranslateFile(resourceFile, outputFilename);

                job.StatusMessage = string.Format("Finished Translating {0} from {1} to {2}", Path.GetFileName(resourceFile.Filename), fromLanguage, toLanguage);
                bwTranslator.ReportProgress((int)percentage, job);
            }

            job.StatusMessage = "Translation Complete";
            bwTranslator.ReportProgress(100, job);

            e.Result = job;
        }

        private void lnkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var resourceFile = listBox1.SelectedItem as ResourceFile;
            if (resourceFile == null) return;
            using (EditResxForm editForm = new EditResxForm(resourceFile))
            {
                editForm.Owner = this;
                editForm.ShowInTaskbar = false;
                editForm.ShowDialog(this);
            }

            SetupDisplay();
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var index = 0;
            if (listBox1.SelectedItem != null)
            {
                index = listBox1.SelectedIndex;
                listBox1.Items.Remove(listBox1.SelectedItem);

            }

            if (index < listBox1.Items.Count)
                listBox1.SelectedIndex = index;
            else if (index > listBox1.Items.Count - 1)
                listBox1.SelectedIndex = listBox1.Items.Count - 1;

            SetupDisplay();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnkEdit.Enabled = lnkDelete.Enabled = listBox1.SelectedItem != null;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.Owner = this;
                settingsForm.ShowInTaskbar = false;
                settingsForm.ShowDialog(this);
            }

            SetupDisplay();
        }

        private void txtOutputPath_TextChanged(object sender, EventArgs e)
        {
            SetupDisplay();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutForm = new AboutBox())
            {
                aboutForm.Owner = this;
                aboutForm.ShowInTaskbar = false;
                aboutForm.ShowDialog(this);
            }

            SetupDisplay();
        }

        #endregion

        #region Helpers

        private void SetupDisplay()
        {
            if (!m_translationInProgress)
            {
                txtOutputPath.Enabled =
                    btnBrowseOutputFolder.Enabled =
                    btnBrowseResx.Enabled =
                    btnTranslate.Enabled =
                    listBox1.Enabled = HasValidLanguageServiceCredentials;

                lnkDelete.Enabled =
                   lnkEdit.Enabled = listBox1.SelectedItem != null && HasValidLanguageServiceCredentials;

                if ((HasValidLanguageServiceCredentials && AllFilesReadyToTranslate && HasValidOutputPath) == false)
                {
                    btnTranslate.Enabled = false;

                    errorProvider1.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
                    errorProvider1.SetIconAlignment(btnTranslate, ErrorIconAlignment.MiddleLeft);
                    errorProvider1.Icon = Properties.Resources.error_icon;

                    if (!HasValidLanguageServiceCredentials)
                    {
                        errorProvider1.SetError(btnTranslate, "Additional Application Settings required\n\nPlease click on settings and enter a valid credentials.");
                    }
                    else if (!AllFilesReadyToTranslate)
                    {
                        if (listBox1.Items.Count > 0)
                        {
                            errorProvider1.SetError(btnTranslate, "Resource File(s) Not Ready\n\nEach resource file must be configured with:\n\t1. An output filename\n\t2. Source language\n\t3. Target language\n\t4. Must include at least 1 entry");
                        }
                        else
                        {
                            errorProvider1.SetError(btnTranslate, "Resource File(s) Not Ready\n\nThere must be at least one resource file in the resource file list.");
                        }
                    }
                    else if (!HasValidOutputPath)
                    {
                        errorProvider1.SetError(btnTranslate, "Output Settings Required \n\nPlease enter a valid output path.");
                    }

                    errorProvider1.SetIconPadding(btnTranslate, 3);
                }
                else
                {
                    btnTranslate.Enabled = true;
                    errorProvider1.SetError(btnTranslate, "");
                }
            }
            else
            {
                txtOutputPath.Enabled =
                    btnBrowseOutputFolder.Enabled =
                    btnBrowseResx.Enabled =
                    btnTranslate.Enabled =
                    listBox1.Enabled =
                    lnkDelete.Enabled =
                    lnkEdit.Enabled = false;
            }
        }

        private static void TranslateFile(ResourceFile resourceFile, string outputFilename)
        {
            if (resourceFile.EntriesToTranslate.Count > 0 || (resourceFile.IncludeAllEntries && resourceFile.EntriesUntranslated.Count > 0))
            {
                ResXResourceWriter writer = new ResXResourceWriter(outputFilename);

                if (resourceFile.EntriesToTranslate.Count > 0)
                {
                    var resourceGroups = GetGroups(resourceFile, resourceFile.EntriesToTranslate);

                    foreach (var resourceGroup in resourceGroups)
                    {
                        TranslateGroup(resourceFile, writer, resourceGroup);
                    }
                }

                if (resourceFile.IncludeAllEntries && resourceFile.EntriesUntranslated.Count > 0)
                {
                    foreach (var entry in resourceFile.EntriesUntranslated)
                    {
                        writer.AddResource(entry.Key, entry.OriginalValue);
                    }
                }

                writer.Generate();
                writer.Dispose();
            }
        }

        private static void TranslateGroup(ResourceFile resourceFile, ResXResourceWriter writer, List<ResourceEntry> resourceGroup)
        {
            string[] texts = (from t in resourceGroup
                              select t.OriginalValueWithPlaceHolders).ToArray();

            TranslatorService.TranslateOptions options = new TranslatorService.TranslateOptions();
            TranslatorService.TranslateArrayResponse[] translatedTexts = null;

            if (Utilities.FormUtility.HasValidMTSCredentials)
            {
                translatedTexts = TranslateUsingMTSService(resourceFile, texts, options);
            }
            else if (Utilities.FormUtility.HasValidAppId)
            {
                translatedTexts = TranslateUsingBingAppId(resourceFile, texts, options);
            }

            int j = 0;

            foreach (TranslatorService.TranslateArrayResponse translationResponse in translatedTexts)
            {
                resourceGroup[j].ReplacePlaceHoldersWithTokens(translationResponse.TranslatedText);
                writer.AddResource(resourceGroup[j].Key, resourceGroup[j].TranslatedValue);
                j++;
            }
        }

        private static TranslatorService.TranslateArrayResponse[] TranslateUsingBingAppId(ResourceFile resourceFile, string[] texts, TranslatorService.TranslateOptions options)
        {
            TranslatorService.TranslateArrayResponse[] translatedTexts =
                Utilities.FormUtility.MTSLanguageServiceClient.TranslateArray(Utilities.FormUtility.BingAppId, texts, resourceFile.FromLanguage, resourceFile.ToLanguage, options);
            return translatedTexts;
        }

        private static TranslatorService.TranslateArrayResponse[] TranslateUsingMTSService(ResourceFile resourceFile, string[] texts, TranslatorService.TranslateOptions options)
        {
            TranslatorService.TranslateArrayResponse[] translatedTexts = null;

            try
            {
                using (OperationContextScope scope = new OperationContextScope(Utilities.FormUtility.MTSLanguageServiceClient.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utilities.FormUtility.MTSAccessHeader;
                    //Keep appId parameter blank as we are sending access token in authorization header.                    
                    translatedTexts = Utilities.FormUtility.MTSLanguageServiceClient.TranslateArray("", texts, resourceFile.FromLanguage, resourceFile.ToLanguage, options);
                }
            }
            catch (WebException wex)
            {
                LogUtility.WriteError(wex.ToString());
            }
            catch (Exception ex)
            {
                LogUtility.WriteError(ex.ToString());
            }
            return translatedTexts;
        }

        private static List<List<ResourceEntry>> GetGroups(ResourceFile resourceFile, List<ResourceEntry> entriesToTranslate)
        {
            int maxSize = GetMaxMessageSize();

            var resourceGroups = new List<List<ResourceEntry>>();
            bool startNewGroup = true;
            List<ResourceEntry> resourceGroup = null;
            int resourceGroupLength;

            for (int i = 0; i < resourceFile.EntriesToTranslate.Count; i++)
            {
                var currentResource = resourceFile.EntriesToTranslate[i];

                if (startNewGroup)
                {
                    resourceGroupLength = 0;
                    resourceGroup = new List<ResourceEntry>();
                    startNewGroup = false;
                }

                resourceGroupLength = (int)resourceGroup.Sum(w => w.OriginalValue.Length);


                if ((currentResource.OriginalValue.Length + resourceGroupLength) * sizeof(char) > maxSize / 19)
                {
                    startNewGroup = true;
                    i--;
                    resourceGroups.Add(resourceGroup);
                }
                else if (i == resourceFile.EntriesToTranslate.Count - 1)
                {
                    resourceGroup.Add(currentResource);
                    resourceGroups.Add(resourceGroup);
                }
                else
                {
                    resourceGroup.Add(currentResource);
                }
            }
            return resourceGroups;
        }

        private static int GetMaxMessageSize()
        {
            int defaultMaxSize = 2 * 131072;
            int maxSize = defaultMaxSize;
            BasicHttpBinding binding = Utilities.FormUtility.MTSLanguageServiceClient.ChannelFactory.Endpoint.Binding as BasicHttpBinding;
            var maxReceivedMessageSize = binding.MaxReceivedMessageSize;
            if (maxReceivedMessageSize != defaultMaxSize)
            {
                if (maxReceivedMessageSize <= 0)
                {
                    binding.MaxBufferSize = 131072;
                    binding.MaxBufferPoolSize = 1048576;
                    binding.MaxReceivedMessageSize = 131072;
                }

                maxSize = (int)binding.MaxReceivedMessageSize;

            }
            return maxSize;
        }

        private void ValidateResourceFiles(string[] filenames)
        {
            if (filenames.Length > 0)
            {
                listBox1.DisplayMember = "Filename";
                listBox1.Enabled = false;

                foreach (var filename in filenames)
                {
                    ResourceFile resourceFile = new ResourceFile(filename);

                    if (resourceFile.EntriesInitialized)
                    {
                        listBox1.Items.Add(resourceFile);
                    }
                    else
                    {
                        MessageBoxEx.Show("Couldn't add \"" + System.IO.Path.GetFileName(filename) + "\" because it isn't a valid resource file.", "Invalid Resource File");
                    }
                }

                SetupDisplay();
            }
        }

        #endregion
    }
}
using System;
using System.Windows.Forms;
using KWizCom.ResourceTranslator.Controls;

namespace KWizCom.ResourceTranslator.Forms
{
    public partial class SettingsForm : AccountValidationFormBase
    {
        #region Constructor

        public SettingsForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!string.IsNullOrEmpty(Properties.UserSettings.Default.BingAppId))
                bingSettingsControl1.BingAppId = Properties.UserSettings.Default.BingAppId;

            if (!string.IsNullOrEmpty(Properties.UserSettings.Default.MTSClientId))
                mtsSettingsControl1.ClientId = Properties.UserSettings.Default.MTSClientId;

            if (!string.IsNullOrEmpty(Properties.UserSettings.Default.MTSClientSecret))
                mtsSettingsControl1.ClientSecret = Properties.UserSettings.Default.MTSClientSecret;

            if (Utilities.FormUtility.UseMTS)
                cmbxTranslationService.SelectedIndex = 0;
            else if (Utilities.FormUtility.UseBing)
                cmbxTranslationService.SelectedIndex = 1;

            pictureBox1.Visible = false;
        }

        #endregion

        #region Event Handlers

        private void cmbxTranslationService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxTranslationService.SelectedIndex == 0)
            {
                pnlSettingsContainer.Controls.Clear();
                mtsSettingsControl1.Dock = DockStyle.Fill;
                pnlSettingsContainer.Controls.Add(mtsSettingsControl1);
            }
            else
            {
                pnlSettingsContainer.Controls.Clear();
                bingSettingsControl1.Dock = DockStyle.Fill;
                pnlSettingsContainer.Controls.Add(bingSettingsControl1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //check saved settings
            if ((cmbxTranslationService.SelectedIndex == 0 && (string.IsNullOrEmpty(mtsSettingsControl1.ClientId) || string.IsNullOrEmpty(mtsSettingsControl1.ClientSecret)))
                || (cmbxTranslationService.SelectedIndex == 1 && string.IsNullOrEmpty(bingSettingsControl1.BingAppId)))
            {
                MessageBoxEx.Show(this, "Please enter the account details to use.", "Specify Account Details");
                return;
            }

            AccountSettings accountSettings = new AccountSettings()
            {
                UseBing = cmbxTranslationService.SelectedIndex == 1,
                UseMTS = cmbxTranslationService.SelectedIndex == 0,
                MTSClientId = mtsSettingsControl1.ClientId,
                MTSClientSecret = mtsSettingsControl1.ClientSecret,
                BingAppId = bingSettingsControl1.BingAppId
            };

            Utilities.FormUtility.ValidateSettingsUsingModalDialog(this, ValidateSavedSettings_WorkEventHandler, accountSettings);

            if (accountSettings.UseMTS)
            {
                if (Utilities.FormUtility.HasValidMTSCredentials)
                {
                    MessageBoxEx.Show(this, "Microsoft Translator Service Account Verified", "Account Validation Success");
                    Close();
                }
                else
                {
                    // show error
                    MessageBoxEx.Show(this, "Could not validate Microsoft Translator Service account credentials. Check the event log for more details.", "Account Validation Error");
                }
            }
            else if (accountSettings.UseBing)
            {
                if (Utilities.FormUtility.HasValidAppId)
                {
                    MessageBoxEx.Show(this, "Bing Application ID verified", "Account Validation Success");
                    Close();
                }
                else
                {
                    // show error
                    MessageBoxEx.Show(this, "Could not validate Bing Application ID. Check the event log for more details.", "Account Validation Error.");
                }
            }
        }        

        private void btnClear_Click(object sender, EventArgs e)
        {
            Properties.UserSettings.Default.MTSClientId = string.Empty;
            Properties.UserSettings.Default.MTSClientSecret = string.Empty;
            Properties.UserSettings.Default.BingAppId = string.Empty;
            Properties.UserSettings.Default.UseMTS = true;
            Properties.UserSettings.Default.UseBing = false;
            Properties.UserSettings.Default.Save();
            Properties.UserSettings.Default.Reload();
            Utilities.FormUtility.HasValidMTSCredentials = false;
            Utilities.FormUtility.HasValidAppId = false;
            bingSettingsControl1.BingAppId = string.Empty;
            mtsSettingsControl1.ClientSecret = string.Empty;
            mtsSettingsControl1.ClientId = string.Empty;
            cmbxTranslationService.SelectedIndex = 0;
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KWizCom.ResourceTranslator.Controls;
using System.Globalization;
//using Microsoft.WindowsAPICodePack.Shell;

namespace KWizCom.ResourceTranslator.Forms
{
    public partial class EditResxForm : Form
    {
        #region Members
        private ResourceFile m_resourceFile;
        private bool m_disableSelectAllCheckBox = false;
        private bool m_selectAllCheckboxClick = false;
        private int m_totalCheckBoxes = 0;
        private int m_checkedCheckBoxes = 0;
        private Regex m_resourceFilenameRegex = null;//new Regex(@"\.[a-zA-Z]{2}\-[a-zA-Z]{2}\.resx", RegexOptions.IgnoreCase);
        #endregion

        #region Properties

        private Regex ResourceFilenameRegex
        {
            get
            {
                if (m_resourceFilenameRegex == null)
                {
                    string pattern = string.Empty;
                    var cultures = CultureInfo.GetCultures(CultureTypes.FrameworkCultures);
                    pattern += "(";
                    foreach (var culture in cultures)
                    {
                        pattern += "\\." + culture.Name + "\\.resx" + "|";
                    }
                    pattern = pattern.TrimEnd(new char[] { '|' });
                    pattern += ")";

                    m_resourceFilenameRegex = new Regex(pattern, RegexOptions.IgnoreCase);
                }

                return m_resourceFilenameRegex;
            }
        }

        #endregion

        #region Constructor

        public EditResxForm(ResourceFile resourceFile)
        {
            InitializeComponent();
            m_resourceFile = resourceFile;

            FileInfo file = new FileInfo(resourceFile.Filename);
            label1.Text = Path.GetFileName(resourceFile.Filename);
            label2.Text = resourceFile.Filename;
            label3.Text = file.Length + " bytes";
            label4.Text = resourceFile.Entries.Count.ToString();
            label5.Text = resourceFile.TotalCharacterCount.ToString();
            //Text = Path.GetFileName(m_resourceFile.Filename) + " (" + m_resourceFile.Filename + ")";
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetupDisplay(false);

            System.Windows.Forms.Timer delayLoadTimer = new System.Windows.Forms.Timer();
            delayLoadTimer.Tick += new EventHandler(delayLoadTimer_Tick);
            delayLoadTimer.Interval = 250;
            delayLoadTimer.Enabled = true;

            //if (AeroGlassCompositionEnabled)
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        ExcludeControlFromAeroGlass(c);
            //    }
            //}
        }

        #endregion

        #region Event Handlers

        private void delayLoadTimer_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Enabled = false;
            dgvResourceEntries.SuspendLayout();
            dgvResourceEntries.AutoGenerateColumns = false;
            dgvResourceEntries.DataSource = m_resourceFile.Entries;// m_resourceFile.ToDataTable();
            dgvResourceEntries.ResumeLayout();
            SetupLanguageDropsDowns();
            SetupDisplay(true);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_disableSelectAllCheckBox)
            {
                m_selectAllCheckboxClick = true;
                foreach (DataGridViewRow rows in dgvResourceEntries.Rows)
                {
                    rows.Cells["Translate"].Value = chkSelectAll.Checked;
                }
                m_checkedCheckBoxes = chkSelectAll.Checked ? m_totalCheckBoxes : 0;

                dgvResourceEntries.RefreshEdit();
                m_selectAllCheckboxClick = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbxTranslateFrom.SelectedValue == cbxTranslateTo.SelectedValue)
            {
                MessageBoxEx.Show(this, "Source language and target language can not be the same.", "Language Conflict");
            }
            else
            {
                foreach (DataGridViewRow row in dgvResourceEntries.Rows)
                {
                    bool translate = (bool)row.Cells["Translate"].Value; ;
                    string originalValue = (string)row.Cells["OriginalValue"].Value;
                    string key = (string)row.Cells["Key"].Value;

                    // TODO: Use a dirty flag instead of checking every row.
                    m_resourceFile.Entries[row.Index].Translate = (bool)row.Cells["Translate"].Value;
                    if (m_resourceFile.Entries[row.Index].OriginalValue != (string)row.Cells["OriginalValue"].Value)                    
                        m_resourceFile.Entries[row.Index].OriginalValue = (string)row.Cells["OriginalValue"].Value;                    
                    m_resourceFile.Entries[row.Index].Key = (string)row.Cells["Key"].Value;
                }

                m_resourceFile.FromLanguage = cbxTranslateFrom.SelectedValue as string;
                m_resourceFile.ToLanguage = cbxTranslateTo.SelectedValue as string;
                m_resourceFile.OutputFilename = txtBoxOutPutFilename.Text;
                m_resourceFile.IncludeAllEntries = chkIncludeAllEntries.Checked;

                Close();
            }
        }

        private void dgvResourceEntries_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_selectAllCheckboxClick && dgvResourceEntries.Columns[e.ColumnIndex].Name == "Translate")
            {
                m_disableSelectAllCheckBox = true;
                m_checkedCheckBoxes = (bool)dgvResourceEntries.Rows[e.RowIndex].Cells["Translate"].Value == true ? m_checkedCheckBoxes + 1 : m_checkedCheckBoxes - 1;
                chkSelectAll.Checked = m_checkedCheckBoxes == m_totalCheckBoxes;
                dgvResourceEntries.EndEdit();
                m_disableSelectAllCheckBox = false;
            }
        }

        private void dgvResourceEntries_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvResourceEntries.CurrentCell is DataGridViewCheckBoxCell)
                dgvResourceEntries.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSuggest_Click(object sender, EventArgs e)
        {
            string selectedFilename = m_resourceFile.Filename;
            string filename = Path.GetFileName(selectedFilename);
            var toValue = (KeyValuePair<string, string>)cbxTranslateTo.SelectedItem;
            filename = ResourceFilenameRegex.Replace(filename, "." + toValue.Key + ".resx");
            txtBoxOutPutFilename.Text = filename;
        }

        #endregion

        #region Helpers

        private void SetupDisplay(bool editMode)
        {
            dgvResourceEntries.Visible =
                cbxTranslateFrom.Visible =
                lblTanslateFrom.Visible =
                cbxTranslateTo.Visible =
                lblTranslateTo.Visible =
                btnSave.Visible =
                chkSelectAll.Visible =
                lblOutPutFilename.Visible =
                txtBoxOutPutFilename.Visible =
                btnSuggest.Visible =
                chkIncludeAllEntries.Visible = editMode;

            chkIncludeAllEntries.Checked = m_resourceFile.IncludeAllEntries;
            if (m_resourceFile.ToLanguage != null)
                cbxTranslateTo.SelectedValue = m_resourceFile.ToLanguage;
            if (m_resourceFile.FromLanguage != null)
                cbxTranslateFrom.SelectedValue = m_resourceFile.FromLanguage;
            if (!string.IsNullOrEmpty(m_resourceFile.OutputFilename))
                txtBoxOutPutFilename.Text = m_resourceFile.OutputFilename;

            pictureBox1.Visible = !editMode;

            if (!editMode)
            {
                txtBoxOutPutFilename.SendToBack();
                pictureBox1.Dock = DockStyle.Fill;
                pictureBox1.BringToFront();
            }
            else
            {
                txtBoxOutPutFilename.BringToFront();
                pictureBox1.SendToBack();
            }
        }

        private void SetupLanguageDropsDowns()
        {
            cbxTranslateTo.DataSource = new BindingSource(Utilities.FormUtility.CultureDataSource, null);
            cbxTranslateFrom.DataSource = new BindingSource(Utilities.FormUtility.CultureDataSource, null);
            cbxTranslateTo.DisplayMember = cbxTranslateFrom.DisplayMember = "Value";
            cbxTranslateTo.ValueMember = cbxTranslateFrom.ValueMember = "Key";
        }

        #endregion
    }
}
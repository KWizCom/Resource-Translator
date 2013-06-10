namespace KWizCom.ResourceTranslator.Controls
{
    partial class BingSettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpbxBingTranslatorSettings = new System.Windows.Forms.GroupBox();
            this.lblBingAppId = new System.Windows.Forms.Label();
            this.txtBingAppId = new System.Windows.Forms.TextBox();
            this.lnkGetBingAppId = new System.Windows.Forms.LinkLabel();
            this.grpbxBingTranslatorSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbxBingTranslatorSettings
            // 
            this.grpbxBingTranslatorSettings.Controls.Add(this.lblBingAppId);
            this.grpbxBingTranslatorSettings.Controls.Add(this.txtBingAppId);
            this.grpbxBingTranslatorSettings.Controls.Add(this.lnkGetBingAppId);
            this.grpbxBingTranslatorSettings.Location = new System.Drawing.Point(3, 3);
            this.grpbxBingTranslatorSettings.Name = "grpbxBingTranslatorSettings";
            this.grpbxBingTranslatorSettings.Size = new System.Drawing.Size(469, 114);
            this.grpbxBingTranslatorSettings.TabIndex = 6;
            this.grpbxBingTranslatorSettings.TabStop = false;
            this.grpbxBingTranslatorSettings.Text = "Bing Translator Settings";
            // 
            // lblBingAppId
            // 
            this.lblBingAppId.AutoSize = true;
            this.lblBingAppId.Location = new System.Drawing.Point(6, 35);
            this.lblBingAppId.Name = "lblBingAppId";
            this.lblBingAppId.Size = new System.Drawing.Size(97, 13);
            this.lblBingAppId.TabIndex = 1;
            this.lblBingAppId.Text = "Bing Application ID";
            // 
            // txtBingAppId
            // 
            this.txtBingAppId.Location = new System.Drawing.Point(109, 32);
            this.txtBingAppId.Name = "txtBingAppId";
            this.txtBingAppId.Size = new System.Drawing.Size(354, 20);
            this.txtBingAppId.TabIndex = 0;
            // 
            // lnkGetBingAppId
            // 
            this.lnkGetBingAppId.AutoSize = true;
            this.lnkGetBingAppId.BackColor = System.Drawing.SystemColors.Control;
            this.lnkGetBingAppId.Location = new System.Drawing.Point(346, 55);
            this.lnkGetBingAppId.Name = "lnkGetBingAppId";
            this.lnkGetBingAppId.Size = new System.Drawing.Size(117, 13);
            this.lnkGetBingAppId.TabIndex = 3;
            this.lnkGetBingAppId.TabStop = true;
            this.lnkGetBingAppId.Text = "Get Bing Application ID";
            this.lnkGetBingAppId.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGetBingAppId_LinkClicked);
            // 
            // BingSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpbxBingTranslatorSettings);
            this.Name = "BingSettingsControl";
            this.Size = new System.Drawing.Size(475, 120);
            this.grpbxBingTranslatorSettings.ResumeLayout(false);
            this.grpbxBingTranslatorSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbxBingTranslatorSettings;
        private System.Windows.Forms.Label lblBingAppId;
        private System.Windows.Forms.TextBox txtBingAppId;
        private System.Windows.Forms.LinkLabel lnkGetBingAppId;

    }
}

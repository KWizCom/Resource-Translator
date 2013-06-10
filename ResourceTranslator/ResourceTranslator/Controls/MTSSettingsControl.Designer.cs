namespace KWizCom.ResourceTranslator.Controls
{
    partial class MTSSettingsControl
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
            this.grpbxMicrosoftTranslatorSettings = new System.Windows.Forms.GroupBox();
            this.lnkGetStartedWithMicrosoftTranslator = new System.Windows.Forms.LinkLabel();
            this.txtMTSClientSecret = new System.Windows.Forms.TextBox();
            this.txtMTSClientId = new System.Windows.Forms.TextBox();
            this.lblClientSecret = new System.Windows.Forms.Label();
            this.lblClientId = new System.Windows.Forms.Label();
            this.grpbxMicrosoftTranslatorSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbxMicrosoftTranslatorSettings
            // 
            this.grpbxMicrosoftTranslatorSettings.Controls.Add(this.lnkGetStartedWithMicrosoftTranslator);
            this.grpbxMicrosoftTranslatorSettings.Controls.Add(this.txtMTSClientSecret);
            this.grpbxMicrosoftTranslatorSettings.Controls.Add(this.txtMTSClientId);
            this.grpbxMicrosoftTranslatorSettings.Controls.Add(this.lblClientSecret);
            this.grpbxMicrosoftTranslatorSettings.Controls.Add(this.lblClientId);
            this.grpbxMicrosoftTranslatorSettings.Location = new System.Drawing.Point(3, 3);
            this.grpbxMicrosoftTranslatorSettings.Name = "grpbxMicrosoftTranslatorSettings";
            this.grpbxMicrosoftTranslatorSettings.Size = new System.Drawing.Size(469, 114);
            this.grpbxMicrosoftTranslatorSettings.TabIndex = 7;
            this.grpbxMicrosoftTranslatorSettings.TabStop = false;
            this.grpbxMicrosoftTranslatorSettings.Text = "Microsoft Translator Settings";
            // 
            // lnkGetStartedWithMicrosoftTranslator
            // 
            this.lnkGetStartedWithMicrosoftTranslator.AutoSize = true;
            this.lnkGetStartedWithMicrosoftTranslator.BackColor = System.Drawing.SystemColors.Control;
            this.lnkGetStartedWithMicrosoftTranslator.Location = new System.Drawing.Point(281, 78);
            this.lnkGetStartedWithMicrosoftTranslator.Name = "lnkGetStartedWithMicrosoftTranslator";
            this.lnkGetStartedWithMicrosoftTranslator.Size = new System.Drawing.Size(182, 13);
            this.lnkGetStartedWithMicrosoftTranslator.TabIndex = 4;
            this.lnkGetStartedWithMicrosoftTranslator.TabStop = true;
            this.lnkGetStartedWithMicrosoftTranslator.Text = "Get Started with Microsoft Translator ";
            this.lnkGetStartedWithMicrosoftTranslator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGetStartedWithMicrosoftTranslator_LinkClicked);
            // 
            // txtMTSClientSecret
            // 
            this.txtMTSClientSecret.Location = new System.Drawing.Point(82, 55);
            this.txtMTSClientSecret.Name = "txtMTSClientSecret";
            this.txtMTSClientSecret.Size = new System.Drawing.Size(381, 20);
            this.txtMTSClientSecret.TabIndex = 3;
            // 
            // txtMTSClientId
            // 
            this.txtMTSClientId.Location = new System.Drawing.Point(82, 29);
            this.txtMTSClientId.Name = "txtMTSClientId";
            this.txtMTSClientId.Size = new System.Drawing.Size(381, 20);
            this.txtMTSClientId.TabIndex = 2;
            // 
            // lblClientSecret
            // 
            this.lblClientSecret.AutoSize = true;
            this.lblClientSecret.Location = new System.Drawing.Point(9, 58);
            this.lblClientSecret.Name = "lblClientSecret";
            this.lblClientSecret.Size = new System.Drawing.Size(67, 13);
            this.lblClientSecret.TabIndex = 1;
            this.lblClientSecret.Text = "Client Secret";
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(9, 32);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(47, 13);
            this.lblClientId.TabIndex = 0;
            this.lblClientId.Text = "Client ID";
            // 
            // MTSSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpbxMicrosoftTranslatorSettings);
            this.Name = "MTSSettingsControl";
            this.Size = new System.Drawing.Size(475, 120);
            this.grpbxMicrosoftTranslatorSettings.ResumeLayout(false);
            this.grpbxMicrosoftTranslatorSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbxMicrosoftTranslatorSettings;
        private System.Windows.Forms.LinkLabel lnkGetStartedWithMicrosoftTranslator;
        private System.Windows.Forms.TextBox txtMTSClientSecret;
        private System.Windows.Forms.TextBox txtMTSClientId;
        private System.Windows.Forms.Label lblClientSecret;
        private System.Windows.Forms.Label lblClientId;
    }
}

namespace KWizCom.ResourceTranslator.Forms
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbxTranslationService = new System.Windows.Forms.ComboBox();
            this.lblTranslationService = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlSettingsContainer = new System.Windows.Forms.Panel();
            this.mtsSettingsControl1 = new KWizCom.ResourceTranslator.Controls.MTSSettingsControl();
            this.bingSettingsControl1 = new KWizCom.ResourceTranslator.Controls.BingSettingsControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlSettingsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(413, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbxTranslationService
            // 
            this.cmbxTranslationService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxTranslationService.FormattingEnabled = true;
            this.cmbxTranslationService.Items.AddRange(new object[] {
            "Microsoft Translation Service (Windows Azure Market Place)",
            "Bing Translation Service (Bing Application ID)"});
            this.cmbxTranslationService.Location = new System.Drawing.Point(113, 12);
            this.cmbxTranslationService.Name = "cmbxTranslationService";
            this.cmbxTranslationService.Size = new System.Drawing.Size(375, 21);
            this.cmbxTranslationService.TabIndex = 7;
            this.cmbxTranslationService.SelectedIndexChanged += new System.EventHandler(this.cmbxTranslationService_SelectedIndexChanged);
            // 
            // lblTranslationService
            // 
            this.lblTranslationService.AutoSize = true;
            this.lblTranslationService.Location = new System.Drawing.Point(9, 15);
            this.lblTranslationService.Name = "lblTranslationService";
            this.lblTranslationService.Size = new System.Drawing.Size(98, 13);
            this.lblTranslationService.TabIndex = 8;
            this.lblTranslationService.Text = "Translation Service";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(330, 174);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(77, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::KWizCom.ResourceTranslator.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 209);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // pnlSettingsContainer
            // 
            this.pnlSettingsContainer.Controls.Add(this.bingSettingsControl1);
            this.pnlSettingsContainer.Controls.Add(this.mtsSettingsControl1);
            this.pnlSettingsContainer.Location = new System.Drawing.Point(12, 39);
            this.pnlSettingsContainer.Name = "pnlSettingsContainer";
            this.pnlSettingsContainer.Size = new System.Drawing.Size(476, 125);
            this.pnlSettingsContainer.TabIndex = 11;
            // 
            // mtsSettingsControl1
            // 
            this.mtsSettingsControl1.ClientId = "";
            this.mtsSettingsControl1.ClientSecret = "";
            this.mtsSettingsControl1.Location = new System.Drawing.Point(3, 3);
            this.mtsSettingsControl1.Name = "mtsSettingsControl1";
            this.mtsSettingsControl1.Size = new System.Drawing.Size(475, 120);
            this.mtsSettingsControl1.TabIndex = 1;
            // 
            // bingSettingsControl1
            // 
            this.bingSettingsControl1.BingAppId = "";
            this.bingSettingsControl1.Location = new System.Drawing.Point(1, 3);
            this.bingSettingsControl1.Name = "bingSettingsControl1";
            this.bingSettingsControl1.Size = new System.Drawing.Size(475, 120);
            this.bingSettingsControl1.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 209);
            this.Controls.Add(this.pnlSettingsContainer);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblTranslationService);
            this.Controls.Add(this.cmbxTranslationService);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(506, 234);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(506, 234);
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlSettingsContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbxTranslationService;
        private System.Windows.Forms.Label lblTranslationService;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlSettingsContainer;
        private Controls.MTSSettingsControl mtsSettingsControl1;
        private Controls.BingSettingsControl bingSettingsControl1;
    }
}
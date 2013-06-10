namespace KWizCom.ResourceTranslator.Forms
{
    partial class EditResxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditResxForm));
            this.dgvResourceEntries = new System.Windows.Forms.DataGridView();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxTranslateTo = new System.Windows.Forms.ComboBox();
            this.lblTranslateTo = new System.Windows.Forms.Label();
            this.cbxTranslateFrom = new System.Windows.Forms.ComboBox();
            this.lblTanslateFrom = new System.Windows.Forms.Label();
            this.txtBoxOutPutFilename = new System.Windows.Forms.TextBox();
            this.lblOutPutFilename = new System.Windows.Forms.Label();
            this.chkIncludeAllEntries = new System.Windows.Forms.CheckBox();
            this.btnSuggest = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFilename = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblEntries = new System.Windows.Forms.Label();
            this.lblCharacters = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Translate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResourceEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResourceEntries
            // 
            this.dgvResourceEntries.AllowUserToAddRows = false;
            this.dgvResourceEntries.AllowUserToDeleteRows = false;
            this.dgvResourceEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResourceEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResourceEntries.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResourceEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResourceEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Translate,
            this.Key,
            this.OriginalValue});
            this.dgvResourceEntries.Location = new System.Drawing.Point(12, 154);
            this.dgvResourceEntries.Name = "dgvResourceEntries";
            this.dgvResourceEntries.Size = new System.Drawing.Size(680, 326);
            this.dgvResourceEntries.TabIndex = 0;
            this.dgvResourceEntries.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResourceEntries_CellValueChanged);
            this.dgvResourceEntries.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvResourceEntries_CurrentCellDirtyStateChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(12, 131);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(617, 647);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxTranslateTo
            // 
            this.cbxTranslateTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxTranslateTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTranslateTo.FormattingEnabled = true;
            this.cbxTranslateTo.Location = new System.Drawing.Point(190, 532);
            this.cbxTranslateTo.Name = "cbxTranslateTo";
            this.cbxTranslateTo.Size = new System.Drawing.Size(254, 21);
            this.cbxTranslateTo.TabIndex = 15;
            // 
            // lblTranslateTo
            // 
            this.lblTranslateTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTranslateTo.AutoSize = true;
            this.lblTranslateTo.Location = new System.Drawing.Point(22, 535);
            this.lblTranslateTo.Name = "lblTranslateTo";
            this.lblTranslateTo.Size = new System.Drawing.Size(167, 13);
            this.lblTranslateTo.TabIndex = 14;
            this.lblTranslateTo.Text = "Select Language To Translate To";
            // 
            // cbxTranslateFrom
            // 
            this.cbxTranslateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxTranslateFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTranslateFrom.FormattingEnabled = true;
            this.cbxTranslateFrom.Location = new System.Drawing.Point(190, 495);
            this.cbxTranslateFrom.Name = "cbxTranslateFrom";
            this.cbxTranslateFrom.Size = new System.Drawing.Size(254, 21);
            this.cbxTranslateFrom.TabIndex = 13;
            // 
            // lblTanslateFrom
            // 
            this.lblTanslateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTanslateFrom.AutoSize = true;
            this.lblTanslateFrom.Location = new System.Drawing.Point(12, 498);
            this.lblTanslateFrom.Name = "lblTanslateFrom";
            this.lblTanslateFrom.Size = new System.Drawing.Size(177, 13);
            this.lblTanslateFrom.TabIndex = 12;
            this.lblTanslateFrom.Text = "Select Language To Translate From";
            // 
            // txtBoxOutPutFilename
            // 
            this.txtBoxOutPutFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBoxOutPutFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxOutPutFilename.Location = new System.Drawing.Point(190, 569);
            this.txtBoxOutPutFilename.Name = "txtBoxOutPutFilename";
            this.txtBoxOutPutFilename.Size = new System.Drawing.Size(254, 20);
            this.txtBoxOutPutFilename.TabIndex = 16;
            // 
            // lblOutPutFilename
            // 
            this.lblOutPutFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutPutFilename.AutoSize = true;
            this.lblOutPutFilename.Location = new System.Drawing.Point(105, 572);
            this.lblOutPutFilename.Name = "lblOutPutFilename";
            this.lblOutPutFilename.Size = new System.Drawing.Size(84, 13);
            this.lblOutPutFilename.TabIndex = 17;
            this.lblOutPutFilename.Text = "Output Filename";
            // 
            // chkIncludeAllEntries
            // 
            this.chkIncludeAllEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIncludeAllEntries.AutoSize = true;
            this.chkIncludeAllEntries.Location = new System.Drawing.Point(190, 606);
            this.chkIncludeAllEntries.Name = "chkIncludeAllEntries";
            this.chkIncludeAllEntries.Size = new System.Drawing.Size(159, 17);
            this.chkIncludeAllEntries.TabIndex = 18;
            this.chkIncludeAllEntries.Text = "Include Untranslated Entries";
            this.chkIncludeAllEntries.UseVisualStyleBackColor = true;
            // 
            // btnSuggest
            // 
            this.btnSuggest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSuggest.Location = new System.Drawing.Point(450, 568);
            this.btnSuggest.Name = "btnSuggest";
            this.btnSuggest.Size = new System.Drawing.Size(87, 23);
            this.btnSuggest.TabIndex = 19;
            this.btnSuggest.Text = "Suggest Name";
            this.btnSuggest.UseVisualStyleBackColor = true;
            this.btnSuggest.Click += new System.EventHandler(this.btnSuggest_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::KWizCom.ResourceTranslator.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(704, 682);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 107);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resource File Info";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblFilename, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLocation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSize, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblEntries, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCharacters, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 86);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(3, 0);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(38, 13);
            this.lblFilename.TabIndex = 0;
            this.lblFilename.Text = "Name:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(3, 21);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(51, 13);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(3, 42);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 1);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "Size:";
            // 
            // lblEntries
            // 
            this.lblEntries.AutoSize = true;
            this.lblEntries.Location = new System.Drawing.Point(3, 42);
            this.lblEntries.Name = "lblEntries";
            this.lblEntries.Size = new System.Drawing.Size(42, 13);
            this.lblEntries.TabIndex = 3;
            this.lblEntries.Text = "Entries:";
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.Location = new System.Drawing.Point(3, 63);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(61, 13);
            this.lblCharacters.TabIndex = 4;
            this.lblCharacters.Text = "Characters:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 1);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "label5";
            // 
            // Translate
            // 
            this.Translate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Translate.DataPropertyName = "Translate";
            this.Translate.HeaderText = "Translate";
            this.Translate.MinimumWidth = 57;
            this.Translate.Name = "Translate";
            this.Translate.Width = 57;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Key.DataPropertyName = "Key";
            this.Key.HeaderText = "Key";
            this.Key.MinimumWidth = 175;
            this.Key.Name = "Key";
            this.Key.Width = 175;
            // 
            // OriginalValue
            // 
            this.OriginalValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OriginalValue.DataPropertyName = "OriginalValue";
            this.OriginalValue.HeaderText = "Original Value";
            this.OriginalValue.MinimumWidth = 400;
            this.OriginalValue.Name = "OriginalValue";
            this.OriginalValue.Width = 400;
            // 
            // EditResxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(704, 682);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvResourceEntries);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.cbxTranslateFrom);
            this.Controls.Add(this.btnSuggest);
            this.Controls.Add(this.lblTranslateTo);
            this.Controls.Add(this.cbxTranslateTo);
            this.Controls.Add(this.lblTanslateFrom);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBoxOutPutFilename);
            this.Controls.Add(this.lblOutPutFilename);
            this.Controls.Add(this.chkIncludeAllEntries);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(712, 709);
            this.Name = "EditResxForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Resource File";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResourceEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResourceEntries;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxTranslateTo;
        private System.Windows.Forms.Label lblTranslateTo;
        private System.Windows.Forms.ComboBox cbxTranslateFrom;
        private System.Windows.Forms.Label lblTanslateFrom;
        private System.Windows.Forms.TextBox txtBoxOutPutFilename;
        private System.Windows.Forms.Label lblOutPutFilename;
        private System.Windows.Forms.CheckBox chkIncludeAllEntries;
        private System.Windows.Forms.Button btnSuggest;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblEntries;
        private System.Windows.Forms.Label lblCharacters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Translate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalValue;

    }
}
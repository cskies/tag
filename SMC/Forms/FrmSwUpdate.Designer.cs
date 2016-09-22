namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSwUpdate
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numSSC = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbServiceSubtype = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbServiceType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbAck = new System.Windows.Forms.ComboBox();
            this.numSwMajor = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numSwMinor = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numSwPatch = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numPartsLength = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btOpenImageFile = new System.Windows.Forms.Button();
            this.txtInputFilePath = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.chkHasImageLength = new System.Windows.Forms.CheckBox();
            this.chkHasImageCrc = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtImageCrc = new System.Windows.Forms.TextBox();
            this.txtImageLength = new System.Windows.Forms.TextBox();
            this.numLduId = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNumberOfParts = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbOutputFormat = new System.Windows.Forms.ComboBox();
            this.btSaveParts = new System.Windows.Forms.Button();
            this.btSendParts = new System.Windows.Forms.Button();
            this.btSaveConfiguration = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btSaveNewImageFile = new System.Windows.Forms.Button();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.label18 = new System.Windows.Forms.Label();
            this.txtScriptPath = new System.Windows.Forms.TextBox();
            this.lblScriptPath = new System.Windows.Forms.Label();
            this.chkTestControlFormat = new System.Windows.Forms.CheckBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.lblMs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSSC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMajor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMinor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwPatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPartsLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLduId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(731, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = " Configuration";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(6, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(731, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Large Packet Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 26);
            this.label10.TabIndex = 57;
            this.label10.Text = "Sequence\r\nCount:";
            // 
            // numSSC
            // 
            this.numSSC.Location = new System.Drawing.Point(15, 132);
            this.numSSC.Maximum = new decimal(new int[] {
            16383,
            0,
            0,
            0});
            this.numSSC.Name = "numSSC";
            this.numSSC.Size = new System.Drawing.Size(57, 20);
            this.numSSC.TabIndex = 6;
            this.numSSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(465, 102);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(158, 13);
            this.label13.TabIndex = 64;
            this.label13.Text = "Service Subtype (large request):";
            // 
            // cmbServiceSubtype
            // 
            this.cmbServiceSubtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceSubtype.FormattingEnabled = true;
            this.cmbServiceSubtype.Location = new System.Drawing.Point(468, 131);
            this.cmbServiceSubtype.Name = "cmbServiceSubtype";
            this.cmbServiceSubtype.Size = new System.Drawing.Size(265, 21);
            this.cmbServiceSubtype.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(243, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 63;
            this.label12.Text = "Service Type:";
            // 
            // cmbServiceType
            // 
            this.cmbServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceType.FormattingEnabled = true;
            this.cmbServiceType.Location = new System.Drawing.Point(246, 131);
            this.cmbServiceType.Name = "cmbServiceType";
            this.cmbServiceType.Size = new System.Drawing.Size(216, 21);
            this.cmbServiceType.TabIndex = 8;
            this.cmbServiceType.SelectedIndexChanged += new System.EventHandler(this.cmbServiceType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(75, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 62;
            this.label11.Text = "Acknowledge:";
            // 
            // cmbAck
            // 
            this.cmbAck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAck.FormattingEnabled = true;
            this.cmbAck.Items.AddRange(new object[] {
            "None",
            "Acceptance",
            "Start of Execution [invalid]",
            "Progress of Execution [invalid]",
            "Completion of Execution",
            "Acceptance + Completion"});
            this.cmbAck.Location = new System.Drawing.Point(78, 131);
            this.cmbAck.Name = "cmbAck";
            this.cmbAck.Size = new System.Drawing.Size(162, 21);
            this.cmbAck.TabIndex = 7;
            // 
            // numSwMajor
            // 
            this.numSwMajor.Location = new System.Drawing.Point(12, 56);
            this.numSwMajor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwMajor.Name = "numSwMajor";
            this.numSwMajor.Size = new System.Drawing.Size(60, 20);
            this.numSwMajor.TabIndex = 0;
            this.numSwMajor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 26);
            this.label2.TabIndex = 57;
            this.label2.Text = "SW APL\r\nVersion:";
            // 
            // numSwMinor
            // 
            this.numSwMinor.Location = new System.Drawing.Point(78, 56);
            this.numSwMinor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwMinor.Name = "numSwMinor";
            this.numSwMinor.Size = new System.Drawing.Size(60, 20);
            this.numSwMinor.TabIndex = 1;
            this.numSwMinor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 26);
            this.label4.TabIndex = 57;
            this.label4.Text = "SW APL\r\nRelease:";
            // 
            // numSwPatch
            // 
            this.numSwPatch.Location = new System.Drawing.Point(144, 56);
            this.numSwPatch.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwPatch.Name = "numSwPatch";
            this.numSwPatch.Size = new System.Drawing.Size(60, 20);
            this.numSwPatch.TabIndex = 2;
            this.numSwPatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 26);
            this.label5.TabIndex = 57;
            this.label5.Text = "SW APL\r\nPatch:";
            // 
            // numPartsLength
            // 
            this.numPartsLength.Location = new System.Drawing.Point(646, 56);
            this.numPartsLength.Maximum = new decimal(new int[] {
            236,
            0,
            0,
            0});
            this.numPartsLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPartsLength.Name = "numPartsLength";
            this.numPartsLength.Size = new System.Drawing.Size(60, 20);
            this.numPartsLength.TabIndex = 5;
            this.numPartsLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numPartsLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPartsLength.ValueChanged += new System.EventHandler(this.numPartsLength_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(643, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 26);
            this.label6.TabIndex = 57;
            this.label6.Text = "Parts Length \r\n(including header):";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(6, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(731, 15);
            this.label7.TabIndex = 38;
            this.label7.Text = "Large Packet Data Field (SW image to send)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btOpenImageFile
            // 
            this.btOpenImageFile.Location = new System.Drawing.Point(552, 199);
            this.btOpenImageFile.Name = "btOpenImageFile";
            this.btOpenImageFile.Size = new System.Drawing.Size(181, 23);
            this.btOpenImageFile.TabIndex = 11;
            this.btOpenImageFile.Text = "Open Flight Software Image File";
            this.btOpenImageFile.UseVisualStyleBackColor = true;
            this.btOpenImageFile.Click += new System.EventHandler(this.btOpenImageFile_Click);
            // 
            // txtInputFilePath
            // 
            this.txtInputFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtInputFilePath.Location = new System.Drawing.Point(15, 202);
            this.txtInputFilePath.Name = "txtInputFilePath";
            this.txtInputFilePath.ReadOnly = true;
            this.txtInputFilePath.Size = new System.Drawing.Size(531, 20);
            this.txtInputFilePath.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 182);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(158, 13);
            this.label19.TabIndex = 67;
            this.label19.Text = "Flight Software Image Input File:";
            // 
            // chkHasImageLength
            // 
            this.chkHasImageLength.AutoSize = true;
            this.chkHasImageLength.Location = new System.Drawing.Point(15, 228);
            this.chkHasImageLength.Name = "chkHasImageLength";
            this.chkHasImageLength.Size = new System.Drawing.Size(498, 17);
            this.chkHasImageLength.TabIndex = 12;
            this.chkHasImageLength.Text = "Image length present at the beggining; check to add if not present in the file (d" +
    "oesn\'t change the file)";
            this.chkHasImageLength.UseVisualStyleBackColor = true;
            this.chkHasImageLength.CheckedChanged += new System.EventHandler(this.chkHasImageLength_CheckedChanged);
            // 
            // chkHasImageCrc
            // 
            this.chkHasImageCrc.AutoSize = true;
            this.chkHasImageCrc.Location = new System.Drawing.Point(15, 255);
            this.chkHasImageCrc.Name = "chkHasImageCrc";
            this.chkHasImageCrc.Size = new System.Drawing.Size(478, 17);
            this.chkHasImageCrc.TabIndex = 14;
            this.chkHasImageCrc.Text = "Image CRC-32 present at the end; check to add if not present in the file (doesn\'t" +
    " change the file)";
            this.chkHasImageCrc.UseVisualStyleBackColor = true;
            this.chkHasImageCrc.CheckedChanged += new System.EventHandler(this.chkHasImageCrc_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(549, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "Image Length:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(549, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Image CRC-32:";
            // 
            // txtImageCrc
            // 
            this.txtImageCrc.BackColor = System.Drawing.SystemColors.Window;
            this.txtImageCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageCrc.ForeColor = System.Drawing.Color.Red;
            this.txtImageCrc.Location = new System.Drawing.Point(630, 252);
            this.txtImageCrc.Name = "txtImageCrc";
            this.txtImageCrc.ReadOnly = true;
            this.txtImageCrc.Size = new System.Drawing.Size(103, 20);
            this.txtImageCrc.TabIndex = 15;
            this.txtImageCrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtImageLength
            // 
            this.txtImageLength.BackColor = System.Drawing.SystemColors.Window;
            this.txtImageLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageLength.ForeColor = System.Drawing.Color.ForestGreen;
            this.txtImageLength.Location = new System.Drawing.Point(630, 229);
            this.txtImageLength.Name = "txtImageLength";
            this.txtImageLength.ReadOnly = true;
            this.txtImageLength.Size = new System.Drawing.Size(103, 20);
            this.txtImageLength.TabIndex = 13;
            this.txtImageLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numLduId
            // 
            this.numLduId.Location = new System.Drawing.Point(577, 56);
            this.numLduId.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numLduId.Name = "numLduId";
            this.numLduId.Size = new System.Drawing.Size(60, 20);
            this.numLduId.TabIndex = 4;
            this.numLduId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(574, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 26);
            this.label14.TabIndex = 57;
            this.label14.Text = "Large Data \r\nUnit ID:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.Window;
            this.label15.Location = new System.Drawing.Point(6, 345);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(731, 15);
            this.label15.TabIndex = 38;
            this.label15.Text = "Output (parts)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 371);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(166, 13);
            this.label16.TabIndex = 67;
            this.label16.Text = "Number of Parts to be Generated:";
            // 
            // txtNumberOfParts
            // 
            this.txtNumberOfParts.BackColor = System.Drawing.SystemColors.Window;
            this.txtNumberOfParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfParts.ForeColor = System.Drawing.Color.Blue;
            this.txtNumberOfParts.Location = new System.Drawing.Point(184, 368);
            this.txtNumberOfParts.Name = "txtNumberOfParts";
            this.txtNumberOfParts.ReadOnly = true;
            this.txtNumberOfParts.Size = new System.Drawing.Size(53, 20);
            this.txtNumberOfParts.TabIndex = 19;
            this.txtNumberOfParts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(210, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 13);
            this.label17.TabIndex = 67;
            this.label17.Text = "Output Format:";
            // 
            // cmbOutputFormat
            // 
            this.cmbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputFormat.FormattingEnabled = true;
            this.cmbOutputFormat.Items.AddRange(new object[] {
            "SDU Parts (data field only)",
            "Service Request Packets",
            "CLTUs (it will be used the TC Frames Header information saved in DB)",
            "Amazonia-1 SATCS / Test Control Scripts"});
            this.cmbOutputFormat.Location = new System.Drawing.Point(210, 55);
            this.cmbOutputFormat.Name = "cmbOutputFormat";
            this.cmbOutputFormat.Size = new System.Drawing.Size(361, 21);
            this.cmbOutputFormat.TabIndex = 3;
            this.cmbOutputFormat.SelectedIndexChanged += new System.EventHandler(this.cmbOutputFormat_SelectedIndexChanged);
            // 
            // btSaveParts
            // 
            this.btSaveParts.Location = new System.Drawing.Point(246, 366);
            this.btSaveParts.Name = "btSaveParts";
            this.btSaveParts.Size = new System.Drawing.Size(109, 23);
            this.btSaveParts.TabIndex = 20;
            this.btSaveParts.Text = "Save Parts Files";
            this.btSaveParts.UseVisualStyleBackColor = true;
            this.btSaveParts.Click += new System.EventHandler(this.btSaveParts_Click);
            // 
            // btSendParts
            // 
            this.btSendParts.Enabled = false;
            this.btSendParts.Location = new System.Drawing.Point(510, 366);
            this.btSendParts.Name = "btSendParts";
            this.btSendParts.Size = new System.Drawing.Size(105, 23);
            this.btSendParts.TabIndex = 22;
            this.btSendParts.Text = "Send Parts Now";
            this.btSendParts.UseVisualStyleBackColor = true;
            this.btSendParts.Click += new System.EventHandler(this.btSendParts_Click);
            // 
            // btSaveConfiguration
            // 
            this.btSaveConfiguration.Location = new System.Drawing.Point(621, 366);
            this.btSaveConfiguration.Name = "btSaveConfiguration";
            this.btSaveConfiguration.Size = new System.Drawing.Size(112, 23);
            this.btSaveConfiguration.TabIndex = 23;
            this.btSaveConfiguration.Text = "Save Configuration";
            this.btSaveConfiguration.UseVisualStyleBackColor = true;
            this.btSaveConfiguration.Click += new System.EventHandler(this.btSaveConfiguration_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // btSaveNewImageFile
            // 
            this.btSaveNewImageFile.Location = new System.Drawing.Point(361, 366);
            this.btSaveNewImageFile.Name = "btSaveNewImageFile";
            this.btSaveNewImageFile.Size = new System.Drawing.Size(143, 23);
            this.btSaveNewImageFile.TabIndex = 21;
            this.btSaveNewImageFile.Text = "Save New Image File";
            this.btSaveNewImageFile.UseVisualStyleBackColor = true;
            this.btSaveNewImageFile.Click += new System.EventHandler(this.btSaveNewImageFile_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.Window;
            this.label18.Location = new System.Drawing.Point(6, 278);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(731, 15);
            this.label18.TabIndex = 68;
            this.label18.Text = "Script Options";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtScriptPath
            // 
            this.txtScriptPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtScriptPath.Location = new System.Drawing.Point(12, 318);
            this.txtScriptPath.MaxLength = 100;
            this.txtScriptPath.Name = "txtScriptPath";
            this.txtScriptPath.Size = new System.Drawing.Size(427, 20);
            this.txtScriptPath.TabIndex = 16;
            // 
            // lblScriptPath
            // 
            this.lblScriptPath.AutoSize = true;
            this.lblScriptPath.Location = new System.Drawing.Point(9, 298);
            this.lblScriptPath.Name = "lblScriptPath";
            this.lblScriptPath.Size = new System.Drawing.Size(295, 13);
            this.lblScriptPath.TabIndex = 70;
            this.lblScriptPath.Text = "Path for Script Files on SATCS Computer (NOT a local path!):";
            // 
            // chkTestControlFormat
            // 
            this.chkTestControlFormat.AutoSize = true;
            this.chkTestControlFormat.Location = new System.Drawing.Point(556, 313);
            this.chkTestControlFormat.Name = "chkTestControlFormat";
            this.chkTestControlFormat.Size = new System.Drawing.Size(184, 30);
            this.chkTestControlFormat.TabIndex = 18;
            this.chkTestControlFormat.Text = "Use Test Control format for paths \r\n(hex instead of ascii)";
            this.chkTestControlFormat.UseVisualStyleBackColor = true;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(443, 298);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(103, 13);
            this.lblDelay.TabIndex = 72;
            this.lblDelay.Text = "Delay between TCs:";
            // 
            // numDelay
            // 
            this.numDelay.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numDelay.Location = new System.Drawing.Point(445, 318);
            this.numDelay.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(78, 20);
            this.numDelay.TabIndex = 17;
            this.numDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numDelay.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lblMs
            // 
            this.lblMs.AutoSize = true;
            this.lblMs.Location = new System.Drawing.Point(526, 321);
            this.lblMs.Name = "lblMs";
            this.lblMs.Size = new System.Drawing.Size(20, 13);
            this.lblMs.TabIndex = 73;
            this.lblMs.Text = "ms";
            // 
            // FrmSwUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(742, 398);
            this.Controls.Add(this.lblMs);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.numDelay);
            this.Controls.Add(this.chkTestControlFormat);
            this.Controls.Add(this.txtScriptPath);
            this.Controls.Add(this.lblScriptPath);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btSaveNewImageFile);
            this.Controls.Add(this.chkHasImageCrc);
            this.Controls.Add(this.chkHasImageLength);
            this.Controls.Add(this.btSendParts);
            this.Controls.Add(this.btSaveConfiguration);
            this.Controls.Add(this.btSaveParts);
            this.Controls.Add(this.btOpenImageFile);
            this.Controls.Add(this.txtNumberOfParts);
            this.Controls.Add(this.txtImageLength);
            this.Controls.Add(this.txtImageCrc);
            this.Controls.Add(this.txtInputFilePath);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbServiceSubtype);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbServiceType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbOutputFormat);
            this.Controls.Add(this.cmbAck);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numLduId);
            this.Controls.Add(this.numPartsLength);
            this.Controls.Add(this.numSwPatch);
            this.Controls.Add(this.numSwMinor);
            this.Controls.Add(this.numSwMajor);
            this.Controls.Add(this.numSSC);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSwUpdate";
            this.Text = "In-Flight Software Update";
            this.Load += new System.EventHandler(this.FrmSwUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSSC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMajor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMinor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwPatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPartsLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLduId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.NumericUpDown numSSC;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.ComboBox cmbServiceSubtype;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbServiceType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbAck;
        public System.Windows.Forms.NumericUpDown numSwMajor;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numSwMinor;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numSwPatch;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown numPartsLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btOpenImageFile;
        private System.Windows.Forms.TextBox txtInputFilePath;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox chkHasImageLength;
        private System.Windows.Forms.CheckBox chkHasImageCrc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtImageCrc;
        private System.Windows.Forms.TextBox txtImageLength;
        public System.Windows.Forms.NumericUpDown numLduId;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNumberOfParts;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbOutputFormat;
        private System.Windows.Forms.Button btSaveParts;
        private System.Windows.Forms.Button btSendParts;
        private System.Windows.Forms.Button btSaveConfiguration;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Button btSaveNewImageFile;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtScriptPath;
        private System.Windows.Forms.Label lblScriptPath;
        private System.Windows.Forms.CheckBox chkTestControlFormat;
        private System.Windows.Forms.Label lblDelay;
        public System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Label lblMs;
    }
}
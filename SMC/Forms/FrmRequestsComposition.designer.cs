namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmRequestsComposition
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.dialog = new System.Windows.Forms.OpenFileDialog();
            this.chkAutoIncrement = new System.Windows.Forms.CheckBox();
            this.lblN = new System.Windows.Forms.Label();
            this.numN = new System.Windows.Forms.NumericUpDown();
            this.chkDataHeaderFlag = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbServiceSubtype = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbServiceType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbAck = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numSSC = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGroupingFlags = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbAPID = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPacketType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mskVersion = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridAppData = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRawPacket = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.chkAutoLength = new System.Windows.Forms.CheckBox();
            this.mskPacketLength = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkAutoCrc = new System.Windows.Forms.CheckBox();
            this.mskPacketCrc = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btSaveRequest = new System.Windows.Forms.Button();
            this.tmrReportDefinition = new System.Windows.Forms.Timer(this.components);
            this.lblPackCurrentSize = new System.Windows.Forms.Label();
            this.txtPacketCurrentSize = new System.Windows.Forms.TextBox();
            this.lblPacketSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSSC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAppData)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(471, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 24);
            this.button1.TabIndex = 18;
            this.button1.Text = "Save Configuration";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.comboBox1.Location = new System.Drawing.Point(75, 110);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(76, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(5, 113);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 16);
            this.label22.TabIndex = 12;
            this.label22.Text = "COM Port";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBox2.Location = new System.Drawing.Point(75, 134);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(76, 21);
            this.comboBox2.TabIndex = 13;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(5, 137);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 19);
            this.label23.TabIndex = 14;
            this.label23.Text = "Baud Rate";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(5, 161);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(64, 16);
            this.label24.TabIndex = 9;
            this.label24.Text = "data Bits";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboBox3.Location = new System.Drawing.Point(75, 158);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(76, 21);
            this.comboBox3.TabIndex = 15;
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBox4.Location = new System.Drawing.Point(75, 206);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(76, 21);
            this.comboBox4.TabIndex = 17;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(5, 209);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(64, 16);
            this.label25.TabIndex = 10;
            this.label25.Text = "Stop Bits";
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None"});
            this.comboBox5.Location = new System.Drawing.Point(75, 182);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(76, 21);
            this.comboBox5.TabIndex = 16;
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(5, 185);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 16);
            this.label26.TabIndex = 11;
            this.label26.Text = "Parity";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label27.Location = new System.Drawing.Point(3, 88);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(580, 15);
            this.label27.TabIndex = 5;
            this.label27.Text = "Serial Port";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label28.Location = new System.Drawing.Point(3, 7);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(580, 15);
            this.label28.TabIndex = 4;
            this.label28.Text = "Database";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAutoIncrement
            // 
            this.chkAutoIncrement.AutoSize = true;
            this.chkAutoIncrement.Location = new System.Drawing.Point(547, 83);
            this.chkAutoIncrement.Name = "chkAutoIncrement";
            this.chkAutoIncrement.Size = new System.Drawing.Size(47, 17);
            this.chkAutoIncrement.TabIndex = 6;
            this.chkAutoIncrement.Text = "auto";
            this.chkAutoIncrement.UseVisualStyleBackColor = true;
            this.chkAutoIncrement.CheckedChanged += new System.EventHandler(this.chkAutoIncrement_CheckedChanged);
            // 
            // lblN
            // 
            this.lblN.AutoSize = true;
            this.lblN.Location = new System.Drawing.Point(661, 122);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(43, 13);
            this.lblN.TabIndex = 59;
            this.lblN.Text = "N Field:";
            // 
            // numN
            // 
            this.numN.Location = new System.Drawing.Point(664, 138);
            this.numN.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numN.Name = "numN";
            this.numN.Size = new System.Drawing.Size(73, 20);
            this.numN.TabIndex = 14;
            this.numN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numN.ValueChanged += new System.EventHandler(this.numN_ValueChanged);
            // 
            // chkDataHeaderFlag
            // 
            this.chkDataHeaderFlag.AutoSize = true;
            this.chkDataHeaderFlag.Checked = true;
            this.chkDataHeaderFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDataHeaderFlag.Enabled = false;
            this.chkDataHeaderFlag.Location = new System.Drawing.Point(130, 60);
            this.chkDataHeaderFlag.Name = "chkDataHeaderFlag";
            this.chkDataHeaderFlag.Size = new System.Drawing.Size(15, 14);
            this.chkDataHeaderFlag.TabIndex = 2;
            this.chkDataHeaderFlag.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(397, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 13);
            this.label13.TabIndex = 58;
            this.label13.Text = "Service Subtype (request):";
            // 
            // cmbServiceSubtype
            // 
            this.cmbServiceSubtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceSubtype.FormattingEnabled = true;
            this.cmbServiceSubtype.Location = new System.Drawing.Point(400, 138);
            this.cmbServiceSubtype.Name = "cmbServiceSubtype";
            this.cmbServiceSubtype.Size = new System.Drawing.Size(254, 21);
            this.cmbServiceSubtype.TabIndex = 13;
            this.cmbServiceSubtype.SelectedIndexChanged += new System.EventHandler(this.cmbServiceSubtype_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(175, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Service Type:";
            // 
            // cmbServiceType
            // 
            this.cmbServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceType.FormattingEnabled = true;
            this.cmbServiceType.Location = new System.Drawing.Point(178, 138);
            this.cmbServiceType.Name = "cmbServiceType";
            this.cmbServiceType.Size = new System.Drawing.Size(216, 21);
            this.cmbServiceType.TabIndex = 12;
            this.cmbServiceType.SelectedIndexChanged += new System.EventHandler(this.cmbServiceType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 56;
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
            this.cmbAck.Location = new System.Drawing.Point(10, 138);
            this.cmbAck.Name = "cmbAck";
            this.cmbAck.Size = new System.Drawing.Size(162, 21);
            this.cmbAck.TabIndex = 11;
            this.cmbAck.SelectedIndexChanged += new System.EventHandler(this.cmbAck_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(544, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 26);
            this.label10.TabIndex = 55;
            this.label10.Text = "Sequence\r\nCount:";
            // 
            // numSSC
            // 
            this.numSSC.Location = new System.Drawing.Point(547, 58);
            this.numSSC.Maximum = new decimal(new int[] {
            16383,
            0,
            0,
            0});
            this.numSSC.Name = "numSSC";
            this.numSSC.Size = new System.Drawing.Size(53, 20);
            this.numSSC.TabIndex = 5;
            this.numSSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numSSC.ValueChanged += new System.EventHandler(this.numSSC_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(402, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Grouping Flags:";
            // 
            // cmbGroupingFlags
            // 
            this.cmbGroupingFlags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupingFlags.FormattingEnabled = true;
            this.cmbGroupingFlags.Items.AddRange(new object[] {
            "First Packet [invalid]",
            "Continuation Packet [invalid]",
            "Last Packet [invalid]",
            "Stand-Alone Packet"});
            this.cmbGroupingFlags.Location = new System.Drawing.Point(402, 57);
            this.cmbGroupingFlags.Name = "cmbGroupingFlags";
            this.cmbGroupingFlags.Size = new System.Drawing.Size(139, 21);
            this.cmbGroupingFlags.TabIndex = 4;
            this.cmbGroupingFlags.SelectedIndexChanged += new System.EventHandler(this.cmbGroupingFlags_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Application Process ID (APID):";
            // 
            // cmbAPID
            // 
            this.cmbAPID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAPID.FormattingEnabled = true;
            this.cmbAPID.Location = new System.Drawing.Point(178, 57);
            this.cmbAPID.Name = "cmbAPID";
            this.cmbAPID.Size = new System.Drawing.Size(218, 21);
            this.cmbAPID.TabIndex = 3;
            this.cmbAPID.SelectedIndexChanged += new System.EventHandler(this.cmbAPID_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 26);
            this.label7.TabIndex = 51;
            this.label7.Text = "Data Field\r\nHeader Flag:";
            // 
            // cmbPacketType
            // 
            this.cmbPacketType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacketType.FormattingEnabled = true;
            this.cmbPacketType.Items.AddRange(new object[] {
            "TC",
            "TM"});
            this.cmbPacketType.Location = new System.Drawing.Point(60, 57);
            this.cmbPacketType.Name = "cmbPacketType";
            this.cmbPacketType.Size = new System.Drawing.Size(41, 21);
            this.cmbPacketType.TabIndex = 1;
            this.cmbPacketType.SelectedIndexChanged += new System.EventHandler(this.cmbPacketType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 26);
            this.label6.TabIndex = 47;
            this.label6.Text = "Packet\r\nType:";
            // 
            // mskVersion
            // 
            this.mskVersion.Location = new System.Drawing.Point(10, 57);
            this.mskVersion.Name = "mskVersion";
            this.mskVersion.PromptChar = ' ';
            this.mskVersion.ReadOnly = true;
            this.mskVersion.Size = new System.Drawing.Size(44, 20);
            this.mskVersion.TabIndex = 0;
            this.mskVersion.TabStop = false;
            this.mskVersion.Text = "0";
            this.mskVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 26);
            this.label5.TabIndex = 44;
            this.label5.Text = "Version\r\nNumber:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(3, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(734, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "Request to Send";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridAppData
            // 
            this.gridAppData.AllowUserToAddRows = false;
            this.gridAppData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridAppData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridAppData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAppData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAppData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridAppData.Location = new System.Drawing.Point(5, 188);
            this.gridAppData.MultiSelect = false;
            this.gridAppData.Name = "gridAppData";
            this.gridAppData.RowHeadersVisible = false;
            this.gridAppData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAppData.Size = new System.Drawing.Size(733, 157);
            this.gridAppData.TabIndex = 15;
            this.gridAppData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAppData_CellContentClick);
            this.gridAppData.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAppData_CellEnter);
            this.gridAppData.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAppData_CellMouseEnter);
            this.gridAppData.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAppData_CellMouseLeave);
            this.gridAppData.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAppData_CellValidated);
            this.gridAppData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridAppData_CellValidating);
            this.gridAppData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridAppData_MouseClick);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(4, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(734, 15);
            this.label4.TabIndex = 39;
            this.label4.Text = "Application Data";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.label3.Location = new System.Drawing.Point(4, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(734, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "Packet Data Field";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(650, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Packet Header";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRawPacket
            // 
            this.txtRawPacket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRawPacket.BackColor = System.Drawing.SystemColors.Window;
            this.txtRawPacket.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawPacket.Location = new System.Drawing.Point(3, 373);
            this.txtRawPacket.Multiline = true;
            this.txtRawPacket.Name = "txtRawPacket";
            this.txtRawPacket.ReadOnly = true;
            this.txtRawPacket.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRawPacket.Size = new System.Drawing.Size(734, 76);
            this.txtRawPacket.TabIndex = 16;
            this.txtRawPacket.TabStop = false;
            // 
            // btSend
            // 
            this.btSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSend.Enabled = false;
            this.btSend.Location = new System.Drawing.Point(627, 455);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(110, 24);
            this.btSend.TabIndex = 18;
            this.btSend.Text = "Send Request";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // chkAutoLength
            // 
            this.chkAutoLength.AutoSize = true;
            this.chkAutoLength.Checked = true;
            this.chkAutoLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoLength.Location = new System.Drawing.Point(606, 84);
            this.chkAutoLength.Name = "chkAutoLength";
            this.chkAutoLength.Size = new System.Drawing.Size(47, 17);
            this.chkAutoLength.TabIndex = 8;
            this.chkAutoLength.TabStop = false;
            this.chkAutoLength.Text = "auto";
            this.chkAutoLength.UseVisualStyleBackColor = true;
            this.chkAutoLength.CheckedChanged += new System.EventHandler(this.chkAutoLength_CheckedChanged);
            // 
            // mskPacketLength
            // 
            this.mskPacketLength.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskPacketLength.Location = new System.Drawing.Point(606, 58);
            this.mskPacketLength.Mask = "#####";
            this.mskPacketLength.Name = "mskPacketLength";
            this.mskPacketLength.PromptChar = ' ';
            this.mskPacketLength.ReadOnly = true;
            this.mskPacketLength.Size = new System.Drawing.Size(46, 20);
            this.mskPacketLength.TabIndex = 7;
            this.mskPacketLength.Text = "0";
            this.mskPacketLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskPacketLength.Leave += new System.EventHandler(this.mskPacketLength_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(606, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 26);
            this.label14.TabIndex = 63;
            this.label14.Text = "Packet\r\nLength";
            // 
            // chkAutoCrc
            // 
            this.chkAutoCrc.AutoSize = true;
            this.chkAutoCrc.Checked = true;
            this.chkAutoCrc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCrc.Location = new System.Drawing.Point(664, 83);
            this.chkAutoCrc.Name = "chkAutoCrc";
            this.chkAutoCrc.Size = new System.Drawing.Size(47, 17);
            this.chkAutoCrc.TabIndex = 10;
            this.chkAutoCrc.TabStop = false;
            this.chkAutoCrc.Text = "auto";
            this.chkAutoCrc.UseVisualStyleBackColor = true;
            this.chkAutoCrc.CheckedChanged += new System.EventHandler(this.chkAutoCrc_CheckedChanged);
            // 
            // mskPacketCrc
            // 
            this.mskPacketCrc.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskPacketCrc.Location = new System.Drawing.Point(664, 58);
            this.mskPacketCrc.Mask = "AA-AA";
            this.mskPacketCrc.Name = "mskPacketCrc";
            this.mskPacketCrc.PromptChar = ' ';
            this.mskPacketCrc.ReadOnly = true;
            this.mskPacketCrc.Size = new System.Drawing.Size(74, 20);
            this.mskPacketCrc.TabIndex = 9;
            this.mskPacketCrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskPacketCrc.Leave += new System.EventHandler(this.mskPacketCrc_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(661, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 13);
            this.label15.TabIndex = 65;
            this.label15.Text = "Packet CRC";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.Window;
            this.label16.Location = new System.Drawing.Point(664, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 15);
            this.label16.TabIndex = 64;
            this.label16.Text = "Error Control";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btSaveRequest
            // 
            this.btSaveRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveRequest.Location = new System.Drawing.Point(499, 455);
            this.btSaveRequest.Name = "btSaveRequest";
            this.btSaveRequest.Size = new System.Drawing.Size(110, 24);
            this.btSaveRequest.TabIndex = 17;
            this.btSaveRequest.Text = "Save Request";
            this.btSaveRequest.UseVisualStyleBackColor = true;
            this.btSaveRequest.Click += new System.EventHandler(this.btSaveRequest_Click);
            // 
            // tmrReportDefinition
            // 
            this.tmrReportDefinition.Interval = 5;
            this.tmrReportDefinition.Tick += new System.EventHandler(this.tmrReportDefinition_Tick);
            // 
            // lblPackCurrentSize
            // 
            this.lblPackCurrentSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPackCurrentSize.AutoSize = true;
            this.lblPackCurrentSize.Location = new System.Drawing.Point(2, 458);
            this.lblPackCurrentSize.Name = "lblPackCurrentSize";
            this.lblPackCurrentSize.Size = new System.Drawing.Size(104, 13);
            this.lblPackCurrentSize.TabIndex = 66;
            this.lblPackCurrentSize.Text = "Current Packet Size:";
            // 
            // txtPacketCurrentSize
            // 
            this.txtPacketCurrentSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPacketCurrentSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPacketCurrentSize.Location = new System.Drawing.Point(112, 455);
            this.txtPacketCurrentSize.Name = "txtPacketCurrentSize";
            this.txtPacketCurrentSize.ReadOnly = true;
            this.txtPacketCurrentSize.Size = new System.Drawing.Size(61, 20);
            this.txtPacketCurrentSize.TabIndex = 67;
            this.txtPacketCurrentSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPacketCurrentSize.TextChanged += new System.EventHandler(this.txtPacketCurrentSize_TextChanged);
            // 
            // lblPacketSize
            // 
            this.lblPacketSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPacketSize.AutoSize = true;
            this.lblPacketSize.Location = new System.Drawing.Point(233, 458);
            this.lblPacketSize.Name = "lblPacketSize";
            this.lblPacketSize.Size = new System.Drawing.Size(163, 13);
            this.lblPacketSize.TabIndex = 68;
            this.lblPacketSize.Text = "Maximum Packet Size: 248 bytes";
            // 
            // FrmRequestsComposition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(743, 483);
            this.Controls.Add(this.lblPacketSize);
            this.Controls.Add(this.txtPacketCurrentSize);
            this.Controls.Add(this.lblPackCurrentSize);
            this.Controls.Add(this.btSaveRequest);
            this.Controls.Add(this.chkAutoCrc);
            this.Controls.Add(this.mskPacketCrc);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.chkAutoLength);
            this.Controls.Add(this.mskPacketLength);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.chkAutoIncrement);
            this.Controls.Add(this.lblN);
            this.Controls.Add(this.numN);
            this.Controls.Add(this.chkDataHeaderFlag);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbServiceSubtype);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbServiceType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbAck);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numSSC);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbGroupingFlags);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbAPID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbPacketType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mskVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridAppData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRawPacket);
            this.Controls.Add(this.btSend);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmRequestsComposition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Requests Composition";
            this.DockStateChanged += new System.EventHandler(this.FrmTcsSending_DockStateChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTcsSending_FormClosed);
            this.Load += new System.EventHandler(this.FrmTcsSending_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSSC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAppData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.ComboBox comboBox1;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.ComboBox comboBox2;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.ComboBox comboBox3;
        internal System.Windows.Forms.ComboBox comboBox4;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.ComboBox comboBox5;
        internal System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.OpenFileDialog dialog;
        public System.Windows.Forms.Label lblN;
        private System.Windows.Forms.CheckBox chkDataHeaderFlag;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox mskVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button btSaveRequest;
        private System.Windows.Forms.Timer tmrReportDefinition;
        private System.Windows.Forms.ComboBox cmbAPID;
        private System.Windows.Forms.ComboBox cmbPacketType;
        private System.Windows.Forms.ComboBox cmbServiceType;
        private System.Windows.Forms.TextBox txtRawPacket;
        private System.Windows.Forms.ComboBox cmbGroupingFlags;
        private System.Windows.Forms.ComboBox cmbAck;
        private System.Windows.Forms.DataGridView gridAppData;
        private System.Windows.Forms.MaskedTextBox mskPacketLength;
        private System.Windows.Forms.MaskedTextBox mskPacketCrc;
        private System.Windows.Forms.CheckBox chkAutoLength;
        private System.Windows.Forms.CheckBox chkAutoCrc;
        public System.Windows.Forms.NumericUpDown numN;
        public System.Windows.Forms.NumericUpDown numSSC;
        public System.Windows.Forms.CheckBox chkAutoIncrement;
        public System.Windows.Forms.ComboBox cmbServiceSubtype;
        private System.Windows.Forms.Label lblPackCurrentSize;
        private System.Windows.Forms.TextBox txtPacketCurrentSize;
        private System.Windows.Forms.Label lblPacketSize;
    }
}


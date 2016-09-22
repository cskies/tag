namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmUatMonitor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.btSave = new System.Windows.Forms.Button();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSendSoft = new System.Windows.Forms.Button();
            this.gpBoxFileFormat = new System.Windows.Forms.GroupBox();
            this.btFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btRunSw = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btWriteMemory = new System.Windows.Forms.Button();
            this.btGetData = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAddressMemBlock = new System.Windows.Forms.TextBox();
            this.gridMemorySwicher = new System.Windows.Forms.DataGridView();
            this.Byte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btReadMemory = new System.Windows.Forms.Button();
            this.txtNumOfBytes = new System.Windows.Forms.TextBox();
            this.txtInitialAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.btStartListenUat = new System.Windows.Forms.Button();
            this.btClearScreen = new System.Windows.Forms.Button();
            this.btSendCommand = new System.Windows.Forms.Button();
            this.txtMessageToSendUat = new System.Windows.Forms.TextBox();
            this.txtUatMessage = new System.Windows.Forms.TextBox();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.gpBoxFileFormat.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMemorySwicher)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStopBits);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.btSave);
            this.groupBox1.Controls.Add(this.cmbDataBits);
            this.groupBox1.Controls.Add(this.cmbBaudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbComPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RS-232 Serial port configuration";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbStopBits.Location = new System.Drawing.Point(254, 72);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(120, 21);
            this.cmbStopBits.TabIndex = 4;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None"});
            this.cmbParity.Location = new System.Drawing.Point(254, 45);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(120, 21);
            this.cmbParity.TabIndex = 3;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(88, 106);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(202, 23);
            this.btSave.TabIndex = 15;
            this.btSave.Text = "Save Configuration";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cmbDataBits.Location = new System.Drawing.Point(254, 18);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(120, 21);
            this.cmbDataBits.TabIndex = 2;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(71, 45);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(105, 21);
            this.cmbBaudRate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Stop bits:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Parity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data bits:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baud rate:";
            // 
            // cmbComPort
            // 
            this.cmbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(71, 18);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(105, 21);
            this.cmbComPort.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM port:";
            // 
            // btSendSoft
            // 
            this.btSendSoft.Enabled = false;
            this.btSendSoft.Location = new System.Drawing.Point(402, 68);
            this.btSendSoft.Name = "btSendSoft";
            this.btSendSoft.Size = new System.Drawing.Size(202, 50);
            this.btSendSoft.TabIndex = 14;
            this.btSendSoft.Text = "Load Software to UAT";
            this.btSendSoft.UseVisualStyleBackColor = true;
            this.btSendSoft.Click += new System.EventHandler(this.btSendSoft_Click);
            // 
            // gpBoxFileFormat
            // 
            this.gpBoxFileFormat.Controls.Add(this.btFile);
            this.gpBoxFileFormat.Controls.Add(this.txtFilePath);
            this.gpBoxFileFormat.Location = new System.Drawing.Point(6, 10);
            this.gpBoxFileFormat.Name = "gpBoxFileFormat";
            this.gpBoxFileFormat.Size = new System.Drawing.Size(598, 52);
            this.gpBoxFileFormat.TabIndex = 16;
            this.gpBoxFileFormat.TabStop = false;
            this.gpBoxFileFormat.Text = "File Format";
            // 
            // btFile
            // 
            this.btFile.Location = new System.Drawing.Point(564, 19);
            this.btFile.Name = "btFile";
            this.btFile.Size = new System.Drawing.Size(26, 23);
            this.btFile.TabIndex = 4;
            this.btFile.Text = "...";
            this.btFile.UseVisualStyleBackColor = true;
            this.btFile.Click += new System.EventHandler(this.btFileExFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.Location = new System.Drawing.Point(6, 21);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(552, 20);
            this.txtFilePath.TabIndex = 2;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 153);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(634, 354);
            this.tabControl1.TabIndex = 17;
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gpBoxFileFormat);
            this.tabPage1.Controls.Add(this.btSendSoft);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(626, 328);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Load Flight Software to UAT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.btRunSw);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(626, 328);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Run SW";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtAddress);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 55);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Address Configuration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Address: ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(63, 22);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(544, 20);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // btRunSw
            // 
            this.btRunSw.Location = new System.Drawing.Point(411, 67);
            this.btRunSw.Name = "btRunSw";
            this.btRunSw.Size = new System.Drawing.Size(202, 50);
            this.btRunSw.TabIndex = 18;
            this.btRunSw.Text = "Run SW";
            this.btRunSw.UseVisualStyleBackColor = true;
            this.btRunSw.Click += new System.EventHandler(this.btRunSw_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btWriteMemory);
            this.tabPage3.Controls.Add(this.btGetData);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.txtAddressMemBlock);
            this.tabPage3.Controls.Add(this.gridMemorySwicher);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(626, 328);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Change Memory Block";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btWriteMemory
            // 
            this.btWriteMemory.Enabled = false;
            this.btWriteMemory.Location = new System.Drawing.Point(522, 253);
            this.btWriteMemory.Name = "btWriteMemory";
            this.btWriteMemory.Size = new System.Drawing.Size(101, 23);
            this.btWriteMemory.TabIndex = 6;
            this.btWriteMemory.Text = "Write to Memory";
            this.btWriteMemory.UseVisualStyleBackColor = true;
            this.btWriteMemory.Click += new System.EventHandler(this.btWriteMemory_Click);
            // 
            // btGetData
            // 
            this.btGetData.Location = new System.Drawing.Point(230, 11);
            this.btGetData.Name = "btGetData";
            this.btGetData.Size = new System.Drawing.Size(75, 23);
            this.btGetData.TabIndex = 5;
            this.btGetData.Text = "Get Data";
            this.btGetData.UseVisualStyleBackColor = true;
            this.btGetData.Click += new System.EventHandler(this.btGetData_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Address: ";
            // 
            // txtAddressMemBlock
            // 
            this.txtAddressMemBlock.Location = new System.Drawing.Point(67, 14);
            this.txtAddressMemBlock.Name = "txtAddressMemBlock";
            this.txtAddressMemBlock.Size = new System.Drawing.Size(157, 20);
            this.txtAddressMemBlock.TabIndex = 4;
            this.txtAddressMemBlock.TextChanged += new System.EventHandler(this.txtAddressMemBlock_TextChanged);
            // 
            // gridMemorySwicher
            // 
            this.gridMemorySwicher.AllowUserToAddRows = false;
            this.gridMemorySwicher.AllowUserToDeleteRows = false;
            this.gridMemorySwicher.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridMemorySwicher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMemorySwicher.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Byte,
            this.Byte2,
            this.Byte3,
            this.Byte4,
            this.Byte5,
            this.Byte6,
            this.Byte7,
            this.Byte8});
            this.gridMemorySwicher.Enabled = false;
            this.gridMemorySwicher.Location = new System.Drawing.Point(4, 40);
            this.gridMemorySwicher.MultiSelect = false;
            this.gridMemorySwicher.Name = "gridMemorySwicher";
            this.gridMemorySwicher.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridMemorySwicher.Size = new System.Drawing.Size(619, 207);
            this.gridMemorySwicher.TabIndex = 0;
            // 
            // Byte
            // 
            this.Byte.HeaderText = "Byte";
            this.Byte.Name = "Byte";
            // 
            // Byte2
            // 
            this.Byte2.HeaderText = "Byte";
            this.Byte2.Name = "Byte2";
            // 
            // Byte3
            // 
            this.Byte3.HeaderText = "Byte";
            this.Byte3.Name = "Byte3";
            // 
            // Byte4
            // 
            this.Byte4.HeaderText = "Byte";
            this.Byte4.Name = "Byte4";
            // 
            // Byte5
            // 
            this.Byte5.HeaderText = "Byte";
            this.Byte5.Name = "Byte5";
            // 
            // Byte6
            // 
            this.Byte6.HeaderText = "Byte";
            this.Byte6.Name = "Byte6";
            // 
            // Byte7
            // 
            this.Byte7.HeaderText = "Byte";
            this.Byte7.Name = "Byte7";
            // 
            // Byte8
            // 
            this.Byte8.HeaderText = "Byte";
            this.Byte8.Name = "Byte8";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btReadMemory);
            this.tabPage4.Controls.Add(this.txtNumOfBytes);
            this.tabPage4.Controls.Add(this.txtInitialAddress);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(626, 328);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Memory to File";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btReadMemory
            // 
            this.btReadMemory.Location = new System.Drawing.Point(250, 10);
            this.btReadMemory.Name = "btReadMemory";
            this.btReadMemory.Size = new System.Drawing.Size(202, 50);
            this.btReadMemory.TabIndex = 8;
            this.btReadMemory.Text = "Read Memory and Save File";
            this.btReadMemory.UseVisualStyleBackColor = true;
            this.btReadMemory.Click += new System.EventHandler(this.btReadMemory_Click);
            // 
            // txtNumOfBytes
            // 
            this.txtNumOfBytes.Location = new System.Drawing.Point(96, 40);
            this.txtNumOfBytes.Name = "txtNumOfBytes";
            this.txtNumOfBytes.Size = new System.Drawing.Size(107, 20);
            this.txtNumOfBytes.TabIndex = 4;
            this.txtNumOfBytes.TextChanged += new System.EventHandler(this.txtNumOfBytes_TextChanged);
            // 
            // txtInitialAddress
            // 
            this.txtInitialAddress.Location = new System.Drawing.Point(96, 14);
            this.txtInitialAddress.Name = "txtInitialAddress";
            this.txtInitialAddress.Size = new System.Drawing.Size(107, 20);
            this.txtInitialAddress.TabIndex = 3;
            this.txtInitialAddress.TextChanged += new System.EventHandler(this.txtInitialAddress_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Num Of Btytes:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Initial Address:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.btStartListenUat);
            this.tabPage5.Controls.Add(this.btClearScreen);
            this.tabPage5.Controls.Add(this.btSendCommand);
            this.tabPage5.Controls.Add(this.txtMessageToSendUat);
            this.tabPage5.Controls.Add(this.txtUatMessage);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(626, 328);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "View and Execute Commands";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 285);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Command:";
            // 
            // btStartListenUat
            // 
            this.btStartListenUat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartListenUat.Location = new System.Drawing.Point(400, 279);
            this.btStartListenUat.Name = "btStartListenUat";
            this.btStartListenUat.Size = new System.Drawing.Size(222, 46);
            this.btStartListenUat.TabIndex = 23;
            this.btStartListenUat.Text = "Start Listen UAT";
            this.btStartListenUat.UseVisualStyleBackColor = true;
            this.btStartListenUat.Click += new System.EventHandler(this.btStartListenUat_Click);
            // 
            // btClearScreen
            // 
            this.btClearScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearScreen.Enabled = false;
            this.btClearScreen.Location = new System.Drawing.Point(285, 305);
            this.btClearScreen.Name = "btClearScreen";
            this.btClearScreen.Size = new System.Drawing.Size(93, 20);
            this.btClearScreen.TabIndex = 22;
            this.btClearScreen.Text = "Clear Screen";
            this.btClearScreen.UseVisualStyleBackColor = true;
            this.btClearScreen.Click += new System.EventHandler(this.btClearScreen_Click);
            // 
            // btSendCommand
            // 
            this.btSendCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSendCommand.Enabled = false;
            this.btSendCommand.Location = new System.Drawing.Point(8, 305);
            this.btSendCommand.Name = "btSendCommand";
            this.btSendCommand.Size = new System.Drawing.Size(93, 20);
            this.btSendCommand.TabIndex = 21;
            this.btSendCommand.Text = "Send";
            this.btSendCommand.UseVisualStyleBackColor = true;
            this.btSendCommand.Click += new System.EventHandler(this.btSendCommand_Click);
            // 
            // txtMessageToSendUat
            // 
            this.txtMessageToSendUat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageToSendUat.Enabled = false;
            this.txtMessageToSendUat.Location = new System.Drawing.Point(67, 282);
            this.txtMessageToSendUat.Name = "txtMessageToSendUat";
            this.txtMessageToSendUat.Size = new System.Drawing.Size(311, 20);
            this.txtMessageToSendUat.TabIndex = 20;
            // 
            // txtUatMessage
            // 
            this.txtUatMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUatMessage.BackColor = System.Drawing.Color.Black;
            this.txtUatMessage.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUatMessage.ForeColor = System.Drawing.Color.White;
            this.txtUatMessage.Location = new System.Drawing.Point(3, 3);
            this.txtUatMessage.Multiline = true;
            this.txtUatMessage.Name = "txtUatMessage";
            this.txtUatMessage.ReadOnly = true;
            this.txtUatMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUatMessage.Size = new System.Drawing.Size(620, 276);
            this.txtUatMessage.TabIndex = 19;
            // 
            // FrmUatMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 515);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmUatMonitor";
            this.Text = "UAT Monitor (COMAV prototype)";
            this.Load += new System.EventHandler(this.FrmLoadFlightSoftUat_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpBoxFileFormat.ResumeLayout(false);
            this.gpBoxFileFormat.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMemorySwicher)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSendSoft;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.GroupBox gpBoxFileFormat;
        private System.Windows.Forms.Button btFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btRunSw;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtMessageToSendUat;
        private System.Windows.Forms.TextBox txtUatMessage;
        private System.Windows.Forms.Button btClearScreen;
        private System.Windows.Forms.Button btSendCommand;
        private System.Windows.Forms.Button btStartListenUat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btReadMemory;
        private System.Windows.Forms.TextBox txtNumOfBytes;
        private System.Windows.Forms.TextBox txtInitialAddress;
        private System.Windows.Forms.DataGridView gridMemorySwicher;
        private System.Windows.Forms.Button btWriteMemory;
        private System.Windows.Forms.Button btGetData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAddressMemBlock;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte8;
    }
}
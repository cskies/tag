namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmConnectionWithEgse
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btConnection = new System.Windows.Forms.Button();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tmrAskForTM = new System.Windows.Forms.Timer(this.components);
            this.btSave = new System.Windows.Forms.Button();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label51 = new System.Windows.Forms.Label();
            this.chkPause = new System.Windows.Forms.CheckBox();
            this.numUpdateTm = new System.Windows.Forms.NumericUpDown();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.gridTmFrame = new System.Windows.Forms.DataGridView();
            this.gridSessionStats = new System.Windows.Forms.DataGridView();
            this.gridClcw = new System.Windows.Forms.DataGridView();
            this.btSendCommandToFarm = new System.Windows.Forms.Button();
            this.mskThirdOctect = new System.Windows.Forms.MaskedTextBox();
            this.mskSecondOctect = new System.Windows.Forms.MaskedTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.mskFirstOctect = new System.Windows.Forms.MaskedTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.cmbControlCommand = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btCancelFrameHeader = new System.Windows.Forms.Button();
            this.btChangeFrameHeader = new System.Windows.Forms.Button();
            this.cbAutoCRC = new System.Windows.Forms.CheckBox();
            this.cbAutoIncrement = new System.Windows.Forms.CheckBox();
            this.cbAutoLength = new System.Windows.Forms.CheckBox();
            this.cmbSeqFlags = new System.Windows.Forms.ComboBox();
            this.cmbFrameType = new System.Windows.Forms.ComboBox();
            this.mskVCID = new System.Windows.Forms.MaskedTextBox();
            this.mskSpacecraftID = new System.Windows.Forms.MaskedTextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.mskFrameCRC = new System.Windows.Forms.MaskedTextBox();
            this.mskFrameSeq = new System.Windows.Forms.MaskedTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.mskFrameLength = new System.Windows.Forms.MaskedTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.mskMapId = new System.Windows.Forms.MaskedTextBox();
            this.mskResB = new System.Windows.Forms.MaskedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.mskResA = new System.Windows.Forms.MaskedTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.mskVersion = new System.Windows.Forms.MaskedTextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rbTcpIp = new System.Windows.Forms.RadioButton();
            this.label44 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.txtTelecommandDataPort = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtCortexControlDataPort = new System.Windows.Forms.TextBox();
            this.txtTelemetryDataPort = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkConfigureCOP = new System.Windows.Forms.CheckBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.txtCopTcDataFlow = new System.Windows.Forms.TextBox();
            this.txtCopControlFlow = new System.Windows.Forms.TextBox();
            this.txtCopMonitoringFlow = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.rdClient = new System.Windows.Forms.RadioButton();
            this.rdServer = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbTcPacketsSeqControl = new System.Windows.Forms.ComboBox();
            this.lblTcPacketsSeqControl = new System.Windows.Forms.Label();
            this.cmbTcFramesSeqControl = new System.Windows.Forms.ComboBox();
            this.lblTcFramesSeqControl = new System.Windows.Forms.Label();
            this.grpSessionOptions = new System.Windows.Forms.GroupBox();
            this.chkConfigureFarm = new System.Windows.Forms.CheckBox();
            this.numSeconds = new System.Windows.Forms.NumericUpDown();
            this.chkAskTm = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkAutoSequence = new System.Windows.Forms.CheckBox();
            this.chkSyncronize = new System.Windows.Forms.CheckBox();
            this.chkSaveSession = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkDiscardIdleFrame = new System.Windows.Forms.CheckBox();
            this.rbCltu = new System.Windows.Forms.RadioButton();
            this.cmbTmChannelCod = new System.Windows.Forms.ComboBox();
            this.lblTmChannelCod = new System.Windows.Forms.Label();
            this.rbFrames = new System.Windows.Forms.RadioButton();
            this.rbPackets = new System.Windows.Forms.RadioButton();
            this.rbNamedPipe = new System.Windows.Forms.RadioButton();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.rbSerial = new System.Windows.Forms.RadioButton();
            this.rbRs422 = new System.Windows.Forms.RadioButton();
            this.label39 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btResetRxFifo = new System.Windows.Forms.Button();
            this.chkRs422DefaultOptions = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbRxTc = new System.Windows.Forms.RadioButton();
            this.rbRxTm = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbTxClockIdleContinuous = new System.Windows.Forms.RadioButton();
            this.rbTxClockIdleNOTContinuous = new System.Windows.Forms.RadioButton();
            this.cmbOperationMode = new System.Windows.Forms.ComboBox();
            this.lblOperationMode = new System.Windows.Forms.Label();
            this.cmbClockRate = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbRxChannel = new System.Windows.Forms.ComboBox();
            this.cmbTxChannel = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPipeName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rbPipeClient = new System.Windows.Forms.RadioButton();
            this.rbPipeServer = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btImportAmazoniaTm = new System.Windows.Forms.Button();
            this.btImportTm = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tmrUpdateTmFrame = new System.Windows.Forms.Timer(this.components);
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateTm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTmFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSessionStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClcw)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpSessionOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btConnection
            // 
            this.btConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btConnection.Location = new System.Drawing.Point(3, 936);
            this.btConnection.Name = "btConnection";
            this.btConnection.Size = new System.Drawing.Size(222, 23);
            this.btConnection.TabIndex = 0;
            this.btConnection.Text = "Connect to ...";
            this.btConnection.UseVisualStyleBackColor = true;
            this.btConnection.Click += new System.EventHandler(this.btConnection_Click);
            // 
            // serial
            // 
            this.serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serial_DataReceived);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // tmrAskForTM
            // 
            this.tmrAskForTM.Interval = 1000;
            this.tmrAskForTM.Tick += new System.EventHandler(this.tmrAskForTM_Tick);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(645, 500);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(161, 23);
            this.btSave.TabIndex = 12;
            this.btSave.Text = "Save Configuration";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.AutoScroll = true;
            this.tabPage4.Controls.Add(this.label51);
            this.tabPage4.Controls.Add(this.chkPause);
            this.tabPage4.Controls.Add(this.numUpdateTm);
            this.tabPage4.Controls.Add(this.label37);
            this.tabPage4.Controls.Add(this.label36);
            this.tabPage4.Controls.Add(this.gridTmFrame);
            this.tabPage4.Controls.Add(this.gridSessionStats);
            this.tabPage4.Controls.Add(this.gridClcw);
            this.tabPage4.Controls.Add(this.btSendCommandToFarm);
            this.tabPage4.Controls.Add(this.mskThirdOctect);
            this.tabPage4.Controls.Add(this.mskSecondOctect);
            this.tabPage4.Controls.Add(this.label30);
            this.tabPage4.Controls.Add(this.mskFirstOctect);
            this.tabPage4.Controls.Add(this.label31);
            this.tabPage4.Controls.Add(this.label32);
            this.tabPage4.Controls.Add(this.cmbControlCommand);
            this.tabPage4.Controls.Add(this.label33);
            this.tabPage4.Controls.Add(this.btCancelFrameHeader);
            this.tabPage4.Controls.Add(this.btChangeFrameHeader);
            this.tabPage4.Controls.Add(this.cbAutoCRC);
            this.tabPage4.Controls.Add(this.cbAutoIncrement);
            this.tabPage4.Controls.Add(this.cbAutoLength);
            this.tabPage4.Controls.Add(this.cmbSeqFlags);
            this.tabPage4.Controls.Add(this.cmbFrameType);
            this.tabPage4.Controls.Add(this.mskVCID);
            this.tabPage4.Controls.Add(this.mskSpacecraftID);
            this.tabPage4.Controls.Add(this.label41);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.mskFrameCRC);
            this.tabPage4.Controls.Add(this.mskFrameSeq);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.mskFrameLength);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.mskMapId);
            this.tabPage4.Controls.Add(this.mskResB);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.mskResA);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.mskVersion);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.label40);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.label28);
            this.tabPage4.Controls.Add(this.label38);
            this.tabPage4.Controls.Add(this.label35);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.label29);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(821, 906);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "CCSDS Frames Connection Management";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ForeColor = System.Drawing.Color.Red;
            this.label51.Location = new System.Drawing.Point(292, 240);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(16, 13);
            this.label51.TabIndex = 58;
            this.label51.Text = ":: ";
            // 
            // chkPause
            // 
            this.chkPause.AutoSize = true;
            this.chkPause.Location = new System.Drawing.Point(655, 239);
            this.chkPause.Name = "chkPause";
            this.chkPause.Size = new System.Drawing.Size(127, 17);
            this.chkPause.TabIndex = 57;
            this.chkPause.Text = "Pause screen update";
            this.chkPause.UseVisualStyleBackColor = true;
            this.chkPause.CheckedChanged += new System.EventHandler(this.chkPause_CheckedChanged);
            // 
            // numUpdateTm
            // 
            this.numUpdateTm.Location = new System.Drawing.Point(208, 238);
            this.numUpdateTm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpdateTm.Name = "numUpdateTm";
            this.numUpdateTm.Size = new System.Drawing.Size(42, 20);
            this.numUpdateTm.TabIndex = 56;
            this.numUpdateTm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpdateTm.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUpdateTm.ValueChanged += new System.EventHandler(this.numUpdateTm_ValueChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(7, 240);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(198, 13);
            this.label37.TabIndex = 55;
            this.label37.Text = "Update TM frame data and CLCW every";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(251, 240);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(47, 13);
            this.label36.TabIndex = 55;
            this.label36.Text = "seconds";
            // 
            // gridTmFrame
            // 
            this.gridTmFrame.AllowUserToAddRows = false;
            this.gridTmFrame.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender;
            this.gridTmFrame.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridTmFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridTmFrame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTmFrame.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTmFrame.Location = new System.Drawing.Point(6, 407);
            this.gridTmFrame.Name = "gridTmFrame";
            this.gridTmFrame.RowHeadersVisible = false;
            this.gridTmFrame.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTmFrame.Size = new System.Drawing.Size(771, 493);
            this.gridTmFrame.TabIndex = 54;
            // 
            // gridSessionStats
            // 
            this.gridSessionStats.AllowUserToAddRows = false;
            this.gridSessionStats.AllowUserToDeleteRows = false;
            this.gridSessionStats.AllowUserToResizeColumns = false;
            this.gridSessionStats.AllowUserToResizeRows = false;
            this.gridSessionStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSessionStats.ColumnHeadersVisible = false;
            this.gridSessionStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridSessionStats.Location = new System.Drawing.Point(325, 130);
            this.gridSessionStats.Name = "gridSessionStats";
            this.gridSessionStats.RowHeadersVisible = false;
            this.gridSessionStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSessionStats.Size = new System.Drawing.Size(452, 79);
            this.gridSessionStats.TabIndex = 54;
            this.gridSessionStats.SelectionChanged += new System.EventHandler(this.gridSessionStats_SelectionChanged);
            // 
            // gridClcw
            // 
            this.gridClcw.AllowUserToAddRows = false;
            this.gridClcw.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Lavender;
            this.gridClcw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridClcw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridClcw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridClcw.Location = new System.Drawing.Point(6, 263);
            this.gridClcw.Name = "gridClcw";
            this.gridClcw.RowHeadersVisible = false;
            this.gridClcw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridClcw.Size = new System.Drawing.Size(771, 115);
            this.gridClcw.TabIndex = 54;
            // 
            // btSendCommandToFarm
            // 
            this.btSendCommandToFarm.Enabled = false;
            this.btSendCommandToFarm.Location = new System.Drawing.Point(162, 185);
            this.btSendCommandToFarm.Name = "btSendCommandToFarm";
            this.btSendCommandToFarm.Size = new System.Drawing.Size(148, 24);
            this.btSendCommandToFarm.TabIndex = 53;
            this.btSendCommandToFarm.Text = "Send Command to FARM";
            this.btSendCommandToFarm.UseVisualStyleBackColor = true;
            this.btSendCommandToFarm.Click += new System.EventHandler(this.btSendCommandToFarm_Click);
            // 
            // mskThirdOctect
            // 
            this.mskThirdOctect.BackColor = System.Drawing.SystemColors.Window;
            this.mskThirdOctect.Location = new System.Drawing.Point(253, 159);
            this.mskThirdOctect.Mask = "###";
            this.mskThirdOctect.Name = "mskThirdOctect";
            this.mskThirdOctect.PromptChar = ' ';
            this.mskThirdOctect.Size = new System.Drawing.Size(56, 20);
            this.mskThirdOctect.TabIndex = 52;
            this.mskThirdOctect.Text = "0";
            this.mskThirdOctect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskThirdOctect.Leave += new System.EventHandler(this.mskThirdOctect_Leave);
            // 
            // mskSecondOctect
            // 
            this.mskSecondOctect.BackColor = System.Drawing.SystemColors.Window;
            this.mskSecondOctect.Location = new System.Drawing.Point(191, 159);
            this.mskSecondOctect.Mask = "###";
            this.mskSecondOctect.Name = "mskSecondOctect";
            this.mskSecondOctect.PromptChar = ' ';
            this.mskSecondOctect.Size = new System.Drawing.Size(56, 20);
            this.mskSecondOctect.TabIndex = 51;
            this.mskSecondOctect.Text = "0";
            this.mskSecondOctect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskSecondOctect.Leave += new System.EventHandler(this.mskSecondOctect_Leave);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(250, 130);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(60, 26);
            this.label30.TabIndex = 49;
            this.label30.Text = "3rd Octect\r\n(Next Seq.)";
            // 
            // mskFirstOctect
            // 
            this.mskFirstOctect.BackColor = System.Drawing.SystemColors.Window;
            this.mskFirstOctect.Location = new System.Drawing.Point(135, 159);
            this.mskFirstOctect.Mask = "###";
            this.mskFirstOctect.Name = "mskFirstOctect";
            this.mskFirstOctect.PromptChar = ' ';
            this.mskFirstOctect.Size = new System.Drawing.Size(50, 20);
            this.mskFirstOctect.TabIndex = 50;
            this.mskFirstOctect.Text = "0";
            this.mskFirstOctect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFirstOctect.Leave += new System.EventHandler(this.mskFirstOctect_Leave);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(188, 130);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(60, 13);
            this.label31.TabIndex = 47;
            this.label31.Text = "2nd Octect";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(132, 130);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(39, 26);
            this.label32.TabIndex = 46;
            this.label32.Text = "Single\r\nOctect";
            // 
            // cmbControlCommand
            // 
            this.cmbControlCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControlCommand.FormattingEnabled = true;
            this.cmbControlCommand.Items.AddRange(new object[] {
            "Invalid Command",
            "SET V(R)",
            "UNLOCK"});
            this.cmbControlCommand.Location = new System.Drawing.Point(8, 158);
            this.cmbControlCommand.Name = "cmbControlCommand";
            this.cmbControlCommand.Size = new System.Drawing.Size(121, 21);
            this.cmbControlCommand.TabIndex = 45;
            this.cmbControlCommand.SelectedIndexChanged += new System.EventHandler(this.cmbControlCommand_SelectedIndexChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 130);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(57, 13);
            this.label33.TabIndex = 48;
            this.label33.Text = "Command:";
            // 
            // btCancelFrameHeader
            // 
            this.btCancelFrameHeader.Location = new System.Drawing.Point(178, 77);
            this.btCancelFrameHeader.Name = "btCancelFrameHeader";
            this.btCancelFrameHeader.Size = new System.Drawing.Size(75, 23);
            this.btCancelFrameHeader.TabIndex = 44;
            this.btCancelFrameHeader.Text = "Cancel";
            this.btCancelFrameHeader.UseVisualStyleBackColor = true;
            this.btCancelFrameHeader.Visible = false;
            this.btCancelFrameHeader.Click += new System.EventHandler(this.btCancelFrameHeader_Click);
            // 
            // btChangeFrameHeader
            // 
            this.btChangeFrameHeader.Location = new System.Drawing.Point(8, 77);
            this.btChangeFrameHeader.Name = "btChangeFrameHeader";
            this.btChangeFrameHeader.Size = new System.Drawing.Size(161, 23);
            this.btChangeFrameHeader.TabIndex = 43;
            this.btChangeFrameHeader.Text = "Change TC Frame Header";
            this.btChangeFrameHeader.UseVisualStyleBackColor = true;
            this.btChangeFrameHeader.Click += new System.EventHandler(this.btChangeFrameHeader_Click);
            // 
            // cbAutoCRC
            // 
            this.cbAutoCRC.AutoSize = true;
            this.cbAutoCRC.Checked = true;
            this.cbAutoCRC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoCRC.Location = new System.Drawing.Point(699, 76);
            this.cbAutoCRC.Name = "cbAutoCRC";
            this.cbAutoCRC.Size = new System.Drawing.Size(47, 17);
            this.cbAutoCRC.TabIndex = 42;
            this.cbAutoCRC.TabStop = false;
            this.cbAutoCRC.Text = "auto";
            this.cbAutoCRC.UseVisualStyleBackColor = true;
            this.cbAutoCRC.CheckedChanged += new System.EventHandler(this.cbAutoCRC_CheckedChanged);
            // 
            // cbAutoIncrement
            // 
            this.cbAutoIncrement.AutoSize = true;
            this.cbAutoIncrement.Checked = true;
            this.cbAutoIncrement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoIncrement.Location = new System.Drawing.Point(478, 76);
            this.cbAutoIncrement.Name = "cbAutoIncrement";
            this.cbAutoIncrement.Size = new System.Drawing.Size(47, 17);
            this.cbAutoIncrement.TabIndex = 27;
            this.cbAutoIncrement.TabStop = false;
            this.cbAutoIncrement.Text = "auto";
            this.cbAutoIncrement.UseVisualStyleBackColor = true;
            this.cbAutoIncrement.CheckedChanged += new System.EventHandler(this.cbAutoIncrement_CheckedChanged);
            // 
            // cbAutoLength
            // 
            this.cbAutoLength.AutoSize = true;
            this.cbAutoLength.Checked = true;
            this.cbAutoLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoLength.Location = new System.Drawing.Point(414, 76);
            this.cbAutoLength.Name = "cbAutoLength";
            this.cbAutoLength.Size = new System.Drawing.Size(47, 17);
            this.cbAutoLength.TabIndex = 25;
            this.cbAutoLength.TabStop = false;
            this.cbAutoLength.Text = "auto";
            this.cbAutoLength.UseVisualStyleBackColor = true;
            this.cbAutoLength.CheckedChanged += new System.EventHandler(this.cbAutoLength_CheckedChanged);
            // 
            // cmbSeqFlags
            // 
            this.cmbSeqFlags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeqFlags.Enabled = false;
            this.cmbSeqFlags.FormattingEnabled = true;
            this.cmbSeqFlags.Items.AddRange(new object[] {
            "[00] Cont. segment",
            "[01] First segment",
            "[10] Last segment",
            "[11] Unsegmented"});
            this.cmbSeqFlags.Location = new System.Drawing.Point(542, 50);
            this.cmbSeqFlags.Name = "cmbSeqFlags";
            this.cmbSeqFlags.Size = new System.Drawing.Size(118, 21);
            this.cmbSeqFlags.TabIndex = 28;
            // 
            // cmbFrameType
            // 
            this.cmbFrameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrameType.Enabled = false;
            this.cmbFrameType.FormattingEnabled = true;
            this.cmbFrameType.Items.AddRange(new object[] {
            "[00] Data Frame",
            "[01] Invalid Frame",
            "[10] By-Pass Frame",
            "[11] Control Frame"});
            this.cmbFrameType.Location = new System.Drawing.Point(54, 50);
            this.cmbFrameType.Name = "cmbFrameType";
            this.cmbFrameType.Size = new System.Drawing.Size(115, 21);
            this.cmbFrameType.Sorted = true;
            this.cmbFrameType.TabIndex = 16;
            // 
            // mskVCID
            // 
            this.mskVCID.BackColor = System.Drawing.SystemColors.Window;
            this.mskVCID.Enabled = false;
            this.mskVCID.Location = new System.Drawing.Point(296, 50);
            this.mskVCID.Mask = "##";
            this.mskVCID.Name = "mskVCID";
            this.mskVCID.PromptChar = ' ';
            this.mskVCID.Size = new System.Drawing.Size(56, 20);
            this.mskVCID.TabIndex = 22;
            this.mskVCID.Text = "0";
            this.mskVCID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskVCID.Leave += new System.EventHandler(this.mskVCID_Leave);
            // 
            // mskSpacecraftID
            // 
            this.mskSpacecraftID.BackColor = System.Drawing.SystemColors.Window;
            this.mskSpacecraftID.Enabled = false;
            this.mskSpacecraftID.Location = new System.Drawing.Point(234, 50);
            this.mskSpacecraftID.Mask = "####";
            this.mskSpacecraftID.Name = "mskSpacecraftID";
            this.mskSpacecraftID.PromptChar = ' ';
            this.mskSpacecraftID.Size = new System.Drawing.Size(56, 20);
            this.mskSpacecraftID.TabIndex = 18;
            this.mskSpacecraftID.Text = "0";
            this.mskSpacecraftID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskSpacecraftID.Leave += new System.EventHandler(this.mskSpacecraftID_Leave);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(293, 71);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(58, 26);
            this.label41.TabIndex = 37;
            this.label41.Text = "(only for\r\nBC frames)";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(293, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 26);
            this.label16.TabIndex = 37;
            this.label16.Text = "Virtual\r\nChannel ID";
            // 
            // mskFrameCRC
            // 
            this.mskFrameCRC.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskFrameCRC.Location = new System.Drawing.Point(699, 50);
            this.mskFrameCRC.Mask = "AA-AA";
            this.mskFrameCRC.Name = "mskFrameCRC";
            this.mskFrameCRC.PromptChar = ' ';
            this.mskFrameCRC.ReadOnly = true;
            this.mskFrameCRC.Size = new System.Drawing.Size(67, 20);
            this.mskFrameCRC.TabIndex = 36;
            this.mskFrameCRC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFrameCRC.Leave += new System.EventHandler(this.mskFrameCRC_Leave);
            // 
            // mskFrameSeq
            // 
            this.mskFrameSeq.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskFrameSeq.Location = new System.Drawing.Point(478, 50);
            this.mskFrameSeq.Mask = "###";
            this.mskFrameSeq.Name = "mskFrameSeq";
            this.mskFrameSeq.PromptChar = ' ';
            this.mskFrameSeq.ReadOnly = true;
            this.mskFrameSeq.Size = new System.Drawing.Size(58, 20);
            this.mskFrameSeq.TabIndex = 26;
            this.mskFrameSeq.Text = "0";
            this.mskFrameSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFrameSeq.Leave += new System.EventHandler(this.mskFrameSeq_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(661, 21);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(30, 26);
            this.label25.TabIndex = 35;
            this.label25.Text = "MAP\r\nID";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(539, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(84, 13);
            this.label24.TabIndex = 38;
            this.label24.Text = "Sequence Flags";
            // 
            // mskFrameLength
            // 
            this.mskFrameLength.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskFrameLength.Location = new System.Drawing.Point(414, 50);
            this.mskFrameLength.Mask = "###";
            this.mskFrameLength.Name = "mskFrameLength";
            this.mskFrameLength.PromptChar = ' ';
            this.mskFrameLength.ReadOnly = true;
            this.mskFrameLength.Size = new System.Drawing.Size(58, 20);
            this.mskFrameLength.TabIndex = 24;
            this.mskFrameLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFrameLength.Leave += new System.EventHandler(this.mskFrameLength_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(696, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 26);
            this.label17.TabIndex = 41;
            this.label17.Text = "Frame\r\nCRC";
            // 
            // mskMapId
            // 
            this.mskMapId.BackColor = System.Drawing.SystemColors.Window;
            this.mskMapId.Enabled = false;
            this.mskMapId.Location = new System.Drawing.Point(664, 50);
            this.mskMapId.Mask = "##";
            this.mskMapId.Name = "mskMapId";
            this.mskMapId.PromptChar = ' ';
            this.mskMapId.Size = new System.Drawing.Size(27, 20);
            this.mskMapId.TabIndex = 29;
            this.mskMapId.Text = "0";
            this.mskMapId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskMapId.Leave += new System.EventHandler(this.mskMapId_Leave);
            // 
            // mskResB
            // 
            this.mskResB.BackColor = System.Drawing.SystemColors.Window;
            this.mskResB.Enabled = false;
            this.mskResB.Location = new System.Drawing.Point(358, 50);
            this.mskResB.Mask = "#";
            this.mskResB.Name = "mskResB";
            this.mskResB.PromptChar = ' ';
            this.mskResB.Size = new System.Drawing.Size(50, 20);
            this.mskResB.TabIndex = 23;
            this.mskResB.Text = "0";
            this.mskResB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskResB.Leave += new System.EventHandler(this.mskResB_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(475, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 26);
            this.label18.TabIndex = 40;
            this.label18.Text = "Frame Seq.\r\nNumber";
            // 
            // mskResA
            // 
            this.mskResA.BackColor = System.Drawing.SystemColors.Window;
            this.mskResA.Enabled = false;
            this.mskResA.Location = new System.Drawing.Point(178, 50);
            this.mskResA.Mask = "#";
            this.mskResA.Name = "mskResA";
            this.mskResA.PromptChar = ' ';
            this.mskResA.Size = new System.Drawing.Size(50, 20);
            this.mskResA.TabIndex = 17;
            this.mskResA.Text = "0";
            this.mskResA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskResA.Leave += new System.EventHandler(this.mskResA_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(411, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 26);
            this.label19.TabIndex = 39;
            this.label19.Text = "Frame\r\nLength";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(231, 21);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 26);
            this.label20.TabIndex = 34;
            this.label20.Text = "Spacecraft\r\nID";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(355, 21);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 26);
            this.label21.TabIndex = 30;
            this.label21.Text = "Reserved\r\nField B";
            // 
            // mskVersion
            // 
            this.mskVersion.BackColor = System.Drawing.SystemColors.Window;
            this.mskVersion.Enabled = false;
            this.mskVersion.Location = new System.Drawing.Point(8, 50);
            this.mskVersion.Mask = "#";
            this.mskVersion.Name = "mskVersion";
            this.mskVersion.PromptChar = ' ';
            this.mskVersion.Size = new System.Drawing.Size(41, 20);
            this.mskVersion.TabIndex = 15;
            this.mskVersion.Text = "0";
            this.mskVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskVersion.Leave += new System.EventHandler(this.mskVersion_Leave);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(175, 21);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 26);
            this.label22.TabIndex = 32;
            this.label22.Text = "Reserved\r\nField A";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(52, 21);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(117, 26);
            this.label23.TabIndex = 33;
            this.label23.Text = "Frame Type (by-pass + \r\ncontrol cmd. flags)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 21);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 26);
            this.label26.TabIndex = 31;
            this.label26.Text = "Version\r\nNumber";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.SystemColors.Window;
            this.label40.Location = new System.Drawing.Point(325, 106);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(454, 17);
            this.label40.TabIndex = 19;
            this.label40.Text = "Session Statistics";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.Window;
            this.label27.Location = new System.Drawing.Point(542, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(149, 17);
            this.label27.TabIndex = 19;
            this.label27.Text = "TC Segment Header";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.SystemColors.Window;
            this.label28.Location = new System.Drawing.Point(700, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(80, 17);
            this.label28.TabIndex = 20;
            this.label28.Text = "Error Control";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.SystemColors.Window;
            this.label38.Location = new System.Drawing.Point(6, 384);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(770, 17);
            this.label38.TabIndex = 21;
            this.label38.Text = "TM Frame Header";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.SystemColors.Window;
            this.label35.Location = new System.Drawing.Point(4, 216);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(772, 17);
            this.label35.TabIndex = 21;
            this.label35.Text = "CLCW Received (by Virtual Channel)";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.Window;
            this.label34.Location = new System.Drawing.Point(8, 106);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(301, 17);
            this.label34.TabIndex = 21;
            this.label34.Text = "Commands to FARM";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.SystemColors.Window;
            this.label29.Location = new System.Drawing.Point(3, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(533, 17);
            this.label29.TabIndex = 21;
            this.label29.Text = "TC Frame Header";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.rbTcpIp);
            this.tabPage1.Controls.Add(this.label44);
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.btSave);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.grpSessionOptions);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.rbNamedPipe);
            this.tabPage1.Controls.Add(this.rbFile);
            this.tabPage1.Controls.Add(this.rbSerial);
            this.tabPage1.Controls.Add(this.rbRs422);
            this.tabPage1.Controls.Add(this.label39);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(821, 906);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connection Type and Options";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rbTcpIp
            // 
            this.rbTcpIp.AutoSize = true;
            this.rbTcpIp.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rbTcpIp.Location = new System.Drawing.Point(10, 6);
            this.rbTcpIp.Name = "rbTcpIp";
            this.rbTcpIp.Size = new System.Drawing.Size(14, 13);
            this.rbTcpIp.TabIndex = 0;
            this.rbTcpIp.UseVisualStyleBackColor = false;
            this.rbTcpIp.CheckedChanged += new System.EventHandler(this.rbTcpIp_CheckedChanged);
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label44.Location = new System.Drawing.Point(7, 5);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(390, 16);
            this.label44.TabIndex = 27;
            this.label44.Text = "    Connect with CORTEX (Ethernet)";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.panel4);
            this.groupBox7.Controls.Add(this.panel2);
            this.groupBox7.Controls.Add(this.label45);
            this.groupBox7.Controls.Add(this.txtIp);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.rdClient);
            this.groupBox7.Controls.Add(this.rdServer);
            this.groupBox7.Location = new System.Drawing.Point(7, 29);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(391, 276);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Ethernet Configuration";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label47);
            this.panel4.Controls.Add(this.label42);
            this.panel4.Controls.Add(this.txtTelecommandDataPort);
            this.panel4.Controls.Add(this.label46);
            this.panel4.Controls.Add(this.txtCortexControlDataPort);
            this.panel4.Controls.Add(this.txtTelemetryDataPort);
            this.panel4.Location = new System.Drawing.Point(9, 173);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(372, 92);
            this.panel4.TabIndex = 1;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(10, 13);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(175, 13);
            this.label47.TabIndex = 39;
            this.label47.Text = "CORTEX Control Data (CTRL) Port:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(10, 65);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(208, 13);
            this.label42.TabIndex = 38;
            this.label42.Text = "CORTEX Telemetry Data (TM) Port:";
            // 
            // txtTelecommandDataPort
            // 
            this.txtTelecommandDataPort.Location = new System.Drawing.Point(289, 36);
            this.txtTelecommandDataPort.Name = "txtTelecommandDataPort";
            this.txtTelecommandDataPort.Size = new System.Drawing.Size(75, 20);
            this.txtTelecommandDataPort.TabIndex = 1;
            this.txtTelecommandDataPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(10, 39);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(235, 13);
            this.label46.TabIndex = 37;
            this.label46.Text = "CORTEX Satellite Telecommand Data (TC) Port:";
            // 
            // txtCortexControlDataPort
            // 
            this.txtCortexControlDataPort.Location = new System.Drawing.Point(290, 10);
            this.txtCortexControlDataPort.Name = "txtCortexControlDataPort";
            this.txtCortexControlDataPort.Size = new System.Drawing.Size(75, 20);
            this.txtCortexControlDataPort.TabIndex = 0;
            this.txtCortexControlDataPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTelemetryDataPort
            // 
            this.txtTelemetryDataPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelemetryDataPort.Location = new System.Drawing.Point(290, 62);
            this.txtTelemetryDataPort.Name = "txtTelemetryDataPort";
            this.txtTelemetryDataPort.Size = new System.Drawing.Size(75, 20);
            this.txtTelemetryDataPort.TabIndex = 2;
            this.txtTelemetryDataPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkConfigureCOP);
            this.panel2.Controls.Add(this.label50);
            this.panel2.Controls.Add(this.label48);
            this.panel2.Controls.Add(this.txtCopTcDataFlow);
            this.panel2.Controls.Add(this.txtCopControlFlow);
            this.panel2.Controls.Add(this.txtCopMonitoringFlow);
            this.panel2.Controls.Add(this.label49);
            this.panel2.Location = new System.Drawing.Point(9, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 118);
            this.panel2.TabIndex = 0;
            // 
            // chkConfigureCOP
            // 
            this.chkConfigureCOP.AutoSize = true;
            this.chkConfigureCOP.Location = new System.Drawing.Point(17, 9);
            this.chkConfigureCOP.Name = "chkConfigureCOP";
            this.chkConfigureCOP.Size = new System.Drawing.Size(140, 17);
            this.chkConfigureCOP.TabIndex = 0;
            this.chkConfigureCOP.Text = "Configure COP-1 at start";
            this.chkConfigureCOP.UseVisualStyleBackColor = true;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(15, 89);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(190, 13);
            this.label50.TabIndex = 48;
            this.label50.Text = "COP-1 Telecommand Data Flow:";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(15, 37);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(102, 13);
            this.label48.TabIndex = 43;
            this.label48.Text = "COP-1 Control Flow:";
            // 
            // txtCopTcDataFlow
            // 
            this.txtCopTcDataFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopTcDataFlow.Location = new System.Drawing.Point(290, 89);
            this.txtCopTcDataFlow.Name = "txtCopTcDataFlow";
            this.txtCopTcDataFlow.Size = new System.Drawing.Size(75, 20);
            this.txtCopTcDataFlow.TabIndex = 3;
            this.txtCopTcDataFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCopControlFlow
            // 
            this.txtCopControlFlow.Location = new System.Drawing.Point(290, 34);
            this.txtCopControlFlow.Name = "txtCopControlFlow";
            this.txtCopControlFlow.Size = new System.Drawing.Size(75, 20);
            this.txtCopControlFlow.TabIndex = 1;
            this.txtCopControlFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCopMonitoringFlow
            // 
            this.txtCopMonitoringFlow.Location = new System.Drawing.Point(290, 60);
            this.txtCopMonitoringFlow.Name = "txtCopMonitoringFlow";
            this.txtCopMonitoringFlow.Size = new System.Drawing.Size(75, 20);
            this.txtCopMonitoringFlow.TabIndex = 2;
            this.txtCopMonitoringFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(15, 63);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(118, 13);
            this.label49.TabIndex = 45;
            this.label49.Text = "COP-1 Monitoring Flow:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 25);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(58, 13);
            this.label45.TabIndex = 36;
            this.label45.Text = "This is the:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(256, 22);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(117, 20);
            this.txtIp.TabIndex = 0;
            this.txtIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(189, 25);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(61, 13);
            this.label43.TabIndex = 34;
            this.label43.Text = "IP Address:";
            // 
            // rdClient
            // 
            this.rdClient.AutoSize = true;
            this.rdClient.Checked = true;
            this.rdClient.Location = new System.Drawing.Point(132, 23);
            this.rdClient.Name = "rdClient";
            this.rdClient.Size = new System.Drawing.Size(51, 17);
            this.rdClient.TabIndex = 0;
            this.rdClient.TabStop = true;
            this.rdClient.Text = "Client";
            this.rdClient.UseVisualStyleBackColor = true;
            // 
            // rdServer
            // 
            this.rdServer.AutoSize = true;
            this.rdServer.Enabled = false;
            this.rdServer.Location = new System.Drawing.Point(70, 23);
            this.rdServer.Name = "rdServer";
            this.rdServer.Size = new System.Drawing.Size(56, 17);
            this.rdServer.TabIndex = 1;
            this.rdServer.Text = "Server";
            this.rdServer.UseVisualStyleBackColor = true;
            this.rdServer.CheckedChanged += new System.EventHandler(this.rdServer_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbTcPacketsSeqControl);
            this.groupBox4.Controls.Add(this.lblTcPacketsSeqControl);
            this.groupBox4.Controls.Add(this.cmbTcFramesSeqControl);
            this.groupBox4.Controls.Add(this.lblTcFramesSeqControl);
            this.groupBox4.Location = new System.Drawing.Point(418, 263);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(389, 84);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TC Sequence Control";
            // 
            // cmbTcPacketsSeqControl
            // 
            this.cmbTcPacketsSeqControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTcPacketsSeqControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTcPacketsSeqControl.FormattingEnabled = true;
            this.cmbTcPacketsSeqControl.Items.AddRange(new object[] {
            "Stop comm. when TC packet is lost",
            "Ignore retransmit requests",
            "Retransmit frames when requested"});
            this.cmbTcPacketsSeqControl.Location = new System.Drawing.Point(90, 25);
            this.cmbTcPacketsSeqControl.Name = "cmbTcPacketsSeqControl";
            this.cmbTcPacketsSeqControl.Size = new System.Drawing.Size(285, 21);
            this.cmbTcPacketsSeqControl.TabIndex = 0;
            // 
            // lblTcPacketsSeqControl
            // 
            this.lblTcPacketsSeqControl.Location = new System.Drawing.Point(6, 28);
            this.lblTcPacketsSeqControl.Name = "lblTcPacketsSeqControl";
            this.lblTcPacketsSeqControl.Size = new System.Drawing.Size(84, 24);
            this.lblTcPacketsSeqControl.TabIndex = 6;
            this.lblTcPacketsSeqControl.Text = "For packets:";
            // 
            // cmbTcFramesSeqControl
            // 
            this.cmbTcFramesSeqControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTcFramesSeqControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTcFramesSeqControl.FormattingEnabled = true;
            this.cmbTcFramesSeqControl.Items.AddRange(new object[] {
            "Stop comm. when TC frame is lost",
            "Ignore retransmit requests",
            "Retransmit frames when requested"});
            this.cmbTcFramesSeqControl.Location = new System.Drawing.Point(90, 52);
            this.cmbTcFramesSeqControl.Name = "cmbTcFramesSeqControl";
            this.cmbTcFramesSeqControl.Size = new System.Drawing.Size(285, 21);
            this.cmbTcFramesSeqControl.TabIndex = 1;
            // 
            // lblTcFramesSeqControl
            // 
            this.lblTcFramesSeqControl.Location = new System.Drawing.Point(6, 55);
            this.lblTcFramesSeqControl.Name = "lblTcFramesSeqControl";
            this.lblTcFramesSeqControl.Size = new System.Drawing.Size(91, 15);
            this.lblTcFramesSeqControl.TabIndex = 4;
            this.lblTcFramesSeqControl.Text = "For frames";
            // 
            // grpSessionOptions
            // 
            this.grpSessionOptions.Controls.Add(this.chkConfigureFarm);
            this.grpSessionOptions.Controls.Add(this.numSeconds);
            this.grpSessionOptions.Controls.Add(this.chkAskTm);
            this.grpSessionOptions.Controls.Add(this.label8);
            this.grpSessionOptions.Controls.Add(this.chkAutoSequence);
            this.grpSessionOptions.Controls.Add(this.chkSyncronize);
            this.grpSessionOptions.Controls.Add(this.chkSaveSession);
            this.grpSessionOptions.Location = new System.Drawing.Point(418, 354);
            this.grpSessionOptions.Name = "grpSessionOptions";
            this.grpSessionOptions.Size = new System.Drawing.Size(389, 140);
            this.grpSessionOptions.TabIndex = 11;
            this.grpSessionOptions.TabStop = false;
            this.grpSessionOptions.Text = "Session Options";
            // 
            // chkConfigureFarm
            // 
            this.chkConfigureFarm.AutoSize = true;
            this.chkConfigureFarm.Location = new System.Drawing.Point(15, 88);
            this.chkConfigureFarm.Name = "chkConfigureFarm";
            this.chkConfigureFarm.Size = new System.Drawing.Size(139, 17);
            this.chkConfigureFarm.TabIndex = 3;
            this.chkConfigureFarm.Text = "Configure FARM at start";
            this.chkConfigureFarm.UseVisualStyleBackColor = true;
            // 
            // numSeconds
            // 
            this.numSeconds.Location = new System.Drawing.Point(97, 110);
            this.numSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSeconds.Name = "numSeconds";
            this.numSeconds.Size = new System.Drawing.Size(39, 20);
            this.numSeconds.TabIndex = 4;
            this.numSeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSeconds.ValueChanged += new System.EventHandler(this.numSeconds_ValueChanged);
            // 
            // chkAskTm
            // 
            this.chkAskTm.AutoSize = true;
            this.chkAskTm.Enabled = false;
            this.chkAskTm.Location = new System.Drawing.Point(15, 111);
            this.chkAskTm.Name = "chkAskTm";
            this.chkAskTm.Size = new System.Drawing.Size(78, 17);
            this.chkAskTm.TabIndex = 4;
            this.chkAskTm.Text = "Ask for TM";
            this.chkAskTm.UseVisualStyleBackColor = true;
            this.chkAskTm.CheckedChanged += new System.EventHandler(this.chkAskTm_CheckedChanged_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(142, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "seconds";
            // 
            // chkAutoSequence
            // 
            this.chkAutoSequence.AutoSize = true;
            this.chkAutoSequence.Location = new System.Drawing.Point(15, 66);
            this.chkAutoSequence.Name = "chkAutoSequence";
            this.chkAutoSequence.Size = new System.Drawing.Size(237, 17);
            this.chkAutoSequence.TabIndex = 2;
            this.chkAutoSequence.Text = "Manage TCs packet sequence automatically";
            this.chkAutoSequence.UseVisualStyleBackColor = true;
            // 
            // chkSyncronize
            // 
            this.chkSyncronize.AutoSize = true;
            this.chkSyncronize.Location = new System.Drawing.Point(15, 43);
            this.chkSyncronize.Name = "chkSyncronize";
            this.chkSyncronize.Size = new System.Drawing.Size(244, 17);
            this.chkSyncronize.TabIndex = 1;
            this.chkSyncronize.Text = "Syncronize OBT with the database server time";
            this.chkSyncronize.UseVisualStyleBackColor = true;
            // 
            // chkSaveSession
            // 
            this.chkSaveSession.AutoSize = true;
            this.chkSaveSession.Location = new System.Drawing.Point(15, 20);
            this.chkSaveSession.Name = "chkSaveSession";
            this.chkSaveSession.Size = new System.Drawing.Size(89, 17);
            this.chkSaveSession.TabIndex = 0;
            this.chkSaveSession.Text = "Save session";
            this.chkSaveSession.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkDiscardIdleFrame);
            this.groupBox6.Controls.Add(this.rbCltu);
            this.groupBox6.Controls.Add(this.cmbTmChannelCod);
            this.groupBox6.Controls.Add(this.lblTmChannelCod);
            this.groupBox6.Controls.Add(this.rbFrames);
            this.groupBox6.Controls.Add(this.rbPackets);
            this.groupBox6.Location = new System.Drawing.Point(418, 106);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(389, 149);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "TC/TM Communication Layer";
            // 
            // chkDiscardIdleFrame
            // 
            this.chkDiscardIdleFrame.AutoSize = true;
            this.chkDiscardIdleFrame.Location = new System.Drawing.Point(32, 92);
            this.chkDiscardIdleFrame.Name = "chkDiscardIdleFrame";
            this.chkDiscardIdleFrame.Size = new System.Drawing.Size(247, 17);
            this.chkDiscardIdleFrame.TabIndex = 2;
            this.chkDiscardIdleFrame.Text = "Discard TM idle frames (for better performance)";
            this.chkDiscardIdleFrame.UseVisualStyleBackColor = true;
            this.chkDiscardIdleFrame.CheckedChanged += new System.EventHandler(this.chkDiscardIdleFrame_CheckedChanged);
            // 
            // rbCltu
            // 
            this.rbCltu.AutoSize = true;
            this.rbCltu.Location = new System.Drawing.Point(15, 65);
            this.rbCltu.Name = "rbCltu";
            this.rbCltu.Size = new System.Drawing.Size(134, 17);
            this.rbCltu.TabIndex = 4;
            this.rbCltu.TabStop = true;
            this.rbCltu.Text = "CLTU (By-Pass COP-1)";
            this.rbCltu.UseVisualStyleBackColor = true;
            this.rbCltu.Visible = false;
            this.rbCltu.CheckedChanged += new System.EventHandler(this.rbCltu_CheckedChanged);
            // 
            // cmbTmChannelCod
            // 
            this.cmbTmChannelCod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTmChannelCod.FormattingEnabled = true;
            this.cmbTmChannelCod.Items.AddRange(new object[] {
            "No Coding",
            "Reed-Solomon",
            "Convolutional",
            "Reed-Solomon + Convolutional"});
            this.cmbTmChannelCod.Location = new System.Drawing.Point(137, 115);
            this.cmbTmChannelCod.Name = "cmbTmChannelCod";
            this.cmbTmChannelCod.Size = new System.Drawing.Size(238, 21);
            this.cmbTmChannelCod.TabIndex = 3;
            // 
            // lblTmChannelCod
            // 
            this.lblTmChannelCod.AutoSize = true;
            this.lblTmChannelCod.Location = new System.Drawing.Point(29, 118);
            this.lblTmChannelCod.Name = "lblTmChannelCod";
            this.lblTmChannelCod.Size = new System.Drawing.Size(102, 13);
            this.lblTmChannelCod.TabIndex = 15;
            this.lblTmChannelCod.Text = "TM channel coding:";
            // 
            // rbFrames
            // 
            this.rbFrames.AutoSize = true;
            this.rbFrames.Location = new System.Drawing.Point(15, 42);
            this.rbFrames.Name = "rbFrames";
            this.rbFrames.Size = new System.Drawing.Size(195, 17);
            this.rbFrames.TabIndex = 1;
            this.rbFrames.Text = "Frames (communication with UTMC)";
            this.rbFrames.UseVisualStyleBackColor = true;
            this.rbFrames.CheckedChanged += new System.EventHandler(this.rbFrames_CheckedChanged_1);
            // 
            // rbPackets
            // 
            this.rbPackets.AutoSize = true;
            this.rbPackets.Checked = true;
            this.rbPackets.Location = new System.Drawing.Point(15, 19);
            this.rbPackets.Name = "rbPackets";
            this.rbPackets.Size = new System.Drawing.Size(191, 17);
            this.rbPackets.TabIndex = 0;
            this.rbPackets.TabStop = true;
            this.rbPackets.Text = "Packets (communication with UPC)";
            this.rbPackets.UseVisualStyleBackColor = true;
            this.rbPackets.CheckedChanged += new System.EventHandler(this.rbPackets_CheckedChanged);
            // 
            // rbNamedPipe
            // 
            this.rbNamedPipe.AutoSize = true;
            this.rbNamedPipe.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rbNamedPipe.Location = new System.Drawing.Point(12, 645);
            this.rbNamedPipe.Name = "rbNamedPipe";
            this.rbNamedPipe.Size = new System.Drawing.Size(14, 13);
            this.rbNamedPipe.TabIndex = 2;
            this.rbNamedPipe.UseVisualStyleBackColor = false;
            this.rbNamedPipe.CheckedChanged += new System.EventHandler(this.rbNamedPipe_CheckedChanged);
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rbFile.Location = new System.Drawing.Point(418, 6);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(14, 13);
            this.rbFile.TabIndex = 6;
            this.rbFile.UseVisualStyleBackColor = false;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
            // 
            // rbSerial
            // 
            this.rbSerial.AutoSize = true;
            this.rbSerial.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rbSerial.Location = new System.Drawing.Point(10, 505);
            this.rbSerial.Name = "rbSerial";
            this.rbSerial.Size = new System.Drawing.Size(14, 13);
            this.rbSerial.TabIndex = 0;
            this.rbSerial.UseVisualStyleBackColor = false;
            this.rbSerial.CheckedChanged += new System.EventHandler(this.rbSerial_CheckedChanged);
            // 
            // rbRs422
            // 
            this.rbRs422.AutoSize = true;
            this.rbRs422.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rbRs422.Location = new System.Drawing.Point(10, 314);
            this.rbRs422.Name = "rbRs422";
            this.rbRs422.Size = new System.Drawing.Size(14, 13);
            this.rbRs422.TabIndex = 2;
            this.rbRs422.UseVisualStyleBackColor = false;
            this.rbRs422.CheckedChanged += new System.EventHandler(this.rbRs422_CheckedChanged);
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label39.Location = new System.Drawing.Point(418, 82);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(388, 16);
            this.label39.TabIndex = 8;
            this.label39.Text = "Communication and session options";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label13.Location = new System.Drawing.Point(7, 313);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(390, 16);
            this.label13.TabIndex = 20;
            this.label13.Text = "    Connect through serial RS-422";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label15.Location = new System.Drawing.Point(416, 5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(390, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "    Import/export files";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label9.Location = new System.Drawing.Point(8, 504);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(389, 16);
            this.label9.TabIndex = 19;
            this.label9.Text = "    Connect through serial port";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btResetRxFifo);
            this.groupBox5.Controls.Add(this.chkRs422DefaultOptions);
            this.groupBox5.Controls.Add(this.panel3);
            this.groupBox5.Controls.Add(this.panel1);
            this.groupBox5.Controls.Add(this.cmbOperationMode);
            this.groupBox5.Controls.Add(this.lblOperationMode);
            this.groupBox5.Controls.Add(this.cmbClockRate);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.cmbRxChannel);
            this.groupBox5.Controls.Add(this.cmbTxChannel);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(7, 337);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(390, 157);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "RS-422 Serial configuration";
            // 
            // btResetRxFifo
            // 
            this.btResetRxFifo.Location = new System.Drawing.Point(198, 122);
            this.btResetRxFifo.Name = "btResetRxFifo";
            this.btResetRxFifo.Size = new System.Drawing.Size(179, 23);
            this.btResetRxFifo.TabIndex = 4;
            this.btResetRxFifo.Text = "Reset Rx FIFO";
            this.btResetRxFifo.UseVisualStyleBackColor = true;
            // 
            // chkRs422DefaultOptions
            // 
            this.chkRs422DefaultOptions.AutoSize = true;
            this.chkRs422DefaultOptions.Checked = true;
            this.chkRs422DefaultOptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRs422DefaultOptions.Enabled = false;
            this.chkRs422DefaultOptions.Location = new System.Drawing.Point(12, 19);
            this.chkRs422DefaultOptions.Name = "chkRs422DefaultOptions";
            this.chkRs422DefaultOptions.Size = new System.Drawing.Size(60, 17);
            this.chkRs422DefaultOptions.TabIndex = 17;
            this.chkRs422DefaultOptions.Text = "Default";
            this.chkRs422DefaultOptions.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbRxTc);
            this.panel3.Controls.Add(this.rbRxTm);
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(9, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 45);
            this.panel3.TabIndex = 16;
            this.panel3.Tag = "Data Reception Configuration";
            // 
            // rbRxTc
            // 
            this.rbRxTc.AutoSize = true;
            this.rbRxTc.Location = new System.Drawing.Point(9, 3);
            this.rbRxTc.Name = "rbRxTc";
            this.rbRxTc.Size = new System.Drawing.Size(157, 17);
            this.rbRxTc.TabIndex = 12;
            this.rbRxTc.Text = "Rx TC ( with Enable Signal )";
            this.rbRxTc.UseVisualStyleBackColor = true;
            // 
            // rbRxTm
            // 
            this.rbRxTm.AutoSize = true;
            this.rbRxTm.Checked = true;
            this.rbRxTm.Location = new System.Drawing.Point(9, 23);
            this.rbRxTm.Name = "rbRxTm";
            this.rbRxTm.Size = new System.Drawing.Size(174, 17);
            this.rbRxTm.TabIndex = 13;
            this.rbRxTm.TabStop = true;
            this.rbRxTm.Text = "Rx TM ( without Enable Signal )";
            this.rbRxTm.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbTxClockIdleContinuous);
            this.panel1.Controls.Add(this.rbTxClockIdleNOTContinuous);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(198, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 45);
            this.panel1.TabIndex = 14;
            this.panel1.Tag = "Board Configuration";
            // 
            // rbTxClockIdleContinuous
            // 
            this.rbTxClockIdleContinuous.AutoSize = true;
            this.rbTxClockIdleContinuous.Checked = true;
            this.rbTxClockIdleContinuous.Location = new System.Drawing.Point(9, 3);
            this.rbTxClockIdleContinuous.Name = "rbTxClockIdleContinuous";
            this.rbTxClockIdleContinuous.Size = new System.Drawing.Size(143, 17);
            this.rbTxClockIdleContinuous.TabIndex = 8;
            this.rbTxClockIdleContinuous.TabStop = true;
            this.rbTxClockIdleContinuous.Text = "Tx Clock Idle Continuous";
            this.rbTxClockIdleContinuous.UseVisualStyleBackColor = true;
            // 
            // rbTxClockIdleNOTContinuous
            // 
            this.rbTxClockIdleNOTContinuous.AutoSize = true;
            this.rbTxClockIdleNOTContinuous.Location = new System.Drawing.Point(9, 23);
            this.rbTxClockIdleNOTContinuous.Name = "rbTxClockIdleNOTContinuous";
            this.rbTxClockIdleNOTContinuous.Size = new System.Drawing.Size(169, 17);
            this.rbTxClockIdleNOTContinuous.TabIndex = 9;
            this.rbTxClockIdleNOTContinuous.TabStop = true;
            this.rbTxClockIdleNOTContinuous.Text = "Tx Clock Idle NOT Continuous";
            this.rbTxClockIdleNOTContinuous.UseVisualStyleBackColor = true;
            // 
            // cmbOperationMode
            // 
            this.cmbOperationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperationMode.FormattingEnabled = true;
            this.cmbOperationMode.Items.AddRange(new object[] {
            "TT&C <--> UTMC",
            "TT&C <--> UPC",
            "TT&C <--> TT&C (debug)"});
            this.cmbOperationMode.Location = new System.Drawing.Point(198, 17);
            this.cmbOperationMode.Name = "cmbOperationMode";
            this.cmbOperationMode.Size = new System.Drawing.Size(179, 21);
            this.cmbOperationMode.TabIndex = 0;
            // 
            // lblOperationMode
            // 
            this.lblOperationMode.AutoSize = true;
            this.lblOperationMode.Location = new System.Drawing.Point(107, 20);
            this.lblOperationMode.Name = "lblOperationMode";
            this.lblOperationMode.Size = new System.Drawing.Size(85, 13);
            this.lblOperationMode.TabIndex = 6;
            this.lblOperationMode.Text = "Operation mode:";
            // 
            // cmbClockRate
            // 
            this.cmbClockRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClockRate.FormattingEnabled = true;
            this.cmbClockRate.Items.AddRange(new object[] {
            "2400",
            "4000",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbClockRate.Location = new System.Drawing.Point(263, 95);
            this.cmbClockRate.Name = "cmbClockRate";
            this.cmbClockRate.Size = new System.Drawing.Size(114, 21);
            this.cmbClockRate.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(201, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Baud rate:";
            // 
            // cmbRxChannel
            // 
            this.cmbRxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRxChannel.FormattingEnabled = true;
            this.cmbRxChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbRxChannel.Location = new System.Drawing.Point(76, 124);
            this.cmbRxChannel.Name = "cmbRxChannel";
            this.cmbRxChannel.Size = new System.Drawing.Size(103, 21);
            this.cmbRxChannel.TabIndex = 2;
            // 
            // cmbTxChannel
            // 
            this.cmbTxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTxChannel.FormattingEnabled = true;
            this.cmbTxChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbTxChannel.Location = new System.Drawing.Point(76, 97);
            this.cmbTxChannel.Name = "cmbTxChannel";
            this.cmbTxChannel.Size = new System.Drawing.Size(103, 21);
            this.cmbTxChannel.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Rx channel:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Tx channel:";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label14.Location = new System.Drawing.Point(10, 644);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(385, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "    Connect through named pipe";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStopBits);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.cmbDataBits);
            this.groupBox1.Controls.Add(this.cmbBaudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbComPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 528);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 107);
            this.groupBox1.TabIndex = 2;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPipeName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.rbPipeClient);
            this.groupBox2.Controls.Add(this.rbPipeServer);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 668);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 76);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Named pipe configuration";
            // 
            // txtPipeName
            // 
            this.txtPipeName.Location = new System.Drawing.Point(79, 45);
            this.txtPipeName.Name = "txtPipeName";
            this.txtPipeName.Size = new System.Drawing.Size(158, 20);
            this.txtPipeName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Pipe name:";
            // 
            // rbPipeClient
            // 
            this.rbPipeClient.AutoSize = true;
            this.rbPipeClient.Location = new System.Drawing.Point(163, 22);
            this.rbPipeClient.Name = "rbPipeClient";
            this.rbPipeClient.Size = new System.Drawing.Size(74, 17);
            this.rbPipeClient.TabIndex = 1;
            this.rbPipeClient.TabStop = true;
            this.rbPipeClient.Text = "Pipe client";
            this.rbPipeClient.UseVisualStyleBackColor = true;
            // 
            // rbPipeServer
            // 
            this.rbPipeServer.AutoSize = true;
            this.rbPipeServer.Location = new System.Drawing.Point(79, 22);
            this.rbPipeServer.Name = "rbPipeServer";
            this.rbPipeServer.Size = new System.Drawing.Size(78, 17);
            this.rbPipeServer.TabIndex = 0;
            this.rbPipeServer.TabStop = true;
            this.rbPipeServer.Text = "Pipe server";
            this.rbPipeServer.UseVisualStyleBackColor = true;
            this.rbPipeServer.CheckedChanged += new System.EventHandler(this.rbPipeServer_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "This is the:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btImportAmazoniaTm);
            this.groupBox3.Controls.Add(this.btImportTm);
            this.groupBox3.Location = new System.Drawing.Point(418, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 50);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import TM file options";
            // 
            // btImportAmazoniaTm
            // 
            this.btImportAmazoniaTm.Location = new System.Drawing.Point(136, 19);
            this.btImportAmazoniaTm.Name = "btImportAmazoniaTm";
            this.btImportAmazoniaTm.Size = new System.Drawing.Size(239, 23);
            this.btImportAmazoniaTm.TabIndex = 1;
            this.btImportAmazoniaTm.Text = "Amazonia-1 TM Data Field Frame Files (Folder)";
            this.btImportAmazoniaTm.UseVisualStyleBackColor = true;
            this.btImportAmazoniaTm.Click += new System.EventHandler(this.btImportAmazoniaTm_Click);
            // 
            // btImportTm
            // 
            this.btImportTm.Location = new System.Drawing.Point(6, 19);
            this.btImportTm.Name = "btImportTm";
            this.btImportTm.Size = new System.Drawing.Size(127, 23);
            this.btImportTm.TabIndex = 0;
            this.btImportTm.Text = "COMAV TM Frame File";
            this.btImportTm.UseVisualStyleBackColor = true;
            this.btImportTm.Click += new System.EventHandler(this.btImportTm_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(829, 932);
            this.tabControl1.TabIndex = 10;
            // 
            // tmrUpdateTmFrame
            // 
            this.tmrUpdateTmFrame.Tick += new System.EventHandler(this.tmrUpdateTmFrame_Tick);
            // 
            // FrmConnectionWithEgse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(833, 964);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btConnection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmConnectionWithEgse";
            this.Text = "Connection with COMAV";
            this.DockStateChanged += new System.EventHandler(this.FrmConnectionWithEgse_DockStateChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmConnectionWithEgse_FormClosed);
            this.Load += new System.EventHandler(this.FrmConnectionWithEgse_Load);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateTm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTmFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSessionStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClcw)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.grpSessionOptions.ResumeLayout(false);
            this.grpSessionOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btConnection;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Timer tmrAskForTM;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkPause;
        private System.Windows.Forms.NumericUpDown numUpdateTm;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.DataGridView gridTmFrame;
        private System.Windows.Forms.DataGridView gridClcw;
        private System.Windows.Forms.Button btSendCommandToFarm;
        private System.Windows.Forms.MaskedTextBox mskThirdOctect;
        private System.Windows.Forms.MaskedTextBox mskSecondOctect;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.MaskedTextBox mskFirstOctect;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cmbControlCommand;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btCancelFrameHeader;
        private System.Windows.Forms.Button btChangeFrameHeader;
        private System.Windows.Forms.CheckBox cbAutoCRC;
        private System.Windows.Forms.CheckBox cbAutoIncrement;
        private System.Windows.Forms.CheckBox cbAutoLength;
        private System.Windows.Forms.ComboBox cmbSeqFlags;
        private System.Windows.Forms.ComboBox cmbFrameType;
        private System.Windows.Forms.MaskedTextBox mskVCID;
        private System.Windows.Forms.MaskedTextBox mskSpacecraftID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox mskFrameCRC;
        private System.Windows.Forms.MaskedTextBox mskFrameSeq;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.MaskedTextBox mskFrameLength;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.MaskedTextBox mskMapId;
        private System.Windows.Forms.MaskedTextBox mskResB;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.MaskedTextBox mskResA;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.MaskedTextBox mskVersion;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbTcPacketsSeqControl;
        private System.Windows.Forms.Label lblTcPacketsSeqControl;
        private System.Windows.Forms.ComboBox cmbTcFramesSeqControl;
        private System.Windows.Forms.Label lblTcFramesSeqControl;
        private System.Windows.Forms.GroupBox grpSessionOptions;
        private System.Windows.Forms.NumericUpDown numSeconds;
        private System.Windows.Forms.CheckBox chkAskTm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAutoSequence;
        private System.Windows.Forms.CheckBox chkSyncronize;
        private System.Windows.Forms.CheckBox chkSaveSession;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbTmChannelCod;
        private System.Windows.Forms.Label lblTmChannelCod;
        public System.Windows.Forms.RadioButton rbFrames;
        private System.Windows.Forms.RadioButton rbPackets;
        public System.Windows.Forms.RadioButton rbNamedPipe;
        public System.Windows.Forms.RadioButton rbFile;
        public System.Windows.Forms.RadioButton rbSerial;
        public System.Windows.Forms.RadioButton rbRs422;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btResetRxFifo;
        private System.Windows.Forms.CheckBox chkRs422DefaultOptions;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbRxTc;
        private System.Windows.Forms.RadioButton rbRxTm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbTxClockIdleContinuous;
        private System.Windows.Forms.RadioButton rbTxClockIdleNOTContinuous;
        private System.Windows.Forms.ComboBox cmbOperationMode;
        private System.Windows.Forms.Label lblOperationMode;
        private System.Windows.Forms.ComboBox cmbClockRate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbRxChannel;
        private System.Windows.Forms.ComboBox cmbTxChannel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPipeName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbPipeClient;
        private System.Windows.Forms.RadioButton rbPipeServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btImportAmazoniaTm;
        private System.Windows.Forms.Button btImportTm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridView gridSessionStats;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Timer tmrUpdateTmFrame;
        private System.Windows.Forms.CheckBox chkDiscardIdleFrame;
        private System.Windows.Forms.CheckBox chkConfigureFarm;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.RadioButton rbTcpIp;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.RadioButton rdClient;
        private System.Windows.Forms.RadioButton rdServer;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtTelecommandDataPort;
        private System.Windows.Forms.TextBox txtTelemetryDataPort;
        private System.Windows.Forms.TextBox txtCortexControlDataPort;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtCopControlFlow;
        private System.Windows.Forms.TextBox txtCopTcDataFlow;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.CheckBox chkConfigureCOP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbCltu;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtCopMonitoringFlow;
    }
}
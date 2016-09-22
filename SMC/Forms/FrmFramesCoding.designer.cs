namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmFramesCoding
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btSaveToBinary = new System.Windows.Forms.Button();
            this.mskThirdOctect = new System.Windows.Forms.MaskedTextBox();
            this.mskSecondOctect = new System.Windows.Forms.MaskedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.mskFirstOctect = new System.Windows.Forms.MaskedTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btCreateNewCLTUFile = new System.Windows.Forms.Button();
            this.btCloseCLTUFile = new System.Windows.Forms.Button();
            this.btOpenCLTUFile = new System.Windows.Forms.Button();
            this.btAddCLTUToFile = new System.Windows.Forms.Button();
            this.gridTCFrame = new System.Windows.Forms.DataGridView();
            this.gridTCs = new System.Windows.Forms.DataGridView();
            this.btOpenTCFile = new System.Windows.Forms.Button();
            this.txtTCFilePath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbAutoCRC = new System.Windows.Forms.CheckBox();
            this.cbAutoIncrement = new System.Windows.Forms.CheckBox();
            this.cbAutoLength = new System.Windows.Forms.CheckBox();
            this.cmbControlCommand = new System.Windows.Forms.ComboBox();
            this.cmbSeqFlags = new System.Windows.Forms.ComboBox();
            this.cmbFrameType = new System.Windows.Forms.ComboBox();
            this.mskVCID = new System.Windows.Forms.MaskedTextBox();
            this.mskSpacecraftID = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mskFrameCRC = new System.Windows.Forms.MaskedTextBox();
            this.mskFrameSeq = new System.Windows.Forms.MaskedTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.mskFrameLength = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mskMapId = new System.Windows.Forms.MaskedTextBox();
            this.mskResB = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.mskResA = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.mskVersion = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkSearchAsm = new System.Windows.Forms.CheckBox();
            this.rbFramesWithNoCrc = new System.Windows.Forms.RadioButton();
            this.rbFramesWithCrc = new System.Windows.Forms.RadioButton();
            this.btReadBinaryTM = new System.Windows.Forms.Button();
            this.chkShowInRealTime = new System.Windows.Forms.CheckBox();
            this.btRealTimeReception = new System.Windows.Forms.Button();
            this.btPathFile = new System.Windows.Forms.Button();
            this.txtRealTimeReceptionPath = new System.Windows.Forms.TextBox();
            this.chkLogToFile = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.rbConvolutional = new System.Windows.Forms.RadioButton();
            this.rbReedSolomon = new System.Windows.Forms.RadioButton();
            this.rbNoCoding = new System.Windows.Forms.RadioButton();
            this.gridTMFrames = new System.Windows.Forms.DataGridView();
            this.label21 = new System.Windows.Forms.Label();
            this.btClearTMFrames = new System.Windows.Forms.Button();
            this.btDecode = new System.Windows.Forms.Button();
            this.btSelectDataFile = new System.Windows.Forms.Button();
            this.txtReceivedDataPath = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.newFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTCFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTCs)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTMFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 492);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btSaveToBinary);
            this.tabPage1.Controls.Add(this.mskThirdOctect);
            this.tabPage1.Controls.Add(this.mskSecondOctect);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.mskFirstOctect);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.btCreateNewCLTUFile);
            this.tabPage1.Controls.Add(this.btCloseCLTUFile);
            this.tabPage1.Controls.Add(this.btOpenCLTUFile);
            this.tabPage1.Controls.Add(this.btAddCLTUToFile);
            this.tabPage1.Controls.Add(this.gridTCFrame);
            this.tabPage1.Controls.Add(this.gridTCs);
            this.tabPage1.Controls.Add(this.btOpenTCFile);
            this.tabPage1.Controls.Add(this.txtTCFilePath);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.cbAutoCRC);
            this.tabPage1.Controls.Add(this.cbAutoIncrement);
            this.tabPage1.Controls.Add(this.cbAutoLength);
            this.tabPage1.Controls.Add(this.cmbControlCommand);
            this.tabPage1.Controls.Add(this.cmbSeqFlags);
            this.tabPage1.Controls.Add(this.cmbFrameType);
            this.tabPage1.Controls.Add(this.mskVCID);
            this.tabPage1.Controls.Add(this.mskSpacecraftID);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.mskFrameCRC);
            this.tabPage1.Controls.Add(this.mskFrameSeq);
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.mskFrameLength);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.mskMapId);
            this.tabPage1.Controls.Add(this.mskResB);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.mskResA);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.mskVersion);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TC Frame Coding";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btSaveToBinary
            // 
            this.btSaveToBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveToBinary.Location = new System.Drawing.Point(529, 436);
            this.btSaveToBinary.Name = "btSaveToBinary";
            this.btSaveToBinary.Size = new System.Drawing.Size(134, 24);
            this.btSaveToBinary.TabIndex = 25;
            this.btSaveToBinary.Text = "Save CLTU to Binary File";
            this.btSaveToBinary.UseVisualStyleBackColor = true;
            this.btSaveToBinary.Click += new System.EventHandler(this.btSaveToBinary_Click);
            // 
            // mskThirdOctect
            // 
            this.mskThirdOctect.BackColor = System.Drawing.SystemColors.Window;
            this.mskThirdOctect.Location = new System.Drawing.Point(125, 149);
            this.mskThirdOctect.Mask = "###";
            this.mskThirdOctect.Name = "mskThirdOctect";
            this.mskThirdOctect.PromptChar = ' ';
            this.mskThirdOctect.Size = new System.Drawing.Size(56, 20);
            this.mskThirdOctect.TabIndex = 17;
            this.mskThirdOctect.Text = "0";
            this.mskThirdOctect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskThirdOctect.Visible = false;
            this.mskThirdOctect.Leave += new System.EventHandler(this.mskThirdOctect_Leave);
            // 
            // mskSecondOctect
            // 
            this.mskSecondOctect.BackColor = System.Drawing.SystemColors.Window;
            this.mskSecondOctect.Location = new System.Drawing.Point(63, 149);
            this.mskSecondOctect.Mask = "###";
            this.mskSecondOctect.Name = "mskSecondOctect";
            this.mskSecondOctect.PromptChar = ' ';
            this.mskSecondOctect.Size = new System.Drawing.Size(56, 20);
            this.mskSecondOctect.TabIndex = 16;
            this.mskSecondOctect.Text = "0";
            this.mskSecondOctect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskSecondOctect.Visible = false;
            this.mskSecondOctect.Leave += new System.EventHandler(this.mskSecondOctect_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(122, 120);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 26);
            this.label18.TabIndex = 13;
            this.label18.Text = "3rd Octect\r\n(Next Seq.)";
            this.label18.Visible = false;
            // 
            // mskFirstOctect
            // 
            this.mskFirstOctect.BackColor = System.Drawing.SystemColors.Window;
            this.mskFirstOctect.Location = new System.Drawing.Point(7, 149);
            this.mskFirstOctect.Mask = "###";
            this.mskFirstOctect.Name = "mskFirstOctect";
            this.mskFirstOctect.PromptChar = ' ';
            this.mskFirstOctect.Size = new System.Drawing.Size(50, 20);
            this.mskFirstOctect.TabIndex = 15;
            this.mskFirstOctect.Text = "0";
            this.mskFirstOctect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFirstOctect.Visible = false;
            this.mskFirstOctect.Leave += new System.EventHandler(this.mskFirstOctect_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(60, 120);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "2nd Octect";
            this.label17.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 120);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 26);
            this.label16.TabIndex = 13;
            this.label16.Text = "Single\r\nOctect";
            this.label16.Visible = false;
            // 
            // btCreateNewCLTUFile
            // 
            this.btCreateNewCLTUFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCreateNewCLTUFile.Location = new System.Drawing.Point(3, 436);
            this.btCreateNewCLTUFile.Name = "btCreateNewCLTUFile";
            this.btCreateNewCLTUFile.Size = new System.Drawing.Size(128, 24);
            this.btCreateNewCLTUFile.TabIndex = 22;
            this.btCreateNewCLTUFile.Text = "Create New CLTU File";
            this.btCreateNewCLTUFile.UseVisualStyleBackColor = true;
            this.btCreateNewCLTUFile.Click += new System.EventHandler(this.btCreateNewCLTUFile_Click);
            // 
            // btCloseCLTUFile
            // 
            this.btCloseCLTUFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCloseCLTUFile.Enabled = false;
            this.btCloseCLTUFile.Location = new System.Drawing.Point(276, 436);
            this.btCloseCLTUFile.Name = "btCloseCLTUFile";
            this.btCloseCLTUFile.Size = new System.Drawing.Size(136, 24);
            this.btCloseCLTUFile.TabIndex = 24;
            this.btCloseCLTUFile.Text = "Close Current CLTU File";
            this.btCloseCLTUFile.UseVisualStyleBackColor = true;
            this.btCloseCLTUFile.Click += new System.EventHandler(this.btCloseCLTUFile_Click);
            // 
            // btOpenCLTUFile
            // 
            this.btOpenCLTUFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOpenCLTUFile.Location = new System.Drawing.Point(137, 436);
            this.btOpenCLTUFile.Name = "btOpenCLTUFile";
            this.btOpenCLTUFile.Size = new System.Drawing.Size(132, 24);
            this.btOpenCLTUFile.TabIndex = 23;
            this.btOpenCLTUFile.Text = "Open Existing CLTU File";
            this.btOpenCLTUFile.UseVisualStyleBackColor = true;
            this.btOpenCLTUFile.Click += new System.EventHandler(this.btOpenCLTUFile_Click);
            // 
            // btAddCLTUToFile
            // 
            this.btAddCLTUToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddCLTUToFile.Enabled = false;
            this.btAddCLTUToFile.Location = new System.Drawing.Point(669, 436);
            this.btAddCLTUToFile.Name = "btAddCLTUToFile";
            this.btAddCLTUToFile.Size = new System.Drawing.Size(99, 24);
            this.btAddCLTUToFile.TabIndex = 26;
            this.btAddCLTUToFile.Text = "Add CLTU to File";
            this.btAddCLTUToFile.UseVisualStyleBackColor = true;
            this.btAddCLTUToFile.Click += new System.EventHandler(this.btAddCLTUToFile_Click);
            // 
            // gridTCFrame
            // 
            this.gridTCFrame.AllowUserToDeleteRows = false;
            this.gridTCFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTCFrame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTCFrame.ColumnHeadersVisible = false;
            this.gridTCFrame.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridTCFrame.Location = new System.Drawing.Point(3, 358);
            this.gridTCFrame.Name = "gridTCFrame";
            this.gridTCFrame.RowHeadersVisible = false;
            this.gridTCFrame.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridTCFrame.Size = new System.Drawing.Size(767, 74);
            this.gridTCFrame.TabIndex = 21;
            // 
            // gridTCs
            // 
            this.gridTCs.AllowUserToAddRows = false;
            this.gridTCs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridTCs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTCs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTCs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTCs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridTCs.Location = new System.Drawing.Point(2, 144);
            this.gridTCs.Name = "gridTCs";
            this.gridTCs.RowHeadersVisible = false;
            this.gridTCs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTCs.Size = new System.Drawing.Size(768, 189);
            this.gridTCs.TabIndex = 20;
            this.gridTCs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTCs_CellValueChanged);
            this.gridTCs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTCs_CellContentClick);
            // 
            // btOpenTCFile
            // 
            this.btOpenTCFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenTCFile.Location = new System.Drawing.Point(681, 117);
            this.btOpenTCFile.Name = "btOpenTCFile";
            this.btOpenTCFile.Size = new System.Drawing.Size(87, 23);
            this.btOpenTCFile.TabIndex = 19;
            this.btOpenTCFile.Text = "Open TC File";
            this.btOpenTCFile.UseVisualStyleBackColor = true;
            this.btOpenTCFile.Click += new System.EventHandler(this.btOpenTCFile_Click);
            // 
            // txtTCFilePath
            // 
            this.txtTCFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTCFilePath.Location = new System.Drawing.Point(53, 120);
            this.txtTCFilePath.Name = "txtTCFilePath";
            this.txtTCFilePath.ReadOnly = true;
            this.txtTCFilePath.Size = new System.Drawing.Size(622, 20);
            this.txtTCFilePath.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "TC File:";
            // 
            // cbAutoCRC
            // 
            this.cbAutoCRC.AutoSize = true;
            this.cbAutoCRC.Checked = true;
            this.cbAutoCRC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoCRC.Location = new System.Drawing.Point(698, 76);
            this.cbAutoCRC.Name = "cbAutoCRC";
            this.cbAutoCRC.Size = new System.Drawing.Size(47, 17);
            this.cbAutoCRC.TabIndex = 14;
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
            this.cbAutoIncrement.Location = new System.Drawing.Point(477, 76);
            this.cbAutoIncrement.Name = "cbAutoIncrement";
            this.cbAutoIncrement.Size = new System.Drawing.Size(47, 17);
            this.cbAutoIncrement.TabIndex = 10;
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
            this.cbAutoLength.Location = new System.Drawing.Point(413, 76);
            this.cbAutoLength.Name = "cbAutoLength";
            this.cbAutoLength.Size = new System.Drawing.Size(47, 17);
            this.cbAutoLength.TabIndex = 8;
            this.cbAutoLength.TabStop = false;
            this.cbAutoLength.Text = "auto";
            this.cbAutoLength.UseVisualStyleBackColor = true;
            this.cbAutoLength.CheckedChanged += new System.EventHandler(this.cbUnlock_CheckedChanged);
            // 
            // cmbControlCommand
            // 
            this.cmbControlCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControlCommand.FormattingEnabled = true;
            this.cmbControlCommand.Items.AddRange(new object[] {
            "Invalid Control Cmd.",
            "SET V(R) Control Cmd.",
            "UNLOCK Control Cmd."});
            this.cmbControlCommand.Location = new System.Drawing.Point(53, 74);
            this.cmbControlCommand.Name = "cmbControlCommand";
            this.cmbControlCommand.Size = new System.Drawing.Size(115, 21);
            this.cmbControlCommand.TabIndex = 2;
            this.cmbControlCommand.Visible = false;
            this.cmbControlCommand.SelectedIndexChanged += new System.EventHandler(this.cmbControlCommand_SelectedIndexChanged);
            // 
            // cmbSeqFlags
            // 
            this.cmbSeqFlags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeqFlags.FormattingEnabled = true;
            this.cmbSeqFlags.Items.AddRange(new object[] {
            "[00] Cont. segment",
            "[01] First segment",
            "[10] Last segment",
            "[11] Unsegmented"});
            this.cmbSeqFlags.Location = new System.Drawing.Point(541, 50);
            this.cmbSeqFlags.Name = "cmbSeqFlags";
            this.cmbSeqFlags.Size = new System.Drawing.Size(118, 21);
            this.cmbSeqFlags.TabIndex = 11;
            this.cmbSeqFlags.SelectedIndexChanged += new System.EventHandler(this.cmbSeqFlags_SelectedIndexChanged);
            // 
            // cmbFrameType
            // 
            this.cmbFrameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrameType.FormattingEnabled = true;
            this.cmbFrameType.Items.AddRange(new object[] {
            "[00] Data Frame",
            "[01] Invalid Frame",
            "[10] By-Pass Frame",
            "[11] Control Frame"});
            this.cmbFrameType.Location = new System.Drawing.Point(53, 50);
            this.cmbFrameType.Name = "cmbFrameType";
            this.cmbFrameType.Size = new System.Drawing.Size(115, 21);
            this.cmbFrameType.Sorted = true;
            this.cmbFrameType.TabIndex = 1;
            this.cmbFrameType.SelectedIndexChanged += new System.EventHandler(this.cmbFrameType_SelectedIndexChanged);
            // 
            // mskVCID
            // 
            this.mskVCID.BackColor = System.Drawing.SystemColors.Window;
            this.mskVCID.Location = new System.Drawing.Point(295, 50);
            this.mskVCID.Mask = "##";
            this.mskVCID.Name = "mskVCID";
            this.mskVCID.PromptChar = ' ';
            this.mskVCID.Size = new System.Drawing.Size(56, 20);
            this.mskVCID.TabIndex = 5;
            this.mskVCID.Text = "0";
            this.mskVCID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskVCID.Leave += new System.EventHandler(this.mskVCID_Leave);
            // 
            // mskSpacecraftID
            // 
            this.mskSpacecraftID.BackColor = System.Drawing.SystemColors.Window;
            this.mskSpacecraftID.Location = new System.Drawing.Point(233, 50);
            this.mskSpacecraftID.Mask = "####";
            this.mskSpacecraftID.Name = "mskSpacecraftID";
            this.mskSpacecraftID.PromptChar = ' ';
            this.mskSpacecraftID.Size = new System.Drawing.Size(56, 20);
            this.mskSpacecraftID.TabIndex = 4;
            this.mskSpacecraftID.Text = "0";
            this.mskSpacecraftID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskSpacecraftID.Leave += new System.EventHandler(this.mskSpacecraftID_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(292, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 26);
            this.label7.TabIndex = 13;
            this.label7.Text = "Virtual\r\nChannel ID";
            // 
            // mskFrameCRC
            // 
            this.mskFrameCRC.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskFrameCRC.Location = new System.Drawing.Point(698, 50);
            this.mskFrameCRC.Mask = "AA-AA";
            this.mskFrameCRC.Name = "mskFrameCRC";
            this.mskFrameCRC.PromptChar = ' ';
            this.mskFrameCRC.ReadOnly = true;
            this.mskFrameCRC.Size = new System.Drawing.Size(67, 20);
            this.mskFrameCRC.TabIndex = 13;
            this.mskFrameCRC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFrameCRC.Leave += new System.EventHandler(this.mskFrameCRC_Leave);
            // 
            // mskFrameSeq
            // 
            this.mskFrameSeq.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskFrameSeq.Location = new System.Drawing.Point(477, 50);
            this.mskFrameSeq.Mask = "###";
            this.mskFrameSeq.Name = "mskFrameSeq";
            this.mskFrameSeq.PromptChar = ' ';
            this.mskFrameSeq.ReadOnly = true;
            this.mskFrameSeq.Size = new System.Drawing.Size(58, 20);
            this.mskFrameSeq.TabIndex = 9;
            this.mskFrameSeq.Text = "0";
            this.mskFrameSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFrameSeq.Leave += new System.EventHandler(this.mskFrameSeq_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(660, 21);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(30, 26);
            this.label25.TabIndex = 13;
            this.label25.Text = "MAP\r\nID";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(538, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(84, 13);
            this.label24.TabIndex = 13;
            this.label24.Text = "Sequence Flags";
            // 
            // mskFrameLength
            // 
            this.mskFrameLength.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mskFrameLength.Location = new System.Drawing.Point(413, 50);
            this.mskFrameLength.Mask = "###";
            this.mskFrameLength.Name = "mskFrameLength";
            this.mskFrameLength.PromptChar = ' ';
            this.mskFrameLength.ReadOnly = true;
            this.mskFrameLength.Size = new System.Drawing.Size(58, 20);
            this.mskFrameLength.TabIndex = 7;
            this.mskFrameLength.Text = "0";
            this.mskFrameLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFrameLength.Leave += new System.EventHandler(this.mskFrameLength_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(695, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 26);
            this.label13.TabIndex = 13;
            this.label13.Text = "Frame\r\nCRC";
            // 
            // mskMapId
            // 
            this.mskMapId.BackColor = System.Drawing.SystemColors.Window;
            this.mskMapId.Location = new System.Drawing.Point(663, 50);
            this.mskMapId.Mask = "##";
            this.mskMapId.Name = "mskMapId";
            this.mskMapId.PromptChar = ' ';
            this.mskMapId.Size = new System.Drawing.Size(27, 20);
            this.mskMapId.TabIndex = 12;
            this.mskMapId.Text = "0";
            this.mskMapId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskMapId.Leave += new System.EventHandler(this.mskMapId_Leave);
            // 
            // mskResB
            // 
            this.mskResB.BackColor = System.Drawing.SystemColors.Window;
            this.mskResB.Location = new System.Drawing.Point(357, 50);
            this.mskResB.Mask = "#";
            this.mskResB.Name = "mskResB";
            this.mskResB.PromptChar = ' ';
            this.mskResB.Size = new System.Drawing.Size(50, 20);
            this.mskResB.TabIndex = 6;
            this.mskResB.Text = "0";
            this.mskResB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskResB.Leave += new System.EventHandler(this.mskResB_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(474, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 26);
            this.label10.TabIndex = 13;
            this.label10.Text = "Frame Seq.\r\nNumber";
            // 
            // mskResA
            // 
            this.mskResA.BackColor = System.Drawing.SystemColors.Window;
            this.mskResA.Location = new System.Drawing.Point(177, 50);
            this.mskResA.Mask = "#";
            this.mskResA.Name = "mskResA";
            this.mskResA.PromptChar = ' ';
            this.mskResA.Size = new System.Drawing.Size(50, 20);
            this.mskResA.TabIndex = 3;
            this.mskResA.Text = "0";
            this.mskResA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskResA.Leave += new System.EventHandler(this.mskResA_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(410, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 26);
            this.label9.TabIndex = 13;
            this.label9.Text = "Frame\r\nLength";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 26);
            this.label6.TabIndex = 13;
            this.label6.Text = "Spacecraft\r\nID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(354, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 26);
            this.label8.TabIndex = 13;
            this.label8.Text = "Reserved\r\nField B";
            // 
            // mskVersion
            // 
            this.mskVersion.BackColor = System.Drawing.SystemColors.Window;
            this.mskVersion.Location = new System.Drawing.Point(7, 50);
            this.mskVersion.Mask = "#";
            this.mskVersion.Name = "mskVersion";
            this.mskVersion.PromptChar = ' ';
            this.mskVersion.Size = new System.Drawing.Size(41, 20);
            this.mskVersion.TabIndex = 0;
            this.mskVersion.Text = "0";
            this.mskVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskVersion.Leave += new System.EventHandler(this.mskVersion_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 26);
            this.label4.TabIndex = 13;
            this.label4.Text = "Reserved\r\nField A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 26);
            this.label3.TabIndex = 13;
            this.label3.Text = "Frame Type (by-pass + \r\ncontrol cmd. flags)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Comand:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 26);
            this.label5.TabIndex = 13;
            this.label5.Text = "Version\r\nNumber";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(3, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(770, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Raw Frame / CLTU";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.Window;
            this.label23.Location = new System.Drawing.Point(541, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(149, 15);
            this.label23.TabIndex = 5;
            this.label23.Text = "Segment Header";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.label1.Location = new System.Drawing.Point(3, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(770, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Frame Data Field";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.Window;
            this.label12.Location = new System.Drawing.Point(699, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 15);
            this.label12.TabIndex = 5;
            this.label12.Text = "Error Ctrl.";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.Window;
            this.label14.Location = new System.Drawing.Point(2, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(533, 15);
            this.label14.TabIndex = 5;
            this.label14.Text = "Frame Header";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkSearchAsm);
            this.tabPage2.Controls.Add(this.rbFramesWithNoCrc);
            this.tabPage2.Controls.Add(this.rbFramesWithCrc);
            this.tabPage2.Controls.Add(this.btReadBinaryTM);
            this.tabPage2.Controls.Add(this.chkShowInRealTime);
            this.tabPage2.Controls.Add(this.btRealTimeReception);
            this.tabPage2.Controls.Add(this.btPathFile);
            this.tabPage2.Controls.Add(this.txtRealTimeReceptionPath);
            this.tabPage2.Controls.Add(this.chkLogToFile);
            this.tabPage2.Controls.Add(this.label26);
            this.tabPage2.Controls.Add(this.rbBoth);
            this.tabPage2.Controls.Add(this.rbConvolutional);
            this.tabPage2.Controls.Add(this.rbReedSolomon);
            this.tabPage2.Controls.Add(this.rbNoCoding);
            this.tabPage2.Controls.Add(this.gridTMFrames);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.btClearTMFrames);
            this.tabPage2.Controls.Add(this.btDecode);
            this.tabPage2.Controls.Add(this.btSelectDataFile);
            this.tabPage2.Controls.Add(this.txtReceivedDataPath);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 466);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TM Frame Decoding";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkSearchAsm
            // 
            this.chkSearchAsm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSearchAsm.AutoSize = true;
            this.chkSearchAsm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSearchAsm.Enabled = false;
            this.chkSearchAsm.Location = new System.Drawing.Point(682, 53);
            this.chkSearchAsm.Name = "chkSearchAsm";
            this.chkSearchAsm.Size = new System.Drawing.Size(86, 17);
            this.chkSearchAsm.TabIndex = 22;
            this.chkSearchAsm.Text = "Search ASM";
            this.chkSearchAsm.UseVisualStyleBackColor = true;
            // 
            // rbFramesWithNoCrc
            // 
            this.rbFramesWithNoCrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbFramesWithNoCrc.AutoSize = true;
            this.rbFramesWithNoCrc.Checked = true;
            this.rbFramesWithNoCrc.Location = new System.Drawing.Point(243, 440);
            this.rbFramesWithNoCrc.Name = "rbFramesWithNoCrc";
            this.rbFramesWithNoCrc.Size = new System.Drawing.Size(121, 17);
            this.rbFramesWithNoCrc.TabIndex = 15;
            this.rbFramesWithNoCrc.TabStop = true;
            this.rbFramesWithNoCrc.Text = "Frames with no CRC";
            this.rbFramesWithNoCrc.UseVisualStyleBackColor = true;
            // 
            // rbFramesWithCrc
            // 
            this.rbFramesWithCrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbFramesWithCrc.AutoSize = true;
            this.rbFramesWithCrc.Location = new System.Drawing.Point(370, 440);
            this.rbFramesWithCrc.Name = "rbFramesWithCrc";
            this.rbFramesWithCrc.Size = new System.Drawing.Size(106, 17);
            this.rbFramesWithCrc.TabIndex = 16;
            this.rbFramesWithCrc.Text = "Frames with CRC";
            this.rbFramesWithCrc.UseVisualStyleBackColor = true;
            // 
            // btReadBinaryTM
            // 
            this.btReadBinaryTM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btReadBinaryTM.Location = new System.Drawing.Point(3, 437);
            this.btReadBinaryTM.Name = "btReadBinaryTM";
            this.btReadBinaryTM.Size = new System.Drawing.Size(234, 23);
            this.btReadBinaryTM.TabIndex = 14;
            this.btReadBinaryTM.Text = "Read TM Frames from Binary File (No Coding)";
            this.btReadBinaryTM.UseVisualStyleBackColor = true;
            // 
            // chkShowInRealTime
            // 
            this.chkShowInRealTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowInRealTime.AutoSize = true;
            this.chkShowInRealTime.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowInRealTime.Enabled = false;
            this.chkShowInRealTime.Location = new System.Drawing.Point(444, 129);
            this.chkShowInRealTime.Name = "chkShowInRealTime";
            this.chkShowInRealTime.Size = new System.Drawing.Size(115, 17);
            this.chkShowInRealTime.TabIndex = 11;
            this.chkShowInRealTime.Text = "Show in Real-Time";
            this.chkShowInRealTime.UseVisualStyleBackColor = true;
            // 
            // btRealTimeReception
            // 
            this.btRealTimeReception.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRealTimeReception.Enabled = false;
            this.btRealTimeReception.Location = new System.Drawing.Point(565, 125);
            this.btRealTimeReception.Name = "btRealTimeReception";
            this.btRealTimeReception.Size = new System.Drawing.Size(205, 23);
            this.btRealTimeReception.TabIndex = 12;
            this.btRealTimeReception.Text = "Start Real-Time Reception (No Coding)";
            this.btRealTimeReception.UseVisualStyleBackColor = true;
            this.btRealTimeReception.Click += new System.EventHandler(this.btRealTimeReception_Click_1);
            // 
            // btPathFile
            // 
            this.btPathFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPathFile.Enabled = false;
            this.btPathFile.Location = new System.Drawing.Point(565, 97);
            this.btPathFile.Name = "btPathFile";
            this.btPathFile.Size = new System.Drawing.Size(205, 23);
            this.btPathFile.TabIndex = 10;
            this.btPathFile.Text = "Create Log File";
            this.btPathFile.UseVisualStyleBackColor = true;
            this.btPathFile.Click += new System.EventHandler(this.btPathFile_Click_1);
            // 
            // txtRealTimeReceptionPath
            // 
            this.txtRealTimeReceptionPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRealTimeReceptionPath.Enabled = false;
            this.txtRealTimeReceptionPath.Location = new System.Drawing.Point(111, 99);
            this.txtRealTimeReceptionPath.Name = "txtRealTimeReceptionPath";
            this.txtRealTimeReceptionPath.ReadOnly = true;
            this.txtRealTimeReceptionPath.Size = new System.Drawing.Size(448, 20);
            this.txtRealTimeReceptionPath.TabIndex = 9;
            // 
            // chkLogToFile
            // 
            this.chkLogToFile.AutoSize = true;
            this.chkLogToFile.Location = new System.Drawing.Point(7, 101);
            this.chkLogToFile.Name = "chkLogToFile";
            this.chkLogToFile.Size = new System.Drawing.Size(98, 17);
            this.chkLogToFile.TabIndex = 8;
            this.chkLogToFile.Text = "Log Frame File:";
            this.chkLogToFile.UseVisualStyleBackColor = true;
            this.chkLogToFile.CheckedChanged += new System.EventHandler(this.chkLogToFile_CheckedChanged);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.SystemColors.Window;
            this.label26.Location = new System.Drawing.Point(3, 77);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(767, 15);
            this.label26.TabIndex = 21;
            this.label26.Text = "Real-Time Reception";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Enabled = false;
            this.rbBoth.Location = new System.Drawing.Point(412, 53);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(171, 17);
            this.rbBoth.TabIndex = 6;
            this.rbBoth.Text = "Reed-Solomon + Convolutional";
            this.rbBoth.UseVisualStyleBackColor = true;
            // 
            // rbConvolutional
            // 
            this.rbConvolutional.AutoSize = true;
            this.rbConvolutional.Enabled = false;
            this.rbConvolutional.Location = new System.Drawing.Point(317, 53);
            this.rbConvolutional.Name = "rbConvolutional";
            this.rbConvolutional.Size = new System.Drawing.Size(89, 17);
            this.rbConvolutional.TabIndex = 5;
            this.rbConvolutional.Text = "Convolutional";
            this.rbConvolutional.UseVisualStyleBackColor = true;
            // 
            // rbReedSolomon
            // 
            this.rbReedSolomon.AutoSize = true;
            this.rbReedSolomon.Enabled = false;
            this.rbReedSolomon.Location = new System.Drawing.Point(192, 53);
            this.rbReedSolomon.Name = "rbReedSolomon";
            this.rbReedSolomon.Size = new System.Drawing.Size(119, 17);
            this.rbReedSolomon.TabIndex = 4;
            this.rbReedSolomon.Text = "Reed-Solomon (RS)";
            this.rbReedSolomon.UseVisualStyleBackColor = true;
            // 
            // rbNoCoding
            // 
            this.rbNoCoding.AutoSize = true;
            this.rbNoCoding.Enabled = false;
            this.rbNoCoding.Location = new System.Drawing.Point(111, 53);
            this.rbNoCoding.Name = "rbNoCoding";
            this.rbNoCoding.Size = new System.Drawing.Size(75, 17);
            this.rbNoCoding.TabIndex = 3;
            this.rbNoCoding.Text = "No Coding";
            this.rbNoCoding.UseVisualStyleBackColor = true;
            // 
            // gridTMFrames
            // 
            this.gridTMFrames.AllowUserToAddRows = false;
            this.gridTMFrames.AllowUserToDeleteRows = false;
            this.gridTMFrames.AllowUserToResizeRows = false;
            this.gridTMFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTMFrames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTMFrames.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTMFrames.Location = new System.Drawing.Point(3, 170);
            this.gridTMFrames.Name = "gridTMFrames";
            this.gridTMFrames.ReadOnly = true;
            this.gridTMFrames.RowHeadersVisible = false;
            this.gridTMFrames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTMFrames.Size = new System.Drawing.Size(765, 261);
            this.gridTMFrames.TabIndex = 13;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.Window;
            this.label21.Location = new System.Drawing.Point(3, 152);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(767, 15);
            this.label21.TabIndex = 20;
            this.label21.Text = "TM Frames Found";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btClearTMFrames
            // 
            this.btClearTMFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearTMFrames.Enabled = false;
            this.btClearTMFrames.Location = new System.Drawing.Point(629, 437);
            this.btClearTMFrames.Name = "btClearTMFrames";
            this.btClearTMFrames.Size = new System.Drawing.Size(139, 23);
            this.btClearTMFrames.TabIndex = 17;
            this.btClearTMFrames.Text = "Clear TM Frames";
            this.btClearTMFrames.UseVisualStyleBackColor = true;
            this.btClearTMFrames.Click += new System.EventHandler(this.btClearTMFrames_Click);
            // 
            // btDecode
            // 
            this.btDecode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDecode.Enabled = false;
            this.btDecode.Location = new System.Drawing.Point(589, 49);
            this.btDecode.Name = "btDecode";
            this.btDecode.Size = new System.Drawing.Size(87, 23);
            this.btDecode.TabIndex = 7;
            this.btDecode.Text = "Try to Decode";
            this.btDecode.UseVisualStyleBackColor = true;
            this.btDecode.Click += new System.EventHandler(this.btDecode_Click);
            // 
            // btSelectDataFile
            // 
            this.btSelectDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectDataFile.Location = new System.Drawing.Point(589, 21);
            this.btSelectDataFile.Name = "btSelectDataFile";
            this.btSelectDataFile.Size = new System.Drawing.Size(181, 23);
            this.btSelectDataFile.TabIndex = 1;
            this.btSelectDataFile.Text = "Select Received Data File";
            this.btSelectDataFile.UseVisualStyleBackColor = true;
            this.btSelectDataFile.Click += new System.EventHandler(this.btSelectDataFile_Click);
            // 
            // txtReceivedDataPath
            // 
            this.txtReceivedDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceivedDataPath.Location = new System.Drawing.Point(111, 23);
            this.txtReceivedDataPath.Name = "txtReceivedDataPath";
            this.txtReceivedDataPath.ReadOnly = true;
            this.txtReceivedDataPath.Size = new System.Drawing.Size(472, 20);
            this.txtReceivedDataPath.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(4, 55);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 13);
            this.label22.TabIndex = 19;
            this.label22.Text = "Try to decode using:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 26);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 13);
            this.label19.TabIndex = 19;
            this.label19.Text = "Received Data File:";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.Window;
            this.label20.Location = new System.Drawing.Point(3, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(767, 15);
            this.label20.TabIndex = 16;
            this.label20.Text = "Received Data Source";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmFramesCoding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 492);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmFramesCoding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frames Encoder / Decoder";
            this.Load += new System.EventHandler(this.frmCoderDecoder_Load);
            this.DockStateChanged += new System.EventHandler(this.FrmFramesCoding_DockStateChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTCFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTCs)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTMFrames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFrameType;
        private System.Windows.Forms.MaskedTextBox mskVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbAutoIncrement;
        private System.Windows.Forms.CheckBox cbAutoLength;
        private System.Windows.Forms.MaskedTextBox mskVCID;
        private System.Windows.Forms.MaskedTextBox mskSpacecraftID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mskFrameSeq;
        private System.Windows.Forms.MaskedTextBox mskFrameLength;
        private System.Windows.Forms.MaskedTextBox mskResB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox mskResA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btOpenTCFile;
        private System.Windows.Forms.TextBox txtTCFilePath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView gridTCs;
        private System.Windows.Forms.Button btAddCLTUToFile;
        private System.Windows.Forms.Button btCreateNewCLTUFile;
        private System.Windows.Forms.Button btCloseCLTUFile;
        private System.Windows.Forms.Button btOpenCLTUFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridView gridTCFrame;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbAutoCRC;
        private System.Windows.Forms.MaskedTextBox mskFrameCRC;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbControlCommand;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox mskThirdOctect;
        private System.Windows.Forms.MaskedTextBox mskSecondOctect;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.MaskedTextBox mskFirstOctect;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.SaveFileDialog newFileDialog;
        private System.Windows.Forms.RadioButton rbBoth;
        private System.Windows.Forms.RadioButton rbConvolutional;
        private System.Windows.Forms.RadioButton rbReedSolomon;
        private System.Windows.Forms.RadioButton rbNoCoding;
        private System.Windows.Forms.DataGridView gridTMFrames;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btSelectDataFile;
        private System.Windows.Forms.TextBox txtReceivedDataPath;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btClearTMFrames;
        private System.Windows.Forms.ComboBox cmbSeqFlags;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.MaskedTextBox mskMapId;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btSaveToBinary;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtRealTimeReceptionPath;
        private System.Windows.Forms.CheckBox chkLogToFile;
        private System.Windows.Forms.Button btPathFile;
        private System.Windows.Forms.Button btReadBinaryTM;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.RadioButton rbFramesWithNoCrc;
        private System.Windows.Forms.RadioButton rbFramesWithCrc;
        public System.Windows.Forms.CheckBox chkShowInRealTime;
        public System.Windows.Forms.Button btRealTimeReception;
        private System.Windows.Forms.CheckBox chkSearchAsm;
        public System.Windows.Forms.Button btDecode;
        private System.Windows.Forms.SaveFileDialog saveDialog;
    }
}


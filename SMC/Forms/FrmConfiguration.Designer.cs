namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmConfiguration
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
            this.btSave = new System.Windows.Forms.Button();
            this.dtMissionEpoch = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbContourPrjs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblContourDBs = new System.Windows.Forms.Label();
            this.cmbContourDBs = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblMissionEpoch = new System.Windows.Forms.Label();
            this.dtpMissionEpoch = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btChangeImage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbTimeTagFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbDefaultSourceId = new System.Windows.Forms.ComboBox();
            this.cmbDefaultServiceType = new System.Windows.Forms.ComboBox();
            this.cmbDefaultApid = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btFlightSw = new System.Windows.Forms.Button();
            this.txtFlightSwpath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btDefaultPerspective = new System.Windows.Forms.Button();
            this.txtLayout = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btSqlQueryPath = new System.Windows.Forms.Button();
            this.txtSqlQueriesPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btTcExportPath = new System.Windows.Forms.Button();
            this.btTmImportPath = new System.Windows.Forms.Button();
            this.txtTcExportPath = new System.Windows.Forms.TextBox();
            this.txtTmImportPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GridSelectDb = new System.Windows.Forms.DataGridView();
            this.CnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CnSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ed_ad = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dtMissionEpoch.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSelectDb)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(595, 468);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(115, 23);
            this.btSave.TabIndex = 1;
            this.btSave.Text = "Save Configuration";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // dtMissionEpoch
            // 
            this.dtMissionEpoch.Controls.Add(this.groupBox6);
            this.dtMissionEpoch.Controls.Add(this.groupBox5);
            this.dtMissionEpoch.Controls.Add(this.groupBox1);
            this.dtMissionEpoch.Controls.Add(this.groupBox4);
            this.dtMissionEpoch.Controls.Add(this.groupBox7);
            this.dtMissionEpoch.Location = new System.Drawing.Point(4, 22);
            this.dtMissionEpoch.Name = "dtMissionEpoch";
            this.dtMissionEpoch.Padding = new System.Windows.Forms.Padding(3);
            this.dtMissionEpoch.Size = new System.Drawing.Size(699, 434);
            this.dtMissionEpoch.TabIndex = 2;
            this.dtMissionEpoch.Text = "Miscelaneous";
            this.dtMissionEpoch.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbContourPrjs);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.lblContourDBs);
            this.groupBox6.Controls.Add(this.cmbContourDBs);
            this.groupBox6.Location = new System.Drawing.Point(186, 195);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(343, 141);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Contour Integration";
            // 
            // cmbContourPrjs
            // 
            this.cmbContourPrjs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbContourPrjs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContourPrjs.FormattingEnabled = true;
            this.cmbContourPrjs.Location = new System.Drawing.Point(141, 48);
            this.cmbContourPrjs.Name = "cmbContourPrjs";
            this.cmbContourPrjs.Size = new System.Drawing.Size(196, 21);
            this.cmbContourPrjs.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Contour Project:";
            // 
            // lblContourDBs
            // 
            this.lblContourDBs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContourDBs.AutoSize = true;
            this.lblContourDBs.Location = new System.Drawing.Point(6, 24);
            this.lblContourDBs.Name = "lblContourDBs";
            this.lblContourDBs.Size = new System.Drawing.Size(129, 13);
            this.lblContourDBs.TabIndex = 1;
            this.lblContourDBs.Text = "Select Contour Database:";
            // 
            // cmbContourDBs
            // 
            this.cmbContourDBs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbContourDBs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContourDBs.FormattingEnabled = true;
            this.cmbContourDBs.Location = new System.Drawing.Point(141, 21);
            this.cmbContourDBs.Name = "cmbContourDBs";
            this.cmbContourDBs.Size = new System.Drawing.Size(196, 21);
            this.cmbContourDBs.TabIndex = 0;
            this.cmbContourDBs.SelectedIndexChanged += new System.EventHandler(this.cmbContourDBs_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblMissionEpoch);
            this.groupBox5.Controls.Add(this.dtpMissionEpoch);
            this.groupBox5.Location = new System.Drawing.Point(3, 347);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(525, 64);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mission Epoch";
            // 
            // lblMissionEpoch
            // 
            this.lblMissionEpoch.AutoSize = true;
            this.lblMissionEpoch.Location = new System.Drawing.Point(8, 28);
            this.lblMissionEpoch.Name = "lblMissionEpoch";
            this.lblMissionEpoch.Size = new System.Drawing.Size(98, 13);
            this.lblMissionEpoch.TabIndex = 1;
            this.lblMissionEpoch.Text = "Set Mission Epoch:";
            // 
            // dtpMissionEpoch
            // 
            this.dtpMissionEpoch.CustomFormat = "dd/MMM/yyyy HH:mm:ss";
            this.dtpMissionEpoch.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMissionEpoch.Location = new System.Drawing.Point(112, 25);
            this.dtpMissionEpoch.MinDate = new System.DateTime(1958, 1, 1, 0, 0, 0, 0);
            this.dtpMissionEpoch.Name = "dtpMissionEpoch";
            this.dtpMissionEpoch.Size = new System.Drawing.Size(153, 20);
            this.dtpMissionEpoch.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btChangeImage);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(3, 195);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 141);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background Image";
            // 
            // btChangeImage
            // 
            this.btChangeImage.Location = new System.Drawing.Point(6, 19);
            this.btChangeImage.Name = "btChangeImage";
            this.btChangeImage.Size = new System.Drawing.Size(150, 23);
            this.btChangeImage.TabIndex = 1;
            this.btChangeImage.Text = "Select Image to Load";
            this.btChangeImage.UseVisualStyleBackColor = true;
            this.btChangeImage.Click += new System.EventHandler(this.btChangeImage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(11, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.cmbTimeTagFormat);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(3, 128);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(526, 61);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Packets Processing";
            // 
            // cmbTimeTagFormat
            // 
            this.cmbTimeTagFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTimeTagFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeTagFormat.FormattingEnabled = true;
            this.cmbTimeTagFormat.Items.AddRange(new object[] {
            "Coarse + Fine (6 bytes)",
            "Coarse Only (4 bytes)"});
            this.cmbTimeTagFormat.Location = new System.Drawing.Point(183, 24);
            this.cmbTimeTagFormat.Name = "cmbTimeTagFormat";
            this.cmbTimeTagFormat.Size = new System.Drawing.Size(337, 21);
            this.cmbTimeTagFormat.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service Reports Time-Tag Format:";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.cmbDefaultSourceId);
            this.groupBox7.Controls.Add(this.cmbDefaultServiceType);
            this.groupBox7.Controls.Add(this.cmbDefaultApid);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Location = new System.Drawing.Point(5, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(524, 116);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Default field values for requests";
            // 
            // cmbDefaultSourceId
            // 
            this.cmbDefaultSourceId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDefaultSourceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultSourceId.FormattingEnabled = true;
            this.cmbDefaultSourceId.Location = new System.Drawing.Point(122, 22);
            this.cmbDefaultSourceId.Name = "cmbDefaultSourceId";
            this.cmbDefaultSourceId.Size = new System.Drawing.Size(396, 21);
            this.cmbDefaultSourceId.TabIndex = 5;
            // 
            // cmbDefaultServiceType
            // 
            this.cmbDefaultServiceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDefaultServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultServiceType.FormattingEnabled = true;
            this.cmbDefaultServiceType.Location = new System.Drawing.Point(122, 76);
            this.cmbDefaultServiceType.Name = "cmbDefaultServiceType";
            this.cmbDefaultServiceType.Size = new System.Drawing.Size(396, 21);
            this.cmbDefaultServiceType.TabIndex = 4;
            // 
            // cmbDefaultApid
            // 
            this.cmbDefaultApid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDefaultApid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultApid.FormattingEnabled = true;
            this.cmbDefaultApid.Location = new System.Drawing.Point(122, 49);
            this.cmbDefaultApid.Name = "cmbDefaultApid";
            this.cmbDefaultApid.Size = new System.Drawing.Size(396, 21);
            this.cmbDefaultApid.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Default Source ID:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Default Service Type:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Default APID:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(699, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File Paths && Addresses";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btFlightSw);
            this.groupBox3.Controls.Add(this.txtFlightSwpath);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.btDefaultPerspective);
            this.groupBox3.Controls.Add(this.txtLayout);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btSqlQueryPath);
            this.groupBox3.Controls.Add(this.txtSqlQueriesPath);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btTcExportPath);
            this.groupBox3.Controls.Add(this.btTmImportPath);
            this.groupBox3.Controls.Add(this.txtTcExportPath);
            this.groupBox3.Controls.Add(this.txtTmImportPath);
            this.groupBox3.Location = new System.Drawing.Point(6, 279);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(687, 153);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Files Import && Export";
            // 
            // btFlightSw
            // 
            this.btFlightSw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFlightSw.Location = new System.Drawing.Point(654, 121);
            this.btFlightSw.Name = "btFlightSw";
            this.btFlightSw.Size = new System.Drawing.Size(27, 23);
            this.btFlightSw.TabIndex = 11;
            this.btFlightSw.Text = "...";
            this.btFlightSw.UseVisualStyleBackColor = true;
            this.btFlightSw.Click += new System.EventHandler(this.btFlightSw_Click);
            // 
            // txtFlightSwpath
            // 
            this.txtFlightSwpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFlightSwpath.Location = new System.Drawing.Point(149, 123);
            this.txtFlightSwpath.Name = "txtFlightSwpath";
            this.txtFlightSwpath.ReadOnly = true;
            this.txtFlightSwpath.Size = new System.Drawing.Size(498, 20);
            this.txtFlightSwpath.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Flight SW export files path:";
            // 
            // btDefaultPerspective
            // 
            this.btDefaultPerspective.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDefaultPerspective.Location = new System.Drawing.Point(654, 16);
            this.btDefaultPerspective.Name = "btDefaultPerspective";
            this.btDefaultPerspective.Size = new System.Drawing.Size(27, 23);
            this.btDefaultPerspective.TabIndex = 8;
            this.btDefaultPerspective.Text = "...";
            this.btDefaultPerspective.UseVisualStyleBackColor = true;
            this.btDefaultPerspective.Click += new System.EventHandler(this.btDefaultPerspective_Click);
            // 
            // txtLayout
            // 
            this.txtLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLayout.BackColor = System.Drawing.SystemColors.Control;
            this.txtLayout.Location = new System.Drawing.Point(149, 19);
            this.txtLayout.Name = "txtLayout";
            this.txtLayout.Size = new System.Drawing.Size(498, 20);
            this.txtLayout.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Layout default path:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "SQL queries default path:";
            // 
            // btSqlQueryPath
            // 
            this.btSqlQueryPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSqlQueryPath.Location = new System.Drawing.Point(653, 95);
            this.btSqlQueryPath.Name = "btSqlQueryPath";
            this.btSqlQueryPath.Size = new System.Drawing.Size(27, 23);
            this.btSqlQueryPath.TabIndex = 5;
            this.btSqlQueryPath.Text = "...";
            this.btSqlQueryPath.UseVisualStyleBackColor = true;
            this.btSqlQueryPath.Click += new System.EventHandler(this.btSqlQueryPath_Click);
            // 
            // txtSqlQueriesPath
            // 
            this.txtSqlQueriesPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlQueriesPath.Location = new System.Drawing.Point(149, 97);
            this.txtSqlQueriesPath.Name = "txtSqlQueriesPath";
            this.txtSqlQueriesPath.ReadOnly = true;
            this.txtSqlQueriesPath.Size = new System.Drawing.Size(498, 20);
            this.txtSqlQueriesPath.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "TC export default path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "TM import default path:";
            // 
            // btTcExportPath
            // 
            this.btTcExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btTcExportPath.Location = new System.Drawing.Point(654, 69);
            this.btTcExportPath.Name = "btTcExportPath";
            this.btTcExportPath.Size = new System.Drawing.Size(27, 23);
            this.btTcExportPath.TabIndex = 3;
            this.btTcExportPath.Text = "...";
            this.btTcExportPath.UseVisualStyleBackColor = true;
            this.btTcExportPath.Click += new System.EventHandler(this.btTcExportPath_Click);
            // 
            // btTmImportPath
            // 
            this.btTmImportPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btTmImportPath.Location = new System.Drawing.Point(654, 43);
            this.btTmImportPath.Name = "btTmImportPath";
            this.btTmImportPath.Size = new System.Drawing.Size(27, 23);
            this.btTmImportPath.TabIndex = 1;
            this.btTmImportPath.Text = "...";
            this.btTmImportPath.UseVisualStyleBackColor = true;
            this.btTmImportPath.Click += new System.EventHandler(this.btTmImportPath_Click);
            // 
            // txtTcExportPath
            // 
            this.txtTcExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTcExportPath.Location = new System.Drawing.Point(149, 71);
            this.txtTcExportPath.Name = "txtTcExportPath";
            this.txtTcExportPath.ReadOnly = true;
            this.txtTcExportPath.Size = new System.Drawing.Size(499, 20);
            this.txtTcExportPath.TabIndex = 2;
            // 
            // txtTmImportPath
            // 
            this.txtTmImportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTmImportPath.Location = new System.Drawing.Point(149, 45);
            this.txtTmImportPath.Name = "txtTmImportPath";
            this.txtTmImportPath.ReadOnly = true;
            this.txtTmImportPath.Size = new System.Drawing.Size(499, 20);
            this.txtTmImportPath.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.GridSelectDb);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(687, 267);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database";
            // 
            // GridSelectDb
            // 
            this.GridSelectDb.AllowUserToAddRows = false;
            this.GridSelectDb.AllowUserToDeleteRows = false;
            this.GridSelectDb.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridSelectDb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridSelectDb.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CnName,
            this.CnSource,
            this.Ed_ad,
            this.colDelete});
            this.GridSelectDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridSelectDb.Location = new System.Drawing.Point(3, 16);
            this.GridSelectDb.Name = "GridSelectDb";
            this.GridSelectDb.ReadOnly = true;
            this.GridSelectDb.RowHeadersVisible = false;
            this.GridSelectDb.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridSelectDb.Size = new System.Drawing.Size(681, 248);
            this.GridSelectDb.TabIndex = 3;
            this.GridSelectDb.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridSelectDb_CellContentClick);
            // 
            // CnName
            // 
            this.CnName.FillWeight = 60F;
            this.CnName.HeaderText = "Name";
            this.CnName.Name = "CnName";
            this.CnName.ReadOnly = true;
            // 
            // CnSource
            // 
            this.CnSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CnSource.HeaderText = "Connection Path";
            this.CnSource.Name = "CnSource";
            this.CnSource.ReadOnly = true;
            // 
            // Ed_ad
            // 
            this.Ed_ad.FillWeight = 11F;
            this.Ed_ad.HeaderText = "Add / Edit";
            this.Ed_ad.Name = "Ed_ad";
            this.Ed_ad.ReadOnly = true;
            this.Ed_ad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colDelete
            // 
            this.colDelete.FillWeight = 11F;
            this.colDelete.HeaderText = "Delete";
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.dtMissionEpoch);
            this.tabControl1.Location = new System.Drawing.Point(3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(707, 460);
            this.tabControl1.TabIndex = 0;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // FrmConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(712, 496);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Configuration";
            this.DockStateChanged += new System.EventHandler(this.FrmConfiguration_DockStateChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmConfiguration_FormClosed);
            this.Load += new System.EventHandler(this.FrmConfiguration_Load);
            this.dtMissionEpoch.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridSelectDb)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TabPage dtMissionEpoch;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmbDefaultSourceId;
        private System.Windows.Forms.ComboBox cmbDefaultServiceType;
        private System.Windows.Forms.ComboBox cmbDefaultApid;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btFlightSw;
        private System.Windows.Forms.TextBox txtFlightSwpath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btDefaultPerspective;
        private System.Windows.Forms.TextBox txtLayout;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btSqlQueryPath;
        private System.Windows.Forms.TextBox txtSqlQueriesPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btTcExportPath;
        private System.Windows.Forms.Button btTmImportPath;
        private System.Windows.Forms.TextBox txtTcExportPath;
        private System.Windows.Forms.TextBox txtTmImportPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbTimeTagFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GridSelectDb;
        private System.Windows.Forms.DataGridViewTextBoxColumn CnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CnSource;
        private System.Windows.Forms.DataGridViewButtonColumn Ed_ad;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btChangeImage;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dtpMissionEpoch;
        private System.Windows.Forms.Label lblMissionEpoch;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblContourDBs;
        private System.Windows.Forms.ComboBox cmbContourDBs;
        private System.Windows.Forms.ComboBox cmbContourPrjs;
        private System.Windows.Forms.Label label2;
    }
}
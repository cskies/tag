namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSubtypes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.subtypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.btEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btConfirm = new System.Windows.Forms.ToolStripButton();
            this.btCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.btReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.colServiceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceSubtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rdbReport = new System.Windows.Forms.RadioButton();
            this.btNewDtField = new System.Windows.Forms.Button();
            this.rdbRequest = new System.Windows.Forms.RadioButton();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.cmbSameStructure = new System.Windows.Forms.ComboBox();
            this.lblSameStructure = new System.Windows.Forms.Label();
            this.lblTitleStructure = new System.Windows.Forms.Label();
            this.gridStructure = new System.Windows.Forms.DataGridView();
            this.colDataField = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colReadOnly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkAllowsRepetition = new System.Windows.Forms.CheckBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.numSubtype = new System.Windows.Forms.NumericUpDown();
            this.cmbService = new System.Windows.Forms.ComboBox();
            this.lblSubtype = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.subtypesTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.subtypesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.subtypesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStructure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSubtype)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // subtypesBindingSource
            // 
            this.subtypesBindingSource.DataMember = "subtypes";
            this.subtypesBindingSource.DataSource = this.dataSetApids;
            // 
            // dataSetApids
            // 
            this.dataSetApids.DataSetName = "DataSetApids";
            this.dataSetApids.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNew,
            this.btEdit,
            this.toolStripSeparator1,
            this.btDelete,
            this.toolStripSeparator2,
            this.btConfirm,
            this.btCancel,
            this.toolStripSeparator3,
            this.btRefresh,
            this.btReport,
            this.toolStripSeparator6});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 321);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btNew
            // 
            this.btNew.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.add_page_blue;
            this.btNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(51, 22);
            this.btNew.Text = "New";
            this.btNew.ToolTipText = "New Record (Insert)";
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // btEdit
            // 
            this.btEdit.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.edit_page_yellow;
            this.btEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(47, 22);
            this.btEdit.Text = "Edit";
            this.btEdit.ToolTipText = "Edit Selected Record (Enter)";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btDelete
            // 
            this.btDelete.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.delete_page_red;
            this.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(60, 22);
            this.btDelete.Text = "Delete";
            this.btDelete.ToolTipText = "Delete Selected Record (Del)";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btConfirm
            // 
            this.btConfirm.Enabled = false;
            this.btConfirm.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.confirmar;
            this.btConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(71, 22);
            this.btConfirm.Text = "Confirm";
            this.btConfirm.ToolTipText = "Confirm Insertion/Edition (Enter)";
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            // 
            // btCancel
            // 
            this.btCancel.Enabled = false;
            this.btCancel.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.left_red;
            this.btCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(63, 22);
            this.btCancel.Text = "Cancel";
            this.btCancel.ToolTipText = "Cancel Insertion/Edition (Esc)";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btRefresh
            // 
            this.btRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btRefresh.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.Refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(66, 22);
            this.btRefresh.Text = "Refresh";
            this.btRefresh.ToolTipText = "Refresh Grid (F5)";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btReport
            // 
            this.btReport.CheckOnClick = true;
            this.btReport.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.relatorio;
            this.btReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btReport.Name = "btReport";
            this.btReport.Size = new System.Drawing.Size(62, 22);
            this.btReport.Text = "Report";
            this.btReport.ToolTipText = "Allows to view, print and export reports in different formats.";
            this.btReport.Click += new System.EventHandler(this.btReport_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(58, 20);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 346);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridDatabase);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 318);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Editing";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // gridDatabase
            // 
            this.gridDatabase.AllowUserToAddRows = false;
            this.gridDatabase.AllowUserToDeleteRows = false;
            this.gridDatabase.AllowUserToOrderColumns = true;
            this.gridDatabase.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridDatabase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDatabase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDatabase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colServiceType,
            this.colServiceName,
            this.colServiceSubtype,
            this.colSubtypeDesc,
            this.colIsRequest});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDatabase.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDatabase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridDatabase.Location = new System.Drawing.Point(3, 3);
            this.gridDatabase.MultiSelect = false;
            this.gridDatabase.Name = "gridDatabase";
            this.gridDatabase.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDatabase.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridDatabase.RowHeadersVisible = false;
            this.gridDatabase.RowHeadersWidth = 25;
            this.gridDatabase.RowTemplate.ReadOnly = true;
            this.gridDatabase.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDatabase.Size = new System.Drawing.Size(746, 312);
            this.gridDatabase.StandardTab = true;
            this.gridDatabase.TabIndex = 0;
            this.gridDatabase.DoubleClick += new System.EventHandler(this.gridDatabase_DoubleClick);
            // 
            // colServiceType
            // 
            this.colServiceType.HeaderText = "Service Type";
            this.colServiceType.Name = "colServiceType";
            this.colServiceType.ReadOnly = true;
            // 
            // colServiceName
            // 
            this.colServiceName.HeaderText = "Service Name";
            this.colServiceName.Name = "colServiceName";
            this.colServiceName.ReadOnly = true;
            // 
            // colServiceSubtype
            // 
            this.colServiceSubtype.HeaderText = "Service Subtype";
            this.colServiceSubtype.Name = "colServiceSubtype";
            this.colServiceSubtype.ReadOnly = true;
            // 
            // colSubtypeDesc
            // 
            this.colSubtypeDesc.HeaderText = "Subtype Description";
            this.colSubtypeDesc.Name = "colSubtypeDesc";
            this.colSubtypeDesc.ReadOnly = true;
            // 
            // colIsRequest
            // 
            this.colIsRequest.HeaderText = "Request / Report";
            this.colIsRequest.Name = "colIsRequest";
            this.colIsRequest.ReadOnly = true;
            this.colIsRequest.Width = 120;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rdbReport);
            this.tabPage2.Controls.Add(this.btNewDtField);
            this.tabPage2.Controls.Add(this.rdbRequest);
            this.tabPage2.Controls.Add(this.btDown);
            this.tabPage2.Controls.Add(this.btUp);
            this.tabPage2.Controls.Add(this.btRemove);
            this.tabPage2.Controls.Add(this.btAdd);
            this.tabPage2.Controls.Add(this.cmbSameStructure);
            this.tabPage2.Controls.Add(this.lblSameStructure);
            this.tabPage2.Controls.Add(this.lblTitleStructure);
            this.tabPage2.Controls.Add(this.gridStructure);
            this.tabPage2.Controls.Add(this.chkAllowsRepetition);
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.lblDescription);
            this.tabPage2.Controls.Add(this.numSubtype);
            this.tabPage2.Controls.Add(this.cmbService);
            this.tabPage2.Controls.Add(this.lblSubtype);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lblService);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 318);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Browsing";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // rdbReport
            // 
            this.rdbReport.AutoSize = true;
            this.rdbReport.Location = new System.Drawing.Point(663, 10);
            this.rdbReport.Name = "rdbReport";
            this.rdbReport.Size = new System.Drawing.Size(57, 17);
            this.rdbReport.TabIndex = 2;
            this.rdbReport.Text = "Report";
            this.rdbReport.UseVisualStyleBackColor = true;
            // 
            // btNewDtField
            // 
            this.btNewDtField.Location = new System.Drawing.Point(654, 78);
            this.btNewDtField.Name = "btNewDtField";
            this.btNewDtField.Size = new System.Drawing.Size(93, 23);
            this.btNewDtField.TabIndex = 11;
            this.btNewDtField.Text = "New Data Field";
            this.btNewDtField.UseVisualStyleBackColor = true;
            this.btNewDtField.Click += new System.EventHandler(this.btNewDtField_Click);
            // 
            // rdbRequest
            // 
            this.rdbRequest.AutoSize = true;
            this.rdbRequest.Checked = true;
            this.rdbRequest.Location = new System.Drawing.Point(592, 10);
            this.rdbRequest.Name = "rdbRequest";
            this.rdbRequest.Size = new System.Drawing.Size(65, 17);
            this.rdbRequest.TabIndex = 1;
            this.rdbRequest.TabStop = true;
            this.rdbRequest.Text = "Request";
            this.rdbRequest.UseVisualStyleBackColor = true;
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(595, 78);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(56, 23);
            this.btDown.TabIndex = 10;
            this.btDown.Text = "Down";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(536, 78);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(56, 23);
            this.btUp.TabIndex = 9;
            this.btUp.Text = "Up";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(477, 78);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(56, 23);
            this.btRemove.TabIndex = 8;
            this.btRemove.Text = "Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(418, 78);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(56, 23);
            this.btAdd.TabIndex = 7;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // cmbSameStructure
            // 
            this.cmbSameStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSameStructure.FormattingEnabled = true;
            this.cmbSameStructure.Location = new System.Drawing.Point(141, 80);
            this.cmbSameStructure.Name = "cmbSameStructure";
            this.cmbSameStructure.Size = new System.Drawing.Size(271, 21);
            this.cmbSameStructure.TabIndex = 6;
            this.cmbSameStructure.SelectedIndexChanged += new System.EventHandler(this.cmbSameStructure_SelectedIndexChanged);
            // 
            // lblSameStructure
            // 
            this.lblSameStructure.AutoSize = true;
            this.lblSameStructure.Location = new System.Drawing.Point(5, 83);
            this.lblSameStructure.Name = "lblSameStructure";
            this.lblSameStructure.Size = new System.Drawing.Size(133, 13);
            this.lblSameStructure.TabIndex = 16;
            this.lblSameStructure.Text = "Use the same structure as:";
            // 
            // lblTitleStructure
            // 
            this.lblTitleStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleStructure.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitleStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleStructure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitleStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleStructure.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitleStructure.Location = new System.Drawing.Point(0, 59);
            this.lblTitleStructure.Name = "lblTitleStructure";
            this.lblTitleStructure.Size = new System.Drawing.Size(749, 16);
            this.lblTitleStructure.TabIndex = 15;
            this.lblTitleStructure.Text = " Subtype Structure";
            // 
            // gridStructure
            // 
            this.gridStructure.AllowUserToAddRows = false;
            this.gridStructure.AllowUserToDeleteRows = false;
            this.gridStructure.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Lavender;
            this.gridStructure.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStructure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStructure.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDataField,
            this.colReadOnly,
            this.colDefaultValue});
            this.gridStructure.Location = new System.Drawing.Point(0, 107);
            this.gridStructure.MultiSelect = false;
            this.gridStructure.Name = "gridStructure";
            this.gridStructure.RowHeadersVisible = false;
            this.gridStructure.RowHeadersWidth = 25;
            this.gridStructure.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStructure.Size = new System.Drawing.Size(749, 205);
            this.gridStructure.StandardTab = true;
            this.gridStructure.TabIndex = 12;
            // 
            // colDataField
            // 
            this.colDataField.HeaderText = "Data Field";
            this.colDataField.Name = "colDataField";
            this.colDataField.Width = 200;
            // 
            // colReadOnly
            // 
            this.colReadOnly.HeaderText = "Read Only";
            this.colReadOnly.Name = "colReadOnly";
            this.colReadOnly.Width = 120;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.HeaderText = "Default Value";
            this.colDefaultValue.Name = "colDefaultValue";
            this.colDefaultValue.Width = 200;
            // 
            // chkAllowsRepetition
            // 
            this.chkAllowsRepetition.AutoSize = true;
            this.chkAllowsRepetition.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllowsRepetition.Location = new System.Drawing.Point(485, 36);
            this.chkAllowsRepetition.Name = "chkAllowsRepetition";
            this.chkAllowsRepetition.Size = new System.Drawing.Size(251, 17);
            this.chkAllowsRepetition.TabIndex = 5;
            this.chkAllowsRepetition.Text = "This subtype allows repetition (has the \"N\" field)";
            this.chkAllowsRepetition.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(180, 35);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(296, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(114, 38);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description:";
            // 
            // numSubtype
            // 
            this.numSubtype.Location = new System.Drawing.Point(57, 36);
            this.numSubtype.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSubtype.Name = "numSubtype";
            this.numSubtype.Size = new System.Drawing.Size(51, 20);
            this.numSubtype.TabIndex = 3;
            this.numSubtype.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numSubtype.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbService
            // 
            this.cmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbService.FormattingEnabled = true;
            this.cmbService.Location = new System.Drawing.Point(57, 9);
            this.cmbService.Name = "cmbService";
            this.cmbService.Size = new System.Drawing.Size(419, 21);
            this.cmbService.TabIndex = 0;
            this.cmbService.SelectedIndexChanged += new System.EventHandler(this.cmbService_SelectedIndexChanged);
            // 
            // lblSubtype
            // 
            this.lblSubtype.AutoSize = true;
            this.lblSubtype.Location = new System.Drawing.Point(3, 38);
            this.lblSubtype.Name = "lblSubtype";
            this.lblSubtype.Size = new System.Drawing.Size(49, 13);
            this.lblSubtype.TabIndex = 1;
            this.lblSubtype.Text = "Subtype:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This subtype is a ...";
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.Location = new System.Drawing.Point(3, 12);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(46, 13);
            this.lblService.TabIndex = 0;
            this.lblService.Text = "Service:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(752, 318);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetApids_subtypes";
            reportDataSource1.Value = this.subtypesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptSubtypes.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(752, 318);
            this.reportViewer1.TabIndex = 0;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 175);
            // 
            // subtypesTableAdapter
            // 
            this.subtypesTableAdapter.ClearBeforeFill = true;
            // 
            // FrmSubtypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(760, 346);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmSubtypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subtypes";
            this.DockStateChanged += new System.EventHandler(this.FrmSubtypes_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSubtypes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.subtypesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStructure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSubtype)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btConfirm;
        private System.Windows.Forms.ToolStripButton btCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridView gridDatabase;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.Label lblSubtype;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.ComboBox cmbService;
        private System.Windows.Forms.NumericUpDown numSubtype;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.RadioButton rdbReport;
        private System.Windows.Forms.RadioButton rdbRequest;
        private System.Windows.Forms.CheckBox chkAllowsRepetition;
        private System.Windows.Forms.DataGridView gridStructure;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceSubtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtypeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsRequest;
        private System.Windows.Forms.Label lblTitleStructure;
        private System.Windows.Forms.ComboBox cmbSameStructure;
        private System.Windows.Forms.Label lblSameStructure;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btNewDtField;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataField;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colReadOnly;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private System.Windows.Forms.BindingSource subtypesBindingSource;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.subtypesTableAdapter subtypesTableAdapter;
    }
}
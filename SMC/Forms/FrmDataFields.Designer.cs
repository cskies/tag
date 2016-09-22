namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmDataFields
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
            this.data_fieldsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.colDataFieldId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkVariableSize = new System.Windows.Forms.CheckBox();
            this.gridList = new System.Windows.Forms.DataGridView();
            this.coValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btNewList = new System.Windows.Forms.Button();
            this.cmbListId = new System.Windows.Forms.ComboBox();
            this.lblListId = new System.Windows.Forms.Label();
            this.lblTitleListOfValues = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.numBits = new System.Windows.Forms.NumericUpDown();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.txtFieldId = new System.Windows.Forms.TextBox();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblNumberBits = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.lblFieldId = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
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
            this.data_fieldsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.data_fieldsTableAdapter();
            this.data_fields_listsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.data_fields_listsTableAdapter();
            this.data_fields_listsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.data_fieldsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBits)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_fields_listsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // data_fieldsBindingSource
            // 
            this.data_fieldsBindingSource.DataMember = "data_fields";
            this.data_fieldsBindingSource.DataSource = this.dataSetApids;
            // 
            // dataSetApids
            // 
            this.dataSetApids.DataSetName = "DataSetApids";
            this.dataSetApids.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(696, 407);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridDatabase);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(688, 381);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browsing";
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
            this.colDataFieldId,
            this.colFieldName,
            this.colNumBits,
            this.colFieldType});
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
            this.gridDatabase.Size = new System.Drawing.Size(682, 375);
            this.gridDatabase.StandardTab = true;
            this.gridDatabase.TabIndex = 1;
            this.gridDatabase.DoubleClick += new System.EventHandler(this.gridDatabase_DoubleClick);
            // 
            // colDataFieldId
            // 
            this.colDataFieldId.HeaderText = "Field Id";
            this.colDataFieldId.Name = "colDataFieldId";
            this.colDataFieldId.ReadOnly = true;
            this.colDataFieldId.Width = 80;
            // 
            // colFieldName
            // 
            this.colFieldName.HeaderText = "Field Name";
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.ReadOnly = true;
            this.colFieldName.Width = 200;
            // 
            // colNumBits
            // 
            this.colNumBits.HeaderText = "Number Of Bits";
            this.colNumBits.Name = "colNumBits";
            this.colNumBits.ReadOnly = true;
            this.colNumBits.Width = 80;
            // 
            // colFieldType
            // 
            this.colFieldType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFieldType.HeaderText = "Field Type";
            this.colFieldType.Name = "colFieldType";
            this.colFieldType.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkVariableSize);
            this.tabPage2.Controls.Add(this.gridList);
            this.tabPage2.Controls.Add(this.btNewList);
            this.tabPage2.Controls.Add(this.cmbListId);
            this.tabPage2.Controls.Add(this.lblListId);
            this.tabPage2.Controls.Add(this.lblTitleListOfValues);
            this.tabPage2.Controls.Add(this.txtTableName);
            this.tabPage2.Controls.Add(this.cmbFieldType);
            this.tabPage2.Controls.Add(this.numBits);
            this.tabPage2.Controls.Add(this.txtFieldName);
            this.tabPage2.Controls.Add(this.txtFieldId);
            this.tabPage2.Controls.Add(this.lblTableName);
            this.tabPage2.Controls.Add(this.lblFieldType);
            this.tabPage2.Controls.Add(this.lblNumberBits);
            this.tabPage2.Controls.Add(this.lblFieldName);
            this.tabPage2.Controls.Add(this.lblFieldId);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(688, 381);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editing";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // chkVariableSize
            // 
            this.chkVariableSize.AutoSize = true;
            this.chkVariableSize.Location = new System.Drawing.Point(470, 4);
            this.chkVariableSize.Name = "chkVariableSize";
            this.chkVariableSize.Size = new System.Drawing.Size(178, 17);
            this.chkVariableSize.TabIndex = 3;
            this.chkVariableSize.Text = "this data field has a variable size";
            this.chkVariableSize.UseVisualStyleBackColor = true;
            this.chkVariableSize.CheckedChanged += new System.EventHandler(this.chkVariableSize_CheckedChanged);
            // 
            // gridList
            // 
            this.gridList.AllowUserToAddRows = false;
            this.gridList.AllowUserToDeleteRows = false;
            this.gridList.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Lavender;
            this.gridList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coValue,
            this.colDefaultValue});
            this.gridList.Location = new System.Drawing.Point(0, 101);
            this.gridList.MultiSelect = false;
            this.gridList.Name = "gridList";
            this.gridList.RowHeadersVisible = false;
            this.gridList.RowHeadersWidth = 25;
            this.gridList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridList.Size = new System.Drawing.Size(688, 277);
            this.gridList.StandardTab = true;
            this.gridList.TabIndex = 10;
            // 
            // coValue
            // 
            this.coValue.HeaderText = "Value";
            this.coValue.Name = "coValue";
            this.coValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.coValue.Width = 120;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDefaultValue.HeaderText = "Text";
            this.colDefaultValue.Name = "colDefaultValue";
            this.colDefaultValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btNewList
            // 
            this.btNewList.Location = new System.Drawing.Point(330, 74);
            this.btNewList.Name = "btNewList";
            this.btNewList.Size = new System.Drawing.Size(118, 23);
            this.btNewList.TabIndex = 7;
            this.btNewList.Text = "Create New List";
            this.btNewList.UseVisualStyleBackColor = true;
            this.btNewList.Click += new System.EventHandler(this.btNewList_Click);
            // 
            // cmbListId
            // 
            this.cmbListId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListId.FormattingEnabled = true;
            this.cmbListId.Location = new System.Drawing.Point(48, 74);
            this.cmbListId.Name = "cmbListId";
            this.cmbListId.Size = new System.Drawing.Size(273, 21);
            this.cmbListId.TabIndex = 6;
            this.cmbListId.SelectedIndexChanged += new System.EventHandler(this.cmbListId_SelectedIndexChanged);
            // 
            // lblListId
            // 
            this.lblListId.AutoSize = true;
            this.lblListId.Location = new System.Drawing.Point(4, 77);
            this.lblListId.Name = "lblListId";
            this.lblListId.Size = new System.Drawing.Size(38, 13);
            this.lblListId.TabIndex = 12;
            this.lblListId.Text = "List Id:";
            // 
            // lblTitleListOfValues
            // 
            this.lblTitleListOfValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleListOfValues.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitleListOfValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleListOfValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleListOfValues.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitleListOfValues.Location = new System.Drawing.Point(0, 54);
            this.lblTitleListOfValues.Name = "lblTitleListOfValues";
            this.lblTitleListOfValues.Size = new System.Drawing.Size(688, 16);
            this.lblTitleListOfValues.TabIndex = 10;
            this.lblTitleListOfValues.Text = "List of Values";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(412, 28);
            this.txtTableName.MaxLength = 100;
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(270, 20);
            this.txtTableName.TabIndex = 5;
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Items.AddRange(new object[] {
            "Boolean",
            "Numeric",
            "Raw Hex",
            "List",
            "Table"});
            this.cmbFieldType.Location = new System.Drawing.Point(188, 2);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(133, 21);
            this.cmbFieldType.TabIndex = 1;
            this.cmbFieldType.SelectedIndexChanged += new System.EventHandler(this.cmbFieldType_SelectedIndexChanged);
            // 
            // numBits
            // 
            this.numBits.Location = new System.Drawing.Point(412, 3);
            this.numBits.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numBits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBits.Name = "numBits";
            this.numBits.Size = new System.Drawing.Size(43, 20);
            this.numBits.TabIndex = 2;
            this.numBits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numBits.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(73, 28);
            this.txtFieldName.MaxLength = 50;
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(248, 20);
            this.txtFieldName.TabIndex = 4;
            // 
            // txtFieldId
            // 
            this.txtFieldId.Location = new System.Drawing.Point(73, 2);
            this.txtFieldId.MaxLength = 5;
            this.txtFieldId.Name = "txtFieldId";
            this.txtFieldId.Size = new System.Drawing.Size(44, 20);
            this.txtFieldId.TabIndex = 0;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(327, 31);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(68, 13);
            this.lblTableName.TabIndex = 4;
            this.lblTableName.Text = "Table Name:";
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(123, 5);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(59, 13);
            this.lblFieldType.TabIndex = 3;
            this.lblFieldType.Text = "Field Type:";
            // 
            // lblNumberBits
            // 
            this.lblNumberBits.AutoSize = true;
            this.lblNumberBits.Location = new System.Drawing.Point(327, 5);
            this.lblNumberBits.Name = "lblNumberBits";
            this.lblNumberBits.Size = new System.Drawing.Size(79, 13);
            this.lblNumberBits.TabIndex = 2;
            this.lblNumberBits.Text = "Number of Bits:";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(4, 31);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(63, 13);
            this.lblFieldName.TabIndex = 1;
            this.lblFieldName.Text = "Field Name:";
            // 
            // lblFieldId
            // 
            this.lblFieldId.AutoSize = true;
            this.lblFieldId.Location = new System.Drawing.Point(4, 5);
            this.lblFieldId.Name = "lblFieldId";
            this.lblFieldId.Size = new System.Drawing.Size(44, 13);
            this.lblFieldId.TabIndex = 0;
            this.lblFieldId.Text = "Field Id:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(688, 381);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetApids_data_fields";
            reportDataSource1.Value = this.data_fieldsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptDataFields.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(688, 381);
            this.reportViewer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
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
            this.btReport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 382);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(696, 25);
            this.toolStrip1.TabIndex = 1;
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
            // data_fieldsTableAdapter
            // 
            this.data_fieldsTableAdapter.ClearBeforeFill = true;
            // 
            // data_fields_listsTableAdapter
            // 
            this.data_fields_listsTableAdapter.ClearBeforeFill = true;
            // 
            // data_fields_listsBindingSource
            // 
            this.data_fields_listsBindingSource.DataMember = "data_fields_lists";
            this.data_fields_listsBindingSource.DataSource = this.dataSetApids;
            // 
            // FrmDataFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(696, 407);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmDataFields";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Fields";
            this.DockStateChanged += new System.EventHandler(this.FrmDataFields_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDataFields_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.data_fieldsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBits)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_fields_listsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btConfirm;
        private System.Windows.Forms.ToolStripButton btCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.DataGridView gridDatabase;
        private System.Windows.Forms.Label lblFieldId;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Label lblNumberBits;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.ComboBox cmbFieldType;
        private System.Windows.Forms.NumericUpDown numBits;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.TextBox txtFieldId;
        private System.Windows.Forms.Label lblTitleListOfValues;
        private System.Windows.Forms.ComboBox cmbListId;
        private System.Windows.Forms.Label lblListId;
        private System.Windows.Forms.Button btNewList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataFieldId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldType;
        private System.Windows.Forms.DataGridView gridList;
        private System.Windows.Forms.DataGridViewTextBoxColumn coValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultValue;
        private System.Windows.Forms.CheckBox chkVariableSize;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStripButton btReport;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.data_fieldsTableAdapter data_fieldsTableAdapter;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.data_fields_listsTableAdapter data_fields_listsTableAdapter;
        private System.Windows.Forms.BindingSource data_fields_listsBindingSource;
        private System.Windows.Forms.BindingSource data_fieldsBindingSource;
    }
}
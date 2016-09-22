namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmViewerSetup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSubtypeId = new System.Windows.Forms.TextBox();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numCollumns = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitleStructure = new System.Windows.Forms.Label();
            this.gridSetup = new System.Windows.Forms.DataGridView();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblSubtype = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCollumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetup)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
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
            this.btRefresh});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 328);
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
            this.tabControl1.Size = new System.Drawing.Size(760, 353);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridDatabase);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 325);
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
            this.gridDatabase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDatabase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.gridDatabase.Size = new System.Drawing.Size(746, 319);
            this.gridDatabase.StandardTab = true;
            this.gridDatabase.TabIndex = 0;
            this.gridDatabase.DoubleClick += new System.EventHandler(this.gridDatabase_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSubtypeId);
            this.tabPage2.Controls.Add(this.numRows);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.numCollumns);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lblTitleStructure);
            this.tabPage2.Controls.Add(this.gridSetup);
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.lblSubtype);
            this.tabPage2.Controls.Add(this.lblService);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 325);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Browsing";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // txtSubtypeId
            // 
            this.txtSubtypeId.Location = new System.Drawing.Point(56, 5);
            this.txtSubtypeId.Name = "txtSubtypeId";
            this.txtSubtypeId.ReadOnly = true;
            this.txtSubtypeId.Size = new System.Drawing.Size(50, 20);
            this.txtSubtypeId.TabIndex = 20;
            this.txtSubtypeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numRows
            // 
            this.numRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numRows.Location = new System.Drawing.Point(691, 6);
            this.numRows.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(51, 20);
            this.numRows.TabIndex = 19;
            this.numRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRows.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRows.ValueChanged += new System.EventHandler(this.numRows_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(648, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Rows:";
            // 
            // numCollumns
            // 
            this.numCollumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numCollumns.Location = new System.Drawing.Point(591, 6);
            this.numCollumns.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numCollumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCollumns.Name = "numCollumns";
            this.numCollumns.Size = new System.Drawing.Size(51, 20);
            this.numCollumns.TabIndex = 17;
            this.numCollumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCollumns.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCollumns.ValueChanged += new System.EventHandler(this.numCollumns_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(533, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Collumns:";
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
            this.lblTitleStructure.Location = new System.Drawing.Point(3, 31);
            this.lblTitleStructure.Name = "lblTitleStructure";
            this.lblTitleStructure.Size = new System.Drawing.Size(741, 16);
            this.lblTitleStructure.TabIndex = 15;
            this.lblTitleStructure.Text = "View Setup: right-click each cell in order to configure its highlighting option.";
            // 
            // gridSetup
            // 
            this.gridSetup.AllowDrop = true;
            this.gridSetup.AllowUserToAddRows = false;
            this.gridSetup.AllowUserToDeleteRows = false;
            this.gridSetup.AllowUserToResizeRows = false;
            this.gridSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSetup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSetup.ColumnHeadersVisible = false;
            this.gridSetup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridSetup.Location = new System.Drawing.Point(0, 52);
            this.gridSetup.MultiSelect = false;
            this.gridSetup.Name = "gridSetup";
            this.gridSetup.RowHeadersVisible = false;
            this.gridSetup.RowHeadersWidth = 25;
            this.gridSetup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSetup.Size = new System.Drawing.Size(746, 267);
            this.gridSetup.StandardTab = true;
            this.gridSetup.TabIndex = 12;
            this.gridSetup.SizeChanged += new System.EventHandler(this.gridSetup_SizeChanged);
            this.gridSetup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridSetup_MouseClick);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(180, 4);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(347, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // lblSubtype
            // 
            this.lblSubtype.AutoSize = true;
            this.lblSubtype.Location = new System.Drawing.Point(113, 8);
            this.lblSubtype.Name = "lblSubtype";
            this.lblSubtype.Size = new System.Drawing.Size(63, 13);
            this.lblSubtype.TabIndex = 1;
            this.lblSubtype.Text = "Description:";
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.Location = new System.Drawing.Point(3, 8);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(47, 13);
            this.lblService.TabIndex = 0;
            this.lblService.Text = "View ID:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(752, 325);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetApids_subtypes";
            reportDataSource1.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptSubtypes.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(752, 325);
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
            // FrmViewerSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(760, 353);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmViewerSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Housekeeping Parameters Viewers Setup";
            this.DockStateChanged += new System.EventHandler(this.FrmViewerSetup_DockStateChanged);
            this.Load += new System.EventHandler(this.FrmViewerSetup_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmViewerSetup_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCollumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetup)).EndInit();
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
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DataGridView gridSetup;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Label lblTitleStructure;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCollumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubtypeId;
    }
}
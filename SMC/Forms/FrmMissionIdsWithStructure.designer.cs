namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmMissionIdsWithStructure
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMissionIdsWithStructure));
            this.reportdefinitionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.event_reportsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btNewDataField = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.gridStructure = new System.Windows.Forms.DataGridView();
            this.colComboStructureFields = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lblStructure = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
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
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.eventreportsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.event_reportsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.event_reportsTableAdapter();
            this.tc_failure_codesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tc_failure_codesTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.tc_failure_codesTableAdapter();
            this.reportdefinitionsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.reportdefinitionsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.reportdefinitionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.event_reportsBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStructure)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventreportsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tc_failure_codesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportdefinitionsBindingSource
            // 
            this.reportdefinitionsBindingSource.DataMember = "reportdefinitions";
            this.reportdefinitionsBindingSource.DataSource = this.dataSetApids;
            // 
            // dataSetApids
            // 
            this.dataSetApids.DataSetName = "DataSetApids";
            this.dataSetApids.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // event_reportsBindingSource
            // 
            this.event_reportsBindingSource.DataMember = "event_reports";
            this.event_reportsBindingSource.DataSource = this.dataSetApids;
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
            this.tabControl1.Size = new System.Drawing.Size(624, 370);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridDatabase);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 344);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // gridDatabase
            // 
            this.gridDatabase.AllowUserToAddRows = false;
            this.gridDatabase.AllowUserToDeleteRows = false;
            this.gridDatabase.AllowUserToOrderColumns = true;
            this.gridDatabase.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridDatabase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDatabase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridDatabase.Location = new System.Drawing.Point(3, 3);
            this.gridDatabase.MultiSelect = false;
            this.gridDatabase.Name = "gridDatabase";
            this.gridDatabase.ReadOnly = true;
            this.gridDatabase.RowHeadersVisible = false;
            this.gridDatabase.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDatabase.Size = new System.Drawing.Size(610, 338);
            this.gridDatabase.StandardTab = true;
            this.gridDatabase.TabIndex = 0;
            this.gridDatabase.DoubleClick += new System.EventHandler(this.gridDatabase_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.txtKey);
            this.tabPage2.Controls.Add(this.lblDescription);
            this.tabPage2.Controls.Add(this.btNewDataField);
            this.tabPage2.Controls.Add(this.btDown);
            this.tabPage2.Controls.Add(this.btUp);
            this.tabPage2.Controls.Add(this.btRemove);
            this.tabPage2.Controls.Add(this.btAdd);
            this.tabPage2.Controls.Add(this.gridStructure);
            this.tabPage2.Controls.Add(this.lblStructure);
            this.tabPage2.Controls.Add(this.lblKey);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 344);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(103, 36);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(505, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(103, 10);
            this.txtKey.MaxLength = 3;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(53, 20);
            this.txtKey.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(4, 39);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "Description";
            // 
            // btNewDataField
            // 
            this.btNewDataField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btNewDataField.Location = new System.Drawing.Point(515, 83);
            this.btNewDataField.Name = "btNewDataField";
            this.btNewDataField.Size = new System.Drawing.Size(93, 23);
            this.btNewDataField.TabIndex = 6;
            this.btNewDataField.Text = "New Data Field";
            this.btNewDataField.UseVisualStyleBackColor = true;
            this.btNewDataField.Click += new System.EventHandler(this.btNewDataField_Click);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(190, 83);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(56, 23);
            this.btDown.TabIndex = 5;
            this.btDown.Text = "Down";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(128, 83);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(56, 23);
            this.btUp.TabIndex = 4;
            this.btUp.Text = "Up";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(66, 83);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(56, 23);
            this.btRemove.TabIndex = 3;
            this.btRemove.Text = "Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(4, 83);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(56, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // gridStructure
            // 
            this.gridStructure.AllowUserToAddRows = false;
            this.gridStructure.AllowUserToDeleteRows = false;
            this.gridStructure.AllowUserToResizeRows = false;
            this.gridStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStructure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStructure.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colComboStructureFields});
            this.gridStructure.Location = new System.Drawing.Point(3, 111);
            this.gridStructure.Name = "gridStructure";
            this.gridStructure.RowHeadersVisible = false;
            this.gridStructure.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStructure.Size = new System.Drawing.Size(605, 227);
            this.gridStructure.StandardTab = true;
            this.gridStructure.TabIndex = 7;
            // 
            // colComboStructureFields
            // 
            this.colComboStructureFields.HeaderText = "Structure Fields";
            this.colComboStructureFields.Name = "colComboStructureFields";
            this.colComboStructureFields.Width = 240;
            // 
            // lblStructure
            // 
            this.lblStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStructure.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStructure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStructure.ForeColor = System.Drawing.SystemColors.Window;
            this.lblStructure.Location = new System.Drawing.Point(3, 62);
            this.lblStructure.Name = "lblStructure";
            this.lblStructure.Size = new System.Drawing.Size(605, 18);
            this.lblStructure.TabIndex = 4;
            this.lblStructure.Text = "Structure";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(3, 13);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(25, 13);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "Key";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(616, 344);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSetApids_reportdefinitions";
            reportDataSource2.Value = this.reportdefinitionsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptReportDefinitions.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(616, 344);
            this.reportViewer1.TabIndex = 0;
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
            this.btExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 345);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
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
            // btExport
            // 
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExport.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(100, 22);
            this.btExport.Text = "Export to FSW";
            this.btExport.ToolTipText = "Generates a header file to be included in FSW source code.";
            this.btExport.Visible = false;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // eventreportsBindingSource
            // 
            this.eventreportsBindingSource.DataMember = "event_reports";
            this.eventreportsBindingSource.DataSource = this.dataSetApids;
            // 
            // event_reportsTableAdapter
            // 
            this.event_reportsTableAdapter.ClearBeforeFill = true;
            // 
            // tc_failure_codesBindingSource
            // 
            this.tc_failure_codesBindingSource.DataMember = "tc_failure_codes";
            this.tc_failure_codesBindingSource.DataSource = this.dataSetApids;
            // 
            // tc_failure_codesTableAdapter
            // 
            this.tc_failure_codesTableAdapter.ClearBeforeFill = true;
            // 
            // reportdefinitionsTableAdapter
            // 
            this.reportdefinitionsTableAdapter.ClearBeforeFill = true;
            // 
            // FrmMissionIdsWithStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(624, 370);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmMissionIdsWithStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportIDs";
            this.DockStateChanged += new System.EventHandler(this.FrmReportStructures_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReportStructures_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.reportdefinitionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.event_reportsBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStructure)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventreportsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tc_failure_codesBindingSource)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblStructure;
        private System.Windows.Forms.DataGridView gridStructure;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btNewDataField;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripButton btReport;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private System.Windows.Forms.BindingSource eventreportsBindingSource;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.event_reportsTableAdapter event_reportsTableAdapter;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.tc_failure_codesTableAdapter tc_failure_codesTableAdapter;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.reportdefinitionsTableAdapter reportdefinitionsTableAdapter;
        private System.Windows.Forms.BindingSource event_reportsBindingSource;
        private System.Windows.Forms.BindingSource tc_failure_codesBindingSource;
        private System.Windows.Forms.BindingSource reportdefinitionsBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn colComboStructureFields;
    }
}
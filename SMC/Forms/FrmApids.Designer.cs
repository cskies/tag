namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmApids
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmApids));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.apidsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.btEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.btConfirm = new System.Windows.Forms.ToolStripButton();
            this.btCancel = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btReport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.apidsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.apidsTableAdapter();
            this.txtAppName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridApid = new System.Windows.Forms.DataGridView();
            this.Apid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Application_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vcid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtVcid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.txtApid = new System.Windows.Forms.TextBox();
            this.lblApplication_name = new System.Windows.Forms.Label();
            this.lblApid = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.apidsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridApid)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // apidsBindingSource
            // 
            this.apidsBindingSource.DataMember = "apids";
            this.apidsBindingSource.DataSource = this.dataSetApids;
            // 
            // dataSetApids
            // 
            this.dataSetApids.DataSetName = "DataSetApids";
            this.dataSetApids.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.btConfirm,
            this.btCancel,
            this.btRefresh,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.btReport,
            this.btExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 317);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(599, 25);
            this.toolStrip1.TabIndex = 2;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // apidsTableAdapter
            // 
            this.apidsTableAdapter.ClearBeforeFill = true;
            // 
            // txtAppName
            // 
            this.txtAppName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppName.Location = new System.Drawing.Point(127, 32);
            this.txtAppName.MaxLength = 100;
            this.txtAppName.Name = "txtAppName";
            this.txtAppName.Size = new System.Drawing.Size(456, 20);
            this.txtAppName.TabIndex = 7;
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
            this.tabControl1.Size = new System.Drawing.Size(599, 342);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridApid);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(591, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browsing";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridApid
            // 
            this.gridApid.AllowUserToAddRows = false;
            this.gridApid.AllowUserToDeleteRows = false;
            this.gridApid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridApid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridApid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridApid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Apid,
            this.Application_name,
            this.Vcid});
            this.gridApid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridApid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridApid.Location = new System.Drawing.Point(3, 3);
            this.gridApid.Margin = new System.Windows.Forms.Padding(4);
            this.gridApid.MultiSelect = false;
            this.gridApid.Name = "gridApid";
            this.gridApid.ReadOnly = true;
            this.gridApid.RowHeadersVisible = false;
            this.gridApid.RowTemplate.Height = 24;
            this.gridApid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridApid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridApid.Size = new System.Drawing.Size(585, 308);
            this.gridApid.TabIndex = 1;
            // 
            // Apid
            // 
            this.Apid.FillWeight = 14.83185F;
            this.Apid.HeaderText = "APID";
            this.Apid.Name = "Apid";
            this.Apid.ReadOnly = true;
            this.Apid.Width = 24;
            // 
            // Application_name
            // 
            this.Application_name.FillWeight = 0.3965735F;
            this.Application_name.HeaderText = "Application Name";
            this.Application_name.Name = "Application_name";
            this.Application_name.ReadOnly = true;
            this.Application_name.Width = 5;
            // 
            // Vcid
            // 
            this.Vcid.FillWeight = 284.7716F;
            this.Vcid.HeaderText = "VCID";
            this.Vcid.Name = "Vcid";
            this.Vcid.ReadOnly = true;
            this.Vcid.Width = 456;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtVcid);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtApplicationName);
            this.tabPage2.Controls.Add(this.txtApid);
            this.tabPage2.Controls.Add(this.lblApplication_name);
            this.tabPage2.Controls.Add(this.lblApid);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(591, 314);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtVcid
            // 
            this.txtVcid.Location = new System.Drawing.Point(128, 58);
            this.txtVcid.MaxLength = 3;
            this.txtVcid.Name = "txtVcid";
            this.txtVcid.Size = new System.Drawing.Size(67, 20);
            this.txtVcid.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "VCID:";
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplicationName.Location = new System.Drawing.Point(128, 29);
            this.txtApplicationName.MaxLength = 100;
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.Size = new System.Drawing.Size(325, 20);
            this.txtApplicationName.TabIndex = 13;
            // 
            // txtApid
            // 
            this.txtApid.Location = new System.Drawing.Point(128, 3);
            this.txtApid.MaxLength = 4;
            this.txtApid.Name = "txtApid";
            this.txtApid.Size = new System.Drawing.Size(67, 20);
            this.txtApid.TabIndex = 12;
            // 
            // lblApplication_name
            // 
            this.lblApplication_name.AutoSize = true;
            this.lblApplication_name.Location = new System.Drawing.Point(3, 32);
            this.lblApplication_name.Name = "lblApplication_name";
            this.lblApplication_name.Size = new System.Drawing.Size(93, 13);
            this.lblApplication_name.TabIndex = 11;
            this.lblApplication_name.Text = "Application Name:";
            // 
            // lblApid
            // 
            this.lblApid.AutoSize = true;
            this.lblApid.Location = new System.Drawing.Point(3, 6);
            this.lblApid.Name = "lblApid";
            this.lblApid.Size = new System.Drawing.Size(35, 13);
            this.lblApid.TabIndex = 10;
            this.lblApid.Text = "APID:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(591, 314);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Report";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSetApids_apids";
            reportDataSource2.Value = this.apidsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptApids.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(585, 308);
            this.reportViewer1.TabIndex = 1;
            // 
            // FrmApids
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 342);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmApids";
            this.Text = "Application IDs";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmApid_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.apidsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridApid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripButton btConfirm;
        private System.Windows.Forms.ToolStripButton btCancel;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.ToolStripButton btReport;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private System.Windows.Forms.BindingSource apidsBindingSource;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.apidsTableAdapter apidsTableAdapter;
        private System.Windows.Forms.TextBox txtAppName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridApid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vcid;
        private System.Windows.Forms.TextBox txtVcid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.TextBox txtApid;
        private System.Windows.Forms.Label lblApplication_name;
        private System.Windows.Forms.Label lblApid;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

    }
}
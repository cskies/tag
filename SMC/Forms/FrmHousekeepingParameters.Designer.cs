namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmHousekeepingParameters
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.parametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridHousekeeping = new System.Windows.Forms.DataGridView();
            this.parameterID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkShowAsHex = new System.Windows.Forms.CheckBox();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.lbDataType = new System.Windows.Forms.Label();
            this.txtParameterDescription = new System.Windows.Forms.TextBox();
            this.txtParameterId = new System.Windows.Forms.TextBox();
            this.lbParameterDescription = new System.Windows.Forms.Label();
            this.lbParameterId = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btConfirm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btCancel = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.parametersTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.parametersTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.parametersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHousekeeping)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // parametersBindingSource
            // 
            this.parametersBindingSource.DataMember = "parameters";
            this.parametersBindingSource.DataSource = this.dataSetApids;
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
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 335);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridHousekeeping);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browsing";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // gridHousekeeping
            // 
            this.gridHousekeeping.AllowUserToAddRows = false;
            this.gridHousekeeping.AllowUserToDeleteRows = false;
            this.gridHousekeeping.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridHousekeeping.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridHousekeeping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHousekeeping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameterID,
            this.parameterDescription,
            this.dataType});
            this.gridHousekeeping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHousekeeping.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridHousekeeping.Location = new System.Drawing.Point(3, 3);
            this.gridHousekeeping.MultiSelect = false;
            this.gridHousekeeping.Name = "gridHousekeeping";
            this.gridHousekeeping.ReadOnly = true;
            this.gridHousekeeping.RowHeadersVisible = false;
            this.gridHousekeeping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHousekeeping.Size = new System.Drawing.Size(498, 303);
            this.gridHousekeeping.StandardTab = true;
            this.gridHousekeeping.TabIndex = 2;
            this.gridHousekeeping.DoubleClick += new System.EventHandler(this.gridHousekeeping_DoubleClick);
            // 
            // parameterID
            // 
            this.parameterID.HeaderText = "Parameter ID";
            this.parameterID.Name = "parameterID";
            this.parameterID.ReadOnly = true;
            this.parameterID.Width = 150;
            // 
            // parameterDescription
            // 
            this.parameterDescription.HeaderText = "Parameter Description";
            this.parameterDescription.Name = "parameterDescription";
            this.parameterDescription.ReadOnly = true;
            this.parameterDescription.Width = 200;
            // 
            // dataType
            // 
            this.dataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataType.HeaderText = "Data Type";
            this.dataType.Name = "dataType";
            this.dataType.ReadOnly = true;
            this.dataType.Width = 76;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkShowAsHex);
            this.tabPage2.Controls.Add(this.cmbDataType);
            this.tabPage2.Controls.Add(this.lbDataType);
            this.tabPage2.Controls.Add(this.txtParameterDescription);
            this.tabPage2.Controls.Add(this.txtParameterId);
            this.tabPage2.Controls.Add(this.lbParameterDescription);
            this.tabPage2.Controls.Add(this.lbParameterId);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 309);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editing";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // chkShowAsHex
            // 
            this.chkShowAsHex.AutoSize = true;
            this.chkShowAsHex.Location = new System.Drawing.Point(298, 58);
            this.chkShowAsHex.Name = "chkShowAsHex";
            this.chkShowAsHex.Size = new System.Drawing.Size(131, 17);
            this.chkShowAsHex.TabIndex = 6;
            this.chkShowAsHex.Text = "Show as Hexadecimal";
            this.chkShowAsHex.UseVisualStyleBackColor = true;
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.Items.AddRange(new object[] {
            "boolean (8 bits)",
            "int8",
            "int16",
            "int32",
            "int64"});
            this.cmbDataType.Location = new System.Drawing.Point(127, 56);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(150, 21);
            this.cmbDataType.TabIndex = 5;
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
            // 
            // lbDataType
            // 
            this.lbDataType.AutoSize = true;
            this.lbDataType.Location = new System.Drawing.Point(8, 59);
            this.lbDataType.Name = "lbDataType";
            this.lbDataType.Size = new System.Drawing.Size(60, 13);
            this.lbDataType.TabIndex = 4;
            this.lbDataType.Text = "Data Type:";
            // 
            // txtParameterDescription
            // 
            this.txtParameterDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameterDescription.Location = new System.Drawing.Point(127, 30);
            this.txtParameterDescription.Name = "txtParameterDescription";
            this.txtParameterDescription.Size = new System.Drawing.Size(369, 20);
            this.txtParameterDescription.TabIndex = 3;
            // 
            // txtParameterId
            // 
            this.txtParameterId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameterId.Location = new System.Drawing.Point(127, 3);
            this.txtParameterId.Name = "txtParameterId";
            this.txtParameterId.Size = new System.Drawing.Size(369, 20);
            this.txtParameterId.TabIndex = 2;
            // 
            // lbParameterDescription
            // 
            this.lbParameterDescription.AutoSize = true;
            this.lbParameterDescription.Location = new System.Drawing.Point(8, 33);
            this.lbParameterDescription.Name = "lbParameterDescription";
            this.lbParameterDescription.Size = new System.Drawing.Size(114, 13);
            this.lbParameterDescription.TabIndex = 1;
            this.lbParameterDescription.Text = "Parameter Description:";
            // 
            // lbParameterId
            // 
            this.lbParameterId.AutoSize = true;
            this.lbParameterId.Location = new System.Drawing.Point(8, 5);
            this.lbParameterId.Name = "lbParameterId";
            this.lbParameterId.Size = new System.Drawing.Size(72, 13);
            this.lbParameterId.TabIndex = 0;
            this.lbParameterId.Text = "Parameter ID:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(504, 309);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetApids_parameters";
            reportDataSource1.Value = this.parametersBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptParameters.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(504, 309);
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
            this.toolStripSeparator1,
            this.btEdit,
            this.toolStripSeparator2,
            this.btDelete,
            this.toolStripSeparator3,
            this.btConfirm,
            this.toolStripSeparator6,
            this.btCancel,
            this.btRefresh,
            this.toolStripSeparator5,
            this.btReport,
            this.toolStripSeparator4});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 309);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(512, 26);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btNew
            // 
            this.btNew.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.add_page_blue;
            this.btNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(51, 23);
            this.btNew.Text = "New";
            this.btNew.ToolTipText = "New Record (Insert)";
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // btEdit
            // 
            this.btEdit.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.edit_page_yellow;
            this.btEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(47, 23);
            this.btEdit.Text = "Edit";
            this.btEdit.ToolTipText = "Edit Selected Record (Enter)";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // btDelete
            // 
            this.btDelete.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.delete_page_red;
            this.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(60, 23);
            this.btDelete.Text = "Delete";
            this.btDelete.ToolTipText = "Delete Selected Record (Del)";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // btConfirm
            // 
            this.btConfirm.Enabled = false;
            this.btConfirm.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.confirmar;
            this.btConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(71, 23);
            this.btConfirm.Text = "Confirm";
            this.btConfirm.ToolTipText = "Confirm Insertion/Edition (Enter)";
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
            // 
            // btCancel
            // 
            this.btCancel.Enabled = false;
            this.btCancel.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.left_red;
            this.btCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(63, 23);
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
            this.btRefresh.Size = new System.Drawing.Size(66, 23);
            this.btRefresh.Text = "Refresh";
            this.btRefresh.ToolTipText = "Refresh Grid (F5)";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
            // 
            // btReport
            // 
            this.btReport.CheckOnClick = true;
            this.btReport.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.relatorio;
            this.btReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btReport.Name = "btReport";
            this.btReport.Size = new System.Drawing.Size(62, 23);
            this.btReport.Text = "Report";
            this.btReport.ToolTipText = "Allows to view, print and export reports in different formats.";
            this.btReport.Click += new System.EventHandler(this.btReport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // parametersTableAdapter
            // 
            this.parametersTableAdapter.ClearBeforeFill = true;
            // 
            // FrmHousekeepingParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(512, 335);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmHousekeepingParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Housekeeping and Diagnostic Parameters";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHousekeeping_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.parametersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHousekeeping)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btEdit;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btConfirm;
        private System.Windows.Forms.ToolStripButton btCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.DataGridView gridHousekeeping;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterID;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataType;
        private System.Windows.Forms.Label lbParameterId;
        private System.Windows.Forms.TextBox txtParameterDescription;
        private System.Windows.Forms.TextBox txtParameterId;
        private System.Windows.Forms.Label lbParameterDescription;
        private System.Windows.Forms.Label lbDataType;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.CheckBox chkShowAsHex;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private System.Windows.Forms.BindingSource parametersBindingSource;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.parametersTableAdapter parametersTableAdapter;

    }
}
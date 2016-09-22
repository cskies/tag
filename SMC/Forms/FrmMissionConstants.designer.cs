namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmMissionConstants
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMissionConstants));
            this.mission_constantsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridMissonConstants = new System.Windows.Forms.DataGridView();
            this.colMissionConstant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConstantDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefinedIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConstantValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsFswConstant = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkUsedByFlightSW = new System.Windows.Forms.CheckBox();
            this.txtConstantValue = new System.Windows.Forms.TextBox();
            this.lbConstantValue = new System.Windows.Forms.Label();
            this.txtDefinedIn = new System.Windows.Forms.TextBox();
            this.lbDefinedIn = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtMissionConstant = new System.Windows.Forms.TextBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbMissionConstant = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btConfirm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btCancel = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btReport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.missionconstantsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mission_constantsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.mission_constantsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.mission_constantsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMissonConstants)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.missionconstantsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mission_constantsBindingSource
            // 
            this.mission_constantsBindingSource.DataMember = "mission_constants";
            this.mission_constantsBindingSource.DataSource = this.dataSetApids;
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
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(596, 271);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridMissonConstants);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(588, 245);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "browsing";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridMissonConstants
            // 
            this.gridMissonConstants.AllowUserToAddRows = false;
            this.gridMissonConstants.AllowUserToDeleteRows = false;
            this.gridMissonConstants.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridMissonConstants.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridMissonConstants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMissonConstants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMissionConstant,
            this.colConstantDescription,
            this.colDefinedIn,
            this.colConstantValue,
            this.colIsFswConstant});
            this.gridMissonConstants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMissonConstants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridMissonConstants.Location = new System.Drawing.Point(4, 4);
            this.gridMissonConstants.Margin = new System.Windows.Forms.Padding(4);
            this.gridMissonConstants.MultiSelect = false;
            this.gridMissonConstants.Name = "gridMissonConstants";
            this.gridMissonConstants.ReadOnly = true;
            this.gridMissonConstants.RowHeadersVisible = false;
            this.gridMissonConstants.RowTemplate.Height = 24;
            this.gridMissonConstants.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridMissonConstants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMissonConstants.Size = new System.Drawing.Size(580, 237);
            this.gridMissonConstants.TabIndex = 0;
            this.gridMissonConstants.DoubleClick += new System.EventHandler(this.gridMissonConstants_DoubleClick);
            // 
            // colMissionConstant
            // 
            this.colMissionConstant.HeaderText = "Mission Constant";
            this.colMissionConstant.Name = "colMissionConstant";
            this.colMissionConstant.ReadOnly = true;
            // 
            // colConstantDescription
            // 
            this.colConstantDescription.HeaderText = "Constant Description";
            this.colConstantDescription.Name = "colConstantDescription";
            this.colConstantDescription.ReadOnly = true;
            // 
            // colDefinedIn
            // 
            this.colDefinedIn.HeaderText = "Defined In";
            this.colDefinedIn.Name = "colDefinedIn";
            this.colDefinedIn.ReadOnly = true;
            // 
            // colConstantValue
            // 
            this.colConstantValue.HeaderText = "Constant Value";
            this.colConstantValue.Name = "colConstantValue";
            this.colConstantValue.ReadOnly = true;
            // 
            // colIsFswConstant
            // 
            this.colIsFswConstant.HeaderText = "Is Flight Sw Constant";
            this.colIsFswConstant.Name = "colIsFswConstant";
            this.colIsFswConstant.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkUsedByFlightSW);
            this.tabPage2.Controls.Add(this.txtConstantValue);
            this.tabPage2.Controls.Add(this.lbConstantValue);
            this.tabPage2.Controls.Add(this.txtDefinedIn);
            this.tabPage2.Controls.Add(this.lbDefinedIn);
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.txtMissionConstant);
            this.tabPage2.Controls.Add(this.lbDescription);
            this.tabPage2.Controls.Add(this.lbMissionConstant);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(588, 245);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "editing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkUsedByFlightSW
            // 
            this.chkUsedByFlightSW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUsedByFlightSW.AutoSize = true;
            this.chkUsedByFlightSW.Location = new System.Drawing.Point(97, 218);
            this.chkUsedByFlightSW.Margin = new System.Windows.Forms.Padding(4);
            this.chkUsedByFlightSW.Name = "chkUsedByFlightSW";
            this.chkUsedByFlightSW.Size = new System.Drawing.Size(207, 17);
            this.chkUsedByFlightSW.TabIndex = 8;
            this.chkUsedByFlightSW.Text = "This constant is used by the Flight SW";
            this.chkUsedByFlightSW.UseVisualStyleBackColor = true;
            // 
            // txtConstantValue
            // 
            this.txtConstantValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConstantValue.Location = new System.Drawing.Point(97, 193);
            this.txtConstantValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtConstantValue.MaxLength = 50;
            this.txtConstantValue.Name = "txtConstantValue";
            this.txtConstantValue.Size = new System.Drawing.Size(482, 19);
            this.txtConstantValue.TabIndex = 7;
            // 
            // lbConstantValue
            // 
            this.lbConstantValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbConstantValue.AutoSize = true;
            this.lbConstantValue.Location = new System.Drawing.Point(4, 196);
            this.lbConstantValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbConstantValue.Name = "lbConstantValue";
            this.lbConstantValue.Size = new System.Drawing.Size(82, 13);
            this.lbConstantValue.TabIndex = 6;
            this.lbConstantValue.Text = "Constant Value:";
            // 
            // txtDefinedIn
            // 
            this.txtDefinedIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefinedIn.Location = new System.Drawing.Point(97, 166);
            this.txtDefinedIn.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefinedIn.MaxLength = 100;
            this.txtDefinedIn.Name = "txtDefinedIn";
            this.txtDefinedIn.Size = new System.Drawing.Size(482, 19);
            this.txtDefinedIn.TabIndex = 5;
            // 
            // lbDefinedIn
            // 
            this.lbDefinedIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDefinedIn.AutoSize = true;
            this.lbDefinedIn.Location = new System.Drawing.Point(4, 169);
            this.lbDefinedIn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDefinedIn.Name = "lbDefinedIn";
            this.lbDefinedIn.Size = new System.Drawing.Size(59, 13);
            this.lbDefinedIn.TabIndex = 4;
            this.lbDefinedIn.Text = "Defined In:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(97, 37);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.MaxLength = 1000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(482, 121);
            this.txtDescription.TabIndex = 3;
            // 
            // txtMissionConstant
            // 
            this.txtMissionConstant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMissionConstant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMissionConstant.Location = new System.Drawing.Point(97, 10);
            this.txtMissionConstant.Margin = new System.Windows.Forms.Padding(4);
            this.txtMissionConstant.MaxLength = 100;
            this.txtMissionConstant.Name = "txtMissionConstant";
            this.txtMissionConstant.Size = new System.Drawing.Size(482, 19);
            this.txtMissionConstant.TabIndex = 2;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(4, 37);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(63, 13);
            this.lbDescription.TabIndex = 1;
            this.lbDescription.Text = "Description:";
            // 
            // lbMissionConstant
            // 
            this.lbMissionConstant.AutoSize = true;
            this.lbMissionConstant.Location = new System.Drawing.Point(4, 13);
            this.lbMissionConstant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMissionConstant.Name = "lbMissionConstant";
            this.lbMissionConstant.Size = new System.Drawing.Size(90, 13);
            this.lbMissionConstant.TabIndex = 0;
            this.lbMissionConstant.Text = "Mission Constant:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(588, 245);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSetApids_mission_constants";
            reportDataSource2.Value = this.mission_constantsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptMissionConstants.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(588, 245);
            this.reportViewer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNew,
            this.toolStripSeparator6,
            this.btEdit,
            this.toolStripSeparator1,
            this.btDelete,
            this.toolStripSeparator4,
            this.btConfirm,
            this.toolStripSeparator5,
            this.btCancel,
            this.btRefresh,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.btReport,
            this.btExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 246);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(596, 25);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // missionconstantsBindingSource
            // 
            this.missionconstantsBindingSource.DataMember = "mission_constants";
            this.missionconstantsBindingSource.DataSource = this.dataSetApids;
            // 
            // mission_constantsTableAdapter
            // 
            this.mission_constantsTableAdapter.ClearBeforeFill = true;
            // 
            // FrmMissionConstants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(596, 271);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMissionConstants";
            this.Text = "Mission Constants";
            this.Load += new System.EventHandler(this.FrmMissionConstants_Load);
            this.DockStateChanged += new System.EventHandler(this.FrmMissionConstants_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMissionConstants_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.mission_constantsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMissonConstants)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.missionconstantsBindingSource)).EndInit();
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
        private System.Windows.Forms.DataGridView gridMissonConstants;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripButton btConfirm;
        private System.Windows.Forms.ToolStripButton btCancel;
        private System.Windows.Forms.TextBox txtConstantValue;
        private System.Windows.Forms.Label lbConstantValue;
        private System.Windows.Forms.TextBox txtDefinedIn;
        private System.Windows.Forms.Label lbDefinedIn;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtMissionConstant;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label lbMissionConstant;
        private System.Windows.Forms.CheckBox chkUsedByFlightSW;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMissionConstant;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConstantDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefinedIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConstantValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsFswConstant;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private System.Windows.Forms.BindingSource missionconstantsBindingSource;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.mission_constantsTableAdapter mission_constantsTableAdapter;
        private System.Windows.Forms.BindingSource mission_constantsBindingSource;
    }
}
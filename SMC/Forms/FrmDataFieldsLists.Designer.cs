namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmDataFieldsLists
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
            this.datafieldslistsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetApids = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.lblTitleListOfValues = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.gridList = new System.Windows.Forms.DataGridView();
            this.coValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.data_fields_listsTableAdapter = new Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.data_fields_listsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.datafieldslistsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // datafieldslistsBindingSource
            // 
            this.datafieldslistsBindingSource.DataMember = "data_fields_lists";
            this.datafieldslistsBindingSource.DataSource = this.dataSetApids;
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
            this.tabControl1.ItemSize = new System.Drawing.Size(58, 20);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 380);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridDatabase);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(748, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browsing";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.gridDatabase.RowTemplate.ReadOnly = true;
            this.gridDatabase.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDatabase.Size = new System.Drawing.Size(742, 346);
            this.gridDatabase.StandardTab = true;
            this.gridDatabase.TabIndex = 0;
            this.gridDatabase.DoubleClick += new System.EventHandler(this.gridDatabase_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btDown);
            this.tabPage2.Controls.Add(this.btUp);
            this.tabPage2.Controls.Add(this.btRemove);
            this.tabPage2.Controls.Add(this.btAdd);
            this.tabPage2.Controls.Add(this.lblTitleListOfValues);
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.txtId);
            this.tabPage2.Controls.Add(this.lblDescription);
            this.tabPage2.Controls.Add(this.lblKey);
            this.tabPage2.Controls.Add(this.gridList);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(748, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(189, 90);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(56, 23);
            this.btDown.TabIndex = 5;
            this.btDown.Text = "Down";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(127, 90);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(56, 23);
            this.btUp.TabIndex = 4;
            this.btUp.Text = "Up";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click_1);
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(65, 90);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(56, 23);
            this.btRemove.TabIndex = 3;
            this.btRemove.Text = "Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(3, 90);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(56, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // lblTitleListOfValues
            // 
            this.lblTitleListOfValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleListOfValues.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitleListOfValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleListOfValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleListOfValues.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitleListOfValues.Location = new System.Drawing.Point(0, 68);
            this.lblTitleListOfValues.Name = "lblTitleListOfValues";
            this.lblTitleListOfValues.Size = new System.Drawing.Size(748, 19);
            this.lblTitleListOfValues.TabIndex = 13;
            this.lblTitleListOfValues.Text = "List of Values";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(129, 35);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(611, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(129, 9);
            this.txtId.MaxLength = 3;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(67, 20);
            this.txtId.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(4, 38);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "List Description";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(4, 12);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(37, 13);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "List ID";
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
            this.gridList.Location = new System.Drawing.Point(0, 118);
            this.gridList.MultiSelect = false;
            this.gridList.Name = "gridList";
            this.gridList.RowHeadersVisible = false;
            this.gridList.RowHeadersWidth = 25;
            this.gridList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridList.Size = new System.Drawing.Size(748, 234);
            this.gridList.StandardTab = true;
            this.gridList.TabIndex = 6;
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(748, 352);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetApids_data_fields_lists";
            reportDataSource1.Value = this.datafieldslistsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptDataFieldsLists.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(748, 352);
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
            this.toolStripSeparator6});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 355);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(756, 25);
            this.toolStrip1.TabIndex = 3;
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
            // data_fields_listsTableAdapter
            // 
            this.data_fields_listsTableAdapter.ClearBeforeFill = true;
            // 
            // FrmDataFieldsLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(756, 380);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmDataFieldsLists";
            this.Text = "Data Fields Lists";
            this.DockStateChanged += new System.EventHandler(this.FrmDataFieldsLists_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDataFieldsLists_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.datafieldslistsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetApids)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView gridDatabase;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblKey;
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
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.DataGridView gridList;
        private System.Windows.Forms.DataGridViewTextBoxColumn coValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultValue;
        private System.Windows.Forms.Label lblTitleListOfValues;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.TabPage tabPage3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStripButton btReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApids dataSetApids;
        private System.Windows.Forms.BindingSource datafieldslistsBindingSource;
        private Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters.data_fields_listsTableAdapter data_fields_listsTableAdapter;
    }
}
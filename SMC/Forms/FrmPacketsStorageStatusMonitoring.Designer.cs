namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmPacketsStorageStatusMonitoring
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnResquestStorage = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridStorageSelection = new System.Windows.Forms.DataGridView();
            this.storeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceSubtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.gridTelemetrySource = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.gridHousekeeping = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.gridEventsPackets = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gridPacketStores = new System.Windows.Forms.DataGridView();
            this.chkShowTimeTags = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStorageSelection)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTelemetrySource)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHousekeeping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEventsPackets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPacketStores)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnResquestStorage
            // 
            this.btnResquestStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResquestStorage.Enabled = false;
            this.btnResquestStorage.Location = new System.Drawing.Point(517, 589);
            this.btnResquestStorage.Name = "btnResquestStorage";
            this.btnResquestStorage.Size = new System.Drawing.Size(136, 23);
            this.btnResquestStorage.TabIndex = 50;
            this.btnResquestStorage.Text = "Request Storage Status";
            this.btnResquestStorage.UseVisualStyleBackColor = true;
            this.btnResquestStorage.Click += new System.EventHandler(this.btnResquestStorage_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gridStorageSelection);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Size = new System.Drawing.Size(657, 358);
            this.splitContainer2.SplitterDistance = 151;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridStorageSelection
            // 
            this.gridStorageSelection.AllowUserToAddRows = false;
            this.gridStorageSelection.AllowUserToDeleteRows = false;
            this.gridStorageSelection.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridStorageSelection.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStorageSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStorageSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStorageSelection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.storeId,
            this.serviceType,
            this.serviceSubtype});
            this.gridStorageSelection.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridStorageSelection.Location = new System.Drawing.Point(3, 22);
            this.gridStorageSelection.MultiSelect = false;
            this.gridStorageSelection.Name = "gridStorageSelection";
            this.gridStorageSelection.ReadOnly = true;
            this.gridStorageSelection.RowHeadersVisible = false;
            this.gridStorageSelection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStorageSelection.Size = new System.Drawing.Size(650, 126);
            this.gridStorageSelection.TabIndex = 41;
            // 
            // storeId
            // 
            this.storeId.HeaderText = "Packet Store";
            this.storeId.Name = "storeId";
            this.storeId.ReadOnly = true;
            this.storeId.Width = 150;
            // 
            // serviceType
            // 
            this.serviceType.HeaderText = "Service Type";
            this.serviceType.Name = "serviceType";
            this.serviceType.ReadOnly = true;
            this.serviceType.Width = 245;
            // 
            // serviceSubtype
            // 
            this.serviceSubtype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.serviceSubtype.HeaderText = "Service Subtype";
            this.serviceSubtype.Name = "serviceSubtype";
            this.serviceSubtype.ReadOnly = true;
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
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(650, 15);
            this.label1.TabIndex = 40;
            this.label1.Text = "Storage Selection Definition (packets not listed here are sent to the Normal Repo" +
                "rts packet store)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 18);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label4);
            this.splitContainer3.Panel1.Controls.Add(this.gridTelemetrySource);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(650, 182);
            this.splitContainer3.SplitterDistance = 268;
            this.splitContainer3.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Telemetry Source Packets";
            // 
            // gridTelemetrySource
            // 
            this.gridTelemetrySource.AllowUserToAddRows = false;
            this.gridTelemetrySource.AllowUserToDeleteRows = false;
            this.gridTelemetrySource.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridTelemetrySource.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTelemetrySource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTelemetrySource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTelemetrySource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.gridTelemetrySource.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTelemetrySource.Location = new System.Drawing.Point(3, 18);
            this.gridTelemetrySource.MultiSelect = false;
            this.gridTelemetrySource.Name = "gridTelemetrySource";
            this.gridTelemetrySource.ReadOnly = true;
            this.gridTelemetrySource.RowHeadersVisible = false;
            this.gridTelemetrySource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTelemetrySource.Size = new System.Drawing.Size(262, 161);
            this.gridTelemetrySource.TabIndex = 47;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Service Type";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Service Subtype";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label5);
            this.splitContainer4.Panel1.Controls.Add(this.gridHousekeeping);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label6);
            this.splitContainer4.Panel2.Controls.Add(this.gridEventsPackets);
            this.splitContainer4.Size = new System.Drawing.Size(378, 182);
            this.splitContainer4.SplitterDistance = 187;
            this.splitContainer4.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Housekeeping";
            // 
            // gridHousekeeping
            // 
            this.gridHousekeeping.AllowUserToAddRows = false;
            this.gridHousekeeping.AllowUserToDeleteRows = false;
            this.gridHousekeeping.AllowUserToOrderColumns = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender;
            this.gridHousekeeping.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridHousekeeping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridHousekeeping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHousekeeping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3});
            this.gridHousekeeping.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridHousekeeping.Location = new System.Drawing.Point(3, 18);
            this.gridHousekeeping.MultiSelect = false;
            this.gridHousekeeping.Name = "gridHousekeeping";
            this.gridHousekeeping.ReadOnly = true;
            this.gridHousekeeping.RowHeadersVisible = false;
            this.gridHousekeeping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHousekeeping.Size = new System.Drawing.Size(181, 161);
            this.gridHousekeeping.TabIndex = 48;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Structure";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Events Packets";
            // 
            // gridEventsPackets
            // 
            this.gridEventsPackets.AllowUserToAddRows = false;
            this.gridEventsPackets.AllowUserToDeleteRows = false;
            this.gridEventsPackets.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Lavender;
            this.gridEventsPackets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridEventsPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEventsPackets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEventsPackets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4});
            this.gridEventsPackets.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridEventsPackets.Location = new System.Drawing.Point(3, 18);
            this.gridEventsPackets.MultiSelect = false;
            this.gridEventsPackets.Name = "gridEventsPackets";
            this.gridEventsPackets.ReadOnly = true;
            this.gridEventsPackets.RowHeadersVisible = false;
            this.gridEventsPackets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEventsPackets.Size = new System.Drawing.Size(181, 161);
            this.gridEventsPackets.TabIndex = 49;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Report";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(650, 15);
            this.label3.TabIndex = 41;
            this.label3.Text = "Packets with Forwarding Disabled";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(475, 15);
            this.label2.TabIndex = 44;
            this.label2.Text = "Packet Stores Catalogues";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridPacketStores
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Lavender;
            this.gridPacketStores.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridPacketStores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPacketStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPacketStores.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridPacketStores.Location = new System.Drawing.Point(3, 27);
            this.gridPacketStores.MultiSelect = false;
            this.gridPacketStores.Name = "gridPacketStores";
            this.gridPacketStores.ReadOnly = true;
            this.gridPacketStores.RowHeadersVisible = false;
            this.gridPacketStores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPacketStores.Size = new System.Drawing.Size(650, 192);
            this.gridPacketStores.TabIndex = 45;
            // 
            // chkShowTimeTags
            // 
            this.chkShowTimeTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowTimeTags.AutoSize = true;
            this.chkShowTimeTags.Checked = true;
            this.chkShowTimeTags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTimeTags.Location = new System.Drawing.Point(501, 4);
            this.chkShowTimeTags.Name = "chkShowTimeTags";
            this.chkShowTimeTags.Size = new System.Drawing.Size(152, 17);
            this.chkShowTimeTags.TabIndex = 46;
            this.chkShowTimeTags.Text = "Show time-tag as Calendar";
            this.chkShowTimeTags.UseVisualStyleBackColor = true;
            this.chkShowTimeTags.CheckedChanged += new System.EventHandler(this.chkShowTimeTags_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkShowTimeTags);
            this.splitContainer1.Panel1.Controls.Add(this.gridPacketStores);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(657, 583);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            // 
            // FrmPacketsStorageStatusMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(657, 624);
            this.Controls.Add(this.btnResquestStorage);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPacketsStorageStatusMonitoring";
            this.Text = "Packets Storage Status Monitoring";
            this.Load += new System.EventHandler(this.frmPacketsStorageStatusMonitoring_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStorageSelection)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTelemetrySource)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHousekeeping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEventsPackets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPacketStores)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnResquestStorage;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView gridStorageSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridPacketStores;
        private System.Windows.Forms.CheckBox chkShowTimeTags;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridTelemetrySource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView gridHousekeeping;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView gridEventsPackets;
        private System.Windows.Forms.DataGridViewTextBoxColumn storeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceSubtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;



    }
}
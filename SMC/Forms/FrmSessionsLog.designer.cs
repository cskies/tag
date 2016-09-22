namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSessionsLog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkTimeTagDate = new System.Windows.Forms.CheckBox();
            this.numSeconds = new System.Windows.Forms.NumericUpDown();
            this.chkRealTime = new System.Windows.Forms.CheckBox();
            this.chkShowValid = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSessions = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPacketsToShow = new System.Windows.Forms.ComboBox();
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridSessionLog = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridAppData = new System.Windows.Forms.DataGridView();
            this.lblAppData = new System.Windows.Forms.Label();
            this.txtRawPacket = new System.Windows.Forms.TextBox();
            this.lblTitleStructure = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSessionLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAppData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkTimeTagDate);
            this.panel1.Controls.Add(this.numSeconds);
            this.panel1.Controls.Add(this.chkRealTime);
            this.panel1.Controls.Add(this.chkShowValid);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbSessions);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbPacketsToShow);
            this.panel1.Controls.Add(this.cmbConnectionType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 76);
            this.panel1.TabIndex = 0;
            // 
            // chkTimeTagDate
            // 
            this.chkTimeTagDate.AutoSize = true;
            this.chkTimeTagDate.Checked = true;
            this.chkTimeTagDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeTagDate.Location = new System.Drawing.Point(356, 51);
            this.chkTimeTagDate.Name = "chkTimeTagDate";
            this.chkTimeTagDate.Size = new System.Drawing.Size(131, 17);
            this.chkTimeTagDate.TabIndex = 4;
            this.chkTimeTagDate.Text = "Show time-tag as date";
            this.chkTimeTagDate.UseVisualStyleBackColor = true;
            this.chkTimeTagDate.CheckedChanged += new System.EventHandler(this.chkTimeTagDate_CheckedChanged);
            // 
            // numSeconds
            // 
            this.numSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numSeconds.Enabled = false;
            this.numSeconds.Location = new System.Drawing.Point(686, 50);
            this.numSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSeconds.Name = "numSeconds";
            this.numSeconds.Size = new System.Drawing.Size(42, 20);
            this.numSeconds.TabIndex = 6;
            this.numSeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSeconds.ValueChanged += new System.EventHandler(this.numSeconds_ValueChanged);
            // 
            // chkRealTime
            // 
            this.chkRealTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRealTime.AutoSize = true;
            this.chkRealTime.Location = new System.Drawing.Point(496, 51);
            this.chkRealTime.Name = "chkRealTime";
            this.chkRealTime.Size = new System.Drawing.Size(188, 17);
            this.chkRealTime.TabIndex = 5;
            this.chkRealTime.Text = "Real-time monitoring, refresh every";
            this.chkRealTime.UseVisualStyleBackColor = true;
            this.chkRealTime.CheckedChanged += new System.EventHandler(this.chkRealTime_CheckedChanged);
            // 
            // chkShowValid
            // 
            this.chkShowValid.AutoSize = true;
            this.chkShowValid.Location = new System.Drawing.Point(209, 51);
            this.chkShowValid.Name = "chkShowValid";
            this.chkShowValid.Size = new System.Drawing.Size(141, 17);
            this.chkShowValid.TabIndex = 3;
            this.chkShowValid.Text = "Show only valid packets";
            this.chkShowValid.UseVisualStyleBackColor = true;
            this.chkShowValid.CheckedChanged += new System.EventHandler(this.chkShowValid_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Packets to show:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(0, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(784, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = " Log View Options";
            // 
            // cmbSessions
            // 
            this.cmbSessions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSessions.FormattingEnabled = true;
            this.cmbSessions.Location = new System.Drawing.Point(259, 24);
            this.cmbSessions.Name = "cmbSessions";
            this.cmbSessions.Size = new System.Drawing.Size(522, 21);
            this.cmbSessions.TabIndex = 1;
            this.cmbSessions.SelectedIndexChanged += new System.EventHandler(this.cmbSessions_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(734, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "seconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Session:";
            // 
            // cmbPacketsToShow
            // 
            this.cmbPacketsToShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacketsToShow.FormattingEnabled = true;
            this.cmbPacketsToShow.Items.AddRange(new object[] {
            "[All]",
            "Requests only",
            "Reports only"});
            this.cmbPacketsToShow.Location = new System.Drawing.Point(100, 49);
            this.cmbPacketsToShow.Name = "cmbPacketsToShow";
            this.cmbPacketsToShow.Size = new System.Drawing.Size(100, 21);
            this.cmbPacketsToShow.TabIndex = 2;
            this.cmbPacketsToShow.SelectedIndexChanged += new System.EventHandler(this.cmbPacketsToShow_SelectedIndexChanged);
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionType.FormattingEnabled = true;
            this.cmbConnectionType.Items.AddRange(new object[] {
            "[All]",
            "Ethernet",
            "Serial",
            "Named Pipe",
            "File"});
            this.cmbConnectionType.Location = new System.Drawing.Point(100, 24);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(100, 21);
            this.cmbConnectionType.TabIndex = 0;
            this.cmbConnectionType.SelectedIndexChanged += new System.EventHandler(this.cmbConnectionType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Connection type:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 76);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridSessionLog);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 450);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // gridSessionLog
            // 
            this.gridSessionLog.AllowUserToAddRows = false;
            this.gridSessionLog.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridSessionLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSessionLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSessionLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSessionLog.Location = new System.Drawing.Point(0, 16);
            this.gridSessionLog.Name = "gridSessionLog";
            this.gridSessionLog.ReadOnly = true;
            this.gridSessionLog.RowHeadersVisible = false;
            this.gridSessionLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSessionLog.Size = new System.Drawing.Size(784, 234);
            this.gridSessionLog.TabIndex = 0;
            this.gridSessionLog.CurrentCellChanged += new System.EventHandler(this.gridSessionLog_CurrentCellChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(784, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = " Session Log [press F5 to refresh]";
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
            this.splitContainer2.Panel1.Controls.Add(this.gridAppData);
            this.splitContainer2.Panel1.Controls.Add(this.lblAppData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtRawPacket);
            this.splitContainer2.Panel2.Controls.Add(this.lblTitleStructure);
            this.splitContainer2.Size = new System.Drawing.Size(784, 196);
            this.splitContainer2.SplitterDistance = 106;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridAppData
            // 
            this.gridAppData.AllowUserToAddRows = false;
            this.gridAppData.AllowUserToDeleteRows = false;
            this.gridAppData.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridAppData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridAppData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridAppData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridAppData.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.gridAppData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAppData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAppData.Location = new System.Drawing.Point(0, 16);
            this.gridAppData.Name = "gridAppData";
            this.gridAppData.ReadOnly = true;
            this.gridAppData.RowHeadersVisible = false;
            this.gridAppData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAppData.Size = new System.Drawing.Size(784, 90);
            this.gridAppData.TabIndex = 0;
            this.gridAppData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAppData_CellContentClick);
            // 
            // lblAppData
            // 
            this.lblAppData.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblAppData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAppData.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAppData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppData.ForeColor = System.Drawing.SystemColors.Window;
            this.lblAppData.Location = new System.Drawing.Point(0, 0);
            this.lblAppData.Name = "lblAppData";
            this.lblAppData.Size = new System.Drawing.Size(784, 16);
            this.lblAppData.TabIndex = 17;
            this.lblAppData.Text = " Application Data";
            // 
            // txtRawPacket
            // 
            this.txtRawPacket.BackColor = System.Drawing.SystemColors.Window;
            this.txtRawPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRawPacket.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawPacket.Location = new System.Drawing.Point(0, 16);
            this.txtRawPacket.Multiline = true;
            this.txtRawPacket.Name = "txtRawPacket";
            this.txtRawPacket.ReadOnly = true;
            this.txtRawPacket.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRawPacket.Size = new System.Drawing.Size(784, 70);
            this.txtRawPacket.TabIndex = 0;
            // 
            // lblTitleStructure
            // 
            this.lblTitleStructure.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitleStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleStructure.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleStructure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitleStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleStructure.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitleStructure.Location = new System.Drawing.Point(0, 0);
            this.lblTitleStructure.Name = "lblTitleStructure";
            this.lblTitleStructure.Size = new System.Drawing.Size(784, 16);
            this.lblTitleStructure.TabIndex = 16;
            this.lblTitleStructure.Text = " Selected Packet: Raw";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FrmSessionsLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 526);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmSessionsLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Sessions Log";
            this.DockStateChanged += new System.EventHandler(this.FrmSessionsLog_DockStateChanged);
            this.Activated += new System.EventHandler(this.FrmSessionsLog_Activated);
            this.Load += new System.EventHandler(this.FrmSessionsLog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSessionsLog_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSessionLog)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAppData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblTitleStructure;
        private System.Windows.Forms.TextBox txtRawPacket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAppData;
        private System.Windows.Forms.ComboBox cmbSessions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbConnectionType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gridAppData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkShowValid;
        private System.Windows.Forms.ComboBox cmbPacketsToShow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView gridSessionLog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox chkTimeTagDate;
        public System.Windows.Forms.NumericUpDown numSeconds;
        public System.Windows.Forms.CheckBox chkRealTime;
    }
}
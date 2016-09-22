namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmEventsDetectionList
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
            this.gridEventDetection = new System.Windows.Forms.DataGridView();
            this.Rid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfOccurrences = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_Seconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_Miliseconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SequenceCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceSubtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btRequestEvents = new System.Windows.Forms.Button();
            this.lblTimeTag = new System.Windows.Forms.Label();
            this.lbEventsDetection = new System.Windows.Forms.Label();
            this.chkTimeTagDate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridEventDetection)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEventDetection
            // 
            this.gridEventDetection.AllowUserToAddRows = false;
            this.gridEventDetection.AllowUserToDeleteRows = false;
            this.gridEventDetection.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridEventDetection.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridEventDetection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEventDetection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEventDetection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rid,
            this.ReportStatus,
            this.ActionStatus,
            this.NumberOfOccurrences,
            this.Time_Seconds,
            this.Time_Miliseconds,
            this.SequenceCount,
            this.ServiceType,
            this.ServiceSubtype});
            this.gridEventDetection.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridEventDetection.Location = new System.Drawing.Point(8, 25);
            this.gridEventDetection.MultiSelect = false;
            this.gridEventDetection.Name = "gridEventDetection";
            this.gridEventDetection.ReadOnly = true;
            this.gridEventDetection.RowHeadersVisible = false;
            this.gridEventDetection.RowTemplate.Height = 24;
            this.gridEventDetection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEventDetection.Size = new System.Drawing.Size(796, 395);
            this.gridEventDetection.TabIndex = 0;
            // 
            // Rid
            // 
            this.Rid.HeaderText = "RID";
            this.Rid.Name = "Rid";
            this.Rid.ReadOnly = true;
            this.Rid.Width = 300;
            // 
            // ReportStatus
            // 
            this.ReportStatus.HeaderText = "Report Status";
            this.ReportStatus.Name = "ReportStatus";
            this.ReportStatus.ReadOnly = true;
            this.ReportStatus.Width = 150;
            // 
            // ActionStatus
            // 
            this.ActionStatus.HeaderText = "Action Status";
            this.ActionStatus.Name = "ActionStatus";
            this.ActionStatus.ReadOnly = true;
            this.ActionStatus.Width = 150;
            // 
            // NumberOfOccurrences
            // 
            this.NumberOfOccurrences.HeaderText = "Number of Occurrences";
            this.NumberOfOccurrences.Name = "NumberOfOccurrences";
            this.NumberOfOccurrences.ReadOnly = true;
            this.NumberOfOccurrences.Width = 150;
            // 
            // Time_Seconds
            // 
            this.Time_Seconds.HeaderText = "Last Occurrence: Seconds";
            this.Time_Seconds.MinimumWidth = 8;
            this.Time_Seconds.Name = "Time_Seconds";
            this.Time_Seconds.ReadOnly = true;
            this.Time_Seconds.Width = 170;
            // 
            // Time_Miliseconds
            // 
            this.Time_Miliseconds.HeaderText = "Last Occurrence: Miliseconds";
            this.Time_Miliseconds.MinimumWidth = 8;
            this.Time_Miliseconds.Name = "Time_Miliseconds";
            this.Time_Miliseconds.ReadOnly = true;
            this.Time_Miliseconds.Width = 170;
            // 
            // SequenceCount
            // 
            this.SequenceCount.HeaderText = "Sequence Count (16 bits)";
            this.SequenceCount.Name = "SequenceCount";
            this.SequenceCount.ReadOnly = true;
            this.SequenceCount.Width = 150;
            // 
            // ServiceType
            // 
            this.ServiceType.HeaderText = "Service Type";
            this.ServiceType.Name = "ServiceType";
            this.ServiceType.ReadOnly = true;
            this.ServiceType.Width = 150;
            // 
            // ServiceSubtype
            // 
            this.ServiceSubtype.HeaderText = "Service Subtype";
            this.ServiceSubtype.Name = "ServiceSubtype";
            this.ServiceSubtype.ReadOnly = true;
            this.ServiceSubtype.Width = 150;
            // 
            // btRequestEvents
            // 
            this.btRequestEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRequestEvents.Location = new System.Drawing.Point(558, 426);
            this.btRequestEvents.Name = "btRequestEvents";
            this.btRequestEvents.Size = new System.Drawing.Size(241, 23);
            this.btRequestEvents.TabIndex = 1;
            this.btRequestEvents.Text = "Request Events Detection List From COMAV";
            this.btRequestEvents.UseVisualStyleBackColor = true;
            this.btRequestEvents.Click += new System.EventHandler(this.btRequestEvents_Click);
            // 
            // lblTimeTag
            // 
            this.lblTimeTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTimeTag.AutoSize = true;
            this.lblTimeTag.Location = new System.Drawing.Point(5, 431);
            this.lblTimeTag.Name = "lblTimeTag";
            this.lblTimeTag.Size = new System.Drawing.Size(187, 13);
            this.lblTimeTag.TabIndex = 2;
            this.lblTimeTag.Text = "Events Detection List not received yet";
            // 
            // lbEventsDetection
            // 
            this.lbEventsDetection.AutoSize = true;
            this.lbEventsDetection.Location = new System.Drawing.Point(5, 9);
            this.lbEventsDetection.Name = "lbEventsDetection";
            this.lbEventsDetection.Size = new System.Drawing.Size(614, 13);
            this.lbEventsDetection.TabIndex = 3;
            this.lbEventsDetection.Text = "Attention: events that are not listed did not occur on COMAV yet. By default, whe" +
                "n these events occur, they will generate reports.";
            // 
            // chkTimeTagDate
            // 
            this.chkTimeTagDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTimeTagDate.AutoSize = true;
            this.chkTimeTagDate.Enabled = false;
            this.chkTimeTagDate.Location = new System.Drawing.Point(668, 5);
            this.chkTimeTagDate.Name = "chkTimeTagDate";
            this.chkTimeTagDate.Size = new System.Drawing.Size(131, 17);
            this.chkTimeTagDate.TabIndex = 5;
            this.chkTimeTagDate.Text = "Show time-tag as date";
            this.chkTimeTagDate.UseVisualStyleBackColor = true;
            this.chkTimeTagDate.CheckedChanged += new System.EventHandler(this.chkTimeTagDate_CheckedChanged);
            // 
            // FrmEventsDetectionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(803, 455);
            this.Controls.Add(this.chkTimeTagDate);
            this.Controls.Add(this.lbEventsDetection);
            this.Controls.Add(this.lblTimeTag);
            this.Controls.Add(this.btRequestEvents);
            this.Controls.Add(this.gridEventDetection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmEventsDetectionList";
            this.Text = "Events Detection List";
            this.DockStateChanged += new System.EventHandler(this.FrmEventsDetectionList_DockStateChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gridEventDetection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridEventDetection;
        private System.Windows.Forms.Label lblTimeTag;
        private System.Windows.Forms.Label lbEventsDetection;
        public System.Windows.Forms.Button btRequestEvents;
        private System.Windows.Forms.CheckBox chkTimeTagDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfOccurrences;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_Seconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_Miliseconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn SequenceCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceSubtype;

    }
}
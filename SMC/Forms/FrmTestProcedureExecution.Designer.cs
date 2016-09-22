namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmTestProcedureExecution
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
            this.lblProcedureId = new System.Windows.Forms.Label();
            this.txtProcedureId = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.txtPurpose = new System.Windows.Forms.TextBox();
            this.lblStartedIn = new System.Windows.Forms.Label();
            this.txtStartedIn = new System.Windows.Forms.TextBox();
            this.lblIteration = new System.Windows.Forms.Label();
            this.txtIteration = new System.Windows.Forms.TextBox();
            this.lblEstimatedTime = new System.Windows.Forms.Label();
            this.txtEstimatedTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btStartProcedure = new System.Windows.Forms.Button();
            this.btStopProcedure = new System.Windows.Forms.Button();
            this.btShowReport = new System.Windows.Forms.Button();
            this.treeGridViewSteps = new AdvancedDataGridView.TreeGridView();
            this.colStepNumber = new AdvancedDataGridView.TreeGridColumn();
            this.colSteps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExecutionRealTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkStopExecution = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExecutionStatus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSessionId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridViewSteps)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProcedureId
            // 
            this.lblProcedureId.AutoSize = true;
            this.lblProcedureId.Location = new System.Drawing.Point(7, 29);
            this.lblProcedureId.Name = "lblProcedureId";
            this.lblProcedureId.Size = new System.Drawing.Size(73, 13);
            this.lblProcedureId.TabIndex = 0;
            this.lblProcedureId.Text = "Procedure ID:";
            // 
            // txtProcedureId
            // 
            this.txtProcedureId.BackColor = System.Drawing.Color.White;
            this.txtProcedureId.Location = new System.Drawing.Point(86, 26);
            this.txtProcedureId.Name = "txtProcedureId";
            this.txtProcedureId.ReadOnly = true;
            this.txtProcedureId.Size = new System.Drawing.Size(100, 20);
            this.txtProcedureId.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(192, 29);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(261, 26);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(900, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // lblPurpose
            // 
            this.lblPurpose.AutoSize = true;
            this.lblPurpose.Location = new System.Drawing.Point(7, 55);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(49, 13);
            this.lblPurpose.TabIndex = 4;
            this.lblPurpose.Text = "Purpose:";
            // 
            // txtPurpose
            // 
            this.txtPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurpose.BackColor = System.Drawing.Color.White;
            this.txtPurpose.Location = new System.Drawing.Point(86, 52);
            this.txtPurpose.Multiline = true;
            this.txtPurpose.Name = "txtPurpose";
            this.txtPurpose.ReadOnly = true;
            this.txtPurpose.Size = new System.Drawing.Size(1075, 95);
            this.txtPurpose.TabIndex = 5;
            // 
            // lblStartedIn
            // 
            this.lblStartedIn.AutoSize = true;
            this.lblStartedIn.BackColor = System.Drawing.Color.Lavender;
            this.lblStartedIn.Location = new System.Drawing.Point(93, 177);
            this.lblStartedIn.Name = "lblStartedIn";
            this.lblStartedIn.Size = new System.Drawing.Size(56, 13);
            this.lblStartedIn.TabIndex = 6;
            this.lblStartedIn.Text = "Started In:";
            // 
            // txtStartedIn
            // 
            this.txtStartedIn.BackColor = System.Drawing.Color.White;
            this.txtStartedIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartedIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartedIn.Location = new System.Drawing.Point(96, 193);
            this.txtStartedIn.Name = "txtStartedIn";
            this.txtStartedIn.ReadOnly = true;
            this.txtStartedIn.Size = new System.Drawing.Size(132, 20);
            this.txtStartedIn.TabIndex = 7;
            this.txtStartedIn.Text = " 00/00/0000  00:00:00 ";
            this.txtStartedIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIteration
            // 
            this.lblIteration.AutoSize = true;
            this.lblIteration.BackColor = System.Drawing.Color.Lavender;
            this.lblIteration.Location = new System.Drawing.Point(231, 177);
            this.lblIteration.Name = "lblIteration";
            this.lblIteration.Size = new System.Drawing.Size(100, 13);
            this.lblIteration.TabIndex = 8;
            this.lblIteration.Text = "Procedure Iteration:";
            // 
            // txtIteration
            // 
            this.txtIteration.BackColor = System.Drawing.Color.White;
            this.txtIteration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIteration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIteration.Location = new System.Drawing.Point(234, 193);
            this.txtIteration.Name = "txtIteration";
            this.txtIteration.ReadOnly = true;
            this.txtIteration.Size = new System.Drawing.Size(132, 20);
            this.txtIteration.TabIndex = 9;
            this.txtIteration.Text = "zero of ";
            this.txtIteration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEstimatedTime
            // 
            this.lblEstimatedTime.AutoSize = true;
            this.lblEstimatedTime.BackColor = System.Drawing.Color.Lavender;
            this.lblEstimatedTime.Location = new System.Drawing.Point(372, 164);
            this.lblEstimatedTime.Name = "lblEstimatedTime";
            this.lblEstimatedTime.Size = new System.Drawing.Size(78, 26);
            this.lblEstimatedTime.TabIndex = 10;
            this.lblEstimatedTime.Text = "Estimated time \r\nto finish (sec):";
            // 
            // txtEstimatedTime
            // 
            this.txtEstimatedTime.BackColor = System.Drawing.Color.White;
            this.txtEstimatedTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEstimatedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstimatedTime.Location = new System.Drawing.Point(372, 193);
            this.txtEstimatedTime.Name = "txtEstimatedTime";
            this.txtEstimatedTime.ReadOnly = true;
            this.txtEstimatedTime.Size = new System.Drawing.Size(132, 20);
            this.txtEstimatedTime.TabIndex = 11;
            this.txtEstimatedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1166, 17);
            this.label2.TabIndex = 80;
            this.label2.Text = "Procedure Steps";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btStartProcedure
            // 
            this.btStartProcedure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btStartProcedure.Location = new System.Drawing.Point(5, 471);
            this.btStartProcedure.Name = "btStartProcedure";
            this.btStartProcedure.Size = new System.Drawing.Size(206, 37);
            this.btStartProcedure.TabIndex = 82;
            this.btStartProcedure.Text = "Start Procedure Execution";
            this.btStartProcedure.UseVisualStyleBackColor = true;
            this.btStartProcedure.Click += new System.EventHandler(this.btStartProcedure_Click);
            // 
            // btStopProcedure
            // 
            this.btStopProcedure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btStopProcedure.Location = new System.Drawing.Point(385, 471);
            this.btStopProcedure.Name = "btStopProcedure";
            this.btStopProcedure.Size = new System.Drawing.Size(206, 37);
            this.btStopProcedure.TabIndex = 83;
            this.btStopProcedure.Text = "Stop Procedure Execution";
            this.btStopProcedure.UseVisualStyleBackColor = true;
            this.btStopProcedure.Click += new System.EventHandler(this.btStopProcedure_Click);
            // 
            // btShowReport
            // 
            this.btShowReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btShowReport.Enabled = false;
            this.btShowReport.Location = new System.Drawing.Point(928, 471);
            this.btShowReport.Name = "btShowReport";
            this.btShowReport.Size = new System.Drawing.Size(115, 37);
            this.btShowReport.TabIndex = 84;
            this.btShowReport.Text = "Show Report";
            this.btShowReport.UseVisualStyleBackColor = true;
            // 
            // treeGridViewSteps
            // 
            this.treeGridViewSteps.AllowUserToAddRows = false;
            this.treeGridViewSteps.AllowUserToDeleteRows = false;
            this.treeGridViewSteps.AllowUserToResizeColumns = false;
            this.treeGridViewSteps.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.treeGridViewSteps.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.treeGridViewSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGridViewSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStepNumber,
            this.colSteps,
            this.colStatus});
            this.treeGridViewSteps.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeGridViewSteps.ImageList = null;
            this.treeGridViewSteps.Location = new System.Drawing.Point(1, 254);
            this.treeGridViewSteps.MultiSelect = false;
            this.treeGridViewSteps.Name = "treeGridViewSteps";
            this.treeGridViewSteps.ReadOnly = true;
            this.treeGridViewSteps.RowHeadersVisible = false;
            this.treeGridViewSteps.RowHeadersWidth = 25;
            this.treeGridViewSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.treeGridViewSteps.Size = new System.Drawing.Size(1166, 211);
            this.treeGridViewSteps.StandardTab = true;
            this.treeGridViewSteps.TabIndex = 88;
            // 
            // colStepNumber
            // 
            this.colStepNumber.DefaultNodeImage = null;
            this.colStepNumber.HeaderText = "Sequence";
            this.colStepNumber.Name = "colStepNumber";
            this.colStepNumber.ReadOnly = true;
            this.colStepNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colStepNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colStepNumber.Width = 120;
            // 
            // colSteps
            // 
            this.colSteps.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSteps.HeaderText = "Steps";
            this.colSteps.Name = "colSteps";
            this.colSteps.ReadOnly = true;
            this.colSteps.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSteps.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colStatus.Width = 285;
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(1049, 471);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(115, 37);
            this.btClose.TabIndex = 90;
            this.btClose.Text = "Finish";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Lavender;
            this.label3.Location = new System.Drawing.Point(507, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 91;
            this.label3.Text = "Elapsed time (sec.):";
            // 
            // txtExecutionRealTime
            // 
            this.txtExecutionRealTime.BackColor = System.Drawing.Color.White;
            this.txtExecutionRealTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExecutionRealTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExecutionRealTime.Location = new System.Drawing.Point(510, 193);
            this.txtExecutionRealTime.Name = "txtExecutionRealTime";
            this.txtExecutionRealTime.ReadOnly = true;
            this.txtExecutionRealTime.Size = new System.Drawing.Size(132, 20);
            this.txtExecutionRealTime.TabIndex = 92;
            this.txtExecutionRealTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Lavender;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Location = new System.Drawing.Point(86, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1075, 68);
            this.label4.TabIndex = 93;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1166, 17);
            this.label6.TabIndex = 95;
            this.label6.Text = "Procedure Header";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkStopExecution
            // 
            this.chkStopExecution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkStopExecution.AutoSize = true;
            this.chkStopExecution.Location = new System.Drawing.Point(217, 475);
            this.chkStopExecution.Name = "chkStopExecution";
            this.chkStopExecution.Size = new System.Drawing.Size(162, 30);
            this.chkStopExecution.TabIndex = 96;
            this.chkStopExecution.Text = "Stop when occurs failure \r\nreport during steps execution";
            this.chkStopExecution.UseVisualStyleBackColor = true;
            this.chkStopExecution.CheckedChanged += new System.EventHandler(this.chkStopExecution_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Lavender;
            this.label1.Location = new System.Drawing.Point(861, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Execution Status:";
            // 
            // txtExecutionStatus
            // 
            this.txtExecutionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExecutionStatus.BackColor = System.Drawing.Color.White;
            this.txtExecutionStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExecutionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExecutionStatus.Location = new System.Drawing.Point(864, 193);
            this.txtExecutionStatus.Name = "txtExecutionStatus";
            this.txtExecutionStatus.Size = new System.Drawing.Size(286, 20);
            this.txtExecutionStatus.TabIndex = 98;
            this.txtExecutionStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Lavender;
            this.label5.Location = new System.Drawing.Point(645, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 99;
            this.label5.Text = "Session ID:";
            // 
            // txtSessionId
            // 
            this.txtSessionId.BackColor = System.Drawing.Color.White;
            this.txtSessionId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSessionId.Location = new System.Drawing.Point(648, 193);
            this.txtSessionId.Name = "txtSessionId";
            this.txtSessionId.Size = new System.Drawing.Size(132, 20);
            this.txtSessionId.TabIndex = 100;
            this.txtSessionId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmTestProcedureExecution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 513);
            this.Controls.Add(this.txtSessionId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtExecutionStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkStopExecution);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtExecutionRealTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.treeGridViewSteps);
            this.Controls.Add(this.btShowReport);
            this.Controls.Add(this.btStopProcedure);
            this.Controls.Add(this.btStartProcedure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEstimatedTime);
            this.Controls.Add(this.lblEstimatedTime);
            this.Controls.Add(this.txtIteration);
            this.Controls.Add(this.lblIteration);
            this.Controls.Add(this.txtStartedIn);
            this.Controls.Add(this.lblStartedIn);
            this.Controls.Add(this.txtPurpose);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtProcedureId);
            this.Controls.Add(this.lblProcedureId);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmTestProcedureExecution";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test Procedure Execution";
            this.Load += new System.EventHandler(this.FrmTestProcedureExecution_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTestProcedureExecution_FormClosed);
            this.DockStateChanged += new System.EventHandler(this.FrmTestProcedureExecution_DockStateChanged);
            ((System.ComponentModel.ISupportInitialize)(this.treeGridViewSteps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProcedureId;
        private System.Windows.Forms.TextBox txtProcedureId;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPurpose;
        private System.Windows.Forms.TextBox txtPurpose;
        private System.Windows.Forms.Label lblStartedIn;
        private System.Windows.Forms.TextBox txtStartedIn;
        private System.Windows.Forms.Label lblIteration;
        private System.Windows.Forms.TextBox txtIteration;
        private System.Windows.Forms.Label lblEstimatedTime;
        private System.Windows.Forms.TextBox txtEstimatedTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btStartProcedure;
        private System.Windows.Forms.Button btStopProcedure;
        private System.Windows.Forms.Button btShowReport;
        private AdvancedDataGridView.TreeGridView treeGridViewSteps;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExecutionRealTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkStopExecution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExecutionStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSessionId;
        private AdvancedDataGridView.TreeGridColumn colStepNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSteps;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}
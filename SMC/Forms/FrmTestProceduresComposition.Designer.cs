namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmTestProceduresComposition
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbContourTCs = new System.Windows.Forms.ComboBox();
            this.lblContourTCs = new System.Windows.Forms.Label();
            this.gridProcedureSteps = new System.Windows.Forms.DataGridView();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.chkSendEMail = new System.Windows.Forms.CheckBox();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grpPacketsSequenceControl = new System.Windows.Forms.GroupBox();
            this.rdbControlSequence = new System.Windows.Forms.RadioButton();
            this.rdbRespectSequence = new System.Windows.Forms.RadioButton();
            this.rdbDisableSequenceControl = new System.Windows.Forms.RadioButton();
            this.numLoopIterations = new System.Windows.Forms.NumericUpDown();
            this.lblLoopIterations = new System.Windows.Forms.Label();
            this.chkRunInLoop = new System.Windows.Forms.CheckBox();
            this.chkGetCpuUsage = new System.Windows.Forms.CheckBox();
            this.chkSynchronizeOBT = new System.Windows.Forms.CheckBox();
            this.txtEstimatedDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtPurpose = new System.Windows.Forms.TextBox();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtProcedureID = new System.Windows.Forms.TextBox();
            this.lblProcedureID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.btExecute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btCopyProcedure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcedureSteps)).BeginInit();
            this.grpPacketsSequenceControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopIterations)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1242, 614);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridDatabase);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1234, 588);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
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
            this.gridDatabase.RowHeadersWidth = 25;
            this.gridDatabase.RowTemplate.ReadOnly = true;
            this.gridDatabase.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDatabase.Size = new System.Drawing.Size(1228, 582);
            this.gridDatabase.StandardTab = true;
            this.gridDatabase.TabIndex = 1;
            this.gridDatabase.DoubleClick += new System.EventHandler(this.gridDatabase_DoubleClick);
            this.gridDatabase.SelectionChanged += new System.EventHandler(this.gridDatabase_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmbContourTCs);
            this.tabPage2.Controls.Add(this.lblContourTCs);
            this.tabPage2.Controls.Add(this.gridProcedureSteps);
            this.tabPage2.Controls.Add(this.btDown);
            this.tabPage2.Controls.Add(this.btUp);
            this.tabPage2.Controls.Add(this.chkSendEMail);
            this.tabPage2.Controls.Add(this.btRemove);
            this.tabPage2.Controls.Add(this.btAdd);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.grpPacketsSequenceControl);
            this.tabPage2.Controls.Add(this.numLoopIterations);
            this.tabPage2.Controls.Add(this.lblLoopIterations);
            this.tabPage2.Controls.Add(this.chkRunInLoop);
            this.tabPage2.Controls.Add(this.chkGetCpuUsage);
            this.tabPage2.Controls.Add(this.chkSynchronizeOBT);
            this.tabPage2.Controls.Add(this.txtEstimatedDuration);
            this.tabPage2.Controls.Add(this.lblDuration);
            this.tabPage2.Controls.Add(this.txtPurpose);
            this.tabPage2.Controls.Add(this.lblPurpose);
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.lblDescription);
            this.tabPage2.Controls.Add(this.txtProcedureID);
            this.tabPage2.Controls.Add(this.lblProcedureID);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1234, 588);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbContourTCs
            // 
            this.cmbContourTCs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbContourTCs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContourTCs.FormattingEnabled = true;
            this.cmbContourTCs.Location = new System.Drawing.Point(801, 43);
            this.cmbContourTCs.Name = "cmbContourTCs";
            this.cmbContourTCs.Size = new System.Drawing.Size(422, 21);
            this.cmbContourTCs.TabIndex = 3;
            // 
            // lblContourTCs
            // 
            this.lblContourTCs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContourTCs.AutoSize = true;
            this.lblContourTCs.Location = new System.Drawing.Point(798, 27);
            this.lblContourTCs.Name = "lblContourTCs";
            this.lblContourTCs.Size = new System.Drawing.Size(58, 13);
            this.lblContourTCs.TabIndex = 85;
            this.lblContourTCs.Text = "Test Case:";
            // 
            // gridProcedureSteps
            // 
            this.gridProcedureSteps.AllowUserToAddRows = false;
            this.gridProcedureSteps.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Lavender;
            this.gridProcedureSteps.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridProcedureSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridProcedureSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcedureSteps.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridProcedureSteps.Location = new System.Drawing.Point(2, 294);
            this.gridProcedureSteps.MultiSelect = false;
            this.gridProcedureSteps.Name = "gridProcedureSteps";
            this.gridProcedureSteps.RowHeadersVisible = false;
            this.gridProcedureSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridProcedureSteps.Size = new System.Drawing.Size(1229, 288);
            this.gridProcedureSteps.TabIndex = 14;
            this.gridProcedureSteps.Scroll += new System.Windows.Forms.ScrollEventHandler(this.gridProcedureSteps_Scroll_1);
            this.gridProcedureSteps.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProcedureSteps_CellValidated_1);
            this.gridProcedureSteps.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridProcedureSteps_CellValidating_1);
            this.gridProcedureSteps.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridProcedureSteps_CellPainting_1);
            this.gridProcedureSteps.Paint += new System.Windows.Forms.PaintEventHandler(this.gridProcedureSteps_Paint_1);
            this.gridProcedureSteps.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProcedureSteps_CellEnter_1);
            this.gridProcedureSteps.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.gridProcedureSteps_ColumnWidthChanged_1);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(245, 268);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(75, 23);
            this.btDown.TabIndex = 13;
            this.btDown.Text = "Down";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click_1);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(164, 268);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(75, 23);
            this.btUp.TabIndex = 12;
            this.btUp.Text = "Up";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click_1);
            // 
            // chkSendEMail
            // 
            this.chkSendEMail.AutoSize = true;
            this.chkSendEMail.Location = new System.Drawing.Point(9, 223);
            this.chkSendEMail.Name = "chkSendEMail";
            this.chkSendEMail.Size = new System.Drawing.Size(127, 17);
            this.chkSendEMail.TabIndex = 9;
            this.chkSendEMail.Text = "Send e-mail on failure";
            this.chkSendEMail.UseVisualStyleBackColor = true;
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(83, 268);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 23);
            this.btRemove.TabIndex = 11;
            this.btRemove.Text = "Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click_1);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(2, 268);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 10;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click_1);
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
            this.label2.Location = new System.Drawing.Point(1, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1231, 15);
            this.label2.TabIndex = 79;
            this.label2.Text = "Procedure Steps";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpPacketsSequenceControl
            // 
            this.grpPacketsSequenceControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPacketsSequenceControl.Controls.Add(this.rdbControlSequence);
            this.grpPacketsSequenceControl.Controls.Add(this.rdbRespectSequence);
            this.grpPacketsSequenceControl.Controls.Add(this.rdbDisableSequenceControl);
            this.grpPacketsSequenceControl.Location = new System.Drawing.Point(297, 149);
            this.grpPacketsSequenceControl.Name = "grpPacketsSequenceControl";
            this.grpPacketsSequenceControl.Size = new System.Drawing.Size(929, 94);
            this.grpPacketsSequenceControl.TabIndex = 78;
            this.grpPacketsSequenceControl.TabStop = false;
            this.grpPacketsSequenceControl.Text = "Packets Sequence Control:";
            // 
            // rdbControlSequence
            // 
            this.rdbControlSequence.AutoSize = true;
            this.rdbControlSequence.Checked = true;
            this.rdbControlSequence.Location = new System.Drawing.Point(6, 19);
            this.rdbControlSequence.Name = "rdbControlSequence";
            this.rdbControlSequence.Size = new System.Drawing.Size(396, 17);
            this.rdbControlSequence.TabIndex = 0;
            this.rdbControlSequence.TabStop = true;
            this.rdbControlSequence.Text = "Control the sequence of packets (sequence and CRC errors can not be tested)";
            this.rdbControlSequence.UseVisualStyleBackColor = true;
            // 
            // rdbRespectSequence
            // 
            this.rdbRespectSequence.AutoSize = true;
            this.rdbRespectSequence.Location = new System.Drawing.Point(6, 43);
            this.rdbRespectSequence.Name = "rdbRespectSequence";
            this.rdbRespectSequence.Size = new System.Drawing.Size(349, 17);
            this.rdbRespectSequence.TabIndex = 1;
            this.rdbRespectSequence.Text = "Respect the sequence of packets (to test sequence and CRC errors)";
            this.rdbRespectSequence.UseVisualStyleBackColor = true;
            // 
            // rdbDisableSequenceControl
            // 
            this.rdbDisableSequenceControl.AutoSize = true;
            this.rdbDisableSequenceControl.Location = new System.Drawing.Point(6, 66);
            this.rdbDisableSequenceControl.Name = "rdbDisableSequenceControl";
            this.rdbDisableSequenceControl.Size = new System.Drawing.Size(346, 17);
            this.rdbDisableSequenceControl.TabIndex = 2;
            this.rdbDisableSequenceControl.Text = "Disable packets sequence control at the beggining of the procedure";
            this.rdbDisableSequenceControl.UseVisualStyleBackColor = true;
            // 
            // numLoopIterations
            // 
            this.numLoopIterations.Location = new System.Drawing.Point(232, 197);
            this.numLoopIterations.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLoopIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLoopIterations.Name = "numLoopIterations";
            this.numLoopIterations.Size = new System.Drawing.Size(59, 20);
            this.numLoopIterations.TabIndex = 8;
            this.numLoopIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLoopIterations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLoopIterations.ValueChanged += new System.EventHandler(this.numLoopIterations_ValueChanged);
            // 
            // lblLoopIterations
            // 
            this.lblLoopIterations.AutoSize = true;
            this.lblLoopIterations.Location = new System.Drawing.Point(146, 201);
            this.lblLoopIterations.Name = "lblLoopIterations";
            this.lblLoopIterations.Size = new System.Drawing.Size(80, 13);
            this.lblLoopIterations.TabIndex = 75;
            this.lblLoopIterations.Text = "Loop Iterations:";
            // 
            // chkRunInLoop
            // 
            this.chkRunInLoop.AutoSize = true;
            this.chkRunInLoop.Location = new System.Drawing.Point(9, 200);
            this.chkRunInLoop.Name = "chkRunInLoop";
            this.chkRunInLoop.Size = new System.Drawing.Size(131, 17);
            this.chkRunInLoop.TabIndex = 7;
            this.chkRunInLoop.Text = "Run procedure in loop";
            this.chkRunInLoop.UseVisualStyleBackColor = true;
            this.chkRunInLoop.CheckedChanged += new System.EventHandler(this.chkRunProcedure_CheckedChanged_1);
            // 
            // chkGetCpuUsage
            // 
            this.chkGetCpuUsage.AutoSize = true;
            this.chkGetCpuUsage.Location = new System.Drawing.Point(9, 177);
            this.chkGetCpuUsage.Name = "chkGetCpuUsage";
            this.chkGetCpuUsage.Size = new System.Drawing.Size(232, 17);
            this.chkGetCpuUsage.TabIndex = 6;
            this.chkGetCpuUsage.Text = "Get CPU usage at the end of the procedure";
            this.chkGetCpuUsage.UseVisualStyleBackColor = true;
            // 
            // chkSynchronizeOBT
            // 
            this.chkSynchronizeOBT.AutoSize = true;
            this.chkSynchronizeOBT.Checked = true;
            this.chkSynchronizeOBT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSynchronizeOBT.Location = new System.Drawing.Point(9, 154);
            this.chkSynchronizeOBT.Name = "chkSynchronizeOBT";
            this.chkSynchronizeOBT.Size = new System.Drawing.Size(269, 17);
            this.chkSynchronizeOBT.TabIndex = 5;
            this.chkSynchronizeOBT.Text = "Synchronize OBT at the beggining of the procedure";
            this.chkSynchronizeOBT.UseVisualStyleBackColor = true;
            // 
            // txtEstimatedDuration
            // 
            this.txtEstimatedDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstimatedDuration.Location = new System.Drawing.Point(601, 43);
            this.txtEstimatedDuration.Name = "txtEstimatedDuration";
            this.txtEstimatedDuration.ReadOnly = true;
            this.txtEstimatedDuration.Size = new System.Drawing.Size(158, 20);
            this.txtEstimatedDuration.TabIndex = 2;
            this.txtEstimatedDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDuration
            // 
            this.lblDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(598, 27);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(135, 13);
            this.lblDuration.TabIndex = 70;
            this.lblDuration.Text = "Estimated Duration (Secs.):";
            // 
            // txtPurpose
            // 
            this.txtPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurpose.Location = new System.Drawing.Point(7, 82);
            this.txtPurpose.MaxLength = 1024;
            this.txtPurpose.Multiline = true;
            this.txtPurpose.Name = "txtPurpose";
            this.txtPurpose.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPurpose.Size = new System.Drawing.Size(1219, 62);
            this.txtPurpose.TabIndex = 4;
            // 
            // lblPurpose
            // 
            this.lblPurpose.AutoSize = true;
            this.lblPurpose.Location = new System.Drawing.Point(5, 66);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(49, 13);
            this.lblPurpose.TabIndex = 68;
            this.lblPurpose.Text = "Purpose:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(83, 43);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(498, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(81, 27);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 66;
            this.lblDescription.Text = "Description:";
            // 
            // txtProcedureID
            // 
            this.txtProcedureID.Location = new System.Drawing.Point(7, 43);
            this.txtProcedureID.Name = "txtProcedureID";
            this.txtProcedureID.ReadOnly = true;
            this.txtProcedureID.Size = new System.Drawing.Size(70, 20);
            this.txtProcedureID.TabIndex = 0;
            this.txtProcedureID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblProcedureID
            // 
            this.lblProcedureID.AutoSize = true;
            this.lblProcedureID.Location = new System.Drawing.Point(5, 27);
            this.lblProcedureID.Name = "lblProcedureID";
            this.lblProcedureID.Size = new System.Drawing.Size(73, 13);
            this.lblProcedureID.TabIndex = 64;
            this.lblProcedureID.Text = "Procedure ID:";
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
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1230, 15);
            this.label1.TabIndex = 63;
            this.label1.Text = "Procedure Header";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btExecute,
            this.toolStripSeparator5,
            this.btCopyProcedure,
            this.toolStripSeparator6});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 589);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1242, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
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
            // btExecute
            // 
            this.btExecute.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.procedure_execution;
            this.btExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExecute.Name = "btExecute";
            this.btExecute.Size = new System.Drawing.Size(67, 22);
            this.btExecute.Text = "Execute";
            this.btExecute.ToolTipText = "Opens the Test Procedures Execution Window";
            this.btExecute.Click += new System.EventHandler(this.btExecute_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btCopyProcedure
            // 
            this.btCopyProcedure.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.copy;
            this.btCopyProcedure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCopyProcedure.Name = "btCopyProcedure";
            this.btCopyProcedure.Size = new System.Drawing.Size(112, 22);
            this.btCopyProcedure.Text = "Copy Procedure";
            this.btCopyProcedure.Click += new System.EventHandler(this.btCopyProcedure_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmTestProceduresComposition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1242, 614);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmTestProceduresComposition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Procedures Composition";
            this.Load += new System.EventHandler(this.FrmTestProceduresComposition_Load);
            this.DockStateChanged += new System.EventHandler(this.FrmTestProceduresComposition_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTestProceduresComposition_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcedureSteps)).EndInit();
            this.grpPacketsSequenceControl.ResumeLayout(false);
            this.grpPacketsSequenceControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopIterations)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridProcedureSteps;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.CheckBox chkSendEMail;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpPacketsSequenceControl;
        private System.Windows.Forms.RadioButton rdbControlSequence;
        private System.Windows.Forms.RadioButton rdbRespectSequence;
        private System.Windows.Forms.RadioButton rdbDisableSequenceControl;
        private System.Windows.Forms.NumericUpDown numLoopIterations;
        private System.Windows.Forms.Label lblLoopIterations;
        private System.Windows.Forms.CheckBox chkRunInLoop;
        private System.Windows.Forms.CheckBox chkGetCpuUsage;
        private System.Windows.Forms.CheckBox chkSynchronizeOBT;
        private System.Windows.Forms.TextBox txtEstimatedDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtPurpose;
        private System.Windows.Forms.Label lblPurpose;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtProcedureID;
        private System.Windows.Forms.Label lblProcedureID;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.ToolStripButton btExecute;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btCopyProcedure;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        public System.Windows.Forms.DataGridView gridDatabase;
        public System.Windows.Forms.ComboBox cmbContourTCs;
        private System.Windows.Forms.Label lblContourTCs;

    }
}
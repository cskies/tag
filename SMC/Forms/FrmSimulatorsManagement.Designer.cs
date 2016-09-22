namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSimulatorsManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridMsgToSend = new System.Windows.Forms.DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeFollowing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gridMsgToReceive = new System.Windows.Forms.DataGridView();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckFullMsg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIntervalToAnswer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponseMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepeatAnswer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colRepetitionInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridSimulators = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSimName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSimDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSimDescription = new System.Windows.Forms.TextBox();
            this.txtSimName = new System.Windows.Forms.TextBox();
            this.txtSimId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.btEdit = new System.Windows.Forms.ToolStripButton();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.btConfirm = new System.Windows.Forms.ToolStripButton();
            this.btCancel = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridMsgToSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMsgToReceive)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulators)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridMsgToSend
            // 
            this.gridMsgToSend.AllowUserToResizeRows = false;
            this.gridMsgToSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMsgToSend.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridMsgToSend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMsgToSend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colDesc,
            this.colMessage,
            this.colTimeFollowing});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridMsgToSend.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridMsgToSend.Location = new System.Drawing.Point(5, 202);
            this.gridMsgToSend.Name = "gridMsgToSend";
            this.gridMsgToSend.RowHeadersWidth = 30;
            this.gridMsgToSend.Size = new System.Drawing.Size(1192, 250);
            this.gridMsgToSend.TabIndex = 3;
            this.gridMsgToSend.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMsgToSend_CellValidated);
            this.gridMsgToSend.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridMsgToSend_RowsAdded);
            // 
            // colIndex
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colIndex.DefaultCellStyle = dataGridViewCellStyle1;
            this.colIndex.HeaderText = "Id";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colIndex.Visible = false;
            this.colIndex.Width = 50;
            // 
            // colDesc
            // 
            this.colDesc.HeaderText = "Name";
            this.colDesc.Name = "colDesc";
            this.colDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDesc.Width = 200;
            // 
            // colMessage
            // 
            this.colMessage.HeaderText = "Message to send";
            this.colMessage.Name = "colMessage";
            this.colMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colMessage.Width = 300;
            // 
            // colTimeFollowing
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colTimeFollowing.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTimeFollowing.HeaderText = "Delay to send again (ms)";
            this.colTimeFollowing.MaxInputLength = 2147483646;
            this.colTimeFollowing.Name = "colTimeFollowing";
            this.colTimeFollowing.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTimeFollowing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTimeFollowing.Width = 90;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1192, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Messages to send periodically";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 458);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1194, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "Messages to receive and answer";
            // 
            // gridMsgToReceive
            // 
            this.gridMsgToReceive.AllowUserToResizeRows = false;
            this.gridMsgToReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMsgToReceive.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridMsgToReceive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMsgToReceive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNum,
            this.colDescription,
            this.colMsg,
            this.colCheckFullMsg,
            this.colIntervalToAnswer,
            this.colResponseMsg,
            this.colRepeatAnswer,
            this.colRepetitionInterval});
            this.gridMsgToReceive.Location = new System.Drawing.Point(5, 480);
            this.gridMsgToReceive.Name = "gridMsgToReceive";
            this.gridMsgToReceive.RowHeadersWidth = 30;
            this.gridMsgToReceive.Size = new System.Drawing.Size(1192, 301);
            this.gridMsgToReceive.TabIndex = 4;
            this.gridMsgToReceive.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMsgToReceive_CellValidated);
            this.gridMsgToReceive.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridMsgToReceive_RowsAdded);
            this.gridMsgToReceive.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMsgToReceive_CellEndEdit);
            // 
            // colNum
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colNum.DefaultCellStyle = dataGridViewCellStyle4;
            this.colNum.HeaderText = "Id";
            this.colNum.Name = "colNum";
            this.colNum.ReadOnly = true;
            this.colNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colNum.Visible = false;
            this.colNum.Width = 50;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Name";
            this.colDescription.Name = "colDescription";
            this.colDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDescription.Width = 200;
            // 
            // colMsg
            // 
            this.colMsg.HeaderText = "Message to receive";
            this.colMsg.Name = "colMsg";
            this.colMsg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colMsg.Width = 300;
            // 
            // colCheckFullMsg
            // 
            this.colCheckFullMsg.HeaderText = "Check full message";
            this.colCheckFullMsg.Name = "colCheckFullMsg";
            this.colCheckFullMsg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheckFullMsg.Width = 70;
            // 
            // colIntervalToAnswer
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colIntervalToAnswer.DefaultCellStyle = dataGridViewCellStyle5;
            this.colIntervalToAnswer.HeaderText = "Delay to answer (ms)";
            this.colIntervalToAnswer.MaxInputLength = 2147483646;
            this.colIntervalToAnswer.Name = "colIntervalToAnswer";
            this.colIntervalToAnswer.Width = 90;
            // 
            // colResponseMsg
            // 
            this.colResponseMsg.HeaderText = "Message to answer";
            this.colResponseMsg.Name = "colResponseMsg";
            this.colResponseMsg.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colResponseMsg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colResponseMsg.Width = 300;
            // 
            // colRepeatAnswer
            // 
            this.colRepeatAnswer.HeaderText = "Repeat answer";
            this.colRepeatAnswer.Name = "colRepeatAnswer";
            this.colRepeatAnswer.Width = 70;
            // 
            // colRepetitionInterval
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colRepetitionInterval.DefaultCellStyle = dataGridViewCellStyle6;
            this.colRepetitionInterval.HeaderText = "Repetition interval (ms)";
            this.colRepetitionInterval.MaxInputLength = 2147483646;
            this.colRepetitionInterval.Name = "colRepetitionInterval";
            this.colRepetitionInterval.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRepetitionInterval.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRepetitionInterval.Width = 90;
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
            this.tabControl1.Size = new System.Drawing.Size(1205, 811);
            this.tabControl1.TabIndex = 25;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridSimulators);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1197, 785);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridSimulators
            // 
            this.gridSimulators.AllowUserToAddRows = false;
            this.gridSimulators.AllowUserToDeleteRows = false;
            this.gridSimulators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSimulators.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colSimName,
            this.colSimDesc});
            this.gridSimulators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSimulators.Location = new System.Drawing.Point(3, 3);
            this.gridSimulators.MultiSelect = false;
            this.gridSimulators.Name = "gridSimulators";
            this.gridSimulators.ReadOnly = true;
            this.gridSimulators.RowHeadersVisible = false;
            this.gridSimulators.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSimulators.Size = new System.Drawing.Size(1191, 779);
            this.gridSimulators.TabIndex = 0;
            this.gridSimulators.DoubleClick += new System.EventHandler(this.gridSimulators_DoubleClick);
            // 
            // colId
            // 
            this.colId.HeaderText = "Simulator Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Width = 110;
            // 
            // colSimName
            // 
            this.colSimName.FillWeight = 200F;
            this.colSimName.HeaderText = "Name";
            this.colSimName.Name = "colSimName";
            this.colSimName.ReadOnly = true;
            this.colSimName.Width = 300;
            // 
            // colSimDesc
            // 
            this.colSimDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSimDesc.HeaderText = "Description";
            this.colSimDesc.Name = "colSimDesc";
            this.colSimDesc.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtSimDescription);
            this.tabPage2.Controls.Add(this.txtSimName);
            this.tabPage2.Controls.Add(this.txtSimId);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.gridMsgToReceive);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.gridMsgToSend);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1197, 785);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Description:";
            // 
            // txtSimDescription
            // 
            this.txtSimDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSimDescription.Location = new System.Drawing.Point(96, 62);
            this.txtSimDescription.Multiline = true;
            this.txtSimDescription.Name = "txtSimDescription";
            this.txtSimDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSimDescription.Size = new System.Drawing.Size(1098, 112);
            this.txtSimDescription.TabIndex = 2;
            // 
            // txtSimName
            // 
            this.txtSimName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSimName.Location = new System.Drawing.Point(96, 35);
            this.txtSimName.Name = "txtSimName";
            this.txtSimName.Size = new System.Drawing.Size(1098, 20);
            this.txtSimName.TabIndex = 1;
            // 
            // txtSimId
            // 
            this.txtSimId.Enabled = false;
            this.txtSimId.Location = new System.Drawing.Point(96, 9);
            this.txtSimId.Name = "txtSimId";
            this.txtSimId.Size = new System.Drawing.Size(67, 20);
            this.txtSimId.TabIndex = 0;
            this.txtSimId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Simulator Id:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Simulator Name:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNew,
            this.btEdit,
            this.btDelete,
            this.btConfirm,
            this.btCancel,
            this.btRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 786);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1205, 25);
            this.toolStrip1.TabIndex = 0;
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
            // FrmSimulatorsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1205, 811);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmSimulatorsManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulators Management";
            this.Load += new System.EventHandler(this.FrmSimulatorsManagement_Load);
            this.DockStateChanged += new System.EventHandler(this.FrmSimulatorsManagement_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSimulatorsManagement_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridMsgToSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMsgToReceive)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulators)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMsgToSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridMsgToReceive;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridSimulators;
        private System.Windows.Forms.TextBox txtSimName;
        private System.Windows.Forms.TextBox txtSimId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSimDescription;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btEdit;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripButton btConfirm;
        private System.Windows.Forms.ToolStripButton btCancel;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeFollowing;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMsg;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheckFullMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIntervalToAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponseMsg;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colRepeatAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepetitionInterval;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSimName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSimDesc;
    }
}


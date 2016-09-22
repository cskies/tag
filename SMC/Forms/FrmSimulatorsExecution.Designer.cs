namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSimulatorsExecution
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
            this.gridSimulators = new System.Windows.Forms.DataGridView();
            this.btNewSimulator = new System.Windows.Forms.Button();
            this.gridSimulatorCommConfig = new System.Windows.Forms.DataGridView();
            this.btFinalizeConfig = new System.Windows.Forms.Button();
            this.gridSimConfigured = new System.Windows.Forms.DataGridView();
            this.btStartAllSimulators = new System.Windows.Forms.Button();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.colSimId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSimName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExecution = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStopTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastReceivedMsgTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastReceivedMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastMessageSentTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastMessageSent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClear = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulatorCommConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimConfigured)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSimulators
            // 
            this.gridSimulators.AllowUserToAddRows = false;
            this.gridSimulators.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridSimulators.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSimulators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSimulators.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridSimulators.Location = new System.Drawing.Point(5, 34);
            this.gridSimulators.Name = "gridSimulators";
            this.gridSimulators.RowHeadersVisible = false;
            this.gridSimulators.Size = new System.Drawing.Size(489, 133);
            this.gridSimulators.TabIndex = 16;
            this.gridSimulators.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridSimulators_CellValidating);
            // 
            // btNewSimulator
            // 
            this.btNewSimulator.Location = new System.Drawing.Point(6, 5);
            this.btNewSimulator.Name = "btNewSimulator";
            this.btNewSimulator.Size = new System.Drawing.Size(221, 23);
            this.btNewSimulator.TabIndex = 26;
            this.btNewSimulator.Text = "Set Communication Settings";
            this.btNewSimulator.UseVisualStyleBackColor = true;
            this.btNewSimulator.Click += new System.EventHandler(this.btNewSimulator_Click);
            // 
            // gridSimulatorCommConfig
            // 
            this.gridSimulatorCommConfig.AllowUserToAddRows = false;
            this.gridSimulatorCommConfig.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridSimulatorCommConfig.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridSimulatorCommConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSimulatorCommConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSimulatorCommConfig.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridSimulatorCommConfig.Location = new System.Drawing.Point(500, 34);
            this.gridSimulatorCommConfig.Name = "gridSimulatorCommConfig";
            this.gridSimulatorCommConfig.RowHeadersVisible = false;
            this.gridSimulatorCommConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridSimulatorCommConfig.Size = new System.Drawing.Size(710, 133);
            this.gridSimulatorCommConfig.TabIndex = 27;
            // 
            // btFinalizeConfig
            // 
            this.btFinalizeConfig.Enabled = false;
            this.btFinalizeConfig.Location = new System.Drawing.Point(500, 5);
            this.btFinalizeConfig.Name = "btFinalizeConfig";
            this.btFinalizeConfig.Size = new System.Drawing.Size(221, 23);
            this.btFinalizeConfig.TabIndex = 28;
            this.btFinalizeConfig.Text = "Apply Settings";
            this.btFinalizeConfig.UseVisualStyleBackColor = true;
            this.btFinalizeConfig.Click += new System.EventHandler(this.btFinalizeConfig_Click);
            // 
            // gridSimConfigured
            // 
            this.gridSimConfigured.AllowUserToAddRows = false;
            this.gridSimConfigured.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender;
            this.gridSimConfigured.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridSimConfigured.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSimConfigured.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSimConfigured.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSimId,
            this.colSimName,
            this.colConnType,
            this.colExecution,
            this.colStartTime,
            this.colStopTime,
            this.colLastReceivedMsgTime,
            this.colLastReceivedMessage,
            this.colLastMessageSentTime,
            this.colLastMessageSent,
            this.colClear});
            this.gridSimConfigured.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridSimConfigured.Location = new System.Drawing.Point(5, 202);
            this.gridSimConfigured.Name = "gridSimConfigured";
            this.gridSimConfigured.RowHeadersVisible = false;
            this.gridSimConfigured.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridSimConfigured.Size = new System.Drawing.Size(1205, 470);
            this.gridSimConfigured.TabIndex = 29;
            this.gridSimConfigured.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSimConfigured_CellContentClick);
            // 
            // btStartAllSimulators
            // 
            this.btStartAllSimulators.Enabled = false;
            this.btStartAllSimulators.Location = new System.Drawing.Point(6, 173);
            this.btStartAllSimulators.Name = "btStartAllSimulators";
            this.btStartAllSimulators.Size = new System.Drawing.Size(221, 23);
            this.btStartAllSimulators.TabIndex = 30;
            this.btStartAllSimulators.Text = "Start All Stopped Simulators";
            this.btStartAllSimulators.UseVisualStyleBackColor = true;
            this.btStartAllSimulators.Click += new System.EventHandler(this.btStartAllSimulators_Click);
            // 
            // colSimId
            // 
            this.colSimId.HeaderText = "Simulator Id";
            this.colSimId.Name = "colSimId";
            this.colSimId.ReadOnly = true;
            // 
            // colSimName
            // 
            this.colSimName.HeaderText = "Simulator";
            this.colSimName.Name = "colSimName";
            this.colSimName.ReadOnly = true;
            this.colSimName.Width = 150;
            // 
            // colConnType
            // 
            this.colConnType.HeaderText = "Communication Settings";
            this.colConnType.Name = "colConnType";
            this.colConnType.ReadOnly = true;
            this.colConnType.Width = 200;
            // 
            // colExecution
            // 
            this.colExecution.HeaderText = "Execution";
            this.colExecution.Name = "colExecution";
            this.colExecution.ReadOnly = true;
            // 
            // colStartTime
            // 
            this.colStartTime.HeaderText = "Start Time";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.ReadOnly = true;
            this.colStartTime.Width = 150;
            // 
            // colStopTime
            // 
            this.colStopTime.HeaderText = "Stop Time";
            this.colStopTime.Name = "colStopTime";
            this.colStopTime.ReadOnly = true;
            this.colStopTime.Width = 150;
            // 
            // colLastReceivedMsgTime
            // 
            this.colLastReceivedMsgTime.HeaderText = "Last Received Message Time";
            this.colLastReceivedMsgTime.Name = "colLastReceivedMsgTime";
            this.colLastReceivedMsgTime.ReadOnly = true;
            this.colLastReceivedMsgTime.Width = 170;
            // 
            // colLastReceivedMessage
            // 
            this.colLastReceivedMessage.HeaderText = "Last Received Message";
            this.colLastReceivedMessage.Name = "colLastReceivedMessage";
            this.colLastReceivedMessage.ReadOnly = true;
            this.colLastReceivedMessage.Width = 400;
            // 
            // colLastMessageSentTime
            // 
            this.colLastMessageSentTime.HeaderText = "Last Message Sent Time";
            this.colLastMessageSentTime.Name = "colLastMessageSentTime";
            this.colLastMessageSentTime.ReadOnly = true;
            this.colLastMessageSentTime.Width = 170;
            // 
            // colLastMessageSent
            // 
            this.colLastMessageSent.HeaderText = "Last Message Sent";
            this.colLastMessageSent.Name = "colLastMessageSent";
            this.colLastMessageSent.ReadOnly = true;
            this.colLastMessageSent.Width = 400;
            // 
            // colClear
            // 
            this.colClear.HeaderText = "Clear Settings";
            this.colClear.Name = "colClear";
            // 
            // FrmSimulatorsExecution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1216, 676);
            this.Controls.Add(this.btStartAllSimulators);
            this.Controls.Add(this.gridSimConfigured);
            this.Controls.Add(this.btFinalizeConfig);
            this.Controls.Add(this.gridSimulatorCommConfig);
            this.Controls.Add(this.btNewSimulator);
            this.Controls.Add(this.gridSimulators);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSimulatorsExecution";
            this.Text = "Simulators Execution";
            this.DockStateChanged += new System.EventHandler(this.FrmSimulatorsExecution_DockStateChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulatorCommConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimConfigured)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSimulators;
        private System.Windows.Forms.Button btNewSimulator;
        private System.Windows.Forms.DataGridView gridSimulatorCommConfig;
        private System.Windows.Forms.Button btFinalizeConfig;
        private System.Windows.Forms.DataGridView gridSimConfigured;
        private System.Windows.Forms.Button btStartAllSimulators;
        private System.IO.Ports.SerialPort serial;
        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSimId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSimName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConnType;
        private System.Windows.Forms.DataGridViewButtonColumn colExecution;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStopTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastReceivedMsgTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastReceivedMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastMessageSentTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastMessageSent;
        private System.Windows.Forms.DataGridViewButtonColumn colClear;

    }
}
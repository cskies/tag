namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSimCortex
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
            this.gridLastPackage = new System.Windows.Forms.DataGridView();
            this.TcpPackages = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessageTcpIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RS422Packages = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessageRs422 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btConnect = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.rdClient = new System.Windows.Forms.RadioButton();
            this.rdServer = new System.Windows.Forms.RadioButton();
            this.gridStatus = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridLastPackage)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLastPackage
            // 
            this.gridLastPackage.AllowUserToAddRows = false;
            this.gridLastPackage.AllowUserToDeleteRows = false;
            this.gridLastPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLastPackage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLastPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLastPackage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TcpPackages,
            this.colMessageTcpIp,
            this.RS422Packages,
            this.colMessageRs422});
            this.gridLastPackage.Location = new System.Drawing.Point(6, 154);
            this.gridLastPackage.MultiSelect = false;
            this.gridLastPackage.Name = "gridLastPackage";
            this.gridLastPackage.ReadOnly = true;
            this.gridLastPackage.RowHeadersVisible = false;
            this.gridLastPackage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLastPackage.Size = new System.Drawing.Size(1274, 528);
            this.gridLastPackage.TabIndex = 4;
            // 
            // TcpPackages
            // 
            this.TcpPackages.FillWeight = 52.41991F;
            this.TcpPackages.HeaderText = "TCP/IP Network";
            this.TcpPackages.Name = "TcpPackages";
            this.TcpPackages.ReadOnly = true;
            // 
            // colMessageTcpIp
            // 
            this.colMessageTcpIp.FillWeight = 121.8274F;
            this.colMessageTcpIp.HeaderText = "Message";
            this.colMessageTcpIp.Name = "colMessageTcpIp";
            this.colMessageTcpIp.ReadOnly = true;
            // 
            // RS422Packages
            // 
            this.RS422Packages.FillWeight = 51.65065F;
            this.RS422Packages.HeaderText = "RS-422 Serial";
            this.RS422Packages.Name = "RS422Packages";
            this.RS422Packages.ReadOnly = true;
            // 
            // colMessageRs422
            // 
            this.colMessageRs422.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMessageRs422.FillWeight = 174.102F;
            this.colMessageRs422.HeaderText = "Message";
            this.colMessageRs422.Name = "colMessageRs422";
            this.colMessageRs422.ReadOnly = true;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(471, 13);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(165, 77);
            this.btConnect.TabIndex = 33;
            this.btConnect.Text = "Start \r\nCortex Simulator";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label45);
            this.groupBox7.Controls.Add(this.label42);
            this.groupBox7.Controls.Add(this.txtPort);
            this.groupBox7.Controls.Add(this.txtIp);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.rdClient);
            this.groupBox7.Controls.Add(this.rdServer);
            this.groupBox7.Location = new System.Drawing.Point(6, 8);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(459, 82);
            this.groupBox7.TabIndex = 26;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tcp/Ip configuration";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(14, 25);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(58, 13);
            this.label45.TabIndex = 36;
            this.label45.Text = "This is the:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(338, 49);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(29, 13);
            this.label42.TabIndex = 35;
            this.label42.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(373, 46);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(80, 20);
            this.txtPort.TabIndex = 32;
            this.txtPort.Text = "8874";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(77, 46);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(255, 20);
            this.txtIp.TabIndex = 31;
            this.txtIp.Text = "localhost";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(13, 49);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(55, 13);
            this.label43.TabIndex = 34;
            this.label43.Text = "IP Adress:";
            // 
            // rdClient
            // 
            this.rdClient.AutoSize = true;
            this.rdClient.Location = new System.Drawing.Point(145, 23);
            this.rdClient.Name = "rdClient";
            this.rdClient.Size = new System.Drawing.Size(51, 17);
            this.rdClient.TabIndex = 0;
            this.rdClient.Text = "Client";
            this.rdClient.UseVisualStyleBackColor = true;
            // 
            // rdServer
            // 
            this.rdServer.AutoSize = true;
            this.rdServer.Checked = true;
            this.rdServer.Location = new System.Drawing.Point(83, 23);
            this.rdServer.Name = "rdServer";
            this.rdServer.Size = new System.Drawing.Size(56, 17);
            this.rdServer.TabIndex = 1;
            this.rdServer.TabStop = true;
            this.rdServer.Text = "Server";
            this.rdServer.UseVisualStyleBackColor = true;
            // 
            // gridStatus
            // 
            this.gridStatus.AllowUserToAddRows = false;
            this.gridStatus.AllowUserToDeleteRows = false;
            this.gridStatus.AllowUserToResizeColumns = false;
            this.gridStatus.AllowUserToResizeRows = false;
            this.gridStatus.BackgroundColor = System.Drawing.Color.White;
            this.gridStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStatus.ColumnHeadersVisible = false;
            this.gridStatus.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridStatus.Location = new System.Drawing.Point(6, 96);
            this.gridStatus.Name = "gridStatus";
            this.gridStatus.RowHeadersVisible = false;
            this.gridStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStatus.Size = new System.Drawing.Size(630, 52);
            this.gridStatus.TabIndex = 55;
            // 
            // FrmSimCortex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 687);
            this.Controls.Add(this.gridLastPackage);
            this.Controls.Add(this.gridStatus);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.btConnect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSimCortex";
            this.Text = "Cortex Simulator";
            this.Load += new System.EventHandler(this.FrmSimCortex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridLastPackage)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridLastPackage;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.RadioButton rdClient;
        private System.Windows.Forms.RadioButton rdServer;
        private System.Windows.Forms.DataGridView gridStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn TcpPackages;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageTcpIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn RS422Packages;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageRs422;
    }
}


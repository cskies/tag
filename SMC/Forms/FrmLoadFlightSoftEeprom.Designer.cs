namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmLoadFlightSoftEeprom
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
            this.rdExFile = new System.Windows.Forms.RadioButton();
            this.rdSrecFile = new System.Windows.Forms.RadioButton();
            this.gpBoxFileFormat = new System.Windows.Forms.GroupBox();
            this.txtImageLength = new System.Windows.Forms.TextBox();
            this.txtImageCrc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btFileExFile = new System.Windows.Forms.Button();
            this.txtExFile = new System.Windows.Forms.TextBox();
            this.btSendSoft = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.btExportBytes = new System.Windows.Forms.Button();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.gpBoxFileFormat.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdExFile
            // 
            this.rdExFile.AutoSize = true;
            this.rdExFile.Location = new System.Drawing.Point(12, 20);
            this.rdExFile.Name = "rdExFile";
            this.rdExFile.Size = new System.Drawing.Size(100, 17);
            this.rdExFile.TabIndex = 0;
            this.rdExFile.TabStop = true;
            this.rdExFile.Text = "Executable File:";
            this.rdExFile.UseVisualStyleBackColor = true;
            // 
            // rdSrecFile
            // 
            this.rdSrecFile.AutoSize = true;
            this.rdSrecFile.Location = new System.Drawing.Point(195, 19);
            this.rdSrecFile.Name = "rdSrecFile";
            this.rdSrecFile.Size = new System.Drawing.Size(76, 17);
            this.rdSrecFile.TabIndex = 1;
            this.rdSrecFile.TabStop = true;
            this.rdSrecFile.Text = "SREC File:";
            this.rdSrecFile.UseVisualStyleBackColor = true;
            // 
            // gpBoxFileFormat
            // 
            this.gpBoxFileFormat.Controls.Add(this.txtImageLength);
            this.gpBoxFileFormat.Controls.Add(this.txtImageCrc);
            this.gpBoxFileFormat.Controls.Add(this.label9);
            this.gpBoxFileFormat.Controls.Add(this.label8);
            this.gpBoxFileFormat.Controls.Add(this.btFileExFile);
            this.gpBoxFileFormat.Controls.Add(this.txtExFile);
            this.gpBoxFileFormat.Controls.Add(this.rdExFile);
            this.gpBoxFileFormat.Controls.Add(this.rdSrecFile);
            this.gpBoxFileFormat.Location = new System.Drawing.Point(12, 121);
            this.gpBoxFileFormat.Name = "gpBoxFileFormat";
            this.gpBoxFileFormat.Size = new System.Drawing.Size(598, 146);
            this.gpBoxFileFormat.TabIndex = 2;
            this.gpBoxFileFormat.TabStop = false;
            this.gpBoxFileFormat.Text = "File Format";
            // 
            // txtImageLength
            // 
            this.txtImageLength.BackColor = System.Drawing.Color.White;
            this.txtImageLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageLength.ForeColor = System.Drawing.Color.ForestGreen;
            this.txtImageLength.Location = new System.Drawing.Point(455, 84);
            this.txtImageLength.Name = "txtImageLength";
            this.txtImageLength.ReadOnly = true;
            this.txtImageLength.Size = new System.Drawing.Size(103, 20);
            this.txtImageLength.TabIndex = 68;
            this.txtImageLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtImageCrc
            // 
            this.txtImageCrc.BackColor = System.Drawing.Color.White;
            this.txtImageCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageCrc.ForeColor = System.Drawing.Color.Red;
            this.txtImageCrc.Location = new System.Drawing.Point(455, 113);
            this.txtImageCrc.Name = "txtImageCrc";
            this.txtImageCrc.ReadOnly = true;
            this.txtImageCrc.Size = new System.Drawing.Size(103, 20);
            this.txtImageCrc.TabIndex = 69;
            this.txtImageCrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(357, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 71;
            this.label9.Text = "Image Checksum:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(357, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "Image Length:";
            // 
            // btFileExFile
            // 
            this.btFileExFile.Location = new System.Drawing.Point(564, 48);
            this.btFileExFile.Name = "btFileExFile";
            this.btFileExFile.Size = new System.Drawing.Size(26, 23);
            this.btFileExFile.TabIndex = 4;
            this.btFileExFile.Text = "...";
            this.btFileExFile.UseVisualStyleBackColor = true;
            this.btFileExFile.Click += new System.EventHandler(this.btFileExFile_Click);
            // 
            // txtExFile
            // 
            this.txtExFile.BackColor = System.Drawing.Color.White;
            this.txtExFile.Location = new System.Drawing.Point(6, 50);
            this.txtExFile.Name = "txtExFile";
            this.txtExFile.ReadOnly = true;
            this.txtExFile.Size = new System.Drawing.Size(552, 20);
            this.txtExFile.TabIndex = 2;
            // 
            // btSendSoft
            // 
            this.btSendSoft.Enabled = false;
            this.btSendSoft.Location = new System.Drawing.Point(400, 65);
            this.btSendSoft.Name = "btSendSoft";
            this.btSendSoft.Size = new System.Drawing.Size(202, 50);
            this.btSendSoft.TabIndex = 3;
            this.btSendSoft.Text = "Send Flight Software to COMAV";
            this.btSendSoft.UseVisualStyleBackColor = true;
            this.btSendSoft.Click += new System.EventHandler(this.btSendSoft_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStopBits);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.cmbDataBits);
            this.groupBox1.Controls.Add(this.cmbBaudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbComPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RS-232 Serial port configuration";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbStopBits.Location = new System.Drawing.Point(254, 72);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(120, 21);
            this.cmbStopBits.TabIndex = 4;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None"});
            this.cmbParity.Location = new System.Drawing.Point(254, 45);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(120, 21);
            this.cmbParity.TabIndex = 3;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cmbDataBits.Location = new System.Drawing.Point(254, 18);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(120, 21);
            this.cmbDataBits.TabIndex = 2;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(71, 45);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(105, 21);
            this.cmbBaudRate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Stop bits:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Parity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data bits:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baud rate:";
            // 
            // cmbComPort
            // 
            this.cmbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(71, 18);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(105, 21);
            this.cmbComPort.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM port:";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(400, 12);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(202, 23);
            this.btSave.TabIndex = 13;
            this.btSave.Text = "Save Configuration";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // btExportBytes
            // 
            this.btExportBytes.Enabled = false;
            this.btExportBytes.Location = new System.Drawing.Point(400, 38);
            this.btExportBytes.Name = "btExportBytes";
            this.btExportBytes.Size = new System.Drawing.Size(202, 23);
            this.btExportBytes.TabIndex = 72;
            this.btExportBytes.Text = "Export Bytes";
            this.btExportBytes.UseVisualStyleBackColor = true;
            this.btExportBytes.Click += new System.EventHandler(this.btExportBytes_Click);
            // 
            // FrmLoadFlightSoftEeprom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(618, 277);
            this.Controls.Add(this.btExportBytes);
            this.Controls.Add(this.btSendSoft);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpBoxFileFormat);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmLoadFlightSoftEeprom";
            this.Text = "Load Fight Software to EEPROM (COMAV prototype)";
            this.Load += new System.EventHandler(this.FrmLoadFlightSoftEeprom_Load);
            this.gpBoxFileFormat.ResumeLayout(false);
            this.gpBoxFileFormat.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdExFile;
        private System.Windows.Forms.RadioButton rdSrecFile;
        private System.Windows.Forms.GroupBox gpBoxFileFormat;
        private System.Windows.Forms.Button btFileExFile;
        private System.Windows.Forms.TextBox txtExFile;
        private System.Windows.Forms.Button btSendSoft;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox txtImageLength;
        private System.Windows.Forms.TextBox txtImageCrc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.Button btExportBytes;
        private System.Windows.Forms.SaveFileDialog saveDialog;
    }
}
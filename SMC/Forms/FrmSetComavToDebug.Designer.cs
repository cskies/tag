namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSetComavToDebug
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
            this.btSetDebug = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWordToReceive = new System.Windows.Forms.TextBox();
            this.txtWordToAnswer = new System.Windows.Forms.TextBox();
            this.txtBootLoaderMessage = new System.Windows.Forms.TextBox();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.btClearScreen = new System.Windows.Forms.Button();
            this.btExpScreen = new System.Windows.Forms.Button();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 103);
            this.groupBox1.TabIndex = 2;
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
            this.btSave.Location = new System.Drawing.Point(400, 67);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(202, 23);
            this.btSave.TabIndex = 12;
            this.btSave.Text = "Save Configuration";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btSetDebug
            // 
            this.btSetDebug.Location = new System.Drawing.Point(400, 92);
            this.btSetDebug.Name = "btSetDebug";
            this.btSetDebug.Size = new System.Drawing.Size(202, 56);
            this.btSetDebug.TabIndex = 13;
            this.btSetDebug.Text = "Set to Debug Mode";
            this.btSetDebug.UseVisualStyleBackColor = true;
            this.btSetDebug.Click += new System.EventHandler(this.btSetDebug_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Word to Receive:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Word to Answer:";
            // 
            // txtWordToReceive
            // 
            this.txtWordToReceive.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWordToReceive.Location = new System.Drawing.Point(107, 122);
            this.txtWordToReceive.MaxLength = 4;
            this.txtWordToReceive.Name = "txtWordToReceive";
            this.txtWordToReceive.Size = new System.Drawing.Size(86, 20);
            this.txtWordToReceive.TabIndex = 16;
            this.txtWordToReceive.Validating += new System.ComponentModel.CancelEventHandler(this.txtWordToReceive_Validating);
            // 
            // txtWordToAnswer
            // 
            this.txtWordToAnswer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWordToAnswer.Location = new System.Drawing.Point(297, 122);
            this.txtWordToAnswer.MaxLength = 4;
            this.txtWordToAnswer.Name = "txtWordToAnswer";
            this.txtWordToAnswer.Size = new System.Drawing.Size(86, 20);
            this.txtWordToAnswer.TabIndex = 17;
            this.txtWordToAnswer.Validating += new System.ComponentModel.CancelEventHandler(this.txtWordToAnswer_Validating);
            // 
            // txtBootLoaderMessage
            // 
            this.txtBootLoaderMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBootLoaderMessage.BackColor = System.Drawing.Color.Black;
            this.txtBootLoaderMessage.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBootLoaderMessage.ForeColor = System.Drawing.Color.White;
            this.txtBootLoaderMessage.Location = new System.Drawing.Point(9, 150);
            this.txtBootLoaderMessage.Multiline = true;
            this.txtBootLoaderMessage.Name = "txtBootLoaderMessage";
            this.txtBootLoaderMessage.ReadOnly = true;
            this.txtBootLoaderMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBootLoaderMessage.Size = new System.Drawing.Size(593, 276);
            this.txtBootLoaderMessage.TabIndex = 18;
            // 
            // btClearScreen
            // 
            this.btClearScreen.Location = new System.Drawing.Point(400, 15);
            this.btClearScreen.Name = "btClearScreen";
            this.btClearScreen.Size = new System.Drawing.Size(202, 23);
            this.btClearScreen.TabIndex = 19;
            this.btClearScreen.Text = "Clear Screen";
            this.btClearScreen.UseVisualStyleBackColor = true;
            this.btClearScreen.Click += new System.EventHandler(this.btClearScreen_Click);
            // 
            // btExpScreen
            // 
            this.btExpScreen.Location = new System.Drawing.Point(400, 41);
            this.btExpScreen.Name = "btExpScreen";
            this.btExpScreen.Size = new System.Drawing.Size(202, 23);
            this.btExpScreen.TabIndex = 20;
            this.btExpScreen.Text = "Export Screen to File";
            this.btExpScreen.UseVisualStyleBackColor = true;
            this.btExpScreen.Click += new System.EventHandler(this.btExpScreen_Click);
            // 
            // FrmSetComavToDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(609, 438);
            this.Controls.Add(this.btExpScreen);
            this.Controls.Add(this.btClearScreen);
            this.Controls.Add(this.txtBootLoaderMessage);
            this.Controls.Add(this.txtWordToAnswer);
            this.Controls.Add(this.txtWordToReceive);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btSetDebug);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSetComavToDebug";
            this.Text = "Set COMAV to Debug Mode";
            this.Load += new System.EventHandler(this.FrmSetComavToDebug_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSetComavToDebug_FormClosed);
            this.DockStateChanged += new System.EventHandler(this.FrmSetComavToDebug_DockStateChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button btSetDebug;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtWordToReceive;
        private System.Windows.Forms.TextBox txtWordToAnswer;
        private System.Windows.Forms.TextBox txtBootLoaderMessage;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.Button btClearScreen;
        private System.Windows.Forms.Button btExpScreen;
        private System.Windows.Forms.SaveFileDialog saveDialog;
    }
}
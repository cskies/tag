namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmCommRS422
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCommRS422));
            this.btSaveInFile = new System.Windows.Forms.Button();
            this.txtSaveInFilePath = new System.Windows.Forms.TextBox();
            this.btSendData = new System.Windows.Forms.Button();
            this.txtDataSendPath = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.ckbBreakLine = new System.Windows.Forms.CheckBox();
            this.rbRecordBinaryRepresentation = new System.Windows.Forms.RadioButton();
            this.rbRecordHexRepresentation = new System.Windows.Forms.RadioButton();
            this.chkRecordInFile = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.btClear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btEnableReception = new System.Windows.Forms.Button();
            this.newFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.chkDataSendFile = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowFramesReception = new System.Windows.Forms.CheckBox();
            this.rbBinary = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDataSended = new System.Windows.Forms.Label();
            this.lblFrameToBeSent = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSaveInFile
            // 
            this.btSaveInFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveInFile.Enabled = false;
            this.btSaveInFile.Location = new System.Drawing.Point(669, 495);
            this.btSaveInFile.Name = "btSaveInFile";
            this.btSaveInFile.Size = new System.Drawing.Size(29, 23);
            this.btSaveInFile.TabIndex = 10;
            this.btSaveInFile.Text = "...";
            this.btSaveInFile.UseVisualStyleBackColor = true;
            this.btSaveInFile.Click += new System.EventHandler(this.btSaveInFile_Click);
            // 
            // txtSaveInFilePath
            // 
            this.txtSaveInFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaveInFilePath.Enabled = false;
            this.txtSaveInFilePath.Location = new System.Drawing.Point(119, 497);
            this.txtSaveInFilePath.Name = "txtSaveInFilePath";
            this.txtSaveInFilePath.ReadOnly = true;
            this.txtSaveInFilePath.Size = new System.Drawing.Size(543, 20);
            this.txtSaveInFilePath.TabIndex = 9;
            // 
            // btSendData
            // 
            this.btSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSendData.Enabled = false;
            this.btSendData.Location = new System.Drawing.Point(668, 546);
            this.btSendData.Name = "btSendData";
            this.btSendData.Size = new System.Drawing.Size(30, 23);
            this.btSendData.TabIndex = 8;
            this.btSendData.Text = "...";
            this.btSendData.UseVisualStyleBackColor = true;
            this.btSendData.Click += new System.EventHandler(this.btSendData_Click);
            // 
            // txtDataSendPath
            // 
            this.txtDataSendPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataSendPath.Enabled = false;
            this.txtDataSendPath.Location = new System.Drawing.Point(119, 548);
            this.txtDataSendPath.Name = "txtDataSendPath";
            this.txtDataSendPath.ReadOnly = true;
            this.txtDataSendPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDataSendPath.Size = new System.Drawing.Size(543, 20);
            this.txtDataSendPath.TabIndex = 54;
            // 
            // btSend
            // 
            this.btSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSend.Enabled = false;
            this.btSend.Location = new System.Drawing.Point(876, 536);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(97, 35);
            this.btSend.TabIndex = 6;
            this.btSend.Text = "Send Data";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // ckbBreakLine
            // 
            this.ckbBreakLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbBreakLine.AutoSize = true;
            this.ckbBreakLine.Location = new System.Drawing.Point(714, 515);
            this.ckbBreakLine.Name = "ckbBreakLine";
            this.ckbBreakLine.Size = new System.Drawing.Size(123, 17);
            this.ckbBreakLine.TabIndex = 29;
            this.ckbBreakLine.Text = "Break line on display";
            this.ckbBreakLine.UseVisualStyleBackColor = true;
            // 
            // rbRecordBinaryRepresentation
            // 
            this.rbRecordBinaryRepresentation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbRecordBinaryRepresentation.AutoSize = true;
            this.rbRecordBinaryRepresentation.Checked = true;
            this.rbRecordBinaryRepresentation.Enabled = false;
            this.rbRecordBinaryRepresentation.Location = new System.Drawing.Point(123, 523);
            this.rbRecordBinaryRepresentation.Name = "rbRecordBinaryRepresentation";
            this.rbRecordBinaryRepresentation.Size = new System.Drawing.Size(213, 17);
            this.rbRecordBinaryRepresentation.TabIndex = 32;
            this.rbRecordBinaryRepresentation.TabStop = true;
            this.rbRecordBinaryRepresentation.Text = "Record with ASCII binary representation";
            this.rbRecordBinaryRepresentation.UseVisualStyleBackColor = true;
            // 
            // rbRecordHexRepresentation
            // 
            this.rbRecordHexRepresentation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbRecordHexRepresentation.AutoSize = true;
            this.rbRecordHexRepresentation.Enabled = false;
            this.rbRecordHexRepresentation.Location = new System.Drawing.Point(342, 523);
            this.rbRecordHexRepresentation.Name = "rbRecordHexRepresentation";
            this.rbRecordHexRepresentation.Size = new System.Drawing.Size(202, 17);
            this.rbRecordHexRepresentation.TabIndex = 31;
            this.rbRecordHexRepresentation.Text = "Record with ASCII hex representation";
            this.rbRecordHexRepresentation.UseVisualStyleBackColor = true;
            // 
            // chkRecordInFile
            // 
            this.chkRecordInFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRecordInFile.Location = new System.Drawing.Point(13, 500);
            this.chkRecordInFile.Name = "chkRecordInFile";
            this.chkRecordInFile.Size = new System.Drawing.Size(109, 15);
            this.chkRecordInFile.TabIndex = 10;
            this.chkRecordInFile.Text = "Save data in:";
            this.chkRecordInFile.UseVisualStyleBackColor = true;
            this.chkRecordInFile.CheckedChanged += new System.EventHandler(this.chkRecordInFile_CheckedChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtReceive);
            this.groupBox3.Location = new System.Drawing.Point(4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1078, 387);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data received";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(6, 336);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(1066, 45);
            this.label5.TabIndex = 6;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // txtReceive
            // 
            this.txtReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceive.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtReceive.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceive.Location = new System.Drawing.Point(6, 19);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(1066, 314);
            this.txtReceive.TabIndex = 5;
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.Location = new System.Drawing.Point(979, 497);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(98, 35);
            this.btClear.TabIndex = 13;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btEnableReception
            // 
            this.btEnableReception.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEnableReception.Enabled = false;
            this.btEnableReception.Location = new System.Drawing.Point(876, 497);
            this.btEnableReception.Name = "btEnableReception";
            this.btEnableReception.Size = new System.Drawing.Size(97, 35);
            this.btEnableReception.TabIndex = 14;
            this.btEnableReception.Text = "Get Data";
            this.btEnableReception.UseVisualStyleBackColor = true;
            this.btEnableReception.Click += new System.EventHandler(this.btEnableReception_Click_1);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(876, 474);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Communication";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(4, 474);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(695, 16);
            this.label11.TabIndex = 33;
            this.label11.Text = "Communication Files Path";
            // 
            // chkDataSendFile
            // 
            this.chkDataSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDataSendFile.AutoSize = true;
            this.chkDataSendFile.Location = new System.Drawing.Point(13, 550);
            this.chkDataSendFile.Name = "chkDataSendFile";
            this.chkDataSendFile.Size = new System.Drawing.Size(101, 17);
            this.chkDataSendFile.TabIndex = 34;
            this.chkDataSendFile.Text = "Send data from:";
            this.chkDataSendFile.UseVisualStyleBackColor = true;
            this.chkDataSendFile.CheckedChanged += new System.EventHandler(this.chkDataSendFile_CheckedChanged_1);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(705, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Frame to be sent";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(705, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 16);
            this.label1.TabIndex = 58;
            this.label1.Text = "Reception Options";
            // 
            // chkShowFramesReception
            // 
            this.chkShowFramesReception.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowFramesReception.AutoSize = true;
            this.chkShowFramesReception.Location = new System.Drawing.Point(714, 492);
            this.chkShowFramesReception.Name = "chkShowFramesReception";
            this.chkShowFramesReception.Size = new System.Drawing.Size(121, 17);
            this.chkShowFramesReception.TabIndex = 61;
            this.chkShowFramesReception.Text = "Show data received";
            this.chkShowFramesReception.UseVisualStyleBackColor = true;
            // 
            // rbBinary
            // 
            this.rbBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbBinary.AutoSize = true;
            this.rbBinary.Enabled = false;
            this.rbBinary.Location = new System.Drawing.Point(550, 523);
            this.rbBinary.Name = "rbBinary";
            this.rbBinary.Size = new System.Drawing.Size(105, 17);
            this.rbBinary.TabIndex = 62;
            this.rbBinary.TabStop = true;
            this.rbBinary.Text = "Record as binary";
            this.rbBinary.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblDataSended);
            this.groupBox1.Location = new System.Drawing.Point(4, 398);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1078, 67);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Last message sent";
            // 
            // lblDataSended
            // 
            this.lblDataSended.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataSended.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.lblDataSended.Location = new System.Drawing.Point(3, 16);
            this.lblDataSended.Name = "lblDataSended";
            this.lblDataSended.Size = new System.Drawing.Size(1072, 48);
            this.lblDataSended.TabIndex = 0;
            // 
            // lblFrameToBeSent
            // 
            this.lblFrameToBeSent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrameToBeSent.AutoSize = true;
            this.lblFrameToBeSent.Enabled = false;
            this.lblFrameToBeSent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrameToBeSent.Location = new System.Drawing.Point(754, 556);
            this.lblFrameToBeSent.Name = "lblFrameToBeSent";
            this.lblFrameToBeSent.Size = new System.Drawing.Size(44, 15);
            this.lblFrameToBeSent.TabIndex = 64;
            this.lblFrameToBeSent.Text = ":: 1º ::";
            // 
            // FrmCommRS422
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1093, 581);
            this.Controls.Add(this.lblFrameToBeSent);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbBinary);
            this.Controls.Add(this.rbRecordBinaryRepresentation);
            this.Controls.Add(this.chkShowFramesReception);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckbBreakLine);
            this.Controls.Add(this.chkDataSendFile);
            this.Controls.Add(this.txtDataSendPath);
            this.Controls.Add(this.txtSaveInFilePath);
            this.Controls.Add(this.btSendData);
            this.Controls.Add(this.chkRecordInFile);
            this.Controls.Add(this.btSaveInFile);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rbRecordHexRepresentation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btEnableReception);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmCommRS422";
            this.Text = "RS-422 Communication";
            this.Load += new System.EventHandler(this.FrmCommRS422_Load_1);
            this.DockStateChanged += new System.EventHandler(this.FrmCommRS422_DockStateChanged);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSaveInFile;
        private System.Windows.Forms.TextBox txtSaveInFilePath;
        private System.Windows.Forms.Button btSendData;
        private System.Windows.Forms.TextBox txtDataSendPath;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkRecordInFile;
        public System.Windows.Forms.Button btEnableReception;
        private System.Windows.Forms.RadioButton rbRecordBinaryRepresentation;
        private System.Windows.Forms.RadioButton rbRecordHexRepresentation;
        private System.Windows.Forms.SaveFileDialog newFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckbBreakLine;
        public System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowFramesReception;
        private System.Windows.Forms.RadioButton rbBinary;
        public System.Windows.Forms.CheckBox chkDataSendFile;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblDataSended;
        public System.Windows.Forms.Label lblFrameToBeSent;

    }
}

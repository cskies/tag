namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmTimeConversion
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
            this.mskCalendarTime = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mskOnboardTime = new System.Windows.Forms.MaskedTextBox();
            this.btConvert = new System.Windows.Forms.Button();
            this.rbCalendarToOnboard = new System.Windows.Forms.RadioButton();
            this.rbOnboardToCalendar = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mskCalendarTime
            // 
            this.mskCalendarTime.AsciiOnly = true;
            this.mskCalendarTime.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskCalendarTime.Location = new System.Drawing.Point(149, 35);
            this.mskCalendarTime.Mask = "00/00/0000 90:00:00,000000";
            this.mskCalendarTime.Name = "mskCalendarTime";
            this.mskCalendarTime.PromptChar = ' ';
            this.mskCalendarTime.Size = new System.Drawing.Size(158, 20);
            this.mskCalendarTime.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Calendar Date and Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "CCSDS CUC Level 1 Time:";
            // 
            // mskOnboardTime
            // 
            this.mskOnboardTime.AsciiOnly = true;
            this.mskOnboardTime.Location = new System.Drawing.Point(163, 62);
            this.mskOnboardTime.Mask = ">AAAAAAAAAAAA";
            this.mskOnboardTime.Name = "mskOnboardTime";
            this.mskOnboardTime.PromptChar = ' ';
            this.mskOnboardTime.ReadOnly = true;
            this.mskOnboardTime.Size = new System.Drawing.Size(144, 20);
            this.mskOnboardTime.TabIndex = 3;
            // 
            // btConvert
            // 
            this.btConvert.Location = new System.Drawing.Point(225, 88);
            this.btConvert.Name = "btConvert";
            this.btConvert.Size = new System.Drawing.Size(82, 23);
            this.btConvert.TabIndex = 4;
            this.btConvert.Text = "Convert";
            this.btConvert.UseVisualStyleBackColor = true;
            this.btConvert.Click += new System.EventHandler(this.btConvert_Click);
            // 
            // rbCalendarToOnboard
            // 
            this.rbCalendarToOnboard.AutoSize = true;
            this.rbCalendarToOnboard.Checked = true;
            this.rbCalendarToOnboard.Location = new System.Drawing.Point(11, 12);
            this.rbCalendarToOnboard.Name = "rbCalendarToOnboard";
            this.rbCalendarToOnboard.Size = new System.Drawing.Size(146, 17);
            this.rbCalendarToOnboard.TabIndex = 0;
            this.rbCalendarToOnboard.TabStop = true;
            this.rbCalendarToOnboard.Text = "Calendar to on-board time";
            this.rbCalendarToOnboard.UseVisualStyleBackColor = true;
            this.rbCalendarToOnboard.CheckedChanged += new System.EventHandler(this.rbCalendarToOnboard_CheckedChanged);
            // 
            // rbOnboardToCalendar
            // 
            this.rbOnboardToCalendar.AutoSize = true;
            this.rbOnboardToCalendar.Location = new System.Drawing.Point(163, 12);
            this.rbOnboardToCalendar.Name = "rbOnboardToCalendar";
            this.rbOnboardToCalendar.Size = new System.Drawing.Size(147, 17);
            this.rbOnboardToCalendar.TabIndex = 1;
            this.rbOnboardToCalendar.Text = "On-board time to calendar";
            this.rbOnboardToCalendar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "0x";
            // 
            // FrmTimeConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 118);
            this.Controls.Add(this.mskOnboardTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbOnboardToCalendar);
            this.Controls.Add(this.rbCalendarToOnboard);
            this.Controls.Add(this.btConvert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mskCalendarTime);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmTimeConversion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Format Conversion";
            this.Load += new System.EventHandler(this.FrmTimeConversion_Load);
            this.DockStateChanged += new System.EventHandler(this.FrmTimeConversion_DockStateChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mskCalendarTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mskOnboardTime;
        private System.Windows.Forms.Button btConvert;
        private System.Windows.Forms.RadioButton rbCalendarToOnboard;
        private System.Windows.Forms.RadioButton rbOnboardToCalendar;
        private System.Windows.Forms.Label label3;
    }
}
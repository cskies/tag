namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmHexToAsciiConverter
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
            this.rdbConvertToHex = new System.Windows.Forms.RadioButton();
            this.txtAsciiMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btConvert = new System.Windows.Forms.Button();
            this.rdbConvertToAscii = new System.Windows.Forms.RadioButton();
            this.txtHexMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rdbConvertToHex
            // 
            this.rdbConvertToHex.AutoSize = true;
            this.rdbConvertToHex.Checked = true;
            this.rdbConvertToHex.Location = new System.Drawing.Point(12, 12);
            this.rdbConvertToHex.Name = "rdbConvertToHex";
            this.rdbConvertToHex.Size = new System.Drawing.Size(56, 17);
            this.rdbConvertToHex.TabIndex = 0;
            this.rdbConvertToHex.TabStop = true;
            this.rdbConvertToHex.Text = "to Hex";
            this.rdbConvertToHex.UseVisualStyleBackColor = true;
            this.rdbConvertToHex.CheckedChanged += new System.EventHandler(this.rdbConvertToHex_CheckedChanged);
            // 
            // txtAsciiMessage
            // 
            this.txtAsciiMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAsciiMessage.Location = new System.Drawing.Point(5, 57);
            this.txtAsciiMessage.Multiline = true;
            this.txtAsciiMessage.Name = "txtAsciiMessage";
            this.txtAsciiMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAsciiMessage.Size = new System.Drawing.Size(284, 199);
            this.txtAsciiMessage.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ASCII Message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hex Message:";
            // 
            // btConvert
            // 
            this.btConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btConvert.Location = new System.Drawing.Point(144, 9);
            this.btConvert.Name = "btConvert";
            this.btConvert.Size = new System.Drawing.Size(145, 23);
            this.btConvert.TabIndex = 5;
            this.btConvert.Text = "Convert";
            this.btConvert.UseVisualStyleBackColor = true;
            this.btConvert.Click += new System.EventHandler(this.btConvert_Click);
            // 
            // rdbConvertToAscii
            // 
            this.rdbConvertToAscii.AutoSize = true;
            this.rdbConvertToAscii.Location = new System.Drawing.Point(74, 12);
            this.rdbConvertToAscii.Name = "rdbConvertToAscii";
            this.rdbConvertToAscii.Size = new System.Drawing.Size(64, 17);
            this.rdbConvertToAscii.TabIndex = 6;
            this.rdbConvertToAscii.TabStop = true;
            this.rdbConvertToAscii.Text = "to ASCII";
            this.rdbConvertToAscii.UseVisualStyleBackColor = true;
            // 
            // txtHexMessage
            // 
            this.txtHexMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHexMessage.Location = new System.Drawing.Point(5, 275);
            this.txtHexMessage.Multiline = true;
            this.txtHexMessage.Name = "txtHexMessage";
            this.txtHexMessage.ReadOnly = true;
            this.txtHexMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHexMessage.Size = new System.Drawing.Size(284, 199);
            this.txtHexMessage.TabIndex = 7;
            // 
            // FrmHexToAsciiConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 496);
            this.Controls.Add(this.txtHexMessage);
            this.Controls.Add(this.rdbConvertToAscii);
            this.Controls.Add(this.btConvert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAsciiMessage);
            this.Controls.Add(this.rdbConvertToHex);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmHexToAsciiConverter";
            this.Text = "Hex to ASCII Converter";
            this.Load += new System.EventHandler(this.FrmHexToAsciiConverter_Load);
            this.DockStateChanged += new System.EventHandler(this.FrmHexToAsciiConverter_DockStateChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbConvertToHex;
        private System.Windows.Forms.TextBox txtAsciiMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btConvert;
        private System.Windows.Forms.RadioButton rdbConvertToAscii;
        private System.Windows.Forms.TextBox txtHexMessage;
    }
}
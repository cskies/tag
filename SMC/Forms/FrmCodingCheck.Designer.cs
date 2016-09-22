namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmCodingCheck
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCoding = new System.Windows.Forms.ComboBox();
            this.txtBytesToCheck = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btCalculate = new System.Windows.Forms.Button();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Coding:";
            // 
            // cmbCoding
            // 
            this.cmbCoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoding.FormattingEnabled = true;
            this.cmbCoding.Items.AddRange(new object[] {
            "CRC-CCITT16",
            "CRC-32",
            "CRC Amazonia-1 (32 bits, signed)",
            "ISO Checksum (16 bits)",
            "BCH (63, 56)",
            "CRC-ACE Amazonia-1 (16 bits)"});
            this.cmbCoding.Location = new System.Drawing.Point(52, 114);
            this.cmbCoding.Name = "cmbCoding";
            this.cmbCoding.Size = new System.Drawing.Size(180, 21);
            this.cmbCoding.TabIndex = 1;
            this.cmbCoding.SelectedIndexChanged += new System.EventHandler(this.cmbCoding_SelectedIndexChanged);
            // 
            // txtBytesToCheck
            // 
            this.txtBytesToCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBytesToCheck.BackColor = System.Drawing.SystemColors.Window;
            this.txtBytesToCheck.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBytesToCheck.Location = new System.Drawing.Point(6, 23);
            this.txtBytesToCheck.Multiline = true;
            this.txtBytesToCheck.Name = "txtBytesToCheck";
            this.txtBytesToCheck.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBytesToCheck.Size = new System.Drawing.Size(303, 83);
            this.txtBytesToCheck.TabIndex = 0;
            this.txtBytesToCheck.Leave += new System.EventHandler(this.txtBytesToCheck_Leave);
            this.txtBytesToCheck.Enter += new System.EventHandler(this.txtBytesToCheck_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Bytes to Check (insert them as hex):";
            // 
            // btCalculate
            // 
            this.btCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCalculate.Location = new System.Drawing.Point(238, 112);
            this.btCalculate.Name = "btCalculate";
            this.btCalculate.Size = new System.Drawing.Size(71, 23);
            this.btCalculate.TabIndex = 2;
            this.btCalculate.Text = "Calculate";
            this.btCalculate.UseVisualStyleBackColor = true;
            this.btCalculate.Click += new System.EventHandler(this.btCalculate_Click);
            // 
            // lblCode
            // 
            this.lblCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(3, 144);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(40, 13);
            this.lblCode.TabIndex = 16;
            this.lblCode.Text = "Result:";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResult.BackColor = System.Drawing.SystemColors.Window;
            this.txtResult.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(52, 141);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(255, 20);
            this.txtResult.TabIndex = 3;
            // 
            // FrmCodingCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 169);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.btCalculate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBytesToCheck);
            this.Controls.Add(this.cmbCoding);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmCodingCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coding Check";
            this.Load += new System.EventHandler(this.FrmCodingCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCoding;
        private System.Windows.Forms.TextBox txtBytesToCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCalculate;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtResult;
    }
}
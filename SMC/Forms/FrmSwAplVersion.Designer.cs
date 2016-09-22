namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSwAplVersion
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
            this.label2 = new System.Windows.Forms.Label();
            this.numSwMajor = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numSwPatch = new System.Windows.Forms.NumericUpDown();
            this.numSwMinor = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMajor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwPatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMinor)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 26);
            this.label2.TabIndex = 59;
            this.label2.Text = "SW APL\r\nVersion:";
            // 
            // numSwMajor
            // 
            this.numSwMajor.Location = new System.Drawing.Point(50, 56);
            this.numSwMajor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwMajor.Name = "numSwMajor";
            this.numSwMajor.Size = new System.Drawing.Size(60, 20);
            this.numSwMajor.TabIndex = 58;
            this.numSwMajor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 26);
            this.label5.TabIndex = 62;
            this.label5.Text = "SW APL\r\nPatch:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 26);
            this.label4.TabIndex = 63;
            this.label4.Text = "SW APL\r\nRelease:";
            // 
            // numSwPatch
            // 
            this.numSwPatch.Location = new System.Drawing.Point(208, 56);
            this.numSwPatch.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwPatch.Name = "numSwPatch";
            this.numSwPatch.Size = new System.Drawing.Size(60, 20);
            this.numSwPatch.TabIndex = 61;
            this.numSwPatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numSwMinor
            // 
            this.numSwMinor.Location = new System.Drawing.Point(130, 56);
            this.numSwMinor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwMinor.Name = "numSwMinor";
            this.numSwMinor.Size = new System.Drawing.Size(60, 20);
            this.numSwMinor.TabIndex = 60;
            this.numSwMinor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(-5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(322, 24);
            this.label3.TabIndex = 64;
            this.label3.Text = "Would You Like to Save the Flight Software Version ?";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(50, 107);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(81, 25);
            this.btSave.TabIndex = 65;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            this.btSave.NotifyDefault(true);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(187, 107);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(81, 25);
            this.btCancel.TabIndex = 66;
            this.btCancel.Text = "Do Not Save";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmSwAplVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(317, 144);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numSwPatch);
            this.Controls.Add(this.numSwMinor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numSwMajor);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmSwAplVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flight Soft Version";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmSwAplVersion_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSwAplVersion_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.numSwMajor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwPatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwMinor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numSwMajor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numSwPatch;
        public System.Windows.Forms.NumericUpDown numSwMinor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
    }
}
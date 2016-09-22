namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSplash
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
            this.lblVersionInfo = new System.Windows.Forms.Label();
            this.cmbSelectDb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btConfirm = new System.Windows.Forms.Button();
            this.btOffline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblVersionInfo
            // 
            this.lblVersionInfo.AutoSize = true;
            this.lblVersionInfo.BackColor = System.Drawing.Color.White;
            this.lblVersionInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionInfo.Location = new System.Drawing.Point(34, 250);
            this.lblVersionInfo.Name = "lblVersionInfo";
            this.lblVersionInfo.Size = new System.Drawing.Size(151, 13);
            this.lblVersionInfo.TabIndex = 0;
            this.lblVersionInfo.Text = "[version information here]";
            this.lblVersionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSelectDb
            // 
            this.cmbSelectDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectDb.FormattingEnabled = true;
            this.cmbSelectDb.Location = new System.Drawing.Point(144, 209);
            this.cmbSelectDb.Name = "cmbSelectDb";
            this.cmbSelectDb.Size = new System.Drawing.Size(234, 21);
            this.cmbSelectDb.TabIndex = 1;
            this.cmbSelectDb.SelectedIndexChanged += new System.EventHandler(this.cmbSelectDb_SelectedIndexChanged);
            this.cmbSelectDb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSplash_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(38, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select The Mission:";
            // 
            // btConfirm
            // 
            this.btConfirm.Location = new System.Drawing.Point(384, 208);
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(82, 23);
            this.btConfirm.TabIndex = 3;
            this.btConfirm.Text = "Connect";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            this.btConfirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSplash_KeyDown);
            // 
            // btOffline
            // 
            this.btOffline.Location = new System.Drawing.Point(384, 237);
            this.btOffline.Name = "btOffline";
            this.btOffline.Size = new System.Drawing.Size(82, 23);
            this.btOffline.TabIndex = 4;
            this.btOffline.Text = "Offline Mode";
            this.btOffline.UseVisualStyleBackColor = true;
            this.btOffline.Click += new System.EventHandler(this.btOffline_Click);
            // 
            // FrmSplash
            // 
            this.AcceptButton = this.btConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.Splash_SMC;
            this.ClientSize = new System.Drawing.Size(694, 361);
            this.Controls.Add(this.btOffline);
            this.Controls.Add(this.btConfirm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSelectDb);
            this.Controls.Add(this.lblVersionInfo);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Splash_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSplash_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersionInfo;
        private System.Windows.Forms.ComboBox cmbSelectDb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.Button btOffline;
    }
}
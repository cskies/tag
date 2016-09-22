namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmCopyProcedure
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
            this.lblNewDescription = new System.Windows.Forms.Label();
            this.txtNewProcDescription = new System.Windows.Forms.TextBox();
            this.btCopy = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lblCurrentDescription = new System.Windows.Forms.Label();
            this.txtCurrentProcDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblNewDescription
            // 
            this.lblNewDescription.AutoSize = true;
            this.lblNewDescription.Location = new System.Drawing.Point(12, 38);
            this.lblNewDescription.Name = "lblNewDescription";
            this.lblNewDescription.Size = new System.Drawing.Size(140, 13);
            this.lblNewDescription.TabIndex = 0;
            this.lblNewDescription.Text = "New Procedure Description:";
            // 
            // txtNewProcDescription
            // 
            this.txtNewProcDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewProcDescription.Location = new System.Drawing.Point(170, 35);
            this.txtNewProcDescription.Name = "txtNewProcDescription";
            this.txtNewProcDescription.Size = new System.Drawing.Size(444, 20);
            this.txtNewProcDescription.TabIndex = 1;
            // 
            // btCopy
            // 
            this.btCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCopy.Location = new System.Drawing.Point(459, 61);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(75, 23);
            this.btCopy.TabIndex = 2;
            this.btCopy.Text = "Copy";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(540, 61);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 3;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lblCurrentDescription
            // 
            this.lblCurrentDescription.AutoSize = true;
            this.lblCurrentDescription.Location = new System.Drawing.Point(12, 9);
            this.lblCurrentDescription.Name = "lblCurrentDescription";
            this.lblCurrentDescription.Size = new System.Drawing.Size(152, 13);
            this.lblCurrentDescription.TabIndex = 4;
            this.lblCurrentDescription.Text = "Current Procedure Description:";
            // 
            // txtCurrentProcDescription
            // 
            this.txtCurrentProcDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentProcDescription.Location = new System.Drawing.Point(170, 6);
            this.txtCurrentProcDescription.Name = "txtCurrentProcDescription";
            this.txtCurrentProcDescription.ReadOnly = true;
            this.txtCurrentProcDescription.Size = new System.Drawing.Size(444, 20);
            this.txtCurrentProcDescription.TabIndex = 5;
            // 
            // FrmCopyProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 93);
            this.Controls.Add(this.txtCurrentProcDescription);
            this.Controls.Add(this.lblCurrentDescription);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btCopy);
            this.Controls.Add(this.txtNewProcDescription);
            this.Controls.Add(this.lblNewDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCopyProcedure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copying Procedure";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNewDescription;
        private System.Windows.Forms.TextBox txtNewProcDescription;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lblCurrentDescription;
        private System.Windows.Forms.TextBox txtCurrentProcDescription;
    }
}
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmConnPathConfig
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btConfirm = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btConnectionFilePath = new System.Windows.Forms.Button();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Path:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(62, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(406, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(62, 35);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(371, 20);
            this.txtPath.TabIndex = 3;
            // 
            // btConfirm
            // 
            this.btConfirm.Location = new System.Drawing.Point(311, 62);
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(75, 23);
            this.btConfirm.TabIndex = 4;
            this.btConfirm.Text = "Confirm";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(392, 62);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // btConnectionFilePath
            // 
            this.btConnectionFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btConnectionFilePath.Location = new System.Drawing.Point(439, 32);
            this.btConnectionFilePath.Name = "btConnectionFilePath";
            this.btConnectionFilePath.Size = new System.Drawing.Size(27, 23);
            this.btConnectionFilePath.TabIndex = 6;
            this.btConnectionFilePath.Text = "...";
            this.btConnectionFilePath.UseVisualStyleBackColor = true;
            this.btConnectionFilePath.Click += new System.EventHandler(this.btConnectionFilePath_Click);
            // 
            // FrmConnPathConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 92);
            this.Controls.Add(this.btConnectionFilePath);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btConfirm);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmConnPathConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Path Configuration";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button btConnectionFilePath;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
    }
}
using System.Windows.Forms;
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmExecutionTimeout
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
            this.mskTimeSecs = new System.Windows.Forms.MaskedTextBox();
            this.dtpTimeout = new System.Windows.Forms.DateTimePicker();
            this.rbAbsolute = new System.Windows.Forms.RadioButton();
            this.rbRelative = new System.Windows.Forms.RadioButton();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mskTimeSecs
            // 
            this.mskTimeSecs.Location = new System.Drawing.Point(12, 48);
            this.mskTimeSecs.Mask = "00:90:00:00";
            this.mskTimeSecs.Name = "mskTimeSecs";
            this.mskTimeSecs.Size = new System.Drawing.Size(302, 20);
            this.mskTimeSecs.TabIndex = 0;
            this.mskTimeSecs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskTimeSecs_MouseClick);
            this.mskTimeSecs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskTimeSecs_KeyPress);
            // 
            // dtpTimeout
            // 
            this.dtpTimeout.Location = new System.Drawing.Point(12, 74);
            this.dtpTimeout.Name = "dtpTimeout";
            this.dtpTimeout.Size = new System.Drawing.Size(302, 20);
            this.dtpTimeout.TabIndex = 1;
            // 
            // rbAbsolute
            // 
            this.rbAbsolute.AutoSize = true;
            this.rbAbsolute.Location = new System.Drawing.Point(12, 2);
            this.rbAbsolute.Name = "rbAbsolute";
            this.rbAbsolute.Size = new System.Drawing.Size(66, 17);
            this.rbAbsolute.TabIndex = 2;
            this.rbAbsolute.TabStop = true;
            this.rbAbsolute.Text = "Absolute";
            this.rbAbsolute.UseVisualStyleBackColor = true;
            // 
            // rbRelative
            // 
            this.rbRelative.AutoSize = true;
            this.rbRelative.Location = new System.Drawing.Point(12, 25);
            this.rbRelative.Name = "rbRelative";
            this.rbRelative.Size = new System.Drawing.Size(64, 17);
            this.rbRelative.TabIndex = 3;
            this.rbRelative.TabStop = true;
            this.rbRelative.Text = "Relative";
            this.rbRelative.UseVisualStyleBackColor = true;
            this.rbRelative.CheckedChanged += new System.EventHandler(this.rbRelative_CheckedChanged);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(158, 102);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(239, 102);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmExecutionTimeout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 131);
            this.Controls.Add(this.rbAbsolute);
            this.Controls.Add(this.rbRelative);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.mskTimeSecs);
            this.Controls.Add(this.dtpTimeout);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExecutionTimeout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Execution Timeout";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmExecutionTimeout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //TODO TESTE watermark maskedTxtBox
        //private void DrawWaterMark() 
        //{
        //    if (this.waterMarkContainer == null && this.)
        //    {
                
        //    }
        //}

        #endregion

        private System.Windows.Forms.MaskedTextBox mskTimeSecs;
        private System.Windows.Forms.DateTimePicker dtpTimeout;
        private System.Windows.Forms.RadioButton rbAbsolute;
        private System.Windows.Forms.RadioButton rbRelative;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;

        //private System.Windows.Forms.Panel waterMarkContainer;
    }
}
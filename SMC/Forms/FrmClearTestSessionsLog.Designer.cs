namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmClearTestSessionsLog
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
            this.label3 = new System.Windows.Forms.Label();
            this.btClearAllSessions = new System.Windows.Forms.Button();
            this.btClearSessionsFrom = new System.Windows.Forms.Button();
            this.cmbSelectSessionsToClear = new System.Windows.Forms.ComboBox();
            this.lblSeparater = new System.Windows.Forms.Label();
            this.rdBtoClearSessionsFrom = new System.Windows.Forms.RadioButton();
            this.rdBtoClearAllSessions = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
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
            this.label3.Location = new System.Drawing.Point(-3, -1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(572, 24);
            this.label3.TabIndex = 65;
            this.label3.Text = "Clear Test Sessions Log Options:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btClearAllSessions
            // 
            this.btClearAllSessions.Location = new System.Drawing.Point(173, 52);
            this.btClearAllSessions.Name = "btClearAllSessions";
            this.btClearAllSessions.Size = new System.Drawing.Size(224, 25);
            this.btClearAllSessions.TabIndex = 4;
            this.btClearAllSessions.Text = "Clear ALL Test Sessions Log";
            this.btClearAllSessions.UseVisualStyleBackColor = true;
            this.btClearAllSessions.Click += new System.EventHandler(this.btClearAllSessions_Click);
            // 
            // btClearSessionsFrom
            // 
            this.btClearSessionsFrom.Location = new System.Drawing.Point(173, 160);
            this.btClearSessionsFrom.Name = "btClearSessionsFrom";
            this.btClearSessionsFrom.Size = new System.Drawing.Size(224, 25);
            this.btClearSessionsFrom.TabIndex = 2;
            this.btClearSessionsFrom.Text = "Clear Test Sessions from";
            this.btClearSessionsFrom.UseVisualStyleBackColor = true;
            this.btClearSessionsFrom.Click += new System.EventHandler(this.btClearSessionsFrom_Click_1);
            // 
            // cmbSelectSessionsToClear
            // 
            this.cmbSelectSessionsToClear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectSessionsToClear.FormattingEnabled = true;
            this.cmbSelectSessionsToClear.Location = new System.Drawing.Point(28, 133);
            this.cmbSelectSessionsToClear.Name = "cmbSelectSessionsToClear";
            this.cmbSelectSessionsToClear.Size = new System.Drawing.Size(530, 21);
            this.cmbSelectSessionsToClear.TabIndex = 1;
            this.cmbSelectSessionsToClear.SelectedIndexChanged += new System.EventHandler(this.cmbSelectSessionsToClear_SelectedIndexChanged);
            // 
            // lblSeparater
            // 
            this.lblSeparater.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblSeparater.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparater.Location = new System.Drawing.Point(-3, 93);
            this.lblSeparater.Name = "lblSeparater";
            this.lblSeparater.Size = new System.Drawing.Size(572, 2);
            this.lblSeparater.TabIndex = 72;
            // 
            // rdBtoClearSessionsFrom
            // 
            this.rdBtoClearSessionsFrom.AutoSize = true;
            this.rdBtoClearSessionsFrom.Location = new System.Drawing.Point(12, 110);
            this.rdBtoClearSessionsFrom.Name = "rdBtoClearSessionsFrom";
            this.rdBtoClearSessionsFrom.Size = new System.Drawing.Size(147, 17);
            this.rdBtoClearSessionsFrom.TabIndex = 0;
            this.rdBtoClearSessionsFrom.TabStop = true;
            this.rdBtoClearSessionsFrom.Text = "Clear Test Sessions from: ";
            this.rdBtoClearSessionsFrom.UseVisualStyleBackColor = true;
            this.rdBtoClearSessionsFrom.CheckedChanged += new System.EventHandler(this.rdBtoClearSessionsFrom_CheckedChanged);
            // 
            // rdBtoClearAllSessions
            // 
            this.rdBtoClearAllSessions.AutoSize = true;
            this.rdBtoClearAllSessions.Location = new System.Drawing.Point(12, 29);
            this.rdBtoClearAllSessions.Name = "rdBtoClearAllSessions";
            this.rdBtoClearAllSessions.Size = new System.Drawing.Size(164, 17);
            this.rdBtoClearAllSessions.TabIndex = 3;
            this.rdBtoClearAllSessions.TabStop = true;
            this.rdBtoClearAllSessions.Text = "Clear ALL Test Sessions Log:";
            this.rdBtoClearAllSessions.UseVisualStyleBackColor = true;
            this.rdBtoClearAllSessions.CheckedChanged += new System.EventHandler(this.rdBtoClearAllSessions_CheckedChanged);
            // 
            // FrmClearTestSessionsLog
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(570, 197);
            this.Controls.Add(this.rdBtoClearAllSessions);
            this.Controls.Add(this.rdBtoClearSessionsFrom);
            this.Controls.Add(this.lblSeparater);
            this.Controls.Add(this.cmbSelectSessionsToClear);
            this.Controls.Add(this.btClearSessionsFrom);
            this.Controls.Add(this.btClearAllSessions);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmClearTestSessionsLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clear Test Sessions Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btClearAllSessions;
        private System.Windows.Forms.Button btClearSessionsFrom;
        private System.Windows.Forms.ComboBox cmbSelectSessionsToClear;
        private System.Windows.Forms.Label lblSeparater;
        private System.Windows.Forms.RadioButton rdBtoClearSessionsFrom;
        private System.Windows.Forms.RadioButton rdBtoClearAllSessions;
    }
}
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmApplicationData
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
            this.gridEmbedded = new System.Windows.Forms.DataGridView();
            this.lblCaption = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.lblN = new System.Windows.Forms.Label();
            this.numN = new System.Windows.Forms.NumericUpDown();
            this.btCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmbedded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEmbedded
            // 
            this.gridEmbedded.AllowUserToAddRows = false;
            this.gridEmbedded.AllowUserToDeleteRows = false;
            this.gridEmbedded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEmbedded.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridEmbedded.Location = new System.Drawing.Point(6, 30);
            this.gridEmbedded.Name = "gridEmbedded";
            this.gridEmbedded.RowHeadersVisible = false;
            this.gridEmbedded.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridEmbedded.Size = new System.Drawing.Size(588, 172);
            this.gridEmbedded.TabIndex = 1;
            this.gridEmbedded.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEmbedded_CellEnter);
            this.gridEmbedded.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEmbedded_CellValidated);
            this.gridEmbedded.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridEmbedded_CellValidating);
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCaption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCaption.Location = new System.Drawing.Point(6, 7);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(522, 15);
            this.lblCaption.TabIndex = 31;
            this.lblCaption.Text = "Embedded TC Application data";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(438, 208);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // lblN
            // 
            this.lblN.AutoSize = true;
            this.lblN.Location = new System.Drawing.Point(537, 8);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(18, 13);
            this.lblN.TabIndex = 40;
            this.lblN.Text = "N:";
            // 
            // numN
            // 
            this.numN.Location = new System.Drawing.Point(557, 4);
            this.numN.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numN.Name = "numN";
            this.numN.Size = new System.Drawing.Size(37, 20);
            this.numN.TabIndex = 0;
            this.numN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numN.ValueChanged += new System.EventHandler(this.numN_ValueChanged);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(519, 208);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmApplicationData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 235);
            this.Controls.Add(this.lblN);
            this.Controls.Add(this.numN);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gridEmbedded);
            this.Controls.Add(this.lblCaption);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmApplicationData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "APID XXXX, SSC XXXX, Service Type XXXX, Service Subtype XXXX";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridEmbedded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridEmbedded;
        private System.Windows.Forms.Button btOK;
        public System.Windows.Forms.Label lblN;
        public System.Windows.Forms.NumericUpDown numN;
        public System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Button btCancel;
    }
}
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmViewsSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridViews = new System.Windows.Forms.DataGridView();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.gridSessions = new System.Windows.Forms.DataGridView();
            this.btRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gridViews);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Your Views";
            // 
            // gridViews
            // 
            this.gridViews.AllowUserToAddRows = false;
            this.gridViews.AllowUserToDeleteRows = false;
            this.gridViews.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridViews.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridViews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViews.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridViews.Location = new System.Drawing.Point(3, 16);
            this.gridViews.MultiSelect = false;
            this.gridViews.Name = "gridViews";
            this.gridViews.RowHeadersVisible = false;
            this.gridViews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridViews.Size = new System.Drawing.Size(620, 153);
            this.gridViews.TabIndex = 15;
            this.gridViews.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridViews_DataError_1);
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(427, 436);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(100, 31);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(533, 436);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 31);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // gridSessions
            // 
            this.gridSessions.AllowUserToAddRows = false;
            this.gridSessions.AllowUserToDeleteRows = false;
            this.gridSessions.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.gridSessions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSessions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridSessions.Location = new System.Drawing.Point(10, 228);
            this.gridSessions.MultiSelect = false;
            this.gridSessions.Name = "gridSessions";
            this.gridSessions.ReadOnly = true;
            this.gridSessions.RowHeadersVisible = false;
            this.gridSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridSessions.Size = new System.Drawing.Size(626, 194);
            this.gridSessions.TabIndex = 15;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(505, 199);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(128, 23);
            this.btRefresh.TabIndex = 16;
            this.btRefresh.Text = "Refresh Sessions Grid";
            this.btRefresh.UseVisualStyleBackColor = true;
            // 
            // FrmViewsSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 475);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.gridSessions);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmViewsSelection";
            this.Text = "Housekeeping Parameters Viewers Selection";
            this.Load += new System.EventHandler(this.FrmViewsSelection_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSessions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.DataGridView gridViews;
        private System.Windows.Forms.DataGridView gridSessions;
        private System.Windows.Forms.Button btRefresh;
    }
}
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmSavedRequests
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
            this.gridDatabase = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btCompositeNewTc = new System.Windows.Forms.ToolStripButton();
            this.btSendTc = new System.Windows.Forms.ToolStripButton();
            this.btDeleteTc = new System.Windows.Forms.ToolStripButton();
            this.btLoadTC = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridDatabase
            // 
            this.gridDatabase.AllowUserToAddRows = false;
            this.gridDatabase.AllowUserToDeleteRows = false;
            this.gridDatabase.AllowUserToOrderColumns = true;
            this.gridDatabase.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridDatabase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDatabase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridDatabase.Location = new System.Drawing.Point(0, 0);
            this.gridDatabase.MultiSelect = false;
            this.gridDatabase.Name = "gridDatabase";
            this.gridDatabase.ReadOnly = true;
            this.gridDatabase.RowHeadersVisible = false;
            this.gridDatabase.RowHeadersWidth = 25;
            this.gridDatabase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDatabase.Size = new System.Drawing.Size(741, 355);
            this.gridDatabase.TabIndex = 1;
            this.gridDatabase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDatabase_CellDoubleClick);
            this.gridDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridDatabase_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btCompositeNewTc,
            this.btSendTc,
            this.btDeleteTc,
            this.btLoadTC,
            this.btRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 358);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(741, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btCompositeNewTc
            // 
            this.btCompositeNewTc.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.add_page_blue;
            this.btCompositeNewTc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCompositeNewTc.Name = "btCompositeNewTc";
            this.btCompositeNewTc.Size = new System.Drawing.Size(150, 22);
            this.btCompositeNewTc.Text = "Compose New Request";
            this.btCompositeNewTc.Click += new System.EventHandler(this.btCompositeNewTc_Click);
            // 
            // btSendTc
            // 
            this.btSendTc.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.send_request;
            this.btSendTc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSendTc.Name = "btSendTc";
            this.btSendTc.Size = new System.Drawing.Size(98, 22);
            this.btSendTc.Text = "Send Request";
            this.btSendTc.Click += new System.EventHandler(this.btSendTc_Click);
            // 
            // btDeleteTc
            // 
            this.btDeleteTc.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.delete_page_red;
            this.btDeleteTc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDeleteTc.Name = "btDeleteTc";
            this.btDeleteTc.Size = new System.Drawing.Size(105, 22);
            this.btDeleteTc.Text = "Delete Request";
            this.btDeleteTc.Click += new System.EventHandler(this.btDeleteTc_Click);
            // 
            // btLoadTC
            // 
            this.btLoadTC.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.load_request;
            this.btLoadTC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btLoadTC.Name = "btLoadTC";
            this.btLoadTC.Size = new System.Drawing.Size(98, 22);
            this.btLoadTC.Text = "Load Request";
            this.btLoadTC.Click += new System.EventHandler(this.btLoadTC_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btRefresh.Image = global::Inpe.Subord.Comav.Egse.Smc.Properties.Resources.Refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(66, 22);
            this.btRefresh.Text = "Refresh";
            this.btRefresh.ToolTipText = "Refresh Grid (F5)";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // FrmSavedRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(741, 383);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gridDatabase);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSavedRequests";
            this.Text = "Saved Requests";
            this.DockStateChanged += new System.EventHandler(this.FrmSavedRequests_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSavedRequests_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridDatabase)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btCompositeNewTc;
        private System.Windows.Forms.ToolStripButton btDeleteTc;
        private System.Windows.Forms.ToolStripButton btLoadTC;
        private System.Windows.Forms.ToolStripButton btRefresh;
        public System.Windows.Forms.ToolStripButton btSendTc;
        public System.Windows.Forms.DataGridView gridDatabase;
    }
}
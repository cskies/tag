namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmQueries
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCreateUserQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btRun = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btOpen = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.gridQuery = new System.Windows.Forms.DataGridView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bCreateUserQuery);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btRun);
            this.panel1.Controls.Add(this.btClear);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Controls.Add(this.btOpen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 30);
            this.panel1.TabIndex = 0;
            // 
            // bCreateUserQuery
            // 
            this.bCreateUserQuery.Enabled = false;
            this.bCreateUserQuery.Location = new System.Drawing.Point(203, 3);
            this.bCreateUserQuery.Name = "bCreateUserQuery";
            this.bCreateUserQuery.Size = new System.Drawing.Size(142, 23);
            this.bCreateUserQuery.TabIndex = 2;
            this.bCreateUserQuery.Text = "Create a New User Query";
            this.bCreateUserQuery.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(532, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "To copy the results, just select the lines you\r\nwant from the grid and press <Ctr" +
                "l+C>.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(351, 3);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(92, 23);
            this.btRun.TabIndex = 3;
            this.btRun.Text = "Run Query (F9)";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(449, 3);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 4;
            this.btClear.Text = "Clear Query";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(103, 3);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(94, 23);
            this.btSave.TabIndex = 1;
            this.btSave.Text = "Save Query File";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(3, 3);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(94, 23);
            this.btOpen.TabIndex = 0;
            this.btOpen.Text = "Open Query File";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtQuery);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridQuery);
            this.splitContainer1.Size = new System.Drawing.Size(747, 288);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 1;
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(747, 130);
            this.txtQuery.TabIndex = 0;
            // 
            // gridQuery
            // 
            this.gridQuery.AllowUserToAddRows = false;
            this.gridQuery.AllowUserToDeleteRows = false;
            this.gridQuery.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridQuery.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridQuery.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridQuery.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridQuery.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.gridQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridQuery.Location = new System.Drawing.Point(0, 0);
            this.gridQuery.Name = "gridQuery";
            this.gridQuery.ReadOnly = true;
            this.gridQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridQuery.Size = new System.Drawing.Size(747, 154);
            this.gridQuery.TabIndex = 0;
            this.gridQuery.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridQuery_DataError);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmQueries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(747, 318);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmQueries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Queries";
            this.Load += new System.EventHandler(this.FrmQueries_Load);
            this.Activated += new System.EventHandler(this.FrmQueries_Activated);
            this.DockStateChanged += new System.EventHandler(this.FrmQueries_DockStateChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmQueries_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridQuery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.DataGridView gridQuery;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button bCreateUserQuery;

    }
}
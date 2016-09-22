namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmCortexCOP1Configuration
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
            this.lblAttention = new System.Windows.Forms.Label();
            this.btGetRefreshConfig = new System.Windows.Forms.Button();
            this.gridParameters = new System.Windows.Forms.DataGridView();
            this.colParamId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPossibleValues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAttention
            // 
            this.lblAttention.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAttention.AutoSize = true;
            this.lblAttention.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttention.ForeColor = System.Drawing.Color.Red;
            this.lblAttention.Location = new System.Drawing.Point(276, 622);
            this.lblAttention.Name = "lblAttention";
            this.lblAttention.Size = new System.Drawing.Size(641, 16);
            this.lblAttention.TabIndex = 1;
            this.lblAttention.Text = "Attention! Check the CORTEX connection configuration on \"Connection With COMAV\" s" +
    "creen.";
            // 
            // btGetRefreshConfig
            // 
            this.btGetRefreshConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btGetRefreshConfig.Location = new System.Drawing.Point(3, 619);
            this.btGetRefreshConfig.Name = "btGetRefreshConfig";
            this.btGetRefreshConfig.Size = new System.Drawing.Size(267, 23);
            this.btGetRefreshConfig.TabIndex = 2;
            this.btGetRefreshConfig.Text = "Get/Refresh Configuration";
            this.btGetRefreshConfig.UseVisualStyleBackColor = true;
            this.btGetRefreshConfig.Click += new System.EventHandler(this.btGetRefreshConfig_Click);
            // 
            // gridParameters
            // 
            this.gridParameters.AllowUserToAddRows = false;
            this.gridParameters.AllowUserToDeleteRows = false;
            this.gridParameters.AllowUserToResizeColumns = false;
            this.gridParameters.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.gridParameters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParamId,
            this.colParameters,
            this.colPossibleValues,
            this.colValue});
            this.gridParameters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridParameters.Location = new System.Drawing.Point(3, 3);
            this.gridParameters.Name = "gridParameters";
            this.gridParameters.RowHeadersVisible = false;
            this.gridParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridParameters.Size = new System.Drawing.Size(1165, 610);
            this.gridParameters.TabIndex = 55;
            // 
            // colParamId
            // 
            this.colParamId.HeaderText = "Parameter Id";
            this.colParamId.Name = "colParamId";
            this.colParamId.ReadOnly = true;
            // 
            // colParameters
            // 
            this.colParameters.HeaderText = "Parameters";
            this.colParameters.Name = "colParameters";
            this.colParameters.ReadOnly = true;
            this.colParameters.Width = 250;
            // 
            // colPossibleValues
            // 
            this.colPossibleValues.HeaderText = "Possible Values";
            this.colPossibleValues.Name = "colPossibleValues";
            this.colPossibleValues.ReadOnly = true;
            this.colPossibleValues.Width = 200;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "Current Values";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // FrmCortexCOP1Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 647);
            this.Controls.Add(this.gridParameters);
            this.Controls.Add(this.btGetRefreshConfig);
            this.Controls.Add(this.lblAttention);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmCortexCOP1Configuration";
            this.Text = "Cortex/COP1 Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.gridParameters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAttention;
        private System.Windows.Forms.DataGridView gridParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPossibleValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        public System.Windows.Forms.Button btGetRefreshConfig;
    }
}
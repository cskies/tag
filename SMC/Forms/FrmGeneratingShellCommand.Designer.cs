namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    partial class FrmGeneratingShellCommand
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
            this.txtStartAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataToLoad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCommandsToEeprom = new System.Windows.Forms.TextBox();
            this.btSaveCommands = new System.Windows.Forms.Button();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.btGenerateCommands = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartAddress.Location = new System.Drawing.Point(16, 25);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtStartAddress.Size = new System.Drawing.Size(586, 20);
            this.txtStartAddress.TabIndex = 2;
            this.txtStartAddress.Validated += new System.EventHandler(this.txtStartAddress_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start Address (Hex 0x):";
            // 
            // txtDataToLoad
            // 
            this.txtDataToLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataToLoad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataToLoad.Location = new System.Drawing.Point(15, 65);
            this.txtDataToLoad.Multiline = true;
            this.txtDataToLoad.Name = "txtDataToLoad";
            this.txtDataToLoad.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDataToLoad.Size = new System.Drawing.Size(586, 289);
            this.txtDataToLoad.TabIndex = 4;
            this.txtDataToLoad.TextChanged += new System.EventHandler(this.txtDataToLoad_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Data to Load:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 399);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Commands wm to the EEPROM:";
            // 
            // txtCommandsToEeprom
            // 
            this.txtCommandsToEeprom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandsToEeprom.Location = new System.Drawing.Point(16, 415);
            this.txtCommandsToEeprom.Multiline = true;
            this.txtCommandsToEeprom.Name = "txtCommandsToEeprom";
            this.txtCommandsToEeprom.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandsToEeprom.Size = new System.Drawing.Size(586, 247);
            this.txtCommandsToEeprom.TabIndex = 7;
            this.txtCommandsToEeprom.TextChanged += new System.EventHandler(this.txtCommandsToEeprom_TextChanged);
            // 
            // btSaveCommands
            // 
            this.btSaveCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveCommands.Location = new System.Drawing.Point(349, 668);
            this.btSaveCommands.Name = "btSaveCommands";
            this.btSaveCommands.Size = new System.Drawing.Size(252, 23);
            this.btSaveCommands.TabIndex = 8;
            this.btSaveCommands.Text = "Save Commands to File";
            this.btSaveCommands.UseVisualStyleBackColor = true;
            this.btSaveCommands.Click += new System.EventHandler(this.btSaveCommands_Click);
            // 
            // btGenerateCommands
            // 
            this.btGenerateCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGenerateCommands.Location = new System.Drawing.Point(349, 360);
            this.btGenerateCommands.Name = "btGenerateCommands";
            this.btGenerateCommands.Size = new System.Drawing.Size(252, 23);
            this.btGenerateCommands.TabIndex = 9;
            this.btGenerateCommands.Text = "Generate Commands wm (Write Memory)";
            this.btGenerateCommands.UseVisualStyleBackColor = true;
            this.btGenerateCommands.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmGeneratingShellCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 698);
            this.Controls.Add(this.btGenerateCommands);
            this.Controls.Add(this.btSaveCommands);
            this.Controls.Add(this.txtCommandsToEeprom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDataToLoad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStartAddress);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmGeneratingShellCommand";
            this.Text = "Generate Shell Command";
            this.DockStateChanged += new System.EventHandler(this.FrmGeneratingShellCommand_DockStateChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStartAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDataToLoad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCommandsToEeprom;
        private System.Windows.Forms.Button btSaveCommands;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Button btGenerateCommands;
    }
}
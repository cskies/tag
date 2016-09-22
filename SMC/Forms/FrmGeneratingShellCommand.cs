/**
 * @file 	    FrmGeneratingShellCommand.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    24/11/2014
 * @note	    Modificado em 25/11/2014 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Este namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmGeneratingShellCommand
     * Formulario usado para gerar comandos wm para a EEPROM do OBDH-DM e EQM do Amazonia-1.
     **/
    public partial class FrmGeneratingShellCommand : DockContent
    {
        #region Construtor

        public FrmGeneratingShellCommand()
        {
            InitializeComponent();

            btGenerateCommands.Enabled = false;
            btSaveCommands.Enabled = false;
        }

        #endregion

        #region Eventos de Interface Grafica

        private void FrmGeneratingShellCommand_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(636, 744);
            }
        }

        private void btSaveCommands_Click(object sender, EventArgs e)
        {
            if (btSaveCommands.Tag == null)
            {
                saveDialog.InitialDirectory = "C:";
            }
            else
            {
                saveDialog.InitialDirectory = btSaveCommands.Tag.ToString();
            }

            saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveDialog.FilterIndex = 0;
            saveDialog.FileName = "cmd_wm_" + DateTime.Today.Date.Year + "-" + DateTime.Today.Date.Month + "-" + DateTime.Today.Date.Day + ".txt";
            saveDialog.Title = "Create a file to record commands";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                btSaveCommands.Tag = saveDialog.FileName;
                File.WriteAllText(saveDialog.FileName, txtCommandsToEeprom.Text); // cria um arquivo em branco
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string toConvert = txtDataToLoad.Text.ToString().Trim().Replace("\r\n", "").Replace(" ", "");
            int convertedAddress = 0;

            if ((toConvert.Length % 2) != 0)
            {
                toConvert = "0" + toConvert;
            }

            try
            {
                int converted;

                for (int i = 0; i < toConvert.Length; i++)
                {
                    converted = Convert.ToInt32(toConvert.Substring(i, 2), 16);

                    i++;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("There are invalid data to send EEPROM.\n\nVerify it and try again.",
                                "Generating Commands",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtDataToLoad.Focus();
                txtDataToLoad.SelectAll();

                return;
            }

            if (int.TryParse(txtStartAddress.Text.ToString().Trim(), out convertedAddress))
            {
                if (txtCommandsToEeprom.Text.ToString().Trim().Length > 0)
                {
                    if (MessageBox.Show("All contents in 'commands vm to the EEPROM' will be deleted.\n\nDo you want to overwrite it?",
                                    "Please Confirm Log File!",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        txtCommandsToEeprom.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
                                
                int startAddress = Convert.ToInt32(txtStartAddress.Text.ToString().Trim(), 16);
                int currentAddress = startAddress;
                string currentAddressStr = currentAddress.ToString();
                string currentAddressHex = "";

                for (int i = 0; i < toConvert.Length; i++)
                {
                    currentAddressStr = Utils.Formatting.ConvertIntToHexString(currentAddress);
                    currentAddressHex = Utils.Formatting.FormatHexString32Bits(currentAddressStr);
                    txtCommandsToEeprom.AppendText("wm " + currentAddressHex + " " + toConvert.Substring(i, 2) + "\r\n");
                    i++;
                    currentAddress++;
                }
            }
            else
            {
                MessageBox.Show("The Start Address is invalid\n\nReplace it and try again.",
                                "Generating Commands",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void txtDataToLoad_TextChanged(object sender, EventArgs e)
        {
            if (txtDataToLoad.Text.ToString().Trim().Length == 0)
            {
                btGenerateCommands.Enabled = false;
            }
            else
            {
                btGenerateCommands.Enabled = true;
            }
        }

        private void txtCommandsToEeprom_TextChanged(object sender, EventArgs e)
        {
            if (txtCommandsToEeprom.Text.ToString().Trim().Length == 0)
            {
                btSaveCommands.Enabled = false;
            }
            else
            {
                btSaveCommands.Enabled = true;
            }
        }

        private void txtStartAddress_Validated(object sender, EventArgs e)
        {
            txtStartAddress.Text = Utils.Formatting.FormatHexString32Bits(txtStartAddress.Text);
        }

        #endregion
    }
}

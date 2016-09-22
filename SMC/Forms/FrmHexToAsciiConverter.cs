/**
 * @file 	    FrmHexToAsciiConverter.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    10/08/2012
 * @note	    Modificado em 13/08/2012 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmHexToAsciiConverter
     * Formulario de conversao de mensagens (Hex to ASCII) e (ASCII to Hex).
     **/
    public partial class FrmHexToAsciiConverter : DockContent
    {
        #region Construtor

        public FrmHexToAsciiConverter()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos da Interface Grafica

        private void btConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbConvertToHex.Checked)
                {
                    String ascii = txtAsciiMessage.Text.Trim();
                    txtHexMessage.Text = Utils.Formatting.ConvertAsciiStringToHexString(ascii);
                    txtHexMessage.Focus();
                    txtHexMessage.SelectAll();
                }
                else
                {
                    String hex = txtHexMessage.Text.Trim();

                    if (Utils.Formatting.HexStringToByteArray(hex) != null) // Verifica se mensagem eh hexadecimal valida
                    {
                        txtAsciiMessage.Text = Utils.Formatting.ConvertHexStringToAsciiString(hex);
                        txtAsciiMessage.Focus();
                        txtAsciiMessage.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("The 'Hex Message' is not valid!\n\nCorrect it and try again.",
                                        "Inconsistent Data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        txtHexMessage.Focus();
                        txtHexMessage.SelectAll();
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error to Convert Message!",
                                "Inconsistent Data", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Exclamation);

                txtAsciiMessage.Focus();
                txtAsciiMessage.SelectAll();
            }
        }

        private void FrmHexToAsciiConverter_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(317, 534);
            }
        }

        private void rdbConvertToHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbConvertToAscii.Checked)
            {
                txtAsciiMessage.ReadOnly = true;
                txtAsciiMessage.Clear();
                txtHexMessage.ReadOnly = false;
                txtHexMessage.Focus();
                txtHexMessage.SelectAll();
            }
            else
            {
                txtHexMessage.ReadOnly = true;
                txtHexMessage.Clear();
                txtAsciiMessage.ReadOnly = false;
                txtAsciiMessage.Focus();
                txtAsciiMessage.SelectAll();
            }
        }

        private void FrmHexToAsciiConverter_Load(object sender, EventArgs e)
        {
            rdbConvertToHex.Checked = true;
        }

        #endregion
    }
}

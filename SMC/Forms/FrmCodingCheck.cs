/**
 * @file 	    FrmCodingCheck.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    09/09/2009
 * @note	    Modificado em 23/10/2013 por Bruna.
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
using Inpe.Subord.Comav.Egse.Smc.Utils;
using Inpe.Subord.Comav.Egse.Smc.Ccsds;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmCodingCheck
     * Formulario auxiliar para verificar codificacoes de CRC, Checksum e BCH.
     **/
    public partial class FrmCodingCheck : DockContent
    {
        public FrmCodingCheck()
        {
            InitializeComponent();
        }

        private void FrmCodingCheck_Load(object sender, EventArgs e)
        {
            cmbCoding.SelectedIndex = 0;
            txtBytesToCheck.Focus();
        }

        private void btCalculate_Click(object sender, EventArgs e)
        {
            if (txtBytesToCheck.Text.Replace("-", "").Trim().Equals(""))
            {
                MessageBox.Show("Inform the data to calculate the selected code!",
                                "Code Calculation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            // tenta converter a string em um array de bytes
            byte[] bytesToCalculate = Utils.Formatting.HexStringToByteArray(txtBytesToCheck.Text.Replace("-", ""));

            if (bytesToCalculate == null)
            {
                MessageBox.Show("The bytes informed are not in a valid hex representation!",
                                "Code Calculation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            switch (cmbCoding.SelectedIndex)
            {
                case 0: // CRC-CCITT 16
                {
                    UInt16 crc = CheckingCodes.CrcCcitt16(ref bytesToCalculate, bytesToCalculate.Length);

                    txtResult.Text = crc.ToString("X").ToUpper();

                    if ((txtResult.Text.Length == 1) || (txtResult.Text.Length == 3))
                    {
                        txtResult.Text = "0" + txtResult.Text;
                    }

                    txtResult.Text = txtResult.Text.Substring(0, 2) + "-" + txtResult.Text.Substring(2);

                    break;
                }
                case 1: // CRC32
                {
                    UInt32 crc32 = CheckingCodes.Crc32(ref bytesToCalculate, (UInt32)bytesToCalculate.LongLength, 0);

                    txtResult.Text = crc32.ToString("X").ToUpper();

                    txtResult.Text = txtResult.Text.Substring(0, 2) + "-" + txtResult.Text.Substring(2, 2) + "-" +
                                     txtResult.Text.Substring(4, 2) + "-" + txtResult.Text.Substring(6, 2);

                    break;
                }
                case 2: // CRC-Amazonia1 (32 bits, signed)
                {
                    Int32 crc32 = CheckingCodes.CrcAmazonia1(ref bytesToCalculate, (UInt32)bytesToCalculate.LongLength, 0);

                    txtResult.Text = crc32.ToString("X").ToUpper();

                    txtResult.Text = txtResult.Text.Substring(0, 2) + "-" + txtResult.Text.Substring(2, 2) + "-" +
                                     txtResult.Text.Substring(4, 2) + "-" + txtResult.Text.Substring(6, 2);

                    break;
                }
                case 3: // Checksum
                {
                    UInt16 checkSum = CheckingCodes.IsoChecksum(bytesToCalculate, bytesToCalculate.Length);

                    txtResult.Text = checkSum.ToString("X").ToUpper();
                    txtResult.Text = txtResult.Text.Substring(0, 2) + "-" + txtResult.Text.Substring(2);

                    break;
                }
                case 4: // BCH
                {
                    byte errorControl = 0;

                    if (!CheckingCodes.Bch6356(bytesToCalculate, ref errorControl))
                    {
                        MessageBox.Show("The input for the BCH has to have 7 bytes!",
                                        "Code Calculation Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }

                    txtResult.Text = errorControl.ToString("X").ToUpper();

                    break;
                }
                case 5: // CRC-ACE Amazonia-1 (16 bits)
                {
                    UInt16 checkSum = CheckingCodes.CrcAceAmazonia1(bytesToCalculate, bytesToCalculate.Length);

                    txtResult.Text = checkSum.ToString("X").ToUpper();
                    txtResult.Text = txtResult.Text.Substring(0, 2) + "-" + txtResult.Text.Substring(2);


                    break;
                }
            }
        }

        private void txtBytesToCheck_Leave(object sender, EventArgs e)
        {
            txtBytesToCheck.Text = Formatting.FormatHexString(txtBytesToCheck.Text);
        }

        private void txtBytesToCheck_Enter(object sender, EventArgs e)
        {
            txtBytesToCheck.SelectAll();
        }

        private void cmbCoding_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

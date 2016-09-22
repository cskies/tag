/**
 * @file 	    FrmCortexCOP1Configuration.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    01/09/2014
 * @note	    Modificado em 02/09/2014 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.Database;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmCortexCOP1Configuration
     * Este Formulario eh usado para monitoramento dos parametros do COP-1. Os parametros sao exibidos em conformidade com o Documento de especificacao do protocolo do Cortex/COP-1.
     **/
    public partial class FrmCortexCOP1Configuration : DockContent
    {
        #region Atributos

        private CortexHandling cortex;
        private FrmConnectionWithEgse frmConnection;
        private byte[] msgCopMonitoring;

        #endregion

        #region Propriedades

        public CortexHandling CortexInstance
        {
            get
            {
                return cortex;
            }
            set
            {
                cortex = value;
                btGetRefreshConfig.Enabled = true;
            }
        }

        public byte[] MessageCOPMonitoring
        {
            set
            {
                msgCopMonitoring = value;

                // extrai os parametros de dentro da mensagem recebida.
                byte[] parameters = new byte[92];
                Array.Copy(msgCopMonitoring, 24, parameters, 0, 92);

                // preenche o grid com os valores.
                FillGridWithParametersValue(parameters);
            }
        }

        #endregion

        #region Construtor

        public FrmCortexCOP1Configuration(MdiMain mdiMain)
        {
            InitializeComponent();
            ConfigureGrid();
            
            frmConnection = mdiMain.FormConnectionWithEgse;
            btGetRefreshConfig.Enabled = false;

            if (frmConnection != null)
            {
                if ((frmConnection.Connected) && frmConnection.rbTcpIp.Checked)
                {
                    cortex = frmConnection.CortexInstance;
                    btGetRefreshConfig.Enabled = true;
                }
            }
        }

        #endregion

        #region Metodos Privados

        /**
         * Este metodo configura o grid com os rotulos e informacoes dos parametros do COP-1 (de acordo com o Documento).
         **/ 
        private void ConfigureGrid()
        {
            gridParameters.Rows.Add(24);

            gridParameters[0, 0].Value = "0";
            gridParameters[0, 1].Value = "1";
            gridParameters[0, 2].Value = "2";
            gridParameters[0, 3].Value = "3";
            gridParameters[0, 4].Value = "4";
            gridParameters[0, 5].Value = "5";
            gridParameters[0, 6].Value = "6";
            gridParameters[0, 7].Value = "7";
            gridParameters[0, 8].Value = "8";
            gridParameters[0, 9].Value = "9";
            gridParameters[0, 10].Value = "10";
            gridParameters[0, 11].Value = "11";
            gridParameters[0, 12].Value = "12";
            gridParameters[0, 13].Value = "13";
            gridParameters[0, 14].Value = "14";
            gridParameters[0, 15].Value = "15";
            gridParameters[0, 16].Value = "16";
            gridParameters[0, 17].Value = "17";
            gridParameters[0, 18].Value = "18";
            gridParameters[0, 19].Value = "19";
            gridParameters[0, 20].Value = "20";
            gridParameters[0, 21].Value = "21";
            gridParameters[0, 22].Value = "22";
            gridParameters[0, 23].Value = "23 to 39";

            gridParameters[1, 0].Value = "Spacecraft identifier";
            gridParameters[1, 1].Value = "Codeblock length";
            gridParameters[1, 2].Value = "Channel auto-switch";
            gridParameters[1, 3].Value = "TM Port number / address";
            gridParameters[1, 4].Value = "IP address";
            gridParameters[1, 5].Value = "TC Loopback";
            gridParameters[1, 6].Value = "FEC present in telemetry data";
            gridParameters[1, 7].Value = "Programming perturbation authorization";
            gridParameters[1, 8].Value = "Check out facilities";
            gridParameters[1, 9].Value = "Satellite telecommand data flow identifier ";
            gridParameters[1, 10].Value = "Monitoring flow identifier";
            gridParameters[1, 11].Value = "Check operational parameters";
            gridParameters[1, 12].Value = "Check VCID equivalence in CLCW data";
            gridParameters[1, 13].Value = "FOP1 -VCID";
            gridParameters[1, 14].Value = "FOP2 -VCID";
            gridParameters[1, 15].Value = "FOP3 -VCID";
            gridParameters[1, 16].Value = "FOP4 -VCID";
            gridParameters[1, 17].Value = "FOP1 -VCID Equiv";
            gridParameters[1, 18].Value = "FOP2 -VCID Equiv";
            gridParameters[1, 19].Value = "FOP3 -VCID Equiv";
            gridParameters[1, 20].Value = "FOP4 -VCID Equiv";
            gridParameters[1, 21].Value = "CLCW offset in TM frame";
            gridParameters[1, 22].Value = "OCF offset in TM frame";
            gridParameters[1, 23].Value = "Unused";

            gridParameters[2, 0].Value = "(0-1023)";
            gridParameters[2, 1].Value = "(5-6-7-8)";
            gridParameters[2, 2].Value = "(0= Off 1 = On)";
            gridParameters[2, 3].Value = "(See document)";
            gridParameters[2, 4].Value = "(See document)";
            gridParameters[2, 5].Value = "(0 = No 1 = Yes)";
            gridParameters[2, 6].Value = "(0 = No 1 = Yes)";
            gridParameters[2, 7].Value = "(0 = Unauthorized 1 = Authorized)";
            gridParameters[2, 8].Value = "(0 = Off 1 = On)";
            gridParameters[2, 9].Value = "(0 to 0xFFFFFFFF)";
            gridParameters[2, 10].Value = "(X)";
            gridParameters[2, 11].Value = "(0 = No 1 = Yes)";
            gridParameters[2, 12].Value = "(0 = No 1 = Yes)";
            gridParameters[2, 13].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 14].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 15].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 16].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 17].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 18].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 19].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 20].Value = "(0 to 63 or -1 if unused)";
            gridParameters[2, 21].Value = "(0 to 2044 (bytes))";
            gridParameters[2, 22].Value = "(0 to 16352 (bits))";
            gridParameters[2, 23].Value = "(0)";

            gridParameters[3, 0].Value = "0";
            gridParameters[3, 1].Value = "0";
            gridParameters[3, 2].Value = "0";
            gridParameters[3, 3].Value = "0000 : 0";
            gridParameters[3, 4].Value = "0.0.0.0";
            gridParameters[3, 5].Value = "0";
            gridParameters[3, 6].Value = "0";
            gridParameters[3, 7].Value = "0";
            gridParameters[3, 8].Value = "0";
            gridParameters[3, 9].Value = "00-00-00-00";
            gridParameters[3, 10].Value = "0";
            gridParameters[3, 11].Value = "0";
            gridParameters[3, 12].Value = "0";
            gridParameters[3, 13].Value = "0";
            gridParameters[3, 14].Value = "0";
            gridParameters[3, 15].Value = "0";
            gridParameters[3, 16].Value = "00-00-00-00";
            gridParameters[3, 17].Value = "0";
            gridParameters[3, 18].Value = "0";
            gridParameters[3, 19].Value = "0";
            gridParameters[3, 20].Value = "00-00-00-00";
            gridParameters[3, 21].Value = "0";
            gridParameters[3, 22].Value = "0";
            gridParameters[3, 23].Value = "0";
            
            gridParameters.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridParameters.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /**
         * Este metodo preenche o grid com os valores dos parametros do COP-1.
         **/
        private void FillGridWithParametersValue(byte[] receivedMessage)
        {
            try
            {
                int wordIndex = 0;
                byte[] parameter = new byte[4];
                byte[] aTemp = new byte[4];

                for (int i = 0; i < receivedMessage.Length; i++)
                {
                    aTemp[0] = receivedMessage[i++];
                    aTemp[1] = receivedMessage[i++];
                    aTemp[2] = receivedMessage[i++];
                    aTemp[3] = receivedMessage[i];

                    if (wordIndex == 3) // O IP Address e a TM Port estao juntos em um unico campo
                    {
                        byte[] tmPNumber = new byte[4];
                        tmPNumber[0] = aTemp[3];
                        tmPNumber[1] = aTemp[2];

                        int tmPortNumber = BitConverter.ToInt32(tmPNumber, 0);

                        byte[] address = new byte[4];
                        address[0] = aTemp[1];
                        address[1] = aTemp[0];

                        int tmAddress = BitConverter.ToInt32(address, 0);

                        gridParameters[3, wordIndex].Value = tmPortNumber + " : " + tmAddress;
                    }
                    else if (wordIndex == 4)
                    {
                        gridParameters[3, wordIndex].Value = (int)aTemp[0] + "." + (int)aTemp[1] + "." + (int)aTemp[2] + "." + (int)aTemp[3];
                    }
                    else if ((wordIndex == 9) || (wordIndex == 16) || (wordIndex == 20))
                    {
                        String value = Utils.Formatting.ConvertByteArrayToHexString(aTemp, aTemp.Length);
                        String valueHexa = Utils.Formatting.FormatHexString(value);
                        gridParameters[3, wordIndex].Value = valueHexa;
                    }
                    else
                    {
                        Array.Reverse(aTemp, 0, aTemp.Length);
                        gridParameters[3, wordIndex].Value = BitConverter.ToUInt32(aTemp, 0);
                    }

                    wordIndex++;
                }

                gridParameters[3, 23].Value = "0";
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Eventos de Interface Grafica

        private void btGetRefreshConfig_Click(object sender, EventArgs e)
        {
            if (cortex != null)
            {
                cortex.COPMonitoringFlow(DbConfiguration.CopMonitoringFlowPort.ToString());
            }
            else
            {
                MessageBox.Show("Check the CORTEX connection configuration on Connection With COMAV screen.");
            }
        }

        #endregion
    }
}

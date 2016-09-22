/**
 * @file 	    FrmUatMonitor.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    05/08/2013
 * @note	    Modificado em 05/08/2013 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.IO.Ports;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmStiTerminal : DockContent
    {
        public FrmStiTerminal()
        {
            InitializeComponent();
        }

        #region Atributos Publicos

        public bool offlineMode;

        #endregion

        #region Atributos Privados

        delegate void AvailableReceivedDataCallBack(String message);
        private AvailableReceivedDataCallBack printReceivedDataCallBack = null;
        private FileHandling writeFile;
        private bool saveDataSave = true;
        private bool saveDataFirst = true;
        private String bytesString;
        private int clrScreen = 0;

        #endregion

        #region Eventos da Interface Gráfica

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                // o Load eh feito para garantir que nao se sobreponha a configuracao de outro 
                // parametro setado por outro usuario apos a abertura desta tela
                DbConfiguration.Load();

                DbConfiguration.EgseRs232Port_Debug = cmbComPort.Text;
                DbConfiguration.EgseRs232Baud_Debug = cmbBaudRate.Text;
                DbConfiguration.EgseRs232DataBits_Debug = cmbDataBits.Text;
                DbConfiguration.EgseRs232Parity_Debug = cmbParity.Text;
                DbConfiguration.EgseRs232StopBits_Debug = cmbStopBits.Text;

                if (DbConfiguration.Save() == true)
                {
                    MessageBox.Show("Configuration saved successfully!",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to save configuration: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btStartListen_Click(object sender, EventArgs e)
        {
            if (btStartListen.Text == "Start Connection")
            {
                btStartListen.Text = "Stop Connection";

                if (!ConnectToSerialPort())
                {
                    return;
                }
                btClearScreen.Enabled = true;

                printReceivedDataCallBack = new AvailableReceivedDataCallBack(PrintData);
                serial.DataReceived += new SerialDataReceivedEventHandler(SerialRead);
            }
            else
            {
                serial.DataReceived -= new SerialDataReceivedEventHandler(SerialRead);
                printReceivedDataCallBack = null;
                serial.Close();
                serial.Dispose();
                btClearScreen.Enabled = false;

                btStartListen.Text = "Start Connection";
            }
        }

        private void FrmStiTerminal_Load(object sender, EventArgs e)
        {
            cmbCharge();
        }             

        private void btClearScreen_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (serial.IsOpen)
            {
                if (ConnectToSerialPort())
                {
                    serial.Write(e.KeyChar.ToString());
                }
            }
        }

        #endregion

        #region Métodos Privados

        private bool ConnectToSerialPort()
        {
            try
            {
                if (serial.IsOpen)
                {
                    serial.Close();
                }

                Parity par = Parity.None;
                StopBits stop = StopBits.None;

                if (cmbParity.SelectedItem.ToString().ToUpper().Equals("EVEN"))
                {
                    par = Parity.Even;
                }
                else if (cmbParity.SelectedItem.ToString().ToUpper().Equals("ODD"))
                {
                    par = Parity.Odd;
                }

                if (cmbStopBits.SelectedItem.ToString().ToUpper().Equals("1"))
                {
                    stop = StopBits.One;
                }
                else if (cmbStopBits.SelectedItem.ToString().ToUpper().Equals("2"))
                {
                    stop = StopBits.Two;
                }

                serial.PortName = cmbComPort.SelectedItem.ToString();
                serial.BaudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());
                serial.StopBits = stop;
                serial.Parity = par;
                serial.DataBits = int.Parse(cmbDataBits.SelectedItem.ToString());

                serial.Open(); // abre a porta com os parametros da interface
            }
            catch (Exception)
            {
                MessageBox.Show("Error trying to connect to " + cmbComPort.Text + "! \n\nPlease check your configuration.",
                                "Connection Error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        void SerialRead(object s, SerialDataReceivedEventArgs e)
        {
            int remainingBytes = serial.BytesToRead;
            byte[] buffer = new byte[remainingBytes];
            serial.Read(buffer, 0, remainingBytes);

            Encoding ascii = Encoding.ASCII;

            for (int i = 0; i < remainingBytes; i++)
            {
                Invoke(printReceivedDataCallBack, ascii.GetString(buffer, i, 1));
            }

        }

        private void cmbCharge()
        {
            if (!offlineMode)
            {
                DbConfiguration.Load();
            }
            // Carrega as portas COM disponiveis no combo
            foreach (string port in SerialPort.GetPortNames())
            {
                cmbComPort.Items.Add(port);
            }

            if (!offlineMode)
            {
                cmbComPort.SelectedIndex = cmbComPort.FindStringExact(DbConfiguration.EgseRs232Port_Debug);
                cmbBaudRate.SelectedIndex = cmbBaudRate.FindStringExact(DbConfiguration.EgseRs232Baud_Debug);
                cmbDataBits.SelectedIndex = cmbDataBits.FindStringExact(DbConfiguration.EgseRs232DataBits_Debug);
                cmbParity.SelectedIndex = cmbParity.FindStringExact(DbConfiguration.EgseRs232Parity_Debug);
                cmbStopBits.SelectedIndex = cmbStopBits.FindStringExact(DbConfiguration.EgseRs232StopBits_Debug);
            }
            else
            {
                btSave.Enabled = false;
            }
        }

        private void PrintData(String data)
        {
            if (data.Equals("["))
            {
                clrScreen++;
            }
            else if (data.Equals(";"))
            {
                clrScreen++;
            }
            else if (data.Equals("H"))
            {
                clrScreen++;
            }
            else
            {
                clrScreen = 0;
            }

            if (clrScreen == 3)
            {
                txtMessage.Clear();
                clrScreen = 0;
            }

            txtMessage.AppendText(data);
        }

        private void SaveData(String data)
        {
            if (data.Equals("\r") || data.Equals("\n"))
            {
                return;
            }
            else
            {

                if (data == ">")
                {
                    if (saveDataFirst == true)
                    {
                        bytesString = bytesString + ": ";
                    }
                }
                else
                {
                    bytesString = bytesString + data + " ";
                }

                if ((data == ">" || data == ":") && saveDataSave == true)
                {
                    if (saveDataFirst == false)
                    {
                        writeFile.Write(bytesString);
                        saveDataSave = false;
                    }
                    saveDataFirst = false;
                }
            }
        }

        #endregion
        
    }
}

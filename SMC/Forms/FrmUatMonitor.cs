/**
 * @file 	    FrmUatMonitor.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    28/09/2012
 * @note	    Modificado em 10/04/2013 por Ayres.
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
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.IO.Ports;
using System.Threading;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Comm;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmLoadFlightSoftUat
     * Formulario de cadastro de Subtypes.
     **/
    public partial class FrmUatMonitor : DockContent
    {
        #region Atributos Privados
        
        private FrmLoadFlightSoftProgressBar frmProgressBar;
        delegate void AvailableReceivedDataCallBack(String message);
        private AvailableReceivedDataCallBack printReceivedDataCallBack = null;
        private FileHandling writeFile;
        private String bytesString;
        private List<String> bytesList;
        private bool saveDataSave = true;
        private bool saveDataFirst = true;
        private int byteCount = 0;
        
        #endregion

        #region Atributos Publicos

        public bool offlineMode;

        #endregion

        #region Construtor

        public FrmUatMonitor()
        {
            InitializeComponent();
        }

        #endregion

        #region Metodos Privados

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
        
        private void PrepareAndSend(bool firstEx)
        {
            int pbarcount = 0;
            byte[] bytesToSend = null;
            byte[] finalBytesToSend = null;
            bool first = true;

            StreamReader objReader = new StreamReader(fileDialog.FileName);

            String sLine = "";
            String data = "";
            String newdata = "";
            String addressByte = "";

            while ((sLine = objReader.ReadLine()) != null)
            {

                String sizeByte = sLine.Substring(1, 2);
                UInt32 numsizeByte = UInt32.Parse(sizeByte, System.Globalization.NumberStyles.HexNumber);

                if (numsizeByte > 0)
                {
                    addressByte = sLine.Substring(3, 4);

                    int teste = (int)numsizeByte;

                    data = sLine.Substring(9, teste * 2);

                    newdata = "";
                    int k = 0;
                    for (int j = 0; j < data.Length / 2; j++)
                    {
                        newdata += data.Substring(k, 2);
                        k = k + 2;
                        newdata += " ";
                    }

                    String lineTotal = "W " + addressByte + "\r " + newdata + " \r";

                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                    bytesToSend = encoding.GetBytes(lineTotal);

                    if (firstEx)
                    {
                        if (first)
                        {
                            finalBytesToSend = bytesToSend;
                        }
                        else
                        {
                            int oldsize = finalBytesToSend.Length;
                            Array.Resize(ref finalBytesToSend, finalBytesToSend.Length + bytesToSend.Length);
                            Array.Copy(bytesToSend, 0, finalBytesToSend, oldsize, bytesToSend.Length);
                        }

                        first = false;
                    }

                    else
                    {
                        for (int p = 0; p < bytesToSend.Length; p++)
                        {
                            serial.Write(bytesToSend, p, 1);
                            Thread.Sleep(13);
                            frmProgressBar.progressBar.Value = pbarcount++;
                        }
                    }               
                }
            }
            if (firstEx)
            {
                frmProgressBar.progressBar.Maximum = finalBytesToSend.Length;
            }
        }

        private void GetDataToGrid(String data)
        {
            saveDataSave = false;

            if (data.Equals(">") || data.Equals(":"))
            {
                if (saveDataFirst == false)
                {
                    saveDataSave = true;
                }
                saveDataFirst = false;
            }

            if (saveDataSave)
            {
                int cellGridCount = 0;
                String byteToGrid = "";
                for (int i = 0; i < bytesList.Count; i++)
                {
                    if (byteCount >= 2)
                    {
                        cellGridCount++;
                        byteToGrid = "";
                        byteCount = 0;
                    }
                    if (cellGridCount < 8)
                    {
                        byteToGrid = byteToGrid + bytesList[i].ToString();
                        gridMemorySwicher[cellGridCount, 0].Value = byteToGrid;
                    }
                    else
                    {
                        byteToGrid = byteToGrid + bytesList[i].ToString();
                        gridMemorySwicher[(cellGridCount - 8), 1].Value = byteToGrid;
                    }
                    byteCount++;

                }
            }

            if (saveDataFirst == false)
            {
                if (data.Equals(":") || data.Equals(" ") || data.Equals("\r") || data.Equals("\n") || data.Equals(">"))
                {
                    return;
                }
                else
                {
                    bytesList.Add(data);
                }
            }
        }

        private void ResetVariables()
        {
            bytesString = null;
            saveDataSave = true;
            saveDataFirst = true;
            byteCount = 0;
        }

        private void VerifyIntContent(TextBox text)
        {
            int Message = 0;
            try
            {
                Convert.ToInt64(text.Text);
                Message = 0;
            }
            catch (Exception)
            {
                Message++;
            }

            if (text.Text != "" && Message == 1)
            {
                Message++;
                text.Text = null;
                MessageBox.Show("Please insert only numeric characters with max 19 digits", "Invalid Data");
            }
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

        private void PrintData(String data)
        {
            txtUatMessage.AppendText(data);
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

        #region Eventos da Interface Grafica

        private void FrmLoadFlightSoftUat_Load(object sender, EventArgs e)
        {
            cmbCharge();
        }

        private void btFileExFile_Click(object sender, EventArgs e)
        {
            String path = "";
            String dialogExtension = "";
            String dialogName = "";

            dialogExtension = ".hex";
            dialogName = "Hexadecimal Files";

            fileDialog.Filter = dialogName + "|*" + dialogExtension + "|All Files|*.*";
            fileDialog.FileName = path;
            fileDialog.FilterIndex = 0;

            /** 
             * @attention 
             * Essa linha pode dar erro (apenas) durante a depuracao, caso se tente abrir um arquivo .udl a partir
             * do dialogo. Para evitar isso, deve-se desmarcar a opcao "throw" para o erro "LoaderLock", na opcao
             * "Managed Debugging Assistants", acessado pelo menu "Debug->Exceptions" do Visual Studio.
             **/
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                btSendSoft.Enabled = true;

                txtFilePath.Text = fileDialog.FileName;
            }
        }

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

        private void btSendSoft_Click(object sender, EventArgs e)
        {
            if (ConnectToSerialPort())
            {

                frmProgressBar = new FrmLoadFlightSoftProgressBar();
                frmProgressBar.MdiParent = this.MdiParent;
                frmProgressBar.Show();
                frmProgressBar.Text = "Loading Software To UAT";
                PrepareAndSend(true);
                PrepareAndSend(false);

                try
                {
                    frmProgressBar.Close();
                    frmProgressBar.Dispose();
                }
                catch (Exception)
                {
                }

                MessageBox.Show("UAT software successfully sent!",
                "Data Sent",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);


                serial.Close();
            }
            else
            {
                MessageBox.Show("ERRO");
            }
        }

        private void btRunSw_Click(object sender, EventArgs e)
        {
            byte[] bytesToSend = null;

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            bytesToSend = encoding.GetBytes("G " + txtAddress.Text + "\r\n" );

            if (!ConnectToSerialPort())
            {
                return;
            }

            for (int i = 0; i < bytesToSend.Length; i++)
            {
                serial.Write(bytesToSend, i, 1);
                Thread.Sleep(13);
            }
        }
        
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            VerifyIntContent(txtAddress); 
        }
        
        private void txtNumOfBytes_TextChanged(object sender, EventArgs e)
        {
            VerifyIntContent(txtNumOfBytes);            
        }
        
        private void txtInitialAddress_TextChanged(object sender, EventArgs e)
        {
            VerifyIntContent(txtInitialAddress);
        }

        private void txtAddressMemBlock_TextChanged(object sender, EventArgs e)
        {
            VerifyIntContent(txtAddressMemBlock);
        }

        private void btStartListenUat_Click(object sender, EventArgs e)
        {
            if (btStartListenUat.Text == "Start Listen UAT")
            {
                btStartListenUat.Text = "Stop Listen UAT";

                if (!ConnectToSerialPort())
                {
                    return;
                }

                txtMessageToSendUat.Enabled = true;
                btSendCommand.Enabled = true;
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

                txtMessageToSendUat.Enabled = false;
                btSendCommand.Enabled = false;
                btClearScreen.Enabled = false;

                btStartListenUat.Text = "Start Listen UAT";
            }
        }        
        
        private void btSendCommand_Click(object sender, EventArgs e)
        {
            byte[] bytesToSend = null;

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            bytesToSend = encoding.GetBytes(txtMessageToSendUat.Text + "\r");

            if (ConnectToSerialPort())
            {

                for (int i = 0; i < bytesToSend.Length; i++)
                {
                    serial.Write(bytesToSend, i, 1);
                    Thread.Sleep(13);
                }
            }
        }
        
        private void btClearScreen_Click(object sender, EventArgs e)
        {
            txtUatMessage.Clear();
        }
        
        private void btDestFile_Click(object sender, EventArgs e)
        {
           
        } 
        
        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            serial.Close();
            serial.Dispose();
        }

        private void btReadMemory_Click(object sender, EventArgs e)
        {
            serial.Close();

            saveDialog.InitialDirectory = Properties.Settings.Default.flight_sw_file_path;
            saveDialog.Title = "Export Bytes";
            saveDialog.Filter = "All Files (*.*)|*.*";
            saveDialog.FileName = "Memory " + txtInitialAddress.Text + " - " + Convert.ToString((Convert.ToInt32(txtNumOfBytes.Text) + (Convert.ToInt32(txtInitialAddress.Text)))) + ".txt";
            saveDialog.FilterIndex = 0;

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            else
            {
                if (ConnectToSerialPort())
                {
                    printReceivedDataCallBack = new AvailableReceivedDataCallBack(SaveData);
                    serial.DataReceived += new SerialDataReceivedEventHandler(SerialRead);


                    Int32 NumOfBytes = Convert.ToInt32(txtNumOfBytes.Text);

                    writeFile = new FileHandling(saveDialog.FileName, true, false);

                    writeFile.CreateNewFile();

                    serial.Write("R " + txtInitialAddress.Text + " " + NumOfBytes.ToString("X2") + "\r");
                    
                    writeFile.CloseWriterFile();

                    ResetVariables();
                    
                    MessageBox.Show("Memory Image File saved successfully!",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                    serial.Close();
                }
            }
        }        
        
        private void btGetData_Click(object sender, EventArgs e)
        {
            if (!ConnectToSerialPort())
            {
                return;
            }

            ResetVariables();

            gridMemorySwicher.Rows.Clear();

            bytesList = new List<String>();

            printReceivedDataCallBack = new AvailableReceivedDataCallBack(GetDataToGrid);
            serial.DataReceived += new SerialDataReceivedEventHandler(SerialRead);

            gridMemorySwicher.Enabled = true;
            btWriteMemory.Enabled = true;

            gridMemorySwicher.Rows.Add(2);
            
            serial.Write("R " + txtAddressMemBlock.Text + " " + "10" + "\r");

            ResetVariables();            

        }
        
        private void btWriteMemory_Click(object sender, EventArgs e)
        {            
            serial.DataReceived -= new SerialDataReceivedEventHandler(SerialRead);
            printReceivedDataCallBack = null;

            serial.Close();
            serial.Dispose();
            ResetVariables();

            String writeData = "";

            if (!ConnectToSerialPort())
            {
                return;
            }

            byte[] bytesToSend = null;

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            bytesToSend = encoding.GetBytes("W " + txtAddressMemBlock.Text + " 10" + "\r");

            for (int i = 0; i < bytesToSend.Length; i++)
            {
                serial.Write(bytesToSend, i, 1);
                Thread.Sleep(13);
            }                      

            for (int i = 0; i < 16; i++)
            {
                if (i < 8)
                {
                    writeData = writeData + gridMemorySwicher[i, 0].Value.ToString() + " ";
                }
                else
                {
                    writeData = writeData + gridMemorySwicher[(i - 8), 1].Value.ToString() + " ";
                }
            }

            bytesToSend = null;

            bytesToSend = encoding.GetBytes(writeData + "\r");

            for (int i = 0; i < bytesToSend.Length; i++)
            {
                serial.Write(bytesToSend, i, 1);
                Thread.Sleep(13);
            }

            serial.Close();
            serial.Dispose();
            ResetVariables();

            MessageBox.Show("Memory Image Block saved successfully!",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }

        #endregion

    }
}
        
    
    


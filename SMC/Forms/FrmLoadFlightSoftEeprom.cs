/**
 * @file 	    FrmLoadFlightSoftEeprom.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    28/09/2012
 * @note	    Modificado em 20/12/2012 por Ayres.
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
using System.IO;
using System.Threading;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmLoadFlightSoftEeprom
     * Formulario de cadastro de Subtypes.
     **/
    public partial class FrmLoadFlightSoftEeprom : DockContent
    {

        #region Atributos Privados

        private byte[] bytesToSend = null;
        private UInt32 sizeSoftware;
        private UInt32 checkSum;
        private byte[] intBytesSize = null;
        private FrmLoadFlightSoftProgressBar frmProgressBar = null;
        private byte[] finalBytesToSend = null;

        #endregion

        #region Atributos Publicos

        public bool offlineMode;

        #endregion

        #region Construtor

        public FrmLoadFlightSoftEeprom()
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

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void ReadAndPrepareFileExe(String path)
        {
            try
            {
                FileStream FluxoDeAquivo = new FileStream(@path, FileMode.Open);

                BinaryReader LeitorDeFluxo = new BinaryReader(FluxoDeAquivo);

                // Cria array de bytes com dados do arquivo
                byte[] dados = LeitorDeFluxo.ReadBytes(Convert.ToInt32(FluxoDeAquivo.Length));

                Int32 SizeInt = (Convert.ToInt32(FluxoDeAquivo.Length));

                byte[] BytesSizeSWApp = BitConverter.GetBytes(SizeInt);

                //O codigo abaixo inverte as words
                byte[] tempBytesSizeSWApp = new byte[BytesSizeSWApp.Length];

                int invert = 0;

                for (int i = 3; i < BytesSizeSWApp.Length; i += 4)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        tempBytesSizeSWApp[invert] = BytesSizeSWApp[i - j];
                        invert++;
                    }
                }

                BytesSizeSWApp = tempBytesSizeSWApp;

                finalBytesToSend = new byte[BytesSizeSWApp.Length + FluxoDeAquivo.Length];

                BytesSizeSWApp.CopyTo(finalBytesToSend, 0);

                dados.CopyTo(finalBytesToSend, BytesSizeSWApp.Length);


                int aux = 0;

                UInt32 word00 = 0;
                UInt32 word01 = 0;
                UInt32 word02 = 0;
                UInt32 word03 = 0;

                byte byte00 = 0;
                byte byte01 = 0;
                byte byte02 = 0;
                byte byte03 = 0;

                UInt32 checksumaux = 0;
                UInt32 checksumfinal = 0;


                for (int i = 4; i < finalBytesToSend.Length; i++)
                {
                    if (aux == 0)
                    {
                        byte00 = finalBytesToSend[i];
                        word00 = byte00;
                        word00 = word00 << 24;
                        aux++;
                    }
                    else if (aux == 1)
                    {
                        byte01 = finalBytesToSend[i];
                        word01 = byte01;
                        word01 = word01 << 16;
                        aux++;
                    }
                    else if (aux == 2)
                    {
                        byte02 = finalBytesToSend[i];
                        word02 = byte02;
                        word02 = word02 << 8;
                        aux++;
                    }
                    else if (aux == 3)
                    {
                        byte03 = finalBytesToSend[i];
                        word03 = byte03;


                        checksumaux = word03;
                        checksumaux |= word02;
                        checksumaux |= word01;
                        checksumaux |= word00;


                        checksumfinal = checksumfinal + checksumaux;

                        aux = 0;
                    }
                }

                long imagelegth = FluxoDeAquivo.Length;

                txtImageCrc.Text = checksumfinal.ToString("X8");
                txtImageLength.Text = imagelegth.ToString();

                LeitorDeFluxo.Close();
                FluxoDeAquivo.Close();

            }
            catch (Exception)
            {
            }


        }

        private byte[] ReadAndPrepareFileSrec(ref UInt32 sizeSW, ref UInt32 valueChecksum, String readPath)
        {
            try
            {
                finalBytesToSend = null;
                byte[] byteToSendSerial = new byte[512 * 1024];

                UInt32 checksum = 0;
                string hexStringword = "";
                UInt32 numword = 0;

                StreamReader objReader = new StreamReader(readPath);
                //StreamWriter objWrite = new StreamWriter("D:\\Result.txt");

                String sLine = "";
                String sizeOfprogram = "";
                UInt32 sizeprogram = 0;
                UInt32 i = 0;

                UInt32 counterBytes = 0;

                while ((sLine = objReader.ReadLine()) != null)
                {
                    i++;

                    if (sLine.Substring(0, 3).Equals("S31"))
                    {
                        hexStringword = sLine.Substring(12, 8);
                        numword = UInt32.Parse(hexStringword, System.Globalization.NumberStyles.HexNumber);

                        checksum += numword; // word01

                        byte[] intBytes = BitConverter.GetBytes(numword);

                        int intBytesLenght = intBytes.Length - 1;


                        for (int t = 0; t < 4; t++)
                        {
                            byteToSendSerial[counterBytes + t] = intBytes[intBytesLenght];
                            intBytesLenght--;
                        }

                        counterBytes += 4; // soma mais 4 bytes!
                        hexStringword = sLine.Substring(20, 8);
                        numword = UInt32.Parse(hexStringword, System.Globalization.NumberStyles.HexNumber);
                        checksum += numword; // word02
                        intBytes = BitConverter.GetBytes(numword);

                        intBytesLenght = intBytes.Length - 1;


                        for (int t = 0; t < 4; t++)
                        {
                            byteToSendSerial[counterBytes + t] = intBytes[intBytesLenght];
                            intBytesLenght--;
                        }

                        counterBytes += 4; // soma mais 4 bytes!

                        hexStringword = sLine.Substring(28, 8);
                        numword = UInt32.Parse(hexStringword, System.Globalization.NumberStyles.HexNumber);
                        checksum += numword; // word03
                        intBytes = BitConverter.GetBytes(numword);

                        intBytesLenght = intBytes.Length - 1;


                        for (int t = 0; t < 4; t++)
                        {
                            byteToSendSerial[counterBytes + t] = intBytes[intBytesLenght];
                            intBytesLenght--;
                        }

                        counterBytes += 4; // soma mais 4 bytes!

                        hexStringword = sLine.Substring(36, 8);
                        numword = UInt32.Parse(hexStringword, System.Globalization.NumberStyles.HexNumber);
                        checksum += numword; // word04
                        intBytes = BitConverter.GetBytes(numword);

                        intBytesLenght = intBytes.Length - 1;


                        for (int t = 0; t < 4; t++)
                        {
                            byteToSendSerial[counterBytes + t] = intBytes[intBytesLenght];
                            intBytesLenght--;
                        }

                        counterBytes += 4; // soma mais 4 bytes!
                    }
                }

                StreamReader newobjReader = new StreamReader(readPath);
                UInt32 j = 0;

                while ((sLine = newobjReader.ReadLine()) != null)
                {
                    j++;
                    if (j == (i - 1))
                    {
                        sizeOfprogram = sLine.Substring(4, 8);
                        sizeprogram = UInt32.Parse(sizeOfprogram, System.Globalization.NumberStyles.HexNumber);
                        sizeprogram = sizeprogram - 0x02000000;
                        sizeOfprogram = Convert.ToString(sizeprogram, 16);
                        sizeOfprogram = sizeOfprogram.ToUpper();
                    }
                }

                string checksumfinal = Convert.ToString(checksum, 16);
                checksumfinal = checksumfinal.ToUpper();

                objReader.Close();
                newobjReader.Close();

                sizeSW = counterBytes;
                valueChecksum = checksum;

                byte[] finalbyteToSendSerial = new byte[sizeprogram];
                Array.Copy(byteToSendSerial, finalbyteToSendSerial, sizeprogram);
                return finalbyteToSendSerial;
            }
            catch (Exception)
            {
                MessageBox.Show("Error to read and prepare file!",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return null;
            }
        }

        #endregion

        #region Eventos da Interface Grafica

        private void FrmLoadFlightSoftEeprom_Load(object sender, EventArgs e)
        {
            cmbCharge();
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

        private void btFileExFile_Click(object sender, EventArgs e)
        {
            String path = "";
            String dialogExtension = "";
            String dialogName = "";

            if (rdExFile.Checked)
            {
                dialogExtension = ".exe";
                dialogName = "Executable Files";
            }
            else if (rdSrecFile.Checked)
            {
                dialogExtension = ".srec";
                dialogName = "SREC Files";
            }

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
                btExportBytes.Enabled = true;

                if (rdSrecFile.Checked)
                {
                    //Verificar se arquivo eh realmente um SREC
                    if (!FileIsSrec(fileDialog.FileName))
                    {
                        return;
                    }

                    txtExFile.Text = fileDialog.FileName;

                    // Ler o arquivo, prepara-lo para envio e obter seu tamanho e checksum

                    bytesToSend = ReadAndPrepareFileSrec(ref sizeSoftware, ref checkSum, txtExFile.Text);

                    if (bytesToSend == null)
                    {
                        return;
                    }

                    // Formatar e setar os valores do checksum e tamnho, obtidos por meio do ReadAndPrepareFile
                    String valueChecksum = Convert.ToString(checkSum, 16);
                    txtImageCrc.Text = valueChecksum.ToUpper();

                    intBytesSize = BitConverter.GetBytes(sizeSoftware);

                    //O codigo abaixo inverte as words
                    byte[] tempIntBytesSize = new byte[intBytesSize.Length];

                    int invert = 0;

                    for (int i = 3; i < intBytesSize.Length; i += 4)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            tempIntBytesSize[invert] = intBytesSize[i - j];
                            invert++;
                        }
                    }

                    intBytesSize = tempIntBytesSize;

                    txtImageLength.Text = sizeSoftware.ToString();
                }
                else if (rdExFile.Checked)
                {
                    txtExFile.Text = fileDialog.FileName;
                    ReadAndPrepareFileExe(fileDialog.FileName);
                }
            }
        }

        private bool FileIsSrec(String filePath)
        {
            try
            {
                StreamReader readFileSrec = File.OpenText(filePath);
                String line = readFileSrec.ReadLine();

                //if (!line.Contains("5254454D535F496E746"))
                //{
                //    return false;
                //}

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error to verify wheter file is SREC format!\n\nPlease check your file.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
        }

        public void SendFlightSoftware(byte[] finalBytesToSend)
        {
            ConnectToSerialPort();
            serial.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            try
            {
                frmProgressBar = new FrmLoadFlightSoftProgressBar();
                frmProgressBar.MdiParent = this.MdiParent;
                frmProgressBar.Show();

                frmProgressBar.progressBar.Maximum = finalBytesToSend.Length;

                for (int i = 0; i < finalBytesToSend.Length; i++)
                {
                    serial.Write(finalBytesToSend, i, 1);

                    if (i % 48 == 0)
                    {
                        Thread.Sleep(100);
                    }

                    frmProgressBar.progressBar.Value = i;
                }


                try
                {
                    frmProgressBar.Close();
                    frmProgressBar.Dispose();
                }
                catch
                {
                }

                MessageBox.Show("Flight software successfully sent!",
                                "Data Sent",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Error to send software to serial!\n\nPlease check your configuration.",
                                "Error to Send",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                try
                {
                    frmProgressBar.Close();
                    frmProgressBar.Dispose();
                }
                catch
                {
                }
            }
        }

        private void btSendSoft_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dear user, prepare the UPC to receive the flight software!!",
                                    "Waiting to send",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            if (rdSrecFile.Checked)
            {
                //adiciona os 4 bytes que indicam o tamanho do arquivo
                finalBytesToSend = new byte[intBytesSize.Length + bytesToSend.Length];
                Array.Copy(intBytesSize, finalBytesToSend, intBytesSize.Length);
                Array.Copy(bytesToSend, 0, finalBytesToSend, intBytesSize.Length, bytesToSend.Length);


                SendFlightSoftware(finalBytesToSend);
            }
            else if (rdExFile.Checked)
            {
                SendFlightSoftware(finalBytesToSend);
            }

        }

        private void btExportBytes_Click(object sender, EventArgs e)
        {
            try
            {
                String fileName = "";

                if (rdExFile.Checked)
                {
                    fileName = "BytesEXE.dat";
                }
                else if (rdSrecFile.Checked)
                {
                    fileName = "BytesSREC.dat";
                }

                saveDialog.InitialDirectory = Properties.Settings.Default.flight_sw_file_path;
                saveDialog.Title = "Export Bytes";
                saveDialog.Filter = "All Files (*.*)|*.*";
                saveDialog.FileName = fileName;
                saveDialog.FilterIndex = 0;

                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                FileStream fileStream = new System.IO.FileStream(saveDialog.FileName,
                                                                 System.IO.FileMode.Create,
                                                                 System.IO.FileAccess.Write);

                fileStream.Write(finalBytesToSend, 0, finalBytesToSend.Length);
                fileStream.Close();

                MessageBox.Show("Flight Software Image File saved successfully!",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {

            }
        }
        #endregion
        
    }
}

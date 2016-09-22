/**
 * @file 	    FrmCommRS422.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este formulario eh usado para testes da comunicacao serial RS422 com a UTMC.
 * @author 	    Thiago Duarte Pereira
 * @date	    26/06/2010
 * @note	    Modificado em 03/10/2011 por Fabricio.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmCommRS422
     * Formulario usado para recepcao, gravacao e analise dos frames de TM recebidos do OBDH.
     **/
    public partial class FrmCommRS422 : DockContent
    {
        #region Variaveis Globais
        
        private SubordRS422 rs422 = new SubordRS422();
        private FileHandling readFile = null;
        private FileHandling writeFile = null;

        // Disponibiliza os dados recebidos para serem usados pela Thread principal da interface grafica.
        // Deve ser usado somente na necessidade de escrita dos dados na interface grafica ou em arquivos.
        delegate void AvailableReceivedDataCallBack(byte[] rxBuffer, bool frameIsValid);
        delegate void AvailableReceivedPacketCallBack(byte[] packet, int numBytes);

        // Declaracao do CallBack.
        private AvailableReceivedDataCallBack printFramesCallBack = null;
        private AvailableReceivedPacketCallBack printPacketsCallBack = null;
        
        private object[] frameArgs = new object[2];
        private object[] packetArgs = new object[2];

        //private TmDecoder tmDecoder = new TmDecoder();
        private bool receiving = false;
        private FrmConnectionWithEgse frmConnectionWithEgse = null;
        private FrmFramesCoding frmFramesCoding = null;
        private MdiMain mdiMain = null;
        private int frameToBeSent = 1;

        #endregion

        #region Construtor

        public FrmCommRS422(MdiMain mdi)
        {
            InitializeComponent();
            mdiMain = mdi;
        }

        #endregion

        #region Propriedades

        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            set
            {
                frmConnectionWithEgse = value;
            }
            get
            {
                return frmConnectionWithEgse;
            }
        }

        public FrmFramesCoding FormFramesCoding
        {
            set
            {
                frmFramesCoding = value;
            }
            get
            {
                return frmFramesCoding;
            }
        }

        public bool Receiving
        {
            set
            {
                receiving = value;
            }
            get
            {
                return receiving;
            }
        }

        public FileHandling ReadFile
        {
            set
            {
                readFile = value;
            }
        }

        public int FrameToBeSent
        {
            set
            {
                frameToBeSent = value;
            }
            get
            {
                return frameToBeSent;
            }
        }

        #endregion

        #region Eventos da Interface

        private void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                btSend.Text = "Sending Data";
                btSend.Enabled = false;

                if (!File.Exists(txtDataSendPath.Text))
                {
                    MessageBox.Show("The data send file path is invalid!",
                                    "File Path Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    return;
                }

                if (readFile == null)
                {
                    readFile = new FileHandling(txtDataSendPath.Text, true, false);
                    readFile.OpenReaderFile();
                }

                object cltu = readFile.ReadPacketLine();

                if (cltu == null) // fim de arquivo. Iniciar novamente.
                {
                    lblFrameToBeSent.Text = ":: " + frameToBeSent + "º ::";
                    readFile.CloseReaderFile();
                    readFile.OpenReaderFile();
                    cltu = readFile.ReadPacketLine();

                    if (cltu == null)
                    {
                        MessageBox.Show("This file hasn´t packets!!",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                bool sended = false;

                sended = frmConnectionWithEgse.SendCltu(Utils.Formatting.HexStringToByteArray(cltu.ToString()));

                Thread.Sleep(700); // Aguarda menos de 1 segundo para habilitar o botao

                if (sended)
                {
                    lblDataSended.Text = cltu.ToString();
                    frameToBeSent++;

                    if (frameToBeSent > readFile.NumLinesWithData)
                    {
                        frameToBeSent = 1;
                        lblFrameToBeSent.Text = ":: " + frameToBeSent + "º ::";
                    }
                    else
                    {
                        lblFrameToBeSent.Text = ":: " + frameToBeSent + "º ::";
                    }
                }
                else
                {
                    lblDataSended.Text = "Last message not sended!";
                }

                btSend.Enabled = true;
                btSend.Text = "Send Data";
            }
            catch
            {
                btSend.Enabled = true;
                btSend.Text = "Send Data";
            }
        }

        private void btFileRead_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open data send file";
            openFileDialog.InitialDirectory = Properties.Settings.Default.file_send_data_path;
            openFileDialog.FileName = "";
            openFileDialog.Filter = "TEXT Files (*.txt)|*.txt|CLTU Files (*.cltu)|*.cltu|TC Packets Files (*.tc)|*.tc|ALL Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName.Equals(txtSaveInFilePath.Text))
                {
                    MessageBox.Show("This file already being used to receive data!\n\nCorrect it and try again.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                    return;
                }

                txtDataSendPath.Text = openFileDialog.FileName;

                // salvar caminho do arquivo
                Properties.Settings.Default.file_send_data_path = openFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void btFileWrite_Click(object sender, EventArgs e)
        {
            newFileDialog.Title = "Open received data file";
            newFileDialog.InitialDirectory = Properties.Settings.Default.file_save_path;
            newFileDialog.FileName = "";
            newFileDialog.Filter = "All Files (*.*)|*.*";
            newFileDialog.FilterIndex = 0;

            if (newFileDialog.ShowDialog() == DialogResult.OK)
            {
                writeFile = new FileHandling(newFileDialog.FileName, true, false);

                if (!writeFile.FileExists())
                {
                    writeFile.CreateNewFile();
                }

                txtSaveInFilePath.Text = newFileDialog.FileName;

                // salvar caminho do arquivo
                Properties.Settings.Default.file_save_path = newFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }
        
        private void chkRecordInFile_CheckedChanged(object sender, EventArgs e)
        {
            rbRecordHexRepresentation.Enabled = chkRecordInFile.Checked;
            rbRecordBinaryRepresentation.Enabled = chkRecordInFile.Checked;
            btSaveInFile.Enabled = chkRecordInFile.Checked;
            txtSaveInFilePath.Enabled = chkRecordInFile.Checked;
        }

        private void chkDataSendFile_CheckedChanged(object sender, EventArgs e)
        {
            btSendData.Enabled = chkDataSendFile.Checked;
            txtDataSendPath.Enabled = chkDataSendFile.Checked;
        }
        
        public void btEnableReception_Click_1(object sender, EventArgs e)
        {
            if (btEnableReception.Text.Equals("Get Data"))
            {
                if (chkRecordInFile.Checked)
                {
                    if (!File.Exists(txtSaveInFilePath.Text))
                    {
                        MessageBox.Show("The file path of received data record is invalid!",
                                "File Path Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                        return;
                    }

                    // Se nao for "null", significa que o arquivo ja instanciado pode dar continuidade no registro dos dados.
                    if (writeFile == null)
                    {
                        writeFile = new FileHandling(txtSaveInFilePath.Text, false, false);
                    }

                    writeFile.OpenWriterFile();
                }

                if (frmConnectionWithEgse.rbFrames.Checked)
                {
                    frmConnectionWithEgse.availableFrameEventHandler += new AvailableFrameEventHandler(AvailableFrame);

                    // Este eh um delegate que recebe como parametro o metodo que recebe e processa os argumentos.
                    // Eh utilizado somente para imprimir os dados disponibilizados pelo Event na interface grafica ou em arquivos.
                    // Para ser executado, o mesmo deve ser transferido para a Thread principal, ou seja, a Thread que roda a interface grafica.
                    printFramesCallBack = new AvailableReceivedDataCallBack(SendDataToMainThread);
                }
                else
                {
                    frmConnectionWithEgse.availablePacketEventHandler += new AvailablePacketEventHandler(AvailablePacket);
                    printPacketsCallBack = new AvailableReceivedPacketCallBack(SendPacketToMainThread);
                }

                btEnableReception.Text = "STOP Getting Data";
                receiving = true;
                chkRecordInFile.Enabled = false;
                rbRecordBinaryRepresentation.Enabled = false;
                rbRecordHexRepresentation.Enabled = false;
                txtSaveInFilePath.Enabled = false;
                btSaveInFile.Enabled = false;

                // Desabilitar o botao da tela TMDecoder, se estiver aberta
                if (mdiMain.FormFramesCoding != null)
                {
                    frmFramesCoding = mdiMain.FormFramesCoding;
                    frmFramesCoding.btRealTimeReception.Enabled = false;
                    frmFramesCoding.chkShowInRealTime.Enabled = false;
                }

                btClear_Click(null, new EventArgs());
            }
            else
            {
                frmConnectionWithEgse.availableFrameEventHandler -= new AvailableFrameEventHandler(AvailableFrame);
                frmConnectionWithEgse.availablePacketEventHandler -= new AvailablePacketEventHandler(AvailablePacket);

                printFramesCallBack = null;
                printPacketsCallBack = null;
                
                if (chkRecordInFile.Checked && (writeFile != null))
                {
                    // Fecha o arquivo de escrita
                    writeFile.CloseWriterFile();
                    writeFile = null;
                }

                btEnableReception.Text = "Get Data";
                receiving = false;
                chkRecordInFile.Enabled = true;
                rbRecordBinaryRepresentation.Enabled = chkRecordInFile.Checked;
                rbRecordHexRepresentation.Enabled = chkRecordInFile.Checked;
                txtSaveInFilePath.Enabled = chkRecordInFile.Checked;
                btSaveInFile.Enabled = chkRecordInFile.Checked;

                // Habilitar o botao da tela TMDecoder, se estiver aberta
                if (mdiMain.FormFramesCoding != null)
                {
                    frmFramesCoding = mdiMain.FormFramesCoding;
                    frmFramesCoding.btRealTimeReception.Enabled = true;
                    frmFramesCoding.chkShowInRealTime.Enabled = true;
                }
            }
        }

        private void FrmCommRS422_Load_1(object sender, EventArgs e)
        {
            if (mdiMain.FormConnectionWithEgse != null)
            {
                frmConnectionWithEgse = mdiMain.FormConnectionWithEgse;

                if ((frmConnectionWithEgse.Connected == true) && 
                    (frmConnectionWithEgse.rbRs422.Checked == true))
                {
                    // Se o SMC estiver conectado, entao deve verificar se a tela FramesCoding esta recebendo para nao permitir que duas telas recebam dados.
                    // Enquanto uma recebe a outra deve estar desabilitada para recepcao.
                    if (mdiMain.FormFramesCoding != null)
                    {
                        frmFramesCoding = mdiMain.FormFramesCoding;

                        if (frmFramesCoding.ReceptionInRealTime)
                        {
                            btEnableReception.Enabled = false;
                        }
                        else
                        {
                            btEnableReception.Enabled = true;

                            if (chkDataSendFile.Checked)
                            {
                                btSend.Enabled = true;
                                lblFrameToBeSent.Text = ":: " + frameToBeSent + "º ::";
                                lblFrameToBeSent.Enabled = true;
                            }
                            else
                            {
                                btSend.Enabled = false;
                                lblFrameToBeSent.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        btEnableReception.Enabled = true;
                        btSend.Enabled = true;
                    }
                }
                else
                {
                    btEnableReception.Enabled = false;
                    btSend.Enabled = false;
                }
            }

            // carregar file paths salvos nas propriedades
            txtSaveInFilePath.Text = Properties.Settings.Default.file_save_path;
            txtDataSendPath.Text = Properties.Settings.Default.file_send_data_path;
        }

        private void chkRecordInFile_CheckedChanged_1(object sender, EventArgs e)
        {
            btSaveInFile.Enabled = chkRecordInFile.Checked;
            rbRecordBinaryRepresentation.Enabled = chkRecordInFile.Checked;
            rbRecordHexRepresentation.Enabled = chkRecordInFile.Checked;
            txtSaveInFilePath.Enabled = chkRecordInFile.Checked;

            chkShowFramesReception.Checked = false;
            ckbBreakLine.Checked = false;
            chkShowFramesReception.Enabled = !chkRecordInFile.Checked;
            ckbBreakLine.Enabled = !chkRecordInFile.Checked;
        }

        private void chkDataSendFile_CheckedChanged_1(object sender, EventArgs e)
        {
            txtDataSendPath.Enabled = chkDataSendFile.Checked;
            btSendData.Enabled = chkDataSendFile.Checked;

            if (chkDataSendFile.Checked && frmConnectionWithEgse.Connected)
            {
                btSend.Enabled = true;
                lblFrameToBeSent.Enabled = true;
                lblFrameToBeSent.Text = ":: " + frameToBeSent + "º ::";
            }
            else
            {
                btSend.Enabled = false;
                lblFrameToBeSent.Enabled = false;
            }
        }

        private void btSaveInFile_Click(object sender, EventArgs e)
        {
            newFileDialog.Title = "Open received data file";
            newFileDialog.InitialDirectory = Properties.Settings.Default.file_save_path;
            newFileDialog.FileName = "";
            newFileDialog.Filter = "All Files (*.*)|*.*";
            newFileDialog.FilterIndex = 0;

            if (newFileDialog.ShowDialog() == DialogResult.OK)
            {
                writeFile = new FileHandling(newFileDialog.FileName, true, false);

                if (!writeFile.FileExists())
                {
                    writeFile.CreateNewFile();
                }

                txtSaveInFilePath.Text = newFileDialog.FileName;

                // salvar caminho do arquivo
                Properties.Settings.Default.file_save_path = newFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void btSendData_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open data send file";
            openFileDialog.InitialDirectory = Properties.Settings.Default.file_send_data_path;
            openFileDialog.FileName = "";
            openFileDialog.Filter = "TEXT Files (*.txt)|*.txt|CLTU Files (*.cltu)|*.cltu|TC Packets Files (*.tc)|*.tc|ALL Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtDataSendPath.Text = openFileDialog.FileName;
                lblDataSended.Text = "";

                readFile = new FileHandling(txtDataSendPath.Text, true, false);
                readFile.OpenReaderFile();

                // salvar caminho do arquivo
                Properties.Settings.Default.file_send_data_path = openFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void FrmCommRS422_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(1101, (682) - 15);
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }

        #endregion

        #region Metodos Privados
        
        /**
         * Metodo executado via delegate sempre que o Invoke ser executado.
         * Aqui os dados ja estao na Thread principal, podendo ser escritos tanto na interface grafica quanto em arquivos.
         **/
        private void SendDataToMainThread(byte[] rxBuffer, bool frameIsValid)
        {
            if (chkShowFramesReception.Checked)
            {
                if (ckbBreakLine.Checked)
                {
                    txtReceive.AppendText("\r\n" + Utils.Formatting.ConvertByteArrayToHexString(rxBuffer, rxBuffer.Length));
                }
                else
                {
                    txtReceive.AppendText(Utils.Formatting.ConvertByteArrayToHexString(rxBuffer, rxBuffer.Length));
                }

                if (ckbBreakLine.Checked)
                {
                    txtReceive.AppendText("\r\n\r\n" + Utils.Formatting.ConvertByteArrayToHexString(rxBuffer, rxBuffer.Length));
                }
                else
                {
                    txtReceive.AppendText(Utils.Formatting.ConvertByteArrayToHexString(rxBuffer, rxBuffer.Length));
                }
            }

            if (chkRecordInFile.Checked)
            {
                if (rbRecordHexRepresentation.Checked)
                {
                    writeFile.Append(Utils.Formatting.ConvertByteArrayToHexString(rxBuffer, rxBuffer.Length));
                }
                else
                {
                    writeFile.Append(Utils.Formatting.ConvertByteArrayToASCIIBinary(rxBuffer, rxBuffer.Length));
                }
            }
        }

        /**
         * Metodo executado via delegate sempre que o Invoke ser executado.
         * Aqui os dados ja estao na Thread principal, podendo ser escritos tanto na interface grafica quanto em arquivos.
         **/
        private void SendPacketToMainThread(byte[] packet, int numBytes)
        {
            if (chkShowFramesReception.Checked)
            {
                if (ckbBreakLine.Checked)
                {
                    txtReceive.AppendText("\r\n" + Utils.Formatting.ConvertByteArrayToHexString(packet, numBytes));
                }
                else
                {
                    txtReceive.AppendText(Utils.Formatting.ConvertByteArrayToHexString(packet, numBytes));
                }

                if (ckbBreakLine.Checked)
                {
                    txtReceive.AppendText("\r\n\r\n" + Utils.Formatting.ConvertByteArrayToHexString(packet, numBytes));
                }
                else
                {
                    txtReceive.AppendText(Utils.Formatting.ConvertByteArrayToHexString(packet, numBytes));
                }

                if (chkRecordInFile.Checked)
                {
                    if (rbRecordHexRepresentation.Checked)
                    {
                        writeFile.Write(Utils.Formatting.ConvertByteArrayToHexString(packet, numBytes));
                    }
                    else
                    {
                        writeFile.Write(Utils.Formatting.ConvertByteArrayToASCIIBinary(packet, numBytes));
                    }
                }
            }
        }
        
        /**
         * Este metodo eh executado sempre que um frame for disponibilizado pela tela de conexao.
         **/
        private void AvailableFrame(object sender, AvailableFrameEventArgs eventArgs)
        {
            frameArgs[0] = eventArgs.Frame;
            frameArgs[1] = eventArgs.FrameValid;

            Invoke(printFramesCallBack, frameArgs);
            Array.Clear(frameArgs, 0, 2);
        }

        /**
         * Este metodo eh executado sempre que qualquer tipo de mensagem for disponibilizado pela tela de conexao.
         **/
        private void AvailablePacket(object sender, AvailablePacketEventArgs eventArgs)
        {
            packetArgs[0] = eventArgs.Packet;
            packetArgs[1] = eventArgs.NumBytes;

            Invoke(printPacketsCallBack, packetArgs);
            Array.Clear(packetArgs, 0, 2);
        }

        #endregion
    }
}
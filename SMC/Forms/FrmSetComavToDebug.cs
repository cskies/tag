/**
 * @file 	    FrmSetComavToDebug.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    07/08/2012
 * @note	    Modificado em 02/07/2013 por Ayres.
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
using System.IO.Ports;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Threading;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSetComavToDebug
     * Este Formulario eh usado para exibir a mensagem de inicializacao do boot loader em modo debug.
     **/
    public partial class FrmSetComavToDebug : DockContent
    {
        #region Variaveis Globais

        delegate void AvailableReceivedDataCallBack(String message);
        private AvailableReceivedDataCallBack printReceivedDataCallBack = null;
        delegate void EnableComponentsCallBack(bool enable);
        private EnableComponentsCallBack enableCompomentsCallBack = null; 
        private byte[] msgToReceive = null;
        private byte[] msgToAnswer = null;
        private bool msgAnswered = false;
        private byte[] lastByteOfBuffer = null;
        private int positionToContinue = 0;
        private byte[] bufferTemp = null;
        public bool offlineMode;

        #endregion

        #region Construtor

        public FrmSetComavToDebug()
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
                txtWordToReceive.Text = DbConfiguration.EgseWordToReceive_Debug.Trim();
                txtWordToAnswer.Text = DbConfiguration.EgseWordToAnswer_Debug.Trim();
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
        
        /**
         * Verifica cada byte recebido da serial rs-232 com cada byte da mensagem esperada.
         * Retorna true se a mensagem esperada for encontrada. A mensagem esperada eh gravada no banco de dados.
         **/
        private bool ReceivedMessage(ref byte[] buffer)
        {
            bufferTemp = null;
            
            // Mensagem esperada: AC53
            // O 'lastByteOfBuffer' guarda o ultimo byte do buffer anterior recebido pela serial. 
            // Ele sera 'null' se esse ultimo byte nao for equivalente ao primeiro byte da mensagem esperada.
            // Entao o if abaixo verifica se existe a possibilidade da mensagem estar quebrada em dois buffers.
            if ((lastByteOfBuffer != null) && (lastByteOfBuffer[0] == msgToReceive[0]))
            {
                // Se a mensagem estiver quebrada, o if abaixo verifica se o primeiro byte do buffer atual 
                // eh o segundo byte da mensagem esperada.
                // Se entrar nesse if quer dizer que a mensagem esperada foi recebida de maneira quebrada.
                if ((buffer.Length >= 1) && (msgToReceive.Length == 2) && (buffer[0] == msgToReceive[1]))
                {
                    positionToContinue = 1;
                    bufferTemp = new byte[buffer.Length - positionToContinue];
                    Array.Copy(buffer, positionToContinue, bufferTemp, 0, (buffer.Length - positionToContinue));
                    lastByteOfBuffer = null;

                    return true;
                }
            }

            // Continua verificando o buffer
            for (int i = 0; i < buffer.Length; i++)
            {
                // Se entrar nesse if quer dizer que o primeiro byte 'AC' foi encontrado.
                // A partir daqui deve-se verificar se o segundo byte eh o '53'.
                if (buffer[i] == msgToReceive[0])
                {
                    // Verifica se a mensagem esperada esta no inicio do buffer recebido, 
                    // ou seja, ocupando as duas primeiras posicoes do buffer.
                    if (((buffer.Length == 2) && (i == 0)) && (buffer[i + 1] == msgToReceive[1]))
                    {
                        return true;
                    }
                    else if ((i < (buffer.Length - 1)) && (msgToReceive.Length == 2) && (buffer[i + 1] == msgToReceive[1]))
                    {
                        // Verifica se a mensagem esperada esta no final do buffer
                        if ((i + 2) == buffer.Length)
                        {
                            positionToContinue = 0;
                        }
                        else if ((i + 2) < buffer.Length)
                        {
                            // Se a msg esperada NAO estiver no final do buffer, guarda a posicao no buffer apos a mensagem esperada e copia o restante dos bytes para o bufferTemp
                            positionToContinue = i + 2;
                            bufferTemp = new byte[buffer.Length - positionToContinue];
                            Array.Copy(buffer, positionToContinue, bufferTemp, 0, (buffer.Length - positionToContinue));
                        }

                        return true;
                    }
                }
                else if ((i == (buffer.Length - 1)) && (!msgAnswered))
                {
                    lastByteOfBuffer = new byte[1];
                    lastByteOfBuffer[0] = buffer[i];
                }
            }

            return false;
        }

        /**
         * Metodo chamado pelo delegate para exibir os dados no TextBox.
         **/
        private void PrintData(String data)
        {
            txtBootLoaderMessage.AppendText(data);
        }

        private void EnableComponents(bool enable)
        {
            cmbComPort.Enabled = enable;
            cmbBaudRate.Enabled = enable;
            cmbDataBits.Enabled = enable;
            cmbStopBits.Enabled = enable;
            cmbParity.Enabled = enable;
            label1.Enabled = enable;
            label2.Enabled = enable;
            label3.Enabled = enable;
            label4.Enabled = enable;
            label5.Enabled = enable;
            label6.Enabled = enable;
            label7.Enabled = enable;
            btSetDebug.Enabled = enable;
            groupBox1.Enabled = enable;
            txtWordToReceive.Enabled = enable;
            txtWordToAnswer.Enabled = enable;
            btSave.Enabled = enable;
            txtBootLoaderMessage.Focus();
        }

        #endregion

        #region Eventos de Interface Grafica

        private void FrmSetComavToDebug_Load(object sender, EventArgs e)
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
                DbConfiguration.EgseWordToReceive_Debug = txtWordToReceive.Text.Trim().ToUpper();
                DbConfiguration.EgseWordToAnswer_Debug = txtWordToAnswer.Text.Trim().ToUpper();

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

        private void btSetDebug_Click(object sender, EventArgs e)
        {
            msgToReceive = Utils.Formatting.HexStringToByteArray(txtWordToReceive.Text);
            msgToAnswer = Utils.Formatting.HexStringToByteArray(txtWordToAnswer.Text);
            msgAnswered = false;

            if (ConnectToSerialPort())
            {
                EnableComponents(false);
                enableCompomentsCallBack = new EnableComponentsCallBack(EnableComponents);
                serial.DataReceived += new SerialDataReceivedEventHandler(SerialRead);
                printReceivedDataCallBack = new AvailableReceivedDataCallBack(PrintData);
            }
        }
        
        private void FrmSetComavToDebug_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(625, 476);
            }
        }

        private void FrmSetComavToDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serial.IsOpen)
            {
                serial.Close();
                serial.Dispose();
            }
        }

        private void txtWordToAnswer_Validating(object sender, CancelEventArgs e)
        {
            if ((txtWordToAnswer.Text.Length == 1) ||
                (txtWordToAnswer.Text.Length == 3))
            {
                txtWordToAnswer.Text = "0" + txtWordToAnswer.Text.Trim();
            }
        }

        private void txtWordToReceive_Validating(object sender, CancelEventArgs e)
        {
            if ((txtWordToReceive.Text.Length == 1) ||
                (txtWordToReceive.Text.Length == 3))
            {
                txtWordToReceive.Text = "0" + txtWordToReceive.Text.Trim();
            }
        }

        private void btClearScreen_Click(object sender, EventArgs e)
        {
            txtBootLoaderMessage.Clear();
        }

        private void btExpScreen_Click(object sender, EventArgs e)
        {
            String todayDateHour = DateTime.Now + Environment.NewLine;

            saveDialog.InitialDirectory = "ScreenToFile";
            saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveDialog.FilterIndex = 0;
            saveDialog.Title = "Save Screen as Text File";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveDialog.FileName, todayDateHour);
                System.IO.File.AppendAllText(saveDialog.FileName, txtBootLoaderMessage.Text);
            }
        }

        #endregion

        #region Rotinas de Comunicacao Serial RS-232 e Verificacao da Menssagem

        /** Evento de recepcao de dados pela serial rs-232. **/
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /**
             * Preciso invocar outro tratador de evento, pois serial_DataReceived eh
             * executado em outra thread (criada pelo SerialPort em Open(). Como
             * este evento eh tratado por outro handler, nao pode manipular objetos
             * criados na thread principal do simulador.
             **/
            this.Invoke(new EventHandler(SerialRead));
        }

        // Respeitar esta assinatura [void (object, EventArgs)], necessaria para qualquer EventHandler
        void SerialRead(object s, EventArgs e)
        {
            int remainingBytes = serial.BytesToRead;
            byte[] buffer = new byte[remainingBytes];
            serial.Read(buffer, 0, remainingBytes);
            
            if (msgAnswered)
            {
                Invoke(enableCompomentsCallBack, true);
                Encoding ascii = Encoding.ASCII;

                if ((bufferTemp == null))// || (bufferTemp.Length == 0))
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (buffer[i] == 0x0a)
                        {
                            if ((i < (buffer.Length - 1)) && (buffer[i + 1] == 0x0d))
                            {
                                Invoke(printReceivedDataCallBack, "\r\n\r\n");
                                i++;
                            }
                            else
                            {
                                Invoke(printReceivedDataCallBack, "\r\n");
                            }
                        }
                        else
                        {
                            Invoke(printReceivedDataCallBack, ascii.GetString(buffer, i, 1));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bufferTemp.Length; i++)
                    {
                        if (bufferTemp[i] == 0x0a)
                        {
                            if ((i < (bufferTemp.Length - 1)) && (bufferTemp[i + 1] == 0x0d))
                            {
                                Invoke(printReceivedDataCallBack, "\r\n\r\n");
                                i++;
                            }
                            else
                            {
                                Invoke(printReceivedDataCallBack, "\r\n");
                            }
                        }
                        else
                        {
                            Invoke(printReceivedDataCallBack, ascii.GetString(bufferTemp, i, 1));
                        }
                    }
                }
            }
            else
            {
                if (ReceivedMessage(ref buffer))
                {
                    try
                    {
                        for (int i = 0; i < msgToAnswer.Length; i++)
                        {
                            serial.Write(msgToAnswer, i, 1);
                        }
                        
                        msgAnswered = true;
                        Invoke(printReceivedDataCallBack, "\r\n");
                    }
                    catch
                    {
                        MessageBox.Show("Error on Word To Answer: 0x" + txtWordToAnswer.Text,
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        #endregion      
              

    }
}

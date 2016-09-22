/**
 * @file 	    FrmSimCortex.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto
 * @date	    16/01/2013
 * @note	    Modificado em 28/05/2013 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSimCortex
     * Este Formulario eh usado para simular o cortex. O mesmo eh composto de interface de comunicacao (de um lado Ethernet TCP/IP e do outro serial RS422).
     **/
    public partial class FrmSimCortex : DockContent
    {
        #region Atributos

        private FrmConnectionWithEgse frmConnectionWithEgse = null;
        private MdiMain mdiMain = null;
        private int receivedTCTcpIp;
        private int receivedTMRs422;
        private int sentTCRs422;
        private int availableTMTcpIp;
        private TcpListener listener = null;
        private Socket connection = null;
        private TcpClient client = null;
        private NetworkStream socketStream = null;
        private BinaryWriter write = null;
        private BinaryReader read = null;
        private Thread thread = null;
        private int port = 0;
        private bool isServer;

        delegate void PrintMessageTcpIpCallBack(String status, String msg, bool addRow);
        private PrintMessageTcpIpCallBack printMessageTcpIp = null;

        delegate void PrintFrameRs422CallBack(String status, String frame, bool crcValid);
        private PrintFrameRs422CallBack printFrame;

        #endregion

        #region Construtor

        public FrmSimCortex(MdiMain mdi)
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
        }

        #endregion

        #region Metodos Privados

        private bool StartServer()
        {
            try
            {
                thread = new Thread(Connect);
                thread.Start();
                Thread.Sleep(1300);

                printMessageTcpIp = new PrintMessageTcpIpCallBack(PrintMessageTcpIp);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool StartClient()
        {
            try
            {
                thread = new Thread(Connect);
                thread.Start();
                Thread.Sleep(1000);

                printMessageTcpIp = new PrintMessageTcpIpCallBack(PrintMessageTcpIp);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Listenning()
        {
            if (isServer)
            {
                if (connection.Connected)
                {
                    do
                    {
                        try
                        {
                            String msg = read.ReadString();
                            Invoke(printMessageTcpIp, new object[] { "Received TC from TCP/IP: ", msg, true });

                            byte[] command = Utils.Formatting.HexStringToByteArray(msg);

                            if ((command[0] == 0xEB) && (command[1] == 0x90))
                            {
                                frmConnectionWithEgse.SendCltu(command);
                            }
                            else
                            {
                                RawPacket request = new RawPacket(true, false);
                                request.RawContents = Utils.Formatting.HexStringToByteArray(msg);
                                frmConnectionWithEgse.SendRequest(request, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                                Invoke(printFrame, new object[] { "Sent TC to RS-422: ", Utils.Formatting.ConvertByteArrayToHexString(request.RawContents, request.RawContents.Length), false });

                                sentTCRs422++;
                                gridStatus[1, 1].Value = sentTCRs422;
                            }

                            receivedTCTcpIp++;
                            gridStatus[1, 0].Value = receivedTCTcpIp;

                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    while (connection.Connected);
                }
            }
            else
            {
                if (client.Connected)
                {
                    do
                    {
                        try
                        {
                            String msg = read.ReadString();
                            Invoke(printMessageTcpIp, new object[] { "Received TC from TCP/IP: ", msg, true });

                            byte[] command = Utils.Formatting.HexStringToByteArray(msg);

                            if ((command[0] == 0xEB) && (command[1] == 0x90))
                            {
                                frmConnectionWithEgse.SendCltu(command);
                            }
                            else
                            {
                                RawPacket request = new RawPacket(true, false);
                                request.RawContents = Utils.Formatting.HexStringToByteArray(msg);
                                frmConnectionWithEgse.SendRequest(request, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                                Invoke(printFrame, new object[] { "Sent TC to RS-422: ", Utils.Formatting.ConvertByteArrayToHexString(request.RawContents, request.RawContents.Length), false });

                                sentTCRs422++;
                                gridStatus[1, 1].Value = sentTCRs422;
                            }

                            receivedTCTcpIp++;
                            gridStatus[1, 0].Value = receivedTCTcpIp;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    while (client.Connected);
                }
            }
        }

        private void SendMessageTcpIp(String status, String msg)
        {
            if (isServer)
            {
                if (connection != null)
                {
                    if (connection.Connected)
                    {
                        write.Write(msg);
                        Invoke(printMessageTcpIp, new object[] { status, msg, false });
                    }
                }
            }
            else
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        write.Write(msg);
                        Invoke(printMessageTcpIp, new object[] { status, msg, false });
                    }
                }
            }
        }

        private void PrintMessageTcpIp(String status, String msg, bool addRow)
        {
            if (addRow)
            {
                gridLastPackage.Rows.Add();
                gridLastPackage.CurrentCell = gridLastPackage.Rows[gridLastPackage.RowCount - 1].Cells[0];
            }

            gridLastPackage[0, gridLastPackage.RowCount - 1].Value = status;
            gridLastPackage.Rows[gridLastPackage.RowCount - 1].Cells[0].Style.BackColor = Color.Lavender;
            gridLastPackage[1, gridLastPackage.RowCount - 1].Value = msg;

            gridLastPackage.ClearSelection();
            gridLastPackage[0, gridLastPackage.RowCount - 1].Selected = true;
        }

        private bool StartReceptionFromRS422()
        {
            if ((frmConnectionWithEgse != null) && frmConnectionWithEgse.Connected)
            {
                frmConnectionWithEgse.availableFrameEventHandler += new AvailableFrameEventHandler(ReceivedFrame);
                printFrame = new PrintFrameRs422CallBack(PrintFrameRs422);

                return true;
            }

            MessageBox.Show("Without connection with the RS-422 ! \n\nConnect it end try again.");
            return false;
        }

        private bool StopReceptionFromRS422()
        {
            if (frmConnectionWithEgse != null)
            {
                frmConnectionWithEgse.availableFrameEventHandler -= new AvailableFrameEventHandler(ReceivedFrame);
                printFrame = null;
            }

            return true;
        }

        private void ReceivedFrame(object sender, AvailableFrameEventArgs eventArgs)
        {
            String frame = Utils.Formatting.ConvertByteArrayToHexString(eventArgs.Frame, eventArgs.Frame.Length);
            Invoke(printFrame, new object[] { "Received TM from RS-422: ", frame, true });
            SendMessageTcpIp("Available TM to TCP/IP: ", frame);

            receivedTMRs422++;
            gridStatus[3, 0].Value = receivedTMRs422;

            availableTMTcpIp++;
            gridStatus[3, 1].Value = availableTMTcpIp;
        }

        private void PrintFrameRs422(String status, String frame, bool addRow)
        {
            if (addRow)
            {
                gridLastPackage.Rows.Add();
                gridLastPackage.CurrentCell = gridLastPackage.Rows[gridLastPackage.RowCount - 1].Cells[0];
            }

            gridLastPackage[2, gridLastPackage.RowCount - 1].Value = status;
            gridLastPackage.Rows[gridLastPackage.RowCount - 1].Cells[2].Style.BackColor = Color.Lavender;
            gridLastPackage[3, gridLastPackage.RowCount - 1].Value = frame;

            gridLastPackage.ClearSelection();
            gridLastPackage[0, gridLastPackage.RowCount - 1].Selected = true;
        }

        #endregion

        #region Metodos Publicos

        public void Connect()
        {
            if (isServer)
            {
                try
                {
                    listener = new TcpListener(IPAddress.Any, port);
                    listener.Start();
                    Thread.Sleep(1000);

                    while (true)
                    {
                        connection = listener.AcceptSocket();
                        socketStream = new NetworkStream(connection);
                        write = new BinaryWriter(socketStream);
                        read = new BinaryReader(socketStream);

                        Listenning();

                        write.Close();
                        read.Close();
                        socketStream.Close();
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    client = new TcpClient();
                    client.Connect(txtIp.Text, port);

                    socketStream = client.GetStream();
                    write = new BinaryWriter(socketStream);
                    read = new BinaryReader(socketStream);

                    Listenning();
                }
                catch (Exception)
                {
                }
            }
        }

        public void Disconnect()
        {
            if (thread.IsAlive)
            {
                thread.Abort();
            }

            if (write != null)
            {
                write.Close();
            }

            if (read != null)
            {
                read.Close();
            }

            if (socketStream != null)
            {
                socketStream.Close();
            }

            if (connection != null)
            {
                connection.Close();
            }

            thread = null;
            listener = null;
            write = null;
            read = null;
            socketStream = null;
            connection = null;
        }

        #endregion

        #region Eventos da Interface Grafica

        private void btConnect_Click(object sender, EventArgs e)
        {
            port = Convert.ToInt32(txtPort.Text);

            if (rdClient.Checked == true)
            {
                isServer = false;
            }
            else
            {
                isServer = true;
            }

            if (isServer)
            {
                if (btConnect.Text.Equals("Start \r\nCortex Simulator"))
                {
                    if (StartClient())
                    {
                        if (!StartReceptionFromRS422())
                        {
                            return;
                        }

                        gridLastPackage.Rows.Clear();
                        btConnect.Text = "Stop \r\nCortex Simulator";
                        groupBox7.Enabled = false;

                        return;
                    }
                }
                else
                {
                    btConnect.Text = "Start \r\nCortex Simulator";
                    StopReceptionFromRS422();
                    groupBox7.Enabled = true;

                    receivedTCTcpIp = 0;
                    receivedTMRs422 = 0;
                    sentTCRs422 = 0;
                    availableTMTcpIp = 0;

                    return;
                }
            }
            else
            {
                if (btConnect.Text.Equals("Start \r\nCortex Simulator"))
                {
                    if (StartServer())
                    {
                        if (!StartReceptionFromRS422())
                        {
                            return;
                        }

                        gridLastPackage.Rows.Clear();
                        btConnect.Text = "Stop \r\nCortex Simulator";
                        groupBox7.Enabled = false;

                        return;
                    }
                }
                else
                {
                    Disconnect();
                    StopReceptionFromRS422();
                    btConnect.Text = "Start \r\nCortex Simulator";
                    groupBox7.Enabled = true;

                    receivedTCTcpIp = 0;
                    receivedTMRs422 = 0;
                    sentTCRs422 = 0;
                    availableTMTcpIp = 0;

                    return;
                }
            }

            MessageBox.Show("Not Started");
        }

        private void FrmSimCortex_Load(object sender, EventArgs e)
        {
            if (mdiMain.FormConnectionWithEgse != null)
            {
                frmConnectionWithEgse = mdiMain.FormConnectionWithEgse;
            }

            gridStatus.Columns.Add("col1", "");
            gridStatus.Columns.Add("col2", "");
            gridStatus.Columns.Add("col3", "");
            gridStatus.Columns.Add("col4", "");

            gridStatus.Rows.Add(2);
            gridStatus.Rows[0].Height = 25;
            gridStatus.Rows[1].Height = 25;

            gridStatus[0, 0].Value = "Received TCs from TCP/IP: ";
            gridStatus[1, 0].Value = 0;
            gridStatus[2, 0].Value = "Received TMs from RS-422: ";
            gridStatus[3, 0].Value = 0;

            gridStatus[0, 1].Value = "Sent TCs to RS-422: ";
            gridStatus[1, 1].Value = 0;
            gridStatus[2, 1].Value = "Available TMs to TCP/IP: ";
            gridStatus[3, 1].Value = 0;

            gridStatus.Columns[0].Width = 150;
            gridStatus.Columns[1].Width = 130;
            gridStatus.Columns[2].Width = 150;
            gridStatus.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            gridStatus.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;
            gridStatus.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridStatus.Columns[2].DefaultCellStyle.BackColor = Color.Lavender;
            gridStatus.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridStatus.Rows[0].Selected = false;
            gridStatus.Enabled = false;
        }

        #endregion
    }
}
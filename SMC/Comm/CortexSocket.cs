/**
 * @file 	    CortexHandling.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    12/08/2013
 * @note	    Modificado em 26/09/2013 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;
using System.Threading;
using System.IO;
using System.Net.Sockets;
/**
 * @Namespace Namespace contendo as classes para tratamento dos diversos tipos de 
 * comunicacoes entre o SMC e o COMAV.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Comm
{
    /**
     * @class SocketToCortex
     * Classe para instanciar um socket para cada porta Tcp/Ip, tanto para o Cortex quanto para o COP.
     **/
    public class CortexSocket
    {
        #region Variaveis e declaracao de eventos

        private Socket connectionTcpIp = null;
        private TcpClient clientTcpIp = null;
        private NetworkStream socketStream = null;
        private BinaryWriter writeTcpIp = null;
        private BinaryReader readTcpIp = null;
        private Thread threadListeningEthernet = null;
        private String ip = String.Empty;
        private String portNumber = String.Empty;
        private AvailableCOPDataEventArgs availableCOPDataEventArgs = new AvailableCOPDataEventArgs();
        public event AvailableCOPDataEventHandler availableCOPDataEventHandler = null;
        private AvailableCortexTelemetryEventArgs availableTelemetryEventArgs = new AvailableCortexTelemetryEventArgs();
        public event AvailableCortexTelemetryEventHandler availableTelemetryEventHandler = null;

        #endregion

        #region Propriedades

        public String TcpIp
        {
            get
            {
                return ip;
            }
        }

        public String Port
        {
            get
            {
                return portNumber;
            }
        }

        #endregion

        #region Metodos publicos

        public bool StartConnection(String tcpIp, String port)
        {
            try
            {
                clientTcpIp = new TcpClient();
                clientTcpIp.Connect(tcpIp, int.Parse(port));
                Thread.Sleep(1500);

                ip = tcpIp;
                portNumber = port;

                socketStream = clientTcpIp.GetStream();
                Thread.Sleep(500);
                writeTcpIp = new BinaryWriter(socketStream);
                readTcpIp = new BinaryReader(socketStream);

                if (int.Parse(port) == 3070) // Cortex Telemetry Data
                {
                    threadListeningEthernet = new Thread(ListenningCortexTelemetryData);
                    threadListeningEthernet.Start();
                }
                else
                {
                    threadListeningEthernet = new Thread(ListenningCop);
                    threadListeningEthernet.Start();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void StopConnection()
        {
            if (threadListeningEthernet.IsAlive)
            {
                threadListeningEthernet.Abort();
            }

            if (writeTcpIp != null)
            {
                writeTcpIp.Close();
            }

            if (readTcpIp != null)
            {
                readTcpIp.Close();
            }

            if (socketStream != null)
            {
                socketStream.Close();
            }

            if (connectionTcpIp != null)
            {
                connectionTcpIp.Close();
            }

            threadListeningEthernet = null;
            writeTcpIp = null;
            readTcpIp = null;
            socketStream = null;
            connectionTcpIp = null;
        }

        public void Send(byte[] command)
        {
            if (writeTcpIp != null)
            {
                writeTcpIp.Write(command);
            }
        }

        #endregion

        #region Metodos privados

        private void ListenningCortexTelemetryData()
        {
            if (clientTcpIp.Connected)
            {
                do
                {
                    // A rotina de deteccão de TM deve receber buffers de 1024 bytes. 
                    // Para reaproveitar este feito, a leitura da porta socket de TM tambem esta sendo feita com 1024 bytes.
                    // O parametro 'rxBufferSizeAcceptedToDecoder' da TmDecoder esta sendo chamado.
                    availableTelemetryEventArgs.Port = int.Parse(portNumber); // DUVIDA-THIAGO: essa linha precisa estar aqui? nao poderia estar fora do loop?
                    availableTelemetryEventArgs.Telemetry = readTcpIp.ReadBytes(TmDecoder.rxBufferSizeAcceptedToDecoder); // DUVIDA-THIAGO: a rotina para aqui ate que sejam recebidos os 1024 bytes, ou ha timeout?

                    if (availableTelemetryEventHandler != null)
                    {
                        availableTelemetryEventHandler(this, availableTelemetryEventArgs);
                    }
                }
                while (clientTcpIp.Connected);
            }
        }

        private void ListenningCop()
        {
            if (clientTcpIp.Connected)
            {
                byte[] header = new byte[4];
                byte[] size = new byte[4];
                byte[] message = new byte[0];
                byte[] allMessage = new byte[0];
                byte[] postamble = new byte[4];
                Int32 headerWord = 0;
                Int32 sizeAllMessageWord = 0;

                do
                {
                    try
                    {
                        header = readTcpIp.ReadBytes(4);

                        headerWord = (Int32)((header[0] << 24) |
                                             (header[1] << 16) |
                                             (header[2] << 8) |
                                             (header[3]));

                        if (headerWord == 0x499602D2)
                        {
                            size = readTcpIp.ReadBytes(4);

                            sizeAllMessageWord = (Int32)((size[0] << 24) |
                                                         (size[1] << 16) |
                                                         (size[2] << 8) |
                                                         (size[3]));

                            message = readTcpIp.ReadBytes(sizeAllMessageWord - (header.Length + size.Length));

                            Array.Resize(ref allMessage, sizeAllMessageWord);
                            Array.Copy(header, 0, allMessage, 0, header.Length);
                            Array.Copy(size, 0, allMessage, 4, size.Length);
                            Array.Copy(message, 0, allMessage, 8, message.Length);

                            if ((allMessage[allMessage.Length - 4] == 0xB6) &
                                (allMessage[allMessage.Length - 3] == 0x69) &
                                (allMessage[allMessage.Length - 2] == 0xFD) &
                                (allMessage[allMessage.Length - 1] == 0x2E))
                            {
                                availableCOPDataEventArgs.Port = int.Parse(portNumber);
                                availableCOPDataEventArgs.Message = allMessage;

                                if (availableCOPDataEventHandler != null)
                                {
                                    availableCOPDataEventHandler(this, availableCOPDataEventArgs);
                                }

                                Array.Resize(ref message, 0);
                                Array.Resize(ref allMessage, 0);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                while (clientTcpIp.Connected);
            }
        }

        #endregion
    }

    #region Metodos e delegates para disponibilizacao dos cdados do COP-1 e SPS

    public class AvailableCOPDataEventArgs : EventArgs
    {
        private byte[] msg;
        private int port;

        public byte[] Message
        {
            set
            {
                msg = value;
            }
            get
            {
                return msg;
            }
        }

        public int Port
        {
            set
            {
                port = value;
            }
            get
            {
                return port;
            }
        }

        public AvailableCOPDataEventArgs()
        {
        }
    }

    public delegate void AvailableCOPDataEventHandler(object sender, AvailableCOPDataEventArgs e);

    public class AvailableCortexTelemetryEventArgs : EventArgs
    {
        private byte[] telemetry;
        private int port;

        public byte[] Telemetry
        {
            set
            {
                telemetry = value;
            }
            get
            {
                return telemetry;
            }
        }

        public int Port
        {
            set
            {
                port = value;
            }
            get
            {
                return port;
            }
        }

        public AvailableCortexTelemetryEventArgs()
        {
        }
    }

    public delegate void AvailableCortexTelemetryEventHandler(object sender, AvailableCortexTelemetryEventArgs e);

    #endregion
}

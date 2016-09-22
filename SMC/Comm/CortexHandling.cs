/**
 * @file 	    CortexHandling.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    05/06/2013
 * @note	    Modificado em 01/09/2014 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;

/**
 * @Namespace Namespace contendo as classes para tratamento dos diversos tipos de 
 * comunicacoes entre o SMC e o COMAV.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Comm
{
    /**
     * @class CortexHandling
     * Classe para o gerenciamento de conexoes com o Cortex.
     **/
    public class CortexHandling
    {
        #region Variaveis

        private String ipAddress;
        private String spacecraftId;
        private String vcid;

        private List<CortexSocket> sockets = new List<CortexSocket>();
        private AvailableCOPDataEventArgs availableCOPMessageEventArgs = new AvailableCOPDataEventArgs();
        public event AvailableCOPDataEventHandler availableCOPMessageEventHandler = null;
        private AvailableCortexTelemetryEventArgs availableTelemetryEventArgs = new AvailableCortexTelemetryEventArgs();
        public event AvailableCortexTelemetryEventHandler availableTelemetryEventHandler = null;
        
        // Devera ser usada para controle das diretivas do protocolo CCSDS, implimentado pelo COP-1
        public enum Directive { None, 
                                ADServiceWithoutCLCWCheck, 
                                ADServiceWithCLCWCheck, 
                                ADServiceWithUnlock, 
                                ADServiceWithSetVR, 
                                TerminateADServiiceDirective
        }

        #endregion

        #region Propriedades

        public String IpAddress
        {
            set
            {
                ipAddress = value;
            }
            get
            {
                return ipAddress;
            }
        }

        public String SpacecraftId
        {
            set
            {
                spacecraftId = value;
            }
            get
            {
                return spacecraftId;
            }
        }

        public String Vcid
        {
            set
            {
                vcid = value;
            }
            get
            {
                return vcid;
            }
        }

        #endregion

        #region Metodos publicos

        /*
         * Este metodo instancia um Socket para cada porta. Eh usado tanto para as portas do Cortex quanto para as portas do COP.
         * O mesmo tambem assina os eventos de recepcao de telemetria via Cortex e de recepcao de dados (Acks e Nacks) do COP.
         */
        public bool Connect(String port)
        {
            try
            {
                CortexSocket socket = new CortexSocket();
                socket.availableCOPDataEventHandler += new AvailableCOPDataEventHandler(ReceivedCOPMessage);
                socket.availableTelemetryEventHandler += new AvailableCortexTelemetryEventHandler(ReceivedCortexTelemetryData);

                if (socket.StartConnection(ipAddress, port))
                {
                    sockets.Add(socket);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
         * Este metodo desconecta as portas individualmente, para o Cortex e para o COP.
         */
        public void Disconnect(String port)
        {
            for (int i = 0; i < sockets.Count; i++)
            {
                if ((sockets[i].TcpIp == ipAddress) && (sockets[i].Port == port))
                {
                    sockets[i].availableCOPDataEventHandler -= new AvailableCOPDataEventHandler(ReceivedCOPMessage);
                    sockets[i].StopConnection();
                    sockets.RemoveAt(i);
                }
            }
        }

        /*
         * Este metodo foi definido para efeito de depuracao das mensagens de Ack e Nack do COP-1.
         */
        public void ConsoleWriteline(byte[] message)
        {
            byte[] aTemp = new byte[4];
            for (int i = 0; i < message.Length; i++)
            {
                aTemp[0] = message[i++];
                aTemp[1] = message[i++];
                aTemp[2] = message[i++];
                aTemp[3] = message[i];

                Console.WriteLine(Utils.Formatting.ConvertByteArrayToHexString(aTemp, aTemp.Length));
            }

            Console.WriteLine();
        }

        #endregion

        #region Metodos privados

        /*
         * Este metodo recebe as mensagens (Acks e Nacks) do COP.
         */
        private void ReceivedCOPMessage(object sender, AvailableCOPDataEventArgs eventArgs)
        {
            if (availableCOPMessageEventHandler != null)
            {
                availableCOPMessageEventHandler(this, eventArgs);
            }
        }

        /*
         * Este metodo recebe as telemetrias do Cortex.
         */
        private void ReceivedCortexTelemetryData(object sender, AvailableCortexTelemetryEventArgs eventArgs)
        {
            if (availableTelemetryEventHandler != null)
            {
                availableTelemetryEventHandler(this, eventArgs);
            }
        }

        /**
         * Este metodo extrai do endereco ip cada valor e alimenta um vetor de 4 posicoes com os respectivos valores do ip em fortato byte.
         **/
        private byte[] ObtainIpValues(String ipValue)
        {
            byte[] ipValues = new byte[4];
            int indexDot = 0;
            int indexStart = 0;
            indexDot = ipValue.IndexOf(".", 0);
            ipValues[0] = BitConverter.GetBytes(int.Parse(ipValue.Substring(indexStart, indexDot)))[0];
            indexStart = indexDot + 1;
            indexDot = ipValue.IndexOf(".", indexStart);
            ipValues[1] = BitConverter.GetBytes(int.Parse(ipValue.Substring(indexStart, (indexDot - indexStart))))[0];
            indexStart = indexDot + 1;
            indexDot = ipValue.IndexOf(".", indexStart);
            ipValues[2] = BitConverter.GetBytes(int.Parse(ipValue.Substring(indexStart, (indexDot - indexStart))))[0];
            indexStart = indexDot + 1;
            indexDot = ipValue.Length;
            ipValues[3] = BitConverter.GetBytes(int.Parse(ipValue.Substring(indexStart, (indexDot - indexStart))))[0];

            return ipValues;
        }

        /*
         * Este metodo envia a mensagem para a porta socket especificada no parametro.
         */
        private void SendCommand(String port, byte[] command)
        {
            for (int i = 0; i < sockets.Count; i++)
            {
                if ((sockets[i].TcpIp == ipAddress) && (sockets[i].Port == port))
                {
                    sockets[i].Send(command);
                }
            }
        }

        #endregion

        #region Metodos que codificam as mensagens para envio ao SPS e ao COP-1.

        /*
         * Os metodos que codificam as mensagens para envio ao SPS e ao COP-1 devem ser reestruturados para melhor aproveitamento de codigo.
         * O status atual possui muito codigo repetitivo, pelo fato da necessidade de agilizar o desenvolvimento. Durante a implementacao,
         * as mensagens que eram validadas corretamento pelo cortex eram mantidas para nao correr riscos de injecao de erros.
         * Reestrutura-los futuramente!!
         * 
         * Os parametros IpAddress, VCID e SpacecraftId estao sendo alimentados de acordo com os mesmos valores salvos na DbConfiguration.
         */
        public void CORTEXTelemetryRequest(String port)
        {
            // O Control Data Command eh uma classe de comandos que sao enviados ao CORTEX com algum interesse.
            // Ex: O Telemetry Request eh um comando do tipo Control Data.
            // O Telemetry Request esta especificado na pag. 65 do seguinte documento: 
            // 'COMMAND RANGING & TELEMETRY CORTEX TCP-IP ETHERNET INTERFACE'

            // ============ Standard TCP-IP header ============
            Int32 START_OF_MESSAGE = 1234567890;

            // SIZE_OF_MESSAGE eh variavel
            Int32 SIZE_OF_MESSAGE = 64; // 64 eh o tamanho do comando Telemetry Request.

            // O Reset flow ID e o Remote Application Control nao podem ser configurados por razoes de seguranca
            // FLOW_ID = '1', para o Control flow ID
            // FLOW_ID = 'FFFFFFFF', para o Reset flow ID
            // FLOW_ID = '0', para o Remote Application Control
            Int32 FLOW_ID = 0;

            // ============ Telemetry channel ============
            // 0 : Telemetry channel A 
            // 1 : Telemetry channel B 
            // 2 : Telemetry channel C 
            // 3 : Telemetry channel D  
            // 4 : Telemetry channel E  
            // 5 : Telemetry channel F  
            Int32 TELEMETRY_CHANNEL = 0;

            // ============ Number of buffered TM blocks or frames (0 = Real time telemetry) ============
            // 0 to 1024
            Int32 NUM_OF_BUFFERED_TM_BLOCKS = 0;

            // ============ Data Flow ============
            // Real time telemetry 
            // 0 : Permanent flow 
            // 1 : Single block/frame transmission 
            // 2 : Permanent flow + dummy TM 

            // Offline telemetry 
            // 4 : Permanent flow 
            // 5 : Single block/frame transmission 
            // 6 : Permanent flow + dummy TM 

            // 80H : Stop TM 

            // Other values : Reserved
            Int32 DATA_FLOW = 0; // Permanent flow

            /*
               pag. 66
               For each reconstructed telemetry frame, the 64 bits following the Synchronization Word are logically ANDed 
               with a 64-bit mask (offsets 6 & 7 in the above table) and the frame is transmitted only if the result matches 
               the expected value (offsets 8 & 9).
               If the Synchronization Word is not byte-aligned, the mask is applied from byte N, where N is the Synchronization Word 
               size rounded off to the nearest byte down. Example : if the Synchronization Word is 20-bit long (2.5 bytes), the mask is 
               applied from byte 2 in the frame (byte 0 being the first byte of the Synchronization Word). 
               To transmit all frames, the mask and the expected value must be set to 0. This feature is inhibited when the frame 
               synchronizer is OFF (raw data transmission). 
             */

            Int32 FRAME_MASK_MSB = 0;
            Int32 FRAME_MASK_LSB = 0;
            Int32 EXPECTED_VALUE_MSB = 0;
            Int32 EXPECTED_VALUE_LSB = 0;

            // ============ Offline TM only Time Tag of the first telemetry frame to send ============ 
            // Estes campos sao opcionais

            Int32 FIRST_FIELD_OF_FIRST_TELEMETRY = 0;
            Int32 SECOND_FIELD_OF_FIRST_TELEMETRY = 0;

            // ============ Offline TM only Time Tag of the last telemetry frame to send ============ 
            // Estes campos sao opcionais

            Int32 FIRST_FIELD_OF_LAST_TELEMETRY = 0;
            Int32 SECOND_FIELD_OF_LAST_TELEMETRY = 0;

            // ============ MSB (16 bits) : First file to read in storage pool ============ 
            // ============ LSB (16 bits) : Number of files to read in storage pool ============ 
            Int32 X = 0;

            // ============ Standard TCP-IP postamble ============
            Int32 END_OF_MESSAGE = -1234567890;

            int destinationIndex = 0;
            byte[] command = new byte[SIZE_OF_MESSAGE];

            Array.Copy(BitConverter.GetBytes(START_OF_MESSAGE), 0, command, (destinationIndex), 4);
            Array.Copy(BitConverter.GetBytes(SIZE_OF_MESSAGE), 0, command, (destinationIndex + 4), 4);
            Array.Copy(BitConverter.GetBytes(FLOW_ID), 0, command, (destinationIndex + 8), 4);
            Array.Copy(BitConverter.GetBytes(TELEMETRY_CHANNEL), 0, command, (destinationIndex + 12), 4);
            Array.Copy(BitConverter.GetBytes(NUM_OF_BUFFERED_TM_BLOCKS), 0, command, (destinationIndex + 16), 4);
            Array.Copy(BitConverter.GetBytes(DATA_FLOW), 0, command, (destinationIndex + 20), 4);
            Array.Copy(BitConverter.GetBytes(FRAME_MASK_MSB), 0, command, (destinationIndex + 24), 4);
            Array.Copy(BitConverter.GetBytes(FRAME_MASK_LSB), 0, command, (destinationIndex + 28), 4);
            Array.Copy(BitConverter.GetBytes(EXPECTED_VALUE_MSB), 0, command, (destinationIndex + 32), 4);
            Array.Copy(BitConverter.GetBytes(EXPECTED_VALUE_LSB), 0, command, (destinationIndex + 36), 4);
            Array.Copy(BitConverter.GetBytes(FIRST_FIELD_OF_FIRST_TELEMETRY), 0, command, (destinationIndex + 40), 4);
            Array.Copy(BitConverter.GetBytes(SECOND_FIELD_OF_FIRST_TELEMETRY), 0, command, (destinationIndex + 44), 4);
            Array.Copy(BitConverter.GetBytes(FIRST_FIELD_OF_LAST_TELEMETRY), 0, command, (destinationIndex + 48), 4);
            Array.Copy(BitConverter.GetBytes(SECOND_FIELD_OF_LAST_TELEMETRY), 0, command, (destinationIndex + 52), 4);
            Array.Copy(BitConverter.GetBytes(X), 0, command, (destinationIndex + 56), 4);
            Array.Copy(BitConverter.GetBytes(END_OF_MESSAGE), 0, command, (destinationIndex + 60), 4);

            if (BitConverter.IsLittleEndian) // porque o cortex trabalha com dados em formato BigEndian
            {
                byte[] arrayTemp = new byte[4];

                for (int i = 0; i < command.Length - 3; i += 4)
                {
                    int w = i;

                    for (int j = 4; j >= 1; j--)
                    {
                        arrayTemp[j - 1] = command[w];
                        w++;
                    }

                    command[i + 0] = arrayTemp[0];
                    command[i + 1] = arrayTemp[1];
                    command[i + 2] = arrayTemp[2];
                    command[i + 3] = arrayTemp[3];
                }
            }

            SendCommand(port, command);
        }

        public void COPMonitoringFlow(String port)
        {
            byte[] command = new byte[0];
            int startIndex = 0;

            //Int32 F1_START_OF_MESSAGE = 1234567890; // 49 96 02 D2
            byte[] F1_START_OF_MESSAGE = new byte[4];
            F1_START_OF_MESSAGE[0] = 0x49;
            F1_START_OF_MESSAGE[1] = 0x96;
            F1_START_OF_MESSAGE[2] = 0x02;
            F1_START_OF_MESSAGE[3] = 0xD2;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F1_START_OF_MESSAGE[0];
            command[startIndex++] = F1_START_OF_MESSAGE[1];
            command[startIndex++] = F1_START_OF_MESSAGE[2];
            command[startIndex++] = F1_START_OF_MESSAGE[3];

            //Int32 F2_SIZE_OF_MESSAGE = 00;
            byte[] F2_SIZE_OF_MESSAGE = new byte[4];
            F2_SIZE_OF_MESSAGE[0] = 0x00;
            F2_SIZE_OF_MESSAGE[1] = 0x00;
            F2_SIZE_OF_MESSAGE[2] = 0x00;
            F2_SIZE_OF_MESSAGE[3] = 0x00;  // No final da montagem do comando o campo SIZE_OF_MESSAGE eh preenchido.
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F2_SIZE_OF_MESSAGE[0];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[1];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[2];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[3];

            //Int32 F3_FLOW_ID = 1;
            byte[] F3_FLOW_ID = new byte[4];
            F3_FLOW_ID[0] = 0x00;
            F3_FLOW_ID[1] = 0x00;
            F3_FLOW_ID[2] = 0x00;
            F3_FLOW_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F3_FLOW_ID[0];
            command[startIndex++] = F3_FLOW_ID[1];
            command[startIndex++] = F3_FLOW_ID[2];
            command[startIndex++] = F3_FLOW_ID[3];

            //Int32 F4_TYPE_OF_OPERATION = 1; // 1 = Control
            byte[] F4_TYPE_OF_OPERATION = new byte[4];
            F4_TYPE_OF_OPERATION[0] = 0x00;
            F4_TYPE_OF_OPERATION[1] = 0x00;
            F4_TYPE_OF_OPERATION[2] = 0x00;
            F4_TYPE_OF_OPERATION[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F4_TYPE_OF_OPERATION[0];
            command[startIndex++] = F4_TYPE_OF_OPERATION[1];
            command[startIndex++] = F4_TYPE_OF_OPERATION[2];
            command[startIndex++] = F4_TYPE_OF_OPERATION[3];

            //Int32 F5_UNUSED = 0;
            byte[] F5_UNUSED = new byte[4];
            F5_UNUSED[0] = 0x00;
            F5_UNUSED[1] = 0x00;
            F5_UNUSED[2] = 0x00;
            F5_UNUSED[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F5_UNUSED[0];
            command[startIndex++] = F5_UNUSED[1];
            command[startIndex++] = F5_UNUSED[2];
            command[startIndex++] = F5_UNUSED[3];

            //Int32 F6_REQUEST_TAG = 170;
            byte[] F6_REQUEST_TAG = new byte[4];
            F6_REQUEST_TAG[0] = 0xFF;
            F6_REQUEST_TAG[1] = 0xFF;
            F6_REQUEST_TAG[2] = 0xFF;
            F6_REQUEST_TAG[3] = 0xFF;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F6_REQUEST_TAG[0];
            command[startIndex++] = F6_REQUEST_TAG[1];
            command[startIndex++] = F6_REQUEST_TAG[2];
            command[startIndex++] = F6_REQUEST_TAG[3];

            //Int32 F71_END_OF_MESSAGE = -1234567890;
            byte[] F71_END_OF_MESSAGE = new byte[4]; //B669FD2E
            F71_END_OF_MESSAGE[0] = 0xB6;
            F71_END_OF_MESSAGE[1] = 0x69;
            F71_END_OF_MESSAGE[2] = 0xFD;
            F71_END_OF_MESSAGE[3] = 0x2E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F71_END_OF_MESSAGE[0];
            command[startIndex++] = F71_END_OF_MESSAGE[1];
            command[startIndex++] = F71_END_OF_MESSAGE[2];
            command[startIndex++] = F71_END_OF_MESSAGE[3];

            // Agora alimenta o tamanho da mensagem
            Int32 size = command.Length;
            byte[] arraySize = new byte[4];
            Array.Copy(BitConverter.GetBytes(size), 0, arraySize, 0, 4);

            command[4] = arraySize[3];
            command[5] = arraySize[2];
            command[6] = arraySize[1];
            command[7] = arraySize[0];

            SendCommand(port, command);
        }

        public void COPControlRequest(String port)
        {
            byte[] command = new byte[0];
            int startIndex = 0;
            int numParameters = 0;

            //Int32 F1_START_OF_MESSAGE = 1234567890; // 49 96 02 D2
            byte[] F1_START_OF_MESSAGE = new byte[4];
            F1_START_OF_MESSAGE[0] = 0x49;
            F1_START_OF_MESSAGE[1] = 0x96;
            F1_START_OF_MESSAGE[2] = 0x02;
            F1_START_OF_MESSAGE[3] = 0xD2;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F1_START_OF_MESSAGE[0];
            command[startIndex++] = F1_START_OF_MESSAGE[1];
            command[startIndex++] = F1_START_OF_MESSAGE[2];
            command[startIndex++] = F1_START_OF_MESSAGE[3];

            //Int32 F2_SIZE_OF_MESSAGE = 00;
            byte[] F2_SIZE_OF_MESSAGE = new byte[4];
            F2_SIZE_OF_MESSAGE[0] = 0x00;
            F2_SIZE_OF_MESSAGE[1] = 0x00;
            F2_SIZE_OF_MESSAGE[2] = 0x00;
            F2_SIZE_OF_MESSAGE[3] = 0x00;  // No final da montagem do comando o campo SIZE_OF_MESSAGE eh preenchido.
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F2_SIZE_OF_MESSAGE[0];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[1];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[2];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[3];

            //Int32 F3_FLOW_ID = 1; // 1 = Control
            byte[] F3_FLOW_ID = new byte[4];
            F3_FLOW_ID[0] = 0x00;
            F3_FLOW_ID[1] = 0x00;
            F3_FLOW_ID[2] = 0x00;
            F3_FLOW_ID[3] = 0x01;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F3_FLOW_ID[0];
            command[startIndex++] = F3_FLOW_ID[1];
            command[startIndex++] = F3_FLOW_ID[2];
            command[startIndex++] = F3_FLOW_ID[3];

            //Int32 F4_TYPE_OF_OPERATION = 1; // 1 = Control
            byte[] F4_TYPE_OF_OPERATION = new byte[4];
            F4_TYPE_OF_OPERATION[0] = 0x00;
            F4_TYPE_OF_OPERATION[1] = 0x00;
            F4_TYPE_OF_OPERATION[2] = 0x00;
            F4_TYPE_OF_OPERATION[3] = 0x01;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F4_TYPE_OF_OPERATION[0];
            command[startIndex++] = F4_TYPE_OF_OPERATION[1];
            command[startIndex++] = F4_TYPE_OF_OPERATION[2];
            command[startIndex++] = F4_TYPE_OF_OPERATION[3];

            //Int32 F5_UNUSED = 0;
            byte[] F5_UNUSED = new byte[4];
            F5_UNUSED[0] = 0x00;
            F5_UNUSED[1] = 0x00;
            F5_UNUSED[2] = 0x00;
            F5_UNUSED[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F5_UNUSED[0];
            command[startIndex++] = F5_UNUSED[1];
            command[startIndex++] = F5_UNUSED[2];
            command[startIndex++] = F5_UNUSED[3];

            //Int32 F6_REQUEST_TAG = 0;
            byte[] F6_REQUEST_TAG = new byte[4];
            F6_REQUEST_TAG[0] = 0xBB;
            F6_REQUEST_TAG[1] = 0xBB;
            F6_REQUEST_TAG[2] = 0xBB;
            F6_REQUEST_TAG[3] = 0xBB;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F6_REQUEST_TAG[0];
            command[startIndex++] = F6_REQUEST_TAG[1];
            command[startIndex++] = F6_REQUEST_TAG[2];
            command[startIndex++] = F6_REQUEST_TAG[3];

            //Int32 F7_NUMBER_OF_PARAMETERS = 00;
            byte[] F7_NUMBER_OF_PARAMETERS = new byte[4];
            F7_NUMBER_OF_PARAMETERS[0] = 0x00;
            F7_NUMBER_OF_PARAMETERS[1] = 0x00;
            F7_NUMBER_OF_PARAMETERS[2] = 0x00;
            F7_NUMBER_OF_PARAMETERS[3] = 0x00; // inicia em 0, mas eh alimentado no final com a variavel numParameters que eh incrementada a cada paramero adicionado no array
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F7_NUMBER_OF_PARAMETERS[0];
            command[startIndex++] = F7_NUMBER_OF_PARAMETERS[1];
            command[startIndex++] = F7_NUMBER_OF_PARAMETERS[2];
            command[startIndex++] = F7_NUMBER_OF_PARAMETERS[3];

            //Int32 F8_PARAMETER_0_ID = 0;
            byte[] F8_PARAMETER_0_ID = new byte[4];
            F8_PARAMETER_0_ID[0] = 0x00;
            F8_PARAMETER_0_ID[1] = 0x00;
            F8_PARAMETER_0_ID[2] = 0x00;
            F8_PARAMETER_0_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F8_PARAMETER_0_ID[0];
            command[startIndex++] = F8_PARAMETER_0_ID[1];
            command[startIndex++] = F8_PARAMETER_0_ID[2];
            command[startIndex++] = F8_PARAMETER_0_ID[3];

            //Int32 F9_SPACECRAFT_ID = 1023; // 606 = 025E 1023 = 03FF
            byte[] F9_SPACECRAFT_ID = new byte[4];
            F9_SPACECRAFT_ID = BitConverter.GetBytes(int.Parse(spacecraftId));
            Array.Reverse(F9_SPACECRAFT_ID);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F9_SPACECRAFT_ID[0];
            command[startIndex++] = F9_SPACECRAFT_ID[1];
            command[startIndex++] = F9_SPACECRAFT_ID[2];
            command[startIndex++] = F9_SPACECRAFT_ID[3];

            numParameters++;

            //Int32 F10_PARAMETER_1_ID = 1;
            byte[] F10_PARAMETER_1_ID = new byte[4];
            F10_PARAMETER_1_ID[0] = 0x00;
            F10_PARAMETER_1_ID[1] = 0x00;
            F10_PARAMETER_1_ID[2] = 0x00;
            F10_PARAMETER_1_ID[3] = 0x01;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F10_PARAMETER_1_ID[0];
            command[startIndex++] = F10_PARAMETER_1_ID[1];
            command[startIndex++] = F10_PARAMETER_1_ID[2];
            command[startIndex++] = F10_PARAMETER_1_ID[3];
            
            //Int32 F11_CODE_BLOCK_LENGTH = 8;
            byte[] F11_CODE_BLOCK_LENGTH = new byte[4];
            F11_CODE_BLOCK_LENGTH[0] = 0x00;
            F11_CODE_BLOCK_LENGTH[1] = 0x00;
            F11_CODE_BLOCK_LENGTH[2] = 0x00;
            F11_CODE_BLOCK_LENGTH[3] = 0x08;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F11_CODE_BLOCK_LENGTH[0];
            command[startIndex++] = F11_CODE_BLOCK_LENGTH[1];
            command[startIndex++] = F11_CODE_BLOCK_LENGTH[2];
            command[startIndex++] = F11_CODE_BLOCK_LENGTH[3];

            numParameters++;

            //Int32 F12_PARAMETER_2_ID = 2;
            byte[] F12_PARAMETER_2_ID = new byte[4];
            F12_PARAMETER_2_ID[0] = 0x00;
            F12_PARAMETER_2_ID[1] = 0x00;
            F12_PARAMETER_2_ID[2] = 0x00;
            F12_PARAMETER_2_ID[3] = 0x02;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F12_PARAMETER_2_ID[0];
            command[startIndex++] = F12_PARAMETER_2_ID[1];
            command[startIndex++] = F12_PARAMETER_2_ID[2];
            command[startIndex++] = F12_PARAMETER_2_ID[3];

            //Int32 F13_CHANNEL_AUTO_SWITCH = 1;
            byte[] F13_CHANNEL_AUTO_SWITCH = new byte[4];
            F13_CHANNEL_AUTO_SWITCH[0] = 0x00;
            F13_CHANNEL_AUTO_SWITCH[1] = 0x00;
            F13_CHANNEL_AUTO_SWITCH[2] = 0x00;
            F13_CHANNEL_AUTO_SWITCH[3] = 0x00; // ATENCAO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                               // Certificar se houve erro de digitacao no documento da INVAP
                                               // La este parametro esta com as opcoes (0 = on) e (1 = off)
                                               // Nao seria o contrario????
                                               // No documento esta o (1 = off).
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F13_CHANNEL_AUTO_SWITCH[0];
            command[startIndex++] = F13_CHANNEL_AUTO_SWITCH[1];
            command[startIndex++] = F13_CHANNEL_AUTO_SWITCH[2];
            command[startIndex++] = F13_CHANNEL_AUTO_SWITCH[3];

            numParameters++;

            // Bit 31 to 16 = Tm Number (01 - 0x01)
            // Bit 15 to 0 = Port Number (3070 - 0x0BFE)
            //Int32 F14_PARAMETER_3_ID = 3;
            byte[] F14_PARAMETER_3_ID = new byte[4];
            F14_PARAMETER_3_ID[0] = 0x00;
            F14_PARAMETER_3_ID[1] = 0x00;
            F14_PARAMETER_3_ID[2] = 0x00;
            F14_PARAMETER_3_ID[3] = 0x03;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F14_PARAMETER_3_ID[0];
            command[startIndex++] = F14_PARAMETER_3_ID[1];
            command[startIndex++] = F14_PARAMETER_3_ID[2];
            command[startIndex++] = F14_PARAMETER_3_ID[3];

            byte[] F15_TM_PORT_NUMBER_AND_ADDRESS = new byte[4];
            F15_TM_PORT_NUMBER_AND_ADDRESS[0] = 0x00;
            F15_TM_PORT_NUMBER_AND_ADDRESS[1] = 0x01;
            F15_TM_PORT_NUMBER_AND_ADDRESS[2] = 0x0B;
            F15_TM_PORT_NUMBER_AND_ADDRESS[3] = 0xFE;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F15_TM_PORT_NUMBER_AND_ADDRESS[0];
            command[startIndex++] = F15_TM_PORT_NUMBER_AND_ADDRESS[1];
            command[startIndex++] = F15_TM_PORT_NUMBER_AND_ADDRESS[2];
            command[startIndex++] = F15_TM_PORT_NUMBER_AND_ADDRESS[3];

            numParameters++;

            //Int32 F16_PARAMETER_4_ID = 4;
            byte[] F16_PARAMETER_4_ID = new byte[4];
            F16_PARAMETER_4_ID[0] = 0x00;
            F16_PARAMETER_4_ID[1] = 0x00;
            F16_PARAMETER_4_ID[2] = 0x00;
            F16_PARAMETER_4_ID[3] = 0x04;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F16_PARAMETER_4_ID[0];
            command[startIndex++] = F16_PARAMETER_4_ID[1];
            command[startIndex++] = F16_PARAMETER_4_ID[2];
            command[startIndex++] = F16_PARAMETER_4_ID[3];

            byte[] F17_IP_ADDRESS = new byte[4];
            byte[] ipValues = ObtainIpValues(this.ipAddress);
            F17_IP_ADDRESS[0] = ipValues[0];
            F17_IP_ADDRESS[1] = ipValues[1];
            F17_IP_ADDRESS[2] = ipValues[2];
            F17_IP_ADDRESS[3] = ipValues[3];
            
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F17_IP_ADDRESS[0];
            command[startIndex++] = F17_IP_ADDRESS[1];
            command[startIndex++] = F17_IP_ADDRESS[2];
            command[startIndex++] = F17_IP_ADDRESS[3];

            numParameters++;

            //Int32 F18_PARAMETER_5_ID = 5;
            byte[] F18_PARAMETER_5_ID = new byte[4];
            F18_PARAMETER_5_ID[0] = 0x00;
            F18_PARAMETER_5_ID[1] = 0x00;
            F18_PARAMETER_5_ID[2] = 0x00;
            F18_PARAMETER_5_ID[3] = 0x05;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F18_PARAMETER_5_ID[0];
            command[startIndex++] = F18_PARAMETER_5_ID[1];
            command[startIndex++] = F18_PARAMETER_5_ID[2];
            command[startIndex++] = F18_PARAMETER_5_ID[3];

            //Int32 F19_TC_LOOPBACK = 0;
            byte[] F19_TC_LOOPBACK = new byte[4];
            F19_TC_LOOPBACK[0] = 0x00;
            F19_TC_LOOPBACK[1] = 0x00;
            F19_TC_LOOPBACK[2] = 0x00;
            F19_TC_LOOPBACK[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F19_TC_LOOPBACK[0];
            command[startIndex++] = F19_TC_LOOPBACK[1];
            command[startIndex++] = F19_TC_LOOPBACK[2];
            command[startIndex++] = F19_TC_LOOPBACK[3];

            numParameters++;

            //Int32 F20_PARAMETER_6_ID = 6;
            byte[] F20_PARAMETER_6_ID = new byte[4];
            F20_PARAMETER_6_ID[0] = 0x00;
            F20_PARAMETER_6_ID[1] = 0x00;
            F20_PARAMETER_6_ID[2] = 0x00;
            F20_PARAMETER_6_ID[3] = 0x06;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F20_PARAMETER_6_ID[0];
            command[startIndex++] = F20_PARAMETER_6_ID[1];
            command[startIndex++] = F20_PARAMETER_6_ID[2];
            command[startIndex++] = F20_PARAMETER_6_ID[3];

            //Int32 F21_FEC_PRESENT_IN_TM = 0;
            byte[] F21_FEC_PRESENT_IN_TM = new byte[4];
            F21_FEC_PRESENT_IN_TM[0] = 0x00;
            F21_FEC_PRESENT_IN_TM[1] = 0x00;
            F21_FEC_PRESENT_IN_TM[2] = 0x00;
            F21_FEC_PRESENT_IN_TM[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F21_FEC_PRESENT_IN_TM[0];
            command[startIndex++] = F21_FEC_PRESENT_IN_TM[1];
            command[startIndex++] = F21_FEC_PRESENT_IN_TM[2];
            command[startIndex++] = F21_FEC_PRESENT_IN_TM[3];

            numParameters++;

            //Int32 F22_PARAMETER_7_ID = 7;
            byte[] F22_PARAMETER_7_ID = new byte[4];
            F22_PARAMETER_7_ID[0] = 0x00;
            F22_PARAMETER_7_ID[1] = 0x00;
            F22_PARAMETER_7_ID[2] = 0x00;
            F22_PARAMETER_7_ID[3] = 0x07;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F22_PARAMETER_7_ID[0];
            command[startIndex++] = F22_PARAMETER_7_ID[1];
            command[startIndex++] = F22_PARAMETER_7_ID[2];
            command[startIndex++] = F22_PARAMETER_7_ID[3];
            
            //Int32 F23_PERTURBATION_AUTORIZATION = 0;
            byte[] F23_PERTURBATION_AUTORIZATION = new byte[4];
            F23_PERTURBATION_AUTORIZATION[0] = 0x00;
            F23_PERTURBATION_AUTORIZATION[1] = 0x00;
            F23_PERTURBATION_AUTORIZATION[2] = 0x00;
            F23_PERTURBATION_AUTORIZATION[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F23_PERTURBATION_AUTORIZATION[0];
            command[startIndex++] = F23_PERTURBATION_AUTORIZATION[1];
            command[startIndex++] = F23_PERTURBATION_AUTORIZATION[2];
            command[startIndex++] = F23_PERTURBATION_AUTORIZATION[3];

            numParameters++;

            //Int32 F24_PARAMETER_8_ID = 8;
            byte[] F24_PARAMETER_8_ID = new byte[4];
            F24_PARAMETER_8_ID[0] = 0x00;
            F24_PARAMETER_8_ID[1] = 0x00;
            F24_PARAMETER_8_ID[2] = 0x00;
            F24_PARAMETER_8_ID[3] = 0x08;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F24_PARAMETER_8_ID[0];
            command[startIndex++] = F24_PARAMETER_8_ID[1];
            command[startIndex++] = F24_PARAMETER_8_ID[2];
            command[startIndex++] = F24_PARAMETER_8_ID[3];
            
            //Int32 F25_CHECK_OUT_FACILITIES = 0;
            byte[] F25_CHECK_OUT_FACILITIES = new byte[4];
            F25_CHECK_OUT_FACILITIES[0] = 0x00;
            F25_CHECK_OUT_FACILITIES[1] = 0x00;
            F25_CHECK_OUT_FACILITIES[2] = 0x00;
            F25_CHECK_OUT_FACILITIES[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F25_CHECK_OUT_FACILITIES[0];
            command[startIndex++] = F25_CHECK_OUT_FACILITIES[1];
            command[startIndex++] = F25_CHECK_OUT_FACILITIES[2];
            command[startIndex++] = F25_CHECK_OUT_FACILITIES[3];

            numParameters++;

            //Int32 F26_PARAMETER_9_ID = 9;
            byte[] F26_PARAMETER_9_ID = new byte[4];
            F26_PARAMETER_9_ID[0] = 0x00;
            F26_PARAMETER_9_ID[1] = 0x00;
            F26_PARAMETER_9_ID[2] = 0x00;
            F26_PARAMETER_9_ID[3] = 0x09;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F26_PARAMETER_9_ID[0];
            command[startIndex++] = F26_PARAMETER_9_ID[1];
            command[startIndex++] = F26_PARAMETER_9_ID[2];
            command[startIndex++] = F26_PARAMETER_9_ID[3];

            //Int32 F27_SATELLITE_TC_DATA_FLOW_ID = 0;
            byte[] F27_SATELLITE_TC_DATA_FLOW_ID = new byte[4];
            F27_SATELLITE_TC_DATA_FLOW_ID[0] = 0x00;
            F27_SATELLITE_TC_DATA_FLOW_ID[1] = 0x00;
            F27_SATELLITE_TC_DATA_FLOW_ID[2] = 0x00;
            F27_SATELLITE_TC_DATA_FLOW_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F27_SATELLITE_TC_DATA_FLOW_ID[0];
            command[startIndex++] = F27_SATELLITE_TC_DATA_FLOW_ID[1];
            command[startIndex++] = F27_SATELLITE_TC_DATA_FLOW_ID[2];
            command[startIndex++] = F27_SATELLITE_TC_DATA_FLOW_ID[3];

            numParameters++;

            //Int32 F28_PARAMETER_10_ID = 10;
            byte[] F28_PARAMETER_10_ID = new byte[4];
            F28_PARAMETER_10_ID[0] = 0x00;
            F28_PARAMETER_10_ID[1] = 0x00;
            F28_PARAMETER_10_ID[2] = 0x00;
            F28_PARAMETER_10_ID[3] = 0x0A;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F28_PARAMETER_10_ID[0];
            command[startIndex++] = F28_PARAMETER_10_ID[1];
            command[startIndex++] = F28_PARAMETER_10_ID[2];
            command[startIndex++] = F28_PARAMETER_10_ID[3];
            
            //Int32 F29_MONITORING_FLOW_ID = 0;
            byte[] F29_MONITORING_FLOW_ID = new byte[4];
            F29_MONITORING_FLOW_ID[0] = 0x00;
            F29_MONITORING_FLOW_ID[1] = 0x00;
            F29_MONITORING_FLOW_ID[2] = 0x00;
            F29_MONITORING_FLOW_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F29_MONITORING_FLOW_ID[0];
            command[startIndex++] = F29_MONITORING_FLOW_ID[1];
            command[startIndex++] = F29_MONITORING_FLOW_ID[2];
            command[startIndex++] = F29_MONITORING_FLOW_ID[3];

            numParameters++;

            //Int32 F30_PARAMETER_11_ID = 11;
            byte[] F30_PARAMETER_11_ID = new byte[4];
            F30_PARAMETER_11_ID[0] = 0x00;
            F30_PARAMETER_11_ID[1] = 0x00;
            F30_PARAMETER_11_ID[2] = 0x00;
            F30_PARAMETER_11_ID[3] = 0x0B;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F30_PARAMETER_11_ID[0];
            command[startIndex++] = F30_PARAMETER_11_ID[1];
            command[startIndex++] = F30_PARAMETER_11_ID[2];
            command[startIndex++] = F30_PARAMETER_11_ID[3];
            
            //Int32 F31_CHECK_OPERATIONAL_PARAMETERS = 0;
            byte[] F31_CHECK_OPERATIONAL_PARAMETERS = new byte[4];
            F31_CHECK_OPERATIONAL_PARAMETERS[0] = 0x00;
            F31_CHECK_OPERATIONAL_PARAMETERS[1] = 0x00;
            F31_CHECK_OPERATIONAL_PARAMETERS[2] = 0x00;
            F31_CHECK_OPERATIONAL_PARAMETERS[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F31_CHECK_OPERATIONAL_PARAMETERS[0];
            command[startIndex++] = F31_CHECK_OPERATIONAL_PARAMETERS[1];
            command[startIndex++] = F31_CHECK_OPERATIONAL_PARAMETERS[2];
            command[startIndex++] = F31_CHECK_OPERATIONAL_PARAMETERS[3];

            numParameters++;

            //Int32 F32_PARAMETER_12_ID = 12;
            byte[] F32_PARAMETER_12_ID = new byte[4];
            F32_PARAMETER_12_ID[0] = 0x00;
            F32_PARAMETER_12_ID[1] = 0x00;
            F32_PARAMETER_12_ID[2] = 0x00;
            F32_PARAMETER_12_ID[3] = 0x0C;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F32_PARAMETER_12_ID[0];
            command[startIndex++] = F32_PARAMETER_12_ID[1];
            command[startIndex++] = F32_PARAMETER_12_ID[2];
            command[startIndex++] = F32_PARAMETER_12_ID[3];
            
            //Int32 F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA = 0;
            byte[] F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA = new byte[4];
            F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[0] = 0x00;
            F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[1] = 0x00;
            F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[2] = 0x00;
            F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[0];
            command[startIndex++] = F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[1];
            command[startIndex++] = F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[2];
            command[startIndex++] = F33_CHECK_VCID_EQUIVALENCE_IN_CLCW_DATA[3];

            numParameters++;

            // Para os parametros de FOP1 a FOP 4, temos:
            // 11111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            //        FOP 1				FOP 2             FOP 3             FOP 4
            //Int32 F34_PARAMETER_13_ID = 13;
            byte[] F34_PARAMETER_13_ID = new byte[4];
            F34_PARAMETER_13_ID[0] = 0x00;
            F34_PARAMETER_13_ID[1] = 0x00;
            F34_PARAMETER_13_ID[2] = 0x00;
            F34_PARAMETER_13_ID[3] = 0x0D;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F34_PARAMETER_13_ID[0];
            command[startIndex++] = F34_PARAMETER_13_ID[1];
            command[startIndex++] = F34_PARAMETER_13_ID[2];
            command[startIndex++] = F34_PARAMETER_13_ID[3];
            
            byte[] F35_FOP1_VCID = new byte[4];
            F35_FOP1_VCID[0] = 0x00;
            F35_FOP1_VCID[1] = 0x00;
            F35_FOP1_VCID[2] = 0x00;
            F35_FOP1_VCID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F35_FOP1_VCID[0];
            command[startIndex++] = F35_FOP1_VCID[1];
            command[startIndex++] = F35_FOP1_VCID[2];
            command[startIndex++] = F35_FOP1_VCID[3];

            numParameters++;

            //Int32 F36_PARAMETER_14_ID = 14;
            byte[] F36_PARAMETER_14_ID = new byte[4];
            F36_PARAMETER_14_ID[0] = 0x00;
            F36_PARAMETER_14_ID[1] = 0x00;
            F36_PARAMETER_14_ID[2] = 0x00;
            F36_PARAMETER_14_ID[3] = 0x0E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F36_PARAMETER_14_ID[0];
            command[startIndex++] = F36_PARAMETER_14_ID[1];
            command[startIndex++] = F36_PARAMETER_14_ID[2];
            command[startIndex++] = F36_PARAMETER_14_ID[3];
            
            byte[] F37_FOP2_VCID = new byte[4];
            F37_FOP2_VCID[0] = 0x00;
            F37_FOP2_VCID[1] = 0x00;
            F37_FOP2_VCID[2] = 0x00;
            F37_FOP2_VCID[3] = 0x01;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F37_FOP2_VCID[0];
            command[startIndex++] = F37_FOP2_VCID[1];
            command[startIndex++] = F37_FOP2_VCID[2];
            command[startIndex++] = F37_FOP2_VCID[3];

            numParameters++;

            //Int32 F38_PARAMETER_15_ID = 15;
            byte[] F38_PARAMETER_15_ID = new byte[4];
            F38_PARAMETER_15_ID[0] = 0x00;
            F38_PARAMETER_15_ID[1] = 0x00;
            F38_PARAMETER_15_ID[2] = 0x00;
            F38_PARAMETER_15_ID[3] = 0x0F;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F38_PARAMETER_15_ID[0];
            command[startIndex++] = F38_PARAMETER_15_ID[1];
            command[startIndex++] = F38_PARAMETER_15_ID[2];
            command[startIndex++] = F38_PARAMETER_15_ID[3];
            
            byte[] F39_FOP3_VCID = new byte[4];
            F39_FOP3_VCID[0] = 0x00;
            F39_FOP3_VCID[1] = 0x00;
            F39_FOP3_VCID[2] = 0x00;
            F39_FOP3_VCID[3] = 0x02;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F39_FOP3_VCID[0];
            command[startIndex++] = F39_FOP3_VCID[1];
            command[startIndex++] = F39_FOP3_VCID[2];
            command[startIndex++] = F39_FOP3_VCID[3];

            numParameters++;

            //Int32 F40_PARAMETER_16_ID = 16;
            byte[] F40_PARAMETER_16_ID = new byte[4];
            F40_PARAMETER_16_ID[0] = 0x00;
            F40_PARAMETER_16_ID[1] = 0x00;
            F40_PARAMETER_16_ID[2] = 0x00;
            F40_PARAMETER_16_ID[3] = 0x10;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F40_PARAMETER_16_ID[0];
            command[startIndex++] = F40_PARAMETER_16_ID[1];
            command[startIndex++] = F40_PARAMETER_16_ID[2];
            command[startIndex++] = F40_PARAMETER_16_ID[3];
                        
            byte[] F41_FOP4_VCID = new byte[4];
            F41_FOP4_VCID[0] = 0xFF;
            F41_FOP4_VCID[1] = 0xFF;
            F41_FOP4_VCID[2] = 0xFF;
            F41_FOP4_VCID[3] = 0xFF;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F41_FOP4_VCID[0];
            command[startIndex++] = F41_FOP4_VCID[1];
            command[startIndex++] = F41_FOP4_VCID[2];
            command[startIndex++] = F41_FOP4_VCID[3];

            numParameters++;
            
            //Int32 F42_PARAMETER_17_ID = 17;
            byte[] F42_PARAMETER_17_ID = new byte[4];
            F42_PARAMETER_17_ID[0] = 0x00;
            F42_PARAMETER_17_ID[1] = 0x00;
            F42_PARAMETER_17_ID[2] = 0x00;
            F42_PARAMETER_17_ID[3] = 0x11;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F42_PARAMETER_17_ID[0];
            command[startIndex++] = F42_PARAMETER_17_ID[1];
            command[startIndex++] = F42_PARAMETER_17_ID[2];
            command[startIndex++] = F42_PARAMETER_17_ID[3];
            
            //Int32 F43_FOP1_VCID_EQUIV = 0;
            byte[] F43_FOP1_VCID_EQUIV = new byte[4];
            F43_FOP1_VCID_EQUIV[0] = 0x00;
            F43_FOP1_VCID_EQUIV[1] = 0x00;
            F43_FOP1_VCID_EQUIV[2] = 0x00;
            F43_FOP1_VCID_EQUIV[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F43_FOP1_VCID_EQUIV[0];
            command[startIndex++] = F43_FOP1_VCID_EQUIV[1];
            command[startIndex++] = F43_FOP1_VCID_EQUIV[2];
            command[startIndex++] = F43_FOP1_VCID_EQUIV[3];

            numParameters++;
            
            //Int32 F44_PARAMETER_18_ID = 18;
            byte[] F44_PARAMETER_18_ID = new byte[4];
            F44_PARAMETER_18_ID[0] = 0x00;
            F44_PARAMETER_18_ID[1] = 0x00;
            F44_PARAMETER_18_ID[2] = 0x00;
            F44_PARAMETER_18_ID[3] = 0x12;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F44_PARAMETER_18_ID[0];
            command[startIndex++] = F44_PARAMETER_18_ID[1];
            command[startIndex++] = F44_PARAMETER_18_ID[2];
            command[startIndex++] = F44_PARAMETER_18_ID[3];
            
            //Int32 FOP2_VCID_EQUIV = 1;
            byte[] F45_FOP2_VCID_EQUIV = new byte[4];
            F45_FOP2_VCID_EQUIV[0] = 0x00;
            F45_FOP2_VCID_EQUIV[1] = 0x00;
            F45_FOP2_VCID_EQUIV[2] = 0x00;
            F45_FOP2_VCID_EQUIV[3] = 0x01;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F45_FOP2_VCID_EQUIV[0];
            command[startIndex++] = F45_FOP2_VCID_EQUIV[1];
            command[startIndex++] = F45_FOP2_VCID_EQUIV[2];
            command[startIndex++] = F45_FOP2_VCID_EQUIV[3];

            numParameters++;
            
            //Int32 F46_PARAMETER_19_ID = 19;
            byte[] F46_PARAMETER_19_ID = new byte[4];
            F46_PARAMETER_19_ID[0] = 0x00;
            F46_PARAMETER_19_ID[1] = 0x00;
            F46_PARAMETER_19_ID[2] = 0x00;
            F46_PARAMETER_19_ID[3] = 0x13;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F46_PARAMETER_19_ID[0];
            command[startIndex++] = F46_PARAMETER_19_ID[1];
            command[startIndex++] = F46_PARAMETER_19_ID[2];
            command[startIndex++] = F46_PARAMETER_19_ID[3];
            
            //Int32 F47_FOP3_VCID_EQUIV = 2;
            byte[] F47_FOP3_VCID_EQUIV = new byte[4];
            F47_FOP3_VCID_EQUIV[0] = 0x00;
            F47_FOP3_VCID_EQUIV[1] = 0x00;
            F47_FOP3_VCID_EQUIV[2] = 0x00;
            F47_FOP3_VCID_EQUIV[3] = 0x02;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F47_FOP3_VCID_EQUIV[0];
            command[startIndex++] = F47_FOP3_VCID_EQUIV[1];
            command[startIndex++] = F47_FOP3_VCID_EQUIV[2];
            command[startIndex++] = F47_FOP3_VCID_EQUIV[3];

            numParameters++;
            
            //Int32 F48_PARAMETER_20_ID = 20;
            byte[] F48_PARAMETER_20_ID = new byte[4];
            F48_PARAMETER_20_ID[0] = 0x00;
            F48_PARAMETER_20_ID[1] = 0x00;
            F48_PARAMETER_20_ID[2] = 0x00;
            F48_PARAMETER_20_ID[3] = 0x14;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F48_PARAMETER_20_ID[0];
            command[startIndex++] = F48_PARAMETER_20_ID[1];
            command[startIndex++] = F48_PARAMETER_20_ID[2];
            command[startIndex++] = F48_PARAMETER_20_ID[3];
            
            //Int32 FOP4_VCID_EQUIV = -1;
            byte[] F49_FOP4_VCID_EQUIV = new byte[4];
            F49_FOP4_VCID_EQUIV[0] = 0xFF;
            F49_FOP4_VCID_EQUIV[1] = 0xFF;
            F49_FOP4_VCID_EQUIV[2] = 0xFF;
            F49_FOP4_VCID_EQUIV[3] = 0xFF;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F49_FOP4_VCID_EQUIV[0];
            command[startIndex++] = F49_FOP4_VCID_EQUIV[1];
            command[startIndex++] = F49_FOP4_VCID_EQUIV[2];
            command[startIndex++] = F49_FOP4_VCID_EQUIV[3];

            numParameters++;
            
            //Int32 F50_PARAMETER_21_ID = 21;
            byte[] F50_PARAMETER_21_ID = new byte[4];
            F50_PARAMETER_21_ID[0] = 0x00;
            F50_PARAMETER_21_ID[1] = 0x00;
            F50_PARAMETER_21_ID[2] = 0x00;
            F50_PARAMETER_21_ID[3] = 0x15;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F50_PARAMETER_21_ID[0];
            command[startIndex++] = F50_PARAMETER_21_ID[1];
            command[startIndex++] = F50_PARAMETER_21_ID[2];
            command[startIndex++] = F50_PARAMETER_21_ID[3];
            
            //Int32 F51_CLCW_OFFSET_IN_TM_FRAME = 0;
            byte[] F51_CLCW_OFFSET_IN_TM_FRAME = new byte[4];
            F51_CLCW_OFFSET_IN_TM_FRAME[0] = 0x00;
            F51_CLCW_OFFSET_IN_TM_FRAME[1] = 0x00;
            F51_CLCW_OFFSET_IN_TM_FRAME[2] = 0x00;
            F51_CLCW_OFFSET_IN_TM_FRAME[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F51_CLCW_OFFSET_IN_TM_FRAME[0];
            command[startIndex++] = F51_CLCW_OFFSET_IN_TM_FRAME[1];
            command[startIndex++] = F51_CLCW_OFFSET_IN_TM_FRAME[2];
            command[startIndex++] = F51_CLCW_OFFSET_IN_TM_FRAME[3];

            numParameters++;

            //Int32 F52_PARAMETER_22_ID = 22;
            byte[] F52_PARAMETER_22_ID = new byte[4];
            F52_PARAMETER_22_ID[0] = 0x00;
            F52_PARAMETER_22_ID[1] = 0x00;
            F52_PARAMETER_22_ID[2] = 0x00;
            F52_PARAMETER_22_ID[3] = 0x16;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F52_PARAMETER_22_ID[0];
            command[startIndex++] = F52_PARAMETER_22_ID[1];
            command[startIndex++] = F52_PARAMETER_22_ID[2];
            command[startIndex++] = F52_PARAMETER_22_ID[3];
            
            //Int32 F53_OCF_OFFSET_IN_TM_FRAME = 0;
            byte[] F53_OCF_OFFSET_IN_TM_FRAME = new byte[4];
            F53_OCF_OFFSET_IN_TM_FRAME[0] = 0x00;
            F53_OCF_OFFSET_IN_TM_FRAME[1] = 0x00;
            F53_OCF_OFFSET_IN_TM_FRAME[2] = 0x00;
            F53_OCF_OFFSET_IN_TM_FRAME[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F53_OCF_OFFSET_IN_TM_FRAME[0];
            command[startIndex++] = F53_OCF_OFFSET_IN_TM_FRAME[1];
            command[startIndex++] = F53_OCF_OFFSET_IN_TM_FRAME[2];
            command[startIndex++] = F53_OCF_OFFSET_IN_TM_FRAME[3];

            numParameters++;

            // OS CAMPOS DE 23 ATE 39 NAO SAO USADOS
            
            //Int32 F71_END_OF_MESSAGE = -1234567890;
            byte[] F71_END_OF_MESSAGE = new byte[4]; //B669FD2E
            F71_END_OF_MESSAGE[0] = 0xB6;
            F71_END_OF_MESSAGE[1] = 0x69;
            F71_END_OF_MESSAGE[2] = 0xFD;
            F71_END_OF_MESSAGE[3] = 0x2E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F71_END_OF_MESSAGE[0];
            command[startIndex++] = F71_END_OF_MESSAGE[1];
            command[startIndex++] = F71_END_OF_MESSAGE[2];
            command[startIndex++] = F71_END_OF_MESSAGE[3];
            
            byte[] arrayNumParameters = new byte[4];
            Array.Copy(BitConverter.GetBytes(numParameters), 0, arrayNumParameters, 0, 4);
            command[24] = arrayNumParameters[3];
            command[25] = arrayNumParameters[2];
            command[26] = arrayNumParameters[1];
            command[27] = arrayNumParameters[0];
            
            Int32 size = command.Length;
            byte[] arraySize = new byte[4];
            Array.Copy(BitConverter.GetBytes(size), 0, arraySize, 0, 4);
            command[4] = arraySize[3];
            command[5] = arraySize[2];
            command[6] = arraySize[1];
            command[7] = arraySize[0];

            SendCommand(port, command);
        }

        // mensagem 30
        public void COPRequestToTransferSegmentBD(String port, byte[] request)
        {
            byte[] command = new byte[0];
            int startIndex = 0;

            //Int32 F1_START_OF_MESSAGE = 1234567890; // 49 96 02 D2
            byte[] F1_START_OF_MESSAGE = new byte[4];
            F1_START_OF_MESSAGE[0] = 0x49;
            F1_START_OF_MESSAGE[1] = 0x96;
            F1_START_OF_MESSAGE[2] = 0x02;
            F1_START_OF_MESSAGE[3] = 0xD2;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F1_START_OF_MESSAGE[0];
            command[startIndex++] = F1_START_OF_MESSAGE[1];
            command[startIndex++] = F1_START_OF_MESSAGE[2];
            command[startIndex++] = F1_START_OF_MESSAGE[3];

            //Int32 F2_SIZE_OF_MESSAGE = 00;
            byte[] F2_SIZE_OF_MESSAGE = new byte[4];
            F2_SIZE_OF_MESSAGE[0] = 0x00;
            F2_SIZE_OF_MESSAGE[1] = 0x00;
            F2_SIZE_OF_MESSAGE[2] = 0x00;
            F2_SIZE_OF_MESSAGE[3] = 0x00;  // No final da montagem do comando o campo SIZE_OF_MESSAGE eh preenchido.
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F2_SIZE_OF_MESSAGE[0];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[1];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[2];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[3];

            //Int32 F3_FLOW_ID = 1; // 0 = TC Data
            byte[] F3_FLOW_ID = new byte[4];
            F3_FLOW_ID[0] = 0x00;
            F3_FLOW_ID[1] = 0x00;
            F3_FLOW_ID[2] = 0x00;
            F3_FLOW_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F3_FLOW_ID[0];
            command[startIndex++] = F3_FLOW_ID[1];
            command[startIndex++] = F3_FLOW_ID[2];
            command[startIndex++] = F3_FLOW_ID[3];

            //Int32 F4_TYPE_OF_OPERATION = 40; // (40 = Transfer CLTU - 0x28) 
            //                                    (20 = Transfer Segment (AD) - 0x14)
            //                                    (30 = Transfer Segment (BD) - 0x1E)
            //                                    (50 = Direct Transfer Frame - 0x32)
            byte[] F4_TYPE_OF_OPERATION = new byte[4];
            F4_TYPE_OF_OPERATION[0] = 0x00;
            F4_TYPE_OF_OPERATION[1] = 0x00;
            F4_TYPE_OF_OPERATION[2] = 0x00;
            F4_TYPE_OF_OPERATION[3] = 0x1E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F4_TYPE_OF_OPERATION[0];
            command[startIndex++] = F4_TYPE_OF_OPERATION[1];
            command[startIndex++] = F4_TYPE_OF_OPERATION[2];
            command[startIndex++] = F4_TYPE_OF_OPERATION[3];

            //Int32 F5_VCID = 1 (0 = TCD -  UTMC) (1 = TCR - UPC)
            byte[] F5_VCID = new byte[4];
            F5_VCID = BitConverter.GetBytes(int.Parse(vcid));
            Array.Reverse(F5_VCID);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F5_VCID[0];
            command[startIndex++] = F5_VCID[1];
            command[startIndex++] = F5_VCID[2];
            command[startIndex++] = F5_VCID[3];

            //Int32 F6_REQUEST_TAG = 0;
            byte[] F6_REQUEST_TAG = new byte[4];
            F6_REQUEST_TAG[0] = 0xAB;
            F6_REQUEST_TAG[1] = 0xCD;
            F6_REQUEST_TAG[2] = 0xEF;
            F6_REQUEST_TAG[3] = 0xFF;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F6_REQUEST_TAG[0];
            command[startIndex++] = F6_REQUEST_TAG[1];
            command[startIndex++] = F6_REQUEST_TAG[2];
            command[startIndex++] = F6_REQUEST_TAG[3];

            // Adicionar o byte de segmentacao do request.
            byte[] requestSegmented = new byte[request.Length + 1];
            Array.Copy(request, 0, requestSegmented, 1, request.Length);
            requestSegmented[0] = 0xC0;

            //Int32 F7_NUMBER_OF_BYTES = 00;
            Int32 F7_NUMBER_OF_BYTES = requestSegmented.Length;
            byte[] arrayNum = new byte[4];
            Array.Copy(BitConverter.GetBytes(F7_NUMBER_OF_BYTES), 0, arrayNum, 0, 4);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = arrayNum[3];
            command[startIndex++] = arrayNum[2];
            command[startIndex++] = arrayNum[1];
            command[startIndex++] = arrayNum[0];

            // Daqui pra frente, alimentar a mensagem com o frameTc.
            // O frameTC eh inserido de 1 : 1 byte, ou seja, 1 byte do frame para 1 byte da mensagem.
            // A mesma nao eh preenchida noo formato de 32 bits pra cada byte.
            // Cada palavra da mensagem tcpIp de 32 bits recebe 4 bytes do frame, e nao um byte do  frame para cada 4 da msg tcpIp.
            Array.Resize(ref command, command.Length + F7_NUMBER_OF_BYTES);

            for (int i = 0; i < F7_NUMBER_OF_BYTES; i++)
            {
                command[startIndex++] = requestSegmented[i];
            }

            // Se o F7_NUMBER_OF_BYTES nao for multiplo de 4, entao acrescenta 0 (zero) nos bytes faltantes
            // para transforma-la em multiplo de 4.
            int mod4 = (F7_NUMBER_OF_BYTES % 4);

            // mod4 eh o resto da divisao do F7_NUMBER_OF_BYTES por 4.
            // Se mod4 > 0, o resto deve ser acrescentado na mensagem com zeros
            if (mod4 > 0)
            {
                Array.Resize(ref command, command.Length + (4 - mod4));

                for (int i = 0; i < (4 - mod4); i++)
                {
                    command[startIndex++] = 0x00;
                }
            }

            // Alimentar o tamanho da mensagem antes de calcular o Check-Sum
            Int32 sizeMessage = command.Length + 8; // + 8 bytes porque o checksum e o postamble tbm entram no tamanho da mensagem
            byte[] arraySize = new byte[4];
            Array.Copy(BitConverter.GetBytes(sizeMessage), 0, arraySize, 0, 4);
            command[4] = arraySize[3];
            command[5] = arraySize[2];
            command[6] = arraySize[1];
            command[7] = arraySize[0];

            // Calcular o Check-Sum (complemento de 2).
            // No complemento de 2, deve-se somar byte a byte da mensagem.
            // Em seguida, fazer o inverso do resultado e somar 1.
            // O resultado do inverso mais 1 deve ser igual ao resultado da soma dos bytes.
            UInt32 sumData = 0;
            byte[] arTemp = new byte[4];

            for (int i = 4; i < command.Length; i++)
            {
                arTemp[0] = command[i++];
                arTemp[1] = command[i++];
                arTemp[2] = command[i++];
                arTemp[3] = command[i];
                Array.Reverse(arTemp);
                sumData += BitConverter.ToUInt32(arTemp, 0);
            }

            UInt32 val = 0xFFFFFFFF;
            UInt32 sumInverse = val - sumData;
            sumInverse++;

            // A soma de sumData com sumInverse tem que ser 0 zero.
            // sumInverse eh o checksum da mensagem.

            //Int32 F7_CHECKSUM = -1234567890;
            byte[] F7_CHECKSUM = new byte[4]; //B669FD2E
            F7_CHECKSUM = BitConverter.GetBytes(sumInverse);
            Array.Reverse(F7_CHECKSUM);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F7_CHECKSUM[0];
            command[startIndex++] = F7_CHECKSUM[1];
            command[startIndex++] = F7_CHECKSUM[2];
            command[startIndex++] = F7_CHECKSUM[3];

            //Int32 F71_END_OF_MESSAGE = -1234567890;
            byte[] F8_END_OF_MESSAGE = new byte[4]; //B669FD2E
            F8_END_OF_MESSAGE[0] = 0xB6;
            F8_END_OF_MESSAGE[1] = 0x69;
            F8_END_OF_MESSAGE[2] = 0xFD;
            F8_END_OF_MESSAGE[3] = 0x2E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F8_END_OF_MESSAGE[0];
            command[startIndex++] = F8_END_OF_MESSAGE[1];
            command[startIndex++] = F8_END_OF_MESSAGE[2];
            command[startIndex++] = F8_END_OF_MESSAGE[3];

            SendCommand(port, command);
        }

        // mensagem 50
        public void COPRequestToDirectTransferFrame(String port, byte[] frameTc)
        {
            byte[] command = new byte[0];
            int startIndex = 0;

            //Int32 F1_START_OF_MESSAGE = 1234567890; // 49 96 02 D2
            byte[] F1_START_OF_MESSAGE = new byte[4];
            F1_START_OF_MESSAGE[0] = 0x49;
            F1_START_OF_MESSAGE[1] = 0x96;
            F1_START_OF_MESSAGE[2] = 0x02;
            F1_START_OF_MESSAGE[3] = 0xD2;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F1_START_OF_MESSAGE[0];
            command[startIndex++] = F1_START_OF_MESSAGE[1];
            command[startIndex++] = F1_START_OF_MESSAGE[2];
            command[startIndex++] = F1_START_OF_MESSAGE[3];

            //Int32 F2_SIZE_OF_MESSAGE = 00;
            byte[] F2_SIZE_OF_MESSAGE = new byte[4];
            F2_SIZE_OF_MESSAGE[0] = 0x00;
            F2_SIZE_OF_MESSAGE[1] = 0x00;
            F2_SIZE_OF_MESSAGE[2] = 0x00;
            F2_SIZE_OF_MESSAGE[3] = 0x00;  // No final da montagem do comando o campo SIZE_OF_MESSAGE eh preenchido.
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F2_SIZE_OF_MESSAGE[0];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[1];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[2];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[3];

            //Int32 F3_FLOW_ID = 1; // 0 = TC Data
            byte[] F3_FLOW_ID = new byte[4];
            F3_FLOW_ID[0] = 0x00;
            F3_FLOW_ID[1] = 0x00;
            F3_FLOW_ID[2] = 0x00;
            F3_FLOW_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F3_FLOW_ID[0];
            command[startIndex++] = F3_FLOW_ID[1];
            command[startIndex++] = F3_FLOW_ID[2];
            command[startIndex++] = F3_FLOW_ID[3];

            //Int32 F4_TYPE_OF_OPERATION = 40; // (40 = Transfer CLTU - 0x28) 
            //                                    (20 = Transfer Segment (AD) - 0x14)
            //                                    (30 = Transfer Segment (BD) - 0x1E)
            //                                    (50 = Direct Transfer Frame - 0x32)
            byte[] F4_TYPE_OF_OPERATION = new byte[4];
            F4_TYPE_OF_OPERATION[0] = 0x00;
            F4_TYPE_OF_OPERATION[1] = 0x00;
            F4_TYPE_OF_OPERATION[2] = 0x00;
            F4_TYPE_OF_OPERATION[3] = 0x32;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F4_TYPE_OF_OPERATION[0];
            command[startIndex++] = F4_TYPE_OF_OPERATION[1];
            command[startIndex++] = F4_TYPE_OF_OPERATION[2];
            command[startIndex++] = F4_TYPE_OF_OPERATION[3];

            //Int32 F5_CODEBLOCK = 1 (0 to use standard control parameter or (5-6-7-8) to force value.)
            byte[] F5_CODEBLOCK = new byte[4];
            F5_CODEBLOCK[0] = 0x00;
            F5_CODEBLOCK[1] = 0x00;
            F5_CODEBLOCK[2] = 0x00;
            F5_CODEBLOCK[3] = 0x08;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F5_CODEBLOCK[0];
            command[startIndex++] = F5_CODEBLOCK[1];
            command[startIndex++] = F5_CODEBLOCK[2];
            command[startIndex++] = F5_CODEBLOCK[3];

            //Int32 F6_REQUEST_TAG = 0;
            byte[] F6_REQUEST_TAG = new byte[4];
            F6_REQUEST_TAG[0] = 0x00;
            F6_REQUEST_TAG[1] = 0xFF;
            F6_REQUEST_TAG[2] = 0xEF;
            F6_REQUEST_TAG[3] = 0xEE;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F6_REQUEST_TAG[0];
            command[startIndex++] = F6_REQUEST_TAG[1];
            command[startIndex++] = F6_REQUEST_TAG[2];
            command[startIndex++] = F6_REQUEST_TAG[3];

            //Int32 F7_NUMBER_OF_BYTES = 00;
            Int32 F7_NUMBER_OF_BYTES = frameTc.Length;
            byte[] arrayNum = new byte[4];
            Array.Copy(BitConverter.GetBytes(F7_NUMBER_OF_BYTES), 0, arrayNum, 0, 4);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = arrayNum[3];
            command[startIndex++] = arrayNum[2];
            command[startIndex++] = arrayNum[1];
            command[startIndex++] = arrayNum[0];

            // Daqui pra frente, alimentar a mensagem com o frameTc.
            // O frameTC eh inserido de 1 : 1 byte, ou seja, 1 byte do frame para 1 byte da mensagem.
            // A mesma nao eh preenchida noo formato de 32 bits pra cada byte.
            // Cada palavra da mensagem tcpIp de 32 bits recebe 4 bytes do frame, e nao um byte do  frame para cada 4 da msg tcpIp.
            Array.Resize(ref command, command.Length + F7_NUMBER_OF_BYTES);

            for (int i = 0; i < F7_NUMBER_OF_BYTES; i++)
            {
                command[startIndex++] = frameTc[i];
            }

            // Se o F7_NUMBER_OF_BYTES nao for multiplo de 4, entao acrescenta 0 (zero) nos bytes faltantes
            // para transforma-la em multiplo de 4.
            int mod4 = (F7_NUMBER_OF_BYTES % 4);

            // mod4 eh o resto da divisao do F7_NUMBER_OF_BYTES por 4.
            // Se mod4 > 0, o resto deve ser acrescentado na mensagem com zeros
            if (mod4 > 0)
            {
                Array.Resize(ref command, command.Length + (4 - mod4));

                for (int i = 0; i < (4 - mod4); i++)
                {
                    command[startIndex++] = 0x00;
                }
            }

            // Alimentar o tamanho da mensagem antes de calcular o Check-Sum
            Int32 sizeMessage = command.Length + 8; // + 8 bytes porque o checksum e o postamble tbm entram no tamanho da mensagem
            byte[] arraySize = new byte[4];
            Array.Copy(BitConverter.GetBytes(sizeMessage), 0, arraySize, 0, 4);
            command[4] = arraySize[3];
            command[5] = arraySize[2];
            command[6] = arraySize[1];
            command[7] = arraySize[0];

            // Calcular o Check-Sum (complemento de 2).
            // No complemento de 2, deve-se somar byte a byte da mensagem.
            // Em seguida, fazer o inverso do resultado e somar 1.
            // O resultado do inverso mais 1 deve ser igual ao resultado da soma dos bytes.
            UInt32 sumData = 0;
            byte[] arTemp = new byte[4];

            for (int i = 4; i < command.Length; i++)
            {
                arTemp[0] = command[i++];
                arTemp[1] = command[i++];
                arTemp[2] = command[i++];
                arTemp[3] = command[i];
                Array.Reverse(arTemp);
                sumData += BitConverter.ToUInt32(arTemp, 0);
            }

            UInt32 val = 0xFFFFFFFF;
            UInt32 sumInverse = val - sumData;
            sumInverse++;

            // A soma de sumData com sumInverse tem que ser 0 zero.
            // sumInverse eh o checksum da mensagem.

            //Int32 F7_CHECKSUM = -1234567890;
            byte[] F7_CHECKSUM = new byte[4]; //B669FD2E
            F7_CHECKSUM = BitConverter.GetBytes(sumInverse);
            Array.Reverse(F7_CHECKSUM);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F7_CHECKSUM[0];
            command[startIndex++] = F7_CHECKSUM[1];
            command[startIndex++] = F7_CHECKSUM[2];
            command[startIndex++] = F7_CHECKSUM[3];

            //Int32 F71_END_OF_MESSAGE = -1234567890;
            byte[] F8_END_OF_MESSAGE = new byte[4]; //B669FD2E
            F8_END_OF_MESSAGE[0] = 0xB6;
            F8_END_OF_MESSAGE[1] = 0x69;
            F8_END_OF_MESSAGE[2] = 0xFD;
            F8_END_OF_MESSAGE[3] = 0x2E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F8_END_OF_MESSAGE[0];
            command[startIndex++] = F8_END_OF_MESSAGE[1];
            command[startIndex++] = F8_END_OF_MESSAGE[2];
            command[startIndex++] = F8_END_OF_MESSAGE[3];

            SendCommand(port, command);
        }

        // mensagem 40
        public void COPRequestTODirectTransferCLTU(String port, byte[] cltu)
        {
            byte[] command = new byte[0];
            int startIndex = 0;

            //Int32 F1_START_OF_MESSAGE = 1234567890; // 49 96 02 D2
            byte[] F1_START_OF_MESSAGE = new byte[4];
            F1_START_OF_MESSAGE[0] = 0x49;
            F1_START_OF_MESSAGE[1] = 0x96;
            F1_START_OF_MESSAGE[2] = 0x02;
            F1_START_OF_MESSAGE[3] = 0xD2;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F1_START_OF_MESSAGE[0];
            command[startIndex++] = F1_START_OF_MESSAGE[1];
            command[startIndex++] = F1_START_OF_MESSAGE[2];
            command[startIndex++] = F1_START_OF_MESSAGE[3];

            //Int32 F2_SIZE_OF_MESSAGE = 00;
            byte[] F2_SIZE_OF_MESSAGE = new byte[4];
            F2_SIZE_OF_MESSAGE[0] = 0x00;
            F2_SIZE_OF_MESSAGE[1] = 0x00;
            F2_SIZE_OF_MESSAGE[2] = 0x00;
            F2_SIZE_OF_MESSAGE[3] = 0x00;  // No final da montagem do comando o campo SIZE_OF_MESSAGE eh preenchido.
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F2_SIZE_OF_MESSAGE[0];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[1];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[2];
            command[startIndex++] = F2_SIZE_OF_MESSAGE[3];
                        
            //Int32 F3_FLOW_ID = 1; // 0 = TC Data
            byte[] F3_FLOW_ID = new byte[4];
            F3_FLOW_ID[0] = 0x00;
            F3_FLOW_ID[1] = 0x00;
            F3_FLOW_ID[2] = 0x00;
            F3_FLOW_ID[3] = 0x00;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F3_FLOW_ID[0];
            command[startIndex++] = F3_FLOW_ID[1];
            command[startIndex++] = F3_FLOW_ID[2];
            command[startIndex++] = F3_FLOW_ID[3];
            
            //Int32 F4_TYPE_OF_OPERATION = 40; // (40 = Transfer CLTU - 0x28) 
            //                                    (20 = Transfer Segment (AD) - 0x14)
            //                                    (30 = Transfer Segment (BD) - 0x1E)
            byte[] F4_TYPE_OF_OPERATION = new byte[4];
            F4_TYPE_OF_OPERATION[0] = 0x00;
            F4_TYPE_OF_OPERATION[1] = 0x00;
            F4_TYPE_OF_OPERATION[2] = 0x00;
            F4_TYPE_OF_OPERATION[3] = 0x28;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F4_TYPE_OF_OPERATION[0];
            command[startIndex++] = F4_TYPE_OF_OPERATION[1];
            command[startIndex++] = F4_TYPE_OF_OPERATION[2];
            command[startIndex++] = F4_TYPE_OF_OPERATION[3];
            
            //Int32 F5_VCID = 1 (0 = TCD -  UTMC) (1 = TCR - UPC)
            byte[] F5_VCID = new byte[4];
            F5_VCID = BitConverter.GetBytes(int.Parse(vcid));
            Array.Reverse(F5_VCID);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F5_VCID[0];
            command[startIndex++] = F5_VCID[1];
            command[startIndex++] = F5_VCID[2];
            command[startIndex++] = F5_VCID[3];
                        
            //Int32 F6_REQUEST_TAG = 0;
            byte[] F6_REQUEST_TAG = new byte[4];
            F6_REQUEST_TAG[0] = 0x00;
            F6_REQUEST_TAG[1] = 0x00;
            F6_REQUEST_TAG[2] = 0x00;
            F6_REQUEST_TAG[3] = 0x01;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F6_REQUEST_TAG[0];
            command[startIndex++] = F6_REQUEST_TAG[1];
            command[startIndex++] = F6_REQUEST_TAG[2];
            command[startIndex++] = F6_REQUEST_TAG[3];
            
            //Int32 F7_NUMBER_OF_BYTES = 00;
            Int32 F7_NUMBER_OF_BYTES = cltu.Length;
            byte[] arrayNum = new byte[4];
            Array.Copy(BitConverter.GetBytes(F7_NUMBER_OF_BYTES), 0, arrayNum, 0, 4);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = arrayNum[3];
            command[startIndex++] = arrayNum[2];
            command[startIndex++] = arrayNum[1];
            command[startIndex++] = arrayNum[0];
            
            // Daqui pra frente, alimentar a mensagem com a CLTU.
            // A CLTU eh inserida de 1 : 1 byte, ou seja, 1 byte da CLTU para 1 byte da mensagem.
            // A mesma nao eh preenchida noo formato de 32 bits pra cada byte.
            // Cada palavra da mensagem tcpIp de 32 bits recebe 4 bytes da CLTU.
            Array.Resize(ref command, command.Length + F7_NUMBER_OF_BYTES);

            for (int i = 0; i < F7_NUMBER_OF_BYTES; i++)
            {
                command[startIndex++] = cltu[i];
            }
            
            // Se o F7_NUMBER_OF_BYTES nao for multiplo de 4, entao acrescenta 0 (zero) nos bytes faltantes
            // para transforma-la em multiplo de 4.
            int mod4 = (F7_NUMBER_OF_BYTES % 4);
            
            // mod4 eh o resto da divisao do F7_NUMBER_OF_BYTES por 4.
            // Se mod4 > 0, o resto deve ser acrescentado na mensagem com zeros
            if (mod4 > 0)
            {
                Array.Resize(ref command, command.Length + (4 - mod4));

                for (int i = 0; i < (4 - mod4); i++)
                {
                    command[startIndex++] = 0x00;
                }
            }
            
            // Alimentar o tamanho da mensagem antes de calcular o Check-Sum
            Int32 sizeMessage = command.Length + 8; // + 8 bytes porque o checksum e o postamble tbm entram no tamanho da mensagem
            byte[] arraySize = new byte[4];
            Array.Copy(BitConverter.GetBytes(sizeMessage), 0, arraySize, 0, 4);
            command[4] = arraySize[3];
            command[5] = arraySize[2];
            command[6] = arraySize[1];
            command[7] = arraySize[0];
            
            // Calcular o Check-Sum (complemento de 2).
            // No complemento de 2, deve-se somar byte a byte da mensagem.
            // Em seguida, fazer o inverso do resultado e somar 1.
            // O resultado do inverso mais 1 deve ser igual ao resultado da soma dos bytes.
            UInt32 sumData = 0;
            byte[] arTemp = new byte[4];

            for (int i = 4; i < command.Length; i++)
            {
                arTemp[0] = command[i++];
                arTemp[1] = command[i++];
                arTemp[2] = command[i++];
                arTemp[3] = command[i];
                Array.Reverse(arTemp);
                sumData += BitConverter.ToUInt32(arTemp, 0);
            }

            UInt32 val = 0xFFFFFFFF;
            UInt32 sumInverse = val - sumData;
            sumInverse++;
            // A soma de sumData com sumInverse tem que ser 0 zero.
            // sumInverse eh o checksum da mensagem.
            
            //Int32 F7_CHECKSUM = -1234567890;
            byte[] F7_CHECKSUM = new byte[4]; //B669FD2E
            F7_CHECKSUM = BitConverter.GetBytes(sumInverse);
            Array.Reverse(F7_CHECKSUM);
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F7_CHECKSUM[0];
            command[startIndex++] = F7_CHECKSUM[1];
            command[startIndex++] = F7_CHECKSUM[2];
            command[startIndex++] = F7_CHECKSUM[3];
            
            //Int32 F71_END_OF_MESSAGE = -1234567890;
            byte[] F8_END_OF_MESSAGE = new byte[4]; //B669FD2E
            F8_END_OF_MESSAGE[0] = 0xB6;
            F8_END_OF_MESSAGE[1] = 0x69;
            F8_END_OF_MESSAGE[2] = 0xFD;
            F8_END_OF_MESSAGE[3] = 0x2E;
            Array.Resize(ref command, command.Length + 4);
            command[startIndex++] = F8_END_OF_MESSAGE[0];
            command[startIndex++] = F8_END_OF_MESSAGE[1];
            command[startIndex++] = F8_END_OF_MESSAGE[2];
            command[startIndex++] = F8_END_OF_MESSAGE[3];
            
            SendCommand(port, command);
        }
        
        /*
         * Este metodo sera usado na configuracao das diretivas do COP-1 para a operacao em modo AD.
         * Atualmente deseja-se manter a comunicacao BD.
         */
        public void COPSendDirectiveRequest(Directive directiveService, int optionalParameter)
        {
            switch ((int)directiveService)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {

                        break;
                    }
                default: break;
            }
        }

        #endregion
    }
}

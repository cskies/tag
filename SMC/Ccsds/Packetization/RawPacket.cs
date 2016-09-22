/**
 * @file 	    RawPacket.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 12/11/2011 por Fabricio.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; // para o StringWriter
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Forms; // para o Timer
using System.Data;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Este Namespace contem as rotinas de tratamento dos bytes a serem enviados e recebidos.
 */ 
namespace Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization
{
    /**
     * @class RawPacket
     * Classe para a composicao, manipulacao e interpretacao de pacotes CCSDS/PUS.
     **/
    public class RawPacket : RawBytes
    {
        public enum ReceptionStatus
        {
            receiving_packet,
            packet_completed,
            packet_completed_with_error,
        };

        #region Atributos Privados e Propriedades

        private bool isRequest;
        private bool isDeviceCommand;
        private int receivedCounter = 0;
        private int packetLength = 0;
        private int totalPacketLength = 0;
        private Timer timer = new Timer();

        private bool autoCrc;

        public bool AutoCrc
        {
            set
            {
                autoCrc = value;
            }
        }

        public bool IsDeviceCommand
        {
            set
            {
                SetupPacket(true, value);
            }
            get
            {
                return isDeviceCommand;
            }
        }
        
        #endregion

        #region Construtor

        public RawPacket(bool isTc, bool isTcd)
        {
            SetupPacket(isTc, isTcd);
        }

        /**
         * Contrutor para a composicao de pacotes atraves da passagem do
         * tipo e subtipo como argumentos.
         * */
        public RawPacket(int type, int subtype)
        {
            string sql = null;
            autoCrc = true;
            DataTable dataTable = null;           

            // TODO: levantar o is_request e, com base nisso, alimentar o tipo do pacote
            sql = @"select top 1
	                    a.allow_repetition,
	                    isnull(b.same_as_subtype, a.service_subtype) as copied_from_subtype,
                        a.is_request
                    from 
	                    subtypes a 
	                    left join subtype_structure b on
		                    a.service_type = b.service_type and
		                    a.service_subtype = b.service_subtype 
                    where
	                    a.service_type = " + type +
	                    " and a.service_subtype = " + subtype;
           
            dataTable = DbInterface.GetDataTable(sql);

            bool allowRepetion = (bool)dataTable.Rows[0]["allow_repetition"];
            int copySubtype = (int)dataTable.Rows[0]["copied_from_subtype"];
            isRequest = (bool)dataTable.Rows[0]["is_request"];          

            // Seta, de uma vez so, o cabecalho do pacote e da area de dados
            rawContent = new byte[12]; // tamanho do maior campo do pacote de tempo

            if (isRequest) // campo padrao
            {
                rawContent[0] = 0x18;
            }
            else
            {
                rawContent[0] = 0x08;
            }

            DbConfiguration.Load();
            

            
            rawContent[1] = Convert.ToByte(DbConfiguration.RequestsDefaultApid); // apid, OBDH
            rawContent[2] = 0xC0; // sequence flags = stand-alone packet
            rawContent[3] = 0x01; // sequence count = 1
            rawContent[4] = 0x00;
            rawContent[5] = 0x07; // packet length
            rawContent[6] = 0x10; // campos padrao, sem ack
            rawContent[7] = (byte)type; // service type
            rawContent[8] = (byte)subtype; // service subtype;
            rawContent[9] = Convert.ToByte(DbConfiguration.RequestsDefaultSourceId); // source id         

            UInt16 packetSize = 12; // tamanho padrao do pacote de TC  

            // faz um getscalar com a query:
            sql = @"select isnull((sum(number_of_bits) / 8),0) from subtype_structure sb inner join data_fields dt 
	                on sb.data_field_id = dt.data_field_id where service_type = " + type + " 	and service_subtype = " + copySubtype;     
                        
            int sumNumberBits = (int)DbInterface.ExecuteScalar(sql);
            packetSize += (UInt16)sumNumberBits;            

            int startBit = 80;

            if (allowRepetion)
            {
                packetSize++;
                Resize(packetSize);

                byte[] nField = new byte[1];

                nField[0] = 1;
                SetPart(startBit, 8, nField);
                startBit += 8;
            }
            else
            {
                Resize(packetSize);
            }

            sql = @"select 
	                    dt.number_of_bits,
	                    isnull(sb.default_value, 0) as value
                    from subtype_structure sb
	                    inner join data_fields dt 
	                    on sb.data_field_id = dt.data_field_id
                    where 
	                    service_type = " + type +
                        "and service_subtype = " + copySubtype + " order by position";
           
            int value;
            byte[] bytes;
            byte[] bytesToGetPart = new byte[1];
            int numberBits;

            dataTable = DbInterface.GetDataTable(sql);

            foreach (DataRow rows in dataTable.Rows)
            {
                value = (int)rows["value"];
                bytes = BitConverter.GetBytes(value);
                numberBits = (int)rows["number_of_bits"];

                Array.Resize(ref bytesToGetPart, (int)Math.Ceiling((double)(numberBits / 8)));

                int lastIndex = (bytes.Length - 1);

                for (int i = (bytesToGetPart.Length - 1); i >= 0; i--)
                {
                    bytesToGetPart[i] = bytes[lastIndex];
                    lastIndex--;

                    if (lastIndex < 0)
                    {
                        break;
                    }
                }

                SetPart(startBit, numberBits, bytesToGetPart);
                startBit += numberBits;
            }

            dataTable.Dispose();
        }

        #endregion

        /**
         * Apenas marca o pacote como device command, sem modificar sua estrutura,
         * como ocorre no atributo IsDeviceCommand.
         * 
         * Usado para quando se carrega um rawpacket a partir de requests salvos na base
         **/
        public void SetAsDeviceComand()
        {
            isDeviceCommand = true;
        }

        /**
         * Sobrescreve o metodo SetPart de RawBytes. Apos chamar o metodo da classe
         * base, recalcula o CRC para TCs.
         **/
        public override void SetPart(int startBit, int numberOfBits, byte[] newPart)
        {
            // Chama o metodo da classe-base
            base.SetPart(startBit, numberOfBits, newPart);

            // Agora, se o packet for um request, recalcula seu CRC
            if (autoCrc)
            {
                // Recalcula o crc
                UInt16 crc = CheckingCodes.CrcCcitt16(ref rawContent, rawContent.Length - 2);

                rawContent[rawContent.Length - 2] = (byte)(crc >> 8);
                rawContent[rawContent.Length - 1] = (byte)(crc & 0xFF);
            }
        }

        /**
         * Redimensiona o array de bytes, zerando todas as posicoes a partir
         * do bit 80 (inicio da area de dados, apos o cabecalho de dados).
         * O campo packet_length eh atualizado automaticamente por este metodo.
         **/
        public void Resize(UInt16 newSize)
        {
            int dataStart = 80;

            if (isDeviceCommand == true)
            {
                dataStart -= 32;
            }

            // Atencao: packet length mantem apenas o tamanho do campo de dados, incluindo o crc
            size = newSize;

            // O campo packet_lenght eh o tamanho do pacote - 6 bytes de cabecalho - 1 byte para adequar ao PUS
            newSize -= 7; 

            // Atualiza o campo packet length
            byte[] part = new byte[2];
            part[0] = (byte)(newSize >> 8);
            part[1] = (byte)(newSize & 0xFF);

            SetPart(32, 16, part);
           
            Array.Resize<byte>(ref rawContent, size);

            // Zera toda a mensagem, a partir do inicio da area de dados,
            // para que os valores possam ser re-escritos em suas novas
            // posicoes (isso fica a cargo do chamador)
            part[0] = 0;

            for (int i = dataStart; i < (rawContent.Length * 8); i++)
            {
                SetPart(i, 1, part);
            }
        }

        /**
         * Recebe um byte e o armazena para montar um report packet.
         **/
        public ReceptionStatus ReceiveByte(byte receivedByte)
        {
            receivedCounter++;

            switch (receivedCounter)
            {
                case 1:
                {
                    // Zera o array para comecar um novo pacote
                    for (int i = 0; i < rawContent.Length; i++)
                    {
                        rawContent[i] = 0;
                    }

                    packetLength = 0;
                    totalPacketLength = 0;

                    // Verifica se o primeiro byte eh o valor fixo esperado: 0x08
                    if (receivedByte == 0x08)
                    {
                        rawContent[0] = receivedByte;
                        timer.Interval = 50; // define 50 ms para receber os proximos 5 bytes
                        timer.Enabled = true;
                    }
                    else
                    {
                        // Primeiro byte invalido! Reinicia a recepcao
                        receivedCounter = 0;
                    }

                    break;
                }
                case 2:
                case 3:
                case 4:
                {
                    // Deveria verificar o apid de origem no segundo byte, mas como o objetivo 
                    // aqui tambem eh detectar erros de comunicacao, nao faz esta verificacao
                    rawContent[receivedCounter - 1] = receivedByte;
                    break;
                }
                case 5:
                {
                    // Se o quinto byte for maior que 3, o tamanho informado da TM sera maior
                    // que 1023, o que eh um tamanho invalido.
                    if (receivedByte <= 0x03)
                    {
                        rawContent[4] = receivedByte;
                    }
                    else
                    {
                        timer.Enabled = false;
                        receivedCounter = 0;
                    }
                    break;
                }
                case 6:
                {
                    packetLength = (int)((rawContent[4] << 8) | receivedByte);
                    totalPacketLength = packetLength + 6 + 1; // + 1 para adequar ao PUS

                    if (packetLength >= 11) // tamanho minimo do campo de dados de TM: 12 bytes (-1 do PUS)
                    {
                        timer.Enabled = false;
                        rawContent[5] = receivedByte;

                        // Redimensiona o array para o tamanho a ser recebido
                        Array.Resize<byte>(ref rawContent, totalPacketLength);

                        timer.Interval = packetLength * 10; // da 10ms por byte para receber todo o pacote
                        timer.Enabled = true;
                    }
                    else
                    {
                        timer.Enabled = false;
                        receivedCounter = 0;
                    }

                    break;
                }
                default:
                {
                    rawContent[receivedCounter - 1] = receivedByte;

                    if (receivedCounter == totalPacketLength)
                    {
                        // Pacote completo recebido; cancela o timeout
                        timer.Enabled = false;

                        // Verifica o crc
                        UInt16 crc = CheckingCodes.CrcCcitt16(ref rawContent, rawContent.Length - 2);

                        if ((rawContent[rawContent.Length - 2] == (byte)(crc >> 8)) &&
                            (rawContent[rawContent.Length - 1] == (byte)(crc & 0xFF)))
                        {
                            receivedCounter = 0;
                            return (ReceptionStatus.packet_completed);
                        }
                        else
                        {
                            receivedCounter = 0;
                            return (ReceptionStatus.packet_completed_with_error);
                        }
                    }

                    break;
                }
           }

            return (ReceptionStatus.receiving_packet);
        }

        /** Rotina de timeout da recepcao de bytes. **/
        private void timer_Tick(object sender, EventArgs e)
        {
            // Reinicia a contagem de bytes recebidos
            receivedCounter = 0;
            timer.Enabled = false;
        }

        private void SetupPacket(bool isTc, bool isTcd)
        {
            isRequest = isTc;
            autoCrc = isTc;
            isDeviceCommand = isTcd;
            timer.Enabled = false;

            DbConfiguration.Load();

            /* O tamanho padrao (e minimo) da mensagem eh 12 bytes para TC, 18 bytes para TM
             * -> 6 bytes para o packet header
             * -> 10 bytes para o data header:
             *      ** 1 byte para sec. header flag + pus version + ack
             *      ** 1 byte para service type
             *      ** 1 byte para service subtype
             *      ** 1 byte para ground station (source / destination) id
             *      ** 6 bytes para time-tag (apenas para TM)
             * -> 2 bytes para o crc
             */
            size = 12;

            if (isDeviceCommand == true)
            {
                // se for device command, obrigatoriamente tera ao menos 2 bytes de 
                // data field, mas sem o data field header
                size -= 4;
            }

            if (!isRequest)
            {
                size += 6; // 6 bytes para a time tag
            }

            rawContent = new byte[size];

            // Soh para garantir que inicie zerado
            for (int i = 0; i < rawContent.Length; i++)
            {
                rawContent[i] = 0;
            }

            if (isRequest)
            {
                // Inicializa o campo packet_length
                byte[] part = new byte[2];
                part[0] = 0;

                // Subtraio 1 para ficar de acordo com o PUS, onde packet_length = (packet_data_field_bytes - 1)
                part[1] = (byte)(size - 6 - 1);
                SetPart(32, 16, part);

                if (isDeviceCommand == false)
                {
                    // Escreve o Source ID no request
                    if (!DbConfiguration.RequestsDefaultSourceId.ToString().Trim().Equals(""))
                    {
                        part[0] = (byte)DbConfiguration.RequestsDefaultSourceId;
                    }
                    else
                    {
                        part[0] = 0;
                    }

                    SetPart(72, 8, part);
                }
            }
            else
            {
                // Adiciona um tratador de evento ao timer (para recepcao)
                timer.Tick += new System.EventHandler(timer_Tick);
            }
        }
    }
}

/**
 * @file 	    CommunicationProtocolSimulator.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    02/04/2013
 * @note	    Modificado em 23/04/2013 por Thiago
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Data;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

/**
 * @Namespace Namespace com as rotinas necessarias para execucao de simuladores de protocolos de comunicacao entre o OBC e equipamentos (sensores e atuadores).
 */
namespace Inpe.Subord.Comav.Egse.Smc.Simulations
{
    /**
     * @class CommunicationProtocolSimulator
     * Esta classe eh a responsavel pela execucao de simuladores de equipamentos (sensores e atuadores) para o OBC.
     **/
    class CommunicationProtocolSimulator
    {
        #region Variaveis

        private int simId;
        private String simName;
        private String simConnType;
        private SerialPort serialRS232;
        private Dictionary<int, DbSimulatorMsgToSend> msgsToSend = new Dictionary<int, DbSimulatorMsgToSend>();
        private Dictionary<int, DbSimulatorMsgToReceive> msgsToReceive = new Dictionary<int, DbSimulatorMsgToReceive>();

        // Event disparado a cada mensagem recebida do COMAV
        private AvailableReceivedMsgEventArgs availableReceivedMsgArgs = new AvailableReceivedMsgEventArgs();
        public AvailableReceivedMsgHandler availableReceivedMsgHandler = null;

        private AvailableLastMsgSentEventArgs availableLastMsgSentArgs = new AvailableLastMsgSentEventArgs();
        public AvailableLastMsgSentHandler availableLastMsgSentHandler = null;

        // Event disparado a cada mensagem enviada ao COMAV
        private AvailableAnsweredMsgEventArgs availableAnsweredMsgArgs = new AvailableAnsweredMsgEventArgs();
        public AvailableAnsweredMsgHandler availableAnsweredMsgHandler = null;

        private byte[] bufferMsgReceived = new byte[1];
        private int indexAllocMessage;
        private bool receivingMessage = false;
        
        private Dictionary<int, RecurrentMessageControl> recurrentMessagesToSend = new Dictionary<int, RecurrentMessageControl>();
        private List<int> allIndexMessagesToSend = new List<int>();
        private TimerTaskMessageToSend timerToSendRecurrentMessage = null;
        private TimerTaskMessageToAnswer timerMsgToAnswer = null;
        public static bool serialPortInUse = false;
        
        #endregion

        #region Propriedades

        public int SimId
        {
            get
            {
                return simId;
            }
            set
            {
                simId = value;
            }
        }

        public String SimName
        {
            get
            {
                return simName;
            }
            set
            {
                simName = value;
            }
        }

        public String SimConnType
        {
            get
            {
                return simConnType;
            }
            set
            {
                simConnType = value;
            }
        }

        public SerialPort RS232
        {
            get
            {
                return serialRS232;
            }
            set
            {
                serialRS232 = value;
            }
        }

        #endregion

        #region Metodos Privados

        private bool LoadMsgsToSend()
        {
            try
            {
                // A partir de agora, deve-se evitar colocar sql dentro do Form
                // Procurar colocar as sql's sembre na camada Db
                String sql = @"select msg_id, 
                                  msg_name, 
                                  msg_to_send, 
                                  delay_to_send_again 
                           from sim_msgs_to_send 
                           where sim_id = " + simId;

                DataTable tblMsgsToSend = DbInterface.GetDataTable(sql);

                foreach (DataRow row in tblMsgsToSend.Rows)
                {
                    DbSimulatorMsgToSend msgToSend = new DbSimulatorMsgToSend();
                    msgToSend.MessageId = int.Parse(row[0].ToString());
                    msgToSend.Name = row[1].ToString();
                    msgToSend.MessageToSend = (byte[])row[2];
                    msgToSend.DelayToSendAgain = int.Parse(row[3].ToString());

                    msgsToSend.Add(msgToSend.MessageId, msgToSend);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool LoadMsgsToReceive()
        {
            try
            {
                // A partir de agora, deve-se evitar colocar sql dentro do Form
                // Procurar colocar as sql's sembre na camada Db
                String sql = @"select msg_id, 
                                  msg_name, 
                                  msg_to_receive, 
                                  check_full_msg, 
                                  delay_to_answer, 
                                  msg_to_answer, 
                                  repeat_answer, 
                                  answer_repetition_interval 
                           from sim_msgs_to_receive 
                           where sim_id = " + simId;

                DataTable tblMsgsToReceive = DbInterface.GetDataTable(sql);

                foreach (DataRow row in tblMsgsToReceive.Rows)
                {
                    DbSimulatorMsgToReceive msgToReceive = new DbSimulatorMsgToReceive();
                    msgToReceive.MessageId = int.Parse(row[0].ToString());
                    msgToReceive.Name = row[1].ToString();
                    msgToReceive.MessageToReceive = (byte[])row[2];
                    msgToReceive.CheckFullMessage = (bool)row[3];
                    msgToReceive.DelayToAnswer = int.Parse(row[4].ToString());
                    msgToReceive.MessageToAnswer = (byte[])row[5];
                    msgToReceive.RepeatAnswer = (bool)row[6];
                    msgToReceive.RepetitionInterval = int.Parse(row[7].ToString());

                    msgsToReceive.Add(msgToReceive.MessageId, msgToReceive);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void AvailableLastMsgSent(object sender, AvailableLastMsgSentEventArgs eventArgs)
        {
            if (availableLastMsgSentHandler != null)
            {
                availableLastMsgSentArgs.SimId = eventArgs.SimId;
                availableLastMsgSentArgs.MessageSent = eventArgs.MessageSent;
                availableLastMsgSentArgs.MessageSentTime = eventArgs.MessageSentTime;
                availableLastMsgSentHandler(this, availableLastMsgSentArgs);
            }
        }

        private void AvailableLastMsgAnswered(object sender, AvailableAnsweredMsgEventArgs eventArgs)
        {
            if (availableAnsweredMsgHandler != null)
            {
                availableAnsweredMsgArgs.SimId = eventArgs.SimId;
                availableAnsweredMsgArgs.MessageSent = eventArgs.MessageSent;
                availableAnsweredMsgArgs.TimeAnswered = eventArgs.TimeAnswered;
                availableAnsweredMsgHandler(this, availableAnsweredMsgArgs);
            }
        }

        #endregion

        #region Metodos Publicos

        public bool LoadMessages()
        {
            if (!LoadMsgsToSend())
            {
                return false;
            }

            if (!LoadMsgsToReceive())
            {
                return false;
            }

            return true;
        }

        public static bool SerialPortAlreadyInUse(String portName)
        {
            try
            {
                SerialPort serial = new SerialPort(portName);
                serial.Open();
                serial.Close();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public void BeginExecution()
        {
            try
            {
                timerMsgToAnswer = new TimerTaskMessageToAnswer();
                timerMsgToAnswer.availableAnsweredMsgHandler += new AvailableAnsweredMsgHandler(AvailableLastMsgAnswered);

                timerToSendRecurrentMessage = new TimerTaskMessageToSend();
                timerToSendRecurrentMessage.SerialRS232 = serialRS232;

                timerToSendRecurrentMessage.SerialRS232.DataReceived += new SerialDataReceivedEventHandler(SerialRead);

                if (!timerToSendRecurrentMessage.SerialRS232.IsOpen)
                {
                    timerToSendRecurrentMessage.SerialRS232.Open();
                }

                DateTime dateTimeNow = DateTime.Now;
                recurrentMessagesToSend.Clear();

                foreach (KeyValuePair<int, DbSimulatorMsgToSend> msgToSend in msgsToSend)
                {
                    RecurrentMessageControl recurrentMessage = new RecurrentMessageControl();
                    recurrentMessage.SimId = simId;
                    recurrentMessage.Index = msgToSend.Key;
                    recurrentMessage.RecurrentMessage = msgToSend.Value.MessageToSend;
                    recurrentMessage.TransmissionIntervalInMs = msgToSend.Value.DelayToSendAgain;

                    TimeSpan timeSpan = new TimeSpan(recurrentMessage.TransmissionIntervalInMs * 10000);
                    recurrentMessage.NextSendMoment = dateTimeNow.Add(timeSpan);

                    recurrentMessagesToSend.Add(msgToSend.Key, recurrentMessage);
                }

                
                timerToSendRecurrentMessage.availableLastMsgSentHandler += new AvailableLastMsgSentHandler(AvailableLastMsgSent);
                
                

                // Logo ao iniciar a execucao, enviar todas as mensagens seguindo a ordem que foram editadas, para que em seguida sejam feitas as verificacoes dos delays para o envio da(s) proxima(s) mensagem(s).
                for (int i = 0; i < recurrentMessagesToSend.Count; i++)
                {
                    timerToSendRecurrentMessage.RecurrentMessage = recurrentMessagesToSend[i];
                    timerToSendRecurrentMessage.Interval = 1; // 1 para ser enviada AGORA.
                    timerToSendRecurrentMessage.Enabled = true;

                    Thread.Sleep(5);

                    while (timerToSendRecurrentMessage.Enabled || CommunicationProtocolSimulator.serialPortInUse || (serialRS232.BytesToWrite != 0))
                    {
                    }

                    Thread.Sleep((int)timerToSendRecurrentMessage.Interval);
                }

                if (recurrentMessagesToSend.Count > 0)
                {
                    while (serialRS232.IsOpen)
                    {
                        int indexToNextSend = 0;
                        allIndexMessagesToSend.Clear();
                        allIndexMessagesToSend.Add(indexToNextSend); // aponta para a primeira mensagen

                        // percorre todas as mensagens e identificar qual mensagem devera ser enviada em seguida.
                        // mensagens com intervalo igual tambem sao identificadas.
                        for (int i = 0; i < (recurrentMessagesToSend.Count - 1); i++)
                        {
                            int result = DateTime.Compare(recurrentMessagesToSend[indexToNextSend].NextSendMoment, recurrentMessagesToSend[i + 1].NextSendMoment);
                            // result = 0 : as duas datas sao iguais
                            // result > 0 : a primeira data eh maior
                            // result < 0 : a primeira data eh menor

                            if (result > 0) // a segunda data eh menor, apontar para ela. Limpar todos os index de mensagens
                            {
                                indexToNextSend = i + 1;
                                allIndexMessagesToSend.Clear();
                                allIndexMessagesToSend.Add(recurrentMessagesToSend[i + 1].Index);
                            }
                            else if (result == 0)
                            {
                                indexToNextSend = i + 1;
                                allIndexMessagesToSend.Add(recurrentMessagesToSend[i + 1].Index);
                            }
                        }

                        // agora solicita o envio da(s) mensagem(s) periódicas detectada(s) para o proximo envio.
                        // seta o interval do timer para o proximo envio.
                        dateTimeNow = DateTime.Now;

                        // subtrair o delay com o tempo atual: nextSendMoment - dateTimeNow
                        TimeSpan timeSpan = recurrentMessagesToSend[allIndexMessagesToSend[0]].NextSendMoment.Subtract(dateTimeNow);

                        if (timeSpan.TotalMilliseconds <= 0)
                        {
                            timerToSendRecurrentMessage.Interval = 1;
                        }
                        else
                        {
                            timerToSendRecurrentMessage.Interval = timeSpan.TotalMilliseconds;
                        }

                        // esse foreach sera executado em mais de 1 loop se existir mais de 1 mensagem com o mesmo intervalo de tempo.
                        foreach (int indexToSend in allIndexMessagesToSend)
                        {
                            timerToSendRecurrentMessage.RecurrentMessage = recurrentMessagesToSend[indexToSend];
                            timerToSendRecurrentMessage.Enabled = true;

                            Thread.Sleep(5);

                            while (timerToSendRecurrentMessage.Enabled || CommunicationProtocolSimulator.serialPortInUse || (serialRS232.BytesToWrite != 0))
                            {
                            }

                            Thread.Sleep((int)timerToSendRecurrentMessage.Interval);
                        }

                        dateTimeNow = DateTime.Now;

                        foreach (int indexToSend in allIndexMessagesToSend)
                        {
                            // recalcular o proximo envio
                            TimeSpan timeSpan2 = new TimeSpan(recurrentMessagesToSend[indexToSend].TransmissionIntervalInMs * 10000);
                            recurrentMessagesToSend[recurrentMessagesToSend[indexToSend].Index].NextSendMoment = dateTimeNow.Add(timeSpan2);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                //timerToSendRecurrentMessage.availableLastMsgSentHandler -= new AvailableLastMsgSentHandler(AvailableLastMsgSent);
                //timerToSendRecurrentMessage.SerialRS232.DataReceived -= new SerialDataReceivedEventHandler(SerialRead);
                //timerToSendRecurrentMessage.SerialRS232.Close();
                //CommunicationProtocolSimulator.serialPortInUse = false;
                //timerToSendRecurrentMessage.Enabled = false;
            }
        }

        #endregion

        #region Rotinas de Comunicacao e Processamento das Menssagens Seriais RS-232

        ///** Evento de recepcao de dados pela serial rs-232. **/
        //public void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    /**
        //     * Preciso invocar outro tratador de evento, pois serial_DataReceived eh
        //     * executado em outra thread (criada pelo SerialPort em Open(). Como
        //     * este evento eh tratado por outro handler, nao pode manipular objetos
        //     * criados na thread principal do simulador.
        //     **/
        //    this.Invoke(new EventHandler(SerialRead));
        //}

        // Respeitar esta assinatura [void (object, EventArgs)], necessaria para qualquer EventHandler
        void SerialRead(object s, EventArgs e)
        {
            int remainingBytes = serialRS232.BytesToRead;
            byte[] receivedByte = new byte[remainingBytes];
            serialRS232.Read(receivedByte, 0, remainingBytes);

            //O metodo abaixo foi alterado para true para atender aos simuladores que nao seguem o mesmo padrao do GPS.
            //Com a verificacao abaixo, se nao receber mensagem do GPS, ja peco para verificar se os bytes recebidos sao alguma mensagem.
            if (!ReceivedMessage(receivedByte))
            {
                FindMessageToAnswer(receivedByte);
            }
        }

        private bool ReceivedMessage(byte[] receivedBytesTemp)
        {
            for (int i = 0; i < receivedBytesTemp.Length; i++)
            {
                if ((receivedBytesTemp[i] == 0x02) && (!receivingMessage))
                {
                    receivingMessage = true;
                    indexAllocMessage = 0;
                    bufferMsgReceived[indexAllocMessage] = receivedBytesTemp[i];
                    indexAllocMessage++;
                }
                else if ((receivedBytesTemp[i] == 0x03) && (receivingMessage))
                {
                    receivingMessage = false;
                    Array.Resize(ref bufferMsgReceived, bufferMsgReceived.Length + 1);
                    bufferMsgReceived[indexAllocMessage] = receivedBytesTemp[i];

                    // Disponibiliza a mensagem recebida
                    if (availableReceivedMsgHandler != null)
                    {
                        availableReceivedMsgArgs.ReceivedTime = DbInterface.ExecuteScalar("select getDate()").ToString();
                        availableReceivedMsgArgs.SimId = simId;
                        availableReceivedMsgArgs.ReceivedMessage = bufferMsgReceived;

                        availableReceivedMsgHandler(this, availableReceivedMsgArgs);
                    }

                    FindMessageToAnswer(bufferMsgReceived);

                    bufferMsgReceived = new byte[1];
                    return true;
                }
                else if (receivingMessage)
                {
                    Array.Resize(ref bufferMsgReceived, bufferMsgReceived.Length + 1);
                    bufferMsgReceived[indexAllocMessage] = receivedBytesTemp[i];
                    indexAllocMessage++;
                }
            }

            return false;
        }

        private void FindMessageToAnswer(byte[] recMsg)
        {
            foreach (KeyValuePair<int, DbSimulatorMsgToReceive> msgToReceive in msgsToReceive)
            {
                DbSimulatorMsgToReceive dbMsgToReceive = msgToReceive.Value;

                if (recMsg.Length == dbMsgToReceive.MessageToReceive.Length)
                {
                    bool isEqual = true;

                    for (int i = 0; i < recMsg.Length; i++)
                    {
                        if (recMsg[i] != dbMsgToReceive.MessageToReceive[i])
                        {
                            isEqual = false;
                            break;
                        }
                    }

                    if (isEqual)
                    {
                        if (dbMsgToReceive.CheckFullMessage)
                        {
                            // Checar mensagem ?
                        }
                        
                        timerMsgToAnswer.SimId = simId;
                        timerMsgToAnswer.MessageToAnswer = dbMsgToReceive.MessageToAnswer; // mesagem a ser respondida
                        timerMsgToAnswer.SerialRS232 = serialRS232;

                        if (dbMsgToReceive.DelayToAnswer == 0)
                        {
                            timerMsgToAnswer.Interval = 1;
                        }
                        else
                        {
                            timerMsgToAnswer.Interval = dbMsgToReceive.DelayToAnswer; // delay para responder a primeira vez
                        }

                        // Verificar se a resposta devera ser frequente
                        if (dbMsgToReceive.RepeatAnswer)
                        {
                            timerMsgToAnswer.RepeatAnswer = true;
                            timerMsgToAnswer.IntervalToRepetitionAnswer = dbMsgToReceive.RepetitionInterval;
                        }

                        timerMsgToAnswer.Enabled = true;

                        break;
                    }
                }
            }
        }

        #endregion
    }
    
    public class AvailableReceivedMsgEventArgs : EventArgs
    {
        #region Variaveis

        private byte[] msg;
        private int sId;
        private String receivedTime;

        #endregion

        #region Propriedades

        public byte[] ReceivedMessage
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
            }
        }

        public int SimId
        {
            get
            {
                return sId;
            }
            set
            {
                sId = value;
            }
        }

        public String ReceivedTime
        {
            get
            {
                return receivedTime;
            }
            set
            {
                receivedTime = value;
            }
        }

        #endregion
    }

    public delegate void AvailableReceivedMsgHandler(object sender, AvailableReceivedMsgEventArgs e);
}

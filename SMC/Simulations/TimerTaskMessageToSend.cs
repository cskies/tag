/**
 * @file 	    TimerTaskMessageToSend.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    02/04/2013
 * @note	    Modificado em 28/05/2013 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Timers;
using System.IO.Ports;

/**
 * @Namespace Namespace com as rotinas necessarias para execucao de simuladores de protocolos de comunicacao entre o OBC e equipamentos (sensores e atuadores).
 */
namespace Inpe.Subord.Comav.Egse.Smc.Simulations
{
    /**
     * @class TimerTaskMessageToSend
     * Esta classe gerencia o timer para disparo do envio de mensagens pre-definidas para envio periodico.
     **/
    public class TimerTaskMessageToSend : Timer
    {
        #region Atributos

        private SerialPort serialRS232;
        private RecurrentMessageControl recurrentMessage = null;
        private AvailableLastMsgSentEventArgs availableLastMsgSentArgs = new AvailableLastMsgSentEventArgs();
        public AvailableLastMsgSentHandler availableLastMsgSentHandler = null;

        #endregion

        #region Propriedades

        public RecurrentMessageControl RecurrentMessage
        {
            get
            {
                return recurrentMessage;
            }
            set
            {
                recurrentMessage = value;
            }
        }

        public SerialPort SerialRS232
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

        #region Construtor

        public TimerTaskMessageToSend()
        {
            Elapsed += new ElapsedEventHandler(timer_Tick_SendMsgsPeriodically);
        }

        #endregion

        #region Metodos executado quando o timer eh disparado

        private void timer_Tick_SendMsgsPeriodically(object sender, EventArgs e)
        {
            try
            {
                // Este loop eh executado para esperar ate que a porta serial correspondente seja liberada
                // Nao retira-lo.
                while (CommunicationProtocolSimulator.serialPortInUse)
                {
                }

                CommunicationProtocolSimulator.serialPortInUse = true;

                double nextInterval = (((double)(recurrentMessage.RecurrentMessage.Length * 1024)) / ((double)serialRS232.BaudRate));
                Interval = nextInterval + recurrentMessage.RecurrentMessage.Length;

                // Esta regra foi inserida porque mensagens com um numero de bytes pequeno tornam o 'Interval' tbm pequeno.
                // 200 milisegundos eh o delay minimo encontrado para enviar mensagens pequenas, como de 2 bytes.
                if (Interval <= 100.0)
                {
                    Interval = 200; // este eh o tempo limite. Com 200 milisegundos no minimo para enviar uma mensagem.
                }

                if (serialRS232.IsOpen)
                {
                    serialRS232.Write(recurrentMessage.RecurrentMessage, 0, recurrentMessage.RecurrentMessage.Length);
                    Console.WriteLine("Message Sent Periodically: " + Utils.Formatting.ConvertByteArrayToHexString(recurrentMessage.RecurrentMessage, recurrentMessage.RecurrentMessage.Length));
                    DateTime timeNow = (DateTime)DbInterface.ExecuteScalar("select getDate()");

                    if (availableLastMsgSentHandler != null)
                    {
                        availableLastMsgSentArgs.SimId = recurrentMessage.SimId;
                        availableLastMsgSentArgs.MessageSent = recurrentMessage.RecurrentMessage;
                        availableLastMsgSentArgs.MessageSentTime = timeNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                        availableLastMsgSentHandler(this, availableLastMsgSentArgs);
                    }
                }
                
                CommunicationProtocolSimulator.serialPortInUse = false;
                
                Enabled = false;
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }

    #region Classe e delegate para disponibilizacao da ultima mensagem enviada periodicamente

    public class AvailableLastMsgSentEventArgs : EventArgs
    {
        #region Atributos

        private int simId;
        private String msgSentTime;
        private byte[] msgSent;

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

        public String MessageSentTime
        {
            get
            {
                return msgSentTime;
            }
            set
            {
                msgSentTime = value;
            }
        }

        public byte[] MessageSent
        {
            get
            {
                return msgSent;
            }
            set
            {
                msgSent = value;
            }
        }

        #endregion
    }

    public delegate void AvailableLastMsgSentHandler(object sender, AvailableLastMsgSentEventArgs e);

    #endregion
}

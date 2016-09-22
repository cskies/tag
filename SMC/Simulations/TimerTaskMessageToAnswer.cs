/**
 * @file 	    TimerTaskMessageToAnswer.cs
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
using System.Timers;
using System.IO.Ports;

/**
 * @Namespace Namespace com as rotinas necessarias para execucao de simuladores de protocolos de comunicacao entre o OBC e equipamentos (sensores e atuadores).
 */
namespace Inpe.Subord.Comav.Egse.Smc.Simulations
{
    /**
     * @class TimerTaskMessageToAnswer
     * Esta classe gerencia o timer para disparo do envio de mensagens pre-definidas para resposta a mensagens recebidas do OBC.
     **/
    public class TimerTaskMessageToAnswer : Timer
    {
        #region Atributos

        private int simId;
        private byte[] msgToAnswer;
        private bool repeatAnswer;
        private int intervalToRepetitionAnswer;
        private SerialPort serialRS232;
        private AvailableAnsweredMsgEventArgs availableAnsweredMsgArgs = new AvailableAnsweredMsgEventArgs();
        public AvailableAnsweredMsgHandler availableAnsweredMsgHandler = null;

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

        public byte[] MessageToAnswer
        {
            get
            {
                return msgToAnswer;
            }
            set
            {
                msgToAnswer = value;
            }
        }

        public bool RepeatAnswer
        {
            get
            {
                return repeatAnswer;
            }
            set
            {
                repeatAnswer = value;
            }
        }

        public int IntervalToRepetitionAnswer
        {
            get
            {
                return intervalToRepetitionAnswer;
            }
            set
            {
                intervalToRepetitionAnswer = value;
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

        public TimerTaskMessageToAnswer()
        {
            Elapsed += new ElapsedEventHandler(timer_Tick_MessageToAnswer);
        }

        #endregion

        #region Metodo executado sempre que o timer eh disparado

        private void timer_Tick_MessageToAnswer(object sender, EventArgs e)
        {
            TimerTaskMessageToAnswer taskMsgToAnswer = (TimerTaskMessageToAnswer)sender;
            taskMsgToAnswer.Enabled = false;

            // Este loop eh executado para esperar ate que a porta serial correspondente seja liberada
            // Nao retira-lo.
            while (CommunicationProtocolSimulator.serialPortInUse)
            {
            }

            CommunicationProtocolSimulator.serialPortInUse = true;

            if (serialRS232.IsOpen)
            {
                serialRS232.Write(taskMsgToAnswer.MessageToAnswer, 0, taskMsgToAnswer.MessageToAnswer.Length);
                DateTime timeNow = (DateTime)DbInterface.ExecuteScalar("select getDate()");

                if (availableAnsweredMsgHandler != null)
                {
                    availableAnsweredMsgArgs.MessageSent = taskMsgToAnswer.MessageToAnswer;
                    availableAnsweredMsgArgs.SimId = taskMsgToAnswer.SimId;
                    availableAnsweredMsgArgs.TimeAnswered = timeNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                    availableAnsweredMsgHandler(this, availableAnsweredMsgArgs);
                }
            }

            CommunicationProtocolSimulator.serialPortInUse = false;

            if (taskMsgToAnswer.RepeatAnswer)
            {
                // Realimentar o mesmo timer para que a partir de agora passe a reenviar a resposta repetidamente com intervalo constante.
                taskMsgToAnswer.Interval = taskMsgToAnswer.IntervalToRepetitionAnswer;
                taskMsgToAnswer.Enabled = true;
            }
            else
            {
                taskMsgToAnswer.Stop();
            }
        }

        #endregion
    }

    #region Classe e delegate para a disponibilizacao da mensagem enviada em resposta aa recebida

    public class AvailableAnsweredMsgEventArgs : EventArgs
    {
        #region Variaveis

        private byte[] msgSent;
        private int sId;
        private String timeAnswered;

        #endregion

        #region Propriedades

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

        public String TimeAnswered
        {
            get
            {
                return timeAnswered;
            }
            set
            {
                timeAnswered = value;
            }
        }

        #endregion
    }

    public delegate void AvailableAnsweredMsgHandler(object sender, AvailableAnsweredMsgEventArgs e);

    #endregion
}

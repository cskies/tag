/**
 * @file 	    DbSimulatorMsgToReceive.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    12/07/2012
 * @note	    Modificado em 13/07/2012 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbSimulatorMsgToReceive
     * Classe criada para o fluxo dos atributos de cada mensagem dos simuladores.
     **/
    class DbSimulatorMsgToReceive
    {
        #region Atributos Internos

        private int msgId;
        private String msgName;
        private byte[] msgToReceive;
        private bool checkFullMessage;
        private int delayToAnswer;
        private byte[] msgToAnswer;
        private bool repeatAnswer;
        private int repetitionInterval;

        #endregion

        #region Propriedades

        public int MessageId
        {
            get
            {
                return msgId;
            }
            set
            {
                msgId = value;
            }
        }

        public String Name
        {
            get
            {
                return msgName;
            }
            set
            {
                msgName = value;
            }
        }

        public byte[] MessageToReceive
        {
            get
            {
                return msgToReceive;
            }
            set
            {
                msgToReceive = value;
            }
        }

        public bool CheckFullMessage
        {
            get
            {
                return checkFullMessage;
            }
            set
            {
                checkFullMessage = value;
            }
        }

        public int DelayToAnswer
        {
            get
            {
                return delayToAnswer;
            }
            set
            {
                delayToAnswer = value;
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

        public int RepetitionInterval
        {
            get
            {
                return repetitionInterval;
            }
            set
            {
                repetitionInterval = value;
            }
        }

        #endregion
    }
}

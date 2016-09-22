/**
 * @file 	    DbSimulatorMsgToSend.cs
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
     * @class DbSimulatorMsgToSend
     * Classe criada para o fluxo dos atributos de cada mensagem dos simuladores.
     **/
    class DbSimulatorMsgToSend
    {
        #region Atributos Internos

        private int msgId;
        private String msgName;
        private byte[] msgToSend;
        private int delayToSendAgain;

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

        public byte[] MessageToSend
        {
            get
            {
                return msgToSend;
            }
            set
            {
                msgToSend = value;
            }
        }

        public int DelayToSendAgain
        {
            get
            {
                return delayToSendAgain;
            }
            set
            {
                delayToSendAgain = value;
            }
        }

        #endregion
    }
}

/**
 * @file 	    RecurrentMessageControl.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    02/04/2013
 * @note	    Modificado em 02/04/2013 por Thiago
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @Namespace Namespace com as rotinas necessarias para execucao de simuladores de protocolos de comunicacao entre o OBC e equipamentos (sensores e atuadores).
 */
namespace Inpe.Subord.Comav.Egse.Smc.Simulations
{
    /**
     * @class RecurrentMessageControl
     * Esta classe eh usada durante o controle de envio entre o conjunto de mensagens configuradas para envios periodicos entre um intervalo de tempo.
     **/
    public class RecurrentMessageControl
    {
        #region Atributos

        private int simId;
        private int index;
        private byte[] recurrentMessage; // mesnagem a ser transmitida
        private int transmissionIntervalInMs;
        private DateTime nextSendMoment;

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

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        public byte[] RecurrentMessage
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

        public int TransmissionIntervalInMs
        {
            get
            {
                return transmissionIntervalInMs;
            }
            set
            {
                transmissionIntervalInMs = value;
            }
        }

        public DateTime NextSendMoment
        {
            get
            {
                return nextSendMoment;
            }
            set
            {
                nextSendMoment = value;
            }
        }

        #endregion
    }
}

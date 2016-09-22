/**
 * @file 	    ProcedureExecutionData.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    26/11/2012
 * @note	    Modificado em 06/12/2012 por Thiago
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @Namespace Namespace com todas as rotinas necessarias para execucao automarica dos procedimentos de teste.
 */
namespace Inpe.Subord.Comav.Egse.Smc.TestProcedure
{
    /**
     * @class ProcedureExecutionData
     * Esta classe mantem os dados gerados durante a execucao do procedimento.
     **/
    class ProcedureExecutionData
    {
        private int executionId;
        private DateTime startTime;
        private DateTime endTime;
        private int executionRealTime;
        private int executedLoopIterations;
        private String status;

        #region Propriedades

        public int ExecutionId
        {
            get
            {
                return executionId;
            }
            set
            {
                executionId = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        public int ExecutionRealTime
        {
            get
            {
                return executionRealTime;
            }
            set
            {
                executionRealTime = value;
            }
        }

        public int ExecutedLoopIterations
        {
            get
            {
                return executedLoopIterations;
            }
            set
            {
                executedLoopIterations = value;
            }
        }

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        #endregion
    }
}

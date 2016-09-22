/**
 * @file 	    StepExecutionData.cs
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
     * @class StepExecutionData
     * Esta classe mantem os dados gerados durante a execucao de cada step do procedimento.
     **/
    class StepExecutionData
    {
        private int executionId;
        private int procedureId;
        private int execStepId;
        private int savedRequestId;
        private int iteration;
        private DateTime dispatchMoment;
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

        public int ProcedureId
        {
            get
            {
                return procedureId;
            }
            set
            {
                procedureId = value;
            }
        }

        public int ExecStepId // position
        {
            get
            {
                return execStepId;
            }
            set
            {
                execStepId = value;
            }
        }

        public int SavedRequestId
        {
            get
            {
                return savedRequestId;
            }
            set
            {
                savedRequestId = value;
            }
        }

        public int Iteration
        {
            get
            {
                return iteration;
            }
            set
            {
                iteration = value;
            }
        }

        public DateTime DispatchMoment
        {
            get
            {
                return dispatchMoment;
            }
            set
            {
                dispatchMoment = value;
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

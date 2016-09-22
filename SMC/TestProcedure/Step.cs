/**
 * @file 	    Step.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    26/11/2012
 * @note	    Modificado em 11/01/2013 por Thiago
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;

/**
 * @Namespace Namespace com todas as rotinas necessarias para execucao automarica dos procedimentos de teste.
 */
namespace Inpe.Subord.Comav.Egse.Smc.TestProcedure
{
    /**
     * @class Step
     * Esta classe mantem os dados dos steps do procedimento.
     **/
    class Step
    {
        private int position;
        private String description;
        private int savedRequestId;
        private RawPacket request;
        private int timeDelay;
        private bool verifyExecution;
        private String verifyCondition;
        private int reportType;
        private int reportSubtype;
        private bool checkDataField;
        private int dataFieldId;
        private int dataFieldPosition;
        private int dataFieldNumOfBits; // para obte-lo da TM
        private int dataFieldNumOfBitsBeforeIt; // saber quantos bits existem atras deste "somente dentro da area de dados"
        private String dataFieldType;
        private String comparisonOperation;
        private uint valueToCompare;
        private int verifyIntervalStart;
        private int verifyIntervalEnd;
        private StepExecutionData stepExecData = new StepExecutionData();

        #region Propriedades

        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
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

        public int DelayBeforeSending
        {
            get
            {
                return timeDelay;
            }
            set
            {
                timeDelay = value;
            }
        }

        public bool VerifyExecution
        {
            get
            {
                return verifyExecution;
            }
            set
            {
                verifyExecution = value;
            }
        }

        public String VerifyCondition
        {
            get
            {
                return verifyCondition;
            }
            set
            {
                verifyCondition = value;
            }
        }

        public int ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
            }
        }

        public int ReportSubtype
        {
            get
            {
                return reportSubtype;
            }
            set
            {
                reportSubtype = value;
            }
        }

        public RawPacket Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
            }
        }

        public bool CheckDataField
        {
            get
            {
                return checkDataField;
            }
            set
            {
                checkDataField = value;
            }
        }

        public int DataFieldId
        {
            get
            {
                return dataFieldId;
            }
            set
            {
                dataFieldId = value;
            }
        }

        public int DataFieldPosition
        {
            get
            {
                return dataFieldPosition;
            }
            set
            {
                dataFieldPosition = value;
            }
        }

        public int DataFieldNumOfBits
        {
            get
            {
                return dataFieldNumOfBits;
            }
            set
            {
                dataFieldNumOfBits = value;
            }
        }

        public int DataFieldNumOfBitsBeforeIt
        {
            get
            {
                return dataFieldNumOfBitsBeforeIt;
            }
            set
            {
                dataFieldNumOfBitsBeforeIt = value;
            }
        }

        public String DataFieldType
        {
            get
            {
                return dataFieldType;
            }
            set
            {
                dataFieldType = value;
            }
        }

        public String ComparisonOperation
        {
            get
            {
                return comparisonOperation;
            }
            set
            {
                comparisonOperation = value;
            }
        }

        public uint ValueToCompare
        {
            get
            {
                return valueToCompare;
            }
            set
            {
                valueToCompare = value;
            }
        }

        public int VerifyIntervalStart
        {
            get
            {
                return verifyIntervalStart;
            }
            set
            {
                verifyIntervalStart = value;
            }
        }

        public int VerifyIntervalEnd
        {
            get
            {
                return verifyIntervalEnd;
            }
            set
            {
                verifyIntervalEnd = value;
            }
        }

        public StepExecutionData ExecutionData
        {
            get
            {
                return stepExecData;
            }
            set
            {
                stepExecData = value;
            }
        }

        #endregion
    }
}

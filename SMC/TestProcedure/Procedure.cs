/**
 * @file 	    Procedure.cs
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
using System.Data;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;

/**
 * @Namespace Namespace com todas as rotinas necessarias para execucao automarica dos procedimentos de teste.
 */
namespace Inpe.Subord.Comav.Egse.Smc.TestProcedure
{
    /**
     * @class Procedure
     * Esta classe mantem toda a estrutura de dados do procedimento em memoria para que seja executado pelo Executor.
     **/
    class Procedure
    {
        private int procedureId;
        private String description;
        private String purpose;
        private int estimatedDuration; // seconds
        private bool synchronizeObt;
        private bool getCpuUsage;
        private bool runInLoop;
        private int loopIterations;
        private bool sendEmail;
        private String sequenceControlOptions;
        private bool executed;
        private ProcedureExecutionData procExecutionData = new ProcedureExecutionData();
        private Dictionary<int, Step> steps = new Dictionary<int, Step>();

        #region Propriedades

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

        public String Purpose
        {
            get
            {
                return purpose;
            }
            set
            {
                purpose = value;
            }
        }

        public int EstimatedDuration
        {
            get
            {
                return estimatedDuration;
            }
            set
            {
                estimatedDuration = value;
            }
        }

        public bool SynchronizeObt
        {
            get
            {
                return synchronizeObt;
            }
            set
            {
                synchronizeObt = value;
            }
        }

        public bool GetCpuUsage
        {
            get
            {
                return getCpuUsage;
            }
            set
            {
                getCpuUsage = value;
            }
        }

        public bool RunInLoop
        {
            get
            {
                return runInLoop;
            }
            set
            {
                runInLoop = value;
            }
        }

        public int LoopIterations
        {
            get
            {
                return loopIterations;
            }
            set
            {
                loopIterations = value;
            }
        }

        public String SequenceControlOptions
        {
            get
            {
                return sequenceControlOptions;
            }
            set
            {
                sequenceControlOptions = value;
            }
        }

        public bool Executed
        {
            get
            {
                return executed;
            }
            set
            {
                executed = value;
            }
        }

        public Dictionary<int, Step> Steps
        {
            get
            {
                return steps;
            }
            set
            {
                steps = value;
            }
        }

        public ProcedureExecutionData ExecutionData
        {
            get
            {
                return procExecutionData;
            }
            set
            {
                procExecutionData = value;
            }
        }

        #endregion

        #region Metodos de carregamento do procedimento na memoria

        public void LoadProcedure(int procId)
        {
            LoadHeaderAndAllSteps(procId);
        }

        private void LoadHeaderAndAllSteps(int procId)
        {
            // Buscar os dados do cabecalho do procedimento e alimentar os campos da interface.
            String sql = @"select procedure_id as procedureId,
                                    description as description,
		                            purpose as purpose,
		                            estimated_duration as estimatedDuration,
		                            synchronize_obt as synchronizeObt,
		                            get_cpu_usage as getCpuUsage,
		                            run_in_loop as runInLoop,
		                            loop_iterations as loopIterations,
		                            send_mail as sendEmail,
		                            packets_sequence_control_options as sequenceControlOptions
                            from test_procedures 
                            where procedure_id = " + procId;

            DataTable tblProcedureHeader = DbInterface.GetDataTable(sql);

            foreach (DataRow row in tblProcedureHeader.Rows)
            {
                procedureId = int.Parse(row["procedureId"].ToString());
                description = row["description"].ToString();
                purpose = row["purpose"].ToString();
                estimatedDuration = int.Parse(row["estimatedDuration"].ToString());
                synchronizeObt = bool.Parse(row["synchronizeObt"].ToString());
                getCpuUsage = bool.Parse(row["getCpuUsage"].ToString());
                runInLoop = bool.Parse(row["runInLoop"].ToString());
                loopIterations = int.Parse(row["loopIterations"].ToString());
                sendEmail = bool.Parse(row["sendEmail"].ToString());
                sequenceControlOptions = row["sequenceControlOptions"].ToString();
            }

            LoadAllSteps();
        }

        private void LoadAllSteps()
        {
            // Buscar os steps do procedimento.
            // No inner join da consulta, as letras representam as seguintes tabelas no banco de dados:
            // a: test_procedure
            // b: test_procedure_steps
            // c: saved_requests
            String sql = @"select b.procedure_id as procedureId,
                                  c.description as description,
                                  b.position as position,
                                  b.saved_request_id as savedRequestId,
                                  c.raw_packet as rawPacket,
                                  b.time_delay as delayBeforeSending,
                                  b.verify_execution as verifyExecution,
                                  b.verify_condition as verifyCondition,
                                  case b.report_type when NULL then 0 else b.report_type end as reportType,
                                  case b.report_subtype when NULL then 0 else b.report_subtype end as reportSubtype,
                                  case b.data_field_id when NULL then 0 else b.data_field_id end as dataField,
                                  b.comparison_operation as comparisonOperation,
                                  b.value_to_compare as valueToCompare,
                                  b.verify_interval_start as verifyIntervalStart,
                                  b.verify_interval_end as verifyIntervalEnd,
                                  b.saved_request_id as savedRequestId
                           from (test_procedures a inner join test_procedure_steps b on a.procedure_id = b.procedure_id) inner join saved_requests c on b.saved_request_id = c.saved_request_id
                           where a.procedure_id = b.procedure_id and b.saved_request_id <> 0 and a.procedure_id = " + procedureId + " order by position";

            DataTable tblProcedureSteps = DbInterface.GetDataTable(sql);

            foreach (DataRow row in tblProcedureSteps.Rows)
            {
                Step step = new Step();
                step.Position = int.Parse(row["position"].ToString());
                step.Description = row["description"].ToString();
                step.SavedRequestId = int.Parse(row["savedRequestId"].ToString());
                RawPacket request = new RawPacket(true, false);
                request.RawContents = (byte[])row["rawPacket"];
                step.Request = request;
                step.DelayBeforeSending = int.Parse(row["delayBeforeSending"].ToString());
                step.VerifyExecution = bool.Parse(row["verifyExecution"].ToString());
                step.VerifyCondition = row["verifyCondition"].ToString();

                step.ReportType = 0;
                step.ReportSubtype = 0;
                step.DataFieldId = 0;
                step.CheckDataField = false;

                if (!String.IsNullOrEmpty(row["reportType"].ToString()))
                {
                    step.ReportType = int.Parse(row["reportType"].ToString());
                }

                if (!String.IsNullOrEmpty(row["reportSubtype"].ToString()))
                {
                    step.ReportSubtype = int.Parse(row["reportSubtype"].ToString());
                }

                if (!String.IsNullOrEmpty(row["dataField"].ToString()))
                {
                    step.DataFieldId = int.Parse(row["dataField"].ToString());
                    step.CheckDataField = true;

                    // Para obter o dado do Data Field esperado, carregar a position, number_of_bits,
                    // e number_of_bits_before_it dessa forma que facilite a busca de seu respectivo
                    // valor no array de bytes da TM. Os Data Fields de cada [Type.Subtype] 
                    // mantem uma ordem, e por isso essa ordem devera ser considerada na busca do 
                    // respectivo dado na area de dados da TM.

                    // position

                    // verificar se o subtype herda a mesma estrutura de outro subtype
                    sql = "select isnull(same_as_subtype, 0) as same from subtype_structure where service_type = " + step.ReportType + " and service_subtype = " + step.ReportSubtype + " group by same_as_subtype";
                    int sameAs = (int)DbInterface.ExecuteScalar(sql);

                    sql = @"select c.position 
                            from services a 
                                  inner join subtypes b on a.service_type = b.service_type
                                  inner join subtype_structure c on b.service_Type = c.service_type and b.service_subtype = c.service_subtype
                                  inner join data_fields d on c.data_field_id = d.data_field_id
                            where d.data_field_id = " + step.DataFieldId + @" and 
                                  c.service_type = " + step.ReportType + @" and ";

                    if (sameAs == 0)
                    {
                        sql += "c.service_subtype = " + step.ReportSubtype;
                    }
                    else
                    {
                        sql += "c.service_subtype = " + sameAs;
                    }

                    step.DataFieldPosition = (int)DbInterface.ExecuteScalar(sql);

                    // number of bits
                    sql = "select number_of_bits from data_fields where data_field_id = " + step.DataFieldId;
                    step.DataFieldNumOfBits = (int)DbInterface.ExecuteScalar(sql);

                    // number of bits before it
                    sql = @"select SUM(d.number_of_bits) 
                            from services a 
                                inner join subtypes b on a.service_type = b.service_type 
                                inner join subtype_structure c on b.service_Type = c.service_type and b.service_subtype = c.service_subtype 
                                inner join data_fields d on c.data_field_id = d.data_field_id 
                            where d.data_field_id > 0 and 
                                  c.position <= " + step.DataFieldPosition + @" and 
                                  c.service_type = " + step.ReportType + " and ";

                    if (sameAs == 0)
                    {
                        sql += "c.service_subtype = " + step.ReportSubtype;
                    }
                    else
                    {
                        sql += "c.service_subtype = " + sameAs;
                    }

                    // Subtrai o numero de bits do respectivo data field porquena consulta sql foi usado o sinal "<=" ao inves de "<" 
                    // pelo fato da inexistencia de data fields alocados antes desse. O sinal "<" provoca erro na consulta.
                    step.DataFieldNumOfBitsBeforeIt = ((int)DbInterface.ExecuteScalar(sql) - step.DataFieldNumOfBits);
                    step.DataFieldNumOfBitsBeforeIt++; // bit Spare

                    // type
                    sql = @"select case d.type_is_bool when 1 
			                            then 'type_is_bool' 
			                            else case d.type_is_list when 1 
					                              then 'type_is_list' 
					                              else case d.type_is_numeric when 1 
								                            then 'type_is_numeric'
								                            else case d.type_is_raw_hex when 1 
										                              then 'type_is_raw_hex'
										                              else 'type_is_table' 
									                             end
					                                    end
				                             end
		                            end
                            from services a 
                                  inner join subtypes b on a.service_type = b.service_type
                                  inner join subtype_structure c on b.service_Type = c.service_type and b.service_subtype = c.service_subtype
                                  inner join data_fields d on c.data_field_id = d.data_field_id
                            where d.data_field_id = " + step.DataFieldId + @" and 
                                  c.service_type = " + step.ReportType + " and ";

                    if (sameAs == 0)
                    {
                        sql += "c.service_subtype = " + step.ReportSubtype;
                    }
                    else
                    {
                        sql += "c.service_subtype = " + sameAs;
                    }

                    step.DataFieldType = DbInterface.GetDataTable(sql).Rows[0][0].ToString();
                }

                step.ComparisonOperation = row["comparisonOperation"].ToString();
                step.ValueToCompare = uint.Parse(row["valueToCompare"].ToString());
                step.VerifyIntervalStart = int.Parse(row["verifyIntervalStart"].ToString());
                step.VerifyIntervalEnd = int.Parse(row["verifyIntervalEnd"].ToString());

                // Aloca a estrutura de dados para registro dos dados de sua execucao
                step.ExecutionData = new StepExecutionData();

                steps.Add(step.Position, step);
            }
        }

        #endregion
    }
}

/**
 * @file 	    Executor.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    26/11/2012
 * @note	    Modificado em 24/06/2013 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Forms;

/**
 * @Namespace Namespace com todas as rotinas necessarias para execucao automarica dos procedimentos de teste.
 */
namespace Inpe.Subord.Comav.Egse.Smc.TestProcedure
{
    /**
     * @class Executor
     * Esta classe recebe todos os dados necessarios para a execucao do procedimento e executa-o.
     **/
    class Executor
    {
        private Procedure procedure;
        private FrmConnectionWithEgse communication;
        private int STEPS_COUNT;
        private int STEP_POSITION;
        private int DELAY;
        private bool CONDITION_VERIFIED;
        private String currentVerifyCondition = String.Empty;
        private SequenceControl ssc = new SequenceControl();
        private System.Timers.Timer timer = new System.Timers.Timer();
        private int currentIteration;
        private bool stopExecution;
        private bool stopped;

        // Eventos que sao gerados durante a execucao.
        public event AvailableStepCountDownStatusEventHandler availableStepCountDownStatus = null;
        private AvailableStepCountDownStatusEventArgs availableStepCountDownStatusEventArgs = new AvailableStepCountDownStatusEventArgs();
        public event AvailableExecutionRealTimeEventHandler availableExecutionRealTime = null;
        private AvailableExecutionRealTimeEventArgs availableExecutionRealTimeEventArgs = new AvailableExecutionRealTimeEventArgs();
        public event AvailableNumIteractionInExecution availableNumIteractionInExecution = null;
        private AvailableNumIteractionInExecutionEventArgs availableNumIteractionInExecEventArgs = new AvailableNumIteractionInExecutionEventArgs();
        public event FinishExecution finishExecution = null;
        private FinishExecutionEventArgs stop = new FinishExecutionEventArgs();
        private System.Timers.Timer timerExecutionRealTime = new System.Timers.Timer();
        
        public Executor()
        {
        }

        #region Propriedades

        public Procedure Procedure
        {
            get
            {
                return procedure;
            }
            set
            {
                procedure = value;
            }
        }

        public FrmConnectionWithEgse Communication
        {
            get
            {
                return communication;
            }
            set
            {
                communication = value;
            }
        }

        public bool StoppedExecution
        {
            get
            {
                return stopExecution;
            }
            set
            {
                stopExecution = value;
            }
        }

        #endregion

        #region Metodos Privados

        private void SetSequenceControl(ref RawPacket requestPacket)
        {
            try
            {
                int apid = (int)requestPacket.GetPart(5, 11);
                int currentSsc = ssc.GetLastSent(apid) + 1;

                byte[] part = new byte[2];

                part[0] = (byte)(currentSsc >> 8);
                part[1] = (byte)(currentSsc & 0xFF);

                requestPacket.SetPart(18, 14, part);
            }
            catch (Exception)
            {
            }
        }

        private RawPacket RequestToSyncObt()
        {
            RawPacket request = new RawPacket(9, 128);

            try
            {
                // obtem o relogio do servidor de dados
                String time = DbInterface.ExecuteScalar("select convert(varchar, getdate(), 103) + ' ' + convert(varchar, getdate(), 108) + '.000000'").ToString();

                // agora converte a string de calendario em uma string no formato de bordo
                String convertedTime = TimeCode.CalendarToOnboardTime(time);

                // converte a string em um array de bytes
                byte[] onboardTime = Utils.Formatting.HexStringToByteArray(convertedTime);

                // soma 2 segundos para compensar atrasos (chamadas a metodos no SMC, 
                // consulta ao banco de dados, transmissao ao OBC, processamento no OBC)

                /** @todo A soma de 2 segundos ao tempo no pacote de sincronizacao eh um chute;
                  * ajustar esse valor quando necessario (por enquanto, isso nao eh preciso). **/
                onboardTime[3] += 2;

                request.SetPart(80, 48, onboardTime); // o CRC eh calculado automaticamente
            }
            catch (Exception)
            {
                request = null;
            }

            return request;
        }

        private void timer_ExecutionRealTime(object sender, EventArgs e)
        {
            procedure.ExecutionData.ExecutionRealTime++;

            availableExecutionRealTimeEventArgs.ExecutionTimerTick = procedure.ExecutionData.ExecutionRealTime.ToString();
            availableExecutionRealTime(this, availableExecutionRealTimeEventArgs);
        }

        #endregion

        #region Metodos Publicos

        public DateTime GetDateTimeNow()
        {
            return ((DateTime)DbInterface.ExecuteScalar("select GETDATE() as dateTime"));
        }

        #endregion

        #region Rotinas de Execucao

        public void Start()
        {
            stopped = false;

            // Disponibilizar a posicao da iteracao
            currentIteration = 1;
            availableNumIteractionInExecEventArgs.Iteraction = currentIteration;
            availableNumIteractionInExecution(this, availableNumIteractionInExecEventArgs);

            // Timer para cronometrar o tempo total da execucao do procedimento, incluindo seu cabecalho
            timerExecutionRealTime = new System.Timers.Timer();
            timerExecutionRealTime.Elapsed += new System.Timers.ElapsedEventHandler(timer_ExecutionRealTime);
            timerExecutionRealTime.Interval = 1000;
            procedure.ExecutionData.ExecutionRealTime = 0;
            timerExecutionRealTime.Enabled = true;

            // Registra tempo de inicio da execucao
            procedure.ExecutionData.StartTime = GetDateTimeNow();

            if (procedure.SynchronizeObt)
            {
                RawPacket request = RequestToSyncObt();

                if (procedure.SequenceControlOptions.Contains("Control the sequence"))
                {
                    SetSequenceControl(ref request);
                }

                communication.SendRequest(request, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
            }
            
            STEPS_COUNT = procedure.Steps.Count;
            timer = new System.Timers.Timer();

            if (STEPS_COUNT > 0)
            {
                STEP_POSITION = 0;
                InitiateStep();
            }
        }

        public void Stop(bool byInterruption)
        {
            DELAY = 0; // condicao de parada
            timer.Enabled = false;
            timerExecutionRealTime.Enabled = false;
            stopped = true;

            if (byInterruption)
            {
                availableStepCountDownStatusEventArgs.Status = "interrupted by the user!";
                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
            }

            availableExecutionRealTimeEventArgs.ExecutionTimerTick = procedure.ExecutionData.ExecutionRealTime.ToString();
            availableExecutionRealTime(this, availableExecutionRealTimeEventArgs);

            availableStepCountDownStatus = null;
            availableExecutionRealTime = null;
            availableNumIteractionInExecution = null;
            timerExecutionRealTime.Elapsed -= new System.Timers.ElapsedEventHandler(timer_ExecutionRealTime);
            timerExecutionRealTime.Dispose();
            timer.Dispose();
            
            stop.Finished = true;
            finishExecution(this, stop);
        }

        private void InitiateStep()
        {
            if (stopped)
            {
                return;
            }

            if (STEP_POSITION >= procedure.Steps.Count)
            {
                return;
            }

            if (procedure.Steps[STEP_POSITION].SavedRequestId != 0)
            {
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_SendRequest);
                timer.Interval = 1;
                DELAY = procedure.Steps[STEP_POSITION].DelayBeforeSending;
                timer.Enabled = true; // inicia a contagem regressiva para envio do request
            }
        }

        #region Execucao dos steps com controle do Timer

        private void timer_SendRequest(object sender, EventArgs e)
        {
            try
            {
                timer.Interval = 999;

                if (DELAY > 0)
                {
                    availableStepCountDownStatusEventArgs.Status = "waiting to send:  " + DELAY + " secs";
                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                    DELAY--;

                    return;
                }

                timer.Enabled = false;
                timer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_SendRequest);

                availableStepCountDownStatusEventArgs.Status = "sending";
                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                if (procedure.Steps[STEP_POSITION].VerifyExecution)
                {
                    DELAY = procedure.Steps[STEP_POSITION].VerifyIntervalStart;
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_StartReportVerification);
                    timer.Interval = 1;

                    if (DELAY == 0)
                    {
                        // assina o evento que obtem as telemetrias recebidas
                        CONDITION_VERIFIED = false;
                        communication.TelemetryReceived += new TelemetryEventHandler(VerifyReceivedReports);
                    }
                    
                    communication.SendRequest(procedure.Steps[STEP_POSITION].Request, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                    timer.Enabled = true; // inicia a contagem regressiva para inicio da verificacao

                    return;
                }

                CONDITION_VERIFIED = false;
                communication.SendRequest(procedure.Steps[STEP_POSITION].Request, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);

                availableStepCountDownStatusEventArgs.Status = "execution ok!";
                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                StepWise();
            }
            catch (Exception)
            {
            }
        }

        private void timer_StartReportVerification(object sender, EventArgs e)
        {
            try
            {
                timer.Interval = 999;

                if (DELAY > 0)
                {
                    availableStepCountDownStatusEventArgs.Status = "waiting to start verification:  " + DELAY + " secs";
                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                    DELAY--;

                    return;
                }
                
                timer.Enabled = false;
                timer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_StartReportVerification);
                
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_StopReportVerification);
                timer.Interval = 1;
                DELAY = (procedure.Steps[STEP_POSITION].VerifyIntervalEnd - procedure.Steps[STEP_POSITION].VerifyIntervalStart);

                // Verifica se existe delay antes de dar inicio a verificacao das telemetrias.
                if (procedure.Steps[STEP_POSITION].VerifyIntervalStart > 0)
                {
                    // assina o evento que obtem as telemetrias recebidas
                    CONDITION_VERIFIED = false;
                    communication.TelemetryReceived += new TelemetryEventHandler(VerifyReceivedReports);
                }

                timer.Enabled = true;
            }
            catch (Exception)
            {
            }
        }

        private void timer_StopReportVerification(object sender, EventArgs e)
        {
            try
            {
                timer.Interval = 999;

                if ((DELAY > 0) && (CONDITION_VERIFIED == false))
                {
                    availableStepCountDownStatusEventArgs.Status = "waiting for answer to verify step: " + DELAY + " secs";
                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                    DELAY--;

                    return;
                }

                communication.TelemetryReceived -= new TelemetryEventHandler(VerifyReceivedReports);
                timer.Enabled = false;
                timer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_StopReportVerification);

                if (!CONDITION_VERIFIED)
                {
                    if (currentVerifyCondition.Equals("DONT RECEIVE A SPECIFIC REPORT"))
                    {
                        availableStepCountDownStatusEventArgs.Status = "execution ok!";
                        availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                        availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                    }
                    else
                    {
                        availableStepCountDownStatusEventArgs.Status = "failure by timeout!";
                        availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                        availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                        if (stopExecution)
                        {
                            Stop(false);
                        }
                    }

                    currentVerifyCondition = String.Empty;
                }

                if (stopped)
                {
                    return;
                }

                StepWise();
            }
            catch (Exception)
            {
            }
        }

        private void StepWise()
        {
            STEP_POSITION++;

            if (STEP_POSITION < STEPS_COUNT)
            {
                InitiateStep();
            }
            else if (currentIteration < procedure.LoopIterations)
            {
                // Aqui inicia-se outra iteracao

                STEP_POSITION = 0;

                // Disponibilizar a posicao da iteracao
                currentIteration++;
                availableNumIteractionInExecEventArgs.Iteraction = currentIteration;
                availableNumIteractionInExecution(this, availableNumIteractionInExecEventArgs);

                InitiateStep();
            }
            else
            {
                Stop(false);
            }
        }

        #endregion

        private bool IsReportOfTheLastRequest(byte[] report)
        {
            // Obter o ssc do request enviado para comparar com o ssc relatado no report correspondente.
            // De acordo com o padrao ECSS-E-70-41-A, pag 42, o campo Source Sequence Count tem 14 bits e se inicia a partir do bit numero 18.
            int sscSent = (int)procedure.Steps[STEP_POSITION].Request.GetPart(18, 14);

            // Obter o sequence counter do ultimo request enviado, presente no report recebido.
            // Temos os seguintes Acks e Nacks:

            // 1.1 - Telecommand Acceptance Report - Success
            // 1.2 - Telecommand Acceptance Report - Failure
            // 1.7 - Telecommand Execution Completed Report - Success
            // 1.8 - Telecommand Execution Completed Report - Failute
            
            // A situacao eh a seguinte:

            // As TMs que apresentam 'Failure' concatenam junto ao AppData o motivo da falha, e as TMs que apresentam 'Success' nao apresentam motivo nenhum.
            // O motivo da falha eh acrescentado apos o 'Sequence Count' correspondente ao TC enviado.
            // Isso significa que para as TMs que informam 'Success', a posicao do 'Sequence Count' do TC eh 14 Bits anteriores ao CRC.
            // Para as TMs que informam 'Failure', a posicao do 'Sequence Count' do TC eh 14 Bits anteriores ao motivo da falha + o CRC.
            // Exemplo 1:
            // 08-02-C4-EA-00-11-00-01-02-64-68-55-B3-9B-45-07-18-02-C[0-17]-26-02-7D-6D
            // A TM acima apresenta Falha de aceitacao. Nesse caso, encontra-se nos 14 Bits (destacados com colchetes[]) antes dos ultimos 4 bytes.
            // Nele o 'Sequence Count' eh 23, ou seja, eh um Nack do TC de sequencia 23.
            // Exemplo 2:
            // 08-02-C4-DD-00-0F-00-01-01-64-68-55-B3-68-31-6F-18-02-C[0-12]-04-60
            // A TM acima apresenta sucesso na aceitacao. Nesse caso nao tem motivo nenhum de falha, porque foi aceito com sucesso.
            // Por isso, o 'Sequence Count' do TC correspondente sao os 14 Bits anteriores ao CRC, que sao os ultimos dois bytes.
            // Nele o 'Sequence Count' eh 18, ou seja, eh um Ack do TC de sequencia 18.

            // Agora, o que deve ser feito eh verificar qual eh o status, se eh Ack ou Nack para obter os Bits correspondentes ao 'Sequence Count'.

            byte firstByte = 0;
            byte secondByte = 0;

            if (((report[7] == 1) && (report[8] == 1)) || 
               ((report[7] == 1) && (report[8] == 7))) // Success
            {
                firstByte = report[(report.Length - 2) - 2];
                secondByte = report[(report.Length - 2) - 1];
            }
            else if (((report[7] == 1) && (report[8] == 2)) || 
                    ((report[7] == 1) && (report[8] == 8))) // Failure
            {
                firstByte = report[(report.Length - 2) - 4];
                secondByte = report[(report.Length - 2) - 3];
            }
            
            firstByte &= 0x3f; // descarta os dois ultimos bits do primeiro byte do Sequence Count para ficar com os 14 bits

            int sscReported = (int)(secondByte + (firstByte << 8));

            if (sscSent == sscReported)
            {
                return true;
            }

            return false;
        }

        private void VerifyReceivedReports(object sender, TelemetryEventArgs eventArgs)
        {
            currentVerifyCondition = procedure.Steps[STEP_POSITION].VerifyCondition.ToUpper();

            switch (currentVerifyCondition)
            {
                case "RECEIVE ACCEPTANCE ACK":
                    {
                        if (((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 1)) || // [1.1] Telecommand Acceptance Report - Success 
                            ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 7))) // [1.7] Telecommand Execution Completed Report - Success
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "execution ok!!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                            }
                        }
                        else if (((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 2)) || // [1.2] Telecommand Acceptance Report - Failure
                                 ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 8))) // [1.8] Telecommand Execution Completed Report - Failure
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                
                                if (stopExecution)
                                {
                                    Stop(false);
                                }
                            }
                        }

                        break;
                    }
                case "RECEIVE EXECUTION ACK":
                    {
                        if ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 7)) // [1.7] Telecommand Execution Completed Report - Success
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                            }
                        }
                        else if ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 8)) // [1.8] Telecommand Execution Completed Report - Failure
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                                if (stopExecution)
                                {
                                    Stop(false);
                                }
                            }
                        }

                        break;
                    }
                case "RECEIVE ACCEPTANCE NACK":
                    {
                        if ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 1)) // [1.1] Telecommand Acceptance Report - Success 
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                                if (stopExecution)
                                {
                                    Stop(false);
                                }
                            }
                        }
                        else if ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 2)) // [1.2] Telecommand Acceptance Report - Failure
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                            }
                        }

                        break;
                    }
                case "RECEIVE EXECUTION NACK":
                    {
                        if ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 7)) // [1.7] Telecommand Execution Completed Report - Success
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                
                                if (stopExecution)
                                {
                                    Stop(false);
                                }
                            }
                        }
                        else if ((eventArgs.ServiceType == 1) && (eventArgs.ServiceSubtype == 8)) // [1.8] Telecommand Execution Completed Report - Failure
                        {
                            if (IsReportOfTheLastRequest(eventArgs.RawPacket.RawContents))
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                            }
                        }

                        break;
                    }
                case "RECEIVE A SPECIFIC REPORT":
                    {
                        if ((eventArgs.ServiceType == procedure.Steps[STEP_POSITION].ReportType &&
                             eventArgs.ServiceSubtype == procedure.Steps[STEP_POSITION].ReportSubtype))
                        {
                            if (procedure.Steps[STEP_POSITION].CheckDataField)
                            {
                                // obter o valor do data field esperado na TM recebida
                                int nBitsOfHeader = 48; // Quantia de bits do Header e de alguns campos do Data Field, como service type e subtype, etc.. essa eh a quantia de bits existentes antes da area de dados da TM.
                                int nBitsOfDataField = procedure.Steps[STEP_POSITION].DataFieldNumOfBits; // Numero de bits do data field. Essa informacao foi consultada no banco de dados.
                                int nBitsBeforeDataField = procedure.Steps[STEP_POSITION].DataFieldNumOfBitsBeforeIt; // Numero de bits existentes antes do data field dentro da area de dados da TM. Esse numero sera > 0 se existir algum data field antes dele.
                                RawPacket report = new RawPacket(false, false);
                                report = eventArgs.RawPacket;
                                uint dataFieldReceived = (uint)report.GetPart((nBitsOfHeader + nBitsBeforeDataField), nBitsOfDataField);

                                switch (procedure.Steps[STEP_POSITION].ComparisonOperation)
                                {
                                    case "=":
                                        {
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_bool"))
                                            {
                                                if (bool.Parse(procedure.Steps[STEP_POSITION].ValueToCompare.ToString()).Equals(bool.Parse(dataFieldReceived.ToString())))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (Array.Equals(procedure.Steps[STEP_POSITION].ValueToCompare, dataFieldReceived))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (procedure.Steps[STEP_POSITION].ValueToCompare == dataFieldReceived)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }

                                            break;
                                        }
                                    case "!=":
                                        {
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_bool"))
                                            {
                                                if (!bool.Parse(procedure.Steps[STEP_POSITION].ValueToCompare.ToString()).Equals(bool.Parse(dataFieldReceived.ToString())))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (!Array.Equals(procedure.Steps[STEP_POSITION].ValueToCompare, dataFieldReceived))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (procedure.Steps[STEP_POSITION].ValueToCompare != dataFieldReceived)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }

                                            break;
                                        }
                                    case ">":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao

                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived > procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived > procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }

                                            break;
                                        }
                                    case "<":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao
                                            
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived < procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived < procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }

                                            break;
                                        }
                                    case ">=":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao
                                            
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived >= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived >= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }

                                            break;
                                        }
                                    case "<=":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao

                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived <= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived <= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                }
                                            }

                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "execution ok!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                            }
                        }

                        break;
                    }
                case "DONT RECEIVE A SPECIFIC REPORT":
                    {
                        if (eventArgs.ServiceType == procedure.Steps[STEP_POSITION].ReportType &&
                            eventArgs.ServiceSubtype == procedure.Steps[STEP_POSITION].ReportSubtype)
                        {
                            if (procedure.Steps[STEP_POSITION].CheckDataField)
                            {
                                // obter o valor do data field esperado na TM recebida
                                int nBitsOfHeader = 48; // Quantia de bits do Header e de alguns campos do Data Field, como service type e subtype, etc.. essa eh a quantia de bits existentes antes da area de dados da TM.
                                int nBitsOfDataField = procedure.Steps[STEP_POSITION].DataFieldNumOfBits; // Numero de bits do data field. Essa informacao foi consultada no banco de dados.
                                int nBitsBeforeDataField = procedure.Steps[STEP_POSITION].DataFieldNumOfBitsBeforeIt; // Numero de bits existentes antes do data field dentro da area de dados da TM. Esse numero sera > 0 se existir algum data field antes dele.
                                RawPacket report = new RawPacket(false, false);
                                report = eventArgs.RawPacket;
                                uint dataFieldReceived = (uint)report.GetPart((nBitsOfHeader + nBitsBeforeDataField), nBitsOfDataField);

                                switch (procedure.Steps[STEP_POSITION].ComparisonOperation)
                                {
                                    case "=":
                                        {
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_bool"))
                                            {
                                                if (bool.Parse(procedure.Steps[STEP_POSITION].ValueToCompare.ToString()).Equals(bool.Parse(dataFieldReceived.ToString())))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (Array.Equals(procedure.Steps[STEP_POSITION].ValueToCompare, dataFieldReceived))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (procedure.Steps[STEP_POSITION].ValueToCompare == dataFieldReceived)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);

                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    case "!=":
                                        {
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_bool"))
                                            {
                                                if (!bool.Parse(procedure.Steps[STEP_POSITION].ValueToCompare.ToString()).Equals(bool.Parse(dataFieldReceived.ToString())))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (!Array.Equals(procedure.Steps[STEP_POSITION].ValueToCompare, dataFieldReceived))
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (procedure.Steps[STEP_POSITION].ValueToCompare != dataFieldReceived)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    case ">":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao

                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived > procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived > procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    case "<":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao

                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived < procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived < procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    case ">=":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao

                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived >= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived >= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    case "<=":
                                        {
                                            // o type_is_bool nao entra nesse tipo de comparacao
                                            
                                            if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_raw_hex"))
                                            {
                                                //@attention: criar um metodo na classe Utils.Formatting para converter de uint para byteArray
                                                if (dataFieldReceived <= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }
                                            else if (procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_numeric") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_list") ||
                                                     procedure.Steps[STEP_POSITION].DataFieldType.Equals("type_is_table"))
                                            {
                                                if (dataFieldReceived <= procedure.Steps[STEP_POSITION].ValueToCompare)
                                                {
                                                    DELAY = 0; // condicao de parada
                                                    CONDITION_VERIFIED = true;
                                                    availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                                    availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                                    availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                                    
                                                    if (stopExecution)
                                                    {
                                                        Stop(false);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                DELAY = 0; // condicao de parada
                                CONDITION_VERIFIED = true;
                                availableStepCountDownStatusEventArgs.Status = "failure by invalid condition!";
                                availableStepCountDownStatusEventArgs.StepPosition = STEP_POSITION;
                                availableStepCountDownStatus(this, availableStepCountDownStatusEventArgs);
                                
                                if (stopExecution)
                                {
                                    Stop(false);
                                }
                            }
                        }

                        break;
                    }

                default:
                    break;
            }
        }

        #endregion
    }

    public class AvailableStepCountDownStatusEventArgs : EventArgs
    {
        private String status;
        private int stepPosition;

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

        public int StepPosition
        {
            get
            {
                return stepPosition;
            }
            set
            {
                stepPosition = value;
            }
        }
    }

    public delegate void AvailableStepCountDownStatusEventHandler(object sender, AvailableStepCountDownStatusEventArgs e);

    public class AvailableExecutionRealTimeEventArgs : EventArgs
    {
        private String timerTick;

        public String ExecutionTimerTick
        {
            get
            {
                return timerTick;
            }
            set
            {
                timerTick = value;
            }
        }
    }

    public delegate void AvailableExecutionRealTimeEventHandler(object sender, AvailableExecutionRealTimeEventArgs e);

    public class AvailableNumIteractionInExecutionEventArgs : EventArgs
    {
        private int iteraction;

        public int Iteraction
        {
            get
            {
                return iteraction;
            }
            set
            {
                iteraction = value;
            }
        }
    }

    public delegate void AvailableNumIteractionInExecution(object sender, AvailableNumIteractionInExecutionEventArgs e);

    public class FinishExecutionEventArgs : EventArgs
    {
        private bool finish;

        public bool Finished
        {
            get
            {
                return finish;
            }
            set
            {
                finish = value;
            }
        }
    }

    public delegate void FinishExecution(object sender, FinishExecutionEventArgs e);
}

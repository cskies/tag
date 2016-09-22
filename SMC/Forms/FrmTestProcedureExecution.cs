/**
 * @file 	    FrmTestProcedureExecution.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    14/06/2011
 * @note	    Modificado em 25/06/2013 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using AdvancedDataGridView;
using System.Threading;
using Inpe.Subord.Comav.Egse.Smc.TestProcedure;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmTestProcedureExecution
     * Esta classe eh a responsavel pela inicializacao da execucao automatica dos Procedimentos de Teste.
     **/
    public partial class FrmTestProcedureExecution : DockContent
    {
        private MdiMain mdiMain = null;
        private Procedure procedure = null;
        private int procedureId;
        private Executor procedureExec = new Executor();

        // Eventos
        delegate void SetStepCountDownStatusCallBack(String status, int stepPosition);
        private SetStepCountDownStatusCallBack setStepCountDownStatusCallBack = null;
        delegate void SetStepSimpleStatusCallBack(String status, int stepPosition);
        private SetStepSimpleStatusCallBack setStepSimpleStatusCallBack = null;
        delegate void FinishedExecCallBack(bool finished);
        private FinishedExecCallBack setFinishedExec = null;
        delegate void PrintExecutionRealTimeCallBack(String timer);
        private PrintExecutionRealTimeCallBack printExecutionRealTime = null;
        delegate void PrintIteractionCallBack(int iteractionNum);
        private PrintIteractionCallBack printIteraction = null;

        private object[] stepCountDownStatusArgs = new object[2];
        private object[] stepSimpleStatusArgs = new object[2];

        private const int columnIndexNum = 0;
        private const int columnIndexSteps = 1;
        private const int columnIndexStatus = 2;

        private Color currentColorRow = new Color();
        private int currentStepPosition = 0;

        #region Propriedades

        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            get
            {
                return mdiMain.FormConnectionWithEgse;
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

        #endregion

        #region Construtor

        public FrmTestProcedureExecution(int procId, MdiMain mdi)
        {
            InitializeComponent();
            
            mdiMain = mdi;

            if (mdiMain.FormConnectionWithEgse != null)
            {
                mdiMain.FormConnectionWithEgse.FormTestProcedureExecution = this;
            }
                        
            procedureId = procId;
        }

        #endregion

        #region Eventos de interface

        private void btStartProcedure_Click(object sender, EventArgs e)
        {
            try
            {
                btStartProcedure.Enabled = false;
                btStopProcedure.Enabled = true;
                btClose.Enabled = false;

                // Preencher os steps no treeGridView
                foreach (KeyValuePair<int, Step> step in procedure.Steps)
                {
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);

                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexSteps].Style.BackColor = treeGridViewSteps.Rows[step.Key].Cells[columnIndexNum].Style.BackColor;
                                        
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexStatus].Style.BackColor = treeGridViewSteps.Rows[step.Key].Cells[columnIndexNum].Style.BackColor;
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexStatus].Value = "waiting to start";

                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexNum].Selected = false;
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexSteps].Selected = false;
                    treeGridViewSteps.Rows[step.Key].Cells[columnIndexStatus].Selected = false;
                }

                treeGridViewSteps.FirstDisplayedScrollingRowIndex = 0;
                
                // Assinar os eventos de exibicao do status da execucao de cada step
                setStepCountDownStatusCallBack += new SetStepCountDownStatusCallBack(PrintStepCountDownStatus);
                setStepSimpleStatusCallBack += new SetStepSimpleStatusCallBack(PrintStepSimpleStatus);
                printExecutionRealTime += new PrintExecutionRealTimeCallBack(PrintExecutionRealTime);
                printIteraction += new PrintIteractionCallBack(PrintNumIteractionInExecution);
                setFinishedExec += new FinishedExecCallBack(SetFinishedExec);

                // Assinar o evento de recepcao dos reports
                procedureExec.availableStepCountDownStatus += new AvailableStepCountDownStatusEventHandler(ReceiveStepCountDownStatus);
                procedureExec.availableExecutionRealTime += new AvailableExecutionRealTimeEventHandler(SetExecutionRealTime);
                procedureExec.availableNumIteractionInExecution += new AvailableNumIteractionInExecution(ReceiveNumIteractionInExecution);
                procedureExec.finishExecution += new FinishExecution(FinishingExec);

                // Carregar o procedimento
                procedureExec.Procedure = procedure;

                // setar a flag que para a execucao caso um erro ocorra na execucao dos steps
                procedureExec.StoppedExecution = chkStopExecution.Checked;

                // Iniciar a execucao do procedimento
                procedureExec.Start();
                
                txtStartedIn.Text = procedure.ExecutionData.StartTime.ToString(" dd/MM/yyyy  HH:mm:ss ");
                txtExecutionRealTime.Text = "0 ";

                txtSessionId.Text = ShowSessionId();
                mdiMain.FormSessionsLog.ShowSession(txtSessionId.Text);
                txtExecutionStatus.Text = "Executing ";
                txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Bold);
                txtExecutionStatus.BackColor = Color.FromArgb(255, 255, 100);

                btStopProcedure.Focus();
            }
            catch (Exception exx)
            {
                MessageBox.Show("Error trying to execute this procedure!\n\nPlease check your configurations.\n\n\n" + exx,
                                "Test Procedure Error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                btStartProcedure.Enabled = true;
                btClose.Enabled = true;
                btStopProcedure.Enabled = false;
                btStartProcedure.Focus();
            }
        }

        public void btStopProcedure_Click(object sender, EventArgs e)
        {
            try
            {
                // Parar a execucao do procedimento
                procedureExec.Stop(true);
                procedureExec.Procedure = null;

                txtSessionId.Text = ShowSessionId();
                txtExecutionStatus.Text = "Execution interrupted by the user ";
                txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);
                txtExecutionStatus.BackColor = Color.White;

                // Desassinar o evento de recepcao dos reports
                procedureExec.availableStepCountDownStatus -= new AvailableStepCountDownStatusEventHandler(ReceiveStepCountDownStatus);

                // Desassinar os eventos de exibicao do status da execucao de cada step
                setStepCountDownStatusCallBack -= new SetStepCountDownStatusCallBack(PrintStepCountDownStatus);
                setStepSimpleStatusCallBack -= new SetStepSimpleStatusCallBack(PrintStepSimpleStatus);
                printExecutionRealTime -= new PrintExecutionRealTimeCallBack(PrintExecutionRealTime);

                procedureExec.availableExecutionRealTime -= new AvailableExecutionRealTimeEventHandler(SetExecutionRealTime);

                btClose.Enabled = true;
                btStopProcedure.Enabled = false;
                btStartProcedure.Enabled = true;

                procedureExec.finishExecution -= new FinishExecution(FinishingExec);
                setFinishedExec -= new FinishedExecCallBack(SetFinishedExec);
                
                btStartProcedure.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Error trying to execute this procedure!\n\nPlease check your configurations.",
                                "Test Procedure Error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                btClose.Enabled = true;
                btStopProcedure.Enabled = false;
                btStartProcedure.Enabled = true;
                btStopProcedure.Focus();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            for (int index = mdiMain.DockPanel.Contents.Count - 1; index >= 0; index--)
            {
                if (mdiMain.DockPanel.Contents[index] is IDockContent)
                {
                    IDockContent content = (IDockContent)mdiMain.DockPanel.Contents[index];

                    content.DockHandler.Form.Enabled = true;

                    if (content.DockHandler.Form.Text.Equals("Test Procedures Composition"))
                    {
                        content.DockHandler.Form.Focus();
                    }

                    if (content.DockHandler.Form.Text.Equals("Test Sessions Log"))
                    {
                        mdiMain.FormSessionsLog.chkRealTime.Checked = false;
                    }
                }
            }

            ClearComponents();
        }

        private void chkStopExecution_CheckedChanged(object sender, EventArgs e)
        {
            procedureExec.StoppedExecution = chkStopExecution.Checked;
        }

        private void FrmTestProcedureExecution_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(1175, 537);
            }
        }

        #endregion

        #region Metodos Privados

        private void ClearComponents()
        {
            txtProcedureId.ResetText();
            txtDescription.ResetText();
            txtEstimatedTime.ResetText();
            txtPurpose.ResetText();
            txtStartedIn.ResetText();
            txtStartedIn.Text = " 00/00/0000  00:00:00 ";
            txtIteration.ResetText();
            txtIteration.Text = " zero of ";
            txtEstimatedTime.ResetText();
            txtExecutionRealTime.ResetText();
            txtSessionId.ResetText();
            txtExecutionStatus.ResetText();
            treeGridViewSteps.Nodes.Clear();
            this.Enabled = false;
        }

        private void ReceiveStepCountDownStatus(object sender, AvailableStepCountDownStatusEventArgs eventArgs)
        {
            stepCountDownStatusArgs[0] = eventArgs.Status;
            stepCountDownStatusArgs[1] = eventArgs.StepPosition;

            Invoke(setStepCountDownStatusCallBack, stepCountDownStatusArgs);
        }

        private void PrintStepCountDownStatus(String status, int stepPosition)
        {
            if (stepPosition == currentStepPosition)
            {
                currentColorRow = treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor;
                currentStepPosition++;
            }

            if (status.Contains("execution ok")) // verde
            {
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);

                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.LightGreen;
            }
            else if (status.Contains("waiting to send")) // amarelo
            {
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.FromArgb(255, 255, 100);
            }
            else if (status.Contains("sending")) // amarelo
            {
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.FromArgb(255, 255, 100);
            }
            else if (status.Contains("waiting to start verification")) // amarelo
            {
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.FromArgb(255, 255, 100);
            }
            else if (status.Contains("waiting for answer"))  // amarelo
            {
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = Color.FromArgb(255, 255, 100);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.FromArgb(255, 255, 100);
            }
            else if (status.Contains("failure by timeout")) // amarelo
            {
                if (!procedureExec.StoppedExecution)
                {
                    treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                    treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                    treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                }

                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.FromArgb(255, 160, 160);

                if (procedureExec.StoppedExecution && chkStopExecution.Checked)
                {
                    txtSessionId.Text = ShowSessionId();
                    txtExecutionStatus.Text = "Executed with failure(s) ";
                    txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);
                    txtExecutionStatus.BackColor = Color.White;
                }
            }
            else if (status.Contains("failure by invalid condition")) // vermelho
            {
                if (!procedureExec.StoppedExecution)
                {
                    treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                    treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                    treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Regular);
                }

                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.FromArgb(255, 160, 160);

                if (procedureExec.StoppedExecution && chkStopExecution.Checked)
                {
                    txtSessionId.Text = ShowSessionId();
                    txtExecutionStatus.Text = "Executed with failure(s) ";
                    txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);
                    txtExecutionStatus.BackColor = Color.White;
                }
            }
            else if (status.Contains("interrupted by the user")) // vermelho
            {
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.Font = new Font(treeGridViewSteps.Font, FontStyle.Bold);

                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexNum].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexSteps].Style.BackColor = currentColorRow;
                treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Style.BackColor = Color.PeachPuff;

                txtSessionId.Text = ShowSessionId();
                txtExecutionStatus.Text = "Execution interrupted by the user ";
                txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);
                txtExecutionStatus.BackColor = Color.White;
            }

            treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Value = status;

            if (stepPosition > 3) // 5 eh a quantidade dos ultimos steps executados que serao exibidos.
            {
                treeGridViewSteps.FirstDisplayedScrollingRowIndex = stepPosition - 3;
            }
            else
            {
                treeGridViewSteps.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void FinishingExec(object sender, FinishExecutionEventArgs eventArgs)
        {
            Invoke(setFinishedExec, eventArgs.Finished);
        }

        private void PrintStepSimpleStatus(String status, int stepPosition)
        {
            treeGridViewSteps.Rows[stepPosition].Cells[columnIndexStatus].Value = status;
        }

        private void SetExecutionRealTime(object sender, AvailableExecutionRealTimeEventArgs eventArgs)
        {
            Invoke(printExecutionRealTime, eventArgs.ExecutionTimerTick + " ");
        }

        private void ReceiveNumIteractionInExecution(object sender, AvailableNumIteractionInExecutionEventArgs eventArgs)
        {
            Invoke(printIteraction, eventArgs.Iteraction);
        }

        private void PrintNumIteractionInExecution(int iteraction)
        {
            txtIteration.Text = iteraction.ToString() + " of " + procedure.LoopIterations + " ";
        }

        private String ShowSessionId()
        {
            if (mdiMain.FormConnectionWithEgse.SessionId == 0)
            {
                return "No Session ";
            }
            else
            {
                return mdiMain.FormConnectionWithEgse.SessionId + " ";
            }
        }

        private void PrintExecutionRealTime(String timer)
        {
            txtExecutionRealTime.Text = timer;
        }

        private void SetFinishedExec(bool finished)
        {
            // Desassinar o evento de recepcao dos reports
            procedureExec.availableStepCountDownStatus -= new AvailableStepCountDownStatusEventHandler(ReceiveStepCountDownStatus);

            // Desassinar os eventos de exibicao do status da execucao de cada step
            setStepCountDownStatusCallBack -= new SetStepCountDownStatusCallBack(PrintStepCountDownStatus);
            setStepSimpleStatusCallBack -= new SetStepSimpleStatusCallBack(PrintStepSimpleStatus);
            printExecutionRealTime -= new PrintExecutionRealTimeCallBack(PrintExecutionRealTime);

            procedureExec.availableExecutionRealTime -= new AvailableExecutionRealTimeEventHandler(SetExecutionRealTime);

            foreach (DataGridViewRow row in treeGridViewSteps.Rows)
            {
                if (row.Cells[columnIndexStatus].Value.ToString().ToUpper().Contains("FAILURE"))
                {
                    txtSessionId.Text = ShowSessionId();
                    txtExecutionStatus.Text = "Executed with failure(s) ";
                    txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);
                    txtExecutionStatus.BackColor = Color.White;
                    break;
                }
            }

            if (!txtExecutionStatus.Text.Contains("Executed with failure(s)"))
            {
                txtSessionId.Text = ShowSessionId();
                txtExecutionStatus.Text = "Successfully executed ";
                txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);
                txtExecutionStatus.BackColor = Color.White;
            }

            btClose.Enabled = true;
            btStopProcedure.Enabled = false;
            btStartProcedure.Enabled = true;

            btStartProcedure.Focus();
        }

        #endregion

        private void FrmTestProcedureExecution_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mdiMain.FormSessionsLog != null)
            {
                if (mdiMain.FormSessionsLog.OpenedByTestProceduresComposition)
                {
                    mdiMain.FormSessionsLog.Close();
                    mdiMain.FormSessionsLog.Dispose();
                }
            }

            mdiMain.DockPanel.Panes[0].Contents[0].DockHandler.Form.Enabled = true;
            mdiMain.DockPanel.Panes[0].Contents[1].DockHandler.Form.Enabled = true;
            mdiMain.DockPanel.Panes[0].Contents[2].DockHandler.Form.Enabled = true;
            mdiMain.DockPanel.Panes[0].Enabled = true;
            mdiMain.toolStrip.Enabled = true;
            mdiMain.menuStrip.Enabled = true;
        }

        public void FrmTestProcedureExecution_Load(object sender, EventArgs e)
        {
            if (procedureId == 0)
            {
                ClearComponents();
                return;
            }

            procedure = new Procedure();
            procedure.LoadProcedure(procedureId);
            procedureExec.Communication = mdiMain.FormConnectionWithEgse;

            // Preencher o cabecalho na interface grafica
            txtProcedureId.Text = procedure.ProcedureId.ToString();
            txtDescription.Text = procedure.Description;
            txtPurpose.Text = procedure.Purpose;
            txtStartedIn.Text = " 00/00/0000  00:00:00 ";

            // O campo abaixo devera ser incrementado a cada vez que o procedimento for executado. 
            // Depende do numero total de iteracoes do campo Loop Iterations setado durante a edicao do procedimento.
            txtIteration.Text = "0 of " + procedure.LoopIterations;
            txtSessionId.Text = mdiMain.FormConnectionWithEgse.SessionId.ToString();
            txtEstimatedTime.Text = procedure.EstimatedDuration.ToString() + " ";
            txtExecutionRealTime.Text = "0 ";
            txtExecutionStatus.Text = "Not executed ";
            txtExecutionStatus.Font = new Font(txtExecutionStatus.Font, FontStyle.Regular | FontStyle.Bold);

            treeGridViewSteps.Nodes.Clear();

            // Preencher os steps no treeGridView
            foreach (KeyValuePair<int, Step> step in procedure.Steps)
            {
                TreeGridNode node = treeGridViewSteps.Nodes.Add((step.Key + 1 + " of " + procedure.Steps.Count), step.Value.Description, " waiting to start");
            }

            btStartProcedure.Enabled = true;
            btStopProcedure.Enabled = false;
            treeGridViewSteps.ClearSelection();
            btStartProcedure.Focus();
        }
    }    
}
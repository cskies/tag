/**
 * @file 	    FrmTestProcedureExecutionLog.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    04/08/2011
 * @note	    Modificado em 05/08/2011 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmTestProcedureExecutionLog
     * Este Formulario eh usado para registrar os logs da execucao dos procedimentos.
     **/
    public partial class FrmTestProcedureExecutionLog : Form
    {
        #region Variaveis

        private FrmTestProcedureExecution frmProcExecution = null;

        #endregion

        #region Construtor

        public FrmTestProcedureExecutionLog(FrmTestProcedureExecution frmExecution)
        {
            InitializeComponent();

            frmProcExecution = frmExecution;
        }

        #endregion

        #region Metodos Publicos

        public void Execute()
        {
            //frmProcExecution.ExecuteProcedure();
        }

        #endregion

        #region Eventos de Interface

        private void btStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTestProcedureExecutionLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmProcExecution.btStopProcedure_Click(null, new EventArgs());
        }

        #endregion
    }
}

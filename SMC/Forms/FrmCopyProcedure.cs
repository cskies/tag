/**
 * @file 	    FrmCopyProcedure.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    22/06/2011
 * @note	    Modificado em 11/01/2013 por Thiago.
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
using System.Data.OleDb;

/**
 * @Namespace Este namespace contem todas as classes que fornecem suporte a varias necessidades
 * do SMC, como manipulacao e exibicao de dados, formatacao de dados, dentre outras..
 **/
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmCopyProcedure
     * Este formulario obtem a nova descricao do procedimento que sera copiado.
     **/
    public partial class FrmCopyProcedure : Form
    {
        #region Atributos

        private FrmTestProceduresComposition frmProcComposition = null;

        #endregion

        #region Construtor

        public FrmCopyProcedure(FrmTestProceduresComposition frmProcComp)
        {
            InitializeComponent();
            frmProcComposition = frmProcComp;

            txtCurrentProcDescription.Text = frmProcComposition.gridDatabase[1, frmProcComposition.gridDatabase.CurrentRow.Index].Value.ToString();
            txtNewProcDescription.Text = frmProcComposition.gridDatabase[1, frmProcComposition.gridDatabase.CurrentRow.Index].Value.ToString();
            txtNewProcDescription.SelectAll();
        }

        #endregion

        #region Eventos da interface

        private void btCopy_Click(object sender, EventArgs e)
        {
            if (txtNewProcDescription.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field '" + lblNewDescription.Text + "' is empty ! \n\nFill it and try again.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                txtNewProcDescription.Focus();
                txtNewProcDescription.SelectAll();
                return;
            }

            String sql = "select count(*) as ExistsEquals from test_procedures where description = '" + txtNewProcDescription.Text.Trim() + "'";
            int countEquals = (int)DbInterface.ExecuteScalar(sql);

            if (txtCurrentProcDescription.Text.Trim().Equals(txtNewProcDescription.Text.Trim()) ||
                (countEquals > 0))
            {
                MessageBox.Show("The 'New Procedure Description' already exist ! \n\nFill it and try again.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                txtNewProcDescription.Focus();
                txtNewProcDescription.SelectAll();
                return;
            }

            try
            {
                //Instanciar os objetos de Conexao e Iniciar a Transacao
                OleDbConnection conn = new OleDbConnection();
                OleDbCommand cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();

                // Inicia transacao
                OleDbTransaction transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                sql = @"insert into test_procedures (procedure_id,
						                            description, 
						                            purpose, 
						                            estimated_duration, 
						                            synchronize_obt, 
						                            get_cpu_usage, 
						                            run_in_loop, 
						                            loop_iterations, 
						                            send_mail, 
						                            packets_sequence_control_options, 
						                            executed)
                       select (select max(procedure_id)+1 from test_procedures), description, purpose, estimated_duration, synchronize_obt, get_cpu_usage, run_in_loop, loop_iterations, send_mail, packets_sequence_control_options, executed from test_procedures where procedure_id = " + frmProcComposition.gridDatabase[0, frmProcComposition.gridDatabase.CurrentRow.Index].Value.ToString();
                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // Atualizar os campos description e executed. O procedimento que esta sendo copiado nao pode ter a mesma descricao e o campo executed tem que ser 'false' porque ainda nao foi executado.
                sql = @"update test_procedures set description = '" + txtNewProcDescription.Text.Trim() + "', executed = 'false' where procedure_id = (select max(procedure_id) from test_procedures)";
                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();                

                sql = @"insert into test_procedure_steps (procedure_id,
			                                              position, 
			                                              saved_request_id,
			                                              time_delay, 
			                                              verify_execution, 
			                                              verify_condition, 
			                                              report_type,
			                                              report_subtype, 
			                                              data_field_id, 
			                                              comparison_operation,
			                                              value_to_compare,
			                                              verify_interval_start,
			                                              verify_interval_end)
                        select (select distinct(max(procedure_id)) from test_procedures), position, saved_request_id, time_delay, verify_execution, verify_condition, report_type, report_subtype, data_field_id, comparison_operation, value_to_compare, verify_interval_start, verify_interval_end from test_procedure_steps where procedure_id = " + frmProcComposition.gridDatabase[0, frmProcComposition.gridDatabase.CurrentRow.Index].Value.ToString();

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // Finalizar transacao
                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                // Atualizar o grid
                int index = frmProcComposition.gridDatabase.CurrentRow.Index;
                frmProcComposition.RefreshGrid();
                frmProcComposition.gridDatabase.Rows[index].Cells[0].Selected = true;
                frmProcComposition.gridDatabase_SelectionChanged(null, new EventArgs());
                txtNewProcDescription.Focus();
                txtNewProcDescription.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to copy procedure. \n\nError: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}

/**
 * @file 	    FrmClearTestSessionsLog.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Bruna Elisa Zanchetta Buani
 * @date	    30/10/2013
 * @note	    Modificado em 08/11/2013 por Bruna.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Data.OleDb;
using Inpe.Subord.Comav.Egse.Smc.TestSession;
using System.Data.Common;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmClearTestSessionsLog : DockContent
    {
        public int numberOfSessions = (int)DbInterface.ExecuteScalar(@"select count(*) from sessions");

        public FrmClearTestSessionsLog()
        {
            InitializeComponent();
            LoadClearTestSession();
        }

        public void LoadClearTestSession()
        {
            btClearSessionsFrom.Focus();
            rdBtoClearAllSessions.Checked = false;
            btClearAllSessions.Enabled = false;
            rdBtoClearSessionsFrom.Checked = true;
            btClearSessionsFrom.Text = "Clear Test Sessions Log From: " + numberOfSessions;

            numberOfSessions = (int)DbInterface.ExecuteScalar(@"select count(*) from sessions");
            if (numberOfSessions == 0)
            {
                cmbSelectSessionsToClear.Items.Clear();
                cmbSelectSessionsToClear.Enabled = false;
                cmbSelectSessionsToClear.Items.Add("TEST SESSIONS LOG IS EMPTY!!");
                cmbSelectSessionsToClear.SelectedIndex = 0;
                btClearSessionsFrom.Text = "Clear Test Sessions Log From: " + numberOfSessions;
            }
            else
            {
                if (DbInterface.TestConnection())
                {
                    String whereClause = "";
                    String sqlCaracteres = "select isnull(MAX(session_id), 0) from sessions";
                    String caracteres = Convert.ToString(DbInterface.ExecuteScalar(sqlCaracteres));
                    Int32 nOfCaracteres = caracteres.Length;

                    String sql = @"select CAST(session_id  AS VARCHAR) + ' [from ' + 
                                  convert(varchar, start_time, 103) + ' ' + 
                                  convert(varchar, start_time, 108) + ' to ' + 
                                  convert(varchar, end_time, 103) + ' ' + 
                                  convert(varchar, end_time, 108) + ', through ' +
                                  connection_type + 
                                  case when ((isnull(swapl_version, 0) = 0) and (isnull(swapl_release, 0) = 0) and (isnull(swapl_patch, 0) = 0)) then 
										']' 
								  else 
										', SW APL Version ' + isnull(convert(varchar, swapl_version), '') + '.' + isnull(convert(varchar, swapl_release), '') + '.' + isnull(convert(varchar, swapl_patch), '') + ']' 
								  end 
                           from sessions " + whereClause + " order by session_id desc";

                    DataTable table = DbInterface.GetDataTable(sql);
                    cmbSelectSessionsToClear.Items.Clear();

                    foreach (DataRow row in table.Rows)
                    {
                        cmbSelectSessionsToClear.Items.Add(row[0]);
                    }
                    cmbSelectSessionsToClear.SelectedIndex = 0;
                    this.ActiveControl = btClearSessionsFrom;
                }
            }
            
        }

        private void btClearAllSessions_Click(object sender, EventArgs e)
        {
            numberOfSessions = (int)DbInterface.ExecuteScalar(@"select count(*) from sessions");
            if (numberOfSessions == 0)
            {
                if (MessageBox.Show("Test Sessions Log is Empty!!",
                                                        "Clear All Test Sessions Log",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    //cancela a exclusao
                    cmbSelectSessionsToClear.Items.Add("TEST SESSIONS LOG IS EMPTY!!");
                    cmbSelectSessionsToClear.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("Do you REALLY want to clear ALL Test Sessions Logs?\n\n" +
                                                        "There are currently " + numberOfSessions + " records in the database.\n\n If you choose 'OK', all these Sessions will be deleted from database!",
                                                        "Clear All Test Sessions Log",
                                                        MessageBoxButtons.OKCancel,
                                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    //cancela a exclusao
                    return;
                }
                else
                {
                    numberOfSessions = 0;
                    clearSessions(numberOfSessions);
                }
            }
        }

        private void btClearSessionsFrom_Click_1(object sender, EventArgs e)
        {
            numberOfSessions = (int)DbInterface.ExecuteScalar(@"select count(*) from sessions");
            if (numberOfSessions == 0)
            {
                cmbSelectSessionsToClear.Items.Clear();
                cmbSelectSessionsToClear.Items.Add("TEST SESSIONS LOG IS EMPTY!!");
                cmbSelectSessionsToClear.SelectedIndex = 0;
                cmbSelectSessionsToClear.Enabled = false;
                btClearSessionsFrom.Text = "Clear Test Sessions Log From: " + 0;
                
                if (MessageBox.Show("Test Sessions Log is Empty!!",
                                                        "Clear All Test Sessions Log",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    //cancela a exclusao
                    return;
                }
            }
            else
            {
                Int32 indexFirst = cmbSelectSessionsToClear.Text.IndexOf("[");
                Int32 sessionID = Convert.ToInt32(cmbSelectSessionsToClear.Text.Substring(0, indexFirst));

                if (MessageBox.Show("Do you REALLY want to clear Test Sessions Logs starting from Session ID [" + sessionID + "]?\n\n" +
                                             "If you choose 'OK', all these Selected Test Sessions Logs will be deleted from database!",
                                             "Clear Selected Sessions",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    //cancela a exclusao
                    return;
                }
                else
                {
                    clearSessions(sessionID);
                }
            }
        }

        private void clearSessions(Int32 sessionID)
        {
            bool executed = false;
            String sql1 = null;
            String sql2 = null;
            String sql3 = null;

            if (sessionID > 0)
            {
                sql1 = @"delete from packets_log_data_field where session_id >= " + sessionID;
                sql2 = @"delete from packets_log where session_id >= " + sessionID;
                sql3 = @"delete from sessions where session_id >= " + sessionID;
            }

            else 
            {
                sql1 = @"delete from packets_log_data_field";
                sql2 = @"delete from packets_log";
                sql3 = @"delete from sessions";
            }

            DbInterface interfaceWithDB = new DbInterface();
            
            try
            {
                interfaceWithDB.BeginTransaction();

                this.Cursor = Cursors.WaitCursor;
                cmbSelectSessionsToClear.Enabled = false;
                this.ActiveControl = btClearSessionsFrom;
                
                //forca a atualizacao da interface grafica
                Refresh();
                                
                //exclui tabela packets_log_data_field
                // verificar o retorno, apenas seguir se retornou true
                executed = interfaceWithDB.ExecuteNonQueryInTransactionWithTimeout(sql1, 2400);

                if (executed == true)
                {
                    //exclui tabela packets_log
                    // verificar o retorno, apenas seguir se retornou true
                    executed = interfaceWithDB.ExecuteNonQueryInTransaction(sql2);

                    if (executed == true)
                    {
                        //exclui tabela sessions
                        executed = interfaceWithDB.ExecuteNonQueryInTransaction(sql3);
                        interfaceWithDB.CommitTransaction();
                    }
                }
            }
            catch (OleDbException ex)
            {
                interfaceWithDB.RollbackTransaction();
                MessageBox.Show("Database error: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                executed = false;
            }

            if (executed == true)
            {
                MessageBox.Show("Test Sessions Log were successfully cleared!!", "Session(s) Deleted");
                cmbSelectSessionsToClear.Items.Clear();
                LoadClearTestSession();
            }

            //setando as variaveis para seus valores default novamente
            this.Cursor = Cursors.Default;
            
            if (rdBtoClearSessionsFrom.Checked)
            {
                cmbSelectSessionsToClear.Items.Clear();
                LoadClearTestSession();
                //para recarregar o cmb com as sessoes atualizadas depois do delete
                cmbSelectSessionsToClear.Enabled = true;
                this.ActiveControl = btClearSessionsFrom;
            }
        }

        private void rdBtoClearAllSessions_CheckedChanged(object sender, EventArgs e)
        {
            numberOfSessions = (int)DbInterface.ExecuteScalar(@"select count(*) from sessions");
            if (numberOfSessions == 0)
            {
                cmbSelectSessionsToClear.Items.Clear();
                cmbSelectSessionsToClear.Enabled = false;
                cmbSelectSessionsToClear.Items.Add("TEST SESSIONS LOG IS EMPTY!!");
                cmbSelectSessionsToClear.SelectedIndex = 0;
                btClearSessionsFrom.Text = "Clear Test Sessions Log From: " + numberOfSessions;
            }
            btClearAllSessions.Enabled = true;
            btClearSessionsFrom.Enabled = false;
            btClearAllSessions.Focus();
            cmbSelectSessionsToClear.Enabled = false;
            this.Refresh();
        }

        private void rdBtoClearSessionsFrom_CheckedChanged(object sender, EventArgs e)
        {
            numberOfSessions = (int)DbInterface.ExecuteScalar(@"select count(*) from sessions");
            if (numberOfSessions == 0)
            {
                cmbSelectSessionsToClear.Items.Clear();
                cmbSelectSessionsToClear.Enabled = false;
                cmbSelectSessionsToClear.Items.Add("TEST SESSIONS LOG IS EMPTY!!");
                cmbSelectSessionsToClear.SelectedIndex = 0;
                btClearSessionsFrom.Text = "Clear Test Sessions Log From: " + numberOfSessions;
            }
           this.ActiveControl = btClearSessionsFrom;
            btClearAllSessions.Enabled = false;
            btClearSessionsFrom.Enabled = true;
            cmbSelectSessionsToClear.Enabled = true;
            btClearSessionsFrom.Focus();
            this.ActiveControl = btClearSessionsFrom;
            this.Refresh();
        }

        private void cmbSelectSessionsToClear_SelectedIndexChanged(object sender, EventArgs e)
        {
            btClearSessionsFrom.Text = "Clear Test Sessions Log from Session: " + cmbSelectSessionsToClear.Text.Substring(0, 4);
            btClearSessionsFrom.Focus();
            this.ActiveControl = btClearSessionsFrom;
            this.Refresh();
        }
    }
}


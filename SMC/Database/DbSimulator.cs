/**
 * @file 	    DbSimulator.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    12/07/2012
 * @note	    Modificado em 19/07/2012 por Thiago.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.OleDb;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbSimulator
     * Classe criada para o gerenciamento da criacao dos simuladores.
     **/
    class DbSimulator
    {
        #region Atributos Internos

        private int simId;
        private String name;
        private String desc;
        private List<DbSimulatorMsgToSend> msgsToSend = new List<DbSimulatorMsgToSend>();
        private List<DbSimulatorMsgToReceive> msgsToReceive = new List<DbSimulatorMsgToReceive>();
        private OleDbConnection conn = null;
        private OleDbCommand cmd = null;
        private OleDbTransaction transaction = null;

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

        public String SimName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public String Description
        {
            get
            {
                return desc;
            }
            set
            {
                desc = value;
            }
        }

        public List<DbSimulatorMsgToReceive> MessagesToReceive
        {
            get
            {
                return msgsToReceive;
            }
            set
            {
                msgsToReceive = value;
            }
        }

        public List<DbSimulatorMsgToSend> MessagesToSend
        {
            get
            {
                return msgsToSend;
            }
            set
            {
                msgsToSend = value;
            }
        }

        #endregion

        #region Construtor

        public DbSimulator()
        {
        }

        #endregion

        #region Metodos Privados

        private bool BeginTransaction()
        {
            try
            {
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CommitTransaction()
        {
            try
            {
                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool RollbackTransaction()
        {
            try
            {
                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool InsertSimulator()
        {
            try
            {
                String sql = @"insert into simulators (sim_id, 
                                                       sim_name, 
                                                       sim_desc) 
                               values (" + simId + ", '" + name + "', '" + desc + "')";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool UpdateSimulator()
        {
            try
            {
                String sql = @"update simulators set sim_name = '" + name + "', sim_desc = '" + desc + "' where sim_id = " + simId;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool DeleteSimulator()
        {
            try
            {
                String sql = "delete from simulators where sim_id = " + simId;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool InsertMsgsToSend()
        {
            try
            {
                if (msgsToSend != null)
                {
                    for (int i = 0; i < msgsToSend.Count; i++)
                    {
                        String sql = @"insert into sim_msgs_to_send (sim_id,
                                                                     msg_id,
                                                                     msg_name, 
                                                                     msg_to_send, 
                                                                     delay_to_send_again)
                                       values (" + simId + ", " + msgsToSend[i].MessageId + ", '" + msgsToSend[i].Name + "', ?, " + msgsToSend[i].DelayToSendAgain + ")";

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.VarBinary, msgsToSend[i].MessageToSend.GetLength(0)));
                        cmd.Parameters["@P1"].Value = msgsToSend[i].MessageToSend;

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool DeleteMsgsToSend()
        {
            try
            {
                String sql = "delete from sim_msgs_to_send where sim_id = " + simId;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool InsertMsgsToReceive()
        {
            try
            {
                if (msgsToReceive != null)
                {
                    for (int i = 0; i < msgsToReceive.Count; i++)
                    {
                        String sql = @"insert into sim_msgs_to_receive (sim_id,
                                                                        msg_id,
                                                                        msg_name, 
                                                                        msg_to_receive, 
                                                                        check_full_msg, 
                                                                        delay_to_answer, 
                                                                        msg_to_answer, 
                                                                        repeat_answer, 
                                                                        answer_repetition_interval)
                                       values (" + simId + ", " + msgsToReceive[i].MessageId + ", '" + msgsToReceive[i].Name + "', ?, '" + msgsToReceive[i].CheckFullMessage + "', " + msgsToReceive[i].DelayToAnswer + ", ?, '" + msgsToReceive[i].RepeatAnswer + "', " + msgsToReceive[i].RepetitionInterval + ")";

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.VarBinary, msgsToReceive[i].MessageToReceive.GetLength(0)));
                        cmd.Parameters.Add(new OleDbParameter("@P2", OleDbType.VarBinary, msgsToReceive[i].MessageToAnswer.GetLength(0)));

                        cmd.Parameters["@P1"].Value = msgsToReceive[i].MessageToReceive;
                        cmd.Parameters["@P2"].Value = msgsToReceive[i].MessageToAnswer;

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool DeleteMsgsToReceive()
        {
            try
            {
                String sql = "delete from sim_msgs_to_receive where sim_id = " + simId;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Metodos Publicos

        public bool Insert()
        {
            if (!BeginTransaction())
            {
                return false;
            }

            if (!InsertSimulator())
            {
                RollbackTransaction();
                return false;
            }

            if (!InsertMsgsToSend())
            {
                RollbackTransaction();
                return false;
            }

            if (!InsertMsgsToReceive())
            {
                RollbackTransaction();
                return false;
            }

            if (!CommitTransaction())
            {
                return false;
            }

            return true;
        }

        public bool Update()
        {
            if (!BeginTransaction())
            {
                return false;
            }

            if (!UpdateSimulator())
            {
                RollbackTransaction();
                return false;
            }

            if (!DeleteMsgsToSend())
            {
                RollbackTransaction();
                return false;
            }

            if (!DeleteMsgsToReceive())
            {
                RollbackTransaction();
                return false;
            }

            if (!InsertMsgsToSend())
            {
                RollbackTransaction();
                return false;
            }

            if (!InsertMsgsToReceive())
            {
                RollbackTransaction();
                return false;
            }

            if (!CommitTransaction())
            {
                return false;
            }

            return true;
        }

        public bool Delete()
        {
            if (!BeginTransaction())
            {
                return false;
            }

            if (!DeleteMsgsToSend())
            {
                RollbackTransaction();
                return false;
            }

            if (!DeleteMsgsToReceive())
            {
                RollbackTransaction();
                return false;
            }

            if (!DeleteSimulator())
            {
                RollbackTransaction();
                return false;
            }

            if (!CommitTransaction())
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}

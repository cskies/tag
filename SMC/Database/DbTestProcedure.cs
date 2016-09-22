/**
 * @file 	    DbTestProcedure.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    23/03/2010
 * @note	    Modificado em 19/09/2013 por Bruna.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbTestProcedure
     * Classe para o gerenciamento da persistencia dos Test Procedures.
     **/
    class DbTestProcedure : DbInterface
    {
        #region Atributos

        private OleDbConnection conn = null;
        private OleDbCommand cmd = null;
        private OleDbTransaction transaction = null;

        private String procedureId = "";
        private String description = "";
        private String purpose = "";
        private String estimatedDuration = "";
        private bool syncObtAtTheBeggining = false;
        private bool getCpuUsage = false;
        private bool runInLoop = false;
        private int loopsIterations = 0;
        private String packetsSequenceControl = "";
        private bool sendEmail = false;
        private List<object> procedureSteps = null;
        private String contour_test_case_id = "";

        #endregion

        #region Construtor

        public DbTestProcedure()
        {
        }

        #endregion

        #region Propriedades

        public String ProcedureId
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

        public String EstimatedDuration
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

        public bool SyncObtAtTheBeggining
        {
            get
            {
                return syncObtAtTheBeggining;
            }
            set
            {
                syncObtAtTheBeggining = value;
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

        public int LoopsIterations
        {
            get
            {
                return loopsIterations;
            }
            set
            {
                loopsIterations = value;
            }
        }

        public String PacketsSequenceControl
        {
            get
            {
                return packetsSequenceControl;
            }
            set
            {
                packetsSequenceControl = value;
            }
        }

        public bool SendEmail
        {
            get
            {
                return sendEmail;
            }
            set
            {
                sendEmail = value;
            }
        }

        public List<object> ProcedureSteps
        {
            get
            {
                return procedureSteps;
            }
            set
            {
                procedureSteps = value;
            }
        }

        public String Contour_test_case_id
        {
            get
            {
                return contour_test_case_id;
            }
            set
            {
                contour_test_case_id = value;
            }
        }

        #endregion

        #region Metodos Publicos

        public bool Insert()
        {
            try
            {
                // Instanciar os objetos de Conexao e Iniciar a Transacao
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                
                // Inserir o Procedure Header
                String sql = @"insert into test_procedures (procedure_id, 
                                                            description, 
                                                            purpose, 
                                                            estimated_duration, 
                                                            synchronize_obt, 
                                                            get_cpu_usage, 
                                                            run_in_loop, 
                                                            loop_iterations, 
                                                            send_mail, 
                                                            packets_sequence_control_options,
                                                            executed, contour_test_case_id)
                                                    values ('" + procedureId + @"', 
                                                            '" + description + @"', 
                                                            '" + purpose + @"', 
                                                            '" + estimatedDuration + @"', 
                                                            '" + syncObtAtTheBeggining + @"', 
                                                            '" + getCpuUsage + @"', 
                                                            '" + runInLoop + @"', 
                                                            '" + loopsIterations + @"', 
                                                            '" + sendEmail + @"', 
                                                            '" + packetsSequenceControl + @"',
                                                             0 ,'" + contour_test_case_id + "')";

                RefreshRecords();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // Inserir Procedure Steps
                InsertProcedureSteps();

                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                
                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return false;
            }
        }

        public bool Update()
        {
            try
            {
                // Instanciar os objetos de Conexao e Iniciar a Transacao
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                String sql = @"update test_procedures set description = '" + description + @"', 
                                                        purpose = '" + purpose + @"', 
                                                        estimated_duration = '" + estimatedDuration + @"', 
                                                        synchronize_obt = '" + syncObtAtTheBeggining + @"', 
                                                        get_cpu_usage = '" + getCpuUsage + @"', 
                                                        run_in_loop = '" + runInLoop + @"', 
                                                        loop_iterations = '" + loopsIterations + @"', 
                                                        send_mail = '" + sendEmail + @"', 
                                                        packets_sequence_control_options = '" + packetsSequenceControl + @"',
                                                        contour_test_case_id = '" + contour_test_case_id + @"'
                            where procedure_id = " + procedureId;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // primeiro devo deletar os steps anteriores do banco de dados e inserir os novos
                sql = "delete test_procedure_steps where procedure_id = " + procedureId;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // Inserir Procedure Steps
                InsertProcedureSteps();

                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                // Instanciar os objetos de Conexao e Iniciar a Transacao
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                String sql = "delete test_procedure_steps where procedure_id = " + procedureId;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "delete from test_procedures where procedure_id = " + procedureId;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return false;
            }
        }           

        /**
         * Este metodo atualiza todos os estimatedDuration em apenas alguns niveis abaixo da hierarquia dos estimatedDurations
         * usados por outros Test Procedures.
         **/
        public bool UpdateEstimatedDuration(int newEstimatedDuration, int innerProcedureId)
        {
            try
            {
                //Instanciar os objetos de Conexao e Iniciar a Transacao
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                String sqlUpdateDuration = @"update test_procedures set estimated_duration = '" + estimatedDuration + @"'                                                         
                                             where procedure_id = " + procedureId;

                cmd.CommandText = sqlUpdateDuration;
                cmd.ExecuteNonQuery();

                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
                
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return false;
            }
        }

        #endregion

        #region Metodos Privados

        private bool InsertProcedureSteps()
        {
            // inserir os Procedure Steps
            int position = 0;

            foreach (List<object> list in procedureSteps)
            {
                int savedRequestId = 0;
                int innerProcedureId = 0;

                if (list[0].ToString().Equals("Request"))
                {
                    savedRequestId = int.Parse(list[1].ToString());
                }
                else
                {
                    innerProcedureId = int.Parse(list[1].ToString());
                }

                String sql = @"insert into test_procedure_steps (procedure_id, 
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
                                            values (" + procedureId + @", 
                                                    " + position + @", 
                                                    " + savedRequestId + @",
                                                    ?,                      /* TIME_DELAY */
                                                    '" + list[3] + @"',     /* VERIFY_EXECUTION */
                                                    '" + list[4] + @"',     /* VERIFY_CONDITION */
                                                    " + list[5] + @",       /* REPORT_TYPE */
                                                    " + list[6] + @",       /* REPORT_SUBTYPE */
                                                    " + list[8] + @",       /* DATA_FIELD_ID */
                                                    '" + list[9] + @"',     /* COMPARISON_OPERATION */
                                                    ?,                      /* VALUE_TO_COMPARE */
                                                    ?,                      /* VERIFY_INTERVAL_START */
                                                    ?)                      /* VERIFY_INTERVAL_END */";

                cmd.CommandText = sql;
                cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.Numeric));
                cmd.Parameters.Add(new OleDbParameter("@P2", OleDbType.Numeric));
                cmd.Parameters.Add(new OleDbParameter("@P3", OleDbType.Numeric));
                cmd.Parameters.Add(new OleDbParameter("@P4", OleDbType.Numeric));
                cmd.Parameters["@P1"].Value = list[2];
                cmd.Parameters["@P2"].Value = list[10];
                cmd.Parameters["@P3"].Value = list[12];
                cmd.Parameters["@P4"].Value = list[14];
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                position++;
            }

            return true;
        }

        /**
         * Este metodo insere os registros com chave = 0 nas tabelas saved_requests e test_procedures.
         * Mas isso eh feito caso estes registros nao existirem na base de dados.
         **/
        private void RefreshRecords()
        {
            String sqlRefresh = "";

            sqlRefresh = "select isnull(count(saved_request_id), 0) from saved_requests where saved_request_id = 0";
            int numRequestsWithZero = (int)DbInterface.ExecuteScalar(sqlRefresh);

            // Se for igual a zero, insere um registro com codigo zero
            if (numRequestsWithZero == 0)
            {
                sqlRefresh = @"insert into saved_requests (saved_request_id, description, apid, auto_ssc, auto_length, auto_crc, ssc, service_type, service_subtype, n_value, crc_error, raw_packet) 
                                values (0, 'no_saved_request', 0, 0, 0, 0, 0, 0, 0, 0, '0', ?)";
                byte[] packetInBytes = new byte[12];

                for (int i = 0; i < packetInBytes.Length; i++)
                {
                    packetInBytes[i] = 0;
                }

                cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.VarBinary, packetInBytes.GetLength(0)));
                cmd.Parameters["@P1"].Value = packetInBytes;

                cmd.CommandText = sqlRefresh;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            sqlRefresh = "select isnull(count(procedure_id), 0) from test_procedures where procedure_id = 0";
            int numProceduresWithZero = (int)DbInterface.ExecuteScalar(sqlRefresh);

            if (numProceduresWithZero == 0)
            {
                sqlRefresh = @"insert into test_procedures (procedure_id, description, purpose, estimated_duration, synchronize_obt, get_cpu_usage, run_in_loop, loop_iterations, send_mail, packets_sequence_control_options, executed)
                                values (0, 'no_test_procedure', 'none', 0, 0, 0, 0, 0, 0, 'none', 0)";

                cmd.CommandText = sqlRefresh;
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
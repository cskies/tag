/**
 * @file 	    DbInterface.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 08/11/2013 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbInterface
     * Esta classe implementa os metodos basicos de acesso ao banco de dados.
     * Deve ser herdada por qualquer classe que acesse o BD.
     * @attention Todos os metodos que nao envolvem transacoes sao estaticos
     **/
    class DbInterface
    {

        #region Atributos Internos

        private OleDbConnection persistentConnection = null;
        private OleDbTransaction transaction = null;

        #endregion

        #region Metodos Estaticos de Acesso ao Banco de Dados (sem transacao)

        /**
         * Apenas verifica se a conexao esta funcionando.
         **/
        public static bool TestConnection()
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                conn.Close();
                conn.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /** Executa uma consulta na base e retorna um DataTable. **/
        public static DataTable GetDataTable(String sql)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
            DataTable table = null;

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
                table = new DataTable();
                adap.Fill(table);

                conn.Close();
                cmd.Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                table = null;
            }

            conn.Dispose();

            return table;
        }

        /**
         * Gera e retorna um DataSet a partir de uma consulta SQL.
         **/
        public static DataSet GetDataSet(String sql)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
            DataSet dataSet = null;

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
                adap.Fill(dataSet);

                cmd.Dispose();
                conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                dataSet = null;
            }

            conn.Dispose();

            return dataSet;
        }

        /**
         * Executa um comando de modificacao (insert, update ou delete) 
         * e retorna se o comando foi bem-sucedido.
         * @attention O comando eh executado fora e independentemente
         * de qualquer transacao aberta pela instancia.
         **/
        public static bool ExecuteNonQuery(String sql)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
            bool executed = false;

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();

                executed = true;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            conn.Dispose();

            return executed;
        }

        /**
         * Executa uma query do tipo Scalar (que resulta em apenas um valor),
         * e devolve o resultado (int, bool, etc) em um Object. Fica a cargo
         * do chamador fazer o cast adequado.
         * @attention Pode retornar null.
         **/
        public static Object ExecuteScalar(String sql)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
            Object toReturn = null;

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                toReturn = cmd.ExecuteScalar();

                cmd.Dispose();
                conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                toReturn = null;
            }

            conn.Dispose();

            return toReturn;
        }

        #endregion

        #region Metodos para Operacoes no BD sob Transacoes

        /** Abre uma conexao e inicia uma transacao. **/
        public void BeginTransaction()
        {
            try
            {
                persistentConnection = new OleDbConnection();
                persistentConnection.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                persistentConnection.Open();

                transaction = persistentConnection.BeginTransaction();
            }
            catch (OleDbException e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        /** 
         * Executa um comando sql dentro de uma transacao aberta.
         * @attention O metodo BeginTransaction deve ter sido chamado antes.
         **/
        public bool ExecuteNonQueryInTransaction(String sql)
        {
            bool executed = false;

            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, persistentConnection);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                executed = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                executed = false;
            }

            return executed;
        }

        /** 
         * Executa um comando sql dentro de uma transacao aberta, recebendo o timeout como parametro para tempo limite.
         * @attention O metodo BeginTransaction deve ter sido chamado antes.
         **/
        public bool ExecuteNonQueryInTransactionWithTimeout(String sql, int timeout)
        {
            bool executed = false;

            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, persistentConnection);
                cmd.CommandTimeout = timeout;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                executed = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                executed = false;
            }

            return executed;
        }

        /** 
         * Executa o commit de todas as operacoes na base desde a ultima chamada
         * ao metodo OpenTransaction.
         **/
        public bool CommitTransaction()
        {
            bool toReturn = false;

            try
            {
                transaction.Commit();

                persistentConnection.Close();
                persistentConnection.Dispose();

                toReturn = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            return toReturn;
        }

        /** 
         * Executa o rollback de todas as operacoes na base desde a ultima chamada
         * ao metodo OpenTransaction.
         **/
        public void RollbackTransaction()
        {
            transaction.Rollback();

            persistentConnection.Close();
            persistentConnection.Dispose();
        }

        #endregion
    }
}

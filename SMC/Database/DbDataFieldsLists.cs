/**
 * @file 	    DbDataFieldsLists.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    08/09/2011
 * @note	    Modificado em 23/09/2011 por Ayres e Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbDataFieldsLists
     * Classe para o gerenciamento da persistencia de dados e estrutura de Data Fields.
     **/
    class DbDataFieldsLists : DbInterface
    {
        #region Atributos Internos

        private String key = "";
        private String description = "";        
        private Object[,] dataFieldList = null;

        #endregion

        #region Construtor

        public void DbDataFieldLists()
        {

        }

        #endregion

        #region Propriedades

        public String Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
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

        public Object[,] DataFieldList
        {
            get
            {
                return dataFieldList;
            }
            set
            {
                dataFieldList = value;
            }
        }
      
        #endregion
        
        #region Metodos Publicos

        /** Insere o relato e a estrutura. **/
        public bool Insert()
        {
            BeginTransaction();

            String sql = "insert into data_field_lists_header (list_id, list_description) values (" + key + ", '" + description + "')";

            if (!ExecuteNonQueryInTransaction(sql))
            {
                RollbackTransaction();
                return false;
            }

            if (!InsertDataFieldList())
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();            
        }

        /** Atualiza o relato. Neste caso, deleto toda a estrutura do relato e insiro a nova. **/
        public bool Update()
        {
            BeginTransaction();

            String sqlUpdate = "update data_field_lists_header set list_description = '" + description + "' where list_id = " + key;

            // Faz a atualizacao
            if (!ExecuteNonQueryInTransaction(sqlUpdate))
            {
                RollbackTransaction();
                return false;
            }

            if (!DeleteDataFieldList())
            {
                RollbackTransaction();
                return false;
            }

            // Insere a nova estrutura, caso houver
            if (!InsertDataFieldList())
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();
        }

        /** Usa o DeleteStructure e depois deleta o relato correspondente. **/
        public bool Delete()
        {
            BeginTransaction();

            // Primeiro deve-se deletar a estrutura, senao da erro de chave extrangeira.
            if (!DeleteDataFieldList())
            {
                RollbackTransaction();
                return false;
            }

            String sql = "delete from data_field_lists_header where list_id = " + key;

            // Deleta o relato
            if (!ExecuteNonQueryInTransaction(sql))
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();
        }                             
        
        /** Retorna um DataTable com todo o conteudo da tabela do BD. **/
        public DataTable GetTable()
        {
            String sql = "select list_id, list_description from data_field_lists_header";
            return DbInterface.GetDataTable(sql);
        }

        #endregion

        #region Metodos Privados

        /**
         * Este metodo insere uma lista de data fields no banco de dados.
         **/
        private bool InsertDataFieldList()
        {
            try
            {
                for (int i = 0; i < (dataFieldList.Length / 2); i++)
                {
                    String value = dataFieldList[i, 0].ToString();
                    String text = dataFieldList[i, 1].ToString();
                    String sql = "insert into data_field_lists (list_id, list_value, list_text) values (" + key + ", " + value + ", '" + text + "')";

                    if (!ExecuteNonQueryInTransaction(sql))
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /** Deleta toda a estrutura da gridList. **/
        private bool DeleteDataFieldList()
        {
            String sqldellist = "delete from data_field_lists where list_id = " + key;

            if (!ExecuteNonQueryInTransaction(sqldellist))
            {
                return false;
            }

            return true;
        }      
      
        #endregion
    }
}

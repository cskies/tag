/**
 * @file 	    DbSimpleTable.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
  * @date	    14/07/2009
 * @note	    Modificado em 27/08/2009 por Thiago.
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
     * @class DbSimpleTable
     * Classe para o gerenciamento da persistencia de dados dos cadastros simples.
     **/
    class DbSimpleTable : DbInterface
    {
        #region Atributos Internos

        private String tableName;
        private String key;
        private String description;
        private String keyField;
        private String descriptionField;

        #endregion

        #region Construtores

        public DbSimpleTable()
        {
        }

        public DbSimpleTable(String table, String key, String description)
        {
            tableName = table;
            keyField = key;
            descriptionField = description;
        }

        #endregion

        #region Propriedades

        public String TableName
        {
            get 
            { 
                return tableName;
            }
            set 
            { 
                tableName = value;
            }
        }

        public String KeyField
        {
            get 
            { 
                return keyField; 
            }
            set 
            { 
                keyField = value; 
            }
        }

        public String DescriptionField
        {
            get 
            { 
                return descriptionField; 
            }
            set 
            { 
                descriptionField = value; 
            }
        }

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

        #endregion

        #region Metodos Publicos

        /** Insere um registro na base e retorna se a operacao foi bem-sucedida. **/
        public bool Insert()
        {
            String sql = "insert into " + tableName + " (" + keyField + ", " + descriptionField + ") values (" + key + ",'" + description + "')";
            return DbInterface.ExecuteNonQuery(sql);
        }

        /** Atualiza um registro na base e retorna se a operacao foi bem-sucedida. **/
        public bool Update()
        {
            String sql = "update " + tableName + " set " + descriptionField + " = '" + description + "' where " + keyField + " = " + key;
            return DbInterface.ExecuteNonQuery(sql);
        }

        /** Remove um registro da base e retorna se a operacao foi bem-sucedida. **/
        public bool Delete()
        {
            String sql = "delete from " + tableName + " where " + keyField + " = " + key;
            return DbInterface.ExecuteNonQuery(sql);
        }

        /** Retorna um DataTable com todo o conteudo da tabela do BD. **/
        public DataTable GetTable()
        {
            String sql = "select " + keyField + ", " + descriptionField + " from " + tableName;
            return DbInterface.GetDataTable(sql);
        }

        #endregion
    }
}

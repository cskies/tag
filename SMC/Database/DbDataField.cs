/**
 * @file 	    DbDataField.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    22/07/2009
 * @note	    Modificado em 31/08/2009 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbDataField
     * Classe para o gerenciamento da persistencia de dados e/ou lista de valores de data fields PUS.
     **/
    class DbDataField : DbInterface
    {
        #region Atributos Internos

        private String fieldId = "";
        private String fieldName = "";
        private String numberOfBits = "";
        private String fieldType = "";
        private String tableName = "";
        private String listId = "";
        private bool variableLength = false;
        private Object[,] listOfValues = null;

        #endregion

        #region Construtor

        public DbDataField()
        {
        }

        #endregion

        #region Propriedades

        public String FieldId
        {
            get
            {
                return fieldId;
            }
            set
            {
                fieldId = value;
            }
        }

        public String FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;
            }
        }

        public String NumberOfBits
        {
            get
            {
                return numberOfBits;
            }
            set
            {
                numberOfBits = value;
            }
        }

        public String FieldType
        {
            get
            {
                return fieldType;
            }
            set
            {
                fieldType = value;
            }
        }

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

        public String ListId
        {
            get
            {
                return listId;
            }
            set
            {
                listId = value;
            }
        }

        public bool VariableLength
        {
            get
            {
                return variableLength;
            }
            set
            {
                variableLength = value;
            }
        }

        public Object[,] ListOfValues
        {
            get
            {
                return listOfValues;
            }
            set
            {
                listOfValues = value;
            }
        }

        #endregion

        #region Metodos Publicos

        /** Insere um Data Field. **/
        public bool Insert()
        {
            String sql = @"insert into data_fields (data_field_id, 
						                            data_field_name, 
						                            type_is_bool, 
						                            type_is_numeric, 
						                            type_is_raw_hex, 
						                            type_is_list, 
						                            type_is_table,
						                            list_id,
						                            table_name,
						                            number_of_bits,
                                                    variable_length) ";

            if (fieldType.Equals("Table"))
            {
                sql += "values (" + fieldId + ", '" + fieldName + "', 0, 0, 0, 0, '1', null, '" + tableName + "', " + numberOfBits + ", 0)";
            }

            if (fieldType.Equals("List"))
            {
                sql += "values (" + fieldId + ", '" + fieldName + "', 0, 0, 0, '1', 0, " + listId + ", null, " + numberOfBits + ", 0)";
            }

            if (fieldType.Equals("Raw Hex"))
            {
                sql += "values (" + fieldId + ", '" + fieldName + "', 0, 0, '1', 0, 0, null, null, " + numberOfBits + ", '" + variableLength + "')";
            }

            if (fieldType.Equals("Numeric"))
            {
                sql += "values (" + fieldId + ", '" + fieldName + "', 0, '1', 0, 0, 0, null, null, " + numberOfBits + ", 0)";
            }

            if (fieldType.Equals("Boolean"))
            {
                sql += "values (" + fieldId + ", '" + fieldName + "', '1', 0, 0, 0, 0, null, null, " + numberOfBits + ", 0)";
            }

            return ExecuteNonQuery(sql);
        }

        /** Altera os dados de um Data Field. **/
        public bool Update()
        {
            String sql = "update data_fields set data_field_name = '" + fieldName + "', ";

            if (fieldType.Equals("Table"))
            {
                sql += "type_is_bool = 0, type_is_numeric = 0, type_is_raw_hex = 0, type_is_list = 0, type_is_table = 1, list_id = null, table_name = '" + tableName + "', number_of_bits = " + numberOfBits + ", variable_length = 0";
            }

            if (fieldType.Equals("List"))
            {
                sql += "type_is_bool = 0, type_is_numeric = 0, type_is_raw_hex = 0, type_is_list = 1, type_is_table = 0, list_id = " + listId + ", table_name = null, number_of_bits = " + numberOfBits + ", variable_length = 0";
            }

            if (fieldType.Equals("Raw Hex"))
            {
                sql += "type_is_bool = 0, type_is_numeric = 0, type_is_raw_hex = 1, type_is_list = 0, type_is_table = 0, list_id = null, table_name = null, number_of_bits = " + numberOfBits + ", variable_length = '" + variableLength + "'";
            }

            if (fieldType.Equals("Numeric"))
            {
                sql += "type_is_bool = 0, type_is_numeric = 1, type_is_raw_hex = 0, type_is_list = 0, type_is_table = 0, list_id = null, table_name = null, number_of_bits = " + numberOfBits + ", variable_length = 0";
            }

            if (fieldType.Equals("Boolean"))
            {
                sql += "type_is_bool = 1, type_is_numeric = 0, type_is_raw_hex = 0, type_is_list = 0, type_is_table = 0, list_id = null, table_name = null, number_of_bits = " + numberOfBits + ", variable_length = 0";
            }

            sql += " where data_field_id = " + fieldId;

            return ExecuteNonQuery(sql);
        }

        /** Deleta um Data Field. **/
        public bool Delete()
        {
            String sql = "delete from data_fields where data_field_id = " + fieldId;

            return ExecuteNonQuery(sql);
        }

        /** Insere uma lista de valores editada na tela de Data Fields. **/
        public bool InsertListOfValues()
        {
            BeginTransaction();

            Object value = null;
            Object text = null;

            for (int line = 0; line < (listOfValues.Length / 2); line++)
            {
                value = listOfValues[line, 0];
                text = listOfValues[line, 1];

                String sql = "insert into data_field_lists (list_id, list_value, list_text) values (" + listId + ", " + value + ", '" + text + "')";

                if (!ExecuteNonQueryInTransaction(sql))
                {
                    RollbackTransaction();
                    return false;
                }
            }

            CommitTransaction();

            return true;
        }

        #endregion
    }
}
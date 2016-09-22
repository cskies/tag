/**
 * @file 	    DbMissionIdsWithStructure.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    23/10/2009
 * @note	    Modificado em 27/12/2013 por Fabricio.
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
     * @class DbReportStructure
     * Classe para o gerenciamento da persistencia de dados e estrutura de relatos do OBC.
     **/
    class DbMissionIdsWithStructure : DbInterface
    {
        #region Atributos Internos

        private String key = "";
        private String description = "";
        private String missionIdTable = ""; // nome da tabela do relato no bd
        private String missionIdKeyField = ""; // atributo key da tabela do relato
        private String missionIdDescriptionField = ""; // atributo description da tabela do relato
        private String tableStructure = ""; // nome da tabela da estrutura do relato.
        private String keyFieldStructure = ""; // atributo key da tabela da estrutura do relato.
        private String structureElementId = ""; // id do elemento que compoe a estrutura
        private Object[] structure = null;
        private int? numberOfColumms = 0;

        #endregion

        #region Construtores

        public DbMissionIdsWithStructure()
        {
        }

        public DbMissionIdsWithStructure(   String tableName,
                                            String keyTabBd,
                                            String descriptionTabBd,
                                            String tableStr,
                                            String keyStructure,
                                            String elementId)
        {
            missionIdTable = tableName;
            missionIdKeyField = keyTabBd;
            missionIdDescriptionField = descriptionTabBd;
            tableStructure = tableStr;
            keyFieldStructure = keyStructure;
            structureElementId = elementId;
        }

        public DbMissionIdsWithStructure(   String tableName,
                                            String keyTabBd,
                                            String descriptionTabBd,
                                            String tableStr,
                                            String keyStructure,
                                            String elementId,
                                            int numOfColumms)
        {
            missionIdTable = tableName;
            missionIdKeyField = keyTabBd;
            missionIdDescriptionField = descriptionTabBd;
            tableStructure = tableStr;
            keyFieldStructure = keyStructure;
            structureElementId = elementId;
            numberOfColumms = numOfColumms;
        }

        #endregion

        #region Propriedades

        public int? NumberOfColumms
        {
            get
            {
                return numberOfColumms;
            }
            set
            {
                numberOfColumms = value;
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

        public String MissionIdTable
        {
            get
            {
                return missionIdTable;
            }
            set
            {
                missionIdTable = value;
            }
        }

        public String MissionIdKeyField
        {
            get
            {
                return missionIdKeyField;
            }
            set
            {
                missionIdKeyField = value;
            }
        }

        public String MissionIdDescriptionField
        {
            get
            {
                return missionIdDescriptionField;
            }
            set
            {
                missionIdDescriptionField = value;
            }
        }

        public String TableStructure
        {
            get
            {
                return tableStructure;
            }
            set
            {
                tableStructure = value;
            }
        }

        public String KeyFieldStructure
        {
            get
            {
                return keyFieldStructure;
            }
            set
            {
                keyFieldStructure = value;
            }
        }

        public String StructureElementId
        {
            get
            {
                return structureElementId;
            }
            set
            {
                structureElementId = value;
            }
        }

        public Object[] Structure
        {
            get
            {
                return structure;
            }
            set
            {
                structure = value;
            }
        }

        #endregion

        #region Metodos Publicos

        /** Insere o relato e a estrutura. **/
        public bool Insert()
        {
            BeginTransaction();

            String sql = "insert into " + missionIdTable + " (" + missionIdKeyField + ", " + missionIdDescriptionField + ") values (" + key + ", '" + description + "')";

            if (!ExecuteNonQueryInTransaction(sql))
            {
                RollbackTransaction();
                return false;
            }

            if (structure != null)
            {
                if (!InsertStructure())
                {
                    return false;
                }
            }

            return CommitTransaction();
        }

        /** Atualiza o relato. Neste caso, deleto toda a estrutura do relato e insiro a nova. **/
        public bool Update()
        {
            BeginTransaction();

            if (!DeleteStructure())
            {
                RollbackTransaction();
                return false;
            }

            String sqlUpdate = "update " + missionIdTable + " set " + missionIdDescriptionField + " = '" + description + "' where " + missionIdKeyField + " = " + key;

            // Faz a atualizacao
            if (!ExecuteNonQueryInTransaction(sqlUpdate))
            {
                RollbackTransaction();
                return false;
            }

            // Insere a nova estrutura, caso houver
            if (structure != null)
            {
                if (!InsertStructure())
                {
                    RollbackTransaction();
                    return false;
                }
            }

            return CommitTransaction();
        }

        /** Usa o DeleteStructure e depois deleta o relato correspondente. **/
        public bool Delete()
        {
            BeginTransaction();

            // Primeiro deve-se deletar a estrutura, senao da erro de chave extrangeira.
            if (!DeleteStructure())
            {
                RollbackTransaction();
                return false;
            }

            String sql = "delete from " + missionIdTable + " where " + missionIdKeyField + " = " + key;

            // Deleta o relato
            if (!ExecuteNonQueryInTransaction(sql))
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();
        }

        /** Deleta toda a estrutura de um relato. **/
        public bool DeleteStructure()
        {
            String sqlDelStr = "delete from " + tableStructure + " where " + keyFieldStructure + " = " + key;

            // Deleta a estrutura
            if (!ExecuteNonQueryInTransaction(sqlDelStr))
            {
                return false;
            }

            return true;
        }

        /** Retorna um DataTable com todo o conteudo da tabela do BD. **/
        public DataTable GetTable()
        {
            String sql = "select " + missionIdKeyField + ", " + missionIdDescriptionField + " from " + missionIdTable;
            return DbInterface.GetDataTable(sql);
        }

        /*retorna um DataTable com todo o conteudo da tabela do BD tratando pelo nome da tela*/
        public DataTable GetTableByScreenName(String screenName)
        {
            String sql = null;
            if (screenName.Equals("Report Definitions"))
            {
                sql = @"select *, 
                    convert(varchar, isnull((select sum(case data_type when 'int8' then 1 when 'boolean (8 bits)'  
                        then 1 when 'int16' then 2 when 'int32' then 4 when 'int64' 
		                then 8 when 'double' then 8 end) 
			                from report_definition_structure a 
			                    inner join parameters b on a.parameter_id = b.parameter_id  
                            where a.structure_id = x.structure_id), 0)) + ' bytes'  as total_number_of_bytes
                                from report_definitions x";
            }
            else
            {
                sql = "select * from " + missionIdTable;
            }
            return DbInterface.GetDataTable(sql);
        }

        #endregion

        #region Metodos Privados

        /** Insere a estrutura na tabela de estruturas do relato correspondente, item a item. **/
        private bool InsertStructure()
        {
            Object dataFieldId = null;

            // Percorrer a estrutura e inserir cada elemento na tabela
            for (int i = 0; i < structure.Length; i++)
            {
                dataFieldId = structure[i];

                String sqlStructure = "insert into " + tableStructure + " (" + keyFieldStructure + ", " + structureElementId + ", position) values(" + key + ", " + dataFieldId + ", " + (i + 1) + ")";

                if (!ExecuteNonQueryInTransaction(sqlStructure))
                {
                    RollbackTransaction();
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}

/**
 * @file 	    DbSubtype.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    14/07/2009
 * @note	    Modificado em 02/06/2016 por Fabricio.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbSubtype
     * Classe para o gerenciamento da persistencia de dados e estrutura de subtipos PUS.
     **/
    class DbSubtype : DbInterface
    {
        #region Atributos Internos

        private String serviceType = "";
        private String serviceSubtype = "";
        private String description = "";
        private bool isRequest = false;
        private bool allowRepetition = false;
        private String sameStructureAs = "";
        private Object[,] subtypeStructure = null;

        #endregion

        #region Construtor

        public DbSubtype()
        {
        }

        #endregion

        #region Propriedades

        public String ServiceType
        {
            get
            {
                return serviceType;
            }
            set
            {
                serviceType = value;
            }
        }

        public String ServiceSubtype
        {
            get
            {
                return serviceSubtype;
            }
            set
            {
                serviceSubtype = value;
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

        public bool IsRequest
        {
            get
            {
                return isRequest;
            }
            set
            {
                isRequest = value;
            }
        }

        public bool AllowRepetition
        {
            get
            {
                return allowRepetition;
            }
            set
            {
                allowRepetition = value;
            }
        }

        public String SameStructureAs
        {
            get
            {
                return sameStructureAs;
            }
            set
            {
                sameStructureAs = value;
            }
        }

        public Object[,] SubtypeStructure
        {
            get
            {
                return subtypeStructure;
            }
            set
            {
                subtypeStructure = value;
            }
        }

        #endregion

        #region Metodos Publicos

        /** 
         * Insere um subtipo e sua estrutura na base e retorna se a operacao foi bem-sucedida. 
         * @attention Todas as operacoes rodam em uma unica transacao.
         **/
        public bool Insert()
        {
            BeginTransaction();

            // Insere o subtype
            String sqlSubtypes = "insert into subtypes (service_type, service_subtype, description, is_request, allow_repetition) " +
                                 "values(" + serviceType + ", " + serviceSubtype + ", '" + description + "', '" + isRequest + "', '" + allowRepetition + "')";

            if (!ExecuteNonQueryInTransaction(sqlSubtypes))
            {                
                RollbackTransaction();
                return false;
            }

            if (!InsertStructure())
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();
        }

        /** 
         * Atualiza um subtipo e sua estrutura na base e retorna se a operacao foi bem-sucedida.
         * O argumento updateLinkedSubtypes deve ser true apenas se o subtipo sendo salvo era base (sameSubtypeAs) de outros, e agora copia de um terceiro subtipo.
         * @attention Todas as operacoes rodam em uma unica transacao.
         **/
        public bool Update(bool updateLinkedSubtypes)
        {
            BeginTransaction();

            String sqlUpdate = "";

            // se updateLinkedSubtypes for true, a primeira coisa a fazer eh atualizar as estruturas que copiavam do subtipo que esta sendo salvo
            if (updateLinkedSubtypes == true)
            {
                sqlUpdate = "update subtype_structure set same_as_subtype = " + sameStructureAs + " where service_type = " + serviceType + " and same_as_subtype = " + serviceSubtype;

                if (!ExecuteNonQueryInTransaction(sqlUpdate))
                {
                    RollbackTransaction();
                    return false;
                }
            }

            // Deleta a estrutura do Subtype
            if (!DeleteStructure())
            {
                RollbackTransaction();
                return false;
            }

            // Atualiza o subtype
            sqlUpdate = "update subtypes set description = '" + description + "', " +
                                                "is_request = '" + isRequest + "', " +
                                                "allow_repetition = '" + allowRepetition + "' " +
                            "where service_type = " + serviceType + " and service_subtype = " + serviceSubtype;

            if (!ExecuteNonQueryInTransaction(sqlUpdate))
            {
                RollbackTransaction();
                return false;
            }

            if (!InsertStructure())
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();
        }

        /** 
         * Remove um subtipo e sua estrutura da base e retorna se a operacao foi bem-sucedida.
         * @attention Todas as operacoes rodam em uma unica transacao.
         **/
        public bool Delete()
        {
            BeginTransaction();

            // Deleta a estrutura
            if (!DeleteStructure())
            {
                RollbackTransaction();
                return false;
            }

            // Deleta o subtype
            String sqlDelSub = "delete from subtypes where service_type = " + serviceType + " and service_subtype = " + serviceSubtype;
            if (!ExecuteNonQueryInTransaction(sqlDelSub))
            {
                RollbackTransaction();
                return false;
            }

            return CommitTransaction();
        }

        #endregion

        #region Metodos Privados

        /** Insere os registros com a estrutura do subtipo no banco de dados */
        private bool InsertStructure()
        {
            //se o subtype herdar uma estrutura, insira aqui.
            if (!sameStructureAs.Equals(""))
            {
                String sqlSameStruc = "insert into subtype_structure " +
                                            "(service_type, service_subtype, data_field_id, position, same_as_subtype, read_only, default_value) " +
                                       "values(" + serviceType + ", " + serviceSubtype + ", 0, 0, " + sameStructureAs + ", 0, null)";

                if (!ExecuteNonQueryInTransaction(sqlSameStruc))
                {
                    // o rollback sera feito pelo chamador
                    return false;
                }

                sameStructureAs = "";
            }
            else if (subtypeStructure != null) // caso o subtype tenha uma estrutura propria, entra aqui.
            {
                Object dataFieldId = null;
                Object readOnly = null;
                Object defaultValue = null;

                for (int line = 0; line < (subtypeStructure.Length / 3); line++)
                {
                    int position = line; //pega a posicao do data field

                    dataFieldId = subtypeStructure[line, 0];
                    readOnly = subtypeStructure[line, 1];
                    defaultValue = subtypeStructure[line, 2];

                    String sqlStructure = "insert into subtype_structure " +
                                          "(service_type, service_subtype, data_field_id, position, same_as_subtype, read_only, default_value) " +
                                          "values(" + serviceType + ", " + serviceSubtype + ", " + dataFieldId + ", " + position + ", null, '" + readOnly + "', " + defaultValue + ")";

                    if (!ExecuteNonQueryInTransaction(sqlStructure))
                    {
                        // o rollback sera feito pelo chamador
                        return false;
                    }
                }
            }

            return true;
        }

        /** Deleta a estrutura do subtype, caso haja. */
        private bool DeleteStructure()
        {
            String sqlDelStr = "delete from subtype_structure where service_type = " + serviceType + " and service_subtype = " + serviceSubtype;
            return (ExecuteNonQueryInTransaction(sqlDelStr));
        }

        #endregion
    }
}


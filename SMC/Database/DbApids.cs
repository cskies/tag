/**
 * @file 	    DbApids.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    22/12/2011
 * @note	    Modificado em 22/12/2011 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    class DbApids : DbInterface
    {
        #region Atributos Internos

        private String apid = "";
        private String application_name = "";
        private String vcid = "";
        
        #endregion

        #region Construtor

        public DbApids()
        {
        }

        #endregion

        #region Propriedades

        public String Apid
        {
            get
            {
                return apid;
            }
            set
            {
                apid = value;
            }
        }

        public String Application_name
        {
            get
            {
                return application_name;
            }
            set
            {
                application_name = value;
            }
        }

        public String Vcid
        {
            get
            {
                return vcid;
            }
            set
            {
                vcid = value;
            }
        }

        #endregion

        #region Metodos Publicos

        /** Insere valores da tabela Apids **/
        public bool Insert()
        {
            String sqlApid = "insert into apids (apid, application_name, vcid)" +
                             "values('" + apid + "', '" + application_name + "', '" + vcid + "')";

            if (!ExecuteNonQuery(sqlApid))
            {
                return false;
            }

            return true;
        }

        /** Atualiza valores da tabela Mission Constants **/
        public bool Update()
        {
            String sqlUpdate = "update apids set application_name = '" + application_name + "', " +
                               "vcid = " + vcid +
                               "where apid = " + apid;

            if (!ExecuteNonQuery(sqlUpdate))
            {
                return false;
            }

            return true;
        }

        /** Remove valores da tabela Mission Constants **/
        public bool Delete()
        {
            String sqlDelSub = "delete from apids where apid = '" + apid + "' ";

            if (!ExecuteNonQuery(sqlDelSub))
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}

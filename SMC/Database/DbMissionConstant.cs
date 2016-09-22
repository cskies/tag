/**
 * @file 	    DbMissionConstant.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Michel Andrade
 * @date	    14/07/2009
 * @note	    Modificado em 19/10/2009 por Michel com acompanhamento de Thiago.
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
     * @class DbMissionConstant
     * Classe para o gerenciamento da persistencia de dados da form Mission Constant.
     **/
    class DbMissionConstant : DbInterface
    {
        #region Atributos Internos

        private String missionConstant = "";
        private String constantDescription = "";
        private String definedIn = "";
        private String constantValue = "";
        private bool   isFlightSWConstant = false;

        #endregion

        #region Construtor

        public DbMissionConstant()
        {
        }

        #endregion

        #region Propriedades

        public String MissionConstant
        {
            get
            {
                return missionConstant;
            }
            set
            {
                missionConstant = value;
            }
        }

        public String ConstantDescription
        {
            get
            {
                return constantDescription;
            }
            set
            {
                constantDescription = value;
            }
        }

        public String DefinedIn
        {
            get
            {
                return definedIn;
            }
            set
            {
                definedIn = value;
            }
        }

        public String ConstantValue
        {
            get
            {
                return constantValue;
            }
            set
            {
                constantValue = value;
            }
        }

        public bool IsFlightSWConstant
        {
            get
            {
                return isFlightSWConstant;
            }
            set
            {
                isFlightSWConstant = value;
            }
        }
        
        #endregion

        #region Metodos Estaticos

        public static DataTable GetFswConstants()
        {
            String sql = "select * from mission_constants where is_flight_sw_constant = 1 order by mission_constant";

            return (DbInterface.GetDataTable(sql));
        }

        #endregion

        #region Metodos Publicos

        /** Insere valores da tabela Mission Constants **/
        public bool Insert()
        {
            String sqlMissionConstants = "insert into mission_constants (mission_constant, constant_description, defined_in, constant_value, is_flight_sw_constant)" +
                                         "values('" + missionConstant + "', '" + constantDescription + "', '" + definedIn + "', '" + constantValue + "', '" + isFlightSWConstant + "')";

            if (!ExecuteNonQuery(sqlMissionConstants))
            {                
                return false;
            } 

            return true;
        }

        /** Atualiza valores da tabela Mission Constants **/
        public bool Update()
        {          
            String sqlUpdate = "update mission_constants set constant_description = '" + constantDescription + "', " +
                                                            "defined_in = '" + definedIn + "', " +
                                                            "constant_value = '" + constantValue + "', " +
                                                            "is_flight_sw_constant = '" + isFlightSWConstant + "' " +
                               "where mission_constant = '" + missionConstant + "' ";         

            if (!ExecuteNonQuery(sqlUpdate))
            {
                return false;
            }

            return true;
        }

        /** Remove valores da tabela Mission Constants **/
        public bool Delete()
        {
            String sqlDelSub = "delete from mission_constants where mission_constant = '" + missionConstant + "' ";

            if (!ExecuteNonQuery(sqlDelSub))
            {
                return false;
            }  

            return true;
        }
        
        #endregion
    }
}

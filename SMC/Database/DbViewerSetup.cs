
/**
 * @file 	    FrmApids.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    23/10/2013
 * @note	    Modificado em 23/10/2013 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    class DbViewerSetup : DbInterface
    {
        #region Atributos Internos

        private int view_id;        
        private int parameter_id;
        private String view_description = "";       
        private int row_index;       
        private int coll_index;        
        private String highlight = "";
        
        #endregion

        #region Construtor

        public DbViewerSetup()
        {
        }

        #endregion

        #region Propriedades

        public int View_id
        {
            get { return view_id; }
            set { view_id = value; }
        }

        public int Parameter_id
        {
            get { return parameter_id; }
            set { parameter_id = value; }
        }

        public String View_description
        {
            get { return view_description; }
            set { view_description = value; }
        }

        public int Row_index
        {
            get { return row_index; }
            set { row_index = value; }
        }

        public int Coll_index
        {
            get { return coll_index; }
            set { coll_index = value; }
        }

        public String Highlight
        {
            get { return highlight; }
            set { highlight = value; }
        }

        #endregion

        #region Querys

        public bool InsertView()
        {
            String sqlInsertView = "insert into hk_parameters_views (view_id, view_description)" +
                             "values('" + view_id + "', '" + view_description + "')";

            if (!ExecuteNonQuery(sqlInsertView))
            {
                return false;
            }

            return true;
        }

        public bool InsertSetup()
        {
            String sqlInsertSetup = "insert into hk_parameters_setup (view_id, parameter_id, row_index, coll_index, highlight)" +
                             "values('" + view_id + "', '" + parameter_id + "', '" + row_index + "', '" + coll_index + "', '" + highlight + "')";

            if (!ExecuteNonQuery(sqlInsertSetup))
            {
                return false;
            }

            return true;
        }

        public int ReturnLastId()
        {
            String sql = "SELECT TOP 1 view_id from hk_parameters_views ORDER BY view_id DESC";

            int? result = (int?)ExecuteScalar(sql);
            if (result != null)
            {
                return (int)result;
            }
            else
            {
                return 0;
            }

        }

        public int ReturnParameterId(String parameterDescription)
        {
            String sql = "SELECT TOP 1 parameter_id from parameters where parameter_description = '" + parameterDescription + "'";

            return (int)ExecuteScalar(sql);
        }

        public DataTable ReturnHkView()
        {
            return GetDataTable("SELECT * FROM hk_parameters_views");
        }

        public DataTable ReturnHkSetup(int viewId)
        {
            return GetDataTable("SELECT * FROM hk_parameters_setup where view_id = " + viewId);
        }

        public DataTable ReturnMaxColRow(int viewId)
        {
            return GetDataTable("SELECT MAX(coll_index), MAX(row_index) from hk_parameters_setup where view_id = " + viewId);
        }

        public String ReturnCmbDescById(int parameterId)
        {
            String sql = @"select parameter_description + 
                           ' [' + data_type + ']' as description from parameters where parameter_id = " + parameterId + " order by parameter_id";
            return (String)ExecuteScalar(sql);
        }

        public DataTable ReturnCmbDesc()
        {
            return GetDataTable(@"select parameter_description + 
                           ' [' + data_type + ']' as description from parameters where parameter_id > 0 order by parameter_id");
        }

        public void DeleteView(int viewId)
        {
            ExecuteScalar("delete from hk_parameters_setup where view_id = " + viewId);
            ExecuteScalar("delete from hk_parameters_views where view_id = " + viewId);
        }

        public void DeleteSetup(int viewId)
        {
            ExecuteScalar("delete from hk_parameters_setup where view_id = " + viewId);
        }

        public DataTable ReturnViewWithDescr()
        {
            return GetDataTable(@"select '[' + CAST(view_id as varchar) + '] ' + view_description as ViewDescription from hk_parameters_views order by view_id");
        }

        public int ReturnViewId(String viewDescription)
        {
            String sql = "SELECT TOP 1 view_id from hk_parameters_views where view_description = '" + viewDescription + "'";

            return (int)ExecuteScalar(sql);
        }

        public DataTable ReturnParametersByViewId(int viewId)
        {
            return GetDataTable(@"select h.parameter_id, p.parameter_description, h.coll_index, h.row_index, h.highlight from hk_parameters_setup as h inner join parameters as p on h.parameter_id = p.parameter_id 
                                where view_id = " + viewId + "ORDER BY  h.row_index");
        }

        public String ReturnParameterDescById(int parameterId)
        {
            String sql = "select parameter_description from parameters where parameter_id = " + parameterId;
            return (String)ExecuteScalar(sql);
        }

        public static DataTable GetSessionList(String connectionType)
        {
            String sqlCaracteres = "select isnull(MAX(session_id), 0) from sessions";
            String caracteres = Convert.ToString(DbInterface.ExecuteScalar(sqlCaracteres));
            Int32 nOfCaracteres = caracteres.Length;

            // f_zero eh uma user-defined function (user dbo) na base Simulador_UTMC
            String sql = @"select dbo.f_zero(session_id, " + nOfCaracteres.ToString() + @" ) + ' [from ' + 
                                  convert(varchar, start_time, 103) + ' ' + 
                                  convert(varchar, start_time, 108) + ' to ' + 
                                  convert(varchar, end_time, 103) + ' ' + 
                                  convert(varchar, end_time, 108) + ', through ' +
                                  connection_type + 
                                  case when ((isnull(swapl_version, 0) = 0) and (isnull(swapl_release, 0) = 0) and (isnull(swapl_patch, 0) = 0)) then 
										']' 
								  else 
										', SW APL Version ' + isnull(convert(varchar, swapl_version), '') + '.' + isnull(convert(varchar, swapl_release), '') + '.' + isnull(convert(varchar, swapl_patch), '') + ']' 
								  end 
                           from sessions where convert(date,end_time) = (select convert(date,GETDATE())) order by session_id desc";

            return (DbInterface.GetDataTable(sql));
        }

        public DataTable ReturnGridDataFieldValue(int sessionId)
        {
            String sql = @"select parameter_id, data_field_value from packets_log_data_field 
                           where session_id = " + sessionId + " and unique_log_id = (select MAX(unique_log_id) from packets_log_data_field where session_id = " + sessionId + ")";

            return (DbInterface.GetDataTable(sql));
        }

        #endregion
    }
}

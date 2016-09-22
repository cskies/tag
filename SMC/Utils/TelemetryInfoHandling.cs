using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inpe.Subord.Comav.Egse.Smc.Utils
{
    /*
     * classes com métodos estáticos utilitarios para capturar a informacao
     * da ultima telemetria recebida para entao fazer o sincronismo 
     * com o relogio do OBC
     * Classe eh utilizada pela frmExecutionTimeout
     */
    static class TelemetryInfoHandling
    {
        #region atributos

        private static string lastLogTime = "";
        private static FrmSessionsLog frmSessionsLog = null;

        #endregion

        #region Metodos publicos estaticos

        /* no momento da chamada desse metodo 
         * o chamador precisara ter a informacao do deltaT 
         * que foi guardado na base
         */
        public static string GetLastLogTime()
        {
            frmSessionsLog = new FrmSessionsLog();

            /* str p/ capturar a ultima tm
             * e assim sync com EGSE
             */
            string sql = @"select top 1 log_time from packets_log
                            where is_request = 0
                            order by session_id desc";

            DataTable tblTm = DbInterface.GetDataTable(sql);

            /* testar chamando frmsessionsLog.LoadLogGrid
             * antes.. pra popular a var
             */
            frmSessionsLog.LoadLogGridAppData();
            string testeTempo = frmSessionsLog.LogTime;
            return testeTempo;
        }

        #endregion
    }
}
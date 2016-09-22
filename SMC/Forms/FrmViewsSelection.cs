/**
 * @file 	    FrmViewersSelection.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    30/10/2013
 * @note	    Modificado em 18/02/2013 por Ayres.
 **/

using Inpe.Subord.Comav.Egse.Smc.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.TestSession;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmViewsSelection : DockContent

    {
        public FrmViewsSelection(MdiMain mdi)
        {
            InitializeComponent();
            mdiMain = mdi;
        }

        private DbViewerSetup dbViewerSetup = new DbViewerSetup();
        private MdiMain mdiMain;

        private void FrmViewsSelection_Load(object sender, EventArgs e)
        {
            gridViews.Columns.Add("Select View", "Select View");
            gridViews.DataSource = dbViewerSetup.ReturnViewWithDescr();
            for (int i = 0; i < gridViews.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chkGrid = new DataGridViewCheckBoxCell(false);
                chkGrid.EditingCellValueChanged = true;
                gridViews[0, i] = chkGrid;
            }
            gridViews.Refresh();

            DataTable table = DbViewerSetup.GetSessionList("all");
            table.Columns[0].ColumnName = "Active Sessions";
            gridSessions.DataSource = table;
            gridSessions.Refresh();
        }

        private void gridViews_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void gridViews_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            List<String> listViews = new List<String>();
            int nOfRows = gridViews.Rows.Count;
            bool chkChecked;

            for (int i = 0; i < nOfRows; i++)
            {
                if (gridViews[0, i].Value != null)
                {
                    chkChecked = (bool)gridViews[0, i].Value;
                }
                else
                {
                    chkChecked = false;
                }
                
                if (chkChecked)
                {
                    listViews.Add((String)gridViews[1, i].Value);
                }
            }

            String allSession = gridSessions.CurrentCell.Value.ToString();
            String sessionId = "";
            int temp = 0;
            bool num = true;
            for (int i = 0; i < allSession.Length; i++)
            {
                try
                {
                    if (num)
                    {
                        temp = Convert.ToInt32(allSession[i].ToString());
                        sessionId = sessionId + temp.ToString();
                    }
                }
                catch
                {
                    num = false;
                }
            }

            FrmViewer frmviewer = new FrmViewer(listViews, Convert.ToInt32(sessionId));
            frmviewer.MdiParent = mdiMain;
            frmviewer.Show(mdiMain.DockPanel);
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

/**
 * @file 	    FrmViewersSelection.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    30/10/2013
 * @note	    Modificado em 02/04/2014 por Ayres.
 * @note	    Modificado em 28/05/2015 por Thiago.
 **/


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
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.TestSession;
using System.Threading;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmViewer : DockContent
    {
        #region Variaveis
        private List<String> listViewsToTab;
        private DbViewerSetup dbViewerSetup = new DbViewerSetup();
        private SessionLog session = new SessionLog();
        private int sessionId;
        private List<DataGridView> gridsList = new List<DataGridView>();  //lista de grids
        private DataTable dtTableOfParameters = new DataTable();
        private List<DataTable> dtParametersList = new List<DataTable>();
        private bool clearByUser = false;
        #endregion

        #region Construtor

        public FrmViewer(List<String> listViews, int sessionId)
        {
            InitializeComponent();
            listViewsToTab = listViews;
            this.sessionId = sessionId;
        }

        #endregion        

        #region Metodos Privados

        private void VerifyAndFillGrids()
        {
            dtTableOfParameters = dbViewerSetup.ReturnGridDataFieldValue(Convert.ToInt32(txtSession.Text)); //verificar como fazer quando tiver o relogio
            DataTable dtFinalGridTable = new DataTable ();
            int indexOfFinalGridRow = 0;
            dtFinalGridTable.Columns.Add("id");
            dtFinalGridTable.Columns.Add("data_field_description");

            //monta uma terceira tabela so com o parameter id e descr especificas do grid
            for (int l = 0; l < dtParametersList.Count; l++)
            {
                for (int i = 0; i < dtParametersList[l].Rows.Count; i++)
                {
                    for (int j = 0; j < dtTableOfParameters.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(dtParametersList[l].Rows[i][0]) == Convert.ToInt32(dtTableOfParameters.Rows[j][0]))
                        {
                            dtFinalGridTable.Rows.Add();
                            indexOfFinalGridRow = dtFinalGridTable.Rows.Count - 1;
                            dtFinalGridTable.Rows[indexOfFinalGridRow][0] = dtTableOfParameters.Rows[j][0];
                            dtFinalGridTable.Rows[indexOfFinalGridRow][1] = dtTableOfParameters.Rows[j][1];
                        }
                    }
                }
            }

            for (int l = 0; l < gridsList.Count; l++)
            {
                //adiciona o grid ao controle da tab
                tabControl1.TabPages[l].Controls.Add(gridsList[l]);
                gridsList[l].Dock = System.Windows.Forms.DockStyle.Fill;
            }

            try
            {
                for (int l = 0; l < gridsList.Count; l++)
                {
                    indexOfGrid = l;
                    //assinatura do evento para o grid l
                    gridsList[l].CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick);

                    for (int j = 0; j < gridsList[l].Rows.Count; j++)
                    {
                        for (int k = 0; k < gridsList[l].Columns.Count; k++)
                        {
                            if (gridsList[l][k, j].Tag != null)
                            {
                                if (j % 2 != 0)
                                {
                                    for (int i = 0; i < dtFinalGridTable.Rows.Count; i++)
                                    {
                                        if (Convert.ToInt32(dtFinalGridTable.Rows[i][0]) == Convert.ToInt32(gridsList[l][k, j].Tag.ToString()))
                                        {
                                            String temp;
                                            bool isHighlight = true;
                                            String highLight = gridsList[l][k, j - 1].Tag.ToString();

                                            if (highLight == "autoClearhighlight" || highLight == "clearByUser")
                                            {
                                                isHighlight = true;
                                            }
                                            else
                                            {
                                                isHighlight = false;
                                            }

                                            try
                                            {
                                                temp = gridsList[l][k, j].Value.ToString();
                                            }
                                            catch
                                            {
                                                gridsList[l][k, j].Value = "";
                                            }

                                            if (gridsList[l][k, j].Value.ToString() != dtFinalGridTable.Rows[i][1].ToString())
                                            {
                                                gridsList[l][k, j].Value = dtFinalGridTable.Rows[i][1].ToString();

                                                if (isHighlight)
                                                {
                                                    gridsList[l][k, j].Style.BackColor = Color.Yellow;
                                                }
                                            }
                                            else
                                            {
                                                if (isHighlight && !clearByUser)
                                                {
                                                    gridsList[l][k, j].Style.BackColor = Color.White;
                                                }
                                            }

                                            //somente atualiza o grid da lista que ja esta presente na tab
                                            tabControl1.TabPages[l].Controls[0].Refresh();
                                            tabControl1.TabPages[l].Refresh();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }            
        }

        #endregion

        #region Eventos da Interface

        private int indexOfGrid = 0;

        //evento chamado pelos grids dinamicos
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String highLight = gridsList[tabControl1.SelectedIndex][e.ColumnIndex, e.RowIndex - 1].Tag.ToString();

                if (highLight == "clearByUser")
                {                    
                    gridsList[tabControl1.SelectedIndex][e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                }
                tabControl1.TabPages[indexOfGrid].Controls[0].Refresh();
                tabControl1.TabPages[indexOfGrid].Refresh();
            }
            catch
            {
            }
           
        }

        private void FrmViewer_Load(object sender, EventArgs e)
        {
            try
            {

                txtSession.Text = sessionId.ToString();

                for (int i = 0; i < listViewsToTab.Count; i++)
                {
                    int startIndex = listViewsToTab[i].IndexOf("]");
                    startIndex = startIndex + 2;
                    String viewDescr = listViewsToTab[i].Substring(startIndex, (listViewsToTab[i].Length - startIndex));
                    int viewId = dbViewerSetup.ReturnViewId(viewDescr);
                    DataTable dtParameters = dbViewerSetup.ReturnParametersByViewId(viewId);
                    DataTable dtMaxColRow = dbViewerSetup.ReturnMaxColRow(viewId);
                    tabControl1.TabPages.Add(listViewsToTab[i]);
                    DataGridView dtgv = new DataGridView();

                    dtgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dtgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                    dtgv.AllowUserToAddRows = false;
                    dtgv.AllowUserToDeleteRows = false;
                    dtgv.AutoSize = true;
                    dtgv.RowHeadersVisible = false;
                    dtgv.ColumnHeadersVisible = false;

                    dtgv.ColumnCount = (Convert.ToInt16(dtMaxColRow.Rows[0][0].ToString()) + 1);
                    dtgv.RowCount = (Convert.ToInt16(dtMaxColRow.Rows[0][1].ToString()) + 1);

                    int increment = 0;
                    int dtgvNOfRows = dtgv.Rows.Count;
                    int column = 0;
                    int row = 0;

                    //adiciona o cabeçalho
                    for (int j = 0; j < dtParameters.Rows.Count; j++)
                    {
                        column = Convert.ToInt16(dtParameters.Rows[j][2].ToString());
                        row = Convert.ToInt16(dtParameters.Rows[j][3].ToString());

                        dtgv[column, row].Value = dtParameters.Rows[j][1].ToString();
                        dtgv[column, row].Tag = dtParameters.Rows[j][4].ToString();
                    }

                    //incrementa a linha dos dados na posição correta
                    for (int k = 0; k < dtgvNOfRows; k++)
                    {
                        dtgv.Rows.Insert((k + increment) + 1);
                        increment++;
                    }

                    //adiciona na tag das celulas dos dados o parameter_id
                    for (int j = 0; j < dtParameters.Rows.Count; j++)
                    {
                        column = Convert.ToInt16(dtParameters.Rows[j][2].ToString());
                        row = Convert.ToInt16(dtParameters.Rows[j][3].ToString());

                        //cabecalho fica sempre em rows pares e conteudo em rows impares
                        dtgv[column, (2 * row + 1)].Tag = dtParameters.Rows[j][0].ToString();
                    }

                    for (int k = 0; k < dtgv.Rows.Count; k++)
                    {
                        if (k % 2 == 0)
                        {
                            dtgv.Rows[k].DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                    }

                    gridsList.Add(dtgv);
                    //pede a lista dos parameters com os dados do bd

                    dtParametersList.Add(dtParameters);


                    VerifyAndFillGrids();
                    //dtgv.Dock = System.Windows.Forms.DockStyle.Fill;
                }

                numSeconds.Enabled = true;
                timer.Enabled = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private void timer_Tick(object sender, EventArgs e)
        {
            VerifyAndFillGrids();
        }        

        private void numSeconds_ValueChanged(object sender, EventArgs e)
        {
            timer.Interval = 1000 * (Convert.ToInt32(numSeconds.Value));
        }

        #endregion
    }
}

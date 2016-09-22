/**
 * @file 	    FrmQueries.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    19/07/2009
 * @note	    Modificado em 21/10/2009 por Fabricio.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmQueries
     * Formulario que permite ao usuario do SMC executar consultas diretamente
     * na base, salva-las, carregar consultas salvas e exportar os resultados.
     * Nao permite operacoes de insert, update ou delete.
     **/
    public partial class FrmQueries : DockContent
    {
        public FrmQueries()
        {
            InitializeComponent();
        }

        private void FrmQueries_Load(object sender, EventArgs e)
        {
            ResetGrid();
        }

        private void FrmQueries_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                btRun_Click(this, new EventArgs());
            }
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            String query = "";

            if (!txtQuery.SelectedText.Trim().Equals(""))
            {
                query = txtQuery.SelectedText;
            }
            else
            {
                query = txtQuery.Text;
            }

            if (query.ToUpper().Contains("INSERT ") ||
                query.ToUpper().Contains("UPDATE ") ||
                query.ToUpper().Contains("DELETE "))
            {
                MessageBox.Show("Insert, Update and Delete SQL commands are not allowed from this form!",
                                "Database query error", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Exclamation);
                return;
            }

            gridQuery.Columns.Clear();

            DataTable table = DbInterface.GetDataTable(query);

            // Se houver algum erro, o GetDataTable ira mostrar uma mensagem
            if (table == null)
            {
                ResetGrid();
            }
            else
            {
                try
                {
                    gridQuery.DataSource = table;
                }
                catch
                {
                    // Nao faco nada; apenas evito que o programa trave
                    // quando a query contem uma coluna Binary ou VarBinary,
                    // que nao podem ser exibidas no grid.
                }

                gridQuery.Refresh();
            }
        }

        private void ResetGrid()
        {
            gridQuery.DataSource = null;
            gridQuery.Rows.Clear();

            gridQuery.Columns.Add("default", "Run a valid query to fill this area");
            gridQuery.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridQuery.RowHeadersWidth = 20;
            gridQuery.Refresh();

            txtQuery.Focus();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            txtQuery.Text = "";
            ResetGrid();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Properties.Settings.Default.sql_queries_default_path;
            openFileDialog.Filter = "SQL Query Files|*.sql|All Files|*.*";
            openFileDialog.FileName = "*.sql";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader queryFile = File.OpenText(openFileDialog.FileName);
                txtQuery.Text = queryFile.ReadToEnd();
                queryFile.Close();
                queryFile.Dispose();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = Properties.Settings.Default.sql_queries_default_path;
            saveFileDialog.Filter = "SQL Query Files|*.sql|All Files|*.*";
            saveFileDialog.FileName = "*.sql";
            saveFileDialog.FilterIndex = 0;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog.FileName);
                writer.Write(txtQuery.Text);
                writer.Close();
                writer.Dispose();

                MessageBox.Show("SQL query saved successfuly!",
                                "SQL query saved" ,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }

        private void gridQuery_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("It was not possible to fill the grid!\n\n" +
                            "Your query probably returned columns of type Binary or VarBinary, which are not supported.",
                            "Error trying to show your results",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

            ResetGrid();
        }

        private void FrmQueries_Activated(object sender, EventArgs e)
        {
            txtQuery.Focus();
        }

        private void FrmQueries_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(755, 352);
            }
        }
    }
}

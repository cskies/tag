/**
 * @file 	    FrmViewersSetup.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    23/10/2013
 * @note	    Modificado em 23/10/2013 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmViewerSetup
     * Formulario de configuracao de visualizacoes de telemetria.
     **/
    public partial class FrmViewerSetup : DockContent
    {
        #region Atributos Internos

        private enum Mode {browsing, inserting, editing};
        private Mode currentMode = Mode.browsing;
        private int gridSetupRow = 10;
        private int gridSetupColumn = 10;

        //variaveis utilizadas no controle da linha clicada da função botão direito
        private int currentMouseOverRow;
        private int currentMouseOverColumn;
        private int currentGridRowClick;
        private int currentGridColumnClick;

        private ContextMenu rightClickMenu;
        private bool simpleHighChk = true;
        private bool autoClearHighChk = false;
        private bool clearByUserHighChk = false;

        private DbViewerSetup dbViewerSetup = new DbViewerSetup();

        #endregion

        #region Construtor

        public FrmViewerSetup()
        {
            InitializeComponent();
            ChangeMode(Mode.browsing);
        }

        #endregion

        #region Tratamento de Eventos da Interface

        private void gridDatabase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangeMode(Mode.editing);
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.inserting);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.editing);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the Subtype " + gridDatabase.CurrentRow.Cells[0].Value.ToString() + ", '" + gridDatabase.CurrentRow.Cells[1].Value.ToString() + "' ?",
                                "Please Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, 
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            dbViewerSetup.DeleteView(Convert.ToInt32(gridDatabase.CurrentRow.Cells[0].Value));
            ChangeMode(Mode.browsing);

            if (CanDelete())
            {
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this edition?",
                                "Cancel Edition",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            ChangeMode(Mode.browsing);
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            // forca que edicoes pendentes sejam finalizadas
            gridSetup.EndEdit();

            if (!ValidateData())
            {
                return;
            }



            dbViewerSetup.View_id = Convert.ToInt16(txtSubtypeId.Text);
            dbViewerSetup.View_description = txtDescription.Text;

            if (currentMode == Mode.inserting)
            {
                dbViewerSetup.InsertView();
            }
            else if (currentMode == Mode.editing)
            {
                dbViewerSetup.DeleteView(dbViewerSetup.View_id);
                dbViewerSetup.InsertView();
            }

            for (int i = 0; i < numCollumns.Value; i++)
            {
                for (int j = 0; j < numRows.Value; j++)
                {
                    if (gridSetup[i, j].Value != null)
                    {
                        String highlight = "";
                        String parameterDesc = gridSetup[i, j].Value.ToString();
                        dbViewerSetup.Parameter_id = dbViewerSetup.ReturnParameterId(parameterDesc.Substring(0, (parameterDesc.IndexOf("[") - 1)));
                        dbViewerSetup.Coll_index = i;
                        dbViewerSetup.Row_index = j;
                        if (gridSetup[i, j].Tag != null)
                        {
                            highlight = (String)gridSetup[i, j].Tag;
                        }
                        else
                        {
                            highlight = "noHighlight";
                        }

                        dbViewerSetup.Highlight = highlight;

                        dbViewerSetup.InsertSetup();
                    }

                }
            }

            if (currentMode == Mode.inserting)
            {
                // Como eh insercao, continua no mesmo modo
                ChangeMode(Mode.inserting);
            }
            else
            {
                // Volta ao modo browsing
                ChangeMode(Mode.browsing);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

        /**
         * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
         * para permitir a operacao da tela pelo usuario.
         **/
        private void FrmViewerSetup_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid antes de chamar o Edit.
            if ((gridDatabase.Focused) && (e.KeyCode == Keys.Enter))
            {
                e.Handled = true;
            }

            if (currentMode == Mode.browsing)
            {
                if ((e.KeyCode == Keys.Insert) && (btNew.Enabled))
                {
                    btNew_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.Delete) && (btDelete.Enabled))
                {
                    btDelete_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.Enter) && (btEdit.Enabled))
                {
                    btEdit_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.F5) && (btRefresh.Enabled))
                {
                    btRefresh_Click(this, new EventArgs());
                }
            }
            else // currentMode = editing ou inserting
            {
                if ((e.KeyCode == Keys.Enter) && (btConfirm.Enabled))
                {
                    btConfirm_Click(this, new EventArgs());
                    txtSubtypeId.Focus();
                }
                else if ((e.KeyCode == Keys.Escape) && (btCancel.Enabled))
                {
                    btCancel_Click(this, new EventArgs());
                }
            }

        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            // para evitar que o usuario acesse o page errado pelo teclado
            if (currentMode != Mode.browsing)
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            // para evitar que o usuario acesse o page errado pelo teclado
            if (currentMode == Mode.browsing)
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        private void gridDatabase_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void FrmViewerSetup_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(768, 380);
            }
        }

        private void FrmViewerSetup_Load(object sender, EventArgs e)
        {

        }

        private void gridSetup_MouseClick(object sender, MouseEventArgs e)
        {
            rightClickMenu = new ContextMenu();

            if (e.Button == MouseButtons.Right)
            {
                if (numRows.Visible == true)
                {
                    currentMouseOverRow = gridSetup.HitTest(e.X, e.Y).RowIndex;
                    currentMouseOverColumn = gridSetup.HitTest(e.X, e.Y).ColumnIndex;

                    if (currentMouseOverRow >= 0)
                    {
                        currentGridRowClick = currentMouseOverRow;
                        currentGridColumnClick = currentMouseOverColumn;

                        gridSetup.Rows[currentGridRowClick].Selected = true;

                        if (gridSetup[currentGridColumnClick, currentGridRowClick].Tag != null)
                        {
                            if ((String)gridSetup[currentGridColumnClick, currentGridRowClick].Tag == "simpleHighlight") //simpleHighlight
                            {
                                simpleHighChk = true;
                                autoClearHighChk = false;
                                clearByUserHighChk = false;
                            }


                            else if ((String)gridSetup[currentGridColumnClick, currentGridRowClick].Tag == "autoClearhighlight") //autoClearhighlight
                            {
                                simpleHighChk = false;
                                autoClearHighChk = true;
                                clearByUserHighChk = false;
                            }


                            else if ((String)gridSetup[currentGridColumnClick, currentGridRowClick].Tag == "clearByUser") //clearByUser
                            {
                                simpleHighChk = false;
                                autoClearHighChk = false;
                                clearByUserHighChk = true;
                            }
                            else if ((String)gridSetup[currentGridColumnClick, currentGridRowClick].Tag == "noHighlight") // noHighlight
                            {
                                simpleHighChk = false;
                                autoClearHighChk = false;
                                clearByUserHighChk = false;
                            }
                        }
                        else if (gridSetup[currentGridColumnClick, currentGridRowClick].Tag == null)
                        {
                            simpleHighChk = false;
                            autoClearHighChk = false;
                            clearByUserHighChk = false;
                        }

                        rightClickMenu.MenuItems.Add("No highlighting when change", new EventHandler(simpleHigh)).Checked = simpleHighChk;
                        rightClickMenu.MenuItems.Add("Highlight when change, auto-clear", new EventHandler(autoClearHigh)).Checked = autoClearHighChk;
                        rightClickMenu.MenuItems.Add("Highlight when change, cleared by user click", new EventHandler(clearByUserHigh)).Checked = clearByUserHighChk;
                    }

                    rightClickMenu.Show(gridSetup, new Point(e.X, e.Y));
                }
            }
        }

        private void gridSetup_SizeChanged(object sender, EventArgs e)
        {
            ResizeGrid();
        }

        private void numCollumns_ValueChanged(object sender, EventArgs e)
        {
            if ((int)numCollumns.Value < gridSetupColumn)
            {
                gridSetup.Columns.RemoveAt(gridSetupColumn - 1);
                gridSetupColumn--;

            }
            else if ((int)numCollumns.Value > gridSetupColumn)
            {
                gridSetup.Columns.Add("", "");
                gridSetupColumn++;
                foreach (DataGridViewRow gridRow in gridSetup.Rows)
                {
                    gridSetup[gridSetupColumn - 1, gridRow.Index] = fillCmb();
                }

            }
            ResizeGrid();
        }

        private void numRows_ValueChanged(object sender, EventArgs e)
        {
            if ((int)numRows.Value < gridSetupRow)
            {
                gridSetup.Rows.RemoveAt(gridSetupRow - 1);
                gridSetupRow--;

            }
            else if ((int)numRows.Value > gridSetupRow)
            {
                gridSetup.Rows.Add();
                gridSetupRow++;
                foreach (DataGridViewColumn gridColumn in gridSetup.Columns)
                {
                    gridSetup[gridColumn.Index, gridSetupRow - 1] = fillCmb();
                }
            }
            ResizeGrid();
        }

        #endregion

        #region Metodos Privados
        
        private void RefreshGrid()
        {
            gridDatabase.DataSource = dbViewerSetup.ReturnHkView();
            gridDatabase.Columns[0].HeaderText = "ID";
            gridDatabase.Columns[1].HeaderText = "Description";
            gridDatabase.Refresh();
        }
        
        private void ChangeMode(Mode newMode)
        {
            switch (newMode)
            {
                case Mode.browsing:
                {
                    RefreshGrid();

                    if (gridDatabase.RowCount == 0)
                    {
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                    }
                    else
                    {
                        btEdit.Enabled = true;
                        btDelete.Enabled = true;
                    }

                    btNew.Enabled = true;
                    btConfirm.Enabled = false;
                    btCancel.Enabled = false;
                    btRefresh.Enabled = true;

                    currentMode = newMode;

                    tabControl1.SelectedIndex = 0;
                    
                    gridDatabase.Focus();

                    break;
                }
                case Mode.inserting:                
                case Mode.editing:
                {

                    if (newMode == Mode.inserting)
                    {
                        txtSubtypeId.Text = Convert.ToString((dbViewerSetup.ReturnLastId() + 1));
                        txtDescription.Text = "";
                        FillHkParametersList();
                    }
                    else // editing
                    {
                        if (btEdit.Enabled == false)
                        {
                            return;
                        }
                        txtSubtypeId.Text = gridDatabase.SelectedCells[0].Value.ToString();
                        txtDescription.Text = gridDatabase.SelectedCells[1].Value.ToString();

                        int subTypeId = Convert.ToInt16(txtSubtypeId.Text);
                        DataTable tempDt = dbViewerSetup.ReturnMaxColRow(subTypeId);
                        DataGridViewComboBoxCell tempCmb = fillCmb();

                        gridSetupColumn = Convert.ToInt16(tempDt.Rows[0][0].ToString());
                        gridSetupRow = Convert.ToInt16(tempDt.Rows[0][1].ToString());
                        gridSetupColumn++;
                        gridSetupRow++;
                        FillHkParametersList();
                        numCollumns.Value = gridSetupColumn;
                        numRows.Value = gridSetupRow;

                        tempDt = dbViewerSetup.ReturnHkSetup(subTypeId);

                        foreach (DataRow row in tempDt.Rows)
                        {
                            gridSetup[Convert.ToInt32(row[3]), Convert.ToInt32(row[2])].Value = dbViewerSetup.ReturnCmbDescById(Convert.ToInt32(row[1]));
                            gridSetup[Convert.ToInt32(row[3]), Convert.ToInt32(row[2])].Tag = row[4].ToString();
                        }
                        
                    }

                    ResizeGrid();

                    btNew.Enabled = false;
                    btEdit.Enabled = false;
                    btDelete.Enabled = false;
                    btConfirm.Enabled = true;
                    btCancel.Enabled = true;
                    btRefresh.Enabled = false;

                    currentMode = newMode;

                    tabControl1.SelectedIndex = 1;
                    txtDescription.Focus();

                    break;
                }
            }
        }
                
       /**
         * Metodo utilizado para fazer a validacao dos dados 
         **/
        private bool ValidateData()
        {
            if (txtDescription.Text == null || txtDescription.Text == "")
            {
                MessageBox.Show("The description value is empty. Correct it and try again.", "Inconsistent Data",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

                return false;
            }

            for (int i = 0; i < gridSetup.Columns.Count; i++)
            {
                for (int x = 0; x < gridSetup.Rows.Count; x++)
                {
                    if (CompareGridSetupData(x, i))
                    {
                        MessageBox.Show("Duplicity on row " + (x + 1) + " column " + (i + 1) + " please change the combo value. Correct it and try again.", "Inconsistent Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                        return false;
                    }
                }
            }

            return true;
        }

        /**
         * Metodo utilizado para utilizar o ponto de referencia recebido para comparar com o resto do grid
         **/
        private bool CompareGridSetupData(int row, int col)
        {
            bool ret = false;
            String ColRow = col + "." + row;
            String ix = "";

            for (int i = 0; i < gridSetup.Columns.Count; i++)
            {
                for (int x = 0; x < gridSetup.Rows.Count; x++)
                {
                    ix = i + "." + x;
                    if (gridSetup[i, x].Value != null && gridSetup[col, row].Value != null)
                    {
                        if (gridSetup[i, x].Value.Equals(gridSetup[col, row].Value) && ColRow != ix)
                        {
                            ret = true;
                        }
                    }
                }

            }

            return ret;
        }


        /**
         * Verifica se o sistema podera deletar o subtipo do banco de dados.
         * Caso a estrutura do subtipo seja usada por algum outro, nao permite a excludao.
         **/
        private bool CanDelete()
        {
            return false;
        }


        private void FillHkParametersList()
        {
            gridSetup.ColumnCount = gridSetupColumn;
            gridSetup.RowCount = gridSetupRow;
            gridSetup.ColumnHeadersVisible = false;
            gridSetup.SelectionMode = DataGridViewSelectionMode.CellSelect;
            gridSetup.DefaultCellStyle.WrapMode = DataGridViewTriState.True;



            foreach (DataGridViewColumn col in gridSetup.Columns)
            {
                col.Width = 200;

                foreach (DataGridViewRow gridRow in gridSetup.Rows)
                {
                    gridSetup[col.Index, gridRow.Index] = fillCmb();
                }

            }
        }

        //preenche o combo utilizado no gridsetup
        private DataGridViewComboBoxCell fillCmb()
        {
            DataGridViewComboBoxCell cmbCell = new DataGridViewComboBoxCell();

            DataTable tblHkParameters = dbViewerSetup.ReturnCmbDesc();

            foreach (DataRow row in tblHkParameters.Rows)
            {
                cmbCell.Items.Add(row[0]);
            }

            return cmbCell;
        }

        //arruma o grid de acordo com a tela
        private void ResizeGrid()
        {
            int colWidth = gridSetup.Width / gridSetup.Columns.Count;
            int rowHeight = gridSetup.Height / gridSetup.Rows.Count;

            foreach (DataGridViewColumn col in gridSetup.Columns)
            {
                col.Width = colWidth;
            }

            foreach (DataGridViewRow row in gridSetup.Rows)
            {
                row.Height = rowHeight;
            }

            gridSetup.Refresh();
        }
               
        #endregion        

        #region Eventos do RightClick

        void simpleHigh(Object sender, EventArgs e)
        {
            gridSetup[currentGridColumnClick, currentGridRowClick].Tag = "simpleHighlight";
        }

        void autoClearHigh(Object sender, EventArgs e)
        {
            gridSetup[currentGridColumnClick, currentGridRowClick].Tag = "autoClearhighlight";
        }

        void clearByUserHigh(Object sender, EventArgs e)
        {
            gridSetup[currentGridColumnClick, currentGridRowClick].Tag = "clearByUser";
        }

        #endregion

    }
}
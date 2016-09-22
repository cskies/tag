/**
 * @file 	    FrmDataFieldsLists.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    31/08/2011
 * @note	    Modificado em 27/09/2011 por Ayres e Thiago.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters;
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmDataFieldsLists
     * Formulario de cadastro de Data Fields Lists
     **/
    public partial class FrmDataFieldsLists : DockContent
    {
        #region Atributos

        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;

        // Variavel para manter o controle da linha selecionada no grid durante mudanca de modos
        private int fieldId = 0;
        private DbDataFieldsLists datafieldlists = new DbDataFieldsLists();
        private Object[,] dataFieldList = null;
        private FrmDataFields frmDataFields = null;
        private bool RetIdBool;

        #endregion

        #region Construtor

        public FrmDataFieldsLists(Form frm)
        {
            InitializeComponent();

            if (frm != null)
            {
                if (frm.Text.Equals("Data Fields"))
                {
                    frmDataFields = (FrmDataFields)frm;
                    ChangeMode(Mode.inserting);
                }
                else
                {
                    ChangeMode(currentMode);
                }
            }
            else
            {
                ChangeMode(currentMode);
            }
        }
        #endregion

        #region Metodos privados

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

                            if (fieldId != 0)
                            {
                                // Seleciona o row correspondewnte
                                foreach (DataGridViewRow row in gridDatabase.Rows)
                                {
                                    int gridFieldId = int.Parse(row.Cells[0].Value.ToString());

                                    if (fieldId == gridFieldId)
                                    {
                                        gridDatabase.Rows[row.Index].Cells[0].Selected = true;
                                        break;
                                    }
                                }

                                fieldId = 0;
                            }
                        }

                        btNew.Enabled = true;
                        btConfirm.Enabled = false;
                        btCancel.Enabled = false;
                        btReport.Enabled = true;
                        btRefresh.Enabled = true;

                        currentMode = newMode;

                        tabControl1.SelectedIndex = 0;
                        gridDatabase.Focus();

                        break;
                    }
                case Mode.inserting:
                case Mode.editing:
                    {
                        currentMode = newMode;

                        // Nao executa esta linha caso seja chamado pelo frmDataFields
                        if (frmDataFields == null)
                        {
                            fieldId = int.Parse(gridDatabase.CurrentRow.Cells[0].Value.ToString());
                        }

                        gridList.Rows.Clear();

                        if (newMode == Mode.inserting)
                        {
                            // Busca o ultimo codigo na tabela data_fields
                            string sql = "select isnull(MAX(list_id),0)+1 from data_field_lists_header";
                            int maxDataFieldsLists = (int)DbInterface.ExecuteScalar(sql);
                            txtId.Text = (maxDataFieldsLists).ToString();

                            gridList.Rows.Add(1);
                            txtDescription.ResetText();
                        }
                        else // editing
                        {
                            if (btEdit.Enabled == false)
                            {
                                return;
                            }

                            txtId.Enabled = false;
                            txtId.Text = gridDatabase.CurrentRow.Cells[0].Value.ToString();
                            txtDescription.Text = gridDatabase.CurrentRow.Cells[1].Value.ToString();
                            FillGridStructure();
                            txtDescription.Focus();
                            fieldId = int.Parse(txtId.Text); // para selecionar o row correspondente.                        

                            if (gridDatabase.RowCount != 0)
                            {
                                fieldId = int.Parse(gridDatabase.CurrentRow.Cells[0].Value.ToString()); // para selecionar o row correspondente.
                            }

                            txtDescription.ResetText();
                            txtDescription.Text = gridDatabase.CurrentRow.Cells[1].Value.ToString();
                        }

                        btNew.Enabled = false;
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btConfirm.Enabled = true;
                        btReport.Enabled = false;
                        btCancel.Enabled = true;
                        btRefresh.Enabled = false;
                        tabControl1.SelectedIndex = 1;
                        txtDescription.Focus();

                        break;
                    }


                case Mode.report:
                    {
                        currentMode = newMode;

                        btNew.Enabled = false;
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btConfirm.Enabled = false;
                        btCancel.Enabled = false;
                        btRefresh.Enabled = false;

                        // TODO: precisamos passar o nome da missao ao relatorio (com base na selecao do usuario ao iniciar o SMC)
                        // como um parametro, substituindo 'Amazonia-1' no rodape

                        data_fields_listsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();

                        int indexOfParameterName = 0;

                        for (int i = 0; i < Properties.Settings.Default.db_connections_names.Count; i++)
                        {
                            if (Properties.Settings.Default.db_connection_string.ToString() == Properties.Settings.Default.db_connections_strings[i].ToString())
                            {
                                indexOfParameterName = i;
                            }
                        }


                        //define o nome da missão como string
                        String MissionNameString = Properties.Settings.Default.db_connections_names[indexOfParameterName];

                        //adiciona os parametros para o rodape
                        ReportParameter MissionName = new ReportParameter("MissionName", MissionNameString);
                        reportViewer1.LocalReport.SetParameters(new ReportParameter[] { MissionName });
                        
                        data_fields_listsTableAdapter.Fill(dataSetApids.data_fields_lists);
                        reportViewer1.RefreshReport();
                        tabControl1.SelectedIndex = 2;
                        Report = false;

                        break;
                    }
            }
        }

        private bool Report = true;

        /**Preenche o gridList**/
        private bool FillGridStructure()
        {
            gridList.Rows.Clear();

            String sql = "select list_value, list_text from data_field_lists where list_id="+txtId.Text;
            DataTable tblStructure = DbInterface.GetDataTable(sql);

            if (tblStructure == null)
            {
                return false; // retorna false porque ocorreu algum erro ao acessar o banco
            }

            if (tblStructure.Rows.Count > 0)
            {
                
                // cria o numero de rows necessarios
                gridList.Rows.Add(tblStructure.Rows.Count);

                int index = 0;

                // Adiciona os rows da estrutura
                foreach (DataRow row in tblStructure.Rows)
                {
                    // Seta o valor correspondente em cada celula
                    gridList.Rows[index].Cells[0].Value = row[0].ToString();
                    gridList.Rows[index].Cells[1].Value = row[1].ToString();
                    index++;
                }
            }

            return true;
        }

        private void RefreshGrid()
        {
            gridDatabase.Columns.Clear();

            gridDatabase.DataSource = datafieldlists.GetTable();
            gridDatabase.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /**
        * Valida os dados do usuario primeiro na interface, depois
        * contra os dados ja existentes no BD.
        **/
        private bool ValidateData()
        {
            String sql = "";

            if (currentMode == Mode.inserting)
            {
                if (datafieldlists.Key.Equals("") || datafieldlists.Description.Equals(""))
                {
                    MessageBox.Show("The Description is empty! \n\nFill this and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtId.Focus();
                    txtId.SelectAll();
                    return false;
                }
            }
            else // currentMode = editing
            {
                if (datafieldlists.Description.Equals(""))
                {
                    MessageBox.Show("The " + gridDatabase.Columns[0].HeaderText + " is empty! \n\nFill it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return false;
                }

                // verifica se ha descricao similar (equivalente)
                sql = "select isnull(count(list_id), 0) as similarValue " +
                      "from data_field_lists_header " +
                      "where dbo.f_regularString(list_description) = dbo.f_regularString('" + datafieldlists.Description + "')" +
                      "and list_id <> " + datafieldlists.Key;

                int numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a list with an equal or equivalent description! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtId.Focus();
                    txtId.SelectAll();
                    return false;
                }
            }
            return true;
        }

        /**
         * Metodo usado para validar a lista de valores do data field. Retorna true se os valores estiverem ok.
         **/
        private bool ValidatedList()
        {
            int prox = 0;

            foreach (DataGridViewRow row in gridList.Rows)
            {
                Object value = row.Cells[0].Value;
                Object text = row.Cells[1].Value;

                // Verifica se value esta vazio
                if ((value == null) || (value.ToString().Trim().Equals("")))
                {
                    MessageBox.Show("The Value " + (row.Index + 1) + "  is empty! \n\nPlease fix it and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    gridList.Rows[row.Index].Cells[0].Selected = true;
                    return false;
                }

                // Verifica se text esta vazio
                if ((text == null) || (text.ToString().Trim().Equals("")))
                {
                    MessageBox.Show("The Text " + (row.Index + 1) + "  is empty! \n\nPlease fix it and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    gridList.Rows[row.Index].Cells[1].Selected = true;
                    return false;
                }

                // Verifica se value eh inteiro
                int outputValue;

                if (!int.TryParse(value.ToString(), out outputValue))
                {
                    MessageBox.Show("The 'Value' column only accepts numeric values! \n\nPlease fix it and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    gridList.Rows[row.Index].Cells[0].Selected = true;
                    return false;
                }
            }

            // Verificar se os dados sao coerentes: Se os dados da coluna Value 
            // sao integer e se nao existem dados repetidos em ambas as colunas.
            foreach (DataGridViewRow row in gridList.Rows)
            {
                Object value = row.Cells[0].Value;
                Object text = row.Cells[1].Value;

                foreach (DataGridViewRow rowProx in gridList.Rows)
                {
                    if (rowProx.Index > prox)
                    {
                        Object valueProx = rowProx.Cells[0].Value;
                        Object textProx = rowProx.Cells[1].Value;

                        if (value.ToString().Equals(valueProx.ToString()))
                        {
                            MessageBox.Show("There is repeated data in the 'Value' column! \n\nPlease fix it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridList.Rows[row.Index].Cells[0].Selected = true;
                            return false;
                        }

                        if (text.ToString().Equals(textProx.ToString()))
                        {
                            MessageBox.Show("There is repeated data in the 'Text' column! \n\nPlease fix it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridList.Rows[row.Index].Cells[1].Selected = true;
                            return false;
                        }
                    }
                }

                prox++;
            }

            return true;
        }

        private void MoveRow(bool up)
        {
            int currentIndex = gridList.CurrentRow.Index;
            int newIndex = 0;

            // determina qual a linha a ser trocada e faz a troca
            if (up)
            {
                newIndex = currentIndex - 1;
            }
            else
            {
                newIndex = currentIndex + 1;
            }

            if ((newIndex < 0) || (newIndex == gridList.RowCount))
            {
                // nao eh possivel mover; sai
                return;
            }

            DataGridViewRow rowToMove = gridList.Rows[currentIndex];

            gridList.Rows.Remove(gridList.Rows[currentIndex]);

            gridList.Rows.Insert(newIndex, rowToMove);
            gridList.Refresh();
            gridList.CurrentCell = gridList.Rows[newIndex].Cells[0];
            gridList.Rows[newIndex].Selected = true;
        }

        #endregion

        #region Eventos da Interface Grafica

        private void btNew_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.inserting);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
            if (frmDataFields != null)
            {
                Close();
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            datafieldlists.Key = txtId.Text.Trim();
            datafieldlists.Description = txtDescription.Text.Trim();

            // forca que edicoes pendentes sejam finalizadas
            gridList.EndEdit();

            // Validar a lista de valores
            if (!ValidatedList())
            {
                return;
            }

            if (!ValidateData())
            {
                return;
            }

            // Criar a matriz para armazenar os dados do gridList
            dataFieldList = new Object[gridList.Rows.Count, gridList.Columns.Count];

            // Popular a matriz com os dados da list
            foreach (DataGridViewRow row in gridList.Rows)
            {
                dataFieldList[row.Index, 0] = row.Cells[0].Value.ToString();
                dataFieldList[row.Index, 1] = row.Cells[1].Value.ToString();
            }

            // Enviar para a classe db
            datafieldlists.DataFieldList = dataFieldList;

            if (currentMode == Mode.inserting)
            {
                if (!datafieldlists.Insert())
                {
                    return; // sai sem voltar ao modo browsing
                }
                


                fieldId = int.Parse(datafieldlists.Key);

                // Como eh insercao, continua no mesmo modo
                ChangeMode(Mode.inserting);
            }
            else
            {
                if (!datafieldlists.Update())
                {
                    return; // sai sem voltar ao modo browsing
                }

                fieldId = int.Parse(datafieldlists.Key);

                // Volta ao modo browsing
                ChangeMode(Mode.browsing);
            }

            if (frmDataFields!=null)
            {
                RetIdBool = true;
                Close();
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }
        
        private void btEdit_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.editing);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            datafieldlists.Key = gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();

            if (MessageBox.Show("Are you sure you want to delete the " + this.Text.Substring(0, this.Text.Length - 1) + " " + datafieldlists.Key + ", '" + datafieldlists.Description + "' ?",
                                "Please confirm deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            int indexSelected = gridDatabase.CurrentRow.Index;

            if (!datafieldlists.Delete())
            {
                return;
            }
            else
            {
                ChangeMode(Mode.browsing);

                if (gridDatabase.RowCount == 0)
                {
                    return;
                }

                // Setar o row seguinte aqui
                if (indexSelected == gridDatabase.RowCount)
                {
                    gridDatabase.Rows[indexSelected - 1].Cells[0].Selected = true;
                }
                else
                {
                    gridDatabase.Rows[indexSelected].Cells[0].Selected = true;
                }
            }
        }

        /**
         * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
         * para permitir a operacao da tela pelo usuario.
         **/
        private void FrmDataFieldsLists_KeyDown(object sender, KeyEventArgs e)
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
                }
                else if ((e.KeyCode == Keys.Escape) && (btCancel.Enabled))
                {
                    btCancel_Click(this, new EventArgs());
                }
            }
        }
            
        private void btDown_Click(object sender, EventArgs e)
        {
            MoveRow(false);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            gridList.Rows.Add(1);

            if (gridList.RowCount > 1)
            {
                btRemove.Enabled = true;
            }

            gridList.Rows[gridList.Rows.Count - 1].Cells[0].Selected = true;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            gridList.Rows.Remove(gridList.CurrentRow);

            if (gridList.RowCount == 1)
            {
                btRemove.Enabled = false;
            }
        }
        
        private void btUp_Click_1(object sender, EventArgs e)
        {
            MoveRow(true);
        }
        
        private void gridDatabase_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void FrmDataFieldsLists_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(772, 418);
            }
        }

        private void btReport_Click(object sender, EventArgs e)
        {
            if (Report == true)
            {
                ChangeMode(Mode.report);
            }
            else
            {
                ChangeMode(Mode.browsing);
                Report = true;
            }
        }
       

        #endregion

        #region Metodos Publicos

        public Boolean RetId() 
        {
            return RetIdBool;
        }

        public string txtid()
        {
            return txtId.Text;
        }
        #endregion

       
        }
    }

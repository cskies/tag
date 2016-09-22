/**
 * @file 	    FrmSubtypes.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    14/07/2009
 * @note	    Modificado em 02/06/2016 por Fabricio.
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
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSubtypes
     * Formulario de cadastro de Subtypes.
     **/
    public partial class FrmSubtypes : DockContent
    {
        #region Atributos Internos

        private enum Mode {browsing, inserting, editing, report};
        private Mode currentMode = Mode.browsing;

        // Variaveis para manter o controle da linha selecionada no grid durante mudanca de modos
        private int serviceSelected = 0;
        private int subtypeSelected = 0;

        private DbSubtype dbSubtypes = new DbSubtype();

        #endregion

        #region Construtor

        public FrmSubtypes()
        {
            InitializeComponent();
            ChangeMode(Mode.browsing);
            gridDatabase.Focus();
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
            if (MessageBox.Show("Are you sure you want to delete the Subtype " + gridDatabase.CurrentRow.Cells[2].Value.ToString() + ", '" + gridDatabase.CurrentRow.Cells[3].Value.ToString() + "' ?",
                                "Please Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, 
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            if (CanDelete())
            {
                dbSubtypes.ServiceType = gridDatabase.CurrentRow.Cells[0].Value.ToString();
                dbSubtypes.ServiceSubtype = gridDatabase.CurrentRow.Cells[2].Value.ToString();

                int indexSelected = gridDatabase.CurrentRow.Index;

                if (dbSubtypes.Delete())
                {
                    ChangeMode(Mode.browsing);

                    if (gridDatabase.RowCount == 0)
                    {
                        return;
                    }

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
            bool updateLinkedSubtypes = false;

            // forca que edicoes pendentes sejam finalizadas
            gridStructure.EndEdit();

            if (!ValidateData())
            {
                return;
            }

            dbSubtypes.ServiceType = cmbService.Text.Substring(1, 3);
            dbSubtypes.ServiceSubtype = numSubtype.Value.ToString();
            dbSubtypes.Description = txtDescription.Text.Trim();
            dbSubtypes.IsRequest = rdbRequest.Checked;
            dbSubtypes.AllowRepetition = chkAllowsRepetition.Checked;

            // Verifica se o cmbSameAs nao tem item selecionado.
            if (cmbSameStructure.SelectedIndex == 0)
            {
                // Verifica se existe items no gridStructure.
                if (gridStructure.RowCount > 0)
                {
                    dbSubtypes.SubtypeStructure = MountStructure();
                }
                else
                {
                    dbSubtypes.SubtypeStructure = null;
                }
            }
            else
            {
                dbSubtypes.SameStructureAs = cmbSameStructure.Text.Substring(1, 3);
            }

            if (currentMode == Mode.inserting)
            {
                if (!dbSubtypes.Insert())
                {
                    return; // sai sem voltar ao modo browsing
                }

                serviceSelected = int.Parse(cmbService.Text.Substring(1, 3));
                subtypeSelected = int.Parse(numSubtype.Value.ToString());

                // Como eh insercao, continua no mesmo modo
                ChangeMode(Mode.inserting);
            }
            else
            {
                if (dbSubtypes.SameStructureAs != "")
                {
                    // verifica se o subtipo editado era base (copia de estrutura) para algum outro, e se passou a copiar (...)
                    string sql = @"select service_subtype from subtype_structure where service_type = " + serviceSelected + " and same_as_subtype = " + subtypeSelected;

                    DataTable structureCopy = DbInterface.GetDataTable(sql);

                    if (structureCopy.Rows.Count > 0)
                    {
                        // (...) nesse caso, avisa o usuario do impacto dessa alteracao
                        string subtypeSameAs = "";

                        foreach (DataRow row in structureCopy.Rows)
                        {
                            subtypeSameAs += row[0].ToString() + ", ";
                        }

                        DialogResult dialogYesNoResult = MessageBox.Show(
                                                        "The following subtypes currently use the structure defined on this subtype: " + subtypeSameAs.Substring(0, subtypeSameAs.Length - 2) + ". \n\n" +
                                                        "As you have made this subtype copy the structure of subtype " + dbSubtypes.SameStructureAs +
                                                        ", by clicking 'Yes', these other subtypes will also have as reference for its structures the subtype " + dbSubtypes.SameStructureAs + ". \n\n" +
                                                        "If you don't want this change, click 'No' and keep editing the current subtype.",
                                                        "Confirm changing of base structure",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

                        if (dialogYesNoResult == DialogResult.No)
                        {
                            return; // sai sem voltar ao modo browsing
                        }

                        updateLinkedSubtypes = true;
                    }
                }

                if (!dbSubtypes.Update(updateLinkedSubtypes))
                {
                    return; // sai sem voltar ao modo browsing
                }

                serviceSelected = int.Parse(cmbService.Text.Substring(1, 3));
                subtypeSelected = int.Parse(numSubtype.Value.ToString());

                // Volta ao modo browsing
                ChangeMode(Mode.browsing);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

        private void cmbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCmbSameStructure(cmbService.Text.Substring(1, 3), "");
        }

        /**
         * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
         * para permitir a operacao da tela pelo usuario.
         **/
        private void FrmSubtypes_KeyDown(object sender, KeyEventArgs e)
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
                    numSubtype.Focus();
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
                tabControl1.SelectedIndex = 1;
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            // para evitar que o usuario acesse o page errado pelo teclado
            if (currentMode == Mode.browsing)
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        private void gridDatabase_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void cmbSameStructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Preenche o gridStructure de acordo com os itens selecionados
            // no cmbService e no cmbSameStructure.
            if (cmbSameStructure.SelectedIndex != 0)
            {
                gridStructure.ReadOnly = true;

                string serviceType = cmbService.Text.Substring(1, 3);
                string serviceSubtype = cmbSameStructure.Text.Substring(1, 3);

                FillGridStructure(serviceType, serviceSubtype);

                gridStructure.ClearSelection();

                btAdd.Enabled = false;
                btRemove.Enabled = false;
                btUp.Enabled = false;
                btDown.Enabled = false;
                btNewDtField.Enabled = false;
            }
            else
            {
                // Caso o subtype tenha sua propria estrutura, sem herdar, 
                // preencho o gridStructure se o item < None > ser selecionado.
                if (currentMode == Mode.editing)
                {
                    string serviceType = cmbService.Text.Substring(1, 3);
                    string serviceSubtype = numSubtype.Value.ToString();

                    FillGridStructure(serviceType, serviceSubtype);
                }

                gridStructure.ReadOnly = false;
                btAdd.Enabled = true;
                btRemove.Enabled = false;
                btUp.Enabled = false;
                btDown.Enabled = false;
                btNewDtField.Enabled = true;

                // Se o grid tiver apenas 1 row, habilita apenas os botões Add, Remove e New Data Field.
                // Se o grid tiver mais de 1 row, habilita todos os botões.
                if (gridStructure.RowCount == 1)
                {
                    btRemove.Enabled = true;
                }
                else if (gridStructure.RowCount > 1)
                {
                    btRemove.Enabled = true;
                    btUp.Enabled = true;
                    btDown.Enabled = true;
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            // Preenche os combos da coluna Data Fields
            if (gridStructure.Rows.Count == 0)
            {
                if (!FillCmbColDataFields())
                {
                    return;
                }
            }

            gridStructure.Rows.Add(1);

            if (gridStructure.RowCount > 1)
            {
                btUp.Enabled = true;
                btDown.Enabled = true;
            }

            btRemove.Enabled = true;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            gridStructure.Rows.Remove(gridStructure.CurrentRow);

            if (gridStructure.RowCount == 1)
            {
                btUp.Enabled = false;
                btDown.Enabled = false;
            }

            if (gridStructure.RowCount == 0)
            {
                btUp.Enabled = false;
                btDown.Enabled = false;
                btRemove.Enabled = false;
            }           
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            MoveRow(true);
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            MoveRow(false);
        }

        private void btNewDtField_Click(object sender, EventArgs e)
        {
            FrmDataFields frm = new FrmDataFields(this);
            frm.ShowDialog();
        }

        private void FrmSubtypes_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(768, 380);
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

        #region Metodos Privados
        
        private void RefreshGrid()
        {
            string sql = @"select a.service_type, 
                                  a.service_name, 
                                  b.service_subtype, 
                                  b.description,
                           case   b.is_request when 1 then 'Request' else 'Report' end as is_request,
                                  b.allow_repetition 
                           from   services a inner join subtypes b 
                           on     a.service_type = b.service_type";

            DataTable tblSubtypes = DbInterface.GetDataTable(sql);

            gridDatabase.Columns.Clear();
            gridDatabase.DataSource = tblSubtypes;

            gridDatabase.Columns[0].HeaderText = "Service Type";
            gridDatabase.Columns[1].HeaderText = "Service Name";
            gridDatabase.Columns[2].HeaderText = "Service Subtype";
            gridDatabase.Columns[3].HeaderText = "Subtype Description";
            gridDatabase.Columns[4].HeaderText = "Request/Report";

            gridDatabase.Columns[5].Visible = false;

            gridDatabase.Columns[0].Width = 60;
            gridDatabase.Columns[1].Width = 180;
            gridDatabase.Columns[2].Width = 60;
            gridDatabase.Columns[3].Width = 300;
            gridDatabase.Columns[4].Width = 120;
            gridDatabase.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        
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

                        serviceSelected = 0;
                        subtypeSelected = 0;
                    }
                    else
                    {
                        btEdit.Enabled = true;
                        btDelete.Enabled = true;

                        // Selecionar o item no gridDatabase
                        if ((serviceSelected != 0) && (subtypeSelected != 0))
                        {
                            foreach (DataGridViewRow row in gridDatabase.Rows)
                            {
                                int service = int.Parse(row.Cells[0].Value.ToString());
                                int subtype = int.Parse(row.Cells[2].Value.ToString());

                                if ((service == serviceSelected) && (subtype == subtypeSelected))
                                {
                                    gridDatabase.Rows[row.Index].Cells[0].Selected = true;
                                    break;
                                }
                            }

                            serviceSelected = 0;
                            subtypeSelected = 0;
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
                    // se estiver incluindo, continua com o mesmo servico selecionado
                    if (!((currentMode == newMode) && (currentMode == Mode.inserting)))
                    {
                        // se o CmbService nao for preenchido, sai e continue no modo browsing.                        
                        if (!FillCmbService())
                        {
                            return;
                        }
                    }

                    if (newMode == Mode.inserting)
                    {                        
                        bool correctFilling = false; // variavel que verifica se ocorreu algum erro no preencimento
                        // do cmbSameAs. OBS: O caso da nao existencia de registros no banco nao eh considerado um erro.
                                                
                        // Ao preencher o cmbSameAs, pergunto antes se o cmbService esta selecionado
                        // com algum item. Pode existir o caso do cmbService nao possuir itens pelo 
                        // fato de nao ter registro de services no banco de dados. Nesse caso, ocorrera
                        // um erro ao solicitar a Substring(1, 4) de um item em branco.
                        if (cmbService.Text.Equals(""))
                        {
                            correctFilling = FillCmbSameStructure("", ""); // nesse caso, a consulta nao da erro. Apenas nao retorna registros.
                        }
                        else
                        {
                            correctFilling = FillCmbSameStructure(cmbService.Text.Substring(1, 3), "");
                        }

                        // Se o CmbSame nao ser preenchido, devido a algum erro,
                        // sai e continua no modo browsing.
                        if (!correctFilling)
                        {
                            return;
                        }

                        cmbService.Enabled = true;
                        numSubtype.Enabled = true;
                        numSubtype.Value = 1;
                        txtDescription.Text = "";
                        rdbRequest.Select();
                        gridStructure.Rows.Clear();
                        gridStructure.ReadOnly = false;
                        btAdd.Enabled = true;
                        btNewDtField.Enabled = true;
                        btRemove.Enabled = false;
                        btUp.Enabled = false;
                        btDown.Enabled = false;
                        chkAllowsRepetition.Checked = false;
                    }
                    else // editing
                    {
                        if (btEdit.Enabled == false)
                        {
                            return;
                        }

                        cmbService.Enabled = false;
                        numSubtype.Enabled = false;

                        string serviceType = gridDatabase.CurrentRow.Cells[0].Value.ToString();
                        string serviceSubtype = gridDatabase.CurrentRow.Cells[2].Value.ToString();
                                                
                        cmbService.SelectedIndex = cmbService.FindString(Formatting.FormatCode(int.Parse(serviceType), 3));
                        numSubtype.Value = int.Parse(gridDatabase.CurrentRow.Cells[2].Value.ToString());

                        // Seta os valores para a selecao do row
                        serviceSelected = int.Parse(serviceType);
                        subtypeSelected = int.Parse(serviceSubtype);

                        if (gridDatabase.CurrentRow.Cells[4].Value.ToString().Equals("Request"))
                        {
                            rdbRequest.Select();
                        }
                        else
                        {
                            rdbReport.Select();
                        }

                        txtDescription.Text = gridDatabase.CurrentRow.Cells[3].Value.ToString();

                        // Aproveita o allowRepetition que ja veio no GetTable()
                        chkAllowsRepetition.Checked = bool.Parse(gridDatabase.CurrentRow.Cells[5].Value.ToString());

                        // Preenche o comboSameStructure  
                        string servType = cmbService.Text.Substring(1, 3).ToString();

                        bool fillCmbSame = FillCmbSameStructure(servType, numSubtype.Value.ToString());

                        // Se o CmbSame nao ser preenchido, devido a algum erro,
                        // sai e continua no modo browsing.
                        if (!fillCmbSame)
                        {
                            return;
                        }

                        // Se o subtype herdar uma estrutura, retorna o valor da mesma, caso contrario nao faz nada, sameAs recebe "0".
                        string query = "select top 1 isnull(same_as_subtype, 0) as sameAs from subtype_structure where service_type = " + gridDatabase.CurrentRow.Cells[0].Value.ToString() + " and service_subtype = " + gridDatabase.CurrentRow.Cells[2].Value.ToString();

                        Object same = DbInterface.ExecuteScalar(query);
                        int sameAs = 0;

                        // Esta query pode retornar NULL, pois na tabela da estrutura podem nao haver registros do subtype
                        if (same != null)
                        {
                            sameAs = int.Parse(same.ToString());
                        }

                        if (sameAs != 0)
                        {
                            //Se entrar aqui, o subtype herda a estrutura de outro subtype
                            bool fillGridStructure = FillGridStructure(gridDatabase.CurrentRow.Cells[0].Value.ToString(), sameAs.ToString());

                            // Retornara false apenas se caso acontecer algum erro de acesso ao banco.
                            // Para os casos onde o subtype nao possuir uma estrutura, o metodo
                            // retornara TRUE, mesmo se o gridStructure nao ser preenchido.
                            if (!fillGridStructure)
                            {
                                return;
                            }

                            btAdd.Enabled = false;
                            btNewDtField.Enabled = false;
                            btRemove.Enabled = false;
                            btUp.Enabled = false;
                            btDown.Enabled = false;

                            gridStructure.ClearSelection();
                        }
                        else
                        {
                            //Se entrar aqui, o subtype nao herda estrutura de nenhum subtype                                           
                            bool fillGridStructure = FillGridStructure(gridDatabase.CurrentRow.Cells[0].Value.ToString(), gridDatabase.CurrentRow.Cells[2].Value.ToString());

                            // Retornara false apenas se caso acontecer algum erro de acesso ao banco.
                            // Para os casos onde o subtype nao possuir uma estrutura, o metodo
                            // retornara TRUE, mesmo se o gridStructure nao ser preenchido.
                            if (!fillGridStructure)
                            {
                                return;
                            }

                            btAdd.Enabled = true;
                            btNewDtField.Enabled = true;
                            btRemove.Enabled = true;
                            btUp.Enabled = true;
                            btDown.Enabled = true;

                            // se o gridStructure nao ser preenchido, configura os botoes.
                            if (gridStructure.RowCount == 0)
                            {
                                btAdd.Enabled = true;
                                btNewDtField.Enabled = true;
                                btRemove.Enabled = false;
                                btUp.Enabled = false;
                                btDown.Enabled = false;
                            }
                        }
                    }

                    btNew.Enabled = false;
                    btEdit.Enabled = false;
                    btDelete.Enabled = false;
                    btConfirm.Enabled = true;
                    btCancel.Enabled = true;
                    btReport.Enabled = false;
                    btRefresh.Enabled = false;

                    currentMode = newMode;

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

                    subtypesTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                    subtypesTableAdapter.Fill(dataSetApids.subtypes);

                    int indexOfParameterName = 0;

                    for (int i = 0; i < Properties.Settings.Default.db_connections_names.Count; i++)
                    {
                        if (Properties.Settings.Default.db_connection_string.ToString() == Properties.Settings.Default.db_connections_strings[i].ToString())
                        {
                            indexOfParameterName = i;
                        }
                    }


                    //define o nome da missão como string
                    string MissionNameString = Properties.Settings.Default.db_connections_names[indexOfParameterName];

                    //adiciona os parametros para o rodape
                    ReportParameter MissionName = new ReportParameter("MissionName", MissionNameString);
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { MissionName });

                    reportViewer1.RefreshReport();
                    tabControl1.SelectedIndex = 2;
                    Report = false;

                    break;
                }
            }
        }
        private bool Report = true;
        private bool FillCmbService()
        {
            cmbService.Items.Clear();

            string sql = @"select '[' + dbo.f_zero(service_type, 3) + '] ' + service_name as service from services";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table == null)
            {
                return false;
            }

            foreach (DataRow row in table.Rows)
            {
                cmbService.Items.Add(row["service"].ToString());
            }

            if (cmbService.Items.Count != 0)
            {
                cmbService.SelectedIndex = 0;
            }

            return true;
        }

        /**
         * Preenche o Combobox Same Structure com todas as estruturas que jah foram
         * cadastradas no Service selecionado.
         **/
        private bool FillCmbSameStructure(string serviceType, string serviceSubtype)
        {
            cmbSameStructure.Items.Clear();
            cmbSameStructure.Items.Add("< None >");

            string sql = @"select distinct('[' + dbo.f_zero(a.service_subtype, 3) + '] ' + b.description) as subtype
                           from   subtype_structure a inner join subtypes b
                           on     a.service_type = b.service_type
                           where  a.service_type = b.service_type and 
                                  a.service_subtype = b.service_subtype 
                                  and a.service_type = '" + serviceType + @"' 
                                  and a.service_subtype <> '" + serviceSubtype + @"' 
                                  and a.same_as_subtype is NULL";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table == null)
            {
                return false;
            }

            foreach (DataRow row in table.Rows)
            {
                cmbSameStructure.Items.Add(row["subtype"].ToString());
            }

            cmbSameStructure.SelectedIndex = 0;

            if (serviceSubtype.Equals(""))
            {
                // Sai, porque o subtype "nao" herda a estrutura, entao nao ha a necessidade de selecionar o item do cmbSameAs.
                // Retorna true porque nao houve nenhum erro de acesso ao banco.
                return true;
            }

            // Caso o subtype herde a estrutura de outro subtype, busca qual eh o subtype.
            sql = @"select top 1 isnull(same_as_subtype, 0) as sameAs 
                    from subtype_structure 
                    where service_type = " + serviceType + " and service_subtype = " + serviceSubtype;

            Object same = DbInterface.ExecuteScalar(sql);
            int sameAs = 0;

            if (same != null)
            {
                sameAs = int.Parse(same.ToString());
            }

            if (sameAs != 0)
            {
                cmbSameStructure.SelectedIndex = cmbSameStructure.FindString(Formatting.FormatCode(sameAs, 3));
            }

            return true;
        }

        /** Preenche a tabela de estrutura com os data fields do subtype correspondente **/
        private bool FillGridStructure(string serviceType, string serviceSubtype)
        {
            gridStructure.Rows.Clear();

            // Busca a estrutura do subtype no banco
            string sql = @"select '[' + dbo.f_zero(a.data_field_id, 4) + '] ' + a.data_field_name as dataField, b.read_only, b.default_value 
                           from   data_fields a inner join subtype_structure b 
                           on     a.data_field_id = b.data_field_id 
                           where  b.service_type = '" + serviceType + @"' 
                           and    b.service_subtype = '" + serviceSubtype + @"'
                           and    a.data_field_id <> 0 
                           order by b.position";

            DataTable tblStructure = DbInterface.GetDataTable(sql);

            if (tblStructure == null)
            {
                return false; // retorna false porque ocorreu algum erro ao acessar o banco
            }

            if (tblStructure.Rows.Count > 0)
            {
                // Preenche o combobox da coluna Data Fields
                FillCmbColDataFields();

                gridStructure.Rows.Add(tblStructure.Rows.Count);

                int index = 0;

                // Adiciona os rows da estrutura do subtype
                foreach (DataRow row in tblStructure.Rows)
                {
                    // Seta os valores correspondentes em cada celula
                    gridStructure.Rows[index].Cells[0].Value = row[0].ToString();
                    gridStructure.Rows[index].Cells[1].Value = row[1].ToString();
                    gridStructure.Rows[index].Cells[2].Value = row[2].ToString();

                    index++;
                }
            }

            gridStructure.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridStructure.Columns[1].Width = 150;
            gridStructure.Columns[2].Width = 200;

            return true;
        }

        /** Move a row selecionada no grid de estrutura para cima ou para baixo. **/
        public void MoveRow(bool up)
        {
            int currentIndex = gridStructure.CurrentRow.Index;
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

            if ((newIndex < 0) || (newIndex == gridStructure.RowCount))
            {
                // nao eh possivel mover; sai
                return;
            }

            DataGridViewRow rowToMove = gridStructure.Rows[currentIndex];

            gridStructure.Rows.Remove(gridStructure.Rows[currentIndex]);

            gridStructure.Rows.Insert(newIndex, rowToMove);
            gridStructure.Refresh();
            gridStructure.CurrentCell = gridStructure.Rows[newIndex].Cells[0];
            gridStructure.Rows[newIndex].Selected = true;
        }

        /** Preenche os ComboBoxCells da coluna Data Fields do gridStructure */
        public bool FillCmbColDataFields()
        {
            // Limpa o combo, caso tenha itens
            colDataField.Items.Clear();

            // Busca os data fields para serem setados nos combos do gridStructure
            string sql = @"select '[' + dbo.f_zero(data_field_id, 4) + '] ' + data_field_name from data_fields where data_field_id <> 0";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table == null)
            {
                return false; // retorna false porque ocorreu algum erro ao acessar o banco
            }

            foreach (DataRow row in table.Rows)
            {
                colDataField.Items.Add(row[0].ToString());
            }

            return true;
        }

        private Object[,] MountStructure()
        {
            int col = gridStructure.Columns.Count;
            int line = gridStructure.Rows.Count;
            Object[,] structure = new Object[line, col];

            foreach (DataGridViewRow row in gridStructure.Rows)
            {
                structure[row.Index, 0] = row.Cells[0].Value.ToString().Substring(1, 4);
                structure[row.Index, 1] = row.Cells[1].Value;
                Object defaultValue = row.Cells[2].Value;

                if ((defaultValue == null) || (defaultValue.ToString().Trim().Equals("")))
                {
                    structure[row.Index, 2] = "null";
                }
                else
                {
                    structure[row.Index, 2] = row.Cells[2].Value.ToString();
                }
            }

            return structure;
        }

        /**
         * Metodo utilizado para fazer a validacao dos dados (se ja existem dados iguais na 
         * base de dados, valores invalidos e campos em branco). Retorna true caso nenhum 
         * destes quesitos sejam verdadeiros, permitindo inserir ou alterar os dados.    
         * 
         * @todo Este metodo deve ser revisado. Funcionalmente esta perfeito, mas ha muita repeticao
         * de codigo nas verificacoes para os modos inserting e editing. Reescrever isso.
         **/
        private bool ValidateData()
        {
            bool canInsert = false;

            if (currentMode == Mode.inserting)
            {
                // verifico se o cmb possui itens
                if (!cmbService.Text.Equals(""))
                {
                    // Verifica se ja existe subtype cadastrado igual ao selecionado no numSubtype
                    string serviceType = cmbService.Text.Substring(1, 3);
                    string serviceSubtype = numSubtype.Value.ToString();

                    string query = "Select isnull(count(service_subtype), 0) as subtypeEqual from subtypes where service_type = " + serviceType + " and service_subtype = " + serviceSubtype;
                    int subtypeIsEqual = (int)DbInterface.ExecuteScalar(query);

                    if (subtypeIsEqual == 0)
                    {
                        if (!txtDescription.Text.Trim().Equals(""))
                        {
                            // Verifica se ja existe descricao                                    
                            string queryEqual = "select isnull(count(service_subtype), 0) as equalDescription from subtypes where description = dbo.f_regularString('" + txtDescription.Text.Trim() + "')";
                            int equalDescription = (int)DbInterface.ExecuteScalar(query);

                            if (equalDescription == 0)
                            {
                                if (cmbSameStructure.SelectedIndex == 0)
                                {
                                    // Verifica se existe DataField em branco no gridStructure...
                                    int existItemNullOnStructure = SearchEmptyRow();

                                    if (existItemNullOnStructure == -1)
                                    {
                                        // Verifica se os itens inseridos na coluna DefaultValue sao inteiros
                                        int isNotInteger = SearchIntegerRow();

                                        if (isNotInteger == -1)
                                        {
                                            canInsert = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("The Default Value " + isNotInteger + " of structure should be numeric !",
                                                            "Inconsistent Data",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Exclamation);

                                            gridStructure.Rows[isNotInteger - 1].Selected = true;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("The Data Field of index " + existItemNullOnStructure + " in structure is empty ! \n\nFill it and try again.",
                                                        "Inconsistent Data",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Exclamation);

                                        gridStructure.Rows[existItemNullOnStructure - 1].Selected = true;
                                    }
                                }
                                else
                                {
                                    canInsert = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("The Subtype '" + txtDescription.Text.Trim() + "' already exist !",
                                                "Inconsistent Data",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                txtDescription.SelectAll();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The field " + lblDescription.Text + " is empty ! \n\nFill it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            txtDescription.ResetText();
                            txtDescription.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The Subtype '" + numSubtype.Value + "' already exists !",
                                        "Inconsistent Data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        numSubtype.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("The field " + lblService.Text + " is empty ! \n\nFill it and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    cmbService.Focus();
                }
            }
            else if (currentMode == Mode.editing)
            {
                if (!txtDescription.Text.Trim().Equals(""))                
                {
                    // Verifica se ja existe descricao igual no banco
                    string querySimilar = "select isnull(count(service_subtype), 0) as equalDescription from subtypes where description = dbo.f_regularString('" + txtDescription.Text.Trim() + "') and service_subtype <> " + numSubtype.Value;
                    int similarDescription = (int)DbInterface.ExecuteScalar(querySimilar);

                    if (similarDescription == 0)
                    {
                        if (cmbSameStructure.SelectedIndex == 0)
                        {
                            // Verifica se existe DataField em branco no gridStructure...
                            int existItemNullOnStructure = SearchEmptyRow();

                            if (existItemNullOnStructure == -1)
                            {
                                // Verifica se os itens inseridos na coluna DefaultValue sao integers
                                int isNotInteger = SearchIntegerRow();

                                if (isNotInteger == -1)
                                {
                                    canInsert = true;
                                }
                                else
                                {
                                    MessageBox.Show("The Default Value of index " + isNotInteger + " in structure should be numeric !",
                                                    "Inconsistent Data",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation);

                                    gridStructure.Rows[isNotInteger - 1].Selected = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("The Data Field of index " + existItemNullOnStructure + " in structure is empty ! \n\nFill it and try again.",
                                                "Inconsistent Data",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                gridStructure.Rows[existItemNullOnStructure - 1].Selected = true;
                            }
                        }
                        else
                        {
                            canInsert = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The Subtype '" + txtDescription.Text + "' already exists !",
                                        "Inconsistent Data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        txtDescription.SelectAll();
                    }
                }
                else
                {
                    MessageBox.Show("The field " + lblDescription.Text + " is empty ! \n\nFill it and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation); 
                    
                    txtDescription.ResetText();
                    txtDescription.Focus();
                }
            }

            // Veificar o limite maximo de Bytes do subtype
            // TC: 248 bytes
            // TM: 1103 bytes

            if (canInsert)
            {
                int maxBits = 0;
                string msg = "The Subtype '" + txtDescription.Text + "' exceeded the limit of bytes !";
                int numBits = 0;

                if (rdbRequest.Checked)
                {
                    maxBits = 248 * 8;
                    msg += "\n\nThe limit for request is 248 bytes !";
                }
                else
                {
                    maxBits = 1103 * 8;
                    msg += "\n\nThe limit for report is 1103 bytes !";
                }

                if (gridStructure.Rows.Count > 0)
                {
                    string sqlSumBits = null;
                    int dataFieldId = 0;

                    for (int i = 0; i < gridStructure.Rows.Count; i++)
                    {
                        dataFieldId = int.Parse(gridStructure.Rows[i].Cells[0].Value.ToString().Substring(1, 4));

                        sqlSumBits = "select number_of_bits from data_fields where data_field_id = "+dataFieldId;

                        numBits += (int)DbInterface.ExecuteScalar(sqlSumBits);

                        sqlSumBits = null;
                    }

                    

                    if (numBits > maxBits)
                    {

                        MessageBox.Show(msg,
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                        canInsert = false;
                    }
                }
            }

            // se ainda nao gerou outro erro, verifica mais um
            if ((canInsert) && (chkAllowsRepetition.Checked) && (gridStructure.RowCount == 0))
            {
                MessageBox.Show("The subtype is marked as allowing repetition, but there is no data field informed ! \n\nCorrect it and try again.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                canInsert = false;
            }

            return canInsert;
        }

        /**
         * Verifica se o sistema podera deletar o subtipo do banco de dados.
         * Caso a estrutura do subtipo seja usada por algum outro, nao permite a excludao.
         **/
        private bool CanDelete()
        {
            // Verificar se a estrutura do subtype eh copiada por outro subtype. Neste caso foi necessario utilizar 
            // Um DataTable, para retornar quais subtypes copiam a estrutura do subtype em questao.
            string verifyDependence = "select service_subtype as subtypeDep " +
                                      "from dbo.subtype_structure " +
                                      "where same_as_subtype is not null and " +
                                      "same_as_subtype = " + gridDatabase.CurrentRow.Cells[2].Value.ToString() + " and " +
                                      "service_type = " + gridDatabase.CurrentRow.Cells[0].Value.ToString();

            DataTable structureCopy = DbInterface.GetDataTable(verifyDependence);

            if (structureCopy.Rows.Count == 0)
            {
                return true;
            }

            // se sua estrutura for copiada por outro(s) subtype(s), imprime qual(s) subtype(s) a copia.
            string subtypeSameAs = "";

            foreach (DataRow row in structureCopy.Rows)
            {
                // concatena os subtypes que copiam a estrutura do subtype em questao
                subtypeSameAs += row[0].ToString() + ", ";
            }

            MessageBox.Show("This subtype cannot be deleted, because its structure is being used by subtype(s): " + subtypeSameAs.Substring(0, subtypeSameAs.Length - 2) + "!",
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

            return false;
        }

        /**
         * Verificar se existem items vazios no gridStructure. 
         * Se nenhum item estiver vazio, retorna -1.
         **/
        private int SearchEmptyRow()
        {
            int emptyRow = -1;
            
            foreach (DataGridViewRow row in gridStructure.Rows)
            {
                Object dataField = row.Cells[0].Value;

                if (dataField == null)
                {                    
                    emptyRow = row.Index + 1;
                    break;
                }
            }

            return emptyRow;
        }

        /**
         * Verificar se os items inseridos na coluna Default Value
         * sao integer. Se todos forem integer, retorna -1.
         **/
        private int SearchIntegerRow()
        {
            int integerRow = -1;

            foreach (DataGridViewRow row in gridStructure.Rows)
            {
                Object defaultValue = row.Cells[2].Value;
                int result = 0;

                if (defaultValue != null)
                {
                    if ((!defaultValue.ToString().Trim().Equals("")) && (!int.TryParse(defaultValue.ToString(), out result)))
                    {
                        integerRow = row.Index + 1;
                        break;
                    }
                }
            }

            return integerRow;
        }

        #endregion        
        
    }
}
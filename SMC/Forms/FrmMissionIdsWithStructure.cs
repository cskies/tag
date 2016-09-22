/**
 * @file 	    FrmMissionIdsWithStructure.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    23/10/2009
 * @note	    Modificado em 27/12/2013 por Fabricio.
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
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters;
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmReportStructures
     * Formulario generico para cadastros de IDs com estruturas associadas.
     * Atualmente utilizado para Event Reports, TC Failure Codes e Report Definitions.
     **/
    public partial class FrmMissionIdsWithStructure : DockContent
    {
        #region Atributos Internos

        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private DbMissionIdsWithStructure dbStructure = new DbMissionIdsWithStructure();
        private int fieldId = 0; // usado para selecionar rows no gridDatabase
        private int keyNumberOfBits = 0;
        private int numberOfCharacters = 0;

        private String myTitle = "";
        private String myDescriptionCaption = "";
        private String myTableName = "";
        private String myKeyField = "";
        private String myDescriptionField = "";
        private String myElementTableName = "";
        private String myElementDescriptionField = "";
        private Microsoft.Reporting.WinForms.ReportDataSource RptDataSource = new Microsoft.Reporting.WinForms.ReportDataSource();

        #endregion

        #region Construtores

        public FrmMissionIdsWithStructure()
        {
            InitializeComponent();
        }

        public FrmMissionIdsWithStructure(String title,
                                            String keyCaption,
                                            String descriptionCaption,
                                            String tableName, // nome da tabela
                                            String keyField, // atributo key da tabela
                                            String descriptionField, // atributo description da tabela
                                            String structureTableName, // nome da tabela da estrutura
                                            String keyFieldStructure, // atributo key da tabela da estrutura
                                            String structureElementId, // campo com o id do elemento que compoe a estrutura
                                            String elementTableName, // nome da tabela de elementos
                                            String elementDescriptionField, // campo com a descricao do elemento
                                            int numberOfBitsKey,
                                            int descriptionLength)
        {
            InitializeComponent();

            // configuracao da tela
            this.Text = title;

            if (this.Text.Equals("Report Definitions"))
            {
                btNewDataField.Text = "New Parameter";
            }

            keyNumberOfBits = numberOfBitsKey;
            numberOfCharacters = Math.Pow(2, numberOfBitsKey).ToString().Length;

            lblKey.Text = keyCaption + ":";
            lblDescription.Text = descriptionCaption + ":";
            lblStructure.Text = title.Substring(0, (title.Length - 1)) + " Structure";
            txtKey.MaxLength = numberOfCharacters;
            txtDescription.MaxLength = descriptionLength;

            // configura a interface com o banco de dados
            dbStructure.MissionIdTable = tableName;
            dbStructure.MissionIdKeyField = keyField;
            dbStructure.MissionIdDescriptionField = descriptionField;
            dbStructure.TableStructure = structureTableName;
            dbStructure.KeyFieldStructure = keyFieldStructure;
            dbStructure.StructureElementId = structureElementId;

            // atributos a serem usado pelo arquivo mission_ids.h
            myTitle = title;
            myDescriptionCaption = descriptionCaption;
            myTableName = tableName;
            myKeyField = keyField;
            myDescriptionField = descriptionField;
            myElementTableName = elementTableName;
            myElementDescriptionField = elementDescriptionField;

            ChangeMode(Mode.browsing);
        }

        #endregion

        #region Metodos Privados

        /**Preenche o gridDatabase**/
        private void RefreshGrid()
        {
            gridDatabase.Columns.Clear();

            //seleciona a tabela de acordo com o nome da tela
            gridDatabase.DataSource = dbStructure.GetTableByScreenName(this.Text);
            gridDatabase.Columns[0].HeaderText = lblKey.Text.Substring(0, lblKey.Text.Length - 1);
            gridDatabase.Columns[1].HeaderText = lblDescription.Text.Substring(0, lblDescription.Text.Length - 1);
            gridDatabase.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (this.Text == "Report Definitions")
            {
                gridDatabase.Columns[2].HeaderText = "Total Structure Length";
                gridDatabase.Columns[2].Width = 200;
            }
        }

        /**Preenche o gridDatabase**/
        private bool FillGridStructure(int rid)
        {
            gridStructure.Rows.Clear();
            
            String sql = @"select ('[' + dbo.f_zero(a." + dbStructure.StructureElementId + @", " + numberOfCharacters + ") + '] ' + a." + myElementDescriptionField + @")
                           from   " + myElementTableName + @" a inner join " + dbStructure.TableStructure + @" b 
                           on     a." + dbStructure.StructureElementId + @" = b." + dbStructure.StructureElementId + @"
                           where  b." + dbStructure.KeyFieldStructure + " = '" + rid + @"'
                           order by b.position";

            DataTable tblStructure = DbInterface.GetDataTable(sql);

            if (tblStructure == null)
            {
                return false; // retorna false porque ocorreu algum erro ao acessar o banco
            }

            if (tblStructure.Rows.Count > 0)
            {
                // Preenche os itens do comboBoxColumn
                FillComboColStructure();

                // cria o numero de rows necessarios
                gridStructure.Rows.Add(tblStructure.Rows.Count);

                int index = 0;

                // Adiciona os rows da estrutura
                foreach (DataRow row in tblStructure.Rows)
                {
                    // Seta o valor correspondente em cada celula
                    gridStructure.Rows[index].Cells[0].Value = row[0].ToString();

                    index++;
                }
            }

            return true;
        }

        private void MoveRow(bool up)
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

        /**Atualiza as permissoes dos botoes da area da estrutura do relato**/
        private void RefreshButtons()
        {
            btAdd.Enabled = true;
            btRemove.Enabled = true;
            btNewDataField.Enabled = true;
            btUp.Enabled = true;
            btDown.Enabled = true;

            if (gridStructure.Rows.Count == 0)
            {
                btRemove.Enabled = false;
                btUp.Enabled = false;
                btDown.Enabled = false;
                btNewDataField.Enabled = false;
            }
            else if (gridStructure.Rows.Count == 1)
            {
                btUp.Enabled = false;
                btDown.Enabled = false;
            }
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
                if (dbStructure.Key.Equals(""))
                {
                    MessageBox.Show("There are empty fields! \n\nFill them and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                if (dbStructure.Description.Equals(""))
                {
                    MessageBox.Show("There are empty fields! \n\nFill them and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return false;
                }

                // verifica se o campo key nao passa do total aceito
                int maximumKey = (int)Math.Pow(2, keyNumberOfBits) - 1;

                if (int.Parse(dbStructure.Key) > maximumKey)
                {
                    MessageBox.Show("The limit of field " + lblKey.Text + " is " + maximumKey.ToString() + "! \n\nCorrect it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                int parsedKey; // nao usado

                if (!int.TryParse(dbStructure.Key, out parsedKey))
                {
                    MessageBox.Show(gridDatabase.Columns[0].HeaderText + " is not numeric! \n\nCorrect it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                // verifica se o codigo do relato ja existe na base
                sql = "select isnull(count(" + dbStructure.MissionIdKeyField + "), 0) as equalKey " +
                      "from " + dbStructure.MissionIdTable + " " +
                      "where " + dbStructure.MissionIdKeyField + " = dbo.f_regularString('" + dbStructure.Key + "')";

                int numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a " + gridDatabase.Columns[0].HeaderText + " with this " + gridDatabase.Columns[0].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                // agora verifica se ha descricao similar (equivalente)
                sql = "select isnull(count(" + dbStructure.MissionIdKeyField + "), 0) as similarValue " +
                      "from " + dbStructure.MissionIdTable + " " +
                      "where dbo.f_regularString(" + dbStructure.MissionIdDescriptionField + ") = dbo.f_regularString('" + dbStructure.Description + "')";

                numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a record with an equal or equivalent " + gridDatabase.Columns[1].HeaderText + "! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return false;
                }
            }
            else // currentMode = editing
            {
                if (dbStructure.Description.Equals(""))
                {
                    MessageBox.Show("The " + gridDatabase.Columns[1].HeaderText + " is empty! \n\nFill it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return false;
                }

                // verifica se ha descricao similar (equivalente)
                sql = "select isnull(count(" + dbStructure.MissionIdKeyField + "), 0) as similarValue " +
                      "from " + dbStructure.MissionIdTable + " " +
                      "where dbo.f_regularString(" + dbStructure.MissionIdDescriptionField + ") = dbo.f_regularString('" + dbStructure.Description + "')" +
                      "and " + dbStructure.MissionIdKeyField + " <> " + dbStructure.Key;

                int numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a record with an equal or equivalent " + gridDatabase.Columns[1].HeaderText + "! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return false;
                }
            }

            // Validar o gridStructure. Verificar se nele existem rows vazios.
            if (gridStructure.Rows.Count != 0)
            {
                foreach (DataGridViewRow row in gridStructure.Rows)
                {
                    Object data = row.Cells[0].Value;

                    if (data == null)
                    {
                        MessageBox.Show("There are empty fields in Structure! \n\nFill them and try again.",
                                        "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        gridStructure.CurrentRow.Selected = false;
                        gridStructure.Rows[row.Index].Selected = true;

                        return false;
                    }
                }
            }

            return true;
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
                        btExport.Enabled = false;

                        fieldId = 0;
                    }
                    else
                    {
                        btEdit.Enabled = true;
                        btDelete.Enabled = true;
                        btExport.Enabled = true;

                        if (fieldId != 0)
                        {
                            // Seleciona o row correspondente
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
                    txtKey.Enabled = false;
                    gridStructure.Rows.Clear();

                    if (newMode == Mode.inserting)
                    {
                        txtKey.ResetText();
                        txtDescription.ResetText();
                        txtKey.Enabled = true;
                        txtKey.Focus();
                    }
                    else // editing
                    {
                        if (btEdit.Enabled == false)
                        {
                            return;
                        }

                        txtKey.Enabled = false;
                        txtKey.Text = gridDatabase.CurrentRow.Cells[0].Value.ToString();

                        fieldId = int.Parse(txtKey.Text); // para selecionar o row correspondente.                                      

                        txtDescription.ResetText();
                        txtDescription.Text = gridDatabase.CurrentRow.Cells[1].Value.ToString();

                        // preenche o gridStructure
                        FillGridStructure(int.Parse(gridDatabase.CurrentRow.Cells[0].Value.ToString()));
                    }

                    RefreshButtons(); // atualiza as permissoes dos botoes da area da estrutura do relato

                    btNew.Enabled = false;
                    btEdit.Enabled = false;
                    btDelete.Enabled = false;
                    btConfirm.Enabled = true;
                    btCancel.Enabled = true;
                    btReport.Enabled = false;
                    btRefresh.Enabled = false;
                    btExport.Enabled = false;

                    currentMode = newMode;

                    tabControl1.SelectedIndex = 1;

                    if (newMode == Mode.inserting)
                    {
                        txtKey.Focus();
                    }
                    else
                    {
                        // Coloca o foco e seleciona o text description
                        txtDescription.Focus();
                        txtDescription.SelectAll();
                    }
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

                    //Define o reportdatasource a ser carregado
                    if (Text == "Event and Error Reports")
                    {
                        event_reportsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                        event_reportsTableAdapter.Fill(dataSetApids.event_reports);
                        RptDataSource.Name = "DataSetApids_event_reports";
                        RptDataSource.Value = this.eventreportsBindingSource;
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptEventReports.rdlc";
                        reportViewer1.LocalReport.DataSources[0] = RptDataSource;
                    }
                    else if (Text == "TC Failure Codes")
                    {
                        tc_failure_codesTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                        tc_failure_codesTableAdapter.Fill(dataSetApids.tc_failure_codes);
                        RptDataSource.Name = "DataSes_tc_failue_codes";
                        RptDataSource.Value = this.tc_failure_codesBindingSource;
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptTcFailureCodes.rdlc";
                        reportViewer1.LocalReport.DataSources[0] = RptDataSource;
                    }
                    else if (Text == "Report Definitions")
                    {
                        reportdefinitionsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                        reportdefinitionsTableAdapter.Fill(dataSetApids.reportdefinitions);
                        RptDataSource.Name = "DataSetApids_reportdefinitions";
                        RptDataSource.Value = this.reportdefinitionsBindingSource;
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptReportDefinitions.rdlc";
                        reportViewer1.LocalReport.DataSources[0] = RptDataSource;
                    }

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

                    reportViewer1.RefreshReport();
                    tabControl1.SelectedIndex = 2;
                    Report = false;

                    break;
                }
            }
        }

        private bool Report = true;

        #endregion

        #region Metodos Publicos

        /**Preenche os itens da coluna colStructure. Deve ser public para 
         * ser acessado pela tela de Data Fields**/
        public bool FillComboColStructure()
        {
            // Limpa o combo, caso tenha itens
            colComboStructureFields.Items.Clear();

            // Busca os campos da estrutura para serem setados nos combos do gridStructure
            String sqlCombo = @"select '[' + dbo.f_zero(" + dbStructure.StructureElementId + ", " + numberOfCharacters + ") + '] ' + " + myElementDescriptionField +
                              @" from " + myElementTableName + " where " + dbStructure.StructureElementId + " <> 0";

            DataTable table = DbInterface.GetDataTable(sqlCombo);

            if (table == null)
            {
                return false; // retorna false porque ocorreu algum erro ao acessar o banco
            }

            foreach (DataRow row in table.Rows)
            {
                colComboStructureFields.Items.Add(row[0].ToString());
            }

            return true;
        }

        #endregion

        #region Eventos da Interface Grafica

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
            dbStructure.Key = gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();
            dbStructure.Description = gridDatabase[1, gridDatabase.CurrentRow.Index].Value.ToString();

            if (MessageBox.Show("Are you sure you want to delete the " + this.Text.Substring(0, this.Text.Length - 1) + " " + dbStructure.Key + ", '" + dbStructure.Description + "' ?",
                                "Please confirm deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            int indexSelected = gridDatabase.CurrentRow.Index;

            if (!dbStructure.Delete())
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

        private void btConfirm_Click(object sender, EventArgs e)
        {
            // forca que edicoes pendentes sejam finalizadas
            gridStructure.EndEdit();

            dbStructure.Key = txtKey.Text.Trim();
            dbStructure.Description = txtDescription.Text.Trim();

            if (!ValidateData())
            {
                return;
            }

            // Verifica se existe itens na estrutura, caso tenha, pegue-os.
            if (gridStructure.Rows.Count > 0)
            {
                Object[] structure = new Object[gridStructure.Rows.Count];
                int index = 0;

                foreach (DataGridViewRow row in gridStructure.Rows)
                {
                    int dataFieldId = int.Parse(row.Cells[0].Value.ToString().Substring(1, numberOfCharacters));
                    structure[index] = (object)dataFieldId;
                    index++;
                }

                dbStructure.Structure = structure;
            }
            else
            {
                dbStructure.Structure = null;
            }

            // Executa as acoes de insert ou update.
            if (currentMode == Mode.inserting)
            {
                if (!dbStructure.Insert())
                {
                    return;
                }

                fieldId = int.Parse(txtKey.Text.ToString().Trim());
                ChangeMode(Mode.inserting);
            }
            else
            {
                if (!dbStructure.Update())
                {
                    return;
                }

                fieldId = int.Parse(txtKey.Text.ToString().Trim());
                ChangeMode(Mode.browsing);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            // Preenche os combos da coluna Data Fields
            if (gridStructure.Rows.Count == 0)
            {
                if (!FillComboColStructure())
                {
                    return;
                }
            }

            gridStructure.Rows.Add(1);

            RefreshButtons();
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            gridStructure.Rows.Remove(gridStructure.CurrentRow);
            RefreshButtons();
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            MoveRow(true);
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            MoveRow(false);
        }

        private void btNewDataField_Click(object sender, EventArgs e)
        {
            if (this.Text.Equals("Report Definitions"))
            {
                FrmHousekeepingParameters frmHousekParam = new FrmHousekeepingParameters(this);
                frmHousekParam.ShowDialog();
            }
            else
            {
                FrmDataFields frmDataFields = new FrmDataFields(this);
                frmDataFields.ShowDialog();
            }
        }

        private void FrmReportStructures_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmReportStructures_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(551, 404);
            }
        }

        private void gridDatabase_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
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

        private void btExport_Click(object sender, EventArgs e)
        {
            FileHandling.ExportMissionIds(myTitle, myDescriptionField, myTableName, myKeyField);
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
    }
}
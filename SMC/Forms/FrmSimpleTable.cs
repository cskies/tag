/**
 * @file 	    FrmSimpleTable.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 09/04/2012 por Ayres.
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
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.Datasets.DataSetApidsTableAdapters;
using System.Data.OleDb;
using Microsoft.ReportingServices.ReportRendering;
using Inpe.Subord.Comav.Egse.Smc.Datasets;
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSimpleTable
     * Formulario para os cadastros simples.
     **/
    public partial class FrmSimpleTable : DockContent
    {
        #region Atributos Internos

        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private int keyNumberOfBits = 0;

        // Variavel para manter o controle da linha selecionada no grid durante mudanca de modos
        private int codeSelected = 0;
        private DbSimpleTable simpleTable = null;

        private String myTitle = "";
        private String myDescriptionCaption = "";
        private String myTableName = "";
        private String myKeyField = "";
        private String myDescriptionField = "";
        private Microsoft.Reporting.WinForms.ReportDataSource RptDataSource = new Microsoft.Reporting.WinForms.ReportDataSource();

        #endregion

        #region Construtores

        public FrmSimpleTable()
        {
            InitializeComponent();
        }

        /** 
         * Construtor que recebe todos os parametros e configura
         * a tela para determinado cadastro. 
         **/
        public FrmSimpleTable(String title,
                                String keyCaption,
                                String descriptionCaption,
                                String tableName,
                                String keyField,
                                String descriptionField,
                                int numberOfBitsKey,
                                int descriptionLength)
        {
            InitializeComponent();

            // configuracao da tela
            this.Text = title;

            keyNumberOfBits = numberOfBitsKey;
            int numberOfCharacters = Math.Pow(2, numberOfBitsKey).ToString().Length;

            lblKey.Text = keyCaption + ":";
            lblDescription.Text = descriptionCaption + ":";
            txtKey.MaxLength = numberOfCharacters;
            txtDescription.MaxLength = descriptionLength;

            // configura a interface com o banco de dados
            simpleTable = new DbSimpleTable(tableName, keyField, descriptionField);

            // atributos a serem usado para a exportacao dos mission ids
            myTitle = title;
            myDescriptionCaption = descriptionCaption;
            myTableName = tableName;
            myKeyField = keyField;
            myDescriptionField = descriptionField;

            ChangeMode(Mode.browsing);
        }

        #endregion

        #region Tratamento de Eventos da Interface

        private void btNew_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.inserting);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.editing);
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            simpleTable.Key = txtKey.Text.Trim();
            simpleTable.Description = txtDescription.Text.Trim();

            if (!ValidateData())
            {
                return;
            }

            if (currentMode == Mode.inserting)
            {
                if (!simpleTable.Insert())
                {
                    return; // sai sem voltar ao modo browsing
                }

                codeSelected = int.Parse(simpleTable.Key);

                // Como eh insercao, continua no mesmo modo
                ChangeMode(Mode.inserting);
            }
            else
            {
                if (!simpleTable.Update())
                {
                    return; // sai sem voltar ao modo browsing
                }

                codeSelected = int.Parse(simpleTable.Key);

                // Volta ao modo browsing
                ChangeMode(Mode.browsing);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
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

        private void btRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            simpleTable.Key = gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();
            simpleTable.Description = gridDatabase[1, gridDatabase.CurrentRow.Index].Value.ToString();

            if (MessageBox.Show("Are you sure you want to delete the " + this.Text.Substring(0, this.Text.Length - 1) + " " + simpleTable.Key + ", '" + simpleTable.Description + "' ?",
                                "Please confirm deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            int indexSelected = gridDatabase.CurrentRow.Index;

            if (!simpleTable.Delete())
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
        private void FrmSimpleTable_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmSimpleTable_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(540, 380);
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

        private void FrmSimpleTable_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Metodos Privados

        /** 
         * Metodo responsavel por modificar a interface de acordo com
         * a operacao que o usuario ira fazer.
         **/
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
                            btReport.Enabled = false;

                            codeSelected = 0;
                        }
                        else
                        {
                            btEdit.Enabled = true;
                            btDelete.Enabled = true;
                            btExport.Enabled = true;
                            btReport.Enabled = true;

                            // Selecionar o item no gridDatabase
                            if (codeSelected != 0)
                            {
                                foreach (DataGridViewRow row in gridDatabase.Rows)
                                {
                                    if (codeSelected == int.Parse(row.Cells[0].Value.ToString()))
                                    {
                                        gridDatabase.Rows[row.Index].Cells[0].Selected = true;
                                        break;
                                    }
                                }

                                codeSelected = 0;
                            }
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
                case Mode.editing:
                case Mode.inserting:
                    {
                        currentMode = newMode;

                        if (newMode == Mode.inserting)
                        {
                            txtKey.Enabled = true;
                            txtKey.Text = "";
                            txtDescription.Text = "";
                            txtKey.Focus();
                        }
                        else // editing
                        {
                            if (btEdit.Enabled == false)
                            {
                                return;
                            }

                            txtKey.Enabled = false;
                            txtKey.Text = gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();
                            txtDescription.Text = gridDatabase[1, gridDatabase.CurrentRow.Index].Value.ToString();
                            txtDescription.Focus();

                            codeSelected = int.Parse(gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString());
                        }

                        btNew.Enabled = false;
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btConfirm.Enabled = true;
                        btCancel.Enabled = true;
                        btReport.Enabled = false;
                        btRefresh.Enabled = false;
                        btExport.Enabled = false;

                        tabControl1.SelectedIndex = 1;

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
                        btExport.Enabled = false;

                        // TODO: precisamos passar o nome da missao ao relatorio (com base na selecao do usuario ao iniciar o SMC)
                        // como um parametro, substituindo 'Amazonia-1' no rodape

                        //Define o reportdatasource a ser carregado
                        if (Text == "Packet Store IDs")
                        {
                            packet_store_idsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                            packet_store_idsTableAdapter.Fill(dataSetApids.packet_store_ids);
                            RptDataSource.Name = "DataSetApids_packet_store_ids";
                            RptDataSource.Value = this.packet_store_idsBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptPacketStoreIds.rdlc";
                            reportViewer1.LocalReport.DataSources[0] = RptDataSource;
                        }
                        else if (Text == "Memory IDs")
                        {
                            memory_idsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                            memory_idsTableAdapter.Fill(dataSetApids.memory_ids);
                            RptDataSource.Name = "DataSetApids_memory_ids";
                            RptDataSource.Value = this.memory_idsBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptMemoryIds.rdlc";
                            reportViewer1.LocalReport.DataSources[0] = RptDataSource;
                        }
                        else if (Text == "Output Lines IDs")
                        {
                            output_line_idsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                            output_line_idsTableAdapter.Fill(dataSetApids.output_line_ids);
                            RptDataSource.Name = "DataSetApids_output_line_ids";
                            RptDataSource.Value = this.output_line_idsBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptOutputLineIds.rdlc";
                            reportViewer1.LocalReport.DataSources[0] = RptDataSource;
                        }
                        else if (Text == "Services")
                        {
                            servicesTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                            servicesTableAdapter.Fill(dataSetApids.services);
                            RptDataSource.Name = "DataSetApids_services";
                            RptDataSource.Value = this.servicesBindingSource;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Inpe.Subord.Comav.Egse.Smc.Reports.RptServices.rdlc";
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
                        Microsoft.Reporting.WinForms.ReportParameter MissionName = new Microsoft.Reporting.WinForms.ReportParameter("MissionName", MissionNameString);
                        reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { MissionName });
                        
                        reportViewer1.RefreshReport();
              
                        tabControl1.SelectedIndex = 2;

                        Report = false;                                                   

                        break;
                    }
            }
        }

        private bool Report = true;

        /** Atualiza o conteudo do grid. **/
        private void RefreshGrid()
        {
            gridDatabase.Columns.Clear();
            gridDatabase.DataSource = simpleTable.GetTable();

            gridDatabase.Columns[0].HeaderText = lblKey.Text.Substring(0, lblKey.Text.Length - 1);
            gridDatabase.Columns[1].HeaderText = lblDescription.Text.Substring(0, lblDescription.Text.Length - 1);

            gridDatabase.Columns[0].Width = 110;
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
                if (simpleTable.Key.Equals("") || simpleTable.Description.Equals(""))
                {
                    MessageBox.Show("There are empty fields! \n\nFill them and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                // verifica se o campo key nao passa do total aceito
                int maximumKey = (int)Math.Pow(2, keyNumberOfBits) - 1;

                if (int.Parse(simpleTable.Key) > maximumKey)
                {
                    MessageBox.Show("The limit of field " + lblKey.Text + " is " + maximumKey.ToString() + "! \n\nCorrect it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                int parsedKey; // nao usado

                if (!int.TryParse(simpleTable.Key, out parsedKey))
                {
                    MessageBox.Show(gridDatabase.Columns[0].HeaderText + " is not numeric! \n\nCorrect it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                // verifica se o codigo ja existe na base
                sql = "select isnull(count(" + simpleTable.KeyField + "), 0) as equalKey " +
                      "from " + simpleTable.TableName + " " +
                      "where " + simpleTable.KeyField + " = dbo.f_regularString('" + simpleTable.Key + "')";

                int numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a " + gridDatabase.Columns[1].HeaderText + " with this " + gridDatabase.Columns[0].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }

                // agora verifica se ha descricao similar (equivalente)
                sql = "select isnull(count(" + simpleTable.KeyField + "), 0) as similarValue " +
                      "from " + simpleTable.TableName + " " +
                      "where dbo.f_regularString(" + simpleTable.DescriptionField + ") = dbo.f_regularString('" + simpleTable.Description + "')";

                numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a record with an equal or equivalent " + gridDatabase.Columns[0].HeaderText + "! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }
            }
            else // currentMode = editing
            {
                if (simpleTable.Description.Equals(""))
                {
                    MessageBox.Show("The " + gridDatabase.Columns[0].HeaderText + " is empty! \n\nFill it and try again.",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();
                    return false;
                }

                // verifica se ha descricao similar (equivalente)
                sql = "select isnull(count(" + simpleTable.KeyField + "), 0) as similarValue " +
                      "from " + simpleTable.TableName + " " +
                      "where dbo.f_regularString(" + simpleTable.DescriptionField + ") = dbo.f_regularString('" + simpleTable.Description + "')" +
                      "and " + simpleTable.KeyField + " <> " + simpleTable.Key;

                int numberOfRecords = (int)DbInterface.ExecuteScalar(sql);

                if (numberOfRecords > 0)
                {
                    MessageBox.Show("There is a record with an equal or equivalent " + gridDatabase.Columns[0].HeaderText + "! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtKey.Focus();
                    txtKey.SelectAll();
                    return false;
                }
            }

            return true;
        }

        #endregion

        
    }
}

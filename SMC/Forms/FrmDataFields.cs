/**
 * @file 	    FrmDataFields.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    19/07/2009
 * @note	    Modificado em 24/10/2013 por Bruna.
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
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmDataFields
     * Formulario de cadastro de Data Fields
     */
    public partial class FrmDataFields : DockContent
    {
        #region Atributos Internos

        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private DbDataField dbDataField = new DbDataField();
        private int fieldId = 0;
        private FrmSubtypes frmSubtypes = null;
        private FrmMissionIdsWithStructure frmReportStr = null;

        #endregion

        #region Construtor

        public FrmDataFields(Form frm)
        {
            InitializeComponent();

            //@attention Melhorar esta chamada para ser acessado por outros Forms.
            if (frm != null)
            {
                if (frm.Text.Equals("Subtypes"))
                {
                    frmSubtypes = (FrmSubtypes)frm;
                }
                else if (frm.Text.Equals("Report IDs") ||
                         frm.Text.Equals("TC Failure Codes") ||
                         frm.Text.Equals("Event and Error Reports"))
                {
                    frmReportStr = (FrmMissionIdsWithStructure)frm;
                }

                ChangeMode(Mode.inserting);
            }
            else
            {
                ChangeMode(Mode.browsing);
                gridDatabase.Focus();
            }
        }

        #endregion

        #region Metodos Privados

        /** Atualiza o gridDatabase com os Data Fields. **/
        private void RefreshGrid()
        {
            gridDatabase.Columns.Clear();

            String sql = @"select 
                               data_field_id, data_field_name,
                            case 
                               when variable_length = 0 then convert(varchar, number_of_bits) else '[Variable]' end 
                            as NumberOfBits,
                            case
                               when type_is_bool = 1 then 'Boolean'
                               when type_is_numeric = 1 then 'Numeric'
                               when type_is_raw_hex = 1 then 'Raw Hex'
                               when type_is_list = 1 then 'List'
                               when type_is_table = 1 then 'Table' end
                            as FieldType,
                            case
                               when table_name is NULL then '' else table_name end as TableName,
                            case
                               when list_id is NULL then 0 else list_id end as listId
                            from 
                               data_fields
                            where
                               data_field_id <> 0
                            order by data_field_id";

            gridDatabase.DataSource = DbInterface.GetDataTable(sql);

            gridDatabase.Columns[0].HeaderText = "Field Id";
            gridDatabase.Columns[1].HeaderText = "Field Name";
            gridDatabase.Columns[2].HeaderText = "Number Of Bits";
            gridDatabase.Columns[3].HeaderText = "Field Type";

            gridDatabase.Columns[0].Width = 80;
            gridDatabase.Columns[1].Width = 200;
            gridDatabase.Columns[2].Width = 80;
            gridDatabase.Columns[4].Visible = false; // table name
            gridDatabase.Columns[5].Visible = false; // list id

            gridDatabase.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /** Preenche o cmbListId. **/
        private bool FillCmbListId()
        {
            cmbListId.Items.Clear();
            String qryList = @"select '[' + dbo.f_zero(list_id, 3) + '] ' + list_description as Description from data_field_lists_header where list_id <> 0";
            DataTable tblList = DbInterface.GetDataTable(qryList);

            if (tblList == null)
            {
                return false;
            }

            foreach (DataRow row in tblList.Rows)
            {
                cmbListId.Items.Add(row["Description"].ToString());
            }

            if (cmbListId.Items.Count != 0)
            {
                cmbListId.SelectedIndex = 0;
            }

            return true;
        }

        /**
         * Valida os dados da tela do modo editing verificando se podem ser inseridos no banco.
         * Verifica dados em banco, ja existentes no banco de dados, etc. Tanto para insert quanto
         * para update.
         **/
        private bool ValidateData()
        {
            if (!txtFieldName.Text.Trim().Equals(""))
            {
                // Verificar se ja existe FieldName
                String queryEqual = "select isnull(count(data_field_id), 0) as equalFieldName from data_fields where data_field_name = dbo.f_regularString('" + txtFieldName.Text.Trim() + "')";

                // Se o usuario editar um data field, data field name podera ser o mesmo
                if (currentMode == Mode.editing)
                {
                    queryEqual += " and data_field_id <> " + txtFieldId.Text.Trim();
                }

                int alreadyExistFieldName = (int)DbInterface.ExecuteScalar(queryEqual);

                if (alreadyExistFieldName == 0)
                {
                    // caso o campo Field Type seja Table
                    if (cmbFieldType.Text.Equals("Table"))
                    {
                        String tableName = txtTableName.Text.Trim();

                        if (tableName.Equals(""))
                        {
                            MessageBox.Show("The field " + lblTableName.Text + " is empty ! \n\nFill it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            txtTableName.Focus();
                            txtTableName.SelectAll();
                            return false;
                        }

                        // Verificar se o nome da tabela especificado existe no banco de dados
                        String sql = "select isnull(count(*), 0) as tableExist from sys.tables where (type = 'U') and (lob_data_space_id = 0) and (name = '" + txtTableName.Text.Trim() + "')";
                        int existTableName = (int)DbInterface.ExecuteScalar(sql);

                        if (existTableName != 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("The table '" + txtTableName.Text.Trim() + "' does not exist ! \n\nFill other and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            txtTableName.Focus();
                            txtTableName.SelectAll();
                            return false;
                        }
                    }

                    // caso o campo Field Type seja List
                    if (cmbFieldType.Text.Equals("List"))
                    {
                        // verifica se o cmbListId esta vazio
                        if (cmbListId.Text.Trim().Equals(""))
                        {
                            MessageBox.Show("The field " + lblListId.Text + " is empty ! \n\nFill it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            cmbListId.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The Data Field '" + txtFieldName.Text.Trim() + "' already exist ! ! \n\nFill other and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    txtFieldName.Focus();
                    txtFieldName.SelectAll();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("The field " + lblFieldName.Text + " is empty ! \n\nFill it and try again.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                txtFieldName.Focus();
                txtFieldName.SelectAll();
                return false;
            }

            return true;
        }

        /** Faz a verificacao se o row selecionado podera ser deletado. **/
        private bool CanDelete()
        {
            // Verificar se o data field esta sendo usado por alguma estrutura da tabela subtype_structure.
            String sql = "select distinct '['+convert (varchar,(service_type))+'.'+ convert(varchar,service_subtype)+']'  as type_subtype from subtype_structure where subtype_structure.data_field_id = " + gridDatabase.CurrentRow.Cells[0].Value.ToString();
            DataTable subtypeStr = DbInterface.GetDataTable(sql);

            // Se sua estrutura for copiada por outro(s) subtype(s), imprime qual(s) subtype(s) a copia.
            String subtypes = "";

            foreach (DataRow row in subtypeStr.Rows)
            {
                // Concatena os subtypes que copiam a estrutura do subtype em questao
                subtypes += row[0].ToString() + ", ";
            }

            //verificar se o data field esta sendo usado por alguma estrutura da tabela tc_failure_code_structure
            String sqlTcFailureCodes = "select distinct convert (varchar,(tc_failure_code_structure.tc_failure_code)) as type_tcFailureCode from data_fields inner join tc_failure_code_structure on tc_failure_code_structure.data_field_id = " + gridDatabase.CurrentRow.Cells[0].Value.ToString();
            DataTable tcFailureCodesStr = DbInterface.GetDataTable(sqlTcFailureCodes);

            // Se sua estrutura for copiada por outro(s) tc_failure_code(s), imprime qual(s) tc_failure_code(s) a copia.
            String tcFailureCodes = "";

            foreach (DataRow row in tcFailureCodesStr.Rows)
            {
                tcFailureCodes += row[0].ToString() + ", ";
            }

            //verificar se o data field esta sendo usado por alguma estrutura da tabela event_report_structure
            String sqlTcEventAndErrorsReports = "select distinct convert (varchar,(event_report_structure.rid)) as type_EventReports from data_fields inner join event_report_structure on event_report_structure.data_field_id = " + gridDatabase.CurrentRow.Cells[0].Value.ToString();
            DataTable eventAndErrorsReportsStr = DbInterface.GetDataTable(sqlTcEventAndErrorsReports);

            // Se sua estrutura for copiada por outro(s) tc_failure_code(s), imprime qual(s) tc_failure_code(s) a copia.
            String tcEventAndErrorsReports = "";

            foreach (DataRow row in eventAndErrorsReportsStr.Rows)
            {
                tcEventAndErrorsReports += row[0].ToString() + ", ";
            }

            //se nao tiver nenhuma tabela relacionada, pode excluir
            if ((subtypeStr.Rows.Count == 0) & (tcFailureCodesStr.Rows.Count == 0) & ((eventAndErrorsReportsStr.Rows.Count == 0)))
            {
                return true;
            }

            //possui apenas subtypes relacionados
            if ((subtypeStr.Rows.Count != 0) & (tcFailureCodes.Length == 0) & (tcEventAndErrorsReports.Length == 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Subtype(s):  " + subtypes.Substring(0, subtypes.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            }//possui apenas Failure Codes relacionados
            else if ((tcFailureCodes.Length != 0) & (tcEventAndErrorsReports.Length == 0) & (subtypeStr.Rows.Count == 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Failure Code(s):  " + tcFailureCodes.Substring(0, tcFailureCodes.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            } //possui apenas Event and Errors Reports relacionados
            else if ((tcFailureCodes.Length == 0) & (tcEventAndErrorsReports.Length != 0) & (subtypeStr.Rows.Count == 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Event and Errors Report(s):  " + tcEventAndErrorsReports.Substring(0, tcEventAndErrorsReports.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            } //tem subtipos e Failure Codes relacinados
            else if ((tcFailureCodes.Length != 0) & (tcEventAndErrorsReports.Length == 0) & (subtypeStr.Rows.Count != 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Subtype(s):  " + subtypes.Substring(0, subtypes.Length - 2) + "/n TC Failure Code(s):  " + tcFailureCodes.Substring(0, tcFailureCodes.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            } //tem subtipos e Event and Error Reports relacinados
            else if ((tcFailureCodes.Length == 0) & (tcEventAndErrorsReports.Length != 0) & (subtypeStr.Rows.Count != 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Subtype(s):  " + subtypes.Substring(0, subtypes.Length - 2) + "/n Event and Errors Report(s):  " + tcEventAndErrorsReports.Substring(0, tcEventAndErrorsReports.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            } //tem Failure Codes e Event and Error Reports relacinados
            else if ((tcFailureCodes.Length != 0) & (tcEventAndErrorsReports.Length != 0) & (subtypeStr.Rows.Count == 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Failure Code(s):  " + tcFailureCodes.Substring(0, tcFailureCodes.Length - 2) + "/n Event and Errors(s):  " + tcEventAndErrorsReports.Substring(0, tcEventAndErrorsReports.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            } //tem subtipos, Failure Codes e Event and Error Reports relacinados
            else if ((tcEventAndErrorsReports.Length != 0) & (tcFailureCodes.Length != 0) & (subtypeStr.Rows.Count != 0))
            {
                MessageBox.Show("You cannot remove this data field because it is used in: \n Subtype(s):  " + subtypes.Substring(0, subtypes.Length - 2) + "\n TC Failure Code(s):  " + tcFailureCodes.Substring(0, tcFailureCodes.Length - 2) + " \n Event and Error Report(s):  " + tcEventAndErrorsReports.Substring(0, tcEventAndErrorsReports.Length - 2),
                            "Record cannot be deleted",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            }
            return false;
        }

        /**
         * Monta os dados do gridList em uma Matriz de Objetos. Ao chegar no metodo InsertListOfValues,
         * a mesma eh desmontada para a insercao de cada item no banco de dados.
         **/
        private Object[,] MountList()
        {
            int col = gridList.Columns.Count;
            int line = gridList.Rows.Count;
            Object[,] list = new Object[line, col];

            foreach (DataGridViewRow row in gridList.Rows)
            {
                Object value = row.Cells[0].Value;
                Object text = row.Cells[1].Value;

                if ((value == null) || (value.ToString().Trim().Equals("")))
                {
                    list[row.Index, 0] = "null";
                }
                else
                {
                    list[row.Index, 0] = row.Cells[0].Value.ToString().Trim();
                }

                if ((text == null) || (text.ToString().Trim().Equals("")))
                {
                    list[row.Index, 1] = "null";
                }
                else
                {
                    list[row.Index, 1] = row.Cells[1].Value.ToString().Trim();
                }
            }

            return list;
        }

        /**
         * Valida os valores do gridList. Valores em branco, null ou 
         * que nao sao inteiros, nao sao aceitos.
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
                            btReport.Enabled = false;

                            fieldId = 0;
                        }
                        else
                        {
                            btEdit.Enabled = true;
                            btDelete.Enabled = true;
                            btReport.Enabled = true;
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
                        btRefresh.Enabled = true;

                        currentMode = newMode;

                        tabControl1.SelectedIndex = 0;
                        gridDatabase.Focus();

                        break;
                    }
                case Mode.inserting:
                case Mode.editing:
                    {
                        // se o CmbService nao for preenchido, sai e continue no modo browsing.                        
                        if (!FillCmbListId())
                        {
                            return;
                        }
                        currentMode = newMode;
                        txtFieldId.Enabled = false;
                        gridList.Rows.Clear();

                        if (newMode == Mode.inserting)
                        {
                            // Busca o ultimo codigo na tabela data_fields
                            string sql = "select isnull(max(data_field_id), 0) as maxDataField from data_fields";
                            int maxDataField = (int)DbInterface.ExecuteScalar(sql);
                            txtFieldId.Text = (maxDataField + 1).ToString();

                            txtFieldName.ResetText();
                            cmbFieldType.SelectedIndex = 1; // setando o item deste combo, o seu evento ja eh disparado.
                            numBits.Value = 8; // valor default
                            numBits.Focus();
                        }
                        else // editing
                        {
                            if (btEdit.Enabled == false)
                            {
                                return;
                            }

                            txtFieldId.Enabled = false;
                            txtFieldId.Text = gridDatabase.CurrentRow.Cells[0].Value.ToString();

                            fieldId = int.Parse(txtFieldId.Text); // para selecionar o row correspondente.                        

                            // forca a atualizaca do indice e o carregamento do grid de lista se necessario
                            cmbFieldType.SelectedIndex = -1;

                            String fieldType = gridDatabase.CurrentRow.Cells[3].Value.ToString();
                            cmbFieldType.SelectedIndex = cmbFieldType.FindString(fieldType);

                            String numberOfBits = gridDatabase.CurrentRow.Cells[2].Value.ToString();

                            if (numberOfBits.Contains("[Variable"))
                            {
                                chkVariableSize.Checked = true;
                            }
                            else
                            {
                                chkVariableSize.Checked = false;
                                numBits.Value = int.Parse(numberOfBits);
                            }

                            if (gridDatabase.RowCount != 0)
                            {
                                fieldId = int.Parse(gridDatabase.CurrentRow.Cells[0].Value.ToString()); // para selecionar o row correspondente.
                            }

                            txtFieldName.ResetText();
                            txtFieldName.Text = gridDatabase.CurrentRow.Cells[1].Value.ToString();
                        }

                        btNew.Enabled = false;
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btConfirm.Enabled = true;
                        btCancel.Enabled = true;
                        btReport.Enabled = false;
                        btRefresh.Enabled = false;

                        // Coloca o foco e seleciona o text do numBits
                        numBits.Focus();
                        numBits.Select(0, numBits.Value.ToString().Length);

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

                        int indexOfParameterName = 0;

                        for (int i = 0; i < Properties.Settings.Default.db_connections_names.Count; i++)
                        {
                            if (Properties.Settings.Default.db_connection_string.ToString() == Properties.Settings.Default.db_connections_strings[i].ToString())
                            {
                                indexOfParameterName = i;
                            }
                        }

                        String sql = "dbo.sp_DataFieldsWithValues_final";
                        DbInterface.ExecuteNonQuery(sql);

                        data_fieldsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();

                        //define o nome da missão como string
                        String MissionNameString = Properties.Settings.Default.db_connections_names[indexOfParameterName];

                        //adiciona os parametros para o rodape
                        ReportParameter MissionName = new ReportParameter("MissionName", MissionNameString);
                        reportViewer1.LocalReport.SetParameters(new ReportParameter[] { MissionName });

                        data_fieldsTableAdapter.Fill(dataSetApids.data_fields);
                        reportViewer1.RefreshReport();
                        tabControl1.SelectedIndex = 2;
                        Report = false;

                        break;
                    }

            }
        }

        private bool Report = true;
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
        }

        #endregion

        #region Eventos

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
            if (MessageBox.Show("Are you sure you want to delete the Data Field " + gridDatabase.CurrentRow.Cells[0].Value.ToString() + ", '" + gridDatabase.CurrentRow.Cells[1].Value.ToString() + "' ?",
                                "Please Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            if (CanDelete())
            {
                dbDataField.FieldId = gridDatabase.CurrentRow.Cells[0].Value.ToString();
                int indexSelected = gridDatabase.CurrentRow.Index;

                if (!dbDataField.Delete())
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
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

            dbDataField.FieldId = txtFieldId.Text;
            dbDataField.FieldName = txtFieldName.Text;
            dbDataField.NumberOfBits = numBits.Value.ToString();
            dbDataField.FieldType = cmbFieldType.Text;
            dbDataField.TableName = txtTableName.Text;

            if (cmbFieldType.SelectedIndex == 3)
            {
                dbDataField.ListId = (cmbListId.SelectedItem.ToString().Substring(1, 3));
            }

            dbDataField.VariableLength = chkVariableSize.Checked;

            if (currentMode == Mode.inserting)
            {
                if (!dbDataField.Insert())
                {
                    return; // sai porque deu erro.
                }

                // Verificar se este Form foi chamado pela tela de Subtypes,
                // ou pela tela de Reports.
                // Caso seja, deve atualizar os combos da coluna Data Fields 
                // do gridList e fechar este Form Data Fields.
                //@attention: melhorar isso.
                if (frmSubtypes != null)
                {
                    // Atualiza os combos do chamador e fecha o form
                    frmSubtypes.FillCmbColDataFields();
                    this.Close();

                    return;
                }
                else if (frmReportStr != null)
                {
                    frmReportStr.FillComboColStructure();
                    this.Close();

                    return;
                }

                fieldId = int.Parse(txtFieldId.Text);

                // Continua no mesmo modo
                ChangeMode(Mode.inserting);
            }
            else if (currentMode == Mode.editing)
            {
                if (!dbDataField.Update())
                {
                    return; // sai sem voltar ao modo browsing
                }

                fieldId = int.Parse(txtFieldId.Text);

                // Volta ao modo browsing
                ChangeMode(Mode.browsing);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (frmSubtypes != null)
            {
                this.Close();
                return;
            }

            ChangeMode(Mode.browsing);
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

        private void gridDatabase_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbListId.Enabled = false;
            cmbListId.Items.Clear();
            gridList.Rows.Clear();

            btNewList.Enabled = false;

            txtTableName.Enabled = false;
            txtTableName.ResetText();

            chkVariableSize.Enabled = false;
            chkVariableSize.Checked = false;
            numBits.Enabled = true;

            if (cmbFieldType.Text.Equals("List"))
            {
                cmbListId.Enabled = true;
                btNewList.Enabled = true;

                if (!FillCmbListId())
                {
                    return;
                }

                if (cmbListId.Items.Count == 0)
                {
                    return;
                }

                if (currentMode == Mode.editing)
                {
                    string listId = gridDatabase.CurrentRow.Cells[5].Value.ToString();

                    if (listId.Equals("0") || listId.Equals(""))
                    {
                        cmbListId.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbListId.SelectedIndex = cmbListId.FindString(Formatting.FormatCode(int.Parse(listId), 3));
                    }
                }
                else if (currentMode == Mode.inserting)
                {
                    cmbListId.SelectedIndex = 0;
                }
            }
            else if (cmbFieldType.Text.Equals("Table"))
            {
                txtTableName.Enabled = true;

                if (currentMode == Mode.editing)
                {
                    txtTableName.Text = gridDatabase.CurrentRow.Cells[4].Value.ToString();
                }

                txtTableName.Select();
            }
            else if (cmbFieldType.Text.Equals("Raw Hex"))
            {
                chkVariableSize.Enabled = true;
            }
        }

        private void cmbListId_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridList.Rows.Clear();
            int retorno = int.Parse(cmbListId.SelectedItem.ToString().Substring(1, 3));
            String sql = "select list_value, list_text from data_field_lists where list_id =" + retorno;
            DataTable table = DbInterface.GetDataTable(sql);

            int index = 0;

            if (table.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                gridList.Rows.Add(1);
                gridList.Rows[index].Cells[0].Value = row[0].ToString();
                gridList.Rows[index].Cells[1].Value = row[1].ToString();

                index++;
            }

            // Nesse caso, o grid nao pode ser editavel. 
            // Eh editavel apenas ao inserir uma nova lista de valores.
            gridList.Columns[0].ReadOnly = true;
            gridList.Columns[1].ReadOnly = true;

            // Nesse caso, a selecao dos rows eh FullRowSelect.
            // No caso da insercao da lista de valores, eh CellSelec.
            gridList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridList.Rows[0].Cells[0].Selected = true;
        }

        private void col_SelectedIndexChanged(object sender, EventArgs e)
        { }
        private void btNewList_Click(object sender, EventArgs e)
        {
            FrmDataFieldsLists frm = new FrmDataFieldsLists(this);
            frm.ShowDialog();

            if (frm.RetId() == true)
            {
                FillCmbListId();
                int txtid = (int.Parse(frm.txtid())) - 1;
                cmbListId.SelectedIndex = cmbListId.FindString(Formatting.FormatCode(txtid, 3));
            }
        }

        private void btCanc_Click(object sender, EventArgs e)
        {
            btNewList.Enabled = true;
            cmbListId.Enabled = true;

            gridList.Rows.Clear();
            FillCmbListId();

            btConfirm.Enabled = true;
            btCancel.Enabled = true;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            // Para evitar que o usuario acesse o page errado pelo teclado
            if (currentMode != Mode.browsing)
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            // Para evitar que o usuario acesse o page errado pelo teclado
            if (currentMode == Mode.browsing)
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        /**
         * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
         * para permitir a operacao da tela pelo usuario.
         **/
        private void FrmDataFields_KeyDown(object sender, KeyEventArgs e)
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
                btDelete.Enabled = true;
            }

            gridList.Rows[gridList.Rows.Count - 1].Cells[0].Selected = true;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            gridList.Rows.Remove(gridList.CurrentRow);

            if (gridList.RowCount == 1)
            {
                btDelete.Enabled = false;
            }
        }

        private void btUp_Click_1(object sender, EventArgs e)
        {
            MoveRow(true);
        }

        private void FrmDataFields_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(704, 441);
            }
        }

        private void chkVariableSize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVariableSize.Checked)
            {
                numBits.Value = 64;
                numBits.Enabled = false;
            }
            else
            {
                numBits.Enabled = true;
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
    }
}

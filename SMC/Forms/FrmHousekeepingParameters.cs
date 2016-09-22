/**
 * @file 	    FrmHousekeepingParameters.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Michel Andrade
 * @date	    01/03/2010
 * @note	    Modificado em 29/10/2012 por Ayres
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
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmHousekeepingParameters
     * Formulario para o cadastro de parametros de housekeeping e de diagnose
     **/
    public partial class FrmHousekeepingParameters : DockContent
    {
        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private int seletedIndexRow;
        string sql;
        private FrmMissionIdsWithStructure frmReportDefinition = null;

        public FrmHousekeepingParameters(Form frm)
        {
            InitializeComponent();

            //@attention Melhorar esta chamada para ser acessado por outros Forms.
            if (frm != null)
            {
                frmReportDefinition = (FrmMissionIdsWithStructure)frm;
                ChangeMode(Mode.inserting);
            }
            else
            {
                ChangeMode(Mode.browsing);
                gridHousekeeping.Focus();
            }
        }

        private void RefreshGrid()
        {
            gridHousekeeping.Columns.Clear();

            sql = @"select 
                     parameter_id, 
                     parameter_description, 
                     data_type,
                     show_as_hex
                   from parameters";

            gridHousekeeping.DataSource = DbInterface.GetDataTable(sql);

            gridHousekeeping.Columns[0].HeaderText = "Parameter ID";
            gridHousekeeping.Columns[1].HeaderText = "Parameter Description";
            gridHousekeeping.Columns[2].HeaderText = "Data Type";
            gridHousekeeping.Columns[3].HeaderText = "Show as Hex";

            gridHousekeeping.Columns[0].Width = 150;
            gridHousekeeping.Columns[1].Width = 300;
            gridHousekeeping.Columns[2].Width = 150;
            gridHousekeeping.Columns[3].Width = 150;
            gridHousekeeping.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             
            gridHousekeeping.Refresh();            
        }

        private void ChangeMode(Mode newMode)
        {
            switch (newMode)
            {
                case Mode.browsing:
                {
                    RefreshGrid();                    

                    if (gridHousekeeping.RowCount == 0)
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
                    btReport.Enabled = true;
                    btRefresh.Enabled = true;

                    currentMode = newMode;

                    tabControl1.SelectedIndex = 0;

                    if (gridHousekeeping.RowCount > 0)
                    {
                        if (gridHousekeeping.RowCount == 1)
                        {
                            gridHousekeeping.Rows[0].Cells[0].Selected = true;
                        }
                        else
                        {
                            if (seletedIndexRow == gridHousekeeping.RowCount)
                            {
                                gridHousekeeping.Rows[gridHousekeeping.RowCount - 1].Cells[0].Selected = true;
                            }
                            else if (seletedIndexRow < gridHousekeeping.RowCount)
                            {
                                gridHousekeeping.Rows[seletedIndexRow].Cells[0].Selected = true;
                            }
                        }
                    }

                    break;
                }
                case Mode.inserting:
                case Mode.editing:
                {
                    currentMode = newMode;

                    // Armazena a posicao atual da linha do grid
                    if (gridHousekeeping.RowCount > 0)
                    {
                        seletedIndexRow = gridHousekeeping.CurrentRow.Index;
                    }
                                       
                    if (newMode == Mode.editing)
                    {
                        txtParameterId.Enabled = false;
                        txtParameterId.Text = gridHousekeeping.CurrentRow.Cells[0].Value.ToString();
                        txtParameterDescription.Text = gridHousekeeping.CurrentRow.Cells[1].Value.ToString();
                        chkShowAsHex.Checked = (bool)gridHousekeeping.CurrentRow.Cells[3].Value;
                        
                        sql = @"select data_type 
                                from parameters
                                where parameter_id = " + txtParameterId.Text;

                        string dataType = DbInterface.GetDataTable(sql).Rows[0]["data_type"].ToString();

                        cmbDataType.SelectedItem = dataType.ToLower();                            
                    }
                    else //inserting
                    {
                        txtParameterId.Enabled = true;

                        // Busca o ultimo codigo na tabela parameters
                        string sql = "select isnull(max(parameter_id), 0) as maxParameterId from parameters";
                        int maxDataField = (int)DbInterface.ExecuteScalar(sql);
                        txtParameterId.Text = (maxDataField + 1).ToString();

                        txtParameterDescription.ResetText();
                        cmbDataType.SelectedIndex = 3;
                        txtParameterId.Focus();
                        chkShowAsHex.Checked = false;
                    }

                    btNew.Enabled = false;
                    btEdit.Enabled = false;
                    btDelete.Enabled = false;
                    btConfirm.Enabled = true;
                    btCancel.Enabled = true;
                    btReport.Enabled = false;
                    btRefresh.Enabled = false;

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

                    // TODO: precisamos passar o nome da missao ao relatorio (com base na selecao do usuario ao iniciar o SMC)
                    // como um parametro, substituindo 'Amazonia-1' no rodape

                    parametersTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                    parametersTableAdapter.Fill(dataSetApids.parameters);

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
            seletedIndexRow = gridHousekeeping.CurrentRow.Index;

            if (MessageBox.Show("Are you sure you want to delete the Parameter ID " + "'" + gridHousekeeping.CurrentRow.Cells[0].Value.ToString() + "'?",
                                "Please Confirm Deletion!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            sql = @"delete parameters
                    where parameter_id = '" + gridHousekeeping.CurrentRow.Cells[0].Value.ToString() + "'";

            DbInterface.ExecuteScalar(sql);

            ChangeMode(Mode.browsing);
            gridHousekeeping.Refresh();

            if (seletedIndexRow == 0)
            {
                gridHousekeeping.Rows[0].Cells[0].Selected = true;
            }
            else if (gridHousekeeping.RowCount > 0)
            {
                gridHousekeeping.Rows[seletedIndexRow - 1].Cells[0].Selected = true;
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            string showAsHex;

            if (chkShowAsHex.Checked == true)
            {
                showAsHex = "1";
            }
            else
            {
                showAsHex = "0";
            }

            if (ValidateFields())
            {
                if (currentMode == Mode.inserting)
                {
                    if (IsParameterIdRegistered())
                    {
                        MessageBox.Show("There is a " + gridHousekeeping.Columns[1].HeaderText + " with this " + gridHousekeeping.Columns[0].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        txtParameterId.Focus();
                        txtParameterDescription.SelectAll();
                    }
                    else if (IsParameterDescRegistered())
                    {
                        MessageBox.Show("There is a " + gridHousekeeping.Columns[0].HeaderText + " with this " + gridHousekeeping.Columns[1].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        txtParameterDescription.Focus();
                        txtParameterDescription.SelectAll();
                    }
                    else
                    {
                        sql = @"insert into parameters 
                                (parameter_id, parameter_description, data_type, show_as_hex)
                                 values('" + txtParameterId.Text + "'," +
                                       "'" + txtParameterDescription.Text + "'," +
                                       "'" + cmbDataType.SelectedItem.ToString() + "', " +
                                       showAsHex + ")";

                        DbInterface.ExecuteNonQuery(sql);

                        gridHousekeeping.Refresh();

                        // Busca o ultimo codigo na tabela parameters
                        sql = "select isnull(max(parameter_id), 0) as maxParameterId from parameters";
                        int maxDataField = (int)DbInterface.ExecuteScalar(sql);
                        txtParameterId.Text = (maxDataField + 1).ToString();

                        // Verificar se este Form foi chamado pela tela de Report Definitions
                        // Caso seja, deve atualizar os combos da coluna HK Parameters do 
                        // GridList dele, e fechar este Form
                        if (frmReportDefinition != null)
                        {
                            // Atualiza os combos do chamador e fecha o form
                            frmReportDefinition.FillComboColStructure();
                            this.Close();

                            return;
                        }

                        txtParameterDescription.Text = "";
                        chkShowAsHex.Checked = false;
                        txtParameterId.Focus();                        
                    }
                }
                else // editing
                {                   
                    if (!IsParameterDescRegistered())
                    {
                        sql = @"update parameters
                                set parameter_description = '" + txtParameterDescription.Text + "'," +
                                   "data_type = '" + cmbDataType.SelectedItem.ToString() + "', " +
                                   "show_as_hex = " + showAsHex + " " + 
                               "where parameter_id = " + txtParameterId.Text;

                        DbInterface.ExecuteNonQuery(sql);

                        gridHousekeeping.Refresh();

                        ChangeMode(Mode.browsing);

                        // Seta a posicao anterior do grid
                        gridHousekeeping.Rows[seletedIndexRow].Cells[0].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("There is a " + gridHousekeeping.Columns[0].HeaderText + " with this " + gridHousekeeping.Columns[1].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            // Verificar se este Form foi chamado pela tela de Report Definitions
            // Caso seja, deve fechar este Form
            if (frmReportDefinition != null)
            {
                this.Close();
                return;
            }
            else
            {
                ChangeMode(Mode.browsing);
            }
        }        

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }
        
        private bool ValidateFields()
        {
            bool emptyField = false;

            if (txtParameterId.Text.Trim().Equals("")) // verifica se existem campos vazios
            {
                emptyField = true;
                txtParameterId.Focus();
                txtParameterId.SelectAll();
            }            
            else if (txtParameterDescription.Text.Trim().Equals("")) // verifica se existem campos vazios
            {
                emptyField = true;
                txtParameterDescription.Focus();
                txtParameterId.SelectAll();
            }
            else if (txtParameterDescription.Text.Trim().Equals("")) // verifica se existem campos vazios
            {
                emptyField = true;
                txtParameterDescription.Focus();
                txtParameterId.SelectAll();
            }

            if (emptyField)
            {
                MessageBox.Show("There are empty fields!\n\nFill them and try again.", "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                return false;
            }            
            else // verifica se o campo 'Parementer ID' e numerico
            {
                try
                {
                    int parameterId = Convert.ToInt32(txtParameterId.Text);

                    if (parameterId > 65535)
                    {
                        MessageBox.Show("The field 'Paramenter ID' must be lower than 65536!\n\nCorrect it and try again.", "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                        return false;
                    }

                    return true;
                }
                catch
                {
                    MessageBox.Show("Parameter ID must be numeric!\n\nCorrect it and try again.", "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    txtParameterId.Focus();
                    txtParameterId.SelectAll();

                    return false;
                }
            }
        }
            
        private bool IsParameterIdRegistered()
        {
            // verifica se o valor do campo 'Paramenter ID'ja esta registrado
            sql = @"select count(parameter_id)
                    from parameters
                    where parameter_id = " + txtParameterId.Text;

            bool isParameterIdRegistered = Convert.ToBoolean(DbInterface.ExecuteScalar(sql));

            return isParameterIdRegistered;
        }
        
        private bool IsParameterDescRegistered()
        {
            sql = @"select 
	                    isnull(count(parameter_id), 0) as similarValue 
                    from 
	                    parameters
                    where 
	                    dbo.f_regularString(parameter_description) = 
                            dbo.f_regularString('" + txtParameterDescription.Text + "') and " +
	                    "parameter_id <> " + txtParameterId.Text;

            bool IsParameterDescRegistered = Convert.ToBoolean(DbInterface.ExecuteScalar(sql));

            return IsParameterDescRegistered; 
        }

        private void gridHousekeeping_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, EventArgs.Empty);
        }

        private void FrmHousekeeping_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid antes de chamar o Edit.
            if ((gridHousekeeping.Focused) && (e.KeyCode == Keys.Enter))
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

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataType.SelectedIndex == 0)
            {
                chkShowAsHex.Enabled = false;
                chkShowAsHex.Checked = false;
            }
            else
            {
                chkShowAsHex.Enabled = true;
            }
        }

        private void gridHousekeeping_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}

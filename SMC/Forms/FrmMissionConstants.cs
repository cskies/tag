/**
 * @file 	    FrmMissionConstants.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Michel Andrade
 * @date	    30/09/2009
 * @note	    Modificado em 22/12/2011 por Ayres.
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
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Reporting.WinForms;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmMissionConstants
     * Formulario para o cadastro de Mission Constants.
     **/
    public partial class FrmMissionConstants : DockContent
    {
        #region Atributos
        
        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private DbMissionConstant dbMissionConstant = new DbMissionConstant();
        private String fieldId = "";

        #endregion

        #region Construtor

        public FrmMissionConstants()
        {
            InitializeComponent();
            ChangeMode(Mode.browsing);
        }

        #endregion

        #region Metodos privados

        /** Atualiza o gridMissonConstants com os Mission Constants. **/
        private void RefreshGrid()
        {
            gridMissonConstants.Columns.Clear();

            String sql = @"select 
                                mission_constant, 
                                constant_description, 
                                defined_in,             
                                constant_value, 
                                is_flight_sw_constant
                           from 
                                mission_constants";

            gridMissonConstants.DataSource = DbInterface.GetDataTable(sql);            

            gridMissonConstants.Columns[0].HeaderText = "Mission Constant";
            gridMissonConstants.Columns[1].HeaderText = "Constant Description";
            gridMissonConstants.Columns[2].HeaderText = "Defined in";
            gridMissonConstants.Columns[3].HeaderText = "Constant Value";
            gridMissonConstants.Columns[4].HeaderText = "Is Flight SW Constant";

            gridMissonConstants.Columns[0].Width = 200;
            gridMissonConstants.Columns[1].Visible = false;
            gridMissonConstants.Columns[2].Width = 350;
            gridMissonConstants.Columns[3].Width = 200;
            gridMissonConstants.Columns[4].Width = 80;
            gridMissonConstants.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            gridMissonConstants.Refresh();
        }
        
        private void ChangeMode(Mode newMode)
        {
            switch (newMode)
            {
                case Mode.browsing:
                {
                    RefreshGrid();

                    if (gridMissonConstants.RowCount == 0)
                    {
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btExport.Enabled = false;

                        fieldId = "";
                    }
                    else
                    {
                        btEdit.Enabled = true;
                        btDelete.Enabled = true;
                        btExport.Enabled = true;

                        if (!fieldId.Equals(""))
                        {
                            // Seleciona o row correspondewnte
                            foreach (DataGridViewRow row in gridMissonConstants.Rows)
                            {
                                String gridFieldId = row.Cells[0].Value.ToString().ToUpper();                                

                                if (fieldId.ToUpper().Equals(gridFieldId))
                                {
                                    gridMissonConstants.Rows[row.Index].Cells[0].Selected = true;
                                    break;
                                }
                            }

                            fieldId = "";
                        }
                    }

                    btNew.Enabled = true;
                    btConfirm.Enabled = false;
                    btCancel.Enabled = false;
                    btReport.Enabled = true;
                    btRefresh.Enabled = true;

                    currentMode = newMode;

                    tabControl1.SelectedIndex = 0;
                    gridMissonConstants.Focus();

                    break;
                }
                
                case Mode.inserting:
                case Mode.editing:
                {
                    currentMode = newMode;

                    if (newMode == Mode.editing)
                    {
                        txtMissionConstant.Enabled = false;
                        txtMissionConstant.Text = gridMissonConstants.CurrentRow.Cells[0].Value.ToString();
                        txtDescription.Text = gridMissonConstants.CurrentRow.Cells[1].Value.ToString();
                        txtDefinedIn.Text = gridMissonConstants.CurrentRow.Cells[2].Value.ToString();
                        txtConstantValue.Text = gridMissonConstants.CurrentRow.Cells[3].Value.ToString();
                        chkUsedByFlightSW.Checked = (bool)gridMissonConstants.CurrentRow.Cells[4].Value;

                        // para selecionar o row correspondente.
                        if (gridMissonConstants.RowCount != 0)
                        {
                            fieldId = gridMissonConstants.CurrentRow.Cells[0].Value.ToString().ToUpper();
                        }
                    }
                    else //inserting
                    {
                        txtMissionConstant.Enabled = true;
                        txtMissionConstant.ResetText();
                        txtDescription.ResetText();
                        txtDefinedIn.ResetText();
                        txtConstantValue.ResetText();
                        chkUsedByFlightSW.Checked = false;
                        txtMissionConstant.Focus();
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

                    mission_constantsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();
                    mission_constantsTableAdapter.Fill(dataSetApids.mission_constants);

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

        private bool ValidateData()
        {
            bool emptyField = false;
            
            if (txtMissionConstant.Text.Trim().Equals("")) // verifica se existem campos vazios
            {
                emptyField = true;
                txtMissionConstant.Focus();
            }
            else if (txtMissionConstant.Text.Trim().Contains(" ")) // verifica se nao existem espacos entre os caracteres da string
            {
                MessageBox.Show("The Mission Constant cannot have space between caracters.", "Inconsistent Data",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation);

                txtMissionConstant.Focus();
                txtMissionConstant.SelectAll();

                return false;
            }            
            else if (txtDescription.Text.Trim().Equals(""))
            {
                emptyField = true;
                txtDescription.Focus();
            }
            else if (txtDefinedIn.Text.Trim().Equals(""))
            {
                emptyField = true;
                txtDefinedIn.Focus();
            }
            else if (txtConstantValue.Text.Trim().Equals(""))
            {
                emptyField = true;
                txtConstantValue.Focus();
            }

            if (emptyField)
            {
                //notificar user
                MessageBox.Show("There are empty fields!\n\nFill them and try again.", "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                return false;
            }

            int existMissionConstants = 0;

            // Se o usuario editar no modo inserting
            if (currentMode == Mode.inserting)
            {
                // Verificar se ja existe a MissionConstant
                String queryEqual = "select count(mission_constant) from mission_constants where mission_constant =  dbo.f_regularString('" + txtMissionConstant.Text.Trim() + "')";
                existMissionConstants = (int)DbInterface.ExecuteScalar(queryEqual);
            }

            // Se Mission Constant nao existir no banco de dados
            if (existMissionConstants != 0)
            {
                MessageBox.Show("There is a " + gridMissonConstants.Columns[1].HeaderText + " with this " + gridMissonConstants.Columns[0].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                txtMissionConstant.Focus();
                txtMissionConstant.SelectAll();
                                
                return false;
            }
            
            if (chkUsedByFlightSW.Checked == true)
            {                
                int value = 0;

                // verificar se o constant value eh numeric
                if (int.TryParse(txtConstantValue.Text.Trim(), out value))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("The Constant Value must be numeric if it is a Flight SW constant. Correct it and try again.", "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                    txtConstantValue.Focus();
                    txtConstantValue.SelectAll();

                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /**
         * Este metodo escreve a descricao e a definicao da constante na forma de
         * comentario para cada constant adicionada no arquivo mission_constant.h
         **/
        private void PrintComment(String description, String definedIn, TextWriter file)
        {
            int lineLenght = 70;
            int breakLine = lineLenght;
            bool newLine = true;

            file.WriteLine();
            file.WriteLine("/**");

            for (int i = 0; i < description.Length; i++)
            {
                if (newLine)
                {
                    file.Write(" * ");
                }

                if (description.Substring(i, 1) != "\r")
                {
                    if (i < lineLenght || description.Substring(i, 1) != " ")
                    {
                        file.Write(description.Substring(i, 1));
                        newLine = false;
                    }
                    else
                    {
                        file.WriteLine(description.Substring(i, 1));
                        lineLenght = i + breakLine;
                        newLine = true;
                    }
                }
                else
                {
                    file.WriteLine();
                    newLine = true;
                    lineLenght = i + breakLine;
                    i++;
                }
            }

            file.WriteLine();
            file.WriteLine(" *");
            file.WriteLine(" * Defined in " + definedIn + ".");
            file.WriteLine(" **/");
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
            if (MessageBox.Show("Are you sure you want to delete the Mission Constant " + "'" + gridMissonConstants.CurrentRow.Cells[0].Value.ToString() + "'?",
                                "Please Confirm Deletion!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            dbMissionConstant.MissionConstant = gridMissonConstants.CurrentRow.Cells[0].Value.ToString();
            int indexSelected = gridMissonConstants.CurrentRow.Index;

            if (dbMissionConstant.Delete())
            {
                ChangeMode(Mode.browsing);

                if (gridMissonConstants.RowCount == 0)
                {
                    return;
                }
                
                // Setar o row seguinte aqui
                if (indexSelected == gridMissonConstants.RowCount)
                {
                    gridMissonConstants.Rows[indexSelected - 1].Cells[0].Selected = true;
                }
                else
                {
                    gridMissonConstants.Rows[indexSelected].Cells[0].Selected = true;
                }
            }            
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            if(ValidateData())
            {
                dbMissionConstant.MissionConstant = txtMissionConstant.Text.Trim();
                dbMissionConstant.ConstantDescription = txtDescription.Text.Trim();
                dbMissionConstant.DefinedIn = txtDefinedIn.Text.Trim();
                dbMissionConstant.ConstantValue = txtConstantValue.Text.Trim();
                dbMissionConstant.IsFlightSWConstant = chkUsedByFlightSW.Checked;
               
                if (currentMode == Mode.inserting)
                {
                    if (!dbMissionConstant.Insert())
                    {
                        return; // sai porque deu erro.
                    }

                    fieldId = txtMissionConstant.Text.ToUpper().Trim();
                                   
                    // continua no mesmo modo
                    ChangeMode(Mode.inserting);               
                }
                else if (currentMode == Mode.editing)
                {
                    if (!dbMissionConstant.Update())
                    {
                        return; // sai sem voltar ao modo browsing
                    }

                    fieldId = txtMissionConstant.Text.ToUpper().Trim();
                    
                    // Volta ao modo browsing
                    ChangeMode(Mode.browsing);
                }
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

        private void gridMissonConstants_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }
        
        /** Este evento trata o tamanho da tela ao ser desacoplada do layout Docking **/
        private void FrmMissionConstants_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(710, 393);
            }
        }

        /**
        * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
        * para permitir a operacao da tela pelo usuario.
        **/
        private void FrmMissionConstants_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid antes de chamar o Edit.
            if ((gridMissonConstants.Focused) && (e.KeyCode == Keys.Enter))
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
                    if (txtDescription.Focused)
                    {
                        return;
                    }

                    btConfirm_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.Escape) && (btCancel.Enabled))
                {
                    btCancel_Click(this, new EventArgs());
                }
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Properties.Settings.Default.flight_sw_file_path + "\\mission_constants.h"))
                {
                    if (MessageBox.Show("Header file already exist! Do you want to overwrite it?",
                                        "Overwrite Header File?",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                TextWriter file = new StreamWriter(Properties.Settings.Default.flight_sw_file_path + "\\mission_constants.h");

                file.WriteLine("/**");
                file.WriteLine(" * @file\tmission_constants.h");
                file.WriteLine(" * @note\tCopyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo");
                file.WriteLine(" * @author\tSUBORD / SMC");
                file.WriteLine(" * @date\t" + DateTime.Now.ToString());
                file.WriteLine(" * @brief\tConstantes de missao definidos nos padroes ECSS.");
                file.WriteLine(" **/");
                file.WriteLine();
                file.WriteLine("#ifndef MISSION_CONSTANTS_H_");
                file.WriteLine("#define MISSION_CONSTANTS_H_");

                DataTable missionConstTbl = DbMissionConstant.GetFswConstants();
                
                for (int x = 0; x < missionConstTbl.Rows.Count; x++)
                {
                    String missionConstant = missionConstTbl.Rows[x][0].ToString();
                    String description = missionConstTbl.Rows[x][1].ToString();
                    String definedIn = missionConstTbl.Rows[x][2].ToString();
                    String constantValue = missionConstTbl.Rows[x][3].ToString();

                    // imprime o comentario da constant
                    PrintComment(description, definedIn, file);

                    // imprime a constant propriamente dita
                    file.WriteLine("const unsigned int " + missionConstant + " = " + constantValue + ";");
                }

                file.WriteLine();
                file.WriteLine("#endif /* MISSION_CONSTANTS_H_ */");
                file.WriteLine();

                file.Close();

                MessageBox.Show("Header file was exported successfully!",
                                "Header File Created",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error when creating the file: \n\n" + ex.Message,
                                "File Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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

     

        private void FrmMissionConstants_Load(object sender, EventArgs e)
        {
        }
    }
}

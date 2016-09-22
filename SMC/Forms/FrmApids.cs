/**
 * @file 	    FrmApids.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto
 * @date	    22/12/2011
 * @note	    Modificado em 28/02/2012 por Ayres.
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
using Inpe.Subord.Comav.Egse.Smc.Comm;
using System.Data.OleDb;
using Microsoft.ReportingServices.ReportRendering;
using Microsoft.Reporting.WinForms;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmApids : DockContent
    {
        #region Atributos

        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private String fieldId = "";
        private DbApids dbApid = new DbApids();

        #endregion

        #region Construtor

        public FrmApids()
        {
            InitializeComponent();
            ChangeMode(Mode.browsing);
        }

        #endregion

        # region Metodos Privados

        private void RefreshGrid()
        {
            gridApid.Columns.Clear();

            String sql = @"select 
                                apid, 
                                application_name, 
                                vcid
                           from 
                                apids";

            gridApid.DataSource = DbInterface.GetDataTable(sql);

            gridApid.Columns[0].HeaderText = "APID";
            gridApid.Columns[1].HeaderText = "Application Name";
            gridApid.Columns[2].HeaderText = "VCID";

            gridApid.Columns[0].Width = 200;
            gridApid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridApid.Columns[2].Width = 200;

            gridApid.Refresh();
        }
        
        private void ChangeMode(Mode newMode)
        {
            switch (newMode)
            {
                case Mode.browsing:
                {
                    RefreshGrid();

                    if (gridApid.RowCount == 0)
                    {
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btExport.Enabled = false;
                        btReport.Enabled = false;

                        fieldId = "";
                    }
                    else
                    {
                        btEdit.Enabled = true;
                        btDelete.Enabled = true;
                        btExport.Enabled = true;
                        btReport.Enabled = true;

                        if (!fieldId.Equals(""))
                        {
                            // Seleciona o row correspondewnte
                            foreach (DataGridViewRow row in gridApid.Rows)
                            {
                                String gridFieldId = row.Cells[0].Value.ToString().ToUpper();

                                if (fieldId.ToUpper().Equals(gridFieldId))
                                {
                                    gridApid.Rows[row.Index].Cells[0].Selected = true;
                                    break;
                                }
                            }

                            fieldId = "";
                        }
                    }

                    btNew.Enabled = true;
                    btConfirm.Enabled = false;
                    btCancel.Enabled = false;
                    btRefresh.Enabled = true;

                    currentMode = newMode;

                    tabControl1.SelectedIndex = 0;
                    gridApid.Focus();

                    break;
                }
                case Mode.inserting:
                case Mode.editing:
                {
                    currentMode = newMode;

                    if (newMode == Mode.editing)
                    {
                        txtApid.Enabled = false;
                        txtApid.Text = gridApid.CurrentRow.Cells[0].Value.ToString();
                        txtApplicationName.Text = gridApid.CurrentRow.Cells[1].Value.ToString();
                        txtVcid.Text = gridApid.CurrentRow.Cells[2].Value.ToString();
                        
                        // para selecionar o row correspondente.
                        if (gridApid.RowCount != 0)
                        {
                            fieldId = gridApid.CurrentRow.Cells[0].Value.ToString().ToUpper();
                        }
                    }
                    else //inserting
                    {
                        txtApid.Enabled = true;
                        txtApid.ResetText();
                        txtApplicationName.ResetText();
                        txtVcid.ResetText();
                        txtApid.Focus();
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
                    apidsTableAdapter.Fill(dataSetApids.apids);
                    reportViewer1.RefreshReport();

                    tabControl1.SelectedIndex = 2;
                    break;
                }
            }
        }

        private bool ValidateData()
        {
            bool emptyField = false;

            if (txtApid.Text.Trim().Equals("")) // verifica se existem campos vazios
            {
                emptyField = true;
                txtApid.Focus();
            }
            else if (txtApplicationName.Text.Trim().Contains(" ")) // verifica se nao existem espacos entre os caracteres da string
            {
                MessageBox.Show("The Application Name cannot have space between caracters.", "Inconsistent Data",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation);

                txtApid.Focus();
                txtApid.SelectAll();

                return false;
            }
            else if (txtApplicationName.Text.Trim().Equals(""))
            {
                emptyField = true;
                txtApplicationName.Focus();
            }
            else if (txtVcid.Text.Trim().Equals(""))
            {
                emptyField = true;
                txtVcid.Focus();
            }
          

            if (emptyField)
            {
                //notificar user
                MessageBox.Show("There are empty fields!\n\nFill them and try again.", "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                return false;
            }

            int existApids = 0;

            int conversionOut = 0;

            // verificar se o Apid value is numeric
            if (int.TryParse(txtApid.Text.Trim(), out conversionOut) == false)
            {
                MessageBox.Show("The APID Value must be numeric. Correct it and try again.", "Inconsistent Data",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

                txtVcid.Focus();
                txtVcid.SelectAll();

                return false;
            }

            // verificar se o Vcid value is numeric
            if (int.TryParse(txtVcid.Text.Trim(), out conversionOut) == false)
            {
                MessageBox.Show("The VCID Value must be numeric. Correct it and try again.", "Inconsistent Data",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

                txtVcid.Focus();
                txtVcid.SelectAll();

                return false;
            }

            // Se o usuario editar no modo inserting
            if (currentMode == Mode.inserting)
            {
                // Verificar se ja existe o apid
                String queryEqual = "select count(apid) from apids where apid =  " + txtApid.Text.Trim();
                existApids = (int)DbInterface.ExecuteScalar(queryEqual);
            }

            // Se Apid nao existir no banco de dados
            if (existApids != 0)
            {
                MessageBox.Show("There is a " + gridApid.Columns[1].HeaderText + " with this " + gridApid.Columns[0].HeaderText + " already! \n\nChange it and try again",
                                    "Inconsistent data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtApid.Focus();
                txtApid.SelectAll();

                return false;
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
            if (MessageBox.Show("Are you sure you want to delete the Apid " + "'" + gridApid.CurrentRow.Cells[0].Value.ToString() + "', " +
                                "'" + gridApid.CurrentRow.Cells[1].Value.ToString() + "' ?",
                                "Please Confirm Deletion!",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            dbApid.Apid = gridApid.CurrentRow.Cells[0].Value.ToString();
            int indexSelected = gridApid.CurrentRow.Index;

            if (dbApid.Delete())
            {
                ChangeMode(Mode.browsing);

                if (gridApid.RowCount == 0)
                {
                    return;
                }

                // Setar o row seguinte aqui
                if (indexSelected == gridApid.RowCount)
                {
                    gridApid.Rows[indexSelected - 1].Cells[0].Selected = true;
                }
                else
                {
                    gridApid.Rows[indexSelected].Cells[0].Selected = true;
                }
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                dbApid.Apid = txtApid.Text.Trim();
                dbApid.Application_name = txtApplicationName.Text.Trim();
                dbApid.Vcid = txtVcid.Text.Trim();
                
                if (currentMode == Mode.inserting)
                {
                    if (!dbApid.Insert())
                    {
                        return; // sai porque deu erro.
                    }

                    fieldId = txtApid.Text.ToUpper().Trim();

                    // continua no mesmo modo
                    ChangeMode(Mode.inserting);
                }
                else if (currentMode == Mode.editing)
                {
                    if (!dbApid.Update())
                    {
                        return; // sai sem voltar ao modo browsing
                    }

                    fieldId = txtApid.Text.ToUpper().Trim();

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

      
        /** Este evento trata o tamanho da tela ao ser desacoplada do layout Docking **/
        private void FrmApid_DockStateChanged(object sender, EventArgs e)
        {

        }

        /**
        * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
        * para permitir a operacao da tela pelo usuario.
        **/
        private void FrmApid_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid antes de chamar o Edit.
            if ((gridApid.Focused) && (e.KeyCode == Keys.Enter))
            {
                e.Handled = true;
            }

            switch (currentMode)
            {
                case Mode.browsing:                    
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

                    break;
                }
                case Mode.editing:
                case Mode.inserting:
                {
                    if ((e.KeyCode == Keys.Enter) && (btConfirm.Enabled))
                    {
                        btConfirm_Click(this, new EventArgs());
                    }
                    else if ((e.KeyCode == Keys.Escape) && (btCancel.Enabled))
                    {
                        btCancel_Click(this, new EventArgs());
                    }

                    break;
                }
                case Mode.report:
                {
                    if (e.KeyCode == Keys.Escape)
                    {


                        btReport.Checked = false;
                        btReport_Click(this, new EventArgs());
                    }

                    break;
                }
            }
        }

        private void gridApid_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            FileHandling.ExportMissionIds(this.Text, "application_name", "apids", "apid");
        }

        private void btReport_Click(object sender, EventArgs e)
        {
            apidsTableAdapter.Connection.ConnectionString = "File Name=" + Properties.Settings.Default.db_connection_string.ToString();

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
                    
            
            if (btReport.Checked == true)
            {
                ChangeMode(Mode.report);
            }
            else
            {
                ChangeMode(Mode.browsing);
            }
        }

        #endregion
    }
}

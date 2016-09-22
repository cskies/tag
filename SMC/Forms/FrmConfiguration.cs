/**
 * @file 	    FrmConfiguration.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    17/07/2009
 * @note	    Modificado em 18/11/2013 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Net;
using Inpe.Subord.Comav.Egse.Smc.Properties;
using System.Configuration;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmConfiguration
     * Formulario de acesso as configuracoes do sistema.
     **/
    public partial class FrmConfiguration : DockContent
    {

        #region Variaveis Globais

        private MdiMain mdiMain = null;
        private DbConfiguration dbConfiguration = new DbConfiguration();
        private bool configuringDb;
 
        #endregion

        #region Construtor

        public FrmConfiguration(MdiMain mdi)
        {
            configuringDb = false;
            InitializeComponent();

            mdiMain = mdi;
            RefreshGrid();
        }

        // soh eh chamado atraves deste construtor pelo splash
        public FrmConfiguration()
        {
            configuringDb = true;
            InitializeComponent();
            RefreshGrid();
        }

        #endregion

        #region Eventos de Tela

        private void FrmConfiguration_Load(object sender, EventArgs e)
        {
            txtTcExportPath.Text = Properties.Settings.Default.tc_file_export_default_path;
            txtTmImportPath.Text = Properties.Settings.Default.tm_file_import_default_path;
            txtSqlQueriesPath.Text = Properties.Settings.Default.sql_queries_default_path;
            txtLayout.Text = Properties.Settings.Default.layouts_default_path;
            txtFlightSwpath.Text = Properties.Settings.Default.flight_sw_file_path;

            if (configuringDb == false)
            {
                pictureBox1.Image = dbConfiguration.GetBackgroundImage();

                if (DbInterface.TestConnection())
                {
                    try
                    {
                        DbConfiguration.Load();

                        dtpMissionEpoch.Value = DbConfiguration.MissionEpoch;

                        if (DbConfiguration.TmTimetagFormat.Equals("4"))
                        {
                            cmbTimeTagFormat.SelectedIndex = 1;
                        }
                        else
                        {
                            // default
                            cmbTimeTagFormat.SelectedIndex = 0;
                        }


                        cmbDefaultSourceId.Items.Clear();
                        cmbDefaultApid.Items.Clear();
                        cmbDefaultServiceType.Items.Clear();

                        String sql = "select '[' + dbo.f_zero(apid, 4) + '] ' + application_name as app_name from apids order by apid";
                        DataTable table = DbInterface.GetDataTable(sql);

                        if (table != null)
                        {
                            if (table.Rows.Count > 0)
                            {
                                // Preencher o combo com DefaultID.
                                foreach (DataRow row in table.Rows)
                                {
                                    cmbDefaultSourceId.Items.Add(row[0]);
                                }

                                // Preencher o combo DefaultAPID for Request.
                                foreach (DataRow row in table.Rows)
                                {
                                    cmbDefaultApid.Items.Add(row[0]);
                                }

                                table.Clear();
                            }
                            else
                            {
                                cmbDefaultSourceId.Items.Add("[There are no apids available in database.]");
                                cmbDefaultApid.Items.Add("[There are no apids available in database.]");
                            }
                        }

                        // Preencher o combo Default ServiceType for Request.
                        sql = "select '[' + dbo.f_zero(service_type, 3) + '] ' + service_name as name from services order by service_type";
                        table = DbInterface.GetDataTable(sql);

                        if (table != null)
                        {
                            if (table.Rows.Count > 0)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    cmbDefaultServiceType.Items.Add(row[0]);
                                }
                            }
                            else
                            {
                                cmbDefaultServiceType.Items.Add("[There are no service types available in database.]");
                            }
                        }

                        int defaultSourceId = DbConfiguration.RequestsDefaultSourceId;
                        int defaultApid = DbConfiguration.RequestsDefaultApid;
                        int defaultServiceType = DbConfiguration.RequestsDefaultServiceType;

                        // Selecionar os valores salvos nas Propriedades de Configuracao
                        cmbDefaultSourceId.SelectedIndex = cmbDefaultSourceId.FindString(Utils.Formatting.FormatCode(defaultSourceId, 4));
                        cmbDefaultApid.SelectedIndex = cmbDefaultApid.FindString(Utils.Formatting.FormatCode(defaultApid, 4));
                        cmbDefaultServiceType.SelectedIndex = cmbDefaultServiceType.FindString(Utils.Formatting.FormatCode(defaultServiceType, 3));

                       //preencher o combo da base de dados do contour e deixar ultimo item selecionado
                        FillContourDatabaseCombo();
                        cmbContourDBs.SelectedItem = DbConfiguration.Contour_database;
                        cmbContourPrjs.SelectedItem = DbConfiguration.Contour_project;

                        
                    }
                    catch (Exception)
                    {
                        // ignoramos o erro
                    }
                } // if (DbInterface.TestConnection())
            } // if (configuringDb == true)
        }

        private void btTmImportPath_Click(object sender, EventArgs e)
        {
            if (txtTmImportPath.Text.Equals(""))
            {
                folderDialog.SelectedPath = Application.StartupPath;
            }
            else
            {
                folderDialog.SelectedPath = txtTmImportPath.Text;
            }
            folderDialog.Description = "Select the default folder for TM import.";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtTmImportPath.Text = folderDialog.SelectedPath;
            }
        }

        private void btTcExportPath_Click(object sender, EventArgs e)
        {
            if (txtTcExportPath.Text.Equals(""))
            {
                folderDialog.SelectedPath = Application.StartupPath;
            }
            else
            {
                folderDialog.SelectedPath = txtTcExportPath.Text;
            }

            folderDialog.Description = "Select the default folder for TC export.";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtTcExportPath.Text = folderDialog.SelectedPath;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.tc_file_export_default_path = txtTcExportPath.Text;
                Properties.Settings.Default.tm_file_import_default_path = txtTmImportPath.Text;
                Properties.Settings.Default.sql_queries_default_path = txtSqlQueriesPath.Text;
                Properties.Settings.Default.flight_sw_file_path = txtFlightSwpath.Text;
                Properties.Settings.Default.layouts_default_path = txtLayout.Text;

                if (configuringDb == false)
                {
                    // apenas salva as configuracoes na base se o connection path ja tiver sido setado
                    if (DbInterface.TestConnection())
                    {
                        DbConfiguration.Load();

                        if (cmbTimeTagFormat.SelectedIndex == 0)
                        {
                            DbConfiguration.TmTimetagFormat = "6";
                        }
                        else
                        {
                            DbConfiguration.TmTimetagFormat = "4";
                        }

                        if (cmbDefaultSourceId.SelectedItem != null)
                        {
                            int defaultId = int.Parse(cmbDefaultSourceId.SelectedItem.ToString().Substring(1, 4));
                            DbConfiguration.RequestsDefaultSourceId = defaultId;
                        }

                        if (cmbDefaultApid.SelectedItem != null)
                        {
                            int defaultApid = int.Parse(cmbDefaultApid.SelectedItem.ToString().Substring(1, 4));
                            DbConfiguration.RequestsDefaultApid = defaultApid;
                        }

                        if (dtpMissionEpoch.Value != null)
                        {
                            DbConfiguration.MissionEpoch = dtpMissionEpoch.Value;
                        }

                        if (cmbContourDBs.SelectedItem != null)
                        {
                            DbConfiguration.Contour_database = (String)cmbContourDBs.SelectedItem;
                        }

                        if (cmbContourPrjs.SelectedItem != null)
                        {
                            DbConfiguration.Contour_project = (String)cmbContourPrjs.SelectedItem;
                        }

                        if (cmbDefaultServiceType.SelectedItem != null)
                        {
                            int defaultServiceType = int.Parse(cmbDefaultServiceType.SelectedItem.ToString().Substring(1, 3));
                            DbConfiguration.RequestsDefaultServiceType = defaultServiceType;
                        }

                        Properties.Settings.Default.Save();
                        if (DbConfiguration.Save() == true)
                        {
                            MessageBox.Show("Configuration saved successfully!",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                    } // if (DbInterface.TestConnection())
                }
                else
                {
                    Properties.Settings.Default.Save();

                    MessageBox.Show("Database configuration saved successfully! \n\n Please restart the system.",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                    Application.Exit();
                } // configuringDb

                // Verifica se foi o mdimain que chamou o form. Alem do mdimain, o splash também chama essa tela. 
                if (mdiMain != null)
                {
                    mdiMain.Refresh_Cmb();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to save configuration: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            if (Filedialogname != null)
            {
                dbConfiguration.UpdateBackgroundImage(Filedialogname);
                mdiMain.RefreshBackground();
            }
        }

        private void btSqlQueryPath_Click(object sender, EventArgs e)
        {
            if (txtSqlQueriesPath.Text.Equals(""))
            {
                folderDialog.SelectedPath = Application.StartupPath;
            }
            else
            {
                folderDialog.SelectedPath = txtSqlQueriesPath.Text;
            }

            folderDialog.Description = "Select the default folder for SQL query files.";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtSqlQueriesPath.Text = folderDialog.SelectedPath;
            }

        }

        private void FrmConfiguration_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(564, 352);
            }
        }

        private void btDefaultPerspective_Click(object sender, EventArgs e)
        {
            if (txtLayout.Text.Equals(""))
            {
                folderDialog.SelectedPath = Application.StartupPath;
            }
            else
            {
                folderDialog.SelectedPath = txtLayout.Text;
            }

            folderDialog.Description = "Select the default folder for Layout import.";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtLayout.Text = folderDialog.SelectedPath;
            }
        }

        private void btFlightSw_Click(object sender, EventArgs e)
        {
            if (txtFlightSwpath.Text.Equals(""))
            {
                folderDialog.SelectedPath = Application.StartupPath;
            }
            else
            {
                folderDialog.SelectedPath = txtSqlQueriesPath.Text;
            }

            folderDialog.Description = "Select the default folder for Flight SW files.";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtFlightSwpath.Text = folderDialog.SelectedPath;
            }
        }

        private void GridSelectDb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (e.RowIndex == (GridSelectDb.RowCount - 1))
                {
                    // Editar
                    FrmConnPathConfig frmConnPath = new FrmConnPathConfig();
                    frmConnPath.ShowDialog();
                    RefreshGrid();
                    GridSelectDb.ClearSelection();
                    GridSelectDb.Rows[GridSelectDb.RowCount - 2].Selected = true;
                }
                else
                {
                    // Deletar
                    Properties.Settings.Default.db_connections_names.RemoveAt(e.RowIndex);
                    Properties.Settings.Default.db_connections_strings.RemoveAt(e.RowIndex);
                    Properties.Settings.Default.Save();
                    RefreshGrid();
                    GridSelectDb.ClearSelection();
                    GridSelectDb.Rows[e.RowIndex].Selected = true;
                }
            }
            else if (e.ColumnIndex == 2)
            {
                // Adicionar
                FrmConnPathConfig frmConnPath = new FrmConnPathConfig();
                frmConnPath.ConnectionName = GridSelectDb[0, e.RowIndex].Value.ToString();
                frmConnPath.ConnectionPath = GridSelectDb[1, e.RowIndex].Value.ToString();
                frmConnPath.ShowDialog();
                RefreshGrid();
                GridSelectDb.ClearSelection();
                GridSelectDb.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btChangeImage_Click(object sender, EventArgs e)
        {

            String path = "";

            fileDialog.Filter = "Image Files|*.jpg|All Files|*.*";

            if (path == (""))
            {
                fileDialog.FileName = "backgroundimage.jpg";
            }
            else
            {
                fileDialog.FileName = path;

            }

            fileDialog.FilterIndex = 0;

            /** 
             * @attention 
             * Essa linha pode dar erro (apenas) durante a depuracao, caso se tente abrir um arquivo .udl a partir
             * do dialogo. Para evitar isso, deve-se desmarcar a opcao "throw" para o erro "LoaderLock", na opcao
             * "Managed Debugging Assistants", acessado pelo menu "Debug->Exceptions" do Visual Studio.
             **/
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Filedialogname = fileDialog.FileName;
                FileStream fileStream = new FileStream(Filedialogname, FileMode.Open, FileAccess.Read);
                Double ImgKb = (fileStream.Length) / 1024;
                //limita o tamanho da imagem em 300Kb
                if (ImgKb > 300)
                {
                    MessageBox.Show("The selected image has more than 300kb, please choose another image", "Image Size", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    btChangeImage_Click(this, new EventArgs());
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(Filedialogname);
                    pictureBox1.Refresh();
                }
            }
        }

        private void cmbContourDBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se mudar a base, a lista de projetos do combo de projetos do contour deve ser atualizada
            if (cmbContourDBs.SelectedIndex != 0)
            {
                cmbContourPrjs.Items.Clear();
                FillContourProjectCombo();
            }
        }

        private void FrmConfiguration_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!DbInterface.TestConnection())
            {
                Application.Exit();
            }
        }

        #endregion

        #region Metodos Privados

        private String Filedialogname;

        private void RefreshGrid()
        {
            int count = 0;

            if (Settings.Default.db_connections_names == null && Settings.Default.db_connections_strings == null)
            {
                GridSelectDb.Columns.Add("DATABASE", "DATABASE");
                GridSelectDb.Rows.Add("Database Not Found, Please add a database to start");
            }

            GridSelectDb.Rows.Clear();

            for (count = 0; count < Settings.Default.db_connections_names.Count; count++)
            {
                GridSelectDb.Rows.Add();
                GridSelectDb[0, count].Value = Settings.Default.db_connections_names[count];
                GridSelectDb[1, count].Value = Settings.Default.db_connections_strings[count];
                GridSelectDb[2, count].Value = "Edit";
                GridSelectDb[3, count].Value = "Delete";
            }

            GridSelectDb.Rows.Add();
            GridSelectDb[3, count].Value = "Add";
            GridSelectDb[2, count] = new DataGridViewTextBoxCell();
        }

        private void FillContourDatabaseCombo()
        {
            DataTable table = null;
            int result = 0;

            if (DbInterface.TestConnection())
            {
                table = DbInterface.GetDataTable("SELECT name FROM master.sys.databases");

                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        result = (int)DbInterface.ExecuteScalar("SELECT COUNT(*) FROM [" + item + "].sys.objects where name = 'testrunset' and type_desc = 'USER_TABLE'");

                        if (result > 0)
                        {
                            cmbContourDBs.Items.Add(item);
                        }
                    }
                }
            }

            cmbContourDBs.Refresh();
            cmbContourDBs.SelectedIndex = 0;
            cmbContourPrjs.Items.Clear();
            FillContourProjectCombo();
        }

         private void FillContourProjectCombo()
        {
            DataTable table = null;
            String sql = "SELECT name FROM " +(String)cmbContourDBs.SelectedItem+".dbo.project WHERE isFolder = 'F' and active = 'T'";

            if (DbInterface.TestConnection())
            {
                table = DbInterface.GetDataTable(sql);
            
                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        cmbContourPrjs.Items.Add(item);
                    }
                }
            }

            cmbContourPrjs.Refresh();
            cmbContourPrjs.SelectedIndex = 0;
        }

        #endregion



    }
}
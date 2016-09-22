/**
 * @file 	    FrmSessionsLog.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    19/07/2009
 * @note	    Modificado em 24/10/2013 por Bruna.
 * @note	    Modificado em 26/05/2015 por Conrado.
 * @note	    Modificado em 28/05/2015 por Thiago.
 **/

using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.TestSession;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSessionsLog
     * Formulario para a visualizacao dos logs de sessoes de teste.
     **/
    public partial class FrmSessionsLog : DockContent
    {
        private bool activated = false;
        bool hasEmbeddedPackets = false;
        private bool openedByTestProceduresComposition = false;
        private string logTime = "";

        #region Propriedades

        public bool OpenedByTestProceduresComposition
        {
            get
            {
                return openedByTestProceduresComposition;
            }
            set
            {
                openedByTestProceduresComposition = value;
            }
        }

        /* prop usada pela TelemetryInfoHandling.cs para
         * capturar ultima TM com o intuito de sincronizar
         * com o relogio do EGSE
         */
        public string LogTime 
        {
            get
            {
                string a = "ts";
                return a;
            }
        }

        #endregion

        #region Tratamento de Eventos da Interface Grafica

        public FrmSessionsLog()
        {
            InitializeComponent();
        }

        private void FrmSessionsLog_Load(object sender, EventArgs e)
        {
            cmbConnectionType.SelectedIndex = 0;
            cmbPacketsToShow.SelectedIndex = 0;

            if (DbConfiguration.TmTimetagFormat.Equals("4"))
            {
                chkTimeTagDate.Checked = false;
                chkTimeTagDate.Enabled = false;
            }

            LoadSessions();
        }

        private void cmbSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se for o primeiro item, recarrega o combo
            if (cmbSessions.SelectedIndex == 0)
            {
                LoadSessions();
                return;
            }

            LoadLogGrid();
        }

        private void chkRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                cmbConnectionType.SelectedIndex = 0;

                // Configura e habilita o timer
                timer.Interval = (int)(numSeconds.Value * 1000);
                timer.Enabled = true;
            }
            else
            {
                timer.Enabled = false;
            }

            numSeconds.Enabled = chkRealTime.Checked;
            label7.Enabled = chkRealTime.Checked;

            cmbConnectionType.Enabled = !chkRealTime.Checked;
            cmbSessions.Enabled = !chkRealTime.Checked;
            label3.Enabled = !chkRealTime.Checked;
            label4.Enabled = !chkRealTime.Checked;
        }

        private void cmbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSessions();
        }

        private void cmbPacketsToShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLogGrid();
        }

        private void FrmSessionsLog_Activated(object sender, EventArgs e)
        {
            // Ao carregar o form (form_load), o codigo que marca linhas com CRC 
            // errado em vermelho nao tem efeito, pois o form e o grid ainda nao
            // estao visiveis. As linhas abaixo sao uma forma de contornar isso.
            if (!activated)
            {
                LoadSessions();
                activated = true;
            }
        }

        private void chkShowValid_CheckedChanged(object sender, EventArgs e)
        {
            LoadLogGrid();
        }

        private void gridSessionLog_CurrentCellChanged(object sender, EventArgs e)
        {
            // Exibe o pacote bruto no textbox
            if ((gridSessionLog.CurrentRow != null))
            {
                txtRawPacket.Text = BitConverter.ToString((byte[])gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 1].Value);

                if ((bool)(gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 3].Value)) // if (allow_repetition)
                {
                    lblAppData.Text = " Application Data, N = " + ((int)(gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 2].Value)).ToString();
                }
                else
                {
                    lblAppData.Text = " Application Data";
                }

                LoadLogGridAppData();
            }
        }

        private void numSeconds_ValueChanged(object sender, EventArgs e)
        {
            // Reconfigura o intervalo do timer que atualiza o monitoramento em tempo real
            timer.Interval = (int)(numSeconds.Value * 1000);
        }

        /** Atualiza as sessoes e, consequentemente, o log de pacotes e app data. **/
        private void timer_Tick(object sender, EventArgs e)
        {
            LoadSessions();
        }

        /** Tratamento do clique nos botoes do grid, quando houverem. **/
        private void gridAppData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (hasEmbeddedPackets)
            {
                if (e.ColumnIndex == (gridAppData.ColumnCount - 1))
                {
                    int serviceType, serviceSubtype, mainSubtype, apid = 0, ssc = 0;
                    int typeIndex, subtypeIndex, apidIndex, sscIndex;

                    mainSubtype = int.Parse(gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 5].Value.ToString());

                    // TODO: trocar a origem destes campos para as celulas do grid
                    apidIndex = 5;
                    sscIndex = 7;
                    typeIndex = 12;
                    subtypeIndex = 13;

                    if ((gridAppData[typeIndex, e.RowIndex].Value == null) ||
                        (gridAppData[subtypeIndex, e.RowIndex].Value == null))
                    {
                        MessageBox.Show("Invalid Service Type / Subtype !!!",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        serviceType = int.Parse(gridAppData[typeIndex, e.RowIndex].Value.ToString().Substring(1, 5));
                        serviceSubtype = int.Parse(gridAppData[subtypeIndex, e.RowIndex].Value.ToString().Substring(1, 5));
                    }

                    if (gridAppData[apidIndex, e.RowIndex].Value != null)
                    {
                        int i = gridAppData[apidIndex, e.RowIndex].Value.ToString().IndexOf("[");
                        int j = gridAppData[apidIndex, e.RowIndex].Value.ToString().IndexOf("]");
                        apid = int.Parse(gridAppData[apidIndex, e.RowIndex].Value.ToString().Substring(i + 1, (j - i - 1)));
                    }

                    if (gridAppData[sscIndex, e.RowIndex].Value != null)
                    {
                        ssc = int.Parse(gridAppData[sscIndex, e.RowIndex].Value.ToString());
                    }

                    // cria um novo rawpacket, e o alimenta com o pacote selecionado no gridSessionLog
                    RawPacket embeddedAppData = new RawPacket(false, false);
                    byte[] rawLogPacket = (byte[])gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 1].Value;
                    embeddedAppData.Resize((UInt16)rawLogPacket.GetLength(0));
                    embeddedAppData.SetPart(0, rawLogPacket.GetLength(0) * 8, rawLogPacket);

                    // calcula o start bit do application data do comando embutido
                    int startBit = 88; // bit inicial do app data dos tipos [11/4] e [19/1]

                    for (int j = 0; j <= gridAppData.CurrentRow.Index; j++)
                    {
                        // O preenchimento da propriedade 'Tag' da coluna de indice 0 nao esta sendo preenchido, 
                        // por isso deve-se iniciar a contagem de bits a partir da segunda coluna, onde 'i' eh iniciado em 1.
                        // A estrutura 'for' da linha 494 comprova isso.
                        for (int i = 1; i < gridAppData.ColumnCount; i++)
                        {
                            if (i == (gridAppData.ColumnCount - 1))
                            {
                                // application data column; pega da tag da celula
                                if (j < gridAppData.CurrentRow.Index)
                                {
                                    startBit += int.Parse(gridAppData[i, j].Tag.ToString());
                                }
                            }
                            else
                            {
                                startBit += int.Parse(gridAppData.Columns[i].Tag.ToString());
                            }
                        }
                    }

                    FrmApplicationData form = new FrmApplicationData(serviceType, serviceSubtype, apid, ssc, false, false, startBit, embeddedAppData);
                    form.ShowDialog();
                }
            }
        }

        private void chkTimeTagDate_CheckedChanged(object sender, EventArgs e)
        {
            ShowTimeTagFormat();
        }

        private void FrmSessionsLog_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(792, 560);
            }
        }

        private void FrmSessionsLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                // atualiza a visualizacao
                LoadSessions();
            }
        }

        #endregion

        #region Metodos Privados

        /** Carrega as sessoes da base no combo. **/
        private void LoadSessions()
        {
            try
            {
                string sessionToSelect = string.Empty;

                if (cmbSessions.Items.Count > 1 && cmbSessions.SelectedIndex > 0)
                {
                    sessionToSelect = cmbSessions.Items[cmbSessions.SelectedIndex].ToString().Substring(0, 6);
                }

                string connectionType = "";

                switch (cmbConnectionType.SelectedIndex)
                {
                    case 0: // all
                        {
                            connectionType = "all";
                            break;
                        }
                    case 1: // ethernet
                        {
                            connectionType = "ethernet";
                            break;
                        }
                    case 2: // serial
                        {
                            connectionType = "serial";
                            break;
                        }
                    case 3: // named pipe
                        {
                            connectionType = "pipe";
                            break;
                        }
                    case 4: // named pipe
                        {
                            connectionType = "file";
                            break;
                        }
                }

                DataTable table = SessionLog.GetSessionList(connectionType);

                cmbSessions.Items.Clear();
                cmbSessions.Items.Add("[Click here to refresh the sessions list]");

                foreach (DataRow row in table.Rows)
                {
                    cmbSessions.Items.Add(row[0]);
                }

                if (cmbSessions.Items.Count > 1)
                {
                    if (sessionToSelect.Equals(string.Empty))
                    {
                        cmbSessions.SelectedIndex = 1;
                    }
                    else
                    {
                        cmbSessions.SelectedIndex = cmbSessions.FindString(sessionToSelect);
                    }
                }

                LoadLogGrid();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            }
        }

        /** Carrega o grid com os pacotes da sessao selecionada **/
        private void LoadLogGrid()
        {
            try
            {
                int sessionId = 0;

                if (cmbSessions.SelectedIndex > 0)
                {
                    string sqlCaracteres = "select isnull(MAX(session_id), 0) from sessions";
                    string caracteres = Convert.ToString(DbInterface.ExecuteScalar(sqlCaracteres));
                    Int32 nOfCaracteres = caracteres.Length;

                    sessionId = int.Parse(cmbSessions.Text.Substring(0, nOfCaracteres));
                }

                string packetsType = "all";

                if (cmbPacketsToShow.SelectedIndex == 1) // TCs
                {
                    packetsType = "requests";
                }
                else if (cmbPacketsToShow.SelectedIndex == 2) // TMs
                {
                    packetsType = "reports";
                }

                DataTable table = SessionLog.GetPacketsInSession(sessionId, packetsType, chkShowValid.Checked);

                // Se nao houver novos registros, a table tera o mesmo numero de rows que o grid.
                // Isso significa que nao precisa recarregar todos os componentes graficos com esta nova consulta.
                // Isso esta sendo feito para que a tela SessionLog nao fique travando.

                if (table.Rows.Count == gridSessionLog.RowCount)
                {
                    return;
                }

                gridSessionLog.DataSource = table;

                gridSessionLog.Columns[2].Width = 120; // seta antes o tamanho da coluna Time Tag, caso seja [N/A].

                ShowTimeTagFormat();

                // Ajusta a largura das colunas         
                gridSessionLog.Columns[0].Width = 123;
                gridSessionLog.Columns[1].Width = 70;
                gridSessionLog.Columns[3].Width = 151;
                gridSessionLog.Columns[4].Width = 53;
                gridSessionLog.Columns[5].Width = 230;
                gridSessionLog.Columns[6].Width = 300;
                gridSessionLog.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gridSessionLog.Columns[7].Width = 100;
                gridSessionLog.Columns[8].Width = 80;
                gridSessionLog.Columns[9].Width = 85;

                // Oculta as ultimas colunas do usuario
                gridSessionLog.Columns[10].Visible = false;
                gridSessionLog.Columns[11].Visible = false;
                gridSessionLog.Columns[12].Visible = false;
                gridSessionLog.Columns[13].Visible = false;
                gridSessionLog.Columns[14].Visible = false;
                gridSessionLog.Columns[15].Visible = false;
                gridSessionLog.Columns[16].Visible = false;
                gridSessionLog.Columns[17].Visible = false;
                gridSessionLog.Columns[18].Visible = false;

                // Agora varre o grid para marcar o CRC em vermelho
                if (!chkShowValid.Checked)
                {
                    foreach (DataGridViewRow row in gridSessionLog.Rows)
                    {
                        if (!row.Cells[7].Value.ToString().Equals("OK"))
                        {
                            gridSessionLog.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }

                // Tira a selecao da primeira linha do row e passa para a ultima
                if (gridSessionLog.RowCount > 0)
                {
                    gridSessionLog.Rows[0].Selected = false;
                    gridSessionLog.CurrentCell = gridSessionLog.Rows[gridSessionLog.RowCount - 1].Cells[9];
                    gridSessionLog_CurrentCellChanged(this, new EventArgs());
                    gridSessionLog.FirstDisplayedScrollingColumnIndex = 9;
                    gridSessionLog.FirstDisplayedScrollingRowIndex = (gridSessionLog.RowCount - 1);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            }
        }

        private void ShowTimeTagFormat()
        {
            // verifica se a opcao Show time-tag as date esta selecionada.
            if (chkTimeTagDate.Checked)
            {
                TimeCode.LoadEpoch();

                foreach (DataGridViewRow row in gridSessionLog.Rows)
                {
                    string timeTag = row.Cells[2].Value.ToString().Replace(".", "");

                    if (!timeTag.Equals("[N/A]"))
                    {
                        Int32 microSeconds = Int32.Parse(timeTag.Substring((timeTag.Length - 4), 4), NumberStyles.HexNumber);
                        Int32 seconds = Int32.Parse(timeTag.Substring(2, (timeTag.Length - 6)), NumberStyles.HexNumber);

                        timeTag = TimeCode.DateFromEpoch(seconds, microSeconds);

                        row.Cells[2].Value = timeTag;
                        gridSessionLog.Columns[2].Width = 170;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in gridSessionLog.Rows)
                {
                    string timeTag = row.Cells[2].Value.ToString();

                    if (!timeTag.Equals("[N/A]"))
                    {
                        if ((timeTag.ToUpper().Contains("0X")) && (!timeTag.Substring(10, 1).Equals(".")))
                        {
                            row.Cells[2].Value = timeTag.Substring(0, 10) + "." + timeTag.Substring(10, 4);
                        }
                        else
                        {
                            string hexTimeTag = "0x" + TimeCode.CalendarToOnboardTime(timeTag);
                            row.Cells[2].Value = hexTimeTag.Substring(0, 10) + "." + hexTimeTag.Substring(10, 4);
                        }
                    }
                }
            }
        }

        /** 
         * Carrega o grid de application data do pacote selecionado na sessao. 
         * 
         * @attention Ha varias medidas neste metodo para melhorar a performance do carregamento do
         * grid, principalmente quando o numero de colunas for grande. Cuidado ao modificar.
         **/
        public void LoadLogGridAppData()
        {
            try
            {
                bool isTelemetryPacket = false;

                int sessionId = 0;

                string sqlCaracteres = "select isnull(MAX(session_id), 0) from sessions";
                string caracteres = Convert.ToString(DbInterface.ExecuteScalar(sqlCaracteres));
                Int32 nOfCaracteres = caracteres.Length;

                sessionId = int.Parse(cmbSessions.Text.Substring(0, nOfCaracteres));

                int serviceType = int.Parse(gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 6].Value.ToString());
                int serviceSubtype = int.Parse(gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 5].Value.ToString());
                int uniqueId = (int)(gridSessionLog.CurrentRow.Cells[15].Value);
                int nValue = (int)(gridSessionLog.CurrentRow.Cells[gridSessionLog.ColumnCount - 2].Value);

                hasEmbeddedPackets = false;

                // Verifica se os subtipos contem pacotes
                // TODO: trocar esta verificacao por uma flag a adicionar no cadastro do subtipo
                if (((serviceType == 11) && (serviceSubtype == 4)) ||
                    ((serviceType == 19) && (serviceSubtype == 1)) ||
                    ((serviceType == 11) && (serviceSubtype == 10)))
                {
                    hasEmbeddedPackets = true;
                }

                // Verifica se eh uma telemetria com Reporting Definition (Housekeeping and Diagnostic Data Service)
                if ((serviceType == 3) && (serviceSubtype == 25))
                {
                    isTelemetryPacket = true;
                }

                // Antes de mais nada, reseta o grid
                gridAppData.Rows.Clear();
                gridAppData.Columns.Clear();
                gridAppData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                string sql = @"select (select count(*) from packets_log_data_field where session_id = a.session_id and unique_log_id = a.unique_log_id) / 
                                    case when a.n_value = 0 then 1 else a.n_value end as nCols 
                           from packets_log a
                           where a.session_id = " + sessionId.ToString() + @" and a.unique_log_id = " + uniqueId.ToString();    

                int nCols = (int)DbInterface.ExecuteScalar(sql);

                if (nCols == 0)
                {
                    gridAppData.Columns.Add("no_structure", "[This subtype has no application data]");
                    gridAppData.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    return;
                }

                // Adiciona-se uma coluna para ordenacao. Importante para manter boa performance quando ha muitas colunas
                nCols++;

                DataTable table = SessionLog.GetApplicationDataFromPacket(sessionId, uniqueId, isTelemetryPacket);

                Application.EnableVisualStyles();
                gridAppData.SuspendLayout();

                // Adiciona as colunas ao grid
                for (int i = 0; i < nCols; i++)
                {
                    gridAppData.Columns.Add(new DataGridViewTextBoxColumn());
                    gridAppData.Columns[gridAppData.ColumnCount - 1].Visible = false;
                }

                // Agora adiciona as linhas ao grid
                gridAppData.Rows.Add(nValue);

                int rowIndex = 0;

                // Alimenta as celulas e headers do grid
                for (int i = 0; i < nValue; i++)
                {
                    // para ordenacao
                    gridAppData.Columns[0].Visible = false;
                    gridAppData[0, i].Value = i;

                    for (int j = 1; j < nCols; j++)
                    {
                        if (i == 0)
                        {
                            gridAppData.Columns[j].HeaderText = table.Rows[rowIndex]["data_field_name"].ToString();
                            gridAppData.Columns[j].Tag = table.Rows[rowIndex]["number_of_bits"].ToString();
                        }

                        gridAppData[j, i].Value = table.Rows[rowIndex]["value"].ToString();
                        rowIndex++;
                    }
                }

                for (int i = 1; i < nCols; i++)
                {
                    gridAppData.Columns[i].Visible = true;
                }

                // Se for [19/1] ou [11/4], adiciona a coluna com os dados da aplicacao
                if (hasEmbeddedPackets)
                {
                    DataGridViewButtonColumn colButton = new DataGridViewButtonColumn();
                    colButton.HeaderText = "Application data";
                    colButton.Text = "Application data";
                    colButton.UseColumnTextForButtonValue = true;
                    gridAppData.Columns.Add(colButton);

                    int nBits = 0;

                    // Agora alimenta o numero de bits do app_data do pacote embarcado.
                    // Soma um ao packet length para adequar ao PUS
                    for (int i = 0; i < nValue; i++)
                    {
                        nBits = ((int.Parse(gridAppData[7, i].Value.ToString()) + 1) * 8) - 32;
                        gridAppData[gridAppData.ColumnCount - 1, i].Tag = nBits;
                    }
                }

                gridAppData.ResumeLayout();
                gridAppData.Sort(gridAppData.Columns[0], ListSortDirection.Ascending);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Metodos Publicos

        /**Metodo usado para selecionar a sessao desejada. Sera muito usado durante a execucao automatica dos procedimentos.**/
        public void ShowSession(string sessionId)
        {
            if (cmbSessions.Items.Count > 0)
            {
                cmbSessions.SelectedIndex = cmbSessions.FindString(sessionId);
            }
        }

        #endregion

    }
}
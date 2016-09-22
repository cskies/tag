/**
 * @file 	    FrmTcsComposition.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    21/07/2009
 * @note	    Modificado em 19/02/2014 por Ayres.
 * @note	    Modificado em 02/03/2015 por Conrado.
 * @note	    Modificado em 26/05/2015 por Conrado.
 * @note	    Modificado em 28/05/2015 por Thiago.
 **/

using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.TestSession;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmTcsComposition
     * Formulario de composicao, save e envio de TCs.
     **/

    public partial class FrmRequestsComposition : DockContent
    {
        #region Atributos Privados

        private RawPacket requestPacket = new RawPacket(true, false);
        private RawPacket reportPacket = new RawPacket(false, false);
        private SessionLog session = new SessionLog();
        private bool hasEmbeddedTCs = false;
        private ASCIIEncoding encoder = new ASCIIEncoding(); // evito recriar a cada byte recebido
        private MdiMain mdiMain = null;
        private FrmConnectionWithEgse frmConnection = null; // objeto para ter acesso a tela ConnectionWithEgse
        private FrmSavedRequests frmSavedRequests = null; // objeto para ter acesso a tela SavedRequests
        private int structureId = 0; // usado para o redimensionamento do grid no 'Define New Housekeeping Report'

        private int variableLengthField = 0;
        // usado para gravar o numero de bits dos campos variaveis da area de dados do pacote.

        // este delegate e o bool sao usados para resolver um bug no DataGrid
        // ver notas a respeito no metodo gridAppData_CellEnter
        private delegate void CellEnterWorkaround();

        bool firstCellEnter;

        // As duas variaveis abaixo sao usadas para controlar o carregamento do FrmRequestsComposition
        // com o evento "FrmTcsSending_Load" ao chamar este formulario pela tela de Saved Requests.
        // Sem o uso delas, o formulario nao carrega corretamente o pacote salvo no banco de dados, 
        // porque o "FrmTcsSending_Load" eh executado depois do construtor FrmRequestsComposition(int savedRequestId).
        private bool calledFromSavedRequests = false;
        private bool alreadyLoaded = false;
        private bool constructing = false;

        //variavel utilizada para evitar conflito no metodo de validacao da celula do grid
        private bool update = true;

        private bool loadingForm = true;

        //utilizada para a comparacao de primeiro click no grid
        private bool firstGridRowClick = true;

        //variaveis utilizadas no controle da linha clicada
        private int currentMouseOverRow;
        private int currentGridRowClick;

        private object _value;
        private string value = "";
        private FrmExecutionTimeout _frmExecutionTimeout;
        private bool _calendar;
        private int idSavedReq;
        private int[] statusCalendar;
        private object _dateInHex;
        private bool isRelative;

        int appDataColumnIndex = 14;

        #endregion

        #region Construtores

        public FrmRequestsComposition()
        {
            InitializeComponent();
        }

        public FrmRequestsComposition(MdiMain mdi)
        {
            InitializeComponent();
            mdiMain = mdi;
            calledFromSavedRequests = false;
            mdiMain.FormRequestsComposition = this;
            
            // verificar se a tela ConnectionWithEGSE esta aberta para manter contato.
            if (mdiMain.FormConnectionWithEgse != null)
            {
                frmConnection = mdiMain.FormConnectionWithEgse;
                // buscar a instancia da classe ConnectionWithEgse para ser usada aqui.
                frmSavedRequests = mdiMain.FormSavedRequests;
                // buscar a instancia da classe SavedRequests para ser usada aqui.
                frmConnection.FormTcsComposition = this;
                // enviar a instancia desta tela para a classe ConnectionWithEgse.

                if (frmConnection.Connected)
                {
                    btSend.Enabled = true;

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = true;
                    }
                }
                else
                {
                    btSend.Enabled = false;

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }
                }
            }
        }

        #endregion

        #region Carregamento de pacotes salvos

        public void LoadRequestComposition(int savedRequestId)
        {
            idSavedReq = savedRequestId;
            calledFromSavedRequests = true;
            constructing = true;
            FrmTcsSending_Load(null, new EventArgs());
            string sql =
                @"select auto_ssc, auto_length, auto_crc, n_value, raw_packet, service_type, service_subtype from saved_requests where saved_request_id = " +
                savedRequestId;
            DataTable tblPacket = DbInterface.GetDataTable(sql);
            byte[] requestSaved = (byte[])tblPacket.Rows[0][4];
            requestPacket.RawContents = requestSaved;
            byte packetType = requestSaved[0];
            int apid = (int)requestPacket.GetPart(5, 11);
            int groupingFlags = (int)requestPacket.GetPart(16, 2);
            int ssc = (int)requestPacket.GetPart(18, 14);
            int packetLenght = (int)requestPacket.GetPart(32, 16);
            string crc = requestPacket.GetString().Substring(requestPacket.GetString().Length - 5, 5);
            int ack = (int)requestPacket.GetPart(52, 4);
            int serviceType = (int)requestPacket.GetPart(56, 8);
            int serviceSubtype = (int)requestPacket.GetPart(64, 8);

            if (apid == 1)
            {
                serviceType = (int)tblPacket.Rows[0][5];
                serviceSubtype = (int)tblPacket.Rows[0][6];
            }

            if (packetType == 08)
            {
                cmbPacketType.SelectedIndex = 1; // TM
            }
            else
            {
                cmbPacketType.SelectedIndex = 0; // TC
            }

            // selecionar o valor do cmbAPID
            cmbAPID.SelectedIndex = cmbAPID.FindString(Utils.Formatting.FormatCode(apid, 4).ToString());

            // selecionar o valor do cmbGroupFlags
            switch (groupingFlags)
            {
                case 1: // first packet
                    {
                        cmbGroupingFlags.SelectedIndex = cmbGroupingFlags.FindString("First Packet [invalid]");
                        break;
                    }
                case 0: // continuation packet
                    {
                        cmbGroupingFlags.SelectedIndex = cmbGroupingFlags.FindString("Continuation Packet [invalid]");
                        break;
                    }
                case 2: // last packet
                    {
                        cmbGroupingFlags.SelectedIndex = cmbGroupingFlags.FindString("Last Packet [invalid]");
                        break;
                    }
                case 3: // stand-alone packet
                    {
                        cmbGroupingFlags.SelectedIndex = cmbGroupingFlags.FindString("Stand-Alone Packet");
                        break;
                    }
            }

            // selecionar o item do cmbAck
            switch (ack)
            {
                case 0: // None
                    {
                        cmbAck.SelectedIndex = cmbAck.FindString("None");
                        break;
                    }
                case 1: // Acceptance
                    {
                        cmbAck.SelectedIndex = cmbAck.FindString("Acceptance");
                        break;
                    }
                case 2: // Start of Execution [invalid]
                    {
                        cmbAck.SelectedIndex = cmbAck.FindString("Start of Execution [invalid]");
                        break;
                    }
                case 4: // Progress of Execution [invalid]
                    {
                        cmbAck.SelectedIndex = cmbAck.FindString("Progress of Execution [invalid]");
                        break;
                    }
                case 8: // Completion of Execution
                    {
                        cmbAck.SelectedIndex = cmbAck.FindString("Completion of Execution");
                        break;
                    }
                case 9: // Acceptance + Completion
                    {
                        cmbAck.SelectedIndex = cmbAck.FindString("Acceptance + Completion");
                        break;
                    }
            }

            // selecionar os cmbServiceType e cmbServiceSubtype
            cmbServiceType.SelectedIndex = cmbServiceType.FindString(Utils.Formatting.FormatCode(serviceType, 3));
            cmbServiceSubtype.SelectedIndex =
                cmbServiceSubtype.FindString(Utils.Formatting.FormatCode(serviceSubtype, 3));

            // obter o campo N
            int fieldN = (int)tblPacket.Rows[0][3];
            numN.Value = fieldN;

            // selecionar os dados da area de dados do pacote
            string sqlData = @"select saved_request_id as SavedRequestId, 
                                    data_field_id as DataFieldId, 
                                    position as Position, 
                                    nth_element as NthElement, 
                                    data_field_value as DataFieldValue, 
                                    master.dbo.fn_varbintohexstr(long_data_field_value) as LongDataFieldValue
                            from 
                                    saved_requests_data_field 
                            where 
                                    saved_request_id = " + savedRequestId + " order by position";

            DataTable dataTable = DbInterface.GetDataTable(sqlData);

            int posI, shiftI, numNPart;

            numNPart = fieldN;
            shiftI = 0;
            posI = shiftI;

            foreach (DataGridViewRow row in gridAppData.Rows)
            {
                foreach (DataGridViewColumn col in gridAppData.Columns)
                {
                    if (col.Index == 11)
                    {

                    }

                    if (posI >= dataTable.Rows.Count) break;

                    value = dataTable.Rows[posI][4].ToString();

                    if (col.ToolTipText.ToUpper().Contains("TABLE/LIST"))
                    {
                        DataGridViewComboBoxColumn cell = (DataGridViewComboBoxColumn)gridAppData.Columns[col.Index];

                        if (col.HeaderText.ToUpper().Contains("SERVICE SUBTYPE"))
                        {
                            gridAppData.CurrentCell = (DataGridViewCell)gridAppData.Rows[row.Index].Cells[col.Index];
                            gridAppData.Rows[row.Index].Selected = true;
                            AppDataGridsHandling.LoadSubtypesInComboBoxCellGrid(gridAppData);
                            AppDataGridsHandling.SelectSubtypeInComboBoxCellGrid(gridAppData, value, col.Index,
                                row.Index);

                            if (hasEmbeddedTCs)
                            {
                                if (gridAppData.Rows[row.Index].Cells[col.Index].Value != null)
                                {
                                    string cellValue = gridAppData.Rows[row.Index].Cells[col.Index].Value.ToString();

                                    if (!cellValue.Contains("[There are no"))
                                    {
                                        int i = cellValue.IndexOf("[", 8);
                                        int j = cellValue.IndexOf("]", 8);
                                        int numberOfBits = int.Parse(cellValue.Substring(i + 1, (j - i - 6)));

                                        gridAppData.Rows[row.Index].Cells[gridAppData.ColumnCount - 2].Value = null;
                                        gridAppData.Rows[row.Index].Cells[gridAppData.ColumnCount - 2].ToolTipText =
                                            "Application data, " + (numberOfBits + 16).ToString() + " bits";

                                        // Atualiza o valor de packet_length (N, se houver, esta incluso em numberOfBits)
                                        // (subtrai 1 para adequar ao PUS)
                                        gridAppData.Rows[row.Index].Cells[7].Value = ((numberOfBits + 48) / 8) - 1;

                                        // Inicializa o valor de appData zerado
                                        byte[] appDataValue = new byte[((numberOfBits + 16) / 8)];
                                        gridAppData.Rows[row.Index].Cells[gridAppData.ColumnCount - 2].Value =
                                            appDataValue;
                                    }
                                }
                            }
                        }
                        else // SERVICE TYPE
                        {
                            // Percorro os items do ComboBoxCell ate encontrar o default value.
                            for (int i = 0; i < cell.Items.Count; i++)
                            {
                                int first = cell.Items[i].ToString().IndexOf("[");
                                int last = cell.Items[i].ToString().IndexOf("]");

                                string codeItem = cell.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                int code = 0;

                                if (int.TryParse(codeItem, out code) && (code == int.Parse(value)))
                                {
                                    gridAppData.Rows[row.Index].Cells[col.Index].Value = cell.Items[i].ToString();
                                    break;
                                }
                            }
                        }
                    }
                    else if (col.ToolTipText.ToUpper().Contains("RAW HEX"))
                    {
                        if (col.HeaderText.Equals("Time: Seconds"))
                        {
                            if (value.EndsWith("00") && !value.Equals("100"))
                            {
                                _value = value.Remove(value.Length - 2, 2);
                                var dateValue = Convert.ToInt64(_value).ToString("X"); // + "0000";
                                dateValue += "0000";
                                _dateInHex = dateValue;

                                try
                                {
                                    Int64 verify = Convert.ToInt64(dateValue, 16);
                                }
                                catch (Exception e)
                                {
                                    dateValue = "";
                                    MessageBox.Show(e.Message);
                                    throw;
                                }

                                TimeCode.LoadEpoch();
                                dateValue = TimeCode.OnboardTimeToCalendar(dateValue);

                                string date = TimeCode.OnboardTimeToCalendar(_dateInHex.ToString());
                                date = date.Remove(date.Length - 7, 7);
                                gridAppData[col.Index, row.Index].Value = date;
                                _calendar = true;
                            }
                            else
                            {
                                gridAppData[col.Index, row.Index].Value = value;
                            }
                        }
                        else
                        {
                            gridAppData[col.Index, row.Index].Value = Formatting.FormatHexString((UInt64.Parse(dataTable.Rows[posI][4].ToString())).ToString("X"));
                        }

                        if (col.ToolTipText.Contains("Raw Hex, variable size"))
                        {
                            string valueInHex = dataTable.Rows[posI][5].ToString();
                            string valueBd = valueInHex.Substring(2, (valueInHex.Length - 2));

                            gridAppData[col.Index, row.Index].Value = valueBd;

                            // atribui o tamanho no tooltip da celula
                            string formatted = Formatting.FormatHexString(gridAppData[col.Index, row.Index].FormattedValue.ToString()).Replace("-", "");
                            byte[] fieldArray = Formatting.HexStringToByteArray(formatted);

                            // reconfigura o valor do numero de bits do campos variavel
                            gridAppData[col.Index, row.Index].ToolTipText = "Raw Hex, " + (fieldArray.Length * 8).ToString() + " bits";
                        }
                    }
                    else if (col.ToolTipText.ToUpper().Contains("BOOL"))
                    {
                        if (value.Equals("1"))
                        {
                            gridAppData[col.Index, row.Index].Value = true;
                        }
                    }
                    else
                    {
                        gridAppData.Rows[row.Index].Cells[col.Index].Value = value;
                    }

                    // chama este evento para setar o cursor editavel na celula. Isto deve ser feito para carregar o 
                    // valor no pacote bruto contido no campo txtRawPacket
                    gridAppData_CellValidated(null, new DataGridViewCellEventArgs(col.Index, row.Index));
                    posI = posI + numNPart;
                }

                posI = shiftI + 1;
                shiftI++;
            }

            numSSC.Value = ssc;
            mskPacketLength.Text = packetLenght.ToString();
            mskPacketCrc.Text = crc;
            chkAutoIncrement.Checked = (bool)tblPacket.Rows[0][0];
            chkAutoLength.Checked = (bool)tblPacket.Rows[0][1];
            chkAutoCrc.Checked = (bool)tblPacket.Rows[0][2];
            ResizeAndUpdatePacket();
            RefreshPacketView();

            alreadyLoaded = true;
            constructing = false;

            if (mdiMain.FormConnectionWithEgse != null)
            {
                // buscar a instancia da classe ConnectionWithEgse para ser usada aqui.
                frmConnection = mdiMain.FormConnectionWithEgse;

                // buscar a instancia da classe SavedRequests para ser usada aqui.
                frmSavedRequests = mdiMain.FormSavedRequests;

                // enviar a instancia desta tela para a classe ConnectionWithEgse.
                frmConnection.FormTcsComposition = this;

                if (frmConnection.Connected)
                {
                    btSend.Enabled = true;

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = true;
                    }
                }
                else
                {
                    btSend.Enabled = false;

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }
                }

            }
        }

        /** 
         *  Extrai pacotes embarcados de Subtypes especiais. Os pacotes especiais são aqueles com TCs dentro de sua area de dados, como o 11.4 e 19.1
         *  Esta rotina foi baseada na forma em que o RawHexa desses pacotes eh manipulado. Exemplo: ao inserir 5 no campo N, teremos 5 Rows com type e subtype vazios.
         *  Antes mesmo de preenche-los, o RawHexa mantem os 5 pacotes de TCs (1 em cada Row) todos shiftados para esquerda. Porem, existem bytes com valor zero à direita desses pacotes.
         *  @attention A criacao da rotina abaixo teve como regra o RawHexa do pacote que ja esta sendo criado. Se a rotina de manipulacao do RawHexa ser atualizada, presumo que esta rotina tambem
         *  tenha que ser atualizada. SIM, eh possivel que exista algum bug na composicao do 11.4
         *  
         * Ass: Thiago
         **/
        private RawPacket GetEmbeddedPacketInSpecialTelecommands(int specialType, int specialSubtype, int indexOfEmbeddedCommand)
        {
            // busca no RawPacket apenas os pacotes e nao ainda as TimeTags.
            RawPacket packetToReturn = new RawPacket(true, false);
            byte[] sourcePacket = requestPacket.RawContents;

            int startIndexNField = 10; // indice fixo
            int nFieldValue = (int)sourcePacket[startIndexNField]; // numero de pacotes dentro do pacote especial
            int startIndexDataField = 11; // indice da TimeTag do primeiro pacote

            // O parametro sizeOfBytesForConditionToExecuteCommand sera usado para definir o numero de bytes
            // usados como condicao para executar o comando que esta embarcado.
            // Exemplo: No 11.4 temos a timeTag de 4 bytes. Ja no 19.1 temos 1 byte para definir qual evento devera ocorrer para que o comando seja executado.
            int numBytesForCondition = 0;

            if ((specialType == 11) && (specialSubtype == 4))
            {
                numBytesForCondition = 4; // o 11.4 aqui possui a TimeTag. //@todo: buscar o tamanho da timetag da base 
            }
            else if ((specialType == 19) && (specialSubtype == 1))
            {
                numBytesForCondition = 1; // o 19.1 aqui possui o Evento que eh a condicao para o comando ser disparado. //@todo: buscar esse valor na base
            }

            int rawPacketHeaderFieldNumBytes = 6; // tamanho fixo
            //int startIndexRawPacket = startIndexTimeTag + timeTagSize; // 15
            int startIndexRawPacket = (startIndexDataField + numBytesForCondition); // 15
            int startIndexRawPacketSize = startIndexRawPacket + rawPacketHeaderFieldNumBytes - 1; // varia em funcao do tamanho dos anteriores

            int subtypeIndex = 23;
            int subtypeValue = 0;

            for (int currentIndex = 0; currentIndex < nFieldValue; currentIndex++)
            {
                subtypeValue = sourcePacket[subtypeIndex];

                if (subtypeValue == 0)
                {
                    startIndexRawPacket += ((startIndexDataField + numBytesForCondition) - 1); // = 14
                    subtypeIndex += 14;
                    startIndexRawPacketSize = startIndexRawPacket + rawPacketHeaderFieldNumBytes - 1;
                }
                else
                {
                    int rawPacketSizeValue = sourcePacket[startIndexRawPacketSize]; // valor do CurrentPacketLength
                    int rawPacketNumbytes = rawPacketHeaderFieldNumBytes + rawPacketSizeValue + 1; // numero de bytes do pacote

                    if (currentIndex == indexOfEmbeddedCommand)
                    {
                        byte[] packet = new byte[rawPacketNumbytes];
                        Array.Copy(sourcePacket, startIndexRawPacket, packet, 0, rawPacketNumbytes); // extrai o pacote
                        packetToReturn.RawContents = packet; // alimenta o rawHexa do pacote 
                        Console.WriteLine(Utils.Formatting.ConvertByteArrayToHexString(packetToReturn.RawContents, packetToReturn.RawContents.Length));
                        break;
                    }

                    startIndexRawPacket += rawPacketNumbytes + numBytesForCondition;
                    startIndexRawPacketSize = startIndexRawPacket + rawPacketHeaderFieldNumBytes - 1;
                }
            }

            return packetToReturn;
        }

        #endregion

        #region Propriedades

        /**Usada para receber a instancia da tela ConnectionWithEgse.**/
        public FrmConnectionWithEgse FrmConnectionWithEgse
        {
            set
            {
                frmConnection = value;
            }
        }

        public RawPacket RequestPacket
        {
            get
            {
                return requestPacket;
            }
        }

        public bool CalledFromSavedRequests
        {
            get
            {
                return calledFromSavedRequests;
            }
            set
            {
                calledFromSavedRequests = value;
            }
        }

        #endregion

        #region Transmissao de pacotes

        //inclusão do if utilizando o método ValidaTcd().
        private void btSend_Click(object sender, EventArgs e)
        {
            if (requestPacket.IsDeviceCommand && (cmbServiceType.SelectedIndex != 1))
            {
                AuthenticateTc();
            }
            else
            {
                if (Convert.ToInt32(txtPacketCurrentSize.Text) > 248)
                {
                    MessageBox.Show("The request packet cannot be greater than 248 bytes!\n\n" +
                                    "It will not be updated anymore until it has 248 bytes or less.",
                                    "Invalid request packet size!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
                else
                {
                    // forca que edicoes pendentes sejam finalizadas                    
                    gridAppData.EndEdit();

                    if (!VerifyRequest())
                    {
                        return;
                    }

                    btSend.Enabled = false;


                    /* TODO: implementa if/else p/ verificar sendo rel, eh necessario chamar o metodo
                     * TMInfoHand.GetLastLogTime() p/ capturar ultima TM e assim sync com relogio do EGSE
                     */


                    // Enviar o Request para a tela de conexao
                    frmConnection.SendRequest(requestPacket, FrmConnectionWithEgse.SourceRequestPacket.RequestComposition);
                    btSend.Enabled = true;
                }
            }
        }

        #endregion

        #region Codigo de Tela

        public void FrmTcsSending_Load(object sender, EventArgs e)
        {
            alreadyLoaded = true;

            if (calledFromSavedRequests && alreadyLoaded)
                return;

            // Seta o version number e data header flag do pacote, que sao fixos
            byte[] part = new byte[2];

            // version number
            part[0] = 0;
            requestPacket.SetPart(0, 3, part);

            // Data header flag
            part[0] = 8; // 00001000
            requestPacket.SetPart(4, 1, part);

            // Ccsds secondary header flag (0) + TC Packet Version Number (1)
            part[0] = 1;
            part[0] = (byte)(part[0] << 4);

            requestPacket.SetPart(48, 4, part);

            cmbPacketType.SelectedIndex = 0;
            cmbGroupingFlags.SelectedIndex = 3;
            cmbAck.SelectedIndex = 0;

            cmbAPID.Items.Clear();
            cmbServiceType.Items.Clear();
            cmbServiceSubtype.Items.Clear();

            // f_zero eh uma user-defined function (user dbo) na base Simulador_UTMC
            string sql = "select '[' + dbo.f_zero(apid, 4) + '] ' + application_name as app_name from apids order by apid";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table == null)
                return;

            foreach (DataRow row in table.Rows)
                cmbAPID.Items.Add(row[0]);

            // Preenche o combo serviceType
            sql = "select '[' + dbo.f_zero(service_type, 3) + '] ' + service_name as name from services order by service_type";

            table = DbInterface.GetDataTable(sql);

            if (table == null)
            {
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                cmbServiceType.Items.Add(row[0]);
            }

            numN.Visible = false;
            numN.Value = 1;
            chkAutoIncrement.Checked = true;
            lblN.Visible = false;
            numSSC.Value = 1;
            cmbServiceSubtype.Width = 340;

            DbConfiguration.Load();

            // se a selecao deste item for alterada, deve-se alterar tambem o 
            // botao connection no trecho de sincronizacao da tela ConnectionWithEgse
            if (!DbConfiguration.RequestsDefaultApid.ToString().Trim().Equals(""))
            {
                cmbAPID.SelectedIndex = cmbAPID.FindString(Formatting.FormatCode(DbConfiguration.RequestsDefaultApid, 4));
            }

            if (!DbConfiguration.RequestsDefaultServiceType.ToString().Trim().Equals(""))
            {
                cmbServiceType.SelectedIndex = cmbServiceType.FindString(Formatting.FormatCode(DbConfiguration.RequestsDefaultServiceType, 3));
            }

            // Inicia a visualizacao
            RefreshPacketView();
            loadingForm = false;
        }

        private void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceType.Items.Count == 0)
                return;

            if (cmbServiceType.SelectedIndex == -1)
                // isso so acontece caso nao tenha sido salvo, nas configuracoes do
                // SMC, um servico padrao a carregar na abertura desta tela
                return;

            cmbServiceSubtype.Items.Clear();

            int serviceType = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));

            // f_zero eh uma user-defined function (user dbo) na base Simulador_UTMC
            string sql = @"select '[' + dbo.f_zero(service_subtype, 3) + '] ' + description as subtype_description 
                    from subtypes where service_type = " + serviceType + " and is_request = 1 order by service_subtype";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    cmbServiceSubtype.Items.Add(row[0]);
                }
            }
            else
            {
                cmbServiceSubtype.Items.Add("[There are no requests available for this service]");
            }

            cmbServiceSubtype.SelectedIndex = 0;

            // device commands (TCDs) nao possuem campo service type
            if (requestPacket.IsDeviceCommand == false)
            {
                // Atualiza a mensagem binaria
                byte[] part = new byte[1];
                part[0] = (byte)(serviceType);

                requestPacket.SetPart(56, 8, part);

                if (numN.Visible)
                {
                    part[0] = 1;
                    requestPacket.SetPart(80, 8, part);
                }
            }

            RefreshPacketView();
        }

        private void cmbServiceSubtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string subtype = cmbServiceSubtype.Items[0].ToString();
            
            if ((cmbServiceSubtype.Items.Count == 0) ||
                (subtype.Substring(0, subtype.Length).Contains("[There are no")))
            {
                gridAppData.Columns.Clear();
                return;
            }

            int serviceType, serviceSubtype;

            serviceType = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));
            serviceSubtype = int.Parse(cmbServiceSubtype.SelectedItem.ToString().Substring(1, 3));

            // Se for um TC com comandos "embarcados", adiciona 2 bytes para o CRC do comando
            // TODO: mudar essa verificacao depois de modificar o cadastro de subtypes
            if (((serviceType == 11) && (serviceSubtype == 4)) ||
                ((serviceType == 19) && (serviceSubtype == 1)))
            {
                hasEmbeddedTCs = true;
            }
            else
            {
                hasEmbeddedTCs = false;
            }

            if (requestPacket.IsDeviceCommand == false)
            {
                // Atualiza a mensagem binaria
                byte[] part = new byte[1];
                part[0] = (byte)(serviceSubtype);

                requestPacket.SetPart(64, 8, part);
            }

            AppDataGridsHandling.LoadAppDataGrid(ref gridAppData, serviceType, serviceSubtype, true);
            AppDataGridsHandling.LoadDefaultCellValues(ref gridAppData, gridAppData.RowCount);
            statusCalendar = new int[gridAppData.Rows.Count];
            ResizeAndUpdatePacket();
        }

        private void cmbPacketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] part = new byte[1];

            if (cmbPacketType.SelectedItem.ToString().ToUpper().Equals("TM"))
            {
                part[0] = 0;
            }
            else // tc
            {
                part[0] = 1;
            }

            part[0] = (byte)(part[0] << 4);

            requestPacket.SetPart(3, 1, part);
            RefreshPacketView();

            // Para atualizar o numero de bytes para a time-tag. TC=4 e TM=6.
            ResizeAndUpdatePacket();
        }

        private void chkAutoIncrement_CheckedChanged(object sender, EventArgs e)
        {
            numSSC.Enabled = !chkAutoIncrement.Checked;
        }

        private void cmbAPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int16 apid = Int16.Parse(cmbAPID.SelectedItem.ToString().Substring(1, 4));
            byte[] part = new byte[2];

            // TODO: melhorar esse tratamento para torna-lo mais generico
            if (apid == 1) // CPDU
            {
                requestPacket.IsDeviceCommand = true; // refaz o pacote

                chkDataHeaderFlag.Checked = false;

                // Sem data header flag
                part[0] = 0; // 00000000
                requestPacket.SetPart(4, 1, part);
            }
            else
            {
                requestPacket.IsDeviceCommand = false; // refaz o pacote
                chkDataHeaderFlag.Checked = true;

                // Com data header flag
                part[0] = 8; // 00001000 = 8 qdo naõ for tc direto
                requestPacket.SetPart(4, 1, part);
            }

            // apos setar requestPacket.IsDeviceCommand, preciso re-setar os campos iniciais

            // version number
            part[0] = 0;
            requestPacket.SetPart(0, 3, part);

            // Ccsds secondary header flag (0) + TC Packet Version Number (1)
            part[0] = 1;
            part[0] = (byte)(part[0] << 4);

            requestPacket.SetPart(48, 4, part);

            part[0] = (byte)(apid >> 8);
            part[1] = (byte)(apid & 0xFF);

            requestPacket.SetPart(5, 11, part);

            cmbPacketType_SelectedIndexChanged(this, new EventArgs());
            cmbGroupingFlags_SelectedIndexChanged(this, new EventArgs());
            numSSC_ValueChanged(this, new EventArgs());
            cmbAck_SelectedIndexChanged(this, new EventArgs());

            ResizeAndUpdatePacket();

            if (loadingForm == false) // para evitar erros ao carregar a tela
            {
                cmbServiceType_SelectedIndexChanged(this, new EventArgs());
            }

            RefreshPacketView();
        }

        private void cmbGroupingFlags_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] part = new byte[1];

            switch (cmbGroupingFlags.SelectedIndex)
            {
                case 0: // first packet
                    part[0] = 1;
                    break;
                case 1: // continuation packet
                    part[0] = 0;
                    break;
                case 2: // last packet
                    part[0] = 2;
                    break;
                case 3: // stand-alone packet
                    part[0] = 3;
                    break;
            }

            // Posiciona corretamente os bits
            part[0] = (byte)(part[0] << 6);

            requestPacket.SetPart(16, 2, part);
            RefreshPacketView();
        }

        private void numSSC_ValueChanged(object sender, EventArgs e)
        {
            Int16 ssc = (Int16)numSSC.Value;
            byte[] part = new byte[2];

            part[0] = (byte)(ssc >> 8);
            part[1] = (byte)(ssc & 0xFF);

            requestPacket.SetPart(18, 14, part);
            RefreshPacketView();
        }

        private void cmbAck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (requestPacket.IsDeviceCommand)
            {
                // device commands (TCDs) nao possuem campo ack
                return;
            }

            byte[] part = new byte[1];

            switch (cmbAck.SelectedIndex)
            {
                case 0: // none
                    part[0] = 0;
                    break;
                case 1: // acceptance
                    part[0] = 1;
                    break;
                case 2: // start (invalid)
                    part[0] = 2;
                    break;
                case 3: // progress (invalid)
                    part[0] = 4;
                    break;
                case 4: // completion
                    part[0] = 8;
                    break;
                case 5: // acceptance + completion
                    part[0] = 9;
                    break;
            }

            requestPacket.SetPart(52, 4, part);
            RefreshPacketView();
        }

        private void numN_ValueChanged(object sender, EventArgs e)
        {
            if (gridAppData.ColumnCount == 0)
            {
                UpdatePacket();
                return;
            }

            
            statusCalendar = new int[(int)numN.Value];

            int rowsCount = gridAppData.RowCount;
            int numRow = int.Parse(numN.Value.ToString());

            if (numRow > rowsCount) // adiciona a quantidade de rows inserida no numSpinner
            {
                int add = numRow - rowsCount;
                gridAppData.Rows.Add(add);

                // usado para controle da edicao da area de dados de TCs embarcados em pacotes especiais.
                // usado na edicao desses dados via FrmAppData
                if (gridAppData.ColumnCount >= 14)
                {
                    gridAppData.Rows[gridAppData.RowCount - 1].Cells[appDataColumnIndex].Tag = "0";
                }

                AppDataGridsHandling.LoadDefaultCellValues(ref gridAppData, add);
            }
            else if (numRow < rowsCount) // remove a quantidade de rows especificada no numSpinner. Debaixo pra cima.
            {
                int remove = rowsCount - numRow;
                int index = gridAppData.RowCount;

                for (int i = 0; i < remove; i++)
                {
                    DataGridViewRow row = gridAppData.Rows[index - 1];
                    gridAppData.Rows.Remove(row);

                    index--;
                }
            }

            ResizeAndUpdatePacket();
        }

        /** Rotina de verificacao do request a enviar. **/
        private bool VerifyRequest()
        {
            bool pass = true;
            if (cmbServiceSubtype.Items[0].ToString().Length > 13)
            {
                if ((cmbServiceSubtype.Items.Count == 0) ||
                    (cmbServiceSubtype.Items[0].ToString().Substring(0, 13).Equals("[There are no")))
                {
                    MessageBox.Show("No service subtype selected. Select one and try again.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    return (false);
                }
            }

            // Varre o grid em busca de nulos ou vazios e determina o tamanho do vetor
            foreach (DataGridViewColumn col in gridAppData.Columns)
            {
                foreach (DataGridViewRow row in gridAppData.Rows)
                {
                    if (gridAppData[col.Index, row.Index].Value == null)
                    {
                        pass = false;
                        break;
                    }

                    if (gridAppData[col.Index, row.Index].Value.ToString().Equals(""))
                    {
                        pass = false;
                        break;
                    }

                    if (gridAppData[col.Index, row.Index].Value.ToString().Contains("[There are no"))
                    {
                        pass = false;
                        break;
                    }

                    if (gridAppData[col.Index, row.Index].Value.ToString().Contains("[Choose a service"))
                    {
                        pass = false;
                        break;
                    }

                    // TODO: nao verifico, no momento, se campos alimentados pelo frmEmbedded estao ou
                    // nao preenchidos (subtipos [11.4 e 19.1])
                }

                if (!pass)
                {
                    break;
                }
            }

            if (!pass)
            {
                MessageBox.Show("There are empty fields. Correct them and try again.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return (false);
            }

            return (true);
        }

        //metodo para validar se comando selecionado e um device command -- BUG SIA_OBC_SW_BUG-60
        private void AuthenticateTc()
        {
            if (requestPacket.IsDeviceCommand)
            {
                if (cmbServiceType.SelectedIndex != 1)
                {
                    MessageBox.Show("Invalid command with this 'Application Process ID (APID)'.\n\nVerify it and try again.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void FrmTcsSending_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(751, 517);
            }
        }

        private void chkAutoLength_CheckedChanged(object sender, EventArgs e)
        {
            mskPacketLength.ReadOnly = chkAutoLength.Checked;

            if (mskPacketLength.ReadOnly)
            {
                mskPacketLength.BackColor = SystemColors.Control;
                ResizeAndUpdatePacket();
            }
            else
            {
                mskPacketLength.BackColor = SystemColors.Window;
            }
        }

        private void chkAutoCrc_CheckedChanged(object sender, EventArgs e)
        {
            mskPacketCrc.ReadOnly = chkAutoCrc.Checked;
            requestPacket.AutoCrc = chkAutoCrc.Checked;

            if (mskPacketCrc.ReadOnly)
            {
                mskPacketCrc.BackColor = SystemColors.Control;
                ResizeAndUpdatePacket(); // recalcula o pacote e atualiza a visualizacao
            }
            else
            {
                mskPacketCrc.BackColor = SystemColors.Window;
            }
        }

        private void mskPacketLength_Leave(object sender, EventArgs e)
        {
            if (mskPacketLength.Text.Trim().Equals(""))
            {
                mskPacketLength.Text = "0";
            }
            else
            {
                if (int.Parse(mskPacketLength.Text) > 65535)
                {
                    MessageBox.Show("Packet length greater than the field supports!\n\nMaximum allowed value is 65535.",
                                    "Invalid Value!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mskPacketLength.Focus();
                }
            }

            // Aproveito e atualizo o frame
            UpdatePacket();
        }

        private void mskPacketCrc_Leave(object sender, EventArgs e)
        {
            if (!chkAutoCrc.Checked)
            {
                mskPacketCrc.Text = mskPacketCrc.Text.ToUpper();
                int parsedValue = 0;

                if (int.TryParse(mskPacketCrc.Text.Replace("-", ""), System.Globalization.NumberStyles.HexNumber, null, out parsedValue))
                {
                    UpdatePacket();
                }
                else
                {
                    MessageBox.Show("CRC is not an hex value!", "Invalid CRC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mskPacketCrc.Focus();
                }
            }
        }

        private void FrmTcsSending_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmConnection != null)
            {
                frmConnection.FormTcsComposition = null;
            }
        }

        private void btSaveRequest_Click(object sender, EventArgs e)
        {
            //inclusão do if utilizando o método AuthenticateTC(), criado acima
            if (requestPacket.IsDeviceCommand && cmbServiceType.SelectedIndex != 1)
            {
                AuthenticateTc();
            }
            else
            {
                if (Convert.ToInt32(txtPacketCurrentSize.Text) > 248)
                {
                    MessageBox.Show("The request packet cannot be greater than 248 bytes!\n\n",
                                    "Invalid request packet size!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
                else
                {
                    // forca que edicoes pendentes sejam finalizadas                    
                    gridAppData.EndEdit();

                    if (!VerifyRequest())
                    {
                        return;
                    }
                    
                    int serviceType = Convert.ToInt32(cmbServiceType.SelectedItem.ToString().Substring(1, 3));
                    int serviceSubtype = int.Parse(cmbServiceSubtype.SelectedItem.ToString().Substring(1, 3));

                    FrmSaveRequest frmSaveRequest = new FrmSaveRequest(requestPacket,
                                                            serviceType,
                                                            serviceSubtype,
                                                            variableLengthField,
                                                            chkAutoIncrement.Checked,
                                                            chkAutoLength.Checked,
                                                            chkAutoCrc.Checked,
                                                            (int)numN.Value,
                                                            isRelative,
                                                            statusCalendar
                                                            );

                    frmSaveRequest.ShowDialog();
                }
            }
        }

        private void txtPacketCurrentSize_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtPacketCurrentSize.Text) > 248)
            {
                //necessario que o readonly seja false para que haja a modificacao de cor e que o backcolor seja setado para white novamente
                txtPacketCurrentSize.ReadOnly = false;
                txtPacketCurrentSize.BackColor = Color.White;
                txtPacketCurrentSize.ForeColor = Color.Red;
                txtPacketCurrentSize.ReadOnly = true;
            }
            else
            {
                txtPacketCurrentSize.ReadOnly = false;
                txtPacketCurrentSize.BackColor = Color.White;
                txtPacketCurrentSize.ForeColor = Color.Black;
                txtPacketCurrentSize.ReadOnly = true;
            }
        }

        #endregion

        #region Redimensionamento e Atualizacao da Mensagem Hexa

        /**
         * Redimensiona o array e atualiza seus valores, a partir do inicio da area de dados.
         * (obs: o packet_length e o crc tambem sao atualizados).
         **/
        private bool ResizeAndUpdatePacket()
        {
            UInt16 newSize = 12;
            UInt16 addValue;

            if (requestPacket.IsDeviceCommand == true)
            {
                newSize -= 4;
            }
            else
            {
                // Se houver parametro N, soma 1 byte ao tamanho
                if (numN.Visible)
                {
                    newSize++;
                }
            }

            if (!hasEmbeddedTCs)
            {
                UInt16 appDataSize = 0;
                UInt16 numberOfBits;
                int i, j;

                foreach (DataGridViewColumn col in gridAppData.Columns)
                {
                    if (col.ToolTipText.Equals(""))
                    {
                        break; // ao atingir a button column, ou se nao houver subtipo
                    }

                    // Se for uma coluna raw hex de tamanho variavel, a posicao do numero de bits fica 
                    if (col.ToolTipText.Contains("Raw Hex, variable size"))
                    {
                        i = gridAppData[col.Index, 0].ToolTipText.IndexOf(",");
                        j = gridAppData[col.Index, 0].ToolTipText.IndexOf("bits");

                        numberOfBits = UInt16.Parse(gridAppData[col.Index, 0].ToolTipText.Substring(i + 2, (j - i - 3)));
                        variableLengthField = numberOfBits;
                    }
                    else
                    {
                        i = col.ToolTipText.IndexOf(",");
                        j = col.ToolTipText.IndexOf("bits");

                        numberOfBits = UInt16.Parse(col.ToolTipText.Substring(i + 2, (j - i - 3)));
                    }

                    appDataSize += numberOfBits;
                }

                newSize += (UInt16)((numN.Value * appDataSize) / 8);
            }
            else
            {
                for (int i = 0; i < gridAppData.RowCount; i++)
                {
                    if (gridAppData.Rows[i].Cells[7].Value == null)
                    {
                        addValue = 0;
                    }
                    else
                    {
                        // packet_length do pacote embutido; soma um para adequar ao PUS
                        addValue = (UInt16)(UInt16.Parse(gridAppData.Rows[i].Cells[7].Value.ToString()) + 6 + 1);
                    }

                    if (cmbServiceType.SelectedItem.ToString().Substring(1, 3).Equals("011"))
                    {
                        // Adiciona bytes para a time-tag
                        //addValue += 6;
                        //@attention: aqui estava sendo setado 6 bytes para o pacote, porem 6 bytes eh para time-tag de TMs.
                        // Nesse caso, foi acrescentada a verificacao se TC ou TM.

                        if (cmbPacketType.Text.Equals("TC"))
                        {
                            addValue += 4;
                        }
                        else
                        {
                            addValue += 6;
                        }
                    }
                    else
                    {
                        // Adiciona bytes para o RID
                        addValue += 1;
                    }

                    newSize += addValue;
                }
            }

            requestPacket.Resize(newSize);

            UpdatePacket();

            txtPacketCurrentSize.Text = newSize.ToString();

            return true;
        }

        /**
         * Varre o grid e transfere os dados lidos para o array com a mensagem hexa.
         **/
        public void UpdatePacket()
        {
            if (!update) return;

            int startPosition = 80;
            int firstByte = 0;
            ushort crc;

            if (requestPacket.IsDeviceCommand == true)
            {
                startPosition -= 32;
            }
            else
            {
                if (numN.Visible)
                {
                    byte[] part = new byte[1];

                    part[0] = (byte)(numN.Value);
                    requestPacket.SetPart(startPosition, 8, part);
                    startPosition += 8;
                }
            }

            // Atualiza TODA a area de dados, chamando o validate para todas as celulas
            foreach (DataGridViewRow row in gridAppData.Rows)
            {
                foreach (DataGridViewColumn col in gridAppData.Columns)
                {
                    // Para o calculo de crc de tttcs
                    if (hasEmbeddedTCs)
                    {
                        // A primeira coluna destes subtipos nao faz parte do tc "embutido"
                        if (((cmbServiceType.SelectedItem.ToString().Substring(1, 3).Equals("011")) ||
                             (cmbServiceType.SelectedItem.ToString().Substring(1, 3).Equals("019"))) && (col.Index == 1))
                        {
                            firstByte = (startPosition / 8);
                        }
                    }

                    if (col.ToolTipText.Equals("")) break; // ao atingir a button appdata

                    int i;
                    int j;
                    int numberOfBits = 0;
                    int firstBit = 0;
                    int shift = 0;

                    if (col.ToolTipText.Contains("Application data"))
                    {
                        if (gridAppData[col.Index, row.Index].ToolTipText.Equals(""))
                        {
                            numberOfBits = 0;
                        }
                        else
                        {
                            i = gridAppData[col.Index, row.Index].ToolTipText.IndexOf(",", StringComparison.Ordinal);
                            j = gridAppData[col.Index, row.Index].ToolTipText.IndexOf("bits", StringComparison.Ordinal);
                            numberOfBits = int.Parse(gridAppData[col.Index, row.Index].ToolTipText.Substring(i + 2, (j - i - 3)));
                        }
                    }
                    else
                    {
                        if (col.ToolTipText.Contains("Raw Hex, variable size"))
                        {
                            i = gridAppData[col.Index, row.Index].ToolTipText.IndexOf(",", StringComparison.Ordinal);
                            j = gridAppData[col.Index, row.Index].ToolTipText.IndexOf("bits", StringComparison.Ordinal);
                            numberOfBits = int.Parse(gridAppData[col.Index, row.Index].ToolTipText.Substring(i + 2, (j - i - 3)));
                        }
                        else
                        {
                            i = col.ToolTipText.IndexOf(",", StringComparison.Ordinal);
                            j = col.ToolTipText.IndexOf("bits", StringComparison.Ordinal);
                            numberOfBits = ushort.Parse(col.ToolTipText.Substring(i + 2, j - i - 3));
                        }
                    }

                    if (numberOfBits > 0)
                    {
                        Int64 fieldValue = 0;
                        byte[] fieldArray = null;

                        if ((!(gridAppData[col.Index, row.Index].FormattedValue == null)) &&
                            (!(gridAppData[col.Index, row.Index].FormattedValue.ToString().Equals(""))) &&
                            (!(gridAppData[col.Index, row.Index].FormattedValue.ToString().Contains("[Choose a service"))) &&
                            (!(gridAppData[col.Index, row.Index].FormattedValue.ToString().Contains("[There are no"))))
                        {
                            // Extrai o valor do campo
                            if (col.ToolTipText.Contains("Table"))
                            {
                                i = gridAppData[col.Index, row.Index].FormattedValue.ToString().IndexOf("[", StringComparison.Ordinal);
                                j = gridAppData[col.Index, row.Index].FormattedValue.ToString().IndexOf("]", StringComparison.Ordinal);
                                fieldValue = Int64.Parse(gridAppData[col.Index, row.Index].FormattedValue.ToString().Substring(i + 1, (j - i - 1)));
                            }
                            else if (col.ToolTipText.Contains("Numeric"))
                            {
                                fieldValue = Int64.Parse(gridAppData[col.Index, row.Index].FormattedValue.ToString());
                            }
                            else if (col.ToolTipText.Contains("Raw Hex"))
                            {
                                if (!col.ToolTipText.Contains("Raw Hex, variable size"))
                                {
                                    if (col.HeaderText.Equals("Time: Seconds"))
                                    {
                                        if (!calledFromSavedRequests)
                                        {
                                            if (_frmExecutionTimeout != null && _value != null)
                                            {
                                                if (_calendar)
                                                {
                                                    DateTime date;

                                                    if (DateTime.TryParse(_value.ToString(), out date))
                                                    {
                                                        String calendarTime = date.ToString();
                                                        TimeCode.LoadEpoch();
                                                        String dateStr = TimeCode.CalendarToOnboardTime(calendarTime);
                                                            
                                                        if (dateStr.Length > dateStr.Length - 4)
                                                        {
                                                            var dateStr1 = dateStr.Remove(dateStr.Length - 4, 4);
                                                            _dateInHex = dateStr1;
                                                            fieldValue = Convert.ToInt64(dateStr1, 16);
                                                        }
                                                    }
                                                }

                                                else
                                                {
                                                    string strippedValue = gridAppData[col.Index, row.Index].FormattedValue.ToString().Replace("-", "");
                                                    fieldValue = Convert.ToInt64(strippedValue, 16);

                                                    // formata o hex
                                                    //gridAppData[col.Index, row.Index].Value = Formatting.FormatHexString(strippedValue);
                                                }
                                                
                                            }
                                            else
                                            {
                                                fieldValue = Convert.ToInt64(gridAppData[col.Index, row.Index].Value.ToString(), 10);
                                            }
                                        }
                                        else
                                        {
                                            if (_calendar)
                                            {
                                                fieldValue = Convert.ToInt64(_value.ToString(), 10);
                                                _calendar = true;
                                            }
                                            else
                                            {
                                                fieldValue = Convert.ToInt64(gridAppData[col.Index, row.Index].Value.ToString(), 10);
                                                //gridAppData[col.Index, row.Index].Value = value;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string strippedValue = gridAppData[col.Index, row.Index].FormattedValue.ToString().Replace("-", "");
                                        fieldValue = Convert.ToInt64(strippedValue, 16);

                                        // formata o hex
                                        gridAppData[col.Index, row.Index].Value = Formatting.FormatHexString(strippedValue);
                                    }
                                }
                            }
                            else if (col.ToolTipText.Contains("Bool"))
                            {
                                if (bool.Parse(gridAppData[col.Index, row.Index].FormattedValue.ToString()))
                                {
                                    fieldValue = 1; // se for false, mantem o valor original de fieldValue (zero)
                                }
                            }
                            else if (col.ToolTipText.Contains("Application data"))
                            {
                                byte[] part = new byte[((numberOfBits) / 8)];
                                part = (byte[])gridAppData[col.Index, row.Index].Value;

                                // Zerar o crc
                                crc = 0xffff;

                                byte[] appData = requestPacket.RawContents;

                                // Calcula o crc do cabecalho do tttc
                                for (int x = firstByte; x < (startPosition / 8); x++)
                                {
                                    crc = CheckingCodes.CrcCcitt16WithSyndrome(appData[x], crc);
                                }

                                // Continua o calculo, agora com a area de dados
                                for (int x = 0; x < (part.GetLength(0) - 2); x++)
                                {
                                    crc = CheckingCodes.CrcCcitt16WithSyndrome(part[x], crc);
                                }

                                // Substitui os 2 ultimos bytes com o CRC
                                part[part.GetLength(0) - 2] = (byte)(crc >> 8);
                                part[part.GetLength(0) - 1] = (byte)(crc & 0xff);

                                requestPacket.SetPart(startPosition, numberOfBits, part);
                            }
                        }

                        if (!col.ToolTipText.Contains("Application data"))
                        {
                            // Valido para campos de ate 64 bits, pois o BitConverter redimensiona 
                            // part para 8 elementos (por receber um int64)
                            if ((numberOfBits > 64) && (!col.ToolTipText.Contains("Raw Hex, variable size")))
                            {
                                MessageBox.Show("Error: More than 64 bits in a field other than 'Raw Application data' or 'Raw Hex, variable size'!",
                                                "Invalid field size!",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);

                                RefreshPacketView();
                                return;
                            }

                            foreach (DataGridViewColumn col2 in gridAppData.Columns)
                            {
                                if (col2.ToolTipText.Equals("")) break; // ao atingir a button column

                                // O loop chegou a coluna atual; extrai os dados necessarios e sai
                                if (col2.Index == col.Index)
                                {
                                    break;
                                }
                                else
                                {
                                    if (col2.ToolTipText.Contains("Raw Hex, variable size"))
                                    {
                                        i = gridAppData[col2.Index, row.Index].ToolTipText.IndexOf(",");
                                        j = gridAppData[col2.Index, row.Index].ToolTipText.IndexOf("bits");

                                        firstBit += int.Parse(gridAppData[col2.Index, row.Index].ToolTipText.Substring(i + 2, (j - i - 3)));
                                    }
                                    else
                                    {
                                        i = col2.ToolTipText.IndexOf(",");
                                        j = col2.ToolTipText.IndexOf("bits");
                                        firstBit += int.Parse(col2.ToolTipText.Substring(i + 2, (j - i - 3)));
                                    }
                                }
                            }

                            shift = requestPacket.DefineLeftShift(firstBit, numberOfBits);

                            // Aplica o shift, se houver
                            if (shift > 0)
                            {
                                fieldValue = fieldValue << shift;
                            }

                            byte[] part = null;

                            if (fieldArray == null)
                            {
                                part = new byte[(int)Math.Ceiling(((double)numberOfBits) / 8)];

                                // ATENCAO: o bitconverter pode modificar o tamanho de part!

                                part = BitConverter.GetBytes(fieldValue);
                                // Para corrigir a posicao dos bytes retornados do bitconverter
                                Array.Reverse(part, 0, (int)Math.Ceiling(((double)numberOfBits) / 8));
                            }
                            else
                            {
                                part = fieldArray;
                            }

                            requestPacket.SetPart(startPosition, numberOfBits, part);
                        }

                        startPosition += numberOfBits;
                    }
                }
            }

            // Se o usuario informou o frame length, sobreponho o tamanho
            if (chkAutoLength.Checked == false)
            {
                byte[] part = new byte[2];
                int packetLength = int.Parse(mskPacketLength.Text);
                part[0] = (byte)(packetLength >> 8);
                part[1] = (byte)(packetLength & 0xff);
                requestPacket.SetPart(32, 16, part);
            }

            // Finalmente, se o CRC nao for automatico, insere no frame
            if (chkAutoCrc.Checked == false)
            {
                int crcValue = 0;

                // O CRC ja foi validado no leave do msgbox
                int.TryParse(mskPacketCrc.Text.Replace("-", ""),
                             System.Globalization.NumberStyles.HexNumber,
                             null,
                             out crcValue);

                byte[] part = new byte[2];
                
                part = new byte[2];
                part[0] = (byte)(crcValue >> 8);
                part[1] = (byte)(crcValue & 0xff);
                requestPacket.SetPart(requestPacket.Size * 8 - 16, 16, part);
            }

            RefreshPacketView();
        }

        private void RefreshPacketView()
        {
            //if (!calledFromSavedRequests)
            //{
            //    txtRawPacket.Text = requestPacket.GetString();
            //}
            //else
            //{
            //    string sql = @"select auto_ssc, auto_length, auto_crc, n_value, raw_packet, service_type, service_subtype from saved_requests where saved_request_id = " + idSavedReq;
            //    DataTable tblPacket = DbInterface.GetDataTable(sql);
            //    byte[] requestSaved = (byte[])tblPacket.Rows[0][4];
            //    requestPacket.RawContents = requestSaved;

            //    txtRawPacket.Text = requestPacket.GetString();
            //}

            txtRawPacket.Text = requestPacket.GetString();


            if (chkAutoLength.Checked)
            {
                mskPacketLength.Text = (requestPacket.Size - 6 - 1).ToString();
            }

            if (chkAutoCrc.Checked)
            {
                mskPacketCrc.Text = requestPacket.GetString().Substring(requestPacket.GetString().Length - 5, 5);
            }
        }

        #endregion

        #region Edicao, Validacao e Formatacao dos Grids

        private void gridAppData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (constructing) return;

            // LoadSubtypesInComboBoxCellGrid pode modificar uma celula de Text para Combo. Se isso ocorrer 
            // a partir do CellEnter em uma celula cujo indice da coluna eh igual ao indice da 
            // linha, o .net cai em um loop infinito. Isso eh um bug conhecido e registrado em
            // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=119366
            // A solucao adotada aqui foi baseada no que eh apresentado no link abaixo:
            // http://social.msdn.microsoft.com/Forums/en-US/winforms/thread/6f6ff25d-bdef-496e-9931-2abd03252ae8

            // Para o carregamento do subtipo em funcao do servico selecionado
            if (gridAppData.Columns[gridAppData.CurrentCell.ColumnIndex].HeaderText.ToUpper() == "SERVICE SUBTYPE")
            {
                if (gridAppData.CurrentCell.ColumnIndex == gridAppData.CurrentCell.RowIndex)
                {
                    // apenas nesta situacao preciso do workaround
                    CellEnterWorkaround workaround = new CellEnterWorkaround(CellEnterWorkaroundHandler);
                    gridAppData.BeginInvoke(workaround);
                    firstCellEnter = true;
                }
                else
                {
                    AppDataGridsHandling.LoadSubtypesInComboBoxCellGrid(gridAppData);
                }
            }
        }

        // Este metodo faz parte do workaround para o bug descrito em gridAppData_CellEnter
        private void CellEnterWorkaroundHandler()
        {
            if (firstCellEnter)
            {
                AppDataGridsHandling.LoadSubtypesInComboBoxCellGrid(gridAppData);
                firstCellEnter = false;
            }
        }

        private void gridAppData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!AppDataGridsHandling.ValidateCellValue(ref gridAppData, e.FormattedValue))
            {
                e.Cancel = true;
                return;
            }

            if ((e.FormattedValue == null) ||
                (e.FormattedValue.ToString().Equals("")) ||
                (e.FormattedValue.ToString().Contains("[Choose a service")) ||
                (e.FormattedValue.ToString().Contains("[There are no")))
            {
                return;
            }

            int i, j, numberOfBits;

            // Para o carregamento do grid de comandos embutidos (insert time-tagged TC e add event)
            if (gridAppData.Columns[e.ColumnIndex].HeaderText.ToUpper().Equals("SERVICE SUBTYPE"))
            {
                bool clearAppData = false;

                if (e.FormattedValue == null)
                {
                    clearAppData = true;
                }

                if (e.FormattedValue.Equals(gridAppData.CurrentCell.FormattedValue))
                {
                    clearAppData = true;
                }

                if (clearAppData)
                {
                    if (hasEmbeddedTCs)
                    {
                        // Zero a coluna appData, que poderia ter um valor anterior
                        gridAppData.CurrentRow.Cells[gridAppData.ColumnCount - 2].Value = null;
                        gridAppData.Columns[gridAppData.ColumnCount - 2].ToolTipText = "Application data, 0 bits";

                        return;
                    }
                }

                if (hasEmbeddedTCs)
                {
                    int serviceType = 0, serviceSubtype = 0;
                    string cellValue;

                    numberOfBits = 0;

                    foreach (DataGridViewColumn col in gridAppData.Columns)
                    {
                        if (col.HeaderText.ToUpper().Equals("SERVICE TYPE"))
                        {
                            if (gridAppData.CurrentRow.Cells[col.Index].Value == null)
                            {
                                // Ainda nao foi selecionado nenhum servico
                                return;
                            }

                            cellValue = gridAppData.CurrentRow.Cells[col.Index].Value.ToString();
                            serviceType = int.Parse(cellValue.Substring(1, 3));
                            break;
                        }
                    }

                    cellValue = e.FormattedValue.ToString();
                    if (cellValue.Contains("[There are no"))
                    {
                        return;
                    }

                    serviceSubtype = int.Parse(cellValue.Substring(1, 3));

                    i = cellValue.IndexOf("[", 8);
                    j = cellValue.IndexOf("]", 8);
                    numberOfBits = int.Parse(cellValue.Substring(i + 1, (j - i - 6)));

                    gridAppData.CurrentRow.Cells[gridAppData.ColumnCount - 2].Value = null;
                    gridAppData.CurrentRow.Cells[gridAppData.ColumnCount - 2].ToolTipText = "Application data, " + (numberOfBits + 16) + " bits";

                    // Atualiza o valor de packet_length (N, se houver, esta incluso em numberOfBits)
                    // (subtrai 1 para adequar ao PUS)
                    gridAppData.CurrentRow.Cells[7].Value = ((numberOfBits + 48) / 8) - 1;

                    // Inicializa o valor de appData zerado
                    byte[] appDataValue = new byte[((numberOfBits + 16) / 8)];
                    gridAppData.CurrentRow.Cells[gridAppData.ColumnCount - 2].Value = appDataValue;

                    ResizeAndUpdatePacket();
                }
            }
        }

        public void gridAppData_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (gridAppData.Columns[e.ColumnIndex].ToolTipText.Contains("Raw Hex, variable size"))
            {
                if (!ResizeAndUpdatePacket())
                {
                    // o redimensionamento do campo raw hex variavel estourou o tamanho do pacote;
                    // para nao dar mais erros, o numero de bits no campo eh zerado
                    gridAppData[e.ColumnIndex, e.RowIndex].ToolTipText = "Raw Hex, 0 bits";
                }
            }
            else
            {
                int serviceType = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));
                int serviceSubtype = 0;

                if (int.TryParse(cmbServiceSubtype.SelectedItem.ToString().Substring(1, 3), out serviceSubtype) == true)
                {
                    /** @attention: Estamos assumindo que o subtype [3.1] nao possui o campo N.
                     * O codigo dentro deste if trata o campo STRUCTURE ID como sendo da primeira posicao, ou seja, primeira coluna.
                     * Isso ocorre ao adquirir o valor do structureId.
                     **/
                    if ((serviceType == 3) && (serviceSubtype == 1))
                    {
                        // o request eh um Define New Housekeeping Report;
                        // se estiver saindo da coluna SubtypeStructure, devo carregar o grid
                        if ((e.ColumnIndex == 0) && (gridAppData.Columns[0].HeaderText.ToUpper() == "STRUCTURE ID"))
                        {
                            // se a celula estiver nula, nenhuma estrutura foi selecionada
                            if (gridAppData.Rows[0].Cells[0].Value != null)
                            {
                                if (gridAppData.ColumnCount > 1)
                                {
                                    structureId = int.Parse(gridAppData.Rows[0].Cells[0].Value.ToString().Substring(1, 5));
                                    string sql = "select count(*) from report_definition_structure where structure_id = " + structureId.ToString();

                                    int numberOfParameters = (int)DbInterface.ExecuteScalar(sql);

                                    // seta o numero de colunas
                                    gridAppData.Rows[0].Cells[2].Value = numberOfParameters;

                                    // habilita o timer que ira alimentar a estrutura e redimensionar o pacote
                                    tmrReportDefinition.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            UpdatePacket();
                        }
                    }
                    else
                    {
                        UpdatePacket();
                    }
                }
                else
                {
                    UpdatePacket();
                }
            }
        }

        private void gridAppData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (hasEmbeddedTCs)
            {
                //if (gridAppData.Columns.Count >= 16 &&
                //    gridAppData.Columns[0].HeaderText.Equals("Time: Seconds"))
                //{
                //    gridAppData.Columns[0].ReadOnly = true;
                //}

                if ((e.ColumnIndex == (gridAppData.ColumnCount - 1)) &&
                    (e.RowIndex >= 0))
                {
                    int serviceType;
                    int serviceSubtype;
                    int mainService;
                    int apid = 0;
                    int ssc = 0;
                    int serviceTypeIndex;
                    int serviceSubtypeIndex;
                    int apidIndex;
                    int sscIndex;
                    int packetLenghtIndex;

                    mainService = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));

                    serviceTypeIndex = 11;
                    serviceSubtypeIndex = 12;
                    apidIndex = 4;
                    sscIndex = 6;
                    packetLenghtIndex = 7;

                    if ((gridAppData[serviceTypeIndex, e.RowIndex].Value == null) ||
                        (gridAppData[serviceSubtypeIndex, e.RowIndex].Value == null))
                    {
                        MessageBox.Show("Select the Service Type / Subtype !!!",
                                            "Subtype Structure Load Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        serviceType = int.Parse(gridAppData[serviceTypeIndex, e.RowIndex].Value.ToString().Substring(1, 3));
                        serviceSubtype = int.Parse(gridAppData[serviceSubtypeIndex, e.RowIndex].Value.ToString().Substring(1, 3));
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

                    // Antes de chamar o FrmApplicationData, deve-se verificar se o type.subtype do 
                    // AppData tem campo para ser editado. A variavel isEditable levara essa referencia.
                    string sql = "select count(service_type) as hasAppData from subtype_structure where service_type = " + serviceType + " and service_subtype = " + serviceSubtype;
                    int hasAppData = (int)DbInterface.ExecuteScalar(sql);

                    bool isEditable = true;

                    if (hasAppData == 0)
                    {
                        isEditable = false;
                    }

                    bool rawPacketEmbeddedAlreadyEdited = true;

                    if (gridAppData.RowCount > 0)
                    {
                        if (hasAppData > 0)
                        {

                            if ((gridAppData.Rows[e.RowIndex].Cells[appDataColumnIndex].Tag != null) &&
                                 gridAppData.Rows[e.RowIndex].Cells[appDataColumnIndex].Tag.Equals("0"))
                            {
                                rawPacketEmbeddedAlreadyEdited = false;
                            }
                        }
                    }

                    // adquirir o type e subtype 11.4 ou 19.1
                    int specialType = (int)requestPacket.GetPart(56, 8);
                    int specialSubtype = (int)requestPacket.GetPart(64, 8);

                    RawPacket packetToLoad = GetEmbeddedPacketInSpecialTelecommands(specialType, specialSubtype, e.RowIndex);
                    int startBitToGetDataFields = 80; // @TODO hoje esta fixo, mas o ideal eh ser generico

                    FrmApplicationData form = new FrmApplicationData(serviceType, serviceSubtype, apid, ssc, isEditable
                        , rawPacketEmbeddedAlreadyEdited, startBitToGetDataFields, packetToLoad);

                    form.ShowDialog();


                    if (form.EmbeddedPacketAlreadyEdited)
                    {
                        gridAppData.Rows[e.RowIndex].Cells[appDataColumnIndex].Tag = "1";
                    }

                    if (isEditable)
                    {
                        if (form.Edited)
                        {
                            int rawPacketSize = form.GetEmbeddedRawPacket().Size;

                            if (rawPacketSize > 0)
                            {
                                // O formulario retornou dados; joga o vetor na celula e atualiza o tamanho
                                gridAppData[gridAppData.ColumnCount - 2, e.RowIndex].Value = form.GetEmbeddedRawPacket().RawContents;
                                gridAppData[gridAppData.ColumnCount - 2, e.RowIndex].ToolTipText = "Application data, " + (form.GetEmbeddedRawPacket().Size * 8) + " bits";

                                // Atualizar o packet_length do tttc (o packet_length do pacote principal eh atualizado ao redimensionar o pacote)
                                // soma o tamanho da area de dados + crc com os 4 bytes de cabecalhos do app data - 1 byte para adequar ao PUS
                                gridAppData[packetLenghtIndex, e.RowIndex].Value = form.GetEmbeddedRawPacket().Size + 4 - 1;
                            }
                        }

                        ResizeAndUpdatePacket();
                    }
                }
            }
        }

        /**
         * Este timer soh eh habilitado quando o usuario sai de uma coluna
         * com o Structure ID de um request 'Define New Housekeeping Report.
         * O timer eh necessario porque a criacao de novas colunas no grid
         * nao pode ocorrer dentro de um evento CellValidated.
         **/
        private void tmrReportDefinition_Tick(object sender, EventArgs e)
        {
            tmrReportDefinition.Enabled = false; // garante que nao vai disparar de novo

            string sql = @" select 
                                '[' + dbo.f_zero(a.parameter_id, 5) + '] ' + b.parameter_description
                            from 
                                report_definition_structure a
                                inner join parameters b on
                                    a.parameter_id = b.parameter_id
                            where 
                                a.structure_id = " + structureId.ToString() + @"
                            order by 
                                a.position";

            DataTable parameters = DbInterface.GetDataTable(sql);

            int columnIndex = 0;

            // remove todas as columas com parametros
            while (gridAppData.Columns.Count > 3)
            {
                gridAppData.Columns.Remove(gridAppData.Columns[3]);
            }

            // agora alimenta as colunas referentes a estrutura selecionada
            foreach (DataRow row in parameters.Rows)
            {
                gridAppData.Columns.Add("parameter" + columnIndex.ToString(), "Parameter ID");

                // marco a coluna como 'Table' para 'enganar' o ResizeAndUpdatePacket
                gridAppData.Columns[gridAppData.Columns.Count - 1].ToolTipText = "Table, 16 bits";
                gridAppData.Columns[gridAppData.Columns.Count - 1].ReadOnly = true;
                gridAppData.Columns[gridAppData.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                gridAppData.CurrentRow.Cells[columnIndex + 3].Value = row[0];

                columnIndex++;
            }

            // atualiza o tamanho do pacote
            ResizeAndUpdatePacket();

            structureId = 0;
        }

        #endregion

        #region Eventos
        //compoe os menus de contexto acionados a partir do grid datafield
        private void gridAppData_MouseClick(object sender, MouseEventArgs e)
        {
            int serviceType = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));
            int serviceSubtype = int.Parse(cmbServiceSubtype.SelectedItem.ToString().Substring(1, 3));

            if (e.Button == MouseButtons.Left
                && (serviceType == 11 && serviceSubtype == 4))
            {
                if (gridAppData.CurrentCell.ColumnIndex.Equals(0) && currentMouseOverRow >= 0)
                {
                    _frmExecutionTimeout = new FrmExecutionTimeout(false, 88, 32, requestPacket);
                    _frmExecutionTimeout.ShowDialog();
                    isRelative = _frmExecutionTimeout.IsRelative;

                    if (_frmExecutionTimeout.EmbeddedPacketAlreadyEdited)
                    {
                        _value = _frmExecutionTimeout.Value;
                        gridAppData.CurrentCell.Tag = "1";
                        gridAppData.CurrentCell.Value = _value;
                        _dateInHex = _frmExecutionTimeout.ValueDateHex;
                        _calendar = _frmExecutionTimeout.Calendar;
                        statusCalendar[gridAppData.CurrentRow.Index] = Convert.ToInt32(_calendar);
                        ResizeAndUpdatePacket();
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();

                if (numN.Visible == true)
                {
                    currentMouseOverRow = gridAppData.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow >= 0)
                    {
                        currentGridRowClick = currentMouseOverRow;

                        gridAppData.Rows[currentGridRowClick].Selected = true;

                        if (gridAppData.RowCount > 1)
                        {
                            m.MenuItems.Add("Move Up", new EventHandler(MoveUpClick));
                            m.MenuItems.Add("Move Down", new EventHandler(MoveDownClick));
                            m.MenuItems.Add(new MenuItem("-"));
                        }

                        m.MenuItems.Add("Delete", new EventHandler(deleteClick));

                        if (serviceType == 011 && serviceSubtype == 004)
                        {
                            m.MenuItems.Add(new MenuItem("-"));
                            //m.MenuItems.Add("Set Execution timeout", new EventHandler(ExecutionTimeout));
                            m.MenuItems[m.MenuItems.Count - 1].Enabled = true;
                            m.MenuItems[m.MenuItems.Count - 2].Enabled = true;
                        }
                        else if (serviceType == 019 && serviceSubtype == 001)
                        {
                            m.MenuItems.Add(new MenuItem("-"));
                            m.MenuItems[m.MenuItems.Count - 1].Enabled = false;
                        }
                    }

                    m.Show(gridAppData, new Point(e.X, e.Y));
                }
            }
        }

        private void MoveUpClick(Object sender, EventArgs e)
        {
            if (currentGridRowClick == 0)
            {
                return;
            }

            int gridCollums = gridAppData.ColumnCount;

            gridAppData.DataError += new DataGridViewDataErrorEventHandler(gridAppData_DataError);

            numN.Value = numN.Value + 1;

            if (gridAppData.ColumnCount >= 11)
            {
                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[11].Value = gridAppData.Rows[(currentGridRowClick - 1)].Cells[11].Value;
                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[11].ToolTipText = gridAppData.Rows[(currentGridRowClick - 1)].Cells[11].ToolTipText;

                //aqui o currentCell eh a celula do ultimo row invisivel
                gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[11];

                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[12].Value = gridAppData.Rows[(currentGridRowClick - 1)].Cells[12].Value;
                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[12].ToolTipText = gridAppData.Rows[(currentGridRowClick - 1)].Cells[12].ToolTipText;

                gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[12];
            }

            for (int i = 0; i < gridCollums; i++)
            {
                MoveUpDown(true, i);
            }

            numN.Value = numN.Value - 1;
            ResizeAndUpdatePacket();
        }

        private void MoveDownClick(Object sender, EventArgs e)
        {
            if (currentGridRowClick == (gridAppData.RowCount - 1))
            {
                return;
            }

            int gridCollums = gridAppData.ColumnCount;

            gridAppData.DataError += new DataGridViewDataErrorEventHandler(gridAppData_DataError);

            numN.Value = numN.Value + 1;

            if (gridAppData.ColumnCount >= 11)
            {
                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[11].Value = gridAppData.Rows[(currentGridRowClick + 1)].Cells[11].Value;
                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[11].ToolTipText = gridAppData.Rows[(currentGridRowClick + 1)].Cells[11].ToolTipText;

                gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[11];

                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[12].Value = gridAppData.Rows[(currentGridRowClick + 1)].Cells[12].Value;
                gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[12].ToolTipText = gridAppData.Rows[(currentGridRowClick + 1)].Cells[12].ToolTipText;

                gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[12];
            }

            for (int i = 0; i < gridCollums; i++)
            {
                MoveUpDown(false, i);
            }

            numN.Value = numN.Value - 1;
            ResizeAndUpdatePacket();
        }

        void deleteClick(Object sender, EventArgs e)
        {
            gridAppData.Rows.RemoveAt(currentGridRowClick);
            int atual = Convert.ToInt32(numN.Value);
            numN.Value = atual - 1;
        }

        private void MoveUpDown(bool up, int currentCollum)
        {
            update = false;

            if (up)
            {
                if ((currentCollum != 12))
                {
                    // guarda o row -1 no row temporario
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value = gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].ToolTipText;
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Tag = gridAppData.Rows[currentGridRowClick - 1].Cells[currentCollum].Tag;

                    if ((currentCollum != 14) && (currentCollum != 12))
                    {
                        gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum];
                    }

                    // joga no row -1 o currentRow
                    gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].Value = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value;
                    gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText;
                    gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].Tag = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Tag;

                    if ((currentCollum != 14) && (currentCollum != 12))
                    {
                        gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum];
                    }

                    // agora joga o row temporario no currentRow
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText;
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Tag = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Tag;

                    if ((currentCollum != 14) && (currentCollum != 12))
                    {
                        gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick)].Cells[currentCollum];
                    }
                }
                else // = 12
                {
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value = gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].ToolTipText;

                    gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum];

                    gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].Value = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value;
                    gridAppData.Rows[(currentGridRowClick - 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText;

                    gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick)].Cells[currentCollum];

                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText;

                    gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum];
                }

                gridAppData.Rows[(currentGridRowClick - 1)].Selected = true;
            }
            else
            {
                if ((currentCollum != 12))
                {
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].ToolTipText;
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Tag = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].Tag;

                    if ((currentCollum != 14) && (currentCollum != 12))
                    {
                        gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum];
                    }

                    gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].Value = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value;
                    gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText;
                    gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].Tag = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Tag;

                    if ((currentCollum != 14) && (currentCollum != 12))
                    {
                        gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum];
                    }

                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText;
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Tag = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Tag;

                    if ((currentCollum != 14) && (currentCollum != 12))
                    {
                        gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick)].Cells[currentCollum];
                    }
                }
                else // = 12
                {
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].ToolTipText;

                    gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum];

                    gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].Value = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value;
                    gridAppData.Rows[(currentGridRowClick + 1)].Cells[currentCollum].ToolTipText = gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText;

                    gridAppData.CurrentCell = gridAppData.Rows[(currentGridRowClick)].Cells[currentCollum];

                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].Value = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].Value;
                    gridAppData.Rows[currentGridRowClick].Cells[currentCollum].ToolTipText = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum].ToolTipText;

                    gridAppData.CurrentCell = gridAppData.Rows[(gridAppData.Rows.Count - 1)].Cells[currentCollum];
                }

                gridAppData.Rows[(currentGridRowClick + 1)].Selected = true;
            }

            update = true;
        }

        void gridAppData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //para que os erros do .net sobre o grid voltem a aparecer retirar o comentario da linha abaixo
            //throw new NotImplementedException();
        }

        private void gridAppData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentMouseOverRow = e.RowIndex;
        }

        private void gridAppData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            currentMouseOverRow = -1;
        }

        #endregion
    }
}
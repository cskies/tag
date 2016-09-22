/**
 * @file 	    FrmFramesCoding.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    26/07/2009
 * @note	    Modificado em 30/11/2012 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Ccsds;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmFramesCoding
     * Formulario com ferramentas para codificacao e decodificacao de frames CCSDS.
     * @attention Este formulario e as classes de codificacao/decodificacao de TC/TM
     * associadas foram criadas para uso no processo de aceitacao da Unidade de TM
     * e TC (UTMC) do COMAV. O codigo aqui nao foi pensado para uso para testes ou
     * operacao do OBC atraves de CCSDS!
     **/
    public partial class FrmFramesCoding : DockContent
    {
        #region Atributos

        private RawFrame frameTC = new RawFrame(true);
        bool tcsCheckedChanged = false;
        byte[] cltu;
        String cltuFilePath = "";

        private MdiMain mdiMain = null;
        private FrmConnectionWithEgse frmConnectionWithEgse = null;
        private FrmCommRS422 frmCommRs422 = null;
        private FileHandling fileHandling = null;

        // Delegate usado na escrita dos frames recebidos no grid.
        delegate void FillFrameGridAndFileCallBack(byte[] frame, bool validCRC);

        private FillFrameGridAndFileCallBack fillFrameGridCallBack = null;
        private object[] frameArgs = new object[2];
        private bool receptionInRealTime = false;

        // o CCSDS versao 1987 permite que apenas um pacote por frame de TC.
        // esta variavel eh utilizada para garantir isso
        private bool packetAlreadySelected = false;

        #endregion

        #region Propriedades

        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            get
            {
                return frmConnectionWithEgse;
            }
            set
            {
                frmConnectionWithEgse = value;
            }
        }

        public FrmCommRS422 FormCommRs422
        {
            get
            {
                return frmCommRs422;
            }
            set
            {
                frmCommRs422 = value;
            }
        }

        public bool ReceptionInRealTime
        {
            get
            {
                return receptionInRealTime;
            }
            set
            {
                receptionInRealTime = value;
            }
        }

        #endregion

        #region Construtor

        public FrmFramesCoding(MdiMain mdi)
        {
            InitializeComponent();
            mdiMain = mdi;
            frmConnectionWithEgse = mdiMain.FormConnectionWithEgse;

            if (frmConnectionWithEgse != null)
            {
                frmConnectionWithEgse.FormFramesCoding = this;
            }
        }

        #endregion

        #region Tratamento de Eventos da Interface Grafica

        private void frmCoderDecoder_Load(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.tm_file_received_data))
            {
                txtReceivedDataPath.Text = Properties.Settings.Default.tm_file_received_data;
                btDecode.Enabled = true;
                chkSearchAsm.Enabled = true;
            }
            else
            {
                txtReceivedDataPath.Text = "C:\\";
            }

            txtRealTimeReceptionPath.Text = Properties.Settings.Default.frames_log_file_path;
            txtTCFilePath.Text = Properties.Settings.Default.open_tc_file;
            
            rbNoCoding.Checked = true;

            ConfigureGrids();

            cmbFrameType.SelectedIndex = 0;
            cmbSeqFlags.SelectedIndex = 3;

            cbAutoIncrement.Checked = true;

            UpdateFrame();

            if (mdiMain.FormConnectionWithEgse != null)
            {
                frmConnectionWithEgse = mdiMain.FormConnectionWithEgse;

                if (frmConnectionWithEgse.Connected)
                {
                    btRealTimeReception.Enabled = true;
                    chkShowInRealTime.Enabled = true;
                    
                    // Se o SMC estiver conectado, entao deve verificar se a tela FramesCoding esta recebendo para nao permitir que duas telas recebam dados.
                    // Enquanto uma recebe a outra deve estar desabilitada para recepcao.
                    if (mdiMain.FormCommRs422 != null)
                    {
                        frmCommRs422 = mdiMain.FormCommRs422;

                        if (frmCommRs422.Receiving)
                        {
                            btRealTimeReception.Enabled = false;
                            chkShowInRealTime.Enabled = false;
                        }
                        else
                        {
                            btRealTimeReception.Enabled = true;
                            chkShowInRealTime.Enabled = true;
                        }
                    }
                }
                else
                {
                    btRealTimeReception.Enabled = false;
                    chkShowInRealTime.Enabled = false;
                }
            }

            // carrega as configuracoes salvas no banco de dados
            DbConfiguration.Load();
            mskVersion.Text = DbConfiguration.TcFrameVersion.ToString();
            cmbFrameType.SelectedIndex = DbConfiguration.TcFrameType;
            mskResA.Text = DbConfiguration.TcFrameReservedA.ToString();
            mskSpacecraftID.Text = DbConfiguration.TcFrameScid.ToString();
            mskVCID.Text = DbConfiguration.TcFrameVcid.ToString();
            mskResB.Text = DbConfiguration.TcFrameReservedB.ToString();
            cmbSeqFlags.SelectedIndex = DbConfiguration.TcFrameSeqFlags;
            mskMapId.Text = DbConfiguration.TcFrameMapid.ToString();
        }

        private void cbUnlock_CheckedChanged(object sender, EventArgs e)
        {
            mskFrameLength.ReadOnly = cbAutoLength.Checked;
            if (mskFrameLength.ReadOnly)
            {
                mskFrameLength.BackColor = SystemColors.Control;
                UpdateFrame();
            }
            else
            {
                mskFrameLength.BackColor = SystemColors.Window;
            }
        }

        private void cbAutoIncrement_CheckedChanged(object sender, EventArgs e)
        {
            mskFrameSeq.ReadOnly = cbAutoIncrement.Checked;
            if (mskFrameSeq.ReadOnly)
            {
                mskFrameSeq.BackColor = SystemColors.Control;
            }
            else
            {
                mskFrameSeq.BackColor = SystemColors.Window;
            }
        }

        private void btOpenTCFile_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Properties.Settings.Default.open_tc_file;
            openFileDialog.Filter = "TC Packet Files (*.tc)|*.tc";
            openFileDialog.FilterIndex = 0;
            openFileDialog.FileName = "";
            openFileDialog.Title = "Open a TC packet file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    txtTCFilePath.Text = openFileDialog.FileName;
                    FillTCsGrid(openFileDialog.FileName);

                    packetAlreadySelected = false;
                }
                else
                {
                    MessageBox.Show("Invalid TC File!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // salvar caminho do arquivo
                Properties.Settings.Default.open_tc_file = openFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void mskVersion_Leave(object sender, EventArgs e)
        {
            validateFields(mskVersion, 3);
        }

        private void mskResA_Leave(object sender, EventArgs e)
        {
            validateFields(mskResA, 3);
        }

        private void mskSpacecraftID_Leave(object sender, EventArgs e)
        {
            validateFields(mskSpacecraftID, 1023);
        }

        private void mskVCID_Leave(object sender, EventArgs e)
        {
            validateFields(mskVCID, 63);
        }

        private void mskFrameLength_Leave(object sender, EventArgs e)
        {
            validateFields(mskFrameLength, 255);
        }

        private void mskFrameSeq_Leave(object sender, EventArgs e)
        {
            validateFields(mskFrameSeq, 255);
        }

        private void validateFields(MaskedTextBox field, int size)
        {
            if (field.Text.Trim().Equals(""))
            {
                field.Text = "0";
            }
            else
            {
                if (int.Parse(field.Text) > size)
                {
                    MessageBox.Show("Value greater than the field supports!\n\nMaximum allowed value is '" + size.ToString() + "'.",
                                    "Invalid Value!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    field.Focus();
                }
            }

            // Aproveito e atualizo o frame
            UpdateFrame();
        }

        private void mskResB_Leave(object sender, EventArgs e)
        {
            validateFields(mskResB, 3);
        }

        private void cmbFrameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFrame();

            cmbControlCommand.SelectedIndex = 0;

            if (cmbFrameType.SelectedIndex == 3)
            {
                cmbControlCommand.Visible = true;
                label15.Visible = true;
            }
            else
            {
                cmbControlCommand.Visible = false;
                label15.Visible = false;
            }
        }

        private void cbAutoCRC_CheckedChanged(object sender, EventArgs e)
        {
            mskFrameCRC.ReadOnly = cbAutoCRC.Checked;
            frameTC.AutoCrc = cbAutoCRC.Checked;

            if (mskFrameCRC.ReadOnly)
            {
                mskFrameCRC.BackColor = SystemColors.Control;
                UpdateFrame(); // recalcula o frame e atualiza a visualizacao
            }
            else
            {
                mskFrameCRC.BackColor = SystemColors.Window;
            }
        }

        private void gridTCs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (!bool.Parse(gridTCs[4, e.RowIndex].FormattedValue.ToString())) // o valor atual eh false, o usuario clicou para torna-lo true
                {
                    if (packetAlreadySelected == true)
                    {
                        MessageBox.Show("The current CCSDS version used by SMC (referred in ESA-PSS-04-107) allows, when segmentation is not used, just one packet per frame.",
                                        "Impossible to add packet",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridTCs.CancelEdit();
                        return;
                    }

                    // Verifica se o pacote ira caber no frame (o tamanho maximo eh 256 bytes)
                    if ((frameTC.Size + gridTCs[5, e.RowIndex].Value.ToString().Length / 2) > 256)
                    {
                        MessageBox.Show("Maximum TC frame size (256 bytes) exceeded!\n\nImpossible to add this packet.",
                                        "Frame Size Exceeded",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridTCs.CancelEdit(); // cancela o clique do usuario
                        return;
                    }

                    packetAlreadySelected = true;
                }
                else
                {
                    packetAlreadySelected = false;
                }

                tcsCheckedChanged = true;

                // Forca a saida da celula para que o valor seja enviado ao grid
                gridTCs.CurrentCell = gridTCs[0, gridTCs.CurrentRow.Index];
            }
        }

        private void gridTCs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (tcsCheckedChanged)
            {
                UpdateFrame();
                tcsCheckedChanged = false;
            }
        }

        private void btAddCLTUToFile_Click(object sender, EventArgs e)
        {
            String line;

            // Monta a linha
            line = gridTCFrame[1, 1].Value.ToString().Replace("-", "");
            line += "\t// original frame: ";
            line += gridTCFrame[1, 0].Value.ToString().Replace("-", "");

            // Adiciona a linha ao arquivo
            try
            {
                StreamWriter file = File.AppendText(cltuFilePath);
                file.WriteLine(line);
                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (gridTCs.Visible && (!txtTCFilePath.Text.Equals("")))
            {
                FillTCsGrid(txtTCFilePath.Text);
            }

            // Se o incremento for automatico...
            if (cbAutoIncrement.Checked)
            {
                mskFrameSeq.Text = (int.Parse(mskFrameSeq.Text) + 1).ToString();
            }

            UpdateFrame();

            MessageBox.Show("CLTU added to file successfully !", "CLTU added to file", MessageBoxButtons.OK, MessageBoxIcon.Information);
            packetAlreadySelected = false;
        }

        private void mskFrameCRC_Leave(object sender, EventArgs e)
        {
            if (!cbAutoCRC.Checked)
            {
                mskFrameCRC.Text = mskFrameCRC.Text.ToUpper();
                int parsedValue = 0;

                if (int.TryParse(mskFrameCRC.Text.Replace("-", ""), System.Globalization.NumberStyles.HexNumber, null, out parsedValue))
                {
                    UpdateFrame();
                }
                else
                {
                    MessageBox.Show("CRC is not an hex value!", "Invalid CRC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mskFrameCRC.Focus();
                }
            }
        }

        private void cmbControlCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbControlCommand.SelectedIndex)
            {
                case 0: // invalid
                    {
                        label11.Visible = true;
                        txtTCFilePath.Visible = true;
                        btOpenTCFile.Visible = true;
                        gridTCs.Visible = true;

                        label16.Visible = false;
                        label17.Visible = false;
                        label18.Visible = false;
                        mskFirstOctect.Visible = false;
                        mskSecondOctect.Visible = false;
                        mskThirdOctect.Visible = false;

                        label23.Visible = true;
                        label24.Visible = true;
                        label25.Visible = true;
                        cmbSeqFlags.Visible = true;
                        mskMapId.Visible = true;

                        break;
                    }
                case 1: // SET V(R)
                    {
                        label11.Visible = false;
                        txtTCFilePath.Visible = false;
                        btOpenTCFile.Visible = false;
                        gridTCs.Visible = false;

                        label16.Text = "1st Octect";
                        mskFirstOctect.Text = "130";
                        label16.Visible = true;
                        label17.Visible = true;
                        label18.Visible = true;
                        mskFirstOctect.Visible = true;
                        mskSecondOctect.Visible = true;
                        mskThirdOctect.Visible = true;

                        label23.Visible = false;
                        label24.Visible = false;
                        label25.Visible = false;
                        cmbSeqFlags.Visible = false;
                        mskMapId.Visible = false;

                        break;
                    }
                case 2: // UNLOCK
                    {
                        label11.Visible = false;
                        txtTCFilePath.Visible = false;
                        btOpenTCFile.Visible = false;
                        gridTCs.Visible = false;

                        label16.Text = "Single\nOctect";
                        mskFirstOctect.Text = "0";
                        label16.Visible = true;
                        label17.Visible = false;
                        label18.Visible = false;
                        mskFirstOctect.Visible = true;
                        mskSecondOctect.Visible = false;
                        mskThirdOctect.Visible = false;

                        label23.Visible = false;
                        label24.Visible = false;
                        label25.Visible = false;
                        cmbSeqFlags.Visible = false;
                        mskMapId.Visible = false;

                        break;
                    }
                default: break;
            }

            UpdateFrame();
        }

        private void mskFirstOctect_Leave(object sender, EventArgs e)
        {
            validateFields(mskFirstOctect, 255);
        }

        private void mskSecondOctect_Leave(object sender, EventArgs e)
        {
            validateFields(mskSecondOctect, 255);
        }

        private void mskThirdOctect_Leave(object sender, EventArgs e)
        {
            validateFields(mskThirdOctect, 255);
        }

        private void btCreateNewCLTUFile_Click(object sender, EventArgs e)
        {
            newFileDialog.InitialDirectory = Application.StartupPath;
            newFileDialog.Filter = "CLTU Files (*.cltu)|*.cltu";
            newFileDialog.FilterIndex = 0;
            newFileDialog.FileName = "new_cltu_file.cltu";
            newFileDialog.Title = "Create a CLTU File";

            // Nao preciso verificar se o arquivo ja existe; o dialogo pergunta ao usuario por mim
            if (newFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(newFileDialog.FileName))
                    {
                        File.Delete(newFileDialog.FileName);
                    }

                    cltuFilePath = newFileDialog.FileName;

                    // Cabecalho
                    TextWriter file = new StreamWriter(cltuFilePath);

                    file.WriteLine("// CLTU File, created with SUBORD's CCSDS Encoder/Decoder Tool on " + DateTime.Now.ToString());
                    file.WriteLine(""); // linha vazia
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("New CLTU file created successfully!", "File Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btAddCLTUToFile.Enabled = true;
                btCloseCLTUFile.Enabled = true;
            }
        }

        private void btOpenCLTUFile_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "CLTU Files (*.cltu)|*.cltu";
            openFileDialog.FilterIndex = 0;
            openFileDialog.FileName = "";
            openFileDialog.Title = "Create a CLTU File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    cltuFilePath = openFileDialog.FileName;
                    btAddCLTUToFile.Enabled = true;
                    btCloseCLTUFile.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invalid CLTU File!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btCloseCLTUFile_Click(object sender, EventArgs e)
        {
            cltuFilePath = "";
            btAddCLTUToFile.Enabled = false;
            btCloseCLTUFile.Enabled = false;
        }

        private void btSelectDataFile_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Properties.Settings.Default.tm_file_received_data;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.FileName = "";
            openFileDialog.Title = "Open a received data file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridTMFrames.Rows.Clear();

                if (File.Exists(openFileDialog.FileName))
                {
                    txtReceivedDataPath.Text = openFileDialog.FileName;
                    rbNoCoding.Enabled = true;
                    rbConvolutional.Enabled = true;
                    rbReedSolomon.Enabled = true;
                    rbBoth.Enabled = true;
                    btDecode.Enabled = true;
                    chkSearchAsm.Enabled = true;
                    
                    // salvar caminho do arquivo
                    Properties.Settings.Default.tm_file_received_data = openFileDialog.FileName;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Invalid File!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtReceivedDataPath.Text = "";
                    rbNoCoding.Enabled = false;
                    rbConvolutional.Enabled = false;
                    rbReedSolomon.Enabled = false;
                    rbBoth.Enabled = false;
                    btDecode.Enabled = false;
                    chkSearchAsm.Enabled = false;
                }
            }
        }

        private void btClearTMFrames_Click(object sender, EventArgs e)
        {
            gridTMFrames.Rows.Clear();
        }

        private void btDecode_Click(object sender, EventArgs e)
        {
            gridTMFrames.Rows.Clear();

            TmDecoder decoder = new TmDecoder();
            Byte[] tmFrame = null;

            String intervalBetweenFrames = "";

            decoder.InputFilePath = txtReceivedDataPath.Text;

            if (rbConvolutional.Checked || rbBoth.Checked)
            {
                decoder.UseConvolutional = true;
            }

            if (rbReedSolomon.Checked || rbBoth.Checked)
            {
                decoder.UseReedSolomon = true;
            }

            decoder.DataWithAsm = chkSearchAsm.Checked;
            tmFrame = decoder.GetNextFrame();

            while (tmFrame != null)
            {
                if (decoder.BitIntervalFound)
                {
                    intervalBetweenFrames += gridTMFrames.RowCount.ToString() + " and " + (gridTMFrames.RowCount + 1).ToString() + ", ";
                }

                AddTMFrameToGridAndFile(tmFrame, decoder.ValidCrc);

                btClearTMFrames.Enabled = true;
                tmFrame = decoder.GetNextFrame();
            }

            if (gridTMFrames.RowCount == 0)
            {
                MessageBox.Show("No frame were found in the file using the selected decode technique.\n\nTry using another one.",
                                "No Frame Found !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (!intervalBetweenFrames.Equals(""))
                {
                    MessageBox.Show("A total of " + gridTMFrames.RowCount + " frames were found. However, a bit interval was found between the " +
                                    "following frames (in ordinal sequence):\n" + intervalBetweenFrames.Substring(0, intervalBetweenFrames.Length - 2) + ".",
                                    "Bit Interval Found !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("A total of " + gridTMFrames.RowCount + " frames were found.",
                                    "Frames Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void mskMapId_Leave(object sender, EventArgs e)
        {
            validateFields(mskMapId, 63);
        }

        private void cmbSeqFlags_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFrame();
        }

        private void FrmFramesCoding_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(924, 530);
            }
        }

        private void btSaveToBinary_Click(object sender, EventArgs e)
        {
            String filePath;

            newFileDialog.InitialDirectory = Application.StartupPath;
            newFileDialog.Filter = "Binary Files (*.dat)|*.dat";
            newFileDialog.FilterIndex = 0;
            newFileDialog.FileName = "new_cltu_file.dat";
            newFileDialog.Title = "Create a Binary CLTU File";

            // Nao preciso verificar se o arquivo ja existe; o dialogo pergunta ao usuario por mim
            if (newFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(newFileDialog.FileName))
                    {
                        File.Delete(newFileDialog.FileName);
                    }

                    filePath = newFileDialog.FileName;

                    // abre o arquivo para escrita
                    System.IO.FileStream fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                    // escreve a cltu no arquivo
                    fileStream.Write(cltu, 0, cltu.Length);

                    // fecha o arquivo
                    fileStream.Close();

                    MessageBox.Show("New CLTU binary file created successfully!", "Binary File Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool FileIsEmpty(string path)
        {
            string fileData = File.ReadAllText(path);

            if (fileData.Trim().Equals(""))
            {
                return true;
            }

            return false;
        }

        public void btRealTimeReception_Click_1(object sender, EventArgs e)
        {
            if (btRealTimeReception.Text.Equals("Start Real-Time Reception (No Coding)"))
            {
                if (frmConnectionWithEgse != null && frmConnectionWithEgse.Connected)
                {
                    if (chkLogToFile.Checked)
                    {
                        bool checkingFile = true;

                        while (checkingFile)
                        {
                            // Verificar se realmente existe arquivo no path indicado.
                            if (txtRealTimeReceptionPath.Text.Equals(""))
                            {
                                MessageBox.Show("Please, select a file to record TM frames!",
                                                "File Error!",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);
                                return;
                            }

                            // Verificar se o arquivo realmente existe
                            if (!File.Exists(txtRealTimeReceptionPath.Text))
                            {
                                MessageBox.Show("The file to record received TM frames not exists!\n\nSelect other and try again.",
                                                "File Error!",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);
                                return;
                            }

                            // Verificar se arquivo ja possui frames. 
                            // Se sim, perguntar se deseja sobrescrever.
                            if (!FileIsEmpty(txtRealTimeReceptionPath.Text))
                            {
                                if (MessageBox.Show("This file already contain data!\n\nDo you want to overwrite it?",
                                    "Please Confirm Log File!",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    btPathFile_Click_1(null, new EventArgs());
                                    checkingFile = false;
                                }
                                else
                                {
                                    File.WriteAllText(txtRealTimeReceptionPath.Text, "");
                                }
                            }
                            else
                            {
                                checkingFile = false;
                            }

                            // Instanciar o FileHandling para escrever 1 frame em cada linha do arquivo.
                            fileHandling = new FileHandling(txtRealTimeReceptionPath.Text, false, false);
                        }
                    }

                    // Assina o Event da tela ConnectionWithEgse, que disponibiliza os frames recebidos.
                    frmConnectionWithEgse.availableFrameEventHandler += new AvailableFrameEventHandler(FrameReceived);

                    // Instancia um novo CallBack do tipo delegate, adicionando o metodo que sera executado.
                    // Este eh para escrever os frames no grid pela thread principal.
                    // Sem ele, os dados nao conseguem chegar na thread principal, ficando na thread secundaria que eh a de recepcao.
                    // Este eh usado pelo metodo Invoke.
                    fillFrameGridCallBack = new FillFrameGridAndFileCallBack(AddTMFrameToGridAndFile);

                    btRealTimeReception.Text = "Stop Real Time Reception";

                    receptionInRealTime = true;

                    txtRealTimeReceptionPath.Enabled = false;
                    btSelectDataFile.Enabled = false;
                    chkLogToFile.Enabled = false;
                    btPathFile.Enabled = false;
                    btDecode.Enabled = false;

                    // Desabilitar o botao da tela CommRs422, se estiver aberta
                    if (mdiMain.FormCommRs422 != null)
                    {
                        frmCommRs422 = mdiMain.FormCommRs422;
                        frmCommRs422.btEnableReception.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("The SMC is without connection!",
                                    "Connection Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                frmConnectionWithEgse.availableFrameEventHandler -= new AvailableFrameEventHandler(FrameReceived);
                btRealTimeReception.Text = "Start Real-Time Reception (No Coding)";

                btSelectDataFile.Enabled = true;
                chkLogToFile.Enabled = true;
                btPathFile.Enabled = chkLogToFile.Checked;
                txtRealTimeReceptionPath.Enabled = chkLogToFile.Checked;

                if (!txtReceivedDataPath.Text.Equals(""))
                {
                    btDecode.Enabled = true;
                }

                receptionInRealTime = false;

                // Habilitar o botao da tela CommRs422, se estiver aberta
                if (mdiMain.FormCommRs422 != null)
                {
                    frmCommRs422 = mdiMain.FormCommRs422;
                    frmCommRs422.btEnableReception.Enabled = true;
                }
            }
        }

        private void chkLogToFile_CheckedChanged(object sender, EventArgs e)
        {
            btPathFile.Enabled = chkLogToFile.Checked;
            txtRealTimeReceptionPath.Enabled = chkLogToFile.Checked;
        }

        private void btPathFile_Click_1(object sender, EventArgs e)
        {
            String initialPath;
            if (txtRealTimeReceptionPath.Text != null)
            {
                initialPath = txtRealTimeReceptionPath.Text;
            }
            else
            {
                initialPath = Application.StartupPath;
            }
            saveDialog.InitialDirectory = initialPath;
            saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveDialog.FilterIndex = 0;
            saveDialog.FileName = "DCT_" + DateTime.Today.Date.Year + "-" + DateTime.Today.Date.Month + "-" + DateTime.Today.Date.Day + ".txt";
            saveDialog.Title = "Create a file to record received frames";


            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                numOfLines = 1;
                txtRealTimeReceptionPath.Text = saveDialog.FileName;
                File.WriteAllText(txtRealTimeReceptionPath.Text, ""); // cria um arquivo em branco

                // salvar caminho do arquivo
                Properties.Settings.Default.frames_log_file_path = txtRealTimeReceptionPath.Text;
                Properties.Settings.Default.Save();

                btRealTimeReception.Enabled = true;
                chkShowInRealTime.Enabled = true;
            }
            else
            {
                return;
            }
            
        }

        /** Importa frames de TM gerados no projeto Amazonia-1. **/
        private void btImportAmazonia1TM_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Metodos Privados

        private int numOfLines = 1;

        /** Configura todos os grids deste formulario. **/
        private void ConfigureGrids()
        {
            gridTCs.Columns.Add("description", "Description (if available)");

            gridTCs.Columns.Add("packet_header", "Packet Header");
            gridTCs.Columns.Add("packet_data_field", "Packet Data Field");
            gridTCs.Columns.Add("crc", "CRC");

            foreach (DataGridViewColumn col in gridTCs.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();

            check.Name = "add_to_frame";
            check.HeaderText = "Add to Frame";
            gridTCs.Columns.Add(check);

            gridTCs.Columns.Add("complete_packet", "Complete Packet");

            gridTCs.Columns[0].Width = 150;
            gridTCs.Columns[1].Width = 130;
            gridTCs.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridTCs.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridTCs.Columns[3].Width = 40;
            gridTCs.Columns[4].Width = 80;
            gridTCs.Columns[5].Visible = false;

            gridTCs.Columns[0].ReadOnly = true;
            gridTCs.Columns[1].ReadOnly = true;
            gridTCs.Columns[2].ReadOnly = true;
            gridTCs.Columns[3].ReadOnly = true;

            gridTCFrame.Columns.Add("row_header", "rowHeader");
            gridTCFrame.Columns.Add("frame", "frame");
            gridTCFrame.Rows.Add(1);
            gridTCFrame[0, 0].Value = "Raw Frame";
            gridTCFrame[0, 1].Value = "CLTU";
            gridTCFrame.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridTCFrame.Columns[0].Width = 80;
            gridTCFrame.Columns[1].Width = 430;
            gridTCFrame.ReadOnly = true;

            gridTMFrames.Columns.Add("version_number", "Version Nr.");
            gridTMFrames.Columns.Add("scid", "SCID");
            gridTMFrames.Columns.Add("vcid", "VCID");
            gridTMFrames.Columns.Add("op_control_flag", "Op.Control Field Flag");
            gridTMFrames.Columns.Add("master_count", "Master Channel Frame Count");
            gridTMFrames.Columns.Add("vc_count", "Virtual Channel Frame Count");
            gridTMFrames.Columns.Add("second_header_flag", "Secondary Header Flag");
            gridTMFrames.Columns.Add("synch_flag", "Synch. Flag");
            gridTMFrames.Columns.Add("packet_order_flag", "Packet Order Flag");
            gridTMFrames.Columns.Add("segment_length_id", "Segment Length ID");
            gridTMFrames.Columns.Add("first_header_pointer", "First Header Pointer");

            gridTMFrames.Columns.Add("frame_data_field", "Frame Data Field");

            gridTMFrames.Columns.Add("control_word_type", "Control Word Type");
            gridTMFrames.Columns.Add("clcw_version_number", "CLCW Version Nr.");
            gridTMFrames.Columns.Add("status_field", "Status Field");
            gridTMFrames.Columns.Add("cop_in_effect", "COP in Effect");
            gridTMFrames.Columns.Add("clcw_vcid", "VCID (TC)");
            gridTMFrames.Columns.Add("reserved", "Reserved");
            gridTMFrames.Columns.Add("no_rf_available", "No RF Available");
            gridTMFrames.Columns.Add("no_bit_lock", "No Bit Lock");
            gridTMFrames.Columns.Add("lockout", "Lock Out");
            gridTMFrames.Columns.Add("wait", "Wait");
            gridTMFrames.Columns.Add("retransmit", "Retransmit");
            gridTMFrames.Columns.Add("frame_b_counter", "FARM B Counter");
            gridTMFrames.Columns.Add("report_type", "Report Type");
            gridTMFrames.Columns.Add("report_value", "Report Value");

            gridTMFrames.Columns.Add("frame_error_control", "Frame Error Control");

            gridTMFrames.Columns.Add("frame_error_control_ok", "Frame Error Control OK?");

            gridTMFrames.Columns[0].Width = 50;
            gridTMFrames.Columns[1].Width = 39;
            gridTMFrames.Columns[2].Width = 40;
            gridTMFrames.Columns[3].Width = 63;
            gridTMFrames.Columns[4].Width = 89;
            gridTMFrames.Columns[5].Width = 86;
            gridTMFrames.Columns[6].Width = 73;
            gridTMFrames.Columns[7].Width = 73;
            gridTMFrames.Columns[8].Width = 65;
            gridTMFrames.Columns[9].Width = 60;
            gridTMFrames.Columns[10].Width = 70;
            gridTMFrames.Columns[11].Width = 195;
            gridTMFrames.Columns[12].Width = 67;
            gridTMFrames.Columns[13].Width = 65;
            gridTMFrames.Columns[14].Width = 44;
            gridTMFrames.Columns[15].Width = 48;
            gridTMFrames.Columns[16].Width = 39;
            gridTMFrames.Columns[17].Width = 58;
            gridTMFrames.Columns[18].Width = 56;
            gridTMFrames.Columns[19].Width = 44;
            gridTMFrames.Columns[20].Width = 35;
            gridTMFrames.Columns[21].Width = 31;
            gridTMFrames.Columns[22].Width = 66;
            gridTMFrames.Columns[23].Width = 56;
            gridTMFrames.Columns[24].Width = 47;
            gridTMFrames.Columns[25].Width = 49;
            gridTMFrames.Columns[26].Width = 68;
            gridTMFrames.Columns[27].Width = 70;

            foreach (DataGridViewColumn col in gridTMFrames.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            gridTMFrames.ReadOnly = true;
            gridTMFrames.MultiSelect = false;
        }

        /** Preenche o grid de TCs com base em um arquivo-texto de TCs. **/
        private void FillTCsGrid(String filePath)
        {
            gridTCs.Rows.Clear();

            StreamReader file = File.OpenText(filePath);

            String line = file.ReadLine();

            while (line != null)
            {
                // Atencao: esta eh uma forma "preguicosa" de identificar um TC
                // foi feita pq o programa foi feito a toque-de-caixa!
                // ao portar este codigo para o SMC, encontrar uma forma melhor
                if (line.Length > 4)
                {
                    if (line.Substring(0, 1) == "1") // apenas verifico se eh um TC
                    {
                        gridTCs.Rows.Add(); // so adicionar se for TC

                        // Description
                        if (line.IndexOf("//") > 0)
                        {
                            gridTCs[0, gridTCs.RowCount - 1].Value = line.Substring(line.IndexOf("//") + 3);
                        }

                        gridTCs[1, gridTCs.RowCount - 1].Value = line.Substring(0, 12);

                        // Encontra o fim do pacote (tambem de forma preguicosa; melhorar)
                        int endPacket = line.IndexOf('\t');

                        gridTCs[2, gridTCs.RowCount - 1].Value = line.Substring(12, (endPacket - 12 - 4));
                        gridTCs[3, gridTCs.RowCount - 1].Value = line.Substring(endPacket - 4, 4);
                        gridTCs[5, gridTCs.RowCount - 1].Value = line.Substring(0, endPacket);
                    }
                }

                line = file.ReadLine();
            }

            // Agora, com o grid carregado, coloca um separador a cada byte
            foreach (DataGridViewRow row in gridTCs.Rows)
            {
                foreach (DataGridViewColumn col in gridTCs.Columns)
                {
                    if ((col.Index > 0) && (col.Index < 4))
                    {
                        String hexValue = gridTCs[col.Index, row.Index].Value.ToString();
                        String newValue = newValue = hexValue.Substring(0, 2);

                        for (int i = 2; i < hexValue.Length; i += 2)
                        {
                            newValue += "-" + hexValue.Substring(i, 2);
                        }

                        gridTCs[col.Index, row.Index].Value = newValue;
                    }
                }
            }

            file.Close();
        }

        private void AddTMFrameToFile(byte[] tmFrame, bool validCRC)
        {
            fileHandling.Write(Utils.Formatting.ConvertByteArrayToHexString(tmFrame, tmFrame.Length));
        }

        /** Apos ter conseguido extrair um frame de um arquivo de entrada, adiciona o frame ao grid e/ou ao file. **/
        private void AddTMFrameToGridAndFile(byte[] tmFrame, bool validCRC)
        {
            bool fillGrid = false;

            if (receptionInRealTime) // botao "Start Real-Time Reception (No Coping)" foi clicado.
            {
                if (chkLogToFile.Checked)
                {
                    fileHandling.Write("["+numOfLines+"] "+Utils.Formatting.ConvertByteArrayToHexString(tmFrame, tmFrame.Length)+"\r\n");
                    numOfLines++;
                }

                if (chkShowInRealTime.Checked)
                {
                    fillGrid = true;
                }
            }
            else
            {
                fillGrid = true; // Decodificacao off-line, via arquivo.
            }

            if (!fillGrid)
            {
                return;
            }

            RawFrame frame = new RawFrame(false);
            frame.SetRawFrame(tmFrame);

            gridTMFrames.Rows.Add();
            int lastLine = gridTMFrames.RowCount - 1;

            // frame header
            gridTMFrames[0, lastLine].Value = frame.GetPart(0, 2).ToString();
            gridTMFrames[1, lastLine].Value = frame.GetPart(2, 10);
            gridTMFrames[2, lastLine].Value = frame.GetPart(12, 3);
            gridTMFrames[3, lastLine].Value = frame.GetPart(15, 1);
            gridTMFrames[4, lastLine].Value = frame.GetPart(16, 8);
            gridTMFrames[5, lastLine].Value = frame.GetPart(24, 8);
            gridTMFrames[6, lastLine].Value = frame.GetPart(32, 1);
            gridTMFrames[7, lastLine].Value = frame.GetPart(33, 1);
            gridTMFrames[8, lastLine].Value = frame.GetPart(34, 1);
            gridTMFrames[9, lastLine].Value = frame.GetPart(35, 2);
            gridTMFrames[10, lastLine].Value = frame.GetPart(37, 11);

            gridTMFrames[11, lastLine].Value = frame.GetString().Substring(18, (1103 * 3) - 1); // Data Field

            // clcw
            gridTMFrames[12, lastLine].Value = frame.GetPart(8872, 1);
            gridTMFrames[13, lastLine].Value = frame.GetPart(8873, 2);
            gridTMFrames[14, lastLine].Value = frame.GetPart(8875, 3);
            gridTMFrames[15, lastLine].Value = frame.GetPart(8878, 2);
            gridTMFrames[16, lastLine].Value = frame.GetPart(8880, 6);
            gridTMFrames[17, lastLine].Value = frame.GetPart(8886, 2);
            gridTMFrames[18, lastLine].Value = frame.GetPart(8888, 1);
            gridTMFrames[19, lastLine].Value = frame.GetPart(8889, 1);
            gridTMFrames[20, lastLine].Value = frame.GetPart(8890, 1);
            gridTMFrames[21, lastLine].Value = frame.GetPart(8891, 1);
            gridTMFrames[22, lastLine].Value = frame.GetPart(8892, 1);
            gridTMFrames[23, lastLine].Value = frame.GetPart(8893, 2);
            gridTMFrames[24, lastLine].Value = frame.GetPart(8895, 1);
            gridTMFrames[25, lastLine].Value = frame.GetPart(8896, 8);

            gridTMFrames[26, lastLine].Value = frame.GetString().Substring(3339, 5); // CRC

            if (!validCRC)
            {
                foreach (DataGridViewCell cell in gridTMFrames.Rows[lastLine].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                }

                gridTMFrames[27, lastLine].Value = "WRONG!";
            }
            else
            {
                gridTMFrames[27, lastLine].Value = "OK";
            }

            gridTMFrames.Rows[gridTMFrames.RowCount - 1].Cells[11].Selected = true;
            btClearTMFrames.Enabled = true;
        }

        /** Atualiza o frame com base nos dados em tela. **/
        private void UpdateFrame()
        {
            byte[] part;

            // Zera o frame
            frameTC.ClearFrame();

            part = new byte[1];

            // Version number
            part[0] = (byte)(int.Parse(mskVersion.Text));
            part[0] = (byte)(part[0] << 6);
            frameTC.SetPart(0, 2, part);

            // Frame type
            switch (cmbFrameType.SelectedIndex)
            {
                case 0: // 00 
                    {
                        part[0] = 0;
                        break;
                    }
                case 1: // 01
                    {
                        part[0] = 1;
                        break;
                    }
                case 2: // 10
                    {
                        part[0] = 2;
                        break;
                    }
                case 3: // 11
                    {
                        part[0] = 3;
                        break;
                    }
                default: break;
            }
            part[0] = (byte)(part[0] << 4);
            frameTC.SetPart(2, 2, part);

            // Reserved field A
            part[0] = (byte)((int.Parse(mskResA.Text)) << 2);
            frameTC.SetPart(4, 2, part);

            // Spacecraft id
            part = new byte[2];
            part[0] = (byte)((int.Parse(mskSpacecraftID.Text)) >> 8);
            part[1] = (byte)((int.Parse(mskSpacecraftID.Text)) & 0xff);
            frameTC.SetPart(6, 10, part);

            // Virtual channel id
            part = new byte[1];
            part[0] = (byte)((int.Parse(mskVCID.Text)) << 2);
            frameTC.SetPart(16, 6, part);

            // Reserved field B
            part[0] = (byte)(int.Parse(mskResB.Text));
            frameTC.SetPart(22, 2, part);

            // Sequence number (independente de a sequencia ser automatica)
            part[0] = (byte)(int.Parse(mskFrameSeq.Text));
            frameTC.SetPart(32, 8, part);

            // Agora alimenta a area de dados do pacote
            if ((!cmbControlCommand.Visible) || (cmbControlCommand.SelectedIndex == 0))
            {
                // Atualiza os pacotes no frame

                // Primeiro, determina o tamanho do frame com base no tamanho dos pacotes selecionados
                // Atencao: assumo aqui que o estouro do tamanho maximo ja foi verificado ao clicar no checkbox
                int frameLength = 8; // 5 bytes de cabecalho + 1 de frame length + 2 de CRC

                foreach (DataGridViewRow row in gridTCs.Rows)
                {
                    if (bool.Parse(gridTCs[4, row.Index].FormattedValue.ToString()))
                    {
                        frameLength += (gridTCs[5, row.Index].Value.ToString().Length / 2);
                    }
                }

                // Redimensiona o pacote
                frameTC.Resize(frameLength);

                // Segment header: sequence flags
                switch (cmbSeqFlags.SelectedIndex)
                {
                    case 0: // 00 
                        {
                            part[0] = 0;
                            break;
                        }
                    case 1: // 01
                        {
                            part[0] = 1;
                            break;
                        }
                    case 2: // 10
                        {
                            part[0] = 2;
                            break;
                        }
                    case 3: // 11
                        {
                            part[0] = 3;
                            break;
                        }
                    default: break;
                }

                part[0] = (byte)(part[0] << 6);
                frameTC.SetPart(40, 2, part);

                // Segment header: map id
                part[0] = (byte)(int.Parse(mskMapId.Text));
                frameTC.SetPart(42, 6, part);

                // Se o usuario informou o frame length, sobreponho o tamanho
                if (cbAutoLength.Checked == false)
                {
                    part[0] = (byte)(int.Parse(mskFrameLength.Text));
                    frameTC.SetPart(24, 8, part);
                }

                int startBit = 48; // os pacotes comecam no bit 48

                // Agora varre os grids, inserindo os TCs no frame
                foreach (DataGridViewRow row in gridTCs.Rows)
                {
                    if (bool.Parse(gridTCs[4, row.Index].FormattedValue.ToString()))
                    {
                        String hexString = gridTCs[5, row.Index].Value.ToString();
                        int numberOfBits = hexString.Length * 4;

                        frameTC.SetPartString(hexString, startBit, numberOfBits);

                        startBit += numberOfBits;
                    }
                }
            }
            else // para comandos ao FARM
            {
                if (cmbControlCommand.SelectedIndex == 1) // SET V(R)
                {
                    // Redimensiona o pacote
                    frameTC.Resize(10);

                    part[0] = (byte)(int.Parse(mskFirstOctect.Text));
                    frameTC.SetPart(40, 8, part);

                    part[0] = (byte)(int.Parse(mskSecondOctect.Text));
                    frameTC.SetPart(48, 8, part);

                    part[0] = (byte)(int.Parse(mskThirdOctect.Text));
                    frameTC.SetPart(56, 8, part);
                }
                else // UNLOCK
                {
                    // Redimensiona o pacote
                    frameTC.Resize(8);

                    part[0] = (byte)(int.Parse(mskFirstOctect.Text));
                    frameTC.SetPart(40, 8, part);
                }

                // Se o usuario informou o frame length, sobreponho o tamanho
                if (cbAutoLength.Checked == false)
                {
                    part[0] = (byte)(int.Parse(mskFrameLength.Text));
                    frameTC.SetPart(24, 8, part);
                }
            }

            // Finalmente, se o CRC nao for automatico, insere no frame
            if (cbAutoCRC.Checked == false)
            {
                int crcValue = 0;

                // O CRC ja foi validado no leave do msgbox
                int.TryParse(mskFrameCRC.Text.Replace("-", ""),
                             System.Globalization.NumberStyles.HexNumber,
                             null,
                             out crcValue);

                part = new byte[2];
                part[0] = (byte)(crcValue >> 8);
                part[1] = (byte)(crcValue & 0xff);
                frameTC.SetPart(((frameTC.Size * 8) - 16), 16, part);
            }

            // Agora recria a CLTU
            int cltuSize = (int)(Math.Ceiling((double)(frameTC.Size / (double)7)));
            cltuSize = (cltuSize * 8) + 10;
            cltu = new byte[cltuSize];

            // Start sequence
            cltu[0] = 0xeb;
            cltu[1] = 0x90;

            int nextCltuIndex = 2;
            int nextFrameIndex = 0;

            byte errorControl = 0;

            // Monta os codeblocks
            for (nextFrameIndex = 0; nextFrameIndex < frameTC.Size; nextFrameIndex += 7)
            {
                byte[] information = new byte[7];

                // Copia a informacao do codeblock para a cltu
                for (int j = 0; j < 7; j++)
                {
                    if ((nextFrameIndex + j) < frameTC.Size)
                    {
                        // Informacao
                        cltu[nextCltuIndex + j] = frameTC.RawContents[nextFrameIndex + j];
                    }
                    else
                    {
                        // Fill octect
                        cltu[nextCltuIndex + j] = 0x55;
                    }

                    // Prepara o vetor de 7 bytes para passar ao BCH
                    information[j] = cltu[nextCltuIndex + j];
                }

                if (CheckingCodes.Bch6356(information, ref errorControl))
                {
                    cltu[nextCltuIndex + 7] = errorControl;
                }
                else
                {
                    // Este erro nao deve ocorrer, mas...
                    MessageBox.Show("Error when creating the CLTU!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                nextCltuIndex += 8;
            }

            // Agora adiciona o tail sequence
            for (int i = 0; i < 8; i++)
            {
                cltu[nextCltuIndex + i] = 0x55; // fill octect
            }

            // Atualiza a exibicao do frame
            gridTCFrame[1, 0].Value = frameTC.GetString();

            // Atualiza a exibicao da cltu
            gridTCFrame[1, 1].Value = BitConverter.ToString(cltu);

            if (cbAutoLength.Checked)
            {
                mskFrameLength.Text = (frameTC.Size - 1).ToString();
            }

            if (cbAutoCRC.Checked)
            {
                mskFrameCRC.Text = frameTC.GetString().Substring(frameTC.GetString().Length - 5, 5);
            }
        }

        private void FrameReceived(object sender, AvailableFrameEventArgs eventArgs)
        {
            try
            {
                frameArgs[0] = eventArgs.Frame;
                frameArgs[1] = eventArgs.FrameValid;

                Invoke(fillFrameGridCallBack, frameArgs);
                Array.Clear(frameArgs, 0, 2);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}

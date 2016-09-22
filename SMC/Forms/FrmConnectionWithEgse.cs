/**
 * @file 	    FrmConnectionWithEgse.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    07/04/2010
 * @note	    Modificado em 02/03/2014 por Conrado.
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
using System.IO.Ports;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.TestSession;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Ccsds;
using System.Threading;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using System.Net.Sockets;
using System.Net;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmConnectionWithEgse
     * Este Formulario eh usado para realizacao das conexoes entre o SMC e o OBC.
     **/
    public partial class FrmConnectionWithEgse : DockContent
    {
        #region Atributos

        private SequenceControl ssc = new SequenceControl();
        private SessionLog session = new SessionLog();
        private RawPacket reportPacket;
        private String connectionType;
        private PipeHandling pipe = new PipeHandling();
        private SubordRS422 rs422 = new SubordRS422();
        private String pipeName;
        private String pipeType;
        private FileHandling tcFile = null;
        private String filePath = "";
        private bool connected = false;
        private bool saveSession = false;
        private MdiMain mdiMain = null;
        private FrmRequestsComposition frmTcsComposition = null;
        private FrmEventsDetectionList frmEventsDetectionList = null;
        private FrmPacketsStorageStatusMonitoring frmPacketStorage = null;
        private FrmSavedRequests frmSavedRequests = null;
        private FrmFramesCoding frmFramesCoding = null;
        private FrmCommRS422 frmRs422 = null;
        private FrmCortexCOP1Configuration frmCortexCOP1Configuration = null;

        private bool waitingForTm = true;
        private bool waitingForTmFrame = true;
        private FrmTestProceduresComposition frmTestProceduresComposition = null;
        private FrmTestProcedureExecution frmTestProcedureExecution = null;
        private TmDecoder tmDecoder = new TmDecoder();

        // Comunicacao com o Cortex e o simulador do mesmo.
        private FrmSimCortex frmSimCortex = null;
        private bool isServerTcpIp;
        private CortexHandling cortex = new CortexHandling();

        delegate void PrintMessageCallBack(String msg);
        private PrintMessageCallBack printMessageTcpIp = null;

        // verica o modo offline
        public bool offlineMode;       

        // contadores de frames de TM
        private int sentTcCounter = 0;
        private int receivedTmCounter = 0;
        private int activeTmCounter = 0;
        private int idleTmCounter = 0;
        private int nextTcFrameSequenceForVc0 = 0;
        private int nextTcFrameSequenceForVc1 = 0;

        // backup do ultimo frame recebido, para exibir no grid
        private bool usingTmFrameToShow = false;
        private bool tmFrameToShowIsValid = false;
        private byte[] tmFrameToShow;
        
        // Eventos
        public event TelemetryEventHandler TelemetryReceived;
        private AvailableFrameEventArgs availableFrameEventArgs = new AvailableFrameEventArgs();
        public event AvailableFrameEventHandler availableFrameEventHandler = null;
        private AvailablePacketEventArgs availablePacketEventArgs = new AvailablePacketEventArgs();
        public event AvailablePacketEventHandler availablePacketEventHandler = null;

        /* Este enum eh usado para informar ao metodo SendRequest quem esta enviado o requestPacket.
         * Esta informacao eh passada para nao confundir a rotina em pontos em que a tela Request Composition eh atualizada a partir deste Form.
         */
        public enum SourceRequestPacket { RequestComposition, OtherSource }

        #endregion

        #region Construtor

        public FrmConnectionWithEgse(MdiMain mdi, bool offlineModeMDI)
        {
            InitializeComponent();

            offlineMode = offlineModeMDI;

            if (offlineMode == false)
            {
                reportPacket = new RawPacket(false, false);
            }

            // Instala uma rotina de tratamento do evento de byte recebido por um pipe
            pipe.ByteReceived += new PipeHandling.ByteReceivedHandler(pipe_ByteReceived);

            mdiMain = mdi;

            // verificar se a tela TCsComposition esta aberta para manter contato.
            if (mdiMain.FormRequestsComposition != null)
            {
                frmTcsComposition = mdiMain.FormRequestsComposition; // buscar a instancia da TCsComposition para ser usada aqui.
                frmTcsComposition.FrmConnectionWithEgse = this; // enviar a instancia desta tela para a TCsComposition.
            }

            // verificar se a tela EventsDetectionList esta aberta para manter contato.
            if (mdiMain.FormEventsDetectionList != null)
            {
                frmEventsDetectionList = mdiMain.FormEventsDetectionList;
                frmEventsDetectionList.FormConnectionWithEgse = this;
            }

            // verificar se a tela PacketsStorage esta aberta para manter contato.
            if (mdiMain.FormPacketStorage != null)
            {
                frmPacketStorage = mdiMain.FormPacketStorage;
                frmPacketStorage.FormConnectionWithEgse = this;
            }

            // verificar se a tela SavedRequests esta aberta para manter contato.
            if (mdiMain.FormSavedRequests != null)
            {
                frmSavedRequests = mdiMain.FormSavedRequests;
                frmSavedRequests.FormConnectionWithEgse = this;
            }

            // o mesmo
            if (mdiMain.FormFramesCoding != null)
            {
                frmFramesCoding = mdiMain.FormFramesCoding;
                frmFramesCoding.FormConnectionWithEgse = this;
            }

            if (mdiMain.FormSimCortex != null)
            {
                frmSimCortex = mdiMain.FormSimCortex;
                frmSimCortex.FormConnectionWithEgse = this;
            }

            if (mdiMain.FormTestProceduresComposition != null)
            {
                frmTestProceduresComposition = mdiMain.FormTestProceduresComposition;
                frmTestProceduresComposition.FormConnectionWithEgse = this;
            }

            if (mdiMain.FormCortexCOP1Configuration != null)
            {
                frmCortexCOP1Configuration = mdiMain.FormCortexCOP1Configuration;
            }
        }

        #endregion

        #region Propriedades

        /**Usada para receber o objeto frmTCsComposition**/
        public FrmRequestsComposition FormTcsComposition
        {
            set
            {
                frmTcsComposition = value;
            }
        }

        /**Usada para receber o objeto frmEventsDetectionList**/
        public FrmEventsDetectionList FormEventsDetectionList
        {
            set
            {
                frmEventsDetectionList = value;
            }
        }

        /**Usada para receber o objeto frmPacketsStorage**/
        public FrmPacketsStorageStatusMonitoring FormPacketsStorage
        {
            set
            {
                frmPacketStorage = value;
            }
        }

        /**Usada para receber o objeto frmSavedRequest**/
        public FrmSavedRequests FormSavedRequest
        {
            set
            {
                frmSavedRequests = value;
            }
        }

        /**Retorna True se estiver conectado ao OBC**/
        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        /**Usada para receber o objeto frmFramesCoding**/
        public FrmFramesCoding FormFramesCoding
        {
            set
            {
                frmFramesCoding = value;
            }
        }

        public FrmSimCortex FormSimCortex
        {
            set
            {
                frmSimCortex = value;
            }
        }

        public FrmTestProcedureExecution FormTestProcedureExecution
        {
            set
            {
                frmTestProcedureExecution = value;
            }
        }

        public FrmCommRS422 FormCommRs422
        {
            set
            {
                frmRs422 = value;
            }
        }

        public FrmCortexCOP1Configuration FormCortexCOP1Config
        {
            set
            {
                frmCortexCOP1Configuration = value;
            }
        }

        public int SessionId
        {
            get
            {
                return session.SessionId;
            }
        }

        public CortexHandling CortexInstance
        {
            get
            {
                return cortex;
            }
            set
            {
                cortex = value;
            }
        }

        #endregion

        #region  Eventos da Interface Grafica

        private void FrmConnectionWithEgse_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(1093, (309) - 25);
            }
        }

        private void rbTcpIp_CheckedChanged(object sender, EventArgs e)
        {
            cmbTxChannel.Enabled = rbRs422.Checked;
            cmbRxChannel.Enabled = rbRs422.Checked;
            cmbClockRate.Enabled = rbRs422.Checked;
            rbRxTc.Enabled = rbRs422.Checked;
            rbRxTm.Enabled = rbRs422.Checked;
            rbTxClockIdleContinuous.Enabled = rbRs422.Checked;
            rbTxClockIdleNOTContinuous.Enabled = rbRs422.Checked;
            lblOperationMode.Enabled = rbRs422.Checked;
            cmbOperationMode.Enabled = rbRs422.Checked;
            label10.Enabled = rbRs422.Checked;
            label11.Enabled = rbRs422.Checked;
            label12.Enabled = rbRs422.Checked;

            rbPipeClient.Enabled = rbNamedPipe.Checked;
            rbPipeServer.Enabled = rbNamedPipe.Checked;
            txtPipeName.Enabled = rbNamedPipe.Checked;
            label6.Enabled = rbNamedPipe.Checked;
            label7.Enabled = rbNamedPipe.Checked;

            btImportTm.Enabled = rbFile.Checked;

            if (offlineMode)
            {
                btImportAmazoniaTm.Enabled = false;
            }
            else
            {
                btImportAmazoniaTm.Enabled = rbFile.Checked && (DbConfiguration.TmTimetagFormat.Equals("4"));
            }

            cmbComPort.Enabled = rbSerial.Checked;
            cmbBaudRate.Enabled = rbSerial.Checked;
            cmbDataBits.Enabled = rbSerial.Checked;
            cmbStopBits.Enabled = rbSerial.Checked;
            cmbParity.Enabled = rbSerial.Checked;
            chkConfigureFarm.Enabled = rbTcpIp.Enabled;
            label1.Enabled = rbSerial.Checked;
            label2.Enabled = rbSerial.Checked;
            label3.Enabled = rbSerial.Checked;
            label4.Enabled = rbSerial.Checked;
            label5.Enabled = rbSerial.Checked;

            groupBox6.Text = "TC Communication Layer";
            rbPackets.Text = "TC Segments (Unsegmented Packets)";
            rbFrames.Text = "TC Transfer Frame";
            chkDiscardIdleFrame.Checked = false;
            rbCltu.Visible = true;
            rbPackets.Checked = true;
            rbCltu_CheckedChanged(null, new EventArgs());

            chkAskTm.Enabled = true;
            numSeconds.Enabled = true;

            groupBox1.Enabled = rbSerial.Checked;
            groupBox2.Enabled = rbNamedPipe.Checked;
            groupBox3.Enabled = rbFile.Checked;
            groupBox5.Enabled = rbRs422.Checked;
            groupBox7.Enabled = rbTcpIp.Checked;

            if (rbTcpIp.Checked)
            {
                chkDiscardIdleFrame.Location = new Point(chkDiscardIdleFrame.Location.X, 92);
                lblTmChannelCod.Location = new Point(lblTmChannelCod.Location.X, 118);
                cmbTmChannelCod.Location = new Point(cmbTmChannelCod.Location.X, 115);

                groupBox6.Size = new Size(groupBox6.Size.Width, 149);

                groupBox4.Location = new Point(groupBox4.Location.X, 263);
                grpSessionOptions.Location = new Point(grpSessionOptions.Location.X, 354);
                btSave.Location = new Point(btSave.Location.X, 500);
            }

            connectionType = "ethernet";
            btConnection.Text = "Connect to TCP/IP Network";
            txtIp.Focus();
            txtIp.SelectAll();
        }

        private void rbRs422_CheckedChanged(object sender, EventArgs e)
        {
            cmbTxChannel.Enabled = rbRs422.Checked;
            cmbRxChannel.Enabled = rbRs422.Checked;
            cmbClockRate.Enabled = rbRs422.Checked;
            rbRxTc.Enabled = rbRs422.Checked;
            rbRxTm.Enabled = rbRs422.Checked;
            rbTxClockIdleContinuous.Enabled = rbRs422.Checked;
            rbTxClockIdleNOTContinuous.Enabled = rbRs422.Checked;
            lblOperationMode.Enabled = rbRs422.Checked;
            cmbOperationMode.Enabled = rbRs422.Checked;
            label10.Enabled = rbRs422.Checked;
            label11.Enabled = rbRs422.Checked;
            label12.Enabled = rbRs422.Checked;

            rbPipeClient.Enabled = rbNamedPipe.Checked;
            rbPipeServer.Enabled = rbNamedPipe.Checked;
            txtPipeName.Enabled = rbNamedPipe.Checked;
            label6.Enabled = rbNamedPipe.Checked;
            label7.Enabled = rbNamedPipe.Checked;

            btImportTm.Enabled = rbFile.Checked;

            if (offlineMode)
            {
                btImportAmazoniaTm.Enabled = false;
            }
            else
            {
                btImportAmazoniaTm.Enabled = rbFile.Checked && (DbConfiguration.TmTimetagFormat.Equals("4"));
            }

            cmbComPort.Enabled = rbSerial.Checked;
            cmbBaudRate.Enabled = rbSerial.Checked;
            cmbDataBits.Enabled = rbSerial.Checked;
            cmbStopBits.Enabled = rbSerial.Checked;
            cmbParity.Enabled = rbSerial.Checked;
            label1.Enabled = rbSerial.Checked;
            label2.Enabled = rbSerial.Checked;
            label3.Enabled = rbSerial.Checked;
            label4.Enabled = rbSerial.Checked;
            label5.Enabled = rbSerial.Checked;
            
            if (rbRs422.Checked)
            {
                chkDiscardIdleFrame.Location = new Point(chkDiscardIdleFrame.Location.X, 65);
                lblTmChannelCod.Location = new Point(lblTmChannelCod.Location.X, 91);
                cmbTmChannelCod.Location = new Point(cmbTmChannelCod.Location.X, 88);

                groupBox6.Size = new Size(groupBox6.Size.Width, 118);

                groupBox4.Location = new Point(groupBox4.Location.X, 231);
                grpSessionOptions.Location = new Point(grpSessionOptions.Location.X, 322);
                btSave.Location = new Point(btSave.Location.X, 468);
            }

            groupBox6.Text = "TC/TM Communication Layer";
            rbPackets.Text = "Packets (communication with UPC)";
            rbFrames.Text = "Frames (communication with UTMC)";
            rbCltu.Visible = false;
            rbFrames.Checked = true;

            chkAskTm.Enabled = true;
            numSeconds.Enabled = true;

            groupBox1.Enabled = rbSerial.Checked;
            groupBox2.Enabled = rbNamedPipe.Checked;
            groupBox3.Enabled = rbFile.Checked;
            groupBox5.Enabled = rbRs422.Checked;
            groupBox7.Enabled = rbTcpIp.Checked;

            connectionType = "rs422";
            btConnection.Text = "Connect to RS-422 Serial";
        }

        private void rbSerial_CheckedChanged(object sender, EventArgs e)
        {
            cmbTxChannel.Enabled = rbRs422.Checked;
            cmbRxChannel.Enabled = rbRs422.Checked;
            cmbClockRate.Enabled = rbRs422.Checked;
            rbRxTc.Enabled = rbRs422.Checked;
            rbRxTm.Enabled = rbRs422.Checked;
            rbTxClockIdleContinuous.Enabled = rbRs422.Checked;
            rbTxClockIdleNOTContinuous.Enabled = rbRs422.Checked;
            lblOperationMode.Enabled = rbRs422.Checked;
            cmbOperationMode.Enabled = rbRs422.Checked;
            label10.Enabled = rbRs422.Checked;
            label11.Enabled = rbRs422.Checked;
            label12.Enabled = rbRs422.Checked;

            rbPipeClient.Enabled = rbNamedPipe.Checked;
            rbPipeServer.Enabled = rbNamedPipe.Checked;
            txtPipeName.Enabled = rbNamedPipe.Checked;
            label6.Enabled = rbNamedPipe.Checked;
            label7.Enabled = rbNamedPipe.Checked;

            btImportTm.Enabled = rbFile.Checked;

            if (offlineMode)
            {
                btImportAmazoniaTm.Enabled = false;
            }
            else
            {
                btImportAmazoniaTm.Enabled = rbFile.Checked && (DbConfiguration.TmTimetagFormat.Equals("4"));
            }

            cmbComPort.Enabled = rbSerial.Checked;
            cmbBaudRate.Enabled = rbSerial.Checked;
            cmbDataBits.Enabled = rbSerial.Checked;
            cmbStopBits.Enabled = rbSerial.Checked;
            cmbParity.Enabled = rbSerial.Checked;
            label1.Enabled = rbSerial.Checked;
            label2.Enabled = rbSerial.Checked;
            label3.Enabled = rbSerial.Checked;
            label4.Enabled = rbSerial.Checked;
            label5.Enabled = rbSerial.Checked;

            if (rbSerial.Checked)
            {
                chkDiscardIdleFrame.Location = new Point(chkDiscardIdleFrame.Location.X, 65);
                lblTmChannelCod.Location = new Point(lblTmChannelCod.Location.X, 91);
                cmbTmChannelCod.Location = new Point(cmbTmChannelCod.Location.X, 88);

                groupBox6.Size = new Size(groupBox6.Size.Width, 118);

                groupBox4.Location = new Point(groupBox4.Location.X, 231);
                grpSessionOptions.Location = new Point(grpSessionOptions.Location.X, 322);
                btSave.Location = new Point(btSave.Location.X, 468);
            }

            groupBox6.Text = "TC/TM Communication Layer";
            rbPackets.Text = "Packets (communication with UPC)";
            rbFrames.Text = "Frames (communication with UTMC)";
            rbCltu.Visible = false;
            rbFrames.Checked = true;

            chkAskTm.Enabled = true;
            numSeconds.Enabled = true;

            groupBox1.Enabled = rbSerial.Checked;
            groupBox2.Enabled = rbNamedPipe.Checked;
            groupBox3.Enabled = rbFile.Checked;
            groupBox5.Enabled = rbRs422.Checked;
            groupBox7.Enabled = rbTcpIp.Checked;

            connectionType = "serial";
            btConnection.Text = "Connect to RS-232 Serial Port";
        }

        private void rbNamedPipe_CheckedChanged(object sender, EventArgs e)
        {
            cmbTxChannel.Enabled = rbRs422.Checked;
            cmbRxChannel.Enabled = rbRs422.Checked;
            cmbClockRate.Enabled = rbRs422.Checked;
            rbRxTc.Enabled = rbRs422.Checked;
            rbRxTm.Enabled = rbRs422.Checked;
            rbTxClockIdleContinuous.Enabled = rbRs422.Checked;
            rbTxClockIdleNOTContinuous.Enabled = rbRs422.Checked;
            lblOperationMode.Enabled = rbRs422.Checked;
            cmbOperationMode.Enabled = rbRs422.Checked;
            label10.Enabled = rbRs422.Checked;
            label11.Enabled = rbRs422.Checked;
            label12.Enabled = rbRs422.Checked;

            rbPipeClient.Enabled = rbNamedPipe.Checked;
            rbPipeServer.Enabled = rbNamedPipe.Checked;
            txtPipeName.Enabled = rbNamedPipe.Checked;
            label6.Enabled = rbNamedPipe.Checked;
            label7.Enabled = rbNamedPipe.Checked;

            btImportTm.Enabled = rbFile.Checked;

            if (offlineMode)
            {
                btImportAmazoniaTm.Enabled = false;
            }
            else
            {
                btImportAmazoniaTm.Enabled = rbFile.Checked && (DbConfiguration.TmTimetagFormat.Equals("4"));
            }

            cmbComPort.Enabled = rbSerial.Checked;
            cmbBaudRate.Enabled = rbSerial.Checked;
            cmbDataBits.Enabled = rbSerial.Checked;
            cmbStopBits.Enabled = rbSerial.Checked;
            cmbParity.Enabled = rbSerial.Checked;
            label1.Enabled = rbSerial.Checked;
            label2.Enabled = rbSerial.Checked;
            label3.Enabled = rbSerial.Checked;
            label4.Enabled = rbSerial.Checked;
            label5.Enabled = rbSerial.Checked;

            if (rbNamedPipe.Checked)
            {
                chkDiscardIdleFrame.Location = new Point(chkDiscardIdleFrame.Location.X, 65);
                lblTmChannelCod.Location = new Point(lblTmChannelCod.Location.X, 91);
                cmbTmChannelCod.Location = new Point(cmbTmChannelCod.Location.X, 88);

                groupBox6.Size = new Size(groupBox6.Size.Width, 118);

                groupBox4.Location = new Point(groupBox4.Location.X, 231);
                grpSessionOptions.Location = new Point(grpSessionOptions.Location.X, 322);
                btSave.Location = new Point(btSave.Location.X, 468);
            }

            groupBox6.Text = "TC/TM Communication Layer";
            rbPackets.Text = "Packets (communication with UPC)";
            rbFrames.Text = "Frames (communication with UTMC)";
            rbCltu.Visible = false;
            rbFrames.Checked = true;
            rbCltu_CheckedChanged(null, new EventArgs());

            chkAskTm.Enabled = true;
            numSeconds.Enabled = true;

            groupBox1.Enabled = rbSerial.Checked;
            groupBox2.Enabled = rbNamedPipe.Checked;
            groupBox3.Enabled = rbFile.Checked;
            groupBox5.Enabled = rbRs422.Checked;
            groupBox7.Enabled = rbTcpIp.Checked;

            connectionType = "pipe";
            btConnection.Text = "Connect to Pipe";
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            cmbTxChannel.Enabled = rbRs422.Checked;
            cmbRxChannel.Enabled = rbRs422.Checked;
            cmbClockRate.Enabled = rbRs422.Checked;
            rbRxTc.Enabled = rbRs422.Checked;
            rbRxTm.Enabled = rbRs422.Checked;
            rbTxClockIdleContinuous.Enabled = rbRs422.Checked;
            rbTxClockIdleNOTContinuous.Enabled = rbRs422.Checked;
            lblOperationMode.Enabled = rbRs422.Checked;
            cmbOperationMode.Enabled = rbRs422.Checked;
            label10.Enabled = rbRs422.Checked;
            label11.Enabled = rbRs422.Checked;
            label12.Enabled = rbRs422.Checked;

            rbPipeClient.Enabled = rbNamedPipe.Checked;
            rbPipeServer.Enabled = rbNamedPipe.Checked;
            txtPipeName.Enabled = rbNamedPipe.Checked;
            label6.Enabled = rbNamedPipe.Checked;
            label7.Enabled = rbNamedPipe.Checked;

            btImportTm.Enabled = rbFile.Checked;

            if (offlineMode)
            {
                btImportAmazoniaTm.Enabled = false;
            }
            else
            {
                btImportAmazoniaTm.Enabled = rbFile.Checked && (DbConfiguration.TmTimetagFormat.Equals("4"));
            }

            cmbComPort.Enabled = rbSerial.Checked;
            cmbBaudRate.Enabled = rbSerial.Checked;
            cmbDataBits.Enabled = rbSerial.Checked;
            cmbStopBits.Enabled = rbSerial.Checked;
            cmbParity.Enabled = rbSerial.Checked;
            label1.Enabled = rbSerial.Checked;
            label2.Enabled = rbSerial.Checked;
            label3.Enabled = rbSerial.Checked;
            label4.Enabled = rbSerial.Checked;
            label5.Enabled = rbSerial.Checked;

            if (rbFile.Checked)
            {
                chkDiscardIdleFrame.Location = new Point(chkDiscardIdleFrame.Location.X, 65);
                lblTmChannelCod.Location = new Point(lblTmChannelCod.Location.X, 91);
                cmbTmChannelCod.Location = new Point(cmbTmChannelCod.Location.X, 88);

                groupBox6.Size = new Size(groupBox6.Size.Width, 118);

                groupBox4.Location = new Point(groupBox4.Location.X, 231);
                grpSessionOptions.Location = new Point(grpSessionOptions.Location.X, 322);
                btSave.Location = new Point(btSave.Location.X, 468);
            }

            groupBox6.Text = "TC/TM Communication Layer";
            rbPackets.Text = "Packets (communication with UPC)";
            rbFrames.Text = "Frames (communication with UTMC)";
            rbCltu.Visible = false;
            rbFrames.Checked = true;

            numSeconds.Enabled = false;

            groupBox1.Enabled = rbSerial.Checked;
            groupBox2.Enabled = rbNamedPipe.Checked;
            groupBox3.Enabled = rbFile.Checked;
            groupBox5.Enabled = rbRs422.Checked;
            groupBox7.Enabled = rbTcpIp.Checked;

            connectionType = "file";
            btConnection.Text = "Create or Open a TC File";
        }

        private void rbPipeServer_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbNamedPipe.Checked)
            {
                return;
            }

            if (rbPipeServer.Checked)
            {
                btConnection.Text = "Create Pipe and Wait for a Connection";
            }
            else
            {
                btConnection.Text = "Connect to Pipe";
            }
        }

        private void rdServer_CheckedChanged(object sender, EventArgs e)
        {
            isServerTcpIp = rdServer.Checked;
        }

        private void FrmConnectionWithEgse_Load(object sender, EventArgs e)
        {
            if (!offlineMode)
            {
                DbConfiguration.Load();

                connectionType = DbConfiguration.EgseConnectionType;
                pipeName = DbConfiguration.EgsePipeName;
                pipeType = DbConfiguration.EgsePipeType;

                // Carregar os valores de configuracao da comunicacao RS-422
                cmbTxChannel.SelectedIndex = cmbTxChannel.FindStringExact(DbConfiguration.EgseRs422TxChannel);
                cmbRxChannel.SelectedIndex = cmbRxChannel.FindStringExact(DbConfiguration.EgseRs422RxChannel);
                cmbClockRate.SelectedIndex = cmbClockRate.FindStringExact(DbConfiguration.EgseRs422ClockRate);

                // Carregar os valores de configuracao da comunicacao ethernet
                txtIp.Text = DbConfiguration.EgseTcpIpAddress;
                txtCopControlFlow.Text = DbConfiguration.CopControlFlowPort.ToString();
                txtCopMonitoringFlow.Text = DbConfiguration.CopMonitoringFlowPort.ToString();
                txtCopTcDataFlow.Text = DbConfiguration.CopTcDataFlowPort.ToString();
                txtCortexControlDataPort.Text = DbConfiguration.CortexControlDataPort.ToString();
                txtTelemetryDataPort.Text = DbConfiguration.CortexTelemetryDataPort.ToString();
                txtTelecommandDataPort.Text = DbConfiguration.CortexTelecommandDataPort.ToString();

                if (DbConfiguration.EgseTcpIpIsServer == false)
                {
                    rdClient.Checked = true;
                    rdServer.Checked = false;
                }
                else
                {
                    rdClient.Checked = false;
                    rdServer.Checked = true;
                }
            }
            else
            {
                //Foi definido um padrao para que as verificacoes sejam validas no modo offline
                connectionType = "RS422";
                pipeType = "SERVER";

            }
            // Carrega as portas COM disponiveis no combo
            foreach (string port in SerialPort.GetPortNames())
            {
                cmbComPort.Items.Add(port);
            }

            if (!offlineMode)
            {
                cmbComPort.SelectedIndex = cmbComPort.FindStringExact(DbConfiguration.EgseRs232Port);
                cmbBaudRate.SelectedIndex = cmbBaudRate.FindStringExact(DbConfiguration.EgseRs232Baud);
                cmbDataBits.SelectedIndex = cmbDataBits.FindStringExact(DbConfiguration.EgseRs232DataBits);
                cmbParity.SelectedIndex = cmbParity.FindStringExact(DbConfiguration.EgseRs232Parity);
                cmbStopBits.SelectedIndex = cmbStopBits.FindStringExact(DbConfiguration.EgseRs232StopBits);
                chkSaveSession.Checked = DbConfiguration.EgseSaveSession;
                chkSyncronize.Checked = DbConfiguration.EgseSyncObt;
                chkAutoSequence.Checked = DbConfiguration.EgseManPack;
                chkConfigureFarm.Checked = DbConfiguration.EgseFarmStart;
                chkAskTm.Checked = DbConfiguration.EgseAskTm;
                chkDiscardIdleFrame.Checked = DbConfiguration.EgseDiscIdleFrame;
                numSeconds.Value = DbConfiguration.EgseAskTmSec;
            }
            
            // desabilitado ate que implementemos outras opcoes de codificacao em tempo real
            //cmbTmChannelCod.SelectedIndex = cmbTmChannelCod.FindStringExact(Properties.Settings.Default.tm_channel_coding);
            cmbTmChannelCod.SelectedIndex = 0;
            cmbTmChannelCod.Enabled = false;
            lblTmChannelCod.Enabled = false;

            // desabilitado ate que implementemos outras opcoes de controle de sequencia
            //cmbTcFramesSeqControl.SelectedIndex = cmbTcFramesSeqControl.FindStringExact(Properties.Settings.Default.tc_frames_seq_control);
            //cmbTcPacketsSeqControl.SelectedIndex = cmbTcPacketsSeqControl.FindStringExact(Properties.Settings.Default.tc_packets_seq_control);
            cmbTcFramesSeqControl.SelectedIndex = 1;
            cmbTcPacketsSeqControl.SelectedIndex = 1;
            cmbTcFramesSeqControl.Enabled = false;
            cmbTcPacketsSeqControl.Enabled = false;

            // TODO: recursos a serem implementados futuramente
            cmbTcFramesSeqControl.SelectedIndex = 1;
            cmbTcPacketsSeqControl.SelectedIndex = 1;
            cmbTcFramesSeqControl.Enabled = false;
            cmbTcPacketsSeqControl.Enabled = false;

            // Carrega as configuracoes de settings
            rbRs422.Checked = false;
            rbSerial.Checked = false;
            rbNamedPipe.Checked = false;
            rbFile.Checked = false;
            cmbOperationMode.SelectedIndex = 0;

            if (connectionType.ToUpper().Equals("ETHERNET"))
            {
                rbTcpIp.Checked = true;
            }
            else if (connectionType.ToUpper().Equals("RS422"))
            {
                rbRs422.Checked = true;
            }
            else if (connectionType.ToUpper().Equals("SERIAL"))
            {
                rbSerial.Checked = true;

                btConnection.Text = "Connect to Serial Port";

                // Configura a serial de acordo com os settings
                Parity par = Parity.None;
                StopBits stop = StopBits.None;
                if (!offlineMode)
                {
                    if (DbConfiguration.EgseRs232Parity.ToUpper().Equals("EVEN"))
                    {
                        par = Parity.Even;
                    }
                    else if (DbConfiguration.EgseRs232Parity.ToUpper().Equals("ODD"))
                    {
                        par = Parity.Odd;
                    }

                    if (DbConfiguration.EgseRs232StopBits.Equals("1"))
                    {
                        stop = StopBits.One;
                    }
                    else if (DbConfiguration.EgseRs232StopBits.Equals("2"))
                    {
                        stop = StopBits.Two;
                    }

                    try
                    {
                        serial.PortName = DbConfiguration.EgseRs232Port;
                        serial.BaudRate = int.Parse(DbConfiguration.EgseRs232Baud);
                        serial.StopBits = stop;
                        serial.Parity = par;
                        serial.DataBits = int.Parse(DbConfiguration.EgseRs232DataBits);
                    }
                    catch
                    {
                    }
                }
            }
            else if (connectionType.ToUpper().Equals("PIPE"))
            {
                rbNamedPipe.Checked = true;
            }
            else
            {
                rbFile.Checked = true;
            }

            if (pipeType.ToUpper().Equals("SERVER"))
            {
                rbPipeServer.Checked = true;
            }
            else
            {
                rbPipeClient.Checked = true;
            }

            // preenche o txt com o pipe.
            txtPipeName.Text = pipeName;

            // configuracao dos grids
            ConfigureStatisticsGrid();
            ConfigureClcwGrid();
            ConfigureTmFrameGrid();

            cmbControlCommand.SelectedIndex = 2;

            if (!offlineMode)
            {
                // carrega do banco de dados as configuracoes de frames
                DbConfiguration.Load();

                mskVersion.Text = DbConfiguration.TcFrameVersion.ToString();
                cmbFrameType.SelectedIndex = DbConfiguration.TcFrameType;
                mskResA.Text = DbConfiguration.TcFrameReservedA.ToString();
                mskSpacecraftID.Text = DbConfiguration.TcFrameScid.ToString();
                mskVCID.Text = DbConfiguration.TcFrameVcid.ToString();
                mskResB.Text = DbConfiguration.TcFrameReservedB.ToString();
                cmbSeqFlags.SelectedIndex = DbConfiguration.TcFrameSeqFlags;
                mskMapId.Text = DbConfiguration.TcFrameMapid.ToString();

                chkConfigureFarm.Checked = DbConfiguration.SessionConfigureFarm;

                rbFrames.Checked = true;

                if (DbConfiguration.EgseCommunicationLayer.ToUpper().Equals("PACKETS"))
                {
                    rbPackets.Checked = true;
                }
                else if (DbConfiguration.EgseCommunicationLayer.ToUpper().Equals("FRAMES"))
                {
                    rbFrames.Checked = true;
                }
                else if (DbConfiguration.EgseCommunicationLayer.ToUpper().Equals("CLTUS"))
                {
                    rbCltu.Checked = true;
                }
            }
            else
            {
                chkSaveSession.Checked = false;
                chkSaveSession.Enabled = false;
                chkSyncronize.Checked = false;
                chkSyncronize.Enabled = false;
                btSave.Enabled = false;
            }
        }

        private void btConnection_Click(object sender, EventArgs e)
        {
            saveSession = chkSaveSession.Checked;

            if (rbTcpIp.Checked)
            {
                btSendCommandToFarm.Enabled = !connected;

                if (!IsValidIPv4(txtIp.Text.Trim()))
                {
                    return;
                }

                if (!connected)
                {
                    try
                    {
                        btConnection.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;
                        
                        cortex.IpAddress = txtIp.Text.Trim();
                        DbConfiguration.Load();
                        cortex.SpacecraftId = DbConfiguration.TcFrameScid.ToString();
                        cortex.Vcid = DbConfiguration.TcFrameVcid.ToString();

                        cortex.availableCOPMessageEventHandler += new AvailableCOPDataEventHandler(ReceivedCOPMessage);
                        cortex.availableTelemetryEventHandler += new AvailableCortexTelemetryEventHandler(ReceivedCortexTelemetry);

                        if (chkConfigureCOP.Checked)
                        {
                            if (!cortex.Connect(txtCopControlFlow.Text.Trim()))
                            {
                                MessageBox.Show("Could not connect to IP Address: " + txtIp.Text.Trim() + " and COP-1 Control Flow Port: " + txtCopControlFlow.Text.Trim(),
                                                Application.ProductName,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);

                                this.Cursor = Cursors.Default;
                                btConnection.Enabled = true;
                                return;
                            }

                            cortex.COPControlRequest(txtCopControlFlow.Text.Trim());
                        }

                        if (!cortex.Connect(txtCopMonitoringFlow.Text.Trim()))
                        {
                            MessageBox.Show("Could not connect to IP Address: " + txtIp.Text.Trim() + " and Telemetry Data (TM) Port: " + txtCopMonitoringFlow.Text.Trim(),
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            this.Cursor = Cursors.Default;
                            btConnection.Enabled = true;
                            return;
                        }

                        if (!cortex.Connect(txtTelemetryDataPort.Text.Trim()))
                        {
                            MessageBox.Show("Could not connect to IP Address: " + txtIp.Text.Trim() + " and Telemetry Data (TM) Port: " + txtTelemetryDataPort.Text.Trim(),
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            this.Cursor = Cursors.Default;
                            btConnection.Enabled = true;
                            return;
                        }

                        cortex.CORTEXTelemetryRequest(txtTelemetryDataPort.Text.Trim());

                        if (!cortex.Connect(txtCopTcDataFlow.Text.Trim()))
                        {
                            MessageBox.Show("Could not connect to IP Address: " + txtIp.Text.Trim() + " and COP-1 TC Data Flow Port: " + txtCopTcDataFlow.Text.Trim(),
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            this.Cursor = Cursors.Default;
                            btConnection.Enabled = true;
                            return;
                        }

                        nextTcFrameSequenceForVc0 = 0;
                        gridSessionStats[1, 3].Value = nextTcFrameSequenceForVc0;

                        nextTcFrameSequenceForVc1 = 0;
                        gridSessionStats[3, 3].Value = nextTcFrameSequenceForVc1;
                        
                        sentTcCounter = 0;
                        gridSessionStats[1, 1].Value = sentTcCounter;

                        receivedTmCounter = 0;
                        gridSessionStats[3, 1].Value = receivedTmCounter;

                        activeTmCounter = 0;
                        gridSessionStats[1, 2].Value = activeTmCounter;

                        idleTmCounter = 0;
                        gridSessionStats[3, 2].Value = idleTmCounter;

                        gridSessionStats[1, 0].Value = DateTime.Now.ToString();
                        gridSessionStats[3, 0].Value = "[still opened]";

                        gridTmFrame.Rows.Clear();
                        gridClcw.Rows.Clear();

                        if (cmbTmChannelCod.SelectedIndex != 0) // != de "No Coding"
                        {
                            MessageBox.Show("TM coding not implemented yet",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            this.Cursor = Cursors.Default;
                            return;
                        }

                        // Sinaliza a opcao para obter Frame Idle ou nao
                        tmDecoder.DiscardIdleFrame = chkDiscardIdleFrame.Checked;

                        // Faz a assinatura do evento que eh disparado sempre que um Frame for encontrado na classe TmDecoder.
                        tmDecoder.frameFoundEventHandler += new FrameFoundEventHandler(FrameFound);
                        
                        btConnection.Enabled = true;
                        btConnection.Text = "Disconnect from TCP/IP Network";
                        mdiMain.statusStrip.Items[0].Text = "Connected to TCP/IP Network.";
                        connected = true;

                        if (frmTcsComposition != null)
                        {
                            frmTcsComposition.btSend.Enabled = true;
                        }

                        if (frmEventsDetectionList != null)
                        {
                            frmEventsDetectionList.btRequestEvents.Enabled = true;
                        }

                        if (frmPacketStorage != null)
                        {
                            frmPacketStorage.btnResquestStorage.Enabled = true;
                        }

                        if (frmSavedRequests != null)
                        {
                            frmSavedRequests.btSendTc.Enabled = true;
                        }

                        if (frmTestProceduresComposition != null)
                        {
                            frmTestProceduresComposition.FormConnectionWithEgse = this;
                        }

                        if (frmFramesCoding != null)
                        {
                            frmFramesCoding.btDecode.Enabled = false;
                            frmFramesCoding.chkShowInRealTime.Enabled = true;
                            frmFramesCoding.btRealTimeReception.Enabled = true;
                        }

                        if (frmCortexCOP1Configuration != null)
                        {
                            frmCortexCOP1Configuration.CortexInstance = cortex;
                            frmCortexCOP1Configuration.btGetRefreshConfig.Enabled = true;
                        }

                        numSeconds.Enabled = false;
                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Not Connected to TCP/IP Ethernet");
                        this.Cursor = Cursors.Default;
                        btConnection.Enabled = true;
                    }
                }
                else
                {
                    tmDecoder.frameFoundEventHandler -= new FrameFoundEventHandler(FrameFound);
                    TelemetryReceived -= new TelemetryEventHandler(AvailableReceivedTelemetry);

                    cortex.availableTelemetryEventHandler -= new AvailableCortexTelemetryEventHandler(ReceivedCortexTelemetry);
                    cortex.availableCOPMessageEventHandler -= new AvailableCOPDataEventHandler(ReceivedCOPMessage);
                    cortex.Disconnect(txtCopControlFlow.Text.Trim());
                    cortex.Disconnect(txtCopTcDataFlow.Text.Trim());
                    cortex.Disconnect(txtTelemetryDataPort.Text.Trim());
                    
                    btSendCommandToFarm.Enabled = false;

                    // registra o momento de fechamento da sessao
                    gridSessionStats[3, 0].Value = DateTime.Now.ToString();

                    session.CloseSession();

                    btConnection.Text = "Connect to TCP/IP Network";
                    mdiMain.statusStrip.Items[0].Text = "Connect to TCP/IP Network";
                    
                    connected = false;
                    saveSession = false;
                    numSeconds.Enabled = true;

                    // verificar se a TCsComposition esta aberta, senao da erro
                    if (frmTcsComposition != null)
                    {
                        // Reinicia o controle de SSCs
                        ssc = new SequenceControl();
                        frmTcsComposition.numSSC.Value = 1;
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    // ao desconectar, o botao btRequestEvents deve ser desabilitado, caso a tela EventsDetectionList estiver aberta
                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = false;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = false;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = null;
                    }

                    if (frmRs422 != null)
                    {
                        if (frmRs422.Receiving)
                        {
                            // Parar a recepcao, se estiver recebendo
                            frmRs422.btEnableReception_Click_1(null, new EventArgs());
                        }

                        frmRs422.btEnableReception.Enabled = false;
                        frmRs422.btSend.Enabled = false;
                        frmRs422.ReadFile = null;
                    }

                    if (frmFramesCoding != null)
                    {
                        // Parar a recepcao, se estiver recebendo
                        if (frmFramesCoding.ReceptionInRealTime)
                        {
                            frmFramesCoding.btRealTimeReception_Click_1(null, new EventArgs());
                        }

                        frmFramesCoding.btDecode.Enabled = true;
                        frmFramesCoding.chkShowInRealTime.Enabled = false;
                        frmFramesCoding.btRealTimeReception.Enabled = false;
                    }

                    if (frmCortexCOP1Configuration != null)
                    {
                        frmCortexCOP1Configuration.btGetRefreshConfig.Enabled = false;
                    }
                }
            }
            else if (rbRs422.Checked)
            {
                btSendCommandToFarm.Enabled = !connected;
                chkConfigureFarm.Enabled = connected;

                if (!connected)
                {
                    int status = 0;
                    int board = 1;
                    int txChannel = int.Parse(cmbTxChannel.Text);
                    int txTimeout = 5000; // miliseconds
                    int rxChannel = int.Parse(cmbRxChannel.Text);
                    int rxTimeout = 2000; // miliseconds
                    int rxSizeBuffer = 1024; // bytes DEFAULT
                    int bitRate = int.Parse(cmbClockRate.Text);
                    bool withEnable = false;
                    bool clockContinuous = true;
                    SubordRS422.Mode mode = SubordRS422.Mode.TTC_TO_UTMC;

                    // registro que estou esperando o primeiro frame de TM
                    waitingForTmFrame = true;

                    nextTcFrameSequenceForVc0 = 0;
                    gridSessionStats[1, 3].Value = nextTcFrameSequenceForVc0;

                    nextTcFrameSequenceForVc1 = 0;
                    gridSessionStats[3, 3].Value = nextTcFrameSequenceForVc1;

                    // Faz a assinatura do evento que eh disparado sempre que o rxBuffer estiver disponivel
                    // O evento nomeado como "dataReceivedEventHandler" na classe SubordRS422 disponibiliza o rxBuffer
                    // para ser acessado por qualquer tela. Basta apenas fazer a assinatura passando como parametro
                    // o metodo para tratar o rxBuffer, como o exemplo do metodo "RS422_DataReceived"
                    rs422.dataReceivedEventHandler += new DataReceiveEventHandler(RS422_ReceivedData);

                    if (rbFrames.Checked)
                    {
                        sentTcCounter = 0;
                        gridSessionStats[1, 1].Value = sentTcCounter;

                        receivedTmCounter = 0;
                        gridSessionStats[3, 1].Value = receivedTmCounter;

                        activeTmCounter = 0;
                        gridSessionStats[1, 2].Value = activeTmCounter;

                        idleTmCounter = 0;
                        gridSessionStats[3, 2].Value = idleTmCounter;

                        gridSessionStats[1, 0].Value = DateTime.Now.ToString();
                        gridSessionStats[3, 0].Value = "[still opened]";

                        gridTmFrame.Rows.Clear();
                        gridClcw.Rows.Clear();

                        if (cmbTmChannelCod.SelectedIndex != 0) // != de "No Coding"
                        {
                            MessageBox.Show("TM coding not implemented yet",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            return;
                        }

                        // Sinaliza a opcao para obter Frame Idle ou nao
                        tmDecoder.DiscardIdleFrame = chkDiscardIdleFrame.Checked;

                        // Faz a assinatura do evento que eh disparado sempre que um Frame for encontrado na classe TmDecoder.
                        tmDecoder.frameFoundEventHandler += new FrameFoundEventHandler(FrameFound);
                    }

                    // configurar e iniciar a placa RS422
                    status = rs422.ConfigTxRxBoard(board, txChannel, txTimeout,
                                                   rxChannel, rxTimeout, rxSizeBuffer,
                                                   bitRate, withEnable, mode, clockContinuous);

                    if (status != 0)
                    {
                        rs422.PrintErrorMessage(status);
                        return;
                    }

                    status = rs422.StartReception();

                    if (status != 0)
                    {
                        rs422.PrintErrorMessage(status);
                        return;
                    }

                    btConnection.Text = "Disconnect from RS-422 Serial";
                    mdiMain.statusStrip.Items[0].Text = "Connected to the RS-422 serial.";
                    connected = true;

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = true;
                    }

                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = true;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = true;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = true;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = this;
                    }

                    if (frmRs422 != null)
                    {
                        frmRs422.btEnableReception.Enabled = true;
                        frmRs422.lblDataSended.Text = "";
                        frmRs422.FrameToBeSent = 1;
                        frmRs422.lblFrameToBeSent.Text = ":: " + frmRs422.FrameToBeSent.ToString() + "º ::";

                        if (frmRs422.chkDataSendFile.Checked)
                        {
                            frmRs422.btSend.Enabled = true;
                            frmRs422.lblFrameToBeSent.Enabled = true;
                        }
                        else
                        {
                            frmRs422.btSend.Enabled = false;
                        }
                    }

                    if (frmFramesCoding != null)
                    {
                        frmFramesCoding.btDecode.Enabled = false;
                        frmFramesCoding.chkShowInRealTime.Enabled = true;
                        frmFramesCoding.btRealTimeReception.Enabled = true;
                    }

                    numSeconds.Enabled = false;
                }
                else
                {
                    btSendCommandToFarm.Enabled = false;

                    // registra o momento de fechamento da sessao
                    gridSessionStats[3, 0].Value = DateTime.Now.ToString();

                    // Desassina o evento de recepcao de dados da RS422
                    rs422.dataReceivedEventHandler -= new DataReceiveEventHandler(RS422_ReceivedData);

                    if (rbFrames.Checked)
                    {
                        // Desassina o evento de Frames encontrados.
                        tmDecoder.frameFoundEventHandler -= new FrameFoundEventHandler(FrameFound);
                    }

                    session.CloseSession();
                    int status = rs422.CloseBoard(1);

                    if (status != 0)
                    {
                        rs422.PrintErrorMessage(status);
                    }

                    btConnection.Text = "Connect to RS-422 Serial";
                    mdiMain.statusStrip.Items[0].Text = "Disconnected from the RS-422 serial.";
                    tmrAskForTM.Enabled = false; // timer;
                    tmrUpdateTmFrame.Enabled = false; // timer;

                    connected = false;
                    saveSession = false;
                    numSeconds.Enabled = true;

                    // verificar se a TCsComposition esta aberta, senao da erro
                    if (frmTcsComposition != null)
                    {
                        // Reinicia o controle de SSCs
                        ssc = new SequenceControl();
                        frmTcsComposition.numSSC.Value = 1;
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    // ao desconectar, o botao btRequestEvents deve ser desabilitado, caso a tela EventsDetectionList estiver aberta
                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = false;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = false;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = null;
                    }

                    if (frmRs422 != null)
                    {
                        if (frmRs422.Receiving)
                        {
                            // Parar a recepcao, se estiver recebendo
                            frmRs422.btEnableReception_Click_1(null, new EventArgs());
                        }

                        frmRs422.btEnableReception.Enabled = false;
                        frmRs422.btSend.Enabled = false;
                        frmRs422.ReadFile = null;
                    }

                    if (frmFramesCoding != null)
                    {
                        // Parar a recepcao, se estiver recebendo
                        if (frmFramesCoding.ReceptionInRealTime)
                        {
                            frmFramesCoding.btRealTimeReception_Click_1(null, new EventArgs());
                        }

                        frmFramesCoding.btDecode.Enabled = true;
                        frmFramesCoding.chkShowInRealTime.Enabled = false;
                        frmFramesCoding.btRealTimeReception.Enabled = false;
                    }
                }
            }
            else if (rbSerial.Checked)
            {
                if (!connected)
                {
                    // Configura a serial de acordo com os componentes da Interface Grafica,
                    // porque o usuario pode alterar as configuracoes de conexao antes de iniciar comunicacao.                    
                    Parity par = Parity.None;
                    StopBits stop = StopBits.None;
                    waitingForTm = true;

                    if (cmbParity.SelectedItem.ToString().ToUpper().Equals("EVEN"))
                    {
                        par = Parity.Even;
                    }
                    else if (cmbParity.SelectedItem.ToString().ToUpper().Equals("ODD"))
                    {
                        par = Parity.Odd;
                    }

                    if (cmbStopBits.SelectedItem.ToString().ToUpper().Equals("1"))
                    {
                        stop = StopBits.One;
                    }
                    else if (cmbStopBits.SelectedItem.ToString().ToUpper().Equals("2"))
                    {
                        stop = StopBits.Two;
                    }

                    serial.PortName = cmbComPort.SelectedItem.ToString();
                    serial.BaudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());
                    serial.StopBits = stop;
                    serial.Parity = par;
                    serial.DataBits = int.Parse(cmbDataBits.SelectedItem.ToString());

                    try
                    {
                        serial.Open(); // abre a porta com os parametros da interface
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error trying to connect to " + cmbComPort.Text + "! \n\nPlease check your configuration.",
                                        "Connection Error!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }

                    btConnection.Text = "Disconnect from RS-232 Serial Port";
                    mdiMain.statusStrip.Items[0].Text = "Connected to the RS-232 serial port.";
                    connected = true;

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = true;
                    }

                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = true;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = true;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = true;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = this;
                    }

                    numSeconds.Enabled = false;
                }
                else
                {
                    serial.Close();
                    session.CloseSession();
                    btConnection.Text = "Connect to RS-232 Serial Port";
                    mdiMain.statusStrip.Items[0].Text = "Disconnected from the RS-232 serial port.";
                    tmrAskForTM.Enabled = false; // timer;

                    connected = false;
                    saveSession = false;
                    numSeconds.Enabled = true;

                    // verificar se a TCsComposition esta aberta, senao da erro
                    if (frmTcsComposition != null)
                    {
                        // Reinicia o controle de SSCs
                        ssc = new SequenceControl();
                        frmTcsComposition.numSSC.Value = 1;
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    // Ao desconectar, o botao btRequestEvents deve ser desabilitado, caso a tela EventsDetectionList estiver aberta
                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = false;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = false;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = null;
                    }
                }
            }
            else if (rbNamedPipe.Checked)
            {
                if (!connected)
                {
                    waitingForTm = true;

                    if (rbPipeServer.Checked)
                    {
                        /** @todo quando puder, devemos melhorar isso. Nao eh legal o programa parar de responder ate o cliente conectar. **/
                        if (MessageBox.Show("By clicking 'Yes' now, a named pipe will be created, and the program " +
                                            "will not respond until the pipe client connects to it.\n\n" +
                                            "Are you sure you want to create the pipe and wait for a connection?",
                                            "Attention, this is the Pipe Server !!",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        mdiMain.statusStrip.Items[0].Text = "Named pipe created. Waiting for client to connect.";
                        this.Cursor = Cursors.WaitCursor;
                        this.Update();

                        pipe.CreatePipeAndConnect(txtPipeName.Text.Trim());
                        mdiMain.statusStrip.Items[0].Text = "Pipe client connected.";

                        btConnection.Text = "Disconnect and Close Pipe";
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;

                        pipe.ConnectToPipe(txtPipeName.Text.Trim());
                        mdiMain.statusStrip.Items[0].Text = "Connected to named pipe.";

                        btConnection.Text = "Disconnect from Pipe";
                    }

                    this.Cursor = Cursors.Default;
                    connected = true;

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = true;
                    }

                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = true;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = true;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = true;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = this;
                    }
                }
                else
                {
                    pipe.Disconnect();
                    session.CloseSession();

                    if (rbPipeServer.Checked)
                    {
                        mdiMain.statusStrip.Items[0].Text = "Named pipe closed.";
                        btConnection.Text = "Create Pipe and Wait for a Connection";
                    }
                    else
                    {
                        mdiMain.statusStrip.Items[0].Text = "Disconnected from named pipe.";
                        btConnection.Text = "Connect to Pipe";
                    }

                    connected = false;
                    saveSession = false;

                    // verificar se a TCsComposition esta aberta, senao da erro
                    if (frmTcsComposition != null)
                    {
                        // Reinicia o controle de SSCs
                        ssc = new SequenceControl();
                        frmTcsComposition.numSSC.Value = 1;
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    if (frmTcsComposition != null)
                    {
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    // ao desconectar, o botao btRequestEvents deve ser desabilitado, caso a tela EventsDetectionList estiver aberta
                    if (frmEventsDetectionList != null)
                    {
                        frmEventsDetectionList.btRequestEvents.Enabled = false;
                    }

                    if (frmPacketStorage != null)
                    {
                        frmPacketStorage.btnResquestStorage.Enabled = false;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }

                    if (frmTestProceduresComposition != null)
                    {
                        frmTestProceduresComposition.FormConnectionWithEgse = null;
                    }
                }
            }
            else // connectionType == file
            {
                if (rbFrames.Checked)
                {
                    MessageBox.Show("The file export of frames is performed through the 'Frames Encoder / Decoder' item of the 'Tools' menu.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                if (!connected)
                {
                    btConnection.Enabled = false;
                    bool fileOpenned = false;
                    OpenFileDialog fileDialog = new OpenFileDialog();

                    while (true)
                    {
                        fileDialog.InitialDirectory = Properties.Settings.Default.tc_file_export_default_path;
                        fileDialog.Title = "Create or Open a TC File";
                        fileDialog.Filter = "TC Packet Files (*.tc)|*.tc|All Files (*.*)|*.*";
                        fileDialog.FileName = "*.tc";
                        fileDialog.FilterIndex = 0;
                        fileDialog.CheckFileExists = false; // retira a chekagem de verificacao de existencia de arquivo

                        if (fileDialog.ShowDialog() != DialogResult.OK)
                        {
                            btConnection.Enabled = true;
                            return;
                        }

                        filePath = fileDialog.FileName;

                        // Instancia o arquivo
                        tcFile = new FileHandling(filePath, true, saveSession);

                        if (!tcFile.FileExists())
                        {
                            // Pergunta o user se deseja criar o file que nao existe
                            if (tcFile.CreateNewFile())
                            {
                                tcFile.Write("// Telecommand Packets File, created with SUBORD's Monitoring");
                                tcFile.Write("// and Remote Control Software on " + DateTime.Now);

                                if (saveSession)
                                {
                                    tcFile.AddTagSessionIdOnFile = true;
                                }

                                fileOpenned = true;

                                break;
                            }
                        }
                        else
                        {
                            if (tcFile.ValidateFile())
                            {
                                fileOpenned = true;

                                if ((!tcFile.SessionIdFound) && (tcFile.IsFirstPacket))
                                {
                                    if (saveSession)
                                    {
                                        tcFile.AddTagSessionIdOnFile = true;
                                    }

                                    break;
                                }
                                else if ((!tcFile.SessionIdFound) && (!tcFile.IsFirstPacket))
                                {
                                    //REGRA:
                                    // todo arquivo que eh aberto e nao possuir uma session id
                                    // e ja possuir pacotes, nao se deve gravar no banco de dados 
                                    // os proximos pacotes.
                                    chkSaveSession.Checked = false;
                                }

                                if (tcFile.SessionIdIsValid)
                                {
                                    session.SessionId = tcFile.SessionId;
                                    session.SessionOpenned = true;

                                    //REGRA:
                                    // todo arquivo que eh aberto e possuir uma session id
                                    // valida, deve-se continuar salvando os proximos pacotes
                                    // na session correspondente aa do arquivo que esta sendo aberto.
                                    chkSaveSession.Checked = true;
                                    saveSession = true;
                                }

                                break;
                            }
                        }
                    }

                    if (fileOpenned)
                    {
                        btConnection.Text = "Close TC File";
                        ToolTip toolTipText = new ToolTip();
                        String toolTip = fileDialog.FileName;

                        if (tcFile.SessionIdFound && tcFile.SessionIdIsValid)
                        {
                            toolTip += "\n" + "Session ID: " + tcFile.SessionId.ToString();
                        }
                        else
                        {
                            toolTip += "\n Not Session ID";
                        }

                        toolTipText.SetToolTip(btConnection, toolTip);
                        connected = true;

                        if (frmTcsComposition != null)
                        {
                            frmTcsComposition.btSend.Enabled = true;
                        }

                        /*
                         * if criado para o botão send ser habilitado, quando uma quando uma janela da requestComposition 
                         * for chamada da SaveRequest e só depois disso o usuario conectar
                         */
                        if (frmSavedRequests != null)
                        {
                            frmSavedRequests.btSendTc.Enabled = true;
                        }
                    }

                    btConnection.Enabled = true;
                }
                else
                {
                    btConnection.Text = "Create or Open a TC File";
                    ToolTip toolTipText = new ToolTip();
                    toolTipText.SetToolTip(btConnection, null);
                    connected = false;

                    saveSession = false;

                    session.CloseSession();

                    if (frmTcsComposition != null)
                    {
                        // Reinicia o controle de SSCs
                        ssc = new SequenceControl();
                        frmTcsComposition.numSSC.Value = 1;
                        frmTcsComposition.btSend.Enabled = false;
                    }

                    if (frmSavedRequests != null)
                    {
                        frmSavedRequests.btSendTc.Enabled = false;
                    }
                }
            }

            RefreshInterface();

            // Verifica se esta conectado
            if (connected)
            {
                if (chkAskTm.Checked)
                {
                    // Habilita o timer
                    tmrAskForTM.Interval = (int)numSeconds.Value * 1000;
                    numSeconds.Enabled = false;

                    if (waitingForTm == false)
                    {
                        tmrAskForTM.Enabled = true;
                    }
                }

                if (rbRs422.Checked || rbTcpIp.Checked)
                {
                    tmrUpdateTmFrame.Interval = (int)numUpdateTm.Value * 1000;
                    tmrUpdateTmFrame.Enabled = true; // timer;
                }
            }
            else // se estiver desconectado 
            {
                numSeconds.Enabled = true;
                tmrAskForTM.Enabled = false;
            }

            if (chkSaveSession.Checked == true)
            {
                if (connected)
                {
                    FrmSwAplVersion frmSwAplVersion = new FrmSwAplVersion();

                    frmSwAplVersion.ShowDialog();

                    // envia se o btcancel foi ativado no formulario
                    session.swVersionCancel = frmSwAplVersion.swAplVersionCancel;
                }
            }
        }

        private void ReceivedCortexTelemetry(object sender, AvailableCortexTelemetryEventArgs eventArgs)
        {
            // Envia para a TmDecoder para decodificar o frame de tm.
            tmDecoder.RxBuffer = eventArgs.Telemetry;
        }

        /**
         * Este eh o metodo receptor das mensagens do COP-1.
         **/
        private void ReceivedCOPMessage(object sender, AvailableCOPDataEventArgs eventArgs)
        {
            // Os Consoles aqui presentes sao para uso em modo debug e por isso estao comentados.

            byte[] word = new byte[4];

            switch (eventArgs.Port)
            {
                // CORTEX Control Data (CTRL) Port: 3001
                // CORTEX Telemetry Data (TM) Port: 3070
                // CORTEX Satellite Telecommand Data (TC) Port: 3020
                // COP-1 Control Flow: 3101
                // COP-1 Monitoring Flow: 3100
                // COP-1 Telecommand Data Flow: 3120

                case 3101: // COP-1 Control Flow
                    {
                        if (eventArgs.Message.Length == 32) // 32 eh um Ack
                        {
                            // Obter o Type of Operation
                            Array.Copy(eventArgs.Message, 12, word, 0, 4);
                            Array.Reverse(word);

                            if (BitConverter.ToInt32(word, 0) == 1) // Confirmar se o Type of Operation da resposta eh do tipo Control
                            {
                                // Agora resta verifiicar a ultima palavra da mensagem (Configuration Status).
                                // Se for igual a 0, entao o Cop foi configurado (0 = OK)
                                Array.Copy(eventArgs.Message, eventArgs.Message.Length - 8, word, 0, 4);
                                Array.Reverse(word);

                                if (BitConverter.ToInt32(word, 0) == 0) // OK!
                                {
                                    //Console.WriteLine("Control Acknowledgement Message!");
                                }

                                cortex.ConsoleWriteline(eventArgs.Message);
                            }
                        }
                        else if (eventArgs.Message.Length == 20) // 20 eh um NAck
                        {
                            //Console.WriteLine("Negative Control Acknowledgement Message!");
                        }

                        break;
                    }
                case 3120: // COP-1 Telecommand Data Flow
                    {
                        if (eventArgs.Message.Length == 36) // 32 eh um Ack
                        {
                            // Obter o Output Flow Identifier
                            Array.Copy(eventArgs.Message, 12, word, 0, 4);
                            Array.Reverse(word);

                            if (BitConverter.ToInt32(word, 0) == 0) // Confirmar se o Output Flow Identifier da resposta eh de Aceitacao ou Rejeicao
                            {
                                // Agora resta verificar a ultima palavra da mensagem (Response status).
                                // Se for igual a 0, entao o Cop aceitou a mensagem sem erros (0 = Accept e 1 = Reject).
                                Array.Copy(eventArgs.Message, eventArgs.Message.Length - 8, word, 0, 4);
                                Array.Reverse(word);

                                if (BitConverter.ToInt32(word, 0) == 0) // OK!
                                {
                                    //Console.WriteLine("Accept Request!");
                                }
                                else
                                {
                                    //Console.WriteLine("Reject Request!");
                                }

                                cortex.ConsoleWriteline(eventArgs.Message);
                            }
                        }
                        else if (eventArgs.Message.Length == 20) // 20 eh um NAck
                        {
                            //Console.WriteLine();
                            //Console.WriteLine("The TC Request is not correct!");
                            //cortex.ConsoleWriteline(eventArgs.Message);
                        }

                        break;
                    }
                case 3100: // COP-1 Monitoring 
                    {
                        // Mensagem com os parametros configurados no Cop.
                        // Enviar para a tela de monitoramento desses parametros.
                        if (frmCortexCOP1Configuration != null)
                        {
                            frmCortexCOP1Configuration.MessageCOPMonitoring = eventArgs.Message;
                        }

                        break;
                    }
                default: break;
            }
        }

        /** Atualiza a interface **/
        private void RefreshInterface()
        {
            if (connected)
            {
                rbPackets.Enabled = false;
                rbFrames.Enabled = false;
                rbCltu.Enabled = false;
                //chkDiscardIdleFrame.Enabled = false;
                //lblTmChannelCod.Enabled = false;
                //cmbTmChannelCod.Enabled = false;

                // desabilitado ate que implementemos outras opcoes de controle de sequencia
                //lblTcPacketsSeqControl.Enabled = false;
                //cmbTcPacketsSeqControl.Enabled = false;
                lblOperationMode.Enabled = false;
                cmbOperationMode.Enabled = false;

                numSeconds.Enabled = !chkAskTm.Checked;
                label8.Enabled = numSeconds.Enabled;

                rbRxTc.Enabled = false;
                rbRxTm.Enabled = false;
                rbTxClockIdleContinuous.Enabled = false;
                rbTxClockIdleNOTContinuous.Enabled = false;

                rbPipeClient.Enabled = false;
                rbPipeServer.Enabled = false;
                rbFile.Enabled = false;
                rbNamedPipe.Enabled = false;
                rbSerial.Enabled = false;
                rbRs422.Enabled = false;
                txtPipeName.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;

                btImportTm.Enabled = false;
                btSave.Enabled = false;

                cmbTxChannel.Enabled = false;
                cmbRxChannel.Enabled = false;
                cmbClockRate.Enabled = false;
                cmbBaudRate.Enabled = false;
                cmbComPort.Enabled = false;
                cmbBaudRate.Enabled = false;
                cmbDataBits.Enabled = false;
                cmbStopBits.Enabled = false;
                cmbParity.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                label10.Enabled = false;
                label11.Enabled = false;
                label12.Enabled = false;

                chkSaveSession.Enabled = false;
                chkSyncronize.Enabled = false;
                chkAutoSequence.Enabled = false;

                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox5.Enabled = false;
                groupBox7.Enabled = false;
                rbTcpIp.Enabled = false;
            }
            else
            {
                rbPackets.Enabled = true;
                rbFrames.Enabled = true;
                rbCltu.Enabled = true;
                //chkDiscardIdleFrame.Enabled = true;
                //lblTmChannelCod.Enabled = true;
                //cmbTmChannelCod.Enabled = true;

                // desabilitado ate que implementemos outras opcoes de controle de sequencia
                //lblTcPacketsSeqControl.Enabled = true;
                //cmbTcPacketsSeqControl.Enabled = true;

                lblOperationMode.Enabled = rbRs422.Checked;
                cmbOperationMode.Enabled = rbRs422.Checked;

                numSeconds.Enabled = true;
                label8.Enabled = true;

                rbRxTc.Enabled = rbRs422.Checked;
                rbRxTm.Enabled = rbRs422.Checked;
                rbTxClockIdleContinuous.Enabled = rbRs422.Checked;
                rbTxClockIdleNOTContinuous.Enabled = rbRs422.Checked;

                rbSerial.Enabled = true;
                rbFile.Enabled = true;
                rbNamedPipe.Enabled = true;
                rbRs422.Enabled = true;
                rbTcpIp.Enabled = true;

                chkSaveSession.Enabled = true;
                
                rbPipeClient.Enabled = rbNamedPipe.Checked;
                rbPipeServer.Enabled = rbNamedPipe.Checked;
                txtPipeName.Enabled = rbNamedPipe.Checked;
                label6.Enabled = rbNamedPipe.Checked;
                label7.Enabled = rbNamedPipe.Checked;

                btImportTm.Enabled = rbFile.Checked;
                btSave.Enabled = true;

                cmbTxChannel.Enabled = rbRs422.Checked;
                cmbRxChannel.Enabled = rbRs422.Checked;
                cmbBaudRate.Enabled = rbRs422.Checked;
                cmbClockRate.Enabled = rbRs422.Checked;
                cmbComPort.Enabled = rbSerial.Checked;
                cmbBaudRate.Enabled = rbSerial.Checked;
                cmbDataBits.Enabled = rbSerial.Checked;
                cmbStopBits.Enabled = rbSerial.Checked;
                cmbParity.Enabled = rbSerial.Checked;
                label1.Enabled = rbSerial.Checked;
                label2.Enabled = rbSerial.Checked;
                label3.Enabled = rbSerial.Checked;
                label4.Enabled = rbSerial.Checked;
                label5.Enabled = rbSerial.Checked;
                label10.Enabled = rbRs422.Checked;
                label11.Enabled = rbRs422.Checked;
                label12.Enabled = rbRs422.Checked;

                chkAutoSequence.Enabled = true;

                groupBox1.Enabled = rbSerial.Checked;
                groupBox2.Enabled = rbNamedPipe.Checked;
                groupBox3.Enabled = rbFile.Checked;
                groupBox5.Enabled = rbRs422.Checked;
                groupBox7.Enabled = rbTcpIp.Checked;
            }
        }

        private void FrmConnectionWithEgse_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmTcsComposition != null)
            {
                frmTcsComposition.btSend.Enabled = false;
            }

            if (frmEventsDetectionList != null)
            {
                frmEventsDetectionList.btRequestEvents.Enabled = false;
            }

            if (frmPacketStorage != null)
            {
                frmPacketStorage.btnResquestStorage.Enabled = false;
            }

            if (frmSavedRequests != null)
            {
                frmSavedRequests.btSendTc.Enabled = false;
            }

            if (frmSimCortex != null)
            {
                frmSimCortex.FormConnectionWithEgse = null;
            }

            if (frmTestProceduresComposition != null)
            {
                frmTestProceduresComposition.FormConnectionWithEgse = null;
            }

            if (frmCortexCOP1Configuration != null)
            {
                frmCortexCOP1Configuration.btGetRefreshConfig.Enabled = false;
            }

            // Executar o botao Connect to... para desconectar a tela caso esteja conectada.
            if (connected)
            {
                btConnection_Click(this, new EventArgs());
            }
        }

        private void btImportTm_Click(object sender, EventArgs e)
        {
            if (rbFrames.Checked)
            {
                MessageBox.Show("The file import of frames is performed through the 'Frames Encoder / Decoder' item of the 'Tools' menu.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            /*
             * Condicoes para que um Packet de TM seja aceito:
             * 
             *   1. Ser maior que 18.
             *   2. Possuir um numero par de caracteres.
             *   3. Ser Hexadecimal.   
             *
             * OBS: Caso as tres condicoes acima sejam verdadeiras, o pacote sera aceito com Crc correto ou incorreto 
             * e armazenado no banco de dados.
             * 
             * Se o arquivo a ser importado tiver uma tag //Session Id com uma session valida, os pacotes devem ser inseridos
             * na session equivalente a da tag //Session Id e o usuario deve ser informado.
             * Caso a session do arquivo estiver correta, mas nao estiver no banco de dados, 
             * uma nova session deve ser criada e o usuario deve ser informado da nova session.
             * Caso a session id nao estiver correta (com lixo, ou nao ser convertida pra integer), uma nova session
             * deve ser criada e o usuario ser informado da nova session.
             */
            while (true)
            {
                fileDialog.InitialDirectory = Properties.Settings.Default.tm_file_import_default_path;
                fileDialog.Title = "Select a TM file to import";
                fileDialog.Filter = "Telemetry Files (*.tm)|*.tm|All Files (*.*)|*.*";
                fileDialog.FileName = "*.tm";
                fileDialog.FilterIndex = 0;

                if (fileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                FileHandling tmFile = new FileHandling(fileDialog.FileName, false, true);

                if (tmFile.ValidateFile())
                {
                    if (tmFile.TelemetryImport())
                    {
                        return;
                    }
                }
                else
                {
                    // Se nao abrir o file, vejo se o motivo esta na session id ser invalida.
                    // Caso seja invalida, crio outra session id e importo as TMs na nova session.
                    if (!tmFile.SessionIdIsValid)
                    {
                        if (tmFile.TelemetryImport()) // se a session do file eh invalid, crio outra session id
                        {
                            return;
                        }
                    }
                }
            }
        }

        // Ajusta o intervalo do timer conforme numero de segundos
        // selecionado no 'numSeconds'
        private void numSeconds_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                // o Load eh feito para garantir que nao se sobreponha a configuracao de outro 
                // parametro setado por outro usuario apos a abertura desta tela
                if (!offlineMode)
                {
                    DbConfiguration.Load();

                    if (rbRs422.Checked)
                    {
                        DbConfiguration.EgseConnectionType = "rs422";
                    }
                    else if (rbSerial.Checked)
                    {
                        DbConfiguration.EgseConnectionType = "serial";
                    }
                    else if (rbNamedPipe.Checked)
                    {
                        DbConfiguration.EgseConnectionType = "pipe";
                    }
                    else if (rbTcpIp.Checked)
                    {
                        DbConfiguration.EgseConnectionType = "ethernet";

                        if (rbPackets.Checked)
                        {
                            DbConfiguration.EgseCommunicationLayer = "packets";
                        }
                        else if (rbFrames.Checked)
                        {
                            DbConfiguration.EgseCommunicationLayer = "frames";
                        }
                        else if (rbCltu.Checked)
                        {
                            DbConfiguration.EgseCommunicationLayer = "cltus";
                        }
                    }
                    else
                    {
                        DbConfiguration.EgseConnectionType = "file";
                    }
                    
                    DbConfiguration.EgseSaveSession = chkSaveSession.Checked;
                    DbConfiguration.EgseSyncObt = chkSyncronize.Checked;
                    DbConfiguration.EgseManPack = chkAutoSequence.Checked;
                    DbConfiguration.EgseFarmStart = chkConfigureFarm.Checked;
                    DbConfiguration.EgseAskTm = chkAskTm.Checked;
                    DbConfiguration.EgseAskTmSec = Convert.ToInt32(numSeconds.Value);
                    DbConfiguration.EgseDiscIdleFrame = chkDiscardIdleFrame.Checked;

                    DbConfiguration.EgseRs422TxChannel = cmbTxChannel.Text;
                    DbConfiguration.EgseRs422RxChannel = cmbRxChannel.Text;
                    DbConfiguration.EgseRs422ClockRate = cmbClockRate.Text;
                    DbConfiguration.EgseRs232Port = cmbComPort.Text;
                    DbConfiguration.EgseRs232Baud = cmbBaudRate.Text;
                    DbConfiguration.EgseRs232DataBits = cmbDataBits.Text;
                    DbConfiguration.EgseRs232Parity = cmbParity.Text;
                    DbConfiguration.EgseRs232StopBits = cmbStopBits.Text;

                    if (rbPipeServer.Checked)
                    {
                        DbConfiguration.EgsePipeType = "server";
                    }
                    else
                    {
                        DbConfiguration.EgsePipeType = "client";
                    }

                    if (rbPackets.Checked == true)
                    {
                        DbConfiguration.EgseCommunicationLayer = "packets";
                    }
                    else if (rbFrames.Checked)
                    {
                        DbConfiguration.EgseCommunicationLayer = "frames";
                    }
                    else if (rbCltu.Checked)
                    {
                        DbConfiguration.EgseCommunicationLayer = "cltus";
                    }

                    if (rdClient.Checked == true)
                    {
                        DbConfiguration.EgseTcpIpIsServer = false;
                    }
                    else
                    {
                        DbConfiguration.EgseTcpIpIsServer = true;
                    }

                    if (!IsValidIPv4(txtIp.Text))
                    {
                        MessageBox.Show("The Ip Adrress is not Ipv4!",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    int copPort = 0;

                    if (!int.TryParse(txtCopControlFlow.Text, out copPort))
                    {
                        MessageBox.Show("The Cop Control Flow Port is invalid!\nFill it correctly and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    if (!int.TryParse(txtCopMonitoringFlow.Text, out copPort))
                    {
                        MessageBox.Show("The Cop Monitoring Flow Port is invalid!\nFill it correctly and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    if (!int.TryParse(txtCopTcDataFlow.Text, out copPort))
                    {
                        MessageBox.Show("The Cop Telecommand Data Flow Port is invalid!\nFill it correctly and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    int cortexPort = 0;

                    if (!int.TryParse(txtCortexControlDataPort.Text, out cortexPort))
                    {
                        MessageBox.Show("The Cortex Control Data Port is invalid!\nFill it correctly and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    if (!int.TryParse(txtTelemetryDataPort.Text, out cortexPort))
                    {
                        MessageBox.Show("The Cortex Telemetry Data Flow Port is invalid!\nFill it correctly and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    if (!int.TryParse(txtTelecommandDataPort.Text, out cortexPort))
                    {
                        MessageBox.Show("The Cortex Telecommand Data Flow Port is invalid!\nFill it correctly and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        return;
                    }

                    DbConfiguration.EgseTcpIpAddress = txtIp.Text;
                    DbConfiguration.CopConfigure = Convert.ToBoolean(chkConfigureCOP.Checked);
                    DbConfiguration.CopControlFlowPort = Convert.ToInt32(txtCopControlFlow.Text);
                    DbConfiguration.CopMonitoringFlowPort = Convert.ToInt32(txtCopMonitoringFlow.Text);
                    DbConfiguration.CopTcDataFlowPort = Convert.ToInt32(txtCopTcDataFlow.Text);
                    DbConfiguration.CortexControlDataPort = Convert.ToInt32(txtCortexControlDataPort.Text);
                    DbConfiguration.CortexTelemetryDataPort = Convert.ToInt32(txtTelemetryDataPort.Text);
                    DbConfiguration.CortexTelecommandDataPort = Convert.ToInt32(txtTelecommandDataPort.Text);
                    DbConfiguration.TmChannelCoding = cmbTmChannelCod.Text;
                    DbConfiguration.TcFramesSeqControl = cmbTcFramesSeqControl.Text;
                    DbConfiguration.TcPacketsSeqControl = cmbTcPacketsSeqControl.Text;
                    DbConfiguration.EgsePipeName = txtPipeName.Text;

                    Properties.Settings.Default.Save();

                    DbConfiguration.SessionConfigureFarm = chkConfigureFarm.Checked;

                    if (DbConfiguration.Save() == true)
                    {
                        MessageBox.Show("Configuration saved successfully!",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to save configuration: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void rbPackets_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbTcpIp.Checked)
            {
                // tabpages nao sao desabilitaveis, preciso fazer um cast para Control
                ((Control)tabPage4).Enabled = rbFrames.Checked;
                
                return;
            }

            // tabpages nao sao desabilitaveis, preciso fazer um cast para Control
            ((Control)tabPage4).Enabled = rbPackets.Checked;

            if (!rbTcpIp.Checked)
            {
                chkDiscardIdleFrame.Enabled = rbFrames.Checked;
                chkConfigureFarm.Enabled = rbFrames.Checked;
            }
            else
            {
                chkConfigureFarm.Enabled = true;
            }
        }

        private void rbCltu_CheckedChanged(object sender, EventArgs e)
        {
            // tabpages nao sao desabilitaveis, preciso fazer um cast para Control
            ((Control)tabPage4).Enabled = rbCltu.Checked;

            // desabilitado ate que tenhamos implementadas outras opcoes de codificacao em tempo real
            //lblTmChannelCod.Enabled = rbFrames.Checked;
            //cmbTmChannelCod.Enabled = rbFrames.Checked;

            if (!rbTcpIp.Checked)
            {
                chkDiscardIdleFrame.Enabled = rbFrames.Checked;
                chkConfigureFarm.Enabled = rbFrames.Checked;
            }
            else
            {
                chkConfigureFarm.Enabled = true;
            }
        }

        private void btImportAmazoniaTm_Click(object sender, EventArgs e)
        {
            byte[] fileAsArray = null;
            byte[] extractedPacket = null;
            int arrayIndex = 0;
            int packetLength = 0;
            FileStream fileStream = null;
            BinaryReader binaryReader = null;
            RawPacket reportPacket = new RawPacket(false, false);
            SessionLog sessionLog = new SessionLog();
            int numberOfExtractedPackets = 0;

            try
            {
                folderDialog.SelectedPath = Application.StartupPath;
                folderDialog.Description = "Select the folder with the Amazonia-1 TM frames to import";
                folderDialog.ShowDialog();

                DirectoryInfo dirInfo = new DirectoryInfo(folderDialog.SelectedPath);
                FileInfo[] importFiles = dirInfo.GetFiles("RTTM*.dat");

                if (importFiles.Length == 0)
                {
                    MessageBox.Show("There is no Amazonia-1 Frame Data Field Files in the selected folder.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    return;
                }

                foreach (FileInfo fileInfo in importFiles)
                {
                    fileStream = new System.IO.FileStream(fileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                    // anexa o filestream ao binary reader
                    binaryReader = new System.IO.BinaryReader(fileStream);

                    // read entire file into buffer
                    fileAsArray = binaryReader.ReadBytes((int)fileInfo.Length);

                    arrayIndex = 0;

                    while (arrayIndex < (fileInfo.Length - 4))
                    {
                        // verifica se eh o comeco de um pacote
                        // obs: o arquivo possui apenas o frame data field, entao a unica forma de 
                        // encontrar o comeco do primeiro pacote eh buscando um padrao fixo
                        if ((fileAsArray[arrayIndex] == 0x08) &&
                            (fileAsArray[arrayIndex + 1] == 0x08) &&
                            (fileAsArray[arrayIndex + 2] == 0xc0)) // isso nao valera para TM Frames maiores que 255!!!
                        {
                            // busca o tamanho do pacote
                            packetLength = (int)((fileAsArray[arrayIndex + 4] << 8) | (fileAsArray[arrayIndex + 5])) + 6 + 1;

                            if (packetLength > (fileAsArray.Length - arrayIndex))
                            {
                                break; // o pacote estah quebrado, nao conseguimos extrair na implementacao atual
                            }

                            Array.Resize<byte>(ref extractedPacket, packetLength);
                            Array.Copy(fileAsArray, arrayIndex, extractedPacket, 0, packetLength);

                            reportPacket.RawContents = extractedPacket;

                            // Verifica o Crc
                            UInt16 crc = CheckingCodes.CrcCcitt16(ref extractedPacket, extractedPacket.Length - 2);

                            if ((extractedPacket[extractedPacket.Length - 2] == (byte)(crc >> 8)) &&
                                (extractedPacket[extractedPacket.Length - 1] == (byte)(crc & 0xFF)))
                            {
                                // Pacote sem erros
                                sessionLog.LogPacket(reportPacket, false, false, "file");
                            }
                            else
                            {
                                // Pacote com erros
                                sessionLog.LogPacket(reportPacket, false, true, "file");
                            }

                            // segue para o proximo pacote
                            arrayIndex += packetLength;
                            numberOfExtractedPackets++;
                        }
                        else
                        {
                            arrayIndex++; // repetira a busca pelo inicio do pacote a partir do proximo byte
                        }
                    }

                    // Fecha o arquivo
                    fileStream.Close();
                    fileStream.Dispose();
                    binaryReader.Close();
                }

                MessageBox.Show("A total of " + numberOfExtractedPackets.ToString() + " packets were imported successfully !",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbFrames_CheckedChanged_1(object sender, EventArgs e)
        {
            // tabpages nao sao desabilitaveis, preciso fazer um cast para Control
            ((Control)tabPage4).Enabled = rbFrames.Checked;

            // desabilitado ate que tenhamos implementadas outras opcoes de codificacao em tempo real
            //lblTmChannelCod.Enabled = rbFrames.Checked;
            //cmbTmChannelCod.Enabled = rbFrames.Checked;

            if (!rbTcpIp.Checked)
            {
                chkDiscardIdleFrame.Enabled = rbFrames.Checked;
                chkConfigureFarm.Enabled = rbFrames.Checked;
            }
            else
            {
                chkConfigureFarm.Enabled = true;
            }
        }

        private void chkAskTm_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkAskTm.Checked == false) // se for deselecionado
            {
                tmrAskForTM.Enabled = false;
                numSeconds.Enabled = true;
                label8.Enabled = numSeconds.Enabled;
            }
            else // se for selecionada
            {
                if (connected == true)
                {
                    numSeconds.Enabled = false;
                    label8.Enabled = numSeconds.Enabled;

                    if (waitingForTm == false)
                    {
                        tmrAskForTM.Interval = (int)numSeconds.Value * 1000;
                        tmrAskForTM.Enabled = true;
                    }
                }
            }
        }

        private void btChangeFrameHeader_Click(object sender, EventArgs e)
        {
            if (btChangeFrameHeader.Text == "Change TC Frame Header")
            {
                btChangeFrameHeader.Text = "Save and Apply Changes";

                mskVersion.Enabled = true;
                cmbFrameType.Enabled = true;
                mskResA.Enabled = true;
                mskSpacecraftID.Enabled = true;
                mskVCID.Enabled = true;
                mskResB.Enabled = true;
                cmbSeqFlags.Enabled = true;
                mskMapId.Enabled = true;

                btCancelFrameHeader.Visible = true;
                mskVersion.Focus();
            }
            else
            {
                DbConfiguration.TcFrameVersion = int.Parse(mskVersion.Text);
                DbConfiguration.TcFrameType = cmbFrameType.SelectedIndex;
                DbConfiguration.TcFrameReservedA = int.Parse(mskResA.Text);
                DbConfiguration.TcFrameScid = int.Parse(mskSpacecraftID.Text);
                DbConfiguration.TcFrameVcid = int.Parse(mskVCID.Text);
                DbConfiguration.TcFrameReservedB = int.Parse(mskResB.Text);
                DbConfiguration.TcFrameSeqFlags = cmbSeqFlags.SelectedIndex;
                DbConfiguration.TcFrameMapid = int.Parse(mskMapId.Text);
                DbConfiguration.Save();

                btChangeFrameHeader.Text = "Change TC Frame Header";

                mskVersion.Enabled = false;
                cmbFrameType.Enabled = false;
                mskResA.Enabled = false;
                mskSpacecraftID.Enabled = false;
                mskVCID.Enabled = false;
                mskResB.Enabled = false;
                cmbSeqFlags.Enabled = false;
                mskMapId.Enabled = false;

                btCancelFrameHeader.Visible = false;
            }
        }

        private void btCancelFrameHeader_Click(object sender, EventArgs e)
        {
            btChangeFrameHeader.Text = "Change TC Frame Header";

            mskVersion.Enabled = false;
            cmbFrameType.Enabled = false;
            mskResA.Enabled = false;
            mskSpacecraftID.Enabled = false;
            mskVCID.Enabled = false;
            mskResB.Enabled = false;
            cmbSeqFlags.Enabled = false;
            mskMapId.Enabled = false;

            btCancelFrameHeader.Visible = false;

            mskVersion.Text = DbConfiguration.TcFrameVersion.ToString();
            cmbFrameType.SelectedIndex = DbConfiguration.TcFrameType;
            mskResA.Text = DbConfiguration.TcFrameReservedA.ToString();
            mskSpacecraftID.Text = DbConfiguration.TcFrameScid.ToString();
            mskVCID.Text = DbConfiguration.TcFrameVcid.ToString();
            mskResB.Text = DbConfiguration.TcFrameReservedB.ToString();
            cmbSeqFlags.SelectedIndex = DbConfiguration.TcFrameSeqFlags;
            mskMapId.Text = DbConfiguration.TcFrameMapid.ToString();
        }

        private void mskVersion_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskVersion, 3);
        }

        private void mskResA_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskResA, 3);
        }

        private void mskSpacecraftID_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskSpacecraftID, 1023);
        }

        private void mskVCID_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskVCID, 63);
        }

        private void mskResB_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskResB, 3);
        }

        private void mskMapId_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskMapId, 63);
        }

        private void cbAutoLength_CheckedChanged(object sender, EventArgs e)
        {
            mskFrameLength.ReadOnly = cbAutoLength.Checked;

            if (mskFrameLength.ReadOnly)
            {
                mskFrameLength.BackColor = SystemColors.Control;
                mskFrameLength.Text = "";
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
                mskFrameSeq.Text = "0";
            }
            else
            {
                mskFrameSeq.BackColor = SystemColors.Window;
            }
        }

        private void cbAutoCRC_CheckedChanged(object sender, EventArgs e)
        {
            mskFrameCRC.ReadOnly = cbAutoCRC.Checked;

            if (mskFrameCRC.ReadOnly)
            {
                mskFrameCRC.BackColor = SystemColors.Control;
                mskFrameCRC.Text = "";
            }
            else
            {
                mskFrameCRC.BackColor = SystemColors.Window;
            }
        }

        private void mskFrameLength_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskFrameLength, 255);
        }

        private void mskFrameSeq_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskFrameSeq, 255);
        }

        private void mskFrameCRC_Leave(object sender, EventArgs e)
        {
            if (!cbAutoCRC.Checked)
            {
                mskFrameCRC.Text = mskFrameCRC.Text.ToUpper();
                int parsedValue = 0;

                if (int.TryParse(mskFrameCRC.Text.Replace("-", ""), System.Globalization.NumberStyles.HexNumber, null, out parsedValue) == false)
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
                        label32.Enabled = false;
                        label31.Enabled = false;
                        label30.Enabled = false;
                        mskFirstOctect.Enabled = false;
                        mskSecondOctect.Enabled = false;
                        mskThirdOctect.Enabled = false;
                        break;
                    }
                case 1: // SET V(R)
                    {
                        label32.Enabled = true;
                        label31.Enabled = true;
                        label30.Enabled = true;
                        label32.Text = "First\nOctect";
                        mskFirstOctect.Text = "130";
                        mskFirstOctect.Enabled = true;
                        mskSecondOctect.Enabled = true;
                        mskThirdOctect.Enabled = true;
                        break;
                    }
                case 2: // UNLOCK
                    {
                        label32.Enabled = true;
                        label31.Enabled = false;
                        label30.Enabled = false;
                        label32.Text = "Single\nOctect";
                        mskFirstOctect.Text = "0";
                        mskFirstOctect.Enabled = true;
                        mskSecondOctect.Enabled = false;
                        mskThirdOctect.Enabled = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void mskFirstOctect_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskFirstOctect, 255);
        }

        private void mskSecondOctect_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskSecondOctect, 255);
        }

        private void mskThirdOctect_Leave(object sender, EventArgs e)
        {
            ValidateFields(mskThirdOctect, 255);
        }

        private void numUpdateTm_ValueChanged(object sender, EventArgs e)
        {
            tmrUpdateTmFrame.Enabled = false;
            tmrUpdateTmFrame.Interval = (int)numUpdateTm.Value * 1000;
            tmrUpdateTmFrame.Enabled = true;
        }

        private void chkPause_CheckedChanged(object sender, EventArgs e)
        {
            tmrUpdateTmFrame.Enabled = !chkPause.Checked;
        }

        private void chkDiscardIdleFrame_CheckedChanged(object sender, EventArgs e)
        {
            // Sinaliza a opcao para obter Frame Idle ou nao
            tmDecoder.DiscardIdleFrame = chkDiscardIdleFrame.Checked;
        }

        private void gridSessionStats_SelectionChanged(object sender, EventArgs e)
        {
            // impede que o usuario selecione linhas, prejudicando a visualizacao
            gridSessionStats.CurrentRow.Selected = false;
        }

        private void btSendCommandToFarm_Click(object sender, EventArgs e)
        {
            RawFrame frameTc;
            byte[] cltu;

            byte[] commandToFarm = null;

            switch (cmbControlCommand.SelectedIndex)
            {
                case 0: // invalid command
                    {
                        commandToFarm = new byte[1];
                        commandToFarm[0] = 0;

                        frameTc = GenerateFrameTc(commandToFarm, true, -1); // -1 para respeitar o VCID de DbConfiguration
                        cltu = GenerateCltu(frameTc);
                        break;
                    }
                case 1: // set v(r)
                    {
                        commandToFarm = new byte[3];
                        commandToFarm[0] = (byte)(int.Parse(mskFirstOctect.Text));
                        commandToFarm[1] = (byte)(int.Parse(mskSecondOctect.Text));
                        commandToFarm[2] = (byte)(int.Parse(mskThirdOctect.Text));

                        frameTc = GenerateFrameTc(commandToFarm, true, -1); // -1 para respeitar o VCID de DbConfiguration
                        cltu = GenerateCltu(frameTc);

                        // mantem o controle de sequencia da tela sincronizado com o comandado para o FARM
                        //TODO: verificar impacto deste if no modo offline
                        if (DbConfiguration.TcFrameVcid == 0)
                        {
                            nextTcFrameSequenceForVc0 = (int)commandToFarm[2];

                            gridSessionStats[1, 3].Value = nextTcFrameSequenceForVc0;
                        }
                        else if (DbConfiguration.TcFrameVcid == 1)
                        {
                            nextTcFrameSequenceForVc1 = (int)commandToFarm[2];

                            gridSessionStats[3, 3].Value = nextTcFrameSequenceForVc1;
                        }

                        break;
                    }
                default: // 2, unlock
                    {
                        commandToFarm = new byte[1];
                        commandToFarm[0] = (byte)(int.Parse(mskFirstOctect.Text));

                        frameTc = GenerateFrameTc(commandToFarm, true, -1); // -1 para respeitar o VCID de DbConfiguration
                        cltu = GenerateCltu(frameTc);

                        break;
                    }
            }
            
            if (rbTcpIp.Checked)
            {
                cortex.Vcid = mskVCID.Text; //o mesmo vcid que esta neste componente esta tbm na dbConfiguration

                if (rbPackets.Checked || rbFrames.Checked)
                {
                    cortex.COPRequestToDirectTransferFrame(txtCopTcDataFlow.Text.Trim(), frameTc.RawContents);
                }
                else if (rbCltu.Checked)
                {
                    cortex.COPRequestTODirectTransferCLTU(txtCopTcDataFlow.Text.Trim(), cltu);
                }
            }
            else if (rbRs422.Checked)
            {
                rs422.TxBuffer = cltu;
                rs422.TransmitMessage();
            }
        }

        #endregion

        #region Codigo de recepcao de dados pelo pipe

        /**
         * Vincula a ocorrencia de um evento a uma rotina de tratamento local.
         * Obs: ainda nao entendi direito esta funcao.
         **/
        void pipe_ByteReceived(byte[] receivedByte)
        {
            try
            {
                Invoke(new PipeHandling.ByteReceivedHandler(DisplayByteReceived), new object[] { receivedByte });
            }
            catch (Exception)
            {
            }
        }

        /**
         * Rotina de tratamento do evento de byte recebido pelo pipe
         **/
        void DisplayByteReceived(byte[] receivedByte)
        {
            RawPacket.ReceptionStatus status;

            status = reportPacket.ReceiveByte(receivedByte[0]);

            if (status == RawPacket.ReceptionStatus.packet_completed)
            {
                // Dispara o evento com seus argumentos
                if (TelemetryReceived != null)
                {
                    // Armazena o service Type e o Subtype
                    int serviceType = (int)reportPacket.GetPart(56, 8);
                    int serviceSubtype = (int)reportPacket.GetPart(64, 8);

                    TelemetryEventArgs telemetryEventArgs =
                        new TelemetryEventArgs(serviceType, serviceSubtype, reportPacket);

                    TelemetryReceived(this, telemetryEventArgs);
                }

                if (saveSession)
                {
                    session.LogPacket(reportPacket, false, false, connectionType);
                }

                // Executa o metodo que fara as verificoes para iniciar o
                // timer e envia o pacote de sincronizacao de tempo
                // caso o usuario tenha checkado estas opcoes
                SynchRequest();
                chkAskTm_CheckedChanged_1(this, EventArgs.Empty);

                mdiMain.statusStrip.Items[0].Text = "Report Packet received through pipe: " + reportPacket.GetString();
            }
            else if (status == RawPacket.ReceptionStatus.packet_completed_with_error)
            {
                if (saveSession)
                {
                    session.LogPacket(reportPacket, false, true, connectionType);
                }

                mdiMain.statusStrip.Items[0].Text = "Report Packet received through pipe: " + reportPacket.GetString();
            }
        }

        #endregion

        #region Codigo de recepcao de dados pelas seriais RS-232, RS-422 e Ethernet

        /** Evento de recepcao de dados pela serial. **/
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /**
             * Preciso invocar outro tratador de evento, pois serial_DataReceived eh
             * executado em outra thread (criada pelo SerialPort em Open(). Como
             * este evento eh tratado por outro handler, nao pode manipular objetos
             * criados na thread principal do simulador.
             **/
            this.Invoke(new EventHandler(SerialRead));
        }

        // Respeitar esta assinatura [void (object, EventArgs)], necessaria para qualquer EventHandler
        void SerialRead(object s, EventArgs e)
        {
            int remainingBytes = serial.BytesToRead;
            byte[] buffer = new byte[remainingBytes];
            RawPacket.ReceptionStatus status;

            serial.Read(buffer, 0, remainingBytes);

            for (int i = 0; i < remainingBytes; i++)
            {
                status = reportPacket.ReceiveByte(buffer[i]);

                if (status == RawPacket.ReceptionStatus.packet_completed)
                {
                    if (TelemetryReceived != null)
                    {
                        // Armazena o service Type e o Subtype
                        int serviceType = (int)reportPacket.GetPart(56, 8);
                        int serviceSubtype = (int)reportPacket.GetPart(64, 8);

                        // Inseri o service type e subtype no construtor do argumento
                        // e inseri o argumento no evento "TelemetryEvent"
                        TelemetryEventArgs telemetryEventArgs =
                            new TelemetryEventArgs(serviceType, serviceSubtype, reportPacket);

                        // Dispara o evento com seus argumentos
                        TelemetryReceived(this, telemetryEventArgs);
                    }

                    if (saveSession)
                    {
                        session.LogPacket(reportPacket, false, false, connectionType);
                    }

                    // Executa o metodo que fara as verificoes para iniciar o
                    // timer e envia o pacote de sincronizacao de tempo
                    // caso o usuario tenha checkado estas opcoes
                    SynchRequest();
                    chkAskTm_CheckedChanged_1(this, EventArgs.Empty);

                    mdiMain.statusStrip.Items[0].Text = "Report Packet received through serial: " + reportPacket.GetString();
                }
                else if (status == RawPacket.ReceptionStatus.packet_completed_with_error)
                {
                    if (saveSession)
                    {
                        session.LogPacket(reportPacket, false, true, connectionType);
                    }

                    mdiMain.statusStrip.Items[0].Text = "Report Packet received through serial: " + reportPacket.GetString();
                }
            }
        }

        private void RS422_ReceivedData(object sender, DataReceiveEventArgs eventArgs)
        {
            try
            {
                if (rbFrames.Checked)
                {
                    // Envia o buffer para a TmDecoder para decodificacao em frames de TM
                    tmDecoder.RxBuffer = eventArgs.RxBuffer;
                }
                else if (rbPackets.Checked)
                {
                    if (availablePacketEventHandler != null)
                    {
                        // Envia os argumentos ao EventArgs. Copia somente os bytes recebidos
                        availablePacketEventArgs.Packet = eventArgs.RxBuffer;
                        availablePacketEventArgs.NumBytes = eventArgs.NumBytes;

                        // Instancia o EventHandler e passa o object sender e o EventArgs com os argumentos.
                        availablePacketEventHandler(this, availablePacketEventArgs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when receiving data through RS-422: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void FrameFound(object sender, FrameFoundEventArgs eventArgs)
        {
            try
            {
                // Envia os argumentos ao EventArgs. Frame Encontrado!!
                availableFrameEventArgs.Frame = eventArgs.FrameFound;
                availableFrameEventArgs.FrameValid = eventArgs.FrameIsValid;

                // incrementa e atualiza os contadores
                if (eventArgs.FrameIsValid == true)
                {
                    receivedTmCounter++;
                    gridSessionStats[3, 1].Value = receivedTmCounter;

                    // verifica se eh idle
                    if (((short)(((short)(((short)((eventArgs.FrameFound[4] << 6)) | ((short)(eventArgs.FrameFound[5]))) << 3)) >> 3)) == 2046)
                    {
                        idleTmCounter++;
                        gridSessionStats[3, 2].Value = idleTmCounter;
                    }
                    else
                    {
                        activeTmCounter++;

                        ExtractTmPackets(ref availableFrameEventArgs);
                        gridSessionStats[1, 2].Value = activeTmCounter;
                    }
                } // if (eventArgs.FrameIsValid == true)

                // guarda um backup do frame para exibicao no proximo tick do timer
                while (usingTmFrameToShow == true)
                {
                    // espera polada (por no maximo uma instrucao no tick do timer)
                    Thread.Sleep(1); // apenas para dar chance ao timer de copiar tmFrameToShow
                }

                usingTmFrameToShow = true;
                Array.Resize(ref tmFrameToShow, eventArgs.FrameFound.Length);
                Array.Copy(eventArgs.FrameFound, tmFrameToShow, eventArgs.FrameFound.Length);
                usingTmFrameToShow = false;

                tmFrameToShowIsValid = eventArgs.FrameIsValid;

                // Instancia o EventHandler e passa o object sender e o EventArgs com os argumentos.
                if (availableFrameEventHandler != null)
                {
                    availableFrameEventHandler(this, availableFrameEventArgs);
                }

                if ((waitingForTmFrame == true) && (chkConfigureFarm.Checked == true))
                {
                    byte[] cltu;

                    // apos a recepcao do primeiro frame de tm, se o usuario pediu para
                    // configurar o farm, enviamos 4 comandos ao FARM: UNLOCK e SET V(R)
                    // para os VCs 0 e 1
                    waitingForTmFrame = false;

                    byte[] commandToFarm = new byte[1];
                    commandToFarm[0] = 0;

                    // unlock para o VC 0
                    RawFrame frameTc = GenerateFrameTc(commandToFarm, true, 0); // -1 para respeitar o VCID de DbConfiguration
                    cltu = GenerateCltu(frameTc);

                    if (rbTcpIp.Checked)
                    {
                        cortex.Vcid = frameTc.GetPart(16, 6).ToString();
                        cortex.SpacecraftId = frameTc.GetPart(6, 10).ToString();
                        cortex.COPRequestTODirectTransferCLTU(txtCopTcDataFlow.Text.Trim(), cltu);
                    }
                    else if (rbRs422.Checked)
                    {
                        rs422.TxBuffer = cltu;
                        rs422.TransmitMessage();
                    }

                    // unlock para o VC 1
                    frameTc = GenerateFrameTc(commandToFarm, true, 1); // -1 para respeitar o VCID de DbConfiguration
                    cltu = GenerateCltu(frameTc);

                    if (rbTcpIp.Checked)
                    {
                        cortex.Vcid = frameTc.GetPart(16, 6).ToString();
                        cortex.SpacecraftId = frameTc.GetPart(6, 10).ToString();
                        cortex.COPRequestTODirectTransferCLTU(txtCopTcDataFlow.Text.Trim(), cltu);
                    }
                    else if (rbRs422.Checked)
                    {
                        rs422.TxBuffer = cltu;
                        rs422.TransmitMessage();
                    }

                    commandToFarm = new byte[3];
                    commandToFarm[0] = 130;
                    commandToFarm[1] = 0;
                    commandToFarm[2] = 1;

                    // set v(r) para o VC 0
                    frameTc = GenerateFrameTc(commandToFarm, true, 0);
                    cltu = GenerateCltu(frameTc);

                    if (rbTcpIp.Checked)
                    {
                        cortex.Vcid = frameTc.GetPart(16, 6).ToString();
                        cortex.SpacecraftId = frameTc.GetPart(6, 10).ToString();
                        cortex.COPRequestTODirectTransferCLTU(txtCopTcDataFlow.Text.Trim(), cltu);
                    }
                    else if (rbRs422.Checked)
                    {
                        rs422.TxBuffer = cltu;
                        rs422.TransmitMessage();
                    }

                    nextTcFrameSequenceForVc0 = 0;
                    gridSessionStats[1, 3].Value = nextTcFrameSequenceForVc0;

                    // set v(r) para o VC 1
                    frameTc = GenerateFrameTc(commandToFarm, true, 1);
                    cltu = GenerateCltu(frameTc);

                    if (rbTcpIp.Checked)
                    {
                        cortex.Vcid = frameTc.GetPart(16, 6).ToString();
                        cortex.SpacecraftId = frameTc.GetPart(6, 10).ToString();
                        cortex.COPRequestTODirectTransferCLTU(txtCopTcDataFlow.Text.Trim(), cltu);
                    }
                    else if (rbRs422.Checked)
                    {
                        rs422.TxBuffer = cltu;
                        rs422.TransmitMessage();
                    }

                    nextTcFrameSequenceForVc1 = 0;
                    gridSessionStats[3, 3].Value = nextTcFrameSequenceForVc1;
                }
            }
            catch (Exception ex)
            {
                if (mdiMain.FormSimCortex != null)
                {
                    return;
                }

                MessageBox.Show("Error when processing a frame found: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Transmissao de pacotes para o FSW

        public void SendRequest(RawPacket requestPacket, SourceRequestPacket sourcePacket)
        {
            int serviceType;
            int serviceSubtype;
            int apid = (int)requestPacket.GetPart(5, 11);
            int currentSsc = ssc.GetLastSent(apid) + 1;

            // check "Manage TCs packet sequence automatically" esta marcado.
            if (chkAutoSequence.Checked)
            {
                byte[] part = new byte[2];

                part[0] = (byte)(currentSsc >> 8);
                part[1] = (byte)(currentSsc & 0xFF);

                requestPacket.SetPart(18, 14, part);
            }

            if (rbTcpIp.Checked)
            {   
                int vcid = 0;
                // comunicacao por frames
                if (!offlineMode)
                {
                    // gera a cltu a partir do pacote recebido, buscando o vcid a partir do apid do pacote
                    vcid = (int)DbInterface.ExecuteScalar("select isnull(vcid, 0) from apids where apid = " + requestPacket.GetPart(5, 11).ToString());
                }

                if (rbPackets.Checked == true)
                {
                    cortex.Vcid = vcid.ToString();
                    cortex.COPRequestToTransferSegmentBD(txtCopTcDataFlow.Text.Trim(), requestPacket.RawContents);
                }
                else if (rbFrames.Checked == true)
                {
                    // Adicionar depois alguns itens de configuracao para separar o envio das mensagens entre BD, AD ou bypass.
                    RawFrame frameTc = GenerateFrameTc(requestPacket.RawContents, false, vcid);
                    cortex.Vcid = frameTc.GetPart(16, 6).ToString();
                    cortex.SpacecraftId = frameTc.GetPart(6, 10).ToString();
                    cortex.COPRequestToDirectTransferFrame(txtCopTcDataFlow.Text.Trim(), frameTc.RawContents);
                }
                else if (rbCltu.Checked == true)
                {
                    // Adicionar depois alguns itens de configuracao para separar o envio das mensagens entre BD, AD ou bypass.
                    RawFrame frameTc = GenerateFrameTc(requestPacket.RawContents, false, vcid);
                    cortex.Vcid = frameTc.GetPart(16, 6).ToString();
                    cortex.SpacecraftId = frameTc.GetPart(6, 10).ToString();

                    byte[] cltu = GenerateCltu(frameTc);
                    cortex.COPRequestTODirectTransferCLTU(txtCopTcDataFlow.Text.Trim(), cltu);
                }
            }
            else if (rbNamedPipe.Checked)
            {
                pipe.Write(requestPacket.RawContents);
            }
            else if (rbSerial.Checked)
            {
                serial.Write(requestPacket.RawContents, 0, requestPacket.Size);
            }
            else if (rbRs422.Checked)
            {
                if (rbFrames.Checked == true)
                {
                    int vcid = 0;
                    
                    // comunicacao por frames
                    if (!offlineMode)
                    {
                        // gera a cltu a partir do pacote recebido, buscando o vcid a partir do apid do pacote
                        vcid = (int)DbInterface.ExecuteScalar("select isnull(vcid, 0) from apids where apid = " + requestPacket.GetPart(5, 11).ToString());
                    }

                    RawFrame frameTc = GenerateFrameTc(requestPacket.RawContents, false, vcid);
                    byte[] cltu = GenerateCltu(frameTc);
                    rs422.TxBuffer = cltu;
                }
                else
                {
                    // comunicacao por pacotes
                    rs422.TxBuffer = requestPacket.RawContents;
                }

                rs422.TransmitMessage();
            }
            else //file
            {
                // pega os dados do pacote e adiciona-os no file
                serviceType = (int)requestPacket.GetPart(56, 8); // pegar o serviceType do request                
                serviceSubtype = (int)requestPacket.GetPart(64, 8); // pegar o serviceSubtype do request

                String subtypeDescription = "";

                if ((frmTcsComposition != null) && (sourcePacket == SourceRequestPacket.RequestComposition))
                {
                    subtypeDescription = frmTcsComposition.cmbServiceSubtype.Text.Substring(6, frmTcsComposition.cmbServiceSubtype.Text.Length - 6);
                }
                else
                {
                    subtypeDescription = frmSavedRequests.gridDatabase.SelectedRows[0].Cells[1].Value.ToString();
                }

                if ((tcFile.AddTagSessionIdOnFile) && (saveSession))
                {
                    // adiciona a nova sessionID no file
                    String sql = "select isnull(max(session_id), 0) + 1 from sessions";
                    int sessionId = (int)DbInterface.ExecuteScalar(sql);
                    tcFile.Write("// Session Id: " + sessionId.ToString());
                    tcFile.Write("");
                    tcFile.AddTagSessionIdOnFile = false;
                }

                // pula uma linha ao adicionar o primeiro pacote no arquivo, e caso
                // a session nao tiver que ser salva.
                if (tcFile.IsFirstPacket && (!saveSession))
                {
                    tcFile.Write("");
                    tcFile.IsFirstPacket = false;
                }

                bool packetAdded = tcFile.Write(requestPacket.GetString().Replace("-", "") + "\t// [" + serviceType + "." + serviceSubtype + "] " + subtypeDescription);

                if (!packetAdded)
                {
                    MessageBox.Show("This packet was not added to file " + filePath,
                                "Sending Request",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Packet added to file " + filePath,
                                    "Sending Request",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }

            // Registra o packet enviado no log
            if (saveSession)
            {
                if (mdiMain.FormSimCortex == null)
                {
                    session.LogPacket(requestPacket, true, false, connectionType);
                }
            }

            mdiMain.statusStrip.Items[0].Text = "Request Packet Sent: " + requestPacket.GetString();

            // pego a sequencia do pacote, se o check 
            // "Manage TCs packet sequence automatically" nao estiver marcado, o currentSsc
            // recebera a sequencia do componente numSSC.
            currentSsc = (int)requestPacket.GetPart(18, 14);

            // Atualiza o controle de SSCs
            ssc.SetLastSent(apid, currentSsc);

            // check autoIncrement da tela TcsComposition esta marcado, por isso deve ser atualizado.
            if ((frmTcsComposition != null) && (sourcePacket == SourceRequestPacket.RequestComposition))
            {
                if (chkAutoSequence.Checked || frmTcsComposition.chkAutoIncrement.Checked)
                {
                    if ((ssc.GetLastSent(apid) + 1) > frmTcsComposition.numSSC.Maximum)
                    {
                        frmTcsComposition.numSSC.Value = 1;
                    }
                    else
                    {
                        frmTcsComposition.numSSC.Value = ssc.GetLastSent(apid) + 1;
                    }
                }
            }
        }

        private RawFrame GenerateFrameTc(byte[] request, bool isCommandToFarm, int virtualChannel)
        {
            RawFrame frameTc = new RawFrame(true);
            byte[] part;
            int whichVc; // usado para o controle de sequencia

            if (isCommandToFarm)
            {
                frameTc.Resize(RawFrame.FRAME_LENGTH + request.Length);
            }
            else
            {
                // redimensiona o frame para o tamanho correto (7 eh o frame header + CRC)            
                frameTc.Resize(RawFrame.FRAME_LENGTH + RawFrame.SEGMENT_LENGTH + request.Length);
            }

            part = new byte[1];

            // Version number
            part[0] = (byte)DbConfiguration.TcFrameVersion;
            part[0] = (byte)(part[0] << 6);
            frameTc.SetPart(0, 2, part);

            // Frame type
            if (isCommandToFarm == true)
            {
                part[0] = 3; // 11b, control frame
            }
            else
            {
                part[0] = (byte)DbConfiguration.TcFrameType;
            }

            part[0] = (byte)(part[0] << 4);
            frameTc.SetPart(2, 2, part);

            // Reserved field A
            part[0] = (byte)(DbConfiguration.TcFrameReservedA << 2);
            frameTc.SetPart(4, 2, part);

            // Spacecraft id
            part = new byte[2];
            part[0] = (byte)(DbConfiguration.TcFrameScid >> 6);
            part[1] = (byte)(DbConfiguration.TcFrameScid & 0xff);
            frameTc.SetPart(6, 10, part);

            // Virtual channel id
            part = new byte[1];

            // se foi passado um vcid, respeita; se nao, usa o de dbconfiguration
            if (virtualChannel >= 0)
            {
                part[0] = (byte)(virtualChannel << 2);
                whichVc = virtualChannel;
            }
            else
            {
                // apenas caio aqui para comandos ao FARM enviados manualmente pelo usuario
                part[0] = (byte)(DbConfiguration.TcFrameVcid << 2);
                whichVc = DbConfiguration.TcFrameVcid;
            }

            frameTc.SetPart(16, 6, part);

            // Reserved field B
            part[0] = (byte)(DbConfiguration.TcFrameReservedB);
            frameTc.SetPart(22, 2, part);

            // Sequence number (independente de a sequencia ser automatica)
            if (isCommandToFarm == true)
            {
                part[0] = 0; // para frames BC/BD, deve ser zero
            }
            else
            {
                if (cbAutoIncrement.Checked == true)
                {
                    part[0] = (byte)GetVcSequence(whichVc);
                }
                else
                {
                    part[0] = (byte)(int.Parse(mskFrameSeq.Text));
                }
            }

            // pega o numero da sequencia do frame no campo mskFrameSeq
            int sequenceNumber = int.Parse(mskFrameSeq.Text.ToString());
            part[0] = (byte)sequenceNumber; 
            
            frameTc.SetPart(32, 8, part);

            // Agora alimenta a area de dados do pacote
            if (isCommandToFarm == false)
            {
                // Segment header: sequence flags
                part[0] = (byte)(DbConfiguration.TcFrameSeqFlags);

                part[0] = (byte)(part[0] << 6);
                frameTc.SetPart(40, 2, part);

                // Segment header: map id
                part[0] = (byte)(DbConfiguration.TcFrameMapid);
                frameTc.SetPart(42, 6, part);

                // Se o usuario informou o frame length, sobreponho o tamanho
                // se nao, o RawFrame gerencia o tamanho
                if (cbAutoLength.Checked == false)
                {
                    part[0] = (byte)(int.Parse(mskFrameLength.Text));
                    frameTc.SetPart(24, 8, part);
                }

                // os pacotes comecam no bit 48
                frameTc.SetPart(48, (request.Length * 8), request);
            }
            else // para comandos ao FARM
            {
                // os pacotes comecam no bit 48
                frameTc.SetPart(40, (request.Length * 8), request);

                // Se o usuario informou o frame length, sobreponho o tamanho
                if (cbAutoLength.Checked == false)
                {
                    part[0] = (byte)(int.Parse(mskFrameLength.Text));
                    frameTc.SetPart(24, 8, part);
                }
            }

            // Finalmente, se o CRC nao for automatico, insere no frame
            // se for automatico, o RawFrame gerencia
            if (cbAutoCRC.Checked == false)
            {
                int crcValue = 0;

                int.TryParse(mskFrameCRC.Text.Replace("-", ""),
                             System.Globalization.NumberStyles.HexNumber,
                             null,
                             out crcValue);

                part = new byte[2];
                part[0] = (byte)(crcValue >> 8);
                part[1] = (byte)(crcValue & 0xff);

                frameTc.AutoCrc = false;
                frameTc.SetPart(((frameTc.Size * 8) - 16), 16, part);
            }

            return frameTc;
        }

        /** 
         * Cria uma CLTU para envio, a partir dos dados recebidos. Gerencia o controle de sequencia.
         * 
         * O argumento virtualChannel so eh usado se isCommandToFarm = true;
         * Caso se queira mandar um comando ao Farm respeitando o VC gravado em
         * DbConfiguration, virtualChannel deve ser menor que zero.
         * TODO: modificar para suportar o modo offline
         **/
        private byte[] GenerateCltu(RawFrame frameTc)
        {
            // Agora criar a CLTU para inserir o frame
            int cltuSize = (int)(Math.Ceiling((double)(frameTc.Size / (double)7)));
            cltuSize = (cltuSize * 8) + 10;
            byte[] cltu = new byte[cltuSize];

            // Start sequence
            cltu[0] = 0xeb;
            cltu[1] = 0x90;

            int nextCltuIndex = 2;
            int nextFrameIndex = 0;

            byte errorControl = 0;

            // Monta os codeblocks
            for (nextFrameIndex = 0; nextFrameIndex < frameTc.Size; nextFrameIndex += 7)
            {
                byte[] information = new byte[7];

                // Copia a informacao do codeblock para a cltu
                for (int j = 0; j < 7; j++)
                {
                    if ((nextFrameIndex + j) < frameTc.Size)
                    {
                        // Informacao
                        cltu[nextCltuIndex + j] = frameTc.RawContents[nextFrameIndex + j];
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
                    return null;
                }

                nextCltuIndex += 8;
            }

            // Agora adiciona o tail sequence
            for (int i = 0; i < 8; i++)
            {
                cltu[nextCltuIndex + i] = 0x55; // fill octect
            }

            sentTcCounter++;
            gridSessionStats[1, 1].Value = sentTcCounter;

            return cltu;
        }

        /** 
         * Pacote de sincronizacao de tempo
         * Cria um 'synchRequest' para sincronizar o relogio de bordo
         * com o relogio do servidor de banco de dados.
         **/
        private void SynchRequest()
        {
            if ((chkSyncronize.Checked) && (connected) && (waitingForTm))
            {
                waitingForTm = false;

                RawPacket request = new RawPacket(9, 128);

                // obtem o relogio do servidor de dados
                String time = DbInterface.ExecuteScalar("select convert(varchar, getdate(), 103) + ' ' + convert(varchar, getdate(), 108) + '.000000'").ToString();

                // agora converte a string de calendario em uma string no formato de bordo
                String convertedTime = TimeCode.CalendarToOnboardTime(time);

                // converte a string em um array de bytes
                byte[] onboardTime = Utils.Formatting.HexStringToByteArray(convertedTime);

                // soma 2 segundos para compensar atrasos (chamadas a metodos no SMC, 
                // consulta ao banco de dados, transmissao ao OBC, processamento no OBC)

                /** @todo A soma de 2 segundos ao tempo no pacote de sincronizacao eh um chute;
                  * ajustar esse valor quando necessario (por enquanto, isso nao eh preciso). **/
                onboardTime[3] += 2;

                request.SetPart(80, 48, onboardTime); // o CRC eh calculado automaticamente

                // Envia o pacote
                SendRequest(request, SourceRequestPacket.OtherSource);
            }
        }

        /**
         * Envia uma CLTU com Frame de Telecomando para o OBDH via rs422 e Tcp/IP Cortex.
         **/
        public bool SendCltu(byte[] cltu)
        {
            try
            {
                if (connectionType.ToUpper().Equals("RS422"))
                {
                    rs422.TxBuffer = cltu;
                    rs422.TransmitMessage();

                    if (cbAutoIncrement.Checked)
                    {
                        mskFrameSeq.Text = (GetVcSequence(int.Parse(mskVCID.Text.ToString())) + 1).ToString();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Metodos Privados

        private bool IsValidIPv4(String value)
        {
            IPAddress address;

            if (IPAddress.TryParse(value, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }

        private void ValidateFields(MaskedTextBox field, int size)
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
        }

        private void ConfigureStatisticsGrid()
        {
            gridSessionStats.Columns.Add("col1", "");
            gridSessionStats.Columns.Add("col2", "");
            gridSessionStats.Columns.Add("col3", "");
            gridSessionStats.Columns.Add("col4", "");

            gridSessionStats.Rows.Add(4);
            gridSessionStats.Rows[0].Height = 19;
            gridSessionStats.Rows[1].Height = 19;
            gridSessionStats.Rows[2].Height = 19;
            gridSessionStats.Rows[3].Height = 19;

            gridSessionStats[0, 0].Value = "Session start:";
            gridSessionStats[1, 0].Value = "[no session]";
            gridSessionStats[2, 0].Value = "Session end:";
            gridSessionStats[3, 0].Value = "[no session]";
            gridSessionStats[0, 1].Value = "Sent TC frames:";
            gridSessionStats[1, 1].Value = 0;
            gridSessionStats[2, 1].Value = "Rec. TM frames:";
            gridSessionStats[3, 1].Value = 0;
            gridSessionStats[0, 2].Value = "TM active frames:";
            gridSessionStats[1, 2].Value = 0;
            gridSessionStats[2, 2].Value = "TM idle frames:";
            gridSessionStats[3, 2].Value = 0;
            gridSessionStats[0, 3].Value = "Nxt TC seq.VC0:";
            gridSessionStats[1, 3].Value = 0;
            gridSessionStats[2, 3].Value = "Nxt TC seq.VC1:";
            gridSessionStats[3, 3].Value = 0;

            gridSessionStats.Columns[0].Width = 95;
            gridSessionStats.Columns[1].Width = 130;
            gridSessionStats.Columns[2].Width = 95;
            gridSessionStats.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            gridSessionStats.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;
            gridSessionStats.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridSessionStats.Columns[2].DefaultCellStyle.BackColor = Color.Lavender;
            gridSessionStats.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ConfigureClcwGrid()
        {
            gridClcw.Columns.Add("control_word_type", "Control Word Type");
            gridClcw.Columns.Add("clcw_version_number", "CLCW Version Nr.");
            gridClcw.Columns.Add("status_field", "Status Field");
            gridClcw.Columns.Add("cop_in_effect", "COP in Effect");
            gridClcw.Columns.Add("clcw_vcid", "VCID (TC)");
            gridClcw.Columns.Add("reserved", "Reserv.");
            gridClcw.Columns.Add("no_rf_available", "No RF Avail.");
            gridClcw.Columns.Add("no_bit_lock", "No Bit Lock");
            gridClcw.Columns.Add("lockout", "Lock Out");
            gridClcw.Columns.Add("wait", "Wait");
            gridClcw.Columns.Add("retransmit", "Retransmit");
            gridClcw.Columns.Add("frame_b_counter", "FARM B Counter");
            gridClcw.Columns.Add("report_type", "Report Type");
            gridClcw.Columns.Add("report_value", "Report Value");

            gridClcw.Columns[0].Width = 75;
            gridClcw.Columns[1].Width = 75;
            gridClcw.Columns[2].Width = 44;
            gridClcw.Columns[3].Width = 48;
            gridClcw.Columns[4].Width = 39;
            gridClcw.Columns[5].Width = 50;
            gridClcw.Columns[6].Width = 50;
            gridClcw.Columns[7].Width = 44;
            gridClcw.Columns[8].Width = 35;
            gridClcw.Columns[9].Width = 31;
            gridClcw.Columns[10].Width = 66;
            gridClcw.Columns[11].Width = 56;
            gridClcw.Columns[12].Width = 47;
            gridClcw.Columns[13].Width = 49;
            gridClcw.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ConfigureTmFrameGrid()
        {
            gridTmFrame.Columns.Add("version_number", "Version Nr.");
            gridTmFrame.Columns.Add("scid", "SCID");
            gridTmFrame.Columns.Add("vcid", "VCID");
            gridTmFrame.Columns.Add("op_control_flag", "Op.Control Field Flag");
            gridTmFrame.Columns.Add("master_count", "Master Channel Frame Count");
            gridTmFrame.Columns.Add("vc_count", "Virtual Channel Frame Count");
            gridTmFrame.Columns.Add("second_header_flag", "Secondary Header Flag");
            gridTmFrame.Columns.Add("synch_flag", "Synch. Flag");
            gridTmFrame.Columns.Add("packet_order_flag", "Packet Order Flag");
            gridTmFrame.Columns.Add("segment_length_id", "Segment Length ID");
            gridTmFrame.Columns.Add("first_header_pointer", "First Header Pointer");
            gridTmFrame.Columns.Add("frame_error_control", "Frame Error Control");
            gridTmFrame.Columns.Add("frame_error_control_ok", "Frame Error Control OK?");

            gridTmFrame.Columns[0].Width = 50;
            gridTmFrame.Columns[1].Width = 35;
            gridTmFrame.Columns[2].Width = 35;
            gridTmFrame.Columns[3].Width = 63;
            gridTmFrame.Columns[4].Width = 95;
            gridTmFrame.Columns[5].Width = 95;
            gridTmFrame.Columns[6].Width = 60;
            gridTmFrame.Columns[7].Width = 45;
            gridTmFrame.Columns[8].Width = 50;
            gridTmFrame.Columns[9].Width = 55;
            gridTmFrame.Columns[10].Width = 50;
            gridTmFrame.Columns[11].Width = 50;
            gridTmFrame.Columns[12].Width = 50;
            gridTmFrame.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /** 
         * Obtem e incrementa o controle de sequencia para frames de TC. 
         * @attention Este metodo nao deve ser chamado para frames do tipo BC/BD
         **/
        private int GetVcSequence(int virtualChannel)
        {
            int toReturn = 0;
            switch (virtualChannel)
            {
                case 0:
                    {
                        toReturn = nextTcFrameSequenceForVc0;
                        nextTcFrameSequenceForVc0++;

                        if (nextTcFrameSequenceForVc0 > 255)
                        {
                            nextTcFrameSequenceForVc0 = 0;
                        }

                        gridSessionStats[1, 3].Value = nextTcFrameSequenceForVc0;

                        break;
                    }
                case 1:
                    {
                        toReturn = nextTcFrameSequenceForVc1;
                        nextTcFrameSequenceForVc1++;

                        if (nextTcFrameSequenceForVc1 > 255)
                        {
                            nextTcFrameSequenceForVc1 = 0;
                        }

                        gridSessionStats[3, 3].Value = nextTcFrameSequenceForVc1;

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return toReturn;
        }

        void ExtractTmPackets(ref AvailableFrameEventArgs receivedFrame)
        {
            // Esta rotina foi implementada em conformidade com o padrao CCSDS, nomeado PSS-04-106 Issue 1.
            try
            {
                int indexTmPacketSize = 0; // TELEMETRY PACKET LENGTH.
                int indexStartTmPacket = 0; // primeiro byte do pacote de tm.
                int numTmPacketBytes = 0; // numero total de bytes da tm, incluindo o cabecalho.
                RawPacket tmPacket = new RawPacket(false, false);

                int LFRAME = 0; // guarda o tamanho total do frame
                int LTRAILER = 4; // guarda o tamanho do TRAILER (CLCW)
                int LCRC = 2; // tamanho do CRC

                byte[] telemetryTemporaryBuffer = new byte[0]; // vetor usado para armazenar Telemetrias que estao quebradas entre os frames. Entre 2 frames no maximo.
                bool coppingToTemporaryTmBuffer = false;
                int numRemainingBytesToTemporaryTmBuffer = 0;
                int numRemainingBytesToFillTmBuffer = 0;

                byte[] frame = receivedFrame.Frame;
                LFRAME = frame.Length;

                // O firstHeaderPoint eh quem define se eh frame idle (2046), frame data syncrono (0) ou frame data asyncrono (2047)
                int firstHeaderPoint = (((frame[4] & 0x7) << 8) | frame[5]);
                bool gettingPackets = true;

                if (firstHeaderPoint < 2046) // < 2046 significa DataFrame. O firstHeaderPoint guarda a posicao do primeiro byte do primeiro pacote de dados dentro do campo FrameDataField.
                {
                    indexStartTmPacket = firstHeaderPoint + 6; // adicionar 6 bytes do frame header

                    while (gettingPackets)
                    {
                        numRemainingBytesToFillTmBuffer = numRemainingBytesToTemporaryTmBuffer;
                        numRemainingBytesToTemporaryTmBuffer = ((LFRAME - (LTRAILER + LCRC)) - indexStartTmPacket);

                        if (numRemainingBytesToTemporaryTmBuffer <= 0)
                        {
                            break;
                        }

                        if (numRemainingBytesToTemporaryTmBuffer < (6 + 1))
                        {
                            coppingToTemporaryTmBuffer = true;
                        }

                        // DUVIDA-THIAGO: por que quase todo o codigo abaixo se repete 3 vezes? Nao seria possivel unificar adicionando um ou outro "if" para os pontos especificos?
                        if ((coppingToTemporaryTmBuffer == true) && (numRemainingBytesToTemporaryTmBuffer > 0))
                        {
                            if ((telemetryTemporaryBuffer.Length < (6 + 1)) && (telemetryTemporaryBuffer.Length >= 1)) // o >= 1 significa que pelo menos 1 byte ja tenha que estar no buffer..
                            {
                                int remainingBytesToTmHeader = (6 + 1) - telemetryTemporaryBuffer.Length;
                                Array.Resize(ref telemetryTemporaryBuffer, telemetryTemporaryBuffer.Length + remainingBytesToTmHeader);
                                Array.Copy(frame, indexStartTmPacket, telemetryTemporaryBuffer, telemetryTemporaryBuffer.Length - remainingBytesToTmHeader, remainingBytesToTmHeader);
                                
                                int tmPacketSize = ((int)((((int)(telemetryTemporaryBuffer[4] << 8)) | ((int)(telemetryTemporaryBuffer[5])))));
                                numTmPacketBytes = (tmPacketSize + 6) + 1;  // adicionar 6 porque o packetLength possui o tamanho da area de dados e adicionar 1 porque o packetLength eh a area de dados - 1.
                                int remainingtmBytesToFillBuffer = numTmPacketBytes - telemetryTemporaryBuffer.Length;
                                
                                Array.Resize(ref telemetryTemporaryBuffer, telemetryTemporaryBuffer.Length + remainingtmBytesToFillBuffer);
                                Array.Copy(frame, (indexStartTmPacket + remainingBytesToTmHeader), telemetryTemporaryBuffer, (6 + 1), remainingtmBytesToFillBuffer);

                                numRemainingBytesToTemporaryTmBuffer = 0;
                                indexStartTmPacket = indexStartTmPacket + (remainingBytesToTmHeader + remainingtmBytesToFillBuffer) + 1;
                                
                                int tmVersionNumber = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                              (telemetryTemporaryBuffer[1] << 16) |
                                                              (telemetryTemporaryBuffer[2] << 8) |
                                                              (telemetryTemporaryBuffer[3]))
                                                              & 0xE0000000) >> 0x1D); // (Bits 0 through 2)

                                int tmType = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                     (telemetryTemporaryBuffer[1] << 16) |
                                                     (telemetryTemporaryBuffer[2] << 8) |
                                                     (telemetryTemporaryBuffer[3]))
                                                     & 0x10000000) >> 0x1C); // (Bit 3)
                                
                                int tmDataFieldHeaderFlag = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                                    (telemetryTemporaryBuffer[1] << 16) |
                                                                    (telemetryTemporaryBuffer[2] << 8) |
                                                                    (telemetryTemporaryBuffer[3]))
                                                                    & 0x08000000) >> 0x1B); // (Bit 4)
                                
                                int tmApplicationProcessId = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                                     (telemetryTemporaryBuffer[1] << 16) |
                                                                     (telemetryTemporaryBuffer[2] << 8) |
                                                                     (telemetryTemporaryBuffer[3]))
                                                                     & 0x07FF0000) >> 0x10); // (Bits 5 through 15)
                                
                                if (((tmVersionNumber == 0) || (tmVersionNumber == 2) || (tmVersionNumber == 4)) &&
                                    ((tmType == 0) || (tmType == 1)) && ((tmDataFieldHeaderFlag == 0) || (tmDataFieldHeaderFlag == 1)) &&
                                    ((tmApplicationProcessId == 0) || (tmApplicationProcessId == 1) || (tmApplicationProcessId == 2) ||
                                     (tmApplicationProcessId == 8) || (tmApplicationProcessId == 100) || (tmApplicationProcessId == 2046)))
                                {
                                    // Verifica o crc
                                    UInt16 crc = CheckingCodes.CrcCcitt16(ref telemetryTemporaryBuffer, telemetryTemporaryBuffer.Length - 2);
                                    bool crcOk = false;

                                    if ((telemetryTemporaryBuffer[telemetryTemporaryBuffer.Length - 2] == (byte)(crc >> 8)) &&
                                        (telemetryTemporaryBuffer[telemetryTemporaryBuffer.Length - 1] == (byte)(crc & 0xFF)))
                                    {
                                        crcOk = true;
                                    }

                                    reportPacket.RawContents = new byte[telemetryTemporaryBuffer.Length];
                                    reportPacket.RawContents = telemetryTemporaryBuffer;

                                    if (saveSession)
                                    {
                                        if (mdiMain.FormSimCortex == null)
                                        {
                                            session.LogPacket(reportPacket, false, !crcOk, connectionType);
                                        }
                                    }

                                    if (crcOk == true)
                                    {
                                        // avisa outras telas que ha TM para processar
                                        if (TelemetryReceived != null)
                                        {
                                            // Armazena o service Type e o Subtype
                                            int serviceType = (int)reportPacket.GetPart(56, 8);
                                            int serviceSubtype = (int)reportPacket.GetPart(64, 8);

                                            TelemetryEventArgs telemetryEventArgs = new TelemetryEventArgs(serviceType, serviceSubtype, reportPacket);
                                            TelemetryReceived(this, telemetryEventArgs);
                                        }
                                    }
                                }

                                coppingToTemporaryTmBuffer = false;
                                Array.Resize(ref telemetryTemporaryBuffer, 0);
                            }
                            else
                            {
                                if (telemetryTemporaryBuffer.Length >= (6 + 1))
                                {
                                    int tmPacketSize = ((int)((((int)(telemetryTemporaryBuffer[4] << 8)) | ((int)(telemetryTemporaryBuffer[5])))));
                                    numTmPacketBytes = (tmPacketSize + 6) + 1;  // adicionar 6 porque o packetLength possui o tamanho da area de dados e adicionar 1 porque o packetLength eh a area de dados - 1.
                                    int remainingtmBytesToFillBuffer = numTmPacketBytes - telemetryTemporaryBuffer.Length;
                                    int indexToContinueFillingTm = telemetryTemporaryBuffer.Length;
                                    
                                    Array.Resize(ref telemetryTemporaryBuffer, (telemetryTemporaryBuffer.Length + remainingtmBytesToFillBuffer));
                                    Array.Copy(frame, indexStartTmPacket, telemetryTemporaryBuffer, indexToContinueFillingTm, remainingtmBytesToFillBuffer);
                                    
                                    numRemainingBytesToTemporaryTmBuffer = 0;
                                    indexStartTmPacket = indexStartTmPacket + remainingtmBytesToFillBuffer;

                                    // verificar se este conjunto de bytes eh uma TM VALIDA
                                    int tmVersionNumber = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                                  (telemetryTemporaryBuffer[1] << 16) |
                                                                  (telemetryTemporaryBuffer[2] << 8) |
                                                                  (telemetryTemporaryBuffer[3]))
                                                                  & 0xE0000000) >> 0x1D); // (Bits 0 through 2)
                                    
                                    int tmType = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                         (telemetryTemporaryBuffer[1] << 16) |
                                                         (telemetryTemporaryBuffer[2] << 8) |
                                                         (telemetryTemporaryBuffer[3]))
                                                         & 0x10000000) >> 0x1C); // (Bit 3)
                                    
                                    int tmDataFieldHeaderFlag = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                                        (telemetryTemporaryBuffer[1] << 16) |
                                                                        (telemetryTemporaryBuffer[2] << 8) |
                                                                        (telemetryTemporaryBuffer[3]))
                                                                        & 0x08000000) >> 0x1B); // (Bit 4)
                                    
                                    int tmApplicationProcessId = (int)((((telemetryTemporaryBuffer[0] << 24) |
                                                                         (telemetryTemporaryBuffer[1] << 16) |
                                                                         (telemetryTemporaryBuffer[2] << 8) |
                                                                         (telemetryTemporaryBuffer[3]))
                                                                         & 0x07FF0000) >> 0x10); // (Bits 5 through 15)

                                    if (((tmVersionNumber == 0) || (tmVersionNumber == 2) || (tmVersionNumber == 4)) &&
                                        ((tmType == 0) || (tmType == 1)) && ((tmDataFieldHeaderFlag == 0) || (tmDataFieldHeaderFlag == 1)) &&
                                        ((tmApplicationProcessId == 0) || (tmApplicationProcessId == 1) || (tmApplicationProcessId == 2) ||
                                         (tmApplicationProcessId == 8) || (tmApplicationProcessId == 100) || (tmApplicationProcessId == 2046)))
                                    {
                                        // Verifica o crc
                                        UInt16 crc = CheckingCodes.CrcCcitt16(ref telemetryTemporaryBuffer, telemetryTemporaryBuffer.Length - 2);
                                        bool crcOk = false;

                                        if ((telemetryTemporaryBuffer[telemetryTemporaryBuffer.Length - 2] == (byte)(crc >> 8)) &&
                                            (telemetryTemporaryBuffer[telemetryTemporaryBuffer.Length - 1] == (byte)(crc & 0xFF)))
                                        {
                                            crcOk = true;
                                        }

                                        reportPacket.RawContents = new byte[telemetryTemporaryBuffer.Length];
                                        reportPacket.RawContents = telemetryTemporaryBuffer;

                                        if (saveSession)
                                        {
                                            if (mdiMain.FormSimCortex == null)
                                            {
                                                session.LogPacket(reportPacket, false, !crcOk, connectionType);
                                            }
                                        }

                                        if (crcOk == true)
                                        {
                                            // avisa outras telas que ha TM para processar
                                            if (TelemetryReceived != null)
                                            {
                                                // Armazena o service Type e o Subtype
                                                int serviceType = (int)reportPacket.GetPart(56, 8);
                                                int serviceSubtype = (int)reportPacket.GetPart(64, 8);

                                                TelemetryEventArgs telemetryEventArgs = new TelemetryEventArgs(serviceType, serviceSubtype, reportPacket);
                                                TelemetryReceived(this, telemetryEventArgs);
                                            }
                                        }
                                    }

                                    coppingToTemporaryTmBuffer = false;
                                    Array.Resize(ref telemetryTemporaryBuffer, 0);
                                }
                                else
                                {
                                    int indexToContinueFillingTm = telemetryTemporaryBuffer.Length;
                                    Array.Resize(ref telemetryTemporaryBuffer, (telemetryTemporaryBuffer.Length + numRemainingBytesToTemporaryTmBuffer));
                                    Array.Copy(frame, indexStartTmPacket, telemetryTemporaryBuffer, indexToContinueFillingTm, numRemainingBytesToTemporaryTmBuffer);

                                    if (numRemainingBytesToTemporaryTmBuffer >= (6 + 1)) // aqui verifico se ja tem como ver qual o tamanho do pacote no campo Packet Length... pois se ja tiver no minimo 6 bytes, ja posso saber quantos bytes no total essa tm tem.
                                    {
                                        coppingToTemporaryTmBuffer = false; // se nao for configurado pra false, significa que continuarei em busca dos proximos bytes desta tm
                                    }

                                    // atualizar o firstHeaderPointer para o inicio do proximo pacote... soma ao indice atual o numero de bytes que faltavam da tm.
                                    indexStartTmPacket = indexStartTmPacket + numRemainingBytesToTemporaryTmBuffer;
                                    numRemainingBytesToTemporaryTmBuffer = 0;
                                }
                            }
                        }
                        else
                        {
                            int firstTmVersionNumber = (int)((((frame[indexStartTmPacket] << 8) |
                                                               (frame[indexStartTmPacket + 1]))
                                                               & 0xE000) >> 0x0D); // (Bits 0 through 2)

                            int firstTmType = (int)((((frame[indexStartTmPacket] << 8) |
                                                      (frame[indexStartTmPacket + 1]))
                                                      & 0x1000) >> 0x0C); // (Bit 3)

                            int firstTmDataFieldHeaderFlag = (int)((((frame[indexStartTmPacket] << 8) |
                                                                     (frame[indexStartTmPacket + 1]))
                                                                     & 0x0800) >> 0x0B); // (Bit 4)

                            int firstTmApplicationProcessId = (int)((((frame[indexStartTmPacket] << 8) |
                                                                      (frame[indexStartTmPacket + 1]))
                                                                      & 0x07FF) >> 0x00); // (Bits 5 through 15)

                            // Verificar se eh um pacote de dados gerado pelo Fligth SW ou um pacote idle gerado pela UTMC.

                            if (((firstTmVersionNumber == 0) || (firstTmVersionNumber == 2) || (firstTmVersionNumber == 4)) &&
                                (firstTmType == 0) && ((firstTmDataFieldHeaderFlag == 1) || (firstTmDataFieldHeaderFlag == 0)) && // '1' tem Cabecalho do Campo de Dados '0' nao tem Cabecalho do Campo de Dados. Entao automaticamente, pacote idle e pacote sem dataField eh '0' e pacote com dataField eh '1'.
                                ((firstTmApplicationProcessId == 0) || (firstTmApplicationProcessId == 1) || (firstTmApplicationProcessId == 2) ||
                                 (firstTmApplicationProcessId == 8) || (firstTmApplicationProcessId == 100) || (firstTmApplicationProcessId == 2046)))
                            {
                                indexTmPacketSize = indexStartTmPacket + 4; // adicionar 4 porque eh a posicao do primeiro byte dos 16 bits do tamanho do pacote.
                                int tmPacketSize = ((int)((((int)(frame[indexTmPacketSize] << 8)) | ((int)(frame[indexTmPacketSize + 1])))));
                                numTmPacketBytes = (tmPacketSize + 6) + 1;  // adicionar 6 porque o packetLength possui o tamanho da area de dados e adicionar 1 porque o packetLength eh a area de dados - 1.
                                int remainingFrameDataFieldBytes = ((LFRAME - indexStartTmPacket) - (LTRAILER + LCRC));

                                if (remainingFrameDataFieldBytes < numTmPacketBytes)
                                {
                                    Array.Resize(ref telemetryTemporaryBuffer, remainingFrameDataFieldBytes);
                                    Array.Copy(frame, indexStartTmPacket, telemetryTemporaryBuffer, 0, remainingFrameDataFieldBytes);
                                    numRemainingBytesToTemporaryTmBuffer = numTmPacketBytes - remainingFrameDataFieldBytes;
                                    coppingToTemporaryTmBuffer = true;
                                }
                                else
                                {
                                    // obter a tm
                                    byte[] telemetry = new byte[numTmPacketBytes];
                                    Array.Copy(frame, indexStartTmPacket, telemetry, 0, numTmPacketBytes);
                                    
                                    // Verifica o crc
                                    UInt16 crc = CheckingCodes.CrcCcitt16(ref telemetry, telemetry.Length - 2);
                                    bool crcOk = false;

                                    if ((telemetry[telemetry.Length - 2] == (byte)(crc >> 8)) &&
                                        (telemetry[telemetry.Length - 1] == (byte)(crc & 0xFF)))
                                    {
                                        crcOk = true;
                                    }

                                    reportPacket.RawContents = new byte[telemetry.Length];
                                    reportPacket.RawContents = telemetry;

                                    if (saveSession)
                                    {
                                        if (mdiMain.FormSimCortex == null)
                                        {
                                            session.LogPacket(reportPacket, false, !crcOk, connectionType);
                                        }
                                    }

                                    if (crcOk == true)
                                    {
                                        // avisa outras telas que ha TM para processar
                                        if (TelemetryReceived != null)
                                        {
                                            // Armazena o service Type e o Subtype
                                            int serviceType = (int)reportPacket.GetPart(56, 8);
                                            int serviceSubtype = (int)reportPacket.GetPart(64, 8);

                                            TelemetryEventArgs telemetryEventArgs = new TelemetryEventArgs(serviceType, serviceSubtype, reportPacket);
                                            TelemetryReceived(this, telemetryEventArgs);
                                        }
                                    }
                                }
                            }
                            else if ((firstTmVersionNumber == 0) && (firstTmType == 0) &&
                                    (firstTmDataFieldHeaderFlag == 0) && (firstTmApplicationProcessId == 2047)) // applicationProcessId == 2047 significa um pacote idle
                            {
                                // se chegar aqui, quer dizer que o pacote eh pacote de TM Idle.
                                // copia-lo para que a rotina nao se perca, mas descarta-lo.
                                indexTmPacketSize = indexStartTmPacket + 4; // adicionar 4 porque eh a posicao do primeiro byte dos 16 bits do tamanho do pacote.
                                int tmPacketSize = ((int)((((int)(frame[indexTmPacketSize] << 8)) | ((int)(frame[indexTmPacketSize + 1])))));
                                numTmPacketBytes = (tmPacketSize + 6) + 1;  // adicionar 6 porque o packetLength possui o tamanho da area de dados e adicionar 1 porque o packetLength eh a area de dados - 1.
                                int remainingBytes = ((LFRAME - indexStartTmPacket) - (LTRAILER + LCRC));

                                if (remainingBytes < numTmPacketBytes)
                                {
                                    // se sim, quer dizer que faltam bytes. A tm eh quebrada
                                    Array.Resize(ref telemetryTemporaryBuffer, numTmPacketBytes - remainingBytes);
                                    Array.Copy(frame, indexStartTmPacket, telemetryTemporaryBuffer, 0, numTmPacketBytes - remainingBytes);

                                    coppingToTemporaryTmBuffer = true;
                                }
                                else
                                {
                                    // obter a tm
                                    byte[] telemetry = new byte[numTmPacketBytes];
                                    Array.Copy(frame, indexStartTmPacket, telemetry, 0, numTmPacketBytes);
                                }
                            }
                            else
                            {
                                // se cair aki.. significa que nao existem mais pacotes na area de dados 
                                // ou o restante de bytes esta assumindo qualquer valor, como o 0x55 ou lixo..
                                // o jeito eh parar a busca por pacotes e aguardar o proximo frame..
                                break;
                            }

                            // atualizar os indices
                            indexStartTmPacket = indexStartTmPacket + numTmPacketBytes;

                            if (indexStartTmPacket >= (LFRAME - (LTRAILER + LCRC)))
                            {
                                // se entrar nesse if, quer dizer que o ultimo pacote coletado chegou ao fim..
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Extract TM Error: " + e,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        
        private void AvailableReceivedTelemetry(object sender, TelemetryEventArgs eventArgs)
        {
            mdiMain.statusStrip.Items[0].Text = "Report packet received through TCP/IP network: " + eventArgs.RawPacket.GetString();
        }

        #endregion

        #region Timers

        /** 
         * Metodo disparado a cada intervalo de tempo definido pelo usuario
         * no 'tmrAskForTM-Tick para composicao e envio do TC
         **/
        private void tmrAskForTM_Tick(object sender, EventArgs e)
        {
            RawPacket rawPacket = new RawPacket(15, 128);
            SendRequest(rawPacket, SourceRequestPacket.OtherSource);
        }

        private void tmrUpdateTmFrame_Tick(object sender, EventArgs e)
        {
            if (tmFrameToShow == null)
            {
                // nenhum frame foi recebido
                return;
            }

            if (tmFrameToShowIsValid == false)
            {
                // se o frame recebido nao for valido, nao devo atualizar a exibicao com ele
                return;
            }

            RawFrame frame = new RawFrame(false);

            // copia tmFrameToShow para uma variavel local e zera
            while (usingTmFrameToShow == true)
            {
                // espera polada (por no maximo uma instrucao no tick do timer)
                Thread.Sleep(1); // apenas para dar chance ao timer de copiar tmFrameToShow
            }

            usingTmFrameToShow = true;
            frame.SetRawFrame(tmFrameToShow);
            tmFrameToShow = null;
            usingTmFrameToShow = false;

            int tmVcid = (int)frame.GetPart(12, 3);
            int tmGridLine = -1;

            int clcwVcid = (int)frame.GetPart(8880, 6);
            int clcwGridLine = -1;

            // determinar o VCID do frame, ver se ja ha no grid
            for (int i = 0; i < gridTmFrame.RowCount; i++)
            {
                if (int.Parse(gridTmFrame[2, i].Value.ToString()) == tmVcid)
                {
                    // ja existe um registro para esse vcid
                    tmGridLine = i;
                    break;
                }
            }

            if (tmGridLine < 0)
            {
                // VCID ainda nao recebido
                gridTmFrame.Rows.Add();
                tmGridLine = gridTmFrame.RowCount - 1;
            }

            gridTmFrame[0, tmGridLine].Value = frame.GetPart(0, 2);
            gridTmFrame[1, tmGridLine].Value = frame.GetPart(2, 10);
            gridTmFrame[2, tmGridLine].Value = frame.GetPart(12, 3);
            gridTmFrame[3, tmGridLine].Value = frame.GetPart(15, 1);
            gridTmFrame[4, tmGridLine].Value = frame.GetPart(16, 8);
            gridTmFrame[5, tmGridLine].Value = frame.GetPart(24, 8);
            gridTmFrame[6, tmGridLine].Value = frame.GetPart(32, 1);
            gridTmFrame[7, tmGridLine].Value = frame.GetPart(33, 1);
            gridTmFrame[8, tmGridLine].Value = frame.GetPart(34, 1);
            gridTmFrame[9, tmGridLine].Value = frame.GetPart(35, 2);
            gridTmFrame[10, tmGridLine].Value = frame.GetPart(37, 11);

            gridTmFrame[11, tmGridLine].Value = frame.GetString().Substring(3339, 5); // CRC

            if (tmFrameToShowIsValid == false)
            {
                foreach (DataGridViewCell cell in gridTmFrame.Rows[tmGridLine].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                }

                gridTmFrame[12, tmGridLine].Value = "WRONG!";
            }
            else
            {
                foreach (DataGridViewCell cell in gridTmFrame.Rows[tmGridLine].Cells)
                {
                    cell.Style.ForeColor = Color.Black;
                }

                gridTmFrame[12, tmGridLine].Value = "OK";
            }

            // determinar o VCID do frame, ver se ja ha no grid
            for (int i = 0; i < gridClcw.RowCount; i++)
            {
                if (int.Parse(gridClcw[4, i].Value.ToString()) == clcwVcid)
                {
                    // ja existe um registro para esse vcid
                    clcwGridLine = i;
                    break;
                }
            }

            if (clcwGridLine < 0)
            {
                // VCID ainda nao recebido
                gridClcw.Rows.Add();
                clcwGridLine = gridClcw.RowCount - 1;
            }

            gridClcw[0, clcwGridLine].Value = frame.GetPart(8872, 1);
            gridClcw[1, clcwGridLine].Value = frame.GetPart(8873, 2);
            gridClcw[2, clcwGridLine].Value = frame.GetPart(8875, 3);
            gridClcw[3, clcwGridLine].Value = frame.GetPart(8878, 2);
            gridClcw[4, clcwGridLine].Value = frame.GetPart(8880, 6);
            gridClcw[5, clcwGridLine].Value = frame.GetPart(8886, 2);
            gridClcw[6, clcwGridLine].Value = frame.GetPart(8888, 1);
            gridClcw[7, clcwGridLine].Value = frame.GetPart(8889, 1);
            gridClcw[8, clcwGridLine].Value = frame.GetPart(8890, 1);
            gridClcw[9, clcwGridLine].Value = frame.GetPart(8891, 1);
            gridClcw[10, clcwGridLine].Value = frame.GetPart(8892, 1);
            gridClcw[11, clcwGridLine].Value = frame.GetPart(8893, 2);
            gridClcw[12, clcwGridLine].Value = frame.GetPart(8895, 1);
            gridClcw[13, clcwGridLine].Value = frame.GetPart(8896, 8);
        }

        #endregion // Timers
    }

    #region Argumentos do evento TelemetryEvent

    public class TelemetryEventArgs : EventArgs
    {
        public int ServiceType;
        public int ServiceSubtype;
        public RawPacket RawPacket;

        public TelemetryEventArgs(int serviceType, int serviceSubtype,
            RawPacket rawPacket)
        {
            ServiceType = serviceType;
            ServiceSubtype = serviceSubtype;
            RawPacket = rawPacket;
        }
    }

    public delegate void TelemetryEventHandler(object sender, TelemetryEventArgs e);

    #endregion

    #region Argumentos do evento AvailableFrame

    /**
     * Esta classe eh usada para carregar os argumentos com seus valores para dentro do evento via delegate.
     * O delegate eh o intermediario que carrega os argumentos disponibilizados
     * atraves do evento. O evento disponibiliza os dados para o mundo externo (para quem quiser usar - qualquer tela por exemplo)
     * atraves de um delegate que contem a classe com os argumentos. Esta classe deve herdar as caracteristicas da classe EventArgs.
     **/
    public class AvailableFrameEventArgs : EventArgs
    {
        private byte[] frame;
        private bool frameVal;

        public byte[] Frame
        {
            set
            {
                frame = value;
            }
            get
            {
                return frame;
            }
        }

        public bool FrameValid
        {
            set
            {
                frameVal = value;
            }
            get
            {
                return frameVal;
            }
        }

        public AvailableFrameEventArgs()
        {
        }
    }

    public delegate void AvailableFrameEventHandler(object sender, AvailableFrameEventArgs e);

    #endregion

    #region Argumentos do evento AvailablePacket

    //@attention: Este evento nomeado para Packets permite receber qualquer tipo de mensagem (qq conjunto de bytes) com no minimo 18 bytes.
    public class AvailablePacketEventArgs : EventArgs
    {
        private byte[] packet;
        private int numBytes;

        public byte[] Packet
        {
            set
            {
                packet = value;
            }
            get
            {
                return packet;
            }
        }

        public int NumBytes
        {
            set
            {
                numBytes = value;
            }
            get
            {
                return numBytes;
            }
        }

        public AvailablePacketEventArgs()
        {
        }
    }

    public delegate void AvailablePacketEventHandler(object sender, AvailablePacketEventArgs e);

    #endregion
}
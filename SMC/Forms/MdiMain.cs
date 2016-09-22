/**
 * @file 	    MdiMain.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 25/11/2014 por Thiago.
 * @note	    Modificado em 02/03/2015 por Conrado e Thiago.
 * @note	    Modificado em 02/03/2015 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Inpe.Subord.Comav.Egse.Smc.Database;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Properties;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class MdiMain
     * Formulario principal do SMC, de onde se acessam todas as telas.
     **/
    public partial class MdiMain : Form
    {
        #region Atributos

        private int currentPerspectiveIndex = -1;
        private bool dontCallLoadLayout = false;
        private bool cancellingPerspectiveChange = false;
        private FrmConnectionWithEgse frmConnection = null;
        private FrmRequestsComposition frmRequestsComposition = null;
        private FrmEventsDetectionList frmEventsDetectionList = null;
        private FrmPacketsStorageStatusMonitoring frmPacketStorage = null;
        private FrmSavedRequests frmSavedRequest = null;
        private FrmFramesCoding frmFramesCoding = null;
        private FrmSimCortex frmSimCortex = null;
        private FrmCommRS422 frmCommRS422 = null;
        private FrmTestProceduresComposition frmTestProceduresComposition = null;
        private FrmSessionsLog frmSessionsLog = null;
        private FrmTestProcedureExecution frmProcExecution = null;
        private DbConfiguration dbconfiguration = new DbConfiguration();
        private bool offlineModeMdi = false;
        private FrmViewerSetup frmViewerSetup = null;
        private FrmCortexCOP1Configuration frmCortexCOPConfig = null;

        #endregion

        #region Codigo de Inicializacao

        /** Inicializa o formulario e chama o splash por alguns segundos. **/
        public MdiMain()
        {
            InitializeComponent();

            // exibe o splash antes do mdi
            FrmSplash frmSplash = new FrmSplash();

            if (frmSplash.ShowDialog(this) == DialogResult.OK)
            {
                offlineModeMdi = frmSplash.offlineMode;
                frmSplash.Refresh();

                //verifica se esta em modo offine
                if (!offlineModeMdi)
                {
                    if (!DbInterface.TestConnection())
                    {
                        MessageBox.Show("Failed to connect to the database!\n\nPlease check your connection file and file path.",
                                        "Database access error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        // chama a tela de configuracoes
                        //configurationToolStripMenuItem_Click(this, new EventArgs());
                        FrmConfiguration frm = new FrmConfiguration();
                        frm.ShowDialog();
                    }
                    else
                    {
                        statusStrip.Items[0].Text = "Database test connection successfull.";
                        RefreshBackground();
                    }
                }

                frmSplash.Close();
                frmSplash.Dispose();
            }
            if (!offlineModeMdi)
            {
                Refresh_Cmb();
            }
            else
            {
                configurationToolStripMenuItem.Enabled = false;
                databaseToolStripMenuItem1.Enabled = false;
                toolStripMenuItem5.Enabled = false;
                functionalTestingToolStripMenuItem.Enabled = false;
                testingHistoryToolStripMenuItem.Enabled = false;
                inFlightSWUpdateToolStripMenuItem.Enabled = false;
                toolSimulation.Enabled = false;
                saveCurrentLayoutToolStripMenuItem.Enabled = false;
                loadLayoutToolStripMenuItem.Enabled = false;
                toolStripCmbSelectDb.Enabled = false;
                cmbPerspective.Enabled = false;

                statusStrip.Items[0].Text = "SMC started in Offline Mode";
            }
        }

        #endregion

        #region Propriedades

        /**Retorna o objeto frmConnection para ser usado na tela de TcsComposition**/
        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            get
            {
                return frmConnection;
            }
        }

        /**Retorna o objeto frmTcsComposition para ser usado na tela de ConnectionWithEgse**/
        public FrmRequestsComposition FormRequestsComposition
        {
            get
            {
                return frmRequestsComposition;
            }
            set
            {
                frmRequestsComposition = value;
            }
        }

        public FrmEventsDetectionList FormEventsDetectionList
        {
            get
            {
                return frmEventsDetectionList;
            }
        }

        public FrmPacketsStorageStatusMonitoring FormPacketStorage
        {
            get
            {
                return frmPacketStorage;
            }
        }

        public FrmSavedRequests FormSavedRequests
        {
            get
            {
                return frmSavedRequest;
            }
        }

        public FrmFramesCoding FormFramesCoding
        {
            get
            {
                return frmFramesCoding;
            }
        }

        public FrmCommRS422 FormCommRs422
        {
            get
            {
                return frmCommRS422;
            }
        }

        public FrmCortexCOP1Configuration FormCortexCOP1Configuration
        {
            get
            {
                return frmCortexCOPConfig;
            }
        }

        public FrmSimCortex FormSimCortex
        {
            get
            {
                return frmSimCortex;
            }
        }

        public FrmTestProceduresComposition FormTestProceduresComposition
        {
            get
            {
                return frmTestProceduresComposition;
            }
        }

        public FrmTestProcedureExecution FormTestProceduresExecution
        {
            get
            {
                return frmProcExecution;
            }
            set
            {
                frmProcExecution = value;
            }
        }

        public FrmSessionsLog FormSessionsLog
        {
            get
            {
                return frmSessionsLog;
            }
            set
            {
                frmSessionsLog = value;
            }
        }

        public DockPanel DockPanel
        {
            get
            {
                return dockPanel;
            }
        }

        #endregion

        #region Tratamento de Eventos da Interface Grafica

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void servicesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Services"))
            {
                return;
            }

            FrmSimpleTable frm = new FrmSimpleTable("Services",
                                                   "Service Type",
                                                    "Service Name",
                                                    "services",
                                                    "service_type",
                                                    "service_name",
                                                    8,
                                                    100);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void applicationIDsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Application IDs"))
            {
                return;
            }

            FrmApids frm = new FrmApids();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void reportIDsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void packetStoreIDsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Packet Store IDs"))
            {
                return;
            }

            FrmSimpleTable frm = new FrmSimpleTable("Packet Store IDs",
                                                    "Packet Store ID",
                                                    "Packet Store Name",
                                                    "packet_store_ids",
                                                    "store_id",
                                                    "packet_store_name",
                                                    8,
                                                    100);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void memoryIDsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Memory IDs"))
            {
                return;
            }

            FrmSimpleTable frm = new FrmSimpleTable("Memory IDs",
                                                    "Memory ID",
                                                    "Memory Unit Description",
                                                    "memory_ids",
                                                    "memory_id",
                                                    "memory_unit_description",
                                                    8,
                                                    100);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void tCFailureCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void subtypesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Subtypes"))
            {
                return;
            }

            FrmSubtypes frm = new FrmSubtypes();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("System Configuration"))
            {
                return;
            }
            
            FrmConfiguration frm = new FrmConfiguration(this);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Database Queries"))
            {
                return;
            }

            FrmQueries frm = new FrmQueries();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void testSessionsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Test Sessions Log"))
            {
                return;
            }

            frmSessionsLog = new FrmSessionsLog();
            frmSessionsLog.MdiParent = this;
            frmSessionsLog.Show(dockPanel);
        }

        private void tcsCompositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Este if foi adicionado para tratar a chamada duplicada do FrmRequestComposition.
            // Ao chamar o FrmRequestComposition pela SavedRequest, o if FormIsOpen nao funciona porque para esse caso o Form nao eh adicionado ao MdiChildren.
            if ((frmRequestsComposition != null) && frmRequestsComposition.CalledFromSavedRequests)
            {
                if (MessageBox.Show("The 'Request Composition' is open!\n\nAre you sure you want close and open it again?",
                                        Application.ProductName,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    frmRequestsComposition.Close();
                    frmRequestsComposition = null;
                }
            }

            if (FormIsOpen("Requests Composition"))
            {
                return;
            }

            frmRequestsComposition = new FrmRequestsComposition(this);
            frmRequestsComposition.MdiParent = this;
            frmRequestsComposition.Show(dockPanel);
        }

        private void connectionWithEGSEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (FormIsOpen("Connection with COMAV"))
            {
                return;
            }

            frmConnection = new FrmConnectionWithEgse(this, offlineModeMdi);
            frmConnection.MdiParent = this;
            frmConnection.Show(dockPanel);
        }

        private void framesEncoderDecoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Frames Encoder / Decoder"))
            {
                return;
            }

            frmFramesCoding = new FrmFramesCoding(this);
            frmFramesCoding.MdiParent = this;

            if (frmConnection != null)
            {
                frmConnection.FormFramesCoding = frmFramesCoding;
            }

            frmFramesCoding.Show(dockPanel);
        }

        private void dataFieldsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Data Fields"))
            {
                return;
            }

            FrmDataFields frm = new FrmDataFields(null);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void getCortexCOP1ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Cortex/COP1 Configuration"))
            {
                return;
            }

            frmCortexCOPConfig = new FrmCortexCOP1Configuration(this);
            frmCortexCOPConfig.MdiParent = this;

            if (frmConnection != null)
            {
                frmConnection.FormCortexCOP1Config = frmCortexCOPConfig;
            }

            frmCortexCOPConfig.Show(dockPanel);
        }

        private void saveCurrentLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = Properties.Settings.Default.layouts_default_path;
            fileDialog.Title = "Save Layout File";
            fileDialog.Filter = "Layout File (*.xml)|*.xml|All Files (*.*)|*.*";
            fileDialog.FileName = "*.xml";
            fileDialog.FilterIndex = 0;

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dockPanel.SaveAsXml(fileDialog.FileName);
        }

        private void loadLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Properties.Settings.Default.layouts_default_path;
            fileDialog.Title = "Open Layout File";
            fileDialog.Filter = "Layout File (*.xml)|*.xml|All Files (*.*)|*.*";
            fileDialog.FileName = "*.xml";
            fileDialog.FilterIndex = 0;

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                // mantem a perspectiva atual
                cancellingPerspectiveChange = true;
                cmbPerspective.SelectedIndex = currentPerspectiveIndex;
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            CloseAllDocuments();

            dockPanel.LoadFromXml(fileDialog.FileName, GetContentFromPersistString);
            statusStrip.Items[0].Text = "Layout loaded successfully.";

            currentPerspectiveIndex = cmbPerspective.SelectedIndex;
            dontCallLoadLayout = true;
            cancellingPerspectiveChange = true;
            
            this.Cursor = Cursors.Default;
        }

        private void toolStripLockLayout_Click(object sender, EventArgs e)
        {
            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
        }

        /** Alterna a perspectiva de acordo com a selecao do usuario. **/
        private void cmbPerspective_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cancellingPerspectiveChange)
            {
                cancellingPerspectiveChange = false;
                return;
            }

            if (MdiChildren.Count() > 0)
            {
                if (MessageBox.Show("Are you sure you want to change the current perspective?\n\n" +
                                    "If you choose 'Yes', all windows will be closed.",
                                    "Change Perspective",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    cancellingPerspectiveChange = true;
                    cmbPerspective.SelectedIndex = currentPerspectiveIndex;
                    return;
                }
            }

            this.Cursor = Cursors.WaitCursor;

            CloseAllDocuments();

            switch (cmbPerspective.SelectedIndex)
            {
                case 0: // Database Management
                    {
                        //Instancia todos os Forms                
                        FrmSimpleTable frmServices = new FrmSimpleTable("Services",
                                                            "Service Type",
                                                             "Service Name",
                                                             "services",
                                                             "service_type",
                                                             "service_name",
                                                             8,
                                                             100);


                        FrmSimpleTable frmPacketStorage = new FrmSimpleTable("Packet Store IDs",
                                                           "Packet Store ID",
                                                           "Packet Store Name",
                                                           "packet_store_ids",
                                                           "store_id",
                                                           "packet_store_name",
                                                           8,
                                                           100);


                        FrmSubtypes frmSubtypes = new FrmSubtypes();
                        FrmDataFields frmDataFields = new FrmDataFields(null);
                        FrmMissionConstants frmMissionConstants = new FrmMissionConstants();
                        FrmHousekeepingParameters frmHouseKeepingParameters = new FrmHousekeepingParameters(null);

                        FrmApids frmApplicationIds = new FrmApids();

                        FrmSimpleTable frmMemoryIds = new FrmSimpleTable("Memory IDs",
                                                           "Memory ID",
                                                           "Memory Unit Description",
                                                           "memory_ids",
                                                           "memory_id",
                                                           "memory_unit_description",
                                                           8,
                                                           100);

                        FrmMissionIdsWithStructure frmTcFailureCodes = new FrmMissionIdsWithStructure("TC Failure Codes",
                                                                  "Failure Code",
                                                                  "Failure Description",
                                                                  "tc_failure_codes",
                                                                  "tc_failure_code",
                                                                  "tc_failure_description",
                                                                  "tc_failure_code_structure",
                                                                  "tc_failure_code",
                                                                  "data_field_id",
                                                                  "data_fields",
                                                                  "data_field_name",
                                                                  8,
                                                                  100);

                        FrmMissionIdsWithStructure frmReportIds = new FrmMissionIdsWithStructure("Event and Error Reports",
                                                                  "RID",
                                                                  "Description",
                                                                  "rids",
                                                                  "rid",
                                                                  "description",
                                                                  "event_report_structure",
                                                                  "rid",
                                                                  "data_field_id",
                                                                  "data_fields",
                                                                  "data_field_name",
                                                                  8,
                                                                  100);

                        FrmMissionIdsWithStructure frmReportDefinitions = new FrmMissionIdsWithStructure("Report Definitions",
                                                                  "Structure ID",
                                                                  "Definition Descript.",
                                                                  "report_definitions",
                                                                  "structure_id",
                                                                  "report_definition_description",
                                                                  "report_definition_structure",
                                                                  "structure_id",
                                                                  "parameter_id",
                                                                  "parameters",
                                                                  "parameter_description",
                                                                  16,
                                                                  200);

                        FrmSimpleTable frmOutputLineIds = new FrmSimpleTable("Output Lines IDs",
                                                                 "Output Line ID",
                                                                 "Line Description",
                                                                 "output_line_ids",
                                                                 "output_line_id",
                                                                 "output_line_description",
                                                                 8,
                                                                 100);

                        FrmQueries frmQueries = new FrmQueries();

                        FrmDataFieldsLists frmDataFieldsLists = new FrmDataFieldsLists(this);

                        frmHouseKeepingParameters.Show(dockPanel, DockState.Document);
                        frmReportDefinitions.Show(frmHouseKeepingParameters.Pane, frmHouseKeepingParameters);
                        frmReportIds.Show(frmHouseKeepingParameters.Pane, frmReportDefinitions);
                        frmTcFailureCodes.Show(frmHouseKeepingParameters.Pane, frmReportIds);
                        frmDataFieldsLists.Show(frmHouseKeepingParameters.Pane, frmTcFailureCodes);
                        frmDataFields.Show(frmHouseKeepingParameters.Pane, frmDataFieldsLists);
                        frmSubtypes.Show(frmHouseKeepingParameters.Pane, frmDataFields);

                        frmMissionConstants.Show(frmReportIds.Pane, DockAlignment.Bottom, 0.5);
                        frmQueries.Show(frmMissionConstants.Pane, frmMissionConstants);

                        frmServices.Show(dockPanel, DockState.DockRight);

                        frmOutputLineIds.Show(frmServices.Pane, DockAlignment.Bottom, 0.5);
                        frmPacketStorage.Show(frmOutputLineIds.Pane, frmOutputLineIds);
                        frmMemoryIds.Show(frmPacketStorage.Pane, frmPacketStorage);
                        frmApplicationIds.Show(frmPacketStorage.Pane, frmMemoryIds);

                        break;
                    }
                case 1: // Testing UPC
                    {
                        frmRequestsComposition = new FrmRequestsComposition(this);
                        frmConnection = new FrmConnectionWithEgse(this, offlineModeMdi);
                        frmSessionsLog = new FrmSessionsLog();
                        FrmTimeConversion frmTimeConversion = new FrmTimeConversion();
                        FrmCodingCheck frmCodingCheck = new FrmCodingCheck();

                        frmConnection.Show(dockPanel, DockState.Document);
                        frmSessionsLog.Show(dockPanel, DockState.Document);
                        frmTimeConversion.Show(dockPanel, DockState.DockRightAutoHide);
                        frmCodingCheck.Show(dockPanel, DockState.DockRightAutoHide);
                        frmRequestsComposition.Show(dockPanel, DockState.Document);
                        dockPanel.Contents[0].DockHandler.Form.Focus();

                        break;
                    }
                case 2: // COMAV Monitoring
                    {
                        frmConnection = new FrmConnectionWithEgse(this, offlineModeMdi);
                        frmConnection.MdiParent = this;

                        frmPacketStorage = new FrmPacketsStorageStatusMonitoring(this);
                        frmPacketStorage.MdiParent = this;
                        frmConnection.FormPacketsStorage = frmPacketStorage;

                        frmEventsDetectionList = new FrmEventsDetectionList(this);
                        frmEventsDetectionList.MdiParent = this;
                        frmConnection.FormEventsDetectionList = frmEventsDetectionList;

                        frmConnection.Show(dockPanel, DockState.Document);
                        frmPacketStorage.Show(dockPanel, DockState.Document);
                        frmEventsDetectionList.Show(dockPanel, DockState.Document);

                        dockPanel.Contents[0].DockHandler.Form.Focus();

                        break;
                    }
                case 3: // Testing UTMC
                    {
                        frmConnection = new FrmConnectionWithEgse(this, offlineModeMdi);
                        frmConnection.MdiParent = this;
                        frmFramesCoding = new FrmFramesCoding(this);
                        FrmTimeConversion frmTimeConversion = new FrmTimeConversion();
                        FrmCodingCheck frmCodingCheck = new FrmCodingCheck();
                        frmCommRS422 = new FrmCommRS422(this);
                        frmCommRS422.MdiParent = this;
                        frmConnection.FormCommRs422 = frmCommRS422;

                        frmConnection.Show(dockPanel, DockState.Document);
                        frmFramesCoding.Show(dockPanel, DockState.Document);
                        frmCommRS422.Show(frmFramesCoding.Pane, frmFramesCoding);
                        frmTimeConversion.Show(dockPanel, DockState.DockRightAutoHide);
                        frmCodingCheck.Show(dockPanel, DockState.DockRightAutoHide);
                        dockPanel.Panes[0].Contents[0].DockHandler.Form.Focus();

                        break;
                    }
                case 4: //Test Procedures
                    {
                        frmConnection = new FrmConnectionWithEgse(this, offlineModeMdi);
                        frmConnection.MdiParent = this;

                        frmSavedRequest = new FrmSavedRequests(this);
                        frmTestProceduresComposition = new FrmTestProceduresComposition(this);

                        frmConnection.Show(dockPanel, DockState.Document);
                        frmSavedRequest.Show(dockPanel, DockState.Document);
                        frmTestProceduresComposition.Show(dockPanel, DockState.Document);

                        frmProcExecution = new FrmTestProcedureExecution(0, this);
                        //frmProcExecution.MdiParent = this;
                        frmProcExecution.Show(dockPanel, DockState.Document);

                        frmSessionsLog = new FrmSessionsLog();
                        frmSessionsLog.MdiParent = this;
                        frmSessionsLog.Show(frmConnection.Pane, DockAlignment.Bottom, 0.48);
                        frmSessionsLog.OpenedByTestProceduresComposition = false;
                        dockPanel.Panes[0].Contents[0].DockHandler.Form.Focus();

                        break;
                    }
                case 5: //User-Defined
                    {
                        if (!dontCallLoadLayout)
                        {
                            loadLayoutToolStripMenuItem_Click(this, new EventArgs());
                        }

                        dontCallLoadLayout = false;

                        break;
                    }
            }

            currentPerspectiveIndex = cmbPerspective.SelectedIndex;
            this.Cursor = Cursors.Default;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frm = new FrmAbout();
            frm.ShowDialog();
        }

        private void timeFormatConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Time Format Conversion"))
            {
                return;
            }

            FrmTimeConversion frm = new FrmTimeConversion();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void codingCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Coding Check"))
            {
                return;
            }

            FrmCodingCheck frm = new FrmCodingCheck();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Mission Constants"))
            {
                return;
            }

            FrmMissionConstants frm = new FrmMissionConstants();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void MdiMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit SMC?",
                                "Please Confirm Exit",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void reportDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Report Definitions"))
            {
                return;
            }

            FrmMissionIdsWithStructure frm = new FrmMissionIdsWithStructure("Report Definitions",
                                                              "Structure ID",
                                                              "Definition Descript.",
                                                              "report_definitions",
                                                              "structure_id",
                                                              "report_definition_description",
                                                              "report_definition_structure",
                                                              "structure_id",
                                                              "parameter_id",
                                                              "parameters",
                                                              "parameter_description",
                                                              16,
                                                              200);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void eventAndErrorReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Event and Error Reports"))
            {
                return;
            }

            FrmMissionIdsWithStructure frm = new FrmMissionIdsWithStructure("Event and Error Reports",
                                                              "RID",
                                                              "Description",
                                                              "rids",
                                                              "rid",
                                                              "description",
                                                              "event_report_structure",
                                                              "rid",
                                                              "data_field_id",
                                                              "data_fields",
                                                              "data_field_name",
                                                              8,
                                                              100);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void tCFailureCodesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("TC Failure Codes"))
            {
                return;
            }

            FrmMissionIdsWithStructure frm = new FrmMissionIdsWithStructure("TC Failure Codes",
                                                                  "Failure Code",
                                                                  "Failure Description",
                                                                  "tc_failure_codes",
                                                                  "tc_failure_code",
                                                                  "tc_failure_description",
                                                                  "tc_failure_code_structure",
                                                                  "tc_failure_code",
                                                                  "data_field_id",
                                                                  "data_fields",
                                                                  "data_field_name",
                                                                  8,
                                                                  100);

            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void MdiMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MdiMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "F2": // Mission Database
                {
                    cmbPerspective.SelectedIndex = 0;
                    break;
                }
                case "F3": // Testing UPC
                {
                    cmbPerspective.SelectedIndex = 1;
                    break;
                }
                case "F4": // COMAV Monitoring
                {
                    cmbPerspective.SelectedIndex = 2;
                    break;
                }
                case "F6": // Testing UTMC
                {
                    cmbPerspective.SelectedIndex = 3;
                    break;
                }
                case "F7": // Test Procedures
                {
                    cmbPerspective.SelectedIndex = 4;
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void eventsDetectionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Events Detection List"))
            {
                return;
            }

            frmEventsDetectionList = new FrmEventsDetectionList(this);
            frmEventsDetectionList.MdiParent = this;
            frmEventsDetectionList.Show(dockPanel);
        }

        private void savedRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Saved Requests"))
            {
                return;
            }

            frmSavedRequest = new FrmSavedRequests(this);
            frmSavedRequest.MdiParent = this;
            frmSavedRequest.Show(dockPanel);
        }

        private void housekeeping2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Housekeeping and Diagnostic Parameters"))
            {
                return;
            }

            FrmHousekeepingParameters frm = new FrmHousekeepingParameters(null);

            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void packetsStorageStatusMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Packets Storage Status Monitoring"))
            {
                return;
            }

            frmPacketStorage = new FrmPacketsStorageStatusMonitoring(this);

            frmPacketStorage.MdiParent = this;
            frmPacketStorage.Show(dockPanel);
        }

        private void missionIDsAndConstantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Mission IDs and Constants"))
            {
                return;
            }

            FrmSimpleTable frm = new FrmSimpleTable("Output Lines IDs",
                                                    "Output Line ID",
                                                    "Line Description",
                                                    "output_line_ids",
                                                    "output_line_id",
                                                    "output_line_description",
                                                    8,
                                                    100);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void testProceduresCompositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Test Procedures Composition"))
            {
                return;
            }

            frmTestProceduresComposition = new FrmTestProceduresComposition(this);
            frmTestProceduresComposition.MdiParent = this;
            frmTestProceduresComposition.Show(dockPanel);
        }

        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("RS-422 Communication"))
            {
                return;
            }

            frmCommRS422 = new FrmCommRS422(this);
            frmCommRS422.MdiParent = this;

            if (frmConnection != null)
            {
                frmConnection.FormCommRs422 = frmCommRS422;
            }

            frmCommRS422.Show(dockPanel);
        }

        private void dataFieldsListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Data Fields Lists"))
            {
                return;
            }

            FrmDataFieldsLists frm = new FrmDataFieldsLists(this);
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void inFlightSWUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("In-Flight Software Update"))
            {
                return;
            }

            FrmSwUpdate frm = new FrmSwUpdate();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void simulatorsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Simulators Management"))
            {
                return;
            }

            FrmSimulatorsManagement frm = new FrmSimulatorsManagement();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void SetComavToDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Set COMAV to Debug Mode"))
            {
                return;
            }

            FrmSetComavToDebug frm = new FrmSetComavToDebug();
            frm.offlineMode = offlineModeMdi;
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void hexToASCIIConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Hex to ASCII Converter"))
            {
                return;
            }

            FrmHexToAsciiConverter frm = new FrmHexToAsciiConverter();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void loadFlightSoftwareToEEPROMCOMAVPrototypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Load Fight Software to EEPROM (COMAV prototype)"))
            {
                return;
            }
            FrmLoadFlightSoftEeprom frm = new FrmLoadFlightSoftEeprom();
            frm.offlineMode = offlineModeMdi;
            frm.MdiParent = this;
            frm.Show(dockPanel);       
        }

        private void loadFlightSoftwareToUATCOMAVPrototypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("UAT Monitor (COMAV prototype)"))
            {
                return;
            }
            FrmUatMonitor frm = new FrmUatMonitor();
            frm.offlineMode = offlineModeMdi;
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void simulatorsExecutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Simulators Execution"))
            {
                return;
            }

            FrmSimulatorsExecution frm = new FrmSimulatorsExecution();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }
        
        private void cortexSimulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Cortex Simulator"))
            {
                return;
            }

            frmSimCortex = new FrmSimCortex(this);
            frmSimCortex.MdiParent = this;
            frmSimCortex.Show(dockPanel);
        }

        private void sTITerminalCOMAVPrototypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("STI Terminal (COMAV prototype)"))
            {
                return;
            }
            FrmStiTerminal frm = new FrmStiTerminal();
            frm.offlineMode = offlineModeMdi;
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        private void hkParametersViewersSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Housekeeping Parameters Viewers Setup"))
            {
                return;
            }

            frmViewerSetup = new FrmViewerSetup();
            frmViewerSetup.MdiParent = this;
            frmViewerSetup.Show(dockPanel);
        }

        private void hkParametersViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Housekeeping Parameters Viewers Selection"))
            {
                return;
            }

            FrmViewsSelection frm = new FrmViewsSelection(this);
            this.TopMost = false;
            frm.ShowDialog();
            this.TopMost = true;
        }

        private void clearTestSessionsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Clear Test Sessions Log"))
            {
                return;
            }

            FrmClearTestSessionsLog frmClearTestSessionsLog = new FrmClearTestSessionsLog();
            frmClearTestSessionsLog.ShowDialog();
        }

        private void generateShellCommandsToEEPROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Generate Shell Command"))
            {
                return;
            }

            FrmGeneratingShellCommand frm = new FrmGeneratingShellCommand();
            frm.MdiParent = this;
            frm.Show(dockPanel);
        }

        #endregion

        #region Metodos Privados

        /**
         * Verifica se o formulario ja esta aberto. Se estiver, lhe da o foco.
         **/
        public bool FormIsOpen(string formTitle)
        {
            foreach (Form child in MdiChildren)
            {
                if (child.Text == formTitle)
                {
                    child.Focus();
                    return true;
                }
            }

            return false;
        }

        /**
         * Fecha todas as janelas da Perspectiva que esta aberta.
         */
        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                for (int index = dockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (dockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = (IDockContent)dockPanel.Contents[index];
                        content.DockHandler.Close();
                    }
                }
            }
        }

        /**
         * Este metodo eh usado pelo Docking para instanciar os formularios ao carregar um layout.
         **/
        private IDockContent GetContentFromPersistString(string persistString, string formText)
        {
            FrmConfiguration frmConfiguration = new FrmConfiguration(this);
            FrmDataFields frmDataFields = new FrmDataFields(null);
            FrmDataFieldsLists frmDataFieldsLists = new FrmDataFieldsLists(null);
            FrmFramesCoding frmFramesCoding = new FrmFramesCoding(this);
            FrmQueries frmQueries = new FrmQueries();
            FrmSessionsLog frmSessionsLog = new FrmSessionsLog();
            FrmSimpleTable frmSimpleTable = new FrmSimpleTable();
            FrmSubtypes frmSubtypes = new FrmSubtypes();
            frmConnection = new FrmConnectionWithEgse(this,offlineModeMdi);
            frmRequestsComposition = new FrmRequestsComposition(this);
            FrmTimeConversion frmTimeConversion = new FrmTimeConversion();
            FrmCodingCheck frmCodingCheck = new FrmCodingCheck();
            FrmMissionConstants frmMissionConstants = new FrmMissionConstants();
            FrmMissionIdsWithStructure frmMissionIdsWithStructure = new FrmMissionIdsWithStructure();
            FrmEventsDetectionList frmEventsDetectionList = new FrmEventsDetectionList(this);
            FrmHousekeepingParameters frmHouseKeepingParameters = new FrmHousekeepingParameters(null);
            FrmPacketsStorageStatusMonitoring frmPacketsStorage = new FrmPacketsStorageStatusMonitoring(this);
            FrmApids frmApids = new FrmApids();
            FrmSetComavToDebug frmSetComavToDebug = new FrmSetComavToDebug();
            FrmTestProceduresComposition frmTestProceduresComposition = new FrmTestProceduresComposition(this);
            FrmSwUpdate frmSwUpdate = new FrmSwUpdate();
            FrmLoadFlightSoftEeprom frmLoadFlightSoftEeprom = new FrmLoadFlightSoftEeprom();
            FrmUatMonitor frmUatMonitor = new FrmUatMonitor();
            FrmStiTerminal frmStiTerminal = new FrmStiTerminal();
            FrmSimulatorsManagement frmSimulatorsManagement = new FrmSimulatorsManagement();
            FrmSimulatorsExecution frmSimulatorsExecution = new FrmSimulatorsExecution();
            FrmHexToAsciiConverter frmHexToAsciiConverter = new FrmHexToAsciiConverter();
            FrmCommRS422 frmCommRS422 = new FrmCommRS422(this);
            FrmSimCortex frmSimCortex = new FrmSimCortex(this);

            if (persistString.Equals(typeof(FrmConfiguration).ToString()))
            {
                return frmConfiguration;
            }
            else if (persistString.Equals(typeof(FrmDataFields).ToString()))
            {
                return frmDataFields;
            }
            else if (persistString.Equals(typeof(FrmDataFieldsLists).ToString()))
            {
                return frmDataFieldsLists;
            }
            else if (persistString.Equals(typeof(FrmFramesCoding).ToString()))
            {
                return frmFramesCoding;
            }
            else if (persistString.Equals(typeof(FrmQueries).ToString()))
            {
                return frmQueries;
            }
            else if (persistString.Equals(typeof(FrmSessionsLog).ToString()))
            {
                return frmSessionsLog;
            }
            else if (persistString.Equals(typeof(FrmSubtypes).ToString()))
            {
                return frmSubtypes;
            }
            else if (persistString.Equals(typeof(FrmRequestsComposition).ToString()))
            {
                return frmRequestsComposition;
            }
            else if (persistString.Equals(typeof(FrmTimeConversion).ToString()))
            {
                return frmTimeConversion;
            }
            else if (persistString.Equals(typeof(FrmCodingCheck).ToString()))
            {
                return frmCodingCheck;
            }
            else if (persistString.Equals(typeof(FrmConnectionWithEgse).ToString()))
            {
                return frmConnection;
            }
            else if (persistString.Equals(typeof(FrmEventsDetectionList).ToString()))
            {
                return frmEventsDetectionList;
            }
            else if (persistString.Equals(typeof(FrmPacketsStorageStatusMonitoring).ToString()))
            {
                return frmPacketsStorage;
            }
            else if (persistString.Equals(typeof(FrmHousekeepingParameters).ToString()))
            {
                return frmHouseKeepingParameters;
            }
            else if (persistString.Equals(typeof(FrmSetComavToDebug).ToString()))
            {
                return frmSetComavToDebug;
            }
            else if (persistString.Equals(typeof(FrmTestProceduresComposition).ToString()))
            {
                return frmTestProceduresComposition;
            }
            else if (persistString.Equals(typeof(FrmSwUpdate).ToString()))
            {
                return frmSwUpdate;
            }
            else if (persistString.Equals(typeof(FrmLoadFlightSoftEeprom).ToString()))
            {
                return frmLoadFlightSoftEeprom;
            }
            else if (persistString.Equals(typeof(FrmUatMonitor).ToString()))
            {
                return frmUatMonitor;
            }
            else if (persistString.Equals(typeof(FrmStiTerminal).ToString()))
            {
                return frmStiTerminal;
            }
            else if (persistString.Equals(typeof(FrmSimulatorsManagement).ToString()))
            {
                return frmSimulatorsManagement;
            }
            else if (persistString.Equals(typeof(FrmSimulatorsExecution).ToString()))
            {
                return frmSimulatorsExecution;
            }
            else if (persistString.Equals(typeof(FrmHexToAsciiConverter).ToString()))
            {
                return frmHexToAsciiConverter;
            }
            else if (persistString.Equals(typeof(FrmCommRS422).ToString()))
            {
                return frmCommRS422;
            }
            else if (persistString.Equals(typeof(FrmSimCortex).ToString()))
            {
                return frmSimCortex;
            }
            if (persistString.Equals(typeof(FrmApids).ToString()))
            {
                return frmApids;
            }
            else if (persistString.Equals(typeof(FrmMissionIdsWithStructure).ToString()))
            {
                if (formText.Equals("TC Failure Codes"))
                {
                    frmMissionIdsWithStructure = new FrmMissionIdsWithStructure("TC Failure Codes",
                                                          "Failure Code",
                                                          "Failure Description",
                                                          "tc_failure_codes",
                                                          "tc_failure_code",
                                                          "tc_failure_description",
                                                          "tc_failure_code_structure",
                                                          "tc_failure_code",
                                                          "data_field_id",
                                                          "data_fields",
                                                          "data_field_name",
                                                          8,
                                                          100);
                }
                else if (formText.Equals("Event and Error Reports"))
                {
                    frmMissionIdsWithStructure = new FrmMissionIdsWithStructure("Event and Error Reports",
                                                          "RID",
                                                          "Description",
                                                          "rids",
                                                          "rid",
                                                          "description",
                                                          "event_report_structure",
                                                          "rid",
                                                          "data_field_id",
                                                          "data_fields",
                                                          "data_field_name",
                                                          8,
                                                          100);
                }
                else if (formText.Equals("Report Definitions"))
                {
                    frmMissionIdsWithStructure = new FrmMissionIdsWithStructure("Report Definitions",
                                                          "Structure ID",
                                                          "Definition Descript.",
                                                          "report_definitions",
                                                          "structure_id",
                                                          "report_definition_description",
                                                          "report_definition_structure",
                                                          "structure_id",
                                                          "parameter_id",
                                                          "parameters",
                                                          "parameter_description",
                                                          16,
                                                          200);
                }

                return frmMissionIdsWithStructure;
            }
            else if (persistString == typeof(FrmSimpleTable).ToString())
            {
                /**
                 * Customizacao pelo SUBORD do DockPanels
                 * 
                 * @attention 
                 * A cada nova instancia de FrmSimpleTable adicionada, 
                 * um novo IF deve ser colocado aqui. 
                 **/
                if (formText.Equals("Services"))
                {
                    frmSimpleTable = new FrmSimpleTable("Services",
                                                        "Service Type",
                                                        "Service Name",
                                                        "services",
                                                        "service_type",
                                                        "service_name",
                                                        8,
                                                        100);
                }
                else if (formText.Equals("Packet Store IDs"))
                {
                    frmSimpleTable = new FrmSimpleTable("Packet Store IDs",
                                                        "Packet Store ID",
                                                        "Packet Store Name",
                                                        "packet_store_ids",
                                                        "store_id",
                                                        "packet_store_name",
                                                        8,
                                                        100);
                }
                else if (formText.Equals("Memory IDs"))
                {
                    frmSimpleTable = new FrmSimpleTable("Memory IDs",
                                                        "Memory ID",
                                                        "Memory Unit Description",
                                                        "memory_ids",
                                                        "memory_id",
                                                        "memory_unit_description",
                                                        8,
                                                        100);
                }
                else if (formText.Equals("Output Lines IDs"))
                {
                    frmSimpleTable = new FrmSimpleTable("Output Lines IDs", "Output Line ID",
                                                        "Line Description",
                                                        "output_line_ids",
                                                        "output_line_id",
                                                        "output_line_description",
                                                        8,
                                                        100);
                }

                return frmSimpleTable;
            }
            else if (persistString.Equals(typeof(FrmMissionConstants).ToString()))
            {
                return frmMissionConstants;
            }
            else
            {
                return null;
            }
        }

        private void packetsStorageStatusMonitoringToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FormIsOpen("Packets Storage Status Monitoring"))
            {
                return;
            }

            frmPacketStorage = new FrmPacketsStorageStatusMonitoring(this);

            frmPacketStorage.MdiParent = this;
            frmPacketStorage.Show(dockPanel);
        }

        private int startindex = -1;

        public void Refresh_Cmb()
        {
            int selectIndex = -1;

            //Limpa o combo para o refresh
            toolStripCmbSelectDb.Items.Clear();

            //Preenche o combo
            for (int i = 0; i < Settings.Default.db_connections_names.Count; i++)
            {
                toolStripCmbSelectDb.Items.Add(Settings.Default.db_connections_names[i]);

                //Verificar se string conectada e selecionar no combo o item referente
                if (Settings.Default.db_connection_string.ToString() == Settings.Default.db_connections_strings[i].ToString())
                {
                    selectIndex = i;
                    startindex = i;
                }
            }

            toolStripCmbSelectDb.Items.Add("Add or Edit Mission...");

            if (selectIndex != -1)
            {
                toolStripCmbSelectDb.SelectedIndex = selectIndex;
            }
        }

        private void toolStripCmbSelectDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool message = false;

            if(toolStripCmbSelectDb.SelectedIndex != startindex)
            {
            message = true;
            }

            int index = toolStripCmbSelectDb.SelectedIndex;

            if (index == (toolStripCmbSelectDb.Items.Count - 1))
            {
                FrmConfiguration frm = new FrmConfiguration(this);
                this.TopMost = false;
                frm.ShowDialog();
                this.TopMost = true;
                message = false;
                Refresh_Cmb();
            }

            if (message)
            {
                if (MessageBox.Show("Are you sure you want to change the current mission?\n\n" +
                                        "If you choose 'Yes', all windows will be closed.",
                                        "Change Mission",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    CloseAllDocuments();

                    Settings.Default.db_connection_string = Settings.Default.db_connections_strings[toolStripCmbSelectDb.SelectedIndex];
                    Settings.Default.Save();
                    cmbPerspective.SelectedIndex = -1;
                    startindex = toolStripCmbSelectDb.SelectedIndex;
                    RefreshBackground();
                }

                else
                {
                    message = false;
                    toolStripCmbSelectDb.SelectedIndex = startindex;

                }
            }
        }

        #endregion

        #region Metodos Publicos

        public void RefreshBackground()
        {
            dockPanel.BackgroundImage = dbconfiguration.GetBackgroundImage();
        }

        #endregion        
    }
}
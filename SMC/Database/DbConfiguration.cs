/**
 * @file 	    DbConfiguration.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    29/09/2011
 * @note	    Modificado em 28/01/2015 por Thiago.
 **/

using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.OleDb;
using System.Drawing.Imaging;

/**
 * @Namespace Este namespace contem as classes de gerenciamento e persistencia dos 
 * dados a serem armazenados e consultados no banco de dados.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Database
{
    /**
     * @class DbConfiguration
     * Esta classe persiste e recupera a configuracao do sistema no banco de dados.
     **/
    class DbConfiguration
    {
        #region Atributos Internos

        static private int tcFrameVersion = 0;
        static private int tcFrameType = 0;
        static private int tcFrameReservedA = 0;
        static private int tcFrameScid = 0;
        static private int tcFrameVcid = 0;
        static private int tcFrameReservedB = 0;
        static private int tcFrameSeqFlags = 0;
        static private int tcFrameMapid = 0;
        static private bool sessionConfigureFarm = false;
        static private int flightSwVersionMajor = 0;
        static private int flightSwVersionMinor = 0;
        static private int flightSwVersionPatch = 0;
        static private int largeDataLduId = 0;
        static private int largeDataPartsLength = 1;
        static private int largeDataSequenceCount = 0;
        static private int largeDataAck = 0;
        static private int largeDataServiceType = 0;
        static private int largeDataServiceSubtype = 0;
        static private String flightSwPartsOutputFormat;
        static private bool scriptForTestControl = false;
        static private String testControlScriptPath = "";
        static private String egseConnectionType = "";
        static private String egseRs232Port = "";
        static private String egseRs232Baud = "";
        static private String egseRs232DataBits = "";
        static private String egseRs232Parity = "";
        static private String egseRs232StopBits = "";
        static private String egseRs232Port_Debug = "";
        static private String egseRs232Baud_Debug = "";
        static private String egseRs232DataBits_Debug = "";
        static private String egseRs232Parity_Debug = "";
        static private String egseRs232StopBits_Debug = "";
        static private String egseWordToReceive_Debug = "";
        static private String egseWordToAnswer_Debug = "";
        static private String egseRs422TxChannel = "";
        static private String egseRs422RxChannel = "";
        static private String egseRs422ClockRate = "";
        static private String egsePipeType = "";
        static private String egsePipeName = "";
        static private String tcFramesSeqControl = "";
        static private String tcPacketsSeqControl = "";
        static private String tmChannelCoding = "";
        static private String tmTimetagFormat = "";
        static private int requestsDefaultApid = 0;
        static private int requestsDefaultSourceId = 0;
        static private int requestsDefaultServiceType = 0;
        static private String egseCommunicationLayer = "";
        static private String egseTcpIpAddress = "";
        static private bool copConfigure = false;
        static private int copControlFlowPort = 0;
        static private int copMonitoringFlowPort = 0;
        static private int copTcDataFlowPort = 0;
        static private int cortexTelecommandDataPort = 0;
        static private int cortexTelemetryDataPort = 0;
        static private int cortexControlDataPort = 0;
        static private bool egseTcpIpIsServer = false;
        static private bool egseSaveSession = false;
        static private bool egseSyncObt = false;
        static private bool egseManPack = false;
        static private bool egseFarmStart = false;
        static private bool egseAskTm = false;
        static private int egseAskTmSec = 0;
        static private bool egseDiscIdleFrame = false;
        static private DateTime missionEpoch;
        static private String contour_database = "";
        static private String contour_project = "";
        static private int swaplUpdateDelayBetweenTcs = 0;

        #endregion

        #region Construtor

        #endregion

        #region Propriedades Estaticas

        public static int TcFrameVersion
        {
            get { return tcFrameVersion; }
            set { tcFrameVersion = value; }
        }

        public static int TcFrameType
        {
            get { return tcFrameType; }
            set { tcFrameType = value; }
        }

        public static int TcFrameReservedA
        {
            get { return tcFrameReservedA; }
            set { tcFrameReservedA = value; }
        }

        public static int TcFrameScid
        {
            get { return tcFrameScid; }
            set { tcFrameScid = value; }
        }

        public static int TcFrameVcid
        {
            get { return tcFrameVcid; }
            set { tcFrameVcid = value; }
        }
        
        public static int TcFrameReservedB
        {
            get { return tcFrameReservedB; }
            set { tcFrameReservedB = value; }
        }

        public static int TcFrameSeqFlags
        {
            get { return tcFrameSeqFlags; }
            set { tcFrameSeqFlags = value; }
        }

        public static int TcFrameMapid
        {
            get { return tcFrameMapid; }
            set { tcFrameMapid = value; }
        }

        public static bool SessionConfigureFarm
        {
            get { return sessionConfigureFarm; }
            set {sessionConfigureFarm = value; }
        }

        public static int LargeDataAck
        {
            get { return largeDataAck; }
            set { largeDataAck = value; }
        }
        
        public static int LargeDataServiceType
        {
            get { return largeDataServiceType; }
            set { largeDataServiceType = value; }
        }
        
        public static int LargeDataServiceSubtype
        {
            get { return largeDataServiceSubtype; }
            set { largeDataServiceSubtype = value; }
        }
        
        public static int SwaplUpdateDelayBetweenTcs
        {
            get { return swaplUpdateDelayBetweenTcs; }
            set { swaplUpdateDelayBetweenTcs = value; }
        }
        
        public static int FlightSwVersionMajor
        {
            get { return flightSwVersionMajor; }
            set { flightSwVersionMajor = value; }
        }
        
        public static int FlightSwVersionMinor
        {
            get { return flightSwVersionMinor; }
            set { flightSwVersionMinor = value; }
        }

        public static int FlightSwVersionPatch
        {
            get { return flightSwVersionPatch; }
            set { flightSwVersionPatch = value; }
        }

        public static int LargeDataLduId
        {
            get { return largeDataLduId; }
            set { largeDataLduId = value; }
        }

        public static int LargeDataPartsLength
        {
            get { return largeDataPartsLength; }
            set { largeDataPartsLength = value; }
        }

        public static int LargeDataSequenceCount
        {
            get { return largeDataSequenceCount; }
            set { largeDataSequenceCount = value; }
        }

        public static String FlightSwPartsOutputFormat
        {
            get { return flightSwPartsOutputFormat; }
            set { flightSwPartsOutputFormat = value; }
        }

        public static bool FlightSwScriptForTestControl
        {
            get { return scriptForTestControl; }
            set { scriptForTestControl = value; }
        }
        
        public static String FlightSwTestControlScriptPath
        {
            get { return testControlScriptPath; }
            set { testControlScriptPath = value; }
        }

        public static String EgseConnectionType
        {
            get { return DbConfiguration.egseConnectionType; }
            set { DbConfiguration.egseConnectionType = value; }
        }

        public static String EgseRs232Port
        {
            get { return DbConfiguration.egseRs232Port; }
            set { DbConfiguration.egseRs232Port = value; }
        }

        public static String EgseRs232Baud
        {
            get { return DbConfiguration.egseRs232Baud; }
            set { DbConfiguration.egseRs232Baud = value; }
        }

        public static String EgseRs232DataBits
        {
            get { return DbConfiguration.egseRs232DataBits; }
            set { DbConfiguration.egseRs232DataBits = value; }
        }

        public static String EgseRs232Parity
        {
            get { return DbConfiguration.egseRs232Parity; }
            set { DbConfiguration.egseRs232Parity = value; }
        }

        public static String EgseRs232StopBits
        {
            get { return DbConfiguration.egseRs232StopBits; }
            set { DbConfiguration.egseRs232StopBits = value; }
        }

        public static String EgseRs232Port_Debug
        {
            get { return DbConfiguration.egseRs232Port_Debug; }
            set { DbConfiguration.egseRs232Port_Debug = value; }
        }

        public static String EgseRs232Baud_Debug
        {
            get { return DbConfiguration.egseRs232Baud_Debug; }
            set { DbConfiguration.egseRs232Baud_Debug = value; }
        }

        public static String EgseRs232DataBits_Debug
        {
            get { return DbConfiguration.egseRs232DataBits_Debug; }
            set { DbConfiguration.egseRs232DataBits_Debug = value; }
        }

        public static String EgseRs232Parity_Debug
        {
            get { return DbConfiguration.egseRs232Parity_Debug; }
            set { DbConfiguration.egseRs232Parity_Debug = value; }
        }

        public static String EgseRs232StopBits_Debug
        {
            get { return DbConfiguration.egseRs232StopBits_Debug; }
            set { DbConfiguration.egseRs232StopBits_Debug = value; }
        }

        public static String EgseWordToReceive_Debug
        {
            get { return DbConfiguration.egseWordToReceive_Debug; }
            set { DbConfiguration.egseWordToReceive_Debug = value; }
        }

        public static String EgseWordToAnswer_Debug
        {
            get { return DbConfiguration.egseWordToAnswer_Debug; }
            set { DbConfiguration.egseWordToAnswer_Debug = value; }
        }

        public static String EgseRs422TxChannel
        {
            get { return DbConfiguration.egseRs422TxChannel; }
            set { DbConfiguration.egseRs422TxChannel = value; }
        }

        public static String EgseRs422RxChannel
        {
            get { return DbConfiguration.egseRs422RxChannel; }
            set { DbConfiguration.egseRs422RxChannel = value; }
        }

        public static String EgseRs422ClockRate
        {
            get { return DbConfiguration.egseRs422ClockRate; }
            set { DbConfiguration.egseRs422ClockRate = value; }
        }

        public static String EgsePipeType
        {
            get { return DbConfiguration.egsePipeType; }
            set { DbConfiguration.egsePipeType = value; }
        }

        public static String EgsePipeName
        {
            get { return DbConfiguration.egsePipeName; }
            set { DbConfiguration.egsePipeName = value; }
        }

        public static String TcFramesSeqControl
        {
            get { return DbConfiguration.tcFramesSeqControl; }
            set { DbConfiguration.tcFramesSeqControl = value; }
        }

        public static String TcPacketsSeqControl
        {
            get { return DbConfiguration.tcPacketsSeqControl; }
            set { DbConfiguration.tcPacketsSeqControl = value; }
        }

        public static String TmChannelCoding
        {
            get { return DbConfiguration.tmChannelCoding; }
            set { DbConfiguration.tmChannelCoding = value; }
        }

        public static String TmTimetagFormat
        {
            get { return DbConfiguration.tmTimetagFormat; }
            set { DbConfiguration.tmTimetagFormat = value; }
        }

        public static int RequestsDefaultApid
        {
            get { return DbConfiguration.requestsDefaultApid; }
            set { DbConfiguration.requestsDefaultApid = value; }
        }

        public static int RequestsDefaultSourceId
        {
            get { return DbConfiguration.requestsDefaultSourceId; }
            set { DbConfiguration.requestsDefaultSourceId = value; }
        }

        public static int RequestsDefaultServiceType
        {
            get { return DbConfiguration.requestsDefaultServiceType; }
            set { DbConfiguration.requestsDefaultServiceType = value; }
        }

        public static String EgseCommunicationLayer
        {
            get { return DbConfiguration.egseCommunicationLayer; }
            set { DbConfiguration.egseCommunicationLayer = value; }
        }

        public static String EgseTcpIpAddress
        {
            get { return DbConfiguration.egseTcpIpAddress; }
            set { DbConfiguration.egseTcpIpAddress = value; }
        }

        public static bool CopConfigure
        {
            get { return DbConfiguration.copConfigure; }
            set { DbConfiguration.copConfigure = value; }
        }

        public static int CopControlFlowPort
        {
            get { return DbConfiguration.copControlFlowPort; }
            set { DbConfiguration.copControlFlowPort = value; }
        }

        public static int CopMonitoringFlowPort
        {
            get { return DbConfiguration.copMonitoringFlowPort; }
            set { DbConfiguration.copMonitoringFlowPort = value; }
        }

        public static int CopTcDataFlowPort
        {
            get { return DbConfiguration.copTcDataFlowPort; }
            set { DbConfiguration.copTcDataFlowPort = value; }
        }

        public static int CortexTelecommandDataPort
        {
            get { return DbConfiguration.cortexTelecommandDataPort; }
            set { DbConfiguration.cortexTelecommandDataPort = value; }
        }

        public static int CortexTelemetryDataPort
        {
            get { return DbConfiguration.cortexTelemetryDataPort; }
            set { DbConfiguration.cortexTelemetryDataPort = value; }
        }

        public static int CortexControlDataPort
        {
            get { return DbConfiguration.cortexControlDataPort; }
            set { DbConfiguration.cortexControlDataPort = value; }
        }

        public static int EgseAskTmSec
        {
            get { return DbConfiguration.egseAskTmSec; }
            set { DbConfiguration.egseAskTmSec = value; }
        }

        public static bool EgseTcpIpIsServer
        {
            get { return DbConfiguration.egseTcpIpIsServer; }
            set { DbConfiguration.egseTcpIpIsServer = value; }
        }

        public static bool EgseSaveSession
        {
            get { return DbConfiguration.egseSaveSession; }
            set { DbConfiguration.egseSaveSession = value; }
        }

        public static bool EgseSyncObt
        {
            get { return DbConfiguration.egseSyncObt; }
            set { DbConfiguration.egseSyncObt = value; }
        }

        public static bool EgseManPack
        {
            get { return DbConfiguration.egseManPack; }
            set { DbConfiguration.egseManPack = value; }
        }

        public static bool EgseFarmStart
        {
            get { return DbConfiguration.egseFarmStart; }
            set { DbConfiguration.egseFarmStart = value; }
        }

        public static bool EgseAskTm
        {
            get { return DbConfiguration.egseAskTm; }
            set { DbConfiguration.egseAskTm = value; }
        }

        public static bool EgseDiscIdleFrame
        {
            get { return DbConfiguration.egseDiscIdleFrame; }
            set { DbConfiguration.egseDiscIdleFrame = value; }
        }

        public static DateTime MissionEpoch
        {
            get { return DbConfiguration.missionEpoch; }
            set { DbConfiguration.missionEpoch = value; }
        }

        public static String Contour_database
        {
            get { return DbConfiguration.contour_database; }
            set { DbConfiguration.contour_database = value; }
        }

        public static String Contour_project
        {
            get { return DbConfiguration.contour_project; }
            set { DbConfiguration.contour_project = value; }
        }
     
        #endregion

        #region Metodos Estaticos

        /** Carrega as configuracoes persistidas no banco de dados nas propriedades da classe. **/
        public static bool Load()
        {
            String sql = "select top 1 * from configuration";
            DataTable table = DbInterface.GetDataTable(sql);

            if (table.Rows.Count == 0)
            {
                // ainda nao ha nenhum registro na tabela de configuracao, retorna false
                return false;
            }
            else
            {
                if (table.Rows[0]["tc_frame_version"] != DBNull.Value)
                {
                    tcFrameVersion = int.Parse(table.Rows[0]["tc_frame_version"].ToString());
                }

                if (table.Rows[0]["tc_frame_type"] != DBNull.Value)
                {
                    tcFrameType = int.Parse(table.Rows[0]["tc_frame_type"].ToString());
                }

                if (table.Rows[0]["tc_frame_reserved_a"] != DBNull.Value)
                {
                    tcFrameReservedA = int.Parse(table.Rows[0]["tc_frame_reserved_a"].ToString());
                }

                if (table.Rows[0]["tc_frame_scid"] != DBNull.Value)
                {
                    tcFrameScid = int.Parse(table.Rows[0]["tc_frame_scid"].ToString());
                }

                if (table.Rows[0]["tc_frame_vcid"] != DBNull.Value)
                {
                    tcFrameVcid = int.Parse(table.Rows[0]["tc_frame_vcid"].ToString());
                }

                if (table.Rows[0]["tc_frame_reserved_b"] != DBNull.Value)
                {
                    tcFrameReservedB = int.Parse(table.Rows[0]["tc_frame_reserved_b"].ToString());
                }

                if (table.Rows[0]["tc_frame_seq_flags"] != DBNull.Value)
                {
                    tcFrameSeqFlags = int.Parse(table.Rows[0]["tc_frame_seq_flags"].ToString());
                }

                if (table.Rows[0]["tc_frame_mapid"] != DBNull.Value)
                {
                    tcFrameMapid = int.Parse(table.Rows[0]["tc_frame_mapid"].ToString());
                }

                if (table.Rows[0]["session_configure_farm"] != DBNull.Value)
                {
                    sessionConfigureFarm = bool.Parse(table.Rows[0]["session_configure_farm"].ToString());
                }

                if (table.Rows[0]["swapl_version"] != DBNull.Value)
                {
                    flightSwVersionMajor = int.Parse(table.Rows[0]["swapl_version"].ToString());
                }

                if (table.Rows[0]["swapl_release"] != DBNull.Value)
                {
                    flightSwVersionMinor = int.Parse(table.Rows[0]["swapl_release"].ToString());
                }

                if (table.Rows[0]["swapl_patch"] != DBNull.Value)
                {
                    flightSwVersionPatch = int.Parse(table.Rows[0]["swapl_patch"].ToString());
                }

                if (table.Rows[0]["large_data_ldu_id"] != DBNull.Value)
                {
                    largeDataLduId = int.Parse(table.Rows[0]["large_data_ldu_id"].ToString());
                }

                if (table.Rows[0]["large_data_parts_length"] != DBNull.Value)
                {
                    largeDataPartsLength = int.Parse(table.Rows[0]["large_data_parts_length"].ToString());
                }

                if (table.Rows[0]["large_data_sequence_count"] != DBNull.Value)
                {
                    largeDataSequenceCount = int.Parse(table.Rows[0]["large_data_sequence_count"].ToString());
                }

                if (table.Rows[0]["large_data_ack"] != DBNull.Value)
                {
                    largeDataAck = int.Parse(table.Rows[0]["large_data_ack"].ToString());
                }

                if (table.Rows[0]["large_data_service_type"] != DBNull.Value)
                {
                    largeDataServiceType = int.Parse(table.Rows[0]["large_data_service_type"].ToString());
                }

                if (table.Rows[0]["large_data_service_subtype"] != DBNull.Value)
                {
                    largeDataServiceSubtype = int.Parse(table.Rows[0]["large_data_service_subtype"].ToString());
                }

                if (table.Rows[0]["flight_sw_parts_output_format"] != DBNull.Value)
                {
                    flightSwPartsOutputFormat = table.Rows[0]["flight_sw_parts_output_format"].ToString();
                }

                if (table.Rows[0]["flight_sw_script_for_test_control"] != DBNull.Value)
                {
                    scriptForTestControl = bool.Parse(table.Rows[0]["flight_sw_script_for_test_control"].ToString());
                }

                if (table.Rows[0]["flight_sw_test_control_script_path"] != DBNull.Value)
                {
                    testControlScriptPath = table.Rows[0]["flight_sw_test_control_script_path"].ToString();
                }

                if (table.Rows[0]["egse_connection_type"] != DBNull.Value)
                {
                    egseConnectionType = table.Rows[0]["egse_connection_type"].ToString();
                }

                if (table.Rows[0]["egse_rs232_port"] != DBNull.Value)
                {
                    egseRs232Port = table.Rows[0]["egse_rs232_port"].ToString();
                }

                if (table.Rows[0]["egse_rs232_baud"] != DBNull.Value)
                {
                    egseRs232Baud = table.Rows[0]["egse_rs232_baud"].ToString();
                }

                if (table.Rows[0]["egse_rs232_data_bits"] != DBNull.Value)
                {
                    egseRs232DataBits = table.Rows[0]["egse_rs232_data_bits"].ToString();
                }

                if (table.Rows[0]["egse_rs232_parity"] != DBNull.Value)
                {
                    egseRs232Parity = table.Rows[0]["egse_rs232_parity"].ToString();
                }

                if (table.Rows[0]["egse_rs232_stop_bits"] != DBNull.Value)
                {
                    egseRs232StopBits = table.Rows[0]["egse_rs232_stop_bits"].ToString();
                }

                if (table.Rows[0]["egse_rs232_port_Debug"] != DBNull.Value)
                {
                    egseRs232Port_Debug = table.Rows[0]["egse_rs232_port_Debug"].ToString();
                }

                if (table.Rows[0]["egse_rs232_baud_Debug"] != DBNull.Value)
                {
                    egseRs232Baud_Debug = table.Rows[0]["egse_rs232_baud_Debug"].ToString();
                }

                if (table.Rows[0]["egse_rs232_data_bits_Debug"] != DBNull.Value)
                {
                    egseRs232DataBits_Debug = table.Rows[0]["egse_rs232_data_bits_Debug"].ToString();
                }

                if (table.Rows[0]["egse_rs232_parity_Debug"] != DBNull.Value)
                {
                    egseRs232Parity_Debug = table.Rows[0]["egse_rs232_parity_Debug"].ToString();
                }

                if (table.Rows[0]["egse_rs232_stop_bits_Debug"] != DBNull.Value)
                {
                    egseRs232StopBits_Debug = table.Rows[0]["egse_rs232_stop_bits_Debug"].ToString();
                }

                if (table.Rows[0]["egse_word_to_receive_Debug"] != DBNull.Value)
                {
                    egseWordToReceive_Debug = table.Rows[0]["egse_word_to_receive_Debug"].ToString();
                }

                if (table.Rows[0]["egse_word_to_answer_Debug"] != DBNull.Value)
                {
                    egseWordToAnswer_Debug = table.Rows[0]["egse_word_to_answer_Debug"].ToString();
                }

                if (table.Rows[0]["egse_rs422_tx_channel"] != DBNull.Value)
                {
                    egseRs422TxChannel = table.Rows[0]["egse_rs422_tx_channel"].ToString();
                }

                if (table.Rows[0]["egse_rs422_rx_channel"] != DBNull.Value)
                {
                    egseRs422RxChannel = table.Rows[0]["egse_rs422_rx_channel"].ToString();
                }

                if (table.Rows[0]["egse_rs422_clock_rate"] != DBNull.Value)
                {
                    egseRs422ClockRate = table.Rows[0]["egse_rs422_clock_rate"].ToString();
                }

                if (table.Rows[0]["egse_pipe_type"] != DBNull.Value)
                {
                    egsePipeType = table.Rows[0]["egse_pipe_type"].ToString();
                }

                if (table.Rows[0]["egse_pipe_name"] != DBNull.Value)
                {
                    egsePipeName = table.Rows[0]["egse_pipe_name"].ToString();
                }

                if (table.Rows[0]["tc_frames_seq_control"] != DBNull.Value)
                {
                    tcFramesSeqControl = table.Rows[0]["tc_frames_seq_control"].ToString();
                }

                if (table.Rows[0]["tc_packets_seq_control"] != DBNull.Value)
                {
                    tcPacketsSeqControl = table.Rows[0]["tc_packets_seq_control"].ToString();
                }

                if (table.Rows[0]["tm_channel_coding"] != DBNull.Value)
                {
                    tmChannelCoding = table.Rows[0]["tm_channel_coding"].ToString();
                }

                if (table.Rows[0]["tm_timetag_format"] != DBNull.Value)
                {
                    tmTimetagFormat = table.Rows[0]["tm_timetag_format"].ToString();
                }

                if (table.Rows[0]["requests_default_apid"] != DBNull.Value)
                {
                    requestsDefaultApid = int.Parse(table.Rows[0]["requests_default_apid"].ToString());
                }

                if (table.Rows[0]["requests_default_source_id"] != DBNull.Value)
                {
                    requestsDefaultSourceId = int.Parse(table.Rows[0]["requests_default_source_id"].ToString());
                }

                if (table.Rows[0]["requests_default_service_type"] != DBNull.Value)
                {
                    requestsDefaultServiceType = int.Parse(table.Rows[0]["requests_default_service_type"].ToString());
                }

                if (table.Rows[0]["egse_communication_layer"] != DBNull.Value)
                {
                    egseCommunicationLayer = table.Rows[0]["egse_communication_layer"].ToString();
                }

                if (table.Rows[0]["egse_tcpIp_address"] != DBNull.Value)
                {
                    egseTcpIpAddress = table.Rows[0]["egse_tcpIp_address"].ToString();
                }

                if (table.Rows[0]["egse_ask_tm_sec"] != DBNull.Value)
                {
                    egseAskTmSec = int.Parse(table.Rows[0]["egse_ask_tm_sec"].ToString());
                }

                if (table.Rows[0]["egse_tcpIp_is_server"] != DBNull.Value)
                {
                    egseTcpIpIsServer = bool.Parse(table.Rows[0]["egse_tcpIp_is_server"].ToString());
                }

                if (table.Rows[0]["cop_configure"] != DBNull.Value)
                {
                    copConfigure = bool.Parse(table.Rows[0]["cop_configure"].ToString());
                }

                if (table.Rows[0]["cop_control_flow_port"] != DBNull.Value)
                {
                    copControlFlowPort = int.Parse(table.Rows[0]["cop_control_flow_port"].ToString());
                }

                if (table.Rows[0]["cop_monitoring_flow_port"] != DBNull.Value)
                {
                    copMonitoringFlowPort = int.Parse(table.Rows[0]["cop_monitoring_flow_port"].ToString());
                }

                if (table.Rows[0]["cop_tc_data_flow_port"] != DBNull.Value)
                {
                    copTcDataFlowPort = int.Parse(table.Rows[0]["cop_tc_data_flow_port"].ToString());
                }

                if (table.Rows[0]["cortex_telecommand_data_port"] != DBNull.Value)
                {
                    cortexTelecommandDataPort = int.Parse(table.Rows[0]["cortex_telecommand_data_port"].ToString());
                }

                if (table.Rows[0]["cortex_telemetry_data_port"] != DBNull.Value)
                {
                    cortexTelemetryDataPort = int.Parse(table.Rows[0]["cortex_telemetry_data_port"].ToString());
                }

                if (table.Rows[0]["cortex_control_data_port"] != DBNull.Value)
                {
                    cortexControlDataPort = int.Parse(table.Rows[0]["cortex_control_data_port"].ToString());
                }

                if (table.Rows[0]["egse_save_session"] != DBNull.Value)
                {
                    egseSaveSession = bool.Parse(table.Rows[0]["egse_save_session"].ToString());
                }

                if (table.Rows[0]["egse_sync_obt"] != DBNull.Value)
                {
                    egseSyncObt = bool.Parse(table.Rows[0]["egse_sync_obt"].ToString());
                }

                if (table.Rows[0]["egse_man_pack"] != DBNull.Value)
                {
                    egseManPack = bool.Parse(table.Rows[0]["egse_man_pack"].ToString());
                }

                if (table.Rows[0]["egse_farm_start"] != DBNull.Value)
                {
                    egseFarmStart = bool.Parse(table.Rows[0]["egse_farm_start"].ToString());
                }

                if (table.Rows[0]["egse_ask_tm"] != DBNull.Value)
                {
                    egseAskTm = bool.Parse(table.Rows[0]["egse_ask_tm"].ToString());
                }

                if (table.Rows[0]["egse_disc_idle_frame"] != DBNull.Value)
                {
                    egseDiscIdleFrame = bool.Parse(table.Rows[0]["egse_disc_idle_frame"].ToString());
                }

                if (table.Rows[0]["mission_epoch"] != DBNull.Value)
                {
                    missionEpoch = Convert.ToDateTime(table.Rows[0]["mission_epoch"].ToString());
                }

                if (table.Rows[0]["contour_database"] != DBNull.Value)
                {
                    contour_database = table.Rows[0]["contour_database"].ToString();
                }

                if (table.Rows[0]["contour_project"] != DBNull.Value)
                {
                    contour_project = table.Rows[0]["contour_project"].ToString();
                }

                if (table.Rows[0]["swapl_update_delay_between_tcs"] != DBNull.Value)
                {
                    swaplUpdateDelayBetweenTcs = int.Parse(table.Rows[0]["swapl_update_delay_between_tcs"].ToString());
                }

                table.Dispose();
                return true;
            }
        }

        /** Persiste em banco de dados as configuracoes setadas nas propriedades da classe. **/
        public static bool Save ()
        {
            // verifica se ja ha um registro de configuracao
            String sql = "select top 1 * from configuration";
            DataTable table = DbInterface.GetDataTable(sql);

            if (table.Rows.Count == 0)
            {
                // estou informando pela primeira vez a configuracao
                sql = @"insert into configuration 
                            (tc_frame_version, tc_frame_type, tc_frame_reserved_a, 
                             tc_frame_scid, tc_frame_vcid, tc_frame_reserved_b, 
                             tc_frame_seq_flags, tc_frame_mapid, session_configure_farm,
	                         swapl_version, swapl_release, swapl_patch,
	                         large_data_ldu_id, large_data_parts_length, large_data_sequence_count,
	                         large_data_ack, large_data_service_type, large_data_service_subtype,
                             flight_sw_parts_output_format, egse_connection_type, egse_rs232_port, 
                             egse_rs232_baud, egse_rs232_data_bits, egse_rs232_parity, egse_rs232_stop_bits, egse_rs232_port_debug, 
                             egse_rs232_baud_debug, egse_rs232_data_bits_debug, egse_rs232_parity_debug, egse_rs232_stop_bits_debug, egse_word_to_receive_debug, egse_word_to_answer,
                             egse_rs422_tx_channel, egse_rs422_rx_channel, egse_rs422_clock_rate, 
                             egse_pipe_type, egse_pipe_name, tc_frames_seq_control, tc_packets_seq_control, 
                             tm_channel_coding, tm_timetag_format, requests_default_apid, 
                             requests_default_source_id, requests_default_service_type, egse_communication_layer, egse_tcpIp_addres,
                             egse_tcpIp_is_server, egse_save_session, egse_sync_obt, egse_man_pack, egse_farm_start, 
                             egse_ask_tm, egse_ask_tm_sec, egse_disc_idle_frame, cop_configure, cop_control_flow_port, cop_monitoring_flow_port, cop_tc_data_flow_port, 
                             cortex_telecommand_data_port, cortex_telemetry_data_port, cortex_control_data_port, 
                             mission_epoch, contour_database, contour_project, flight_sw_script_for_test_control, flight_sw_test_control_script_path, swapl_update_delay_between_tcs) 
                        values 
                            (" + tcFrameVersion.ToString() + ", " + tcFrameType.ToString() + ", " + tcFrameReservedA.ToString() + ", " +
                                 tcFrameScid.ToString() + ", " + tcFrameVcid.ToString() + ", " + tcFrameReservedB.ToString() + ", " +
                                 tcFrameSeqFlags.ToString() + ", " + tcFrameMapid.ToString() + ", " +
                                 Math.Abs(Convert.ToInt32(sessionConfigureFarm)).ToString() + ", " +
                                 flightSwVersionMajor.ToString() + ", " + flightSwVersionMinor.ToString() + ", " + flightSwVersionPatch.ToString() + ", " +
                                 largeDataLduId.ToString() + ", " + largeDataPartsLength.ToString() + ", " + largeDataSequenceCount.ToString() + ", " +
                                 largeDataAck.ToString() + ", " + largeDataServiceType.ToString() + ", " + largeDataServiceSubtype.ToString() + ", '" +
                                 flightSwPartsOutputFormat + "', '" + egseConnectionType + "', '" + egseRs232Port + "', '" + egseRs232Baud + "', '" +
                                 egseRs232DataBits + "', '" + egseRs232Parity + "', '" + egseRs232StopBits + "', '" + egseRs232Port_Debug + "', '" + egseRs232Baud_Debug + "', '" +
                                 egseRs232DataBits_Debug + "', '" + egseRs232Parity_Debug + "', '" + egseRs232StopBits_Debug + "', '" + egseWordToReceive_Debug + "', '" + egseWordToAnswer_Debug + "', '" + egseRs422TxChannel + "', '" +
                                 egseRs422RxChannel + "', '" + egseRs422ClockRate + "', '" + egsePipeType + "', '" + egsePipeName + "', '" +
                                 tcFramesSeqControl + "', '" + tcPacketsSeqControl + "', '" + tmChannelCoding + "', '" + tmTimetagFormat + "', " + 
                                 requestsDefaultApid.ToString() + ", " + requestsDefaultSourceId.ToString() + ", " + requestsDefaultServiceType.ToString() + ", '" +
                                 egseCommunicationLayer + "', '" + egseTcpIpAddress + ", " + Math.Abs(Convert.ToInt32(egseTcpIpIsServer)).ToString() +
                                 ", " + Math.Abs(Convert.ToInt32(egseSaveSession)).ToString() + ", " + Math.Abs(Convert.ToInt32(egseSyncObt)).ToString() + ", " + Math.Abs(Convert.ToInt32(egseManPack)).ToString() +
                                 ", " + Math.Abs(Convert.ToInt32(egseFarmStart)).ToString() + ", " + Math.Abs(Convert.ToInt32(egseAskTm)).ToString() + egseAskTmSec.ToString() + ", " + Math.Abs(Convert.ToInt32(egseDiscIdleFrame)).ToString() + ", '" +
                                 copConfigure + "', '" + copControlFlowPort + "', '" + copMonitoringFlowPort + "', '" + copTcDataFlowPort + "', '" + cortexTelecommandDataPort + "', '" + cortexTelemetryDataPort + "', '" + cortexControlDataPort + ", " + 
                                 missionEpoch + ", " + contour_database + ", " + contour_project + "', " + Math.Abs(Convert.ToInt32(scriptForTestControl)).ToString() + ", '" + testControlScriptPath + "', '" + swaplUpdateDelayBetweenTcs + "' )";
            }
            else
            {
                // estou atualizando a configuracao
                sql = @"update configuration set 
                            tc_frame_version = " + tcFrameVersion.ToString() + @", 
                            tc_frame_type = " + tcFrameType.ToString() + @",
                            tc_frame_reserved_a = " + tcFrameReservedA.ToString() + @",
                            tc_frame_scid = " + tcFrameScid.ToString() + @",
                            tc_frame_vcid = " + tcFrameVcid.ToString() + @",
                            tc_frame_reserved_b = " + tcFrameReservedB.ToString() + @",
                            tc_frame_seq_flags = " + tcFrameSeqFlags.ToString() + @",
                            tc_frame_mapid = " + tcFrameMapid.ToString() + @",
                            session_configure_farm = " + Math.Abs(Convert.ToInt32(sessionConfigureFarm)).ToString() + @",
                            swapl_version = " + flightSwVersionMajor.ToString() + @",
                            swapl_release = " + flightSwVersionMinor.ToString() + @",
                            swapl_patch = " + flightSwVersionPatch.ToString() + @",
	                        large_data_ldu_id = " + largeDataLduId.ToString() + @",
                            large_data_parts_length = " + largeDataPartsLength.ToString() + @",
                            large_data_sequence_count = " + largeDataSequenceCount.ToString() + @",
	                        large_data_ack = " + largeDataAck.ToString() + @",
                            large_data_service_type = " + largeDataServiceType.ToString() + @",
                            large_data_service_subtype = " + largeDataServiceSubtype.ToString() + @",
                            flight_sw_parts_output_format = '" + flightSwPartsOutputFormat + @"',
                            egse_connection_type = '" + egseConnectionType + @"',
                            egse_rs232_port = '" + egseRs232Port + @"',
                            egse_rs232_baud = '" + egseRs232Baud + @"',
                            egse_rs232_data_bits = '" + egseRs232DataBits + @"',
                            egse_rs232_parity = '" + egseRs232Parity + @"',
                            egse_rs232_stop_bits = '" + egseRs232StopBits + @"',
                            egse_rs232_port_debug = '" + egseRs232Port_Debug + @"',
                            egse_rs232_baud_debug = '" + egseRs232Baud_Debug + @"',
                            egse_rs232_data_bits_debug = '" + egseRs232DataBits_Debug + @"',
                            egse_rs232_parity_debug = '" + egseRs232Parity_Debug + @"',
                            egse_rs232_stop_bits_debug = '" + egseRs232StopBits_Debug + @"',
                            egse_word_to_receive_debug = '" + egseWordToReceive_Debug + @"',
                            egse_word_to_answer_debug = '" + egseWordToAnswer_Debug + @"',
                            egse_rs422_tx_channel = '" + egseRs422TxChannel + @"',
                            egse_rs422_rx_channel = '" + egseRs422RxChannel + @"',
                            egse_rs422_clock_rate = '" + egseRs422ClockRate + @"',
                            egse_pipe_type = '" + egsePipeType + @"',
                            egse_pipe_name = '" + egsePipeName + @"',
                            tc_frames_seq_control = '" + tcFramesSeqControl + @"',
                            tc_packets_seq_control = '" + tcPacketsSeqControl + @"',
                            tm_channel_coding = '" + tmChannelCoding + @"',
                            tm_timetag_format = '" + tmTimetagFormat + @"',
                            requests_default_apid = " + requestsDefaultApid + @",
                            requests_default_source_id = " + requestsDefaultSourceId + @",
                            requests_default_service_type = " + requestsDefaultServiceType + @",
                            egse_communication_layer = '" + egseCommunicationLayer + @"',
                            egse_tcpIp_address = '" + egseTcpIpAddress + @"',
                            egse_tcpIp_is_server = " + Math.Abs(Convert.ToInt32(egseTcpIpIsServer)).ToString() + @",
                            egse_save_session = " + Math.Abs(Convert.ToInt32(egseSaveSession)).ToString() + @",
                            egse_sync_obt = " + Math.Abs(Convert.ToInt32(egseSyncObt)).ToString() + @",
                            egse_man_pack = " + Math.Abs(Convert.ToInt32(egseManPack)).ToString() + @",
                            egse_farm_start = " + Math.Abs(Convert.ToInt32(egseFarmStart)).ToString() + @",
                            egse_ask_tm = " + Math.Abs(Convert.ToInt32(egseAskTm)).ToString() + @",
                            egse_ask_tm_sec = " + egseAskTmSec + @",
                            egse_disc_idle_frame = " + Math.Abs(Convert.ToInt32(egseDiscIdleFrame)).ToString() + @",
                            cop_configure = '" + copConfigure + @"',
                            cop_control_flow_port = '" + copControlFlowPort + @"',
                            cop_monitoring_flow_port = '" + copMonitoringFlowPort + @"',
                            cop_tc_data_flow_port = '" + copTcDataFlowPort + @"',
                            cortex_telecommand_data_port = '" + cortexTelecommandDataPort + @"',
                            cortex_telemetry_data_port = '" + cortexTelemetryDataPort + @"',
                            cortex_control_data_port = '" + cortexControlDataPort + @"',
                            mission_epoch = '" + missionEpoch + @"',
                            contour_database = '" + contour_database + @"',
                            contour_project = '" + contour_project + @"',
                            flight_sw_script_for_test_control = " + Math.Abs(Convert.ToInt32(scriptForTestControl)).ToString() + @",
                            flight_sw_test_control_script_path = '" + testControlScriptPath + @"',
                            swapl_update_delay_between_tcs = '" + swaplUpdateDelayBetweenTcs + "'";
            }

            if (!DbInterface.ExecuteNonQuery(sql))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Metodos Publicos

        public void UpdateBackgroundImage(String path)
        {
            // http://www.redmondpie.com/inserting-in-and-retrieving-image-from-sql-server-database-using-c/
            //http://www.c-sharpcorner.com/uploadfile/e628d9/inserting-images-into-ms-access-file-using-oledb/


            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] imageBytes = new byte[fileStream.Length];
            fileStream.Read(imageBytes, 0, System.Convert.ToInt32(fileStream.Length));
            fileStream.Close();

            String sql = @" update configuration set background_image = ?";
            OleDbConnection connection = new OleDbConnection("file name = " + Properties.Settings.Default.db_connection_string);
            connection.Open();
            
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            cmd.Parameters.Add(new OleDbParameter("@image", OleDbType.VarBinary));
            cmd.Parameters["@image"].Value = imageBytes;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public Image GetBackgroundImage()
        {
            try
            {
                byte[] imageBytes = null;
                String sql = "select background_image from configuration";
                imageBytes = (byte[])DbInterface.ExecuteScalar(sql);
                MemoryStream MemStream = new MemoryStream(imageBytes);
                return Image.FromStream(MemStream);
            }
            catch
            {
                return null;
            }
        }
        
        #endregion
    }
}

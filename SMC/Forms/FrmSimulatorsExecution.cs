/**
 * @file 	    FrmSimulatorsExecution.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    21/09/2012
 * @note	    Modificado em 27/03/2013 por Thiago
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Database;
using WeifenLuo.WinFormsUI.Docking;
using System.IO.Ports;
using System.Threading;
using System.Collections;
using System.Timers;
using System.Text.RegularExpressions;
using Inpe.Subord.Comav.Egse.Smc.Simulations;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSimulatorsExecution
     * Esta classe eh a responsavel pela execucao de simuladores de equipamentos (sensores e atuadores) para o OBC.
     **/
    public partial class FrmSimulatorsExecution : DockContent
    {
        #region Variaveis

        private CommunicationProtocolSimulator simulator = null;
        private Dictionary<int, CommunicationProtocolSimulator> allSimulators = new Dictionary<int, CommunicationProtocolSimulator>();
        private List<AvailableAnsweredMsgEventArgs> msgsSentToPrint = new List<AvailableAnsweredMsgEventArgs>();
        private Dictionary<int, Thread> runningSimulators = new Dictionary<int, Thread>();

        #endregion

        #region Construtor

        public FrmSimulatorsExecution()
        {
            InitializeComponent();
        }

        #endregion

        #region Metodos Privados

        private void AddSimulatorToConfiguration()
        {
            AddColumnsGridSimulators();

            FillComboSimulators();

            // Preencher a coluna Connection Type
            DataGridViewComboBoxCell cmbConnType = new DataGridViewComboBoxCell();
            cmbConnType.Items.Add("RS-232");
            cmbConnType.Items.Add("RS-422");
            gridSimulators[1, 0] = cmbConnType;
        }

        private void FillComboSimulators()
        {
            // Busca os simuladores cadastrados no SAD
            String sql = "select sim_id, sim_name from simulators";
            DataTable tblSim = DbInterface.GetDataTable(sql);

            // Preencher a coluna Simulators
            DataGridViewComboBoxCell cmbSim = new DataGridViewComboBoxCell();

            if ((tblSim != null) && (tblSim.Rows.Count > 0))
            {
                foreach (DataRow row in tblSim.Rows)
                {
                    if (gridSimConfigured.Rows.Count > 0)
                    {
                        bool simAlreadConfigured = false;

                        // Verificar se o simulador ja foi configurado
                        foreach (DataGridViewRow gridRow in gridSimConfigured.Rows)
                        {
                            if (gridRow.Cells[0].Value.ToString().Equals(row[0].ToString()))
                            {
                                simAlreadConfigured = true;
                            }
                        }

                        if (!simAlreadConfigured)
                        {
                            cmbSim.Items.Add(row[1]);
                        }
                    }
                    else
                    {
                        cmbSim.Items.Add(row[1]);
                    }
                }
            }

            if (cmbSim.Items.Count == 0)
            {
                cmbSim.Items.Add("[There are no available simulators]");
            }

            gridSimulators[0, 0] = cmbSim;

            if (cmbSim.Items.Count > 0)
            {
                gridSimulators[0, 0].Value = cmbSim.Items[0].ToString();
            }
        }

        private bool AddColumnsToConfigConnection(String simName, String simSerialType)
        {
            try
            {
                gridSimulatorCommConfig.Columns.Clear();

                if (simSerialType.Equals("RS-232"))
                {
                    DataGridViewComboBoxColumn colSerialPort = new DataGridViewComboBoxColumn();
                    colSerialPort.HeaderText = "Serial Port";
                    gridSimulatorCommConfig.Columns.Add(colSerialPort);

                    DataGridViewComboBoxColumn colBaud = new DataGridViewComboBoxColumn();
                    colBaud.HeaderText = "Baud Rate";
                    gridSimulatorCommConfig.Columns.Add(colBaud);

                    DataGridViewComboBoxColumn colDataBits = new DataGridViewComboBoxColumn();
                    colDataBits.HeaderText = "Data Bits";
                    gridSimulatorCommConfig.Columns.Add(colDataBits);

                    DataGridViewComboBoxColumn colParity = new DataGridViewComboBoxColumn();
                    colParity.HeaderText = "Parity";
                    gridSimulatorCommConfig.Columns.Add(colParity);

                    DataGridViewComboBoxColumn colStop = new DataGridViewComboBoxColumn();
                    colStop.HeaderText = "Stop Bits";
                    gridSimulatorCommConfig.Columns.Add(colStop);

                    gridSimulatorCommConfig.Rows.Add();

                    DataGridViewTextBoxCell cellSim = new DataGridViewTextBoxCell();
                    cellSim.Value = simName;
                    gridSimulatorCommConfig[0, 0] = cellSim;

                    DataGridViewComboBoxCell cmbPorts = new DataGridViewComboBoxCell();

                    // Buscar as portas seriais. O numero de rows no grid sera o mesmo numero de portas disponiveis.
                    string[] ports = SerialPort.GetPortNames();

                    if ((ports != null) && (ports.Length > 0))
                    {
                        foreach (string port in ports)
                        {
                            cmbPorts.Items.Add(port);
                        }
                    }

                    if (cmbPorts.Items.Count == 0)
                    {
                        cmbPorts.Items.Add("[There are no available serial ports]");
                    }

                    gridSimulatorCommConfig[0, 0] = cmbPorts;

                    if (cmbPorts.Items.Count > 0)
                    {
                        gridSimulatorCommConfig[0, 0].Value = cmbPorts.Items[0].ToString();
                    }

                    // Preencher a coluna Baud Rate
                    DataGridViewComboBoxCell cmbBaud = new DataGridViewComboBoxCell();
                    cmbBaud.Items.Add("9600");
                    cmbBaud.Items.Add("19200");
                    cmbBaud.Items.Add("38400");
                    cmbBaud.Items.Add("57600");
                    cmbBaud.Items.Add("115200");
                    gridSimulatorCommConfig[1, 0] = cmbBaud;
                    gridSimulatorCommConfig[1, 0].Value = "38400";

                    // Preencher a coluna Data Bits
                    DataGridViewComboBoxCell cmbDataBits = new DataGridViewComboBoxCell();
                    cmbDataBits.Items.Add("5");
                    cmbDataBits.Items.Add("6");
                    cmbDataBits.Items.Add("7");
                    cmbDataBits.Items.Add("8");
                    gridSimulatorCommConfig[2, 0] = cmbDataBits;
                    gridSimulatorCommConfig[2, 0].Value = "8";

                    // Preencher a coluna Parity
                    DataGridViewComboBoxCell cmbParity = new DataGridViewComboBoxCell();
                    cmbParity.Items.Add("Even");
                    cmbParity.Items.Add("Odd");
                    cmbParity.Items.Add("None");
                    gridSimulatorCommConfig[3, 0] = cmbParity;
                    gridSimulatorCommConfig[3, 0].Value = "None";

                    // Preencher a coluna Stop Bits
                    DataGridViewComboBoxCell cmbStop = new DataGridViewComboBoxCell();
                    cmbStop.Items.Add("1");
                    cmbStop.Items.Add("2");
                    gridSimulatorCommConfig[4, 0] = cmbStop;
                    gridSimulatorCommConfig[4, 0].Value = "1";

                    gridSimulatorCommConfig[0, 0].Selected = false;
                }
                else if (simSerialType.Equals("RS-422"))
                {
                    DataGridViewComboBoxColumn colTxChannel = new DataGridViewComboBoxColumn();
                    colTxChannel.HeaderText = "Tx Channel";
                    gridSimulatorCommConfig.Columns.Add(colTxChannel);

                    DataGridViewComboBoxColumn colRxChannel = new DataGridViewComboBoxColumn();
                    colRxChannel.HeaderText = "Rx Channel";
                    gridSimulatorCommConfig.Columns.Add(colRxChannel);

                    DataGridViewComboBoxColumn colBaudRate = new DataGridViewComboBoxColumn();
                    colBaudRate.HeaderText = "Baud Rate";
                    gridSimulatorCommConfig.Columns.Add(colBaudRate);

                    gridSimulatorCommConfig.Rows.Add();

                    DataGridViewComboBoxCell cmbTxChannel = new DataGridViewComboBoxCell();
                    cmbTxChannel.Items.Add("1");
                    cmbTxChannel.Items.Add("2");
                    cmbTxChannel.Items.Add("3");
                    cmbTxChannel.Items.Add("4");

                    gridSimulatorCommConfig[0, 0] = cmbTxChannel;

                    DataGridViewComboBoxCell cmbRxChannel = new DataGridViewComboBoxCell();
                    cmbRxChannel.Items.Add("1");
                    cmbRxChannel.Items.Add("2");
                    cmbRxChannel.Items.Add("3");
                    cmbRxChannel.Items.Add("4");

                    gridSimulatorCommConfig[1, 0] = cmbRxChannel;

                    DataGridViewComboBoxCell cmbBaud = new DataGridViewComboBoxCell();
                    cmbBaud.Items.Add("2400");
                    cmbBaud.Items.Add("4000");
                    cmbBaud.Items.Add("4800");
                    cmbBaud.Items.Add("9600");
                    cmbBaud.Items.Add("19200");
                    cmbBaud.Items.Add("38400");
                    cmbBaud.Items.Add("57600");
                    cmbBaud.Items.Add("115200");

                    gridSimulatorCommConfig[2, 0] = cmbBaud;
                }

                gridSimulatorCommConfig.EndEdit();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddColumnsGridSimulators()
        {
            if (gridSimulators.Columns.Count == 0)
            {
                DataGridViewComboBoxColumn colSim = new DataGridViewComboBoxColumn();
                colSim.HeaderText = "Simulator";
                colSim.Width = 250;
                gridSimulators.Columns.Add(colSim);

                DataGridViewComboBoxColumn colSerialType = new DataGridViewComboBoxColumn();
                colSerialType.HeaderText = "Connection Type";
                gridSimulators.Columns.Add(colSerialType);

                gridSimulators.Rows.Add();
            }
        }

        private bool ValidateConfiguration()
        {
            foreach (DataGridViewRow row in gridSimulators.Rows)
            {
                if ((row.Cells[0].Value == null) || row.Cells[0].Value.ToString().Equals(""))
                {
                    MessageBox.Show("There are empty fields. Correct them and try again.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    return false;
                }

                if ((row.Cells[1].Value == null) || row.Cells[1].Value.ToString().Equals(""))
                {
                    MessageBox.Show("There are empty fields. Correct them and try again.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    return false;
                }
            }

            if ((gridSimulatorCommConfig.Rows.Count > 0) &&
                ((gridSimulatorCommConfig[0, 0].Value == null) ||
                (gridSimulatorCommConfig[0, 0].Value.ToString().Equals(""))))
            {
                MessageBox.Show("There are empty fields. Correct them and try again.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                return false;
            }

            String selectedPortName = gridSimulatorCommConfig[0, 0].Value.ToString();
            
            // Buscar as portas seriais para verificar se ha disponiveis
            string[] ports = SerialPort.GetPortNames();

            if (gridSimulators[1, 0].Value.Equals("RS-232"))
            {
                if ((ports != null) && (ports.Length > 0))
                {
                    bool isUsed = false;

                    //verificar se a porta ja esta em uso ou se ja esta no gridSimConfigured
                    //foreach (string port in ports)
                    {
                        gridSimulatorCommConfig.EndEdit();
                        String port = String.Empty;

                        if (gridSimulatorCommConfig[0, 0].Value != null)
                        {
                            port = gridSimulatorCommConfig[0, 0].Value.ToString();
                        }

                        for (int rowIndex = 0; rowIndex < gridSimConfigured.Rows.Count; rowIndex++)
                        {
                            if (gridSimConfigured[2, rowIndex].Value.ToString().Contains(port))
                            {
                                isUsed = true;
                                gridSimulatorCommConfig[0, 0].Selected = true;
                            }
                        }
                    }

                    // se a porta nao tiver sido adicionada no gridSimConfigured, verifica se esta sendo usada por outra entidade externa.
                    if (!isUsed)
                    {
                        isUsed = CommunicationProtocolSimulator.SerialPortAlreadyInUse(selectedPortName);
                    }

                    if (isUsed)
                    {
                        MessageBox.Show("The serial port '" + selectedPortName + "' already is being used.\n\nSelect other and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        return false;
                    }
                }
            }

            return true;
        }

        private bool StartSimulator(int simId)
        {
            if (allSimulators.ContainsKey(simId))
            {
                try
                {
                    ((CommunicationProtocolSimulator)allSimulators[simId]).availableReceivedMsgHandler += new AvailableReceivedMsgHandler(PrintReceivedMessage);
                    ((CommunicationProtocolSimulator)allSimulators[simId]).availableAnsweredMsgHandler += new AvailableAnsweredMsgHandler(PrintMessageAnswered);
                    ((CommunicationProtocolSimulator)allSimulators[simId]).availableLastMsgSentHandler += new AvailableLastMsgSentHandler(PrintLastMsgSent);
                    
                    // antes de iniciar a execucao, a porta pode ser iniciada por outra entidade, entao deve-se verificar novamente.
                    if (CommunicationProtocolSimulator.SerialPortAlreadyInUse(((CommunicationProtocolSimulator)allSimulators[simId]).RS232.PortName))
                    {
                        MessageBox.Show("The serial port '" + ((CommunicationProtocolSimulator)allSimulators[simId]).RS232.PortName + "' already is being used.\n\nSelect other and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return false;
                    }

                    runningSimulators[simId] = new Thread(((CommunicationProtocolSimulator)allSimulators[simId]).BeginExecution);
                    runningSimulators[simId].Start();

                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error trying to connect the " + allSimulators[simId].SimName + " to " + allSimulators[simId].SimConnType + ", Port " + allSimulators[simId].RS232.PortName + " ! \n\nPlease check your configuration.",
                                    "Connection Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return false;
        }

        private bool StopSimulator(int simId)
        {
            if (allSimulators.ContainsKey(simId))
            {
                try
                {
                    //allSimulators[simId].availableReceivedMsgHandler -= new AvailableReceivedMsgHandler(PrintReceivedMessage);
                    //allSimulators[simId].availableAnsweredMsgHandler -= new AvailableAnsweredMsgHandler(PrintMessageAnswered);
                    //allSimulators[simId].availableLastMsgSentHandler -= new AvailableLastMsgSentHandler(PrintLastMsgSent);

                    ((CommunicationProtocolSimulator)allSimulators[simId]).availableReceivedMsgHandler -= new AvailableReceivedMsgHandler(PrintReceivedMessage);
                    ((CommunicationProtocolSimulator)allSimulators[simId]).availableAnsweredMsgHandler -= new AvailableAnsweredMsgHandler(PrintMessageAnswered);
                    ((CommunicationProtocolSimulator)allSimulators[simId]).availableLastMsgSentHandler -= new AvailableLastMsgSentHandler(PrintLastMsgSent);

                    if (((CommunicationProtocolSimulator)allSimulators[simId]).RS232.IsOpen)
                    {
                        //((CommunicationProtocolSimulator)allSimulators[simId]).RS232.DataReceived -= new SerialDataReceivedEventHandler(((CommunicationProtocolSimulator)allSimulators[simId]).Serial);
                        ((CommunicationProtocolSimulator)allSimulators[simId]).RS232.Close();
                    }
                    

                    //timerToSendRecurrentMessage.availableLastMsgSentHandler -= new AvailableLastMsgSentHandler(AvailableLastMsgSent);
                    //timerToSendRecurrentMessage.SerialRS232.DataReceived -= new SerialDataReceivedEventHandler(SerialRead);
                    //timerToSendRecurrentMessage.SerialRS232.Close();
                    //CommunicationProtocolSimulator.serialPortInUse = false;
                    //timerToSendRecurrentMessage.Enabled = false;


                    if (runningSimulators[simId].IsAlive)
                    {
                        runningSimulators[simId].Abort();
                    }

                    runningSimulators[simId] = null;

                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error trying to disconnect the " + allSimulators[simId].SimName + " to " + allSimulators[simId].SimConnType + ", Port " + allSimulators[simId].RS232.PortName + "'!",
                                    "Connection Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }

        private void PrintLastMsgSent(object sender, AvailableLastMsgSentEventArgs eventArgs)
        {
            foreach (DataGridViewRow row in gridSimConfigured.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(eventArgs.SimId.ToString()))
                {
                    // Last Message Sent
                    gridSimConfigured[8, row.Index].Value = eventArgs.MessageSentTime; // O Time devera ser fornecido pelo evento que disponibiliza a mensagem recebida.

                    // Last Message Sent
                    gridSimConfigured[9, row.Index].Value = Utils.Formatting.ConvertByteArrayToHexString(eventArgs.MessageSent, eventArgs.MessageSent.Length);
                }
            }
        }

        // Foi definido um metodo para cada evento (Received Message e Message Sent) para otimizar o processamento, pois ambos estarao sendo executados em paralelo.
        private void PrintReceivedMessage(object sender, AvailableReceivedMsgEventArgs eventArgs)
        {
            foreach (DataGridViewRow row in gridSimConfigured.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(eventArgs.SimId.ToString()))
                {
                    // Last Received Message Time
                    gridSimConfigured[6, row.Index].Value = eventArgs.ReceivedTime; // O Time devera ser fornecido pelo evento que disponibiliza a mensagem recebida.

                    // Last Received Message
                    gridSimConfigured[7, row.Index].Value = Utils.Formatting.ConvertByteArrayToHexString(eventArgs.ReceivedMessage, eventArgs.ReceivedMessage.Length);
                }
            }
        }

        private void PrintMessageAnswered(object sender, AvailableAnsweredMsgEventArgs eventArgs)
        {
            foreach (DataGridViewRow row in gridSimConfigured.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(eventArgs.SimId.ToString()))
                {
                    // Last Message Answered
                    gridSimConfigured[8, row.Index].Value = eventArgs.TimeAnswered; // O Time devera ser fornecido pelo evento que disponibiliza a mensagem recebida.

                    // Last Message Answered
                    gridSimConfigured[9, row.Index].Value = Utils.Formatting.ConvertByteArrayToHexString(eventArgs.MessageSent, eventArgs.MessageSent.Length);
                }
            }
        }

        #endregion

        #region Eventos de Interface Grafica

        private void gridSimulators_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if ((e.FormattedValue == null) || (e.FormattedValue.ToString().Equals("")))
                {
                    return;
                }

                if ((gridSimulators[0, e.RowIndex].Value != null) &&
                    (!gridSimulators[0, 0].Value.ToString().Equals("")))
                {
                    if ((gridSimulators[0, 0].Value.ToString().Contains("[There are no ")))
                    {
                        MessageBox.Show("There are no available simulator to setup.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        String simName = gridSimulators[0, e.RowIndex].Value.ToString();
                        String simSerialType = e.FormattedValue.ToString();
                        
                        if (AddColumnsToConfigConnection(simName, simSerialType))
                        {
                            btFinalizeConfig.Enabled = true;
                        }
                    }
                }

                gridSimulators.EndEdit();
            }
            else if (e.ColumnIndex == 0)
            {
                if ((e.FormattedValue == null) || (e.FormattedValue.ToString().Equals("")))
                {
                    return;
                }

                if ((gridSimulators[1, e.RowIndex].Value != null) &&
                    (!gridSimulators[1, e.RowIndex].Value.ToString().Equals("")) &&
                    (!gridSimulators[0, e.RowIndex].Value.ToString().Contains("[There are no ")))
                {
                    if (gridSimulatorCommConfig.Columns.Count == 0)
                    {
                        String simName = e.FormattedValue.ToString();
                        String simSerialType = gridSimulators[1, e.RowIndex].Value.ToString();

                        if (AddColumnsToConfigConnection(simName, simSerialType))
                        {
                            btFinalizeConfig.Enabled = true;
                        }
                    }
                }

                gridSimulators.EndEdit();
            }
        }

        private void btNewSimulator_Click(object sender, EventArgs e)
        {
            if (btNewSimulator.Text.Contains("Set "))
            {
                AddSimulatorToConfiguration();
                gridSimulatorCommConfig.Columns.Clear();
                btNewSimulator.Text = "Cancel Communication Settings";
            }
            else
            {
                gridSimulators.Columns.Clear();
                gridSimulatorCommConfig.Columns.Clear();
                btNewSimulator.Text = "Set Communication Settings";
                btFinalizeConfig.Enabled = false;
            }
        }

        private void btFinalizeConfig_Click(object sender, EventArgs e)
        {
            if (!ValidateConfiguration())
            {
                return;
            }

            gridSimConfigured.Rows.Add();

            String sql = "select sim_id from simulators where sim_name = '" + gridSimulators.Rows[0].Cells[0].Value.ToString() + "'";
            //Simulator Id
            gridSimConfigured[0, (gridSimConfigured.Rows.Count - 1)].Value = DbInterface.ExecuteScalar(sql);
            // Simulator Name
            gridSimConfigured[1, (gridSimConfigured.Rows.Count - 1)].Value = gridSimulators.Rows[0].Cells[0].Value.ToString();
            // Conn Type
            String connType = gridSimulators.Rows[0].Cells[1].Value.ToString();
            String port = gridSimulatorCommConfig[0, 0].Value.ToString();
            String baudRate = gridSimulatorCommConfig[1, 0].Value.ToString();

            if (connType.Equals("RS-232"))
            {
                gridSimConfigured[2, (gridSimConfigured.Rows.Count - 1)].Value = connType + " - Port " + port + " - @" + baudRate + " bps";
            }
            else
            {
                gridSimConfigured[2, (gridSimConfigured.Rows.Count - 1)].Value = connType;
            }

            gridSimConfigured[2, (gridSimConfigured.Rows.Count - 1)].Style.ForeColor = Color.Blue;

            // Status
            gridSimConfigured[3, (gridSimConfigured.Rows.Count - 1)].Value = "START";
            // Execution
            gridSimConfigured[5, (gridSimConfigured.Rows.Count - 1)].Value = "NOT EXECUTED";
            gridSimConfigured.Rows[(gridSimConfigured.Rows.Count - 1)].Cells[5].Style.Font = new Font(gridSimConfigured.Font, FontStyle.Bold);
            gridSimConfigured.Rows[(gridSimConfigured.Rows.Count - 1)].Cells[5].Style.BackColor = Color.PeachPuff;
            // Clear
            gridSimConfigured[gridSimConfigured.Columns.Count - 1, (gridSimConfigured.Rows.Count - 1)].Value = "Clear Settings";

            // Carregar a estrutura de configuracao
            simulator = new CommunicationProtocolSimulator();
            sql = "select sim_id from simulators where sim_name = '" + gridSimulators.CurrentRow.Cells[0].Value.ToString() + "'";
            simulator.SimId = (int)DbInterface.ExecuteScalar(sql);
            simulator.SimName = gridSimulators.CurrentRow.Cells[0].Value.ToString();
            simulator.SimConnType = gridSimulators.CurrentRow.Cells[1].Value.ToString();

            if (simulator.SimConnType.Equals("RS-232"))
            {
                SerialPort serialRs232 = new SerialPort();
                serialRs232.PortName = gridSimulatorCommConfig.Rows[0].Cells[0].Value.ToString();
                serialRs232.BaudRate = int.Parse(gridSimulatorCommConfig.Rows[0].Cells[1].Value.ToString());
                serialRs232.DataBits = int.Parse(gridSimulatorCommConfig.Rows[0].Cells[2].Value.ToString());

                if (gridSimulatorCommConfig.Rows[0].Cells[3].Value.ToString().Equals(Parity.None.ToString()))
                {
                    serialRs232.Parity = Parity.None;
                }
                else if (gridSimulatorCommConfig.Rows[0].Cells[3].Value.ToString().Equals(Parity.Even.ToString()))
                {
                    serialRs232.Parity = Parity.Even;
                }
                else if (gridSimulatorCommConfig.Rows[0].Cells[3].Value.ToString().Equals(Parity.Odd.ToString()))
                {
                    serialRs232.Parity = Parity.Odd;
                }

                if (gridSimulatorCommConfig.Rows[0].Cells[4].Value.ToString().Equals("1"))
                {
                    serialRs232.StopBits = StopBits.One;
                }
                else if (gridSimulatorCommConfig.Rows[0].Cells[4].Value.ToString().Equals("2"))
                {
                    serialRs232.StopBits = StopBits.Two;
                }

                simulator.RS232 = serialRs232;

                // Carregar as mensagens
                simulator.LoadMessages();

                // Adicionar o simulador ah lista de simuladores
                allSimulators.Add(simulator.SimId, simulator);
            }

            Thread threadSimulator = null;
            runningSimulators.Add(simulator.SimId, threadSimulator);

            gridSimulators.Columns.Clear();
            gridSimulatorCommConfig.Columns.Clear();
            btFinalizeConfig.Enabled = false;
            btNewSimulator.Text = "Set Communication Settings";
            btNewSimulator.Enabled = true;

            if (gridSimConfigured.RowCount > 1)
            {
                btStartAllSimulators.Enabled = true;
            }

            gridSimConfigured.ClearSelection();
            gridSimConfigured.Rows[gridSimConfigured.Rows.Count - 1].Selected = true;
        }

        private void btStartAllSimulators_Click(object sender, EventArgs e)
        {
            if (btStartAllSimulators.Text.Contains("Start"))
            {
                foreach (DataGridViewRow row in gridSimConfigured.Rows)
                {
                    gridSimConfigured.CurrentCell = gridSimConfigured[3, row.Index];

                    if (gridSimConfigured.CurrentCell.Value.ToString().Equals("START"))
                    {
                        gridSimConfigured_CellContentClick(null, new DataGridViewCellEventArgs(3, row.Index));
                    }
                }

                gridSimConfigured.ClearSelection();
                btStartAllSimulators.Text = "Stop All Simulators Running";
            }
            else
            {
                foreach (DataGridViewRow row in gridSimConfigured.Rows)
                {
                    gridSimConfigured.CurrentCell = gridSimConfigured[3, row.Index];

                    if (gridSimConfigured.CurrentCell.Value.ToString().Equals("STOP"))
                    {
                        gridSimConfigured_CellContentClick(null, new DataGridViewCellEventArgs(3, row.Index));
                    }
                }

                btStartAllSimulators.Text = "Start All Stopped Simulators";
            }
        }

        private void FrmSimulatorsExecution_DockStateChanged(object sender, EventArgs e)
        {
            if (DockState == DockState.Float)
            {
                FloatPane.FloatWindow.ClientSize = new Size(1232, 714);
            }
        }

        private void gridSimConfigured_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == 3)
            {
                if (gridSimConfigured.CurrentCell.Value.ToString().Equals("START"))
                {
                    if (StartSimulator(int.Parse(gridSimConfigured[0, gridSimConfigured.CurrentRow.Index].Value.ToString())))
                    {
                        gridSimConfigured.CurrentCell.Value = "STOP";
                        DateTime timeNow = (DateTime)DbInterface.ExecuteScalar("select getDate()");
                        gridSimConfigured.Rows[e.RowIndex].Cells[4].Value = timeNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                        gridSimConfigured.Rows[e.RowIndex].Cells[5].Value = "RUNNING";

                        gridSimConfigured.Rows[e.RowIndex].Cells[5].Style.Font = new Font(gridSimConfigured.Font, FontStyle.Regular);
                        gridSimConfigured.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.LightGreen;
                    }
                }
                else
                {
                    if (!StopSimulator(int.Parse(gridSimConfigured[0, gridSimConfigured.CurrentRow.Index].Value.ToString())))
                    {
                        return;
                    }

                    gridSimConfigured.CurrentCell.Value = "START";
                    DateTime timeNow = (DateTime)DbInterface.ExecuteScalar("select getDate()");
                    gridSimConfigured.Rows[e.RowIndex].Cells[5].Value = timeNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

                    gridSimConfigured.Rows[e.RowIndex].Cells[5].Style.Font = new Font(gridSimConfigured.Font, FontStyle.Regular);
                    gridSimConfigured.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.PeachPuff;

                }
            }
            else if (e.ColumnIndex == (gridSimConfigured.Columns.Count - 1))
            {
                if (gridSimConfigured[5, e.RowIndex].Value != null &&
                    gridSimConfigured[5, e.RowIndex].Value.ToString().Equals("RUNNING"))
                {
                    MessageBox.Show("The '" + gridSimConfigured.CurrentRow.Cells[1].Value.ToString() + "' simulator is running.\n\nFinalize the execution and try again.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to delete the settings of '" + gridSimConfigured.CurrentRow.Cells[1].Value.ToString() + "' ?",
                                    "Please Confirm Deletion",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                
                if (gridSimConfigured[2, e.RowIndex].Value.ToString().Contains("RS-232"))
                {
                    int simId = int.Parse(gridSimConfigured[0, gridSimConfigured.CurrentRow.Index].Value.ToString());

                    if (!runningSimulators.Remove(simId))
                    {
                        MessageBox.Show("Error to remove this setting",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!allSimulators.Remove(simId))
                    {
                        MessageBox.Show("Error to remove this setting",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                gridSimConfigured.Rows.RemoveAt(gridSimConfigured.CurrentRow.Index);

                if (gridSimulators.RowCount > 0)
                {
                    // Atualizar o combo
                    String simName = String.Empty;

                    if ((gridSimulators.RowCount == 1) && (gridSimulators[0, 0].Value != null))
                    {
                        simName = gridSimulators[0, 0].Value.ToString();
                    }

                    FillComboSimulators();

                    if (!simName.Equals(String.Empty) && (!simName.Contains("[There are no ")))
                    {
                        gridSimulators[0, 0].Value = simName;
                    }

                    gridSimulators[1, 0].Value = "";
                }
            }

            // Verificar se existe row e se todos estao conectados. Se sim, desabilita o botao 'Start All Stopped Simulators'.
            bool hasSimToStart = false;

            foreach (DataGridViewRow row in gridSimConfigured.Rows)
            {
                if (gridSimConfigured[3, row.Index].Value.ToString().Equals("START"))
                {
                    hasSimToStart = true;
                    break;
                }
            }

            if (hasSimToStart)
            {
                btStartAllSimulators.Text = "Start All Stopped Simulators";
            }
            else
            {
                btStartAllSimulators.Text = "Stop All Simulators Running";
            }

            // Se nao existem mais nenhum row, desabilitar botao Start All
            if (gridSimConfigured.RowCount <= 1)
            {
                btStartAllSimulators.Enabled = false;
                btStartAllSimulators.Text = "Start All Stopped Simulators";
            }
        }

        #endregion
    }
}

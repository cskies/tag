/**
 * @file 	    FrmSimulatorsManagement.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    13/07/2012
 * @note	    Modificado em 06/08/2012 por Thiago.
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
using System.Collections;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSimulatorsManagement
     * Esta classe eh a responsavel pela criacao de simuladores de forma generica para simulacao dos equipamentos (sensores e atuadores) para o OBC.
     **/
    public partial class FrmSimulatorsManagement : DockContent
    {
        #region Atributos internos

        private enum Mode { browsing, inserting, editing, report };
        private Mode currentMode = Mode.browsing;
        private int codeSelected = 0;
        private DbSimulator dbSimulator = new DbSimulator();

        #endregion

        #region Construtor

        public FrmSimulatorsManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region Metodos privados

        private void ChangeMode(Mode newMode)
        {
            switch (newMode)
            {
                case Mode.browsing:
                    {
                        RefreshGrid();

                        if (gridSimulators.RowCount == 0)
                        {
                            btEdit.Enabled = false;
                            btDelete.Enabled = false;

                            codeSelected = 0;
                        }
                        else
                        {
                            btEdit.Enabled = true;
                            btDelete.Enabled = true;

                            // Selecionar o item no grid
                            if (codeSelected != 0)
                            {
                                foreach (DataGridViewRow row in gridSimulators.Rows)
                                {
                                    if (codeSelected == int.Parse(row.Cells[0].Value.ToString()))
                                    {
                                        gridSimulators.Rows[row.Index].Cells[0].Selected = true;
                                        break;
                                    }
                                }

                                codeSelected = 0;
                            }
                            else
                            {
                                if (gridSimulators.RowCount > 0)
                                {
                                    gridSimulators.Focus();
                                    gridSimulators[0, 0].Selected = true;
                                }
                            }
                        }

                        btNew.Enabled = true;
                        btConfirm.Enabled = false;
                        btCancel.Enabled = false;
                        btRefresh.Enabled = true;

                        currentMode = newMode;
                        tabControl1.SelectedIndex = 0;
                        gridSimulators.Focus();

                        break;
                    }
                case Mode.editing:
                case Mode.inserting:
                    {
                        currentMode = newMode;

                        if (gridSimulators.RowCount > 0)
                        {
                            codeSelected = int.Parse(gridSimulators[0, gridSimulators.CurrentRow.Index].Value.ToString());
                        }
                        
                        if (newMode == Mode.inserting)
                        {
                            String sql = "select isnull((max(sim_id) + 1), 1) as maxSimId from simulators";
                            object nextSimId = DbInterface.ExecuteScalar(sql);
                            txtSimId.Text = nextSimId.ToString();
                            txtSimName.Text = "";
                            txtSimDescription.Text = "";

                            // Formatar os grids das mensagens
                            gridMsgToSend.Rows.Clear();
                            gridMsgToReceive.Rows.Clear();

                            txtSimName.Focus();
                        }
                        else // editing
                        {
                            if (btEdit.Enabled == false)
                            {
                                return;
                            }

                            txtSimId.Text = gridSimulators[0, gridSimulators.CurrentRow.Index].Value.ToString();
                            txtSimName.Text = gridSimulators[1, gridSimulators.CurrentRow.Index].Value.ToString();
                            txtSimDescription.Text = gridSimulators[2, gridSimulators.CurrentRow.Index].Value.ToString();
                            txtSimName.Focus();

                            // Buscar agora as mensagens
                            String sql = @"select msg_id, 
                                                  msg_name, 
                                                  msg_to_send, 
                                                  delay_to_send_again 
                                           from sim_msgs_to_send 
                                           where sim_id = " + txtSimId.Text;

                            DataTable tblMsgsToSend = DbInterface.GetDataTable(sql);
                            int rowIndexToSend = 0;
                            gridMsgToSend.Rows.Clear();

                            foreach (DataRow row in tblMsgsToSend.Rows)
                            {
                                gridMsgToSend.Rows.Add(1);
                                gridMsgToSend[0, rowIndexToSend].Value = row[0];
                                gridMsgToSend[1, rowIndexToSend].Value = row[1];
                                byte[] msgToSend = (byte[])row[2];
                                gridMsgToSend[2, rowIndexToSend].Value = Utils.Formatting.ConvertByteArrayToHexString(msgToSend, msgToSend.Length);
                                gridMsgToSend[3, rowIndexToSend].Value = row[3];
                                rowIndexToSend++;
                            }

                            sql = @"select msg_id, 
                                           msg_name, 
                                           msg_to_receive, 
                                           check_full_msg, 
                                           delay_to_answer, 
                                           msg_to_answer, 
                                           repeat_answer, 
                                           answer_repetition_interval 
                                    from sim_msgs_to_receive 
                                    where sim_id = " + txtSimId.Text;

                            DataTable tblMsgsToReceive = DbInterface.GetDataTable(sql);
                            int rowIndexToReceive = 0;
                            gridMsgToReceive.Rows.Clear();

                            foreach (DataRow row in tblMsgsToReceive.Rows)
                            {
                                gridMsgToReceive.Rows.Add(1);
                                gridMsgToReceive[0, rowIndexToReceive].Value = row[0];
                                gridMsgToReceive[1, rowIndexToReceive].Value = row[1];
                                byte[] msgToReceive = (byte[])row[2];
                                gridMsgToReceive[2, rowIndexToReceive].Value = Utils.Formatting.ConvertByteArrayToHexString(msgToReceive, msgToReceive.Length);
                                gridMsgToReceive[3, rowIndexToReceive].Value = row[3];
                                gridMsgToReceive[4, rowIndexToReceive].Value = row[4];
                                byte[] msgToAnswer = (byte[])row[5];
                                gridMsgToReceive[5, rowIndexToReceive].Value = Utils.Formatting.ConvertByteArrayToHexString(msgToAnswer, msgToAnswer.Length);
                                gridMsgToReceive[6, rowIndexToReceive].Value = row[6];
                                gridMsgToReceive[7, rowIndexToReceive].Value = row[7];
                                rowIndexToReceive++;
                            }
                        }

                        btNew.Enabled = false;
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btConfirm.Enabled = true;
                        btCancel.Enabled = true;
                        btRefresh.Enabled = false;

                        tabControl1.SelectedIndex = 1;
                        
                        break;
                    }
            }
        }

        private void RefreshGrid()
        {
            String sql = "select sim_id as 'Simulator Id', sim_name as 'Simulator Name', sim_desc as 'Simulator Description' from simulators";
            gridSimulators.Columns.Clear();
            gridSimulators.DataSource = DbInterface.GetDataTable(sql);
            gridSimulators.Columns[0].Width = 90;
            gridSimulators.Columns[1].Width = 350;
            gridSimulators.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private bool ValidateSimulatorData()
        {
            try
            {
                gridMsgToSend.EndEdit();
                gridMsgToReceive.EndEdit();

                if (txtSimName.Text.Trim().Equals(""))
                {
                    MessageBox.Show("The field 'Simulator Name' is empty!\n\nFill it and try again.",
                                    "Inconsistent data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    txtSimName.Focus();
                    return false;
                }
                
                foreach (DataGridViewRow row in gridMsgToSend.Rows)
                {
                    if (row.Index == (gridMsgToSend.RowCount - 1))
                    {
                        break;
                    }

                    if ((row.Cells[1].Value == null) ||
                        (row.Cells[1].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to send!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToSend.Focus();
                        gridMsgToSend.ClearSelection();
                        gridMsgToSend[1, row.Index].Selected = true;
                        return false;
                    }

                    if ((row.Cells[2].Value == null) ||
                        (row.Cells[2].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to send!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToSend.Focus();
                        gridMsgToSend.ClearSelection();
                        gridMsgToSend[2, row.Index].Selected = true;
                        return false;
                    }
                    else
                    {
                        byte[] msg = Utils.Formatting.HexStringToByteArray(row.Cells[2].Value.ToString().Trim());

                        if (msg == null)
                        {
                            MessageBox.Show("One message to send is not valid!\nThe correct format is hexadecimal.\n\nCorrect it and try again.",
                                            "Inconsistent data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridMsgToSend.Focus();
                            gridMsgToSend.ClearSelection();
                            gridMsgToSend[2, row.Index].Selected = true;
                            return false;
                        }
                        else
                        {
                            bool isEmpty = true;

                            for (int i = 0; i < msg.Length; i++)
                            {
                                if (msg[i] != 0x0)
                                {
                                    isEmpty = false;
                                }
                            }

                            if (isEmpty)
                            {
                                MessageBox.Show("One message to send is not valid!\nThe message cannot be zero.\n\nCorrect it and try again.",
                                                "Inconsistent data",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);
                                                                
                                gridMsgToSend.Focus();
                                gridMsgToSend.ClearSelection();
                                gridMsgToSend[2, row.Index].Selected = true;
                                return false;
                            }
                        }
                    }

                    if ((row.Cells[3].Value == null) ||
                        (row.Cells[3].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to send!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToSend.Focus();
                        gridMsgToSend.ClearSelection();
                        gridMsgToSend[3, row.Index].Selected = true;
                        return false;
                    }
                    else
                    {
                        int delayToSendAgain = -1;
                        bool isNumeric = int.TryParse(row.Cells[3].Value.ToString().Trim(), out delayToSendAgain);

                        if (!isNumeric)
                        {
                            MessageBox.Show("One delay to send the message again is invalid!\n\nCorrect it and try again.",
                                            "Inconsistent data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridMsgToSend.Focus();
                            gridMsgToSend.ClearSelection();
                            gridMsgToSend[3, row.Index].Selected = true;
                            return false;
                        }
                    }
                }

                foreach (DataGridViewRow row in gridMsgToReceive.Rows)
                {
                    if (row.Index == (gridMsgToReceive.RowCount - 1))
                    {
                        break;
                    }

                    if ((row.Cells[1].Value == null) ||
                        (row.Cells[1].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to receive!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToReceive.Focus();
                        gridMsgToReceive.ClearSelection();
                        gridMsgToReceive[1, row.Index].Selected = true;
                        return false;
                    }

                    if ((row.Cells[2].Value == null) ||
                        (row.Cells[2].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to receive!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToReceive.Focus();
                        gridMsgToReceive.ClearSelection();
                        gridMsgToReceive[2, row.Index].Selected = true;
                        return false;
                    }
                    else
                    {
                        byte[] msg = Utils.Formatting.HexStringToByteArray(row.Cells[2].Value.ToString().Trim());

                        if (msg == null)
                        {
                            MessageBox.Show("One message to receive is invalid!\nThe correct format is hexadecimal.\n\nCorrect it and try again.",
                                            "Inconsistent data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridMsgToReceive.Focus();
                            gridMsgToReceive.ClearSelection();
                            gridMsgToReceive[2, row.Index].Selected = true;
                            return false;
                        }
                        else
                        {
                            bool isEmpty = true;

                            for (int i = 0; i < msg.Length; i++)
                            {
                                if (msg[i] != 0x0)
                                {
                                    isEmpty = false;
                                }
                            }

                            if (isEmpty)
                            {
                                MessageBox.Show("One message to receive is not valid!\nThe message cannot be zero.\n\nCorrect it and try again.",
                                                "Inconsistent data",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                gridMsgToReceive.Focus();
                                gridMsgToReceive.ClearSelection();
                                gridMsgToReceive[2, row.Index].Selected = true;
                                return false;
                            }
                        }
                    }

                    if (row.Cells[3].Value == null)
                    {
                        gridMsgToReceive[3, row.Index].Value = false;
                    }

                    if ((row.Cells[4].Value == null) ||
                        (row.Cells[4].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to receive!\n\nFill it and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToReceive.Focus();
                        gridMsgToReceive.ClearSelection();
                        gridMsgToReceive[4, row.Index].Selected = true;
                        return false;
                    }
                    else
                    {
                        int delayToAnswer = -1;
                        bool isNumeric = int.TryParse(row.Cells[4].Value.ToString().Trim(), out delayToAnswer);

                        if (!isNumeric)
                        {
                            MessageBox.Show("One delay to answer the message is invalid!\n\nCorrect it and try again.",
                                            "Inconsistent data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridMsgToReceive.Focus();
                            gridMsgToReceive.ClearSelection();
                            gridMsgToReceive[4, row.Index].Selected = true;
                            return false;
                        }
                    }

                    if ((row.Cells[5].Value == null) ||
                        (row.Cells[5].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to receive!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToReceive.Focus();
                        gridMsgToReceive.ClearSelection();
                        gridMsgToReceive[5, row.Index].Selected = true;
                        return false;
                    }
                    else
                    {
                        byte[] msg = Utils.Formatting.HexStringToByteArray(row.Cells[5].Value.ToString().Trim());

                        if (msg == null)
                        {
                            MessageBox.Show("One message to answer is invalid!\nThe correct format is hexadecimal.\n\nCorrect it and try again.",
                                            "Inconsistent data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridMsgToReceive.Focus();
                            gridMsgToReceive.ClearSelection();
                            gridMsgToReceive[5, row.Index].Selected = true;
                            return false;
                        }
                        else
                        {
                            bool isEmpty = true;

                            for (int i = 0; i < msg.Length; i++)
                            {
                                if (msg[i] != 0x0)
                                {
                                    isEmpty = false;
                                }
                            }

                            if (isEmpty)
                            {
                                MessageBox.Show("One message to answer is not valid!\nThe message cannot be zero.\n\nCorrect it and try again.",
                                                "Inconsistent data",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                gridMsgToReceive.Focus();
                                gridMsgToReceive.ClearSelection();
                                gridMsgToReceive[5, row.Index].Selected = true;
                                return false;
                            }
                        }
                    }

                    if (row.Cells[6].Value == null)
                    {
                        gridMsgToReceive[6, row.Index].Value = false;
                    }

                    if ((row.Cells[7].Value == null) ||
                        (row.Cells[7].Value.ToString().Trim().Equals("")))
                    {
                        MessageBox.Show("There are empty fields in the messages to receive!\n\nFill them and try again.",
                                        "Inconsistent data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        gridMsgToReceive.Focus();
                        gridMsgToReceive.ClearSelection();
                        gridMsgToReceive[7, row.Index].Selected = true;
                        return false;
                    }
                    else
                    {
                        int repetitionInterval = -1;
                        bool isNumeric = int.TryParse(row.Cells[7].Value.ToString().Trim(), out repetitionInterval);

                        if (!isNumeric)
                        {
                            MessageBox.Show("One repetition interval to answer a message received is invalid!\n\nCorrect it and try again.",
                                            "Inconsistent data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            gridMsgToReceive.Focus();
                            gridMsgToReceive.ClearSelection();
                            gridMsgToReceive[7, row.Index].Selected = true;
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool InsertSimulator()
        {
            dbSimulator.SimId = int.Parse(txtSimId.Text.Trim());
            dbSimulator.SimName = txtSimName.Text.Trim();
            dbSimulator.Description = txtSimDescription.Text.Trim();
            dbSimulator.MessagesToSend = ReadMessagesToSend();
            dbSimulator.MessagesToReceive = ReadMessagesToReceive();
            
            return dbSimulator.Insert();
        }

        private bool UpdateSimulator()
        {
            dbSimulator.SimId = int.Parse(txtSimId.Text.Trim());
            dbSimulator.SimName = txtSimName.Text.Trim();
            dbSimulator.Description = txtSimDescription.Text.Trim();
            dbSimulator.MessagesToSend = ReadMessagesToSend();
            dbSimulator.MessagesToReceive = ReadMessagesToReceive();

            return dbSimulator.Update();
        }

        private bool DeleteSimulator()
        {
            dbSimulator.SimId = int.Parse(gridSimulators[0, gridSimulators.CurrentRow.Index].Value.ToString());
            return dbSimulator.Delete();
        }

        private List<DbSimulatorMsgToSend> ReadMessagesToSend()
        {
            List<DbSimulatorMsgToSend> msgsToSend = null;
            DbSimulatorMsgToSend msg = null;

            if (gridMsgToSend.RowCount != 1)
            {
                msgsToSend = new List<DbSimulatorMsgToSend>();

                foreach (DataGridViewRow row in gridMsgToSend.Rows)
                {
                    msg = new DbSimulatorMsgToSend();
                    msg.MessageId = row.Index;
                    msg.Name = gridMsgToSend[1, row.Index].Value.ToString().Trim();
                    msg.MessageToSend = Utils.Formatting.HexStringToByteArray(gridMsgToSend[2, row.Index].Value.ToString().Trim());
                    msg.DelayToSendAgain = int.Parse(gridMsgToSend[3, row.Index].Value.ToString().Trim());
                    msgsToSend.Add(msg);

                    if ((row.Index + 1) == (gridMsgToSend.RowCount - 1))
                    {
                        break;
                    }
                }
            }

            return msgsToSend;
        }

        private List<DbSimulatorMsgToReceive> ReadMessagesToReceive()
        {
            List<DbSimulatorMsgToReceive> msgsToReceive = null;
            DbSimulatorMsgToReceive msg = null;

            if (gridMsgToReceive.RowCount != 1)
            {
                msgsToReceive = new List<DbSimulatorMsgToReceive>();

                foreach (DataGridViewRow row in gridMsgToReceive.Rows)
                {
                    msg = new DbSimulatorMsgToReceive();

                    msg.MessageId = row.Index;
                    msg.Name = gridMsgToReceive[1, row.Index].Value.ToString();
                    msg.MessageToReceive = Utils.Formatting.HexStringToByteArray(gridMsgToReceive[2, row.Index].Value.ToString());
                    msg.CheckFullMessage = bool.Parse(gridMsgToReceive[3, row.Index].Value.ToString());
                    msg.DelayToAnswer = int.Parse(gridMsgToReceive[4, row.Index].Value.ToString());
                    msg.MessageToAnswer = Utils.Formatting.HexStringToByteArray(gridMsgToReceive[5, row.Index].Value.ToString());
                    msg.RepeatAnswer = bool.Parse(gridMsgToReceive[6, row.Index].Value.ToString());
                    msg.RepetitionInterval = int.Parse(gridMsgToReceive[7, row.Index].Value.ToString());
                    msgsToReceive.Add(msg);

                    if ((row.Index + 1) == (gridMsgToReceive.RowCount - 1))
                    {
                        break;
                    }
                }
            }

            return msgsToReceive;
        }

        #endregion

        #region Eventos de interface grafica

        private void FrmSimulatorsManagement_Load(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

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
            if (MessageBox.Show("Are you sure you want to delete the Simulator " + gridSimulators.CurrentRow.Cells[0].Value.ToString() + ", '" + gridSimulators.CurrentRow.Cells[1].Value.ToString() + "' ?",
                                "Please Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            int indexSelected = gridSimulators.CurrentRow.Index;

            if (!DeleteSimulator())
            {
                return;
            }
            else
            {
                ChangeMode(Mode.browsing);

                if (gridSimulators.RowCount == 0)
                {
                    return;
                }

                // Setar o row seguinte aqui
                if (indexSelected == gridSimulators.RowCount)
                {
                    gridSimulators.Rows[indexSelected - 1].Cells[0].Selected = true;
                }
                else
                {
                    gridSimulators.Rows[indexSelected].Cells[0].Selected = true;
                }
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateSimulatorData())
            {
                return;
            }

            if (currentMode == Mode.inserting)
            {
                if (!InsertSimulator())
                {
                    return; // sai sem voltar ao modo browsing
                }

                codeSelected = int.Parse(txtSimId.Text);

                // Como eh insercao, continua no mesmo modo
                ChangeMode(Mode.inserting);
            }
            else
            {
                if (!UpdateSimulator())
                {
                    return; // sai sem voltar ao modo browsing
                }

                codeSelected = int.Parse(txtSimId.Text);

                // Volta ao modo browsing
                ChangeMode(Mode.browsing);
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

        private void gridMsgToReceive_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            gridMsgToReceive[4, e.RowIndex].Value = "0";
            gridMsgToReceive[7, e.RowIndex].Value = "0";
            gridMsgToReceive[7, e.RowIndex].ReadOnly = true;
            gridMsgToReceive[7, e.RowIndex].Value = "0";
        }

        private void gridMsgToSend_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            gridMsgToSend[3, e.RowIndex].Value = "0";
        }

        private void gridMsgToReceive_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gridMsgToReceive[6, e.RowIndex].Value != null)
            {
                bool repeatAnswer = bool.Parse(gridMsgToReceive[6, e.RowIndex].Value.ToString());
                gridMsgToReceive[7, e.RowIndex].ReadOnly = !repeatAnswer;

                if (gridMsgToReceive[7, e.RowIndex].ReadOnly)
                {
                    gridMsgToReceive[7, e.RowIndex].Value = "0";
                }
            }
        }

        private void gridMsgToReceive_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    if (!(gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value.ToString().Length % 2 == 0))
                    {
                        gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value = "0" + gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                    else
                    {
                        gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value = gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                }
            }

            if (e.ColumnIndex == 5)
            {
                if (gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    if (!(gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value.ToString().Length % 2 == 0))
                    {
                        gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value = "0" + gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                    else
                    {
                        gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value = gridMsgToReceive[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                }
            }
            
            if ((gridMsgToReceive[4, e.RowIndex].Value == null) ||
                (gridMsgToReceive[4, e.RowIndex].Value.ToString().Trim().Equals("")))
            {
                gridMsgToReceive[4, e.RowIndex].Value = "0";
            }

            if ((gridMsgToReceive[7, e.RowIndex].Value == null) ||
                (gridMsgToReceive[7, e.RowIndex].Value.ToString().Trim().Equals("")))
            {
                gridMsgToReceive[7, e.RowIndex].Value = "0";
            }
        }

        private void gridMsgToSend_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (gridMsgToSend[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    if (!(gridMsgToSend[e.ColumnIndex, e.RowIndex].Value.ToString().Length % 2 == 0))
                    {
                        gridMsgToSend[e.ColumnIndex, e.RowIndex].Value = "0" + gridMsgToSend[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                    else
                    {
                        gridMsgToSend[e.ColumnIndex, e.RowIndex].Value = gridMsgToSend[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                }
            }

            if ((gridMsgToSend[3, e.RowIndex].Value == null) ||
                (gridMsgToSend[3, e.RowIndex].Value.ToString().Trim().Equals("")))
            {
                gridMsgToSend[3, e.RowIndex].Value = "0";
            }
        }

        private void gridSimulators_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void FrmSimulatorsManagement_DockStateChanged(object sender, EventArgs e)
        {
            if (DockState == DockState.Float)
            {
                FloatPane.FloatWindow.ClientSize = new Size(1221, 849);
            }
        }

        private void FrmSimulatorsManagement_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid antes de chamar o Edit.
            if ((gridSimulators.Focused) && (e.KeyCode == Keys.Enter))
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
                else if (((e.KeyCode == Keys.Enter) || ((e.KeyCode == Keys.Space))) && (btEdit.Enabled))
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
                    if (txtSimDescription.Focused)
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

        #endregion
    }
}

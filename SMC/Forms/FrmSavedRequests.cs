/**
 * @file 	    FrmSavedRequests.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    07/12/2009
 * @note	    Modificado em 24/06/2013 por Thiago.
 * @note	    Modificado em 02/03/2015 por Conrado e Thiago.
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
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSavedRequests
     * Formulario usado para listar os pacotes salvos.
     **/
    public partial class FrmSavedRequests : DockContent
    {
        #region Atributos privados

        private FrmRequestsComposition frmComposition = null;
        private FrmConnectionWithEgse frmConnection = null;
        private MdiMain mdiMain = null;

        #endregion

        #region Construtor

        public FrmSavedRequests(MdiMain mdi)
        {
            InitializeComponent();
            mdiMain = mdi;

            if (mdiMain != null)
            {
                frmComposition = mdiMain.FormRequestsComposition;
                frmConnection = mdiMain.FormConnectionWithEgse;
            }

            RefreshGrid();
            btSendTc.Enabled = false;

            if (frmConnection != null)
            {
                frmConnection.FormSavedRequest = this;

                if (frmConnection.Connected &&
                   (frmConnection.rbNamedPipe.Checked ||
                    frmConnection.rbSerial.Checked ||
                    frmConnection.rbRs422.Checked))
                {
                    btSendTc.Enabled = true;
                }
            }

            if (gridDatabase.RowCount == 0)
            {
                btDeleteTc.Enabled = false;
                btLoadTC.Enabled = false;
                btSendTc.Enabled = false;
            }
            else
            {
                btDeleteTc.Enabled = true;
                btLoadTC.Enabled = true;
            }
        }

        #endregion

        #region Propriedades

        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            set
            {
                frmConnection = value;
            }
        }

        #endregion

        #region Metodos privados

        private void RefreshGrid()
        {
            //02-02-15 Conrado Moura - Correção do BUG SIA_OBC_SW_BUG-60
            //ALTERADO (select alterado, para inserção da coluna APID.)

            string sql = @"select 
                              distinct(a.saved_request_id) as 'Saved Request ID',
                              '['+ dbo.f_zero(d.apid, 4)+ '] ' + d.application_name as 'APID',              -- INSERIDA A COLUNA APID, PARA EXIBIÇÃO AO USUARIO
                              a.description as 'Request Description',
                              a.ssc as 'Sequence',
                              '['+ dbo.f_zero(a.service_type, 3)+ '] ' + c.service_name as 'Service Type',
                              '['+ dbo.f_zero(a.service_subtype, 3)+ '] ' + b.description as 'Service Subtype',
                              case convert(int, convert(varbinary, '0x0' + substring(master.dbo.fn_varbintohexstr(a.raw_packet), 16, 1), 1)) 
                                when 0 
                                    then 'None'
                                when 1
                                    then 'Acceptance'
                                when 2
                                    then 'Start of Execution [invalid]'
                                when 4
                                    then 'Progress of Execution [invalid]'
                                when 8
                                    then 'Completion of Execution'
                                when 9
                                    then 'Acceptance + Completion'
                              end as 'Acknowledge',
                              a.raw_packet
                        from
                              saved_requests as a inner join subtypes as b
                              on a.service_type = b.service_type
                              and a.service_subtype = b.service_subtype
                              inner join services as c
                              on a.service_type = c.service_type
                              inner join apids as d 
                              on d.apid = a.apid
                        where saved_request_id <> 0
                        order by a.saved_request_id desc";

            gridDatabase.DataSource = DbInterface.GetDataTable(sql);

            gridDatabase.Columns[0].Width = 80;
            gridDatabase.Columns[1].Width = 150;
            gridDatabase.Columns[2].Width = 350;
            gridDatabase.Columns[3].Width = 80;
            gridDatabase.Columns[4].Width = 280;
            gridDatabase.Columns[5].Width = 350;
            gridDatabase.Columns[6].Width = 180;
            gridDatabase.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridDatabase.Columns[7].Visible = false;
        }

        private bool CanDelete()
        {
            string sql = @"select count(saved_requests.saved_request_id) as RequestsUseds 
                           from saved_requests, test_procedure_steps 
                           where saved_requests.saved_request_id = test_procedure_steps.saved_request_id 
                           and saved_requests.saved_request_id = " + gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();

            int numRequests = (int)DbInterface.ExecuteScalar(sql);

            if (numRequests > 0)
            {
                MessageBox.Show("You cannot remove this request because it is used in some test procedure !",
                                "Record cannot be deleted",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                return false;
            }

            return true;
        }

        private void LoadAndRefreshRequestComposition()
        {
            if (mdiMain != null)
            {
                if ((mdiMain.FormRequestsComposition != null))
                {
                    if (!mdiMain.FormRequestsComposition.CalledFromSavedRequests)
                    {
                        if (MessageBox.Show("The 'Request Composition' is open!\n\nAre you sure you want close and open it again?",
                                        Application.ProductName,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    mdiMain.FormRequestsComposition.Close();
                    mdiMain.FormRequestsComposition = null;
                }

                int savedRequestId = Convert.ToInt32(gridDatabase.CurrentRow.Cells[0].Value);
                frmComposition = new FrmRequestsComposition(mdiMain);
                frmComposition.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                frmComposition.WindowState = FormWindowState.Normal;
                frmComposition.Show(this);
                frmComposition.LoadRequestComposition(savedRequestId);

                mdiMain.FormRequestsComposition = frmComposition;
            }
        }

        #endregion

        #region Eventos da interface grafica

        private void btCompositeNewTc_Click(object sender, EventArgs e)
        {
            if (frmComposition != null)
            {
                frmComposition.Close();
                frmComposition = null;
            }

            FrmRequestsComposition frm = new FrmRequestsComposition();
            frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            frm.ShowDialog();
        }

        private void btSendTc_Click(object sender, EventArgs e)
        {
            if ((frmConnection != null) && frmConnection.Connected)
            {
                RawPacket rawPacket = new RawPacket(true, false);
                rawPacket.RawContents = (byte[])gridDatabase.CurrentRow.Cells[6].Value;

                if (frmConnection.rbRs422.Checked == true)
                {
                    // RS - 422
                    frmConnection.SendCltu(rawPacket.RawContents);
                }
                else
                {
                    // RS - 232, named pipe, file
                    if (rawPacket.GetPart(5, 11) == 1) // APID = CPDU, eh um device command
                    {
                        rawPacket.SetAsDeviceComand();
                    }

                    frmConnection.SendRequest(rawPacket, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                }
            }
        }

        private void btDeleteTc_Click(object sender, EventArgs e)
        {
            if (frmComposition != null)
            {
                frmComposition.Close();
            }

            if (MessageBox.Show("Are you sure you want to delete this packet ?",
                                "Please Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            if (gridDatabase.Rows.Count == 0)
            {
                return;
            }

            if (!CanDelete())
            {
                return;
            }

            int indexSelected = gridDatabase.CurrentRow.Index;
            string requestID = gridDatabase.CurrentRow.Cells[0].Value.ToString();
            string sql1 = "delete from saved_requests_data_field where saved_request_id = " + requestID;
            string sql2 = "delete from saved_requests where saved_request_id = " + requestID;

            DbInterface.ExecuteNonQuery(sql1);
            DbInterface.ExecuteNonQuery(sql2);

            RefreshGrid();

            if (indexSelected == gridDatabase.RowCount)
            {
                if (gridDatabase.RowCount == 0)
                {
                    btDeleteTc.Enabled = false;
                    btLoadTC.Enabled = false;
                    btSendTc.Enabled = false;
                    return;
                }

                gridDatabase.Rows[indexSelected - 1].Cells[0].Selected = true;
            }
            else
            {
                gridDatabase.Rows[indexSelected].Cells[0].Selected = true;
            }
        }

        private void btLoadTC_Click(object sender, EventArgs e)
        {
            LoadAndRefreshRequestComposition();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();

            if (gridDatabase.RowCount == 0)
            {
                btDeleteTc.Enabled = false;
                btLoadTC.Enabled = false;
                btSendTc.Enabled = false;
            }
            else
            {
                btDeleteTc.Enabled = true;
                btLoadTC.Enabled = true;
                btSendTc.Enabled = true;
            }
        }

        private void gridDatabase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadAndRefreshRequestComposition();
        }

        private void gridDatabase_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid.
            if ((gridDatabase.Focused) && (e.KeyCode == Keys.Enter))
            {
                e.Handled = true;

                LoadAndRefreshRequestComposition();
            }

            if ((e.KeyCode == Keys.F5) && (btRefresh.Enabled))
            {
                btRefresh_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Insert)
            {
                btCompositeNewTc_Click(null, new EventArgs());
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btDeleteTc_Click(null, new EventArgs());
            }
        }

        private void FrmSavedRequests_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid.
            if ((gridDatabase.Focused) && (e.KeyCode == Keys.Enter))
            {
                e.Handled = true;

                LoadAndRefreshRequestComposition();
            }

            if ((e.KeyCode == Keys.F5) && (btRefresh.Enabled))
            {
                btRefresh_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Insert)
            {
                btCompositeNewTc_Click(null, new EventArgs());
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btDeleteTc_Click(null, new EventArgs());
            }
        }

        private void FrmSavedRequests_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(760, 417);
            }
        }

        #endregion
    }
}
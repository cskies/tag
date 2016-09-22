/**
 * @file 	    FrmEventsDetectionList.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Michel Andrade
 * @date	    19/11/2009
 * @note	    Modificado em 27/08/2013 por Ayres.
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
using Inpe.Subord.Comav.Egse.Smc.Forms;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using System.Globalization;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 **/
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmEventsDetectionList
     * Formulario de monitoramento do Events Detection List do COMAV.
     **/
    public partial class FrmEventsDetectionList : DockContent
    {
        private FrmConnectionWithEgse frmConnection = null;
        private MdiMain mdiMain = null;

        public FrmEventsDetectionList(MdiMain mdi)
        {
            InitializeComponent();

            mdiMain = mdi;
            frmConnection = mdiMain.FormConnectionWithEgse;

            btRequestEvents.Enabled = false;

            if (frmConnection != null)
            {
                frmConnection.FormEventsDetectionList = this;

                if ((frmConnection.Connected) && (!frmConnection.rbFile.Checked))
                {
                    btRequestEvents.Enabled = true;
                }
            }
        }

        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            set
            {
                frmConnection = value;
            }
        }

        private void FrmEventsDetectionList_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(746, 579);
            }
        }

        private void btRequestEvents_Click(object sender, EventArgs e)
        {
            // Assina no evento "TelemetryEvent" para ser informado quando o evento for disparado
            frmConnection.TelemetryReceived += new TelemetryEventHandler(frmConnectionWithEgse_TelemetryEvent);

            // Envia o pacote
            frmConnection.SendRequest(new RawPacket(19, 6), FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
            frmConnection.SendRequest(new RawPacket(15, 128), FrmConnectionWithEgse.SourceRequestPacket.OtherSource);

            // desabilita o botao ate que seja recebida a TM.
            // isso evita que o usuario assine varias vezes ao evento TelemetryReceived
            gridEventDetection.Rows.Clear();
            btRequestEvents.Enabled = false;

            lblTimeTag.Text = "Events Detection List not received yet";
            chkTimeTagDate.Enabled = false;
            chkTimeTagDate.Checked = false;
        }

        /**
         * Metodo que sera executado apos o disparo do evendo "TelemetryEvent"
         * na FrmConnection.
         **/
        void frmConnectionWithEgse_TelemetryEvent(object sender, TelemetryEventArgs eventArgs)
        {
            if ((eventArgs.ServiceType == 19) && (eventArgs.ServiceSubtype == 7))
            {
                AppDataGridsHandling.FillGridWithPacketDataField(eventArgs.RawPacket, ref gridEventDetection);
   
                // Desassina do evento "TelemetryEvent"
                frmConnection.TelemetryReceived -= frmConnectionWithEgse_TelemetryEvent;

                // permite que o usuario solicite novamente o Events Detection List
                btRequestEvents.Enabled = true;

                if (gridEventDetection.RowCount > 0)
                {
                    chkTimeTagDate.Enabled = true;
                }

                foreach (DataGridViewRow row in gridEventDetection.Rows)
                {
                    // faz um backup dos dados
                    row.Cells[4].Tag = row.Cells[4].Value;
                }


                lblTimeTag.Text = "Events Detection List Received at "+ DateTime.Now.ToLongTimeString();
                gridEventDetection.Columns[4].HeaderText = "Last Occurrence: Seconds";
                gridEventDetection.Columns[5].HeaderText = "Last Occurrence: Miliseconds";

            }          
        }

        /**
         * Cria um request set_time para sincronizar o relogio de bordo
         * com o relogio do servidor de banco de dados.
         **/
        public static RawPacket CreateRequestPacket()
        {
            RawPacket request = new RawPacket(true, false);

            DbConfiguration.Load();

            // Seta, de uma vez so, o cabecalho do pacote e da area de dados
            byte[] part = new byte[10]; // tamanho do maior campo do pacote de tempo
            
            part[0] = 0x18; // campos padrao
            part[1] = Convert.ToByte(DbConfiguration.RequestsDefaultApid); // apid, OBDH
            part[2] = 0xC0; // sequence flags = stand-alone packet
            part[3] = 0x01; // sequence count = 1
            part[4] = 0x00;
            part[5] = 0x05; // packet length
            part[6] = 0x10; // campos padrao, sem ack
            part[7] = 0x13; // service type
            part[8] = 0x06; // service subtype;
            part[9] = Convert.ToByte(DbConfiguration.RequestsDefaultSourceId); // source id  
            request.SetPart(0, 80, part);

            return request;
        }

        private void chkTimeTagDate_CheckedChanged(object sender, EventArgs e)
        {          
            if (chkTimeTagDate.Checked)
            {
                gridEventDetection.Columns[4].HeaderText = "Last Occurrence";

                foreach (DataGridViewRow row in gridEventDetection.Rows)
                {
                    String timeTag = null;
                    
                        Int32 microSeconds = Int32.Parse(row.Cells[5].Value.ToString(), NumberStyles.HexNumber);
                        Int32 seconds = Int32.Parse(row.Cells[4].Value.ToString(), NumberStyles.HexNumber);

                        timeTag = TimeCode.DateFromEpoch(seconds, microSeconds);

                        row.Cells[4].Value = timeTag;                                   

                }
                gridEventDetection.Columns[5].Visible = false;
            }
            else
            {

                foreach (DataGridViewRow row in gridEventDetection.Rows)
                {
                    row.Cells[4].Value = row.Cells[4].Tag;
                }

                gridEventDetection.Columns[5].Visible = true;
                gridEventDetection.Columns[4].HeaderText = "Last Occurrence: Seconds";
            }
        }
    }
}

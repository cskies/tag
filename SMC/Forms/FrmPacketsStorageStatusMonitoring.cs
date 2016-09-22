/**
 * @file 	    FrmPacketsStoreMonitoring.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Michel Andrade
 * @date	    12/05/2010
 * @note	    Modificado em 28/05/2013 por Thiago.
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
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using System.Globalization;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmPacketsStorageStatusMonitoring : DockContent
    {
        private FrmConnectionWithEgse frmConnection = null;
        private MdiMain mdiMain = null;

        bool tm14_4Received = false;
        bool tm14_8Received = false;
        bool tm14_16Received = false;
        bool tm15_6Received = false;
        bool tm15_13Received = false;

        public FrmPacketsStorageStatusMonitoring(MdiMain mdiMain)
        {
            InitializeComponent(); 

            this.mdiMain = mdiMain;
            frmConnection = mdiMain.FormConnectionWithEgse;

            btnResquestStorage.Enabled = false;

            if (frmConnection != null)
            {
                frmConnection.FormPacketsStorage = this;

                if ((frmConnection.Connected) && (!frmConnection.rbFile.Checked))
                {
                    btnResquestStorage.Enabled = true;
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

        private void frmPacketsStorageStatusMonitoring_Load(object sender, EventArgs e)
        {            
            gridPacketStores.Columns.Add("packetStore", "Packet Store");
            gridPacketStores.Columns.Add("oldPacketSequence", "Packet Sequence");
            gridPacketStores.Columns.Add("oldPacketServiceType", "Service Type");
            gridPacketStores.Columns.Add("oldServiceSubtype", "Service Subtype");
            gridPacketStores.Columns.Add("oldTimeTag", "Time-Tag");
            gridPacketStores.Columns.Add("newPacketSequence", "Packet Sequence");
            gridPacketStores.Columns.Add("newServiceType", "Service Type");
            gridPacketStores.Columns.Add("newServiceSubtype", "Service Subtype");
            gridPacketStores.Columns.Add("newTimeTag", "Time-Tag");
            gridPacketStores.Columns.Add("percentageFilled", "Percentage Filled");
            gridPacketStores.Columns.Add("percentageDownlinked", "Percentage Downlinked");
            gridPacketStores.Columns.Add("numberOfPacketsStored", "Number of Packets Stored");
            gridPacketStores.Columns.Add("numberOfPacketsDownlinked", "Number of Packets Downlinked");          
            
            gridPacketStores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            gridPacketStores.ColumnHeadersHeight = gridPacketStores.ColumnHeadersHeight * 3;
            gridPacketStores.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            gridPacketStores.CellPainting += new DataGridViewCellPaintingEventHandler(gridPacketStores_CellPainting);
            gridPacketStores.Paint += new PaintEventHandler(gridPacketStores_Paint);
            gridPacketStores.Scroll += new ScrollEventHandler(gridPacketStores_Scroll);
            gridPacketStores.ColumnWidthChanged += new DataGridViewColumnEventHandler(gridPacketStores_ColumnWidthChanged);             
        }
        
        private void gridPacketStores_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {           
            Rectangle rtHeader = this.gridPacketStores.DisplayRectangle;
            rtHeader.Height = this.gridPacketStores.ColumnHeadersHeight / 2;
            this.gridPacketStores.Invalidate(rtHeader);                       
        }

        private void gridPacketStores_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = gridPacketStores.DisplayRectangle;
            rtHeader.Height = gridPacketStores.ColumnHeadersHeight;
            gridPacketStores.Invalidate(rtHeader);
        }

        private void gridPacketStores_Paint(object sender, PaintEventArgs e)
        {           
            string[] packets = { "Oldest Packet", "Newest Packet"};
            int numPacket = 0;
            
            for (int i = 0; i < 5; )
            {
                Rectangle r1 = this.gridPacketStores.GetCellDisplayRectangle(i + 1, -1, true);
                int w2 = this.gridPacketStores.GetCellDisplayRectangle(i + 4, -1, true).Width;

                r1.X += 1;
                r1.Y += 1;

                r1.Width = r1.Width + w2 * 3  - 2;
                r1.Height = 32;

                e.Graphics.FillRectangle(new SolidBrush(gridPacketStores.ColumnHeadersDefaultCellStyle.BackColor), r1);

                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(packets[numPacket],
                    gridPacketStores.ColumnHeadersDefaultCellStyle.Font,
                        new SolidBrush(gridPacketStores.ColumnHeadersDefaultCellStyle.ForeColor),
                        r1, format);

                numPacket++;
                i +=4;            
           }
        }

        private void gridPacketStores_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {       
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height;
                r2.Height = e.CellBounds.Height;

                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }
        }

        private void btnResquestStorage_Click(object sender, EventArgs e)
        {
            if (frmConnection != null)
            {
                tm14_4Received = false;
                tm14_8Received = false;
                tm14_16Received = false;
                tm15_6Received = false;
                tm15_13Received = false;

                // Assina no evento "TelemetryEvent" para ser informado quando o evento for disparado
                frmConnection.TelemetryReceived += new TelemetryEventHandler(frmConnectionWithEgse_TelemetryEvent);

                // Envia o pacotes                
                frmConnection.SendRequest(new RawPacket(14, 15), FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                frmConnection.SendRequest(new RawPacket(14, 3), FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                frmConnection.SendRequest(new RawPacket(14, 7), FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                frmConnection.SendRequest(new RawPacket(15, 5), FrmConnectionWithEgse.SourceRequestPacket.OtherSource);

                RawPacket request = new RawPacket(15,12);
                                 
                byte[] part = new byte[6];  
                part[0] = 0x05; // N Field
                part[1] = 0x00; // normal report
                part[2] = 0x01; // priority reports
                part[3] = 0x02; // real-time reports
                part[4] = 0x03; // command schedule
                part[5] = 0x05; // events_response

                request.Resize((ushort)18);
                request.SetPart(80, 48, part);               

                frmConnection.SendRequest(request, FrmConnectionWithEgse.SourceRequestPacket.OtherSource);
                
                btnResquestStorage.Enabled = false;                
            }
        }

        /**
        * Metodo que sera executado apos o disparo do evento "TelemetryEvent" no FrmConnection.
        **/
        void frmConnectionWithEgse_TelemetryEvent(object sender, TelemetryEventArgs eventArgs)
        {
            btnResquestStorage.Enabled = true;  

            if (eventArgs.ServiceType == 15 && eventArgs.ServiceSubtype == 13)
            {
                tm15_13Received = true;

                AppDataGridsHandling.FillGridWithPacketDataField(eventArgs.RawPacket, ref gridPacketStores);
                 
                // verifica se a opcao Show time-tag as date esta selecionada.
                if (chkShowTimeTags.Checked)
                {
                    foreach (DataGridViewRow row in gridPacketStores.Rows)
                    {
                        if (row.Cells[4].Value != null)
                        {                          
                            row.Cells[4].Value =
                                TimeCode.OnboardTimeToCalendar(row.Cells[4].Value.ToString() + "0000");

                            row.Cells[8].Value =
                                TimeCode.OnboardTimeToCalendar(row.Cells[8].Value.ToString() + "0000");
                        }
                    }
                }               
            }

            if (eventArgs.ServiceType == 15 && eventArgs.ServiceSubtype == 6)
            {
                tm15_6Received = true;

                AppDataGridsHandling.FillGridWithPacketDataField(eventArgs.RawPacket, ref gridStorageSelection);                        
            }

            if (eventArgs.ServiceType == 14 && eventArgs.ServiceSubtype == 4)
            {     
                tm14_4Received = true;

                AppDataGridsHandling.FillGridWithPacketDataField(eventArgs.RawPacket, ref gridTelemetrySource);
            }

            if (eventArgs.ServiceType == 14 && eventArgs.ServiceSubtype == 8)
            {
                tm14_8Received = true;

                AppDataGridsHandling.FillGridWithPacketDataField(eventArgs.RawPacket, ref gridHousekeeping);
            }

            if (eventArgs.ServiceType == 14 && eventArgs.ServiceSubtype == 16)
            {
                tm14_16Received = true;

                AppDataGridsHandling.FillGridWithPacketDataField(eventArgs.RawPacket, ref gridEventsPackets);
            }

            // Habilita o botao apos o recebimento de toda as telemetrias
            if (tm14_4Received == true && 
                tm14_8Received == true && 
                tm14_16Received == true && 
                tm15_6Received == true &&
                tm15_13Received == true)
            {
                // Desassina do evento "TelemetryEvent"
                frmConnection.TelemetryReceived -= frmConnectionWithEgse_TelemetryEvent;

                btnResquestStorage.Enabled = true;
            }
        }

        private void chkShowTimeTags_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowTimeTags.Checked == false)
            {
                foreach (DataGridViewRow row in gridPacketStores.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        row.Cells[4].Value = 
                            "0x" + TimeCode.CalendarToOnboardTime(row.Cells[4].Value.ToString());  
                    }
                    if (row.Cells[8].Value != null)
                    {
                        row.Cells[8].Value =
                            "0x" + TimeCode.CalendarToOnboardTime(row.Cells[8].Value.ToString());
                    }
                }
            }

            else // Show Time as Calendar selecionado
            {
                foreach (DataGridViewRow row in gridPacketStores.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {                                        
                        row.Cells[4].Value = 
                            TimeCode.OnboardTimeToCalendar(row.Cells[4].Value.ToString().Remove(0,2) + "0000");
                    }
                    if (row.Cells[8].Value != null)
                    {
                        row.Cells[8].Value =
                            TimeCode.OnboardTimeToCalendar(row.Cells[8].Value.ToString().Remove(0,2) + "0000");
                    }
                }
            }
        }  
    }
}

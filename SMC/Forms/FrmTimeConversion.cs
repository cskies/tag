/**
 * @file 	    FrmTcsSending.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    30/08/2009
 * @note	    Modificado em 02/09/2009 por Fabricio.
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
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmTimeConversion
     * Formulario auxiliar para a conversao de formato de tempo.
     **/
    public partial class FrmTimeConversion : DockContent
    {
        public FrmTimeConversion()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbCalendarToOnboard_CheckedChanged(object sender, EventArgs e)
        {
            mskCalendarTime.ReadOnly = !rbCalendarToOnboard.Checked;
            mskOnboardTime.ReadOnly = rbCalendarToOnboard.Checked;
        }

        private void FrmTimeConversion_Load(object sender, EventArgs e)
        {
            // inicializa ambos os campos
            mskCalendarTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ",000000";

            btConvert_Click(this, new EventArgs());
        }

        private void btConvert_Click(object sender, EventArgs e)
        {
            if (rbCalendarToOnboard.Checked) // conversao de calendario para onboard
            {
                // formata a data informada; preenche zeros onde nao houver
                String calendarTime = mskCalendarTime.Text.Replace(" ", "0");
                
                // como substituiu um espaco que nao devia, o devolve
                calendarTime = calendarTime.Substring(0, 10) + " " + calendarTime.Substring(11);

                if (calendarTime.EndsWith("."))
                {
                    calendarTime += "000000";
                }

                // devolve a data reformatada ao mask
                mskCalendarTime.Text = calendarTime;

                // agora valida a data
                DateTime parsedDate;

                if ((!DateTime.TryParse(mskCalendarTime.Text.Substring(0, 21), out parsedDate)) ||
                    (parsedDate.Year < 1958))
                {
                    mskOnboardTime.Text = "";
                    MessageBox.Show("Invalid calendar date informed !", 
                                    "Time Conversion Error", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Exclamation);
                    mskCalendarTime.Focus();
                    return;
                }

                TimeCode.LoadEpoch();

                mskOnboardTime.Text = TimeCode.CalendarToOnboardTime(mskCalendarTime.Text);
            }
            else // conversao de onboard para calendar
            {
                // preenche espacos com zeros
                mskOnboardTime.Text = (mskOnboardTime.Text.Replace(" ", "0") + "000000000000").Substring(0, 12);

                // verifica se a string eh um hexa valido
                try
                {
                    Int64 verify = Convert.ToInt64(mskOnboardTime.Text, 16);
                }
                catch
                {
                    mskCalendarTime.Text = "";
                    MessageBox.Show("Invalid onboard time informed !", "Time Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    mskOnboardTime.Focus();
                    return;
                }

                TimeCode.LoadEpoch();
                mskCalendarTime.Text = TimeCode.OnboardTimeToCalendar(mskOnboardTime.Text);
            }
        }

        private void FrmTimeConversion_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.FloatPane.FloatWindow.ClientSize = new Size(323, 152);
            }
        }
    }
}

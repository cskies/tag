/**
 * @file 	    FrmSwAplVersion.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto da Silva
 * @date	    25/06/2013
 * @note	    Modificado em 24/10/2013 por Bruna.
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
using Inpe.Subord.Comav.Egse.Smc.TestSession;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmSwAplVersion : DockContent
    {
        public FrmSwAplVersion()
        {
            InitializeComponent();
        }

        // verifica se o frmSwAplVersion foi cancelado
        public bool swAplVersionCancel;

        private void FrmSwAplVersion_Load(object sender, EventArgs e)
        {
            DbConfiguration.Load();
            numSwMajor.Value = DbConfiguration.FlightSwVersionMajor;
            numSwMinor.Value = DbConfiguration.FlightSwVersionMinor;
            numSwPatch.Value = DbConfiguration.FlightSwVersionPatch;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            swAplVersionCancel = true;
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DbConfiguration.Load();
            DbConfiguration.FlightSwVersionMajor = (int)numSwMajor.Value;
            DbConfiguration.FlightSwVersionMinor = (int)numSwMinor.Value;
            DbConfiguration.FlightSwVersionPatch = (int)numSwPatch.Value;
            DbConfiguration.Save();
            this.Close();
        }

        private void FrmSwAplVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventArgs evt = new EventArgs();

            if (e.KeyChar == 27) //ESC - PARA SAIR DA TELA SEM SALVAR
            {
                btCancel_Click(sender, evt);
            }
            else if (e.KeyChar == 13) //ENTER - PARA SAIR DA TELA SALVANDO
            {
                btSave_Click(sender, evt);
            }
        }
    }
}

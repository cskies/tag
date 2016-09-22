/**
 * @file 	    FrmSplash.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 17/02/2012 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Properties;
using System.Threading;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSplash
     * Splash screen do SMC.
     **/
    public partial class FrmSplash : DockContent
    {
        public FrmSplash()
        {
            InitializeComponent();
            Refresh_Cmb();
        }

        private void Splash_Load(object sender, EventArgs e)
        {            
            lblVersionInfo.Text = "Versão 0.1.2, 29 de Novembro de 2011.";
        }

        private void Refresh_Cmb()
        {
            int selectIndex = -1;

            //Limpa o combo para o refresh
            cmbSelectDb.Items.Clear();

            //Preenche o combo
            for (int i = 0; i < Settings.Default.db_connections_names.Count; i++)
            {
                cmbSelectDb.Items.Add(Settings.Default.db_connections_names[i]);

                //Verificar se string conectada e selecionar no combo o item referente
                if (Settings.Default.db_connection_string.ToString() == Settings.Default.db_connections_strings[i].ToString())
                {
                    selectIndex = i;
                }
            }

            cmbSelectDb.Items.Add("Add or Edit Mission...");

            if (selectIndex != -1)
            {
                cmbSelectDb.SelectedIndex = selectIndex;
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            Settings.Default.db_connection_string = Settings.Default.db_connections_strings[cmbSelectDb.SelectedIndex];
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }


        private void cmbSelectDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbSelectDb.SelectedIndex;
            
            if (index == (cmbSelectDb.Items.Count - 1))
            {
                FrmConfiguration frm = new FrmConfiguration();
                this.TopMost = false;
                frm.ShowDialog();
                this.TopMost = true;
                Refresh_Cmb();
            }
        }

        private void FrmSplash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void btOffline_Click(object sender, EventArgs e)
        {
            offlineMode = true;
            this.DialogResult = DialogResult.OK;
        }

        public bool offlineMode = false;
    }
}

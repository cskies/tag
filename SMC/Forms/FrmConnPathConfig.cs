/**
 * @file 	    FrmConnPathConfig.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Ayres Augusto
 * @date	    26/03/2012
 * @note	    Modificado em 26/03/2012 por Ayres.
 **/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Net;
using Inpe.Subord.Comav.Egse.Smc.Properties;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmConnPathConfig : Form
    {
        private String connectionName = String.Empty;
        private String connectionpath = String.Empty;

        public FrmConnPathConfig()
        {
            InitializeComponent();

            this.Focus();
            txtName.Text = String.Empty;
            txtPath.Text = String.Empty;
            txtName.Focus();
            btConfirm.Select();
        }

        public String ConnectionName
        {
            set
            {
                connectionName = value;
                txtName.Text = connectionName;
            }
        }

        public String ConnectionPath
        {
            set
            {
                connectionpath = value;
                txtPath.Text = connectionpath;
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {                       
                // Adicionar ou alterar
                if (Properties.Settings.Default.db_connections_names.Contains(connectionName))
                {
                    int index = Properties.Settings.Default.db_connections_names.IndexOf(connectionName);
                    Properties.Settings.Default.db_connections_names[index] = txtName.Text;
                    Properties.Settings.Default.db_connections_strings[index] = txtPath.Text;
                }
                else
                {
                    Properties.Settings.Default.db_connections_names.Add(txtName.Text);
                    Properties.Settings.Default.db_connections_strings.Add(txtPath.Text);
                }

                Properties.Settings.Default.Save();
            
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btConnectionFilePath_Click(object sender, EventArgs e)
        {

            fileDialog.Filter = "Universal Data Link Files|*.udl|All Files|*.*";

            if (txtPath.Text.Equals(""))
            {
                fileDialog.FileName = "connection.udl";
            }
            else
            {
                fileDialog.FileName = txtPath.Text;
                fileDialog.InitialDirectory = txtPath.Text.Substring(0, txtPath.Text.LastIndexOf("\\"));
            }

            fileDialog.FilterIndex = 0;

            /** 
             * @attention 
             * Essa linha pode dar erro (apenas) durante a depuracao, caso se tente abrir um arquivo .udl a partir
             * do dialogo. Para evitar isso, deve-se desmarcar a opcao "throw" para o erro "LoaderLock", na opcao
             * "Managed Debugging Assistants", acessado pelo menu "Debug->Exceptions" do Visual Studio.
             **/
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fileDialog.FileName;
            }

        }
    }
}

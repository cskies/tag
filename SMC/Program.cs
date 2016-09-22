/**
 * @file 	    Program.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @note	    Modificado em 27/08/2009 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Forms;

/**
 * @Namespace Este eh o namespace raiz do projeto COMAV_EGSE: SMC. Nele sao desenvolvidas todos
 * os recursos referentes a tecnologia de software para a realizacao de testes em Computadores 
 * Embarcados em Satelites.
 */
namespace Inpe.Subord.Comav.Egse.Smc
{
    /**
     * @class Program
     * Contem o codigo de inicializacao do SMC. Classe gerada automaticamente pelo IDE.
     **/
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MdiMain());
        }
    }
}

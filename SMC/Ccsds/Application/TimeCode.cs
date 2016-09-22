/**
 * @file 	    TimeCode.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 24/10/2013 por Bruna.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;

/**
 * @Namespace Este Namespace possui recursos para controlar o envio e recepcao dos pacotes.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Ccsds.Application
{
    /**
     * @class TimeCode
     * Esta classe traz os campos e metodos necessarios para manipular informacao
     * temporal no formato CCSDS CUC Level 2, sem preambulo.
     **/
    public class TimeCode
    {
        private int sec = 0; // a diferenca em segundos nao passa de 4 bytes devido ao formato CUC
        private int microsec = 0;
        private static DateTime currentEpoch;

        #region Propriedades

        public int Seconds
        {
            get
            {
                return sec;
            }
            set
            {
                sec = value;
            }
        }

        public int Microseconds
        {
            get
            {
                return microsec;
            }
            set
            {
                microsec = value;
            }
        }

        #endregion

        #region Construtores

        public TimeCode()
        {
        }

        public TimeCode(int seconds, int microseconds)
        {
            sec = seconds;
            microsec = microseconds;
        }

        #endregion

        #region Metodos

        /**
         * Converte a soma de um numero em (segundos e microsegundos) para uma data inteligivel ao
         * nosso calendario. Este numero eh somado a partir da data de 01/01/1958.
         */
        public String GetTimeCodeAsDate()
        {
            return (DateFromEpoch(sec, microsec));
        }

        #endregion

        #region Metodos Estaticos

        /** 
         * Converte uma string com a data em formato calendario, com ou sem
         * nanosegundos. Os formatos aceitos sao "dd/mm/yyyy hh:mm:ss" e
         * "dd/mm/yyyy hh:mm:ss.uuuuuu", onde "u" denota microsegundo.
         **/
        public static String CalendarToOnboardTime(String calendarTime)
        {
            DbConfiguration.Load();

            String uSecString = "";
            int uSeconds = 0;

            if (calendarTime.Length == 26) // ha microsegundos, os extrai
            {
                uSecString = calendarTime.Substring(calendarTime.Length - 6);
                uSeconds = int.Parse(uSecString);
                uSeconds = uSeconds / 15; // ajusta os microsegundos
            }

            DateTime toConvert = Convert.ToDateTime(calendarTime.Substring(0, 19));

            DateTime epoch = currentEpoch;

            TimeSpan diff = toConvert.Subtract(epoch);

            // Faco um cast para int porque sei que a diferenca entre 
            // datas nao pode passar de 4 bytes
            int seconds = (int)diff.TotalSeconds;

            // Esta formatacao para Int.ToString() eh dificil de encontrar na documentacao.
            // Ela converte um inteiro em uma string de 8 caracteres com representacao hexa.
            String toReturn = seconds.ToString("X8");

            // Agora adiciona a parte dos microsegundos
            toReturn += uSeconds.ToString("X4");

            return toReturn;
        }

        /**
         * Converte de uma string hex no formato 000000000000, onde os primeiros
         * 8 caracteres sao os segundos e os demais os microsegundos, em uma
         * data no formato calendario.
         **/
        public static String OnboardTimeToCalendar(String onboardTime)
        {
            DbConfiguration.Load();
            int seconds = 0;
            int uSeconds = 0;

            if (int.Parse(DbConfiguration.TmTimetagFormat) == 6)
            {
                seconds = Convert.ToInt32(onboardTime.Substring(0, 8), 16);
                uSeconds = Convert.ToInt32(onboardTime.Substring(8, 4), 16);
            }
            else
            {
                seconds = Convert.ToInt32(onboardTime.Substring(0, 8), 16);
                String secondsIn4Bytes = seconds.ToString();
                secondsIn4Bytes = secondsIn4Bytes.Substring(0, 8) + "00";
                seconds = Convert.ToInt32(secondsIn4Bytes);
            }

            return (DateFromEpoch(seconds, uSeconds));
        }

        /**
         * Converte um numero de segundos e microsegundos em uma data.
         **/
        public static String DateFromEpoch(int seconds, int microseconds)
        {
            DateTime datePreviews = currentEpoch;
            datePreviews = datePreviews.AddSeconds(seconds);
            int us = microseconds * 15;

            String toReturn = datePreviews.ToString("dd/MM/yyy HH:mm:ss.");
            String stringMs = "000000" + us.ToString();

            stringMs = stringMs.Substring(stringMs.Length - 6);
            toReturn += stringMs;

            return toReturn;
        }

        public static void LoadEpoch()
        {
            DbConfiguration.Load();
            currentEpoch = DbConfiguration.MissionEpoch;
        }

        #endregion
    }
}
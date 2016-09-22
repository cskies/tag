/**
 * @file 	    Formatting.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    14/07/2009
 * @note	    Modificado em 25/11/2014 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inpe.Subord.Comav.Egse.Smc.Database;

/**
 * @Namespace Este namespace contem todas as classes que fornecem suporte a varias necessidades
 * do SMC, como manipulacao e exibicao de dados, formatacao de dados, dentre outras..
 */
namespace Inpe.Subord.Comav.Egse.Smc.Utils
{
    /**
     * @class Formatting
     * Conjunto de metodos (provavelmente todos estaticos) diversos 
     * de formatacao, de uso geral no sistema.
     **/
    class Formatting
    {
        #region Metodos Estaticos

        /** Formata um codigo para exibicao na interface grafica. **/
        public static String FormatCode(int code, int size)
        {
            String value = code.ToString();
            String zeros = "000000000000000";
            String leftZero = zeros.Substring(0, size - value.Length);
            String numberFormated = "[" + leftZero + value + "]";

            return numberFormated;
        }
        /** Formata um valor hexadecimal de 32 bits para 8 caracters. **/
        public static String FormatHexString32Bits(string toFormat)
        {
            String zeros = "00000000";
            String numberFormated = zeros.Substring(0, zeros.Length - toFormat.Length) + toFormat;

            return numberFormated;
        }

        /**
         * Verifica se uma string pode ser convertida em um array de bytes hexadecimal.
         * Retorna null caso nao possa ser convertida.   
         **/
        public static byte[] HexStringToByteArray(String stringBytes)
        {
            try
            {
                int numBytes = (stringBytes.Length / 2);
                byte[] hexByte = new byte[numBytes];

                int index = 0;

                for (int i = 0; i < stringBytes.Length; i += 2)
                {
                    hexByte[index] = Convert.ToByte(stringBytes.Substring(i, 2), 16);
                    index++;
                }

                return hexByte;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /** 
         * Formata strings hexa, removendo espacos/tabs/enter, colocando em maiusculas
         * e separando os bytes por uma barra. Se o numero de caracteres for impar,
         * adiciona um zero a esquerda.
         * Nao verifica se o hexa eh valido!
         **/
        public static String FormatHexString(String inputString)
        {
            String strippedValue = inputString.Replace("-", "").Replace(" ", "").Replace("\n", "").Replace("\t", "");

            if (strippedValue.Length == 0)
            {
                return "";
            }

            // formata o hex
            String formatted = "";
            int nibbleCount = 0;

            // se houver um numero impar de caracteres, coloca um zero a esquerda
            if ((strippedValue.Length % 2) != 0)
            {
                strippedValue = "0" + strippedValue;
            }

            // separa os bytes na string
            for (int k = 0; k < strippedValue.Length; k++)
            {
                formatted += strippedValue.Substring(k, 1);

                nibbleCount++;

                if (nibbleCount == 2)
                {
                    nibbleCount = 0;
                    formatted += "-";
                }
            }

            // remove o ultimo "-" e coloca tudo em maiusculas
            formatted = formatted.ToUpper().Remove(formatted.Length - 1);

            return formatted;
        }

        /**
         * Este metodo retorna true caso um valor em Hexadecimal seja convertido para inteiro de 64 bits.
         **/
        public static bool ConvertToUInt64(String valueHexString)
        {
            try
            {
                UInt64 valueInt64 = Convert.ToUInt64(valueHexString, 16);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /**
         * Verifica se uma string eh um numerico de 64 bits.
         * Foi extraido da fonte: http://www.geekpedia.com/code94_Check-if-string-is-numeric.html
         **/
        public static bool IsNumeric(string strToCheck)
        {
            return Regex.IsMatch(strToCheck, "^\\d+(\\.\\d+)?$");
        }

        /**
         * Converte um array de bytes em formato Hexadecimal
         * @param lastPosition requer o numero de bytes a serem convertidos e nao o index da ultima posicao a serem convertidos.
         **/
        public static String ConvertByteArrayToHexString(byte[] value, int lastPosition)
        {
            StringBuilder hex = new StringBuilder(value.Length * 2);

            for (int i = 0; i < lastPosition; i++)
            {
                hex.AppendFormat("{0:x2}", value[i]);
            }

            return hex.ToString().ToUpper();
        }

        /**
         * Converte um numero inteiro em formato hexadecimal.
         **/
        public static String ConvertIntToHexString(int value)
        {
            return value.ToString("X");
        }

        /**
         * Converte um array de bytes em uma frase legivel em português.
         **/
        public static String ConvertByteArrayToASCIISentence(byte[] value)
        {
            String bufferString = ASCIIEncoding.ASCII.GetString(value, 0, (value.Length - 1));
            return bufferString;
        }

        /**
         * Converter um array de bytes em formato binario ASCII. Exemplo: C4 => 11000100
         **/
        public static String ConvertByteArrayToASCIIBinary(byte[] byteArray, int lastPosition)
        {
            String stringValue = "";

            if (byteArray == null)
            {
                return stringValue;
            }

            byte byteValue = 0x0;

            // Ao converter um byte para formato binario, a funcao usada retorna
            // o resultado sem levar em consideracao os Zeros a esquerda..
            // Por isso foi necessario adiciona-los ao concatenar na string
            for (int i = 0; i < lastPosition; i++)
            {
                byteValue = byteArray[i];
                String value = "00000000" + Convert.ToString(byteValue, 2);
                value = value.Substring((value.Length - 8), 8);
                stringValue += value;
            }

            return stringValue;
        }

        /**
        * Converter uma string em formato Hexadecimal para formato ASCII.
        **/
        public static String ConvertHexStringToAsciiString(String hex)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hex.Length; i += 2)
            {
                String hs = hex.Substring(i, 2);
                stringBuilder.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }

            return stringBuilder.ToString();
        }

        /**
         * Converter uma string em formato ASCII para formato Hexadecimal.
         **/
        public static String ConvertAsciiStringToHexString(String ascii)
        {
            String hex = "";

            foreach (char character in ascii)
            {
                int charTemp = character;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(charTemp.ToString()));
            }

            return hex.ToUpper();
        }


        //****teste
        public static string ConvertDateToHexString(DateTime date)
        {
            string dateInStr = date.ToString() + ",000000";
            return date.Ticks.ToString("X");
        }

        public static DateTime ConvertHexStringToDateTime(string hexInput)
        {
            //hexInput = hexInput + "0000";
            long ticks = Convert.ToInt64(hexInput, 16);
            return new DateTime(Convert.ToInt64(ticks));
        }

        #endregion
    }
}

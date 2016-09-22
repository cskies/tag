/**
 * @file 	    RawFrame.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 26/09/2009 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Este Namespace contem as classes para tratamento dos Frames de TC e TM.
 */ 
namespace Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer
{
    /**
     * @class RawFrame
     * Classe para a composicao, manipulacao e interpretacao de frames CCSDS.
     **/
    class RawFrame : RawBytes
    {
        #region Atributos Privados e Propriedades

        private bool autoCrc;

        public bool AutoCrc
        {
            set
            {
                autoCrc = value;
            }
        }

        #endregion

        public static int FRAME_LENGTH = 7; // 5 bytes para frame header + 2 bytes para frame_crc
        public static int SEGMENT_LENGTH = 1; 

        public RawFrame(bool isTc)
        {
            autoCrc = isTc;
            ClearFrame();
        }

        /**
         * Sobrescreve o metodo SetPart de RawBytes. Apos chamar o metodo da classe
         * base, recalcula o CRC, caso o recalculo tenha sido setado como automatico.
         **/
        public override void SetPart(int startBit, int numberOfBits, byte[] newPart)
        {
            // Chama o metodo da classe-base
            base.SetPart(startBit, numberOfBits, newPart);

            if (autoCrc)
            {
                // Recalcula o crc
                UInt16 crc = CheckingCodes.CrcCcitt16(ref rawContent, rawContent.Length - 2);

                rawContent[rawContent.Length - 2] = (byte)(crc >> 8);
                rawContent[rawContent.Length - 1] = (byte)(crc & 0xFF);
            }
        }

        /**
         * Recebe um array de strings em hexa (no formato '0A-0B-0C' etc), 
         * converte em um array de bytes e chama SetPart.
         * Esta rotina nao faz o alinhamento dos bits necessario antes de chamar
         * SetPart (ver notas no metodo); assume-se que os bytes estejam alinhados.
         **/
        public bool SetPartString(String part, int startBit, int numberOfBits)
        {
            // TODO: Deve retornar false se a string nao for conversivel em hexa, ou se o tamanho
            // exceder o do rawContent. Por enquanto nao faco isso.
            byte[] newPart = StringToByteArray(part);
            SetPart(startBit, numberOfBits, newPart);

            return (true);
        }

        /**
         * Redimensiona o array de bytes, zerando todas as posicoes a partir
         * do bit 40 (inicio da area de dados do frame).
         * O campo frame length eh atualizado automaticamente por este metodo.
         **/
        public void Resize(int newSize)
        {
            size = newSize;

            // Atualiza o campo frame length
            byte[] part = new byte[1];
            part[0] = (byte)((newSize - 1) & 0xFF);
            SetPart(24, 8, part);

            Array.Resize<byte>(ref rawContent, size);

            // Zera toda a mensagem, a partir do inicio da area de dados,
            // para que os valores possam ser re-escritos em suas novas
            // posicoes (isso fica a cargo do chamador)
            part[0] = 0;

            for (int i = 40; i < (rawContent.Length * 8); i++)
            {
                SetPart(i, 1, part);
            }
        }

        /** Metodo para sobrescrever diretamente o conteudo bruto do frame **/
        public void SetRawFrame(byte[] frame)
        {
            rawContent = frame;
            size = rawContent.Length;
        }

        /** 
         * Converte uma string em um array de bytes.
         * fonte: http://stackoverflow.com/questions/321370/convert-hex-string-to-byte-array
         **/
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        /** Reseta o frame. **/
        public void ClearFrame()
        {
            size = 7;

            rawContent = new byte[size];

            // Soh para garantir que inicie zerado
            for (int i = 0; i < rawContent.Length; i++)
            {
                rawContent[i] = 0;
            }
        }
    }
}

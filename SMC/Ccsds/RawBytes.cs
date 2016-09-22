/**
 * @file 	    RawBytes.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    27/07/2009
 * @note	    Modificado em 21/08/2012 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/**
 * @Namespace Este Namespace possui as rotinas de tratamento dos dados a serem enviados e 
 * recebidos, seguindo a norma ECSS-E-70-41-A (PUS), que faz parte da CCSDS.
 */ 
namespace Inpe.Subord.Comav.Egse.Smc.Ccsds
{
    /**
     * @class RawBytes
     * Classe para a manipulacao, a nivel de bits, de arrays de bytes de tamanho variavel.
     * Esta classe eh a base para as classes RawPacket e RawFrame.
     **/
    public class RawBytes
    {
        #region Atributos Protegidos (a serem herdados)

        protected byte[] rawContent;
        protected int size;

        #endregion

        #region Propriedades

        public byte[] RawContents
        {
            get
            {
                return rawContent;
            }
            
            set
            {
                rawContent = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }
        }

        #endregion

        #region Metodos Set/Get Part

        /**
         * Seta uma parte qualquer da mensagem, nao necessariamente alinhada em bytes.
         * Por isso, todos os parametros sao passados em bits. Entretanto, o primeiro 
         * byte dos arrays de origem e destino tem que estar alinhados; Ex:
         * startBit = 3, newPart[0] = xxx01011.
         * Se for TC, os ultimos 2 bytes sao reservados ao CRC, que eh recalculado a qualquer
         * modificacao no pacote.
         **/
        public virtual void SetPart(int startBit, int numberOfBits, byte[] newPart)
        {
            if (numberOfBits == 0)
            {
                MessageBox.Show("Error trying to set a zero-bit field!",
                                "Error !",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // Encontra o indice referente a startBit e o numero de bytes a modificar
            int index = (int)Math.Floor((double)(startBit / 8));
            int firstBit = startBit - (index * 8); // primeiro bit do primeiro byte a ser manipulado

            int numberOfFullBytes;

            if (firstBit == 0)
            {
                numberOfFullBytes = (int)Math.Floor((double)((numberOfBits / 8)));
            }
            else
            {
                numberOfFullBytes = (int)Math.Floor((double)((numberOfBits - (8 - firstBit)) / 8));
            }

            int remainingBits = numberOfBits - (8 - firstBit) - (numberOfFullBytes * 8);

            if (remainingBits < 0) remainingBits = 0;

            // Preenche o primeiro byte, alinhando corretamente
            if (firstBit > 0)
            {
                // Zera os bits menos significativos que serao sobrescritos no destino
                if ((firstBit + numberOfBits) >= 8)
                {
                    // Zera os bits mais significativos nao usados no primeiro byte da origem
                    newPart[0] = (byte)(newPart[0] << firstBit);
                    newPart[0] = (byte)(newPart[0] >> firstBit);

                    rawContent[index] = (byte)(rawContent[index] >> (8 - firstBit));
                    rawContent[index] = (byte)(rawContent[index] << (8 - firstBit));
                }
                else
                {
                    byte mask = 0xFF;
                    mask = (byte)(mask >> (8 - numberOfBits)); // reduz a mascara ao numero de bits necessarios
                    mask = (byte)(mask << (8 - firstBit - numberOfBits)); // posiciona corretamente a mascara

                    newPart[0] = (byte)(newPart[0] & mask);
                    mask = (byte)~mask; // inverte a mascara
                    rawContent[index] = (byte)(rawContent[index] & mask);
                }

                rawContent[index] = (byte)(rawContent[index] | newPart[0]);
            }
            else
            {
                if (numberOfFullBytes == 0)
                {
                    byte mask = 0xFF;
                    mask = (byte)(mask >> (8 - numberOfBits)); // reduz a mascara ao numero de bits necessarios
                    mask = (byte)(mask << (8 - numberOfBits)); // posiciona corretamente a mascara

                    newPart[0] = (byte)(newPart[0] & mask);
                    mask = (byte)~mask; // inverte a mascara
                    rawContent[index] = (byte)(rawContent[index] & mask);

                    rawContent[index] = (byte)(rawContent[index] | newPart[0]);
                }
            }

            if (numberOfFullBytes > 0)
            {
                // Copia todos os bytes "inteiros" de um array para outro
                if (firstBit > 0)
                {
                    Array.Copy(newPart, 1, rawContent, index + 1, numberOfFullBytes);
                }
                else
                {
                    if (rawContent.Length < ((index + numberOfFullBytes) - 1))
                    {
                        Array.Resize(ref rawContent, (rawContent.Length + numberOfFullBytes));
                    }

                    Array.Copy(newPart, 0, rawContent, index, numberOfFullBytes);
                }
            }

            // Preenche o ultimo byte (se houver), alinhando corretamente
            if (remainingBits > 0)
            {
                // Zera os bits menos significativos nao usados no primeiro byte da origem
                newPart[numberOfFullBytes] = (byte)(newPart[numberOfFullBytes] >> remainingBits);
                newPart[numberOfFullBytes] = (byte)(newPart[numberOfFullBytes] << remainingBits);

                // Zera os bits mais significativos que serao sobrescritos no destino
                rawContent[index + numberOfFullBytes] = (byte)(rawContent[index + numberOfFullBytes] << (remainingBits));
                rawContent[index + numberOfFullBytes] = (byte)(rawContent[index + numberOfFullBytes] >> (remainingBits));

                rawContent[index + numberOfFullBytes] = (byte)(rawContent[index + numberOfFullBytes] | newPart[numberOfFullBytes]);
            }
        }

        /*
         * Retorna um UInt64 com a parte solicitada, independente do tamanho
         * (ate 64 bits, claro!)
         */
        public UInt64 GetPart(int startBit, int numberOfBits)
        {
            if (numberOfBits > 64)
            {
                MessageBox.Show("Error trying to get a field greater than 64 bits!",
                                "Error !",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return (0);
            }

            if (numberOfBits == 0)
            {
                MessageBox.Show("Error trying to get a zero-bit field!",
                                "Error !",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return (0);
            }

            // Vetor que ira conter a parte solicitada 
            // Obs: os 64 bits podem estar dispostos em 9 bytes, dependendo do start bit
            byte[] part = new byte[9];

            // Encontra o indice referente a startBit e o numero de bytes a modificar
            int index = (int)Math.Floor(((double)(startBit) / 8));

            // Copia os bytes que contem os bits solicitados
            for (int i = 0; i <= 8; i++)
            {
                if ((index + i) >= rawContent.GetLength(0))
                {
                    break;
                }
                else
                {
                    part[i] = rawContent[index + i];
                }
            }

            // Limpa os bits nao utilizados do elemento [0]
            if (startBit < 8)
            {
                part[0] = (byte)(part[0] << startBit);
                part[0] = (byte)(part[0] >> startBit);
            }
            else
            {
                part[0] = (byte)(part[0] << (startBit % 8));
                part[0] = (byte)(part[0] >> (startBit % 8));
            }

            /* 
             * Os bytes de TM/TC sao big endian. Se o processador onde o simulador rodar
             * for little endian, preciso adequar os bytes (inverter) antes de fazer o 
             * shift, e voltar a ordem original depois.
             */
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(part);

                for (int i = 0; i < 8; i++)
                {
                    part[i] = part[i + 1];
                }
            }

            UInt64 shifted = (UInt64)BitConverter.ToInt64(part, 0);
            shifted = (UInt64)(shifted >> (64 - numberOfBits - (startBit % 8)));

            return (shifted);
        }

        #endregion

        #region Outros Metodos Publicos

        /**
         * Calcula o shift a esquerda necessario para um campo, de acordo com a 
         * posicao de seu primeiro bit no array e do numero de bits. Utilizado 
         * para alinhar bytes antes de passa-los para o SetPart.
         **/
        public int DefineLeftShift(int firstBit, int numberOfBits)
        {
            int shift = 0;

            if (((firstBit % 8) == 0) && ((numberOfBits % 8) == 0))
            {
                shift = 0;
            }
            else if (firstBit > 8)
            {
                if (numberOfBits > 8)
                {
                    shift = 8 - (firstBit % 8) - (numberOfBits % 8);
                }
                else
                {
                    shift = 8 - (firstBit % 8) - numberOfBits;
                }
            }
            else if (firstBit < 8)
            {
                if (((firstBit + numberOfBits) % 8) != 0)
                {
                    shift = 7 - firstBit;
                }
                else
                {
                    shift = 0;
                }
            }

            return (shift);
        }

        /**
         * Calcula o shift a direita necessario para um campo, de acordo com a 
         * posicao de seu primeiro bit no array e do numero de bits. Utilizado 
         * para alinhar bytes antes de devolve-los pelo GetPart.
         **/
        public int DefineRightShift(int firstBit, int numberOfBits)
        {
            int shift = 0;
            int lastBit = firstBit + numberOfBits;
            int lastByte = (int)Math.Ceiling(((double)(lastBit) / 8));

            shift = (lastByte * 8) - lastBit;

            return (shift);
        }

        /** Retorna o pacote como uma String. **/
        public string GetString()
        {
            return (BitConverter.ToString(rawContent));
        }

        #endregion
    }
}

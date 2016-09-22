/**
 * @file 	    CheckingCodes.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 23/10/2013 por Bruna.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

/**
 * @Namespace Este Namespace possui as rotinas de tratamento dos dados a serem enviados e 
 * recebidos, seguindo a norma ECSS-E-70-41-A (PUS), que faz parte da CCSDS.
 */ 
namespace Inpe.Subord.Comav.Egse.Smc.Utils
{
    /**
     * @class CheckingCodes
     * Possui metodos estaticos para o calculo do CRC CCITT-16, ISO CheckSum e BCH(63, 56),
     * utilizados em diversos pontos do CCSDS e PUS.
     * 
     * @todo Falta implementar o Reed-Solomon
     **/
    class CheckingCodes
    {
        // lookup table para o CRC 32
        private static UInt32[] CRC32_TABLE = new UInt32[256] {
            0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419, 0x706af48f, 0xe963a535, 0x9e6495a3, 
            0x0edb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988, 0x09b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91, 
            0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 
            0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9, 0xfa0f3d63, 0x8d080df5, 
            0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b,
	        0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59,
            0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599, 0xb8bda50f,
            0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924, 0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d,
            0x76dc4190, 0x01db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f, 0x9fbfe4a5, 0xe8b8d433,
            0x7807c9a2, 0x0f00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x086d3d2d, 0x91646c97, 0xe6635c01,
	        0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457,
            0x65b0d9c6, 0x12b7e950, 0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65,
            0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb,
            0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9,
            0x5005713c, 0x270241aa, 0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f,
	        0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad,
            0xedb88320, 0x9abfb3b6, 0x03b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x04db2615, 0x73dc1683,
            0xe3630b12, 0x94643b84, 0x0d6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1,
            0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb, 0x196c3671, 0x6e6b06e7,
            0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5,
	        0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b,
            0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef, 0x4669be79,
            0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236, 0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f,
            0xc5ba3bbe, 0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d,
            0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x026d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x05005713,
	        0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38, 0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21,
            0x86d3d2d4, 0xf1d4e242, 0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777,
            0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45,
            0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db,
            0xaed16a4a, 0xd9d65adc, 0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,
	        0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693, 0x54de5729, 0x23d967bf,
            0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d};

        private static UInt16[] CRC16_TABLE = new UInt16[256] {
	        0x0000,	0x1021,	0x2042,	0x3063,	0x4084,	0x50a5,	0x60c6,	0x70e7, 0x8108,	0x9129,	0xa14a,	0xb16b,	
            0xc18c,	0xd1ad,	0xe1ce,	0xf1ef, 0x1231,	0x0210,	0x3273,	0x2252,	0x52b5,	0x4294,	0x72f7,	0x62d6,
	        0x9339,	0x8318,	0xb37b,	0xa35a,	0xd3bd,	0xc39c,	0xf3ff,	0xe3de, 0x2462,	0x3443,	0x0420,	0x1401,
            0x64e6,	0x74c7,	0x44a4,	0x5485, 0xa56a,	0xb54b,	0x8528,	0x9509,	0xe5ee,	0xf5cf,	0xc5ac,	0xd58d,
	        0x3653,	0x2672,	0x1611,	0x0630,	0x76d7,	0x66f6,	0x5695,	0x46b4, 0xb75b,	0xa77a,	0x9719,	0x8738,
            0xf7df,	0xe7fe,	0xd79d,	0xc7bc, 0x48c4,	0x58e5,	0x6886,	0x78a7,	0x0840,	0x1861,	0x2802,	0x3823,
	        0xc9cc,	0xd9ed,	0xe98e,	0xf9af,	0x8948,	0x9969,	0xa90a,	0xb92b, 0x5af5,	0x4ad4,	0x7ab7,	0x6a96,
            0x1a71,	0x0a50,	0x3a33,	0x2a12, 0xdbfd,	0xcbdc,	0xfbbf,	0xeb9e,	0x9b79,	0x8b58,	0xbb3b,	0xab1a,
	        0x6ca6,	0x7c87,	0x4ce4,	0x5cc5,	0x2c22,	0x3c03,	0x0c60,	0x1c41, 0xedae,	0xfd8f,	0xcdec,	0xddcd,
            0xad2a,	0xbd0b,	0x8d68,	0x9d49, 0x7e97,	0x6eb6,	0x5ed5,	0x4ef4,	0x3e13,	0x2e32,	0x1e51,	0x0e70,
	        0xff9f,	0xefbe,	0xdfdd,	0xcffc,	0xbf1b,	0xaf3a,	0x9f59,	0x8f78, 0x9188,	0x81a9,	0xb1ca,	0xa1eb,
            0xd10c,	0xc12d,	0xf14e,	0xe16f, 0x1080,	0x00a1,	0x30c2,	0x20e3,	0x5004,	0x4025,	0x7046,	0x6067,
	        0x83b9,	0x9398,	0xa3fb,	0xb3da,	0xc33d,	0xd31c,	0xe37f,	0xf35e, 0x02b1,	0x1290,	0x22f3,	0x32d2,
            0x4235,	0x5214,	0x6277,	0x7256, 0xb5ea,	0xa5cb,	0x95a8,	0x8589,	0xf56e,	0xe54f,	0xd52c,	0xc50d,
	        0x34e2,	0x24c3,	0x14a0,	0x0481,	0x7466,	0x6447,	0x5424,	0x4405, 0xa7db,	0xb7fa,	0x8799,	0x97b8,
            0xe75f,	0xf77e,	0xc71d,	0xd73c, 0x26d3,	0x36f2,	0x0691,	0x16b0,	0x6657,	0x7676,	0x4615,	0x5634,
	        0xd94c,	0xc96d,	0xf90e,	0xe92f,	0x99c8,	0x89e9,	0xb98a,	0xa9ab, 0x5844,	0x4865,	0x7806,	0x6827,
            0x18c0,	0x08e1,	0x3882,	0x28a3, 0xcb7d,	0xdb5c,	0xeb3f,	0xfb1e,	0x8bf9,	0x9bd8,	0xabbb,	0xbb9a,
	        0x4a75,	0x5a54,	0x6a37,	0x7a16,	0x0af1,	0x1ad0,	0x2ab3,	0x3a92, 0xfd2e,	0xed0f,	0xdd6c,	0xcd4d,
            0xbdaa,	0xad8b,	0x9de8,	0x8dc9, 0x7c26,	0x6c07,	0x5c64,	0x4c45,	0x3ca2,	0x2c83,	0x1ce0,	0x0cc1,
	        0xef1f,	0xff3e,	0xcf5d,	0xdf7c,	0xaf9b,	0xbfba,	0x8fd9,	0x9ff8, 0x6e17,	0x7e36,	0x4e55,	0x5e74,
            0x2e93,	0x3eb2,	0x0ed1,	0x1ef0};

        /**
         * Calculo de CRC de 32 bits, utilizado para o upload de SW.
         * 
         * Polinomio gerador:
         * x^32 + x^26 + x^23 + x^22 + x^16 + x^12 + x^11 + x^10 + x^8 + x^7 + x^5 + x^4 + x^2 + x^1 + 1
         **/
        public static UInt32 Crc32(ref byte[] data, UInt32 dataLength, UInt32 startIndex)
        {
            UInt32 crc32Value = 0xffffffff;

            for (UInt32 i = startIndex; i < (startIndex + dataLength); i++)
            {
                crc32Value = CRC32_TABLE[(crc32Value ^ (data[i])) & 0xff] ^ (crc32Value >> 8);
            }

            return (crc32Value ^ 0xffffffff);
        }

        /**
         * Calculo de CRC do Amazonia, originalmente com o mesmo polinomio gerador da funcao Crc32 acima,
         * mas que por erro da INVAP foi implementada com retorno em Int, e nao UInt.
         **/
        public static Int32 CrcAmazonia1(ref byte[] data, UInt32 dataLength, UInt32 startIndex)
        {
            Int32 crc32Value = -1; // originalmente seria UInt32 e 0xffffffff

            for (UInt32 i = startIndex; i < (startIndex + dataLength); i++)
            {
                crc32Value = (Int32)(CRC32_TABLE[(crc32Value ^ (data[i])) & 0xff] ^ (crc32Value >> 8));
            }

            return (crc32Value ^ -1); // originalmente seria (crc32Value ^ 0xffffffff)
        }
 
       /**
        * Calculo de CRC 16 (CCITT) baseada na rotina apresentada em ECSS-E-70-41A, pagina 210. 
        **/
        public static UInt16 CrcCcitt16(ref byte[] data, int numberOfBytes)
        {
            UInt16 crcValue = 0xffff;

            for (int i = 0; i < numberOfBytes; i++)
            {
                crcValue = (UInt16)((crcValue << 8) ^ CRC16_TABLE[(crcValue >> 8) ^ data[i]]);
            }

            crcValue ^= 0x0000;

            return crcValue;
        }

        /**
         * Calculo de CRC 16 (CCITT) baseada na rotina apresentada em ECSS-E-70-41A, pagina 210. 
         * 
         * Esta versao nao eh otimizada, e nao faz uso do CRC16_TABLE. Ela executa apenas uma
         * iteracao no loop de CRC, recebendo a Syndrome (resultado da iteracao anterior).
         * 
         * Deve sempre ser chamada dentro de um loop nos dados para os quais sera calculado o CRC.
         **/
        public static UInt16 CrcCcitt16WithSyndrome(byte data, UInt16 syndrome)
        {
            for (int i = 0; i < 8; i++)
            {
                // atencao: o operador '^' nao eh potencia, eh XOR!
                if ((((data & 0x80) ^ ((syndrome & 0x8000) >> 8))) != 0)
                {
                    syndrome = (UInt16)(((syndrome << 1) ^ 0x1021) & 0xffff);
                }
                else
                {
                    syndrome = (UInt16)((syndrome << 1) & 0xffff);
                }

                data = (byte)(data << 1);
            }

            return (syndrome);
        }

        /**
         * Funcao de calculo de um Checksum ISO 8473 de 16 bits (Fletcher), conforme
         * descrito no ECSS-E-70-41A, item A.2.3, pagina 213.
         **/
        public static UInt16 IsoChecksum(byte[] data, int numberOfBytes)
        {
	        int c0 = 0;
	        int c1 = 0;
	        int checkSum1 = 0;
	        int checkSum2 = 0;

	        for (int i = 0; i < numberOfBytes; i++)
	        {
		        c0 += data[i];
		        c1 += c0;
	        }

            c0 %= 255; // c0 = c0 mod 255
            c1 %= 255; // c1 = c1 mod 255

	        checkSum1 = -(c0 + c1);
	        checkSum2 = c1;

	        checkSum1 %= 255;
	        checkSum2 %= 255;

	        if (checkSum1 <= 0)
	        {
		        checkSum1 = 255 + checkSum1;
	        }

	        if (checkSum2 <= 0)
	        {
		        checkSum2 = 255 + checkSum2;
	        }

	        return ((UInt16) ((checkSum1 << 8) | (checkSum2)));
        }

        /**
         * Rotina de calculo do BCH(63, 56), aplicado a camada de codificacao de TCs
         * do CCSDS. A rotina abaixo recebe um array de 7 bytes e devolve um byte com
         * os 7 bits de paridade calculados, mais um filler bit, conforme descrito em
         * ESA-PSS-04-107 Issue 2, item 5.3.1.
         * A rotina implementada nao eh otima, e foi criada a partir da figura 5.4 do
         * ESA-PSS-04-107 (descricao da implementacao por hardware).
         **/
        public static bool Bch6356(byte[] information, ref byte errorControl)
        {
            // Apenas aceito arrays de 7 bytes; se o tamanho for 
            // diferente, o BitArray sera criado de forma incorreta
            if (information.Length != 7)
            {
                return false;
            }

            // Transformo o array de bytes em um BitArray para trabalhar melhor
            BitArray infoBitsLittleEndian = new BitArray(information);
            BitArray infoBits = new BitArray(56);

            // Como o PC eh Little Endian, preciso inverter os bits
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < 7; i++) // do byte 0 ao 6 (7 bytes de dados)
                {
                    // Dentro de cada byte, inverte o bit
                    for (int j = 0; j < 8; j++)
                    {
                        infoBits[(i * 8) + j] = infoBitsLittleEndian[(i * 8) + 7 - j];
                    }
                }
            }

            BitArray parityBits = new BitArray(8, false); // inicializo os bits de paridade em zero

            for (int i = 0; i < infoBits.Length; i++)
            {
                // O bit 7 de parityBits eh usado como variavel temporaria
                parityBits[7] = (parityBits[0] ^ infoBits[i]); // XOR

                parityBits[0] = (parityBits[1] ^ parityBits[7]);
                parityBits[1] = parityBits[2];
                parityBits[2] = parityBits[3];
                parityBits[3] = parityBits[4];
                parityBits[4] = (parityBits[5] ^ parityBits[7]);
                parityBits[5] = parityBits[6];
                parityBits[6] = parityBits[7];
            }

            // Todos os bits de paridade devem entao ser complementados (invertidos)
            parityBits.Not();

            // Converte o BitArray em um byte novamente
            byte toReturn = (byte)0;

            // Como o PC eh Little Endian, nao posso usar o CopyTo e entao aplico bit a bit
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (parityBits[7 - i])
                    {
                        toReturn |= (byte)(1 << i);
                    }
                }
            }

            // Atribui errorControl, zerando o filler bit
            errorControl = (byte)(toReturn & 0xFE);

            return true;
        }

        /**
         * Calcula o CRC - ACE Amazonia_1 (CRC ANSI/IBM de 16 bits) 
         * fonte (adaptado): http://stackoverflow.com/questions/12372180/crc16-implementation-java
         **/
        public static UInt16 CrcAceAmazonia1(byte[] data, int numberOfBytes)
        {
            Int32 i;
            int crcValue = 0;
            for (int len = 0; len < data.Length; len++)
            {
                for (i = 0x80; i != 0; i >>= 1)
                {
                    if ((crcValue & 0x8000) != 0)
                    {
                        crcValue = (crcValue << 1) ^ 0x8005;
                    }
                    else
                    {
                        crcValue = crcValue << 1;
                    }
                    if ((data[len] & i) != 0)
                    {
                        crcValue ^= 0x8005;
                    }
                }
            }
            return (UInt16)crcValue;
        }

        /** Calcula o CRC do packet e retorna False caso nao tenha erros **/
        public static bool CrcError(byte[] packetInBytes)
        {
            // Verifica o crc
            UInt16 crc = CheckingCodes.CrcCcitt16(ref packetInBytes, packetInBytes.Length - 2);

            if ((packetInBytes[packetInBytes.Length - 2] == (byte)(crc >> 8)) &&
                (packetInBytes[packetInBytes.Length - 1] == (byte)(crc & 0xFF)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

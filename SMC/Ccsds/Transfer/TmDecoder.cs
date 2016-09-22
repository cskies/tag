/**
 * @file 	    TmDecoder.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 05/07/2013 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Este Namespace contem as classes para tratamento dos Frames de TC e TM.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer
{
    /**
     * @class TmDecoder
     * Esta classe realiza a decodificacao e extracao de um frame de TM CCSDS
     * a partir de um arquivo texto com "0" e "1". 
     * O tipo de decodificacao a ser utilizado eh definido pelas propriedades
     * UseConvolutional e UseReedSolomon.
     * A cada frame extraido, informacoes sobre a extracao ficam disponiveis
     * atraves das propriedades ValidCRC e BitIntervalFound.
     * 
     * @todo Esta classe foi criada na medida do necessario e seu codigo precisa
     * ser melhorado, trocando "numeros magicos" por constantes, por exemplo.
     **/
    class TmDecoder
    {
        private const int INTERLEAVING_LEVEL = 5;
        private const int FRAME_LENGTH = 1115;
        private const int RS_FRAME_LENGTH = 1275; // frame length + 160 bytes do Reed-Solomon

        #region Atributos

        private bool useConvolutional = false;
        private bool useReedSolomon = false;
        private bool bitIntervalFound = false;

        private bool validCrc = false;

        private String inputFilePath = "";

        private byte[] alphaElements = new byte[256]; // tabela dos elementos Alpha
        private byte[] alphaIndexes = new byte[256]; // tabela dos indices de Alpha

        // Vetores extraidos de CCSDS-131.0-b-1, a serem utilizados na conversao 
        // dual-canonica-dual (serao convertidos em matrizes de bits) 
        private byte[] canonicToDualVector = new byte[] { 0x8D, 0xEF, 0xEC, 0x86, 0xFA, 0x99, 0xAF, 0x7B };
        private byte[] dualToCanonicVector = new byte[] { 0xC5, 0x42, 0x2E, 0xFD, 0xF0, 0x79, 0xAC, 0xCC };

        // Valores gn de g(x) para o Reed-Solomon
        private byte[] generatorPolynomial = new byte[33];

        // Indices usados na busca do ASM. Estes sao usados nos shifts entre os bits do rxBuffer.
        private int currentBitPosition = 0; // Posicao do primeiro bit "depois" do ASM que foi encontrado - 0 se a busca por frames for iniciada.
        private int currentBytePosition = 0; // Armazena a posicao do primeiro byte do candidateCADU.
        private int currentBitOfSearchByte = 0; // A cada shift feito no candidateASM, esta variavel eh incrementada em 1. Armazena a posição atual do primeiro bit do primeiro byte do candidateASM.
        private int searchBytePosition = 0; // Posicao fixa do primeiro byte.

        // Indice do bit inicial do primeiro byte do bloco de verificacao do ASM.
        // Usado para guardar o indice do proximo bit do proximo byte depois do ASM.
        private int searchBitOfSearchByte = 0;

        // Tamanho do buffer aceito para decodificacao. 
        // Eh estatico para ser usado pela RS-422, Cortex e demais interfaces que recebam dados para decodificacao dos frames de TM.
        // Atualmente trabalha com 1024 pelo fato da placa RS-422 receber buffers de 1024 bytes. Funciona!
        public static int rxBufferSizeAcceptedToDecoder = 1024;

        // Armazena os 4 ultimos bytes do rxBuffer recebido.
        // Estes 4 bytes sao concatenados no inicio do rxBuffer seguinte.
        // Esta tarefa eh feita para tratar os casos onde um ASM podera estar quebrado entre dois rxBuffers.
        // Estes 4 ultimos bytes NAO sao concatenados no inicio do rxBuffer seguinte quando o ASM eh 
        // encontrado e o CADU esta sendo adquirido. Somente serao usados durante a busca pelo proximo ASM.
        // Os 4 ultimos bytes do rxBuffer anterior sao usados somente para busca do ASM.
        private byte[] temp4Bytes = new byte[4];

        // Este buffer eh usado para concatenar o rxBuffer e otemp4Bytes durante a busca pelo ASM.
        private byte[] rxBufferTemp = new byte[1028];

        // Este buffer armazena os bytes do rxBuffer recebido quando o evento de recepcao dos dados da placa RS-422 eh disparado.
        // Tambem usado na recepcao dos dados do Cortex para decodificacao.
        private byte[] rxBuffer = new byte[rxBufferSizeAcceptedToDecoder];
        private int numBytes = 0;

        // A principio, sera inicializado comtamanho de frame sem codificacao.
        private int frameSize = FRAME_LENGTH; // Sem codificacao tem 1115 bytes.

        // Buffer usado para montar o candidateCADU. A cada ASM encontrado, este buffer obtem os proximos bytes para montar o candidadeCADU.
        // Seu tamanho dependera do tipo de codificacao.
        private byte[] arrayCADU = new byte[FRAME_LENGTH];

        // Este byte sera usado quando o frame iniciar em byte quebrado.
        // Ao iniciar em byte quebrado, o frame devera ser shiftado para esquerda o numero de vezes necessario.
        // Se isso ocorrer, os ultimos bits do arrayCADU devem ser preenchidos com os proximos bits que se encontram no proximo byte.
        // Este proximo byte eh coletado sempre que o arrayCADU for preenchido completamente e sempre que o frame iniciar em byte quebrado.
        // Se o frame nao iniciar em byte quebrado, este byte nao sera usado.
        private byte nextByteToShift = 0x0;

        // Sinaliza o momento em que a obtencao do CADU deve ser feita.
        // Esta verificacao eh feita a cada rxBuffer recebido da placa.
        // Recebera "false" quando a aquisição do CADU for concluida e "true" quando o ASM for encontrado.
        // Depois que o CADU eh obtido, a busca pelo ASM deve ser continuada no bit seguinte ao ultimo do CADU.
        private bool startGetCADU = true;

        // Armazena o numero de bytes que deverao ser gravados no arrayCADU.
        private int numBytesINArrayCADU = 0;

        // Sinaliza o termino do rxBuffer quando o mesmo estiver sendo percorrido (para busca do ASM ou obtencao do CADU)
        private bool finishRxBuffer = false;

        // Informa se o primeiro byte do bloco de candidateASM deve ser quebrado para Convolucional.
        // Isto deve ser verificado porque o ASM com Convolucional tem 6 bytes e meio.
        private bool breakFirstByte = false;

        private enum ReceptionStatus { searchingAsm, gettingTentativeCADU };
        private ReceptionStatus receptionStatus = ReceptionStatus.searchingAsm;

        // Sinaliza se a decodificacao sera feita em tempo real. Se nao, sera via arquivo.
        private bool decodingRealTime = true;

        private int asmIndex = 0;
        private int count = 1; // DUVIDA-THIAGO: para que serve count e countCADU? Apenas incrementam. Sao para depuracao?
        private int countCADU = 1;

        // Declaracao da classe de argumentos. Usada para carregar os argumentos ao Event.
        private FrameFoundEventArgs frameFoundEventArgs = new FrameFoundEventArgs();

        // Evento que sera disparado sempre que um frame for encontrado. O mesmo disponibiliza o frame atraves dos argumentos da classe FrameFoundEventArgs.
        public event FrameFoundEventHandler frameFoundEventHandler = null;

        private short frameTypeField = -1;
        private bool dataWithAsm = false; // A principio, os dados gravados em arquivo nao estao com ASM.
        private bool discardIdleFrame = false; // A principio, nao descarta frame idle.

        #endregion

        #region Construtor

        /*
         * Ja no construtor, as tabelas utilizadas pelo codificador Reed-Solomon sao
         * inicializadas. Mesmo que nao sejam utilizadas sempre, optamos por deixar 
         * isso no construtor para simplificar a implementacao.
         * Baseado no codigo de Fabio Batagin Armelin (11/2007).
         **/
        public TmDecoder()
        {
            byte[] y = new byte[33]; // auxilia nos calculos de gn
            byte[] z = new byte[33]; // auxilia nos calculos de gn

            byte polynomial = 0x87; // polinomio gerador do Corpo de Galois
            byte n; // auxilia nos calculos de gn
            int index;

            // Gera uma tabela com os elementos do Corpo de Galois
            // o primeiro e ultimo elementos sao, respectivamente, 1 e 0
            alphaElements[0] = 1;
            alphaElements[255] = 0;

            for (int i = 1; i < 255; i++)
            {
                if ((alphaElements[i - 1] & 0x80) == 0x80)
                {
                    alphaElements[i] = (byte)((alphaElements[i - 1] << 1) ^ polynomial);
                }
                else
                {
                    alphaElements[i] = (byte)(alphaElements[i - 1] << 1);
                }
            }

            // Agora gera uma tabela com os indices dos elementos do Corpo de Galois
            for (int i = 0; i < 256; i++)
            {
                alphaIndexes[alphaElements[i]] = (byte)i;
            }

            // Inicializa os valores gn de g(X)
            for (int i = 0; i < 33; i++)
            {
                generatorPolynomial[i] = 0;
                y[i] = 0;
                z[i] = 0;
            }

            generatorPolynomial[0] = alphaElements[212]; // a[11*112] = a[1232] = a[212]
            generatorPolynomial[1] = alphaElements[0]; // X = a[0] * X

            // Calcula os valores gn de g(X)
            for (int i = 0; i < 31; i++)
            {
                index = (11 * (i + 1)) + 212;

                while (index >= 255)
                {
                    index = index - 255;
                }

                n = alphaElements[index];
                z[0] = 0;

                for (int j = 0; j < 32; j++)
                {
                    z[j + 1] = generatorPolynomial[j];

                    if (alphaIndexes[generatorPolynomial[j]] == 255)
                    {
                        y[j] = 0;
                    }
                    else
                    {
                        index = alphaIndexes[generatorPolynomial[j]] + alphaIndexes[n];

                        if (index >= 255)
                        {
                            index = index - 255;
                        }

                        y[j] = alphaElements[index];
                    }

                    generatorPolynomial[j] = (byte)(z[j] ^ y[j]);
                }

                generatorPolynomial[32] = z[32];
            }
        }

        #endregion

        #region Propriedades

        public String InputFilePath
        {
            get
            {
                return inputFilePath;
            }
            set
            {
                inputFilePath = value;

                if (inputFilePath.Equals(""))
                {
                    decodingRealTime = true;
                }
                else
                {
                    decodingRealTime = false;
                }
            }
        }

        public bool UseConvolutional
        {
            get
            {
                return (useConvolutional);
            }
            set
            {
                useConvolutional = value;

                if (decodingRealTime)
                {
                    // Atualizacao do tamanho do arrayCADU
                    if (useReedSolomon)
                    {
                        frameSize = RS_FRAME_LENGTH; // o RS adiciona 160 bytes de check symbols
                    }
                    else
                    {
                        frameSize = FRAME_LENGTH;
                    }

                    if (useConvolutional)
                    {
                        frameSize = frameSize * 2;
                    }

                    // Atualiza o tamanho do arrayCADU
                    Array.Resize(ref arrayCADU, frameSize);
                }
            }
        }

        public bool UseReedSolomon
        {
            get
            {
                return (useReedSolomon);
            }
            set
            {
                useReedSolomon = value;

                if (decodingRealTime)
                {
                    // Atualizacao do tamanho do arrayCADU
                    if (useReedSolomon)
                    {
                        frameSize = RS_FRAME_LENGTH; // o RS adiciona 160 bytes de check symbols
                    }
                    else
                    {
                        frameSize = FRAME_LENGTH;
                    }

                    if (useConvolutional)
                    {
                        frameSize = frameSize * 2;
                    }

                    // Atualiza o tamanho do arrayCADU
                    Array.Resize(ref arrayCADU, frameSize);
                }
            }
        }

        public bool BitIntervalFound
        {
            get
            {
                return bitIntervalFound;
            }
            set
            {
                bitIntervalFound = value;
            }
        }

        public bool ValidCrc
        {
            get
            {
                return validCrc;
            }
            set
            {
                validCrc = value;
            }
        }

        public byte[] RxBuffer
        {
            get
            {
                return rxBuffer;
            }
            set
            {
                count++;
                rxBuffer = value;

                if (decodingRealTime == true)
                {
                    // Procura por Frame em tempo de recepcao.
                    SearchFrameInRealTime();
                }
            }
        }

        public int NumBytes
        {
            get
            {
                return numBytes;
            }
            set
            {
                numBytes = value;
            }
        }

        public bool DataWithAsm
        {
            get
            {
                return dataWithAsm;
            }
            set
            {
                dataWithAsm = value;
            }
        }

        public bool DiscardIdleFrame
        {
            get
            {
                return discardIdleFrame;
            }
            set
            {
                discardIdleFrame = value;
            }
        }

        #endregion

        #region Metodos Publicos

        /**
         * Tenta extrair o proximo frame do arquivo-texto conforme as configuracoes
         * passadas para a classe (s/codificacao, Reed-Solomon, convolucional)
         * e retorna ao chamador. Se nao encontrar nenhum frame, retorna null.
         **/
        public byte[] GetNextFrame()
        {
            byte[] cadu = null;

            int previousIndex = currentBitPosition; // usado para identificar intervalos de bits entre frames
            int asmIndex = 0;

            if (dataWithAsm)
            {
                asmIndex = SearchAsm();
            }
            else
            {
                asmIndex = previousIndex;
            }

            bitIntervalFound = false;
            validCrc = false;

            if ((previousIndex > 0) && (asmIndex != previousIndex))
            {
                // Marca para alerta que ha intervalo entre um frame e outro
                if (useConvolutional)
                {
                    if ((asmIndex - previousIndex) != 8932)
                    {
                        // se usa convolucional, pela forma como buscamos, um intervalo fixo de 8932 bits esta ok
                        bitIntervalFound = true;
                    }
                }
                else
                {
                    bitIntervalFound = true;
                }
            }

            if (asmIndex < 0)
            {
                // Nao conseguiu encontrar o ASM
                return null;
            }

            cadu = ExtractTentativeCADU();

            if (cadu == null)
            {
                // O numero de bits restante no arquivo nao permite extrair um CADU
                return null;
            }
            else
            {
                if (useConvolutional)
                {
                    cadu = DecodeConvolutional(cadu);

                    if (cadu == null)
                    {
                        // Nao conseguiu decodificar
                        return null;
                    }
                }

                if (useReedSolomon)
                {
                    if (!CheckRsSymbols(cadu))
                    {
                        // Os simbolos RS nao batem.
                        return null;
                    }
                }

                if (CheckCrc(cadu)) // o CRC do frame bate
                {
                    validCrc = true;
                }

                if (useReedSolomon)
                {
                    currentBitPosition += (RS_FRAME_LENGTH * 8); // aponta para o primeiro bit apos o frame

                    // Extrai o check symbol do cadu antes de devolver
                    Array.Resize<byte>(ref cadu, FRAME_LENGTH);
                }
                else
                {
                    currentBitPosition += (FRAME_LENGTH * 8); // aponta para o primeiro bit apos o frame
                }

                return cadu;
            }
        }

        /** Reinicia os indices de busca dentro do arquivo. **/
        public void ResetFrameSearch()
        {
            currentBitPosition = 0;
        }

        /**
         * Calcula o check symbol Reed-Solomon da area de dados recebida
         * por parametro, e devolve ao chamador.
         * Baseado no codigo de Fabio Batagin Armelin (11/2007), a excessao
         * das conversoes dual-canonica-dual.
         **/
        public byte[] RSCheckSymbol(byte[] data)
        {
            byte[] reminder = new byte[255];
            int dataIndex = 0;

            byte[] canonicData = null;
            byte[] canonicCheckSymbol = new byte[32];
            byte[] dualCheckSymbol = null;

            // Converter data da base dual para canonica
            canonicData = MatrixBinaryMultiply(data, dualToCanonicVector);

            for (int i = 222; i >= 0; i--)
            {
                reminder[i + 32] = canonicData[dataIndex];
                dataIndex++;
            }

            byte[] a = new byte[223];

            // Calcula a divisao de polinomios
            for (int i = 222; i >= 0; i--)
            {
                a[i] = PolynomialDivision(reminder[i + 32], generatorPolynomial[32]);

                for (int j = 32; j >= 0; j--)
                {
                    reminder[j + i] = (byte)(PolynomialMultiply(a[i], generatorPolynomial[j]) ^ reminder[j + i]);
                }
            }

            // Agora reordena os bytes para retornar ao chamador
            dataIndex = 0;

            for (int i = 31; i >= 0; i--)
            {
                canonicCheckSymbol[dataIndex] = reminder[i];
                dataIndex++;
            }

            // Converter checkSymbol de base canonica para dual
            dualCheckSymbol = MatrixBinaryMultiply(canonicCheckSymbol, canonicToDualVector);

            return dualCheckSymbol;
        }

        #endregion

        #region Metodos Privados

        /**
         * Busca pela marca de sincronizacao (Attached Synchronization Marker)
         * no arquivo-texto a partir da ultima posicao processada, e retorna 
         * o indice do primeiro bit apos o ASM, ou seja, do primeiro bit do 
         * possivel CADU. Se nao envontrar o ASM, retorna -1.
         **/
        private int SearchAsm()
        {
            StreamReader file = File.OpenText(inputFilePath);
            string fileContents = file.ReadToEnd();
            file.Close();

            int searchBitPosition = currentBitPosition;

            int asmBits = 0;

            if (useConvolutional)
            {
                // Este nao eh o asm inteiro (que possui 64 bits), mas a parte dele que nao depende de bits anteriores
                asmBits = 56; // preciso apenas de 52 destes bits; 56 eh para converter corretamente no array de bytes
            }
            else
            {
                asmBits = 32;
            }

            char[] buffer = new char[asmBits];

            while (searchBitPosition < (fileContents.Length - asmBits))
            {
                byte[] bufferInBytes = BitStringToByteArray(fileContents.Substring(searchBitPosition, asmBits));

                if (bufferInBytes == null)
                {
                    // Erro ao processar o arquivo
                    return -1;
                }

                if (useConvolutional)
                {
                    long candidateASM = ((((long)bufferInBytes[0]) << 44) |
                                         (((long)bufferInBytes[1]) << 36) |
                                         (((long)bufferInBytes[2]) << 28) |
                                         (((long)bufferInBytes[3]) << 20) |
                                         (((long)bufferInBytes[4]) << 12) |
                                         (((long)bufferInBytes[5]) << 4) |
                                         (((long)bufferInBytes[6]) >> 4));

                    candidateASM &= (long)0xFFFFFFFFFFFFF;

                    if (candidateASM.Equals(0x81C971AA73D3E))
                    {
                        // O indice passa a ser o primeiro bit apos o ASM
                        currentBitPosition = searchBitPosition + 52;
                        return searchBitPosition;
                    }
                    else // nao encontrou o ASM ainda; incrementa o indice em um bit e tenta novamente
                    {
                        searchBitPosition++;
                    }
                }
                else
                {
                    // Nao uso o BitConverter para a conversao para evitar problemas no Endianess
                    int candidateASM = (int)((bufferInBytes[0] << 24) |
                                             (bufferInBytes[1] << 16) |
                                             (bufferInBytes[2] << 8) |
                                              bufferInBytes[3]);

                    if (candidateASM == 0x1ACFFC1D)
                    {
                        // O indice passa a ser o primeiro bit apos o ASM
                        currentBitPosition = searchBitPosition + 32;
                        return searchBitPosition;
                    }
                    else // nao encontrou o ASM ainda; incrementa o indice em um bit e tenta novamente
                    {
                        searchBitPosition++;
                    }
                }
            }

            return -1;
        }

        /**
         * Extrai um conjunto de bytes que, com base na posicao detectada
         * por SearchASM, provavelmente contem um CADU.
         **/
        private byte[] ExtractTentativeCADU()
        {
            StreamReader file = File.OpenText(inputFilePath);
            string fileContents = file.ReadToEnd();
            file.Close();

            int frameSize = 0;

            if (useReedSolomon)
            {
                frameSize = RS_FRAME_LENGTH * 8; // o RS adiciona 160 bytes de check symbols
            }
            else
            {
                frameSize = FRAME_LENGTH * 8;
            }

            if (useConvolutional)
            {
                frameSize = frameSize * 2;
            }

            byte[] toReturn = null;

            try
            {
                toReturn = BitStringToByteArray(fileContents.Substring(currentBitPosition, frameSize));
            }
            catch
            {
                // Nao preciso tratar o erro aqui; o erro que pode ocorrer eh com o Substring;
                // (posso ter encontrado um ASM no final do arquivo, que nao possui um
                // frame inteiro apos o ASM - o frame foi "quebrado" no final do arquivo)
                // apenas retorno nulo
            }

            return toReturn;
        }

        /**
         * Rotina de decodificacao convolucional baseada no codificador ilustrado
         * na figura 4.1, pagina 11, de ESA-PSS-04-103 Issue 1.
         * Implementacao baseada no algoritmo provido por Fabio Batagin Armelin 
         * em 06/2009.
         * @attention Esta rotina apenas decodifica mensagens sem erro, sem efetuar
         * a deteccao ou correcao de bits. Sua finalidade eh apenas validar a UTMC,
         * e nao deve ser utilizada para qualquer outro fim!!!
         **/
        private byte[] DecodeConvolutional(byte[] cadu)
        {
            BitArray data = new BitArray(7);
            BitArray caduAsBitsLittleEndian = new BitArray(cadu);
            BitArray caduAsBits = new BitArray(cadu.Length * 8);
            BitArray decodedCadu = new BitArray(caduAsBits.Length / 2);
            byte[] toReturnCadu = new byte[cadu.Length / 2];

            // Como o PC eh Little Endian, preciso inverter os bits
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < cadu.Length; i++)
                {
                    // Dentro de cada byte, inverte o bit
                    for (int j = 0; j < 8; j++)
                    {
                        caduAsBits[(i * 8) + j] = caduAsBitsLittleEndian[(i * 8) + 7 - j];
                    }
                }
            }
            else
            {
                // Essa linha nao sera executada na arquitetura x86
                caduAsBits = caduAsBitsLittleEndian;
            }

            bool symbol1 = false;
            bool symbol2 = false;

            // Inicializacao dos bits
            data[0] = false; // bit de dados decodificado
            data[1] = true;
            data[2] = false;
            data[3] = true;
            data[4] = true;
            data[5] = true;
            data[6] = false;

            for (int i = 0; i < caduAsBits.Length; i += 2)
            {
                symbol1 = caduAsBits[i];
                symbol2 = !caduAsBits[i + 1];

                if ((symbol1 ^ data[6] ^ data[3] ^ data[2] ^ data[1]) ==
                    (symbol2 ^ data[6] ^ data[5] ^ data[3] ^ data[2]))
                {
                    data[0] = symbol1 ^ data[6] ^ data[3] ^ data[2] ^ data[1];
                    data[6] = data[5];
                    data[5] = data[4];
                    data[4] = data[3];
                    data[3] = data[2];
                    data[2] = data[1];
                    data[1] = data[0];

                    decodedCadu[i / 2] = data[0];
                }
                else
                {
                    // Nao conseguiu decodificar algum bit; 
                    // cancela a decodificacao do convolucional
                    return null;
                }
            }

            // Converte o BitArray em array de bytes para retornar
            for (int i = 0; i < toReturnCadu.Length; i++)
            {
                byte convertedByte = (byte)0;

                if (BitConverter.IsLittleEndian)
                {
                    // Como o PC eh Little Endian, nao posso usar o CopyTo
                    // entao aplico bit a bit
                    for (int j = 0; j < 8; j++)
                    {
                        if (decodedCadu[(i * 8) + 7 - j])
                        {
                            convertedByte |= (byte)(1 << j);
                        }
                    }
                }

                toReturnCadu[i] = convertedByte;
            }

            return toReturnCadu;
        }

        /** 
         * A verificacao da codificacao eh feita por similaridade, ou seja, CheckRSSymbols 
         * refaz a codificacao (com interleaving nivel 5) e compara o resultado. Nao eh 
         * feita a decodificacao nem a correcao. ATENCAO: este metodo foi desenvolvido apenas
         * para a validacao da UTMC, e NAO DEVE SER UTILIZADA EM UM DECODIFICADOR DE TM!!!
         **/
        private bool CheckRsSymbols(byte[] cadu)
        {
            /** 
             * @todo 
             * Ja tenho uma constante INTERLEAVING_LEVEL, mas para que o codificador
             * seja totalmente parametrizado, precido fazer com que as demais constantes
             * deste metodo (1275, 1115, 223 e demais) sejam definidos em funcao de 
             * INTERLEAVING_LEVEL.
             **/
            if (cadu.Length != RS_FRAME_LENGTH) // 1115 data + 160 check symbols
            {
                return (false);
            }

            // Um laco de loop para cada decodificador, de zero ate INTERLEAVING_LEVEL
            for (int encoderIndex = 0; encoderIndex < INTERLEAVING_LEVEL; encoderIndex++)
            {
                byte[] data = new byte[223];
                int dataIndex = 0;

                // Extrai os dados para o codificador de nr. [encoderIndex]
                for (int i = 0; i < FRAME_LENGTH; i += INTERLEAVING_LEVEL)
                {
                    data[dataIndex] = cadu[i + encoderIndex];
                    dataIndex++;
                }

                byte[] calculatedCheckSymbol = RSCheckSymbol(data);

                byte[] receivedCheckSymbol = new byte[32];

                // Extrai os dados para o check symbol para codificador de nr. [encoderIndex]
                dataIndex = 0;
                for (int i = 0; i < 160; i += INTERLEAVING_LEVEL)
                {
                    receivedCheckSymbol[dataIndex] = cadu[FRAME_LENGTH + encoderIndex + i];
                    dataIndex++;
                }

                // Extrai do cadu o check symbol do codificador de nr. [encoderIndex]
                if (!EqualArrays(receivedCheckSymbol, calculatedCheckSymbol))
                {
                    // O RS calculado difere do recebido
                    return false;
                }
            }

            return true;
        }

        /**
         * Compara dois arrays de bytes. A unica forma que encontrei de
         * fazer isso corretamente eh comparando elemento a elemento.
         * (ha outra maneira, usando IEnumerable, mas nao vale a pena
         * implementar devido a complexidade.
         **/
        private bool EqualArrays(byte[] array1, byte[] array2)
        {
            if (array1 == null)
            {
                return false;
            }

            if (array2 == null)
            {
                return false;
            }

            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        /** Verifica o CRC do CADU. **/
        static public bool CheckCrc(byte[] cadu)
        {
            UInt16 crc = CheckingCodes.CrcCcitt16(ref cadu, FRAME_LENGTH - 2);

            if ((cadu[FRAME_LENGTH - 2] == (byte)(crc >> 8)) &&
                (cadu[FRAME_LENGTH - 1] == (byte)(crc & 0xFF)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /** Converte uma string do tipo "01010101" em um array de bytes. */
        private byte[] BitStringToByteArray(String stringOfBits)
        {
            int value = 0;
            int bitIndex = 7;
            byte completeByte = 0;

            byte[] toReturn = new byte[stringOfBits.Length / 8];
            int nextIndex = 0;

            // Faz um loop na string, converte os caracteres em valores, 
            // compoe os bytes e alimentam o array a retornar
            for (int i = 0; i < stringOfBits.Length; i++)
            {
                if (stringOfBits.Substring(i, 1).Equals("0"))
                {
                    value = 0;
                }
                else if (stringOfBits.Substring(i, 1).Equals("1"))
                {
                    value = 1;
                }
                else
                {
                    // ha algum caractere invalido
                    return null;
                }

                completeByte = (byte)(completeByte | (value << bitIndex));
                bitIndex--;
                if (bitIndex < 0)
                {
                    toReturn[nextIndex] = completeByte;

                    bitIndex = 7;
                    nextIndex++;
                    completeByte = 0;
                }
            }

            return toReturn;
        }

        /** Multiplicacao polinomial para o Reed-Solomon. */
        private byte PolynomialMultiply(byte a, byte b)
        {
            if ((a == 0) || (b == 0))
            {
                return 0;
            }
            else
            {
                int index = alphaIndexes[a] + alphaIndexes[b];

                if (index >= 255)
                {
                    index = index - 255;
                }

                return alphaElements[index];
            }
        }

        /** Divisao polinomial para o Reed-Solomon */
        private byte PolynomialDivision(byte a, byte b)
        {
            if (a == 0)
            {
                return 0;
            }
            else
            {
                int index;

                if (alphaIndexes[a] >= alphaIndexes[b])
                {
                    index = alphaIndexes[a] - alphaIndexes[b];
                }
                else
                {
                    index = (255 - alphaIndexes[b]) + alphaIndexes[a];
                }

                return alphaElements[index];
            }
        }

        /**
         * Recebe dois vetores de bytes, converte-os em matrizes de bits,
         * faz a multiplicacao binaria (XOR) deles, converte a matriz
         * resultante de volta em um vetor de bytes e a devolve ao chamador.
         * Utilizado para fazer as conversoes de base dual-canonica-dual do
         * codificador Reed-Solomon.
         **/
        private byte[] MatrixBinaryMultiply(byte[] vectorA, byte[] vectorB)
        {
            BitArray[] matrixA = new BitArray[vectorA.Length];
            BitArray[] matrixB = new BitArray[vectorB.Length];

            // A saida deve ter o mesmo numero de linhas da matriz A
            BitArray[] productMatrix = new BitArray[vectorA.Length];
            byte[] toReturn = new byte[vectorA.Length];

            byte[] oneByte = new byte[1];

            // Alimenta a matrix de bits do vetor A, e inicializa a matriz do produto
            for (int i = 0; i < vectorA.Length; i++)
            {
                oneByte[0] = vectorA[i];

                BitArray inverted = new BitArray(oneByte);
                matrixA[i] = new BitArray(8);

                if (BitConverter.IsLittleEndian)
                {
                    // Como o PC eh Little Endian, inverto os bits
                    for (int j = 0; j <= 7; j++)
                    {
                        matrixA[i][j] = inverted[7 - j];
                    }
                }

                productMatrix[i] = new BitArray(8);
            }

            // Alimenta a matriz de bits do vetor B
            for (int i = 0; i < vectorB.Length; i++)
            {
                oneByte[0] = vectorB[i];

                BitArray inverted = new BitArray(oneByte);
                matrixB[i] = new BitArray(8);

                if (BitConverter.IsLittleEndian)
                {
                    // Como o PC eh Little Endian, inverto os bits
                    for (int j = 0; j <= 7; j++)
                    {
                        matrixB[i][j] = inverted[7 - j];
                    }
                }
            }

            // Faz a multiplicacao das matrizes
            for (int i = 0; i < matrixA.Length; i++) // loop das linhas de A
            {
                for (int j = 0; j <= 7; j++) // loop das colunas de B
                {
                    productMatrix[i][j] = false; // = 0

                    for (int k = 0; k <= 7; k++) // loop das colunas de A
                    {
                        productMatrix[i][j] = (productMatrix[i][j] ^ (matrixA[i][k] & matrixB[k][j]));
                    }
                }
            }

            // Converte a matriz resultante em um vetor de bytes para retornar
            for (int i = 0; i < productMatrix.Length; i++)
            {
                byte convertedByte = (byte)0;

                if (BitConverter.IsLittleEndian)
                {
                    // Como o PC eh Little Endian, nao posso usar o CopyTo
                    // entao aplico bit a bit
                    for (int j = 0; j < 8; j++)
                    {
                        if (productMatrix[i][7 - j])
                        {
                            convertedByte |= (byte)(1 << j);
                        }
                    }
                }

                toReturn[i] = convertedByte;
            }

            return toReturn;
        }

        #endregion

        #region Metodos usados na identificacao de frames durante a recepcao continua de dados pela UTMC

        /**
         * Este metodo verifica a existencia de frames durante a recepcao de dados da serial RS-422.
         * A mesma procura pela palavra de sincronizacao chamada ASM e a partir dela extrai 
         * o CADU candidato a ser um Frame. Esta verificacao eh feita para todos os rxBuffers recebidos
         * da placa, byte a byte. Todos os bytes de todos os rxBuffers sao verificados.
         **/
        // DUVIDA-THIAGO: essa rotina nao eh mais exclusiva para a 422, certo? Rever a documentacao.
        public byte[] SearchFrameInRealTime()
        {
            // Atualizar para false porque todos os rxBuffers devem ser percorridos.
            // Sera true quando as verificacoes chegarem ao final do rxBuffer.
            finishRxBuffer = false;

            validCrc = false;

            // A busca pelo ASM e obtencao do CADU devem ser feitas de forma continua.
            // Por isso, todos os rxBuffers recebidos da placa RS-422 devem ser percorridos, 
            // seja para procura do ASM, seja para obtencao do CADU. Ao encontrar o ASM, 
            // o CADU deve ser obtido logo em seguida. Ao obter o CADU, 
            // a busca pelo proximo ASM deve ser iniciada logo em seguida (no próximo bit).
            // Ao chegar no final do rxBuffer, sai deste loop para aguardar o proximo rxBuffer
            while (!finishRxBuffer)
            {
                byte[] cadu = null;

                int previousIndex = currentBitPosition; // usado para identificar intervalos de bits entre frames

                if (receptionStatus == ReceptionStatus.searchingAsm)
                {
                    // Copia os 1024 bytes recebidos para o rxBufferTemp, iniciando da posicao 4
                    Array.Copy(rxBuffer, 0, rxBufferTemp, 4, rxBuffer.Length);

                    // Copia os 4 ultimos bytes do ultimo rxBuffer para os 4 primeiros do rxBuffer atual
                    rxBufferTemp[0] = temp4Bytes[0];
                    rxBufferTemp[1] = temp4Bytes[1];
                    rxBufferTemp[2] = temp4Bytes[2];
                    rxBufferTemp[3] = temp4Bytes[3];

                    // @bug DURANTE A RECEPCAO, UM ASM EH PERDIDO A CADA CERTO CONJUNTO DE FRAMES RECEBIDOS. 
                    // ESTE ASM EH ENCONTRADO NO MEIO DA AREA DE DADOS DO FRAME, QUE EH INDICADO COMO ERRADO NA INTERFACE GRAFICA.
                    //
                    asmIndex = SearchAsmInRealTime();

                    if ((previousIndex > 0) && (asmIndex != previousIndex))
                    {
                        // Marca para alerta que ha intervalo entre um frame e outro
                        if (useConvolutional)
                        {
                            if ((asmIndex - previousIndex) != 8932)
                            {
                                // Se usa convolucional, pela forma como buscamos, um intervalo fixo de 8932 bits esta ok
                                bitIntervalFound = true;
                            }
                        }
                        else
                        {
                            bitIntervalFound = true;
                        }
                    }

                    if (asmIndex >= 0)
                    {
                        receptionStatus = ReceptionStatus.gettingTentativeCADU;
                        cadu = ExtractTentativeCADUInRealTime();
                    }
                    else
                    {
                        // Chegou no final do rxBuffer e nao encontrou nenhum asm, entao nao tenta extrair o cadu ainda
                        finishRxBuffer = true;
                    }
                }
                else
                {
                    cadu = ExtractTentativeCADUInRealTime();
                }

                if (cadu != null)
                {
                    cadu = ShiftByteArrayCADU(cadu, searchBitOfSearchByte);

                    if (frameFoundEventHandler != null)
                    {
                        if (discardIdleFrame)
                        {
                            if (!FrameIdle(ref cadu))
                            {
                                frameFoundEventArgs.FrameFound = cadu;

                                // Verificar se o CADU obtido eh um Frame. Se o CADU for um Frame, verificar se ele eh um Frame valido.
                                // Esta verificacao eh feita para sem codificacao.
                                if (CheckCrc(cadu)) // o CRC do frame bate
                                {
                                    validCrc = true;
                                }

                                frameFoundEventArgs.FrameIsValid = validCrc;

                                // Instancia o EventHandler e passa o object sender e o EventArgs com os argumentos.
                                frameFoundEventHandler(this, frameFoundEventArgs);
                            }
                        }
                        else
                        {
                            frameFoundEventArgs.FrameFound = cadu;

                            // Verificar se o CADU obtido eh um Frame. Se o CADU for um Frame, verificar se ele eh um Frame valido.
                            // Esta verificacao eh feita para sem codificacao.
                            if (CheckCrc(cadu)) // o CRC do frame bate
                            {
                                validCrc = true;
                            }

                            frameFoundEventArgs.FrameIsValid = validCrc;

                            // Instancia o EventHandler e passa o object sender e o EventArgs com os argumentos.
                            frameFoundEventHandler(this, frameFoundEventArgs);
                        }
                    }

                    receptionStatus = ReceptionStatus.searchingAsm;
                    countCADU++;
                }
            }

            return null;
        }

        /**
         * Busca pela marca de sincronizacao (Attached Synchronization Marker)
         * nos rxBuffers que sao obtidos da placa RS-422. Retorna o indice do 
         * primeiro bit apos o ASM, ou seja, do primeiro bit do possivel CADU. 
         * Se nao envontrar o ASM, retorna -1.
         **/
        private int SearchAsmInRealTime()
        {
            // inicializa com 4 bytes, mas se for convolucional
            // atribui 7 para o tamanho do bloco a ser verificado.
            int sizeBuffer = 4;
            int mask = 0x100;
            useConvolutional = false;

            if (useConvolutional)
            {
                // Como sao 52 bits que equivalem a 6 bytes e meio, pego um bloco de 7 bytes e verifico do 
                // primeiro ate a metade do ultimo byte deste bloco.
                sizeBuffer = 7;
            }

            // Posicao do primeiro byte do bloco a ser selecionado.
            // Esta posicao eh incrementada em 1 se o bloco ser shiftado
            // 8 posicoes aa esquerda e o asm nao ser encontrado
            searchBytePosition = currentBytePosition;

            // Posicao do ultimo byte do bloco selecionado.
            // Tambem eh incrementado em 1 igual a variavel searchBytePosition.
            int finishBytePosition = searchBytePosition + (sizeBuffer - 1); // O buffer comeca na posicao 0.

            // Posicao do byte seguinte ao ultimo do bloco selecionado.
            // Os bits deste byte sao introduzidos bit-a-bit da direita para esquerda a cada shift do bloco.
            int nextBytePosition = finishBytePosition + 1;

            // Este valor eh usado para marcar o indice do bit do proximo byte que sera shiftado para esquerda.
            int bitNextByte = 8; // tem que comecar com valor 8 para nao pegar nenhum bit na primeira verificacao.

            // Inicia o shift em 0 para que na primeira vez em que o bloco for selecionado, nao fazer nenhum shift, para que logo de cara comparar para ver se ja eh o ASM
            int shift = 0;

            // Este indice passa a ser o primeiro bit apos o ultimo ASM encontrado
            int searchBitPosition = currentBitPosition;

            // Indice do bit inicial do primeiro byte do bloco de verificacao do ASM
            // Usado para guardar o indice do proximo bit do proximo byte depois do ASM.            
            searchBitOfSearchByte = currentBitOfSearchByte;

            //==================================================
            //
            // !!!!!!Atencao!!!!!!
            //
            // PARA O USO DO CONVOLUCIONAL, A BUSCA PELO ASM TAMBEM ESTA SENDO FEITA CORRETAMENTE, POREM, AINDA NAO ESTA SENDO TRATADO 
            // A BUSCA DE ASM QUEBRADO (OS QUE INICIAM NO FINAL DO BUFFER ATUAL E TERMINAM NO INICIO DO BUFFER SEGUINTE)
            // MAS ISSO NAO ESTA SENDO TRATADO APENAS PARA A CODIFICACAO CONVOLUCIONAL. ATUALMENTE, ESTAO SENDO USADOS OS 4 ULTIMOS 
            // BYTES DO BUFFER (ASM SEM CODIFICACAO = 32 BITS) PORQUE O SMC ESTA ADQUIRINDO FRAMES SEM CODIFICACAO 
            // (**FRAMES COM CODIFICACAO SERAO OBTIDOS SOMENTE NO EAA DO EGSE**). PARA O CONVOLUCIONAL DEVERAO SER USADOS 
            // 6 BYTES E MEIO. **ISTO SERA IMPLEMENTADO SOMENTE NO SOFTWARE DE TEMPO REAL QUE SERA IMPLANTADO NO EAA DO EGSE**
            //
            //==================================================

            // A posicao inicial da busca pelo ASM inicia em 0.
            while (nextBytePosition <= rxBufferTemp.Length)
            {
                // Os codigos de busca do ASM sem Codificacao e Convolucional sao diferentes.
                if (useConvolutional)
                {
                    long candidateASM = 0; // recebe os 6 bytes e meio (52 bits)
                    long candidateASMTemp = 0; // recebe os 7 bytes (56 bits)

                    // Pega os 7 bytes do rxBufferTemp.
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 0]) << 48));
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 1]) << 40));
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 2]) << 32));
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 3]) << 24));
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 4]) << 16));
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 5]) << 8));
                    candidateASMTemp |= ((((long)rxBufferTemp[searchBytePosition + 6])));

                    long nextByte = 0;

                    if (nextBytePosition < rxBufferTemp.Length)
                    {
                        nextByte = (long)rxBufferTemp[nextBytePosition];
                    }

                    // Como o convolucional eh um valor quebrado (6 bytes e meio, equivalente a 52 bits), 
                    // a cada dois blocos de verificacao do buffer, os 4 primeiros bits do primeiro byte 
                    // sao shiftados apenas para dar continuidade a partir do ultimo bit verificado no 
                    // bloco anterior. Estes mesmos 4 bits, ao serem shiftados nao causam efeito nenhum.
                    // Este if eh usado para atualizar a variavel searchBitPosition.
                    if (breakFirstByte)
                    {
                        breakFirstByte = false;

                        // deve ser decrementado 4 posicoes (0 ate 3) porque o primeiro byte do bloco
                        // que esta sendo verificado deve ser quebrado. Isso significa que estou verificando
                        // novamente os 4 primeiros bits do primeiro byte deste bloco.
                        searchBitPosition -= 3;
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        // O shift eh feito nos 7 bytes (56 bits), mas a verificacao do candidateASM eh feita somente nos 6 bytes e meio (52 bits).
                        candidateASMTemp = candidateASMTemp << shift;
                        candidateASMTemp &= 0xffffffffffffff;
                        candidateASMTemp |= (long)(nextByte & mask) >> bitNextByte; // Os bits do proximo byte auxiliar sao obtidos da esquerda para direita.

                        // Atualiza a maskara para pegar o proximo bit (da esquerda para direita) do proximo byte auxiliar "nextByte".
                        bitNextByte--;
                        mask = mask >> 1;

                        // Pega apenas os 6 bytes e meio (52 bits).
                        candidateASM = candidateASMTemp & 0xfffffffffffff0;

                        if (candidateASM == 0x81C971AA73D3E0)
                        {
                            // O indice passa a ser o primeiro bit apos o ASM
                            currentBitPosition = searchBitPosition + 52;

                            if (i > 0)
                            {
                                // A variavel i sera > 0 quando o ASM encontrado iniciar em byte quebrado e concluir em byte completo
                                currentBytePosition = ((searchBytePosition + sizeBuffer));
                                nextBytePosition = ((nextBytePosition + sizeBuffer));
                            }
                            else
                            {
                                // A variavel i sera == 0 quando o ASM encontrado iniciar em byte que nao eh quebrado e terminar em byte quebrado.
                                // Subtrai 1 para dar continuidade na busca pelo ASM porque o ultimo byte do bloco foi quebrado. 
                                // Preciso continuar deste mesmo byte porque ainda faltam os 4 ultimos bits para serem verificados.
                                currentBytePosition = (searchBytePosition + sizeBuffer) - 1;
                                nextBytePosition = (nextBytePosition + sizeBuffer) - 1;
                                breakFirstByte = true;
                            }

                            shift = 1; // na proxima passagem o bloco deve ser shiftado. shift = 0 significa: nao faca nenhum shift.
                            return searchBitPosition;
                        }
                        else if (shift == 1)
                        {
                            // nao encontrou o ASM ainda; incrementa o indice em um bit e tenta novamente
                            searchBitPosition++;
                        }

                        shift = 1;
                    }

                    // atualizar os indices para iniciar a verificacao com outro bloco de bits
                    searchBytePosition++;
                    finishBytePosition++;
                    nextBytePosition++;
                    bitNextByte = 8;
                    shift = 0; // shift = 0 porque ao pegar o proximo bloco de bytes, verifico se ja eh o ASM. Por isso nao faco nenhum shift.
                    mask = 0x100;
                }
                else
                {
                    uint candidateASM = 0;

                    // Pega os 4 primeiros bytes
                    candidateASM |= (uint)((rxBufferTemp[searchBytePosition + 0] << 24));
                    candidateASM |= (uint)((rxBufferTemp[searchBytePosition + 1] << 16));
                    candidateASM |= (uint)((rxBufferTemp[searchBytePosition + 2] << 8));
                    candidateASM |= (uint)(rxBufferTemp[searchBytePosition + 3]);

                    uint nextByte = 0;

                    if (nextBytePosition < rxBufferTemp.Length)
                    {
                        nextByte = (uint)rxBufferTemp[nextBytePosition];
                    }

                    // Verifica se o indice do proximo bit seguido do ultimo ASM encontrado inicia em byte quebrado.
                    // Se for > 0 significa que o primeiro byte do candidateASM devera ser shiftado (ou seja, quebrado).
                    if (searchBitOfSearchByte > 0)
                    {
                        // Shift para esquerda para posicionar o primeiro bit apos o ultimo ASM no inicio dos 32 bits a serem verificados.
                        candidateASM = candidateASM << (searchBitOfSearchByte);

                        // Pega os ultimos bits do proximo byte depois do bloco. 
                        // Ex: Se o primeiro bit dos 32 bits estiver na posicao 3 do 
                        // primeiro byte, o "nextByte" tambem devera ser shiftado (8 - 3).
                        candidateASM |= nextByte >> (8 - (searchBitOfSearchByte));

                        // Atualiza o indice do "bitNextByte" e da maskara para dar continuidade na busca pelo proximo ASM.
                        // Subtrai a mesma quantia de bits que ja foram shiftados
                        bitNextByte = bitNextByte - (searchBitOfSearchByte);
                        mask = mask >> (searchBitOfSearchByte); // subtrai a mesma quantia de bits da mascara.
                    }

                    // Inicia o shift bit a bit. Na primeira passagem nao faz nenhum shift porque os primeiros bits poderao ser um ASM.
                    for (int i = (searchBitOfSearchByte - 1); i < 8; i++)
                    {
                        candidateASM = candidateASM << shift; // Shifta os 4 bytes
                        candidateASM &= 0xffffffff;

                        // Obtem o proximo bit do "nextByte". O shift eh feito da direita para esquerda para pegar cada bit (da esquerda para direita).
                        candidateASM |= (uint)(nextByte & mask) >> bitNextByte;

                        // Atualiza o indice do proximo bit do "nextByte" e a maskara para pegar o proximo bit (da esquerda para direita).
                        bitNextByte--;
                        mask = mask >> 1;

                        if (candidateASM == 0x1ACFFC1D)
                        {
                            // Atualiza o indice do primeiro bit apos o ultimo bit do ultimo ASM encontrado.
                            currentBitPosition = searchBitPosition + 32;

                            // Atualiza os indices do proximo bloco que devera ser obtido para dar continuidade aa verificacao do proximo ASM.
                            currentBytePosition = (searchBytePosition + sizeBuffer); // Armazena o indice do primeiro byte do candidateCADU.
                            nextBytePosition = (nextBytePosition + sizeBuffer);

                            // Verifica se o ASM encontrado inicia em byte quebrado. 
                            // Se i >= 0, guarda o indice do primeiro bit do proximo bloco de bits a serem verificados na busca pelo ASM.
                            if (i >= 0)
                            {
                                // Atualiza a posicao do primeiro bit do proximo bloco.
                                currentBitOfSearchByte = i + 1;
                            }

                            searchBitOfSearchByte = currentBitOfSearchByte;
                            return searchBitPosition;
                        }
                        else if (shift == 1) // Nao encontrou o ASM ainda; incrementa o indice em um bit e tenta novamente
                        {
                            searchBitPosition++;
                        }

                        shift = 1;
                    }

                    // Atualizar os indices para iniciar a verificacao com outro bloco de bytes
                    searchBytePosition++;
                    finishBytePosition++;
                    nextBytePosition++;
                    searchBitOfSearchByte = 0;
                    bitNextByte = 8;
                    shift = 0; // Shift = 0 para que na primeira passagem nao faca nenhum shift, pois o proximo bloco de bits ja podem ser um ASM.
                    mask = 0x100; // mask = 100000000. Este valor faz com que nenhum bit do "nextByte" seja obtido na primeira passagem.
                }
            }

            // Reinicia indicadores para que ao receber novo rxBuffer, seja feita nova busca por novo ASM.
            currentBytePosition = 0;
            currentBitPosition = 0;
            currentBitOfSearchByte = 0;

            // Adquire os 4 ultimos bytes do rxBufferTemp atual para serem usados no início do proximo rxBuffer para busca do proximo ASM.
            temp4Bytes[0] = rxBufferTemp[1024];
            temp4Bytes[1] = rxBufferTemp[1025];
            temp4Bytes[2] = rxBufferTemp[1026];
            temp4Bytes[3] = rxBufferTemp[1027];

            // Verifica se o primeiro byte dos 4 ultimos obtidos inicia com um suposto comeco de ASM.
            // 1A = 11010. Este valor eh o inicio de um ASM. Se eu leva-lo para o proximo rxBuffer,
            // corro o risco de gerar um novo ASM sem existir.
            if (temp4Bytes[0] == 0x1A)
            {
                temp4Bytes[0] = 0x0;
            }

            return -1;
        }

        /**
         * Extrai um conjunto de bytes a partir do bit seguinte ao ultimo do ASM encontrado.
         **/
        private byte[] ExtractTentativeCADUInRealTime()
        {
            try
            {
                int numBytesToWriteArrayCADU = 0; // Numero de bytes que deverao ser inseridos no arrayCADU.

                // Inicia a obtencao do CADU.
                // startGetCADU = indica se ja pode ser iniciada a obtencao do CADU "agora".
                // Entrara no "else" para nao correr o risco de duplicar os 4 ultimos bytes que sao usados na busca pelo ASM.
                if (startGetCADU)
                {
                    // Resetar o arrayCADU
                    Array.Clear(arrayCADU, 0, arrayCADU.Length);

                    // Calcula a quantidade de bytes existentes no RxBuffer atual que farao parte do CADU, - 4 para extrair os 4 primeiros bytes que foram concatenados ao rxBufferTemp.
                    numBytesToWriteArrayCADU = rxBuffer.Length - (currentBytePosition - 4); // "currentBytePosition" armazena a posicao do primeiro byte do CADU.
                    Array.Copy(rxBuffer, (currentBytePosition - 4), arrayCADU, 0, numBytesToWriteArrayCADU);

                    numBytesINArrayCADU += numBytesToWriteArrayCADU;
                    startGetCADU = false; // Esta sera "true" quando o CADU for obtido por completo.
                    finishRxBuffer = true; // rxBuffer inteiro inserido no arrayCADU.
                }
                else
                {
                    // Calcula a quantidade de bytes que faltam para preencher o arrayCADU
                    numBytesToWriteArrayCADU = (arrayCADU.Length - numBytesINArrayCADU);

                    // Verifica se sera necessario pegar todos os bytes do rxBuffer atual.
                    // Este ocorrera se o ASM for encontrado no final do rxBuffer anterior.
                    // Ex: o ASM foi encontrado no byte 1018 do rxBuffer anterior. Isto significa que
                    // aqui o arrayCADU tera apenas 7 bytes (1024 - 1018). 
                    // Isto significa que o rxBuffer atual devera ser copiado por completo.
                    if (numBytesToWriteArrayCADU >= rxBuffer.Length)
                    {
                        Array.Copy(rxBuffer, 0, arrayCADU, numBytesINArrayCADU, rxBuffer.Length);
                        numBytesINArrayCADU += rxBuffer.Length;
                        startGetCADU = false;
                        finishRxBuffer = true;
                    }
                    else if (numBytesToWriteArrayCADU < rxBuffer.Length)
                    {
                        // Aqui o CADU ja eh obtido por completo.
                        Array.Copy(rxBuffer, 0, arrayCADU, numBytesINArrayCADU, numBytesToWriteArrayCADU);

                        if (searchBitOfSearchByte > 0)
                        {
                            nextByteToShift = rxBuffer[numBytesToWriteArrayCADU];
                            currentBytePosition = numBytesToWriteArrayCADU;
                        }
                        else
                        {
                            currentBytePosition = numBytesToWriteArrayCADU;
                        }

                        numBytesINArrayCADU += numBytesToWriteArrayCADU;

                        // Atualiza a "startGetCADU" para true para continuar as buscas pelo proximo CADU.
                        startGetCADU = true;

                        // Ainda nao chegou no final do rxBuffer. A busca pelo ASM deve ser continuada.
                        finishRxBuffer = false;
                    }
                }

                // Verifica se o CADU ja foi obtido.
                if (numBytesINArrayCADU == arrayCADU.Length)
                {
                    // Zera o numero de bytes obtidos para o arrayCADU para iniciar 
                    // uma nova contagem durante a obtencao do proximo CADU.
                    numBytesINArrayCADU = 0;
                    return arrayCADU;
                }
            }
            catch
            {
                // Nao preciso tratar o erro aqui; o erro que pode ocorrer eh com o Substring;
                // (posso ter encontrado um ASM no final do arquivo, que nao possui um
                // frame inteiro apos o ASM - o frame foi "quebrado" no final do arquivo)
                // apenas retorno nulo
            }

            return null;
        }

        /**
         * Shift do conjunto de CADUBytes. Este shift eh feito sempre que o primeiro bit do CADU iniciar em byte quebrado.
         **/
        private byte[] ShiftByteArrayCADU(byte[] array, int numShifts)
        {
            try
            {
                for (int i = 0; i < (array.Length - 1); i++)
                {
                    array[i] = (byte)(((array[i]) << numShifts) | ((array[i + 1]) >> (8 - numShifts)));
                }

                if (numShifts > 0)
                {
                    byte aux = array[array.Length - 1];

                    for (int i = 0; i < numShifts; i++)
                    {
                        aux = (byte)((aux << 1));
                        aux &= 0xfe;
                    }

                    // Adiciona ao ultimo byte do arrayCADU o seu valor shiftado mais os proximos bits do byte seguinte.
                    array[array.Length - 1] = ((byte)(aux | ((byte)(nextByteToShift >> (8 - numShifts)))));
                }

                return array;
            }
            catch (Exception)
            {
            }

            return null;
        }

        /**
         * Verifica se eh Frame Idle. Para identificacao de um Frame Idle, eh necessario adquirir o campo referente no frame.
         * Este campo eh iniciado do bit 38 e tem tamanho 11. Um Frame Idle tem 
         * neste trecho as seguintes combinacoes binarias: "11111111110" = 2046.
         **/
        private bool FrameIdle(ref byte[] frame)
        {
            frameTypeField = -1;
            frameTypeField = (short)(((short)(((short)((frame[4] << 6)) | ((short)(frame[5]))) << 3)) >> 3);

            if (frameTypeField == 2046)
            {
                return true;
            }

            return false;
        }

        #endregion
    }

    #region Argumentos do Evento FrameFound

    /**
     * Esta classe eh usada para carregar os argumentos com seus valores para dentro do evento via delegate.
     * O delegate eh o intermediario que carrega os argumentos disponibilizados
     * atraves do evento. O evento disponibiliza os dados para o mundo externo (para quem quiser usar - qualquer tela por exemplo)
     * atraves de um delegate que contem a classe com os argumentos. Esta classe deve herdar as caracteristicas da classe EventArgs.
     **/
    public class FrameFoundEventArgs : EventArgs
    {
        private byte[] frame;
        private bool frameValid;

        public byte[] FrameFound
        {
            set
            {
                frame = value;
            }
            get
            {
                return frame;
            }
        }

        public bool FrameIsValid
        {
            set
            {
                frameValid = value;
            }
            get
            {
                return frameValid;
            }
        }

        public FrameFoundEventArgs()
        {
        }
    }

    public delegate void FrameFoundEventHandler(object sender, FrameFoundEventArgs e);

    #endregion
}

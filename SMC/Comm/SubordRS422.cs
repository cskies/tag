/**
 * @file 	    SubordRS422.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este formulario eh usado para testes da comunicacao serial RS422 com a UTMC.
 * @author 	    Thiago Duarte Pereira
 * @date	    27/06/2010
 * @note	    Modificado em 07/10/2011 por Fabricio.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using NetGscApi;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Comm;
using Inpe.Subord.Comav.Egse.Smc.Forms;

/**
 * @Namespace Namespace contendo as classes para tratamento dos diversos tipos de 
 * comunicacoes entre o SMC e o COMAV.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Comm
{
    /**
     * @class SubordRS422
     * Classe responsavel pela comunicacao direta com a serial RS422. 
     * Ela contem os metodos de configuracao, transmissao e recepcao, bem como toda a 
     * estrutura de interrupcao que eh usada para recepcao.
     **/
    public class SubordRS422
    {
        #region Atributos Locais

        private const int PIN_SOURCE_BASE_REG = 128; // criada para guardar o endereco do registrador correspondente. Este endereco nao esta sendo acessado via API NetGsc.
        private const int CONTROL_STATUS_BASE_REG = 28; // criada para guardar o endereco do registrador correspondente. Este endereco nao esta sendo acessado via API NetGsc.

        private int localBoard;
        private int localTxChannel;
        private int localTxTimeout;
        private int localRxChannel;
        private int localRxTimeout;
        private int localRxSizeBuffer;
        private byte[] txBuffer = null;
        private byte[] rxBuffer = null;
        private GscCallback cbRxFifoData, cbRxFifoFull; // Delegates used to register interrupt callbacks.        
        private GCHandle gchRxFifoData, gchRxFifoFull; // Delegate handles to bump reference counting to avoid gc.
        private DataReceiveEventArgs dataReceivedEventArgs = new DataReceiveEventArgs(); // Classe de argumentos usada para carregar os argumentos ao Event.
        private int bufferIndex = 0; // contador do numero de buffers recebidos da UTMC.

        #endregion

        #region Atributos Publicos

        public enum Mode { TTC_TO_UTMC = 0, TTC_TO_UPC = 1, TTC_to_TTC_Debug = 2 };

        // Event declarado para ser instanciado sempre que o metodo RxFifoData ser executado, ou seja, sempre que tiver dado na fifo.
        // Eh necessario instanciar e passar os argumentos ao DataReceiveEventArgs.
        public event DataReceiveEventHandler dataReceivedEventHandler = null;

        #endregion

        #region Propriedades

        public byte[] TxBuffer
        {
            set
            {
                txBuffer = value;
            }
            get
            {
                return txBuffer;
            }
        }

        public byte[] RxBuffer
        {
            set
            {
                rxBuffer = null;
            }
            get
            {
                return rxBuffer;
            }
        }

        #endregion

        #region Construtores

        public SubordRS422()
        {
        }

        #endregion

        #region Metodos Publicos

        /**
         * Este metodo configura a placa RS422 Sync com e sem Enable.
         * @attention: CUIDADO ao modificar este metodo. Ele eh extremamente complexo!!
         **/
        public int ConfigTxRxBoard(int board, int txChannel, int txTimeout, int rxChannel, int rxTimeout, int rxSizeBuffer, int bitRate, bool withEnable, Mode mode, bool clockContinuous)
        {
            try
            {
                localBoard = board;
                localTxChannel = txChannel;
                localTxTimeout = txTimeout;
                localRxChannel = rxChannel;
                localRxTimeout = rxTimeout;
                localRxSizeBuffer = rxSizeBuffer;
                rxBuffer = new byte[localRxSizeBuffer];

                int status = 0;
                int result = 0;

                GscWin32.GSC_SYNC_CONFIG syncConfig = new GscWin32.GSC_SYNC_CONFIG();

                // Abrir placa
                status = GscWin32.GscOpen(board, GscWin32.GSC_API_VERSION);

                if (status != 0)
                {
                    return status;
                }

                // Resetar canal Tx
                status = GscWin32.GscSio4ChannelReset(board, txChannel);

                if (status != 0)
                {
                    return status;
                }

                // Resetar canal Rx
                status = GscWin32.GscSio4ChannelReset(board, rxChannel);

                if (status != 0)
                {
                    return status;
                }

                // Get default values and set both channels:
                status = GscWin32.GscSio4SyncGetDefaults(ref syncConfig);

                if (status != 0)
                {
                    return status;
                }

                // Channel defaults
                syncConfig.bitRate = bitRate;
                syncConfig.encoding = (int)GscWin32.GSC_TOKENS.GSC_ENCODING_NRZ;
                syncConfig.protocol = (int)GscWin32.GSC_TOKENS.GSC_PROTOCOL_RS422_RS485;
                syncConfig.termination = (int)GscWin32.GSC_TOKENS.GSC_TERMINATION_ENABLED;

                // Transmitter defaults
                syncConfig.txStatus = (int)GscWin32.GSC_TOKENS.GSC_ENABLED;
                syncConfig.txCharacterLength = 8;
                syncConfig.txGapLength = 0;
                syncConfig.txClockSource = (int)GscWin32.GSC_TOKENS.GSC_CLOCK_INTERNAL;
                syncConfig.txClockEdge = (int)GscWin32.GSC_TOKENS.GSC_RISING_EDGE;
                syncConfig.txEnvPolarity = (int)GscWin32.GSC_TOKENS.GSC_LOW_TRUE;
                syncConfig.txIdleCondition = (int)GscWin32.GSC_TOKENS.GSC_IDLE_ALL_1;

                if (clockContinuous)
                {
                    syncConfig.txClockIdleCondition = (int)GscWin32.GSC_TOKENS.GSC_IDLE_ACTIVE; // Clock continuo em IDLE
                }
                else
                {
                    syncConfig.txClockIdleCondition = (int)GscWin32.GSC_TOKENS.GSC_IDLE_NOT_ACTIVE; // Clock nao continuo em IDLE
                }

                syncConfig.txMsbLsb = (int)GscWin32.GSC_TOKENS.GSC_MSB_FIRST;

                // Receiver defaults
                syncConfig.rxStatus = (int)GscWin32.GSC_TOKENS.GSC_ENABLED;
                syncConfig.rxClockSource = (int)GscWin32.GSC_TOKENS.GSC_CLOCK_EXTERNAL;
                syncConfig.rxClockEdge = (int)GscWin32.GSC_TOKENS.GSC_RISING_EDGE;
                syncConfig.rxEnvPolarity = (int)GscWin32.GSC_TOKENS.GSC_LOW_TRUE;
                syncConfig.rxMsbLsb = (int)GscWin32.GSC_TOKENS.GSC_MSB_FIRST;
                
                syncConfig.txDataPinMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_AUTO;
                syncConfig.rxDataPinMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_AUTO;
                syncConfig.txClockPinMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_AUTO;
                syncConfig.rxClockPinMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_AUTO;
                syncConfig.txEnvPinMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_AUTO;
                syncConfig.rxEnvPinMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_AUTO;
                syncConfig.loopbackMode = (int)GscWin32.GSC_TOKENS.GSC_LOOP_NONE;

                // Setup the Misc defaults
                syncConfig.packetFraming = (int)GscWin32.GSC_TOKENS.GSC_DISABLED;

                // Pin Configuration Variables
                switch (mode)
                {
                    case Mode.TTC_TO_UTMC: // TT&C - UTMC
                        {
                            syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DTE; // O modo de operacao eh configurado separado para Tx e Rx
                            break;
                        }
                    case Mode.TTC_TO_UPC: // TT&C - UPC
                        {
                            syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DCE; // O modo de operacao eh configurado separado para Tx e Rx
                            break;
                        }
                    case Mode.TTC_to_TTC_Debug: // TT&C - TT&C (modo debug para ser usado com o cabo reto!)
                        {
                            syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DCE; // O modo de operacao eh configurado separado para Tx e Rx
                            break;
                        }
                    default:
                        {
                            syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DTE; // O modo de operacao eh configurado separado para Tx e Rx
                            break;
                        }
                }

                status = GscWin32.GscSio4SyncSetConfig(board, txChannel, syncConfig);

                if (status != 0)
                {
                    return status;
                }

                status = GscWin32.GscSio4SyncSetConfig(board, txChannel, syncConfig);

                if (status != 0)
                {
                    return status;
                }

                // Verifica se os canais Tx/Rx sao diferentes
                // Se forem diferentes precisa configurar independente
                if (rxChannel != txChannel)
                {
                    switch (mode)
                    {
                        case Mode.TTC_TO_UTMC: // TT&C - UTMC
                            {
                                syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DTE; // O modo de operacao eh configurado separado para Tx e Rx
                                break;
                            }
                        case Mode.TTC_TO_UPC: // TT&C - UPC
                            {
                                syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DCE; // O modo de operacao eh configurado separado para Tx e Rx
                                break;
                            }
                        case Mode.TTC_to_TTC_Debug: // TT&C - TT&C (modo debug para ser usado com o cabo em loopback!)
                            {
                                syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DTE; // O modo de operacao eh configurado separado para Tx e Rx
                                break;
                            }
                        default:
                            syncConfig.interfaceMode = (int)GscWin32.GSC_TOKENS.GSC_PIN_DTE; // O modo de operacao eh configurado separado para Tx e Rx
                            break;
                    }

                    status = GscWin32.GscSio4SyncSetConfig(board, rxChannel, syncConfig);

                    if (status != 0)
                    {
                        return status;
                    }

                    status = GscWin32.GscSio4SyncSetConfig(board, rxChannel, syncConfig);

                    if (status != 0)
                    {
                        return status;
                    }

                    //-----------------------------------------------------------
                    //    TxE Polarity Workaround, RxE polarity is set correctly
                    //-----------------------------------------------------------      
                    // As configuracoes sao realizadas no registrador CHANNEL_PIN_SOURCE 0x0080 / 0x0084 / 0x0088 / 0x008C.
                    // O workaround configura os seguintes sinais:
                    // - Sinal de Enable do Rx como ativo baixo (bits D15:14);
                    // - Sinal de Enable do Tx como ativo baixo (bits D5:4);
                    // - Sinais de Dado e Enable sao alterados na borda de decida (bits D2:0);
                    // - Aquisicao de Dados de Rx eh realizada na borda de subida (bit D13).
                    // A DESCRICAO DE FUNCIOPNALIDADES DOS REGISTRADORES ESTAO DISPONIVEIS NO DOCUMENTO PC104P-SIO4B-SYNC

                    // WorkAround Rx:
                    status = GscWin32.GscLocalRegisterRead(board, PIN_SOURCE_BASE_REG + ((rxChannel - 1) * 4), ref result);

                    if (status != 0)
                    {
                        return status;
                    }

                    if (withEnable)
                    {
                        // Recepcao com Enable.
                        result |= 0x6011; // -- 0110 0000 0001 0001
                    }
                    else
                    {
                        // Recepcao sem Enable
                        result |= 0xE011; // -- 1110 0000 0001 0001
                    }

                    status = GscWin32.GscLocalRegisterWrite(board, PIN_SOURCE_BASE_REG + ((rxChannel - 1) * 4), result);

                    if (status != 0)
                    {
                        return status;
                    }

                    // WorkAround ResetRXFifo:
                    status = ResetRxFifo(board, rxChannel);

                    if (status != 0)
                    {
                        return status;
                    }
                }

                // WorkAround Tx:
                status = GscWin32.GscLocalRegisterRead(board, PIN_SOURCE_BASE_REG + ((txChannel - 1) * 4), ref result);

                if (status != 0)
                {
                    return status;
                }

                if (withEnable)
                {
                    // Recepcao com Enable.
                    result |= 0x6011; // -- 0110 0000 0001 0001
                }
                else
                {
                    // Recepcao sem Enable
                    result |= 0xE011; // -- 1110 0000 0001 0001
                }

                status = GscWin32.GscLocalRegisterWrite(board, PIN_SOURCE_BASE_REG + ((txChannel - 1) * 4), result);

                if (status != 0)
                {
                    return status;
                }

                // WorkArounds criados para resetar as FIFOs. 
                // Neste metodo, a fifo eh resetada a nivel de registrador, porque a funcao de reset da API nao funciona.
                status = ResetTxFifo(board, txChannel);

                if (status != 0)
                {
                    return status;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message,
                                "Connection Error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return -1;
            }

            return 0;
        }

        /**
         * Este metodo captura a mensagem de erro da placa conforme o erro de status retornado.
         **/
        public void PrintErrorMessage(int status)
        {
            try
            {
                byte[] message = new byte[100];
                GscWin32.GscGetErrorString(status, message);

                MessageBox.Show(Utils.Formatting.ConvertByteArrayToASCIISentence(message),
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                // nao imprimir nada porque se entrar neste esception, significa que 
                // algo ja foi impresso antes.
            }
        }

        /**
         * Este metodo eh um WorkAround gerado para resetar a RxFifo. A funcao da NetGscApi que se encarrega disto nao funciona.
         * Por isso, foi necessario ler, mascarar o valor e escrever no registrador correspondente para que a fifo fosse resetada.
        **/
        public int ResetRxFifo(int board, int rxChannel)
        {
            int status = 0;
            int result = 0;

            // ler o valor do registrador
            status = GscWin32.GscLocalRegisterRead(board, (CONTROL_STATUS_BASE_REG + ((rxChannel - 1) * 16)), ref result);

            if (status != 0)
            {
                return status;
            }

            // aplicar a mascara
            // result |= 0x03; // 0000 0000 0000 0000 0000 0011 => Reseta os dois canais
            result |= 0x02; // 0000 0000 0000 0000 0000 0010

            // escrever o valor no registrador
            status = GscWin32.GscLocalRegisterWrite(board, CONTROL_STATUS_BASE_REG + ((rxChannel - 1) * 16), result);

            if (status != 0)
            {
                return status;
            }

            return 0;
        }

        /**
         * Este metodo eh um WorkAround gerado para resetar a TxFifo. A funcao da NetGscApi que se encarrega disto nao funciona.
         * Por isso, foi necessario ler, mascarar o valor e escrever no registrador correspondente para que a fifo fosse resetada.
        **/
        public int ResetTxFifo(int board, int txChannel)
        {
            int status = 0;
            int result = 0;

            // le o valor do registrador
            status = GscWin32.GscLocalRegisterRead(board, (CONTROL_STATUS_BASE_REG + ((txChannel - 1) * 16)), ref result);

            if (status != 0)
            {
                return status;
            }

            // aplicar a mascara
            // result |= 0x03; // 0000 0000 0000 0000 0000 0011 => Reseta os dois canais
            result |= 0x01; // 0000 0000 0000 0000 0000 0001

            // escrever o valor no registrador
            status = GscWin32.GscLocalRegisterWrite(board, CONTROL_STATUS_BASE_REG + ((txChannel - 1) * 16), result);

            if (status != 0)
            {
                return status;
            }

            return 0;
        }

        /**
         * Usado para transmitir o buffer de bytes pela porta serial RS422 escolhida.
         **/
        public int TransmitMessage()
        {
            int status = 0;
            int id = 0;
            int txBytesTransferred = 0;
            //char[] txBufferAsChar = null;

            // como GscSio4ChannelTransmitDataAndWait recebe um array de char (e nao byte), uso outro array
            //Array.Resize<char>(ref txBufferAsChar, txBuffer.Length);
            //Array.Copy(txBuffer, txBufferAsChar, txBuffer.Length);

            //TODO [Fabricio]: estou testando isso no lugar das duas chamadas anteriores
            //status = GscWin32.GscSio4ChannelTransmitDataAndWait(localBoard, localTxChannel, txBufferAsChar, txBuffer.Length, localTxTimeout, ref txBytesTransferred);
            status = GscWin32.GscSio4ChannelTransmitData(localBoard, localTxChannel, txBuffer, txBuffer.Length, ref id);
            status = GscWin32.GscSio4ChannelWaitForTransfer(localBoard, localTxChannel, localTxTimeout, id, ref txBytesTransferred);

            return status;
        }

        /**
         * Faz o registro de duas interrupcoes que sao usadas para controlar a recepcao. Uma interrupcao ocorre sempre que tiver dado na RxFIFO e a outra sempre que a RxFIFO estiver cheia.
         * 
         * @todo O SMC esta com o seguinte BUG:
         * Durante a recepcao, se o usuario mover a interface grafica do SMC, como maximizar ou minimizar, a RxFifo enche e a recepcao de dados para.
         * Alguns testes foram feitos, como a verificacao do metodo ResetRxFifo e a verificacao das flags da interrupcao RX_FIFO_FULL.
         * Esperava-se que o problema estivesse no reset da fifo, mas nao eh este o motivo. E as flags das interrupcoes estao corretas.
         * A principio, acredita-se que a recepcao eh paralisada por causa do consumo de processamento que a interface grafica exige.
         * Isto deve obrigatoriamente ser verificado.
         * Porque sempre que a fifo esta FULL, a recepcao para? e porque a fifo fica FULL sempre que o usuario mexe a interface?
         * Acredito que o problema esteja na Thread de recepcao.
         **/
        public int StartReception()
        {
            bufferIndex = 0;

            int status = 0;

            // Adiciona o metodo que eh executado sempre que ocorrer a interrupcao de ter dado na fifo.
            cbRxFifoData = new GscCallback(RxFifoData);

            // Aloca o objeto GscCallback ao GCHandle para ser processado.
            // Este objeto (gchRxFifoData) eh o ultimo parametro que deve ser setado na funcao GscSio4ChannelRegisterInterrupt.
            gchRxFifoData = GCHandle.Alloc(cbRxFifoData);

            // registra interrupcao quando tiver dado na RxFIFO. Sempre que tiver dado na RxFIFO, le. 
            status = GscWin32.GscSio4ChannelRegisterInterrupt(localBoard, localRxChannel, (int)GscWin32.GSC_INTR_TOKENS.GSC_INTR_RX_FIFO_EMPTY, (int)GscWin32.GSC_TOKENS.GSC_FALLING_EDGE, cbRxFifoData);

            if (status != 0)
            {
                return status;
            }

            // Adiciona o metodo que eh executado sempre que ocorrer a interrupcao de fifo full.
            cbRxFifoFull = new GscCallback(RxFifoFull);

            // Aloca o objeto GscCallback ao GCHandle para ser processado.
            // Este objeto (gchRxFifoFull) eh o ultimo parametro que deve ser setado na funcao GscSio4ChannelRegisterInterrupt.
            gchRxFifoFull = GCHandle.Alloc(cbRxFifoFull);

            // registra interrupcao quando a RxFIFO estiver cheia. Sempre que estiver cheia, limpa.
            status = GscWin32.GscSio4ChannelRegisterInterrupt(localBoard, localRxChannel, (int)GscWin32.GSC_INTR_TOKENS.GSC_INTR_RX_FIFO_FULL, (int)GscWin32.GSC_TOKENS.GSC_RISING_EDGE, cbRxFifoFull);

            if (status != 0)
            {
                return status;
            }

            return 0;
        }

        /**
         * Para a recepcao. Desaloca as Threads das interrupcoes e utiliza o metodo StopInterrupt para desabilitar as interrupcoes.
         **/
        public int StopReception()
        {
            if (gchRxFifoData.IsAllocated)
            {
                gchRxFifoData.Free();
            }

            if (gchRxFifoFull.IsAllocated)
            {
                gchRxFifoFull.Free();
            }

            // Nao basta somente desalocar as Threads de interrupcao.
            // Tem que parar a interrupcao via registrador na placa.
            return StopInterrupt(localBoard);
        }

        /**
         * Desliga a placa RS422.
         **/
        public int CloseBoard(int board)
        {
            try
            {
                int status = 0;
                status = GscWin32.GscClose(board);

                if (status != 0)
                {
                    return status;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                               "Error to disconnect",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return -1;
            }

            return 0;
        }

        #endregion

        #region Metodos Privados

        /**
         * Este metodo eh executado sempre que a FIFO nao estiver vazia. Eh chamado dentro do metodo StartReception.
         **/
        private void RxFifoData(int board, int channel, int interrupt)
        {
            int rxId = 0;
            int status = 0;
            byte[] lastByte = new byte[1]; // workaround usado para corrigir outro bug da placa (RxTimeOut - nao pega o ultimo byte do buffer sempre que ocorre timeout)

            // bytesTransferred eh a quantidade de bytes recebidos com timeout, que certamente eh menor que o limite 
            // maximo do rxBuffer. O timeout ocorre se a fifo nao encher completamente dentro do tempo esperado.
            int bytesTransferred = 0;
            int numBytes = 0;

            //TODO [Fabricio]: estou testando isso no lugar das duas chamadas anteriores
            status = GscWin32.GscSio4ChannelReceiveDataAndWait(localBoard, localRxChannel, rxBuffer, rxBuffer.Length, localRxTimeout, ref bytesTransferred);

            //status = GscWin32.GscSio4ChannelReceiveData(localBoard, localRxChannel, rxBuffer, rxBuffer.Length, ref rxId);
            //status = GscWin32.GscSio4ChannelWaitForTransfer(localBoard, localRxChannel, localRxTimeout, rxId, ref bytesTransferred);

            if (status == 0) // Recepcao sem timeout e sem erro.
            {
                numBytes = rxBuffer.Length;
            }
            else if (bytesTransferred > 0) // Recepcao com timeout e sem erro.
            {
                // Nao mexer dentro deste else if!! Super complexo!! Este eh mais um workaround feito para corrigir outro bug da API da placa RS422.
                numBytes = bytesTransferred;
                status = GscWin32.GscSio4ChannelReceiveData(localBoard, localRxChannel, lastByte, 1, ref rxId);

                Thread.Sleep(100);
                rxBuffer[numBytes] = lastByte[0];
                numBytes++;
            }
            else if ((bytesTransferred == -1)) // recepcao concluida sem timeout, mas com erro.
            {
                numBytes = rxBuffer.Length;
            }
            else // Recepcao com timeout, sem dado na fifo e com erro. Este erro nao eh definido pelo manual da placa, mas existe.
            {
                numBytes = 0;
            }

            if (numBytes > 0)
            {
                bufferIndex++;

                // Passa os argumentos necessarios ao EventArg do evento DataReceivedEventHandler.
                dataReceivedEventArgs.RxBuffer = rxBuffer;
                dataReceivedEventArgs.NumBytes = numBytes;

                if (dataReceivedEventHandler == null)
                {
                    return;
                }

                // Instancia o EventHandler e passa o object sender e o EventArgs com os argumentos.
                dataReceivedEventHandler(this, dataReceivedEventArgs);
            }
            else
            {
                ResetRxFifo(localBoard, localRxChannel);
            }
        }

        /**
         * Este metodo eh executado sempre que ocorrer a interrupcao ALMOST_FIFO_FULL. Chama o metodo que reseta a FIFO.
         **/
        private void RxFifoFull(int board, int channel, int interrupt)
        {
            // Resetar a Fifo do canal escolhido.
            ResetRxFifo(board, channel);
        }

        /**
         * Este metodo desabilita todas as funcoes de interrupcao de todos os canais da placa.
         * Cada bit do registrador INTERRUPT_CONTROL (de 32 bits no total) eh usado para um tipo de interrupcao.
         * Verificar no manual PC104P-SIO4B-SYNC para maiores informações.
         * Este metodo foi criado para interromper a recepcao de dados que nao eh interrompida somente com a finalizacao das Threads GCHandler.
         **/
        private int StopInterrupt(int board)
        {
            int status = 0;
            int result = 0;
            const int INTERRUPT_CONTROL = 96; // este valor esta no manual da placa. Esta sendo usado desta forma porque a flag da API nao funciona.

            status = GscWin32.GscLocalRegisterRead(board, INTERRUPT_CONTROL, ref result);

            if (status != 0)
            {
                return status;
            }

            result = 0x00000000;

            status = GscWin32.GscLocalRegisterWrite(board, INTERRUPT_CONTROL, result);

            if (status != 0)
            {
                return status;
            }

            return 0;
        }

        #endregion
    }

    #region Argumentos do Evento de recepcao

    /**
     * Esta classe eh usada para carregar os argumentos com seus valores para dentro do evento via delegate.
     * O delegate eh o intermediario que carrega os argumentos disponibilizados
     * atraves do evento. O evento disponibiliza os dados para o mundo externo (para quem quiser usar - qualquer tela por exemplo)
     * atraves de um delegate que contem a classe com os argumentos. Esta classe deve herdar as caracteristicas da classe EventArgs.
     **/
    public class DataReceiveEventArgs : EventArgs
    {
        private byte[] rxBuffer;
        private int numBytes;

        public byte[] RxBuffer
        {
            set
            {
                rxBuffer = value;
            }
            get
            {
                return rxBuffer;
            }
        }

        public int NumBytes
        {
            set
            {
                numBytes = value;
            }
            get
            {
                return numBytes;
            }
        }

        public DataReceiveEventArgs()
        {
        }
    }

    /**
     * Este delegate deve ser adicionado ao evento que esta declarado no corpo das
     * variaveis globais da classe SubordRS422. Para ser iniciado, uma ASSINATURA eh feita
     * ao evento. O delegate deve receber um ou um conjunto de argumentos, que neste 
     * caso sao os argumentos da classe DataReceiveEventArgs. Veja como esta sendo 
     * feito. A assinatura do evento deve ser feita por quem utilizara os argumentos, ou seja,
     * a Thread principal da interface grafica da(s) tela(s) que utilizam os dados. 
     **/
    public delegate void DataReceiveEventHandler(object sender, DataReceiveEventArgs e);

    #endregion
}

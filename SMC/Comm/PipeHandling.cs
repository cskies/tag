/**
 * @file 	    PipeHandling.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabrício de Novaes Kucinskis
 * @date	    22/07/2009
 * @note	    Modificado em 03/05/2013 por Thiago.
 **/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using System.IO;
using System.Windows.Forms;

/**
 * @Namespace Namespace contendo as classes para tratamento dos diversos tipos de 
 * comunicacoes entre o SMC e o COMAV.
 */ 
namespace Inpe.Subord.Comav.Egse.Smc.Comm
{
    /**
     * @class PipeHandling
     * Classe para o gerenciamento de conexoes via named pipe, usadas para 
     * a comunicacao com o FSW no SIS, dentro de uma maquina virtual VMWare.
     * Baseado no codigo disponivel em http://blog.paranoidferret.com/?p=33
     * @attention Nao modificar os parametros utilizados para criacao e conexao
     * do pipe. A conexao deve ser assincrona e permitir sobrescrita 
     * (overlapped I/O), e o tamanho do buffer deve ser de 1 byte, pois eh assim
     * que o VMWare trabalha com a conexao.
     **/
    class PipeHandling
    {
        #region Constantes e declaracoes das APIS do Windows utilizadas

        private const uint PIPE_ACCESS_DUPLEX = 0x3;
        private const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        private const uint PIPE_WAIT = 0x0;
        private const uint PIPE_TYPE_BYTE = 0x0;
        private const uint DUPLEX = 0x00000003;
        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        private const uint GENERIC_READ = (0x80000000);
        private const uint GENERIC_WRITE = (0x40000000);
        private const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateNamedPipe(
           String pipeName,
           uint dwOpenMode,
           uint dwPipeMode,
           uint nMaxInstances,
           uint nOutBufferSize,
           uint nInBufferSize,
           uint nDefaultTimeOut,
           IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ConnectNamedPipe(
           SafeFileHandle hNamedPipe,
           IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int DisconnectNamedPipe(SafeFileHandle hNamedPipe);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
           String pipeName,
           uint dwDesiredAccess,
           uint dwShareMode,
           IntPtr lpSecurityAttributes,
           uint dwCreationDisposition,
           uint dwFlagsAndAttributes,
           IntPtr hTemplate);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(SafeFileHandle hNamedPipe);

        #endregion

        // Cria o evento disparado pela leitura de bytes do buffer
        public delegate void ByteReceivedHandler(byte[] ReceivedByte); // delegate eh basicamente um "type-safe function pointer"
        public event ByteReceivedHandler ByteReceived;

        #region Atributos Internos

        private string _pipeName;

        private SafeFileHandle pipeHandle;
        private byte[] oneByteBuffer = new byte[1];

        private bool isServer = false;

        FileStream stream = null;
        Thread readThread = null;

        #endregion

        public PipeHandling()
        {
        }

        #region Metodos Publicos

        /**
         * Cria o pipe e aguarda conexao de um cliente. Nesse caso, esta aplicacao eh o pipe server.
         * @attention Nao retorna ate que a conexao seja estabelecida.
         **/
        public void CreatePipeAndConnect(string pipeName)
        {
            int connected;
            uint pipeMode = PIPE_WAIT | PIPE_TYPE_BYTE;
            uint openMode = PIPE_ACCESS_DUPLEX | FILE_FLAG_WRITE_THROUGH | FILE_FLAG_OVERLAPPED;

            _pipeName = pipeName;

            // Cria o named pipe e o vincula a um filestream
            // Obs: o VMWare nao aceita pipes do tipo PIPE_TYPE_MESSAGE
            pipeHandle = CreateNamedPipe(_pipeName, openMode, pipeMode, 1, 4096, 4096, 0, IntPtr.Zero);
            if (pipeHandle.IsInvalid) return; // nao conseguiu criar o pipe

            // Cria o filestream com um buffer de 2048 bytes para nao haver sobreposicao 
            // de bytes durante a comunicacao (a leitura / escrita eh de 1 byte por vez)
            stream = new FileStream(pipeHandle, FileAccess.ReadWrite, 2048, true);

            // TODO: nao retorna enquanto o client nao se conectar. Tentar melhorar isso.
            connected = ConnectNamedPipe(pipeHandle, IntPtr.Zero);

            if (connected == 0) return; // nao conseguiu conectar ao cliente

            isServer = true;

            // Cria uma thread para a leitura do pipe
            readThread = new Thread(new ThreadStart(Read));
            readThread.Start();
        }

        /** Conecta a um pipe existente (nesse caso, esta aplicacao eh o pipe client). **/
        public void ConnectToPipe(string pipeName)
        {
            // Conecta ao pipe e o vincula a um FileStream
            pipeHandle = CreateFile(pipeName,
                                      GENERIC_READ | GENERIC_WRITE,
                                      0,
                                      IntPtr.Zero,
                                      OPEN_EXISTING,
                                      FILE_FLAG_OVERLAPPED,
                                      IntPtr.Zero);

            // Erro ao criar o handle - o servidor provavelmente nao esta rodando
            if (pipeHandle.IsInvalid)
            {
                return;
            }

            // Cria o filestream com um buffer de 2048 bytes para nao haver sobreposicao 
            // de bytes durante a comunicacao (a leitura / escrita eh de 1 byte por vez)
            stream = new FileStream(pipeHandle, FileAccess.ReadWrite, 2048, true);

            isServer = false;

            // Cria uma thread para a leitura do pipe
            readThread = new Thread(new ThreadStart(Read));
            readThread.Start();
        }

        /**
         * Desconecta o named pipe. Se este for o server, fecha o 
         * handle (destroi o pipe).
         **/
        public void Disconnect()
        {
            // ocorrera um erro caso a serial da VM nao esteja conectada
            try
            {
                if (isServer)
                {
                    DisconnectNamedPipe(pipeHandle);
                    CloseHandle(pipeHandle);
                }
                else
                {

                        stream.Flush();
                        stream.Close();
                        stream.Dispose();
                }
                
                readThread.Abort();
            }
            catch
            {
                // podemos ignorar o erro sem problemas
            }
        }

        /**
         * Metodo a ser executado em uma thread separada, para a leitura
         * de bytes recebidos pelo pipe.
         **/
        private void Read()
        {
            int bytesRead = 0;

            while (true)
            {
                try
                {
                    bytesRead = stream.Read(oneByteBuffer, 0, 1);
                }
                catch
                {
                    // Ocorreu algum erro de leitura
                    break;
                }

                // Ocorreu algum erro de leitura
                if (bytesRead == 0) break;

                // Dispara um evento de byte recebido
                if (ByteReceived != null) // acho que fica travado ate o evento ser tratado, verificar
                {
                    ByteReceived(oneByteBuffer);
                }
            }
        }

        /**
         * Converte uma string em bytes e escreve no pipe, byte a byte.
         **/
        public void Write(byte[] messageBuffer)
        {
            try
            {
                ASCIIEncoding encoder = new ASCIIEncoding();

                for (int i = 0; i < messageBuffer.Length; i++)
                {
                    oneByteBuffer[0] = messageBuffer[i];
                    stream.Write(oneByteBuffer, 0, 1);

                    // Eh necessario um delay de pelo menos 20ms entre cada byte enviado;
                    // sem este delay, o sw comav trava. Nao consegui detectar o motivo,
                    // mas provavelmente eh alguma falha do SIS. Monitorei e nao detectei
                    // nenhum problema na escrita/leitura do named pipe pelo VMWare.
                    Thread.Sleep(20);
                }

                stream.Flush();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to write to the named pipe. Please check if the named pipe connection is properly configured.\n(i.e. if the server is running, the emulated serial port connected, etc).",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}

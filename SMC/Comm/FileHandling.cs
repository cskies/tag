/**
 * @file 	    FileHandling.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    21/07/2009
 * @note	    Modificado em 10/10/2011 por Fabricio.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Forms;
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.TestSession;
using System.Data.OleDb;
using Inpe.Subord.Comav.Egse.Smc.Ccsds;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using System.Data;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Namespace contendo as classes para tratamento dos diversos tipos de 
 * comunicacoes entre o SMC e o COMAV.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Comm
{
    /**
     * @clas FileHandling
     * Classe que trata o envio de TCs para TCFiles e leitura de TM de TMFiles.
     **/
    public class FileHandling
    {
        #region Atributos Internos

        private SessionLog sessionLog = new SessionLog();
        private RawPacket reportPacket = new RawPacket(false, false);
        private StreamReader readFile = null;
        private StreamWriter writeFile = null;
        private String filePath = "";
        private int sessionId = 0;
        private bool isTC = false;
        private bool addSessionId = false;
        private bool isFirstPacket = false;
        private bool sessionIdFound = false;
        private bool sessionIdIsValid = false;
        private bool saveLogSession = false;
        private bool packetError = false;
        private bool fileIsOpen = false;
        private bool readFileIsOpen = false;
        private bool writeFileIsOpen = false;
        private bool allpacketSended = false;
        private int numLinesWithData = 0;

        #endregion

        #region Construtor

        public FileHandling(String path, bool isRequest, bool saveSession)
        {
            filePath = path;
            isTC = isRequest;
            saveLogSession = saveSession;
        }

        #endregion

        #region Propriedades

        public bool SessionIdIsValid
        {
            get
            {
                return sessionIdIsValid;
            }

            set
            {
                sessionIdIsValid = value;
            }
        }

        public int SessionId
        {
            get
            {
                return sessionId;
            }
        }

        public bool AddTagSessionIdOnFile
        {
            get
            {
                return addSessionId;
            }

            set
            {
                addSessionId = value;
            }
        }

        public bool SessionIdFound
        {
            get
            {
                return sessionIdFound;
            }

            set
            {
                sessionIdFound = value;
            }
        }

        public bool IsFirstPacket
        {
            get
            {
                return isFirstPacket;
            }

            set
            {
                isFirstPacket = value;
            }
        }

        public bool PacketError
        {
            get
            {
                return packetError;
            }

            set
            {
                packetError = value;
            }
        }

        public bool FileIsOpen
        {
            get
            {
                return fileIsOpen;
            }

            set
            {
                fileIsOpen = value;
            }
        }

        public bool ReadFileIsOpen
        {
            get
            {
                return readFileIsOpen;
            }
        }

        public bool WriteFileIsOpen
        {
            get
            {
                return writeFileIsOpen;
            }
        }

        public bool AllPacketSended
        {
            get
            {
                return allpacketSended;
            }

            set
            {
                allpacketSended = value;
            }
        }

        public int NumLinesWithData
        {
            get
            {
                return numLinesWithData;
            }
            set
            {
                numLinesWithData = value;
            }
        }

        #endregion

        #region Metodos de Tratamento de dados com os Files de TC e de TM

        /**Verifica se existe o arquivo desejado*/
        public bool FileExists()
        {
            bool existTcFile = File.Exists(filePath);

            if (!existTcFile)
            {
                return false;
            }

            return true;
        }

        public bool OpenReaderFile()
        {
            if (!readFileIsOpen)
            {
                readFile = File.OpenText(filePath);
                readFileIsOpen = true;
                allpacketSended = false;
                packetError = false;
                
                // Obtem o numero de linhas do arquivo de leitura
                numLinesWithData = NumberOfLinesWithData();
            }

            return true;
        }

        public bool CloseReaderFile()
        {
            if (readFileIsOpen)
            {
                readFile.Close();
                readFile.Dispose();
                readFileIsOpen = false;
            }

            return true;
        }

        public bool OpenWriterFile()
        {
            if (!writeFileIsOpen)
            {
                writeFile = new StreamWriter(filePath, true);
                writeFileIsOpen = true;
            }

            return true;
        }

        public bool CloseWriterFile()
        {
            if (writeFileIsOpen)
            {
                writeFile.Close();
                writeFile.Dispose();
                writeFileIsOpen = false;
            }

            return true;
        }

        /**
         * Abre o File se existir uma SessionId "VALIDA", ou se nao existir uma SessionId.
         * @attention: Podera ser readaptado para ser usado tanto para TC quanto TM
         **/
        public bool ValidateFile()
        {
            try
            {
                sessionIdFound = false;
                sessionIdIsValid = false;
                isFirstPacket = true;

                StreamReader readFile = File.OpenText(filePath);
                String line = readFile.ReadLine();
                String session = "";
                sessionId = 0;

                while (line != null)
                {
                    line = line.Trim();

                    if (line.Length > 0)
                    {
                        if (!line.Substring(0, 2).Equals("//"))
                        {
                            isFirstPacket = false;
                        }

                        if (line.ToUpper().Replace(" ", "").Contains("//SESSIONID"))
                        {
                            int sessionIdStartPosition = line.IndexOf(":") + 1;
                            session = line.Substring(sessionIdStartPosition, (line.Length - sessionIdStartPosition));
                            session = session.Trim();
                            sessionIdFound = true;

                            if (int.TryParse(session, out sessionId))
                            {
                                sessionIdIsValid = true;

                                if (!sessionLog.OpenFileSession(sessionId))
                                {
                                    if (isTC)
                                    {
                                        MessageBox.Show("The Session Id informed in the TC file does not exist!",
                                                                                        "TC file open aborted!",
                                                                                        MessageBoxButtons.OK,
                                                                                        MessageBoxIcon.Exclamation);
                                    }

                                    sessionIdIsValid = false;
                                    sessionLog.SessionOpenned = false;
                                    sessionId = 0;
                                }
                                else
                                {
                                    sessionLog.SessionOpenned = true;
                                }
                            }
                            else
                            {
                                if (isTC)
                                {
                                    MessageBox.Show("The Session Id informed in the TC file is invalid!",
                                                    "TC file open aborted!",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation);
                                }

                                sessionIdIsValid = false;
                            }
                        }
                    }

                    line = readFile.ReadLine();
                }

                readFile.Close();

                if (!sessionIdFound)
                {
                    return true; // false se nao encontrar a tag
                }
                else
                {
                    if (sessionIdIsValid)
                    {
                        return true; // true se encontrar a tag com uma session valida
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message,
                                "Error on File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            return false;
        }

        /**
         * Retorna o numero de linhas do arquivo.
         **/
        public int NumberOfLinesWithData()
        {
            int numLines = 0;
            StreamReader readFile = File.OpenText(filePath);
            object line = readFile.ReadLine();

            if (line != null)
            {
                numLines++;
            }

            while (line != null)
            {
                line = readFile.ReadLine();

                if (line != null)
                {
                    numLines++;
                }
            }

            return numLines;
        }

        /**
         * Importa os Packets de TM presentes no file de TM. Sao importadas 
         * apenas TM validas, com excecao apenas de Crc invalido.
         */
        public bool TelemetryImport()
        {
            try
            {
                StreamReader readFile = File.OpenText(filePath);
                String line = readFile.ReadLine();
                bool packetInsert = false;

                while (line != null)
                {
                    byte[] packet = ConvertStringPacketToByteArrayPacket(line, true);

                    if (packet != null)
                    {
                        // Se entrar aqui, significa que temos um packet valido. Independente do Crc estar correto ou incorreto
                        // o packet eh passado para a classe RawPacket para ser tratado.
                        reportPacket.RawContents = packet;

                        // Verifica o Crc
                        UInt16 crc = CheckingCodes.CrcCcitt16(ref packet, packet.Length - 2);

                        if ((packet[packet.Length - 2] == (byte)(crc >> 8)) &&
                            (packet[packet.Length - 1] == (byte)(crc & 0xFF)))
                        {
                            // Pacote sem erros
                            sessionLog.LogPacket(reportPacket, false, false, "file");
                        }
                        else
                        {
                            // Pacote com erros
                            sessionLog.LogPacket(reportPacket, false, true, "file");
                        }

                        packetInsert = true; // Indica que existe um pacote valido no arquivo

                        line = readFile.ReadLine();
                    }
                }

                readFile.Close();

                if (packetInsert)
                {
                    // Caso o arquivo nao contenha a tag // session, uma nova session deve ser criada.
                    if (sessionId == 0)
                    {
                        // Uma nova session foi criada
                        String msg = "Packets successfully imported to new session " + sessionLog.SessionId + "!";
                        MessageBoxIcon icon = MessageBoxIcon.Information;

                        if (packetError)
                        {
                            msg += "\n\nHowever, there were invalid lines, which were ignored.";
                            icon = MessageBoxIcon.Warning;
                        }

                        MessageBox.Show(msg,
                                        "TM file import aborted!",
                                        MessageBoxButtons.OK,
                                        icon);
                    }
                    else
                    {
                        // Informa a session em que as TMs foram importadas
                        String msg = "Packets successfully imported to session " + sessionLog.SessionId + "!";
                        MessageBoxIcon icon = MessageBoxIcon.Information;

                        if (packetError)
                        {
                            msg += "\n\nHowever, there were invalid lines, which were ignored.";
                            icon = MessageBoxIcon.Warning;
                        }

                        MessageBox.Show(msg,
                                        "TM file import aborted!",
                                        MessageBoxButtons.OK,
                                        icon);
                    }
                }
                else
                {
                    MessageBox.Show("There is no valid packet in this file.",
                                    "TM file to import",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    sessionLog.CloseSession();

                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message,
                                "Error on File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                sessionLog.CloseSession();

                return false;
            }

            sessionLog.CloseSession();

            return true;
        }

        /**
         * Le e retorna linha a linha (uma de cada vez) em formato object.
         **/
        public object ReadPacketLine()
        {
            object line = null;
            int comment = 0;

            while (comment != -1)
            {
                line = readFile.ReadLine();

                if (line == null) // fim de arquivo
                {
                    allpacketSended = true;
                    return null;
                }

                if (line.ToString().Trim().Equals(""))
                {
                    comment = 0;
                }
                else
                {
                    comment = line.ToString().Trim().IndexOf("//");
                }
            }

            return line;
        }

        /**
         * Converte o pacote de formato string para um array de bytes.
         * Verifica se pacote eh TC ou TM valido(a), em funcao do tamanho 
         * de cada um. e se podem ser convertidos em array de bytes.
         **/
        public byte[] ConvertStringPacketToByteArrayPacket(String line, bool isTC)
        {
            // Tenho que verificar se a soma dos caracteres eh par.
            String tmpPacket = "";

            int comment = line.IndexOf("//");

            if (comment == -1)
            {
                comment = line.Length;
            }

            int numCarac = 0;

            // Percorre a linha lida verificando os caracteres
            for (int i = 0; i < comment; i++)
            {
                String caracter = line.Substring(i, 1);

                if (caracter.Equals(" ") || caracter.Equals("\t"))
                {
                    break;
                }

                tmpPacket = tmpPacket + caracter;
                numCarac = numCarac + 1;
            }

            // Este if verifica se a linha lida esta vazia
            if (tmpPacket.Length == 0)
            {
                return null;
            }

            int isEven = numCarac % 2;

            // Verifica se o numero de caracteres do packet eh par, pois um array hexadecimal deve ser par
            if (isEven != 0)
            {
                packetError = true;
                return null;
            }

            // Verificar se todos os caracteres sao hexadecimais, caso o numero de caracteres seja par.                        
            byte[] packet = Utils.Formatting.HexStringToByteArray(tmpPacket); //retorna null, caso nao sejam convertidos para hexadecimal

            if (packet == null)
            {
                packetError = true;
                return null;
            }

            if (isTC)
            {
                return packet;
            }
            else
            {
                // Se o tamanho da TM packet for menor que 18, desconsidera-o, porque eh um pacote de TM invalido.
                if (packet.Length < 18)
                {
                    packetError = true;
                    return null;
                }
                else if (((packet[0] >> 4) & 0x0F) == 1) // TC = 1, TM = 0. Caso seja um TC, nao acita-o.
                {
                    packetError = true;
                    return null;
                }
            }

            return packet;
        }

        /** Cria um novo arquivo no disco **/
        public bool CreateNewFile()
        {
            try
            {
                // OBS: sao criados apenas tcFiles
                if (!isTC)
                {
                    return false;
                }

                if (MessageBox.Show("Do you want to create the file: " + filePath + " ?",
                                    "Create new TC file",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }

                FileStream currentFile = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                currentFile.Close(); // tenho que fechar pra usar o TextWriter

                isFirstPacket = true; // ao criar o file, ja indico que o proximo packet sera o primeiro.
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message,
                                "Error on File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        /** Abre o arquivo indicado no path e escreve a linha desejada. **/
        public bool Write(String line)
        {
            try
            {
                // abre o arquivo
                StreamWriter writeFile = new StreamWriter(filePath, true);

                // escreve a linha e fecha o arquivo
                writeFile.WriteLine(line);
                writeFile.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message,
                                "Error on File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        /** 
         * Abre o arquivo indicado no objeto writeFile e escreve a linha desejada.
         * Devera ser usado sempre que o arquivo estiver aberto, pois o mesmo nao abre
         * o arquivo. Ao termino, o arquivo devera ser fechado.
         **/
        public bool Append(String line)
        {
            try
            {
                writeFile.Write(line);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /**
         * Este metodo estatico exporta os dados do banco de dados
         * utilizados para os forms: Mission IDs: Application IDs,
         * Report IDs, Packet Store IDs, Memory IDs e TC Failure Codes
         **/
        public static void ExportMissionIds(String title, String descriptionField, String tableName, String keyField)
        {
            try
            {
                if (File.Exists(Properties.Settings.Default.flight_sw_file_path + "\\" + tableName + ".h"))
                {
                    if (MessageBox.Show("Header file already exist! Do you want to overwrite it?",
                                        "Overwrite Header File?",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                TextWriter file = new StreamWriter(Properties.Settings.Default.flight_sw_file_path + "\\" + tableName + ".h");
                file.WriteLine("/**");
                file.WriteLine(" * @file\t" + tableName + ".h");
                file.WriteLine(" * @note\tCopyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo");
                file.WriteLine(" * @author\tSUBORD / SMC");
                file.WriteLine(" * @date\t" + DateTime.Now.ToString());
                file.WriteLine(" * @brief\tEnumeracao de " + title + " do PUS.");
                file.WriteLine(" * @note\tModificado em [data aqui] por [seu nome aqui].");
                file.WriteLine(" **/");
                file.WriteLine();
                file.WriteLine("#ifndef " + tableName.ToUpper() + "_H_");
                file.WriteLine("#define " + tableName.ToUpper() + "_H_");
                file.WriteLine();
                file.WriteLine("/**");
                file.WriteLine(" * @enum " + tableName);
                file.WriteLine(" * [descricao da enumeracao aqui]");
                file.WriteLine(" **/");

                DbSimpleTable simpleTable = new DbSimpleTable(tableName, keyField, descriptionField);
                DataTable simpletbl = simpleTable.GetTable();

                file.WriteLine("typedef enum " + tableName);
                file.WriteLine("{");

                for (int i = 0; i < simpletbl.Rows.Count; i++)
                {
                    String variable = simpletbl.Rows[i][1].ToString().Replace(" ", "_");
                    String atribute = simpletbl.Rows[i][0].ToString();

                    if ((i + 1) != simpletbl.Rows.Count) // se nao for a ultima linha da tabela
                    {
                        file.WriteLine("    " + variable + " = " + atribute + ",");
                    }
                    else // se for a ultima linha da tabela, nao coloca virgula
                    {
                        file.WriteLine("    " + variable + " = " + atribute);
                    }
                }

                file.WriteLine("};");
                file.WriteLine();
                file.WriteLine("#endif /* " + tableName.ToUpper() + "_H_ */");
                file.Close();
                file.Dispose();

                MessageBox.Show("Header file was exported successfully!",
                                "Header File Created",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error when creating the file: \n\n" + ex.Message,
                                "File Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
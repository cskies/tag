/**
 * @file 	    SessionLog.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabrício de Novaes Kucinskis
 * @date	    22/07/2009
 * @note	    Modificado em 24/10/2013 por Bruna.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Data;

/**
 * @Namespace Contem todos os recursos para o controle das Sessoes de Teste.
 */
namespace Inpe.Subord.Comav.Egse.Smc.TestSession
{
    /**
     * @class SessionLog
     * Classe para o registro de logs de sessoes de testes.
     */
    class SessionLog
    {
        #region Atributos Internos

        private int sessionId = 0;
        private bool sessionOpenned = false;
        private OleDbConnection conn = null;
        private OleDbCommand cmd = null;
        private OleDbTransaction transaction = null;

        /* Esta variavel eh usada como um semaforo na chamada do metodo LogPacket
         * Quando o registro de pacotes eh muito rapido, ocorre disputa entre essas chamadas.
         * Dependendo da velocidade, o metodo nao eh executado completamente durante sucessivas chamadas.
         * O correto eh esperar que o metodo seja executado totalmete, ou seja, pacote por pacote.
         * Este parametro eh static porque esta regra se aplica a qualquer instancia desta classe.
         * Cada pacote de uma unica vez, e pronto.
         * No inicio do metodo LogPacket, esse parametro eh verificado, se for true, espera ate que seja false
         * recordingPacket = true (metodo LogPacket esta sendo executado)
         * recordingPacket = false (metodo LogPacket ja executou por completo e esta apto a inserir outro pacote)
         */
        private static bool recordingPacket = false;

        #endregion

        #region Atributos Externos

        public bool swVersionCancel = false;

        #endregion

        #region Construtor

        public SessionLog()
        {
        }

        #endregion

        #region Propriedades

        public int SessionId
        {
            get
            {
                return sessionId;
            }

            set
            {
                sessionId = value;
            }
        }

        public bool SessionOpenned
        {
            get
            {
                return sessionOpenned;
            }

            set
            {
                sessionOpenned = value;
            }
        }

        #endregion

        #region Metodos Privados

        /** Cria uma nova sessao no banco de dados. **/
        private void OpenSession(string connectionType)
        {
            //carrega os dados presentes na dbconfiguration para inserir a versao do sw na sessao iniciada
            DbConfiguration.Load();

            string sql = "select isnull(max(session_id), 0) + 1 from sessions";

            sessionId = (int)DbInterface.ExecuteScalar(sql);

            if (swVersionCancel == false)
            {
                sql = @"insert into sessions (session_id, start_time, end_time, connection_type, swapl_version, swapl_release, swapl_patch) 
            values (" + sessionId.ToString() + ", getdate(), getdate(), '" + connectionType + "', " + DbConfiguration.FlightSwVersionMajor + ", " + DbConfiguration.FlightSwVersionMinor +
                          ", " + DbConfiguration.FlightSwVersionPatch + ")";
            }
            else
            {
                sql = @"insert into sessions (session_id, start_time, end_time, connection_type, swapl_version, swapl_release, swapl_patch) 
            values (" + sessionId.ToString() + ", getdate(), getdate(), '" + connectionType + "', 0, 0, 0)";
            }

            if (DbInterface.ExecuteNonQuery(sql))
            {
                sessionOpenned = true;
            }
            else
            {
                sessionId = 0;
            }
        }

        #endregion

        #region Metodos Publicos

        /** Encerra a gravacao de dados na sessao aberta. **/
        public void CloseSession()
        {
            sessionOpenned = false;
            sessionId = 0;
        }

        /** 
         * Registra um pacote transmitido na base. Se for o primeiro pacote, cria uma nova sessao. 
         * 
         * @attention Este metodo chama diretamente o OledDbConnection (ao inves de usar o DbInterface),
         * porque precisa trabalhar com tipos complexos como DateTime e VarBinary, alem de fazer uso
         * constante de DataReaders, nao providos pelo DbInterface.
         **/
        public void LogPacket(RawPacket rawPacket, bool isRequest, bool crcError, string connectionType)
        {
            // A descricao deste while esta na descricao do parametro recordingPacket na area das variaveis globais.
            while (recordingPacket) { } // Aguarde ate que o pacote seja registrado.

            recordingPacket = true;

            // Abre uma sessao, se isso ainda nao tiver ocorrido
            if (!sessionOpenned)
            {
                OpenSession(connectionType);
            }

            // Atualiza o end_time da sessao
            try
            {
                int uniqueID = 0;
                int apid;
                int ssc;
                int serviceType;
                int serviceSubtype;
                int differentSubtype = 0;
                string request = "0";
                string crc = "0";
                DateTime serverDate = new DateTime();
                bool allowRepetition = false;
                bool hasEmbeddedTCs = false;
                bool isReportPacket = false;
                bool isTelemetryPacket = false; // indica se eh um pacote com parametros de hkeep/diagnose
                bool isNackPacket = false;
                bool isNewReportDefinitionPacket = false;
                int nParameter = 1;
                int numberOfFields = 0;
                byte[] packetInBytes = rawPacket.RawContents;
                int tcFailureCode = -1;
                int reportId = -1;
                int structureId = -1;
                int numberOfParameters = 0;
                int timeTagBitsDiff = 0; // para subtrair o nr de bits da timetag, a depender do formato

                // Instanciar os objetos de Conexao e Iniciar a Transacao
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                string sql = "";

                sql = "select getdate()";
                cmd.CommandText = sql;
                serverDate = (DateTime)cmd.ExecuteScalar();

                sql = "select isnull(max(unique_log_id), 0) + 1 from packets_log";
                cmd.CommandText = sql;
                uniqueID = (int)cmd.ExecuteScalar();

                sql = "update sessions set end_time = ? where session_id = " + sessionId.ToString();

                cmd.CommandText = sql;
                cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.Date));
                cmd.Parameters["@P1"].Value = serverDate;
                cmd.ExecuteNonQuery();

                // Agora registra o pacote
                apid = (int)rawPacket.GetPart(5, 11);

                ssc = (int)((packetInBytes[2] & 0x3f) << 8 | packetInBytes[3]);

                if (rawPacket.IsDeviceCommand == true)
                {
                    // TODO: fixo por enquanto para device commands, ver se consigo melhorar isso depois
                    serviceType = 2;
                    serviceSubtype = 3;
                }
                else
                {
                    serviceType = (int)packetInBytes[7];
                    serviceSubtype = (int)packetInBytes[8];
                }

                // Determina se ha o parametro N neste pacote
                sql = @"select top 1 isnull(a.same_as_subtype, 0) as same_as_subtype, isnull(b.allow_repetition, 0) as allow_repetition
                        from subtype_structure a right join subtypes b on a.service_type = b.service_type and a.service_subtype = b.service_subtype 
                        where b.service_type = " + serviceType.ToString() + " and b.service_subtype = " + serviceSubtype.ToString() +
                        " order by a.position";

                cmd.CommandText = sql;
                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    differentSubtype = dr.GetInt32(dr.GetOrdinal("same_as_subtype"));
                    allowRepetition = dr.GetBoolean(dr.GetOrdinal("allow_repetition"));
                }

                dr.Close();

                if (allowRepetition)
                {
                    if (isRequest)
                    {
                        if (rawPacket.IsDeviceCommand == true)
                        {
                            // para Device Commands, extraio o campo "n" do tamanho do pacote
                            nParameter = (((int)packetInBytes[5]) - 1) / 2;
                        }
                        else
                        {
                            nParameter = (int)packetInBytes[10];
                        }
                    }
                    else
                    {
                        nParameter = (int)packetInBytes[16];
                    }
                }

                if (isRequest)
                {
                    request = "1";
                }

                if (crcError)
                {
                    crc = "1";
                }

                // Uso oledbparameters aqui para a insercao dos dados binarios (time-tag e raw_packet)
                sql = @"insert into packets_log 
                            (session_id, unique_log_id, log_time, apid, ssc, service_type, 
                             service_subtype, time_tag, is_request, n_value, crc_error, raw_packet) 
                        values
                            (" + sessionId.ToString() + ", " + uniqueID.ToString() + ", ?, " + apid.ToString() +
                             ", " + ssc.ToString() + ", " + serviceType.ToString() + ", " + serviceSubtype.ToString() +
                             ", ?, " + request + ", " + nParameter.ToString() + ", " + crc + ", ?)";

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.Date));

                DbConfiguration.Load();
                int numberOfTimeTagBytes = int.Parse(DbConfiguration.TmTimetagFormat);

                if (numberOfTimeTagBytes == 4)
                {
                    timeTagBitsDiff = 16;
                }

                cmd.Parameters.Add(new OleDbParameter("@P2", OleDbType.VarBinary, numberOfTimeTagBytes));
                cmd.Parameters.Add(new OleDbParameter("@P3", OleDbType.VarBinary, packetInBytes.GetLength(0)));

                cmd.Parameters["@P1"].Value = serverDate;

                if (isRequest)
                {
                    // Eh TC; nao ha time-tag
                    cmd.Parameters["@P2"].Value = null;
                }
                else
                {
                    // Loga a time-tag dos TMs recebidos. O tamanho varia em funcao da configuracao do sistema
                    byte[] timeTag = new byte[numberOfTimeTagBytes];
                    Array.Copy(packetInBytes, 10, timeTag, 0, numberOfTimeTagBytes);
                    cmd.Parameters["@P2"].Value = timeTag;
                }

                cmd.Parameters["@P3"].Value = packetInBytes;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // Grava agora os campos do data field. Obs: preciso de toda uma parafernalia de 
                // array bidimensional + loops devido ao fato de que a estrutura do campo pode 
                // se repetir "N" vezes (devido ao famigerado parametro N do PUS)
                if (!crcError)
                {
                    int subtypeStructure;

                    if (differentSubtype == 0)
                    {
                        subtypeStructure = serviceSubtype;
                    }
                    else
                    {
                        subtypeStructure = differentSubtype;
                    }

                    // Descubro o tamanho do vetor que vou precisar
                    sql = @"select count(*)
                            from 
                                subtype_structure a 
                                inner join data_fields b on 
                                                a.data_field_id = b.data_field_id
                            where 
                                a.service_type = " + serviceType.ToString() + @" and 
                                a.service_subtype = " + subtypeStructure.ToString();

                    cmd.CommandText = sql;
                    numberOfFields = (int)cmd.ExecuteScalar();

                    // verifica se eh uma TM de Event Reporting, com estrutura associada
                    if ((serviceType == 5) && ((serviceSubtype >= 1) && (serviceSubtype <= 4)))
                    {
                        isReportPacket = true;
                        reportId = (int)rawPacket.GetPart(128, 8);

                        // Adiciono o numero de campos da estrutura do relato
                        sql = "select count(*) from event_report_structure where rid = " + reportId.ToString();

                        cmd.CommandText = sql;
                        numberOfFields += (int)cmd.ExecuteScalar();
                    }

                    // verifica se eh uma TM de Nack do TC Verification, com estrutura associada
                    if ((serviceType == 1) && ((serviceSubtype == 2) || (serviceSubtype == 8)))
                    {
                        isNackPacket = true;
                        tcFailureCode = (int)rawPacket.GetPart(160 - timeTagBitsDiff, 8);

                        // Adiciono o numero de campos da estrutura do nack
                        sql = "select count(*) from tc_failure_code_structure where tc_failure_code = " + tcFailureCode.ToString();

                        cmd.CommandText = sql;
                        numberOfFields += (int)cmd.ExecuteScalar();
                    }

                    // verifica se eh uma TM de housekeeping, tambem com estrutura associada
                    if ((serviceType == 3) && (serviceSubtype == 25))
                    {
                        isTelemetryPacket = true;

                        /** 
                         * @todo Esta associacao eh indireta, e nao esta boa; defino o tamanho do SID 
                         * em funcao do numero de bytes da time-tag. Rever isso posteriormente
                         **/
                        if (numberOfTimeTagBytes == 4)
                        {
                            // Configuracao Amazonia-1;
                            structureId = (int)rawPacket.GetPart(128 - timeTagBitsDiff, 8);
                        }
                        else
                        {
                            // Configuracao COMAV
                            structureId = (int)rawPacket.GetPart(128 - timeTagBitsDiff, 16);
                        }

                        // Adiciono o numero de campos da estrutura do relato
                        sql = "select count(*) from report_definition_structure where structure_id = " + structureId.ToString();

                        cmd.CommandText = sql;
                        numberOfFields += (int)cmd.ExecuteScalar();
                    }

                    // verifica se eh um TC Define New Housekeeping Report, que repete N vezes o campo ParameterId
                    if ((serviceType == 3) && (serviceSubtype == 1))
                    {
                        isNewReportDefinitionPacket = true;
                        structureId = (int)rawPacket.GetPart(80 - timeTagBitsDiff, 16);

                        // Adiciono o numero de campos da estrutura do relato
                        sql = "select count(*) from report_definition_structure where structure_id = " + structureId.ToString();

                        cmd.CommandText = sql;
                        numberOfParameters = (int)cmd.ExecuteScalar();
                        numberOfFields += numberOfParameters;
                    }

                    // Agora obtenho a estrutura do pacote
                    sql = @"select 
                                a.data_field_id, a.position, b.number_of_bits, b.data_field_name
                            from 
                                subtype_structure a 
                                inner join data_fields b on 
                                                a.data_field_id = b.data_field_id
                            where 
                                a.service_type = " + serviceType.ToString() + @" and 
                                a.service_subtype = " + subtypeStructure.ToString() + @" 
                            order by 
                                a.position";

                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();

                    // verifica e eh um TC com comandos embutidos
                    if (((serviceType == 19) && (serviceSubtype == 1)) ||
                        ((serviceType == 11) && (serviceSubtype == 4)))
                    {
                        hasEmbeddedTCs = true;
                    }

                    // Declaro um array bidimensional para armazenar a estrutura do pacote
                    int[,] packetStructure = new int[numberOfFields, 5];

                    int index = 0;

                    // Alimento o array com a estrutura do pacote
                    while (dr.Read())
                    {
                        packetStructure[index, 0] = dr.GetInt32(dr.GetOrdinal("data_field_id"));
                        packetStructure[index, 1] = 0; // parameter_id
                        packetStructure[index, 2] = dr.GetInt32(dr.GetOrdinal("position"));

                        // TODO: isso nao funciona corretamente para campos do tipo Variable Size menores de 64 bits.
                        // a outros pontos em que este campo eh utilizado, eles deveriam ser revistos tambem
                        packetStructure[index, 3] = dr.GetInt32(dr.GetOrdinal("number_of_bits"));

                        if (hasEmbeddedTCs)
                        {
                            if ((dr.GetString(dr.GetOrdinal("data_field_name")).ToUpper().Equals("PACKET LENGTH")) &&
                                (dr.GetInt32(dr.GetOrdinal("number_of_bits")) == 16))
                            {
                                packetStructure[index, 4] = 1; // indica que este campo mantem o packet_length
                            }
                            else
                            {
                                packetStructure[index, 4] = 0;
                            }
                        }
                        else
                        {
                            packetStructure[index, 4] = 0;
                        }

                        index++;
                    }

                    dr.Close();

                    int reportAndNackStartIndex = index;

                    // agora adiciono a estrutura os campos do pacote de nack, se houverem
                    if (isReportPacket)
                    {
                        sql = @"select 
	                                a.data_field_id,
	                                a.position,
	                                b.number_of_bits
                                from 
	                                event_report_structure a
	                                inner join data_fields b on
			                                a.data_field_id = b.data_field_id
                                where
	                                a.rid = " + reportId.ToString() + @"
                                order by
	                                a.position";

                        cmd.CommandText = sql;
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            packetStructure[index, 0] = dr.GetInt32(dr.GetOrdinal("data_field_id"));
                            packetStructure[index, 1] = 0; // parameter_id
                            packetStructure[index, 2] = dr.GetInt32(dr.GetOrdinal("position")) + reportAndNackStartIndex;
                            packetStructure[index, 3] = dr.GetInt32(dr.GetOrdinal("number_of_bits"));
                            packetStructure[index, 4] = 0;

                            index++;
                        }

                        dr.Close();
                    }

                    // agora adiciono a estrutura os campos do pacote de report, se houverem
                    if (isNackPacket)
                    {
                        sql = @"select 
	                                a.data_field_id,
	                                a.position,
	                                b.number_of_bits
                                from 
	                                tc_failure_code_structure a
	                                inner join data_fields b on
			                                a.data_field_id = b.data_field_id
                                where
	                                a.tc_failure_code = " + tcFailureCode.ToString() + @"
                                order by
	                                a.position";

                        cmd.CommandText = sql;
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            packetStructure[index, 0] = dr.GetInt32(dr.GetOrdinal("data_field_id"));
                            packetStructure[index, 1] = 0; // parameter_id
                            packetStructure[index, 2] = dr.GetInt32(dr.GetOrdinal("position")) + reportAndNackStartIndex;
                            packetStructure[index, 3] = dr.GetInt32(dr.GetOrdinal("number_of_bits"));
                            packetStructure[index, 4] = 0;

                            index++;
                        }

                        dr.Close();
                    }

                    // agora adiciono a estrutura os campos do pacote de housekeeping, se houverem
                    if (isTelemetryPacket)
                    {
                        sql = @"select 
	                                a.parameter_id,
	                                a.position,
	                                isnull(convert(int,
                                                case data_type
                                                    when 'boolean (8 bits)' then 8
                                                    when 'int8' then 8
                                                    when 'int16' then 16
                                                    when 'int32' then 32
                                                    when 'int64' then 64
                                                end)
                                           , 0) as number_of_bits
                                from 
	                                report_definition_structure a
	                                inner join parameters b on
			                                a.parameter_id = b.parameter_id
                                where
	                                a.structure_id = " + structureId.ToString() + @"
                                order by
	                                a.position";

                        cmd.CommandText = sql;
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            packetStructure[index, 0] = 0; // data_field_id
                            packetStructure[index, 1] = dr.GetInt32(dr.GetOrdinal("parameter_id"));
                            packetStructure[index, 2] = dr.GetInt32(dr.GetOrdinal("position")) + reportAndNackStartIndex;
                            packetStructure[index, 3] = dr.GetInt32(dr.GetOrdinal("number_of_bits"));
                            packetStructure[index, 4] = 0;

                            index++;
                        }

                        dr.Close();
                    }

                    // adiciono "N" campos Parameter ID
                    if (isNewReportDefinitionPacket)
                    {
                        for (int i = 0; i < numberOfParameters; i++)
                        {
                            /** @attention A linha abaixo depende do cadastro no banco de dados! **/
                            packetStructure[index, 0] = 61; // data_field_id para o ParameterId

                            packetStructure[index, 1] = 0;
                            packetStructure[index, 2] = reportAndNackStartIndex + i + 1;
                            packetStructure[index, 3] = 16;
                            packetStructure[index, 4] = 0;

                            index++;
                        }
                    }

                    // Define o sql comum a todos os inserts do loop abaixo
                    /** @todo Passar a tratar e gravar o long_data_field_value corretamente. **/
                    sql = @"insert into packets_log_data_field 
                                (session_id, unique_log_id, data_field_id, parameter_id, position, nth_element, is_data_field, data_field_value, long_data_field_value) 
                            values 
                                (" + sessionId.ToString() + ", " + uniqueID.ToString() + ", ?, ?, ?, ?, ?, ?, NULL)";

                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.Integer));
                    cmd.Parameters.Add(new OleDbParameter("@P2", OleDbType.Integer));
                    cmd.Parameters.Add(new OleDbParameter("@P3", OleDbType.Integer));
                    cmd.Parameters.Add(new OleDbParameter("@P4", OleDbType.Integer));
                    cmd.Parameters.Add(new OleDbParameter("@P5", OleDbType.Boolean));
                    cmd.Parameters.Add(new OleDbParameter("@P6", OleDbType.BigInt));

                    int startBit;
                    Int64 fieldValue; // nao pode ser uint para nao estourar na conversao para BigInt do SQL

                    // Posiciona o bit inicial, pulando os campos de time-tag
                    if (rawPacket.IsDeviceCommand == true)
                    {
                        startBit = 48;
                    }
                    else
                    {
                        if (allowRepetition)
                        {
                            startBit = 88 - timeTagBitsDiff; // ha parametro N
                        }
                        else
                        {
                            startBit = 80 - timeTagBitsDiff; // nao ha parametro N
                        }
                    }

                    // Se for TM, ha 48 bits a mais da time-tag
                    if (!isRequest)
                    {
                        startBit += 48;
                    }

                    // Agora, repetindo [N parameter] vezes, grava os campos de acordo com a estrutura
                    int embeddedPacketLength = 0;

                    for (int i = 1; i <= nParameter; i++)
                    {
                        for (int j = 0; j < numberOfFields; j++)
                        {
                            // Obtem o valor do campo no packet recebido
                            // reduzo de UInt64 para Int64 para evitar erros na conversao para o tipo BigInt do SQL Server
                            // na pratica, os campos poderao ter valores de no maximo 63 bits devido a limitacao 
                            fieldValue = (Int64)rawPacket.GetPart(startBit, (int)packetStructure[j, 3]);

                            cmd.Parameters["@P1"].Value = packetStructure[j, 0];
                            cmd.Parameters["@P2"].Value = packetStructure[j, 1];
                            cmd.Parameters["@P3"].Value = packetStructure[j, 2];
                            cmd.Parameters["@P4"].Value = i;

                            if (packetStructure[j, 0] == 0)
                            {
                                cmd.Parameters["@P5"].Value = false;
                            }
                            else
                            {
                                cmd.Parameters["@P5"].Value = true;
                            }

                            cmd.Parameters["@P6"].Value = fieldValue;

                            if (hasEmbeddedTCs)
                            {
                                if (packetStructure[j, 4] == 1)
                                {
                                    embeddedPacketLength = (int)fieldValue + 1;
                                }
                            }

                            cmd.ExecuteNonQuery();

                            startBit += (int)packetStructure[j, 3];
                        }

                        // "Pula" os bits que pertencem ao application_data, se for um TC [19/1] ou [11/4]
                        if (hasEmbeddedTCs)
                        {
                            // Os 32 bits subtraidos dizem respeito aos campos do data header (4 bytes) que ja foram gravados
                            startBit += (embeddedPacketLength * 8) - 32;
                            embeddedPacketLength = 0;
                        }
                    }
                }

                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
            }
            catch (OleDbException ex)
            {
                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                MessageBox.Show("Database Error: " + ex.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            recordingPacket = false;
        }

        /**         
         * Checa se o SessionId existe no banco
         **/
        public bool OpenFileSession(int session)
        {
            string sql = "select count(session_id) as countSessionId from sessions where connection_type = 'file' and session_id = " + session;
            int sessId = (int)DbInterface.ExecuteScalar(sql);

            if (sessId > 0)
            {
                sessionId = session; //setar o atributo interno sessionId
                sessionOpenned = true; //marcar como sessao aberta
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Metodos Publicos Estaticos

        /** Retorna um DataTable com uma lista com as sessoes disponiveis na base. **/
        public static DataTable GetSessionList(string connectionType)
        {
            string whereClause = "";

            if (!connectionType.ToUpper().Equals("ALL"))
            {
                whereClause = " where connection_type = '" + connectionType + "' ";
            }

            string sqlCaracteres = "select isnull(MAX(session_id), 0) from sessions";
            string caracteres = Convert.ToString(DbInterface.ExecuteScalar(sqlCaracteres));
            Int32 nOfCaracteres = caracteres.Length;

            // f_zero eh uma user-defined function (user dbo) na base Simulador_UTMC
            string sql = @"select dbo.f_zero(session_id, " + nOfCaracteres.ToString() + @" ) + ' [from ' + 
                                  convert(varchar, start_time, 103) + ' ' + 
                                  convert(varchar, start_time, 108) + ' to ' + 
                                  convert(varchar, end_time, 103) + ' ' + 
                                  convert(varchar, end_time, 108) + ', through ' +
                                  connection_type + 
                                  case when ((isnull(swapl_version, 0) = 0) and (isnull(swapl_release, 0) = 0) and (isnull(swapl_patch, 0) = 0)) then 
										']' 
								  else 
										', SW APL Version ' + isnull(convert(varchar, swapl_version), '') + '.' + isnull(convert(varchar, swapl_release), '') + '.' + isnull(convert(varchar, swapl_patch), '') + ']' 
								  end 
                           from sessions " + whereClause + " order by session_id desc";

            return (DbInterface.GetDataTable(sql));
        }

        /** Retorna um DataTable com uma lista dos pacotes de uma sessao especifica, de acordo com os parametros. **/
        public static DataTable GetPacketsInSession(int loggedSessionId, string packetsType, bool onlyValid)
        {
            string sql = "";
            string whereClause = "a.session_id = " + loggedSessionId.ToString() + " ";

            if (packetsType.ToUpper().Equals("REQUESTS")) // TCs
            {
                whereClause += " and b.is_request = 1 ";
            }
            else if (packetsType.ToUpper().Equals("REPORTS")) // TMs
            {
                whereClause += " and b.is_request = 0 ";
            }

            if (onlyValid)
            {
                whereClause += " and b.crc_error = 0 ";
            }

            // Cuidado ao modificar esta query; ela eh extremamente complexa!
            #region selectGetPacketsInSession
            sql = @"select 
                        convert(varchar, b.log_time, 103) + ' ' + convert(varchar, b.log_time, 108) as [Log Time],
                        case b.is_request when 1 then 'Request' else 'Report' end as [Packet Type],
		                case b.is_request when 1 then '[N/A]' else upper(master.dbo.fn_varbintohexstr(b.time_tag)) end as [Time Tag],
                        '[' + dbo.f_zero(c.apid, 4) + '] ' + isnull(c.application_name, 'INVALID APID !!!') as APID,
                        b.ssc as SSC,
                        '[' + dbo.f_zero(b.service_type, 3) + '] ' + isnull(d.service_name, 'INVALID SERVICE TYPE !!!') as [Service Type],
                        '[' + dbo.f_zero(b.service_subtype, 3) + '] ' + isnull(e.description, 'INVALID SERVICE SUBTYPE !!!') as [Service Subtype],        
                        case b.crc_error when 0 then 'OK' else 'Error' end as CRC,
                        case b.is_request when 0 then '[N/A]' else
                            case (convert(int, convert(varbinary, '0x0' + substring(master.dbo.fn_varbintohexstr(b.raw_packet), 16, 1), 1)) & 1)
                                when 0 then 
                                    'Not Asked' 
                                else
                                    isnull(
                                            (select top 1
	                                            case service_subtype 
		                                            when 1 then 
			                                            'Success Ack' 
		                                            else 
			                                            'Failure Ack: 0x' + substring(master.dbo.fn_varbintohexstr(raw_packet), 42, 1)
		                                            end
                                            from 
	                                            packets_log 
                                            where 
	                                            session_id = b.session_id and 
	                                            is_request = 0 and
	                                            service_type = 1 and 
	                                            service_subtype in (1, 2) and
                                                log_time >= b.log_time and
	                                            substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 37, 2) = substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.apid))), 3, 2) and
	                                            /** obs: a clausula abaixo so funciona para sscs <= 255!!! **/						
	                                            substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 41, 2) = substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.ssc))), 3, 2)
                                            ), 
                                            'Not Received')
                               end end as 'Reception Ack',
                        case b.is_request when 0 then '[N/A]' else	                    
                            case (convert(int, convert(varbinary, '0x0' + substring(master.dbo.fn_varbintohexstr(b.raw_packet), 16, 1), 1)) & 8)
                                when 0 then 
                                    'Not Asked' 
                                else
                                    isnull(
                                            (select top 1
	                                            case service_subtype 
		                                            when 7 then 
			                                            'Success Ack' 
		                                            else 
			                                            'Failure Ack: 0x' + substring(master.dbo.fn_varbintohexstr(raw_packet), 42, 1)
		                                            end
                                            from 
	                                            packets_log 
                                            where 
	                                            session_id = b.session_id and 
	                                            is_request = 0 and
	                                            service_type = 1 and 
	                                            service_subtype in (7, 8) and
                                                log_time >= b.log_time and
	                                            substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 37, 2) = substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.apid))), 3, 2) and
	                                            /** obs: a clausula abaixo so funciona para sscs <= 255!!! **/						
	                                            substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 41, 2) = substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.ssc))), 3, 2)
                                            ), 
                                            'Not Received')
                                end end as 'Execution Ack',
                        /* as colunas abaixo devem ficar ocultas no grid */
                        b.session_id,
                        b.log_time,
                        b.apid,
                        b.service_type,
                        b.service_subtype,
                        b.unique_log_id,
                        isnull(e.allow_repetition, 0) as allow_repetition,
                        b.n_value,
                        b.raw_packet
                    from 
                        sessions a 
                        inner join packets_log b on
                                a.session_id = b.session_id
                        left join apids c on
                                b.apid = c.apid
                        left join services d on
                                b.service_type = d.service_type
                        left join subtypes e on
                                b.service_type = e.service_type and
                                b.service_subtype = e.service_subtype
                    where " + whereClause + " order by [Log Time], b.is_request desc, b.ssc";
            #endregion

            return (DbInterface.GetDataTable(sql));
        }

        /** Retorna o Application Data Field de um pacote especifico logado na base. **/
        public static DataTable GetApplicationDataFromPacket(int sessionId, int uniqueId, bool isTelemetryPacket)
        {
            string sql = "";

            // Cuidado ao modificar esta query; ela eh extremamente complexa!
            sql = @"select
                        b.data_field_name,
                        case
	                        when b.type_is_bool = 1 then
		                        case a.data_field_value
			                        when 1 then 
				                        'True' 
			                        else 
				                        'False' 
			                        end
	                        when b.type_is_numeric = 1 then
		                        convert(varchar, a.data_field_value)
	                        when b.type_is_raw_hex = 1 then
		                        upper(master.dbo.fn_varbintohexstr(a.data_field_value))
	                        when b.type_is_list = 1 then
		                        '[' + dbo.f_zero(a.data_field_value, 4) + '] ' + isnull((select top 1 list_text from data_field_lists where list_id = b.list_id and list_value = a.data_field_value), '')
	                        /* como exec() nao funciona como subquery, precisei usar um case para as tabelas. Melhorar isso depois. */
	                        when b.type_is_table = 1 then
		                        '[' + dbo.f_zero(a.data_field_value, 5) + '] ' + isnull(
		                        case lower(b.table_name)
			                        when 'apids' then
				                        (select top 1 application_name from apids where apid = a.data_field_value)
			                        when 'rids' then
				                        (select top 1 description from rids where rid = a.data_field_value)
			                        when 'services' then
				                        (select top 1 service_name from services where service_type = a.data_field_value)
			                        when 'subtypes' then
				                        (select top 1 description from subtypes where service_subtype = a.data_field_value and 
                                            service_type = (select 
                                                                top 1 x.data_field_value 
                                                            from 
                                                                packets_log_data_field x inner join data_fields y on x.data_field_id = y.data_field_id 
                                                            where 
                                                                y.data_field_name = 'Service Type' and 
                                                                x.session_id = a.session_id and 
                                                                x.unique_log_id = a.unique_log_id and 
                                                                a.nth_element = x.nth_element and
                                                                x.position < a.position
                                                            order by
                                                                x.position desc))
                                    when 'parameters' then
                                        (select top 1 parameter_description from parameters where parameter_id = a.data_field_value)
                                    when 'report_definitions' then
                                        (select top 1 report_definition_description from report_definitions where structure_id = a.data_field_value)
                                    when 'memory_ids' then
        		                        (select top 1 memory_unit_description from memory_ids where memory_id = a.data_field_value)
                                    when 'output_line_ids' then
        		                        (select top 1 output_line_description from output_line_ids where output_line_id = a.data_field_value)
                                    when 'packet_store_ids' then
                                        (select top 1 packet_store_name from packet_store_ids where store_id = a.data_field_value)
                                    when 'tc_failure_codes' then
                                        (select top 1 tc_failure_description from tc_failure_codes where tc_failure_code = a.data_field_value)
		                        end, '** INVALID VALUE! **')
                        end as [value],
                        b.number_of_bits,
                        a.nth_element,
                        a.position
                    from    
                        packets_log_data_field a 
                        inner join data_fields b on
	                        a.data_field_id = b.data_field_id and
                            a.data_field_id <> 0
                    where
                        a.session_id = " + sessionId.ToString() + @" and
                        a.unique_log_id = " + uniqueId.ToString();


            if (!isTelemetryPacket)
            {
                sql += " order by a.nth_element, a.position";
            }
            else
            {
                // Se for um pacote de telemetria, agrega as colunas dos parametros
                sql += @"   union all 
                            select 
	                            b.parameter_description as data_field_name,
                                convert(nvarchar, 
			                            case b.data_type
			                            when  'boolean (8 bits)' then
				                            case when a.data_field_value = 0 then 'false' else 'true' end
			                            when 'int8' then
				                            case when b.show_as_hex = 1 then
					                            '0x' + right(master.dbo.fn_varbintohexstr(a.data_field_value), 2)
				                            else
					                            convert(nvarchar, a.data_field_value)
				                            end
			                            when 'int16' then
        		                            case when b.show_as_hex = 1 then
					                            '0x' + right(master.dbo.fn_varbintohexstr(a.data_field_value), 4)
				                            else
					                            convert(nvarchar, a.data_field_value)
				                            end
			                            when 'int32' then
        		                            case when b.show_as_hex = 1 then
					                            '0x' + right(master.dbo.fn_varbintohexstr(a.data_field_value), 8)
				                            else
					                            convert(nvarchar, a.data_field_value)
				                            end
			                            when 'int64' then
        		                            case when b.show_as_hex = 1 then
					                            '0x' + right(master.dbo.fn_varbintohexstr(a.data_field_value), 16)
				                            else
					                            convert(nvarchar, a.data_field_value)
				                            end
			                            end) as [value],
	                            isnull(convert(int,
                                                    case b.data_type
                                                        when 'boolean (8 bits)' then 8
                                                        when 'int8' then 8
                                                        when 'int16' then 16
                                                        when 'int32' then 32
                                                        when 'int64' then 64
                                                    end)
                                               , 0) as number_of_bits,
                                a.nth_element,
                                a.position
                            from
	                            packets_log_data_field a 
                                inner join parameters b on 
                                        a.parameter_id = b.parameter_id and
                                        a.parameter_id <> 0
                            where 
                                a.session_id = " + sessionId.ToString() + @" and
                                a.unique_log_id = " + uniqueId.ToString() + @"
                            order by 
                                nth_element,
                                position";
            }

            return DbInterface.GetDataTable(sql);
        }

        #endregion
    }
}

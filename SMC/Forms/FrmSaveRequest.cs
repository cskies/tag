/**
 * @file 	    FrmSaveRequest.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    07/12/2009
 * @note	    Modificado em 24/10/2013 por Bruna.
 * @note        Modificado em 02/03/2015 por Conrado.
 * @note	    Modificado em 28/05/2015 por Thiago.
 **/

using System;
using System.Data;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Data.OleDb;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;
using Inpe.Subord.Comav.Egse.Smc.Utils;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSaveRequest
     * Formulario usado para salvar o pacote editado na tela Requests Composition.
     **/
    public partial class FrmSaveRequest : Form
    {
        #region Atributos privados

        private OleDbConnection conn = null;
        private OleDbCommand cmd = null;
        private OleDbTransaction transaction = null;
        private RawPacket requestPacket = null;
        private int fieldVariableLength = 0;
        private String autoSsc = "0";
        private String autoLength = "0";
        private String autoCrc = "0";

        private int serviceType = 0;
        private int serviceSubType = 0;
        private int nFields = 1;

        private object dateTime = null;
        private int[] _isCalendar;
        private bool _relativeTime;

        #endregion

        #region Construtor

        //inclusao dos parametros type e subType para posteriormente tratar comandos diretos
        public FrmSaveRequest(RawPacket request, int type, int subType, int variableLength
            , bool autoChekSsc, bool autoChekLength, bool autoChekCrc, int numberFields, bool isRelative, int[]statusCalendar )
        {
            _relativeTime = isRelative;
            _isCalendar = statusCalendar;
            InitializeComponent();
            requestPacket = request;
            fieldVariableLength = variableLength;

            if (autoChekSsc)
            {
                autoSsc = "1";
            }

            if (autoChekLength)
            {
                autoLength = "1";
            }

            if (autoChekCrc)
            {
                autoCrc = "1";
            }

            int apid = (int)request.GetPart(5, 11);
            nFields = numberFields;
            serviceType = (int)request.GetPart(56, 8);
            serviceSubType = (int)request.GetPart(64, 8);

            if (apid == 1) //cpdu
            {
                serviceType = type;
                serviceSubType = subType;
            }

            string sql = "select service_name from services where service_type = " + type;
            string serviceName = (string)DbInterface.ExecuteScalar(sql);

            txtDescription.Text = serviceName + " [" + serviceType.ToString() + "." + serviceSubType.ToString() + "]";
            txtDescription.Focus();
            txtDescription.SelectAll();

            CancelButton = btCancel;
        }

        #endregion

        #region Metodos privados

        //inclusão dos parametros type e subType
        private bool SaveRequest(RawPacket rawPacket, int type, int subType)
        {
            try
            { 
                int savedRequestId = 0;
                string description = txtDescription.Text.Trim();
                int apid, ssc, serviceType, serviceSubtype, differentSubtype = 0;
                string crcError = "0";
                DateTime serverDate = new DateTime();
                bool allowRepetition = false;
                bool hasEmbeddedTCs = false;
                bool variableLength = false;
                bool isNewReportDefinitionPacket = false;
                int structureId = -1;
                int nParameter = 1;
                int numberOfFields = 0;
                int numberOfParameters = 0;
                byte[] packetInBytes = rawPacket.RawContents;

                // Instanciar os objetos de Conexao e Iniciar a Transacao
                conn = new OleDbConnection();
                cmd = new OleDbCommand();
                conn.ConnectionString = "file name = " + Properties.Settings.Default.db_connection_string;
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                
                string sql;


                // Verificar se existe description duplicada
                sql = "select isnull(count(saved_request_id), 0) as requestId from saved_requests where description = dbo.f_regularString('" + description + "')";
                int alreadyExistDescription = (int)DbInterface.ExecuteScalar(sql);

                if (alreadyExistDescription > 0)
                {
                    MessageBox.Show("The Request Description '" + txtDescription.Text.Trim() + "' already exist !\n\nCorrect it and try again.",
                                        "Inconsistent Data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    txtDescription.SelectAll();

                    transaction.Rollback();
                    conn.Close();
                    cmd.Dispose();
                    conn.Dispose();

                    return false;
                }

                if (txtDescription.Text.Trim().Equals(""))
                {
                    MessageBox.Show("The field 'Request Description' is empty ! \n\nFill it and try again.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                    txtDescription.Focus();

                    transaction.Rollback();
                    conn.Close();
                    cmd.Dispose();
                    conn.Dispose();

                    return false;
                }

                // selecionar o ultimo id e incrementar um novo
                sql = "select isnull(max(saved_request_id), 0) + 1 from saved_requests";
                savedRequestId = (int)DbInterface.ExecuteScalar(sql);

                // Agora registra o pacote
                apid = (int)rawPacket.GetPart(5, 11);
                ssc = (int)((packetInBytes[2] & 0x3f) << 8 | packetInBytes[3]);
                serviceType = (int)packetInBytes[7];
                serviceSubtype = (int)packetInBytes[8];

                if (apid == 1)
                {
                    serviceType = type;
                    serviceSubtype = subType;
                }

                // Determina se ha o parametro N neste pacote
                sql = @"select top 1 isnull(a.same_as_subtype, 0) as same_as_subtype, isnull(b.allow_repetition, 0) as allow_repetition, getdate() as now
                        from subtype_structure a right join subtypes b on a.service_type = b.service_type and a.service_subtype = b.service_subtype 
                        where b.service_type = " + serviceType.ToString() + " and b.service_subtype = " + serviceSubtype.ToString() +
                        " order by a.position";

                DataTable tblParamN = DbInterface.GetDataTable(sql);

                for (int i = 0; i < tblParamN.Rows.Count; i++)
                {
                    differentSubtype = Convert.ToInt32(tblParamN.Rows[i]["same_as_subtype"]);
                    allowRepetition = Convert.ToBoolean(tblParamN.Rows[i]["allow_repetition"]);
                    serverDate = Convert.ToDateTime(tblParamN.Rows[i]["now"]);
                }

                /*atendendo o parâmentro nField em caso de TCD's -- if incluído em 11/02/15
                em caso do usuario inserir ou excluir novos campos no teste*/
                if (allowRepetition && (apid != 1))
                {
                    nParameter = (int)packetInBytes[10];
                }
                else
                {
                    nParameter = nFields;
                }

                if (CheckingCodes.CrcError(packetInBytes))
                {
                    crcError = "1"; // tem erro
                }

                // Uso oledbparameters aqui para a insercao do raw_packet bruto no banco de dados
                sql = @"insert into saved_requests (saved_request_id, 
							                    description, 
							                    apid, 
                                                auto_ssc,
                                                auto_length,
                                                auto_crc,
							                    ssc, 
							                    service_type, 
							                    service_subtype, 
							                    n_value, 
							                    crc_error, 
							                    raw_packet) 
                    values (" + savedRequestId + ", '" + description + "', " + apid + @", 
                    " + autoSsc + ", " + autoLength + ", " + autoCrc + @",
                    " + ssc + ", " + serviceType + ", " + serviceSubtype + @",
                    " + nParameter + ", '" + crcError + "', ?)";

                cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.VarBinary, packetInBytes.GetLength(0)));
                cmd.Parameters["@P1"].Value = packetInBytes;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

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

                numberOfFields = (int)DbInterface.ExecuteScalar(sql);

                // verifica se eh um TC com comandos embutidos
                if (((serviceType == 19) && (serviceSubtype == 1)) ||
                    ((serviceType == 11) && (serviceSubtype == 4)))
                {
                    hasEmbeddedTCs = true;
                }

                // Agora obtenho a estrutura do pacote            
                sql = @"select 
                            a.data_field_id, a.position, b.number_of_bits, b.data_field_name, b.variable_length
                        from 
                            subtype_structure a 
                            inner join data_fields b on 
                                            a.data_field_id = b.data_field_id
                        where 
                            a.service_type = " + serviceType.ToString() + @" and 
                            a.service_subtype = " + subtypeStructure.ToString() + @" 
                        order by 
                            a.position";

                // Grava agora os campos do data field. Obs: preciso de toda uma parafernalha de 
                // array bidimensional + loops devido ao fato de que a estrutura do campo pode 
                // se repetir "N" vezes (devido ao famigerado parametro N do PUS)
                DataTable tblPacketStructure = DbInterface.GetDataTable(sql);
                int[,] packetStructure = new int[numberOfFields, 5];

                DataTable tblParameterId = null;

                // verifica se eh um TC Define New Housekeeping Report, que repete N vezes o campo ParameterId
                if ((serviceType == 3) && (serviceSubtype == 1))
                {
                    isNewReportDefinitionPacket = true;
                    structureId = (int)rawPacket.GetPart(80, 16);

                    // Adiciono o numero de campos da estrutura do relato
                    sql = "select count(*) from report_definition_structure where structure_id = " + structureId.ToString();

                    numberOfParameters = (int)DbInterface.ExecuteScalar(sql);
                    numberOfFields += numberOfParameters;

                    sql = "select * from report_definition_structure where structure_id = " + structureId + " order by position";
                    tblParameterId = DbInterface.GetDataTable(sql);
                    packetStructure = new int[numberOfFields, 5];
                }


                int dataFieldId = 0;
                int arrayPosition = 0;

                for (int i = 0; i < tblPacketStructure.Rows.Count; i++)
                {
                    packetStructure[i, 0] = (int)tblPacketStructure.Rows[i]["data_field_id"];
                    packetStructure[i, 1] = (int)tblPacketStructure.Rows[i]["position"];
                    packetStructure[i, 2] = (int)tblPacketStructure.Rows[i]["number_of_bits"];
                    string dataFieldName = tblPacketStructure.Rows[i]["data_field_name"].ToString();

                    if (dataFieldName.ToUpper().Equals("STRUCTURE ID"))
                    {
                        dataFieldId = packetStructure[i, 0];
                    }

                    if (hasEmbeddedTCs)
                    {
                        if ((dataFieldName.ToUpper().Equals("PACKET LENGTH")) &&
                            ((int)tblPacketStructure.Rows[i]["number_of_bits"] == 16))
                        {
                            packetStructure[i, 3] = 1; // indica que este campo mantem o packet_length
                        }
                        else
                        {
                            packetStructure[i, 3] = 0;
                        }
                    }
                    else
                    {
                        packetStructure[i, 3] = 0;
                    }

                    // para verificar se tamanho eh variavel
                    variableLength = (bool)tblPacketStructure.Rows[i]["variable_length"];

                    if (variableLength)
                    {
                        packetStructure[i, 4] = 1;
                    }
                    else
                    {
                        packetStructure[i, 4] = 0;
                    }

                    arrayPosition++;
                }

                // Se for o subtype [3.1], preencho o array com os Parameter Ids
                if (isNewReportDefinitionPacket)
                {
                    int j = 0;
                    int t = arrayPosition;// para continuar a partir da posicao que parou

                    for (int w = 0; w < tblParameterId.Rows.Count; w++)
                    {
                        packetStructure[t, 0] = 61;
                        packetStructure[t, 1] = arrayPosition;
                        packetStructure[t, 2] = (int)tblParameterId.Rows[j]["parameter_id"];
                        packetStructure[t, 3] = 0;
                        packetStructure[t, 4] = 0;

                        arrayPosition++;
                        j++;
                        t++;
                    }
                }

                sql = @"insert into saved_requests_data_field (saved_request_id, 
								                           data_field_id, 
								                           position, 
								                           nth_element, 
								                           data_field_value, 
								                           long_data_field_value) 
                    values (" + savedRequestId + ", ?, ?, ?, ?, ?)";

                //@todo: FALTA INSERIR O PARAMETER ID.. VER NO BANCO DE DADOS ESTE ATRIBUTO 

                cmd.CommandText = sql;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new OleDbParameter("@P1", OleDbType.Integer));
                cmd.Parameters.Add(new OleDbParameter("@P2", OleDbType.Integer));
                cmd.Parameters.Add(new OleDbParameter("@P3", OleDbType.Integer));
                cmd.Parameters.Add(new OleDbParameter("@P4", OleDbType.BigInt));
                cmd.Parameters.Add(new OleDbParameter("@P5", OleDbType.VarBinary));

                int startBit;

                // Posiciona o bit inicial, pulando os campos de time-tag
                if (requestPacket.IsDeviceCommand == false)
                {
                    if (allowRepetition)
                    {
                        startBit = 88; // ha parametro N
                    }
                    else
                    {
                        startBit = 80; // nao ha parametro N
                    }
                }
                else
                {
                    // para device commands (sem data field header)
                    startBit = 48; //TCD não tem time-tag (por isso, o bit init = 48)
                }

                // Agora, repetindo [N parameter] vezes, grava os campos de acordo com a estrutura
                int embeddedPacketLength = 0;

                for (int i = 1; i <= nParameter; i++) //linha
                {
                    for (int j = 0; j < numberOfFields; j++)
                    {
                        cmd.Parameters["@P1"].Value = packetStructure[j, 0];    // data_field_id
                        cmd.Parameters["@P2"].Value = packetStructure[j, 1];    // position
                        cmd.Parameters["@P3"].Value = i;                        // nth_element
                        var valueBidArray = packetStructure[j, 2];

                        // verificar se atributo eh de tamanho variavel
                        if (packetStructure[j, 4] == 0)
                        {
                            if (isNewReportDefinitionPacket && (j >= 3)) // pergunta se eh subtype [3.1] e se ja chegou nos Parameter IDs
                            {
                                cmd.Parameters["@P4"].Value = packetStructure[j, 2];
                            }
                            else
                            {
                                if(_isCalendar[i-1] != 1)
                                {
                                    UInt64 valueToPersist = rawPacket.GetPart(startBit, packetStructure[j, 2]); // data_field_value
                                    cmd.Parameters["@P4"].Value = valueToPersist;
                                    startBit += valueBidArray;
                                }
                                else
                                {
                                    string colHeaderText = tblPacketStructure.Rows[j]["data_field_name"].ToString();

                                    if (colHeaderText.Equals("Time: Seconds"))
                                    {
                                        string fieldValue = rawPacket.GetPart(startBit, packetStructure[j, 2]).ToString();
                                        char pad = '0';
                                        UInt64 longFieldValue = Convert.ToUInt64(fieldValue.PadRight(12, pad));

                                        cmd.Parameters["@P4"].Value = longFieldValue;
                                        startBit += valueBidArray;
                                    }
                                    else
                                    {
                                        UInt64 valueToPersist = rawPacket.GetPart(startBit, valueBidArray); 
                                        cmd.Parameters["@P4"].Value = valueToPersist;
                                        startBit += valueBidArray;
                                    }
                                }
                            }

                            cmd.Parameters["@P5"].Value = null;                 // long_data_field_value
                        }
                        else
                        {
                            cmd.Parameters["@P4"].Value = 0;                    // data_field_value

                            // pegar o valor do campo variavel (long_data_field_value)
                            byte[] valueVariable = new byte[((packetInBytes.Length - 4) - 18) + 1];
                            int indexValue = 0;

                            for (int w = 18; w <= (packetInBytes.Length - 4); w++)
                            {
                                valueVariable[indexValue] = packetInBytes[w - 1];
                                indexValue++;
                            }

                            cmd.Parameters["@P5"].Value = valueVariable;        // long_data_field_value

                            startBit += fieldVariableLength;
                        }

                        if (hasEmbeddedTCs)
                        {
                            if (packetStructure[j, 4] == 1)
                            {
                                if (packetStructure[j, 5] == 1) // verifica se campo eh de tamanho variavel
                                {
                                    embeddedPacketLength = (int)fieldVariableLength + 1;
                                }
                                else
                                {
                                    embeddedPacketLength = (int)packetStructure[j, 3] + 1;
                                }
                            }
                        }

                        cmd.ExecuteNonQuery();
                    }

                    // "Pula" os bits que pertencem ao application_data, se for um TC [19/1] ou [11/4]
                    if (hasEmbeddedTCs)
                    {
                        // Os 32 bits subtraidos dizem respeito aos campos do data header (4 bytes) que ja foram gravados
                        startBit += (embeddedPacketLength * 8) - 32;
                        embeddedPacketLength = 0;
                    }
                }



                transaction.Commit();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                MessageBox.Show("Database Error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }
        }

        #endregion

        #region Eventos da interface

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!SaveRequest(requestPacket, serviceType, serviceSubType))
            {
                return;
            }

            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSaveRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btSave.Enabled))
            {
                btSave_Click(this, new EventArgs());
            }
            else if ((e.KeyCode == Keys.Escape) && (btCancel.Enabled))
            {
                btCancel_Click(this, new EventArgs());
            }
        }

        #endregion
    }
}
/**
* @file 	    FrmApplicationData.cs
* @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
* @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
* @author 	    Fabricio de Novaes Kucinskis
* @date	    21/07/2009
* @note	    Modificado em 21/08/2012 por Thiago.
* @note	    Modificado em 26/05/2015 por Conrado.
* @note	    Modificado em 28/05/2015 por Thiago.
**/

using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using System;
using System.Windows.Forms;

/**
 * @Namespace Este namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmApplicationData
     * Formulario para a exibicao e edicao dos campos do Application Data
     * de pacotes que contenham pacotes (subtipos 11.4 e 19.1). Este formulario
     * pode ser chamado dos formularios FrmTcsSending e FrmSessionLog.
     **/
    public partial class FrmApplicationData : Form
    {
        #region Atributos

        private RawPacket rawPacket;
        private bool editable;
        private bool edited;
        private int type;
        private int subtype;
        public Int64 fieldValue;
        private bool embeddedPacketAlreadyEdited = false;

        #endregion

        #region Construtor

        public FrmApplicationData(int serviceType,
                                    int serviceSubtype,
                                    int apid,
                                    int ssc,
                                    bool isEditable,
                                    bool packetAlreadyEdited,
                                    int startBit,
                                    RawPacket rawPacketEmbedded)
        {
            InitializeComponent();

            type = serviceType;
            subtype = serviceSubtype;

            this.Text = "APID " + apid.ToString() +
                        ", SSC " + ssc.ToString() +
                        ", Service Type " + serviceType.ToString() +
                        ", Service Subtype " + serviceSubtype.ToString();

            // Com base no tipo / subtipo, carrega o grid
            editable = isEditable;

            AppDataGridsHandling.LoadAppDataGrid(ref gridEmbedded, serviceType, serviceSubtype, editable);
            AppDataGridsHandling.LoadDefaultCellValues(ref gridEmbedded, gridEmbedded.RowCount);

            gridEmbedded.ReadOnly = !editable;
            numN.Enabled = editable;
            btOK.Visible = editable;

            rawPacket = rawPacketEmbedded;

            if (!editable)
            {
                btCancel.Text = "Close";
            }

            if (packetAlreadyEdited)
            {
                if (rawPacketEmbedded != null)
                {
                    FillGrid(startBit, rawPacketEmbedded);
                }
            }
        }

        #endregion

        #region Propriedades

        public bool Edited
        {
            get
            {
                return edited;
            }
        }

        public bool EmbeddedPacketAlreadyEdited
        {
            get
            {
                return embeddedPacketAlreadyEdited;
            }
        }

        #endregion

        #region Metodos Publicos

        // Da acesso a mensagem pelo formulario chamador (FrmTcsSending)
        public RawPacket GetEmbeddedRawPacket()
        {
            return (rawPacket);
        }

        #endregion

        #region Metodos Privados

        /**
         * Rotina que carrega os valores do grid com base no que foi lido do pacote de origem.
         **/
        private void FillGrid(int startBit, RawPacket sourcePacket)
        {
            if ((bool)numN.Tag)
            {
                int nField = (int)sourcePacket.GetPart(startBit, 8);

                if (nField > 0)
                {
                    numN.Value = sourcePacket.GetPart(startBit, 8);
                }

                // Se o TC embarcado tem campo N, entao avanca 1 byte
                startBit += 8;
            }

            int x, y, numberOfBits;
            UInt64 value;

            for (int i = 0; i < gridEmbedded.RowCount; i++)
            {
                for (int j = 0; j < gridEmbedded.ColumnCount; j++)
                {
                    x = gridEmbedded.Columns[j].ToolTipText.IndexOf(",");
                    y = gridEmbedded.Columns[j].ToolTipText.IndexOf("bits");
                    numberOfBits = UInt16.Parse(gridEmbedded.Columns[j].ToolTipText.Substring(x + 2, (y - x - 3)));

                    value = sourcePacket.GetPart(startBit, numberOfBits);

                    if (gridEmbedded.Columns[j].ToolTipText.Contains("Bool"))
                    {
                        if (value == 0)
                        {
                            gridEmbedded[j, i].Value = "False";
                        }
                        else
                        {
                            gridEmbedded[j, i].Value = "True";
                        }
                    }
                    else if (gridEmbedded.Columns[j].ToolTipText.Contains("Raw Hex"))
                    {
                        // Formata para hexa
                        gridEmbedded[j, i].Value = "0x" + value.ToString("X");
                    }
                    else if (gridEmbedded.Columns[j].ToolTipText.Contains("Table"))
                    {
                        //* Por enquanto NAO deletar o trecho de codigo abaixo.
                        //* Este faz uma busca na base de dados quando vai carregar dados no FrmAppData.
                        //* Hoje temos uma solucao para carregar o FrmAppData com os valores extraidos do RawPacket.
                        //* Mas por medida de precaucao, vamos deixar este ttrecho por enquanto.
                        //* 
                        //* Ass: Thiago
                        //*
                        //* 
                        #region anotacaoThiagoFrmAppDataLoad
                        /*
                         String dataFieldID = gridEmbedded.Columns[j].Name.ToString();

                         String sql = "";

                        // Consultar a base e buscar o valor
                        sql = @"select top 1
                                    case
                                        when a.type_is_list = 1 then
                                            '[' + dbo.f_zero(" + value.ToString() + @", 4) + '] ' + isnull((select top 1 list_text from data_field_lists where list_id = a.list_id and list_value = " + value.ToString() + @"), '')
                                        /* como exec() nao funciona como subquery, precisei usar um case para as tabelas. Melhorar isso depois. */
                        /*              when a.type_is_table = 1 then
                                          '[' + dbo.f_zero(" + value.ToString() + @", 4) + '] ' +
                                          case lower(a.table_name)
                                              when 'apids' then
                                                  (select top 1 application_name from apids where apid = " + value.ToString() + @")
                                              when 'rids' then
                                                  (select top 1 description from rids where rid = " + value.ToString() + @")
                                              when 'services' then
                                                  (select top 1 service_name from services where service_type = " + value.ToString() + @")
                                              when 'subtypes' then
                                                  (select top 1 description from subtypes where service_subtype = " + value.ToString() + @")
                                              when 'memory_ids' then
                                                  (select top 1 memory_unit_description from memory_ids where memory_id = " + value.ToString() + @")
                                              when 'output_line_ids' then
                                                  (select top 1 output_line_description from output_line_ids where output_line_id = " + value.ToString() + @")
                                              when 'packet_store_ids' then
                                                  (select top 1 packet_store_name from packet_store_ids where store_id = " + value.ToString() + @")
                                              when 'parameters' then
                                                  (select top 1 parameter_description from parameters where parameter_id = " + value.ToString() + @")
                                              when 'report_definitions' then
                                                  (select top 1 report_definition_description from report_definitions where structure_id = " + value.ToString() + @")
                                          end
                                  end as [value]
                              from 
                                  data_fields a
                              where
                                  a.data_field_id = " + dataFieldID;

                      gridEmbedded[j, i].Value = (string)DbInterface.ExecuteScalar(sql);
                           
                      *
                      * 
                      */
                        #endregion

                        DataGridViewComboBoxColumn cell = (DataGridViewComboBoxColumn)gridEmbedded.Columns[j];

                        for (int w = 0; w < cell.Items.Count; w++)
                        {
                            int first = cell.Items[w].ToString().IndexOf("[");
                            int last = cell.Items[w].ToString().IndexOf("]");

                            String codeItem = cell.Items[w].ToString().Substring((first + 1), (last - 1)).Trim();
                            UInt64 code = 0;

                            if (UInt64.TryParse(codeItem, out code) && (code == (value)))
                            {
                                gridEmbedded.Rows[i].Cells[j].Value = cell.Items[w].ToString();
                                break;
                            }
                        }
                    }
                    else // numeric
                    {
                        gridEmbedded[j, i].Value = value;
                    }

                    startBit += numberOfBits;
                }
            }
        }

        #endregion

        #region Eventos de Interface

        private void numN_ValueChanged(object sender, EventArgs e)
        {
            gridEmbedded.Rows.Clear();
            gridEmbedded.Rows.Add((int)numN.Value);

            AppDataGridsHandling.LoadDefaultCellValues(ref gridEmbedded, gridEmbedded.RowCount);
        }

        private void gridEmbedded_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!editable)
            {
                return;
            }

            if (!AppDataGridsHandling.ValidateCellValue(ref gridEmbedded, e.FormattedValue))
            {
                e.Cancel = true;
            }
        }

        private void gridEmbedded_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (editable)
            {
                // Para o carregamento do subtipo em funcao do servico selecionado
                if (gridEmbedded.Columns[gridEmbedded.CurrentCell.ColumnIndex].HeaderText.ToUpper() == "SERVICE SUBTYPE")
                {
                    AppDataGridsHandling.LoadSubtypesInComboBoxCellGrid(gridEmbedded);
                }
            }
        }

        /**
         * Se o chamador for FrmTcsSending, compoem um trecho de pacote para ser incluido no
         * pacote principal.
         **/
        private void btOK_Click(object sender, EventArgs e)
        {
            edited = false;

            // forca que edicoes pendentes sejam finalizadas
            gridEmbedded.EndEdit();

            // Se o chamador for a visualizacao de logs, apenas fecha
            if (!editable)
            {
                this.Close();
                return;
            }

            bool pass = true;
            UInt16 numberOfBits = 0;
            int i, j;

            // Varre o grid em busca de nulos ou vazios e determina o tamanho do vetor
            foreach (DataGridViewColumn col in gridEmbedded.Columns)
            {
                foreach (DataGridViewRow row in gridEmbedded.Rows)
                {
                    if (gridEmbedded[col.Index, row.Index].Value == null)
                    {
                        pass = false;
                        break;
                    }

                    if (gridEmbedded[col.Index, row.Index].Value.ToString().Equals(""))
                    {
                        pass = false;
                        break;
                    }

                    if (gridEmbedded[col.Index, row.Index].Value.ToString().Contains("[There are no"))
                    {
                        pass = false;
                        break;
                    }

                    if (gridEmbedded[col.Index, row.Index].Value.ToString().Contains("[Choose a service"))
                    {
                        pass = false;
                        break;
                    }

                    if (col.ToolTipText.Contains("Raw Hex, variable size"))
                    {
                        i = gridEmbedded[col.Index, gridEmbedded.CurrentRow.Index].ToolTipText.IndexOf(",");
                        j = gridEmbedded[col.Index, gridEmbedded.CurrentRow.Index].ToolTipText.IndexOf("bits");

                        numberOfBits += UInt16.Parse(gridEmbedded[col.Index, gridEmbedded.CurrentRow.Index].ToolTipText.Substring(i + 2, (j - i - 3)));
                    }
                    else
                    {
                        i = col.ToolTipText.IndexOf(",");
                        j = col.ToolTipText.IndexOf("bits");
                        numberOfBits += UInt16.Parse(col.ToolTipText.Substring(i + 2, (j - i - 3)));
                    }
                }

                if (!pass) break;
            }

            if (!pass)
            {
                MessageBox.Show("There are empty fields. Correct them and try again.",
                                        "Empty Fields !",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                return;
            }

            // Passou pela validacao; agora converte os dados do grid para hexa 
            int startPosition = 0;

            if (numN.Visible)
            {
                numberOfBits += 24; // 8 bits para o parametro n, 16 para crc
                rawPacket.Resize((UInt16)(numberOfBits / 8));
                byte[] part = new byte[1];
                part[0] = (byte)numN.Value;
                rawPacket.SetPart(0, 8, part);
                startPosition = 8;
            }
            else
            {
                numberOfBits += 16; // 16 bits para o crc
                rawPacket.Resize((UInt16)(numberOfBits / 8));
            }

            numberOfBits = 0;

            foreach (DataGridViewRow row in gridEmbedded.Rows)
            {
                foreach (DataGridViewColumn col in gridEmbedded.Columns)
                {
                    int firstBit = 0;
                    int shift = 0;

                    if (col.ToolTipText.Contains("Raw Hex, variable size"))
                    {
                        i = gridEmbedded[col.Index, gridEmbedded.CurrentRow.Index].ToolTipText.IndexOf(",");
                        j = gridEmbedded[col.Index, gridEmbedded.CurrentRow.Index].ToolTipText.IndexOf("bits");

                        numberOfBits = UInt16.Parse(gridEmbedded[col.Index, gridEmbedded.CurrentRow.Index].ToolTipText.Substring(i + 2, (j - i - 3)));
                    }
                    else
                    {
                        i = col.ToolTipText.IndexOf(",");
                        j = col.ToolTipText.IndexOf("bits");
                        numberOfBits = UInt16.Parse(col.ToolTipText.Substring(i + 2, (j - i - 3)));
                    }

                    if (numberOfBits > 0)
                    {
                        Int64 fieldValue = 0;
                        byte[] fieldArray = null;

                        // Extrai o valor do campo
                        if (col.ToolTipText.Contains("Table"))
                        {
                            i = gridEmbedded[col.Index, row.Index].FormattedValue.ToString().IndexOf("[");
                            j = gridEmbedded[col.Index, row.Index].FormattedValue.ToString().IndexOf("]");
                            fieldValue = Int64.Parse(gridEmbedded[col.Index, row.Index].FormattedValue.ToString().Substring(i + 1, (j - i - 1)));
                        }
                        else if (col.ToolTipText.Contains("Numeric"))
                        {
                            fieldValue = Int64.Parse(gridEmbedded[col.Index, row.Index].FormattedValue.ToString());
                        }
                        else if (col.ToolTipText.Contains("Bool"))
                        {
                            // Se for false, mantem o valor original de fieldValue (zero)
                            if (bool.Parse(gridEmbedded[col.Index, row.Index].FormattedValue.ToString()))
                            {
                                fieldValue = 1;
                            }
                        }
                        else if ((col.ToolTipText.Contains("Raw Hex")))
                        {
                            if (!col.ToolTipText.Contains("Raw Hex, variable size"))
                            {
                                string strippedValue = gridEmbedded[col.Index, row.Index].FormattedValue.ToString().Replace("-", "");
                                fieldValue = Convert.ToInt64(strippedValue, 16);
                            }
                            else
                            {
                                string formatted = Formatting.FormatHexString(gridEmbedded[col.Index, row.Index].FormattedValue.ToString()).Replace("-", "");
                                fieldArray = Formatting.HexStringToByteArray(formatted);
                            }
                        }

                        // Valido para campos de ate 64 bits, pois o BitConverter redimensiona 
                        // part para 8 elementos (por receber um int64)
                        if (!col.ToolTipText.Contains("Raw Hex, variable size"))
                        {
                            if (numberOfBits > 64)
                            {
                                MessageBox.Show("Error: More than 64 bits in a field other than 'Raw Application data' or 'Raw Hex, variable size'!",
                                                "Invalid field size!",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                return;
                            }
                        }

                        foreach (DataGridViewColumn col2 in gridEmbedded.Columns)
                        {
                            // O loop chegou a coluna atual; extrai os dados necessarios e sai
                            if (col2.Index == col.Index)
                            {
                                break;
                            }
                            else
                            {
                                if (col2.ToolTipText.Contains("Raw Hex, variable size"))
                                {
                                    i = gridEmbedded[col2.Index, gridEmbedded.CurrentRow.Index].ToolTipText.IndexOf(",");
                                    j = gridEmbedded[col2.Index, gridEmbedded.CurrentRow.Index].ToolTipText.IndexOf("bits");

                                    firstBit += UInt16.Parse(gridEmbedded[col2.Index, gridEmbedded.CurrentRow.Index].ToolTipText.Substring(i + 2, (j - i - 3)));
                                }
                                else
                                {
                                    i = col2.ToolTipText.IndexOf(",");
                                    j = col2.ToolTipText.IndexOf("bits");
                                    firstBit += int.Parse(col2.ToolTipText.Substring(i + 2, (j - i - 3)));
                                }
                            }
                        }

                        shift = rawPacket.DefineLeftShift(firstBit, numberOfBits);

                        // Aplica o shift, se houver
                        if (shift > 0)
                        {
                            fieldValue = fieldValue << shift;
                        }

                        byte[] part = null;

                        if (fieldArray == null)
                        {
                            part = new byte[(int)Math.Ceiling(((double)numberOfBits) / 8)];

                            // ATENCAO: o bitconverter pode modificar o tamanho de part!
                            part = BitConverter.GetBytes(fieldValue);

                            // Para corrigir a posicao dos bytes retornados do bitconverter
                            Array.Reverse(part, 0, (int)Math.Ceiling(((double)numberOfBits) / 8));
                        }
                        else
                        {
                            part = fieldArray;
                        }

                        rawPacket.SetPart(startPosition, numberOfBits, part);

                        startPosition += numberOfBits;
                    }
                }
            }

            embeddedPacketAlreadyEdited = true;

            edited = true;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            edited = false;
            this.Close();
        }

        private void gridEmbedded_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (gridEmbedded.Columns[e.ColumnIndex].ToolTipText.Contains("Raw Hex, variable size"))
            {
                string formatted = Formatting.FormatHexString(gridEmbedded[e.ColumnIndex, e.RowIndex].FormattedValue.ToString()).Replace("-", "");
                byte[] fieldArray = Formatting.HexStringToByteArray(formatted);

                // se for um TC de load, atualiza o checksum
                if ((type == 6) && (subtype == 2) && (e.ColumnIndex == 3))
                {
                    UInt16 checkSum = CheckingCodes.IsoChecksum(fieldArray, fieldArray.Length);

                    // formata o hex
                    gridEmbedded[e.ColumnIndex, e.RowIndex].Value = BitConverter.ToString(fieldArray);
                    gridEmbedded[4, e.RowIndex].Value = Formatting.FormatHexString(checkSum.ToString("X"));

                    // coloca o tamanho do campo
                    gridEmbedded[2, e.RowIndex].Value = (fieldArray.Length / 4); // a unidade eh SAUs, nao bytes
                }
            }
        }

        #endregion



    }
}

/**
 * @file 	    AppDataGridsHandling.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    22/07/2009
 * @note	    Modificado em 02/08/2013 por Thiago.
 * @note	    Modificado em 26/05/2015 por Conrado.
 * @note	    Modificado em 28/05/2015 por Thiago.
 **/

using System;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Database;
using System.Data;
using Inpe.Subord.Comav.Egse.Smc.Forms;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;

/**
 * @Namespace Este namespace contem todas as classes que fornecem suporte a varias necessidades
 * do SMC, como manipulacao e exibicao de dados, formatacao de dados, dentre outras..
 */
namespace Inpe.Subord.Comav.Egse.Smc.Utils
{
    /**
     * @class GridsHandling
     * Classe com metodos estaticos utilitarios para o carregamento e 
     * edicao dos grids de Application Data dos formularios FrmTcsSending e 
     * FrmApplicationData.
     **/
    class AppDataGridsHandling
    {
        #region Construtor

        #endregion

        #region Metodos publicos estaticos

        /**
         * Carrega um grid passado por referencia a partir da configuracao 
         * lida do banco de dados para o tipo / subtipo.
         **/
        public static void LoadAppDataGrid(ref DataGridView grid, int serviceType, int serviceSubtype, bool editable)
        {
            string sql = "";
            bool showN = false;

            // Verifico se a configuracao do subtipo eh "copia" da configuracao de outro subtipo
            sql = @"select top 1 isnull(a.same_as_subtype, 0) as same_as_subtype, isnull(b.allow_repetition, 0) as allow_repetition
                    from subtype_structure a inner join subtypes b on a.service_type = b.service_type and a.service_subtype = b.service_subtype 
                    where a.service_type = " + serviceType.ToString() + " and a.service_subtype = " + serviceSubtype.ToString() +
                    " order by a.position";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table.Rows.Count > 0)
            {
                int differentSubtype = (int)table.Rows[0]["same_as_subtype"];

                if (differentSubtype > 0)
                {
                    serviceSubtype = differentSubtype;
                }

                showN = (bool)table.Rows[0]["allow_repetition"];
            }

            sql = @"select d.data_field_name, d.type_is_bool, d.type_is_numeric, d.type_is_raw_hex, d.type_is_list, d.type_is_table, 
                    d.table_name, d.list_id, d.number_of_bits, c.read_only, c.default_value, d.data_field_id, d.variable_length
                    from services a 
                    inner join subtypes b on a.service_type = b.service_type
                    inner join subtype_structure c on b.service_Type = c.service_type and b.service_subtype = c.service_subtype
                    inner join data_fields d on c.data_field_id = d.data_field_id
                    where d.data_field_id > 0 and a.service_type = " + serviceType.ToString() + " and b.service_subtype = " + serviceSubtype.ToString() +
                    " order by c.position";

            table = DbInterface.GetDataTable(sql);

            grid.Columns.Clear();
            grid.Rows.Clear();

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (editable)
                    {
                        if ((bool)row["type_is_bool"])
                        {
                            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                            col.HeaderText = row[0].ToString();
                            col.ToolTipText = "Bool, " + row["number_of_bits"].ToString() + " bits";

                            if ((bool)row["read_only"])
                            {
                                col.ReadOnly = true;
                            }

                            if (row["default_value"] != DBNull.Value)
                            {
                                col.Tag = (int)row["default_value"];
                            }

                            grid.Columns.Add(col);
                        }
                        else if ((bool)row["type_is_numeric"])
                        {
                            var col = new DataGridViewTextBoxColumn();
                            col.HeaderText = row[0].ToString();
                            col.ToolTipText = "Numeric, " + row["number_of_bits"].ToString() + " bits";

                            if ((bool)row["read_only"])
                            {
                                col.ReadOnly = true;
                            }

                            if (row["default_value"] != DBNull.Value)
                            {
                                col.Tag = (int)row["default_value"];
                            }

                            grid.Columns.Add(col);
                        }
                        else if ((bool)row["type_is_raw_hex"])
                        {
                            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                            col.HeaderText = row[0].ToString();

                            if ((bool)row["variable_length"])
                            {
                                col.ToolTipText = "Raw Hex, variable size";
                            }
                            else
                            {
                                col.ToolTipText = "Raw Hex, " + row["number_of_bits"].ToString() + " bits";

                                if (col.HeaderText.Equals("Time: Seconds"))
                                {
                                    col.Width = 150;
                                }
                            }

                            if ((bool)row["read_only"])
                            {
                                col.ReadOnly = true;
                            }

                            if (row["default_value"] != DBNull.Value)
                            {
                                col.Tag = (int)row["default_value"];
                            }

                            grid.Columns.Add(col);
                        }
                        else if ((bool)row["type_is_list"] || (bool)row["type_is_table"])
                        {
                            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
                            col.HeaderText = row[0].ToString();
                            col.Width += 50;

                            if ((bool)row["type_is_list"]) // carrega os dados de uma lista
                            {
                                sql = @"select '[' + dbo.f_zero(a.list_value, 
                                        (select len(convert(varchar(5), max(list_value))) from data_field_lists where list_id = a.list_id))
                                        + '] ' + a.list_text as list_item
                                        from data_field_lists a where a.list_id = " + row["list_id"].ToString();
                            }
                            else // carrega os dados de uma tabela
                            {
                                switch (row["table_name"].ToString().ToUpper())
                                {
                                    case "APIDS":
                                        sql = "select '[' + dbo.f_zero(apid, 4) + '] ' + application_name from apids order by apid";
                                        break;
                                    case "RIDS":
                                        sql = "select '[' + dbo.f_zero(rid, 4) + '] ' + description from rids order by rid";
                                        break;
                                    case "MEMORY_IDS":
                                        sql = "select '[' + dbo.f_zero(memory_id, 4) + '] ' + memory_unit_description from memory_ids order by memory_id";
                                        break;
                                    case "OUTPUT_LINE_IDS":
                                        sql = "select '[' + dbo.f_zero(output_line_id, 4) + '] ' + output_line_description from output_line_ids order by output_line_id";
                                        break;
                                    case "PACKET_STORE_IDS":
                                        sql = "select '[' + dbo.f_zero(store_id, 4) + '] ' + packet_store_name from packet_store_ids order by store_id";
                                        break;
                                    case "PARAMETERS":
                                        sql = "select '[' + dbo.f_zero(parameter_id, 5) + '] ' + parameter_description from parameters order by parameter_id";
                                        break;
                                    case "REPORT_DEFINITIONS":
                                        sql = "select '[' + dbo.f_zero(structure_id, 5) + '] ' + report_definition_description from report_definitions order by structure_id";
                                        break;
                                    case "SERVICES":
                                        sql = "select '[' + dbo.f_zero(service_type, 3) + '] ' + service_name from services order by service_type";
                                        break;
                                    case "SUBTYPES":
                                        // o select aqui filtra os subtipos que contem novos TC (11/4 e 19/1)
                                        sql = "select '[Choose a service type first]'";
                                        break;
                                    default:
                                        break;
                                }
                            }

                            DataTable table2 = DbInterface.GetDataTable(sql);

                            if (table2.Rows.Count > 0)
                            {
                                foreach (DataRow row2 in table2.Rows)
                                {
                                    col.Items.Add(row2[0]);
                                }
                            }
                            else
                            {
                                col.Items.Add("[There are no items available]");
                            }

                            col.ToolTipText = "Table/List, " + row["number_of_bits"].ToString() + " bits";

                            if ((bool)row["read_only"])
                            {
                                col.ReadOnly = true;
                            }

                            if (row["default_value"] != DBNull.Value)
                            {
                                col.Tag = (int)row["default_value"];
                            }

                            grid.Columns.Add(col);
                        }
                    }
                    else // not editable
                    {
                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                        col.HeaderText = row[0].ToString();

                        // Mantenho o data_field_id no Name da coluna (utilizado ao carregar o grid a partir do log)
                        col.Name = row["data_field_id"].ToString();

                        if ((bool)row["type_is_bool"])
                        {
                            col.ToolTipText = "Bool, ";
                        }
                        else if ((bool)row["type_is_numeric"])
                        {
                            col.ToolTipText = "Numeric, ";
                        }
                        else if ((bool)row["type_is_raw_hex"])
                        {
                            col.ToolTipText = "Raw Hex, ";
                        }
                        else if ((bool)row["type_is_list"] || (bool)row["type_is_table"])
                        {
                            col.ToolTipText = "Table/List, ";
                        }

                        col.ToolTipText += row["number_of_bits"].ToString() + " bits";

                        grid.Columns.Add(col);
                    }
                }

                grid.Rows.Add(1);
            }

            Application.EnableVisualStyles();

            // TODO: modificar esta verificacao apos mudar a estrutura da tabela de subtipos
            if (((serviceType == 11) && (serviceSubtype == 4)) ||
                ((serviceType == 19) && (serviceSubtype == 1)))
            {
                /*
                 * Coluna oculta para as info 
                 * inseridas no btn FrmAppData do TTTC criado abaixo
                 */
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn
                {
                    HeaderText = @"Raw Application data",
                    ToolTipText = @"Application data, 0 bits",
                    Visible = false
                };
                grid.Columns.Add(col);

                DataGridViewButtonColumn colButton = new DataGridViewButtonColumn
                {
                    HeaderText = @"Application data",
                    Text = "Application data",
                    UseColumnTextForButtonValue = true
                };
                grid.Columns.Add(colButton);
            }

            switch (grid.Name)
            {
                case "gridAppData":
                    ((FrmRequestsComposition)grid.Parent).numN.Visible = showN;
                    ((FrmRequestsComposition)grid.Parent).numN.Value = 1;
                    ((FrmRequestsComposition)grid.Parent).lblN.Visible = showN;

                    ((FrmRequestsComposition)grid.Parent).cmbServiceSubtype.Width = showN ? 250 : 340;
                    break;
                case "gridEmbedded":
                    ((FrmApplicationData)grid.Parent).numN.Visible = showN;
                    ((FrmApplicationData)grid.Parent).numN.Tag = showN;
                    ((FrmApplicationData)grid.Parent).numN.Value = 1;
                    ((FrmApplicationData)grid.Parent).lblN.Visible = showN;

                    ((FrmApplicationData)grid.Parent).lblCaption.Width = showN ? 522 : 588;
                    break;
            }
        }

        /**
         * Preenche o comboboxCell de subtypes dentro da celula no grid 
         * em funcao do service type selecionado na celula anterior.
         **/
        public static void LoadSubtypesInComboBoxCellGrid(DataGridView grid)
        {
            grid.EndEdit();

            // OBS: este if deve ficar aqui para nao ocorrer alguns erros internos de interface
            if (grid.CurrentCell == null)
            {
                return;
            }

            // Carregamento do subtipo em funcao do servico selecionado
            if (grid.Columns[grid.CurrentCell.ColumnIndex].HeaderText.ToUpper() == "SERVICE SUBTYPE")
            {
                int serviceType = 0;
                string sql = "";

                // Obtem o service selecionado
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (col.HeaderText.ToUpper() == "SERVICE TYPE")
                    {
                        // TODO Ayres: verificar se o valor da celula seguinte eh nulo; se nao for, guardar para uso posterior
                        // mas... ha um problema caso o usuario ha houvesse selecionado um subtipo de um outro servico, ver como tratar
                        // (talvez gravar na propriedade "tag" o valor anterior da celula (o ultimo service type selecionado naquela linha) e comparar)

                        if (grid.CurrentRow.Cells[col.Index].Value == null)
                        {
                            // ainda nao foi selecionado nenhum service
                            return;
                        }

                        string cellValue = grid.CurrentRow.Cells[col.Index].Value.ToString();
                        serviceType = int.Parse(cellValue.Substring(1, 3));
                        break;
                    }
                }

                // Carrega o combo com base no serviceType
                sql = @"select distinct '[' + dbo.f_zero(a.service_subtype, 3) + '] ' + description + ' [' + convert(varchar(10), 
                        case a.allow_repetition when 1 then 8 else 0 end + 
                        isnull((select sum(c.number_of_bits) from subtype_structure b 
                        inner join data_fields c on b.data_field_id = c.data_field_id 
                        where b.service_type = a.service_type and b.service_subtype = isnull(x.same_as_subtype, a.service_subtype)), 0))
                        + ' bits]', a.service_subtype
                    from subtypes a
                    left join subtype_structure x on 
                            a.service_type = x.service_type and 
                            a.service_subtype = x.service_subtype
                    where a.service_type = " + serviceType.ToString() +
                      @" and (not (a.service_type = 11 and a.service_subtype = 4)) and 
                         (not (a.service_type = 19 and a.service_subtype = 1)) order by a.service_subtype";

                DataTable table = DbInterface.GetDataTable(sql);

                // Eh preciso criar uma nova celula com um combo especifico, pois os itens do combo da linha 1
                // provavelmente nao serao os mesmos itens do combo da linha 2, e assim segue.
                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        combo.Items.Add(row[0]);
                    }
                }
                else
                {
                    combo.Items.Add("[There are no requests available for this service]");
                }

                grid[grid.CurrentCell.ColumnIndex, grid.CurrentRow.Index] = combo;
            }

            // TODO ayres: se o service type nao mudou, e havia um subtype anteriormente na celula, recuperar isso selecionando no combo
            // A rotina do thiago para carregar saved requests faz isso em algum ponto, consulta-lo
            // importante: nao esquecer de atualizar a propriedade "tag" da celula do service type, aqui, depois de tudo
            grid[grid.CurrentCell.ColumnIndex, grid.CurrentRow.Index].Selected = false;
        }

        /**
         * Valida os dados entrados em uma celula.
         **/
        public static bool ValidateCellValue(ref DataGridView grid, object value)
        {
            if (value == null)
            {
                return (true);
            }

            if (value.ToString().Equals(""))
            {
                return (true);
            }

            if (grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.Equals(""))
            {
                // eh a coluna do botao para o application data; nao ha o que validar
                return true;
            }

            // Extrai o numero de bits da coluna para verificar se o valor eh aceitavel
            int i, j, numberOfBits;

            if (grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.Contains("Raw Hex, variable size"))
            {
                i = grid[grid.CurrentCell.ColumnIndex, grid.CurrentCell.RowIndex].ToolTipText.IndexOf(",");
                j = grid[grid.CurrentCell.ColumnIndex, grid.CurrentCell.RowIndex].ToolTipText.IndexOf("bits");

                numberOfBits = int.Parse(grid[grid.CurrentCell.ColumnIndex, grid.CurrentCell.RowIndex].ToolTipText.Substring(i + 2, (j - i - 3)));
            }
            else
            {
                i = grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.IndexOf(",");
                j = grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.IndexOf("bits");

                numberOfBits = int.Parse(grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.Substring(i + 2, (j - i - 3)));
            }

            if (grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.Contains("Numeric"))
            {
                double parsedValue;

                if (!double.TryParse(value.ToString(), out parsedValue))
                {
                    MessageBox.Show("Invalid field value. A numeric value is expected.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    return false;
                }

                if (parsedValue > (Math.Pow(2, numberOfBits) - 1))
                {
                    MessageBox.Show("Invalid field value. The maximum value allowed for this field is " + (Math.Pow(2, numberOfBits) - 1).ToString() + ".",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    return false;
                }
            }
            else if (grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.Contains("Raw Hex"))
            {
                try
                {
                    // se for uma coluna de tamanho variavel, verifica se eh um hex valido e atualiza o tamanho no tooltip da celula
                    if (grid.Columns[grid.CurrentCell.ColumnIndex].ToolTipText.Contains("Raw Hex, variable size"))
                    {
                        string formatted = Formatting.FormatHexString(value.ToString()).Replace("-", "");
                        byte[] stringAsBytes = Formatting.HexStringToByteArray(formatted);

                        if (stringAsBytes == null)
                        {
                            // o valor nao eh um hex valido
                            MessageBox.Show("Invalid field value. An hex value is expected.",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                            return false;
                        }

                        // atualiza o tooltip
                        grid[grid.CurrentCell.ColumnIndex, grid.CurrentCell.RowIndex].ToolTipText = "Raw Hex, " + (stringAsBytes.Length * 8).ToString() + " bits";
                    }
                    else
                    {
                        //Int64 parsedValue = Convert.ToInt64(value.ToString().Replace("-", ""), 16);

                        if (!grid.Columns[grid.CurrentCell.ColumnIndex].HeaderText.Equals("Time: Seconds"))
                        {
                            Int64 parsedValue = Convert.ToInt64(value.ToString().Replace("-", ""), 16);
                        }
                        else
                        {
                            //string valueString = value.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "");
                            Int64 parsedValue = Convert.ToInt64(value.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", ""), 16);
                        }

                        // cada caractere na string hex representa 4 bits

                        if (grid.Columns.Count < 16)
                        {
                            int bitsInParsedValue = (value.ToString().Replace("-", "").Length * 4);

                            if (bitsInParsedValue > numberOfBits)
                            {
                                MessageBox.Show("Invalid field value. The maximum value allowed for this field is 0x" + ((Int64)(Math.Pow(2, numberOfBits) - 1)).ToString("X" + (numberOfBits / 4).ToString()) + ".",
                                                Application.ProductName,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                return false;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid field value. An hex value is expected.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            // Para zerar o subtipo se o tipo for modificado
            if (grid.Columns[grid.CurrentCell.ColumnIndex].HeaderText.ToUpper() == "SERVICE TYPE")
            {
                if (value.Equals(grid.CurrentCell.FormattedValue))
                {
                    return (true);
                }

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (col.HeaderText.ToUpper() == "SERVICE SUBTYPE")
                    {
                        grid.CurrentRow.Cells[col.Index].Value = null;
                        break;
                    }
                }
            }

            return true;
        }

        /**
         * Seleciona o value subtype da comboboxCell.
         **/
        public static void SelectSubtypeInComboBoxCellGrid(DataGridView grid, string serviceSubtype, int colIndex, int rowIndex)
        {
            string serviceType = "";

            if ((colIndex > 0))
            {
                if (grid.Columns[colIndex - 1].HeaderText.Contains("Service Type"))
                {
                    serviceType = grid.Rows[rowIndex].Cells[colIndex - 1].Value.ToString();
                    int first = serviceType.IndexOf("[");
                    int last = serviceType.IndexOf("]");

                    serviceType = serviceType.Substring((first + 1), (last - 1)).Trim();
                }
            }

            // fazer a consulta da string ja formatada
            string sql = @"select distinct '[' + dbo.f_zero(a.service_subtype, 3) + '] ' + description + ' [' + convert(varchar(10), 
                                                      case a.allow_repetition when 1 then 8 else 0 end + 
                                                      isnull((select sum(c.number_of_bits) from subtype_structure b 
                                                      inner join data_fields c on b.data_field_id = c.data_field_id 
                                                      where b.service_type = a.service_type and b.service_subtype = isnull(x.same_as_subtype, a.service_subtype)), 0))
                                                      + ' bits]', a.service_subtype
                                                  from subtypes a left join subtype_structure x on 
                                                          a.service_type = x.service_type and 
                                                          a.service_subtype = x.service_subtype
                                                  where a.service_type = " + serviceType + " and a.service_subtype = " + serviceSubtype + @"
                                                  and (not (a.service_type = 11 and a.service_subtype = 4)) and 
                                                       (not (a.service_type = 19 and a.service_subtype = 1)) order by a.service_subtype";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table.Rows.Count > 0)
            {
                string subtypeSelect = table.Rows[0][0].ToString();
                grid.Rows[rowIndex].Cells[colIndex].Value = subtypeSelect;
            }

            // Quando o usuario clica no gridAppData, o evento que preenche o combo Service Subtype
            // eh disparado. Se isso ocorrer quando a celula do tipo Service Subtype estiver 
            // com o foco, a selecao do item (valor) Service Subtype eh perdida.
            // Para que isso nao ocorra, eh necessario tirar o foco da celula do tipo Service Subtype.
            // Nesse caso, passo null para a propriedade CurrentCell.
            grid.CurrentCell = null;
        }

        /**
         * Carrega os valores default de cada coluna nas celulas. Os valores default 
         * foram lidos do banco de dados em LoadGrid() e escritos na propriedade tag
         * das colunas.
         * O carregamento so ocorre nas linhas adicionadas.
         **/
        public static void LoadDefaultCellValues(ref DataGridView grid, int numberOfRowsAdded)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                // so carrega se for uma linha nova
                if (row.Index >= (grid.RowCount - numberOfRowsAdded))
                {
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        if (col.Tag != null)
                        {
                            // Se for do tipo Table/List, eh um ComboBoxCell.
                            if (col.ToolTipText.ToUpper().Contains("TABLE/LIST"))
                            {
                                DataGridViewComboBoxColumn cell = (DataGridViewComboBoxColumn)grid.Columns[col.Index];

                                if (col.HeaderText.Contains("Service Subtype"))
                                {
                                    grid.CurrentCell = (DataGridViewCell)grid.Rows[row.Index].Cells[col.Index];
                                    LoadSubtypesInComboBoxCellGrid(grid);
                                    SelectSubtypeInComboBoxCellGrid(grid, col.Tag.ToString(), col.Index, row.Index);
                                }
                                else
                                {
                                    // Percorro os items do ComboBox ate encontrar o default value.
                                    for (int i = 0; i < cell.Items.Count; i++)
                                    {
                                        int first = cell.Items[i].ToString().IndexOf("[");
                                        int last = cell.Items[i].ToString().IndexOf("]");

                                        string codeItem = cell.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                        int code = 0;

                                        if (int.TryParse(codeItem, out code) && (code == int.Parse(col.Tag.ToString())))
                                        {
                                            grid.Rows[row.Index].Cells[col.Index].Value = cell.Items[i].ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (col.ToolTipText.ToUpper().Contains("RAW HEX"))
                            {
                                if (grid.Columns.Count >= 16 && col.HeaderText.Equals("Time: Seconds"))
                                {
                                    grid[col.Index, row.Index].Value = (int)col.Tag;
                                }
                                else
                                {
                                    grid[col.Index, row.Index].Value = Formatting.FormatHexString(((int)col.Tag).ToString("X"));
                                }

                                if (col.ToolTipText.Contains("Raw Hex, variable size"))
                                {
                                    // atribui o tamanho no tooltip da celula
                                    string formatted = Formatting.FormatHexString(grid[col.Index, row.Index].FormattedValue.ToString()).Replace("-", "");
                                    byte[] fieldArray = Formatting.HexStringToByteArray(formatted);
                                    grid[col.Index, row.Index].ToolTipText = "Raw Hex, " + (fieldArray.Length / 8).ToString() + " bits";
                                }
                            }
                            else
                            {
                                grid[col.Index, row.Index].Value = col.Tag;
                            }
                        }
                        else if (col.ToolTipText.Contains("Raw Hex, variable size"))
                        {
                            // atribui o tamanho no tooltip da celula
                            grid[col.Index, row.Index].ToolTipText = "Raw Hex, 0 bits";
                        }

                        if (col.Index == 14)
                        {
                            // usar esta flag para informar se o pacote embarcado no 11.4 ja foi editado ou nao.
                            grid.Rows[row.Index].Cells[col.Index].Tag = "0";
                        }
                    }
                }
            }
        }


        /**
        * Preenche o determinado grid com a responta de uma telemetria recebida
        **/
        public static void FillGridWithPacketDataField(RawPacket rawPacket, ref DataGridView gridView)
        {
            int ServiceType = (int)rawPacket.GetPart(56, 8);
            int ServiceSubtype = (int)rawPacket.GetPart(64, 8);

            string sql;

            sql = @"Select
                        top 1 isnull(same_as_subtype, 0) 
                        from subtype_structure where service_type = " + ServiceType +
            " and service_subtype = " + ServiceSubtype;

            int sameAsSubtype = Convert.ToInt32(DbInterface.ExecuteScalar(sql));

            string sqlNumberFiedls;
            int numberOfFields;

            if (sameAsSubtype > 0)
            {
                sql = @"select 
                        b.data_field_name, 
                        b.number_of_bits, 
                        case 
                            when (b.type_is_bool = 1) then 'Bool' 
                            when (b.type_is_numeric = 1) then 'Numeric' 
                            when (b.type_is_raw_hex = 1) then 'Hex'
                            when (b.type_is_list = 1) then 'List'
                            else 'Table'
                        end as data_field_type,
                        b.list_id,
                        b.table_name
                    from 
                        subtype_structure a 
                        inner join data_fields b on 
                                a.data_field_id = b.data_field_id
                    where 
                        a.service_type = " + ServiceType + " and " +
                        "a.service_subtype = " + sameAsSubtype +
                    "order by a.position";

                // number of fiedls
                sqlNumberFiedls = @"select 
	                        count(*)
                        from 
	                        subtype_structure
                        where 
	                        service_type = " + ServiceType + " and " +
                            "service_subtype = " + sameAsSubtype;

                numberOfFields = (int)DbInterface.ExecuteScalar(sqlNumberFiedls);
            }
            else
            {
                sql = @"select 
                            b.data_field_name, 
                            b.number_of_bits, 
                            case 
                                when (b.type_is_bool = 1) then 'Bool' 
                                when (b.type_is_numeric = 1) then 'Numeric' 
                                when (b.type_is_raw_hex = 1) then 'Hex'
                                when (b.type_is_list = 1) then 'List'
                                else 'Table'
                            end as data_field_type,
                            b.list_id,
                            b.table_name
                        from 
                            subtype_structure a 
                            inner join data_fields b on 
                                    a.data_field_id = b.data_field_id
                        where 
                            a.service_type = " + ServiceType + " and " +
                            "a.service_subtype = " + ServiceSubtype +
                        "order by a.position";

                // number of fiedls
                sqlNumberFiedls = @"select 
	                        count(*)
                        from 
	                        subtype_structure
                        where 
	                        service_type = " + ServiceType + " and " +
                            "service_subtype = " + ServiceSubtype;

                numberOfFields = (int)DbInterface.ExecuteScalar(sqlNumberFiedls);
            }

            DataTable dataTable = DbInterface.GetDataTable(sql);

            // Array bidimensional para armazenar a estrutura do pacote
            string[,] packetStructure = new string[numberOfFields, 5];

            // Numero de index/linhas da tabela
            int index = 0;

            // Alimenta a matriz com os campos data_field_name, number_of_bits, list_id e table_name
            foreach (DataRow rows in dataTable.Rows)
            {
                packetStructure[index, 0] = rows["data_field_name"].ToString();
                packetStructure[index, 1] = rows["number_of_bits"].ToString();
                packetStructure[index, 2] = rows["data_field_type"].ToString();
                packetStructure[index, 3] = rows["list_id"].ToString();
                packetStructure[index, 4] = rows["table_name"].ToString();

                index++;
            }

            dataTable.Dispose();

            // Numero de colunas na tabela
            int numberColumns = index;

            // Posicao da time-tag
            int startBit = 80;

            // Obtem o time-tag do argumento RawPacket enviado pelo evento
            ulong timeTag = rawPacket.GetPart(startBit, 48);
            Byte[] arrayByte = new Byte[8];
            arrayByte = BitConverter.GetBytes(timeTag);
            Array.Reverse(arrayByte, 0, arrayByte.Length);

            startBit += 48;

            // Converte o time-tag para calendario
            //string calendar = TimeCode.OnboardTimeToCalendar(timeTag.ToString("X8"));
            string calendar = TimeCode.OnboardTimeToCalendar(Utils.Formatting.ConvertByteArrayToHexString(arrayByte, arrayByte.Length).Substring(4, 12));

            int numberOfEvents = (int)rawPacket.GetPart(startBit, 8);
            startBit += 8;

            int getPartValue;
            int serviceType = 0;

            // Iteracao para preenchimento do grid "gridEventDetection"
            // utilizando a matriz criada anteriormente
            for (int i = 0; i < numberOfEvents; i++) // grid-linha
            {
                // gridEventDetection.Rows.Add(); // adiciona linha
                gridView.Rows.Add();

                for (int j = 0; j < numberColumns; j++) // matriz-linha / grid-coluna
                {
                    if (i == 0)
                    {
                        // Define o cabecalho de cada coluna
                        gridView.Columns[j].HeaderText = packetStructure[j, 0];
                    }

                    // Armazena o GetPart de cada iteracao
                    getPartValue = (int)rawPacket.GetPart(startBit, Int32.Parse(packetStructure[j, 1]));

                    if (packetStructure[j, 2] == "Table") // se data_field_type for "Table"
                    {
                        if (packetStructure[j, 4] == "services") // services
                        {
                            sql = @"select isnull((
                                            select '[' + dbo.f_zero(service_type, 4) + '] ' + service_name 
                                            from services
                                            where service_type = " + getPartValue + "), " +
                                 "'[' + dbo.f_zero(" + getPartValue + ", 4) + ']' + ' ** INVALID VALUE! **')";

                            serviceType = getPartValue;
                        }
                        else if (packetStructure[j, 4] == "subtypes") // subtypes
                        {
                            sql = @"select isnull((
                                            select '[' + dbo.f_zero(service_subtype, 4) + '] ' + description 
                                            from subtypes
                                            where service_subtype = " + getPartValue + " and service_type = " + serviceType + ")," +
                                   "'[' + dbo.f_zero(" + serviceType + ", 4) + ']' + ' ** INVALID VALUE! **')";
                        }
                        else if (packetStructure[j, 4] == "packet_store_ids") // packet_store_ids
                        {

                            sql = @"select isnull((
                                         select '[' + dbo.f_zero(" + getPartValue + ", 4) + '] ' + packet_store_name " +
                                        "from packet_store_ids " +
                                        "where store_id = " + getPartValue + "), " +
                                   "'[' + dbo.f_zero(" + serviceType + ", 4) + ']' + ' ** INVALID VALUE! **')";
                        }
                        else if (packetStructure[j, 4] == "report_definitions") // report_definitions
                        {
                            sql = @"select isnull((
                                         select '[' + dbo.f_zero(structure_id, 4) + '] ' + report_definition_description
                                         from report_definitions 
                                         where structure_id = " + getPartValue + "), " +
                                   "'[' + dbo.f_zero(" + getPartValue + ", 4) + ']' + ' ** INVALID VALUE! **')";
                        }
                        else if (packetStructure[j, 4] == "rids") // report_definitions
                        {
                            sql = @"select isnull((
                                            select '[' + dbo.f_zero(rid, 4) + '] ' + description 
                                            from rids
                                            where rid = " + getPartValue + ")," +
                                        "'[' + dbo.f_zero(" + getPartValue + ", 4) + ']' + ' ** INVALID VALUE! **')";
                        }

                        gridView.Rows[i].Cells[j].Value = DbInterface.ExecuteScalar(sql);
                    }
                    else if (packetStructure[j, 2] == "List") // se data_field_type for "List"
                    {
                        sql = @"select list_text 
                                    from data_field_lists
                                    where list_id = " + packetStructure[j, 3] + " and list_value = " + getPartValue;

                        gridView.Rows[i].Cells[j].Value = DbInterface.ExecuteScalar(sql);
                    }
                    else if (packetStructure[j, 2] == "Numeric") // se data_field_type for "Numeric"
                    {
                        gridView.Rows[i].Cells[j].Value = getPartValue.ToString();
                    }
                    else if (packetStructure[j, 2] == "Hex") // se data_field_type for "Numeric"
                    {
                        gridView.Rows[i].Cells[j].Value = getPartValue.ToString("X8");
                    }
                    else if (packetStructure[j, 2] == "Bool")
                    {
                        gridView.Rows[i].Cells[j].Value = getPartValue;
                    }

                    startBit += Int32.Parse(packetStructure[j, 1]); // soma os bits da matriz 
                }
            }
        }

        #endregion
    }
}

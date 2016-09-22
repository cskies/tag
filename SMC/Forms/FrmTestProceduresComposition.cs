/**
 * @file 	    FrmTestProceduresComposition.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Thiago Duarte Pereira
 * @date	    13/03/2010
 * @note	    Modificado em 19/09/2013 por Bruna.
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Inpe.Subord.Comav.Egse.Smc.Database;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using System.Data.OleDb;

/**
 * @Namespace Namespace com todos os Formularios do SMC.
 */
namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmTestProceduresComposition
     * Esta classe eh a responsavel pela composicao dos Procedimentos de Teste
     * que serao executados de forma automatica.
     **/
    public partial class FrmTestProceduresComposition : DockContent
    {
        #region Atributos

        private DbTestProcedure dbTestProcedure = new DbTestProcedure();
        private enum Mode { browsing, inserting, editing };
        private Mode currentMode = Mode.browsing;

        // Para manter o controle da linha selecionada no gridDatabase
        private int procedureSelected = 0;

        // Este atributo eh usado para verificar se o item de uma comboBoxCellGrid foi alterado.
        // Se nao for alterado, nao faz nada.
        private String valueSelectedInGrid = "";
        UInt32 originalEstimatedDuration = 0;
        private MdiMain mdiMain = null;
        private FrmConnectionWithEgse frmConnection = null;
        private FrmSessionsLog frmSessionsLog = null;
        
        #endregion

        #region

        public FrmConnectionWithEgse FormConnectionWithEgse
        {
            get
            {
                return frmConnection;
            }
            set
            {
                frmConnection = value;
            }
        }

        #endregion

        #region Construtor

        public FrmTestProceduresComposition(MdiMain mdi)
        {
            InitializeComponent();

            mdiMain = mdi;
            frmConnection = mdiMain.FormConnectionWithEgse;
            ChangeMode(Mode.browsing);
        }

        #endregion

        #region Eventos da Interface

        private void FrmTestProceduresComposition_Load(object sender, EventArgs e)
        {
            chkRunInLoop.Checked = false;
            numLoopIterations.Enabled = false;

            btRemove.Enabled = false;
            btUp.Enabled = false;
            btDown.Enabled = false;

            DataGridViewComboBoxColumn col1 = new DataGridViewComboBoxColumn();
            col1.Width = 120;
            col1.Items.Insert(0, "Request");
            col1.Items.Insert(1, "Procedure");
            col1.HeaderText = "Step Type";

            DataGridViewComboBoxColumn col2 = new DataGridViewComboBoxColumn();
            col2.Width = 250;
            col2.Items.Insert(0, "[Choose a step type first]");
            col2.HeaderText = "Step";

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Width = 120;
            col3.HeaderText = "Delay Before Sending (Secs.)";

            DataGridViewCheckBoxColumn col4 = new DataGridViewCheckBoxColumn();
            col4.Width = 70;
            col4.HeaderText = "Verify Execution?";

            DataGridViewComboBoxColumn col5 = new DataGridViewComboBoxColumn();
            col5.Width = 200;
            col5.Items.Insert(0, "Receive acceptance ack");
            col5.Items.Insert(1, "Receive execution ack");
            col5.Items.Insert(2, "Receive acceptance nack");
            col5.Items.Insert(3, "Receive execution nack");
            col5.Items.Insert(4, "Receive a specific report");
            col5.Items.Insert(5, "Dont receive a specific report");
            col5.HeaderText = "Verify Condition";

            DataGridViewComboBoxColumn col6 = new DataGridViewComboBoxColumn();
            col6.Width = 300;
            col6.HeaderText = "Report Type";

            DataGridViewComboBoxColumn col7 = new DataGridViewComboBoxColumn();
            col7.Width = 300;
            col7.Items.Insert(0, "[Choose a report type first]");
            col7.HeaderText = "Report Subtype";

            DataGridViewCheckBoxColumn col8 = new DataGridViewCheckBoxColumn();
            col8.Width = 70;
            col8.HeaderText = "Check Data Field?";

            DataGridViewComboBoxColumn col9 = new DataGridViewComboBoxColumn();
            col9.Width = 220;
            col9.HeaderText = "Data Field";

            DataGridViewComboBoxColumn col10 = new DataGridViewComboBoxColumn();
            col10.Width = 160;
            col10.Items.Insert(0, "equal to");
            col10.Items.Insert(1, "major than");
            col10.Items.Insert(2, "minor than");
            col10.Items.Insert(3, "major than or equal to");
            col10.Items.Insert(4, "minor than or equal to");
            col10.Items.Insert(5, "different than");
            col10.HeaderText = "Comparison Operation";

            DataGridViewTextBoxColumn col11 = new DataGridViewTextBoxColumn();
            col11.Width = 250;
            col11.HeaderText = "Compare To";

            DataGridViewTextBoxColumn col12 = new DataGridViewTextBoxColumn();
            col12.Width = 70;
            col12.ReadOnly = true;
            col12.HeaderText = "";

            DataGridViewTextBoxColumn col13 = new DataGridViewTextBoxColumn();
            col13.Width = 70;
            col13.HeaderText = "";

            DataGridViewTextBoxColumn col14 = new DataGridViewTextBoxColumn();
            col14.Width = 70;
            col14.ReadOnly = true;
            col14.HeaderText = "";

            DataGridViewTextBoxColumn col15 = new DataGridViewTextBoxColumn();
            col15.Width = 70;
            col15.HeaderText = "";

            DataGridViewTextBoxColumn col16 = new DataGridViewTextBoxColumn();
            col16.Width = 70;
            col16.ReadOnly = true;
            col16.HeaderText = "";

            gridProcedureSteps.Columns.Add(col1);
            gridProcedureSteps.Columns.Add(col2);
            gridProcedureSteps.Columns.Add(col3);
            gridProcedureSteps.Columns.Add(col4);
            gridProcedureSteps.Columns.Add(col5);
            gridProcedureSteps.Columns.Add(col6);
            gridProcedureSteps.Columns.Add(col7);
            gridProcedureSteps.Columns.Add(col8);
            gridProcedureSteps.Columns.Add(col9);
            gridProcedureSteps.Columns.Add(col10);
            gridProcedureSteps.Columns.Add(col11);
            gridProcedureSteps.Columns.Add(col12);
            gridProcedureSteps.Columns.Add(col13);
            gridProcedureSteps.Columns.Add(col14);
            gridProcedureSteps.Columns.Add(col15);
            gridProcedureSteps.Columns.Add(col16);

            // O trecho abaixo configura o layout das colunas no grid.
            // Contamos com o auxilio do conteudo do link:
            // http://social.msdn.microsoft.com/Forums/en-US/winformsdatacontrols/thread/87004d70-482a-4b86-ba18-371670254b6a
            gridProcedureSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            gridProcedureSteps.ColumnHeadersHeight = (gridProcedureSteps.ColumnHeadersHeight * 2) + (gridProcedureSteps.ColumnHeadersHeight - 5);
            gridProcedureSteps.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            gridProcedureSteps.CellPainting += new DataGridViewCellPaintingEventHandler(gridProcedureSteps_CellPainting_1);
            gridProcedureSteps.Paint += new PaintEventHandler(gridProcedureSteps_Paint_1);
            gridProcedureSteps.Scroll += new ScrollEventHandler(gridProcedureSteps_Scroll_1);
            gridProcedureSteps.ColumnWidthChanged += new DataGridViewColumnEventHandler(gridProcedureSteps_ColumnWidthChanged_1);

            col1.Visible = false;
        }

        private void FrmTestProceduresComposition_DockStateChanged(object sender, EventArgs e)
        {
            if (DockState == DockState.Float)
            {
                FloatPane.FloatWindow.ClientSize = new Size(736, 520);
            }
        }

        private void chkRunProcedure_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkRunInLoop.Checked)
            {
                numLoopIterations.Enabled = true;
            }
            else
            {
                numLoopIterations.Enabled = false;
            }

            RefreshEstimatedDuration();
        }

        private void gridProcedureSteps_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;

                e.PaintBackground(r2, true);

                e.PaintContent(r2);
                e.Handled = true;
            }
        }

        private void gridProcedureSteps_Paint_1(object sender, PaintEventArgs e)
        {
            //PRIMEIRA COLUNA
            Rectangle r1 = gridProcedureSteps.GetCellDisplayRectangle(4, -1, true);
            int sizeC1 = gridProcedureSteps.GetCellDisplayRectangle(5, -1, true).Width;
            int sizeC2 = gridProcedureSteps.GetCellDisplayRectangle(6, -1, true).Width;
            int sizeC3 = gridProcedureSteps.GetCellDisplayRectangle(7, -1, true).Width;
            int sizeC4 = gridProcedureSteps.GetCellDisplayRectangle(8, -1, true).Width;
            int sizeC5 = gridProcedureSteps.GetCellDisplayRectangle(9, -1, true).Width;
            int sizeC6 = gridProcedureSteps.GetCellDisplayRectangle(10, -1, true).Width;
            int sizeC7 = gridProcedureSteps.GetCellDisplayRectangle(11, -1, true).Width;
            int sizeC8 = gridProcedureSteps.GetCellDisplayRectangle(12, -1, true).Width;
            int sizeC9 = gridProcedureSteps.GetCellDisplayRectangle(13, -1, true).Width;
            int sizeC10 = gridProcedureSteps.GetCellDisplayRectangle(14, -1, true).Width;
            int sizeC11 = gridProcedureSteps.GetCellDisplayRectangle(15, -1, true).Width;

            r1.X += 1;
            r1.Y += 1;
            r1.Width = r1.Width + (sizeC1 + sizeC2 + sizeC3 + sizeC4 + sizeC5 + sizeC6 + sizeC7 + sizeC8 + sizeC9 + sizeC10 + sizeC11) - 2;
            r1.Height = 32;
            e.Graphics.FillRectangle(new SolidBrush(gridProcedureSteps.ColumnHeadersDefaultCellStyle.BackColor), r1);
            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;
            format1.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("Execution Verification Condition",
                                  gridProcedureSteps.ColumnHeadersDefaultCellStyle.Font,
                                  new SolidBrush(gridProcedureSteps.ColumnHeadersDefaultCellStyle.ForeColor),
                                  r1,
                                  format1);

            //SEGUNDA COLUNA
            Rectangle r2 = gridProcedureSteps.GetCellDisplayRectangle(11, -1, true);
            int sizeC12 = gridProcedureSteps.GetCellDisplayRectangle(12, -1, true).Width;
            int sizeC13 = gridProcedureSteps.GetCellDisplayRectangle(13, -1, true).Width;
            int sizeC14 = gridProcedureSteps.GetCellDisplayRectangle(14, -1, true).Width;
            int sizeC15 = gridProcedureSteps.GetCellDisplayRectangle(15, -1, true).Width;

            r2.X += 1;
            r2.Y += 1;
            r2.Width = r2.Width + (sizeC12 + sizeC13 + sizeC14 + sizeC15) - 3;
            r2.Height = 32;

            e.Graphics.FillRectangle(new SolidBrush(gridProcedureSteps.ColumnHeadersDefaultCellStyle.BackColor), r2);
            StringFormat format2 = new StringFormat();
            format2.Alignment = StringAlignment.Center;
            format2.LineAlignment = StringAlignment.Far;
            e.Graphics.DrawString("Verify Interval",
                                  gridProcedureSteps.ColumnHeadersDefaultCellStyle.Font,
                                  new SolidBrush(gridProcedureSteps.ColumnHeadersDefaultCellStyle.ForeColor),
                                  r2,
                                  format2);
        }

        private void gridProcedureSteps_Scroll_1(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = gridProcedureSteps.DisplayRectangle;
            rtHeader.Height = gridProcedureSteps.ColumnHeadersHeight / 2;
            gridProcedureSteps.Invalidate(rtHeader);
        }

        private void gridProcedureSteps_ColumnWidthChanged_1(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle rtHeader = gridProcedureSteps.DisplayRectangle;
            rtHeader.Height = gridProcedureSteps.ColumnHeadersHeight / 2;
            gridProcedureSteps.Invalidate(rtHeader);
        }

        private void gridProcedureSteps_CellValidating_1(object sender, DataGridViewCellValidatingEventArgs e)
        {
            object value = e.FormattedValue;

            if ((value == null ||
                value.ToString().Trim().Contains("[") ||
                value.ToString().Trim().Equals("") ||
                value.ToString().Trim().Contains("[Choose") ||
                value.ToString().Trim().Contains("[There are no") ||
                gridProcedureSteps[e.ColumnIndex, e.RowIndex].ValueType.Name.Equals("Boolean")) &&
                (e.ColumnIndex != 14))
            {
                if ((e.ColumnIndex == 2) ||
                    (e.ColumnIndex == 12) ||
                    (e.ColumnIndex == 14))
                {
                    if (!Utils.Formatting.IsNumeric(value.ToString().Trim()))
                    {
                        MessageBox.Show("Invalid field value. A numeric value is expected.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        e.Cancel = true;
                    }
                    else
                    {
                        RefreshEstimatedDuration();
                    }
                }

                return;
            }

            if ((e.ColumnIndex == 14) && (value.ToString().Trim().Equals("")))
            {
                value = "0";
                gridProcedureSteps[14, e.RowIndex].Value = "0";
            }

            String columnName = gridProcedureSteps.Columns[e.ColumnIndex].HeaderText.ToUpper();

            if ((columnName.Contains("COMPARE TO")))
            {
                if (gridProcedureSteps[8, e.RowIndex].ToolTipText.Equals("Raw Hex"))
                {
                    // Verificar se eh Hexadecimal de no maximo 64 bits!!!!!                    
                    String valueFormatted = value.ToString().Replace("-", "").Trim();
                    int bitsInParsedValue = (valueFormatted.Length * 4);

                    if (bitsInParsedValue > 64)
                    {
                        MessageBox.Show("Invalid field value. The maximum value allowed for this field is 0xffffffffffffffff.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        valueFormatted = Utils.Formatting.FormatHexString(valueFormatted).Replace("-", "");

                        if (Utils.Formatting.HexStringToByteArray(valueFormatted) == null)
                        {
                            MessageBox.Show("Invalid field value. A hexadecimal value is expected.",
                                            Application.ProductName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else if (gridProcedureSteps[8, e.RowIndex].ToolTipText.Equals("Numeric"))
                {
                    // Verificar se eh Numeric de no maximo 64 bits!!!!!
                    UInt64 parsedValue = 0;

                    if (!Utils.Formatting.IsNumeric(value.ToString().Trim()))
                    {
                        MessageBox.Show("Invalid field value. A numeric value is expected.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        e.Cancel = true;
                        return;
                    }

                    if (!UInt64.TryParse(value.ToString().Trim(), out parsedValue))
                    {
                        MessageBox.Show("Invalid field value. The maximum value allowed for this field is 18446744073709551615.",
                                        Application.ProductName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);

                        e.Cancel = true;
                        return;
                    }
                }
            }
            else if ((e.ColumnIndex == 12) || // celula sucessora a celula com valor "Between"
                     (e.ColumnIndex == 14) || // celula sucessora a celula com valor "And"                    
                     (columnName.Contains("DELAY BEFORE SENDING")))
            {
                // Verificar se eh numerico de no maximo 32 bits!!!!!
                UInt32 parsedValue = 0;

                if (!Utils.Formatting.IsNumeric(value.ToString().Trim()))
                {
                    MessageBox.Show("Invalid field value. A numeric value is expected.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    e.Cancel = true;
                    return;
                }

                if (!UInt32.TryParse(value.ToString().Trim(), out parsedValue))
                {
                    MessageBox.Show("Invalid field value. The maximum value allowed for this field is 4294967295.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    e.Cancel = true;
                    return;
                }

                // Dentro deste if verifico a consistencia dos valores da coluna Verify Interval.
                // O primeiro valor nao pode ser maior que o segundo, porque o calculo eh sempre o segundo menos o primeiro.
                if (!columnName.Contains("DELAY BEFORE SENDING"))
                {
                    UInt32 firstValue = 0;
                    UInt32 secondValue = 0;

                    if (e.ColumnIndex == 12)
                    {
                        firstValue = Convert.ToUInt32(value);
                        secondValue = UInt32.Parse(gridProcedureSteps[14, e.RowIndex].Value.ToString());
                    }
                    else if (e.ColumnIndex == 14)
                    {
                        firstValue = UInt32.Parse(gridProcedureSteps[12, e.RowIndex].Value.ToString());
                        secondValue = Convert.ToUInt32(value);

                        if (firstValue > secondValue)
                        {
                            MessageBox.Show("The verify interval of step " + (e.RowIndex + 1) + " is invalid!\n\n Correct it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            e.Cancel = true;
                            return;
                        }
                    }

                    // Condicao: Se firstValue e secondValue forem diferentes de 0, firstValue deve ser menor.
                    if ((firstValue != 0) && (secondValue != 0))
                    {
                        if (firstValue > secondValue)
                        {
                            MessageBox.Show("The verify interval of step " + (e.RowIndex + 1) + " is invalid!\n\n Correct it and try again.",
                                            "Inconsistent Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }

            RefreshEstimatedDuration();
        }

        private void gridProcedureSteps_CellValidated_1(object sender, DataGridViewCellEventArgs e)
        {
            object value = gridProcedureSteps.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (value == null ||
                value.ToString().Trim().Equals("") ||
                value.ToString().Trim().Equals("[") ||
                value.ToString().Contains("[Choose") ||
                value.ToString().Contains("[There are no"))
            {
                if ((e.ColumnIndex == 2) ||
                    (e.ColumnIndex == 12) ||
                    (e.ColumnIndex == 14))
                {
                    gridProcedureSteps[e.ColumnIndex, e.RowIndex].Value = "0";
                    RefreshEstimatedDuration();
                }

                return;
            }

            String columnName = gridProcedureSteps.Columns[e.ColumnIndex].HeaderText.ToUpper();
            DataTable table = null;
            String sql = "";

            if (columnName.Contains("STEP TYPE"))
            {
                if (value.Equals(valueSelectedInGrid))
                {
                    valueSelectedInGrid = "";
                    return;
                }
                
                sql = @"select '[' + dbo.f_zero(saved_request_id, 4) + '] ' + description 
                       from saved_requests order by saved_request_id";

                if (!sql.Equals(""))
                {
                    table = DbInterface.GetDataTable(sql);
                }

                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        combo.Items.Add(row[0]);
                    }
                }
                else
                {
                    combo.Items.Add("[There are no records for this step type]");
                }

                gridProcedureSteps[1, gridProcedureSteps.CurrentRow.Index] = combo;
            }
            else if (columnName.Contains("STEP"))
            {
                // Habilitar a coluna "VERIFY EXECUTION"
                gridProcedureSteps[3, e.RowIndex].ReadOnly = false;
            }
            else if (columnName.Contains("VERIFY EXECUTION"))
            {
                bool isChecked = (bool)gridProcedureSteps.Rows[e.RowIndex].Cells[3].Value;

                if (isChecked)
                {
                    gridProcedureSteps[4, e.RowIndex].ReadOnly = false;
                    gridProcedureSteps[12, e.RowIndex].ReadOnly = false;
                    gridProcedureSteps[14, e.RowIndex].ReadOnly = false;
                }
                else
                {
                    gridProcedureSteps[4, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[4, e.RowIndex].Value = "";

                    gridProcedureSteps[5, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[5, e.RowIndex].Value = "";

                    gridProcedureSteps[6, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[6, e.RowIndex].Value = "";

                    gridProcedureSteps[7, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[7, e.RowIndex].Value = false;

                    gridProcedureSteps[8, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[8, e.RowIndex].Value = "";

                    gridProcedureSteps[9, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[9, e.RowIndex].Value = "";

                    gridProcedureSteps[10, e.RowIndex] = new DataGridViewTextBoxCell();
                    gridProcedureSteps[10, e.RowIndex].ReadOnly = true;

                    gridProcedureSteps[12, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[12, e.RowIndex].Value = "0";

                    gridProcedureSteps[14, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[14, e.RowIndex].Value = "0";

                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "";
                }
            }
            else if (columnName.Contains("VERIFY CONDITION"))
            {
                String condition = gridProcedureSteps[4, e.RowIndex].Value.ToString().ToUpper();

                if ((condition.Contains("ACK")) || (condition.Contains("NACK")))
                {
                    gridProcedureSteps[5, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[5, e.RowIndex].Value = "";

                    gridProcedureSteps[6, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[6, e.RowIndex].Value = "";

                    gridProcedureSteps[7, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[7, e.RowIndex].Value = false;

                    gridProcedureSteps[8, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[8, e.RowIndex].Value = "";

                    gridProcedureSteps[9, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[9, e.RowIndex].Value = "";

                    gridProcedureSteps[10, e.RowIndex] = new DataGridViewTextBoxCell();
                    gridProcedureSteps[10, e.RowIndex].ReadOnly = true;
                }
                else
                {
                    gridProcedureSteps[5, e.RowIndex].ReadOnly = false;
                    gridProcedureSteps[6, e.RowIndex].ReadOnly = false;
                }
            }
            else if (columnName.Equals("REPORT TYPE"))
            {
                String serviceType = value.ToString();

                if (serviceType.Equals(valueSelectedInGrid))
                {
                    valueSelectedInGrid = "";
                    return;
                }

                gridProcedureSteps[7, e.RowIndex].ReadOnly = true;
                gridProcedureSteps[7, e.RowIndex].Value = false;

                gridProcedureSteps[8, e.RowIndex].ReadOnly = true;
                gridProcedureSteps[8, e.RowIndex].Value = "";

                gridProcedureSteps[9, e.RowIndex].ReadOnly = true;
                gridProcedureSteps[9, e.RowIndex].Value = "";

                gridProcedureSteps[10, gridProcedureSteps.Rows.Count - 1] = new DataGridViewTextBoxCell();
                gridProcedureSteps[10, e.RowIndex].ReadOnly = true;

                int i = serviceType.IndexOf("[");
                int j = serviceType.IndexOf("]");
                String type = serviceType.Substring((i + 1), (j - 1));

                sql = "select '[' + dbo.f_zero(service_subtype, 4) + '] ' + description from subtypes where service_type = " + type + " and is_request = 0 order by service_subtype";

                table = DbInterface.GetDataTable(sql);
                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        combo.Items.Add(row[0]);
                    }
                }
                else
                {
                    combo.Items.Add("[There are no report subtypes for this report type]");
                }

                gridProcedureSteps[6, gridProcedureSteps.CurrentRow.Index] = combo;
            }
            else if (columnName.Equals("REPORT SUBTYPE"))
            {
                String serviceType = gridProcedureSteps.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                String serviceSubtype = value.ToString();

                if (serviceSubtype.Equals(valueSelectedInGrid))
                {
                    valueSelectedInGrid = "";
                    return;
                }

                gridProcedureSteps[7, e.RowIndex].ReadOnly = true;
                gridProcedureSteps[7, e.RowIndex].Value = false;

                gridProcedureSteps[8, e.RowIndex].ReadOnly = true;
                gridProcedureSteps[8, e.RowIndex].Value = "";

                gridProcedureSteps[9, e.RowIndex].ReadOnly = true;
                gridProcedureSteps[9, e.RowIndex].Value = "";

                gridProcedureSteps[10, gridProcedureSteps.Rows.Count - 1] = new DataGridViewTextBoxCell();
                gridProcedureSteps[10, e.RowIndex].ReadOnly = true;

                int i = serviceType.IndexOf("[");
                int j = serviceType.IndexOf("]");
                String type = serviceType.Substring((i + 1), (j - 1));

                i = serviceSubtype.IndexOf("[");
                j = serviceSubtype.IndexOf("]");
                String subtype = serviceSubtype.Substring((i + 1), (j - 1));

                // Saber se o subtype herda sua estrutura
                sql = "select isnull((select top 1 same_as_subtype from subtype_structure where service_type = " + type + " and service_subtype = " + subtype + "), 0) as sameAs ";
                int sameAs = (int)DbInterface.ExecuteScalar(sql);

                sql = "select count(*) as numDataFields from subtype_structure where service_type = " + type + " and service_subtype = " + subtype;
                int numDataFields = (int)DbInterface.ExecuteScalar(sql);

                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                if (numDataFields == 0)
                {
                    return;
                }

                if (sameAs > 0)
                {
                    sql = @"select distinct('[' + dbo.f_zero(subtype_structure.data_field_id, 4) + '] ' + data_fields.data_field_name)
                            from subtype_structure, data_fields
                            where subtype_structure.data_field_id = data_fields.data_field_id
                            and subtype_structure.service_type = " + type + " and subtype_structure.service_subtype = " + sameAs;
                }
                else
                {
                    sql = @"select distinct('[' + dbo.f_zero(subtype_structure.data_field_id, 4) + '] ' + data_fields.data_field_name)
                            from subtype_structure, data_fields
                            where subtype_structure.data_field_id = data_fields.data_field_id
                            and subtype_structure.service_type = " + type + " and subtype_structure.service_subtype = " + subtype;
                }

                table = DbInterface.GetDataTable(sql);

                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        combo.Items.Add(row[0]);
                    }
                }
                else
                {
                    combo.Items.Add("[There are no data fields for this report subtype]");
                }

                gridProcedureSteps[8, gridProcedureSteps.CurrentRow.Index] = combo;
                gridProcedureSteps[7, gridProcedureSteps.CurrentRow.Index].ReadOnly = false;
            }
            else if (columnName.Contains("CHECK DATA FIELD"))
            {
                bool isChecked = (bool)gridProcedureSteps.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (isChecked)
                {
                    gridProcedureSteps[8, e.RowIndex].ReadOnly = false;
                    gridProcedureSteps[9, e.RowIndex].ReadOnly = false;
                    gridProcedureSteps[10, e.RowIndex].ReadOnly = false;
                }
                else
                {
                    gridProcedureSteps[8, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[8, e.RowIndex].Value = "";

                    gridProcedureSteps[9, e.RowIndex].ReadOnly = true;
                    gridProcedureSteps[9, e.RowIndex].Value = "";

                    gridProcedureSteps[10, e.RowIndex] = new DataGridViewTextBoxCell();
                    gridProcedureSteps[10, e.RowIndex].ReadOnly = true;

                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "";
                }
            }
            else if (columnName.Contains("DATA FIELD"))
            {
                String dataFieldCell = value.ToString();

                if (dataFieldCell.Equals(valueSelectedInGrid))
                {
                    valueSelectedInGrid = "";
                    return;
                }

                gridProcedureSteps[10, e.RowIndex] = new DataGridViewTextBoxCell();

                int i = dataFieldCell.IndexOf("[");
                int j = dataFieldCell.IndexOf("]");
                String dataField = dataFieldCell.Substring((i + 1), (j - 1));

                sql = @"select
                        case
                           when type_is_bool = 1 then 'Boolean'
                           when type_is_numeric = 1 then 'Numeric'
                           when type_is_raw_hex = 1 then 'Raw Hex'
                           when type_is_list = 1 then 'List'
                           when type_is_table = 1 then 'Table' end
                        as FieldType,
                        case
                           when table_name is NULL then '' else table_name end as TableName,
                        case
                           when list_id is NULL then 0 else list_id end as ListId
                        from 
                           data_fields
                        where
                           data_field_id = " + dataField + @"
                           and
                           data_field_id <> 0";

                table = DbInterface.GetDataTable(sql);

                if (table.Rows[0]["FieldType"].ToString().Equals("Boolean"))
                {
                    DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell();
                    gridProcedureSteps[10, e.RowIndex] = cell;
                    gridProcedureSteps[10, e.RowIndex].Value = false;
                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "Boolean";
                }
                else if (table.Rows[0]["FieldType"].ToString().Equals("Numeric"))
                {
                    gridProcedureSteps[10, e.RowIndex] = new DataGridViewTextBoxCell();
                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "Numeric";
                }
                else if (table.Rows[0]["FieldType"].ToString().Equals("Raw Hex"))
                {
                    gridProcedureSteps[10, e.RowIndex] = new DataGridViewTextBoxCell();
                    gridProcedureSteps[10, e.RowIndex].Value = Formatting.FormatHexString("000000");
                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "Raw Hex";
                }
                else if (table.Rows[0]["FieldType"].ToString().Equals("List"))
                {
                    gridProcedureSteps[10, e.RowIndex] = new DataGridViewComboBoxCell();

                    sql = @"select '[' + dbo.f_zero(a.list_value, 
                                        (select len(convert(varchar(5), max(list_value))) from data_field_lists where list_id = a.list_id))
                                        + '] ' + a.list_text as list_item
                                        from data_field_lists a where a.list_id = " + table.Rows[0]["ListId"].ToString();

                    DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();
                    table = DbInterface.GetDataTable(sql);

                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            combo.Items.Add(row[0]);
                        }
                    }
                    else
                    {
                        // caso nao tenha registro no banco de dados para o tipo List selecionado
                        combo.Items.Add("[There are no " + dataFieldCell.Substring((j + 1), (dataFieldCell.Length - (j + 1))) + " for this data field]");
                    }

                    gridProcedureSteps[10, e.RowIndex] = combo;
                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "List";

                }
                else if (table.Rows[0]["FieldType"].ToString().Equals("Table"))
                {
                    // O trecho abaixo foi copiado da classe AppDataGridsHandling da linha 159 ate 184
                    // @todo: verificar se a Table "subtypes" deve ser acrescentada a esta consulta
                    switch (table.Rows[0]["TableName"].ToString().ToUpper())
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
                        case "TC_FAILURE_CODES":
                            sql = "    select '[' + dbo.f_zero(tc_failure_code, 3) + '] ' + tc_failure_description from tc_failure_codes order by tc_failure_code";
                            break;
                        case "SUBTYPES":
                            // o select aqui filtra os subtipos que contem novos TC (11/4 e 19/1)
                            sql = "select '[Choose a service type first]'";
                            break;
                        default:
                            break;
                    }

                    DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                    table = DbInterface.GetDataTable(sql);

                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            combo.Items.Add(row[0]);
                        }
                    }
                    else
                    {
                        // caso nao tenha registro no banco de dados para o tipo Table selecionado
                        combo.Items.Add("[There are no " + dataFieldCell.Substring((j + 1), (dataFieldCell.Length - (j + 1))) + " for this data field]");
                    }

                    gridProcedureSteps[10, e.RowIndex] = combo;
                    gridProcedureSteps[8, e.RowIndex].ToolTipText = "Table";
                }
            }
            else if ((columnName.Contains("COMPARE TO")) &&
                     (!gridProcedureSteps[10, e.RowIndex].ValueType.Name.Equals("Boolean")) &&
                     (!gridProcedureSteps[10, e.RowIndex].Value.ToString().Contains("[")) &&
                     (gridProcedureSteps[8, e.RowIndex].ToolTipText.Equals("Raw Hex")))
            {
                gridProcedureSteps[10, e.RowIndex].Value = Formatting.FormatHexString(value.ToString().Trim());
            }
            else if ((columnName.Contains("DELAY BEFORE SENDING")) ||
                     (e.ColumnIndex == 12) ||
                     (e.ColumnIndex == 14))
            {
                gridProcedureSteps[e.ColumnIndex, e.RowIndex].Value = UInt32.Parse(value.ToString().Trim()).ToString();
            }

            RefreshEstimatedDuration();
        }

        private void gridProcedureSteps_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            object value = gridProcedureSteps[e.ColumnIndex, e.RowIndex].Value;

            if (value == null)
            {
                return;
            }

            String columnName = gridProcedureSteps.Columns[e.ColumnIndex].HeaderText.ToUpper();

            if (columnName.Contains("REPORT TYPE"))
            {
                valueSelectedInGrid = value.ToString();
            }
            else if (columnName.Contains("REPORT SUBTYPE"))
            {
                valueSelectedInGrid = value.ToString();
            }
            else if (columnName.Contains("DATA FIELD"))
            {
                valueSelectedInGrid = value.ToString();
            }
            else if (columnName.Contains("STEP TYPE"))
            {
                valueSelectedInGrid = value.ToString();
            }
        }

        private void btAdd_Click_1(object sender, EventArgs e)
        {
            gridProcedureSteps.Rows.Add();

            FillStepCell("Request", gridProcedureSteps.RowCount - 1);
            gridProcedureSteps.Rows[gridProcedureSteps.Rows.Count - 1].Cells[0].Value = "Request";

            gridProcedureSteps.Rows[gridProcedureSteps.Rows.Count - 1].Cells[11].Value = "Between";
            gridProcedureSteps.Rows[gridProcedureSteps.Rows.Count - 1].Cells[13].Value = "and";
            gridProcedureSteps.Rows[gridProcedureSteps.Rows.Count - 1].Cells[15].Value = "seconds";

            // Preencher a coluna REPORT TYPE com os services do banco de dados
            String sql = "select '[' + dbo.f_zero(service_type, 4) + '] ' + service_name from services order by service_type";
            DataTable table = DbInterface.GetDataTable(sql);
            DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

            if ((table != null) && (table.Rows.Count > 0))
            {
                foreach (DataRow row in table.Rows)
                {
                    combo.Items.Add(row[0]);
                }
            }
            else
            {
                combo.Items.Add("[There are no records for this step type]");
            }

            gridProcedureSteps[5, gridProcedureSteps.Rows.Count - 1] = combo;

            // Configurar as permissoes de edicao das colunas
            gridProcedureSteps[3, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[4, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[5, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[6, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[7, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[8, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[9, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;

            gridProcedureSteps[10, gridProcedureSteps.Rows.Count - 1] = new DataGridViewTextBoxCell();
            gridProcedureSteps[10, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;

            gridProcedureSteps[11, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[12, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[13, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[14, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;
            gridProcedureSteps[15, gridProcedureSteps.Rows.Count - 1].ReadOnly = true;

            // Default Values in Cells
            gridProcedureSteps[2, gridProcedureSteps.Rows.Count - 1].Value = "0";
            gridProcedureSteps[3, gridProcedureSteps.Rows.Count - 1].Value = false;
            gridProcedureSteps[7, gridProcedureSteps.Rows.Count - 1].Value = false;
            gridProcedureSteps[12, gridProcedureSteps.Rows.Count - 1].Value = "0";
            gridProcedureSteps[14, gridProcedureSteps.Rows.Count - 1].Value = "0";

            gridProcedureSteps.Columns[11].DefaultCellStyle.Font = new Font(gridProcedureSteps.Columns[11].InheritedStyle.Font, FontStyle.Regular);
            gridProcedureSteps.Columns[13].DefaultCellStyle.Font = new Font(gridProcedureSteps.Columns[11].InheritedStyle.Font, FontStyle.Regular);
            gridProcedureSteps.Columns[15].DefaultCellStyle.Font = new Font(gridProcedureSteps.Columns[11].InheritedStyle.Font, FontStyle.Regular);

            gridProcedureSteps.Columns[11].DefaultCellStyle.ForeColor = Color.Navy;
            gridProcedureSteps.Columns[13].DefaultCellStyle.ForeColor = Color.Navy;
            gridProcedureSteps.Columns[15].DefaultCellStyle.ForeColor = Color.Navy;

            gridProcedureSteps.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (gridProcedureSteps.RowCount > 1)
            {
                btRemove.Enabled = true;
                btUp.Enabled = true;
                btDown.Enabled = true;
            }
        }

        private void btRemove_Click_1(object sender, EventArgs e)
        {
            gridProcedureSteps.Rows.Remove(gridProcedureSteps.CurrentRow);

            if (gridProcedureSteps.RowCount == 1)
            {
                btUp.Enabled = false;
                btDown.Enabled = false;
                btRemove.Enabled = false;
            }
        }

        private void btUp_Click_1(object sender, EventArgs e)
        {
            MoveRow(true);
        }

        private void btDown_Click_1(object sender, EventArgs e)
        {
            MoveRow(false);
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.inserting);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.editing);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the Test Procedure " + gridDatabase.CurrentRow.Cells[0].Value.ToString() + ", '" + gridDatabase.CurrentRow.Cells[1].Value.ToString() + "' ?",
                                "Please Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            dbTestProcedure.ProcedureId = gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();
            int indexSelected = gridDatabase.CurrentRow.Index;

            if (dbTestProcedure.Delete())
            {
                ChangeMode(Mode.browsing);

                if (gridDatabase.RowCount == 0)
                {
                    return;
                }

                if (indexSelected == gridDatabase.RowCount)
                {
                    gridDatabase.Rows[indexSelected - 1].Cells[0].Selected = true;
                }
                else
                {
                    gridDatabase.Rows[indexSelected].Cells[0].Selected = true;
                }
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            gridProcedureSteps.Focus();
            gridProcedureSteps.EndEdit();

            RefreshEstimatedDuration();
            
            if (!ValidateProcedureSteps())
            {
                return;
            }

            dbTestProcedure.ProcedureId = txtProcedureID.Text.Trim();
            dbTestProcedure.Description = txtDescription.Text.Trim().Replace("'", "''");
            dbTestProcedure.Purpose = txtPurpose.Text.Trim().Replace("'", "''");
            dbTestProcedure.EstimatedDuration = txtEstimatedDuration.Text.Trim();
            dbTestProcedure.SyncObtAtTheBeggining = chkSynchronizeOBT.Checked;
            dbTestProcedure.GetCpuUsage = chkGetCpuUsage.Checked;
            dbTestProcedure.RunInLoop = chkRunInLoop.Checked;
            dbTestProcedure.LoopsIterations = (int)numLoopIterations.Value;
            dbTestProcedure.SendEmail = chkSendEMail.Checked;
            dbTestProcedure.ProcedureSteps = GetProcedureStepsInGrid();

            //salva o valor selecionado no combo para o campo correspondente na base de dados
            if (cmbContourTCs.SelectedItem != null)
            {
                String cmbValue = null;
                cmbValue = cmbContourTCs.SelectedItem.ToString();
                dbTestProcedure.Contour_test_case_id = cmbValue.Substring(1, cmbValue.IndexOf("]") - 1);
            }
            
            if (rdbControlSequence.Checked)
            {
                dbTestProcedure.PacketsSequenceControl = "control";
            }
            else if (rdbRespectSequence.Checked)
            {
                dbTestProcedure.PacketsSequenceControl = "respect";
            }
            else if (rdbDisableSequenceControl.Checked)
            {
                dbTestProcedure.PacketsSequenceControl = "disable";
            }

            if (currentMode == Mode.inserting)
            {
                if (!dbTestProcedure.Insert())
                {
                    return; // sai sem voltar para o modo browsing
                }

                procedureSelected = int.Parse(txtProcedureID.Text);
            }
            else if (currentMode == Mode.editing)
            {
                if (!dbTestProcedure.Update())
                {
                    return; // sai sem voltar para o modo browsing
                }

                procedureSelected = int.Parse(txtProcedureID.Text);
            }

            // ATUALIZAR O ESTIMATED DURATION DE TODOS OS REGISTROS QUE USAM ESTE QUE ACABARA DE SER ALTERADO
            int newEstimatedDuration = int.Parse(txtEstimatedDuration.Text.Trim());

            if (newEstimatedDuration != originalEstimatedDuration)
            {
                dbTestProcedure.UpdateEstimatedDuration(newEstimatedDuration, int.Parse(txtProcedureID.Text.Trim()));
            }

            ChangeMode(Mode.browsing);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this edition?",
                                "Cancel Edition",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            ChangeMode(Mode.browsing);
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.browsing);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            gridProcedureSteps.EndEdit();
        }

        /**
         * Metodo de intercepcao e gerenciamento das teclas Insert, Enter, Esc e F5
         * para permitir a operacao da tela pelo usuario.
         * OBS: Foi retirado do mesmo evento do FrmSubtypes
         **/
        private void FrmTestProceduresComposition_KeyDown(object sender, KeyEventArgs e)
        {
            // Para evitar que o Enter mude a linha do grid antes de chamar o Edit.
            if ((gridDatabase.Focused) && (e.KeyCode == Keys.Enter))
            {
                e.Handled = true;
            }

            if (currentMode == Mode.browsing)
            {
                if ((e.KeyCode == Keys.Insert) && (btNew.Enabled))
                {
                    btNew_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.Delete) && (btDelete.Enabled))
                {
                    btDelete_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.Enter) && (btEdit.Enabled))
                {
                    btEdit_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.F5) && (btRefresh.Enabled))
                {
                    btRefresh_Click(this, new EventArgs());
                }
            }
            else // currentMode = editing ou inserting
            {
                if ((e.KeyCode == Keys.Enter) && (btConfirm.Enabled))
                {
                    if (txtPurpose.Focused)
                    {
                        return;
                    }

                    btConfirm_Click(this, new EventArgs());
                }
                else if ((e.KeyCode == Keys.Escape) && (btCancel.Enabled))
                {
                    btCancel_Click(this, new EventArgs());
                }
            }
        }

        private void gridDatabase_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(this, new EventArgs());
        }

        private void numLoopIterations_ValueChanged(object sender, EventArgs e)
        {
            RefreshEstimatedDuration();
        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            if ((frmConnection != null) && (frmConnection.Connected))
            {
                int procedureId = int.Parse(gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString());

                if (mdiMain.FormIsOpen("Test Procedure Execution"))
                {
                    mdiMain.FormTestProceduresExecution.ProcedureId = procedureId;
                    mdiMain.FormTestProceduresExecution.FrmTestProcedureExecution_Load(this, new EventArgs());
                }
                else
                {
                    mdiMain.FormTestProceduresExecution = new FrmTestProcedureExecution(procedureId, mdiMain);
                    mdiMain.FormTestProceduresExecution.MdiParent = mdiMain;
                    mdiMain.FormTestProceduresExecution.Show(mdiMain.DockPanel, DockState.Document);
                }

                mdiMain.FormTestProceduresExecution.Enabled = true;
                mdiMain.FormTestProceduresExecution.Focus();

                if (mdiMain.FormIsOpen("Test Sessions Log"))
                {
                    mdiMain.FormSessionsLog.chkRealTime.Checked = true;
                    mdiMain.FormSessionsLog.numSeconds.Value = 1;
                }
                else
                {
                    frmSessionsLog = new FrmSessionsLog();
                    frmSessionsLog.MdiParent = mdiMain;
                    frmSessionsLog.Show(mdiMain.FormConnectionWithEgse.Pane, DockAlignment.Bottom, 0.45);
                    frmSessionsLog.OpenedByTestProceduresComposition = true;
                    frmSessionsLog.chkRealTime.Checked = true;
                    frmSessionsLog.numSeconds.Value = 1;
                    mdiMain.FormSessionsLog = frmSessionsLog;
                }

                // Desabilitar todos os outros formularios para que o usuario nao continue em outra atividade no SMC.
                for (int index = mdiMain.DockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (mdiMain.DockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = (IDockContent)mdiMain.DockPanel.Contents[index];

                        if (content.DockHandler.Form.Text.Equals("Test Procedure Execution"))
                        {
                            content.DockHandler.Form.Focus();
                        }
                        else if (!content.DockHandler.Form.Text.Equals("Test Sessions Log"))
                        {
                            content.DockHandler.Form.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("The SMC is not connected to the EGSE.\n\nConnect it and try again.",
                                "Please Confirm Connection",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button2);
            }
        }

        public void gridDatabase_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar se o procedimento que esta sendo selecionado ja foi executado.
            String sql = "select executed from test_procedures where procedure_id = " + gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();
            object objExecuted = DbInterface.ExecuteScalar(sql);

            if (objExecuted == null)
            {
                return;
            }

            bool executed = (bool)objExecuted;

            if (executed)
            {
                btEdit.Enabled = false;
                btDelete.Enabled = false;
            }
            else
            {
                btEdit.Enabled = true;
                btDelete.Enabled = true;
            }
        }

        private void btCopyProcedure_Click(object sender, EventArgs e)
        {
            FrmCopyProcedure frmCopyProcedure = new FrmCopyProcedure(this);
            frmCopyProcedure.ShowDialog();
        }

        #endregion

        #region Metodos Privados

        public void RefreshGrid()
        {
            String sql = @"select procedure_id as 'Procedure ID', description as 'Description', 
                            estimated_duration as 'Estimated Duration (Secs.)', purpose as 'Purpose', contour_test_case_id as 'Test Case ID' 
                            from test_procedures where procedure_id <> 0 
                            order by procedure_id";

            gridDatabase.DataSource = DbInterface.GetDataTable(sql);

            gridDatabase.Columns[0].Width = 80;
            gridDatabase.Columns[1].Width = 220;
            gridDatabase.Columns[2].Width = 160;
            gridDatabase.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            gridDatabase.Refresh();
        }

        /**
         * Move a row selecionada no grid de estrutura para cima ou para baixo.
         **/
        private void MoveRow(bool up)
        {
            int currentIndex = gridProcedureSteps.CurrentRow.Index;
            int currentCellIndex = gridProcedureSteps.CurrentCell.ColumnIndex;
            int newIndex = 0;

            // determina qual a linha a ser trocada e faz a troca
            if (up)
            {
                newIndex = currentIndex - 1;
            }
            else
            {
                newIndex = currentIndex + 1;
            }

            if ((newIndex < 0) || (newIndex == gridProcedureSteps.RowCount))
            {
                // nao eh possivel mover; sai
                return;
            }

            DataGridViewRow rowToMove = gridProcedureSteps.Rows[currentIndex];

            gridProcedureSteps.Rows.Remove(gridProcedureSteps.Rows[currentIndex]);

            gridProcedureSteps.Rows.Insert(newIndex, rowToMove);
            gridProcedureSteps.Refresh();
            gridProcedureSteps.CurrentCell = gridProcedureSteps.Rows[newIndex].Cells[currentCellIndex];
            gridProcedureSteps.Rows[newIndex].Selected = true;
        }

        /**
         * Preenche o combo da celula da coluna Step Type em funcao do step selecionado na primeira celula do grid.
         **/
        private void FillStepCell(String stepType, int rowIndex)
        {
            String sql = "";

            if (stepType.Equals("Procedure"))
            {
                sql = @"select '[' + dbo.f_zero(procedure_id, 4) + '] ' + description 
                        from test_procedures where procedure_id <> 0 order by procedure_id";
            }
            else
            {
                sql = @"select '[' + dbo.f_zero(saved_request_id, 4) + '] ' + description 
                        from saved_requests where saved_request_id <> 0 order by saved_request_id";
            }

            DataTable table = null;

            if (!sql.Equals(""))
            {
                table = DbInterface.GetDataTable(sql);
            }

            DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

            if ((table != null) && (table.Rows.Count > 0))
            {
                foreach (DataRow row in table.Rows)
                {
                    combo.Items.Add(row[0]);
                }
            }
            else
            {
                combo.Items.Add("[There are no records for this step]");
            }

            gridProcedureSteps[1, rowIndex] = combo;
        }

        private void ChangeMode(Mode newMode)
        {
            String sql = "";

            switch (newMode)
            {
                case Mode.browsing:
                    {
                        RefreshGrid();

                        if (gridDatabase.RowCount == 0)
                        {
                            btEdit.Enabled = false;
                            btDelete.Enabled = false;
                            btExecute.Enabled = false;

                            procedureSelected = 0;
                        }
                        else
                        {
                            btEdit.Enabled = true;
                            btDelete.Enabled = true;
                            btExecute.Enabled = true;

                            // Selecionar o item no gridDatabase
                            if (procedureSelected != 0)
                            {
                                foreach (DataGridViewRow row in gridDatabase.Rows)
                                {
                                    if (procedureSelected == int.Parse(row.Cells[0].Value.ToString()))
                                    {
                                        gridDatabase.Rows[row.Index].Cells[0].Selected = true;
                                        break;
                                    }
                                }

                                procedureSelected = 0;
                            }
                        }

                        btNew.Enabled = true;
                        btConfirm.Enabled = false;
                        btCancel.Enabled = false;
                        btRefresh.Enabled = true;

                        currentMode = newMode;

                        tabControl1.SelectedIndex = 0;
                        gridDatabase.Focus();

                        break;
                    }
                case Mode.inserting:
                case Mode.editing:
                    {
                        gridProcedureSteps.Rows.Clear();
                        currentMode = newMode;
                        //preencher o combo de test case
                        FillTestCaseCombo();

                        if (newMode == Mode.inserting)
                        {
                            // CABECALHO
                            sql = @"select isnull(max(procedure_id + 1), 1) as 'NextProcedure' 
                            from test_procedures";

                            txtProcedureID.Text = DbInterface.ExecuteScalar(sql).ToString();
                            txtDescription.Text = "";
                            txtEstimatedDuration.Text = "0";
                            txtPurpose.Text = "";

                            chkSynchronizeOBT.Checked = true;
                            chkGetCpuUsage.Checked = false;
                            chkRunInLoop.Checked = false;
                            chkSendEMail.Checked = false;
                            numLoopIterations.Enabled = false;
                            numLoopIterations.Value = 1;
                            rdbControlSequence.Checked = true;
                            rdbRespectSequence.Checked = false;
                            rdbDisableSequenceControl.Checked = false;

                            // PROCEDURE STEPS 
                            gridProcedureSteps.Rows.Clear();
                            btAdd.Enabled = true;
                            btRemove.Enabled = false;
                            btUp.Enabled = false;
                            btDown.Enabled = false;

                            btAdd_Click_1(this, new EventArgs());
                        }
                        else // editing
                        {
                            if (btEdit.Enabled == false)
                            {
                                return;
                            }

                            // CABECALHO
                            txtProcedureID.Text = gridDatabase[0, gridDatabase.CurrentRow.Index].Value.ToString();
                            txtDescription.Text = gridDatabase[1, gridDatabase.CurrentRow.Index].Value.ToString();
                            txtEstimatedDuration.Text = gridDatabase[2, gridDatabase.CurrentRow.Index].Value.ToString();

                            originalEstimatedDuration = UInt32.Parse(txtEstimatedDuration.Text.ToString());
                            txtPurpose.Text = gridDatabase[3, gridDatabase.CurrentRow.Index].Value.ToString();

                            sql = "select synchronize_obt, get_cpu_usage, run_in_loop, loop_iterations, send_mail, packets_sequence_control_options from test_procedures where procedure_id = " + txtProcedureID.Text;
                            DataTable table = DbInterface.GetDataTable(sql);

                            chkSynchronizeOBT.Checked = (bool)table.Rows[0]["synchronize_obt"];
                            chkGetCpuUsage.Checked = (bool)table.Rows[0]["get_cpu_usage"];
                            chkRunInLoop.Checked = (bool)table.Rows[0]["run_in_loop"];

                            if (chkRunInLoop.Checked)
                            {
                                numLoopIterations.Enabled = (bool)table.Rows[0]["run_in_loop"];
                                numLoopIterations.Value = (int)table.Rows[0]["loop_iterations"];
                            }
                            else
                            {
                                numLoopIterations.Enabled = false;
                                numLoopIterations.Value = 1;
                            }

                            chkSendEMail.Checked = (bool)table.Rows[0]["send_mail"];

                            String controlSequence = table.Rows[0]["packets_sequence_control_options"].ToString();

                            if (controlSequence.ToUpper().Equals("CONTROL"))
                            {
                                rdbControlSequence.Checked = true;
                            }
                            else if (controlSequence.ToUpper().Equals("RESPECT"))
                            {
                                rdbRespectSequence.Checked = true;
                            }
                            else if (controlSequence.ToUpper().Equals("DISABLE"))
                            {
                                rdbDisableSequenceControl.Checked = true;
                            }

                            // PROCEDURE STEPS
                            gridProcedureSteps.Rows.Clear();

                            sql = @"select procedure_id, 
                                  position, 
                                  saved_request_id,
                                  time_delay, 
                                  verify_execution, 
                                  verify_condition, 
                                  report_type, 
                                  report_subtype, 
                                  data_field_id, 
                                  comparison_operation, 
                                  value_to_compare, 
                                  verify_interval_start, 
                                  verify_interval_end 
                           from test_procedure_steps 
                           where procedure_id = " + txtProcedureID.Text;

                            DataTable tableSteps = DbInterface.GetDataTable(sql);

                            int rowIndex = 0;

                            foreach (DataRow row in tableSteps.Rows)
                            {
                                // adicionar um row
                                btAdd_Click_1(this, new EventArgs());

                                String codeOfStep = "";

                                if (((int)row["saved_request_id"]) != 0)
                                {
                                    gridProcedureSteps[0, rowIndex].Value = "Request";

                                    sql = @"select '[' + dbo.f_zero(saved_request_id, 4) + '] ' + description 
                                    from saved_requests where saved_request_id <> 0 order by saved_request_id";

                                    codeOfStep = row["saved_request_id"].ToString();
                                }

                                // Preencher o comboboxCell Step
                                DataTable tableStep = DbInterface.GetDataTable(sql);
                                DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();

                                if ((tableStep != null) && (tableStep.Rows.Count > 0))
                                {
                                    foreach (DataRow rowStep in tableStep.Rows)
                                    {
                                        combo.Items.Add(rowStep[0]);
                                    }
                                }
                                else
                                {
                                    combo.Items.Add("[There are no records for this step type]");
                                }

                                gridProcedureSteps[1, rowIndex] = combo;

                                // Selecionar o step
                                DataGridViewComboBoxCell cellStep = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[1];
                                String valueStep = "";

                                for (int i = 0; i < cellStep.Items.Count; i++)
                                {
                                    int first = cellStep.Items[i].ToString().IndexOf("[");
                                    int last = cellStep.Items[i].ToString().IndexOf("]");

                                    String codeItem = cellStep.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                    int code = 0;

                                    if (int.TryParse(codeItem, out code) && (code == int.Parse(codeOfStep)))
                                    {
                                        gridProcedureSteps.Rows[rowIndex].Cells[1].Value = cellStep.Items[i].ToString();
                                        valueStep = cellStep.Items[i].ToString();
                                        break;
                                    }
                                }

                                // Preencher a celula da coluna Delay Before Sending
                                gridProcedureSteps[2, rowIndex].Value = row["time_delay"];

                                // Preencher a celula da coluna Verify Execution
                                gridProcedureSteps[3, rowIndex].Value = (bool)row["verify_execution"];

                                // Ver se Verify Execution eh false, se for sai.
                                if (((bool)row["verify_execution"]))
                                {
                                    // Preencher a celula da coluna Verify Condition
                                    gridProcedureSteps[4, rowIndex].Value = row["verify_condition"];

                                    // Preencher a celula da coluna de indice 12
                                    gridProcedureSteps[12, rowIndex].Value = row["verify_interval_start"];

                                    // Preencher a celula da coluna de indice 14
                                    gridProcedureSteps[14, rowIndex].Value = row["verify_interval_end"];

                                    // Ver se Verify Condition possui valor que contenha ACK ou NACK, se tiver sai.
                                    if (!(row["verify_condition"]).ToString().ToUpper().Contains("ACK") &&
                                        !(row["verify_condition"]).ToString().ToUpper().Contains("NACK"))
                                    {
                                        // Selecionar o valor do Report Type no ComboboxCell
                                        String codeOfReportType = row["report_type"].ToString();

                                        DataGridViewComboBoxCell cellReportType = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[5];

                                        for (int i = 0; i < cellReportType.Items.Count; i++)
                                        {
                                            int first = cellReportType.Items[i].ToString().IndexOf("[");
                                            int last = cellReportType.Items[i].ToString().IndexOf("]");

                                            String codeItem = cellReportType.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                            int code = 0;

                                            if (int.TryParse(codeItem, out code) && (code == int.Parse(codeOfReportType)))
                                            {
                                                gridProcedureSteps.Rows[rowIndex].Cells[5].Value = cellReportType.Items[i].ToString();
                                                break;
                                            }
                                        }

                                        // Preencher o ComboBoxCell do Report Subtype
                                        String sqlReportSubtypes = "select '[' + dbo.f_zero(service_subtype, 4) + '] ' + description from subtypes where service_type = " + codeOfReportType + " and is_request = 0 order by service_subtype";
                                        DataTable tableReportSubtypes = null;

                                        if (!sqlReportSubtypes.Equals(""))
                                        {
                                            tableReportSubtypes = DbInterface.GetDataTable(sqlReportSubtypes);
                                        }

                                        DataGridViewComboBoxCell comboReportSubtypes = new DataGridViewComboBoxCell();

                                        if ((tableReportSubtypes != null) && (tableReportSubtypes.Rows.Count > 0))
                                        {
                                            foreach (DataRow rowSubtype in tableReportSubtypes.Rows)
                                            {
                                                comboReportSubtypes.Items.Add(rowSubtype[0]);
                                            }
                                        }
                                        else
                                        {
                                            comboReportSubtypes.Items.Add("[There are no records for this step type]");
                                        }

                                        gridProcedureSteps[6, rowIndex] = comboReportSubtypes;

                                        // Selecionar o valor do Report Subtype no ComboboxCell
                                        String codeOfReportSubtype = row["report_subtype"].ToString();

                                        DataGridViewComboBoxCell cellReportSubtype = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[6];

                                        for (int i = 0; i < cellReportSubtype.Items.Count; i++)
                                        {
                                            int first = cellReportSubtype.Items[i].ToString().IndexOf("[");
                                            int last = cellReportSubtype.Items[i].ToString().IndexOf("]");

                                            String codeItem = cellReportSubtype.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                            int code = 0;

                                            if (int.TryParse(codeItem, out code) && (code == int.Parse(codeOfReportSubtype)))
                                            {
                                                gridProcedureSteps.Rows[rowIndex].Cells[6].Value = cellReportSubtype.Items[i].ToString();
                                                break;
                                            }
                                        }

                                        // Preencher a celula dos data fields
                                        String sqlStructureInherit = "select isnull((select top 1 same_as_subtype from subtype_structure where service_type = " + codeOfReportType + " and service_subtype = " + codeOfReportSubtype + "), 0) as sameAs ";
                                        int sameAs = (int)DbInterface.ExecuteScalar(sqlStructureInherit);

                                        sqlStructureInherit = "select count(*) as numDataFields from subtype_structure where service_type = " + codeOfReportType + " and service_subtype = " + codeOfReportSubtype;
                                        int numDataFields = (int)DbInterface.ExecuteScalar(sqlStructureInherit);

                                        DataGridViewComboBoxCell comboDataFields = new DataGridViewComboBoxCell();

                                        // Ainda nao sei porque sai da rotina se numDataFields ser 0.
                                        // Alguns procedimentos nao editam se esse return estiver aqui.
                                        // Deixar comentado por enquanto. Assinado: Thiago Duarte Pereira
                                        //if (numDataFields == 0)
                                        //{
                                        //    return;
                                        //}

                                        if (sameAs > 0)
                                        {
                                            sqlStructureInherit = @"select distinct('[' + dbo.f_zero(subtype_structure.data_field_id, 4) + '] ' + data_fields.data_field_name)
                                                                   from subtype_structure, data_fields
                                                                   where subtype_structure.data_field_id = data_fields.data_field_id
                                                                   and subtype_structure.service_type = " + codeOfReportType + " and subtype_structure.service_subtype = " + sameAs;
                                        }
                                        else
                                        {
                                            sqlStructureInherit = @"select distinct('[' + dbo.f_zero(subtype_structure.data_field_id, 4) + '] ' + data_fields.data_field_name)
                                                                   from subtype_structure, data_fields
                                                                   where subtype_structure.data_field_id = data_fields.data_field_id
                                                                   and subtype_structure.service_type = " + codeOfReportType + " and subtype_structure.service_subtype = " + codeOfReportSubtype;
                                        }

                                        DataTable tableDataFields = DbInterface.GetDataTable(sqlStructureInherit);

                                        if ((tableDataFields != null) && (tableDataFields.Rows.Count > 0))
                                        {
                                            foreach (DataRow rowDataFields in tableDataFields.Rows)
                                            {
                                                comboDataFields.Items.Add(rowDataFields[0]);
                                            }
                                        }
                                        else
                                        {
                                            comboDataFields.Items.Add("[There are no data fields for this report subtype]");
                                        }

                                        gridProcedureSteps[8, rowIndex] = comboDataFields;
                                        gridProcedureSteps[7, rowIndex].ReadOnly = false;

                                        String codeOfDataField = row["data_field_id"].ToString();

                                        if (!codeOfDataField.Equals(""))
                                        {
                                            gridProcedureSteps[7, rowIndex].Value = true;

                                            // Selecionar o valor do Data Field no ComboboxCell
                                            DataGridViewComboBoxCell cellDataField = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[8];
                                            String codeItemDtField = "";

                                            for (int i = 0; i < cellDataField.Items.Count; i++)
                                            {
                                                int first = cellDataField.Items[i].ToString().IndexOf("[");
                                                int last = cellDataField.Items[i].ToString().IndexOf("]");

                                                codeItemDtField = cellDataField.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                                int code = 0;

                                                if (int.TryParse(codeItemDtField, out code) && (code == int.Parse(codeOfDataField)))
                                                {
                                                    gridProcedureSteps.Rows[rowIndex].Cells[8].Value = cellDataField.Items[i].ToString();
                                                    break;
                                                }
                                            }

                                            // setar o Data Field Type
                                            String sqlDataFieldType = @"select case
   	                                                                      when type_is_bool = 1 then 'Boolean'
	                                                                      when type_is_numeric = 1 then 'Numeric'
	                                                                      when type_is_raw_hex = 1 then 'Raw Hex'
	                                                                      when type_is_list = 1 then 'List'
	                                                                      when type_is_table = 1 then 'Table' end
	                                                                      as type
                                                                       from data_fields
                                                                       where data_field_id = " + codeItemDtField;

                                            String dataFieldType = (String)DbInterface.ExecuteScalar(sqlDataFieldType);
                                            gridProcedureSteps[8, rowIndex].ToolTipText = dataFieldType;
                                        }
                                        else
                                        {
                                            gridProcedureSteps[7, rowIndex].Value = false;
                                        }

                                        // Preencher a celula da coluna Comparison Operation
                                        String operation = row["comparison_operation"].ToString();

                                        if (operation.Equals("="))
                                        {
                                            gridProcedureSteps[9, rowIndex].Value = "equal to";
                                        }
                                        else if (operation.Equals(">"))
                                        {
                                            gridProcedureSteps[9, rowIndex].Value = "major than";
                                        }
                                        else if (operation.Equals("<"))
                                        {
                                            gridProcedureSteps[9, rowIndex].Value = "minor than";
                                        }
                                        else if (operation.Equals(">="))
                                        {
                                            gridProcedureSteps[9, rowIndex].Value = "major than or equal to";
                                        }
                                        else if (operation.Equals("<="))
                                        {
                                            gridProcedureSteps[9, rowIndex].Value = "minor than or equal to";
                                        }
                                        else if (operation.Equals("!="))
                                        {
                                            gridProcedureSteps[9, rowIndex].Value = "different than";
                                        }

                                        // Preencher a celula da coluna Compare To
                                        if (gridProcedureSteps[8, rowIndex].ToolTipText.Equals("Table"))
                                        {
                                            String sqlTblName = @"select isnull(table_name, '') from data_fields where data_field_id = " + row["data_field_id"].ToString();
                                            String tableName = (String)DbInterface.ExecuteScalar(sqlTblName);
                                            String sqlTblItems = "";

                                            if (!tableName.Trim().Equals(""))
                                            {
                                                // O trecho abaixo foi copiado da classe AppDataGridsHandling da linha 159 ate 184
                                                // @todo: verificar se a Table "subtypes" deve ser acrescentada a esta consulta
                                                switch (tableName.ToUpper())
                                                {
                                                    case "APIDS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(apid, 4) + '] ' + application_name from apids order by apid";
                                                        break;
                                                    case "RIDS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(rid, 4) + '] ' + description from rids order by rid";
                                                        break;
                                                    case "MEMORY_IDS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(memory_id, 4) + '] ' + memory_unit_description from memory_ids order by memory_id";
                                                        break;
                                                    case "OUTPUT_LINE_IDS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(output_line_id, 4) + '] ' + output_line_description from output_line_ids order by output_line_id";
                                                        break;
                                                    case "PACKET_STORE_IDS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(store_id, 4) + '] ' + packet_store_name from packet_store_ids order by store_id";
                                                        break;
                                                    case "PARAMETERS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(parameter_id, 5) + '] ' + parameter_description from parameters order by parameter_id";
                                                        break;
                                                    case "REPORT_DEFINITIONS":
                                                        sqlTblItems = "select '[' + dbo.f_zero(structure_id, 5) + '] ' + report_definition_description from report_definitions order by structure_id";
                                                        break;
                                                    case "SERVICES":
                                                        sqlTblItems = "select '[' + dbo.f_zero(service_type, 3) + '] ' + service_name from services order by service_type";
                                                        break;
                                                    case "TC_FAILURE_CODES":
                                                        sqlTblItems = "select '[' + dbo.f_zero(tc_failure_code, 3) + '] ' + tc_failure_description from tc_failure_codes order by tc_failure_code";
                                                        break;
                                                    case "SUBTYPES":
                                                        // o select aqui filtra os subtipos que contem novos TC (11/4 e 19/1)
                                                        sqlTblItems = "select '[Choose a service type first]'";
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }

                                            // Preencher o ComboBoxCell da celula CompareTo
                                            DataTable tblItems = null;

                                            if (!sqlTblItems.Equals(""))
                                            {
                                                tblItems = DbInterface.GetDataTable(sqlTblItems);
                                            }

                                            DataGridViewComboBoxCell cmbTblItems = new DataGridViewComboBoxCell();

                                            if ((tblItems != null) && (tblItems.Rows.Count > 0))
                                            {
                                                foreach (DataRow rowItems in tblItems.Rows)
                                                {
                                                    cmbTblItems.Items.Add(rowItems[0]);
                                                }
                                            }
                                            else
                                            {
                                                cmbTblItems.Items.Add("[There are no records for this data field]");
                                            }

                                            gridProcedureSteps[10, rowIndex] = cmbTblItems;

                                            // Selecionar o valor da celula compareTo
                                            String codeOfToCompare = row["value_to_compare"].ToString();

                                            DataGridViewComboBoxCell cellToCompare = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[10];

                                            for (int i = 0; i < cellToCompare.Items.Count; i++)
                                            {
                                                int first = cellToCompare.Items[i].ToString().IndexOf("[");
                                                int last = cellToCompare.Items[i].ToString().IndexOf("]");

                                                String codeItem = cellToCompare.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                                int code = 0;

                                                if (int.TryParse(codeItem, out code) && (code == int.Parse(codeOfToCompare)))
                                                {
                                                    gridProcedureSteps.Rows[rowIndex].Cells[10].Value = cellToCompare.Items[i].ToString();
                                                    break;
                                                }
                                            }
                                        }
                                        else if (gridProcedureSteps[8, rowIndex].ToolTipText.Equals("List"))
                                        {
                                            gridProcedureSteps[10, rowIndex] = new DataGridViewComboBoxCell();
                                            DataGridViewComboBoxCell listCell = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[10];

                                            String sqlListId = "select isnull(list_id, 0) as listId from data_fields where data_field_id = " + row["data_field_id"].ToString();
                                            int listId = (int)DbInterface.ExecuteScalar(sqlListId);

                                            String sqlList = @"select '[' + dbo.f_zero(a.list_value, (select len(convert(varchar(5), max(list_value))) 
                                                        from data_field_lists where list_id = a.list_id)) + '] ' + a.list_text as list_item from data_field_lists a 
                                                        where a.list_id = " + listId;

                                            DataGridViewComboBoxCell comboList = new DataGridViewComboBoxCell();
                                            DataTable tableList = DbInterface.GetDataTable(sqlList);

                                            if ((tableList != null) && (tableList.Rows.Count > 0))
                                            {
                                                foreach (DataRow rowList in tableList.Rows)
                                                {
                                                    comboList.Items.Add(rowList[0]);
                                                }
                                            }
                                            else
                                            {
                                                // caso nao tenha registro no banco de dados para o tipo List selecionado
                                                comboList.Items.Add("[There are no values for this data field]");
                                            }

                                            gridProcedureSteps[10, rowIndex] = comboList;

                                            // Selecionar o valor da celula compareTo
                                            String codeOfToCompare = row["value_to_compare"].ToString();

                                            DataGridViewComboBoxCell cellToCompare = (DataGridViewComboBoxCell)gridProcedureSteps.Rows[rowIndex].Cells[10];

                                            for (int i = 0; i < cellToCompare.Items.Count; i++)
                                            {
                                                int first = cellToCompare.Items[i].ToString().IndexOf("[");
                                                int last = cellToCompare.Items[i].ToString().IndexOf("]");

                                                String codeItem = cellToCompare.Items[i].ToString().Substring((first + 1), (last - 1)).Trim();
                                                int code = 0;

                                                if (int.TryParse(codeItem, out code) && (code == int.Parse(codeOfToCompare)))
                                                {
                                                    gridProcedureSteps.Rows[rowIndex].Cells[10].Value = cellToCompare.Items[i].ToString();
                                                    break;
                                                }
                                            }
                                        }
                                        else if (gridProcedureSteps[8, rowIndex].ToolTipText.Equals("Raw Hex"))
                                        {
                                            String valueHexString = row["value_to_compare"].ToString();
                                            gridProcedureSteps[10, rowIndex].Value = Formatting.FormatHexString((UInt64.Parse(valueHexString)).ToString("X"));
                                        }
                                        else if (gridProcedureSteps[8, rowIndex].ToolTipText.Equals("Boolean"))
                                        {
                                            gridProcedureSteps[10, rowIndex] = new DataGridViewCheckBoxCell();

                                            if (row["value_to_compare"].ToString().Equals("0"))
                                            {
                                                gridProcedureSteps[10, rowIndex].Value = false;
                                            }
                                            else
                                            {
                                                gridProcedureSteps[10, rowIndex].Value = true;
                                            }
                                        }
                                        else if (gridProcedureSteps[8, rowIndex].ToolTipText.Equals("Numeric"))
                                        {
                                            gridProcedureSteps[10, rowIndex].Value = UInt64.Parse(row["value_to_compare"].ToString());
                                        }
                                    }
                                }

                                rowIndex++;
                            }

                            // Se nao trazer nada do banco de dados, inserir pelo menos 1 row por default
                            if (tableSteps.Rows.Count == 0)
                            {
                                btAdd_Click_1(this, new EventArgs());
                            }

                            //Seleciona para o procedutre o caso de teste correspondente que está armazenado no banco
                            String sqlContour = "SELECT contour_test_case_id FROM test_procedures WHERE procedure_id = " + txtProcedureID.Text;
                            for (int i = 0; i < cmbContourTCs.Items.Count; i++)
                            {
                                String objSub = cmbContourTCs.Items[i].ToString();
                                if (objSub.Substring(1, objSub.IndexOf("]") - 1) == DbTestProcedure.ExecuteScalar(sqlContour).ToString())
                                {
                                    cmbContourTCs.SelectedItem = cmbContourTCs.Items[i];
                                }
                            }

                            ConfigureCells();
                            gridProcedureSteps.Refresh();
                            procedureSelected = int.Parse(txtProcedureID.Text);
                            RefreshEstimatedDuration();
                        }

                        // posiciona a selecao na primeira celula para atualizar o scrollBar vertical para o canto esquerdo.
                        gridProcedureSteps.CurrentCell = gridProcedureSteps[1, 0];

                        btNew.Enabled = false;
                        btEdit.Enabled = false;
                        btDelete.Enabled = false;
                        btConfirm.Enabled = true;
                        btCancel.Enabled = true;
                        btExecute.Enabled = false;
                        btRefresh.Enabled = false;

                        tabControl1.SelectedIndex = 1;

                        txtDescription.Focus();
                        txtDescription.SelectAll();

                        break;
                    }
            }
        }

        /**
         * Configura as permissoes de cada celula individual no grid.
         **/
        private void ConfigureCells()
        {
            // Habilitar ou desabilitar as celulas do gridProcedureSteps
            foreach (DataGridViewRow rowSteps in gridProcedureSteps.Rows)
            {
                gridProcedureSteps[0, rowSteps.Index].ReadOnly = false;
                gridProcedureSteps[1, rowSteps.Index].ReadOnly = false;
                gridProcedureSteps[2, rowSteps.Index].ReadOnly = false;
                gridProcedureSteps[3, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[4, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[5, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[6, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[7, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[8, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[9, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[10, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[12, rowSteps.Index].ReadOnly = true;
                gridProcedureSteps[14, rowSteps.Index].ReadOnly = true;

                if (!rowSteps.Cells[0].Value.ToString().Equals("Procedure"))
                {
                    gridProcedureSteps[3, rowSteps.Index].ReadOnly = false;

                    if (((bool)rowSteps.Cells[3].Value) == true)
                    {
                        gridProcedureSteps[4, rowSteps.Index].ReadOnly = false;
                        gridProcedureSteps[12, rowSteps.Index].ReadOnly = false;
                        gridProcedureSteps[14, rowSteps.Index].ReadOnly = false;

                        if ((!rowSteps.Cells[4].Value.ToString().ToUpper().Contains("ACK")) &&
                            (!rowSteps.Cells[4].Value.ToString().ToUpper().Contains("NACK")))
                        {
                            gridProcedureSteps[5, rowSteps.Index].ReadOnly = false;
                            gridProcedureSteps[6, rowSteps.Index].ReadOnly = false;
                            gridProcedureSteps[7, rowSteps.Index].ReadOnly = false;

                            if (((bool)rowSteps.Cells[7].Value) == true) // Check Data Field ?
                            {
                                gridProcedureSteps[8, rowSteps.Index].ReadOnly = false;
                                gridProcedureSteps[9, rowSteps.Index].ReadOnly = false;
                                gridProcedureSteps[10, rowSteps.Index].ReadOnly = false;
                                gridProcedureSteps[12, rowSteps.Index].ReadOnly = false;
                                gridProcedureSteps[14, rowSteps.Index].ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        /**
         * Seleciona todos os valores das celulas do grd dos Procedure Steps e insere em
         * uma estrutura do tipo List de objects. Ja trata as celulas com valores nulls ou vazios.
         **/
        private List<object> GetProcedureStepsInGrid()
        {
            List<object> listSteps = new List<object>();
            List<object> listRow = null;

            foreach (DataGridViewRow rowGrid in gridProcedureSteps.Rows)
            {
                listRow = new List<object>();

                foreach (DataGridViewColumn col in gridProcedureSteps.Columns)
                {
                    int i = 0, j = 0;
                    object value = rowGrid.Cells[col.Index].Value;

                    if (value == null || value.ToString().Trim().Equals(""))
                    {
                        if (col.HeaderText.Equals("Compare To"))
                        {
                            if (rowGrid.Cells[8].ToolTipText.Equals("Boolean"))
                            {
                                listRow.Insert(col.Index, false);
                            }
                            else
                            {
                                listRow.Insert(col.Index, 0);
                            }
                        }
                    }
                    else
                    {
                        if ((col.HeaderText.Equals("Step")) ||
                            (col.HeaderText.Equals("Report Type")) ||
                            (col.HeaderText.Equals("Report Subtype")) ||
                            (col.HeaderText.Equals("Data Field")))
                        {
                            i = value.ToString().IndexOf("[");
                            j = value.ToString().IndexOf("]");

                            String valueFormated = value.ToString().Substring((i + 1), (j - 1));
                            listRow.Insert(col.Index, valueFormated);
                        }
                        else if (col.HeaderText.Contains("Verify Execution"))
                        {
                            if (!(bool)value)
                            {
                                listRow.Insert(col.Index, false);
                                listRow.Insert(4, "");
                                listRow.Insert(5, "null");
                                listRow.Insert(6, "null");
                                listRow.Insert(7, false);
                                listRow.Insert(8, "null");
                                listRow.Insert(9, "");
                                listRow.Insert(10, 0);
                                listRow.Insert(11, "Between");
                                listRow.Insert(12, 0);
                                listRow.Insert(13, "And");
                                listRow.Insert(14, 0);
                                listRow.Insert(15, "Seconds");

                                break;
                            }
                            else
                            {
                                listRow.Insert(col.Index, true);
                            }
                        }
                        else if (col.HeaderText.Contains("Verify Condition"))
                        {
                            listRow.Insert(col.Index, value.ToString());

                            if (value.ToString().ToUpper().Contains("ACK") ||
                                value.ToString().ToUpper().Contains("NACK"))
                            {
                                listRow.Insert(5, "null");
                                listRow.Insert(6, "null");
                                listRow.Insert(7, false);
                                listRow.Insert(8, "null");
                                listRow.Insert(9, "");
                                listRow.Insert(10, 0);
                                listRow.Insert(11, "Between");
                                listRow.Insert(12, rowGrid.Cells[12].Value);
                                listRow.Insert(13, "And");
                                listRow.Insert(14, rowGrid.Cells[14].Value);
                                listRow.Insert(15, "Seconds");

                                break;
                            }
                        }
                        else if (col.HeaderText.Contains("Check Data Field"))
                        {
                            if ((bool)value == true)
                            {
                                listRow.Insert(col.Index, true);
                            }
                            else
                            {
                                listRow.Insert(col.Index, false);
                                listRow.Insert(8, "null");
                                listRow.Insert(9, "");
                                listRow.Insert(10, 0);
                                listRow.Insert(11, "Between");
                                listRow.Insert(12, rowGrid.Cells[12].Value);
                                listRow.Insert(13, "And");
                                listRow.Insert(14, rowGrid.Cells[14].Value);
                                listRow.Insert(15, "Seconds");

                                break;
                            }
                        }
                        else if (col.HeaderText.Contains("Compare To"))
                        {
                            if (rowGrid.Cells[8].ToolTipText.Equals("Table") ||
                                rowGrid.Cells[8].ToolTipText.Equals("List"))
                            {
                                i = value.ToString().IndexOf("[");
                                j = value.ToString().IndexOf("]");

                                String valueFormated = value.ToString().Substring((i + 1), (j - 1));
                                listRow.Insert(col.Index, valueFormated);
                            }
                            else if (rowGrid.Cells[8].ToolTipText.Equals("Raw Hex"))
                            {
                                String valueHexString = rowGrid.Cells[col.Index].Value.ToString().Replace("-", "");
                                UInt64 valueUInt64 = Convert.ToUInt64(valueHexString, 16);
                                listRow.Insert(col.Index, valueUInt64);
                            }
                            else if (rowGrid.Cells[8].ToolTipText.Equals("Numeric"))
                            {
                                UInt64 valueUInt64 = UInt64.Parse(rowGrid.Cells[col.Index].Value.ToString());
                                listRow.Insert(col.Index, rowGrid.Cells[col.Index].Value);
                            }
                            else if (rowGrid.Cells[8].ToolTipText.Equals("Boolean"))
                            {
                                listRow.Insert(col.Index, (bool)rowGrid.Cells[col.Index].Value);
                            }
                        }
                        else if (col.HeaderText.Contains("Comparison Operation"))
                        {
                            if (value.ToString().Equals("equal to"))
                            {
                                listRow.Insert(col.Index, "=");
                            }
                            else if (value.ToString().Equals("major than"))
                            {
                                listRow.Insert(col.Index, ">");
                            }
                            else if (value.ToString().Equals("minor than"))
                            {
                                listRow.Insert(col.Index, "<");
                            }
                            else if (value.ToString().Equals("major than or equal to"))
                            {
                                listRow.Insert(col.Index, ">=");
                            }
                            else if (value.ToString().Equals("minor than or equal to"))
                            {
                                listRow.Insert(col.Index, "<=");
                            }
                            else if (value.ToString().Equals("different than"))
                            {
                                listRow.Insert(col.Index, "!=");
                            }
                        }
                        else if (col.HeaderText.Contains("Verify Condition"))
                        {
                            String valueFormatted = value.ToString().ToLower().Replace(" ", "_");
                            listRow.Insert(col.Index, value);
                        }
                        else if ((col.Index == 12) || (col.Index == 14))
                        {
                            UInt32 uInt32 = UInt32.Parse(rowGrid.Cells[col.Index].Value.ToString());
                            listRow.Insert(col.Index, uInt32);
                        }
                        else
                        {
                            listRow.Insert(col.Index, rowGrid.Cells[col.Index].Value);
                        }
                    }
                }

                listSteps.Insert(rowGrid.Index, listRow);
            }

            return listSteps;
        }

        /**
         * Valida todos os campos do modo de edicao da tela e faz uma varredura no grid 
         * a procura de celulas vazias que devem obrigatoriamente ser preenchidas.
         **/
        private bool ValidateProcedureSteps()
        {
            bool gridIsEmpty = false;

            if (!txtDescription.Text.Trim().Equals(""))
            {
                String sql = "Select isnull(count(procedure_id), 0) from test_procedures where description = dbo.f_regularString('" + txtDescription.Text.Trim() + "')";

                if (currentMode == Mode.editing)
                {
                    sql += " and procedure_id <> " + txtProcedureID.Text.Trim();
                }

                if ((int)DbInterface.ExecuteScalar(sql) == 0)
                {
                    if (!txtPurpose.Text.Trim().Equals(""))
                    {
                        // Validar o gridProcedureSteps
                        foreach (DataGridViewRow row in gridProcedureSteps.Rows)
                        {
                            // celula Step Type
                            if (gridProcedureSteps[0, row.Index].Value == null ||
                                gridProcedureSteps[0, row.Index].Value.ToString().Equals(""))
                            {
                                gridProcedureSteps[0, row.Index].Selected = true;
                                gridIsEmpty = true;
                                break;
                            }

                            // celula Step
                            if (gridProcedureSteps[1, row.Index].Value == null ||
                                gridProcedureSteps[1, row.Index].Value.ToString().Equals(""))
                            {
                                gridProcedureSteps[1, row.Index].Selected = true;
                                gridIsEmpty = true;
                                break;
                            }
                            else if (gridProcedureSteps[1, row.Index].Value.ToString().Contains("[There are no") ||
                                     gridProcedureSteps[1, row.Index].Value.ToString().Contains("[Choose"))
                            {
                                MessageBox.Show("No Step selected. Select Step type and try again.",
                                                Application.ProductName,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                gridProcedureSteps[0, row.Index].Selected = true;
                                return false;
                            }

                            // celula Delay Before Sending
                            if (gridProcedureSteps[2, row.Index].Value == null ||
                                gridProcedureSteps[2, row.Index].Value.ToString().Equals(""))
                            {
                                gridProcedureSteps[2, row.Index].Selected = true;
                                gridIsEmpty = true;
                                break;
                            }

                            // celula Verify Execution
                            if ((bool)gridProcedureSteps[3, row.Index].Value == true)
                            {
                                if (gridProcedureSteps[4, row.Index].Value == null ||
                                    gridProcedureSteps[4, row.Index].Value.ToString().Equals(""))
                                {
                                    gridProcedureSteps[4, row.Index].Selected = true;
                                    gridIsEmpty = true;
                                    break;
                                }
                                else
                                {
                                    if (!gridProcedureSteps[4, row.Index].Value.ToString().ToUpper().Contains("ACK") &&
                                        !gridProcedureSteps[4, row.Index].Value.ToString().ToUpper().Contains("NACK"))
                                    {
                                        // celula Report Type
                                        if (gridProcedureSteps[5, row.Index].Value == null ||
                                            gridProcedureSteps[5, row.Index].Value.ToString().Equals(""))
                                        {
                                            gridProcedureSteps[5, row.Index].Selected = true;
                                            gridIsEmpty = true;
                                            break;
                                        }

                                        // celula Report Subtype
                                        if (gridProcedureSteps[6, row.Index].Value == null ||
                                            gridProcedureSteps[6, row.Index].Value.ToString().Equals(""))
                                        {
                                            gridProcedureSteps[6, row.Index].Selected = true;
                                            gridIsEmpty = true;
                                            break;
                                        }
                                        else if (gridProcedureSteps[6, row.Index].Value.ToString().Contains("[There are no") ||
                                                 gridProcedureSteps[6, row.Index].Value.ToString().Contains("[Choose"))
                                        {
                                            MessageBox.Show("No Report Subtype selected. Select Report Type and try again.",
                                                            Application.ProductName,
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Exclamation);

                                            gridProcedureSteps[5, row.Index].Selected = true;
                                            return false;
                                        }

                                        if ((bool)gridProcedureSteps[7, row.Index].Value == true)
                                        {
                                            // celula Data Field
                                            if (gridProcedureSteps[8, row.Index].Value == null ||
                                                gridProcedureSteps[8, row.Index].Value.ToString().Equals(""))
                                            {
                                                gridProcedureSteps[8, row.Index].Selected = true;
                                                gridIsEmpty = true;
                                                break;
                                            }
                                            else if (gridProcedureSteps[8, row.Index].Value.ToString().Contains("[There are no") ||
                                                     gridProcedureSteps[8, row.Index].Value.ToString().Contains("[Choose"))
                                            {
                                                MessageBox.Show("No Data Field selected. Select Report Subtype and try again.",
                                                            Application.ProductName,
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Exclamation);

                                                gridProcedureSteps[6, row.Index].Selected = true;
                                                return false;
                                            }

                                            // celula Comparison Operation
                                            if (gridProcedureSteps[9, row.Index].Value == null ||
                                                gridProcedureSteps[9, row.Index].Value.ToString().Equals(""))
                                            {
                                                gridProcedureSteps[9, row.Index].Selected = true;
                                                gridIsEmpty = true;
                                                break;
                                            }

                                            // celula Compare To
                                            if (gridProcedureSteps[10, row.Index].Value == null ||
                                                gridProcedureSteps[10, row.Index].Value.ToString().Equals(""))
                                            {
                                                gridProcedureSteps[10, row.Index].Selected = true;
                                                gridIsEmpty = true;
                                                break;
                                            }
                                        }
                                    }

                                    // celula Virify Interval (os intervalos Between X and Y seconds)
                                    if (gridProcedureSteps[12, row.Index].Value.ToString().Equals("") ||
                                        gridProcedureSteps[14, row.Index].Value.ToString().Equals(""))
                                    {
                                        gridProcedureSteps[12, row.Index].Selected = true;
                                        gridIsEmpty = true;
                                        break;
                                    }
                                }
                            }

                            // verificar se os valores do between sao concistentes..
                            Int32 firstValue = Int32.Parse(gridProcedureSteps[12, row.Index].Value.ToString());
                            Int32 secondValue = Int32.Parse(gridProcedureSteps[14, row.Index].Value.ToString());

                            if (firstValue > secondValue)
                            {
                                MessageBox.Show("The verify interval of step " + (row.Index + 1) + " is invalid!\n\n Correct it and try again.",
                                                "Inconsistent Data",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation);

                                gridProcedureSteps[12, row.Index].Selected = true;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The field Purpose is empty ! \n\nFill it and try again.",
                                                            "Inconsistent Data",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Exclamation);

                        txtPurpose.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("The Test Procedure '" + txtDescription.Text.Trim() + "' already exist ! ! \n\nFill other and try again.",
                                    "Inconsistent Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    txtDescription.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("The field Description is empty ! \n\nFill it and try again.",
                                "Inconsistent Data",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                txtDescription.Focus();
                return false;
            }

            if (gridIsEmpty)
            {
                MessageBox.Show("There are empty fields in Procedure Steps. Fill it and try again.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                return false;
            }

            return true;
        }

        /**
         * Atualiza o campo Estimated Duration do Form.
         **/
        private void RefreshEstimatedDuration()
        {
            Int32 estimatedDuration = 0;
            Int32 value = 0;

            if (currentMode == Mode.browsing)
            {
                return;
            }

            foreach (DataGridViewRow row in gridProcedureSteps.Rows)
            {
                // Verificar se Delay Before Sending eh numerico
                if (!Utils.Formatting.IsNumeric(gridProcedureSteps[2, row.Index].Value.ToString().Trim()))
                {
                    MessageBox.Show("Invalid field value. A numeric value is expected.",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    // Delay Before Sending
                    value = Convert.ToInt32(gridProcedureSteps[2, row.Index].Value);
                    estimatedDuration += value;

                    // Verify Interval End
                    Int32 endValue = Int32.Parse(gridProcedureSteps[14, row.Index].Value.ToString());
                    estimatedDuration += endValue;
                }
            }

            if (chkRunInLoop.Checked)
            {
                txtEstimatedDuration.Text = (int.Parse(estimatedDuration.ToString()) * int.Parse(numLoopIterations.Value.ToString())).ToString();
            }
            else
            {
                txtEstimatedDuration.Text = estimatedDuration.ToString();
            }
        }

        public void FillTestCaseCombo()
        {
            cmbContourTCs.Items.Clear();
            DbConfiguration.Load();
            DataTable table = null;
            String baseContour = DbConfiguration.Contour_database;
            String projetoContour = DbConfiguration.Contour_project;

            //buscar document key e descricao dos casos de teste
            String str1 = @"SELECT '[' + a.documentKey + '] ' + a.name as FullDescription FROM " + baseContour + @"
                            .dbo.document a inner join " + baseContour + @"
                            .dbo.project b on a.projectId = b.id WHERE b.name = '" + projetoContour + @"' 
                            and a.documentTypeId = '26' and a.active = 'T' ORDER BY a.id";
            try
            {
                if (DbInterface.TestConnection())
                {
                    table = DbInterface.GetDataTable(str1);
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            cmbContourTCs.Items.Add(item);
                        }
                    }
                }
            }
            catch (OleDbException e)
            {
                MessageBox.Show("Transaction error: " + e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        #endregion
    }
}
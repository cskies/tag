using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Application;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    public partial class FrmExecutionTimeout : Form
    {
        #region atributos

        private RawPacket rawPacket;
        private bool embeddedPacketAlreadyEdited = false;
        int numberBits = 0;
        private object value = null;
        private bool calendar = false;

        private object valueDateHex;
        private bool isRelative;

        #endregion

        public object Value
        {
            get
            {
                return value;
            }
        }

        public bool EmbeddedPacketAlreadyEdited
        {
            get
            {
                return embeddedPacketAlreadyEdited;
            }
        }

        public bool Calendar
        {
            get
            {
                return calendar;
            }
        }

        public object ValueDateHex
        {
            get
            {
                return valueDateHex;
            }
        }

        public bool IsRelative
        {
            get
            {
                return isRelative;
            }
        }

        #region construtor

        public FrmExecutionTimeout(bool packetAlreadyEdited,
                                    int startBit,
                                    int numberOfBits,
                                    RawPacket rawPacketFromRequest)
        {
            InitializeComponent();
            numberBits = numberOfBits;
            rawPacket = rawPacketFromRequest;
        }

        #endregion

        #region metodos privados

        private object SaveTimeout()
        {
            int firstBit = 0;
            int shift = 0;

            try
            {
                object value = null;
                Int64 formattedValue = 0;

                if (!rbRelative.Checked)
                {
                    if (mskTimeSecs.Text.Equals("  :  :  :"))
                    {
                        value = dtpTimeout.Value;
                        string valueString = Regex.Replace(value.ToString(), "[^0-9]", "");
                        formattedValue = Convert.ToInt64(valueString);
                        calendar = true;
                        DateInHex(value);
                    }
                    else
                    {
                        value = Convert.ToInt32(mskTimeSecs.Text.Replace(":", ""));
                        calendar = false;
                    }
                }
                else
                {
                    isRelative = true;
                    string valueString = Regex.Replace(mskTimeSecs.Text, "[^0-9]", "");
                    value = Convert.ToUInt64(valueString);
                }

                long fieldValue = 0;
                fieldValue = formattedValue;
                byte[] part = new byte[4];
                string strippedValue = formattedValue.ToString().Replace("-", "");
                fieldValue = Convert.ToInt64(strippedValue, 16);

                return value;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                return e.Message;
            }
        }

        private object DateInHex(object value)
        {
            string calendarTime = value.ToString();

            if (calendarTime.EndsWith("."))
            {
                calendarTime += "000000";
            }

            TimeCode.LoadEpoch();
            value = TimeCode.CalendarToOnboardTime(calendarTime);
            valueDateHex = value.ToString().Replace("0", "");
            return valueDateHex;
        }

        #endregion

        public FrmExecutionTimeout()
        {
            InitializeComponent();
            mskTimeSecs.Text = @"Time: Seconds";
        }

        private void rbRelative_CheckedChanged(object sender, EventArgs e)
        {
            dtpTimeout.Enabled = rbAbsolute.Checked ? true : false;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            value = SaveTimeout();
            embeddedPacketAlreadyEdited = true;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmExecutionTimeout_Load(object sender, EventArgs e)
        {
            rbAbsolute.Checked = true;
            dtpTimeout.Format = DateTimePickerFormat.Custom;
            dtpTimeout.CustomFormat = "dd/MM/yyyy - HH:mm:ss";
        }

        private void mskTimeSecs_MouseClick(object sender, MouseEventArgs e)
        {
            if (mskTimeSecs.Tag == null)
            {
                mskTimeSecs.Tag = "1";
            }
        }

        private void mskTimeSecs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}

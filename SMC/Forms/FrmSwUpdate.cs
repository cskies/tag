/**
 * @file 	    FrmSwUpdate.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    07/11/2011
 * @note	    Modificado em 28/01/2015 por Thiago.
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
using System.IO;
using Inpe.Subord.Comav.Egse.Smc.Utils;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Packetization;
using Inpe.Subord.Comav.Egse.Smc.Ccsds.Transfer;

namespace Inpe.Subord.Comav.Egse.Smc.Forms
{
    /**
     * @class FrmSwUpdate 
     * Formulario para a preparacao de partes de software a enviar para o satelite.
     **/
    public partial class FrmSwUpdate : DockContent
    {
        // Tamanho do pacote "Large":
        //   * 1 byte para o "Unit Type"
        //   * 6 bytes para o Packet Header
        //   * 4 bytes para o Data Field Header
        //   * Large packets nao possuem CRC16!!
        private const int LARGE_PACKET_HEADER_LENGTH = 11;
        private const int TRANSFER_WINDOW_SIZE = 256; // number of parts to be sent at a time

        private byte[] imageToSend = null;
        private bool fileLoaded = false;

        public FrmSwUpdate()
        {
            InitializeComponent();
        }

        private void FrmSwUpdate_Load(object sender, EventArgs e)
        {
            DataTable table;
            String sql;

            cmbAck.SelectedIndex = 5;
            cmbOutputFormat.SelectedIndex = 0;

            // Preenche o combo serviceType
            sql = "select '[' + dbo.f_zero(service_type, 3) + '] ' + service_name as name from services order by service_type";

            table = DbInterface.GetDataTable(sql);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    cmbServiceType.Items.Add(row[0]);
                }
            }

            numSSC.Value = 1;

            DbConfiguration.Load();

            numSwMajor.Value = DbConfiguration.FlightSwVersionMajor;
            numSwMinor.Value = DbConfiguration.FlightSwVersionMinor;
            numSwPatch.Value = DbConfiguration.FlightSwVersionPatch;
            numLduId.Value = DbConfiguration.LargeDataLduId;
            numPartsLength.Value = DbConfiguration.LargeDataPartsLength;
            numSSC.Value = DbConfiguration.LargeDataSequenceCount;
            cmbAck.SelectedIndex = DbConfiguration.LargeDataAck;

            cmbServiceType.SelectedIndex = cmbServiceType.FindString(Utils.Formatting.FormatCode(DbConfiguration.LargeDataServiceType, 3));
            cmbServiceSubtype.SelectedIndex = cmbServiceSubtype.FindString(Utils.Formatting.FormatCode(DbConfiguration.LargeDataServiceSubtype, 3));

            numDelay.Value = DbConfiguration.SwaplUpdateDelayBetweenTcs;

            chkHasImageLength.Enabled = false;
            chkHasImageCrc.Enabled = false;
            btSaveNewImageFile.Enabled = false;

            if (DbConfiguration.FlightSwPartsOutputFormat == "parts")
            {
                cmbOutputFormat.SelectedIndex = 0;
            }
            else if (DbConfiguration.FlightSwPartsOutputFormat == "packets")
            {
                cmbOutputFormat.SelectedIndex = 1;
            }
            else if (DbConfiguration.FlightSwPartsOutputFormat == "cltus")
            {
                cmbOutputFormat.SelectedIndex = 2;
            }
            else // satcs
            {
                cmbOutputFormat.SelectedIndex = 3;
            }

            txtScriptPath.Text = DbConfiguration.FlightSwTestControlScriptPath;
            chkTestControlFormat.Checked = DbConfiguration.FlightSwScriptForTestControl;
        }

        private void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceType.Items.Count == 0)
            {
                return;
            }

            if (cmbServiceType.SelectedIndex == -1)
            {
                // isso so acontece caso nao tenha sido salvo, nas configuracoes do
                // SMC, um servico padrao a carregar na abertura desta tela
                return;
            }

            cmbServiceSubtype.Items.Clear();

            String sql = "";
            int serviceType = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));

            // f_zero eh uma user-defined function (user dbo) na base Simulador_UTMC
            sql = @"select '[' + dbo.f_zero(service_subtype, 3) + '] ' + description as subtype_description 
                    from subtypes where service_type = " + serviceType.ToString() + " and is_request = 1 order by service_subtype";

            DataTable table = DbInterface.GetDataTable(sql);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    cmbServiceSubtype.Items.Add(row[0]);
                }
            }
            else
            {
                cmbServiceSubtype.Items.Add("[There are no requests available for this service]");
            }

            cmbServiceSubtype.SelectedIndex = 0;
        }

        private void btSaveConfiguration_Click(object sender, EventArgs e)
        {
            DbConfiguration.Load();

            DbConfiguration.FlightSwVersionMajor = (int)numSwMajor.Value;
            DbConfiguration.FlightSwVersionMinor = (int)numSwMinor.Value;
            DbConfiguration.FlightSwVersionPatch = (int)numSwPatch.Value;
            DbConfiguration.LargeDataLduId = (int)numLduId.Value;
            DbConfiguration.LargeDataPartsLength = (int) numPartsLength.Value;
            DbConfiguration.LargeDataSequenceCount = (int) numSSC.Value;
            DbConfiguration.LargeDataAck = cmbAck.SelectedIndex;
            DbConfiguration.LargeDataServiceType = int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3));
            DbConfiguration.LargeDataServiceSubtype = int.Parse(cmbServiceSubtype.SelectedItem.ToString().Substring(1, 3));;
            DbConfiguration.SwaplUpdateDelayBetweenTcs = int.Parse(numDelay.Value.ToString());

            switch (cmbOutputFormat.SelectedIndex)
            {
                case 0:
                {
                    DbConfiguration.FlightSwPartsOutputFormat = "parts";
                    break;
                }
                case 1:
                {
                    DbConfiguration.FlightSwPartsOutputFormat = "packets";
                    break;
                }
                case 2:
                {
                    DbConfiguration.FlightSwPartsOutputFormat = "cltus";
                    break;
                }
                default:
                {
                    DbConfiguration.FlightSwPartsOutputFormat = "satcs";
                    DbConfiguration.FlightSwScriptForTestControl = chkTestControlFormat.Checked;
                    DbConfiguration.FlightSwTestControlScriptPath = txtScriptPath.Text;
                    break;
                }
            }

            if (DbConfiguration.Save() == true)
            {
                MessageBox.Show("Configuration saved successfully!",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void btOpenImageFile_Click(object sender, EventArgs e)
        {
            FileStream fileStream = null;
            BinaryReader binaryReader = null;
            UInt32 imageLength = 0;
            Int32 imageCrc = 0;
            Int32 calculatedCrc = 0;

            fileLoaded = false;

            try
            {
                // usar o Flight SW Export Default Path, no XML (tanto para a imagem como para a exportacao)
                fileDialog.InitialDirectory = Properties.Settings.Default.flight_sw_file_path;
                fileDialog.Title = "Select a Flight Software Image File";
                fileDialog.Filter = "All Files (*.*)|*.*";
                fileDialog.FileName = "*.*";
                fileDialog.FilterIndex = 0;

                if (fileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                txtInputFilePath.Text = fileDialog.FileName;
                txtImageCrc.Text = "";

                fileStream = new System.IO.FileStream(fileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // anexa o filestream ao binary reader
                binaryReader = new System.IO.BinaryReader(fileStream);

                // read entire file into buffer
                imageToSend = binaryReader.ReadBytes((int)fileStream.Length);

                // verifica se o tamanho esta no arquivo
                imageLength = (UInt32) ((UInt32)imageToSend[0] << 24) | 
                                       ((UInt32)imageToSend[1] << 16) | 
                                       ((UInt32)imageToSend[2] << 8) | 
                                        (UInt32)imageToSend[3];

                if (imageLength == (fileStream.Length - 4))
                {
                    chkHasImageLength.Checked = true;
                    chkHasImageCrc.Checked = false;
                }
                else if (imageLength == (fileStream.Length - 8))
                {
                    chkHasImageLength.Checked = true;
                    chkHasImageCrc.Checked = true;
                }
                else
                {
                    chkHasImageLength.Checked = false;
                    
                    // assumo que o arquivo passado eh apenas a imagem
                    imageLength = (UInt32)fileStream.Length;
                }

                txtImageLength.Text = String.Format("{0:n0}", imageLength) + " bytes";

                if (chkHasImageLength.Checked == true)
                {
                    txtImageLength.ForeColor = Color.ForestGreen;
                }
                else
                {
                    txtImageLength.ForeColor = Color.Red;
                }

                if (chkHasImageCrc.Checked == true)
                {
                    // verifica se o crc esta no arquivo
                    imageCrc = (Int32)((Int32)imageToSend[imageToSend.Length - 4] << 24) |
                                       ((Int32)imageToSend[imageToSend.Length - 3] << 16) |
                                       ((Int32)imageToSend[imageToSend.Length - 2] << 8) |
                                        (Int32)imageToSend[imageToSend.Length - 1];

                    this.Cursor = Cursors.WaitCursor;

                    // TODO: ao implementar o update de SW para o COMAV, devo passar a selecionar qual
                    //       CRC aplicar na interface, CRC32 (real) ou o CRC Amazonia-1
                    if (chkHasImageLength.Checked == true)
                    {
                        // NAO APAGAR A LINHA COMENTADA ABAIXO!!!
                        //calculatedCrc = CheckingCodes.Crc32(ref imageToSend, imageLength, 4);
                        calculatedCrc = CheckingCodes.CrcAmazonia1(ref imageToSend, imageLength, 4);
                    }
                    else
                    {
                        // NAO APAGAR A LINHA COMENTADA ABAIXO!!!
                        //calculatedCrc = CheckingCodes.Crc32(ref imageToSend, (UInt32)imageToSend.Length - 4, 0);
                        calculatedCrc = CheckingCodes.CrcAmazonia1(ref imageToSend, (UInt32)imageToSend.Length - 4, 0);
                    }

                    this.Cursor = Cursors.Default;

                    txtImageCrc.Text = imageCrc.ToString("X").ToUpper();
                    txtImageCrc.Text = txtImageCrc.Text.Substring(0, 2) + "-" + txtImageCrc.Text.Substring(2, 2) + "-" +
                                       txtImageCrc.Text.Substring(4, 2) + "-" + txtImageCrc.Text.Substring(6, 2);

                    if (imageCrc == calculatedCrc)
                    {
                        txtImageCrc.ForeColor = Color.ForestGreen;
                    }
                    else
                    {
                        txtImageCrc.ForeColor = Color.Red;
                    }
                }

                chkHasImageLength.Enabled = true;
                chkHasImageCrc.Enabled = true;
                btSaveNewImageFile.Enabled = true;

                txtNumberOfParts.Text = Math.Ceiling((imageToSend.Length + LARGE_PACKET_HEADER_LENGTH) / (numPartsLength.Value - 4)).ToString();

                // Fecha o arquivo
                fileStream.Close();
                fileStream.Dispose();
                binaryReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            fileLoaded = true;
        }

        private void btSaveParts_Click(object sender, EventArgs e)
        {
            try
            {
                // se houver inconsistencia do tamanho e/ou CRC, exibe uma mensagem ao usuario, perguntando 
                // se ele quer enviar mesmo assim (ele pode estar querendo testar crc invalido, por exemplo)
                if ((txtImageLength.ForeColor == Color.Red) ||
                    (txtImageCrc.ForeColor == Color.Red))
                {
                    if (MessageBox.Show("The CRC-32 or image lenght are wrong or missing in the image to be sent.\n\n" +
                                        "Do you whish to save the parts files anyway?",
                                   "Invalid Flight Software Image",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                byte[] largePacketHeader = new byte[LARGE_PACKET_HEADER_LENGTH];
                byte ack;
                int numberOfParts = int.Parse(txtNumberOfParts.Text);
                int partsLength = (int)numPartsLength.Value;
                String fileName;
                FileStream fileStream = null; // apenas para evitar warnings
                TextWriter textFile = null; // apenas para evitar warnings
                String partName = "";
                RawPacket packet = new RawPacket(true, false);
                byte[] cltu = new byte[1]; // sera redimensionado
                byte cltuCounter = 0;

                DbConfiguration.Load();

                folderDialog.SelectedPath = Properties.Settings.Default.flight_sw_file_path;
                folderDialog.Description = "Select the folder to save the parts files.";

                if (folderDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // monta o cabecalho do pacote "large"
                largePacketHeader[0] = 1; // extended packet
                largePacketHeader[1] = 0x18;

                largePacketHeader[2] = (byte)DbConfiguration.RequestsDefaultApid; // APID

                largePacketHeader[3] = (byte)(0xc0 | ((UInt32)(numSSC.Value)) >> 8); // sequence, primeiro byte
                largePacketHeader[4] = (byte)(((UInt32)(numSSC.Value)) & 0xff); // sequence, segundo byte

                largePacketHeader[5] = 0; // de acordo com o PUS, o Packet Length de pacotes extendidos eh zero
                largePacketHeader[6] = 0;

                switch (cmbAck.SelectedIndex)
                {
                    case 0: // none
                    {
                        ack = 0;
                        break;
                    }
                    case 1: // acceptance
                    {
                        ack = 1;
                        break;
                    }
                    case 2: // start (invalid)
                    {
                        ack = 2;
                        break;
                    }
                    case 3: // progress (invalid)
                    {
                        ack = 4;
                        break;
                    }
                    case 4: // completion
                    {
                        ack = 8;
                        break;
                    }
                    default: // acceptance + completion
                    {
                        ack = 9;
                        break;
                    }
                }

                largePacketHeader[7] = (byte) (0x10 | ack); // ack
                largePacketHeader[8] = (byte) int.Parse(cmbServiceType.SelectedItem.ToString().Substring(1, 3)); // service type
                largePacketHeader[9] = (byte) int.Parse(cmbServiceSubtype.SelectedItem.ToString().Substring(1, 3)); // service subtype

                largePacketHeader[10] = (byte)DbConfiguration.RequestsDefaultSourceId; // source id

                // anexo o packet header e a imagem em um unico array com o large packet completo
                byte[] largePacket = new byte[imageToSend.Length + largePacketHeader.Length];
                byte[] part = new byte[partsLength];
                byte[] satcsPart = new byte[partsLength - 4];

                Array.Copy(largePacketHeader, 0, largePacket, 0, largePacketHeader.Length);
                Array.Copy(imageToSend, 0, largePacket, largePacketHeader.Length, imageToSend.Length);

                for (int i = 0; i < numberOfParts; i++)
                {
                    // copia o LDU ID para a parte
                    part[0] = (byte)(((UInt32)(numLduId.Value)) >> 8);
                    part[1] = (byte)(((UInt32)(numLduId.Value)) & 0xff);

                    // copia a sequencia (i + 1) para a parte
                    part[2] = (byte)(((UInt32)(i + 1)) >> 8);
                    part[3] = (byte)(((UInt32)(i + 1)) & 0xff);

                    // copia a parte do SW
                    if (i < (numberOfParts - 1))
                    {
                        Array.Copy(largePacket, (i * (partsLength - 4)), part, 4, (partsLength - 4));
                    }
                    else // ultima parte, pode ser menor
                    {
                        int whatIsLeft = largePacket.Length - (i * (partsLength - 4));
                        Array.Copy(largePacket, (i * (partsLength - 4)), part, 4, whatIsLeft);
                        Array.Resize(ref part, whatIsLeft + 4);
                    }

                    switch (cmbOutputFormat.SelectedIndex)
                    {
                        case 0: // SDU
                        {
                            partName = "sdu_part";
                            break;
                        }
                        case 1: // Packet
                        {
                            partName = "packet";
                            break;
                        }
                        case 2: // CLTU
                        {
                            partName = "cltu";
                            break;
                        }
                        default: // SATCS
                        {
                            partName = "satcs";
                            break;
                        }
                    }

                    // define o nome do arquivo e o salva
                    fileName = "\\obdh_sw_image_" + numSwMajor.Value.ToString() + "." +
                                                   numSwMinor.Value.ToString() + "." +
                                                   numSwPatch.Value.ToString() + "_" + partName + "_" +
                                                   Formatting.FormatCode(i + 1, 4).Replace("[", "").Replace("]", "");

                    // tenho a parte, agora defino o formato de exportação
                    if (cmbOutputFormat.SelectedIndex > 0)
                    {
                        // a saida eh pacote ou CLTU

                        byte[] packetPart = new byte[2];
                        
                        // insere o SDU em um pacote
                        if (i == 0)
                        {
                            // primeira parte
                            packet = new RawPacket(13, 9);
                        }
                        else if (i < (numberOfParts - 1))
                        {
                            // parte intermediaria
                            packet = new RawPacket(13, 10);
                        }
                        else
                        {
                            // parte final
                            packet = new RawPacket(13, 11);
                        }

                        // como o tamanho da parte eh variavel e definido pelo usuario, eh necessario 
                        // redimensionar; o campo packet length eh automaticamente recalculado
                        packet.Resize((UInt16)(12 + part.Length));

                        // seta o APID
                        packetPart[0] = (byte)DbConfiguration.RequestsDefaultApid;
                        packet.SetPart(8, 8, packetPart);

                        // seta o Destination ID
                        packetPart[0] = (byte)DbConfiguration.RequestsDefaultSourceId;
                        packet.SetPart(72, 8, packetPart);
                        
                        // seta a sequencia do pacote (i + 1)
                        packetPart[0] = (byte)(0xc0 | ((UInt32)(i + 1)) >> 8);
                        packetPart[1] = (byte)(((UInt32)(i + 1)) & 0xff);
                        
                        packet.SetPart(16, 16, packetPart);
 
                        // copia a parte no data field (inteiro)
                        byte[] rawPacket = new byte[packet.RawContents.Length];
                        Array.Copy(packet.RawContents, rawPacket, packet.RawContents.Length);
                        Array.Copy(part, 0, rawPacket, 10, part.Length);
                        packet.RawContents = rawPacket;

                        // para atualizar o CRC...
                        packetPart[0] = (byte)packet.GetPart(0, 8);
                        packet.SetPart(0, 8, packetPart);
                    }

                    if (cmbOutputFormat.SelectedIndex > 1)
                    {
                        // a saida eh CLTU, coloca o pacote em um frame
                        cltu = GenerateCltu(packet.RawContents, cltuCounter);

                        cltuCounter++; // estoura em 256, zera
                    }

                    if (cmbOutputFormat.SelectedIndex <= 2)
                    {
                        fileStream = new System.IO.FileStream(folderDialog.SelectedPath + fileName + ".dat",
                                                              System.IO.FileMode.Create,
                                                              System.IO.FileAccess.Write);
                    }
                    else
                    {
                        textFile = new StreamWriter(folderDialog.SelectedPath + fileName + ".txt");
                    }

                    switch (cmbOutputFormat.SelectedIndex)
                    {
                        case 0: // SDU
                        {
                            fileStream.Write(part, 0, part.Length);
                            break;
                        }
                        case 1: // Packet
                        {
                            fileStream.Write(packet.RawContents, 0, packet.RawContents.Length);
                            break;
                        }
                        case 2: // CLTU
                        {
                            fileStream.Write(cltu, 0, cltu.Length);
                            break;
                        }
                        default: // SATCS
                        {
                            // IMPORTANTE: a geracao de arquivos depende do formato configurado
                            // para os comandos na base do SATCS. O que eh gerado aqui reflete a
                            // configuracao da base em nov/2011 para o Amazonia-1.
                            if (i == 0) // first part
                            {
                                textFile.WriteLine("Command Id=TcServ13-9");
                                textFile.WriteLine("pLDUnitId=" + numLduId.Value.ToString());
                                // a primeira parte nao precisa que a sequencia seja informada ao SATCS
                            }
                            else if (i < (numberOfParts - 1)) // intermediate part
                            {
                                textFile.WriteLine("Command Id=TcServ13-10");
                                textFile.WriteLine("pLDUnitId=" + numLduId.Value.ToString());
                                textFile.WriteLine("pLDUnitSeqNum=" + (i + 1).ToString());
                            }
                            else // last part
                            {
                                Array.Resize(ref satcsPart, part.Length - 4);

                                textFile.WriteLine("Command Id=TcServ13-11");
                                textFile.WriteLine("pLDUnitId=" + numLduId.Value.ToString());
                                textFile.WriteLine("pLDUnitSeqNum=" + (i + 1).ToString());
                                textFile.WriteLine("pLDUByteLength=" + satcsPart.Length.ToString());
                            }

                            Array.Copy(part, 4, satcsPart, 0, satcsPart.Length);
                            
                            textFile.WriteLine("pLDUnitPart=0x" + Formatting.ConvertByteArrayToHexString(satcsPart, satcsPart.Length));
                            textFile.Close();

                            break;
                        }
                    }

                    if (cmbOutputFormat.SelectedIndex <= 2)
                    {
                        fileStream.Close();
                    }
                    else
                    {
                        textFile.Close();
                    }
                } // for (int i = 0; i < numberOfParts; i++)

                if (cmbOutputFormat.SelectedIndex == 3)
                {
                    // exportacao para o SATCS; agora preciso gerar os arquivos de scripts para enviar as janelas
                    double decimalNumberOfWindows = numberOfParts / (double)TRANSFER_WINDOW_SIZE;
                    int numberOfWindows = (int)Math.Ceiling(decimalNumberOfWindows);
                    int sendingPart = 0;

                    for (int i = 1; i <= numberOfWindows; i++)
                    {
                        fileName = "\\satcs_script_sw_update_" + numSwMajor.Value.ToString() + "." +
                                                   numSwMinor.Value.ToString() + "." +
                                                   numSwPatch.Value.ToString() + "_parts_" + 
                                                   ((i * TRANSFER_WINDOW_SIZE) - TRANSFER_WINDOW_SIZE + 1).ToString() + 
                                                   "_to_";

                        if (i < numberOfWindows)
                        {
                            fileName += (i * TRANSFER_WINDOW_SIZE).ToString() + ".tcf"; // test control file
                        }
                        else
                        {
                            fileName += txtNumberOfParts.Text + ".tcf"; // test control file
                        }

                        textFile = new StreamWriter(folderDialog.SelectedPath + fileName);

                        // um arquivo de script por janela
                        for (int j = 0; j < TRANSFER_WINDOW_SIZE; j++)
                        {
                            sendingPart++;

                            if (sendingPart == 1) // first part
                            {
                                textFile.Write("TcServ13-9;");
                            }
                            else if (sendingPart == int.Parse(txtNumberOfParts.Text))// last part
                            {
                                textFile.Write("TcServ13-11;");
                            }
                            else // intermediate part
                            {
                                textFile.Write("TcServ13-10;");
                            }

                            // header
                            textFile.Write("EB9001");

                            // path do arquivo de telecomando
                            String commandPath = txtScriptPath.Text.Replace('\\', '/').Trim();
                            String commandFileName = "";
                            String commandString = "";

                            if (!commandPath.Equals(""))
                            {
                                // se necessario, adiciona uma '/' ao fim do path
                                if (!commandPath.Substring(txtScriptPath.Text.Length - 1, 1).Equals("/"))
                                {
                                    commandPath += "/"; // atencao: nao eh barra invertida!!
                                }
                            }

                            commandFileName = "fileName=" + commandPath + "obdh_sw_image_" + numSwMajor.Value.ToString() + "." +
                                                        numSwMinor.Value.ToString() + "." +
                                                        numSwPatch.Value.ToString() + "_satcs_" +
                                                        Formatting.FormatCode(sendingPart, 4).Replace("[", "").Replace("]", "") +
                                                        ".txt";

                            // data length
                            int satcsDataLength = 62 + commandPath.Length; // 16 de command ID + 35 de nome do arquivo + 2 de STX + 9 de 'fileName=' + tamanho do diretorio

                            textFile.Write("00" + Formatting.ConvertIntToHexString(satcsDataLength));

                            // command id, 16 caracteres completados com null char
                            if (sendingPart == 1) // first part
                            {
                                commandString = "TcServ13-9\0\0\0\0\0\0";
                            }
                            else if (sendingPart == int.Parse(txtNumberOfParts.Text))// last part
                            {
                                commandString = "TcServ13-11\0\0\0\0\0";
                            }
                            else // intermediate part
                            {
                                commandString = "TcServ13-10\0\0\0\0\0";
                            }

                            // file name
                            if (chkTestControlFormat.Checked == false)
                            {
                                commandString += commandFileName;
                                textFile.Write(commandString);
                            }
                            else
                            {
                                // Deus sabe o porque temos que escrever esses bytes para que funcione... (02 eh o char ascii para 'start text')
                                commandString += "\x02\0" + commandFileName;

                                // converte a commandString, caractere a caractere, em sua representacao hexa, e escrever no arquivo
                                // isso eh meio vergonhoso de se fazer, mas se deve a uma implementacao errada no SW Test Control da INVAP
                                textFile.Write(Formatting.ConvertAsciiStringToHexString(commandString));
                            }
                            
                            // end of instruction
                            textFile.Write(";SATCS");
                            textFile.WriteLine();

                            if (numDelay.Value > 0)
                            {
                                // o usuario pediu um delay entre comandos; inserir
                                textFile.WriteLine("Delay;" + numDelay.Value.ToString() + ";SATCS");
                            }
                            
                            if (sendingPart == int.Parse(txtNumberOfParts.Text))// last part
                            {
                                break;
                            }
                        }

                        textFile.Close();
                    }
                } // if (cmbOutputFormat.SelectedIndex == 3)

                // a informacao de versao eh salva automaticamente
                DbConfiguration.Load();
                DbConfiguration.FlightSwVersionMajor = (int)numSwMajor.Value;
                DbConfiguration.FlightSwVersionMinor = (int)numSwMinor.Value;
                DbConfiguration.FlightSwVersionPatch = (int)numSwPatch.Value;
                DbConfiguration.Save();

                MessageBox.Show("Flight Software Image (" + partName + ") files saved successfully!",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }

        private void btSendParts_Click(object sender, EventArgs e)
        {
            // este comando independe do output format, ele compoe e envia pacotes.
            // a tela de conexao gerencia o nivel do protocolo a ser utilizado no envio
        }

        private void chkHasImageLength_CheckedChanged(object sender, EventArgs e)
        {
            if (fileLoaded == false)
            {
                return;
            }

            if (chkHasImageLength.Checked == true)
            {
                // o usuario quer adicionar o image length
                byte[] copyArray = new byte[imageToSend.Length + 4];
                UInt32 newLength;

                Array.Copy(imageToSend, 0, copyArray, 4, imageToSend.Length);
                Array.Resize(ref imageToSend, copyArray.Length);
                Array.Copy(copyArray, imageToSend, copyArray.Length);

                txtImageLength.ForeColor = Color.ForestGreen;

                if (chkHasImageCrc.Checked == true)
                {
                    newLength = (UInt32)imageToSend.Length - 8;
                }
                else
                {
                    newLength = (UInt32)imageToSend.Length - 4;
                }

                txtImageLength.Text = String.Format("{0:n0}", newLength) + " bytes";
                imageToSend[0] = (byte)((newLength >> 24) & 0xff);
                imageToSend[1] = (byte)((newLength >> 16) & 0xff);
                imageToSend[2] = (byte)((newLength >> 8) & 0xff);
                imageToSend[3] = (byte)((newLength) & 0xff);
            }
            else
            {
                // o usuario quer remover o image length
                byte[] copyArray = new byte[imageToSend.Length - 4];
                
                Array.Copy(imageToSend, 4, copyArray, 0, imageToSend.Length - 4);
                Array.Resize(ref imageToSend, imageToSend.Length - 4);
                Array.Copy(copyArray, imageToSend, copyArray.Length);

                txtImageLength.ForeColor = Color.Red;

                if (chkHasImageCrc.Checked == true)
                {
                    txtImageLength.Text = String.Format("{0:n0}", imageToSend.Length - 4) + " bytes";
                }
                else
                {
                    txtImageLength.Text = String.Format("{0:n0}", imageToSend.Length) + " bytes";
                }
            }

            txtNumberOfParts.Text = Math.Ceiling((imageToSend.Length + LARGE_PACKET_HEADER_LENGTH) / (numPartsLength.Value - 4)).ToString();
        }

        private void chkHasImageCrc_CheckedChanged(object sender, EventArgs e)
        {
            if (fileLoaded == false)
            {
                return;
            }

            if (chkHasImageCrc.Checked == true)
            {
                byte[] copyArray = new byte[imageToSend.Length + 4];
                Int32 newCrc;

                // o usuario quer adicionar o CRC
                Array.Copy(imageToSend, 0, copyArray, 0, imageToSend.Length);
                Array.Resize(ref imageToSend, copyArray.Length);
                Array.Copy(copyArray, imageToSend, copyArray.Length);

                txtImageCrc.ForeColor = Color.ForestGreen;

                // TODO: ao implementar o update de SW para o COMAV, devo passar a selecionar qual
                //       CRC aplicar na interface, CRC32 (real) ou o CRC Amazonia-1
                if (chkHasImageLength.Checked == true)
                {
                    // NAO APAGAR A LINHA COMENTADA ABAIXO!!!
                    // newCrc = CheckingCodes.Crc32(ref imageToSend, (UInt32)(imageToSend.Length - 8), 4);
                    newCrc = CheckingCodes.CrcAmazonia1(ref imageToSend, (UInt32)(imageToSend.Length - 8), 4);
                }
                else
                {
                    // NAO APAGAR A LINHA COMENTADA ABAIXO!!!
                    // newCrc = CheckingCodes.Crc32(ref imageToSend, (UInt32)(imageToSend.Length - 4), 0);
                    newCrc = CheckingCodes.CrcAmazonia1(ref imageToSend, (UInt32)(imageToSend.Length - 4), 0);
                }

                txtImageCrc.Text = newCrc.ToString("X").ToUpper();
                txtImageCrc.Text = txtImageCrc.Text.Substring(0, 2) + "-" + txtImageCrc.Text.Substring(2, 2) + "-" +
                                   txtImageCrc.Text.Substring(4, 2) + "-" + txtImageCrc.Text.Substring(6, 2);

                imageToSend[imageToSend.Length - 4] = (byte)((newCrc >> 24) & 0xff);
                imageToSend[imageToSend.Length - 3] = (byte)((newCrc >> 16) & 0xff);
                imageToSend[imageToSend.Length - 2] = (byte)((newCrc >> 8) & 0xff);
                imageToSend[imageToSend.Length - 1] = (byte)((newCrc) & 0xff);
            }
            else
            {
                // o usuario quer remover o CRC
                byte[] copyArray = new byte[imageToSend.Length - 4];
                Int32 newCrc;

                Array.Resize(ref imageToSend, imageToSend.Length - 4);

                txtImageCrc.ForeColor = Color.Red;

                // TODO: ao implementar o update de SW para o COMAV, devo passar a selecionar qual
                //       CRC aplicar na interface, CRC32 (real) ou o CRC Amazonia-1
                if (chkHasImageLength.Checked == true)
                {
                    // NAO APAGAR A LINHA COMENTADA ABAIXO!!!
                    // newCrc = CheckingCodes.Crc32(ref imageToSend, (UInt32)(imageToSend.Length - 4), 4);
                    newCrc = CheckingCodes.CrcAmazonia1(ref imageToSend, (UInt32)(imageToSend.Length - 4), 4);
                }
                else
                {
                    // NAO APAGAR A LINHA COMENTADA ABAIXO!!!
                    // newCrc = CheckingCodes.Crc32(ref imageToSend, (UInt32)(imageToSend.Length), 0);
                    newCrc = CheckingCodes.CrcAmazonia1(ref imageToSend, (UInt32)(imageToSend.Length), 0);
                }

                txtImageCrc.Text = newCrc.ToString("X").ToUpper();
                txtImageCrc.Text = txtImageCrc.Text.Substring(0, 2) + "-" + txtImageCrc.Text.Substring(2, 2) + "-" +
                                   txtImageCrc.Text.Substring(4, 2) + "-" + txtImageCrc.Text.Substring(6, 2);
           }

            txtNumberOfParts.Text = Math.Ceiling((imageToSend.Length + LARGE_PACKET_HEADER_LENGTH) / (numPartsLength.Value - 4)).ToString();
        }

        private void numPartsLength_ValueChanged(object sender, EventArgs e)
        {
            if (fileLoaded == false)
            {
                return;
            }

            txtNumberOfParts.Text = Math.Ceiling((imageToSend.Length + LARGE_PACKET_HEADER_LENGTH) / (numPartsLength.Value - 4)).ToString();
        }

        private void btSaveNewImageFile_Click(object sender, EventArgs e)
        {
            try
            {
                String fileName;

                fileName = "obdh_sw_image_" + numSwMajor.Value.ToString() + "." +
                                              numSwMinor.Value.ToString() + "." +
                                              numSwPatch.Value.ToString() + ".dat";

                // usar o Flight SW Export Default Path, no XML (tanto para a imagem como para a exportacao)
                saveDialog.InitialDirectory = Properties.Settings.Default.flight_sw_file_path;
                saveDialog.Title = "Save a New Flight Software Image File";
                saveDialog.Filter = "All Files (*.*)|*.*";
                saveDialog.FileName = fileName;
                saveDialog.FilterIndex = 0;

                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                FileStream fileStream = new System.IO.FileStream(saveDialog.FileName, 
                                                                 System.IO.FileMode.Create, 
                                                                 System.IO.FileAccess.Write);
                fileStream.Write(imageToSend, 0, imageToSend.Length);
                fileStream.Close();

                MessageBox.Show("Flight Software Image File saved successfully!",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nSource: " + ex.Source, 
                                "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /** 
         * Cria uma CLTU para envio, a partir dos dados recebidos. Gerencia o controle de sequencia.
         * 
         * O argumento virtualChannel so eh usado se isCommandToFarm = true;
         * Caso se queira mandar um comando ao Farm respeitando o VC gravado em
         * DbConfiguration, virtualChannel deve ser menor que zero.
         **/
        private byte[] GenerateCltu(byte[] frameDataField, byte frameSequence)
        {
            RawFrame frameTc = new RawFrame(true);
            byte[] part;
            byte[] cltu;
            int whichVc; // usado para o controle de sequencia

            // redimensiona o frame para o tamanho correto (8 eh o frame header + 1 de segment header + CRC)
            frameTc.Resize(8 + frameDataField.Length);

            part = new byte[1];

            // Version number
            part[0] = (byte)DbConfiguration.TcFrameVersion;
            part[0] = (byte)(part[0] << 6);
            frameTc.SetPart(0, 2, part);

            // Frame type
            part[0] = (byte)DbConfiguration.TcFrameType;

            part[0] = (byte)(part[0] << 4);
            frameTc.SetPart(2, 2, part);

            // Reserved field A
            part[0] = (byte)(DbConfiguration.TcFrameReservedA << 2);
            frameTc.SetPart(4, 2, part);

            // Spacecraft id
            part = new byte[2];
            part[0] = (byte)(DbConfiguration.TcFrameScid >> 6);
            part[1] = (byte)(DbConfiguration.TcFrameScid & 0xff);
            frameTc.SetPart(6, 10, part);

            // Virtual channel id
            part = new byte[1];
            part[0] = (byte)(DbConfiguration.TcFrameVcid << 2);
            whichVc = DbConfiguration.TcFrameVcid;

            frameTc.SetPart(16, 6, part);

            // Reserved field B
            part[0] = (byte)(DbConfiguration.TcFrameReservedB);
            frameTc.SetPart(22, 2, part);

            // Frame sequence
            part[0] = frameSequence;
            frameTc.SetPart(32, 8, part);

            // Segment header: sequence flags
            part[0] = (byte)(DbConfiguration.TcFrameSeqFlags);

            part[0] = (byte)(part[0] << 6);
            frameTc.SetPart(40, 2, part);

            // Segment header: map id
            part[0] = (byte)(DbConfiguration.TcFrameMapid);
            frameTc.SetPart(42, 6, part);

            // os pacotes comecam no bit 48
            frameTc.SetPart(48, (frameDataField.Length * 8), frameDataField);
            
            // Agora recria a CLTU
            int cltuSize = (int)(Math.Ceiling((double)(frameTc.Size / (double)7)));
            cltuSize = (cltuSize * 8) + 10;
            cltu = new byte[cltuSize];

            // Start sequence
            cltu[0] = 0xeb;
            cltu[1] = 0x90;

            int nextCltuIndex = 2;
            int nextFrameIndex = 0;

            byte errorControl = 0;

            // Monta os codeblocks
            for (nextFrameIndex = 0; nextFrameIndex < frameTc.Size; nextFrameIndex += 7)
            {
                byte[] information = new byte[7];

                // Copia a informacao do codeblock para a cltu
                for (int j = 0; j < 7; j++)
                {
                    if ((nextFrameIndex + j) < frameTc.Size)
                    {
                        // Informacao
                        cltu[nextCltuIndex + j] = frameTc.RawContents[nextFrameIndex + j];
                    }
                    else
                    {
                        // Fill octect
                        cltu[nextCltuIndex + j] = 0x55;
                    }

                    // Prepara o vetor de 7 bytes para passar ao BCH
                    information[j] = cltu[nextCltuIndex + j];
                }

                if (CheckingCodes.Bch6356(information, ref errorControl))
                {
                    cltu[nextCltuIndex + 7] = errorControl;
                }
                else
                {
                    // Este erro nao deve ocorrer, mas...
                    MessageBox.Show("Error when creating the CLTU!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                nextCltuIndex += 8;
            }

            // Agora adiciona o tail sequence
            for (int i = 0; i < 8; i++)
            {
                cltu[nextCltuIndex + i] = 0x55; // fill octect
            }

            return cltu;
        }

        private void cmbOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOutputFormat.SelectedIndex == 3)
            {
                lblScriptPath.Enabled = true;
                txtScriptPath.Enabled = true;
                chkTestControlFormat.Enabled = true;
                lblDelay.Enabled = true;
                numDelay.Enabled = true;
                lblMs.Enabled = true;
            }
            else
            {
                lblScriptPath.Enabled = false;
                txtScriptPath.Enabled = false;
                chkTestControlFormat.Enabled = false;
                lblDelay.Enabled = false;
                numDelay.Enabled = false;
                lblMs.Enabled = false;
            }
        }
    } // class
} // namespace

using LabProgram.DAL;
using LabProgram.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LabProgram
{
    public partial class ReadControlCardFRM : Form
    {
        log4net.ILog log;
        DataTable table;
        string[] ports = SerialPort.GetPortNames();
        public ReadControlCardFRM()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            InitializeComponent();
            if(ports.Length > 0)
            {
                cboPort.Items.AddRange(ports);
                cboPort.SelectedIndex = 0; 
            }
        }
        //public void load_grid()
        //{
        //    try
        //    {
        //        table = new DataTable();
        //        table.Columns.Add("كروت التحكم المستخدمة في العداد", typeof(int));
        //        table.Rows.Add(10);
        //        table.Rows.Add(20);
        //        table.Rows.Add(30);
        //        table.Rows.Add(40);
        //        table.Rows.Add(50);
        //        dataGridView1.DataSource = table;
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(e);
        //        log.Error(e.StackTrace);
        //        MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}




        private void ReadControlCardFRM_Load(object sender, EventArgs e)
        {
          //  load_grid();
        }
        public int ReadData(string order)
        {
           
            var dictionary = new Dictionary<string, string>();
            string str = order;
            char[] charValues = str.ToCharArray();
            string hexOutput = "";
            List<byte> hexOutputList2 = new List<byte>();
            hexOutputList2.Add(0x00);
            hexOutputList2.Add(0x00);
            hexOutputList2.Add(0x01);
            hexOutputList2.Add(0x57);
            hexOutputList2.Add(0x31);
            hexOutputList2.Add(0x02);
            foreach (char _eachChar in charValues)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(_eachChar);
                // Convert the decimal value to a hexadecimal value in string form.
                hexOutput += String.Format("{0:X}", value);
                hexOutputList2.Add(BitConverter.GetBytes(value)[0]);

            }
            hexOutputList2.Add(0x03);
            int CRC = 0;

            foreach (var item in hexOutputList2)
            {
                CRC ^= item;
            }
            var command = hexOutput;
            string HexaString = "000001573102" + command + "03" + CRC.ToString("X2");
            byte[] ConvertedToArrayOfByte = Enumerable.Range(0, HexaString.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(HexaString.Substring(x, 2), 16))
            .ToArray();
            serialPort1.Write(ConvertedToArrayOfByte, 0, ConvertedToArrayOfByte.Length);
            Thread.Sleep(4000);
            return serialPort1.BytesToRead;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ports.Length > 0)
            {
                serialPort1.PortName = cboPort.Text;
                serialPort1.BaudRate = 2400;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.Even;
                serialPort1.DataBits = 7;
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                }
                else
                {
                    serialPort1.Close();
                    serialPort1.Open();
                }
            }
            try
            {
                
                if (serialPort1.IsOpen)
                {
                    int BytesToRead = ReadData("C2()");
                    if (BytesToRead > 0)
                    {
                        byte[] result = new byte[BytesToRead];
                        serialPort1.Read(result, 0, BytesToRead);
                        var LastElem = result.Last();
                        var res = System.Text.Encoding.Default.GetString(result);
                        String ASCIIString = Encoding.ASCII.GetString(result, 0, result.Length);
                        string outputStr = ASCIIString.Substring(128);
                        outputStr = outputStr.Substring(0, 32);
                        List<string> lst = StringExtensions.SplitInParts(outputStr,8).ToList();
                        List<string> lst2 = new List<string>();
                        for(int i = 0; i < lst.Count; i++)
                        {
                            string concated = "";
                            var str= StringExtensions.SplitInParts(lst[i],2).ToList(); 
                            for(int j=str.Count-1; j >= 0; j--) {
                                 concated +=str[j];
                            }
                            lst2.Add(concated);
                        }
                        table = new DataTable();
                        table.Columns.Add("كروت التحكم المستخدمة في العداد", typeof(string));
                        table.Rows.Add(lst2[0]);
                        table.Rows.Add(lst2[1]);
                        table.Rows.Add(lst2[2]);
                        table.Rows.Add(lst2[3]);
                        dataGridView1.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex.StackTrace);
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
            }
        }
    }
}

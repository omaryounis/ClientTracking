using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.DLMS.Reader;
using Gurux.DLMS.Secure;
using Gurux.Net;
using Gurux.Serial;
using LabProgram.DAL;
using LabProgram.Helper;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LabProgram
{
    public partial class ReadOpticalMeterFrm : Form
    { 
        OpticalViewModel OpticalViewModelObj = new OpticalViewModel();
        log4net.ILog log;
        public ReadOpticalMeterFrm()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            cboPort.SelectedIndex = 0;
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
            SerialPort.Write(ConvertedToArrayOfByte, 0, ConvertedToArrayOfByte.Length);
            Thread.Sleep(4000);
            return SerialPort.BytesToRead;
        }
        private void ReadOpticalMeterFrm_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (!SerialPort.IsOpen)
                {
                    SerialPort.PortName = cboPort.Text;
                    SerialPort.BaudRate = 2400;
                    SerialPort.StopBits = StopBits.One;
                    SerialPort.Parity = Parity.Even;
                    SerialPort.DataBits = 7;
                }
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
                SerialPort.Open();

                try
                {
                    if (SerialPort.IsOpen)
                    {
                        int BytesToRead = ReadData("T1()");

                        //int BytesToRead = ReadData("C2()");
                        if (BytesToRead > 0)
                        {
                            byte[] result = new byte[BytesToRead];
                            SerialPort.Read(result, 0, BytesToRead);
                            var LastElem = result.Last();
                            var res = System.Text.Encoding.Default.GetString(result);
                           var xxxxxx= ByteArrayToString(result);
                            String ASCIIString = Encoding.ASCII.GetString(result, 0, result.Length);
                            List<string> SplittedArr = ASCIIString.Split('\n').ToList();
                            List<string> SplittedArr2 = new List<string>();
                            List<string> SplittedArr3 = new List<string>();
                            for (int i = 0; i < SplittedArr.Count; i++)
                            {
                                SplittedArr2.Add(SplittedArr[i].Replace("\r", ""));
                            }
                            SplittedArr2 = SplittedArr2.Take(SplittedArr2.Count - 3).ToList();
                            for (int i = 0; i < SplittedArr2.Count; i++)
                            {
                                SplittedArr3.Add(SplittedArr2[i].Replace(")", ""));
                            }
                            var dictionary = new Dictionary<string, string>();
                            for (int i = 0; i < SplittedArr3.Count - 3; i++)
                            {
                                dictionary.Add(SplittedArr3[i].Split('(')[0], SplittedArr3[i].Split('(')[1]);
                            }
                            string time = "";
                            string date = "";
                            /****************************************************************************************************************************************Read software device name*****************************************************************************************************************************************/
                            int SWToRead = ReadData("SW()");
                            string DeviceVersion = "";
                            if (SWToRead > 0)
                            {
                                byte[] result1 = new byte[SWToRead];
                                SerialPort.Read(result1, 0, SWToRead); 
                                var xuuxxxxx = ByteArrayToString(result1);
                                String ASCIIString1 = Encoding.ASCII.GetString(result1, 0, result1.Length);
                                List<string> SplittedArr1 = ASCIIString1.Replace("\r", "").Split('\n').ToList();
                                DeviceVersion = SplittedArr1[0];
                            }
                            /****************************************************************************************************************************************Read software device name*****************************************************************************************************************************************/
                            if (DeviceVersion.Contains("16") && !DeviceVersion.Contains("V2"))
                            {
                                foreach (KeyValuePair<string, string> entry in dictionary)
                                {
                                    // txtRecieve.Text += entry.Key + "=>" + entry.Value + "\r";
                                    if (entry.Key.Contains("M_SN"))
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.MeterSerialNumber = val;
                                    }
                                    else if (entry.Key == "M_ID")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.ClientID = val;
                                    }
                                    else if (entry.Key == "Time")
                                    {

                                        var val = entry.Value;
                                        //var timeArr = val.Split(':').ToList();

                                        //for (int i = 0; i < timeArr.Count; i++)
                                        //{
                                        //    if (timeArr[timeArr.Count - 1] == timeArr[i])
                                        //    {
                                        //        time += Convert.ToInt64(timeArr[i], 16).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        time += Convert.ToInt64(timeArr[i], 16).ToString() + ":";
                                        //    }

                                        //}
                                    }
                                    else if (entry.Key == "Date")
                                    {
                                        //List<string> lst = new List<string>();
                                        //var val = entry.Value;
                                        //var dateArr = val.Split('-').Skip(1).ToList();
                                        //lst = dateArr;
                                        //for (int i = 0; i < lst.Count; i++)
                                        //{
                                        //    if (lst[lst.Count - 1] == lst[i])
                                        //    {
                                        //        date += Convert.ToInt64(lst[i], 16).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        date += Convert.ToInt64(lst[i], 16).ToString() + "/";
                                        //    }

                                        //}
                                        // OpticalViewModelObj.InstallDate = date + " " + time;
                                        OpticalViewModelObj.InstallDate = "0";

                                    }
                                    else if (entry.Key == "T_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var TotalKiloWat = lst[0];
                                        OpticalViewModelObj.TotalConsum = TotalKiloWat;
                                    }
                                    else if (entry.Key == "M_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var CurrentMonthKiloWat = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.CurrentMonthKiloWat = CurrentMonthKiloWat;
                                    }
                                    else if (entry.Key == "Tarif")
                                    {
                                        var val = entry.Value;
                                        var Tarif = Convert.ToInt64(val, 16);
                                        OpticalViewModelObj.Tarif = Tarif;
                                    }
                                    else if (entry.Key == "R_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var RemainKilowWat = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.RemainKilowWat = RemainKilowWat;
                                    }
                                    else if (entry.Key == "R_LE")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var RemainMoney = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.BalanceInMeter = RemainMoney;
                                    }
                                    //else if (entry.Key == "T_Crdt")
                                    //{
                                    //    var val = entry.Value;
                                    //    List<string> lst = new List<string>();
                                    //    lst = val.Split(' ').ToList();
                                    //    var TotalCredit = Convert.ToInt64(lst[0], 16);
                                    //    OpticalViewModelObj.TotalCredit = TotalCredit;
                                    //}
                                    else if (entry.Key == "L_Ch_D")
                                    {
                                        List<string> lst = new List<string>();
                                        var val = entry.Value;
                                        var days = val.Substring(0, 2);
                                        var months = val.Substring(2, 2);
                                        var years = val.Substring(val.Length - 4, 4);
                                        string daysValue = Convert.ToInt64(days, 16).ToString();
                                        string monthsValue = Convert.ToInt64(months, 16).ToString();
                                        string yearsValue = Convert.ToInt64(years, 16).ToString();
                                        OpticalViewModelObj.LastChargeDate = daysValue + "/" + monthsValue + "/" + yearsValue;

                                    }
                                    else if (entry.Key == "R_Days")
                                    {
                                        var val = entry.Value;
                                        var RemainDays = Convert.ToInt64(val, 16);
                                        OpticalViewModelObj.RemainDays = RemainDays;
                                    }
                                    else if (entry.Key == "Deon")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var DebitsInMeter = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.DebitsInMeter = DebitsInMeter;
                                        //char[] x = new char[] { ' ', 'k', 'W', 'h', '-', 'L', 'E' };
                                        //lst = val.Split(x).ToList();
                                        //var dtList = lst.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                                        //OpticalViewModelObj.DyonKilo = Convert.ToInt64(dtList[0], 16);
                                        //OpticalViewModelObj.DebitsInMeter = Convert.ToInt64(dtList[1], 16);
                                    }
                                    else if (entry.Key == "Rd_Cnt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "Ch_Cnt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "O_Load")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.ROverLoad = Convert.ToInt64(val, 16);
                                    }
                                    else if (entry.Key == "Cover")
                                    {
                                        List<string> lst = new List<string>();
                                        var val = entry.Value;
                                        lst = val.Split('-').ToList();
                                        if (lst.Count() > 1)
                                        {

                                            OpticalViewModelObj.DoorOpen = Convert.ToInt64(lst[0], 16);
                                            OpticalViewModelObj.coveropen = Convert.ToInt64(lst[1], 16);
                                        }
                                        else
                                        {

                                            OpticalViewModelObj.DoorOpen = Convert.ToInt64(val, 16);
                                        }
                                    }
                                    else if (entry.Key == "Earth")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.EarthOpen = val.ToString();
                                    }
                                    else if (entry.Key == "Rever")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "Relay")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "L_Batt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "MD0")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.overload = val;
                                    }
                                }

                            }
                            else if (DeviceVersion.Contains("V2") || DeviceVersion.Contains("17") || DeviceVersion.Contains("18"))
                            {
                                foreach (KeyValuePair<string, string> entry     in dictionary)
                                {
                                    // txtRecieve.Text += entry.Key + "=>" + entry.Value + "\r";
                                    if (entry.Key.Contains("M_SN"))
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.MeterSerialNumber = val;
                                    }
                                    else if (entry.Key == "M_ID")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.ClientID = val;
                                    }
                                    else if (entry.Key == "Time")
                                    {

                                        //var val = entry.Value;
                                        //var timeArr = val.Split(':').ToList();

                                        //for (int i = 0; i < timeArr.Count; i++)
                                        //{
                                        //    if (timeArr[timeArr.Count - 1] == timeArr[i])
                                        //    {
                                        //        time += Convert.ToInt64(timeArr[i], 16).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        time += Convert.ToInt64(timeArr[i], 16).ToString() + ":";
                                        //    }

                                        //}
                                    }
                                    else if (entry.Key == "Date")
                                    {
                                        //List<string> lst = new List<string>();
                                        //var val = entry.Value;
                                        //var dateArr = val.Split('-').Skip(1).ToList();
                                        //lst = dateArr;
                                        //for (int i = 0; i < lst.Count; i++)
                                        //{
                                        //    if (lst[lst.Count - 1] == lst[i])
                                        //    {
                                        //        date += Convert.ToInt64(lst[i], 16).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        date += Convert.ToInt64(lst[i], 16).ToString() + "/";
                                        //    }

                                        //}
                                        //OpticalViewModelObj.InstallDate = date + " " + time;
                                        OpticalViewModelObj.InstallDate = "0";

                                    }
                                    else if (entry.Key == "T_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var TotalKiloWat = lst[0];
                                        OpticalViewModelObj.TotalConsum = Convert.ToInt64(TotalKiloWat, 16).ToString();
                                    }
                                    else if (entry.Key == "M_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var CurrentMonthKiloWat = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.CurrentMonthKiloWat = CurrentMonthKiloWat;
                                    }
                                    else if (entry.Key == "Tarif")
                                    {
                                        var val = entry.Value;
                                        var Tarif = Convert.ToInt64(val, 16);
                                        OpticalViewModelObj.Tarif = Tarif;
                                    }
                                    else if (entry.Key == "R_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var RemainKilowWat = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.RemainKilowWat = RemainKilowWat;
                                    }
                                    else if (entry.Key == "R_LE")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var RemainMoney = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.BalanceInMeter = RemainMoney;
                                    }
                                    else if (entry.Key == "T_Crdt")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var TotalCredit = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.TotalCredit = TotalCredit;
                                    }
                                    else if (entry.Key == "L_Ch_D")
                                    {
                                        List<string> lst = new List<string>();
                                        var val = entry.Value;
                                        var days = val.Substring(0, 2);
                                        var months = val.Substring(2, 2);
                                        var years = val.Substring(val.Length - 4, 4);
                                        string daysValue = Convert.ToInt64(days, 16).ToString();
                                        string monthsValue = Convert.ToInt64(months, 16).ToString();
                                        string yearsValue = Convert.ToInt64(years, 16).ToString();
                                        OpticalViewModelObj.LastChargeDate = daysValue + "/" + monthsValue + "/" + yearsValue;
                                    }
                                    else if (entry.Key == "R_Days")
                                    {
                                        var val = entry.Value;
                                        var RemainDays = Convert.ToInt64(val, 16);
                                        OpticalViewModelObj.RemainDays = RemainDays;
                                    }
                                    else if (entry.Key == "Deon")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var DebitsInMeter = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.DebitsInMeter = DebitsInMeter;
                                        //char[] x = new char[] { ' ', 'k', 'W', 'h', '-', 'L', 'E' };
                                        //lst = val.Split(x).ToList();
                                        //var dtList = lst.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                                        //OpticalViewModelObj.DyonKilo = Convert.ToInt64(dtList[0], 16);
                                        //OpticalViewModelObj.DebitsInMeter = Convert.ToInt64(dtList[1], 16);
                                    }
                                    else if (entry.Key == "Rd_Cnt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "Ch_Cnt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "O_Load")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.ROverLoad = Convert.ToInt64(val, 16);
                                    }
                                    else if (entry.Key == "Cover")
                                    {
                                        List<string> lst = new List<string>();
                                        var val = entry.Value;
                                        lst = val.Split('-').ToList();
                                        if (lst.Count() > 1)
                                        {

                                            OpticalViewModelObj.DoorOpen = Convert.ToInt64(lst[0], 16);
                                            OpticalViewModelObj.coveropen = Convert.ToInt64(lst[1], 16);
                                        }
                                        else
                                        {

                                            OpticalViewModelObj.DoorOpen = Convert.ToInt64(val, 16);
                                        }
                                    }
                                    else if (entry.Key == "Earth")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.EarthOpen = val.ToString();
                                    }
                                    else if (entry.Key == "Rever")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "Relay")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "L_Batt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "MD0")
                                    {
                                        string x = "";
                                        var val = entry.Value;
                                        var lst = val.Split(' ').ToList();
                                        for (int i = 0; i < lst.Count; i++)
                                        {
                                            if (i == 1)
                                            {
                                                x = lst[i].Substring(0, 3);
                                                lst[i] = lst[i].Remove(0, 3);
                                            }
                                        }
                                        var km = Convert.ToInt64(lst[0], 16).ToString();
                                        var dc1 = decimal.Parse(km) / 100;
                                        var amp = Convert.ToInt64(lst[1], 16).ToString();
                                        var dc2 = decimal.Parse(amp) / 10;
                                        OpticalViewModelObj.overload = dc1 + " " + "KW" + " " + "-" + " " + dc2 + " " + "AMP";

                                    }
                                }
                            }
                            else
                            {
                                foreach (KeyValuePair<string, string> entry in dictionary)
                                {
                                    // txtRecieve.Text += entry.Key + "=>" + entry.Value + "\r";
                                    if (entry.Key.Contains("M_SN"))
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.MeterSerialNumber = val;
                                    }
                                    else if (entry.Key == "M_ID")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.ClientID = val;
                                    }
                                    else if (entry.Key == "Time")
                                    {

                                        //var val = entry.Value;
                                        //var timeArr = val.Split(':').ToList();

                                        //for (int i = 0; i < timeArr.Count; i++)
                                        //{
                                        //    if (timeArr[timeArr.Count - 1] == timeArr[i])
                                        //    {
                                        //        time += Convert.ToInt64(timeArr[i], 16).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        time += Convert.ToInt64(timeArr[i], 16).ToString() + ":";
                                        //    }

                                        //}
                                    }
                                    else if (entry.Key == "Date")
                                    {
                                        //List<string> lst = new List<string>();
                                        //var val = entry.Value;
                                        //var dateArr = val.Split('-').Skip(1).ToList();
                                        //lst = dateArr;
                                        //for (int i = 0; i < lst.Count; i++)
                                        //{
                                        //    if (lst[lst.Count - 1] == lst[i])
                                        //    {
                                        //        date += Convert.ToInt64(lst[i], 16).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        date += Convert.ToInt64(lst[i], 16).ToString() + "/";
                                        //    }

                                        //}
                                        //OpticalViewModelObj.InstallDate = date + " " + time;
                                        OpticalViewModelObj.InstallDate = "0";

                                    }
                                    else if (entry.Key == "T_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var TotalKiloWat = Convert.ToInt64(lst[0], 16).ToString();
                                        OpticalViewModelObj.TotalConsum = TotalKiloWat;
                                    }
                                    else if (entry.Key == "M_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var CurrentMonthKiloWat = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.CurrentMonthKiloWat = CurrentMonthKiloWat;
                                    }
                                    else if (entry.Key == "Tarif")
                                    {
                                        var val = entry.Value;
                                        var Tarif = Convert.ToInt64(val, 16);
                                        OpticalViewModelObj.Tarif = Tarif;
                                    }
                                    else if (entry.Key == "R_kWh")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var RemainKilowWat = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.RemainKilowWat = RemainKilowWat;
                                    }
                                    else if (entry.Key == "R_LE")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        lst = val.Split(' ').ToList();
                                        var RemainMoney = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.BalanceInMeter = RemainMoney;
                                    }
                                    else if (entry.Key == "T_Crdt")
                                    {
                                        //var val = entry.Value;
                                        //List<string> lst = new List<string>();
                                        //lst = val.Split(' ').ToList();
                                        //var TotalCredit = Convert.ToInt64(lst[0], 16);
                                        //OpticalViewModelObj.TotalCredit = TotalCredit;
                                    }
                                    else if (entry.Key == "L_Ch_D")
                                    {
                                        List<string> lst = new List<string>();
                                        var val = entry.Value;
                                        var days = val.Substring(0, 2);
                                        var months = val.Substring(2, 2);
                                        var years = val.Substring(val.Length - 4, 4);
                                        string daysValue = Convert.ToInt64(days, 16).ToString();
                                        string monthsValue = Convert.ToInt64(months, 16).ToString();
                                        string yearsValue = Convert.ToInt64(years, 16).ToString();
                                        OpticalViewModelObj.LastChargeDate = daysValue + "/" + monthsValue + "/" + yearsValue;
                                    }
                                    else if (entry.Key == "R_Days")
                                    {
                                        var val = entry.Value;
                                        var RemainDays = Convert.ToInt64(val, 16);
                                        OpticalViewModelObj.RemainDays = RemainDays;
                                    }
                                    else if (entry.Key == "Deon")
                                    {
                                        var val = entry.Value;
                                        List<string> lst = new List<string>();
                                        char[] x = new char[] { ' ', 'k', 'W', 'h', '-', 'L', 'E' };
                                        lst = val.Split(x).ToList();
                                        var dtList = lst.Where(s => !string.IsNullOrEmpty(s)).ToList();
                                        OpticalViewModelObj.DyonKilo = Convert.ToInt64(dtList[0], 16);
                                        OpticalViewModelObj.DebitsInMeter = Convert.ToInt64(dtList[1], 16);
                                    }
                                    else if (entry.Key == "Rd_Cnt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "Ch_Cnt")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "O_Load")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.ROverLoad = Convert.ToInt64(val, 16);
                                    }
                                    else if (entry.Key == "Cover")
                                    {
                                        List<string> lst = new List<string>();
                                        var val = entry.Value;
                                        lst = val.Split('-').ToList();
                                        OpticalViewModelObj.DoorOpen = Convert.ToInt64(lst[0], 16);
                                        OpticalViewModelObj.coveropen = Convert.ToInt64(lst[1], 16);
                                    }
                                    else if (entry.Key == "Earth")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.EarthOpen = val.ToString();
                                    }
                                    else if (entry.Key == "Rever")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "Relay")
                                    {
                                        var val = entry.Value;
                                    }
                                    else if (entry.Key == "L_Batt")
                                    {
                                        var val = entry.Value;
                                    }
                                    // do something with entry.Value or entry.Key
                                    else if (entry.Key == "MD0")
                                    {
                                        var val = entry.Value;
                                        OpticalViewModelObj.overload = val;
                                    }
                                }
                            }

                            DataTable dt = new DataTable();
                            dt.Columns.Add("ClientID", typeof(string));
                            dt.Columns.Add("MeterSerialNumber", typeof(string));
                            dt.Columns.Add("InstallDate", typeof(string));
                            dt.Columns.Add("TotalConsum", typeof(string));
                            dt.Columns.Add("CurrentMonthKiloWat", typeof(decimal));
                            dt.Columns.Add("Tarif", typeof(int));
                            dt.Columns.Add("RemainKilowWat", typeof(decimal));
                            dt.Columns.Add("BalanceInMeter", typeof(decimal));
                            dt.Columns.Add("TotalCredit", typeof(decimal));
                            dt.Columns.Add("LastChargeDate", typeof(string));
                            dt.Columns.Add("RemainDays", typeof(int));
                            dt.Columns.Add("DebitsInMeter", typeof(decimal));
                            dt.Columns.Add("DyonKilo", typeof(decimal));
                            dt.Columns.Add("ROverLoad", typeof(Int64));
                            dt.Columns.Add("EarthOpen", typeof(string));
                            dt.Columns.Add("overload", typeof(string));
                            dt.Rows.Add(new object[] {
                                OpticalViewModelObj.ClientID  ,
                                OpticalViewModelObj.MeterSerialNumber ,
                                OpticalViewModelObj.InstallDate,
                                OpticalViewModelObj.TotalConsum,
                                OpticalViewModelObj.CurrentMonthKiloWat,
                                OpticalViewModelObj.Tarif,
                                OpticalViewModelObj.RemainKilowWat,
                                OpticalViewModelObj.BalanceInMeter/1000,
                                OpticalViewModelObj.TotalCredit,
                                OpticalViewModelObj.LastChargeDate,
                                OpticalViewModelObj.RemainDays,
                                OpticalViewModelObj.DebitsInMeter/1000,
                                OpticalViewModelObj.DyonKilo,
                                OpticalViewModelObj.ROverLoad,
                                OpticalViewModelObj.EarthOpen,
                                 OpticalViewModelObj.overload
                            });

                            string exeFolder = Application.StartupPath;
                            string reportPath = Path.Combine(exeFolder, @"Reports\OpticalMeterData.rdlc");
                            this.reportViewer1.LocalReport.DataSources.Clear();
                            reportViewer1.LocalReport.ReportPath = reportPath;
                            ReportDataSource rds = new ReportDataSource("CardReader", dt);
                            reportViewer1.LocalReport.EnableExternalImages = true;
                            ReportParameter Logo = new ReportParameter("Logo", Application.StartupPath + "\\logo.png");
                            ReportParameter Header1 = new ReportParameter("CompanyName", "شركة جنوب القاهرة");
                            ReportParameter MeterSerialNumber = new ReportParameter("MeterSerialNumber", dt.Rows[0]["MeterSerialNumber"].ToString());
                            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { Header1, MeterSerialNumber });
                            this.reportViewer1.LocalReport.DataSources.Add(rds);
                            this.reportViewer1.LocalReport.Refresh();
                            this.reportViewer1.RefreshReport();
                            this.radButton1.Visible = true;
                        }
                        else
                        {

                            MessageBox.Show("برجاء توصيل الوصله الضوئية بالعداد", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    log.Error(ex.StackTrace);
                    MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK,  MessageBoxIcon.Error);
                }
                finally
                {
                    if (SerialPort.IsOpen)
                    {
                        SerialPort.Close();
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
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            OleDbParameter[] param = new OleDbParameter[11];
            param[0] = new OleDbParameter("@ClientID", OleDbType.VarChar);
            param[0].Value = OpticalViewModelObj.ClientID;

            param[1] = new OleDbParameter("@DebitsInMeter", OleDbType.Decimal);
            param[1].Value = OpticalViewModelObj.DebitsInMeter / 1000;

            param[2] = new OleDbParameter("@TotalConsum", OleDbType.VarChar);
            param[2].Value = OpticalViewModelObj.TotalConsum;

            param[3] = new OleDbParameter("@InstallDate", OleDbType.VarChar);
            param[3].Value = OpticalViewModelObj.InstallDate;


            param[4] = new OleDbParameter("@MeterSerialNumber", OleDbType.VarChar);
            param[4].Value = OpticalViewModelObj.MeterSerialNumber == null ? "" : OpticalViewModelObj.MeterSerialNumber;

            param[5] = new OleDbParameter("@BalanceInMeter", OleDbType.Decimal);
            param[5].Value = OpticalViewModelObj.BalanceInMeter / 1000;

            param[6] = new OleDbParameter("@LastChargeDate", OleDbType.VarChar);
            param[6].Value = OpticalViewModelObj.LastChargeDate;
            param[7] = new OleDbParameter("@ROverLoad", OleDbType.BigInt);
            param[7].Value = OpticalViewModelObj.ROverLoad;

            param[8] = new OleDbParameter("@EarthOpen", OleDbType.VarChar);
            param[8].Value = OpticalViewModelObj.EarthOpen;
            param[9] = new OleDbParameter("@RegisterDate", OleDbType.Date);
            param[9].Value = DateTime.Now;
            param[10] = new OleDbParameter("@overload", OleDbType.VarChar);
            param[10].Value = OpticalViewModelObj.overload;

            try
            {
                dataAccessLayer.open();
                dataAccessLayer.executecommand(@"insert into Optical (ClientID,[DebitsInMeter],[TotalConsum],[InstallDate],[MeterSerialNumber],[BalanceInMeter],[LastChargeDate],[ROverLoad],[EarthOpen],[RegisterDate],[overload]
                                                                        )
                                                                        values 
                                                                        (@ClientID ,@DebitsInMeter,@TotalConsum,@InstallDate,@MeterSerialNumber,@BalanceInMeter,@LastChargeDate,@ROverLoad,@EarthOpen,@RegisterDate,@overload
                                                                        )", param);
                dataAccessLayer.close();
                MessageBox.Show("تم الحفظ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                log.Error(ex);
                log.Error(ex.StackTrace);
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

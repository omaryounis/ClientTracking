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
    public partial class ReadOpticalMeterDlmsFrm : Form
    {
        OpticalViewModel OpticalViewModelObj = new OpticalViewModel();
        log4net.ILog log;
        public ReadOpticalMeterDlmsFrm()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            cboPort.SelectedIndex = 0;
        }

        public class Settings
        {
            public IGXMedia media = null;
            public TraceLevel trace = TraceLevel.Info;
            public bool iec = false;
            public GXDLMSSecureClient client = new GXDLMSSecureClient(true);
            //Objects to read.
            public List<KeyValuePair<string, int>> readObjects = new List<KeyValuePair<string, int>>();
        }

        static GXDateTime[] GetInterval(String IntervalOBIS, GXDLMSReader reader)
        {
            GXDLMSActionSchedule item = new GXDLMSActionSchedule(IntervalOBIS);
            reader.Read(item, 4);
            return item.ExecutionTime;
        }
       
        private void ReadOpticalMeterDlmsFrm_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.client.InterfaceType = InterfaceType.HDLC;
            settings.client.UseLogicalNameReferencing = true;
            settings.media = new GXSerial();
            ((GXSerial)settings.media).PortName = cboPort.Text;
            settings.client.ServerAddress = 1;
            settings.client.ServerAddressSize = 2;
            settings.client.ClientAddress = 16;
            settings.trace = TraceLevel.Verbose;
            //((GXNet)settings.media).Port = 4061;
            GXDLMSReader reader = null;
            try
            {
                if (settings.media is GXSerial)
                {
                    GXSerial serial = settings.media as GXSerial;

                    if (settings.iec)
                    {
                        serial.BaudRate = 300;
                        serial.DataBits = 7;
                        serial.Parity = System.IO.Ports.Parity.Even;
                        serial.StopBits = System.IO.Ports.StopBits.One;
                    }
                    else
                    {
                        serial.BaudRate = 9600;
                        serial.DataBits = 8;
                        serial.Parity = System.IO.Ports.Parity.None;
                        serial.StopBits = System.IO.Ports.StopBits.One;
                    }
                }
                else if (settings.media is GXNet)
                {
                }
                else
                {
                    throw new Exception("Unknown media type.");
                }
                ////////////////////////////////////////
                reader = new GXDLMSReader(settings.client, settings.media, settings.trace);
                settings.media.Open();
                //Some meters need a break here.
                Thread.Sleep(1000);
                reader.InitializeConnection();

                GXDLMSData item = new GXDLMSData("0.0.94.20.10.255");
                reader.Read(item, 2);
                string result = BitConverter.ToString((byte[])item.Value).Replace("-", "");
                ReadMeterCardWrapper.METER_READ_DATA meterData = new ReadMeterCardWrapper.METER_READ_DATA();
                int iRet = ReadMeterCardWrapper.ReadCollectionCardRecord((byte[])item.Value, ref meterData);
                OpticalViewModelObj.ClientID = meterData.ClientID.ToString();
                OpticalViewModelObj.MeterSerialNumber = meterData.MeterSerialNumber.ToString();
                decimal DebitsInMeterPstr = Convert.ToDecimal(meterData.DebitsInMeterPstr / 100);
                OpticalViewModelObj.DebitsInMeter = meterData.DebitsInMeterPnd + DebitsInMeterPstr;
                OpticalViewModelObj.TotalConsum = meterData.TotalConsum.ToString();
                OpticalViewModelObj.InstallDate = Cls_Global.GetDate(meterData.MeterInstallYears, meterData.MeterInstallMonthes, meterData.MeterInstallDays, meterData.MeterInstallHours, meterData.MeterInstallMinute).ToString();
                decimal RemainPiasters = Convert.ToDecimal(meterData.RemainPiasters / 100);
                OpticalViewModelObj.BalanceInMeter = meterData.RemainPound + RemainPiasters;
                OpticalViewModelObj.overload = meterData.overload.ToString();
                var LastChargeDateFromCard = Convert.ToUInt32(meterData.LastChargeDate);
                string LastChargeDate = "";
                string HC = string.Format("{0:x8}", LastChargeDateFromCard);
                try
                {

                    OpticalViewModelObj.LastChargeDate = DateTime.ParseExact("" + (2000 + Convert.ToInt16(HC.Substring(0, 2))).ToString() + "-" + HC.Substring(2, 2) + "-" + HC.Substring(4, 2), "yyyy-MM-dd",
                                            System.Globalization.CultureInfo.InvariantCulture).ToString();

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
                                OpticalViewModelObj.BalanceInMeter,
                                OpticalViewModelObj.TotalCredit,
                                OpticalViewModelObj.LastChargeDate,
                                OpticalViewModelObj.RemainDays,
                                OpticalViewModelObj.DebitsInMeter,
                                OpticalViewModelObj.DyonKilo,
                                OpticalViewModelObj.ROverLoad,
                                OpticalViewModelObj.EarthOpen,
                               OpticalViewModelObj.overload
                            });
                    string exeFolder = Application.StartupPath;
                    string reportPath = Path.Combine(exeFolder, @"Reports\OpticalMeterData.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CardReader", dt);
                    reportViewer1.LocalReport.EnableExternalImages = true;
                    ReportParameter Logo = new ReportParameter("Logo", Application.StartupPath + "\\logo.png");
                    ReportParameter Header1 = new ReportParameter("CompanyName", " ");
                    ReportParameter MeterSerialNumber = new ReportParameter("MeterSerialNumber", dt.Rows[0]["MeterSerialNumber"].ToString());
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { Header1, MeterSerialNumber });
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                    this.reportViewer1.LocalReport.Refresh();
                    this.reportViewer1.RefreshReport();
                    this.radButton1.Visible = true;
                }
                catch (Exception ex)
                {

                    log.Error(ex);
                    log.Error(ex.StackTrace);
                    MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
               
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    //Console.ReadKey();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Console.WriteLine("Ended. Press any key to continue.");
                    //Console.ReadKey();
                }
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@ClientID", OleDbType.VarChar);
            param[0].Value = OpticalViewModelObj.ClientID;

            param[1] = new OleDbParameter("@DebitsInMeter", OleDbType.Decimal);
            param[1].Value = OpticalViewModelObj.DebitsInMeter;

            param[2] = new OleDbParameter("@TotalConsum", OleDbType.VarChar);
            param[2].Value = OpticalViewModelObj.TotalConsum;

            param[3] = new OleDbParameter("@InstallDate", OleDbType.VarChar);
            param[3].Value = OpticalViewModelObj.InstallDate;


            param[4] = new OleDbParameter("@MeterSerialNumber", OleDbType.VarChar);
            param[4].Value = OpticalViewModelObj.MeterSerialNumber;

            param[5] = new OleDbParameter("@BalanceInMeter", OleDbType.Decimal);
            param[5].Value = OpticalViewModelObj.BalanceInMeter;

            param[6] = new OleDbParameter("@LastChargeDate", OleDbType.VarChar);
            param[6].Value = OpticalViewModelObj.LastChargeDate;
            param[7] = new OleDbParameter("@ROverLoad", OleDbType.BigInt);
            param[7].Value = OpticalViewModelObj.ROverLoad;

            //param[8] = new OleDbParameter("@EarthOpen", OleDbType.VarChar);
            //param[8].Value = OpticalViewModelObj.EarthOpen;
            param[8] = new OleDbParameter("@RegisterDate", OleDbType.Date);
            param[8].Value = DateTime.Now;
            param[9] = new OleDbParameter("@overload", OleDbType.VarChar);
            param[9].Value = OpticalViewModelObj.overload;

            try
            {
                dataAccessLayer.open();
                dataAccessLayer.executecommand(@"insert into Optical (ClientID,[DebitsInMeter],[TotalConsum],[InstallDate],[MeterSerialNumber],[BalanceInMeter],[LastChargeDate],[ROverLoad],[RegisterDate],[overload]
                                                                        )
                                                                        values 
                                                                        (@ClientID ,@DebitsInMeter,@TotalConsum,@InstallDate,@MeterSerialNumber,@BalanceInMeter,@LastChargeDate,@ROverLoad,@RegisterDate,@overload
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

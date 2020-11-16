using LabProgram.DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabProgram
{
    public partial class MeterDataReport : Form
    {
        string txt_MeterId = "";
        log4net.ILog log;
        public MeterDataReport()
        {
            //this.reportViewer1.RefreshReport();
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }


        private void Report_Load(object sender, EventArgs e)
        {
            try
            {

                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@txt_MeterId", OleDbType.VarChar);
                param[0].Value = textBox1.Text.ToString();
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                var recs = dataAccessLayer.selectdata(@"select [ClientID],[TotalConsum] ,[BalanceInMeter] ,[DebitsInMeter] ,[InstallDate],[LastChargeDate],[RegisterDate] as ReadDate  from Optical where MeterSerialNumber = @txt_MeterId", param);
                var MeterData = recs;
                string exeFolder = Application.StartupPath;
                string reportPath = Path.Combine(exeFolder, @"Reports\MeterData.rdlc");
                this.reportViewer1.LocalReport.DataSources.Clear();
                ReportParameter CompanyName = new ReportParameter("CompanyName", "شركة جنوب القاهرة");
                ReportParameter MeterNumber = new ReportParameter("MeterSerialNumber", textBox1.Text.ToString());
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CardReader", MeterData));
                var result = reportViewer1.LocalReport.GetParameters();
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { CompanyName, MeterNumber });
                reportViewer1.LocalReport.Refresh();
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex.StackTrace);
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MeterDataReport_Load(object sender, EventArgs e)
        {
            try
            {

                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@txt_MeterId", OleDbType.VarChar);
                param[0].Value = textBox1.Text.ToString();
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                var recs = dataAccessLayer.selectdata(@"select [ClientID],[TotalConsum] ,[BalanceInMeter] ,[DebitsInMeter] ,[InstallDate],[LastChargeDate],[RegisterDate] as ReadDate  from Optical where MeterSerialNumber = @txt_MeterId", param);
                var MeterData = recs;
                string exeFolder = Application.StartupPath;
                string reportPath = Path.Combine(exeFolder, @"Reports\LabProgram.rdlc");
                this.reportViewer1.LocalReport.DataSources.Clear();
                ReportParameter CompanyName = new ReportParameter("CompanyName", "  ");
                ReportParameter MeterNumber = new ReportParameter("MeterSerialNumber", textBox1.Text.ToString());
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CardReader", MeterData));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { CompanyName, MeterNumber });
                reportViewer1.LocalReport.Refresh();
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
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

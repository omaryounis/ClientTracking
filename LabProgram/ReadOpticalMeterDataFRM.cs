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
using System.Windows.Forms;

namespace LabProgram
{
    public partial class ReadOpticalMeterDataFRM : Form
    {
        log4net.ILog log;
        public ReadOpticalMeterDataFRM()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
        }
        public void load_grid(List<OpticalViewModel> lst)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("رقم العداد", typeof(string));
                table.Columns.Add("الإستهلاك الكلي", typeof(string));
                table.Columns.Add("الرصيد بالعداد", typeof(decimal));
                table.Columns.Add("الديون بالعداد", typeof(decimal));
                table.Columns.Add("تاريخ التركيب", typeof(string));
                table.Columns.Add("تاريخ أخر شحنه", typeof(string));
                table.Columns.Add("تاريخ التسجيل", typeof(string));
                table.Columns.Add("رقم المشترك", typeof(string));
                foreach (var item in lst)
                {
                    table.Rows.Add(item.MeterSerialNumber, item.TotalConsum, item.BalanceInMeter, item.DebitsInMeter, item.InstallDate, item.LastChargeDate, item.ReadDate, item.ClientID);
                }
                dataGridView1.DataSource = table;
            }
            catch (Exception e)
            {
                log.Error(e);
                log.Error(e.StackTrace);
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public DataTable runQuery()
        {
            try
            {
                OleDbParameter[] param = new OleDbParameter[1];
                param[0] = new OleDbParameter("@txt_MeterId", OleDbType.VarChar);
                param[0].Value = textBox1.Text;
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                var recs = dataAccessLayer.selectdata(@"select [ClientID],[MeterSerialNumber],[TotalConsum],[BalanceInMeter],[DebitsInMeter],[InstallDate],[LastChargeDate],[RegisterDate]  from Optical where MeterSerialNumber = @txt_MeterId", param);
                return recs;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex.StackTrace);
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("يجب إدخال رقم العداد", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable recs = runQuery();
                    if (recs.Rows.Count > 0)
                    {
                        List<OpticalViewModel> MeterDataModelList = new List<OpticalViewModel>();
                        for (int i = 0; i < recs.Rows.Count; i++)
                        {
                            OpticalViewModel meterDataModel = new OpticalViewModel();
                            meterDataModel.MeterSerialNumber = recs.Rows[i]["MeterSerialNumber"].ToString();
                            meterDataModel.TotalConsum = recs.Rows[i]["TotalConsum"].ToString();
                            meterDataModel.BalanceInMeter = Convert.ToDecimal(recs.Rows[i]["BalanceInMeter"]);
                            meterDataModel.DebitsInMeter = Convert.ToDecimal(recs.Rows[i]["DebitsInMeter"]);
                            meterDataModel.InstallDate = recs.Rows[i]["InstallDate"].ToString();
                            meterDataModel.LastChargeDate = recs.Rows[i]["LastChargeDate"].ToString();
                            meterDataModel.ReadDate = recs.Rows[i]["RegisterDate"].ToString();
                            meterDataModel.ClientID = recs.Rows[i]["ClientID"].ToString();
                            MeterDataModelList.Add(meterDataModel);
                        }
                        load_grid(MeterDataModelList);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {

                log.Error(ex);
                log.Error(ex.StackTrace);
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    dataGridView1.DataSource = null;
                   MessageBox.Show("يجب إدخال رقم العداد", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MeterDataReport Report = new MeterDataReport();
                    Report.textBox1.Text = textBox1.Text.ToString();
                    Report.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex.StackTrace);
               MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            label1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
        }
    }
}

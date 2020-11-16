using LabProgram.DAL;
using LabProgram.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabProgram
{
    public partial class Login_frm : Form
    {
        log4net.ILog log;
        public Login_frm()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            InitializeComponent();
            txt_pass.PasswordChar = '*';
        }

        private void RadButton1_Click(object sender, EventArgs e)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.open();
            try
            {
                string HashedPassword = HelperMethods.HashPassword(txt_pass.Text.ToString());

                OleDbParameter[] param = new OleDbParameter[2];
                param[0] = new OleDbParameter("@UserName", OleDbType.VarChar, 50);
                param[0].Value = txt_usname.Text.ToString();
                param[1] = new OleDbParameter("@Password", OleDbType.VarChar);
                param[1].Value = HashedPassword;

                var recs = dataAccessLayer.selectdata("SELECT  [ID],[UserName] from [Users] Where [UserName]=[@UserName] AND [Password]=[@Password]", param);

                if (recs.Rows.Count > 0)
                {
                    var name = param[0].Value.ToString();
                    Cls_Global.username = name;
                    this.Hide();
                    MainFrm f = new MainFrm();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("اسم المستخدم أو كلمة السر خطأ أعد المحاولة", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                dataAccessLayer.close();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(ex.StackTrace);
                dataAccessLayer.close();
                MessageBox.Show("تواصل مع مسؤولي النظام", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void RadButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

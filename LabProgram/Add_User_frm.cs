using LabProgram.DAL;
using LabProgram.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabProgram
{

    public partial class Add_User_frm : Form
    {
        log4net.ILog log;
        public Add_User_frm()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            InitializeComponent();
            //txt_userpassword.PasswordChar = '*';
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                if (txt_username.Text.Trim() == "" || txt_userpassword.Text.Trim() == "")
                {
                    MessageBox.Show("أدخل البيانات المطلوبة", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrEmpty(txt_userpassword.Text))
                {
                    MessageBox.Show("الباسورد خاطيء", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbParameter[] param = new OleDbParameter[1];
                    param[0] = new OleDbParameter("@UserName", OleDbType.VarChar, 50);
                    param[0].Value = txt_username.Text.ToString();
                    var recs = dataAccessLayer.selectdata("select 1 from Users where UserName = @UserName", param);
                    if (recs.Rows.Count > 0)
                    {
                        MessageBox.Show("هذا المستخدم مدخل مسبقا", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string Password = HelperMethods.HashPassword(txt_userpassword.Text.ToString());
                        OleDbParameter[] param1 = new OleDbParameter[2];
                        param1[0] = new OleDbParameter("@UserName", OleDbType.VarChar, 50);
                        param1[0].Value = txt_username.Text.ToString();
                        param1[1] = new OleDbParameter("@Password", OleDbType.VarChar);
                        param1[1].Value = Password;
                        dataAccessLayer.open();
                        dataAccessLayer.executecommand("insert into Users (UserName,[Password]) values (@UserName,@Password)", param1);
                        dataAccessLayer.close();
                        MessageBox.Show("تم تسجيل المستخدم", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

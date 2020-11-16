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

    public partial class Change_User_PWD_FRM : Form
    {
        log4net.ILog log;
        public Change_User_PWD_FRM()
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
                int UserId = 0;
                if (txt_username.Text.Trim() == "" || txt_old_userpassword.Text.Trim() == "" || txt_new_userpassword.Text.Trim() == "")
                {
                   MessageBox.Show("أدخل البيانات المطلوبة", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataAccessLayer dataAccessLayer = new DataAccessLayer();
                    dataAccessLayer.open();
                    OleDbParameter[] param = new OleDbParameter[2];
                    param[0] = new OleDbParameter("@UserName", OleDbType.VarChar, 50);
                    param[0].Value = txt_username.Text.ToString();
                    param[1] = new OleDbParameter("@Password", OleDbType.VarChar);
                    string HashedPassword = HelperMethods.HashPassword(txt_old_userpassword.Text.ToString());
                    param[1].Value = HashedPassword;
                    var recs = dataAccessLayer.selectdata("SELECT  [ID],[UserName] from [Users] Where [UserName]=[@UserName] AND [Password]=[@Password]", param);
                    if (recs.Rows.Count > 0)
                    {
                        UserId =(int) recs.Rows[0][0];
                        OleDbParameter[] param2 = new OleDbParameter[1];
                        param2[0] = new OleDbParameter("@UserId", OleDbType.Integer);
                        param2[0].Value = UserId;
                        //param2[1] = new OleDbParameter("@Password", OleDbType.VarChar);
                        //string HashedPassword2 = HelperMethods.HashPassword(txt_old_userpassword.Text.ToString());
                        //param2[1].Value = HashedPassword2;
                        int y = dataAccessLayer.executecommand2("DELETE FROM [Users] WHERE [ID]=[@UserId]", param2);
                        string UserName = recs.Rows[0][1].ToString();
                        string Password = HelperMethods.HashPassword(txt_new_userpassword.Text.ToString());
                        OleDbParameter[] param1 = new OleDbParameter[2];
                        param1[0] = new OleDbParameter("@UserName", OleDbType.VarChar);
                        param1[0].Value = UserName;
                        param1[1] = new OleDbParameter("@Password", OleDbType.VarChar);
                        param1[1].Value = Password;
                        int x = dataAccessLayer.executecommand2("INSERT INTO [Users] (UserName,[Password]) VALUES (@UserName,@Password)", param1);
                        dataAccessLayer.close();
                        if (x == 1 && y == 1)
                        {
                           MessageBox.Show("تم تغيير كلمة السر للمستخدم بنجاح", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("هذا المستخدم غير موجود", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    dataAccessLayer.close();
                    Application.Exit();
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

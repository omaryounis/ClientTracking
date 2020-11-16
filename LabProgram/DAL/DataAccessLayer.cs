using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabProgram.DAL
{

    class DataAccessLayer
    {
        OleDbConnection sqlconnection;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataAccessLayer()
        {

            string exeFolder = Application.StartupPath;
            string reportPath = Path.Combine(exeFolder, "LabProgramDB.accdb");
            sqlconnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + reportPath);
        }
        //method to open the connection
        public void open()
        {
            if (sqlconnection.State != ConnectionState.Open)
            {
                try
                {
                    sqlconnection.Open();
                }
                catch (Exception e)
                {

                    log.Error(e);
                    log.Error(e.StackTrace);
                }

            }
        }
        public void close()
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                try
                {
                    sqlconnection.Close();
                }
                catch (Exception e)
                {

                    log.Error(e);
                    log.Error(e.StackTrace);
                }
            }
        }
        public DataTable selectdata(string stored_procedure, OleDbParameter[] param)
        {
            OleDbCommand sqlcmd = new OleDbCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }

            }
            OleDbDataAdapter da = new OleDbDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void executecommand(string stored_procedure, OleDbParameter[] param)
        {
            OleDbCommand sqlcmd = new OleDbCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;

            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
        public int executecommand2(string stored_procedure, OleDbParameter[] param)
        {
            OleDbCommand sqlcmd = new OleDbCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;

            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            return sqlcmd.ExecuteNonQuery();
        }
    }
}

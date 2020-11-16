using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabProgram
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void تسجيلخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("تم الخروج", "warining");
            this.Close();
            Application.Exit();
        }

        private void إستدعاءبياناتالعدادToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadOpticalMeterDataFRM frm = new ReadOpticalMeterDataFRM();
            frm.ShowDialog();
        }

        private void قراءةعدادالمعملToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReadOpticalMeterFrm frm = new ReadOpticalMeterFrm();
            frm.ShowDialog();
        }

        private void قراءةعداد2019ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadOpticalMeterDlmsFrm frm = new ReadOpticalMeterDlmsFrm();
            frm.ShowDialog();
        }

        private void قراءةعداد2015ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadOpticalMeterV2015Frm frm = new ReadOpticalMeterV2015Frm();
            frm.ShowDialog();
        }

        private void شاشةالمستخدمToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Add_User_frm frm = new Add_User_frm();
            frm.ShowDialog();
        }

        private void تغييركلمةالسرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_User_PWD_FRM frm = new Change_User_PWD_FRM();
            frm.ShowDialog();
        }

        private void قراءةكروتالتحكمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadControlCardFRM readControlCardFRM = new ReadControlCardFRM();
            readControlCardFRM.ShowDialog();
        }
    }
}

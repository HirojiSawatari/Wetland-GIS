using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevComponents.DotNetBar;

using System.Data.OleDb;
using System.Web;

namespace 湿地GIS
{
    public partial class 用户登录 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public 用户登录()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            textBoxX1.Clear();
            textBoxX2.Clear();
        }

        private void 用户登录_Load(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            string user,pass;
            OleDbConnection conn = null;
            OleDbDataReader rdr = null;
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=LNNU_GIS.mdb ;");
                conn.Open();
                user = textBoxX1.Text.Trim();
                pass = textBoxX2.Text.Trim();
                OleDbCommand cmd = new OleDbCommand("Select pass FROM userid where user= '" + user + "' ", conn);
                rdr = cmd.ExecuteReader();
                if (textBoxX1.Text == string.Empty || textBoxX2.Text == string.Empty)
                {
                    MessageBox.Show(this, "输入信息不完整，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (rdr.Read())
                    {
                        string a = rdr["pass"].ToString();
                        if (a == pass)
                        {
                            Program.u = user;
                            Program.a = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "用户名或密码错误，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "此用户不存在，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (conn != null) conn.Close();
            }
        }
    }
}
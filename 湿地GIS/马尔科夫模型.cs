using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace 湿地GIS
{
    public partial class 马尔科夫模型 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public 马尔科夫模型()
        {
            InitializeComponent();
        }
        //行政区枚举
        public enum xingzhengqu : int
        {
            大洼县,
            大洼镇,
            东风镇,
            二界沟镇,
            辽滨沿海经济区,
            平安乡,
            清水镇,
            荣兴镇,
            唐家镇,
            田家镇,
            田庄台镇,
            王家镇,
            西安镇,
            新建镇,
            新开镇,
            新立镇,
            新兴镇,
            赵圈河镇,
        }

        //景观类型枚举
        public enum LandscapeType : int
        {
            建设用地,
            旱地,
            林地草地,
            水库坑塘,
            水田,
            河流,
            沼泽,
            滩涂,
            盐田及海水养殖场

        }

        public bool Exportlistview(ListView lView, bool isShowExcle)
        {
            if (lView.Items == null)
                return false;

            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;

            //生成字段名称
            for (int i = 0; i < lView.Columns.Count; i++)
            {
                excel.Cells[1, i + 1] = lView.Columns[i].Text;
            }

            //填充数据
            for (int i = 0; i < lView.Items.Count; i++)
            {
                ListViewItem item = lView.Items[i];
                for (int j = 0; j < item.SubItems.Count; j++)
                {
                    excel.Cells[i + 2, j + 1] = item.SubItems[j].Text;
                }
            }
            return true;
        }
        OleDbConnection my_conn;
        OleDbDataAdapter my_adapter;
        DataSet my_ds;
        private void Form2_Load(object sender, EventArgs e)
        {
            C_conn c1 = new C_conn();
            my_conn = c1.get_conn();
            string selectstr = "select * from 2000";
            my_adapter = new OleDbDataAdapter(selectstr, my_conn);
            my_ds = new DataSet();
            my_adapter.Fill(my_ds, "2000");
            dataGridView1.DataSource = my_ds.Tables["2000"];
            my_conn.Close();

            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView1.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView1.Visible = true;

            C_conn c2 = new C_conn();
            my_conn = c2.get_conn();
            string selectstr2 = "select * from change";
            my_adapter = new OleDbDataAdapter(selectstr2, my_conn);
            my_ds = new DataSet();
            my_adapter.Fill(my_ds, "change");
            dataGridView2.DataSource = my_ds.Tables["change"];
            my_conn.Close();

            string strcom = comboBox2.Text.ToString().Trim();
            this.listView3.View = View.Details;
            this.listView3.GridLines = true;
            this.listView3.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView3.Columns.Add(strcom, 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView3.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("盐田及海水养殖场", 100, HorizontalAlignment.Right);
            this.listView3.Visible = true;

            this.listView4.View = View.Details;
            this.listView4.GridLines = true;
            this.listView4.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView4.Columns.Add(Program.name, 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView4.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("盐田及海水养殖场", 100, HorizontalAlignment.Right);
            this.listView4.Visible = true;

            C_conn c3 = new C_conn();
            my_conn = c3.get_conn();
            string selectst2 = "select * from 2009";
            my_adapter = new OleDbDataAdapter(selectst2, my_conn);
            my_ds = new DataSet();
            my_adapter.Fill(my_ds, "2009");
            dataGridView3.DataSource = my_ds.Tables["2009"];
            my_conn.Close();

            this.listView5.View = View.Details;
            this.listView5.GridLines = true;
            this.listView5.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView5.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView5.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView5.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)  //查询*******************************
        {
            string str = comboBox1.Text.ToString().Trim();
            double[] m = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (DataGridViewRow row in this.dataGridView1.Rows)  //遍历控件中的行
            {
                if (row.Cells["乡镇"].Value == null)
                    break;
                string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                if (str == "大洼县")
                {
                    int x = Convert.ToInt32(row.Cells["ClassId00"].Value);
                    m[x - 1] = Convert.ToDouble(row.Cells["面积"].Value);


                }
                else
                {

                    if (str.Trim() == strbiaozhi.Trim())
                    {
                        int x = Convert.ToInt32(row.Cells["ClassId00"].Value);
                        m[x - 1] = Convert.ToDouble(row.Cells["面积"].Value);

                    }
                }



            }
            listView1.Clear();
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView1.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView1.Visible = true;
            ListViewItem li1 = new ListViewItem();
            li1.SubItems[0].Text = Convert.ToString(str);
            for (int i = 0; i < 9; i++)
            {
                double s = m[i];
                li1.SubItems.Add(Convert.ToString(s));
            }
            this.listView1.Items.Add(li1);
        }

        private void button2_Click(object sender, EventArgs e)   //查询所有
        {
            listView1.Clear();
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView1.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView1.Visible = true;

            Type quyu = typeof(xingzhengqu);
            Array Arrays = Enum.GetValues(quyu);
            double[,] m = new double[Arrays.LongLength, 9];
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                string str = Convert.ToString(Arrays.GetValue(i));
                foreach (DataGridViewRow row in this.dataGridView1.Rows)  //遍历控件中的行
                {
                    if (row.Cells["乡镇"].Value == null)
                        break;
                    string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                    if (str == "大洼县")
                    {
                        int x = Convert.ToInt32(row.Cells["ClassId00"].Value);
                        m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                    }
                    else
                    {
                        if (str.Trim() == strbiaozhi.Trim())
                        {
                            int x = Convert.ToInt32(row.Cells["ClassId00"].Value);
                            m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                        }
                    }
                }
            }
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = Convert.ToString(Arrays.GetValue(i));
                for (int j = 0; j < 9; j++)
                {
                    double s = m[i, j];
                    li.SubItems.Add(Convert.ToString(s));

                }
                this.listView1.Items.Add(li);
            }

        }

        private void button3_Click(object sender, EventArgs e)   //保存所有到excel
        {
            listView1.Clear();
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView1.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView1.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView1.Visible = true;

            Type quyu = typeof(xingzhengqu);
            Array Arrays = Enum.GetValues(quyu);
            double[,] m = new double[Arrays.LongLength, 9];
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                string str = Convert.ToString(Arrays.GetValue(i));
                foreach (DataGridViewRow row in this.dataGridView1.Rows)  //遍历控件中的行
                {
                    if (row.Cells["乡镇"].Value == null)
                        break;
                    string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                    if (str == "大洼县")
                    {
                        int x = Convert.ToInt32(row.Cells["ClassId00"].Value);
                        m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                    }
                    else
                    {

                        if (str.Trim() == strbiaozhi.Trim())
                        {
                            int x = Convert.ToInt32(row.Cells["ClassId00"].Value);
                            m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);

                        }
                    }
                }

            }
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = Convert.ToString(Arrays.GetValue(i));
                for (int j = 0; j < 9; j++)
                {
                    double s = m[i, j];
                    li.SubItems.Add(Convert.ToString(s));

                }
                this.listView1.Items.Add(li);
            }
            Exportlistview(listView1, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str = comboBox2.Text.ToString().Trim();
            if (str == "") 
            {
                MessageBox.Show("请先选择查询区域！");
                return;
            }
            Program.name = str;
            double[,] m = new double[9, 9];


            foreach (DataGridViewRow row in this.dataGridView2.Rows)  //遍历控件中的行
            {
                if (row.Cells["乡镇"].Value == null)
                    break;
                string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                if (str == "大洼县")
                {
                    int i = Convert.ToInt32(row.Cells["change"].Value);
                    int x = i / 10;
                    int y = i % 10;
                    m[x - 1, y - 1] += Convert.ToDouble(row.Cells["area"].Value);


                }
                else
                {

                    if (str.Trim() == strbiaozhi.Trim())
                    {
                        int i = Convert.ToInt32(row.Cells["change"].Value);
                        int x = i / 10;
                        int y = i % 10;
                        m[x - 1, y - 1] += Convert.ToDouble(row.Cells["area"].Value);
                    }
                }
            }

            listView3.Clear();
            this.listView3.View = View.Details;
            this.listView3.GridLines = true;
            this.listView3.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView3.Columns.Add(str, 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView3.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView3.Columns.Add("盐田及海水养殖场", 100, HorizontalAlignment.Right);
            this.listView3.Visible = true;
            //转移矩阵
            Type land = typeof(LandscapeType);
            Array Arrays = Enum.GetValues(land);
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = Convert.ToString(Arrays.GetValue(i));
                for (int j = 0; j < 9; j++)
                {
                    double s = m[i, j];
                    li.SubItems.Add(Convert.ToString(s));

                }
                this.listView3.Items.Add(li);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            if (listView3.Items.Count == 0)
            {
                MessageBox.Show("请先做查询！");
                return;
            }
            Exportlistview(listView3, true);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (listView3.Items.Count == 0)
            {
                MessageBox.Show("请先求面积转移矩阵！");
                return;
            }
            string strcom = comboBox2.Text.ToString().Trim();
            this.listView2.Clear();
            this.listView2.View = View.Details;
            this.listView2.GridLines = true;
            this.listView2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView2.Columns.Add(strcom, 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView2.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView2.Columns.Add("盐田及海水养殖场", 100, HorizontalAlignment.Right);
            this.listView2.Visible = true;

            //填充数据
            double[,] n = new double[9, 9];
            double[] m = new double[9];//00年值
            double[] k = new double[9];
            for (int i = 0; i < listView3.Items.Count; i++)
            {
                ListViewItem item = listView3.Items[i];
                for (int j = 0; j < item.SubItems.Count - 1; j++)
                {
                    n[i, j] = Convert.ToDouble(item.SubItems[j + 1].Text.ToString());
                    m[i] += n[i, j];
                    k[j] += n[i, j];
                }
            }
            Program.chushi09 = k;//传递值
            //计算转移概率矩阵
            double[,] x = new double[9, 9];
            for (int i = 0; i < 9; i++)
            {
                double q = 0;
                double t = 0;
                for (int j = 0; j < 9; j++)
                {
                    if (m[i] == 0)
                        continue;
                    if (i == j)
                    {
                        t = x[i, j];
                        x[i, j] = 0;
                    }

                    else
                    {
                        x[i, j] = 1 - Math.Pow(((m[i] - n[i, j]) / m[i]), (double)1 / 9);
                    }
                    q += x[i, j];
                }
                if (q == 0 && t == 0)
                    x[i, i] = 0;
                else
                    x[i, i] = 1 - q;
            }
            Program.zhuanyi = x;
            //转移矩阵
            Type land = typeof(LandscapeType);
            Array Arrays = Enum.GetValues(land);
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = Convert.ToString(Arrays.GetValue(i));
                for (int j = 0; j < 9; j++)
                {
                    double s = x[i, j];
                    string st = s.ToString("0.000000");
                    li.SubItems.Add(st);
                }
                this.listView2.Items.Add(li);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count == 0)
            {
                MessageBox.Show("请先求转移该路矩阵！");
                return;
            }
            Exportlistview(listView2, true);
        }

        //private void define_Click(object sender, EventArgs e)
        //{
        //    string year;
        //    year = comboBox3.SelectedItem.ToString();
        //    int a = int.Parse(year);
        //    double[] p;
        //    p = Program.chushi09;
        //    double[,] dou;
        //    dou = Program.zhuanyi;
        //    double[] r = new double[9];
        //    for (int z = 2010; z <= a; z++)
        //    {
        //        for (int i = 0; i < 9; i++)
        //        {
        //            double x = 0;
        //            for (int j = 0; j < 9; j++)
        //            {
        //                x = p[j] * dou[j, i] + x;
        //            }
        //            r[i] = x;
        //        }
        //        p = r;
        //    }
        //    //结果矩阵
        //    ListViewItem li3 = new ListViewItem();
        //    li3.SubItems[0].Text = Convert.ToString(year);
        //    for (int i = 0; i < 9; i++)
        //    {
        //        double s = p[i];
        //        li3.SubItems.Add(Convert.ToString(s));

        //    }
        //    this.listView4.Items.Add(li3);
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            if(int.Parse(textBox1.Text)<2010)
            {
                MessageBox.Show("应输入大于2009的年份，如2010！");
            }
            
            int a, b;
            a = int.Parse(textBox1.Text);
            b = int.Parse(textBox2.Text);
            this.listView4.Clear();
            this.listView4.View = View.Details;
            this.listView4.GridLines = true;
            this.listView4.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView4.Columns.Add(Program.name, 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView4.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView4.Columns.Add("盐田及海水养殖场", 100, HorizontalAlignment.Right);
            this.listView4.Visible = true;

            double[] p;
            p = Program.chushi09;
            double[,] dou;
            dou = Program.zhuanyi;
            double[] r = new double[9];
            for (int z = a; z <= b; z++)
            {
                for (int i = 0; i < 9; i++)
                {
                    double x = 0;
                    for (int j = 0; j < 9; j++)
                    {
                        x = p[j] * dou[j, i] + x;
                    }
                    r[i] = x;
                }
                p = r;
                //结果矩阵
                ListViewItem li3 = new ListViewItem();
                li3.SubItems[0].Text = Convert.ToString(z);
                for (int i = 0; i < 9; i++)
                {
                    double s = p[i];
                    li3.SubItems.Add(Convert.ToString(s));

                }
                this.listView4.Items.Add(li3);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Exportlistview(listView3, true);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listView5.Clear();
            this.listView5.View = View.Details;
            this.listView5.GridLines = true;
            this.listView5.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView5.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView5.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView5.Visible = true;

            Type quyu = typeof(xingzhengqu);
            Array Arrays = Enum.GetValues(quyu);
            double[,] m = new double[Arrays.LongLength, 9];
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                string str = Convert.ToString(Arrays.GetValue(i));
                foreach (DataGridViewRow row in this.dataGridView3.Rows)  //遍历控件中的行
                {
                    if (row.Cells["乡镇"].Value == null)
                        break;
                    string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                    if (str == "大洼县")
                    {
                        int x = Convert.ToInt32(row.Cells["ClassId09"].Value);
                        m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                    }
                    else
                    {
                        if (str.Trim() == strbiaozhi.Trim())
                        {
                            int x = Convert.ToInt32(row.Cells["ClassId09"].Value);
                            m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                        }
                    }
                }
            }
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = Convert.ToString(Arrays.GetValue(i));
                for (int j = 0; j < 9; j++)
                {
                    double s = m[i, j];
                    li.SubItems.Add(Convert.ToString(s));

                }
                this.listView5.Items.Add(li);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str = comboBox4.Text.ToString().Trim();
            double[] m = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (DataGridViewRow row in this.dataGridView3.Rows)  //遍历控件中的行
            {
                if (row.Cells["乡镇"].Value == null)
                    break;
                string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                if (str == "大洼县")
                {
                    int x = Convert.ToInt32(row.Cells["ClassId09"].Value);
                    m[x - 1] = Convert.ToDouble(row.Cells["面积"].Value);
                }
                else
                {
                    if (str.Trim() == strbiaozhi.Trim())
                    {
                        int x = Convert.ToInt32(row.Cells["ClassId09"].Value);
                        m[x - 1] = Convert.ToDouble(row.Cells["面积"].Value);
                    }
                }
            }

            listView5.Clear();
            this.listView5.View = View.Details;
            this.listView5.GridLines = true;
            this.listView5.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView5.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView5.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView5.Visible = true;
            ListViewItem li1 = new ListViewItem();
            li1.SubItems[0].Text = Convert.ToString(str);
            for (int i = 0; i < 9; i++)
            {
                double s = m[i];
                li1.SubItems.Add(Convert.ToString(s));
            }
            this.listView5.Items.Add(li1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listView5.Clear();
            this.listView5.View = View.Details;
            this.listView5.GridLines = true;
            this.listView5.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView5.Columns.Add("行政区", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("建设用地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("旱地", 110, HorizontalAlignment.Right);
            this.listView5.Columns.Add("林地草地", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水库坑塘", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("水田", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("河流", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("沼泽", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("滩涂", 100, HorizontalAlignment.Right);
            this.listView5.Columns.Add("盐田及海水养殖场", 110, HorizontalAlignment.Right);
            this.listView5.Visible = true;

            Type quyu = typeof(xingzhengqu);
            Array Arrays = Enum.GetValues(quyu);
            double[,] m = new double[Arrays.LongLength, 9];
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                string str = Convert.ToString(Arrays.GetValue(i));
                foreach (DataGridViewRow row in this.dataGridView3.Rows)  //遍历控件中的行
                {
                    if (row.Cells["乡镇"].Value == null)
                        break;
                    string strbiaozhi = row.Cells["乡镇"].Value.ToString().Trim();
                    if (str == "大洼县")
                    {
                        int x = Convert.ToInt32(row.Cells["ClassId09"].Value);
                        m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                    }
                    else
                    {
                        if (str.Trim() == strbiaozhi.Trim())
                        {
                            int x = Convert.ToInt32(row.Cells["ClassId09"].Value);
                            m[i, x - 1] += Convert.ToDouble(row.Cells["面积"].Value);
                        }
                    }
                }
            }
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = Convert.ToString(Arrays.GetValue(i));
                for (int j = 0; j < 9; j++)
                {
                    double s = m[i, j];
                    li.SubItems.Add(Convert.ToString(s));
                }
                this.listView5.Items.Add(li);
            }
            Exportlistview(listView5, true);
        }
    }
}
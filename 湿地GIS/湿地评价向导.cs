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
    public partial class 湿地评价向导 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public 湿地评价向导()
        {
            InitializeComponent();
        }
        private bool isIndexSelected = false;
        private bool isWeightSet = false;
        private bool isStandardize = false;
        private bool isdivided = false;
        private bool isfinished = false;
        private DataTable WeightTable;
        private DataTable _2000table, _2009table;
        public DataTable _2000final, _2009final;
        private int standarizedstep = 0;
        double break1, break2, break3, break4;
        private void 湿地评价向导_Load(object sender, EventArgs e)
        {
            #region 将二级指标添加到treeView控件中
            String connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=湿地健康预警系统.mdb";
            String sqlcommand = "select * from 评价指标2";
            OleDbConnection conn = new OleDbConnection(connectionString);
            conn.Open();
            OleDbDataAdapter dap = new OleDbDataAdapter(sqlcommand, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0].DefaultView;
            String indexname = "";
            TreeNode node1 = new TreeNode();

            OleDbCommand sqlcmd = new OleDbCommand(sqlcommand, conn);
            foreach (DataRow testRow in ds.Tables[0].Rows)
            {
                if (indexname != testRow["评价准则"].ToString())
                {
                    indexname = testRow["评价准则"].ToString();
                    node1 = new TreeNode();
                    node1.Text = indexname;
                    treeView1.Nodes.Add(node1);
                }
                String itemname = testRow["评价指标"].ToString();
                TreeNode node2 = new TreeNode();
                node2.Text = itemname;
                node1.Nodes.Add(node2);
            }
            node1.Nodes.Add("当地人对湿地的认知程度");
            node1.Nodes.Add("政府对湿地保护的指标");
            conn.Close();
            #endregion
            #region 将年份显示在ComboBox1中
            for (int y = 2000; y <= 2009; y++)
            {
                comboBox1.Items.Add(y.ToString() + "年");
            }
            comboBox1.Text = comboBox1.Items[0].ToString();
            #endregion
            #region 将2000年和2009年各指标的数据加载到_2000table和_2009table中
            String connectionString2 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Supermap课件\" +
                            @"2013-07-20李珊珊\湿地健康预警系统.mdb";
            String sql1 = "select * from 2000年";
            String sql2 = "select * from 2009年";
            OleDbConnection conn2 = new OleDbConnection(connectionString2);
            conn.Open();
            OleDbDataAdapter dad2 = new OleDbDataAdapter(sql1, conn);
            DataSet ds2 = new DataSet();
            dad2.Fill(ds2);
            dataGridView1.DataSource = ds2.Tables[0].DefaultView;
            _2000table = ((DataView)dataGridView1.DataSource).Table;   //将dataGridView表中的内容赋给DataTable类型对象
            dad2 = new OleDbDataAdapter(sql2, conn);
            ds2 = new DataSet();
            dad2.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0].DefaultView;
            _2009table = ((DataView)dataGridView2.DataSource).Table;
            #endregion
            #region 设定dataGridView1和dataGridView2的状态
            dataGridView1.DataSource = null;
            dataGridView2.Visible = false;
            #endregion
            #region 往comboBox2控件中加入年份数据
            for (int i = 2000; i <= 2009; i++)
            {
                comboBox2.Items.Add(i.ToString() + "年");
            }
            comboBox2.Text = comboBox2.Items[0].ToString();
            #endregion
            dataGridView3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                listBox1.Items.Add(treeView1.SelectedNode.Text);
            }
            else
                MessageBox.Show("您选的是评价准则，故无法插入到右边的指标体系列表中。");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (TreeNode node in treeView1.Nodes)
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    listBox1.Items.Add(node2.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            isIndexSelected = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isIndexSelected = true;
            MessageBox.Show("指标已确定。");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            textBox1.Text = "0.13";
            textBox2.Text = "0.15";
            textBox3.Text = "0.5";
            textBox4.Text = "0.12";
            textBox5.Text = "0.1";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Double in1, in2, in3, in4, in5;
                in1 = Convert.ToDouble(textBox1.Text);
                in2 = Convert.ToDouble(textBox2.Text);
                in3 = Convert.ToDouble(textBox3.Text);
                in4 = Convert.ToDouble(textBox4.Text);
                in5 = Convert.ToDouble(textBox5.Text);
                if (in1 + in2 + in3 + in4 + in5 == 1)
                {
                    WeightTable = new DataTable();
                    WeightTable.Columns.Add(label1.Text);
                    WeightTable.Columns.Add(label2.Text);
                    WeightTable.Columns.Add(label3.Text);
                    WeightTable.Columns.Add(label4.Text);
                    WeightTable.Columns.Add(label5.Text);
                    DataRow dr = WeightTable.NewRow();
                    dr[label1.Text] = textBox1.Text;
                    dr[label2.Text] = textBox2.Text;
                    dr[label3.Text] = textBox3.Text;
                    dr[label4.Text] = textBox4.Text;
                    dr[label5.Text] = textBox5.Text;
                    WeightTable.Rows.Add(dr);
                    isWeightSet = true;
                    MessageBox.Show("唯一值设定成功。");
                }
                else
                {
                    MessageBox.Show("权重值相交不得1，无法继续。");
                    isWeightSet = false;
                }
            }
            catch (Exception ea)
            {
                MessageBox.Show("数值格式出现异常");
                isWeightSet = false;
            }
        }
        /// <summary>
        /// 计算一个表的标准化值
        /// </summary>
        /// <param name="dt1"></param>
        private void Standardize(DataTable dt1)
        {
            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                String FieldName = dt1.Columns[i].ToString();
                String Name = FieldName.Substring(5);
                List<double> values = new List<double>();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    values.Add(Convert.ToDouble(dt1.Rows[j][FieldName]));
                }
                double minvalue, maxvalue;
                minvalue = values[0]; maxvalue = values[0];
                foreach (double v in values)
                {
                    if (minvalue > v) minvalue = v;
                    if (maxvalue < v) maxvalue = v;
                }
                String connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=湿地健康预警系统.mdb";
                String sq1 = "select 评价指标,与健康相关性 from 评价指标2 where 评价指标='" + Name + "'";
                OleDbConnection conn = new OleDbConnection(connectionString);
                conn.Open();
                OleDbDataAdapter dad = new OleDbDataAdapter(sq1, conn);
                DataSet ds = new DataSet();
                dad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0]["与健康相关性"].ToString() == "+")
                {
                    for (int j = 0; j < values.Count; j++)
                    {
                        values[j] = (values[j] - minvalue) / (maxvalue - minvalue);
                    }
                }
                else
                {
                    for (int j = 0; j < values.Count; j++)
                    {
                        values[j] = (maxvalue - values[j]) / (maxvalue - minvalue);
                    }
                }
                conn.Close();
                for (int j = 0; j < values.Count; j++)
                {
                    dt1.Rows[j][i] = Convert.ToString(values[j]);
                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (standarizedstep == 0)
            {
                Standardize(_2000table);
                Standardize(_2009table);
                isStandardize = true;
            }
            if (comboBox1.Text == "2000年" || comboBox1.Text == "2001年" || comboBox1.Text == "2002年" || comboBox1.Text == "2003年" || comboBox1.Text == "2004年" || comboBox1.Text == "2005年")
            {
                dataGridView1.DataSource = _2000table;
            }
            else
            {
                dataGridView1.DataSource = _2009table;
            }
            standarizedstep++;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            isdivided = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            break1 = 0.2; break2 = 0.4; break3 = 0.6; break4 = 0.8;
            textBox6.Text = Convert.ToString(break1);
            textBox7.Text = Convert.ToString(break2);
            textBox8.Text = Convert.ToString(break3);
            textBox9.Text = Convert.ToString(break4);
            textBox11.Text = "0 - " + break1.ToString();
            textBox12.Text = break1.ToString() + " - " + break2.ToString();
            textBox13.Text = break2.ToString() + " - " + break3.ToString();
            textBox14.Text = break3.ToString() + " - " + break4.ToString();
            textBox15.Text = break4.ToString() + " - 1";
            isdivided = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                break1 = Convert.ToDouble(textBox6.Text);
                break2 = Convert.ToDouble(textBox7.Text);
                break3 = Convert.ToDouble(textBox8.Text);
                break4 = Convert.ToDouble(textBox9.Text);
                if (!(break1 < break2 && break2 < break3 && break3 < break4 && break1 > 0 && break4 < 1))
                {
                    MessageBox.Show("输入的范围不正确。");
                    isdivided = false;
                }
                else
                {
                    textBox11.Text = "0 - " + break1.ToString();
                    textBox12.Text = break1.ToString() + " - " + break2.ToString();
                    textBox13.Text = break2.ToString() + " - " + break3.ToString();
                    textBox14.Text = break3.ToString() + " - " + break4.ToString();
                    textBox15.Text = break4.ToString() + " - 1";
                    isdivided = true;
                }
            }
            catch
            {
                MessageBox.Show("填写的格式不正确。");
                isdivided = false;
            }
        }

        private DataTable calculate_standard_value(DataTable dtf, String year, DataTable dt1)
        {
            List<String> county = new List<string>();
            String connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=湿地健康预警系统.mdb";
            String sql = "select 评价准则,评价指标 as 评价指标2,权重 from 评价指标2";
            OleDbConnection conn = new OleDbConnection(connectionString);
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter dad = new OleDbDataAdapter(sql, conn);
            dad.Fill(ds);
            DataTable dt3 = ds.Tables[0];//dt3存储了二级评价指标、其所在一级指标和其权重值
            //dataGridView1.DataSource = dt3;
            conn.Close();
            dtf = new DataTable();
            dtf.Columns.Add("行政区");
            dtf.Columns.Add(year + "湿地健康评价值");
            dtf.Columns.Add(year + "划分类别");
            #region 将辽东湾的县名添加到对象名为county的String类数组中
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                county.Add(dt1.Rows[i]["行政区"].ToString());
            }
            #endregion
            #region 以县为单位，求出各县的湿地健康评价值
            foreach (string cou_name in county)
            {
                Double evaluation_value = 0;
                DataRow[] rows = dt1.Select("行政区='" + cou_name + "'");
                DataRow row = rows[0];
                #region 以二级评价指标为单位
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    String _1stindexname = dt3.Rows[i]["评价准则"].ToString();
                    String _2ndindexname = dt3.Rows[i]["评价指标2"].ToString();
                    Double _1stindexweight = Convert.ToDouble(WeightTable.Rows[0][_1stindexname]);
                    Double _2ndindexweight = Convert.ToDouble(dt3.Rows[i]["权重"]);
                    Double StandardValue = Convert.ToDouble(row[year + _2ndindexname]);
                    Double accuvalue = _1stindexweight * _2ndindexweight * StandardValue;
                    evaluation_value += accuvalue;
                }
                #endregion
                DataRow value2 = dtf.NewRow();
                value2[0] = cou_name;
                value2[1] = evaluation_value;

                if (evaluation_value >= 0 && evaluation_value <= break1)
                    value2[2] = "病态";
                if (evaluation_value > break1 && evaluation_value <= break2)
                    value2[2] = "不健康";
                if (evaluation_value > break2 && evaluation_value <= break3)
                    value2[2] = "亚健康";
                if (evaluation_value > break3 && evaluation_value <= break4)
                    value2[2] = "健康";
                if (evaluation_value > break4 && evaluation_value <= 1)
                    value2[2] = "很健康";
                dtf.Rows.Add(value2);
            }
            #endregion
            return dtf;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (isdivided && isIndexSelected && isStandardize && isWeightSet)
            {
                _2000final = calculate_standard_value(_2000final, "2000年", _2000table);
                dataGridView1.DataSource = _2000final;
                _2009final = calculate_standard_value(_2009final, "2009年", _2009table);
                dataGridView2.DataSource = _2009final;
                dataGridView2.Visible = false;
                if (comboBox2.Text == "2000年" || comboBox2.Text == "2001年" || comboBox2.Text == "2002年" || comboBox2.Text == "2003年" || comboBox2.Text == "2004年" || comboBox2.Text == "2005年")
                {
                    dataGridView4.DataSource = _2000final;
                }
                else
                {
                    dataGridView4.DataSource = _2009final;
                }
                isfinished = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (isfinished)
            {
                MessageBox.Show("分级完成。");
                this.Close();
            }
        }
    }
}
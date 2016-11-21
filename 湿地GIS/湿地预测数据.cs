using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

using Microsoft.Office.Interop.Owc11;
using System.Collections;
using Microsoft.Office.Interop;
using OWC11 = Microsoft.Office.Interop.Owc11;
using System.IO;

namespace 湿地GIS
{
    public partial class 湿地预测数据 : DevComponents.DotNetBar.Metro.MetroForm
    {
        ChartChartTypeEnum _Type;
        public 湿地预测数据()
        {
            InitializeComponent();
        }
        string st;//dilei
        string st1;//diqu
        private void showChart(ChartChartTypeEnum Type)
        {
            
            try
            {
                axChartSpace2.Clear();
                ChChart objChart = axChartSpace2.Charts.Add(0);

                objChart.Type = Type;
                objChart.HasLegend = true;
                objChart.HasTitle = true;
                objChart.Title.Caption = "湿地预测数据";
                objChart.Axes[0].HasTitle = true;
                objChart.Axes[0].Title.Caption = "年份";
                objChart.Axes[1].HasTitle = true;
                objChart.Axes[1].Title.Caption = "面积";

                objChart.SeriesCollection.Add(0);
                objChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames,
                 +(int)ChartSpecialDataSourcesEnum.chDataLiteral, st);
                //st = dataGridView1.Columns[1].HeaderText;
                objChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                 +(int)ChartSpecialDataSourcesEnum.chDataLiteral,
                 dataGridView1.Rows[0].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[1].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[2].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[3].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[4].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[5].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[6].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[7].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[8].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[9].Cells[0].Value.ToString() + '\t' + dataGridView1.Rows[10].Cells[0].Value.ToString() + '\t');
                Random r = new Random(DateTime.Now.Second);
                string w1 = dataGridView1.Rows[0].Cells[1].Value.ToString();
                string w2 = dataGridView1.Rows[1].Cells[1].Value.ToString();
                string w3 = dataGridView1.Rows[2].Cells[1].Value.ToString();
                string w4 = dataGridView1.Rows[3].Cells[1].Value.ToString();
                string w5 = dataGridView1.Rows[4].Cells[1].Value.ToString();
                string w6 = dataGridView1.Rows[5].Cells[1].Value.ToString();
                string w7 = dataGridView1.Rows[6].Cells[1].Value.ToString();
                string w8 = dataGridView1.Rows[7].Cells[1].Value.ToString();
                string w9 = dataGridView1.Rows[8].Cells[1].Value.ToString();
                string w10 = dataGridView1.Rows[9].Cells[1].Value.ToString();
                string w11 = dataGridView1.Rows[10].Cells[1].Value.ToString();
                objChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                 (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                 w1 + '\t' + w2 + '\t' + w3 + '\t' + w4 + '\t' + w5 + '\t' + w6 + '\t' + w7 + '\t' + w8 + '\t' + w9 + '\t' + w10 + '\t' + w11 + '\t');



            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _Type = Type;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            st = comboBox2.Text.ToString().Trim();//dilei
            st1 = comboBox1.Text.ToString().Trim();//diqu
            OleDbConnection my_conn;
            OleDbDataAdapter my_adapter;
            DataSet my_ds;
            C_conn c1 = new C_conn();
            my_conn = c1.get_conn();
            string selectstr = "select 预测年份," + st + " from biao3 where 行政区 = '" + st1 + "'";
            my_adapter = new OleDbDataAdapter(selectstr, my_conn);
            my_ds = new DataSet();
            my_adapter.Fill(my_ds, "biao3");
            dataGridView1.DataSource = my_ds.Tables["biao3"];
            my_conn.Close();

            showChart(ChartChartTypeEnum.chChartTypeColumnClustered);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            showChart(_Type);
        }


    }
}
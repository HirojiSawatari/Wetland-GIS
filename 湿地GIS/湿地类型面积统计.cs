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
    public partial class 湿地类型面积统计 : DevComponents.DotNetBar.Metro.MetroForm
    {
        ChartChartTypeEnum _Type;
        public 湿地类型面积统计()
        {
            InitializeComponent();
        }
        int m;
        string st;
        private void showChart(ChartChartTypeEnum Type)
        {
            try
            {
                axChartSpace1.Clear();
                ChChart objChart = axChartSpace1.Charts.Add(0);

                objChart.Type = Type;
                objChart.HasLegend = true;
                objChart.HasTitle = true;
                objChart.Title.Caption = "湿地类型面积";
                objChart.Axes[0].HasTitle = true;
                objChart.Axes[0].Title.Caption = "湿地类型";
                objChart.Axes[1].HasTitle = true;
                objChart.Axes[1].Title.Caption = "面积";

                objChart.SeriesCollection.Add(0);
                objChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames,
                 +(int)ChartSpecialDataSourcesEnum.chDataLiteral, st);
                st=dataGridView1.Columns[1].HeaderText;
                objChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                 +(int)ChartSpecialDataSourcesEnum.chDataLiteral,
                 dataGridView1.Columns[1].HeaderText + '\t' + dataGridView1.Columns[2].HeaderText + '\t' + dataGridView1.Columns[3].HeaderText + '\t' + dataGridView1.Columns[4].HeaderText + '\t' + dataGridView1.Columns[5].HeaderText + '\t' + dataGridView1.Columns[6].HeaderText + '\t' + dataGridView1.Columns[7].HeaderText + '\t' + dataGridView1.Columns[8].HeaderText + '\t' + dataGridView1.Columns[9].HeaderText + '\t');
                Random r = new Random(DateTime.Now.Second);
                string w1 = dataGridView1.Rows[0].Cells[1].Value.ToString();
                string w2 = dataGridView1.Rows[0].Cells[2].Value.ToString();
                string w3 = dataGridView1.Rows[0].Cells[3].Value.ToString();
                string w4 = dataGridView1.Rows[0].Cells[4].Value.ToString();
                string w5 = dataGridView1.Rows[0].Cells[5].Value.ToString();
                string w6 = dataGridView1.Rows[0].Cells[6].Value.ToString();
                string w7 = dataGridView1.Rows[0].Cells[7].Value.ToString();
                string w8 = dataGridView1.Rows[0].Cells[8].Value.ToString();
                string w9 = dataGridView1.Rows[0].Cells[9].Value.ToString();
                objChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                 (int)ChartSpecialDataSourcesEnum.chDataLiteral,
                 w1 + '\t' + w2 + '\t' + w3 + '\t' + w4 + '\t' + w5 + '\t' + w6 + '\t' + w7 + '\t' + w8 + '\t' + w9 + '\t');


               
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

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            showChart(_Type);
        }

        private void 湿地类型面积统计_Load(object sender, EventArgs e)
        {
            //showChart(ChartChartTypeEnum.chChartTypeLine);
        }

        private void comboBox2_S(object sender, EventArgs e)
        {
            int m = Convert.ToInt32(comboBox1.Text.ToString());
            string st = comboBox2.Text.ToString().Trim();
            OleDbConnection my_conn;
            OleDbDataAdapter my_adapter;
            DataSet my_ds;
            C_conn c1 = new C_conn();
            my_conn = c1.get_conn();
            string selectstr = "select 类型,建设用地,旱地,林地草地,河流,水库坑塘,水田,沼泽,滩涂,盐田及海水养殖场 from biao1 where 年份 = " + m + " and 行政区 = '" + st + "'";
            my_adapter = new OleDbDataAdapter(selectstr, my_conn);
            my_ds = new DataSet();
            my_adapter.Fill(my_ds, "biao1");
            dataGridView1.DataSource = my_ds.Tables["biao1"];
            my_conn.Close();

            showChart(ChartChartTypeEnum.chChartTypeLine);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                showChart(ChartChartTypeEnum.chChartTypeColumnClustered);
                return;
            }
            if (radioButton2.Checked)
            {
                showChart(ChartChartTypeEnum.chChartTypeColumn3D);
                return;
            }
            if (radioButton3.Checked)
            {
                showChart(ChartChartTypeEnum.chChartTypeLineMarkers);
                return;
            }
        }


    }
}
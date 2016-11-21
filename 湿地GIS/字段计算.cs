using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SuperMap.Realspace;
using SuperMap.UI;
using SuperMap.Data;
using SuperMap.Mapping;
using System.Diagnostics;
using SuperMap.Data.Conversion;

namespace 湿地GIS
{
    public partial class 字段计算 : DevComponents.DotNetBar.Metro.MetroForm
    {
        private DatasetVector dv;
        private int i;
        private string tc;
        private string nm;
        private string nm2;
        public 字段计算()
        {
            InitializeComponent();
        }

        private void 字段计算_Load(object sender, EventArgs e)
        {
            int x = SampleRun.mapControl1.Map.Layers.Count;
            for (i = 0; i < x; i++)
            {
                comboBoxEx1.Items.Add(SampleRun.mapControl1.Map.Layers[i].Name.ToString());
            }
        }

        private void comboBoxEx1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            nm = comboBoxEx1.SelectedItem.ToString();
            dv = SampleRun.mapControl1.Map.Layers[nm].Dataset as DatasetVector;
            if (dv == null)
            {
                MessageBox.Show("此图层无字段！");
            }
            else
            {
                int y = dv.FieldInfos.Count;
                for (i = 0; i < y; i++)
                {
                    listBox1.Items.Add(dv.FieldInfos[i].Name.ToString());
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            tc = comboBoxEx2.SelectedItem.ToString();
            nm2 = textBoxX1.Text.Trim();
            textBoxX1.Text = nm2 + tc;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Length == 0)
            {
                MessageBox.Show("查询信息不能为空");
                return;
            }
            Int32 layerCount = SampleRun.mapControl1.Map.Layers.Count;
            if (layerCount == 0)
            {
                MessageBox.Show("请先打开一个矢量数据集！");
                return;
            }
            string tx = textBoxX1.Text.ToString();
            QueryParameter queryParameter = new QueryParameter();
            queryParameter.AttributeFilter = tx;
            queryParameter.CursorType = CursorType.Static;

            Boolean hasGeometry = false;
            DatasetVector dataset = SampleRun.mapControl1.Map.Layers[nm].Dataset as DatasetVector;
            Recordset recordset = dataset.Query(queryParameter);
            if (recordset.RecordCount > 0)
            {
                hasGeometry = true;
            }
            Selection selection = SampleRun.mapControl1.Map.Layers[nm].Selection;
            selection.FromRecordset(recordset);
            recordset.Dispose();
            if (!hasGeometry)
            {
                MessageBox.Show("没有符合查询条件的结果或查询条件有误，请重新确认后查询！");
            }
            queryParameter.Dispose();
            SampleRun.mapControl1.Refresh();
            hasGeometry = false;
            this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            tc = listBox1.SelectedItem.ToString();
            nm2 = textBoxX1.Text.Trim();
            textBoxX1.Text = nm2 + tc;
        }
    }
}
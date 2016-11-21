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
    public partial class 条件查询 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public 条件查询()
        {
            InitializeComponent();
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
            QueryParameter queryParameter = new QueryParameter();
            queryParameter.AttributeFilter = textBoxX1.Text;
            queryParameter.CursorType = CursorType.Static;
            Boolean hasGeometry = false;
            foreach (Layer layer in SampleRun.mapControl1.Map.Layers)
            {
                DatasetVector dataset = layer.Dataset as DatasetVector;
                if (dataset == null)
                {
                    continue;
                }
                Recordset recordset = dataset.Query(queryParameter);
                if (recordset.RecordCount > 0)
                {
                    hasGeometry = true;
                }
                Selection selection = layer.Selection;
                selection.FromRecordset(recordset);
                recordset.Dispose();
            }
            if (!hasGeometry)
            {
                MessageBox.Show("没有符合查询条件的结果或查询条件有误，请重新确认后查询！");
            }
            queryParameter.Dispose();
            SampleRun.mapControl1.Refresh();
            hasGeometry = false;
            this.Close();
        }
    }
}
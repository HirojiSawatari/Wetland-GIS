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
using SuperMap.Analyst.SpatialAnalyst;

namespace 湿地GIS
{
    public partial class 合并 : DevComponents.DotNetBar.Metro.MetroForm
    {
        private int i;
        public 合并()
        {
            InitializeComponent();
        }

        private void 叠加分析_Load(object sender, EventArgs e)
        {
            int x = SampleRun.mapControl1.Map.Layers.Count;
            for (i = 0; i < x; i++)
            {
                comboBoxEx3.Items.Add(SampleRun.mapControl1.Map.Layers[i].Name.ToString());
            }
            int y = SampleRun.mapControl1.Map.Layers.Count;
            for (i = 0; i < x; i++)
            {
                comboBoxEx2.Items.Add(SampleRun.mapControl1.Map.Layers[i].Name.ToString());
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] a = new string[2];
                a[0] = "ClassId00";
                a[1] = "Class_Name";
                string[] b = new string[2];
                b[0] = "ClassId09";
                b[1] = "name";
                Datasource Ds = SampleRun.workspace1.Datasources[0];
                string name_1 = comboBoxEx2.SelectedItem.ToString(); //图层1
                string name_2 = comboBoxEx3.SelectedItem.ToString(); //图层2
                DatasetVector dataset = SampleRun.mapControl1.Map.Layers[name_1].Dataset as DatasetVector;  //第一数据集
                DatasetVector eraseDataset = SampleRun.mapControl1.Map.Layers[name_2].Dataset as DatasetVector;  //第二数据集
                OverlayAnalystParameter parameter = new OverlayAnalystParameter();
                DatasetVectorInfo datasetvectorInfoUpdate = new DatasetVectorInfo();
                String DtName = Ds.Datasets.GetAvailableDatasetName("new");
                datasetvectorInfoUpdate.Type = dataset.Type;
                datasetvectorInfoUpdate.Name = DtName;
                parameter.SourceRetainedFields = a;
                parameter.OperationRetainedFields = b;
                DatasetVector exdv = Ds.Datasets.Create(datasetvectorInfoUpdate);
                bool c = OverlayAnalyst.Union(dataset, eraseDataset, exdv, parameter);
                SampleRun.layersControl1.Map.Layers.Add(exdv, true);
                SampleRun.layersControl1.Map.Refresh();
            }
            catch
            {
                MessageBox.Show("两图层不在同一坐标系中！");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using SuperMap.Data;
using SuperMap.Mapping;
using SuperMap.UI;
using SuperMap.Realspace;

namespace 湿地GIS
{
    public enum MeasureAction
    {
        None,
        Distance,
        Area,
        Angle
    }
    public partial class 主界面 : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        private SampleRun m_sampleRun;
        private Datasource m_datasource;
        Double curPlayRate = 0.0; 
        Double updatedPlayRate = 0.0;

        // 用于记录速度改变后的速度
        Double updatedSpeed = 0.0;
        public 主界面()
        {
            InitializeComponent();
            //Initialize();
        }
       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Select2;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomFree;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Length == 0)
            {
                MessageBox.Show("查询信息不能为空");
                return;
            }
            Int32 layerCount = mapControl1.Map.Layers.Count;
            if (layerCount == 0)
            {
                MessageBox.Show("请先打开一个矢量数据集！");
                return;
            }
            string dl = toolStripComboBox1.SelectedItem.ToString();
            string nf = toolStripComboBox2.SelectedItem.ToString();
            string year;
            if (nf == "2000")
            {
                year = "00";
            }
            else
            {
                year = "09";
            }
            string tj = toolStripTextBox1.Text;
            string sql = dl + year + tj;
            QueryParameter queryParameter = new QueryParameter();
            queryParameter.AttributeFilter = sql;
            queryParameter.CursorType = CursorType.Static;
            Boolean hasGeometry = false;
            DatasetVector dataset = SampleRun.workspace1.Datasources[0].Datasets["盘锦行政区划"] as DatasetVector;
            Recordset recordset = dataset.Query(queryParameter);
            if (recordset.RecordCount > 0)
            {
                hasGeometry = true;
            }
            Selection selection = SampleRun.mapControl1.Map.Layers[0].Selection;
            selection.FromRecordset(recordset);
            recordset.Dispose();
            if (!hasGeometry)
            {
                MessageBox.Show("没有符合查询条件的结果或查询条件有误，请重新确认后查询！");
            }
            queryParameter.Dispose();
            mapControl1.Refresh();
            hasGeometry = false;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Selection[] selection = mapControl1.Map.FindSelection(true);
            if (selection == null || selection.Length == 0)
            {
                MessageBox.Show("请选择要查询属性的空间对象");
                return;
            }
            Program.re = selection[0].ToRecordset();
            图查属性 ab = new 图查属性();
            ab.Show();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            SymbolLibraryDialog.ShowDialog(Program.resources, SymbolType.Marker); ;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            SymbolLibraryDialog.ShowDialog(Program.resources, SymbolType.Line);

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            SymbolLibraryDialog.ShowDialog(Program.resources, SymbolType.Fill);

        }

        private void buttonX6_Click(object sender, EventArgs e)
        {

            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                转为TAB ad = new 转为TAB();
                ad.Show();
            }
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                转为SIT ae = new 转为SIT();
                ae.Show();
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                转为SHP ac = new 转为SHP();
                ac.Show();
            }
        }

        private void mapControl1_DoubleClick(object sender, EventArgs e)
        {
            Selection[] selection = mapControl1.Map.FindSelection(true);
            Recordset recordset = selection[0].ToRecordset();
            FieldInfo objFieldinfo = null;

            if (recordset.RecordCount > 0)
            {
                string str = " ";
                for (int i = 0; i < recordset.FieldCount; i++)
                {
                    objFieldinfo = recordset.GetFieldInfos()[i];

                    //String fieldName = recordset.GetFieldInfos()[i].Name;
                    str += objFieldinfo.Name;
                    str += ":" + recordset.GetFieldValue(i).ToString() + "\n";

                }
                MessageBox.Show(str, "属性");
            }
            recordset.Close();
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {

        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                合并 af = new 合并();
                af.Show();
            }
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                属性筛选 af = new 属性筛选();
                af.Show();
            }
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxResult.Clear();
                m_sampleRun.DrawMeasure(MeasureAction.Distance);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxResult.Clear();
                m_sampleRun.DrawMeasure(MeasureAction.Area);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void buttonX12_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxResult.Clear();
                m_sampleRun.DrawMeasure(MeasureAction.Angle);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreatePoint);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateLine);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreatePolyline);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateFreePolyline);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateCurve);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateParallel);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreatePolygon);
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateRectangle);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateCircle3P);
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateRoundRectangle);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(SuperMap.UI.Action.CreateParallelogram);
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(Action.CreateNorthArrow);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(Action.CreateMapScale);
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(Action.Pan);
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            m_sampleRun.SetLayoutAction(Action.CreateText);
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            try
            {
                m_sampleRun.OutputBMP();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            try
            {
                m_sampleRun.OutputJPG();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            try
            {
                m_sampleRun.OutputPNG(true);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            字段计算 ai = new 字段计算();
            ai.Show();
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            马尔科夫模型 ah = new 马尔科夫模型();
            ah.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s_type = comboBox1.SelectedItem.ToString();
            GeoMapScale geomapscale = new GeoMapScale();
            if (s_type == "1：5000")
            {
                geomapscale.Scale = 1 / 5000;
                mapControl1.Map.Scale = 0.0002;
                mapControl1.Map.Refresh();

            }
            else if (s_type == "1：10000")
            {
                geomapscale.Scale = 1 / 10000;
                mapControl1.Map.Scale = 0.0001;
                mapControl1.Map.Refresh();
            }
            else if (s_type == "1：25000")
            {
                geomapscale.Scale = 1 / 25000;
                mapControl1.Map.Scale = 0.00004;
                mapControl1.Map.Refresh();
            }
            else if (s_type == "1：50000")
            {
                geomapscale.Scale = 1 / 50000;
                mapControl1.Map.Scale = 0.00002;
                mapControl1.Map.Refresh();
            }
            else if (s_type == "1：100000")
            {
                geomapscale.Scale = 1 / 100000;
                mapControl1.Map.Scale = 0.00001;
                mapControl1.Map.Refresh();
            }
            else if (s_type == "1：250000")
            {
                geomapscale.Scale = 1 / 250000;
                mapControl1.Map.Scale = 0.000004;
                mapControl1.Map.Refresh();
            }
            else if (s_type == "1：500000")
            {
                geomapscale.Scale = 1 / 500000;
                mapControl1.Map.Scale = 0.000002;
                mapControl1.Map.Refresh();
            }
            else if (s_type == "1：1000000")
            {
                geomapscale.Scale = 1 / 1000000;
                mapControl1.Map.Scale = 0.00000001;
                mapControl1.Map.Refresh();
            }
        }

        private void buttonX17_Click(object sender, EventArgs e)
        {
            条件查询 aj = new 条件查询();
            aj.Show();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mapControl1.Dispose();
                mapControl2.Dispose();
                mapLayoutControl1.Dispose();
                workspace1.Dispose();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "smwu 工作空间文件(*.smwu)|*.smwu|smw 工作空间文件(*.smw)|*.smw";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mapControl1.Map.Close();
                mapControl2.Map.Close();
                workspace1.Close();
                mapControl1.Map.Refresh();
                mapControl2.Map.Refresh();
                String fileName = openFileDialog1.FileName;
                WorkspaceConnectionInfo connectionInfo = new WorkspaceConnectionInfo(fileName);
                workspace1.Open(connectionInfo);
                Program.resources = workspace1.Resources;
                workspaceControl1.WorkspaceTree.Workspace = workspace1;
                mapControl1.Map.Workspace = workspace1;
                mapControl2.Map.Workspace = workspace1;
                sceneControl1.Scene.Workspace = workspace1;
                mapLayoutControl1.MapLayout.Workspace = workspace1;
                m_datasource = workspace1.Datasources[0];
                if (workspace1.Maps.Count == 0)
                {
                    MessageBox.Show("当前工作空间中不存在地图!");
                }

                m_sampleRun = new SampleRun(workspaceControl1, layersControl1, mapControl1, mapControl2, mapLayoutControl1, toolStripStatusLabel2, textBoxResult, toolStripStatusLabel3, sceneControl1);
                m_sampleRun.AddDatasetToScene(m_datasource.Datasets[0].Name);
                m_sampleRun.RegisterEvents(false);
            }
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            打印 al = new 打印();
            al.Show();
        }

        private void buttonX22_Click_1(object sender, EventArgs e)
        {
            m_sampleRun.RegisterEvents(true);
            //m_sampleRun.TransferCamera();
        }

        private void m_buttonFly_Click(object sender, EventArgs e)
        {
            m_sampleRun.Fly();
        }

        private void m_buttonFaster_Click(object sender, EventArgs e)
        {
            curPlayRate = m_sampleRun.FlyManager.PlayRate;
            m_sampleRun.FlyManager.PlayRate = curPlayRate * 1.2;
            updatedPlayRate = m_sampleRun.FlyManager.PlayRate;
            updatedSpeed = Math.Round(50 * updatedPlayRate, 2);
            m_textBoxSpeed.Text = updatedSpeed.ToString();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            数据集管理 f4 = new 数据集管理();
            f4.Show();
        }

        private void buttonX23_Click(object sender, EventArgs e)
        {
            try
            {
                m_sampleRun.RegisterEvents(false);
                sceneControl1.Action = SuperMap.UI.Action3D.Pan;
                sceneControl1.Scene.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void m_buttonSlower_Click(object sender, EventArgs e)
        {
            curPlayRate = m_sampleRun.FlyManager.PlayRate;
            m_sampleRun.FlyManager.PlayRate = curPlayRate * 0.8;
            updatedPlayRate = m_sampleRun.FlyManager.PlayRate;
            updatedSpeed = Math.Round(50 * updatedPlayRate, 2);
            m_textBoxSpeed.Text = updatedSpeed.ToString();
        }

        private void m_buttonPause_Click(object sender, EventArgs e)
        {
            m_sampleRun.Pause();
        }

        private void m_buttonStop_Click(object sender, EventArgs e)
        {
            m_sampleRun.Stop();
        }

        private void buttonX24_Click(object sender, EventArgs e)
        {
            try
            {
                SuperMap.Data.Rectangle2D bounds = new SuperMap.Data.Rectangle2D(-120, -90, 120, 90);
                sceneControl1.Scene.EnsureVisible(bounds);
                sceneControl1.Scene.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void buttonX28_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                裁剪 af = new 裁剪();
                af.Show();
            }
        }

        private void buttonX29_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                擦除 af = new 擦除();
                af.Show();
            }
        }

        private void buttonX30_Click(object sender, EventArgs e)
        {
            if (SampleRun.workspace1 == null)
            {
                MessageBox.Show("尚未加载数据！");
            }
            else
            {
                求交 af = new 求交();
                af.Show();
            }
        }
        private void buttonX9_Click_1(object sender, EventArgs e)
        {
            SampleRun.dilei = comboBox2.SelectedItem.ToString();
            m_sampleRun.setThemeGraphBar3DVisible(workspaceControl1.WorkspaceTree.Workspace);
        }

        private void buttonX31_Click(object sender, EventArgs e)
        {
            湿地类型面积统计 f4 = new 湿地类型面积统计();
            f4.Show();
        }

        private void buttonX32_Click(object sender, EventArgs e)
        {
            湿地转移概率统计 f5 = new 湿地转移概率统计();
            f5.Show();
        }

        private void buttonX34_Click(object sender, EventArgs e)
        {
            湿地预测数据 f6 = new 湿地预测数据();
            f6.Show();
        }

        private void buttonX35_Click(object sender, EventArgs e)
        {
            湿地类型变化统计 f7 = new 湿地类型变化统计();
            f7.Show();
        }

        private void buttonX33_Click(object sender, EventArgs e)
        {
            湿地评价向导 f8 = new 湿地评价向导();
            f8.Show();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }
    }
}
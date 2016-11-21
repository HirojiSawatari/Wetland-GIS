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
    public partial class 转为TAB : DevComponents.DotNetBar.Metro.MetroForm
    {
        private Datasource srcDatasource1;
        private DatasetVector sourceRegion1;
        private DataExport dataExport1;
        public 转为TAB()
        {
            dataExport1 = new DataExport();
            InitializeComponent();
        }
        private void SetExportSettings(SuperMap.Data.Conversion.FileType type, String targetFilePath)
        {
            try
            {
                ExportSetting setting = new ExportSetting();
                setting.TargetFilePath = targetFilePath;
                setting.TargetFileType = type;
                setting.IsOverwrite = true;
                setting.SourceData = sourceRegion1;
                dataExport1.ExportSettings.Add(setting);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            string sp = comboBox1.SelectedItem.ToString();
            try
            {
                srcDatasource1 = SampleRun.workspace1.Datasources[0];
                sourceRegion1 = srcDatasource1.Datasets[sp] as DatasetVector;
                if (sourceRegion1 == null)
                {
                    MessageBox.Show(this, "图层不符条件，无法转换！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    String targetDirectory = @"..\..\SampleData";
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    String targetFilePath = targetDirectory + @"\ETab.tab";
                    this.SetExportSettings(FileType.TAB, targetFilePath);
                    SampleRun.mapControl1.Map.Layers.Clear();
                    dataExport1.Run();
                    SampleRun.mapControl1.Map.Layers.Add(sourceRegion1, true);
                    SampleRun.mapControl1.Map.ViewEntire();
                    SampleRun.mapControl1.Map.Refresh();
                    System.Diagnostics.Process.Start("explorer.exe", "/n,/select, " + targetFilePath);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void 转为TAB_Load(object sender, EventArgs e)
        {
            int i;
            int x = SampleRun.workspace1.Datasources[0].Datasets.Count;
            for (i = 0; i < x; i++)
            {
                comboBox1.Items.Add(SampleRun.workspace1.Datasources[0].Datasets[i].Name.ToString());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using SuperMap.Data;
using System.Diagnostics;


namespace 湿地GIS
{
    public partial class 数据集管理 : DevComponents.DotNetBar.Metro.MetroForm
    {

        private SampleRun1 m_sampleRun;
        private Workspace m_workspace;
        private DatasourceConnectionInfo m_srcDatasourceInfo;
        private DatasourceConnectionInfo m_dstDatasourceInfo;


        public 数据集管理()
        {
            try
            {
                InitializeComponent();

                InitializeDatasetTypeCombox();
                m_workspace = new Workspace();
                m_srcDatasourceInfo = new DatasourceConnectionInfo("D:/World/world.udb", "world", "");
                m_dstDatasourceInfo = new DatasourceConnectionInfo("D:/World/copyworld.udb", "copyworld", "");
                m_sampleRun = null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                m_sampleRun = new SampleRun1(m_workspace, m_srcDatasourceInfo);

                m_sampleRun.CopyDatasource(m_dstDatasourceInfo);


                InitializeDatasetView();

                this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);

                this.deleteCurrent.Click += new EventHandler(deleteCurrent_Click);

                this.changeName.Enabled = false;
                this.changeName.Click += new EventHandler(changeName_Click);
                this.newNameTextBox.TextChanged += new EventHandler(newNameTextBox_TextChanged);

                this.createDataset.Enabled = false;
                this.createDataset.Click += new EventHandler(createDataset_Click);
                this.createDatasetName.TextChanged += new EventHandler(createDatasetName_TextChanged);

                this.datasetView.NodeMouseClick += new TreeNodeMouseClickEventHandler(datasetView_NodeMouseClick);
                this.datasetView.LostFocus += new EventHandler(datasetView_LostFocus);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void InitializeDatasetView()
        {
            try
            {
                if (m_sampleRun != null)
                {
                    String[] datasetNames = m_sampleRun.GetDatasetsNames();
                    foreach (String name in datasetNames)
                    {
                        datasetView.Nodes.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void InitializeDatasetTypeCombox()
        {
            try
            {
                datasetTypeCombox.Items.Add("点数据集");
                datasetTypeCombox.Items.Add("线数据集");
                datasetTypeCombox.Items.Add("面数据集");
                datasetTypeCombox.Items.Add("文本数据集");
                datasetTypeCombox.Items.Add("CAD数据集");
                datasetTypeCombox.Items.Add("数据表数据集");

                datasetTypeCombox.Items.Add("栅格数据集");
                datasetTypeCombox.Items.Add("影像数据集");

                datasetTypeCombox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private DatasetType GetDatasetType(String typeName)
        {
            DatasetType result = DatasetType.Point;
            try
            {
                switch (typeName)
                {
                    case "点数据集":
                        result = DatasetType.Point;
                        break;
                    case "线数据集":
                        result = DatasetType.Line;
                        break;
                    case "面数据集":
                        result = DatasetType.Region;
                        break;
                    case "文本数据集":
                        result = DatasetType.Text;
                        break;
                    case "CAD数据集":
                        result = DatasetType.CAD;
                        break;
                    case "数据表数据集":
                        result = DatasetType.Tabular;
                        break;
                    case "栅格数据集":
                        result = DatasetType.Grid;
                        break;
                    case "影像数据集":
                        result = DatasetType.Image;
                        break;
                    default:
                        result = DatasetType.Line;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_workspace.Dispose();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void datasetView_LostFocus(object sender, EventArgs e)
        {
            try
            {
                datasetView.SelectedNode.BackColor = Color.DarkBlue;
                datasetView.SelectedNode.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void datasetView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (datasetView.SelectedNode != null)
                {
                    datasetView.SelectedNode.BackColor = Color.White;
                    datasetView.SelectedNode.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void deleteCurrent_Click(object sender, EventArgs e)
        {
            try
            {

                if (datasetView.SelectedNode == null)
                {
                    MessageBox.Show("没有选中任何数据集！");
                    return;
                }
                if (MessageBox.Show("确定要删除数据集？ 不可恢复", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    m_sampleRun.DeleteDataset(datasetView.SelectedNode.Text);
                    datasetView.Nodes.Remove(datasetView.SelectedNode);
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void changeName_Click(object sender, EventArgs e)
        {
            try
            {
                if (datasetView.SelectedNode == null)
                {
                    MessageBox.Show("没有选中任何数据集！");
                    return;
                }
                if (m_sampleRun.RenameDataset(datasetView.SelectedNode.Text, newNameTextBox.Text))
                {
                    datasetView.SelectedNode.Text = newNameTextBox.Text;
                    newNameTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void newNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (newNameTextBox.Text.Length != 0)
                {
                    changeName.Enabled = true;
                }
                else
                {
                    changeName.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void createDatasetName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (createDatasetName.Text.Length != 0)
                {
                    createDataset.Enabled = true;
                }
                else
                {
                    createDataset.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void datasetTypeCombox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (createDatasetName.Text.Length != 0)
                {
                    createDataset.Enabled = true;
                }
                else
                {
                    createDataset.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void createDataset_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_sampleRun.CreateDataset(GetDatasetType(datasetTypeCombox.Text), createDatasetName.Text))
                {
                    datasetView.Nodes.Add(createDatasetName.Text);
                    MessageBox.Show("添加数据集成功");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

    }
}

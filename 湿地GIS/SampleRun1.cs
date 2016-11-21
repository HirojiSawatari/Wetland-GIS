using System;
using System.Collections.Generic;
using System.Text;
using SuperMap.Data;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace 湿地GIS
{
    public class SampleRun1
    {
        private Workspace m_workspace;
        // 保存拷贝后的数据源
        private Datasource m_datasource;
        public SampleRun1(Workspace workspace, DatasourceConnectionInfo dsInfo)
        {
            try
            {
                this.Initialize(workspace);

                if (m_workspace.Datasources.Open(dsInfo) == null)
                {
                    MessageBox.Show("数据打开失败~！");

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void Initialize(Workspace workspace)
        {
            m_datasource = null;

            m_workspace = workspace;
        }
        public void CopyDatasource(DatasourceConnectionInfo dstInfo)
        {
            try
            {
                if (m_workspace.Datasources.Count == 0)
                {
                    m_datasource = null;
                    return;
                }
                CopyDatasource(m_workspace.Datasources[0].ConnectionInfo, dstInfo);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void CopyDatasource(DatasourceConnectionInfo srcInfo, DatasourceConnectionInfo dstInfo)
        {
            try
            {
                String targetPath = dstInfo.Server;

                this.DeleteDatasource(dstInfo);

                m_datasource = m_workspace.Datasources.Create(dstInfo);
                if (m_datasource == null)
                {
                    throw new Exception("Create datasource failed");
                }

                Datasets datasetsToCopy = m_workspace.Datasources[srcInfo.Alias].Datasets;

                // 逐个拷贝数据集
                foreach (Dataset dataset in datasetsToCopy)
                {
                    m_datasource.CopyDataset(dataset, dataset.Name, dataset.EncodeType);
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void DeleteDatasource(DatasourceConnectionInfo targetInfo)
        {
            try
            {
                this.CloseDatasource(targetInfo);

                String targetPath = targetInfo.Server;

                File.Delete(targetPath);
                String sddPath = Path.ChangeExtension(targetPath, "udd");
                File.Delete(sddPath);

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void CloseDatasource(DatasourceConnectionInfo targetInfo)
        {
            try
            {
                if (m_datasource != null)
                {
                    m_workspace.Datasources.Close(targetInfo.Alias);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public String[] GetDatasetsNames()
        {
            try
            {
                if (m_datasource != null)
                {
                    String[] datasetNames = new String[m_datasource.Datasets.Count];
                    Int32 index = 0;
                    foreach (Dataset dataset in m_datasource.Datasets)
                    {
                        datasetNames[index++] = dataset.Name;
                    }
                    return datasetNames;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return null;
        }
        public Boolean DeleteDataset(String datasetName)
        {
            try
            {
                if (m_datasource != null)
                {
                    return m_datasource.Datasets.Delete(datasetName);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return false;
        }
        public Boolean RenameDataset(String srcName, String targetName)
        {
            try
            {
                if (m_datasource != null)
                {
                    if (!m_datasource.Datasets.IsAvailableDatasetName(targetName))
                    {
                        MessageBox.Show("该名字已经存在或不合法");
                    }
                    else
                    {
                        return m_datasource.Datasets.Rename(srcName, targetName);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return false;
        }
        public Boolean CreateDataset(DatasetType datasetType, String datasetName)
        {
            Boolean result = false;
            if (m_datasource == null)
            {
                return result;
            }

            // 首先要判断输入的名字是否可用
            if (!m_datasource.Datasets.IsAvailableDatasetName(datasetName))
            {
                MessageBox.Show("该名字已经存在或不合法");
                return result;
            }

            Datasets datasets = m_datasource.Datasets;
            DatasetVectorInfo vectorInfo = new DatasetVectorInfo();
            vectorInfo.Name = datasetName;

            try
            {
                // Point等为Vector类型，类型是一样的，可以统一处理
                switch (datasetType)
                {
                    case DatasetType.Point:
                    case DatasetType.Line:
                    case DatasetType.CAD:
                    case DatasetType.Region:
                    case DatasetType.Text:
                    case DatasetType.Tabular:
                        {
                            vectorInfo.Type = datasetType;
                            if (datasets.Create(vectorInfo) != null)
                                result = true;
                        }
                        break;
                    case DatasetType.Grid:
                        {
                            DatasetGridInfo datasetGridInfo = new DatasetGridInfo();
                            datasetGridInfo.Name = datasetName;
                            datasetGridInfo.BlockSize = 125;
                            datasetGridInfo.Height = 200;
                            datasetGridInfo.Width = 200;
                            datasetGridInfo.NoValue = 1.0;
                            datasetGridInfo.PixelFormat = PixelFormat.Single;
                            datasetGridInfo.EncodeType = EncodeType.LZW;

                            if (datasets.Create(datasetGridInfo) != null)
                                result = true;
                        }
                        break;
                    case DatasetType.Image:
                        {
                            DatasetImageInfo datasetImageInfo = new DatasetImageInfo();
                            datasetImageInfo.Name = datasetName;
                            datasetImageInfo.Height = 200;
                            datasetImageInfo.Width = 200;
                            datasetImageInfo.Palette = Colors.MakeRandom(10);
                            datasetImageInfo.EncodeType = EncodeType.None;

                            if (datasets.Create(datasetImageInfo) != null)
                                result = true;
                        }
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
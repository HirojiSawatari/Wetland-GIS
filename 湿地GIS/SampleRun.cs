using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SuperMap.Realspace;
using SuperMap.UI;
using SuperMap.Data;
using SuperMap.Mapping;
using System.Diagnostics;
using SuperMap.Data.Conversion;
using System.IO;
using SuperMap.Layout;

namespace 湿地GIS
{
    class SampleRun
    {
        public static String dilei;
        public static String mapName;
        public static LayersControl layersControl1;
        public static WorkspaceControl workspaceControl1;
        public static Workspace workspace1;
        public static MapControl m_mapControlEagleEye;
        public static MapLayoutControl m_mapLayoutControl;
        public static MapControl mapControl1;
        private DatasetVector m_datasetVector;
        private Rectangle2D m_engleRectangle;
        private Boolean m_isMoveEngleRect;
        private Point m_pointBefore;
        private ToolStripStatusLabel label1;
        private SceneControl scontrol;
        private Datasource m_datasource;
        private String m_pointName;
        private ToolStripStatusLabel m_toolStrip;
        private Rectangle2D rect;
        private GeoRectangle geoRect;
        private TextBox m_textBoxResult;
        private Point2D pointMove;
        private Point3Ds m_point3Ds;
        private Point3Ds m_point3DsAll;
        private MeasureAction m_myAction;
        private LayoutElements elements;
        private GeoMap geoMap;
        private Layer m_layerThemeGraphBar3D;
        private GeoLine3D m_geoLine3D;
        private FlyManager m_flyManager;
        private Boolean m_customButtonEnable = true;
        private Int32 m_index = -1;

        private readonly String m_meter = "米";
        private readonly String m_squareMeter = "平方米";
        private readonly String m_length = "总长度：";
        private readonly String m_lengthcurrent = "当前长度:";
        private readonly String m_area = "面积：";
        private readonly String m_degree = "度";
        private readonly String m_azimuth = "方位角:";
        private readonly String m_angle = "当前角度:";

        public SampleRun(WorkspaceControl workspaceControl,
                         LayersControl layersControl,
                         MapControl mapControl,
                         MapControl mapControl2,
                         MapLayoutControl mapLayoutControl1,
                         ToolStripStatusLabel toolStripStatusLabel2,
                         TextBox textBoxResult,
                         ToolStripStatusLabel toolStripStatusLabel3,
                         SceneControl scenecontrol
                         )
        {
            workspaceControl1 = workspaceControl;
            workspace1 = workspaceControl1.WorkspaceTree.Workspace;
            layersControl1 = layersControl;
            mapControl1 = mapControl;
            m_mapControlEagleEye = mapControl2;
            m_mapLayoutControl = mapLayoutControl1;
            m_toolStrip = toolStripStatusLabel2;
            m_textBoxResult = textBoxResult;
            scontrol = scenecontrol;
            label1 = toolStripStatusLabel3;
            m_datasource = workspace1.Datasources[0];
            m_point3Ds = new Point3Ds();
            m_pointName = "Point";
            m_geoLine3D = new GeoLine3D();
            m_point3DsAll = new Point3Ds();

            m_flyManager = scontrol.Scene.FlyManager;
            m_flyManager.Routes.Clear();
            m_flyManager.Scene = scontrol.Scene;

            m_mapControlEagleEye.MouseMove += new MouseEventHandler(EagleEyeMapMouseMoveHandler);
            m_mapControlEagleEye.MouseDown += new MouseEventHandler(EagleEyeMapMouseDownHandler);
            m_mapControlEagleEye.MouseUp += new MouseEventHandler(EagleEyeMapMouseUpHandler);
            m_mapControlEagleEye.ActionCursorChanging += new ActionCursorChangingEventHandler(EagleEyeMapCursorChangedHandler);
            mapControl1.Map.Drawn += new MapDrawnEventHandler(MainMapDrawnHandler);
            scontrol.MouseDown += new System.Windows.Forms.MouseEventHandler(m_sceneControl_MouseDown);
            scontrol.MouseMove += new System.Windows.Forms.MouseEventHandler(m_sceneControl_MouseMove);
            Initialize();
        }
        private void Initialize()
        {
            try
            {
                layersControl1.Map = mapControl1.Map;
                mapControl1.Action = Action.Pan;
                m_mapControlEagleEye.Map.ViewEntire();
                m_mapControlEagleEye.MarginPanEnabled = false;
                m_mapControlEagleEye.IsWaitCursorEnabled = false;
                m_mapControlEagleEye.InteractionMode = InteractionMode.CustomAll;
                m_mapControlEagleEye.Cursor = Cursors.Arrow;
                workspaceControl1.WorkspaceTree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(WorkspaceTree_NodeMouseDoubleClick);
                mapControl1.IsWaitCursorEnabled = false;
                mapControl1.TrackMode = TrackMode.Track;
                m_mapLayoutControl.TrackMode = TrackMode.Edit;
                m_mapLayoutControl.MapLayout.Zoom(4);
                elements = m_mapLayoutControl.MapLayout.Elements;
                geoMap = new GeoMap();
                m_myAction = MeasureAction.None;
                mapControl1.MouseMove += new MouseEventHandler(MouseMoveHandler);
                mapControl1.Tracking += new TrackingEventHandler(TrackingHandler);
                mapControl1.Tracked += new TrackedEventHandler(TrackedHandler);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public FlyManager FlyManager
        {
            get
            {
                return m_flyManager;
            }
        }
        public Point3Ds Point3Ds
        {
            get
            {
                return m_point3Ds;
            }
        }
        public Int32 Index
        {
            get
            {
                return m_index;
            }
            set
            {
                m_index = value;
            }
        }

        internal void Pause()
        {
            if (m_flyManager != null)
            {
                m_flyManager.Pause();
            }
        }

        internal void Stop()
        {
            if (m_flyManager != null)
            {
                m_flyManager.Stop();
            }
        }

        private void WorkspaceTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            AddData();
        }
        private void AddData()
        {
            try
            {
                WorkspaceTreeNodeBase node = workspaceControl1.WorkspaceTree.SelectedNode as WorkspaceTreeNodeBase;
                WorkspaceTreeNodeDataType type = node.NodeType;
                if ((type & WorkspaceTreeNodeDataType.Dataset) != WorkspaceTreeNodeDataType.Unknown)
                {
                    type = WorkspaceTreeNodeDataType.Dataset;
                }
                switch (type)
                {
                    case WorkspaceTreeNodeDataType.Dataset:
                        {
                            Dataset dataset = node.GetData() as Dataset;
                            layersControl1.Map.Layers.Add(dataset, true);
                            layersControl1.Map.Refresh();
                            m_mapControlEagleEye.Map.Layers.Add(dataset, true);
                            m_mapControlEagleEye.Map.Refresh();
                        }
                        break;
                    case WorkspaceTreeNodeDataType.MapName:
                        {
                            mapName = node.GetData() as String;
                            mapControl1.Map.Open(mapName);
                            geoMap.MapName = mapName;
                            rect = new Rectangle2D(new Point2D(850, 1300), new Size2D(1500, 1500));
                            geoRect = new GeoRectangle(rect, 0);
                            geoMap.Shape = geoRect;
                            elements.AddNew(geoMap);
                            m_mapControlEagleEye.Map.Open(mapName);
                            mapControl1.Map.Refresh();
                            m_mapControlEagleEye.Map.Refresh();
                            m_mapControlEagleEye.Map.ViewEntire();
                        }
                        break;
                    case WorkspaceTreeNodeDataType.SymbolMarker:
                        {
                            SymbolLibraryDialog.ShowDialog(workspace1.Resources, SymbolType.Marker);
                        }
                        break;
                    case WorkspaceTreeNodeDataType.SymbolLine:
                        {
                            SymbolLibraryDialog.ShowDialog(workspace1.Resources, SymbolType.Line);
                        }
                        break;
                    case WorkspaceTreeNodeDataType.SymbolFill:
                        {
                            SymbolLibraryDialog.ShowDialog(workspace1.Resources, SymbolType.Fill);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            { } 
        }
        private void EagleEyeMapCursorChangedHandler(object sender, ActionCursorChangingEventArgs e)
        {
            e.FollowingCursor = Cursors.Cross;
        }
        private void EagleEyeMapMouseUpHandler(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (!m_isMoveEngleRect)
                    {
                        Point2D pntCenter = m_mapControlEagleEye.Map.PixelToMap(e.Location);
                        mapControl1.Map.Center = pntCenter;
                        mapControl1.Map.Refresh();
                    }

                    m_isMoveEngleRect = false;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void EagleEyeMapMouseDownHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_pointBefore = e.Location;
            }
        }
        private void EagleEyeMapMouseMoveHandler(object sender, MouseEventArgs e)
        {
            m_mapControlEagleEye.Cursor = Cursors.Arrow;
            label1.Text = "                                                                 当前比例尺:  1:" + Math.Round(Convert.ToDecimal(1 / mapControl1.Map.Scale), 6).ToString();
            try
            {
                MapControl eagleEyeMapControl = sender as MapControl;
                Map eagleEyeMap = eagleEyeMapControl.Map;
                if (m_engleRectangle.Contains(eagleEyeMap.PixelToMap(e.Location)))
                {
                    m_mapControlEagleEye.Cursor = Cursors.Cross;

                    if (e.Button == MouseButtons.Left)
                    {
                        m_isMoveEngleRect = true;
                    }
                }

                if (m_isMoveEngleRect)
                {
                    Point2D point2DBeforeMove = m_mapControlEagleEye.Map.PixelToMap(m_pointBefore);
                    Point2D point2DAfterMove = m_mapControlEagleEye.Map.PixelToMap(e.Location);
                    Double dx = point2DAfterMove.X - point2DBeforeMove.X;
                    Double dy = point2DAfterMove.Y - point2DBeforeMove.Y;
                    m_engleRectangle = new Rectangle2D(OffsetPoint(new Point2D(m_engleRectangle.Left, m_engleRectangle.Bottom), dx, dy), OffsetPoint(new Point2D(m_engleRectangle.Right, m_engleRectangle.Top), dx, dy));
                    DisplayRect(m_engleRectangle);
                    mapControl1.Map.ViewBounds = m_engleRectangle;
                    mapControl1.Map.Refresh();
                    m_pointBefore = e.Location;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private Point2D OffsetPoint(Point2D point, Double dx, Double dy)
        {
            Point2D temp = point;
            temp.Offset(dx, dy);
            return temp;
        }
        private void DisplayRect(Rectangle2D rectangleDisplay)
        {
            try
            {
                m_engleRectangle = rectangleDisplay;
                m_mapControlEagleEye.Cursor = Cursors.Cross;

                Double rectangleWidth = rectangleDisplay.Width;
                Double rectangleHeight = rectangleDisplay.Height;
                Double pntLeftTopX = rectangleDisplay.Left;
                Double pntLeftTopY = rectangleDisplay.Top;
                Point2Ds points = new Point2Ds();
                Point2D pntLeftTop = new Point2D(pntLeftTopX, pntLeftTopY);
                points.Add(pntLeftTop);
                Point2D pntLeftBottom = new Point2D(pntLeftTopX, pntLeftTopY - rectangleHeight);
                points.Add(pntLeftBottom);
                Point2D pntRightBottom = new Point2D(pntLeftTopX + rectangleWidth, pntLeftTopY - rectangleHeight);
                points.Add(pntRightBottom);
                Point2D pntRightTop = new Point2D(pntLeftTopX + rectangleWidth, pntLeftTopY);
                points.Add(pntRightTop);
                points.Add(pntLeftTop);
                GeoLine rectangleBoundary = new GeoLine();
                rectangleBoundary.AddPart(points);

                GeoStyle rectangleStyle = new GeoStyle();
                rectangleStyle.LineColor = Color.FromArgb(255, 0, 0);
                rectangleStyle.LineWidth = 0.5;

                rectangleBoundary.Style = rectangleStyle;
                m_mapControlEagleEye.Map.TrackingLayer.Clear();
                m_mapControlEagleEye.Map.TrackingLayer.Add((Geometry)rectangleBoundary, "");
                m_mapControlEagleEye.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void MainMapDrawnHandler(object sender, MapDrawnEventArgs e)
        {
            if (!m_isMoveEngleRect)
            {
                DisplayRect(mapControl1.Map.ViewBounds);
            }
        }
        private void TrackedHandler(object sender, TrackedEventArgs e)
        {
            try
            {
                switch (m_myAction)
                {
                    case MeasureAction.Distance:
                        {
                            String totalLength = String.Format("{0}{1}{2}", m_length, Math.Round(Convert.ToDecimal(e.Length), 2), m_meter);
                            m_textBoxResult.Text = totalLength;
                        }
                        break;
                    case MeasureAction.Area:
                        {
                            String totalArea = String.Format("{0}{1}{2}", m_area, Math.Round(Convert.ToDecimal(e.Area), 2), m_squareMeter);
                            m_textBoxResult.Text = totalArea;
                        }
                        break;
                    case MeasureAction.Angle:
                        {
                            String currentAzimuth = String.Format("{0}{1}{2}", m_azimuth, Math.Round(Convert.ToDecimal(e.Azimuth), 2), m_degree);
                            String currentAngle = String.Format("{0}{1}{2}", m_angle, Math.Round(Convert.ToDecimal(e.Angle), 2), m_degree);
                            m_textBoxResult.Text = currentAzimuth + ",  " + currentAngle;
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
        }
        private void TrackingHandler(object sender, TrackingEventArgs e)
        {
            try
            {
                switch (m_myAction)
                {
                    case MeasureAction.Distance:
                        {
                            String totalLength = String.Format("{0}{1}{2}", m_length, Math.Round(Convert.ToDecimal(e.TotalLength), 2), m_meter);
                            String currentLength = String.Format("{0}{1}{2}", m_lengthcurrent, Math.Round(Convert.ToDecimal(e.CurrentLength), 2), m_meter);
                            m_textBoxResult.Text = totalLength + "," + currentLength;
                        }
                        break;
                    case MeasureAction.Area:
                        {
                            String totalArea = String.Format("{0}{1}{2}", m_area, Math.Round(Convert.ToDecimal(e.TotalArea), 2), m_squareMeter);
                            m_textBoxResult.Text = totalArea;
                        }
                        break;
                    case MeasureAction.Angle:
                        {
                            String currentAzimuth = String.Format("{0}{1}{2}", m_azimuth, Math.Round(Convert.ToDecimal(e.CurrentAzimuth), 2), m_degree);
                            String currentAngle = String.Format("{0}{1}{2}", m_angle, Math.Round(Convert.ToDecimal(e.CurrentAngle), 2), m_degree);
                            m_textBoxResult.Text = currentAzimuth + "," + currentAngle;
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
        }
        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            pointMove = mapControl1.Map.PixelToMap(e.Location);
            String textXY = String.Format("X:{0},  Y:{1}", Math.Round(pointMove.X, 4), Math.Round(pointMove.Y, 4));
            m_toolStrip.Text = textXY;
        }
        public void DrawMeasure(MeasureAction myAction)
        {
            m_myAction = myAction;

            switch (m_myAction)
            {
                case MeasureAction.Distance:
                    {
                        mapControl1.Action = SuperMap.UI.Action.CreatePolyline;
                    }
                    break;
                case MeasureAction.Area:
                    {
                        mapControl1.Action = SuperMap.UI.Action.CreatePolygon;
                    }
                    break;
                case MeasureAction.Angle:
                    {
                        mapControl1.Action = SuperMap.UI.Action.CreatePolyline;
                    }
                    break;
                default:
                    break;
            }
        }
        public void SetLayoutAction(Action action)
        {
            try
            {
                m_mapLayoutControl.LayoutAction = action;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private String GetFilePath(String postfix)
        {
            String result = String.Empty;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "请选择要保存的文件路径";
            dialog.Filter = String.Format("{0}格式(*.{0})|*.{0}", postfix);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                result = dialog.FileName;
            }
            return result;
        }
        public void OutputBMP()
        {
            try
            {
                String fileName = GetFilePath("bmp");
                m_mapLayoutControl.MapLayout.OutputLayoutToBMP(fileName,150);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void OutputJPG()
        {
            try
            {
                String fileName = GetFilePath("jpg");
                m_mapLayoutControl.MapLayout.OutputLayoutToBMP(fileName, 150);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void OutputPNG(Boolean isBackTransparent)
        {
            try
            {
                String fileName = GetFilePath("png");
                m_mapLayoutControl.MapLayout.OutputLayoutToPNG(fileName, isBackTransparent);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void PrintLayout(String printerName)
        {
            try
            {
                m_mapLayoutControl.MapLayout.Printer.PrinterName = printerName;
                m_mapLayoutControl.MapLayout.Printer.Print();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void AddDatasetToScene(string datasetName)
        {
            try
            {
                scontrol.Scene.Layers.Clear();
                scontrol.Scene.TerrainLayers.Clear();

                if (!String.IsNullOrEmpty(datasetName))
                {
                    Layer3DSetting layer3dSetting = new Layer3DSettingGrid();

                    //m_maxGridVaule = (m_datasource.Datasets[datasetName] as DatasetGrid).MaxValue;
                    scontrol.Scene.TerrainLayers.Add((DatasetGrid)m_datasource.Datasets[datasetName], true);
                    // m_sceneControl.Scene.TerrainExaggeration = 2;


                    Layer3DDataset layer3d = scontrol.Scene.Layers.Add(m_datasource.Datasets[datasetName], layer3dSetting, true);
                    //if (datasetName.Equals("gridAspectResult"))
                    //{
                    //    ChangeAspectResultColorTable(layer3d.Name);
                    //}
                    //else if (datasetName.Equals("gridSlopeResult"))
                    //{
                    //    ChangeSlopeResultColorTable(layer3d.Name);
                    //}
                    scontrol.Scene.EnsureVisible(m_datasource.Datasets[datasetName].Bounds);
                    scontrol.Scene.Refresh();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        public void TransferCamera()
        {
            Camera camera = new Camera();
            camera.Altitude = 5000.05341279041;
            camera.Longitude = 116.391305696988;
            camera.Latitude = 39.9933447121584;
            camera.Heading = 2.76012171129487;
            camera.Tilt = 75.2282529563474;

            scontrol.Scene.Fly(camera, 10);
        }
        public void RegisterEvents(Boolean register)
        {
            if (register)
            {
                scontrol.MouseDown += new System.Windows.Forms.MouseEventHandler(m_sceneControl_MouseDown);
                scontrol.MouseMove += new System.Windows.Forms.MouseEventHandler(m_sceneControl_MouseMove);

                Route route = new Route();
                scontrol.Action = Action3D.Null;
            }
            else
            {
                scontrol.MouseDown -= new System.Windows.Forms.MouseEventHandler(m_sceneControl_MouseDown);
                scontrol.MouseMove -= new System.Windows.Forms.MouseEventHandler(m_sceneControl_MouseMove);

                scontrol.Action = Action3D.Pan;
            }
        }
        void m_sceneControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 鼠标左键
            if (e.Button == System.Windows.Forms.MouseButtons.Left
                && !Double.IsNaN(scontrol.Scene.PixelToGlobe(e.Location).X))
            {
                //RouteStop stop = new RouteStop();
                //TreeNode treeNode = new TreeNode();
                //m_treeView.Nodes[0].Nodes.Add(treeNode);
                //if (treeNode.Index > 0)
                //{
                //    treeNode.Text = GetStopName(treeNode.PrevNode.Text);
                //    //stop.Name = GetStopName(treeNode.PrevNode.Text);
                //}
                //else
                //{
                //    treeNode.Text = "Stop1";
                //    //stop.Name = "Stop1";

                //}
                //m_treeView.Nodes[0].ExpandAll();

                Point3D point3D = scontrol.Scene.PixelToGlobe(e.Location);
                point3D.Z = scontrol.Scene.GetAltitude(point3D.X, point3D.Y) + 30;
                m_point3Ds.Add(point3D);

                //stop.Camera = new Camera(point3D.X,point3D.Y,point3D.Z);



                GeoPoint3D geoPoint3D = new GeoPoint3D(point3D);
                geoPoint3D.Style3D = GetPointGeoStyle3D(geoPoint3D.Z);
                geoPoint3D.Z = 0;

                //m_routeStops.Add(stop);

                scontrol.Scene.TrackingLayer.Add(geoPoint3D, m_pointName);
            }
             //鼠标右键，结束绘制，将实时生成的路线加到FlyManager中
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (m_point3Ds.Count > 1)
                {
                    SetGeoLine3DToTrackingLayer(m_point3Ds.ToPoint2Ds(), 20);

                    m_flyManager.Routes.Add(GetRoute());
                }
                else
                {
                    ResumeDefault();
                }
                RegisterEvents(false);
            }
        }
        void m_sceneControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (m_customButtonEnable && m_point3Ds.Count > 0
                && !Double.IsNaN(scontrol.Scene.PixelToGlobe(e.Location).X))
            {
                Point2Ds point2Ds = m_point3Ds.ToPoint2Ds();
                Point3D point3D = scontrol.Scene.PixelToGlobe(e.Location);
                point3D.Z = scontrol.Scene.GetAltitude(point3D.X, point3D.Y);
                point2Ds.Add(new Point2D(point3D.X, point3D.Y));

                SetGeoLine3DToTrackingLayer(point2Ds, 20);
            }
        }
        private String GetStopName(String name)
        {
            String lastName = name.Substring(4);
            Int32 index = Convert.ToInt32(lastName);
            lastName = "Stop" + (++index).ToString();

            return lastName;
        }
        private GeoStyle3D GetPointGeoStyle3D(Double z)
        {
            GeoStyle3D geoStyle3D = new GeoStyle3D();

            geoStyle3D.AltitudeMode = AltitudeMode.Absolute;
            geoStyle3D.MarkerColor = System.Drawing.Color.FromArgb(255, 255, 255, 0);
            geoStyle3D.MarkerSize = 5;
            geoStyle3D.ExtendedHeight = z;

            return geoStyle3D;
        }
        private void SetGeoLine3DToTrackingLayer(Point2Ds point2Ds, Int32 count)
        {
            Int32 index = scontrol.Scene.TrackingLayer.IndexOf("line");

            GeoCardinal geoCardinal = new GeoCardinal(point2Ds);
            GeoLine geoLine = geoCardinal.ConvertToLine(count);

            if (m_geoLine3D.Length > 0)
            {
                m_geoLine3D.RemovePart(0);
            }
            m_geoLine3D.AddPart(ConvertPoint2DsToPoint3Ds(geoLine[0]));

            if (index > 0)
            {
                scontrol.Scene.TrackingLayer.Set(index, m_geoLine3D);
            }
            else
            {
                scontrol.Scene.TrackingLayer.Add(m_geoLine3D, "line");
            }
        }
        private Point3Ds ConvertPoint2DsToPoint3Ds(Point2Ds point2Ds)
        {
            m_point3DsAll.Clear();
            for (int i = 0; i < point2Ds.Count; i++)
            {
                m_point3DsAll.Add(new Point3D(point2Ds[i].X, point2Ds[i].Y,
                    scontrol.Scene.GetAltitude(point2Ds[i].X, point2Ds[i].Y) + 30));
            }
            return m_point3DsAll;
        }
        internal void Fly()
        {
            if (m_flyManager != null && m_flyManager.Routes.CurrentRoute != null)
            {
                m_flyManager.Play();
            }
        }
        private Route GetRoute()
        {
            Route route = new Route();
            Point3Ds point3Ds = m_geoLine3D[0];
            for (int i = 0; i < point3Ds.Count; i++)
            {
                Point3D point3D = new Point3D(point3Ds[i].X, point3Ds[i].Y, point3Ds[i].Z + 1000);
                point3Ds[i] = point3D;
            }
            GeoLine3D geoLine3D = new GeoLine3D(point3Ds);

            route.FromGeoLine3D(geoLine3D);
            route.IsFlyAlongTheRoute = true;
            route.IsHeadingFixed = true;
            route.IsAltitudeFixed = true;
            route.IsTiltFixed = true;
            route.IsLinesVisible = false;
            route.IsStopsVisible = false;

            return route;
        }
        public void ResumeDefault()
        {
            m_point3Ds.Clear();
            m_point3DsAll.Clear();
            //m_treeView.Nodes[0].Nodes.Clear();
            //m_index = -1;

            if (m_geoLine3D.Length > 0)
            {
                m_geoLine3D.SetEmpty();
            }

            if (m_flyManager.Routes.Count > 0)
            {
                m_flyManager.Routes.Clear();
            }

            scontrol.Scene.TrackingLayer.Clear();
        }
        public void InterpolateHeight(Int32 count)
        {
            Point2Ds curPoint2Ds = new Point2Ds();
            Point2Ds point2Ds = new Point2Ds();

            for (int i = 0; i < m_point3Ds.Count; i++)
            {
                curPoint2Ds.Add(new Point2D(m_point3Ds[i].X, m_point3Ds[i].Y));
                GeoCardinal geoCardinal = new GeoCardinal(curPoint2Ds);
                GeoLine geoLine = geoCardinal.ConvertToLine(count);
                double dLength = geoLine.Length;
                Point2D point2D = new Point2D(dLength, m_point3Ds[i].Z);
                point2Ds.Add(point2D);
            }

            GeoCardinal geoCardinalHeight = new GeoCardinal(point2Ds);
            GeoLine geoLineHeight = geoCardinalHeight.ConvertToLine(count);

            for (Int32 i = 0; i < m_point3DsAll.Count; i++)
            {
                Point3D point3D = new Point3D(m_point3DsAll[i].X, m_point3DsAll[i].Y, geoLineHeight[0][i].Y);
                m_point3DsAll[i] = point3D;
            }

            Int32 indexLine = scontrol.Scene.TrackingLayer.IndexOf("line");

            m_geoLine3D.SetEmpty();
            m_geoLine3D.AddPart(m_point3DsAll);

            if (indexLine > 0)
            {
                scontrol.Scene.TrackingLayer.Set(indexLine, m_geoLine3D);
            }
            else
            {
                scontrol.Scene.TrackingLayer.Add(m_geoLine3D, "line");
            }

            SetPointStyle3D(m_index.ToString(), true);

            m_flyManager.Routes.Remove(0);
            m_flyManager.Routes.Add(GetRoute());
        }
        public void SetPointStyle3D(String name, Boolean enable)
        {
            Int32 index = scontrol.Scene.TrackingLayer.IndexOf(m_pointName + name);
            Int32 indexPoints = 0;
            if (index >= 0)
            {
                GeoPoint3D geoPoint3D = scontrol.Scene.TrackingLayer.Get(index) as GeoPoint3D;
                //跟踪图层中索引为1的是线对象
                if (index > 0)
                {
                    indexPoints = index - 1;
                }
                else
                {
                    indexPoints = index;
                }

                if (enable)
                {
                    geoPoint3D.Style3D = SelectPointGeoStyle3D(m_point3Ds[indexPoints].Z);
                }
                else
                {
                    geoPoint3D.Style3D = GetPointGeoStyle3D(m_point3Ds[indexPoints].Z);
                }
                scontrol.Scene.TrackingLayer.Set(index, geoPoint3D);
            }
        }
        private GeoStyle3D SelectPointGeoStyle3D(Double z)
        {
            GeoStyle3D geoStyle3D = new GeoStyle3D();

            geoStyle3D.AltitudeMode = AltitudeMode.Absolute;
            geoStyle3D.MarkerColor = System.Drawing.Color.FromArgb(255, 255, 0, 0);
            geoStyle3D.MarkerSize = 5;
            geoStyle3D.ExtendedHeight = z;

            return geoStyle3D;
        }
        public void setThemeGraphBar3DVisible(Workspace workspaceN)
        {
            Workspace wp = workspaceN;
            m_layerThemeGraphBar3D = null;
            try
            {
                if (m_layerThemeGraphBar3D == null)
                {

                    m_datasetVector = wp.Datasources[0].Datasets["盘锦行政区划"] as DatasetVector;
                    //构造统计专题图
                    ThemeGraph graphBar3D = new ThemeGraph();

                    //初始化子项，及子项的风格
                    GeoStyle geoStyle = new GeoStyle();
                    geoStyle.LineWidth = 0.1;
                    geoStyle.FillGradientMode = FillGradientMode.Linear;

                    ThemeGraphItem item0 = new ThemeGraphItem();
                    item0.GraphExpression = dilei + "00";
                    geoStyle.FillForeColor = Color.FromArgb(231, 154, 0);
                    item0.UniformStyle = geoStyle;

                    ThemeGraphItem item1 = new ThemeGraphItem();
                    item1.GraphExpression = dilei + "09";
                    geoStyle.FillForeColor = Color.FromArgb(70, 153, 255);
                    item1.UniformStyle = geoStyle;



                    //添加子项
                    graphBar3D.Add(item0);
                    graphBar3D.Add(item1);


                    //设置偏移量，和非固定偏移，即随图偏移
                    //graphBar3D.IsOffsetFixed = false;
                    //graphBar3D.OffsetY = "13000";

                    ////设置非流动显示
                    //graphBar3D.IsFlowEnabled = false;

                    ////设置非避让方式显示
                    //graphBar3D.IsOverlapAvoided = false;

                    ////设置统计图显示的最大值和最小值,和非固定大小，即随图缩放
                    //graphBar3D.MaxGraphSize = 10000000.0000;
                    //graphBar3D.MinGraphSize = 0.0000;

                    ////显示统计图文本，并设置文本显示模式
                    //graphBar3D.IsGraphTextDisplayed = true;
                    //graphBar3D.GraphTextFormat = ThemeGraphTextFormat.Value;

                    //设置统计图类型
                    graphBar3D.GraphType = ThemeGraphType.Bar3D;

                    TextStyle textStyle = graphBar3D.GraphTextStyle;
                    textStyle.IsSizeFixed = false;
                    textStyle.FontHeight = 10000;

                    //添加三维柱状统计图图层
                    m_layerThemeGraphBar3D = mapControl1.Map.Layers.Add(m_datasetVector, graphBar3D, true);
                }

                //设置图层是否可显示，并刷新地图
                m_layerThemeGraphBar3D.IsVisible = true;
                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
  
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SuperMap.Data;

namespace 湿地GIS
{
    static class Program
    {
        public static Resources resources;
        public static bool a,e;
        public static string u;
        public static Recordset re;
        public static double[] chushi09;
        public static double[,] zhuanyi;
        public static string name;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 加载界面());
            if (e)
            {
                Application.Run(new 用户登录());
            }
            if (a)
            {
                Application.Run(new 主界面());
            }
        }
    }
}

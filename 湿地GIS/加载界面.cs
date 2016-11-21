using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace 湿地GIS
{
    public partial class 加载界面 : Form
    {
        public 加载界面()
        {
            InitializeComponent();
        }
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            Program.e = true;
            this.Close();
        }
        private void 加载界面_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            System.Timers.Timer t = new System.Timers.Timer(1500);
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            t.AutoReset = false;
            t.Enabled = true;
        }
    }
}

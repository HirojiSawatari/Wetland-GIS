using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using DevComponents.DotNetBar;

namespace 湿地GIS
{
    public partial class 打印 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public 打印()
        {
            InitializeComponent();
        }

        private void 打印_Load(object sender, EventArgs e)
        {
            foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                SampleRun.m_mapLayoutControl.MapLayout.Printer.PrinterName = comboBox1.SelectedItem as String;
                SampleRun.m_mapLayoutControl.MapLayout.Printer.Print();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
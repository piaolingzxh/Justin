using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.TaskMonitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timerRecordRAM_Tick(object sender, EventArgs e)
        {
            RecordRAMP();
        }
        private void RecordRAMP()
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            Process[] processes = Process.GetProcessesByName("Justin.Stock.exe");
            foreach (Process process in processes)
            {
                dic.Add(process.Id, process.PrivateMemorySize);
                Console.WriteLine(process.PrivateMemorySize);
            }
        }
    }
}

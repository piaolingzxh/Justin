using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static object syncObject = new Object();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    Test();
                }
                else
                {
                    Test1();
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.ToString() + Environment.NewLine);
            }
        }

        public void Test()
        {
            try
            {
                lock (syncObject)
                {
                    int x = 10;
                    int y = 0;
                    int z = x / y;
                }

            }
            catch
            {
                throw;
            }

        }
        public void Test1()
        {
            lock (syncObject)
            {
                try
                {

                    int x = 10;
                    int y = 0;
                    int z = x / y;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}

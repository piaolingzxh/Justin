using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Message
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        INotify form;

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                form = new QQStyleMessage();
            }
            else
            {
                //form = new NormalForm();
            }

            this.timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            form.Show(DateTime.Now.ToString());
        }
    }
}

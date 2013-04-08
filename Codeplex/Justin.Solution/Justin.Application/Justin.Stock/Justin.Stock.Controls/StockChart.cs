using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Justin.Stock.Controls;
using Justin.Stock.Controls.Entities;

namespace Justin.Stock.Controls
{
    public partial class StockChart : Form
    {
        StockChartCtrl ctrl;
        public StockChart()
        {
            InitializeComponent();
            ctrl = new StockChartCtrl();
            ctrl.Dock = DockStyle.Fill;
            this.Controls.Add(ctrl);
        }

        private void StockChart_Load(object sender, EventArgs e)
        {
            this.MouseWheel += new MouseEventHandler(StockChart_MouseWheel);
            this.Opacity = 0.1;
        }

        void StockChart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && this.Opacity < 1)
            {
                this.Opacity += 0.1;
            }
            else if (e.Delta < 0 && this.Opacity > 0.12)
            {
                this.Opacity -= 0.1;
            }
        }

        private void StockChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Hide();
            for (int i = this.Controls.Count; i > 0; i--)
            {
                StockChartCtrl c = this.Controls[i - 1] as StockChartCtrl;
                c.Dispose();
            }
            this.Controls.Clear();
        }

        public void Show(string stockCode, ChartType chartType)
        {
            ctrl.Show(stockCode, chartType);
            this.Show();
        }


    }
}

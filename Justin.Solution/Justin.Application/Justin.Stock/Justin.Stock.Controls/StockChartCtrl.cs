using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.Stock.Controls;
using Justin.Stock.Controls.Entities;

namespace Justin.Stock
{
    public partial class StockChartCtrl : UserControl
    {
        public String stockCode;
        public StockChartCtrl()
        {
            InitializeComponent();
        }

        private void DrawChart(ChartType chartType)
        {
            string url;
            switch (chartType)
            {
                case ChartType.TimeSheet0://http://hqpicr.dfcfw.com/r/0026492.png?0.6290230534505099
                    url = string.Format("http://hqpicr.dfcfw.com/r/{0}{1}.png", stockCode.Substring(2, stockCode.Length - 2), GetStockType(stockCode));
                    picTimeSheet0Trans.ImageLocation = url;
                    picTimeSheet0Trans.Refresh();
                    break;
                case ChartType.TimeSheet:
                    url = string.Format("http://image.sinajs.cn/newchart/min/n/{0}.gif", stockCode);
                    picTimeSheetTrans.ImageLocation = url;
                    picTimeSheetTrans.Refresh();
                    break;
                case ChartType.KOfDay://http://image.sinajs.cn/newchart/daily/n/{0}.gif
                    url = string.Format("http://hqpick.eastmoney.com/EM_Quote2010PictureProducter/Index.aspx?ImageType=KXL&ID={0}{1}&EF=&Formula=MACD&UnitWidth=6&StockFQ=0&type=", stockCode.Substring(2, stockCode.Length - 2), GetStockType(stockCode));
                    picDayTrans.ImageLocation = url;
                    picDayTrans.Refresh();
                    break;
                case ChartType.KOfWeek:
                    url = string.Format("http://image.sinajs.cn/newchart/weekly/n/{0}.gif", stockCode);
                    picWeekTrans.ImageLocation = url;
                    picWeekTrans.Refresh();
                    break;
                case ChartType.KOfMonth:
                    url = string.Format("http://image.sinajs.cn/newchart/monthly/n/{0}.gif", stockCode);
                    picMonthTrans.ImageLocation = url;
                    picMonthTrans.Refresh();
                    break;
                case ChartType.Comprehensive:

                    break;

                default: MessageBox.Show("不支持该分析图");
                    break;
            }
        }

        private void btnRefreshChart_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri(string.Format("http://i2.sinaimg.cn/cj/hsuan/flash/SinaKLine207a.swf?symbol={0}", stockCode));
        }

        private int GetStockType(string stockCode)
        {
            string type = stockCode.Substring(0, 2);
            return string.Compare(type, "sh", true) == 0 ? 1 : 2;
        }


        private void tbTransChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tbTransChart.SelectedIndex)
            {
                case 0:
                    DrawChart(ChartType.TimeSheet0);
                    break;
                case 1:
                    DrawChart(ChartType.TimeSheet);
                    break;
                case 2:
                    DrawChart(ChartType.KOfDay);
                    break;
                case 3:
                    DrawChart(ChartType.KOfWeek);
                    break;
                case 4:
                    DrawChart(ChartType.KOfMonth);
                    break;
                case 5:
                    DrawChart(ChartType.Comprehensive);
                    break;
                default: MessageBox.Show("不支持该分析图");
                    break;
            }
        }


        public void Show(string stockCode, ChartType chartType)
        {
            this.stockCode = stockCode;
            DrawChart(chartType);
            this.tbTransChart.SelectedIndex = (int)chartType;

        }

        public new void Dispose()
        {
            for (int i = this.Controls.Count; i > 0; i--)
            {
                Control c = this.Controls[i - 1];
                c.Dispose();
            }
            base.Dispose();
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}

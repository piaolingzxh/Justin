using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Services;
using Justin.FrameWork.WinForm.Extensions;
using Justin.FrameWork.WinForm.Helper;
using Justin.Log;
using Justin.Stock.Controls.Entities;
using Justin.Stock.DAL;
using Justin.Stock.Service.Entities;
using Justin.Stock.Service.Models;

namespace Justin.Stock.Controls
{
    delegate void FormInvoke(FormInvokArgument argument);

    public partial class DeskStockCtrl : UserControl
    {
        public DeskStockCtrl()
        {
            InitializeComponent();
        }

        #region 只打开一次的Form

        MyStock myStock = new MyStock();

        #endregion

        private void DeskStockCtrl_Load(object sender, EventArgs e)
        {
            var dal = new StockDAL();
            StockService.QuerySumInvestFunc = dal.GetSumInvest;
            StockService.GetAllMyStockFunc = dal.getAllMyStock;
            StockService.AddEvent(Display);

            StockService.AddEvent(Display);
        }

        #region 桌面显示和通知功能

        private void Display(object sender, StockEventArgs e)
        {

            IEnumerable<StockInfo> stockList = e.Stocks;
            try
            {
                #region format
                //简单通知
                //string notifyFormat = "{0}:{1}{2}%{6}%";
                string tipsFormat =
@"{0} 涨:{9}% 换:{14}% 盈:{11}
现价：{3} 成本：{13} 股:{12} 
最高：{4} 最低：{5}
今开:{1} 昨收：{2} 
成交量/额:{6}手/{7}万元
买：          卖：
{15}
代码：{16}  板块：{17}
{18}
时间：{8}
当前：{10}";

                string fiveDealFormat =
@"{0}:{1}   {2}:{3}";

                #endregion

                #region 桌面控件初始化

                TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
                tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);

                tableLayoutPanel1.SuspendLayout();
                var rowStyle = new RowStyle(SizeType.Absolute, 9);

                for (int j = 0; j < 50; j++)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
                }
                tableLayoutPanel1.ColumnCount = 1;
                tableLayoutPanel1.RowCount = stockList.Count();
                tableLayoutPanel1.Height = tableLayoutPanel1.RowCount * 10;
                tableLayoutPanel1.Dock = DockStyle.Fill;
                List<Control> controls = new List<Control>();
                foreach (Control item in tableLayoutPanel1.Controls)
                {
                    controls.Add(item);
                }
                foreach (var item in controls)
                {
                    item.Dispose();
                }
                tableLayoutPanel1.Controls.Clear();

                #endregion

                int rowIndex = 0;
                ToolTip tip = new ToolTip();

                #region 显示每一只股票

                //只显示指定的股票    并且先按Order+BuyCount排序
                var stocks = stockList.Where(row => row.ShowInFolatWindow).OrderByDescending(row => row.Order).ThenByDescending(row => row.BuyCount);

                foreach (var rtStock in stocks)
                {
                    Label stockLabel = this.GetNewlabel(rtStock.Order == -1);
                    stockLabel.ForeColor = GetWarnColor(rtStock);

                    #region 股票桌面信息

                    stockLabel.Tag = rtStock.Code;
                    string nameInShort = GetUpOrDownArrowCompareToLastDay(rtStock) + " " + rtStock.SpellingInShort.PadLeft(4, ' ');

                    stockLabel.Text = string.Format(Constants.Setting.DeskDisplayFormat
                                              , nameInShort                                                                          //简称
                                              , Math.Round(rtStock.PriceNow, rtStock.PriceNow >= 1000 ? 1 : 2).ToString().PadLeft(6, ' ')                                                 //当前价格
                                              , (rtStock.SurgedRange.ToString() + "%").PadLeft(7, ' ')                              //当日涨幅
                                              , (rtStock.CurrentProfit.ToString()).PadLeft(6, ' ')                               //当前盈亏
                                             , Math.Round(rtStock.SumProfit, 0).ToString().PadLeft(6, ' ')                                              //总盈亏
                                              , (Math.Round(rtStock.SumProfitPercent * 100, 2).ToString() + "%").PadLeft(8, ' ')      //总盈亏比例
                                             , Math.Round(rtStock.BuyPrice, 2).ToString().PadLeft(6, ' ')                           //成本价
                                             , rtStock.BuyCount.ToString().PadLeft(5, ' ')                                          //股数
                                             , (rtStock.TurnOver.ToString() + "%").PadLeft(7, ' ')                                  //换手率
                                             , rtStock.MarketValue.ToString().PadLeft(6, ' ')                                       //当前市值
                                             , Math.Round(rtStock.SumCost, 0).ToString().PadLeft(6, ' ')

                                              );
                    #endregion

                    #region 股票提示信息

                    string fiveDeal = new StringBuilder()
                        .AppendFormat(fiveDealFormat, rtStock.Buy1Price.ToString().PadLeft(5, ' '), rtStock.Buy1Count.ToString().PadLeft(6, ' '), rtStock.Sell1Price.ToString().PadLeft(5, ' '), rtStock.Sell1Count.ToString().PadLeft(6, ' ')).AppendLine()
                        .AppendFormat(fiveDealFormat, rtStock.Buy2Price.ToString().PadLeft(5, ' '), rtStock.Buy2Count.ToString().PadLeft(6, ' '), rtStock.Sell2Price.ToString().PadLeft(5, ' '), rtStock.Sell2Count.ToString().PadLeft(6, ' ')).AppendLine()
                        .AppendFormat(fiveDealFormat, rtStock.Buy3Price.ToString().PadLeft(5, ' '), rtStock.Buy3Count.ToString().PadLeft(6, ' '), rtStock.Sell3Price.ToString().PadLeft(5, ' '), rtStock.Sell3Count.ToString().PadLeft(6, ' ')).AppendLine()
                        .AppendFormat(fiveDealFormat, rtStock.Buy4Price.ToString().PadLeft(5, ' '), rtStock.Buy4Count.ToString().PadLeft(6, ' '), rtStock.Sell4Price.ToString().PadLeft(5, ' '), rtStock.Sell4Count.ToString().PadLeft(6, ' ')).AppendLine()
                        .AppendFormat(fiveDealFormat, rtStock.Buy5Price.ToString().PadLeft(5, ' '), rtStock.Buy5Count.ToString().PadLeft(6, ' '), rtStock.Sell5Price.ToString().PadLeft(5, ' '), rtStock.Sell5Count.ToString().PadLeft(6, ' '))
                        .ToString();
                    string stockTips = string.Format(tipsFormat
                        , rtStock.Name
                         , rtStock.PriceTodayStart
                         , rtStock.PriceYesterdayEnd
                         , rtStock.PriceNow
                         , rtStock.PriceTodayHigh
                         , rtStock.PriceTodayLow
                         , rtStock.DealsStockAmt
                         , rtStock.DealsMoney
                         , rtStock.DateTime
                         , rtStock.SurgedRange
                         , rtStock.Now
                         , rtStock.CurrentProfit
                         , rtStock.BuyCount
                         , rtStock.BuyPrice
                         , rtStock.TurnOver
                         , fiveDeal
                         , rtStock.Code
                         , rtStock.CategroyDesc
                         , rtStock.Description
                        );

                    tip.SetToolTip(stockLabel, stockTips);

                    #endregion

                    tableLayoutPanel1.Controls.Add(stockLabel, 0, rowIndex);
                    rowIndex++;
                }

                #endregion

                #region 表格标题信息

                Label columnNamesLabel = GetNewlabel(true);

                columnNamesLabel.Click += new EventHandler(stockLabel_Click);
                columnNamesLabel.Text = string.Format(Constants.Setting.DeskDisplayFormat
                                               , "Name".PadLeft(6, ' ')                                                     //简称
                                               , "Now¥".PadLeft(6, ' ')                                                 //当前价格
                                               , "↓↑%".PadLeft(7, ' ')                                                 //当日涨幅
                                               , "PF".PadLeft(6, ' ')                                                   //当前盈亏        
                                              , "∑PF".PadLeft(6, ' ')                                                   //总盈亏
                                               , "∑PF%".PadLeft(8, ' ')                                               //总盈亏比例
                                              , "Cost¥".PadLeft(6, ' ')                                                   //成本价
                                              , "*".PadLeft(5, ' ')                                                     //股数
                                              , "Turn%".PadLeft(7, ' ')                                                   //换手率
                                              , "Mkt¥".PadLeft(6, ' ')                                                  //当前市值
                                              , "∑Cost¥".PadLeft(6, ' ')                                                  //总成本
                                               );
                tip.SetToolTip(columnNamesLabel, string.Format(Constants.Setting.DeskDisplayFormat
                                               , "Name:简称" + Environment.NewLine                                               //简称
                                               , "Now¥:当前价格" + Environment.NewLine                                         //当前价格
                                               , "↓↑%:当日涨幅" + Environment.NewLine                                         //当日涨幅
                                               , "PF:当前盈亏" + Environment.NewLine                                         //当前盈亏        
                                              , "∑PF:总盈亏" + Environment.NewLine                                          //总盈亏
                                               , "∑PF%:总盈亏比例" + Environment.NewLine                                         //总盈亏比例
                                              , "Cost¥:成本价" + Environment.NewLine                                          //成本价
                                              , "*:股数" + Environment.NewLine                                          //股数
                                              , "Turn%:换手率" + Environment.NewLine                                            //换手率
                                              , "Mkt¥:当前市值" + Environment.NewLine                                              //当前市值
                                              , "∑Cost¥:总成本" + Environment.NewLine                                               //总成本
                                              ));
                tableLayoutPanel1.Controls.Add(columnNamesLabel, 0, rowIndex++);

                #endregion

                #region 总盈亏信息
                int sumInvest = (int)StockService.SumInvest;
                int sumMarketValue = (int)stockList.Sum(row => row.MarketValue);
                int accountMoney = (int)(sumMarketValue + Constants.Setting.Balance);
                int currentProfit = (int)stockList.Sum(row => row.CurrentProfit);
                int sumProfit = (int)(sumMarketValue + Constants.Setting.Balance - sumInvest);
                string summaryMsg = string.Format("{0}/{1}/{2} {3}/{4}/{5}", currentProfit, sumProfit, (int)Constants.Setting.Balance, (int)sumMarketValue, accountMoney, (int)sumInvest);

                string summaryMsgTips = string.Format(@"{0}/{1}/{2} {3}/{4}/{5}", "当前盈亏", "总盈亏", "可用余额", "股票资产", "账户总资产", "总投入资产");

                Label summaryLabel = GetNewlabel();

                summaryLabel.Click += new EventHandler(stockLabel_Click);
                summaryLabel.Text = summaryMsg;
                tip.SetToolTip(summaryLabel, summaryMsgTips);
                tableLayoutPanel1.Controls.Add(summaryLabel, 0, rowIndex++);

                //总成本=所有股票总成本+当前余额
                //decimal sumCost = stockList.Sum(row => row.SumCost) + Constants.Setting.Balance;//成本
                //总市值=所有股票当前市值+当前余额
                //decimal sumCurrentPrice = stockList.Sum(row => row.MarketValue) + Constants.Setting.Balance;//市值
                //decimal sumProfitOrLoss = stockList.Sum(row => row.SumProfit);

                tableLayoutPanel1.Tag = summaryMsg;

                #endregion

                tableLayoutPanel1.ResumeLayout();



                #region 实时显示股票警告信息到桌面标题

                FormInvokArgument argument = new FormInvokArgument()
                {
                    tableLayoutPanel1 = tableLayoutPanel1,
                };

                if (this.InvokeRequired == true)
                {
                    this.Invoke(new FormInvoke(ShowStockInDesk), argument);
                }
                else
                {
                    ShowStockInDesk(argument);
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex);
            }


        }
        private void stockLabel_Click(object sender, EventArgs ee)
        {
            var form = this.FindForm();
            if (form is AutoAnchorForm)
            {
                var autoHideForm = form as AutoAnchorForm;
                autoHideToolStripMenuItem.Checked = !autoHideToolStripMenuItem.Checked;
                autoHideForm.EnableAutoAnchor = autoHideToolStripMenuItem.Checked;
            };
        }
        private void ShowStockInDesk(FormInvokArgument argument)
        {
            this.DoubleBuffered = true;
            this.SuspendLayout();
            TableLayoutPanel tableLayoutPanel1 = argument.tableLayoutPanel1;

            #region 重绘控件

            List<Control> controls = new List<Control>();
            foreach (Control item in this.Controls)
            {
                controls.Add(item);
            }
            foreach (var item in controls)
            {
                item.Dispose();
            }
            this.Controls.Clear();

            this.Controls.Add(tableLayoutPanel1);

            #endregion
            this.ResumeLayout();

            #region 抛出股票总盈亏汇总和警告信息给容器，以便显示到标题上

            string message = string.Format("{0}  {1}", tableLayoutPanel1.Tag.ToString(), argument.Message);
            this.Text = message;
            if (DisplaySummaryMessageAction != null)
            {
                DisplaySummaryMessageAction(message);
            }

            #endregion
        }
        //超过警戒线用红色，低于警戒线用绿色
        private Color GetWarnColor(StockInfo stock)
        {
            Color color = Color.Black, up = Color.Red, down = Color.Green;

            if (!Constants.Setting.ShowWarn || stock.PriceNow == 0 || !stock.Warn) return color;

            if ((stock.WarnPrice_Max != 0 && stock.PriceNow > stock.WarnPrice_Max)
                || ((stock.WarnPercent_Max != 0 && stock.SurgedRange > stock.WarnPercent_Max)))
            {
                color = up;
            }

            if ((stock.WarnPrice_Min != 0 && stock.PriceNow < stock.WarnPrice_Min) || ((stock.WarnPercent_Min != 0 && stock.SurgedRange < stock.WarnPercent_Min)))
            {
                color = down;
            }
            return color;
        }
        //用箭头表示相对上一个交易日的涨跌
        private string GetUpOrDownArrowCompareToLastDay(StockInfo stock)
        {
            if (stock.PriceNow == 0 || stock.PriceNow == stock.PriceYesterdayEnd) return " ";
            return stock.PriceNow > stock.PriceYesterdayEnd ? "↑" : "↓";
        }

        #endregion

        #region 右键菜单

        Label stockLabel;
        private void deskMenu_Opening(object sender, CancelEventArgs e)
        {
            Control sourceControl = (sender as ContextMenuStrip).SourceControl;
            if (sourceControl is Label)
            {
                stockLabel = sourceControl as Label;
            }
            Form form = this.FindForm();
            if (form != null)
                this.topMostToolStripMenuItem.Checked = form.TopMost;

            var tempAutoHideForm = this.FindForm();
            if (tempAutoHideForm is AutoAnchorForm)
            {
                var autoHideForm = tempAutoHideForm as AutoAnchorForm;
                autoHideToolStripMenuItem.Checked = autoHideForm.EnableAutoAnchor;
            }
        }

        private void timeSheetMenuItem_Click(object sender, EventArgs e)
        {
            if (stockLabel == null)
                return;
            this.ShowChart(stockLabel.Tag.ToString(), ChartType.TimeSheet);
        }
        private void DayKMenuItem_Click(object sender, EventArgs e)
        {
            if (stockLabel == null)
                return;
            this.ShowChart(stockLabel.Tag.ToString(), ChartType.KOfDay);
        }
        private void WeekKMenuItem_Click(object sender, EventArgs e)
        {
            if (stockLabel == null)
                return;
            this.ShowChart(stockLabel.Tag.ToString(), ChartType.KOfWeek);
        }
        private void MonthKMenuItem_Click(object sender, EventArgs e)
        {
            if (stockLabel == null)
                return;
            this.ShowChart(stockLabel.Tag.ToString(), ChartType.KOfMonth);
        }
        private void monitorStockMenuItem_Click(object sender, EventArgs e)
        {
            myStock.Show(0);
        }
        private void personalStocksMenuItem_Click(object sender, EventArgs e)
        {
            myStock.Show(1);
        }
        private void systemSettingMenuItem_Click(object sender, EventArgs e)
        {
            myStock.Show(3);
        }

        private void ShowChart(string stockNo, ChartType chartType)
        {
            StockChart chart = new StockChart();
            chart.Show(stockNo, chartType);
        }

        #endregion

        #region 提供给外部注册事件用
        /// <summary>
        /// 注册显示盈亏总汇总和破线预警信息到容器标题
        /// </summary>
        public static Action<string> DisplaySummaryMessageAction { get; set; }


        public void AddDisplayHandler()
        {

        }
        public void RemoveDisplayHandler()
        {
            StockService.RemoveEvent(Display);
        }

        public void CloseChildrenForm()
        {
            this.myStock.Close(true);
        }

        #endregion

        private Label GetNewlabel(bool bold = false)
        {
            Label stockLabel = new Label();
            stockLabel.ContextMenuStrip = deskMenu;
            stockLabel.Width = 200;
            stockLabel.Font = new Font("Consolas", 8F, bold ? FontStyle.Bold : FontStyle.Regular);
            stockLabel.Dock = DockStyle.Fill;
            return stockLabel;
        }

        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = this.FindForm();
            if (form != null)
                form.TopMost = !form.TopMost;
        }

        private void autoHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = this.FindForm();
            if (form is AutoAnchorForm)
            {
                var autoHideForm = form as AutoAnchorForm;
                autoHideToolStripMenuItem.Checked = !autoHideToolStripMenuItem.Checked;
                autoHideForm.EnableAutoAnchor = autoHideToolStripMenuItem.Checked;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Justin.FrameWork.Entities;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.Log;
using Justin.Stock.Controls;
using Justin.Stock.Controls.Entities;
using Justin.Stock.DAL;
using Justin.Stock.Service;
using Justin.Stock.Service.Entities;
using Justin.Stock.Service.Models;

namespace Justin.Stock.Controls
{
    delegate void MyStockScreenMessage(IEnumerable<StockInfo> stocks);

    public partial class MyStock : Form
    {
        private bool forceClose = false;
        //DataGridView contextMenuSourceGridView = null;
        string CurrentStockCode { get; set; }
        StockDAL stockDAL = new StockDAL();
        SystemSettingCtrl settingCtrl = new SystemSettingCtrl();

        public MyStock()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(MyStock_MouseWheel);
        }

        private static object syncdgvMonitorStocks = new Object();

        #region 窗体事件、控件事件

        private void MyStock_Load(object sender, EventArgs e)
        {
            #region 股票无关

            foreach (Control item in this.Controls)
            {
                item.MouseWheel += new MouseEventHandler(MyStock_MouseWheel);
            }
            this.Opacity = 0.1;
            dgvMonitorStocks.AutoGenerateColumns = false;
            BindMyStocks(StockService.MyStock);

            #endregion
            dgvStocksetting.AutoGenerateColumns = false;

            this.tabPageSetting.Controls.Clear();
            this.tabPageSetting.Controls.Add(settingCtrl);

        }

        private void MyStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            StockService.RemoveEvent(ShowMyStockInfoChanged);
            e.Cancel = !forceClose;
            if (!forceClose)
            {
                this.Hide();
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    BindMyStocks();
                    break;
                case 1:
                    RefreshPersonalStockSetting();
                    break;
                case 2:
                    ShowChart(ChartType.TimeSheet, true);
                    break;
                case 3:
                    settingCtrl.RefreshSetting();
                    break;
            }
        }

        #endregion

        #region 实时读盘

        private void monitorContextMenu_Opening(object sender, CancelEventArgs e)
        {
        }
        private void dgvMonitorStocks_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            lock (syncdgvMonitorStocks)
            {
                if (e.RowIndex >= 0)
                {
                    //dgvMonitorStocks.ClearSelection();
                    dgvMonitorStocks.Rows[e.RowIndex].Selected = true;
                    dgvMonitorStocks.CurrentCell = dgvMonitorStocks.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                CurrentStockCode = dgvMonitorStocks.SelectedRows[0].Cells["S_Code"].Value.Value<String>();
            }
        }
        #endregion

        #region 自选设置(预警+购入信息等)

        //从股票大全里边，添加股票代码到自选
        private void btnQueryStock_Click(object sender, EventArgs e)
        {
            List<StockBaseInfo> allStocks = stockDAL.GetAllStocks();
            if (!string.IsNullOrEmpty(txtSrockName.Text))
            {
                string value = txtSrockName.Text.Trim();
                allStocks = allStocks.Where(row => row.Code.IndexOf(value) >= 0 || row.Name.IndexOf(value) >= 0 || row.No.IndexOf(value) >= 0 || row.SpellingInShort.IndexOf(value) >= 0).ToList();
            }

            dgvQueryResultStocks.DataSource = new BindingList<StockBaseInfo>(allStocks);
        }
        private void dgvQueryResultStocks_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                dgvQueryResultStocks.ClearSelection();
                dgvQueryResultStocks.Rows[e.RowIndex].Selected = true;
                if (e.ColumnIndex >= 0)
                    dgvQueryResultStocks.CurrentCell = dgvQueryResultStocks.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            CurrentStockCode = dgvQueryResultStocks.SelectedRows[0].Cells["Code"].Value.Value<String>();
        }

        private void 加入自选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvQueryResultStocks.SelectedRows[0];

            string code = row.Cells["Code"].Value.ToString();
            string no = row.Cells["No"].Value.ToString();
            string name = row.Cells["_Name"].Value.ToString();
            string shortName = row.Cells["InShort"].Value.ToString();


            stockDAL.InsertStock(code, no, name, shortName);
            RefreshPersonalStockSetting();

        }
        private void 删除自选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvQueryResultStocks.SelectedRows[0];

            string code = row.Cells["Code"].Value.ToString();

            stockDAL.DeleteStock(code);
            RefreshPersonalStockSetting();
        }

        //自选个股设置部分dgvStocksetting
        private void dgvStocksetting_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //dgvStocksetting.ClearSelection();
                dgvStocksetting.Rows[e.RowIndex].Selected = true;
                if (e.Button == MouseButtons.Right)
                {
                    dgvStocksetting.CurrentCell = dgvStocksetting.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
            CurrentStockCode = dgvStocksetting.SelectedRows[0].Cells["StockCode"].Value.Value<String>();
        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dgvStocksetting.SelectedRows)
            //{
            //    string code = row.Cells["StockCode"].Value.ToString();
            //    string name = row.Cells["StockName"].Value.ToString();
            //    string inShort = row.Cells["StockInShort"].Value.ToString();
            //    decimal warnprice_Min = row.Cells["WarnPrice_Min"].Value.Value<decimal>();
            //    decimal warnprice_Max = row.Cells["WarnPrice_Max"].Value.Value<decimal>();
            //    decimal warnpercent_Min = row.Cells["WarnPercent_Min"].Value.Value<decimal>();
            //    decimal warnpercent_Max = row.Cells["WarnPercent_Max"].Value.Value<decimal>();
            //    decimal buyPrice = row.Cells["BuyPrice"].Value.Value<decimal>();
            //    int buyCount = row.Cells["BuyCount"].Value.Value<int>();
            //    bool showInFolatWindow = row.Cells["ShowInFolatWindow"].Value.Value<bool>();
            //    int order = row.Cells["Order"].Value.Value<int>();
            //    string profitOrLossHistory = row.Cells["profitOrLossHistory"].Value.Value<string>();

            //    stockDAL.UpdateStock(code, name, inShort, warnprice_Min, warnprice_Max, warnpercent_Min, warnpercent_Max, buyPrice, buyCount, showInFolatWindow, order, profitOrLossHistory);
            //}

            try
            {
                DataTable table = dgvStocksetting.DataSource as DataTable;
                if (table != null)
                {
                    stockDAL.UpdateByDataSet(table);
                    Constants.ResetMyStock();
                }
            }
            catch (Exception ex)
            {
                JLog.Write(LogMode.Error, ex);
            }
        }
        //刷新自选列表
        private void btnRefreshPersonalStockSetting_Click(object sender, EventArgs e)
        {
            RefreshPersonalStockSetting();
        }
        private void RefreshPersonalStockSetting()
        {
            DataTable table = stockDAL.Query("select * from  MyStocks order by [order] desc");
            dgvStocksetting.DataSource = table;


            foreach (DataGridViewRow row in dgvStocksetting.Rows)
            {
                object objProfitOrLossHistory = row.Cells["ProfitOrLossHistory"].Value;
                if (objProfitOrLossHistory != null)
                {
                    row.Cells["HasProfitOrLoss"].Value = StockInfo.GetProfitOrLossHistoryData(objProfitOrLossHistory.ToString()).Sum();
                }
            }
            //List<StockInfo> stockList = stockDAL.getAllMyStock();
            if (!string.IsNullOrEmpty(CurrentStockCode))
            {
                foreach (DataGridViewRow dataRow in dgvStocksetting.Rows)
                {
                    if (CurrentStockCode == dataRow.Cells["StockCode"].Value.ToString())
                        dataRow.Selected = true;
                }
            }
            Constants.ResetMyStock();
        }

        private void btnUpdateStockInfo_Click(object sender, EventArgs e)
        {
            var list = StockService.AllStocks;
            stockDAL.ResetAllStocks(list);

        }


        #endregion

        #region K线公共右键菜单

        private void timeSheetMenuItem_Click(object sender, EventArgs e)
        {
            ShowChart(ChartType.TimeSheet);
        }

        private void dayKMenuItem_Click(object sender, EventArgs e)
        {
            ShowChart(ChartType.KOfDay);

        }

        private void weekKMenuItem_Click(object sender, EventArgs e)
        {
            ShowChart(ChartType.KOfWeek);
        }

        private void monthKMenuItem_Click(object sender, EventArgs e)
        {
            ShowChart(ChartType.KOfMonth);
        }
        #endregion

        #region 股票无关

        #region 鼠标滚轮 => 透明度

        void MyStock_MouseWheel(object sender, MouseEventArgs e)
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

        #endregion

        #endregion

        #region  辅助函数

        long i = 0;
        private void ShowMyStockInfoChanged(object sender, StockEventArgs e)
        {
            if (i % 3 == 0)
            {
                IEnumerable<StockInfo> stocks = e.Stocks;
                if (this.InvokeRequired == true)
                {
                    this.Invoke(new MyStockScreenMessage(BindMyStocks), stocks);
                }
                else
                {
                    BindMyStocks(stocks);
                }
            }
            i++;
        }

        private void BindMyStocks(IEnumerable<StockInfo> stocks = null)
        {
            lock (syncdgvMonitorStocks)
            {
                if (stocks == null)
                    stocks = StockService.MyStock;
                //List<StockInfo> list = new List<StockInfo>();
                //var groups = stocks.GroupBy(row => row.BuyCount > 0);
                //foreach (var item in groups)
                //{
                //    list.AddRange(item.OrderBy(row => row.SurgedRange).ToList());
                //}
                dgvMonitorStocks.DataSource = new BindingCollection<StockInfo>(stocks.OrderByDescending(row => row.Order).ThenByDescending(row => row.BuyCount).ThenBy(row => row.SurgedRange).ToList());
                if (!string.IsNullOrEmpty(CurrentStockCode))
                {
                    foreach (DataGridViewRow dataRow in dgvMonitorStocks.Rows)
                    {
                        if (CurrentStockCode == dataRow.Cells["S_Code"].Value.ToString())
                            dataRow.Selected = true;
                    }
                }
            }
        }

        private void ShowChart(ChartType chartType, bool inner = false)
        {
            //DataGridView dgv = contextMenuSourceGridView;
            //if (dgv == null)
            //{
            //    dgv = dgvMonitorStocks;
            //}
            //string codeColumnName = "S_Code";

            //if (dgv.Name == dgvQueryResultStocks.Name)
            //{
            //    codeColumnName = "Code";
            //}
            //else if (dgv.Name == dgvMonitorStocks.Name)
            //{
            //    codeColumnName = "S_Code";
            //}
            //else if (dgv.Name == dgvStocksetting.Name)
            //{
            //    codeColumnName = "StockCode";

            //}
            //else
            //{
            //    throw new NotSupportedException();
            //}

            //if (dgv != null && dgv.SelectedRows != null && dgv.SelectedRows.Count > 0)
            //{
            //var row = dgv.SelectedRows[0];

            if (!string.IsNullOrEmpty(CurrentStockCode))
            {
                string code = CurrentStockCode; //row.Cells[codeColumnName].Value.ToString();
                if (inner)
                {
                    stockChartCtrl.Show(code, chartType);
                }
                else
                {
                    ShowChart(code, chartType);
                }
            }
        }
        public void ShowChart(string stockNo, ChartType chartType)
        {
            StockChart chart = new StockChart();
            chart.Show(stockNo, chartType);
        }
        public void Show(int tabIndex = 0)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            StockService.AddEvent(ShowMyStockInfoChanged);
            base.Show();
            tabControl1.SelectedIndex = tabIndex;
        }

        public new void Hide()
        {
            StockService.RemoveEvent(ShowMyStockInfoChanged);
            base.Hide();
        }


        #endregion

        public void Close(bool force = false)
        {
            forceClose = force;
            base.Close();
        }

        #region 表格列显示设置

        #region 实时控盘表格列显示设置

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl3.SelectedIndex == 1)
            {
                cboxListMonitorSetting.Items.Clear();

                foreach (DataGridViewColumn item in dgvMonitorStocks.Columns)
                {
                    cboxListMonitorSetting.Items.Add(item, item.Visible);
                }
            }
        }

        private void btnSaveMonitorSetting_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn item in dgvMonitorStocks.Columns)
            {
                item.Visible = cboxListMonitorSetting.CheckedItems.Contains(item);
            }
        }

        #endregion

        #region 实时控盘表格列显示设置

        #endregion

        private void btnMyStockColumnSetting_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn item in dgvStocksetting.Columns)
            {
                item.Visible = cBoxListMyStockColumnsSetting.CheckedItems.Contains(item);
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl2.SelectedIndex == 2)
            {
                cBoxListMyStockColumnsSetting.Items.Clear();

                foreach (DataGridViewColumn item in dgvStocksetting.Columns)
                {
                    cBoxListMyStockColumnsSetting.Items.Add(item, item.Visible);
                }
            }
        }

        #endregion

    }
}

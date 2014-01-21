using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Justin.FrameWork.Entities;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Services;
using Justin.FrameWork.WinForm.Extensions;
using Justin.Log;
using Justin.Stock.Controls;
using Justin.Stock.Controls.Entities;
using Justin.Stock.DAL;
using Justin.Stock.Service;
using Justin.Stock.Service.Entities;
using Justin.Stock.Service.Models;
namespace Justin.Stock.Controls
{

    public partial class MyStock : Form
    {
        private bool forceClose = false;
        string CurrentStockCode { get; set; }
        StockDAL stockDAL = new StockDAL();

        private MyStock()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(MyStock_MouseWheel);
        }
        private static MyStock _instance;
        public static MyStock Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MyStock();

                return _instance;
            }
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

            #endregion

            DataService.AddEvent(ShowMyStockInfoChanged);

            dgvMonitorStocks.AutoGenerateColumns = false;
            dgvStocksetting.AutoGenerateColumns = false;
            BindMyStocks(DataService.MyStock);
        }

        private void MyStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
                forceClose = true;
            if (!forceClose)
            {
                DataService.RemoveEvent(ShowMyStockInfoChanged);
                e.Cancel = true;
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
                    this.systemSettingCtrl1.RefreshSetting();
                    break;
                case 4:
                    RefreshCheckHistory();
                    break;
            }
        }

        #endregion

        #region 实时读盘
        private DataGridView currentGridView;
        private void monitorContextMenu_Opening(object sender, CancelEventArgs e)
        {
            currentGridView = dgvMonitorStocks;
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
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentGridView != null)
            {
                currentGridView.ExportToExcel();
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
            string description = row.Cells["Description"].Value.ToJString();

            stockDAL.InsertStock(code, no, name, shortName, description, DataService.MyStock.Min(r => r.Order) - (decimal)0.01);
            RefreshPersonalStockSetting();
            DataService.ResetMyStock();

        }
        private void 删除自选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvQueryResultStocks.SelectedRows[0];

            string code = row.Cells["Code"].Value.ToString();

            stockDAL.DeleteStock(code);
            RefreshPersonalStockSetting();
            DataService.ResetMyStock();
        }

        //自选个股设置部分dgvStocksetting
        private void stockSettingsContextMenu_Opening(object sender, CancelEventArgs e)
        {
            currentGridView = dgvStocksetting;
        }
        private void dgvStocksetting_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvStocksetting.Rows[e.RowIndex].Selected = true;
                if (e.Button == MouseButtons.Right)
                {
                    dgvStocksetting.CurrentCell = dgvStocksetting.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
            CurrentStockCode = dgvStocksetting.SelectedRows[0].Cells["StockCode"].Value.Value<String>();
        }
        private void deletePersonalStockMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvStocksetting.SelectedRows[0];

            string code = row.Cells["StockCode"].Value.ToString();
            stockDAL.DeleteStock(code);
            RefreshPersonalStockSetting();
        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = dgvStocksetting.DataSource as DataTable;
                if (table != null)
                {
                    stockDAL.UpdateByDataSet(table);
                    DataService.ResetMyStock();
                }
            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex);
            }
        }
        private void excelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentGridView != null)
            {
                currentGridView.ExportToExcel();
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
                    decimal value = row.Cells["HasProfitBefore"].Value.Value<decimal>() + StockInfo.GetProfitHistoryData(objProfitOrLossHistory.ToString()).Sum();
                    row.Cells["HasProfitOrLoss"].Value = Math.Round(value, 2);
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
        }

        private void btnUpdateStockInfo_Click(object sender, EventArgs e)
        {
            var list = DataService.AllStocks;
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
        private void ShowMyStockInfoChanged(object sender, DataEventArgs e)
        {
            if (i % 3 == 0)
            {
                IEnumerable<StockInfo> stocks = e.Stocks;
                if (this.InvokeRequired == true)
                {
                    this.Invoke(new Action<IEnumerable<StockInfo>>(BindMyStocks), stocks);
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
                    stocks = DataService.MyStock;
                stocks = stocks.OrderByDescending(row => row.Order);

                dgvMonitorStocks.DataSource = new BindingCollection<StockInfo>(stocks.ToList());
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
            if (!string.IsNullOrEmpty(CurrentStockCode))
            {
                string code = CurrentStockCode;
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
            base.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            if (tabControl1.SelectedIndex != tabIndex)
            {
                tabControl1.SelectedIndex = tabIndex;
            }
        }
        public new void Hide()
        {
            base.Hide();
        }
        public void Close(bool force = false)
        {
            forceClose = force;
            base.Close();
        }

        #endregion

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

        #region CheckHistory

        public void RefreshCheckHistory()
        {
            dgvCheckHistory.DataSource = stockDAL.Query("select * from CheckHistory order by checktime");
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = dgvCheckHistory.DataSource as DataTable;
                if (table != null)
                {
                    bool success = stockDAL.UpdateCheckHistory(table);
                    if (success)
                        DataService.ResetStockSumInvest();

                }
            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex);
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}

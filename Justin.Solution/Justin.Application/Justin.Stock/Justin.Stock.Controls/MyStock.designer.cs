namespace Justin.Stock.Controls
{
    partial class MyStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyStock));
            this.stockSettingsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weekLinepMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryResultStocksContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加入自选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除自选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeSheetMenuItemOfAllStocks = new System.Windows.Forms.ToolStripMenuItem();
            this.dayKMenuItemOfAllStocks = new System.Windows.Forms.ToolStripMenuItem();
            this.weekKMenuItemOfAllStocks = new System.Windows.Forms.ToolStripMenuItem();
            this.MonthKMenuItemOfAllStocks = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPagePersonalOption = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageMyStockSetting = new System.Windows.Forms.TabPage();
            this.dgvStocksetting = new System.Windows.Forms.DataGridView();
            this.StockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockInShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarnPrice_Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarnPrice_Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarnPercent_Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarnPercent_Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HasProfitOrLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitOrLossHistory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowInFolatWindow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Warn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPageAllStockList = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvQueryResultStocks = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSrockName = new System.Windows.Forms.TextBox();
            this.btnQueryStock = new System.Windows.Forms.Button();
            this.btnRefreshPersonalStockSetting = new System.Windows.Forms.Button();
            this.btnUpdateStockInfo = new System.Windows.Forms.Button();
            this.tabPageMyStockColumnsSetting = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMyStockColumnSetting = new System.Windows.Forms.Button();
            this.cBoxListMyStockColumnsSetting = new System.Windows.Forms.CheckedListBox();
            this.tabPageStockList = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMonitorStocks = new System.Windows.Forms.DataGridView();
            this.S_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monitorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timeSheetMenuItemOfMyStock = new System.Windows.Forms.ToolStripMenuItem();
            this.dayKMenuItemOfMyStock = new System.Windows.Forms.ToolStripMenuItem();
            this.weekKMenuItemOfMyStock = new System.Windows.Forms.ToolStripMenuItem();
            this.monthKMenuItemOfMyStock = new System.Windows.Forms.ToolStripMenuItem();
            this.S_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Now = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_High = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_BuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_ProfitOrLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboxListMonitorSetting = new System.Windows.Forms.CheckedListBox();
            this.btnSaveMonitorSetting = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageStockChart = new System.Windows.Forms.TabPage();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockChartCtrl = new Justin.Stock.StockChartCtrl();
            this.stockSettingsContextMenu.SuspendLayout();
            this.queryResultStocksContextMenu.SuspendLayout();
            this.tabPagePersonalOption.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageMyStockSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocksetting)).BeginInit();
            this.tabPageAllStockList.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResultStocks)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPageMyStockColumnsSetting.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPageStockList.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitorStocks)).BeginInit();
            this.monitorContextMenu.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageStockChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // stockSettingsContextMenu
            // 
            this.stockSettingsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.timeSheetMenuItem,
            this.dayLineMenuItem,
            this.weekLinepMenuItem,
            this.monthLineMenuItem});
            this.stockSettingsContextMenu.Name = "MySrockMenu";
            this.stockSettingsContextMenu.Size = new System.Drawing.Size(99, 114);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // timeSheetMenuItem
            // 
            this.timeSheetMenuItem.Name = "timeSheetMenuItem";
            this.timeSheetMenuItem.Size = new System.Drawing.Size(98, 22);
            this.timeSheetMenuItem.Text = "分时";
            this.timeSheetMenuItem.Click += new System.EventHandler(this.timeSheetMenuItem_Click);
            // 
            // dayLineMenuItem
            // 
            this.dayLineMenuItem.Name = "dayLineMenuItem";
            this.dayLineMenuItem.Size = new System.Drawing.Size(98, 22);
            this.dayLineMenuItem.Text = "日线";
            this.dayLineMenuItem.Click += new System.EventHandler(this.dayKMenuItem_Click);
            // 
            // weekLinepMenuItem
            // 
            this.weekLinepMenuItem.Name = "weekLinepMenuItem";
            this.weekLinepMenuItem.Size = new System.Drawing.Size(98, 22);
            this.weekLinepMenuItem.Text = "周线";
            this.weekLinepMenuItem.Click += new System.EventHandler(this.weekKMenuItem_Click);
            // 
            // monthLineMenuItem
            // 
            this.monthLineMenuItem.Name = "monthLineMenuItem";
            this.monthLineMenuItem.Size = new System.Drawing.Size(98, 22);
            this.monthLineMenuItem.Text = "月线";
            this.monthLineMenuItem.Click += new System.EventHandler(this.monthKMenuItem_Click);
            // 
            // queryResultStocksContextMenu
            // 
            this.queryResultStocksContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加入自选ToolStripMenuItem,
            this.删除自选ToolStripMenuItem,
            this.timeSheetMenuItemOfAllStocks,
            this.dayKMenuItemOfAllStocks,
            this.weekKMenuItemOfAllStocks,
            this.MonthKMenuItemOfAllStocks});
            this.queryResultStocksContextMenu.Name = "contextMenuStrip1";
            this.queryResultStocksContextMenu.Size = new System.Drawing.Size(123, 136);
            // 
            // 加入自选ToolStripMenuItem
            // 
            this.加入自选ToolStripMenuItem.Name = "加入自选ToolStripMenuItem";
            this.加入自选ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.加入自选ToolStripMenuItem.Text = "加入自选";
            this.加入自选ToolStripMenuItem.Click += new System.EventHandler(this.加入自选ToolStripMenuItem_Click);
            // 
            // 删除自选ToolStripMenuItem
            // 
            this.删除自选ToolStripMenuItem.Name = "删除自选ToolStripMenuItem";
            this.删除自选ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.删除自选ToolStripMenuItem.Text = "删除自选";
            this.删除自选ToolStripMenuItem.Click += new System.EventHandler(this.删除自选ToolStripMenuItem_Click);
            // 
            // timeSheetMenuItemOfAllStocks
            // 
            this.timeSheetMenuItemOfAllStocks.Name = "timeSheetMenuItemOfAllStocks";
            this.timeSheetMenuItemOfAllStocks.Size = new System.Drawing.Size(122, 22);
            this.timeSheetMenuItemOfAllStocks.Text = "分时";
            this.timeSheetMenuItemOfAllStocks.Click += new System.EventHandler(this.timeSheetMenuItem_Click);
            // 
            // dayKMenuItemOfAllStocks
            // 
            this.dayKMenuItemOfAllStocks.Name = "dayKMenuItemOfAllStocks";
            this.dayKMenuItemOfAllStocks.Size = new System.Drawing.Size(122, 22);
            this.dayKMenuItemOfAllStocks.Text = "日线";
            this.dayKMenuItemOfAllStocks.Click += new System.EventHandler(this.dayKMenuItem_Click);
            // 
            // weekKMenuItemOfAllStocks
            // 
            this.weekKMenuItemOfAllStocks.Name = "weekKMenuItemOfAllStocks";
            this.weekKMenuItemOfAllStocks.Size = new System.Drawing.Size(122, 22);
            this.weekKMenuItemOfAllStocks.Text = "周线";
            this.weekKMenuItemOfAllStocks.Click += new System.EventHandler(this.weekKMenuItem_Click);
            // 
            // MonthKMenuItemOfAllStocks
            // 
            this.MonthKMenuItemOfAllStocks.Name = "MonthKMenuItemOfAllStocks";
            this.MonthKMenuItemOfAllStocks.Size = new System.Drawing.Size(122, 22);
            this.MonthKMenuItemOfAllStocks.Text = "月线";
            this.MonthKMenuItemOfAllStocks.Click += new System.EventHandler(this.monthKMenuItem_Click);
            // 
            // tabPagePersonalOption
            // 
            this.tabPagePersonalOption.Controls.Add(this.tabControl2);
            this.tabPagePersonalOption.Location = new System.Drawing.Point(4, 22);
            this.tabPagePersonalOption.Name = "tabPagePersonalOption";
            this.tabPagePersonalOption.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePersonalOption.Size = new System.Drawing.Size(877, 461);
            this.tabPagePersonalOption.TabIndex = 1;
            this.tabPagePersonalOption.Text = "Personal";
            this.tabPagePersonalOption.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageMyStockSetting);
            this.tabControl2.Controls.Add(this.tabPageAllStockList);
            this.tabControl2.Controls.Add(this.tabPageMyStockColumnsSetting);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(871, 455);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPageMyStockSetting
            // 
            this.tabPageMyStockSetting.Controls.Add(this.dgvStocksetting);
            this.tabPageMyStockSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPageMyStockSetting.Name = "tabPageMyStockSetting";
            this.tabPageMyStockSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMyStockSetting.Size = new System.Drawing.Size(863, 429);
            this.tabPageMyStockSetting.TabIndex = 0;
            this.tabPageMyStockSetting.Text = "Setting";
            this.tabPageMyStockSetting.UseVisualStyleBackColor = true;
            // 
            // dgvStocksetting
            // 
            this.dgvStocksetting.AllowUserToAddRows = false;
            this.dgvStocksetting.AllowUserToDeleteRows = false;
            this.dgvStocksetting.AllowUserToOrderColumns = true;
            this.dgvStocksetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvStocksetting.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvStocksetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStocksetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockName,
            this.StockCode,
            this.StockNo,
            this.StockInShort,
            this.WarnPrice_Min,
            this.WarnPrice_Max,
            this.WarnPercent_Min,
            this.WarnPercent_Max,
            this.BuyCount,
            this.BuyPrice,
            this.Order,
            this.HasProfitOrLoss,
            this.ProfitOrLossHistory,
            this.ShowInFolatWindow,
            this.Warn});
            this.dgvStocksetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStocksetting.Location = new System.Drawing.Point(3, 3);
            this.dgvStocksetting.MultiSelect = false;
            this.dgvStocksetting.Name = "dgvStocksetting";
            this.dgvStocksetting.RowTemplate.Height = 23;
            this.dgvStocksetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStocksetting.Size = new System.Drawing.Size(857, 423);
            this.dgvStocksetting.TabIndex = 22;
            this.dgvStocksetting.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStocksetting_CellMouseDown);
            // 
            // StockName
            // 
            this.StockName.ContextMenuStrip = this.stockSettingsContextMenu;
            this.StockName.DataPropertyName = "Name";
            this.StockName.Frozen = true;
            this.StockName.HeaderText = "Name";
            this.StockName.Name = "StockName";
            this.StockName.Width = 60;
            // 
            // StockCode
            // 
            this.StockCode.ContextMenuStrip = this.stockSettingsContextMenu;
            this.StockCode.DataPropertyName = "Code";
            this.StockCode.Frozen = true;
            this.StockCode.HeaderText = "Code";
            this.StockCode.Name = "StockCode";
            this.StockCode.ReadOnly = true;
            this.StockCode.Visible = false;
            this.StockCode.Width = 57;
            // 
            // StockNo
            // 
            this.StockNo.ContextMenuStrip = this.stockSettingsContextMenu;
            this.StockNo.DataPropertyName = "No";
            this.StockNo.HeaderText = "No";
            this.StockNo.Name = "StockNo";
            this.StockNo.ReadOnly = true;
            this.StockNo.Visible = false;
            this.StockNo.Width = 46;
            // 
            // StockInShort
            // 
            this.StockInShort.ContextMenuStrip = this.stockSettingsContextMenu;
            this.StockInShort.DataPropertyName = "SpellingInShort";
            this.StockInShort.HeaderText = "InShort";
            this.StockInShort.Name = "StockInShort";
            this.StockInShort.Width = 66;
            // 
            // WarnPrice_Min
            // 
            this.WarnPrice_Min.ContextMenuStrip = this.stockSettingsContextMenu;
            this.WarnPrice_Min.DataPropertyName = "WarnPrice_Min";
            this.WarnPrice_Min.HeaderText = "￥Min";
            this.WarnPrice_Min.Name = "WarnPrice_Min";
            this.WarnPrice_Min.Width = 61;
            // 
            // WarnPrice_Max
            // 
            this.WarnPrice_Max.ContextMenuStrip = this.stockSettingsContextMenu;
            this.WarnPrice_Max.DataPropertyName = "WarnPrice_Max";
            this.WarnPrice_Max.HeaderText = "￥Max";
            this.WarnPrice_Max.Name = "WarnPrice_Max";
            this.WarnPrice_Max.Width = 64;
            // 
            // WarnPercent_Min
            // 
            this.WarnPercent_Min.ContextMenuStrip = this.stockSettingsContextMenu;
            this.WarnPercent_Min.DataPropertyName = "WarnPercent_Min";
            this.WarnPercent_Min.HeaderText = "%Min";
            this.WarnPercent_Min.Name = "WarnPercent_Min";
            this.WarnPercent_Min.Width = 57;
            // 
            // WarnPercent_Max
            // 
            this.WarnPercent_Max.ContextMenuStrip = this.stockSettingsContextMenu;
            this.WarnPercent_Max.DataPropertyName = "WarnPercent_Max";
            this.WarnPercent_Max.HeaderText = "%Max";
            this.WarnPercent_Max.Name = "WarnPercent_Max";
            this.WarnPercent_Max.Width = 60;
            // 
            // BuyCount
            // 
            this.BuyCount.ContextMenuStrip = this.stockSettingsContextMenu;
            this.BuyCount.DataPropertyName = "BuyCount";
            this.BuyCount.HeaderText = "股数";
            this.BuyCount.Name = "BuyCount";
            this.BuyCount.Width = 56;
            // 
            // BuyPrice
            // 
            this.BuyPrice.ContextMenuStrip = this.stockSettingsContextMenu;
            this.BuyPrice.DataPropertyName = "BuyPrice";
            this.BuyPrice.HeaderText = "购入价";
            this.BuyPrice.Name = "BuyPrice";
            this.BuyPrice.Width = 68;
            // 
            // Order
            // 
            this.Order.ContextMenuStrip = this.stockSettingsContextMenu;
            this.Order.DataPropertyName = "Order";
            this.Order.HeaderText = "排序";
            this.Order.Name = "Order";
            this.Order.Width = 56;
            // 
            // HasProfitOrLoss
            // 
            this.HasProfitOrLoss.ContextMenuStrip = this.stockSettingsContextMenu;
            this.HasProfitOrLoss.HeaderText = "已盈亏";
            this.HasProfitOrLoss.Name = "HasProfitOrLoss";
            this.HasProfitOrLoss.ReadOnly = true;
            this.HasProfitOrLoss.Width = 68;
            // 
            // ProfitOrLossHistory
            // 
            this.ProfitOrLossHistory.ContextMenuStrip = this.stockSettingsContextMenu;
            this.ProfitOrLossHistory.DataPropertyName = "ProfitOrLossHistory";
            this.ProfitOrLossHistory.HeaderText = "历史盈亏";
            this.ProfitOrLossHistory.Name = "ProfitOrLossHistory";
            this.ProfitOrLossHistory.Width = 80;
            // 
            // ShowInFolatWindow
            // 
            this.ShowInFolatWindow.ContextMenuStrip = this.stockSettingsContextMenu;
            this.ShowInFolatWindow.DataPropertyName = "ShowInFolatWindow";
            this.ShowInFolatWindow.HeaderText = "显示";
            this.ShowInFolatWindow.Name = "ShowInFolatWindow";
            this.ShowInFolatWindow.Width = 37;
            // 
            // Warn
            // 
            this.Warn.ContextMenuStrip = this.stockSettingsContextMenu;
            this.Warn.DataPropertyName = "Warn";
            this.Warn.HeaderText = "预警";
            this.Warn.Name = "Warn";
            this.Warn.Width = 37;
            // 
            // tabPageAllStockList
            // 
            this.tabPageAllStockList.Controls.Add(this.tableLayoutPanel2);
            this.tabPageAllStockList.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllStockList.Name = "tabPageAllStockList";
            this.tabPageAllStockList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllStockList.Size = new System.Drawing.Size(863, 429);
            this.tabPageAllStockList.TabIndex = 1;
            this.tabPageAllStockList.Text = "AllStockList";
            this.tabPageAllStockList.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dgvQueryResultStocks, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(857, 423);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // dgvQueryResultStocks
            // 
            this.dgvQueryResultStocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvQueryResultStocks.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvQueryResultStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueryResultStocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.No,
            this._Name,
            this.InShort});
            this.dgvQueryResultStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueryResultStocks.Location = new System.Drawing.Point(3, 38);
            this.dgvQueryResultStocks.MultiSelect = false;
            this.dgvQueryResultStocks.Name = "dgvQueryResultStocks";
            this.dgvQueryResultStocks.RowTemplate.Height = 23;
            this.dgvQueryResultStocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueryResultStocks.Size = new System.Drawing.Size(851, 382);
            this.dgvQueryResultStocks.TabIndex = 10;
            this.dgvQueryResultStocks.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvQueryResultStocks_CellMouseDown);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Controls.Add(this.txtSrockName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnQueryStock, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRefreshPersonalStockSetting, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnUpdateStockInfo, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(851, 29);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // txtSrockName
            // 
            this.txtSrockName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSrockName.Location = new System.Drawing.Point(3, 3);
            this.txtSrockName.Name = "txtSrockName";
            this.txtSrockName.Size = new System.Drawing.Size(605, 20);
            this.txtSrockName.TabIndex = 10;
            // 
            // btnQueryStock
            // 
            this.btnQueryStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQueryStock.Location = new System.Drawing.Point(614, 3);
            this.btnQueryStock.Name = "btnQueryStock";
            this.btnQueryStock.Size = new System.Drawing.Size(74, 23);
            this.btnQueryStock.TabIndex = 11;
            this.btnQueryStock.Text = "查询个股";
            this.btnQueryStock.UseVisualStyleBackColor = true;
            this.btnQueryStock.Click += new System.EventHandler(this.btnQueryStock_Click);
            // 
            // btnRefreshPersonalStockSetting
            // 
            this.btnRefreshPersonalStockSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefreshPersonalStockSetting.Location = new System.Drawing.Point(694, 3);
            this.btnRefreshPersonalStockSetting.Name = "btnRefreshPersonalStockSetting";
            this.btnRefreshPersonalStockSetting.Size = new System.Drawing.Size(74, 23);
            this.btnRefreshPersonalStockSetting.TabIndex = 12;
            this.btnRefreshPersonalStockSetting.Text = "刷新个股";
            this.btnRefreshPersonalStockSetting.UseVisualStyleBackColor = true;
            this.btnRefreshPersonalStockSetting.Click += new System.EventHandler(this.btnRefreshPersonalStockSetting_Click);
            // 
            // btnUpdateStockInfo
            // 
            this.btnUpdateStockInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateStockInfo.Location = new System.Drawing.Point(774, 3);
            this.btnUpdateStockInfo.Name = "btnUpdateStockInfo";
            this.btnUpdateStockInfo.Size = new System.Drawing.Size(74, 23);
            this.btnUpdateStockInfo.TabIndex = 13;
            this.btnUpdateStockInfo.Text = "代码更新";
            this.btnUpdateStockInfo.UseVisualStyleBackColor = true;
            this.btnUpdateStockInfo.Click += new System.EventHandler(this.btnUpdateStockInfo_Click);
            // 
            // tabPageMyStockColumnsSetting
            // 
            this.tabPageMyStockColumnsSetting.Controls.Add(this.tableLayoutPanel4);
            this.tabPageMyStockColumnsSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPageMyStockColumnsSetting.Name = "tabPageMyStockColumnsSetting";
            this.tabPageMyStockColumnsSetting.Size = new System.Drawing.Size(863, 429);
            this.tabPageMyStockColumnsSetting.TabIndex = 2;
            this.tabPageMyStockColumnsSetting.Text = "ColumnsSetting";
            this.tabPageMyStockColumnsSetting.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnMyStockColumnSetting, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.cBoxListMyStockColumnsSetting, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.77109F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.228916F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(863, 429);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnMyStockColumnSetting
            // 
            this.btnMyStockColumnSetting.Location = new System.Drawing.Point(3, 400);
            this.btnMyStockColumnSetting.Name = "btnMyStockColumnSetting";
            this.btnMyStockColumnSetting.Size = new System.Drawing.Size(484, 22);
            this.btnMyStockColumnSetting.TabIndex = 2;
            this.btnMyStockColumnSetting.Text = "√";
            this.btnMyStockColumnSetting.UseVisualStyleBackColor = true;
            this.btnMyStockColumnSetting.Click += new System.EventHandler(this.btnMyStockColumnSetting_Click);
            // 
            // cBoxListMyStockColumnsSetting
            // 
            this.cBoxListMyStockColumnsSetting.CheckOnClick = true;
            this.cBoxListMyStockColumnsSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoxListMyStockColumnsSetting.FormattingEnabled = true;
            this.cBoxListMyStockColumnsSetting.Location = new System.Drawing.Point(3, 3);
            this.cBoxListMyStockColumnsSetting.Name = "cBoxListMyStockColumnsSetting";
            this.cBoxListMyStockColumnsSetting.Size = new System.Drawing.Size(857, 391);
            this.cBoxListMyStockColumnsSetting.TabIndex = 3;
            // 
            // tabPageStockList
            // 
            this.tabPageStockList.Controls.Add(this.tabControl3);
            this.tabPageStockList.Location = new System.Drawing.Point(4, 22);
            this.tabPageStockList.Name = "tabPageStockList";
            this.tabPageStockList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStockList.Size = new System.Drawing.Size(877, 461);
            this.tabPageStockList.TabIndex = 0;
            this.tabPageStockList.Text = "Monitor";
            this.tabPageStockList.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage1);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(3, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(871, 455);
            this.tabControl3.TabIndex = 2;
            this.tabControl3.SelectedIndexChanged += new System.EventHandler(this.tabControl3_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMonitorStocks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(863, 429);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvMonitorStocks
            // 
            this.dgvMonitorStocks.AllowUserToAddRows = false;
            this.dgvMonitorStocks.AllowUserToDeleteRows = false;
            this.dgvMonitorStocks.AllowUserToOrderColumns = true;
            this.dgvMonitorStocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMonitorStocks.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMonitorStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonitorStocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.S_Name,
            this.S_Code,
            this.S_Low,
            this.S_Now,
            this.S_High,
            this.S_BuyPrice,
            this.S_Profit,
            this.S_ProfitOrLoss,
            this.S_Percent,
            this.SCount});
            this.dgvMonitorStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonitorStocks.Location = new System.Drawing.Point(3, 3);
            this.dgvMonitorStocks.MultiSelect = false;
            this.dgvMonitorStocks.Name = "dgvMonitorStocks";
            this.dgvMonitorStocks.ReadOnly = true;
            this.dgvMonitorStocks.RowTemplate.Height = 23;
            this.dgvMonitorStocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMonitorStocks.Size = new System.Drawing.Size(857, 423);
            this.dgvMonitorStocks.TabIndex = 1;
            this.dgvMonitorStocks.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMonitorStocks_CellMouseDown);
            // 
            // S_Name
            // 
            this.S_Name.ContextMenuStrip = this.monitorContextMenu;
            this.S_Name.DataPropertyName = "Name";
            this.S_Name.Frozen = true;
            this.S_Name.HeaderText = "名称";
            this.S_Name.Name = "S_Name";
            this.S_Name.ReadOnly = true;
            this.S_Name.Width = 56;
            // 
            // monitorContextMenu
            // 
            this.monitorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeSheetMenuItemOfMyStock,
            this.dayKMenuItemOfMyStock,
            this.weekKMenuItemOfMyStock,
            this.monthKMenuItemOfMyStock});
            this.monitorContextMenu.Name = "MySrockMenu";
            this.monitorContextMenu.Size = new System.Drawing.Size(99, 92);
            // 
            // timeSheetMenuItemOfMyStock
            // 
            this.timeSheetMenuItemOfMyStock.Name = "timeSheetMenuItemOfMyStock";
            this.timeSheetMenuItemOfMyStock.Size = new System.Drawing.Size(98, 22);
            this.timeSheetMenuItemOfMyStock.Text = "分时";
            this.timeSheetMenuItemOfMyStock.Click += new System.EventHandler(this.timeSheetMenuItem_Click);
            // 
            // dayKMenuItemOfMyStock
            // 
            this.dayKMenuItemOfMyStock.Name = "dayKMenuItemOfMyStock";
            this.dayKMenuItemOfMyStock.Size = new System.Drawing.Size(98, 22);
            this.dayKMenuItemOfMyStock.Text = "日线";
            this.dayKMenuItemOfMyStock.Click += new System.EventHandler(this.dayKMenuItem_Click);
            // 
            // weekKMenuItemOfMyStock
            // 
            this.weekKMenuItemOfMyStock.Name = "weekKMenuItemOfMyStock";
            this.weekKMenuItemOfMyStock.Size = new System.Drawing.Size(98, 22);
            this.weekKMenuItemOfMyStock.Text = "周线";
            this.weekKMenuItemOfMyStock.Click += new System.EventHandler(this.weekKMenuItem_Click);
            // 
            // monthKMenuItemOfMyStock
            // 
            this.monthKMenuItemOfMyStock.Name = "monthKMenuItemOfMyStock";
            this.monthKMenuItemOfMyStock.Size = new System.Drawing.Size(98, 22);
            this.monthKMenuItemOfMyStock.Text = "月线";
            this.monthKMenuItemOfMyStock.Click += new System.EventHandler(this.monthKMenuItem_Click);
            // 
            // S_Code
            // 
            this.S_Code.ContextMenuStrip = this.monitorContextMenu;
            this.S_Code.DataPropertyName = "Code";
            this.S_Code.HeaderText = "Code";
            this.S_Code.Name = "S_Code";
            this.S_Code.ReadOnly = true;
            this.S_Code.Width = 57;
            // 
            // S_Low
            // 
            this.S_Low.ContextMenuStrip = this.monitorContextMenu;
            this.S_Low.DataPropertyName = "PriceTodayLow";
            this.S_Low.HeaderText = "最低";
            this.S_Low.Name = "S_Low";
            this.S_Low.ReadOnly = true;
            this.S_Low.Width = 56;
            // 
            // S_Now
            // 
            this.S_Now.ContextMenuStrip = this.monitorContextMenu;
            this.S_Now.DataPropertyName = "PriceNow";
            this.S_Now.HeaderText = "现价";
            this.S_Now.Name = "S_Now";
            this.S_Now.ReadOnly = true;
            this.S_Now.Width = 56;
            // 
            // S_High
            // 
            this.S_High.ContextMenuStrip = this.monitorContextMenu;
            this.S_High.DataPropertyName = "PriceTodayHigh";
            this.S_High.HeaderText = "最高";
            this.S_High.Name = "S_High";
            this.S_High.ReadOnly = true;
            this.S_High.Width = 56;
            // 
            // S_BuyPrice
            // 
            this.S_BuyPrice.ContextMenuStrip = this.monitorContextMenu;
            this.S_BuyPrice.DataPropertyName = "BuyPrice";
            this.S_BuyPrice.HeaderText = "成本";
            this.S_BuyPrice.Name = "S_BuyPrice";
            this.S_BuyPrice.ReadOnly = true;
            this.S_BuyPrice.Width = 56;
            // 
            // S_Profit
            // 
            this.S_Profit.ContextMenuStrip = this.monitorContextMenu;
            this.S_Profit.DataPropertyName = "CurrentProfitOrLoss";
            this.S_Profit.HeaderText = "盈亏";
            this.S_Profit.Name = "S_Profit";
            this.S_Profit.ReadOnly = true;
            this.S_Profit.Width = 56;
            // 
            // S_ProfitOrLoss
            // 
            this.S_ProfitOrLoss.ContextMenuStrip = this.monitorContextMenu;
            this.S_ProfitOrLoss.DataPropertyName = "SumProfitOrLoss";
            this.S_ProfitOrLoss.HeaderText = "总盈亏";
            this.S_ProfitOrLoss.Name = "S_ProfitOrLoss";
            this.S_ProfitOrLoss.ReadOnly = true;
            this.S_ProfitOrLoss.Width = 68;
            // 
            // S_Percent
            // 
            this.S_Percent.ContextMenuStrip = this.monitorContextMenu;
            this.S_Percent.DataPropertyName = "SurgedRange";
            this.S_Percent.HeaderText = "涨幅";
            this.S_Percent.Name = "S_Percent";
            this.S_Percent.ReadOnly = true;
            this.S_Percent.Width = 56;
            // 
            // SCount
            // 
            this.SCount.DataPropertyName = "BuyCount";
            this.SCount.HeaderText = "股数";
            this.SCount.Name = "SCount";
            this.SCount.ReadOnly = true;
            this.SCount.Width = 56;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(863, 429);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cboxListMonitorSetting, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveMonitorSetting, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 423);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // cboxListMonitorSetting
            // 
            this.cboxListMonitorSetting.CheckOnClick = true;
            this.cboxListMonitorSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxListMonitorSetting.FormattingEnabled = true;
            this.cboxListMonitorSetting.Location = new System.Drawing.Point(3, 3);
            this.cboxListMonitorSetting.Name = "cboxListMonitorSetting";
            this.cboxListMonitorSetting.Size = new System.Drawing.Size(851, 389);
            this.cboxListMonitorSetting.TabIndex = 0;
            // 
            // btnSaveMonitorSetting
            // 
            this.btnSaveMonitorSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveMonitorSetting.Location = new System.Drawing.Point(3, 398);
            this.btnSaveMonitorSetting.Name = "btnSaveMonitorSetting";
            this.btnSaveMonitorSetting.Size = new System.Drawing.Size(851, 22);
            this.btnSaveMonitorSetting.TabIndex = 1;
            this.btnSaveMonitorSetting.Text = "√";
            this.btnSaveMonitorSetting.UseVisualStyleBackColor = true;
            this.btnSaveMonitorSetting.Click += new System.EventHandler(this.btnSaveMonitorSetting_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageStockList);
            this.tabControl1.Controls.Add(this.tabPagePersonalOption);
            this.tabControl1.Controls.Add(this.tabPageStockChart);
            this.tabControl1.Controls.Add(this.tabPageSetting);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(885, 487);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageStockChart
            // 
            this.tabPageStockChart.Controls.Add(this.stockChartCtrl);
            this.tabPageStockChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageStockChart.Name = "tabPageStockChart";
            this.tabPageStockChart.Size = new System.Drawing.Size(877, 461);
            this.tabPageStockChart.TabIndex = 3;
            this.tabPageStockChart.Text = "Chart";
            this.tabPageStockChart.UseVisualStyleBackColor = true;
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.Size = new System.Drawing.Size(877, 461);
            this.tabPageSetting.TabIndex = 4;
            this.tabPageSetting.Text = "Settings";
            this.tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // Code
            // 
            this.Code.ContextMenuStrip = this.queryResultStocksContextMenu;
            this.Code.DataPropertyName = "StockCode";
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Width = 57;
            // 
            // No
            // 
            this.No.DataPropertyName = "StockNo";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.Width = 46;
            // 
            // _Name
            // 
            this._Name.DataPropertyName = "StockName";
            this._Name.HeaderText = "StockName";
            this._Name.Name = "_Name";
            this._Name.Width = 88;
            // 
            // InShort
            // 
            this.InShort.DataPropertyName = "SpellingInShort";
            this.InShort.HeaderText = "InShort";
            this.InShort.Name = "InShort";
            this.InShort.Width = 66;
            // 
            // stockChartCtrl
            // 
            this.stockChartCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockChartCtrl.Location = new System.Drawing.Point(0, 0);
            this.stockChartCtrl.Name = "stockChartCtrl";
            this.stockChartCtrl.Size = new System.Drawing.Size(877, 461);
            this.stockChartCtrl.TabIndex = 0;
            // 
            // MyStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 487);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyStock_FormClosing);
            this.Load += new System.EventHandler(this.MyStock_Load);
            this.stockSettingsContextMenu.ResumeLayout(false);
            this.queryResultStocksContextMenu.ResumeLayout(false);
            this.tabPagePersonalOption.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageMyStockSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocksetting)).EndInit();
            this.tabPageAllStockList.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResultStocks)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPageMyStockColumnsSetting.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabPageStockList.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitorStocks)).EndInit();
            this.monitorContextMenu.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageStockChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip queryResultStocksContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 加入自选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除自选ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip stockSettingsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPagePersonalOption;
        private System.Windows.Forms.TabPage tabPageStockList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem timeSheetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dayLineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weekLinepMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthLineMenuItem;
        private System.Windows.Forms.ContextMenuStrip monitorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem timeSheetMenuItemOfMyStock;
        private System.Windows.Forms.ToolStripMenuItem dayKMenuItemOfMyStock;
        private System.Windows.Forms.ToolStripMenuItem weekKMenuItemOfMyStock;
        private System.Windows.Forms.ToolStripMenuItem monthKMenuItemOfMyStock;
        private System.Windows.Forms.ToolStripMenuItem timeSheetMenuItemOfAllStocks;
        private System.Windows.Forms.ToolStripMenuItem dayKMenuItemOfAllStocks;
        private System.Windows.Forms.ToolStripMenuItem weekKMenuItemOfAllStocks;
        private System.Windows.Forms.ToolStripMenuItem MonthKMenuItemOfAllStocks;
        private System.Windows.Forms.TabPage tabPageStockChart;
        private System.Windows.Forms.DataGridView dgvMonitorStocks;
        private StockChartCtrl stockChartCtrl;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageMyStockSetting;
        private System.Windows.Forms.DataGridView dgvStocksetting;
        private System.Windows.Forms.TabPage tabPageAllStockList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvQueryResultStocks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox txtSrockName;
        private System.Windows.Forms.Button btnQueryStock;
        private System.Windows.Forms.Button btnRefreshPersonalStockSetting;
        private System.Windows.Forms.Button btnUpdateStockInfo;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckedListBox cboxListMonitorSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnSaveMonitorSetting;
        private System.Windows.Forms.TabPage tabPageMyStockColumnsSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnMyStockColumnSetting;
        private System.Windows.Forms.CheckedListBox cBoxListMyStockColumnsSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockInShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarnPrice_Min;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarnPrice_Max;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarnPercent_Min;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarnPercent_Max;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn HasProfitOrLoss;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitOrLossHistory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ShowInFolatWindow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Warn;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Low;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Now;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_High;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_BuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Profit;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_ProfitOrLoss;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCount;
        private System.Windows.Forms.TabPage tabPageSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn InShort;

    }
}
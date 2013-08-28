namespace Justin.Stock.Controls
{
    partial class DeskStockCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.deskMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timeSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayKMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WeekKMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MonthKMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalStocksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorStockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deskMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // deskMenu
            // 
            this.deskMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeSheetMenuItem,
            this.DayKMenuItem,
            this.WeekKMenuItem,
            this.MonthKMenuItem,
            this.monitorStockMenuItem,
            this.personalStocksMenuItem,
            this.systemSettingMenuItem});
            this.deskMenu.Name = "contextMenuStrip1";
            this.deskMenu.Size = new System.Drawing.Size(153, 180);
            this.deskMenu.Opening += new System.ComponentModel.CancelEventHandler(this.deskMenu_Opening);
            // 
            // timeSheetMenuItem
            // 
            this.timeSheetMenuItem.Name = "timeSheetMenuItem";
            this.timeSheetMenuItem.Size = new System.Drawing.Size(152, 22);
            this.timeSheetMenuItem.Text = "TimeSheet";
            this.timeSheetMenuItem.Click += new System.EventHandler(this.timeSheetMenuItem_Click);
            // 
            // DayKMenuItem
            // 
            this.DayKMenuItem.Name = "DayKMenuItem";
            this.DayKMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DayKMenuItem.Text = "DayK";
            this.DayKMenuItem.Click += new System.EventHandler(this.DayKMenuItem_Click);
            // 
            // WeekKMenuItem
            // 
            this.WeekKMenuItem.Name = "WeekKMenuItem";
            this.WeekKMenuItem.Size = new System.Drawing.Size(152, 22);
            this.WeekKMenuItem.Text = "WeekK";
            this.WeekKMenuItem.Click += new System.EventHandler(this.WeekKMenuItem_Click);
            // 
            // MonthKMenuItem
            // 
            this.MonthKMenuItem.Name = "MonthKMenuItem";
            this.MonthKMenuItem.Size = new System.Drawing.Size(152, 22);
            this.MonthKMenuItem.Text = "MonthK";
            this.MonthKMenuItem.Click += new System.EventHandler(this.MonthKMenuItem_Click);
            // 
            // personalStocksMenuItem
            // 
            this.personalStocksMenuItem.Name = "personalStocksMenuItem";
            this.personalStocksMenuItem.Size = new System.Drawing.Size(152, 22);
            this.personalStocksMenuItem.Text = "Personal";
            this.personalStocksMenuItem.Click += new System.EventHandler(this.personalStocksMenuItem_Click);
            // 
            // systemSettingMenuItem
            // 
            this.systemSettingMenuItem.Name = "systemSettingMenuItem";
            this.systemSettingMenuItem.Size = new System.Drawing.Size(152, 22);
            this.systemSettingMenuItem.Text = "SystemSetting";
            this.systemSettingMenuItem.Click += new System.EventHandler(this.systemSettingMenuItem_Click);
            // 
            // monitorStockMenuItem
            // 
            this.monitorStockMenuItem.Name = "monitorStockMenuItem";
            this.monitorStockMenuItem.Size = new System.Drawing.Size(152, 22);
            this.monitorStockMenuItem.Text = "Monitor";
            this.monitorStockMenuItem.Click += new System.EventHandler(this.monitorStockMenuItem_Click);
            // 
            // DeskStockCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.deskMenu;
            this.Name = "DeskStockCtrl";
            this.Size = new System.Drawing.Size(296, 109);
            this.Load += new System.EventHandler(this.DeskStockCtrl_Load);
            this.deskMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip deskMenu;
        private System.Windows.Forms.ToolStripMenuItem timeSheetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DayKMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WeekKMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MonthKMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalStocksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemSettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorStockMenuItem;
    }
}

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
            this.monitorStockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalStocksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemSettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripSeparator1,
            this.monitorStockMenuItem,
            this.personalStocksMenuItem,
            this.toolStripSeparator2,
            this.systemSettingMenuItem,
            this.toolStripSeparator3,
            this.topMostToolStripMenuItem,
            this.autoHideToolStripMenuItem});
            this.deskMenu.Name = "contextMenuStrip1";
            this.deskMenu.Size = new System.Drawing.Size(153, 242);
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
            // monitorStockMenuItem
            // 
            this.monitorStockMenuItem.Name = "monitorStockMenuItem";
            this.monitorStockMenuItem.Size = new System.Drawing.Size(152, 22);
            this.monitorStockMenuItem.Text = "Monitor";
            this.monitorStockMenuItem.Click += new System.EventHandler(this.monitorStockMenuItem_Click);
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
            // topMostToolStripMenuItem
            // 
            this.topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
            this.topMostToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.topMostToolStripMenuItem.Text = "TopMost";
            this.topMostToolStripMenuItem.Click += new System.EventHandler(this.topMostToolStripMenuItem_Click);
            // 
            // autoHideToolStripMenuItem
            // 
            this.autoHideToolStripMenuItem.Name = "autoHideToolStripMenuItem";
            this.autoHideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.autoHideToolStripMenuItem.Text = "Auto Hide";
            this.autoHideToolStripMenuItem.Click += new System.EventHandler(this.autoHideToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
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
        private System.Windows.Forms.ToolStripMenuItem topMostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoHideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

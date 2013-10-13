namespace Justin.Stock
{
    partial class DeskStocks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeskStocks));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.noticeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.topMostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DetailInfoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.deskStockCtrl1 = new Justin.Stock.Controls.DeskStockCtrl();
            this.personalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noticeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.noticeMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // noticeMenu
            // 
            this.noticeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topMostMenuItem,
            this.monitorMenuItem,
            this.personalMenuItem,
            this.settingMenuItem,
            this.exitMenuItem});
            this.noticeMenu.Name = "contextMenuStrip1";
            this.noticeMenu.Size = new System.Drawing.Size(153, 136);
            this.noticeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.noticeMenu_Opening);
            // 
            // topMostMenuItem
            // 
            this.topMostMenuItem.Name = "topMostMenuItem";
            this.topMostMenuItem.Size = new System.Drawing.Size(152, 22);
            this.topMostMenuItem.Text = "TopMost";
            this.topMostMenuItem.Click += new System.EventHandler(this.topMostMenuItem_Click);
            // 
            // settingMenuItem
            // 
            this.settingMenuItem.Name = "settingMenuItem";
            this.settingMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingMenuItem.Text = "Settings";
            this.settingMenuItem.Click += new System.EventHandler(this.settingMenuItem_Click);
            // 
            // monitorMenuItem
            // 
            this.monitorMenuItem.Name = "monitorMenuItem";
            this.monitorMenuItem.Size = new System.Drawing.Size(152, 22);
            this.monitorMenuItem.Text = "Monitor";
            this.monitorMenuItem.Click += new System.EventHandler(this.MonitorMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // deskStockCtrl1
            // 
            this.deskStockCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deskStockCtrl1.Location = new System.Drawing.Point(0, 0);
            this.deskStockCtrl1.Name = "deskStockCtrl1";
            this.deskStockCtrl1.Size = new System.Drawing.Size(303, 106);
            this.deskStockCtrl1.TabIndex = 2;
            // 
            // personalMenuItem
            // 
            this.personalMenuItem.Name = "personalMenuItem";
            this.personalMenuItem.Size = new System.Drawing.Size(152, 22);
            this.personalMenuItem.Text = "Person";
            this.personalMenuItem.Click += new System.EventHandler(this.personalMenuItem_Click);
            // 
            // DeskStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 106);
            this.Controls.Add(this.deskStockCtrl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeskStocks";
            this.Opacity = 0.2D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeskStocks_FormClosing);
            this.Load += new System.EventHandler(this.DeskStocks_Load);
            this.ResizeEnd += new System.EventHandler(this.DeskStocks_ResizeEnd);
            this.noticeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip noticeMenu;
        private System.Windows.Forms.ToolStripMenuItem topMostMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolTip DetailInfoToolTip;
        private System.Windows.Forms.ToolStripMenuItem monitorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingMenuItem;
        private Controls.DeskStockCtrl deskStockCtrl1;
        private System.Windows.Forms.ToolStripMenuItem personalMenuItem;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.Extensions;
using Justin.FrameWork.WinForm.Helper;
using Justin.Log;
using Justin.Stock.Controls;
using Justin.Stock.Controls.Entities;
using Justin.Stock.DAL;
using Justin.Stock.Extensions;
using Justin.Stock.Service.Entities;
using Justin.Stock.Service.Models;

namespace Justin.Stock
{
    public partial class DeskStocks : AutoAnchorForm
    {
        #region 私有变量

        string fileName;
        bool forceClose = false;
        bool IsShow = false;//当前是否显示窗体     

        #endregion

        #region 窗体加载、关闭事件

        public DeskStocks(string fileName)
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(DeskStocks_MouseWheel);
            this.fileName = fileName;
        }

        private void DeskStocks_Load(object sender, EventArgs e)
        {
            #region 股票无关

            LoadLastFormPosition();
            RegisterHotKey();
            notifyIcon1.Text = DateTime.Now.ToString();

            #endregion

            DeskStockCtrl.DisplaySummaryMessageAction += ShowTotal;
            //StockService.Start();

            SetVisibleCore(false);

        }
        public void ShowTotal(string message)
        {
            this.Text = message;
        }
        private void DeskStocks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                CloseMe();
            }
        }

        #endregion

        #region 通知区域  菜单

        private void noticeMenu_Opening(object sender, CancelEventArgs e)
        {
            this.topMostMenuItem.Checked = this.TopMost;
            this.autoHideMenuItem.Checked = this.EnableAutoAnchor;
        }

        private void topMostMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }
        private void MonitorMenuItem_Click(object sender, EventArgs e)
        {
            deskStockCtrl1.ShowMyStock(0);
        }
        private void personalMenuItem_Click(object sender, EventArgs e)
        {
            deskStockCtrl1.ShowMyStock(1);
        }
        private void settingMenuItem_Click(object sender, EventArgs e)
        {
            deskStockCtrl1.ShowMyStock(3);
        }
        private void inScreenMenuItem_Click(object sender, EventArgs e)
        {
            this.Top = 300;
            this.Left = 400;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }
        private void autoHideMenuItem_Click(object sender, EventArgs e)
        {
            this.EnableAutoAnchor = this.autoHideMenuItem.Checked = !this.autoHideMenuItem.Checked;
        }
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            forceClose = true;
            this.Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IsShow)
            {
                this.Hide();
            }
            else
            {
                this.EnableAutoAnchor = false;
                this.Show();
            }
        }

        #endregion

        #region 股票信息无关

        #region 窗体位置
        private void DeskStocks_ResizeEnd(object sender, EventArgs e)
        {
            SaveFormPosition();
        }
        #region 保存窗体位置信息和宽高信息，下次启动时加载

        private void SaveFormPosition()
        {

            if (Constants.Setting.DBPath == Constants.DefaultDBPath)
            {
                Constants.Setting.DBPath = Path.GetFileName(Constants.DefaultDBPath);
            }
            Constants.Setting.StartPosition.Top = this.Top;
            Constants.Setting.StartPosition.Left = this.Left;
            Constants.Setting.StartPosition.Height = this.Height;
            Constants.Setting.StartPosition.Width = this.Width;

            SerializeHelper.XmlSerializeToFile<JSettings>(Constants.Setting, fileName, true);
        }

        #endregion
        #region 加载上次窗体关闭时的问题
        private void LoadLastFormPosition()
        {
            this.Top = Constants.Setting.StartPosition.Top;
            this.Left = Constants.Setting.StartPosition.Left;
            this.Width = Constants.Setting.StartPosition.Width;
            this.Height = Constants.Setting.StartPosition.Height;
        }

        #endregion
        #endregion
        #region 快捷键
        HotkeyHelper hotkeyHelper;
        int showWindowKey;
        int showWarnKey;
        int stopOrStartServicekey;

        private void RegisterHotKey()
        {
            hotkeyHelper = new HotkeyHelper(this.Handle);
            showWindowKey = hotkeyHelper.RegisterHotkey(Keys.Oemtilde, HotkeyHelper.KeyFlags.MOD_CONTROL);
            stopOrStartServicekey = hotkeyHelper.RegisterHotkey(Keys.Oemtilde, HotkeyHelper.KeyFlags.MOD_WIN);
            showWarnKey = hotkeyHelper.RegisterHotkey(Keys.D1, HotkeyHelper.KeyFlags.MOD_CONTROL);
            hotkeyHelper.OnHotkey += new HotkeyEventHandler(OnHotkey);
        }
        private void OnHotkey(int hotkeyID)
        {
            if (hotkeyID == showWindowKey)
            {
                if (IsShow)
                {
                    this.EnableAutoAnchor = true;
                    this.Hide();
                }
                else
                {
                    this.EnableAutoAnchor = false;
                    this.Show();
                }
            }
            else if (hotkeyID == showWarnKey)
            {
                Constants.Setting.ShowWarn = !Constants.Setting.ShowWarn;
            }
            else if (hotkeyID == stopOrStartServicekey)
            {
                if (StockService.IsRunning)
                {
                    StockService.Stop();
                    this.EnableAutoAnchor = true;
                    this.Hide();
                }
                else
                {
                    StockService.ReStart();
                    this.EnableAutoAnchor = false;
                    this.Show();
                }
            }
        }

        #endregion
        #region 鼠标滚轮 => 透明度

        void DeskStocks_MouseWheel(object sender, MouseEventArgs e)
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
        #region 启动最小化

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);
        }

        #endregion

        #endregion

        #region 覆盖系统方法

        private new void Show()
        {
            IsShow = true;
            base.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
            if (!StockService.IsRunning)
            {
                StockService.Start();
            }
        }

        private new void Hide()
        {
            base.Hide();
            IsShow = false;
        }

        #endregion

        private void CloseMe()
        {
            StockService.Stop();
            deskStockCtrl1.RemoveDisplayHandler();
            deskStockCtrl1.CloseChildrenForm();
            hotkeyHelper.UnregisterHotkeys();
            Application.Exit();
        }



    }
}

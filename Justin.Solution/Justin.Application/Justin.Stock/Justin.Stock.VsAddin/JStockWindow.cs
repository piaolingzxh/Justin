using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.Helper;
using Justin.Stock.Controls;
using Justin.Stock.Controls.Entities;
using Justin.Stock.Service.Models;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Justin.Justin_Stock_VsAddin
{
    public delegate void DisplayHandler(bool show, bool force);
    [Guid("dc998857-17c2-4ead-aebd-c0b6a218728f")]
    public class JStockWindow : ToolWindowPane
    {


        DeskStockCtrl deskStock;

        public JStockWindow() :
            base(null)
        {
            this.Caption = "";
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            LoadSetting();
            Init();
        }

        private void LoadSetting()
        {
            Justin.Stock.Controls.Entities.Constants.SettingFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), string.Format(@"JStock\{0}", Justin.Stock.Controls.Entities.Constants.SettingFileName));

            string settingData = File.ReadAllText(Justin.Stock.Controls.Entities.Constants.SettingFilePath, Encoding.UTF8);
            JSettings settings = SerializeHelper.XmlDeserialize<JSettings>(settingData);
            if (settings == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("加载配置{0}信息出错", Justin.Stock.Controls.Entities.Constants.SettingFilePath));
            }
            CheckSetting(settings);
            Justin.Stock.Controls.Entities.Constants.Setting = settings;
            RequestFactory.ServiceProvider = settings.MonitorSite;
        }
        private void Init()
        {
            deskStock = new DeskStockCtrl();
            DeskStockCtrl.DisplaySumProfitAndWarnMsgInContainerEvent += ShowTotal;
            RegisterHotKey();
            deskStock.AddDisplayHandler();
            deskStock.AddWarnHandler();
            //StockService.Start();
        }
        public override System.Windows.Forms.IWin32Window Window
        {
            get
            {
                return deskStock;
            }
        }
        private void ShowTotal(string message)
        {
            this.Caption = message;
        }

        private static void CheckSetting(JSettings setting)
        {
            if (string.IsNullOrEmpty(setting.DBPath))
            {
                setting.DBPath = Justin.Stock.Controls.Entities.Constants.DefaultDBPath;
            }
            Justin.Stock.Controls.Entities.Constants.ResetDBConnString(setting.DBPath);

            if (string.IsNullOrEmpty(setting.DeskDisplayFormat))
            {
                setting.DeskDisplayFormat = Justin.Stock.Controls.Entities.Constants.DefaultDeskDisplayFormat;
            }
            StockService.CheckTime = setting.CheckTime;
            if (setting.StartPosition == null)
            {
                setting.StartPosition = new StartPosition()
                {
                    Top = 311,
                    Left = 1005,
                    Width = 334,
                    Height = 121
                };
            }
        }

        public static DisplayHandler Display;


        #region 快捷键
        HotkeyHelper hotkeyHelper;
        int showWindowKey;
        int showWarnKey;
        int stopOrStartServicekey;

        private void RegisterHotKey()
        {
            hotkeyHelper = new HotkeyHelper(this.deskStock.Handle);
            showWindowKey = hotkeyHelper.RegisterHotkey(Keys.Oemtilde, HotkeyHelper.KeyFlags.MOD_CONTROL);
            stopOrStartServicekey = hotkeyHelper.RegisterHotkey(Keys.Oemtilde, HotkeyHelper.KeyFlags.MOD_WIN);
            showWarnKey = hotkeyHelper.RegisterHotkey(Keys.D1, HotkeyHelper.KeyFlags.MOD_CONTROL);
            hotkeyHelper.OnHotkey += new HotkeyEventHandler(OnHotkey);
        }
        private void OnHotkey(int hotkeyID)
        {
            System.Windows.Forms.MessageBox.Show(hotkeyID.ToString());
            if (hotkeyID == showWindowKey)
            {
                if (Display != null)
                {
                    Display(false, false);
                }
                //if (IsShow)
                //{
                //    this.Hide();
                //}
                //else
                //{
                //    this.Show();
                //}
            }
            else if (hotkeyID == showWarnKey)
            {
                Justin.Stock.Controls.Entities.Constants.Setting.ShowWarn = !Justin.Stock.Controls.Entities.Constants.Setting.ShowWarn;
            }
            else if (hotkeyID == stopOrStartServicekey)
            {
                if (StockService.IsRunning)
                {
                    StockService.Stop();
                    Display(false, true);
                }
                else
                {
                    StockService.ReStart();
                    Display(true, true);
                }
            }
        }

        #endregion

    }
}

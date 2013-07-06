using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.Utility;
using Justin.Stock.Controls.Entities;
using Justin.Stock.DAL;
using Justin.Stock.Service.Models;

namespace Justin.Stock.Controls
{
    public partial class SystemSettingCtrl : UserControl
    {
        public SystemSettingCtrl()
        {
            InitializeComponent();
        }

        private void SystemSetting_Load(object sender, EventArgs e)
        {
            RefreshSetting();
        }
        public void RefreshSetting()
        {
            txtBalance.Text = Constants.Setting.Balance.ToString();
            txtDesktopDisplayFormat.Text = Constants.Setting.DeskDisplayFormat;
            toolTip1.SetToolTip(btnDeskDisplayFormat, Constants.DefaultDeskDisplayFormatTips);
            checkBoxShowWarn.Checked = Constants.Setting.ShowWarn;
            checkBoxCheckTime.Checked = Constants.Setting.CheckTime;
            checkBoxAutoStart.Checked = AutoStart.Current.EnabledThroughRegistry;
            txtDBPath.Text = Constants.Setting.DBPath;

        }
        private void btnBalance_Click(object sender, EventArgs e)
        {
            Constants.Setting.Balance = decimal.Parse(txtBalance.Text.Trim());
        }

        private void btnDeskDisplayFormat_Click(object sender, EventArgs e)
        {
            string deskDisplayFormat = txtDesktopDisplayFormat.Text.Trim();
            if (!string.IsNullOrEmpty(deskDisplayFormat))
            {
                Constants.Setting.DeskDisplayFormat = deskDisplayFormat;
            }
        }

        private void btnShowWarn_Click(object sender, EventArgs e)
        {
            Constants.Setting.ShowWarn = checkBoxShowWarn.Checked;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SaveSetting();
            RefreshSetting();
        }

        private void btnCheckTime_Click(object sender, EventArgs e)
        {
            StockService.CheckTime = Constants.Setting.CheckTime = checkBoxCheckTime.Checked;
        }

        private void btnDBPath_Click(object sender, EventArgs e)
        {
            string dbPath = txtDBPath.Text;
            if (dbPath.IndexOf(':') == -1)
            {
                dbPath = Constants.DefaultDBPath;
            }
            if (!File.Exists(dbPath))
            {
                MessageBox.Show(string.Format("文件{0}不存在", dbPath));
            }
            if (dbPath != Constants.Setting.DBPath)
            {
                Constants.Setting.DBPath = dbPath;
                Constants.ResetDBConnString(Constants.Setting.DBPath);
            }
        }

        private void btnAutoStart_Click(object sender, EventArgs e)
        {
            AutoStart.Current.EnabledThroughRegistry = this.checkBoxAutoStart.Checked;
        }

        private void SaveSetting()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string xmlData = SerializeHelper.XmlSerialize<JSettings>(Constants.Setting);
            xmlDoc.LoadXml(xmlData);
            if (Constants.Setting.DBPath == Constants.DefaultDBPath)
            {
                Constants.Setting.DBPath = Path.GetFileName(Constants.DefaultDBPath);
            }
            xmlDoc.Save(Constants.SettingFilePath);
        }
    }
}

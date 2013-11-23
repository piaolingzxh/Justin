using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.FrameWork.WinForm.Helper;
using Justin.Log;
using Justin.Stock.Controls.Entities;
using Justin.Stock.DAL;
using Justin.Stock.Service.Models;

namespace Justin.Stock
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点 。 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Constants.SettingFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), string.Format(@"JStock\{0}", Constants.SettingFileName));
            try
            {
                if (File.Exists(Constants.SettingFilePath))
                {
                    string settingData = File.ReadAllText(Constants.SettingFilePath, Encoding.UTF8);
                    JSettings settings = SerializeHelper.XmlDeserialize<JSettings>(settingData);
                    if (settings == null)
                    {
                        MessageBox.Show(string.Format("加载配置{0}信息出错", Constants.SettingFilePath));
                    }
                    CheckSetting(settings);
                    //if (settings.DBPath.IndexOf("\\") == -1)
                    //{
                    //    settings.DBPath = Path.Combine(Application.StartupPath, settings.DBPath);
                    //}
                    if (File.Exists(settings.DBPath))
                    {
                        Constants.ResetDBConnString(settings.DBPath);
                    }
                    Constants.Setting = settings;
                    RequestFactory.ServiceProvider = settings.MonitorSite;
                    var form = new DeskStocks(Constants.SettingFilePath);
                    JLog.Write(LogMode.Info, "Start");

                    Application.Run(form);
                }
                else
                {
                    JSettings settings = new JSettings();
                    CheckSetting(settings);

                    XmlDocument xmlDoc = new XmlDocument();
                    string xmlData = SerializeHelper.XmlSerialize<JSettings>(settings);
                    xmlDoc.LoadXml(xmlData);
                    xmlDoc.Save(Constants.SettingFilePath);
                    MessageBox.Show(string.Format("配置信息{0}不存在,已新建默认配置", Constants.SettingFilePath));
                }
            }
            catch (Exception ex)
            {
                JLog.Write(LogMode.Error, ex);
            }


        }

        public static void CheckSetting(JSettings setting)
        {
            if (string.IsNullOrEmpty(setting.DBPath))
            {
                setting.DBPath = Constants.DefaultDBPath;
            }

            if (string.IsNullOrEmpty(setting.DeskDisplayFormat))
            {
                setting.DeskDisplayFormat = Constants.DefaultDeskDisplayFormat;
            }
            //setting.Balance = 0;
            //setting.CheckTime = false;
            //setting.ShowWarn = true;
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

    }
}

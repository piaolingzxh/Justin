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
using Justin.FrameWork.Services;
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
            MessageSvc.Default.MessageReceived += RegisterLogService;

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
                    MessageSvc.Default.Write(MessageLevel.Debug, "Start");

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
                    MessageSvc.Default.Write(MessageLevel.Warn, "配置信息{0}不存在,已新建默认配置,请重新打开程序！", Constants.SettingFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageSvc.Default.Write(MessageLevel.Error, ex);
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
            StockService.CheckTime = setting.CheckTime;
            if (setting.StartPosition == null || (setting.StartPosition.Top == 0 && setting.StartPosition.Left == 0 && setting.StartPosition.Width == 0 && setting.StartPosition.Height == 0))
            {
                setting.StartPosition = new StartPosition()
                {
                    Top = 662,
                    Left = 1146,
                    Width = 334,
                    Height = 121
                };
            }
        }



        private static void RegisterLogService(object sender, MessageEventArgs e)
        {
            LogMode logMode = (LogMode)Enum.Parse(typeof(LogMode), Enum.GetName(typeof(MessageLevel), e.Level), true);
            JLog.Default.Write(logMode, e.Message);
        }

    }
}

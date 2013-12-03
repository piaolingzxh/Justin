using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.FrameWork.Extensions;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;

namespace Justin.BI.DBLibrary.Utility
{
    public class JTools
    {
        public static string GetFileName(string tableName, FileType fileType)
        {
            //todo:
            //string ConfigFileNameFormat = Constants.ConfigFileFolder + "{0}." + FileType.TableConfig.GetDefaultFileExtension();
            //string OuputSQLFileNameFormat = Constants.OuputSQLFileFolder + "{0}." + FileType.SQL.GetDefaultFileExtension();
            //string fileName = string.Format(fileType == FileType.TableConfig ? ConfigFileNameFormat : OuputSQLFileNameFormat, tableName);
            //return fileName;
            return "";
        }
        public static void SetToolTips(Control ctrl, ToolTip tips)
        {
            foreach (Control item in ctrl.Controls)
            {
                if (item is Button)
                {
                    Button btn = item as Button;
                    if (btn.Tag != null)
                    {
                        tips.SetToolTip(btn, btn.Tag.ToJString());
                    }
                }
                else
                {
                    SetToolTips(item, tips);
                }
            }
        }
        public static JTable ReadTableSetting(string tableName)
        {
            string fileName = GetFileName(tableName, FileType.TableConfig);
            string settingContent = File.ReadAllText(fileName, Encoding.UTF8);
            JTable table = SerializeHelper.XmlDeserialize<JTable>(settingContent);
            return table;
        }

        public static JTable ReadTableSettingByFile(string tableSettingFileName)
        {
            string settingContent = File.ReadAllText(tableSettingFileName, Encoding.UTF8);
            JTable table = SerializeHelper.XmlDeserialize<JTable>(settingContent);
            return table;
        }



    }
}

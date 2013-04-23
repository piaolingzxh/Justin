using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Extensions;
using Justin.Controls.TestDataGenerator.Entities;

namespace Justin.Controls.TestDataGenerator.Utility
{
    public class JTools
    {
        
        //public static JTable ReadTableSetting(string tableName)
        //{
        //    string fileName = GetFileName(tableName, FileType.TableConfig);
        //    string settingContent = File.ReadAllText(fileName, Encoding.UTF8);
        //    JTable table = SerializeHelper.XmlDeserialize<JTable>(settingContent);
        //    return table;
        //}

        //public static JTable ReadTableSettingByFile(string tableSettingFileName)
        //{
        //    string settingContent = File.ReadAllText(tableSettingFileName, Encoding.UTF8);
        //    JTable table = SerializeHelper.XmlDeserialize<JTable>(settingContent);
        //    return table;
        //}



    }
}

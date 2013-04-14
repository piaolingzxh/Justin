using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Justin.FrameWork.WinForm.Utility;

namespace Justin.Core
{
    public static class JFormat
    {
        public static void FormatFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                return;

            string extension = Path.GetExtension(fileName);
            if (extension == ".xml")
            {
                FormatFile(fileName);
            }
            else
            {
                ProcessBackground pbg = new SyncProcessBackground(Path.Combine(Application.StartupPath, "AStyle.exe"));
                string args = string.Format("--style=allman -N -Y {0}", fileName);
                pbg.ExecuteCommand(args);
            }

        }

        public static void FormatXmlFile(string xmlFileName)
        {
            MemoryStream stream = new MemoryStream(0x400);
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            XmlDocument document = new XmlDocument();
            writer.Formatting = Formatting.Indented;
            document.Load(xmlFileName);
            document.WriteTo(writer);
            writer.Flush();
            writer.Close();
            string formatedContent = Encoding.GetEncoding("utf-8").GetString(stream.ToArray());
            stream.Close();
            File.WriteAllText(xmlFileName, formatedContent, Encoding.UTF8);
        }
    }
}

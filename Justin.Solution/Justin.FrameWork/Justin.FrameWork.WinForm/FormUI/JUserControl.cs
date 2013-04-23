using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.FrameWork.WinForm.FormUI
{
    public delegate void FileChangedEventHandler(string fileName);
    public class JUserControl : UserControl
    {
        public Action<string> LoadAction;
        public Action<string> SaveAction;
        public FileChangedEventHandler FileChanged;

        public virtual string FileName { get; set; }

        public virtual void SaveFile(string fileName)
        {
            string tempFileName = "";
            if (string.IsNullOrEmpty(fileName))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    tempFileName = saveFileDialog1.FileName;
                }
            }
            else
            {
                tempFileName = fileName;
            }
            if (!string.IsNullOrEmpty(tempFileName))
            {
                if (SaveAction != null)
                    SaveAction(tempFileName);
                this.FileName = tempFileName;
            }
            OnFileChanged(tempFileName);
        }
        public virtual void LoadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                this.ShowMessage(string.Format("文件{0}不存在", fileName));
                return;
            }
            if (LoadAction != null)
            {
                LoadAction(fileName);
            }
            this.FileName = fileName;
            OnFileChanged(fileName);
        }


        private void OnFileChanged(string fileName)
        {
            if (FileChanged != null)
            {
                FileChanged(fileName);
            }
        }


        public virtual string ConnStr { get; set; }
        public void CheckConnStringAssigned(Action action)
        {
            if (!string.IsNullOrEmpty(ConnStr))
            {
                action();
            }
            else
            {
                this.ShowMessage("请选择数据源。");
            }
        }
    }
}

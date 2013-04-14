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
        public FileChangedEventHandler FileChanged;
        private string fileName = "";
        public virtual string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    if (FileChanged != null)
                    {
                        FileChanged(this.fileName);
                    }
                }
            }
        }

        public virtual void SaveFile(string fileName)
        {
            if (File.Exists(this.FileName))
            {
                File.Delete(FileName);
            }
        }
        public virtual void LoadFile(string fileName) { }

    }
}

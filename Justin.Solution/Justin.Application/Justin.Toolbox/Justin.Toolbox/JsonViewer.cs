using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Core;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Toolbox
{
    public partial class JsonViewer : JForm, IFile
    {
        public JsonViewer()
        {
            InitializeComponent();
            this.jsonViewCtrl1.FileChanged += this.OnFileChanged;
            this.LoadAction = (fileName) => { this.jsonViewCtrl1.LoadFile(fileName); };
            this.SaveAction = (fileName) => { this.jsonViewCtrl1.SaveFile(fileName); };
        
        }
        public JsonViewer(string[] args)
            : this()
        {
            if (args != null)
            {
                this.FileName = args[0];
            }
        }
        private void JsonViewer_Load(object sender, EventArgs e)
        {
            this.LoadFile(this.FileName);
        }
        #region 继承


        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.FileName);
        }

        public string Extension
        {
            get { return this.jsonViewCtrl1.Extension; }
        }
        protected override string FileName
        {
            get
            {
                return jsonViewCtrl1.FileName;
            }
            set
            {
                jsonViewCtrl1.FileName = value;
            }
        }

        #endregion


    }
}

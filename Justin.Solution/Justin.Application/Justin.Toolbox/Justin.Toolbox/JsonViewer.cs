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
    public partial class JsonViewer : JForm,IFile
    {
        public JsonViewer()
        {
            InitializeComponent();
        }
        public JsonViewer(string[] args)
            : this()
        {
            if (args != null)
            {
                this.FileName = args[0];
            }
        }

        #region 继承


        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.FileName);
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
                base.FileName = value;

            }
        }

        public override void LoadFile(string fileName)
        {
            base.LoadFile(fileName);
            this.jsonViewCtrl1.LoadFile(fileName);
        }
        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            this.jsonViewCtrl1.SaveFile(fileName);
        }
        #endregion
    }
}

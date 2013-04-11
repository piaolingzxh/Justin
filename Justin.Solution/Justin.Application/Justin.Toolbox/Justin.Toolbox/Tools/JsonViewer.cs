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

namespace Justin.Toolbox.Tools
{
    public partial class JsonViewer : JDockForm
    {
        public JsonViewer()
            : this(null)
        {
        }
        public JsonViewer(string[] args)
        {
            InitializeComponent();
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

        protected override bool IsFile { get { return true; } }

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
        #endregion
    }
}

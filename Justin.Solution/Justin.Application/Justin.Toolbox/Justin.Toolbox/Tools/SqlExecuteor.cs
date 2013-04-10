using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.BI.DBLibrary.TestDataGenerate;
using Justin.FrameWork.Helper;
using Justin.Log;
using Justin.FrameWork.Settings;
using Justin.BI.DBLibrary.Utility;
using Justin.Core;

namespace Justin.Toolbox.Tools
{
    public delegate void AsyncDelegate();
    public partial class SqlExecuteor : JDBDcokForm
    {
        public SqlExecuteor(string fileName, string connStr)
        {
            InitializeComponent();
            this.sqlExecuterCtrl1.FileName = fileName;
            this.ConnStr = connStr;
            if (!string.IsNullOrEmpty(fileName))
            {
                this.Text = Path.GetFileName(fileName);
            }
        }
        protected override string ConnStr
        {
            get
            {
                return this.sqlExecuterCtrl1.ConnStr;
            }
            set
            {
                this.sqlExecuterCtrl1.ConnStr = value;
                base.ConnStr = value;
            }
        }
        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), this.sqlExecuterCtrl1.FileName, this.sqlExecuterCtrl1.ConnStr);
        }


    }
}

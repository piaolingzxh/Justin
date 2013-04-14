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
using Justin.BI.DBLibrary.Utility;
using Justin.Core;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.Models;
using Justin.Log;

namespace Justin.Toolbox
{
    public delegate void AsyncDelegate();
    public partial class SqlExecuteor : JForm,IDB,IFile
    {
        public SqlExecuteor()
        {
            InitializeComponent();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="args" type="string[]">
        ///     <para>
        ///           0:fileName
        ///           1:ConnStr
        ///     </para>
        /// </param>
        public SqlExecuteor(string[] args):this()
        {
            if (args != null)
            {
                this.FileName = args[0]; ;
                this.ConnStr = args.Length > 1 ? args[1] : "";
            }
        }

        #region 继承
        public override string ConnStr
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
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), this.FileName, this.ConnStr);
        }
      

        protected override string FileName
        {
            get
            {
                return sqlExecuterCtrl1.FileName;
            }
            set
            {
                sqlExecuterCtrl1.FileName = value;
                base.FileName = value;

            }
        }

        public  override void LoadFile(string fileName)
        {
            this.sqlExecuterCtrl1.ReadFile();
            base.LoadFile(fileName);
        }
        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            this.sqlExecuterCtrl1.SaveFile(fileName);
        }

        #endregion

 
    }
}

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
using Justin.Core;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Toolbox
{
    public delegate void AsyncDelegate();
    public partial class SqlExecuteor : JForm, IDB, IFile
    {
        public SqlExecuteor()
        {
            InitializeComponent();
            this.sqlExecuterCtrl1.FileChanged += this.OnFileChanged;
            this.LoadAction = (fileName) =>
            {
                this.sqlExecuterCtrl1.LoadFile(fileName);
                this.ShowInStatus(this.ConnStr);
            };
            this.SaveAction = (fileName) => { this.sqlExecuterCtrl1.SaveFile(fileName, this.Extension); };
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
        public SqlExecuteor(string[] args)
            : this()
        {
            if (args != null)
            {
                this.FileName = args[0]; ;
                this.ConnStr = args.Length > 1 ? args[1] : "";
            }
        }

        private void SqlExecuteor_Load(object sender, EventArgs e)
        {
            //this.LoadFile(this.FileName);
            this.ShowInStatus(this.ConnStr);
        }

        #region 继承
        public string Extension
        {
            get { return this.sqlExecuterCtrl1.Extension; }
        }

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), this.FileName, this.ConnStr);
        }

        public override string ConnStr
        {
            get
            {
                return this.sqlExecuterCtrl1.ConnStr;
            }
            set
            {
                this.sqlExecuterCtrl1.ConnStr = value;
            }
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
            }
        }

        #endregion
    }
}

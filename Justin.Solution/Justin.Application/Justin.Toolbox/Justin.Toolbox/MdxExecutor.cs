using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using Justin.Core;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.Models;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Toolbox
{
    public partial class MdxExecutor : JForm, IDB, IFile
    {
        public MdxExecutor()
        {
            InitializeComponent();
            this.mdxExecuterCtrl1.FileChanged += this.OnFileChanged;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="args" type="string[]">
        ///     <para>
        ///           0:fileName
        ///           1:Connection
        ///     </para>
        /// </param>
        public MdxExecutor(string[] args)
            : this()
        {
            if (args != null)
            {
                this.FileName = args[0];
                if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
                    this.ConnStr = args[1];

            }
        }

        #region 继承

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), this.FileName, this.ConnStr);
        }

        protected string ConnStr
        {
            get
            {
                return this.mdxExecuterCtrl1.ConnStr;
            }
            set
            {
                this.mdxExecuterCtrl1.ConnStr = value;
                base.ConnStr = value;
            }
        }

        protected override string FileName
        {
            get
            {
                return mdxExecuterCtrl1.FileName;
            }
            set
            {
                mdxExecuterCtrl1.FileName = value;
            }
        }
        public override void LoadFile(string fileName)
        {
            base.LoadFile(fileName);
            this.mdxExecuterCtrl1.LoadFile(fileName);
        }
        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            this.mdxExecuterCtrl1.SaveFile(fileName);
        }
        #endregion

    }
}

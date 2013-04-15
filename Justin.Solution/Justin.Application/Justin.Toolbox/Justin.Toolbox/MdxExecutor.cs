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
    public partial class MdxExecutor : JForm, IFile
    {
        public MdxExecutor()
        {
            InitializeComponent();
            this.mdxExecuterCtrl1.FileChanged += this.OnFileChanged;
            this.LoadAction = (fileName) => { this.mdxExecuterCtrl1.LoadFile(fileName); };
            this.SaveAction = (fileName) => { this.mdxExecuterCtrl1.SaveFile(fileName); };

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

        private void MdxExecutor_Load(object sender, EventArgs e)
        {
            this.LoadFile(this.FileName);
        }

        #region 继承

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), this.FileName, this.ConnStr);
        }

        public override string ConnStr
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

        #endregion



    }
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using Justin.Controls.CodeCompiler;
using Justin.Core;
using Justin.FrameWork.Settings;
using Justin.FrameWork.Utility;
using Justin.FrameWork.WinForm.Models;
using Microsoft.CSharp;

namespace Justin.Toolbox
{
    public partial class JCodeCompiler : JForm, IFile, IFormat
    {
        public JCodeCompiler()
        {
            InitializeComponent();
            JSetting.SetUseAppSetting("JDKPath", "JDKPath");
            //CodeComplierCtrl.JDKPath = ConfigurationManager.AppSettings["JDKPath"];
            this.codeComplierCtrl1.FileChanged = (fileName) => { this.FileName = fileName; };
            this.LoadAction = (fileName) => { this.codeComplierCtrl1.LoadFile(fileName); };
            this.SaveAction = (fileName) => { this.codeComplierCtrl1.SaveFile(fileName, this.Extension); };
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="args" type="string[]">
        ///     <para>
        ///          0:fileName
        ///     </para>
        /// </param>
        public JCodeCompiler(string[] args)
            : this()
        {
            if (args != null)
            {
                this.FileName = args[0];
            }
        }

        private void JCodeCompiler_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FileName))
                this.LoadFile(this.FileName);
        }

        #region override


        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.FileName);
        }
        public string Extension
        {
            get { return this.codeComplierCtrl1.Extension; }
        }

        protected override string FileName
        {
            get
            {
                return codeComplierCtrl1.FileName;
            }
            set
            {
                codeComplierCtrl1.FileName = value;
            }
        }

        #endregion
    }
}

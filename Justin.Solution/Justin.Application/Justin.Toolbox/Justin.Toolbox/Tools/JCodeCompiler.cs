using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
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
using Justin.FrameWork.Utility;
using Microsoft.CSharp;
using Justin.FrameWork.Settings;
using Justin.Core;

namespace Justin.Toolbox.Tools
{
    public partial class JCodeCompiler : JForm
    {
        public JCodeCompiler()
        {
            InitializeComponent();
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
        }
        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.FileName);
        }

        protected override bool IsFile { get { return true; } }

        protected override string FileName
        {
            get
            {
                return codeComplierCtrl1.FileName;
            }
            set
            {
                codeComplierCtrl1.FileName = value;
                base.FileName = value;
            }
        }

        protected override void Save()
        {
            base.Save();
            this.codeComplierCtrl1.Save();
        }
    }
}

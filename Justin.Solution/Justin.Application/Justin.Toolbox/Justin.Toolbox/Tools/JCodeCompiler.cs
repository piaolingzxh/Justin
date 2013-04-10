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
    public partial class JCodeCompiler : JDockForm
    {
        public JCodeCompiler()
        {
            InitializeComponent();
        }

        private void JCodeCompiler_Load(object sender, EventArgs e)
        {
        }
        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), "");
        }
       
    }
}

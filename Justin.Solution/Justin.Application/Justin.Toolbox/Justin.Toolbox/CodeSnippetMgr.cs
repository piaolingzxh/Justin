using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Controls.CodeSnippet;
using Justin.Core;

namespace Justin.Toolbox
{
    public partial class CodeSnippetMgr : JForm
    {
        public CodeSnippetMgr()
        {
            CodeSnippetCtrl.CodeSnippetFileDirectory = ConfigurationManager.AppSettings["CodeSnippet"];
            InitializeComponent();
        }

        private void CodeSnippetMgr_Load(object sender, EventArgs e)
        {

        }
        protected override string GetPersistString()
        {
            return string.Format("{0}", GetType().ToString());
        }
    }
}

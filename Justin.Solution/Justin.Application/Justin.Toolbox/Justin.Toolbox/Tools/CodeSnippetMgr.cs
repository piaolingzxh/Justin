using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Core;

namespace Justin.Toolbox.Tools
{
    public partial class CodeSnippetMgr : JDockForm
    {
        public CodeSnippetMgr()
        {
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

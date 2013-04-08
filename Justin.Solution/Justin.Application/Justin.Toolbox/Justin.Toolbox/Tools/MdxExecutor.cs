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
using Justin.FrameWork.Settings;
using Microsoft.AnalysisServices.AdomdClient;

namespace Justin.Toolbox.Tools
{
    public partial class MdxExecutor : JDBDcokForm
    {
        public MdxExecutor()
        {
            InitializeComponent();
        }
        public MdxExecutor(string connectionString, string mdx)
            : this()
        {
            if (!string.IsNullOrEmpty(connectionString))
                this.ConnStr = connectionString;
            this.mdxExecuterCtrl1.Mdx = mdx;
        }
        protected override bool NeedChooseDataSource
        {
            get
            {
                return false;
            }
        }
        protected override string ConnStr
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
        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}{0}{3}", Constants.Splitor, GetType().ToString(), this.mdxExecuterCtrl1.ConnStr, this.mdxExecuterCtrl1.Mdx);
        }
    }
}

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
    public partial class JsonViewer : JDockForm
    {
        public JsonViewer()
        {
            InitializeComponent();
        }

        #region 继承


        protected override string GetPersistString()
        {
            return string.Format("{0}", GetType().ToString());
        }

        #endregion
    }
}

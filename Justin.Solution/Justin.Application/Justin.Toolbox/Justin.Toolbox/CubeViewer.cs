using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.Controls.CubeView;
using Justin.Core;
using Justin.FrameWork.Settings;

namespace Justin.Toolbox
{
    public partial class CubeViewer : JForm
    {
        public CubeViewer()
        {
            InitializeComponent();
        }

        private void CubeViewer_Load(object sender, EventArgs e)
        {
            try
            {
                CubeViewCtrl.DefaultConnStr = ConfigurationManager.AppSettings["OLAPConnStr"];
            }
            catch { }
        }
        #region 继承

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.ConnStr);
        }

        public override string ConnStr
        {
            get
            {
                return this.cubeViewCtrl1.ConnStr;
            }
            set
            {
                this.cubeViewCtrl1.ConnStr = value;
                base.ConnStr = value;
            }
        }
        #endregion
    }
}

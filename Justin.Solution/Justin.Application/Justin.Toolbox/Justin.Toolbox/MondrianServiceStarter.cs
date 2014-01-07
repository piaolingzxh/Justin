using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.Core;
using Justin.FrameWork.Settings;

namespace Justin.Toolbox
{
    public partial class MondrianServiceStarter : JForm, IDB
    {
        public MondrianServiceStarter()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MondrianServiceStarter_FormClosing);
        }

        public MondrianServiceStarter(string[] args)
            : this()
        {
            if (args != null)
            {
                this.ConnStr = args[0];
            }
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
                return base.ConnStr;
            }
            set
            {
                this.mondrianServiceCtrl1.ConnStr = value;
            }
        }



        #endregion

        private void MondrianServiceStarter_Load(object sender, EventArgs e)
        {
            this.ShowInStatus(this.ConnStr);
        }

        public void MondrianServiceStarter_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mondrianServiceCtrl1.StopService();
        }
    }
}

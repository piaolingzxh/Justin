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

namespace Justin.Toolbox.Tools
{
    public partial class JEditor : JForm
    {
        public JEditor()
        {
            InitializeComponent();
        }
        public JEditor(string[] args)
            : this()
        {
            if (args.Length > 0)
                this.FileName = args[0];
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
                return jEditorCtrl1.FileName;
            }
            set
            {
                jEditorCtrl1.FileName = value;
            }
        }

        protected override void Save(string fileName)
        {
            base.Save(fileName);
            this.jEditorCtrl1.Save(fileName);
        }

        protected override void ReloadFile()
        {
            base.ReloadFile();
            this.jEditorCtrl1.LoadFile();
        }

        private void JEditor_Load(object sender, EventArgs e)
        {
            this.jEditorCtrl1.FileChanged = (fileName) => { this.FileName = fileName; };
        }
    }
}

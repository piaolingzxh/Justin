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
using Justin.FrameWork.WinForm.Models;

namespace Justin.Toolbox
{
    public partial class JEditor : JForm,IFile
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

        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            this.jEditorCtrl1.SaveFile(fileName);
        }

        public override void LoadFile(string fileName)
        {
            base.LoadFile(fileName);
            this.jEditorCtrl1.LoadFile(fileName);
        }

        private void JEditor_Load(object sender, EventArgs e)
        {
            this.jEditorCtrl1.FileChanged = (fileName) => { this.FileName = fileName; };
        }
    }
}

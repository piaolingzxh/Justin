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
    public partial class JEditor : JForm, IFile, IFormat
    {
        public JEditor()
        {
            InitializeComponent();
            this.jEditorCtrl1.FileChanged += this.OnFileChanged;
            this.LoadAction = (fileName) => { this.jEditorCtrl1.LoadFile(fileName); };
            this.SaveAction = (fileName) => { this.jEditorCtrl1.SaveFile(fileName); };
     
        }
        public JEditor(string[] args)
            : this()
        {
            if (args.Length > 0)
                this.FileName = args[0];
        }


        #region override

        protected override string GetPersistString()
        {
            return string.Format("{1}{0}{2}", Constants.Splitor, GetType().ToString(), this.FileName);
        }
        public string Extension
        {
            get { return this.jEditorCtrl1.Extension; }
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

        #endregion

        private void JEditor_Load(object sender, EventArgs e)
        {
            this.LoadFile(this.FileName);
        }
    }
}

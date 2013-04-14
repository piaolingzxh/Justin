using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.WinForm.FormUI;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.WinForm.Models;
namespace Justin.Controls.TextEditor
{
    public partial class JEditorCtrl : JUserControl,IFile
    {
        public JEditorCtrl()
        {
            InitializeComponent();
        }

        public override string FileName
        {
            get
            {
                return base.FileName;
            }
            set
            {
                base.FileName = value;
                richTextBox1.Text = base.FileName;
                this.LoadFile(this.FileName);
            }
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.FileName = openFileDialog1.FileName;
            }
        }

        public override void SaveFile(string fileName)
        {
            base.SaveFile(fileName);
            textEditorControl1.SaveFile(fileName);
        }
        public override void LoadFile(string fileName)
        {
            base.LoadFile(fileName);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                textEditorControl1.LoadFile(fileName);
        }

    }
}

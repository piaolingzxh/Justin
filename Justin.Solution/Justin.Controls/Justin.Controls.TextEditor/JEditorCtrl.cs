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
            this.LoadAction = (fileName) =>
            {
                textEditorControl1.LoadFile(fileName);
            };
            this.SaveAction = (fileName) =>
            {
                textEditorControl1.SaveFile(fileName);
            };
        }

        
         
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.FileName = openFileDialog1.FileName;
            }
        }


        public override string FileName
        {
            get
            {
                return base.FileName;
            }
            set
            {
                richTextBox1.Text = base.FileName;
            }
        }

    }
}

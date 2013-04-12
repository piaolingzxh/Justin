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
namespace Justin.Controls.TextEditor
{
    public partial class JEditorCtrl : JUserControl
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
                this.LoadFile();
            }
        }
        public override void Save(string fileName)
        {
            base.Save(fileName);
            File.AppendAllText(fileName, textEditorControl1.Text);
        }
        public override void LoadFile()
        {
            base.LoadFile();
            if (!string.IsNullOrEmpty(this.FileName) && File.Exists(this.FileName))
                textEditorControl1.LoadFile(this.FileName);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.FileName = openFileDialog1.FileName;
            }
        }
    }
}

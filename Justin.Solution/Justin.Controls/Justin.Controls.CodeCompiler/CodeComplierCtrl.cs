using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Utility;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using ICSharpCode.TextEditor.Document;
using System.Threading;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Models;

namespace Justin.Controls.CodeCompiler
{
    public delegate void AppendTextCallback(string text);
    public partial class CodeComplierCtrl : JUserControl, IFile
    {
        public CodeComplierCtrl()
        {
            InitializeComponent();
            this.LoadAction = (fileName) => { this.txtCode.LoadFile(fileName); InitComplier(fileName); };
            this.SaveAction = (fileName) => { this.txtCode.SaveFile(fileName); InitComplier(fileName); };
        }

        CodeComplierBase complier;

        #region 按钮事件

        private void btnCompiler_Click(object sender, EventArgs e)
        {
            this.SaveFile(FileName);
            if (!string.IsNullOrEmpty(FileName))
            {
                complier.SourceFileName = FileName;
                complier.Complier();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.SaveFile(FileName);
            if (!string.IsNullOrEmpty(FileName))
            {
                complier.SourceFileName = FileName;
                complier.Run();
            }
        }

        private void btnShowILCode_Click(object sender, EventArgs e)
        {
            this.SaveFile(FileName);
            if (!string.IsNullOrEmpty(FileName))
            {
                complier.SourceFileName = FileName;
                string ilFileName = complier.IL();
                if (File.Exists(ilFileName))
                {
                    var sr = new StreamReader(ilFileName);
                    string outfile = sr.ReadToEnd();
                    sr.Close();
                    txtMSILCode.SetText(outfile);
                }
            }

        }

        #endregion

        public void ShowMsg(string msg)
        {
            this.ShowMessage(msg);
        }


        private void InitComplier(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                complier = null;
                if (fileName.EndsWith("cs", StringComparison.CurrentCultureIgnoreCase))
                {
                    complier = new NetCodeComplier(NetDialect.CSharp);
                }
                else if (fileName.EndsWith("vb", StringComparison.CurrentCultureIgnoreCase))
                {
                    complier = new NetCodeComplier(NetDialect.VB);
                }
                else if (fileName.EndsWith("java", StringComparison.CurrentCultureIgnoreCase))
                {
                    complier = new JavaCodeComplier();
                }
                else
                {
                    throw new NotSupportedException("不支持该语言");
                }
                if (complier != null)
                    complier.MsgReceivedEvent = this.ShowMsg;
            }

        }
        private void ExpandIlDasm(string file)
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();

            var objStream = objAssembly.GetManifestResourceStream(string.Format("{0}.ildasm.exe", this.GetType().Namespace));
            var ildasmResource = new byte[objStream.Length];
            objStream.Read(ildasmResource, 0, (int)objStream.Length);
            var objFileStream = new FileStream(file, FileMode.Create);
            objFileStream.Write(ildasmResource, 0, (int)objStream.Length);
            objFileStream.Close();
        }

        private void CodeComplierCtrl_Load(object sender, EventArgs e)
        {
            txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtCode.Encoding = Encoding.GetEncoding("GB2312");

            txtMSILCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtMSILCode.Encoding = Encoding.GetEncoding("GB2312");

            saveFileDialog1.Filter = "cs 文件(*.cs)|*.cs|vb 文件(*.vb)|*.vb|java 文件(*.java)|*.java|所有文件(*.*)|*.*";

        }

        public static String JDKPath
        {
            get
            {
                return JavaCodeComplier.JDKPath;
            }
            set
            {
                JavaCodeComplier.JDKPath = value;
            }
        }
        #region 示例代码

        private void insertJavaTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCode.SetText(@"public class Program {
	public static void main(String[] args) {
		System.out.println(""hello word"");
        }
}");
        }

        private void insertCSharpTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCode.SetText(@"using System;
namespace ConsoleApplication1
{
    class Program
    {
        public static void Main()
        {
            int i = 3 + 5;
            Console.WriteLine(i.ToString());
        }
    }
}");

        }

        private void insertVBTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCode.SetText(@"Module Module1
    Sub Main()
        Console.WriteLine(""hello VB!"")
    End Sub
End Module");

        }

        #endregion

        #region override

        public string Extension
        {
            get { return ".cs,.vb,.java"; }
        }
        #endregion

    }


}

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
using Justin.Log;
using ICSharpCode.TextEditor.Document;
using System.Threading;
using Justin.FrameWork.WinForm.FormUI;

namespace Justin.Controls.CodeCompiler
{
    public delegate void AppendTextCallback(string text);
    public partial class CodeComplierCtrl : JUserControl
    {
        public CodeComplierCtrl()
        {
            InitializeComponent();
        }

        CodeComplierBase jcc;

        private void btnCompiler_Click(object sender, EventArgs e)
        {
            SetFilePath();
            if (!string.IsNullOrEmpty(FileName))
            {
                jcc.SourceFileName = FileName;
                jcc.Complier();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SetFilePath();
            if (!string.IsNullOrEmpty(FileName))
            {
                jcc.SourceFileName = FileName;
                jcc.Run();
            }
        }

        private void btnShowILCode_Click(object sender, EventArgs e)
        {
            SetFilePath();
            if (!string.IsNullOrEmpty(FileName))
            {
                jcc.SourceFileName = FileName;
                string ilFileName = jcc.IL();
                if (File.Exists(ilFileName))
                {
                    var sr = new StreamReader(ilFileName);
                    string outfile = sr.ReadToEnd();
                    sr.Close();
                    txtMSILCode.SetText(outfile);
                }
            }

            //test();

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

            JavaCodeComplier.JDKPath = @"C:\Programs\Java\jdk1.6.0_24\bin";
            //jcc = new NetCodeComplier(NetDialect.CSharp);
            jcc = new JavaCodeComplier();
            jcc.MsgReceivedEvent += this.ShowMsg;
            txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtCode.Encoding = Encoding.GetEncoding("GB2312");

            txtMSILCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtMSILCode.Encoding = Encoding.GetEncoding("GB2312");

            saveFileDialog1.Filter = "cs 文件(*.cs)|*.cs|vb 文件(*.vb)|*.vb|java 文件(*.java)|*.java|所有文件(*.*)|*.*";

            LoadFile();
        }
        public void ShowMsg(string msg)
        {
            this.ShowMessage(msg);
        }

        private void SetFilePath()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileName = Path.Combine(Path.GetDirectoryName(saveFileDialog1.FileName), Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + Path.GetExtension(saveFileDialog1.FileName).ToLower());
                }

            }
            if (!string.IsNullOrEmpty(FileName))
            {
                this.Save(FileName);
                InitComplier();
            }
        }

        private void InitComplier()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                jcc = null;
                if (FileName.EndsWith("cs", StringComparison.CurrentCultureIgnoreCase))
                {
                    jcc = new NetCodeComplier(NetDialect.CSharp);
                }
                else if (FileName.EndsWith("vb", StringComparison.CurrentCultureIgnoreCase))
                {
                    jcc = new NetCodeComplier(NetDialect.VB);
                }
                else if (FileName.EndsWith("java", StringComparison.CurrentCultureIgnoreCase))
                {
                    jcc = new JavaCodeComplier();
                }
                else
                {
                    throw new NotSupportedException("不支持该语言");
                }
                jcc.MsgReceivedEvent = this.ShowMsg;
            }

        }

        private void test()
        {
            var options = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
            var provider = new CSharpCodeProvider(options);

            var parms = new CompilerParameters
            {
                CompilerOptions = @"/lib:C:\Windows\assembly\GAC /target:library",
                GenerateExecutable = true,
                GenerateInMemory = false
            };
            parms.ReferencedAssemblies.AddRange(CodeCompilerFactory.GetReferencedAssemblies(FrameworkVersion.VersionLatest).ToArray());
            //foreach (string item in lbReferences.Items)
            //{
            //    parms.ReferencedAssemblies.Add(item + ".dll");
            //}
            string outFile = Path.GetTempFileName();
            string ilOutFile = Path.GetTempFileName();
            string ildasm = Path.GetTempFileName() + ".exe";
            parms.OutputAssembly = outFile;

            CompilerResults res = provider.CompileAssemblyFromSource(parms, txtCode.Text);
            if (res.Errors.HasErrors)
            {
                try
                {
                    File.Delete(outFile);
                }
                catch
                {
                }
                try
                {
                    File.Delete(ildasm);
                }
                catch
                {
                }
                try
                {
                    File.Delete(ilOutFile);
                }
                catch
                {
                }
                // BindErrors(res);
                return;
            }
            ExpandIlDasm(ildasm);
            var startInfo = new ProcessStartInfo();
            var p = new Process();

            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = "/out:" + ilOutFile + " " + outFile;
            startInfo.FileName = ildasm;
            p.StartInfo = startInfo;

            p.Start();
            p.WaitForExit();

            var sr = new StreamReader(ilOutFile);
            string outfile = sr.ReadToEnd();
            sr.Close();
            txtMSILCode.SetText(outfile);
            try
            {
                File.Delete(outFile);
            }
            catch
            {
            }
            try
            {
                File.Delete(ildasm);
            }
            catch
            {
            }
            try
            {
                File.Delete(ilOutFile);
            }
            catch
            {
            }
        }

        private void btnSaveCodeToFile_Click(object sender, EventArgs e)
        {
            SetFilePath();
        }

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


        #region    override

        public override string FileName
        {
            get
            {
                return base.FileName;
            }
            set
            {
                base.FileName = value;
                this.LoadFile();
            }
        }
        public override void Save(string fileName)
        {
            base.Save(fileName);
            File.AppendAllText(fileName, txtCode.Text);
        }
        public override void LoadFile()
        {
            base.LoadFile();
            if (!string.IsNullOrEmpty(this.FileName) && File.Exists(this.FileName))
                txtCode.LoadFile(this.FileName);
        }

        #endregion

    }


}

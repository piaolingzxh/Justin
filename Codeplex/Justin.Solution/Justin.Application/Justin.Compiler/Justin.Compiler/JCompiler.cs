using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Utility;

namespace Justin.Compiler
{
    public partial class JCompiler : Form
    {
        public JCompiler()
        {
            InitializeComponent();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            ScriptCode code = new ScriptCode();

            code.SourceCode = txtCode.Text;
            code.StartUpList.Add(new StartUpInfo() { ClassName = "ConsoleApplication1.Program", MethordName = "Test", order = 0, MethordParameters = new object[] { this }, Instance = this, });

            CodeCompilerWraper csWrapper = new CSharpCompilerWraper();
            csWrapper.FrameworkVersion = FrameworkVersion.Version20;
            csWrapper.CustomeAssemblies = null;

            csWrapper.Run(code, new CompilerOutputDelegate(HandleCompilerOutput));
        }

        private void btnCompiler_Click(object sender, EventArgs e)
        {
            CodeCompilerWraper csWrapper = new CSharpCompilerWraper();
            csWrapper.FrameworkVersion = FrameworkVersion.Version20;
            csWrapper.CustomeAssemblies = null;

            csWrapper.Compile(txtCode.Text, new CompilerOutputDelegate(HandleCompilerOutput));
        }
        private delegate void AddCompilerOutputLineDelegate(string line);
        private void HandleCompilerOutput(string line)
        {
            txtResult.BeginInvoke(new AddCompilerOutputLineDelegate(this.WriteLine), line);
        }
        public void WriteLine(string msg)
        {
            txtResult.Text += msg + Environment.NewLine;
        }
    }
}

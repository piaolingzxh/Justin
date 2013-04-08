using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace Justin.FrameWork.Utility
{
    public delegate void ReferencedAssembliesChanged();
    public delegate void CompilerOutputDelegate(string outputLine);

    #region CodeCompilerFactory

    public class CodeCompilerFactory
    {

        public static List<string> GetReferencedAssemblies(FrameworkVersion frameworkVersion)
        {

            switch (frameworkVersion)
            {
                case FrameworkVersion.Version11:
                    break;
                case FrameworkVersion.Version20:
                    break;
                case FrameworkVersion.Version30:
                    break;
                case FrameworkVersion.Version35:
                    break;
                case FrameworkVersion.Version40:
                    break;
            }
            List<string> collection = new List<string>();
            //string[] list = new string[] { "MicroSoft.VisualBasic.dll", "System.dll", "System.Data.dll", "System.xml.dll", "System.Windows.Forms.dll"};
            //collection.AddRange(list);
            //collection.Add(Path.GetFileName(typeof(CodeCompilerFactory).Assembly.Location));
            //collection.Add(Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName));
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                collection.Add(asm.Location);
            }


            return collection;
        }

        public static PermissionSet GetDefaultScriptPermissionSet()
        {
            PermissionSet internalDefScriptPermSet = new PermissionSet(PermissionState.None);

            internalDefScriptPermSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            internalDefScriptPermSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));

            return internalDefScriptPermSet;
        }
    }

    #endregion

    #region CodeCompilerWraper

    public abstract class CodeCompilerWraper
    {
        protected abstract CodeDomProvider Provider { get; }
        protected ICodeCompiler codeCompiler
        {
            get
            {
                return Provider.CreateCompiler();
            }
        }

        private CompilerParameters _memoryComplieParameters = null;
        public CompilerParameters MemoryComplieParameters
        {
            get
            {
                if (_memoryComplieParameters == null)
                {
                    _memoryComplieParameters = new CompilerParameters();
                    _memoryComplieParameters.GenerateExecutable = false;
                    _memoryComplieParameters.GenerateInMemory = true;
                    _memoryComplieParameters.IncludeDebugInformation = false;
                    _memoryComplieParameters.TreatWarningsAsErrors = false;
                }
                return _memoryComplieParameters;
            }
        }

        public FrameworkVersion FrameworkVersion { get; set; }

        public List<string> CustomeAssemblies { get; set; }

        public CompilerResults Compile(string code, CompilerOutputDelegate cod)
        {
            try
            {
                MemoryComplieParameters.ReferencedAssemblies.AddRange(CodeCompilerFactory.GetReferencedAssemblies(FrameworkVersion).ToArray());
                if (CustomeAssemblies != null)
                {
                    MemoryComplieParameters.ReferencedAssemblies.AddRange(CustomeAssemblies.ToArray());
                }
                return this.Compile(code, cod, MemoryComplieParameters);
            }
            catch (System.IO.FileNotFoundException exp)
            {
                throw new Exception("Unable to load script assembly (probably a complier error, check debug output)", exp);
            }

        }
        public virtual CompilerResults Compile(string code, CompilerOutputDelegate cod, CompilerParameters compilerParameters)
        {
            CompilerResults results = codeCompiler.CompileAssemblyFromSource(compilerParameters, code);

            if (results.Errors.HasErrors)
            {
                if (cod != null)
                    cod("-- Compilation of script failed");
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError err in results.Errors)
                {
                    if (cod != null)
                        cod(err.ToString());
                    sb.Append(err.ToString()).AppendLine();
                }
            }
            else
            {
                if (cod != null)
                    cod("-- Compilation of script succesfull");
            }

            return results;
        }

        public void Run(ScriptCode code, CompilerOutputDelegate cod)
        {
            this.Run(code, cod, null);
        }
        public void Run(ScriptCode code, CompilerOutputDelegate cod, PermissionSet permissionSet)
        {
            var resultAssembly = this.Compile(code.SourceCode, cod).CompiledAssembly;
            if (resultAssembly != null)
            {
                if (permissionSet != null)
                {
                    permissionSet.PermitOnly();
                }
                //// run script
                foreach (var item in code.StartUpList.OrderBy(row => row.order))
                {
                    if (resultAssembly.GetType(item.ClassName) == null || resultAssembly.GetType(item.ClassName).GetMethod(item.MethordName) == null)
                    {
                        throw new Exception(string.Format("没有找到公共的{0}.{0}", item.ClassName, item.MethordName));
                    }
                    MethodInfo methordInfo = resultAssembly.GetType(item.ClassName).GetMethod(item.MethordName);
                    methordInfo.Invoke(item.Instance, item.MethordParameters);

                }
                if (permissionSet != null)
                {
                    CodeAccessPermission.RevertPermitOnly();
                }
            }
        }
    }

    #endregion

    #region VBCompilerWraper

    public class VBCompilerWraper : CodeCompilerWraper
    {
        public VBCompilerWraper() { }

        private CodeDomProvider _provider;
        protected override CodeDomProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new VBCodeProvider();
                }
                return _provider;
            }
        }

    }

    #endregion

    #region CSharpCompilerWraper

    public class CSharpCompilerWraper : CodeCompilerWraper
    {
        private CodeDomProvider _provider;
        protected override CodeDomProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new CSharpCodeProvider();
                }
                return _provider;
            }
        }

    }

    #endregion

    #region ScriptCode

    public class ScriptCode
    {
        public ScriptCode()
        {
            StartUpList = new List<StartUpInfo>();
        }
        public List<StartUpInfo> StartUpList { get; set; }
        public string SourceCode { get; set; }
    }

    public class StartUpInfo
    {
        public string ClassName { get; set; }
        public object Instance { get; set; }

        public string MethordName { get; set; }
        public object[] MethordParameters { get; set; }

        public int order { get; set; }
    }

    #endregion

    #region ReferencedAssemblyCollection


    public class ReferencedAssemblyCollection : System.Collections.ArrayList
    {
        internal event ReferencedAssembliesChanged ReferencedAssembliesChanged;

        private void FireChangedEvent()
        {
            if (ReferencedAssembliesChanged != null)
            {
                ReferencedAssembliesChanged();
            }
        }

        public override void Clear()
        {
            base.Clear();
            FireChangedEvent();
        }

        public string[] ToStringArray()
        {
            string[] array = new string[this.Count];
            int i = 0;

            foreach (string str in this)
            {
                array[i++] = str;
            }

            return array;
        }


        public override int Add(object value)
        {
            int index = base.Add(value);
            FireChangedEvent();

            return index;
        }

        public override void AddRange(System.Collections.ICollection c)
        {
            base.AddRange(c);
            FireChangedEvent();
        }

        public override void Insert(int index, object value)
        {
            base.Insert(index, value);
            FireChangedEvent();
        }

        public override void InsertRange(int index, System.Collections.ICollection c)
        {
            base.InsertRange(index, c);
            FireChangedEvent();
        }

        public override void Remove(object obj)
        {
            base.Remove(obj);
            FireChangedEvent();
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            FireChangedEvent();
        }

        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            FireChangedEvent();
        }

        public override object this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                FireChangedEvent();
            }
        }
    }

    #endregion

    #region Enum

    public enum FrameworkVersion
    {
        Version11 = 0,
        Version20 = 1,
        Version30 = 2,
        Version35 = 3,
        Version40 = 4,
        VersionLatest = Version40,
    }

    public enum CodeCategory
    {
        CSharp,
        VB,
    }

    #endregion

    //public class JCodeCompiler
    //{
    //    public static CodeCompilerWraper CreateCompiler(CodeCategory category)
    //    {
    //        CodeCompilerWraper wrapper;
    //        switch (category)
    //        {
    //            case CodeCategory.CSharp:
    //                wrapper = new CSharpCompilerWraper();
    //                break;
    //            case CodeCategory.VB:
    //                wrapper = new VBCompilerWraper();
    //                break;
    //            default:
    //                wrapper = new CSharpCompilerWraper();
    //                break;
    //        }
    //        return wrapper;
    //    }
    //}
}

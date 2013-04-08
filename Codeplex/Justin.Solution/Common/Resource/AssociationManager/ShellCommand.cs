using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace AssociationManager
{
    public class ShellCommand : IDisposable
    {
        private readonly string _parentClassId;
        private readonly string _shellRegistryPath;
        private readonly string _shellName;

        internal ShellCommand(string parentclassid, string shellname, string displayname)
        {
            if (string.IsNullOrEmpty(shellname))
                throw new Exception("Name cannot be null or empty!");

            _parentClassId = parentclassid;
            _shellRegistryPath = parentclassid + @"\shell\";
            _shellName = shellname;

            DisplayName = displayname;
        }

        #region IDisposable implementation

        ~ShellCommand()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private bool _disposed = false;
        public void Dispose(bool disposing)
        {
            lock (this)
            {
                if (!_disposed)
                {
                    try
                    {
                        if (disposing)
                        {
                            // Release managed components
                        }

                        // Release unmanaged components
                        // ...
                    }
                    finally
                    {
                        _disposed = true;
                    }
                }
            }
        }

        #endregion IDisposable implementation

        public string ShellName
        {
            get { return _shellName; }
        }

        public string DisplayName
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_shellRegistryPath + _shellName))
                {
                    if (regkey == null)
                        return "";

                    return regkey.GetValue(null) as string;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("DisplayName cannot be null or empty!");

                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_shellRegistryPath + _shellName))
                {
                    //if (string.IsNullOrEmpty(value))
                    //{
                    //    regkey.DeleteValue(null, false);
                    //}
                    //else
                    //{
                        regkey.SetValue(null, value);
                    //}
                }
            }
        }

        public string Command
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_shellRegistryPath + _shellName + @"\command"))
                {
                    if (regkey == null)
                        return "";

                    return regkey.GetValue(null) as string;
                }
            }
            set
            {
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_shellRegistryPath + _shellName + @"\command"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        regkey.DeleteValue(null, false);
                    }
                    else
                    {
                        string executable = value;

                        /*
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            if (executable.EndsWith(".vshost.exe", StringComparison.CurrentCultureIgnoreCase))
                            {
                                executable = executable.Substring(0, executable.Length - 11);
                                executable += ".exe";
                            }
                        }
                        */

                        if (!executable.Contains("%1"))
                        {
                            executable += " \"%1\"";
                        }

                        regkey.SetValue(null, executable);
                    }
                }
            }
        }
    }
}

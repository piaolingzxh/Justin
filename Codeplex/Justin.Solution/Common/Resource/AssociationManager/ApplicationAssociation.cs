using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace AssociationManager
{
    // For information regarding the FileTypeAttributeFlags see this url: http://msdn.microsoft.com/en-us/library/bb762506%28v=VS.85%29.aspx
    [Flags]
    public enum FileTypeAttributeFlags : int
    {
        FTA_None = 0x00000000,
        FTA_Exclude = 0x00000001,
        FTA_Show = 0x00000002,
        FTA_HasExtension = 0x00000004,
        FTA_NoEdit = 0x00000008,
        FTA_NoRemove = 0x00000010,
        FTA_NoNewVerb = 0x00000020,
        FTA_NoEditVerb = 0x00000040,
        FTA_NoRemoveVerb = 0x00000080,
        FTA_NoEditDesc = 0x00000100,
        FTA_NoEditIcon = 0x00000200,
        FTA_NoEditDflt = 0x00000400,
        FTA_NoEditVerbCmd = 0x00000800,
        FTA_NoEditVerbExe = 0x00001000,
        FTA_NoDDE = 0x00002000,
        FTA_NoEditMIME = 0x00008000,
        FTA_OpenIsSafe = 0x00010000,
        FTA_AlwaysUnsafe = 0x00020000,
        FTA_AlwaysShowExt = 0x00040000,
        FTA_NoRecentDocs = 0x00100000
    }

    public class ApplicationAssociation : IDisposable
    {
        private const int S_OK = 0;

        private readonly string _extension;
        private readonly string _classid;
        private readonly FileAssociationManager _associationManager;

        internal ApplicationAssociation(FileAssociationManager associationmanager, string extension, string classid, string description)
        {
            _associationManager = associationmanager;
            _extension = "." + extension.TrimStart('.');
            _classid = classid;

            if (!string.IsNullOrEmpty(description))
            {
                // Write the description to the registry.
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_classid))
                {
                    regkey.SetValue(null, description);
                }
            }
        }

        #region IDisposable implementation

        ~ApplicationAssociation()
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

        #region Private methods

        private bool HasValue(Dictionary<string, string> dictionary, string keyvalue, bool isvalue)
        {
            // We could use LINQ here but then older .NET versions won't be possible to use, though this has not been tested.
            foreach (KeyValuePair<string, string> kvp in dictionary)
            {
                if (isvalue)
                {
                    if (kvp.Value.Equals(keyvalue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
                else
                {
                    if (kvp.Key.Equals(keyvalue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetAssociatedProgram(string keyvalue, bool isclassid)
        {
            foreach (RegisteredProgram program in _associationManager.GetRegisteredPrograms())
            {
                if (HasValue(program.Extensions, keyvalue, isclassid))
                    return program.Name;

                if (HasValue(program.Mimes, keyvalue, isclassid))
                    return program.Name;
            }

            return null;
        }

        #endregion Private methods

        #region Properties

        public string Extension
        {
            get { return _extension; }
        }

        public string Description
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_classid))
                {
                    if (regkey == null)
                        return null;

                    return regkey.GetValue(null) as string;
                }
            }
            set
            {
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_classid))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        regkey.DeleteValue(null, false);
                    }
                    else
                    {
                        regkey.SetValue(null, value);
                    }
                }
            }
        }

        public string InfoTip
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_classid))
                {
                    if (regkey == null)
                        return null;

                    return regkey.GetValue("InfoTip") as string;
                }
            }
            set
            {
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_classid))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        regkey.DeleteValue("InfoTip", false);
                    }
                    else
                    {
                        regkey.SetValue("InfoTip", value);
                    }
                }
            }
        }

        public FileTypeAttributeFlags EditFlags
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_classid))
                {
                    if (regkey == null)
                        return FileTypeAttributeFlags.FTA_None;

                    object val = regkey.GetValue("EditFlags");

                    if (!(val is int))
                        return FileTypeAttributeFlags.FTA_None;

                    return (FileTypeAttributeFlags)((int)val);
                }
            }
            set
            {
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_classid))
                {
                    if (value == FileTypeAttributeFlags.FTA_None)
                    {
                        regkey.DeleteValue("EditFlags", false);
                    }
                    else
                    {
                        regkey.SetValue("EditFlags", (int)value);
                    }
                }
            }
        }

        public ApplicationIcon DefaultIcon
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_classid + @"\DefaultIcon"))
                {
                    if (regkey == null)
                        return null;

                    string iconpath = regkey.GetValue(null) as string;

                    int idx = iconpath.LastIndexOf(',');
                    if (idx > 0)
                    {
                        string idxstr = iconpath.Substring(idx + 1).Trim();

                        try
                        {
                            int iconindex = Int32.Parse(idxstr);
                            iconpath = iconpath.Substring(0, iconindex - 1).Trim();

                            return new ApplicationIcon(iconpath, iconindex);
                        }
                        catch { }
                    }

                    return new ApplicationIcon(iconpath);
                }
            }
            set
            {
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_classid + @"\DefaultIcon"))
                {
                    if (value == null)
                    {
                        regkey.DeleteValue(null, false);
                    }
                    else
                    {
                        string iconpath = value.IconLibraryPath;
                        if (value.IconIndex != null)
                            iconpath += ", " + value.IconIndex.ToString();

                        regkey.SetValue(null, iconpath);
                    }
                }
            }
        }

        public string ShellOpenCommand
        {
            get
            {
                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_classid + @"\Shell\Open\Command"))
                {
                    if (regkey == null)
                        return null;

                    return regkey.GetValue(null) as string;
                }
            }
            set
            {
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_classid + @"\Shell\Open\Command"))
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

        public bool Associated
        {
            get
            {
                if (_associationManager.ApplicationRegistration != null)
                {
                    bool associated = false;
                    int res = _associationManager.ApplicationRegistration.QueryAppIsDefault(_extension, ASSOCIATIONTYPE.AT_FILEEXTENSION, ASSOCIATIONLEVEL.AL_EFFECTIVE, _associationManager.ProductName, out associated);

                    return (res == S_OK) && associated;
                }

                using (RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(_extension))
                {
                    if (regkey == null)
                        return false;

                    string associatedclassid = regkey.GetValue(null) as string;

                    return _classid.Equals(associatedclassid, StringComparison.CurrentCultureIgnoreCase);
                }
            }
            set
            {
                if (_associationManager.ApplicationRegistration != null)
                {
                    // Use new windows vista & windows 7 registration functionality
                    using (RegistryKey extensionregkey = Registry.ClassesRoot.CreateSubKey(_extension))
                    {
                        string currentdefault = null;

                        int res = _associationManager.ApplicationRegistration.QueryCurrentDefault(_extension, ASSOCIATIONTYPE.AT_FILEEXTENSION, ASSOCIATIONLEVEL.AL_EFFECTIVE, out currentdefault);
                        if (res != S_OK)
                            currentdefault = null;

                        
                        if (value) // Create association
                        {
                            // Don't do anything if the extension is already associated...
                            if (_classid.Equals(currentdefault, StringComparison.CurrentCultureIgnoreCase))
                                return;

                            if (!string.IsNullOrEmpty(currentdefault))
                            {
                                // Keep the previous association before we continue
                                extensionregkey.SetValue("PrevDefault", currentdefault);
                            }

                            // Associate the extension with this classid
                            res = _associationManager.ApplicationRegistration.SetAppAsDefault(_associationManager.ProductName, _extension, ASSOCIATIONTYPE.AT_FILEEXTENSION);
                            if (res == S_OK)
                            {
                            }
                        }
                        else // Remove association
                        {
                            // Don't do anything if the extension isn't associatied with this application
                            if (!_classid.Equals(currentdefault, StringComparison.CurrentCultureIgnoreCase))
                                return;

                            string previousdefault = extensionregkey.GetValue("PrevDefault") as string;

                            if (!string.IsNullOrEmpty(previousdefault))
                            {
                                // First we must find the application associated with this classid
                                string previousapp = GetAssociatedProgram(previousdefault, true);

                                // Revert back to previous association
                                res = _associationManager.ApplicationRegistration.SetAppAsDefault(previousapp, _extension, ASSOCIATIONTYPE.AT_FILEEXTENSION);
                                if (res == S_OK)
                                {
                                }
                                extensionregkey.DeleteValue("PrevDefault", false);
                            }
                            else
                            {
                                // No previous association. Try to find the first application associated with this extension
                                string previousapp = GetAssociatedProgram(_extension, true);

                                res = _associationManager.ApplicationRegistration.SetAppAsDefault(previousapp, _extension, ASSOCIATIONTYPE.AT_FILEEXTENSION);
                                if (res == S_OK)
                                {
                                }
                            }
                        }
                    }

                    return;
                }

                
                // Use old windows registration
                using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(_extension))
                {
                    string currentclassid = regkey.GetValue(null) as string;

                    if (value) // Create association
                    {
                        // Don't do anything if the extension is already associated...
                        if (_classid.Equals(currentclassid, StringComparison.CurrentCultureIgnoreCase))
                            return;

                        if (!string.IsNullOrEmpty(currentclassid))
                        {
                            // Keep the previous association before we continue
                            regkey.SetValue("PrevDefault", currentclassid);
                        }

                        // Associate the extension with this classid
                        regkey.SetValue(null, _classid);
                    }
                    else // Remove association
                    {
                        // Don't do anything if the extension isn't associatied with this application
                        if (!_classid.Equals(currentclassid, StringComparison.CurrentCultureIgnoreCase))
                            return;

                        string previousclassid = regkey.GetValue("PrevDefault") as string;

                        if (!string.IsNullOrEmpty(previousclassid))
                        {
                            // Revert back to previous association
                            regkey.SetValue(null, previousclassid);
                            regkey.DeleteValue("PrevDefault", false);
                        }
                        else
                        {
                            // No previous association. Just delete the current association.
                            regkey.DeleteValue(null, false);
                        }
                    }
                }
            }
        }

        #endregion Properties

        public ShellCommand RegisterShellCommand(string shellname, string displayname)
        {
            return RegisterShellCommand(shellname, displayname, null);
        }

        public ShellCommand RegisterShellCommand(string shellname, string displayname, string command)
        {
            // Note: Should we prefix the shellname with the application name? For instance Winamp uses "Winamp." as prefix for it's names.

            ShellCommand result = new ShellCommand(_classid, shellname, displayname);

            if (command != null)
            {
                result.Command = command;
            }

            return result;
        }
    }
}

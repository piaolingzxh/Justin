using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace AssociationManager
{
    public class FileAssociationManager : IDisposable
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern void SHChangeNotify(int wEventId, int uFlags, IntPtr dwItem1, IntPtr dwItem2);


        private IApplicationAssociationRegistration _applicationRegistration = null;
        private readonly string _companyName;
        private readonly string _productName;
        private readonly string _applicationDescription;
        private readonly string _executablePath;
        private IEnumerable<RegisteredProgram> _registeredPrograms;

        public FileAssociationManager()
            : this(null, null, null, null)
        {
        }

        /*
         * companyname:
         *      Describes the name of the company. If no companyname is supplied Application.CompanyName will be used.
         *      
         * productname:
         *      Describes the name of the product. If no productname is supplied Application.ProductName will be used.
         *      
         * applicationdescription:
         * 
         * executable:
         * 
         */
        public FileAssociationManager(string companyname, string productname, string applicationdescription, string executable)
        {
            if (string.IsNullOrEmpty(companyname))
                companyname = Application.CompanyName;

            if (string.IsNullOrEmpty(productname))
                productname = Application.ProductName;

            if (string.IsNullOrEmpty(applicationdescription))
            {
                Assembly exeasm = Assembly.GetExecutingAssembly();
                AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)AssemblyDescriptionAttribute.GetCustomAttribute(exeasm, typeof(AssemblyDescriptionAttribute));

                applicationdescription = desc.Description;

                if (string.IsNullOrEmpty(applicationdescription))
                {
                    applicationdescription = Application.ProductName;
                }
            }

            if (string.IsNullOrEmpty(executable))
                executable = Application.ExecutablePath;

            // ***

            if (string.IsNullOrEmpty(companyname))
                throw new Exception("Company name cannot be null or empty!");

            if (string.IsNullOrEmpty(productname))
                throw new Exception("Product name cannot be null or empty!");

            // ***

            _companyName = companyname;
            _productName = productname;
            _applicationDescription = applicationdescription;
            _executablePath = executable;

            if (IsDefaultProgramsAvailable())
            {
                _applicationRegistration = (IApplicationAssociationRegistration)new ApplicationAssociationRegistration();
            }
        }

        internal IApplicationAssociationRegistration ApplicationRegistration
        {
            get { return _applicationRegistration; }
        }

        internal string ProductName
        {
            get { return _productName; }
        }

        private bool IsDefaultProgramsAvailable()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                return true;
            }

            return false;
        }

        private string GetDefaultCompanyKeyPath()
        {
            return string.Format(@"Software\{0}\", _companyName);
        }

        private string GetDefaultProducKeyPath()
        {
            return GetDefaultCompanyKeyPath() + _productName + @"\";
        }

        private string GetDefaultCapabilitiesKeyPath()
        {
            return GetDefaultProducKeyPath() + @"Capabilities\";
        }

        /*
         * Get the registry location of the capabilities for the "Default Program".
         * If the application isn't registered as a "Default Program", it will be created here.
         */
        private string GetCapabilitiesLocation()
        {
            string defaultpath = GetDefaultCapabilitiesKeyPath();
            string capabilitieskeypath = defaultpath;

            if (IsDefaultProgramsAvailable())
            {
                using (RegistryKey regkey = Registry.LocalMachine.CreateSubKey(@"Software\RegisteredApplications"))
                {
                    capabilitieskeypath = regkey.GetValue(_productName) as string;

                    if (string.IsNullOrEmpty(capabilitieskeypath))
                    {
                        // Product not registered as a "Default Program" yet so we register it here.
                        capabilitieskeypath = defaultpath.TrimEnd('\\');
                        regkey.SetValue(_productName, capabilitieskeypath);
                    }
                }
            }

            
            // Make sure the path ends with a path seperator. This way we can always rely on this and not have to check it all the time.
            capabilitieskeypath = capabilitieskeypath.TrimEnd('\\', '/') + '\\';

            using (RegistryKey capkey = Registry.LocalMachine.CreateSubKey(capabilitieskeypath))
            {
                // Only set the description if it doesn't already exist.
                if (capkey.GetValue("ApplicationDescription") == null)
                {
                    capkey.SetValue("ApplicationDescription", _applicationDescription);
                }
            }

            return capabilitieskeypath;
        }

        private void RemoveAssociatedClassIds(RegistryKey capabilitieskey, string subkeyname, string classidprefix)
        {
            classidprefix = _productName + "." + classidprefix + ".";

            using (RegistryKey associationskey = capabilitieskey.OpenSubKey(subkeyname))
            {
                if (associationskey != null)
                {
                    foreach (string ext in associationskey.GetValueNames())
                    {
                        string classid = associationskey.GetValue(ext) as string;
                        if (string.IsNullOrEmpty(classid))
                            continue;

                        // Only remove classid's created by this FileAssociationManager.
                        if (!classid.StartsWith(classidprefix, StringComparison.CurrentCultureIgnoreCase))
                            continue;

                        // Remove the association to this 
                        using (ApplicationAssociation association = new ApplicationAssociation(this, ext, classid, null))
                        {
                            association.Associated = false;
                        }

                        Registry.ClassesRoot.DeleteSubKeyTree(classid, false);
                    }
                }
            }
        }

        public void UnregisterApplicationAssociation()
        {
            UnregisterApplicationAssociation(true);
        }

        /*
         * Remove application from list of default programs and any associated entries.
         * removeregistrykeys:
         *      If set to true and the registered registry location is the same as the default productkey path, the registry keys for
         *       the application used by the AssociationManager will be removed.
         */
        public void UnregisterApplicationAssociation(bool removeregistrykeys)
        {
            string capabilitiespath = null;

            if (IsDefaultProgramsAvailable())
            {
                // Remove the Default Programs registration
                using (RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\RegisteredApplications", true))
                {
                    if (regkey != null)
                    {
                        capabilitiespath = regkey.GetValue(_productName) as string;
                        
                        // Remove the link to the "Default Program".
                        regkey.DeleteValue(_productName, false);
                    }
                }
            }

            if (string.IsNullOrEmpty(capabilitiespath))
            {
                capabilitiespath = GetDefaultCapabilitiesKeyPath();
            }


            // Remove Associations from the registry.
            using (RegistryKey capkey = Registry.LocalMachine.OpenSubKey(capabilitiespath, true))
            {
                if (capkey != null)
                {
                    RemoveAssociatedClassIds(capkey, "FileAssociations", "AssocFile");
                    RemoveAssociatedClassIds(capkey, "MimeAssociations", "MIME");

                    capkey.DeleteSubKey("FileAssociations", false);
                    capkey.DeleteSubKey("MimeAssociations", false);
                }
            }


            if (removeregistrykeys)
            {
                // Remove the registry keys associated with the company, but make sure we only delete it if the registered companykeypath is the same as the
                //  default we generate for the company. This way we don't delete anything unexpected if anyone has tampered with the registry.
                string companykeypath = GetDefaultCompanyKeyPath();
                if (capabilitiespath.StartsWith(companykeypath, StringComparison.CurrentCultureIgnoreCase))
                {
                    bool canremovecompanykey = true;

                    using (RegistryKey companykey = Registry.LocalMachine.OpenSubKey(companykeypath, true))
                    {
                        if (companykey != null)
                        {
                            companykey.DeleteSubKeyTree(_productName, false);
                            
                            string[] subkeynames = companykey.GetSubKeyNames();
                            string[] valuenames = companykey.GetValueNames();

                            // Makes sure we only remove a company key that is totally empty so we don't remove other products from that company that is installed.
                            if ((subkeynames.Length > 0) || (valuenames.Length > 0))
                            {
                                canremovecompanykey = false;
                            }
                        }
                    }

                    if (canremovecompanykey)
                    {
                        using (RegistryKey softwarekey = Registry.LocalMachine.OpenSubKey("Software", true))
                        {
                            if (softwarekey != null)
                            {
                                softwarekey.DeleteSubKey(_companyName, false);
                            }
                        }
                    }
                }
            }
        }

        internal IEnumerable<RegisteredProgram> GetRegisteredPrograms()
        {
            // Make sure we only fill this list 1 time since it might take a while filling this list
            if (_registeredPrograms == null)
            {
                _registeredPrograms = RegisteredProgram.GetRegisteredPrograms();
            }

            return _registeredPrograms;
        }

        #region File Association

        public ApplicationAssociation RegisterFileAssociation(string extension)
        {
            return RegisterFileAssociation(extension, null);
        }

        /*
         * This method will only associate the file extension with the "Default Program".
         * extension:
         *      The extension to associate with the application.
         *      
         * description:
         *      A descriptional text for the current extension. For instance "MP3 Format Sound".
         *      If no description is supplied a standard text is created.
         */
        public ApplicationAssociation RegisterFileAssociation(string extension, string description)
        {
            // Make sure the extension dont start with a dot.
            extension = extension.TrimStart('.');
            if (string.IsNullOrEmpty(extension))
                throw new Exception("Extension cannot be null or empty!");

            if (string.IsNullOrEmpty(description))
            {
                description = string.Format("{0} {1} File", _productName, extension.ToUpper());
            }

            using (RegistryKey regkey = Registry.LocalMachine.CreateSubKey(GetCapabilitiesLocation() + "FileAssociations"))
            {
                string classid = string.Format("{0}.AssocFile.{1}", _productName, extension);
                regkey.SetValue("." + extension, classid);

                return new ApplicationAssociation(this, extension, classid, description);
            }
        }

        #endregion File Association

        #region Internet media type (Mime) Association

        public ApplicationAssociation RegisterMimeAssociation(string contenttype, string subtype)
        {
            return RegisterMimeAssociation(contenttype, subtype, null);
        }

        /*
         * This method will only associate the file extension with the "Default Program".
         * extension:
         *      The extension to associate with the application.
         *      
         * description:
         *      A descriptional text for the current extension. For instance "MP3 Format Sound".
         *      If no description is supplied a standard text is created.
         *      
         * See also:
         *      http://en.wikipedia.org/wiki/Internet_media_type
         */
        public ApplicationAssociation RegisterMimeAssociation(string contenttype, string subtype, string description)
        {
            throw new Exception("Mime association has not been tested properly yet!");

            if (string.IsNullOrEmpty(contenttype))
                throw new Exception("ContentType cannot be null or empty!");

            if (string.IsNullOrEmpty(subtype))
                throw new Exception("Subtype cannot be null or empty!");
            
            contenttype = contenttype.ToLower();
            subtype = subtype.ToLower();

            if (string.IsNullOrEmpty(description))
            {
                description = string.Format("{0} {1}/{2} MIME type", _productName, contenttype, subtype);
            }

            
            using (RegistryKey regkey = Registry.LocalMachine.CreateSubKey(GetCapabilitiesLocation() + "MimeAssociations"))
            {
                string classid = string.Format("{0}.MIME.{1}", _productName, subtype);
                string mimetype = string.Format("{0}/{1}", contenttype, subtype);
                regkey.SetValue(mimetype, classid);

                return new ApplicationAssociation(this, mimetype, classid, description);
            }
        }

        #endregion Internet media type (Mime) Association


        #region Folder Shell Commands

        public ShellCommand RegisterDirectoryShellCommand(string shellname, string displayname)
        {
            return RegisterDirectoryShellCommand(shellname, displayname, null);
        }

        public ShellCommand RegisterDirectoryShellCommand(string shellname, string displayname, string command)
        {
            // Note: Should we prefix the shellname with the application name? For instance Winamp uses "Winamp." as prefix for it's names.

            ShellCommand result = new ShellCommand("Directory", shellname, displayname);

            if (command != null)
            {
                result.Command = command;
            }

            return result;
        }

        #endregion Folder Shell Commands

        #region IDisposable implementation

        ~FileAssociationManager()
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
                        _applicationRegistration = null;

                        // Notify the shell that there has been some changes to the association entries...
                        int SHCNE_ASSOCCHANGED = 0x08000000;
                        int SHCNF_IDLIST = 0x0000;
                        SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
                    }
                    finally
                    {
                        _disposed = true;
                    }
                }
            }
        }

        #endregion IDisposable implementation
    }
}

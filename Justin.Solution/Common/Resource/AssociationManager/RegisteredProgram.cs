using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;

namespace AssociationManager
{
    [DebuggerDisplay("{Name}")]
    public class RegisteredProgram
    {
        private readonly string _productName;
        private readonly Dictionary<string, string> _extensions = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _mimes = new Dictionary<string, string>();

        public RegisteredProgram(string productname)
        {
            _productName = productname;
        }

        public string Name
        {
            get { return _productName; }
        }

        public Dictionary<string, string> Extensions
        {
            get { return _extensions; }
        }

        public Dictionary<string, string> Mimes
        {
            get { return _mimes; }
        }

        public static IEnumerable<RegisteredProgram> GetRegisteredPrograms()
        {
            List<RegisteredProgram> result = new List<RegisteredProgram>();

            using (RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\RegisteredApplications"))
            {
                if (regkey != null)
                {
                    foreach (string productname in regkey.GetValueNames())
                    {
                        string capabilitieskey = regkey.GetValue(productname) as string;
                        if (string.IsNullOrEmpty(capabilitieskey))
                            continue;

                        capabilitieskey = capabilitieskey.TrimEnd('\\') + '\\';


                        RegisteredProgram regprog = new RegisteredProgram(productname);
                        result.Add(regprog);

                        string[] capabilitytypes = new string[] { "FileAssociations", "MimeAssociations", "URLAssociations", "StartMenu" };
                        foreach (string capabilitytype in capabilitytypes)
                        {
                            using (RegistryKey extregkey = Registry.LocalMachine.OpenSubKey(capabilitieskey + capabilitytype))
                            {
                                if (extregkey == null)
                                    continue;

                                foreach (string valuename in extregkey.GetValueNames())
                                {
                                    string value = extregkey.GetValue(valuename) as string;
                                    if (string.IsNullOrEmpty(value))
                                        continue;

                                    switch (capabilitytype)
                                    {
                                        case "FileAssociations":
                                            {
                                                regprog._extensions.Add("." + valuename.TrimStart('.'), value);
                                                break;
                                            }
                                        case "MimeAssociations":
                                            {
                                                regprog._mimes.Add(valuename, value);
                                                break;
                                            }
                                        case "URLAssociations":
                                            {
                                                // TODO: Include these when URL is implemented
                                                // regprog._urls.Add(valuename, value);
                                                break;
                                            }
                                        case "StartMenu":
                                            {
                                                // TODO: Include these when StartMenu is implemented
                                                // regprog._startmenus.Add(valuename, value);
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}

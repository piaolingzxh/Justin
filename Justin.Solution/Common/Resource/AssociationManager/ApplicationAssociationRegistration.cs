using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AssociationManager
{
    internal enum ASSOCIATIONLEVEL
    {
        AL_MACHINE,
        AL_EFFECTIVE,
        AL_USER,
    };

    internal enum ASSOCIATIONTYPE
    {
        AT_FILEEXTENSION,
        AT_URLPROTOCOL,
        AT_STARTMENUCLIENT,
        AT_MIMETYPE,
    };

    [ComVisible(true), ComImport, GuidAttribute("4e530b0a-e611-4c77-a3ac-9031d022281b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IApplicationAssociationRegistration
    {
        [PreserveSig]
        int QueryCurrentDefault(
                                [In, MarshalAs(UnmanagedType.LPWStr)] string pszQuery,
                                [In, MarshalAs(UnmanagedType.I4)] ASSOCIATIONTYPE atQueryType,
                                [In, MarshalAs(UnmanagedType.I4)] ASSOCIATIONLEVEL alQueryLevel,
                                [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszAssociation
                               );

        [PreserveSig]
        int QueryAppIsDefault(
                              [In, MarshalAs(UnmanagedType.LPWStr)] string pszQuery,
                              [In] ASSOCIATIONTYPE atQueryType,
                              [In] ASSOCIATIONLEVEL alQueryLevel,
                              [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName,
                              [Out] out bool pfDefault
                             );

        [PreserveSig]
        int QueryAppIsDefaultAll(
                                 [In] ASSOCIATIONLEVEL alQueryLevel,
                                 [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName,
                                 [Out] out bool pfDefault
                                );

        [PreserveSig]
        int SetAppAsDefault(
                            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName,
                            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSet,
                            [In] ASSOCIATIONTYPE atSetType
                           );

        [PreserveSig]
        int SetAppAsDefaultAll(
                               [In, MarshalAs(UnmanagedType.LPWStr)] string pszAppRegistryName
                              );

        [PreserveSig]
        int ClearUserAssociations();
    }

    [ComImport, Guid("591209c7-767b-42b2-9fba-44ee4615f2c7")]
    internal class ApplicationAssociationRegistration
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Web;

namespace Justin.Framework.Utility
{
    public class IdentityScope : IDisposable
    {
        // obtains user token
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        // closes open handes returned by LogonUser
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        extern static bool CloseHandle(IntPtr handle);

        [DllImport("Advapi32.DLL")]
        static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

        [DllImport("Advapi32.DLL")]
        static extern bool RevertToSelf();

        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_LOGON_NEWCREDENTIALS = 9;//域控中的需要用:Interactive = 2
        private bool disposed;

        public IdentityScope(string domain, string username, string password)
        {
            // initialize tokens
            IntPtr pExistingTokenHandle = new IntPtr(0);
            IntPtr pDuplicateTokenHandle = new IntPtr(0);

            try
            {
                // get handle to token
                bool bImpersonated = LogonUser(username, domain, password, LOGON32_LOGON_NEWCREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref pExistingTokenHandle);
                if (true == bImpersonated)
                {
                    if (!ImpersonateLoggedOnUser(pExistingTokenHandle))
                    {
                        int nErrorCode = Marshal.GetLastWin32Error();
                        throw new System.Exception("ImpersonateLoggedOnUser error;Code=" + nErrorCode);
                    }
                }
                else
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    throw new System.Exception("LogonUser error;Code=" + nErrorCode);
                }
            }
            finally
            {
                // close handle(s)
                if (pExistingTokenHandle != IntPtr.Zero)
                    CloseHandle(pExistingTokenHandle);
                if (pDuplicateTokenHandle != IntPtr.Zero)
                    CloseHandle(pDuplicateTokenHandle);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                RevertToSelf();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

    //使用
    class Test
    {
        private void Main()
        {
            using (IdentityScope c = new IdentityScope(@"\\172.0.0.1\test\", "userName", "password"))
            {//在此就可以访问了.  

                //string dateFolder = @"\\127.0.0.1\test\";
                //if (!Directory.Exists(dateFolder))
                //    Directory.CreateDirectory(dateFolder);
                string filePath = @"\\127.0.0.1\test\test.DB";
                System.IO.FileInfo file = new System.IO.FileInfo(filePath);
                if (file.Exists)
                {
                    HttpContext.Current.Response.Clear();

                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.Filter.Close();

                    HttpContext.Current.Response.WriteFile(file.FullName);
                    HttpContext.Current.Response.End();
                }
            }
        }
    }

}

namespace client
{
    using System;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;

    public class FullWebBrowserCookie
    {
        private static void DemandWebPermission(Uri uri_0)
        {
            string uriString = UriToString(uri_0);
            if (uri_0.IsFile)
            {
                string localPath = uri_0.LocalPath;
                new FileIOPermission(FileIOPermissionAccess.Read, localPath).Demand();
            }
            else
            {
                new WebPermission(NetworkAccess.Connect, uriString).Demand();
            }
        }

        [SecurityCritical]
        public static string GetCookieInternal(Uri uri_0, bool bool_0)
        {
            uint num = 0;
            string str = UriToString(uri_0);
            uint num2 = 0x2000;
            if (NativeMethods.InternetGetCookieExW(str, null, null, ref num, 0x2000, IntPtr.Zero))
            {
                num++;
                StringBuilder builder = new StringBuilder((int) num);
                if (NativeMethods.InternetGetCookieExW(str, null, builder, ref num, num2, IntPtr.Zero))
                {
                    DemandWebPermission(uri_0);
                    return builder.ToString();
                }
            }
            int num3 = Marshal.GetLastWin32Error();
            if (!bool_0 && (num3 == 0x103))
            {
                return null;
            }
            return null;
        }

        private static string UriToString(Uri uri_0)
        {
            if (uri_0 == null)
            {
                throw new ArgumentNullException("uri");
            }
            UriComponents components = uri_0.IsAbsoluteUri ? UriComponents.AbsoluteUri : UriComponents.SerializationInfoString;
            return new StringBuilder(uri_0.GetComponents(components, UriFormat.SafeUnescaped), 0x823).ToString();
        }
    }
}


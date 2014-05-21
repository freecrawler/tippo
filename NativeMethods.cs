namespace client
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    internal sealed class NativeMethods
    {
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("wininet.dll", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        internal static extern bool InternetGetCookieExW([In] string string_0, [In] string string_1, [Out] StringBuilder stringBuilder_0, [In, Out] ref uint uint_0, uint uint_1, IntPtr intptr_0);

        public enum ErrorFlags
        {
            ERROR_INSUFFICIENT_BUFFER = 0x7a,
            ERROR_INVALID_PARAMETER = 0x57,
            ERROR_NO_MORE_ITEMS = 0x103
        }

        public enum InternetFlags
        {
            INTERNET_COOKIE_HTTPONLY = 0x2000,
            INTERNET_COOKIE_THIRD_PARTY = 0x20000,
            INTERNET_FLAG_RESTRICTED_ZONE = 0x10
        }
    }
}


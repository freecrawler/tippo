namespace client
{
    using System;
    using System.Runtime.InteropServices;

    public class Win32Utils
    {
        public const uint FILE_SHARE_READ = 1;
        public const uint FILE_SHARE_WRITE = 2;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint INVALID_HANDLE_VALUE = uint.MaxValue;
        public const uint IOCTL_NDIS_QUERY_GLOBAL_STATS = 0x170002;
        public const uint OPEN_EXISTING = 3;
        public const int PERMANENT_ADDRESS = 0x1010101;
        public const string REG_NET_CARDS_KEY = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkCards";

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(uint uint_0);
        [DllImport("kernel32.dll")]
        public static extern uint CreateFile(string string_0, uint uint_0, uint uint_1, int int_0, uint uint_2, uint uint_3, int int_1);
        [DllImport("kernel32.dll")]
        public static extern int DeviceIoControl(uint uint_0, uint uint_1, ref int int_0, int int_1, byte[] byte_0, int int_2, ref uint uint_2, int int_3);
    }
}


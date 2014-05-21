namespace client
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;

    public class GW2hxt
    {
        public static List<string> GetNetCardList()
        {
            List<string> list = new List<string>();
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkCards");
                if (key == null)
                {
                    return list;
                }
                string[] subKeyNames = key.GetSubKeyNames();
                RegistryKey key2 = null;
                foreach (string str in subKeyNames)
                {
                    key2 = key.OpenSubKey(str);
                    if (key2 != null)
                    {
                        object obj2 = key2.GetValue("ServiceName");
                        if (obj2 != null)
                        {
                            list.Add(obj2.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        private static string GetPhysicalAddr(string string_0)
        {
            string str = string.Empty;
            uint num = 0;
            try
            {
                num = Win32Utils.CreateFile(@"\\.\" + string_0, 0xc0000000, 3, 0, 3, 0, 0);
                if (num == uint.MaxValue)
                {
                    return str;
                }
                byte[] buffer = new byte[6];
                uint num2 = 0;
                int num3 = 0x1010101;
                if (Win32Utils.DeviceIoControl(num, 0x170002, ref num3, 4, buffer, 6, ref num2, 0) == 0)
                {
                    return str;
                }
                string str3 = string.Empty;
                foreach (byte num4 in buffer)
                {
                    str3 = Convert.ToString(num4, 0x10).PadLeft(2, '0');
                    str = str + str3;
                    str3 = string.Empty;
                }
            }
            finally
            {
                if (num != 0)
                {
                    Win32Utils.CloseHandle(num);
                }
            }
            return str;
        }

        public static string y1KU1T()
        {
            List<string>.Enumerator enumerator = GetNetCardList().GetEnumerator();
            string physicalAddr = string.Empty;
            while (enumerator.MoveNext())
            {
                physicalAddr = GetPhysicalAddr(enumerator.Current);
                if (physicalAddr != string.Empty)
                {
                    return physicalAddr;
                }
            }
            return physicalAddr;
        }
    }
}


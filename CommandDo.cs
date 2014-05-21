namespace client
{
    using System;
    using System.Diagnostics;

    public class CommandDo
    {
        public static string Execute(string string_0)
        {
            return Execute(string_0, 0x1770);
        }

        public static string Execute(string string_0, int int_0)
        {
            string str = "";
            if ((string_0 != null) && (string_0 != ""))
            {
                Process process = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "rasdial.exe";
                info.Arguments = string_0;
                info.UseShellExecute = false;
                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.CreateNoWindow = true;
                process.StartInfo = info;
                try
                {
                    if (!process.Start())
                    {
                        return str;
                    }
                    if (int_0 == 0)
                    {
                        process.WaitForExit();
                    }
                    else
                    {
                        process.WaitForExit(int_0);
                    }
                    return process.StandardOutput.ReadToEnd();
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                    {
                        process.Close();
                    }
                }
            }
            return str;
        }
    }
}


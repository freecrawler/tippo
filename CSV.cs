namespace client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class CSV
    {
        private bool bool_0 = true;
        private FileStream fileStream_0;
        public bool isCreate = false;
        public string path = "";
        public string split = ",";
        private StreamWriter streamWriter_0;

        public CSV(string string_0, string string_1)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择存放的文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;
                this.path = selectedPath + @"\";
                if (File.Exists(this.path + string_0))
                {
                    File.Delete(this.path + string_0);
                }
                this.fileStream_0 = new FileStream(this.path + string_0, FileMode.Create, FileAccess.Write);
                this.streamWriter_0 = new StreamWriter(this.fileStream_0, Encoding.GetEncoding(string_1));
                this.isCreate = true;
            }
        }

        public void addCell(string string_0)
        {
            if (this.bool_0)
            {
                this.streamWriter_0.Write(this.method_0(string_0));
            }
            else
            {
                this.streamWriter_0.Write(this.split + this.method_0(string_0));
            }
            this.bool_0 = false;
        }

        public void addEndRow()
        {
            this.streamWriter_0.WriteLine();
            this.bool_0 = true;
        }

        public void addRow(string[] string_0)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < string_0.Length; i++)
            {
                builder.Append(this.method_0(string_0[i]) + this.split);
            }
            this.streamWriter_0.WriteLine(builder.ToString());
        }

        public void addRow(List<string> list_0)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list_0.Count; i++)
            {
                if (i == 0)
                {
                    builder.Append(this.method_0(list_0[i]));
                }
                else
                {
                    builder.Append(this.split + this.method_0(list_0[i]));
                }
            }
            this.streamWriter_0.WriteLine(builder.ToString());
        }

        public void addString(string string_0)
        {
            this.streamWriter_0.WriteLine(string_0);
        }

        private string method_0(string string_0)
        {
            if (this.split == "\t")
            {
                if (string_0.Contains("\""))
                {
                    return ("\"" + string_0.Replace("\"", "\"\"") + "\"");
                }
                return string_0;
            }
            if (!string_0.Contains(",") && !string_0.Contains("\""))
            {
                return string_0;
            }
            return ("\"" + string_0.Replace("\"", "\"\"") + "\"");
        }

        public void Save()
        {
            try
            {
                this.streamWriter_0.Close();
                this.streamWriter_0.Dispose();
                this.fileStream_0.Close();
                this.fileStream_0.Dispose();
            }
            catch
            {
            }
        }
    }
}


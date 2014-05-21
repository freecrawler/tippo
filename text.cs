namespace client
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;

    public class text
    {
        public static double batchEdit(double double_0, int int_0, int int_1, double double_1)
        {
            switch (int_0)
            {
                case 1:
                    switch (int_1)
                    {
                        case 1:
                            double_0 += double_1;
                            return double_0;

                        case 2:
                            double_0 -= double_1;
                            return double_0;

                        case 3:
                            double_0 *= double_1;
                            return double_0;

                        case 4:
                            double_0 /= double_1;
                            return double_0;
                    }
                    return double_0;

                case 2:
                    double_0 = double_1;
                    return double_0;
            }
            return double_0;
        }

        public static string batchEdit(string string_0, int int_0, string string_1, string string_2)
        {
            switch (int_0)
            {
                case 1:
                    string_0 = string_1 + string_0 + string_2;
                    return string_0;

                case 2:
                    string_0 = func.正则替换(string_0, string_1, string_2);
                    return string_0;

                case 3:
                    string_0 = string_1.Trim();
                    return string_0;
            }
            return string_0;
        }

        private static string boolTo01(bool bool_0)
        {
            if (bool_0)
            {
                return "1";
            }
            return "0";
        }

        public static double change(double double_0, bool bool_0, double double_1, double double_2, int int_0, double double_3)
        {
            double num = 0.0;
            num = Math.Round(double_0, int_0);
            if ((double_1 > num) && (double_1 != 0.0))
            {
                num = double_1;
            }
            if ((double_2 < num) && (double_2 != 0.0))
            {
                num = double_2;
            }
            if (bool_0 && (num == 0.0))
            {
                num = double_3;
            }
            return num;
        }

        public static int change(int int_0, bool bool_0, double double_0, double double_1, int int_1, double double_2)
        {
            return (int) change((double) int_0, bool_0, double_0, double_1, int_1, double_2);
        }

        public static double change(string string_0, bool bool_0, double double_0, double double_1, int int_0, double double_2)
        {
            return change(toDouble(string_0), bool_0, double_0, double_1, int_0, double_2);
        }

        public static string change(string string_0, int int_0, int int_1, bool bool_0, bool bool_1, List<string> list_0, string string_1)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                if (bool_0)
                {
                    string_0 = string_1;
                    return string_0;
                }
                return "";
            }
            string_0 = string_0.Trim();
            if (!bool_1)
            {
                string_0 = func.正则替换(string_0, @"[^\x00-\xff]", "");
            }
            foreach (string str in list_0)
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    string_0 = func.正则替换(string_0, str, "");
                }
            }
            if ((string_0.Length < int_0) && (int_0 != 0))
            {
                string_0 = string_0.PadRight(int_0, '_');
            }
            if ((string_0.Length > int_1) && (int_1 != 0))
            {
                string_0 = string_0.Substring(0, int_1);
            }
            if (bool_0 && (string_0.Length == 0))
            {
                string_0 = string_1;
            }
            return string_0;
        }

        public static string change(string string_0, int int_0, int int_1, bool bool_0, bool bool_1, List<string> list_0, List<string> list_1, string string_1)
        {
            string str = change(string_0, int_0, int_1, bool_0, bool_1, list_0, string_1);
            foreach (string str2 in list_1)
            {
                if (!string.IsNullOrEmpty(str2.Trim()) && !func.正则匹配(str, str2, ""))
                {
                    str = str2.Split(new char[] { '|' })[0] + " " + str;
                }
            }
            return str;
        }

        public static string check(double double_0, bool bool_0, double double_1, double double_2, int int_0)
        {
            return check(double_0.ToString(), bool_0, double_1, double_2, int_0);
        }

        public static string check(string string_0, bool bool_0, double double_0, double double_1, int int_0)
        {
            string str2;
            if (bool_0 && (string_0 == ""))
            {
                return "不能为空";
            }
            double num = 0.0;
            try
            {
                num = toDouble(string_0);
                string str = "";
                if ((num < double_0) && (double_0 != 0.0))
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, "不能小于", double_0, "," });
                }
                if ((num > double_1) && (double_1 != 0.0))
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, "不能大于", double_1, "," });
                }
                double num3 = num - Convert.ToInt32(num);
                int num2 = num3.ToString().Length - 2;
                if ((int_0 >= num2) || (int_0 == 10))
                {
                    return str;
                }
                if (int_0 == 0)
                {
                    return (str + "必须是整数");
                }
                object obj4 = str;
                return string.Concat(new object[] { obj4, "小数点位数不能超过", int_0, "位" });
            }
            catch
            {
                str2 = "请输入数字";
            }
            return str2;
        }

        public static string check(string string_0, int int_0, int int_1, bool bool_0, bool bool_1, List<string> list_0)
        {
            return check(string_0, int_0, int_1, bool_0, bool_1, list_0, new List<string>());
        }

        public static string check(string string_0, int int_0, int int_1, bool bool_0, bool bool_1, List<string> list_0, List<string> list_1)
        {
            string str = "";
            if (string.IsNullOrEmpty(string_0))
            {
                if (bool_0)
                {
                    str = str + "不能为空,";
                }
                return str;
            }
            string_0 = string_0.Trim();
            if (((string_0.Length > 0) && (string_0.Length < int_0)) && (int_0 != 0))
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "不能少于", int_0, "个字符," });
            }
            if ((string_0.Length > int_1) && (int_1 != 0))
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, "不能超过", int_1, "个字符," });
            }
            if (bool_0 && (string_0.Length < 0))
            {
                str = str + "不能为空,";
            }
            if (!bool_1 && func.正则匹配(string_0, @"[^\x00-\xff]", ""))
            {
                str = str + "不能包含中文,";
            }
            foreach (string str2 in list_0)
            {
                if (!string.IsNullOrEmpty(str2.Trim()) && func.正则匹配(string_0, str2, ""))
                {
                    str = str + "不能包含禁用词-" + str2 + ",";
                }
            }
            foreach (string str3 in list_1)
            {
                if (!string.IsNullOrEmpty(str3.Trim()) && !func.正则匹配(string_0, str3, ""))
                {
                    str = str + "必须包含词语-" + str3 + ",";
                }
            }
            return str;
        }

        public Dictionary<string, string> getPics(string string_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, JToken> pair in func.转成JObject(func.正则获取所有(string_0, "(%1%2\\\\[^'\"]\\\\[^'\"]\\\\([^'\"\\\\]+))['\"]", ""), 0, 1))
            {
                if (File.Exists(pair.Key))
                {
                    dictionary[pair.ToString()] = "";
                }
                else
                {
                    string_0 = func.正则替换(string_0, "<img[^>]*src=['\"]" + pair.Key.Replace(@"\", @"\\") + "['\"][^>]*>", "");
                }
            }
            return dictionary;
        }

        public static int intTo01(bool bool_0)
        {
            if (bool_0)
            {
                return 1;
            }
            return 0;
        }

        public static DataSet LoadDataFromExcel(string string_0, string string_1)
        {
            try
            {
                OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + string_0 + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'");
                selectConnection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM  [" + string_1 + "$]", selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Sheet1");
                selectConnection.Close();
                return dataSet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string map(string string_0, Dictionary<string, string> dictionary_0, bool bool_0, int int_0)
        {
            int num = 0;
            int num2 = -1;
            string str = "";
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                num = func.strSimilar(pair.Key, string_0);
                if (num > num2)
                {
                    str = pair.Value.ToString();
                    num2 = num;
                }
            }
            if (!bool_0 && (num2 < int_0))
            {
                str = "";
            }
            return str;
        }

        public static textID mapTextId(textID textID_0, List<textID> list_0, bool bool_0, int int_0, string string_0)
        {
            int num = 0;
            int num2 = 0;
            textID tid = new textID();
            if (textID_0.text[string_0].Trim() == "")
            {
                return tid;
            }
            foreach (textID tid2 in list_0)
            {
                num = func.strSimilar(tid2.text[string_0], textID_0.text[string_0]);
                if (num > num2)
                {
                    tid = tid2;
                    num2 = num;
                }
            }
            if (num2 >= int_0)
            {
                return tid;
            }
            if (bool_0)
            {
                tid.text[string_0] = textID_0.text[string_0];
                tid.id = "other";
                return tid;
            }
            return new textID();
        }

        public static bool OpenCSVFile(ref DataTable dataTable_0, Stream stream_0)
        {
            try
            {
                string str;
                int length = 0;
                bool flag = true;
                StreamReader reader = new StreamReader(stream_0, Encoding.GetEncoding("gb2312"));
                while ((str = reader.ReadLine()) != null)
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    if (flag)
                    {
                        flag = false;
                        length = strArray.Length;
                        int num2 = 0;
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            num2 = j + 1;
                            DataColumn column = new DataColumn(num2.ToString());
                            dataTable_0.Columns.Add(column);
                        }
                    }
                    DataRow row = dataTable_0.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        row[i] = strArray[i];
                    }
                    dataTable_0.Rows.Add(row);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool OpenCSVFile(ref DataTable dataTable_0, string string_0)
        {
            string path = string_0;
            try
            {
                string str2;
                int length = 0;
                bool flag = true;
                StreamReader reader = new StreamReader(path, Encoding.Default);
                while ((str2 = reader.ReadLine()) != null)
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    if (flag)
                    {
                        flag = false;
                        length = strArray.Length;
                        int num2 = 0;
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            num2 = j + 1;
                            DataColumn column = new DataColumn(num2.ToString());
                            dataTable_0.Columns.Add(column);
                        }
                    }
                    DataRow row = dataTable_0.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        row[i] = strArray[i];
                    }
                    dataTable_0.Rows.Add(row);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string replacePics(string string_0, Dictionary<string, string> dictionary_0)
        {
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                if (pair.Value != "")
                {
                    string oldValue = pair.Key.Replace(@"\", @"\\");
                    string_0 = string_0.Replace(oldValue, pair.Value);
                }
            }
            return string_0;
        }

        public static string strTo01(string string_0)
        {
            if (string_0.ToLower() == "true")
            {
                return "1";
            }
            return "0";
        }

        public static double toDouble(string string_0)
        {
            double num = -1.0;
            string_0 = func.正则替换(string_0, @"[^\d\.\-]", "");
            try
            {
                num = Convert.ToDouble(string_0);
            }
            catch (Exception)
            {
            }
            return num;
        }

        public static int toInt(string string_0)
        {
            int num = -1;
            try
            {
                num = Convert.ToInt32(string_0);
            }
            catch (Exception)
            {
            }
            return num;
        }
    }
}


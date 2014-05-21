namespace client
{
    using Conversive.PHPSerializationLibrary;
    using ICSharpCode.SharpZipLib.Zip;
    using IfacesEnumsStructsClasses;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Windows.Forms;

    public class func
    {
        public static string AbsUrl(string string_0, string string_1)
        {
            try
            {
                Uri uri = new Uri(new Uri(string_0), string_1);
                return uri.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static Stream copyStream(Stream stream_0)
        {
            Stream stream = new MemoryStream();
            try
            {
                int num;
                byte[] buffer = new byte[0x400];
                goto Label_0024;
            Label_0013:
                if (num > 0)
                {
                    goto Label_0024;
                }
                goto Label_0036;
            Label_0019:
                stream.Write(buffer, 0, num);
                goto Label_0013;
            Label_0024:
                num = stream_0.Read(buffer, 0, buffer.Length);
                if (num <= 0)
                {
                    goto Label_0013;
                }
                goto Label_0019;
            Label_0036:
                stream.Position = 0L;
            }
            catch
            {
            }
            return stream;
        }

        public static void copyToDic(JObject jobject_0, JObject jobject_1)
        {
            foreach (KeyValuePair<string, JToken> pair in jobject_1)
            {
                jobject_0[pair.Key] = pair.Value;
            }
        }

        public static void copyToDic(Dictionary<string, string> dictionary_0, Dictionary<string, string> dictionary_1)
        {
            foreach (KeyValuePair<string, string> pair in dictionary_1)
            {
                dictionary_0[pair.Key] = pair.Value;
            }
        }

        public static void copyToDic2(JObject jobject_0, JObject jobject_1)
        {
            foreach (KeyValuePair<string, JToken> pair in jobject_1)
            {
                if (!pair.Value.ToString().StartsWith("^abc"))
                {
                    jobject_0[pair.Key] = pair.Value;
                }
            }
        }

        public static string Dept(string string_0, string string_1)
        {
            if (string_0 == "")
            {
                return "";
            }
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            byte[] buffer = new byte[string_0.Length];
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(string_1.Substring(0, 8));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                buffer = Convert.FromBase64String(string_0);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                return encoding.GetString(stream.ToArray());
            }
            catch
            {
                return string_0;
            }
        }

        public static string end(string string_0)
        {
            string format = "r";
            return DateTime.Now.AddDays((double) Convert.ToInt32(string_0)).ToString(format, DateTimeFormatInfo.InvariantInfo);
        }

        public static string Enpt(string string_0, string string_1)
        {
            if (string_0 == "")
            {
                return "";
            }
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(string_1.Substring(0, 8));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.UTF8.GetBytes(string_0);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                return Convert.ToBase64String(stream.ToArray());
            }
            catch
            {
                return string_0;
            }
        }

        public static string getAttrByElem(IHTMLElement ihtmlelement_0, string string_0)
        {
            try
            {
                return ihtmlelement_0.getAttribute(string_0, 0).ToString().Replace("about:blank", "").Trim();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string getCateById(string string_0, string string_1, string string_2)
        {
            JObject obj2 = new JObject();
            obj2["cateid"] = string_1;
            obj2["site"] = string_0;
            obj2["type"] = string_2;
            http http = new http();
            http.url = PzFuEX.ekcg10 + "?jo=0";
            http.PostData.addString(Enpt(JsonConvert.SerializeObject(obj2), "dsfgdsghocvxcthr"));
            return Dept(http.DownHtml(), "dsfgdsghocvxcthr");
        }

        public static Dictionary<string, string> getDicFromJObject(JObject jobject_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                dictionary[pair.Key] = pair.Value.ToString();
            }
            return dictionary;
        }

        public static string getDomain(string string_0)
        {
            try
            {
                string_0 = string_0.Replace("https:", "http:");
                if (!string_0.StartsWith("http"))
                {
                    string_0 = "http://" + string_0;
                }
                Uri uri = new Uri(string_0);
                return uri.Host;
            }
            catch
            {
                return "";
            }
        }

        private static JArray getJa(JArray jarray_0, string string_0, JObject jobject_0)
        {
            JArray array = new JArray();
            if (jarray_0.Count > 0)
            {
                foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                {
                    foreach (KeyValuePair<string, JToken> pair in jobject_0)
                    {
                        array.Add(token.ToString() + "," + string_0 + ":" + pair.Key);
                    }
                }
                return array;
            }
            foreach (KeyValuePair<string, JToken> pair2 in jobject_0)
            {
                array.Add(string_0 + ":" + pair2.Key);
            }
            return array;
        }

        public static JArray getJaFromDic(Dictionary<string, string> dictionary_0)
        {
            JArray array = new JArray();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                array.Add(pair.Key);
            }
            return array;
        }

        public static JArray getJaFromDic2(Dictionary<string, string> dictionary_0)
        {
            JArray array = new JArray();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                array.Add(text.toInt(pair.Key));
            }
            return array;
        }

        public static JArray getJaFromJo(JObject jobject_0)
        {
            JArray array = new JArray();
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                array.Add(pair.Key);
            }
            return array;
        }

        public static JArray getJArrayFromList(List<string> list_0)
        {
            JArray array = new JArray();
            foreach (string str in list_0)
            {
                array.Add(str);
            }
            return array;
        }

        public static JObject getJObjectFromDic(Dictionary<string, string> dictionary_0)
        {
            JObject obj2 = new JObject();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                obj2[pair.Key] = pair.Value;
            }
            return obj2;
        }

        public static string getKeyByValue(JObject jobject_0, string string_0)
        {
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                if (pair.Value.ToString() == string_0)
                {
                    return pair.Key.ToString();
                }
            }
            return "";
        }

        public static string getKeyByValue(Dictionary<string, string> dictionary_0, string string_0)
        {
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                if (pair.Value.ToString() == string_0)
                {
                    return pair.Key.ToString();
                }
            }
            return "";
        }

        public static List<string> getKeysByValue(Dictionary<string, string> dictionary_0, string string_0)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                if (pair.Value == string_0)
                {
                    list.Add(pair.Key);
                }
            }
            return list;
        }

        public static int getLength(string string_0)
        {
            int num = 正则获取所有(string_0, "[^\0-\x00ff]", "").Count * 2;
            string_0 = 正则替换(string_0, @"[^\x00-\xff]", "");
            return (num + string_0.Length);
        }

        public static List<string> getListFromJArray(JArray jarray_0)
        {
            List<string> list = new List<string>();
            foreach (string str in (IEnumerable<JToken>) jarray_0)
            {
                list.Add(str);
            }
            return list;
        }

        public static string getLocalPath(string string_0)
        {
            if (string_0 == "")
            {
                return "";
            }
            try
            {
                Uri uri = new Uri(string_0);
                return uri.LocalPath;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static Dictionary<string, string> getNewDic(Dictionary<string, string> dictionary_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (dictionary_0 != null)
            {
                foreach (KeyValuePair<string, string> pair in dictionary_0)
                {
                    dictionary[pair.Key] = "";
                }
            }
            return dictionary;
        }

        public static Dictionary<string, double> getNewDic2(Dictionary<string, double> dictionary_0)
        {
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            if (dictionary_0 != null)
            {
                foreach (KeyValuePair<string, double> pair in dictionary_0)
                {
                    dictionary[pair.Key] = 1.0;
                }
            }
            return dictionary;
        }

        public static bool getNum(int int_0)
        {
            string path = PzFuEX.Jo4304 + "mm.txt";
            if (!File.Exists(path))
            {
                writeTxt(path, Enpt(int_0.ToString(), "safsdtgyg"));
                return (int_0 >= 20);
            }
            string str2 = Dept(readTxt(path), "safsdtgyg").Trim();
            if (正则匹配(str2, @"^\d+$", ""))
            {
                int result = 0;
                int.TryParse(str2, out result);
                result += int_0;
                writeTxt(path, Enpt(result.ToString(), "safsdtgyg"));
                return (result >= 20);
            }
            return true;
        }

        public static object getPhpObj(string string_0)
        {
            Serializer serializer = new Serializer();
            return serializer.Deserialize(string_0);
        }

        public static string getPhpStr(Hashtable hashtable_0)
        {
            Serializer serializer = new Serializer();
            return serializer.Serialize(hashtable_0);
        }

        public static JObject getPrice(JObject jobject_0, JObject jobject_1)
        {
            JObject obj2 = getPriceFromOption(jobject_1);
            foreach (KeyValuePair<string, JToken> pair in obj2)
            {
                string[] strArray = pair.Key.Split(new char[] { ',' });
                using (IEnumerator<KeyValuePair<string, JToken>> enumerator2 = jobject_0.GetEnumerator())
                {
                    KeyValuePair<string, JToken> current;
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        string[] strArray2 = current.Key.Split(new char[] { ',' });
                        if (isContain(strArray, strArray2))
                        {
                            goto Label_0080;
                        }
                    }
                    continue;
                Label_0080:
                    obj2[pair.Key] = current.Value;
                }
            }
            return obj2;
        }

        public static JArray getPrice(string string_0, string string_1)
        {
            JArray array = new JArray();
            JObject item = new JObject();
            item["min"] = "1";
            item["max"] = "1";
            item["sell"] = string_0;
            if (string_1 != "")
            {
                item["num"] = string_1;
            }
            array.Add(item);
            return array;
        }

        public static JArray getPrice(string string_0, string string_1, string string_2, string string_3)
        {
            JArray array = new JArray();
            JObject item = new JObject();
            item["min"] = string_0;
            item["max"] = string_1;
            item["sell"] = string_2;
            if (string_3 != "")
            {
                item["num"] = string_3;
            }
            array.Add(item);
            return array;
        }

        public static JObject getPriceFromOption(JObject jobject_0)
        {
            JArray array = new JArray();
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                array = getJa(array, pair.Key, (JObject) pair.Value);
            }
            JObject obj2 = new JObject();
            foreach (JToken token in (IEnumerable<JToken>) array)
            {
                obj2[token.ToString()] = new JArray();
            }
            return obj2;
        }

        public static JArray getPriceTable(string string_0, string string_1, JArray jarray_0)
        {
            JArray array = new JArray();
            foreach (JArray array2 in (IEnumerable<JToken>) jarray_0)
            {
                JObject item = new JObject();
                string str = array2[0].ToString();
                if (str.Contains(string_0))
                {
                    JArray array3 = 正则获取(array2[0].ToString(), @"([^-]+)[^\d]+(\d+)", "");
                    item["min"] = array3[0].ToString();
                    item["max"] = array3[1].ToString();
                }
                else
                {
                    item["min"] = str;
                    item["max"] = str;
                }
                if (array2[1].ToString().Contains(string_1))
                {
                    item["sell"] = 正则获取(array2[1].ToString(), @"~[^\d]+([\d.]+)");
                }
                else
                {
                    item["sell"] = 正则获取(array2[1].ToString(), @"([\d.]+)");
                }
                array.Add(item);
            }
            return array;
        }

        public static string getstrFromObj(JObject jobject_0, string string_0)
        {
            string str = "";
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                if (str == "")
                {
                    str = str + string_0 + pair.Key.Replace("'", "''") + string_0;
                }
                else
                {
                    string str2 = str;
                    str = str2 + "," + string_0 + pair.Key.Replace("'", "''") + string_0;
                }
            }
            return str;
        }

        public static List<textID> getTxtByBaseData(bool bool_0, string string_0, string string_1)
        {
            if (bool_0)
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\" + string_0 + @"\base.jpg";
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + string_0 + @"\base.jpg";
            }
            List<textID> list = new List<textID>();
            DataSet set = SQLiteHelper.ExecuteDataSet("select data from base where linkid='' and key='" + string_1 + "'", CommandType.Text);
            if (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && (set.Tables[0].Rows[0]["data"].ToString() != ""))
            {
                foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(set.Tables[0].Rows[0]["data"].ToString()))
                {
                    list.Add(textID.getValueFromJObject((JObject) token));
                }
            }
            return list;
        }

        public static JArray getUnitLotnum(string string_0)
        {
            return 正则获取(string_0, @"(\d+)([^/]+)", "");
        }

        public static string getUrl(string string_0)
        {
            if (string_0 == "")
            {
                return "";
            }
            try
            {
                Uri uri = new Uri(string_0);
                return uri.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string getUrlPath(string string_0)
        {
            Uri uri = new Uri(string_0);
            string str = "";
            string str2 = uri.Scheme + "://" + uri.Host + str + uri.LocalPath;
            int num = str2.LastIndexOf('/');
            string str3 = str2.Substring(0, num + 1);
            string str4 = str2.Substring(num + 1);
            if (!str4.Contains(".") && !(str4 == ""))
            {
                return (str3 + str4 + "/");
            }
            return str3;
        }

        public static string getValueByIndex(JObject jobject_0, int int_0)
        {
            int num = 0;
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                if (num == int_0)
                {
                    return pair.Key.ToString();
                }
                num++;
            }
            return "";
        }

        public static string getValueByIndex2(JObject jobject_0, int int_0)
        {
            int num = 0;
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                if (num == int_0)
                {
                    return pair.Value.ToString();
                }
                num++;
            }
            return "";
        }

        internal static void handleDB(List<string> list_0, string string_0)
        {
            try
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                builder.DataSource = string_0;
                SQLiteConnection connection = new SQLiteConnection();
                connection.ConnectionString = builder.ToString();
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                foreach (string str in list_0)
                {
                    command.CommandText = str;
                    command.ExecuteNonQuery();
                }
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static string handleHtml(string string_0)
        {
            string_0 = string_0.Replace("&nbsp;", " ");
            string_0 = HttpUtility.HtmlDecode(string_0);
            string_0 = 正则替换(string_0, @"[\r\n\t\v]", "");
            string_0 = 正则替换(string_0, " {2,}", " ");
            return string_0;
        }

        public static string handleHtml2(string string_0)
        {
            string_0 = 正则替换(string_0, @"[\r\n\t\v]", "");
            string_0 = 正则替换(string_0, " {2,}", " ");
            return string_0;
        }

        public static bool haveKey(string string_0, JObject jobject_0)
        {
            try
            {
                jobject_0[string_0].ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool haveKey(string string_0, Dictionary<string, string> dictionary_0)
        {
            try
            {
                dictionary_0[string_0].ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string Html标签过滤(string string_0, string string_1)
        {
            string str = string_0;
            try
            {
                foreach (string str2 in string_1.Split(new char[] { ',' }))
                {
                    string str3;
                    if (str2 == "all")
                    {
                        str3 = "</?[A-Za-z]+[^<>]*>";
                    }
                    else
                    {
                        str3 = "</?" + str2 + "[^<>]*>";
                    }
                    str = 正则替换(str, str3, "");
                }
            }
            catch (Exception)
            {
            }
            return str;
        }

        public static bool isContain(string[] string_0, string[] string_1)
        {
            bool flag = false;
            if (string_0.Length <= string_1.Length)
            {
                foreach (string str in string_0)
                {
                    foreach (string str2 in string_1)
                    {
                        if (str.ToString() == str2.ToString())
                        {
                            goto Label_0049;
                        }
                        flag = false;
                    }
                    goto Label_004B;
                Label_0049:
                    flag = true;
                Label_004B:
                    if (!flag)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        public static bool isContain(JArray jarray_0, JArray jarray_1)
        {
            bool flag = false;
            if (jarray_0.Count <= jarray_1.Count)
            {
                foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                {
                    using (IEnumerator<JToken> enumerator2 = ((IEnumerable<JToken>) jarray_1).GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            JToken current = enumerator2.Current;
                            if (token.ToString() == current.ToString())
                            {
                                goto Label_0054;
                            }
                            flag = false;
                        }
                        continue;
                    Label_0054:
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public static bool isSame(JArray jarray_0, JArray jarray_1)
        {
            bool flag = false;
            if (jarray_0.Count == jarray_1.Count)
            {
                foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                {
                    using (IEnumerator<JToken> enumerator2 = ((IEnumerable<JToken>) jarray_1).GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            JToken current = enumerator2.Current;
                            if (token.ToString() == current.ToString())
                            {
                                goto Label_0054;
                            }
                            flag = false;
                        }
                        goto Label_0064;
                    Label_0054:
                        flag = true;
                    }
                Label_0064:
                    if (!flag)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        public static bool isUser(JArray jarray_0, string string_0)
        {
            JObject obj2 = new JObject();
            obj2["acc"] = jarray_0;
            obj2["siteid"] = string_0;
            return (ERRypu.server("isuser", obj2)["r"].ToString() == "");
        }

        public static int LD(string string_0, string string_1)
        {
            int length = string_0.Length;
            int num2 = string_1.Length;
            int[,] numArray = new int[length + 1, num2 + 1];
            if (length == 0)
            {
                return num2;
            }
            if (num2 == 0)
            {
                return length;
            }
            int num4 = 0;
            while (num4 <= length)
            {
                numArray[num4, 0] = num4++;
            }
            int num5 = 0;
            while (num5 <= num2)
            {
                numArray[0, num5] = num5++;
            }
            for (int i = 1; i <= length; i++)
            {
                for (int j = 1; j <= num2; j++)
                {
                    int num3 = (string_1.Substring(j - 1, 1) == string_0.Substring(i - 1, 1)) ? 0 : 1;
                    numArray[i, j] = Math.Min(Math.Min((int) (numArray[i - 1, j] + 1), (int) (numArray[i, j - 1] + 1)), numArray[i - 1, j - 1] + num3);
                }
            }
            return numArray[length, num2];
        }

        private static int LD2(Dictionary<string, string> dictionary_0, Dictionary<string, string> dictionary_1)
        {
            int num = 0;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                foreach (KeyValuePair<string, string> pair2 in dictionary_1)
                {
                    if (!dictionary.ContainsKey(pair2.Key) && (((pair.Key == pair2.Key) || (MakeSingle(pair.Key) == pair2.Key)) || (MakePlural(pair.Key) == pair2.Key)))
                    {
                        num++;
                        dictionary[pair2.Key] = "";
                    }
                }
            }
            return num;
        }

        public static string listToStr(JArray jarray_0, string string_0, string string_1)
        {
            StringBuilder builder = new StringBuilder();
            foreach (JToken token in (IEnumerable<JToken>) jarray_0)
            {
                if (builder.Length == 0)
                {
                    builder.Append(string_1 + token.ToString() + string_1);
                }
                else
                {
                    builder.Append(string_0 + string_1 + token.ToString() + string_1);
                }
            }
            return builder.ToString();
        }

        public static string listToStr(List<string> list_0, string string_0, string string_1)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in list_0)
            {
                if (builder.Length == 0)
                {
                    builder.Append(string_1 + str.ToString() + string_1);
                }
                else
                {
                    builder.Append(string_0 + string_1 + str.ToString() + string_1);
                }
            }
            return builder.ToString();
        }

        public static string listToStr2(List<int> list_0, string string_0, string string_1)
        {
            StringBuilder builder = new StringBuilder();
            foreach (int num in list_0)
            {
                if (builder.Length == 0)
                {
                    builder.Append(string_1 + num.ToString() + string_1);
                }
                else
                {
                    builder.Append(string_0 + string_1 + num.ToString() + string_1);
                }
            }
            return builder.ToString();
        }

        public static string MakePlural(string string_0)
        {
            Regex regex = new Regex("(?<keep>[^aeiou])y$");
            Regex regex2 = new Regex("(?<keep>[aeiou]y)$");
            Regex regex3 = new Regex("(?<keep>[sxzh])$");
            Regex regex4 = new Regex("(?<keep>[^sxzhy])$");
            if (regex.IsMatch(string_0))
            {
                return regex.Replace(string_0, "${keep}ies");
            }
            if (regex2.IsMatch(string_0))
            {
                return regex2.Replace(string_0, "${keep}s");
            }
            if (regex3.IsMatch(string_0))
            {
                return regex3.Replace(string_0, "${keep}es");
            }
            if (regex4.IsMatch(string_0))
            {
                return regex4.Replace(string_0, "${keep}s");
            }
            return string_0;
        }

        public static string MakeSingle(string string_0)
        {
            Regex regex = new Regex("(?<keep>[^aeiou])ies$");
            Regex regex2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex regex3 = new Regex("(?<keep>[sxzh])es$");
            Regex regex4 = new Regex("(?<keep>[^sxzhyu])s$");
            if (regex.IsMatch(string_0))
            {
                return regex.Replace(string_0, "${keep}y");
            }
            if (regex2.IsMatch(string_0))
            {
                return regex2.Replace(string_0, "${keep}");
            }
            if (regex3.IsMatch(string_0))
            {
                return regex3.Replace(string_0, "${keep}");
            }
            if (regex4.IsMatch(string_0))
            {
                return regex4.Replace(string_0, "${keep}");
            }
            return string_0;
        }

        public static string md5_hash(Stream stream_0)
        {
            try
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                return BitConverter.ToString(provider.ComputeHash(stream_0)).Replace("-", "");
            }
            catch
            {
                return "";
            }
        }

        public static void Message(string string_0)
        {
            MessageBox.Show(string_0, "温馨提示");
        }

        internal static List<Dictionary<string, string>> queryDB(string string_0, string string_1)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            try
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                builder.DataSource = string_1;
                SQLiteConnection connection = new SQLiteConnection();
                connection.ConnectionString = builder.ToString();
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = string_0;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> item = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            item.Add(reader.GetName(i), reader[i].ToString().Replace("''", "'"));
                        }
                        list.Add(item);
                    }
                    reader.Close();
                    reader.Dispose();
                }
                command.Dispose();
                connection.Close();
                connection.Dispose();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string quotToC(string string_0)
        {
            return string_0.Replace("&quot;", "\"");
        }

        public static string quotToHtml(string string_0)
        {
            return string_0.Replace("\"", "&quot;");
        }

        public static string readTxt(string string_0)
        {
            using (StreamReader reader = new StreamReader(string_0))
            {
                string str = reader.ReadToEnd();
                reader.Close();
                return str;
            }
        }

        public static bool RemoteFileExists(string string_0)
        {
            bool flag = false;
            WebResponse response = null;
            try
            {
                response = WebRequest.Create(string_0).GetResponse();
                flag = response != null;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return flag;
        }

        public static void replaceABC(JObject jobject_0, string string_0)
        {
            JObject obj2 = new JObject();
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                obj2[pair.Key.Replace("^abc", string_0)] = new JObject();
                foreach (KeyValuePair<string, JToken> pair2 in (JObject) pair.Value)
                {
                    string introduced7 = pair2.Key.Replace("^abc", string_0);
                    obj2[pair.Key.Replace("^abc", string_0)][introduced7] = pair2.Value;
                }
            }
            jobject_0.RemoveAll();
            foreach (KeyValuePair<string, JToken> pair3 in obj2)
            {
                jobject_0[pair3.Key] = pair3.Value;
            }
        }

        public static void replaceABC2(JObject jobject_0, string string_0)
        {
            JObject obj2 = new JObject();
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                string introduced5 = pair.Key.Replace("^abc", string_0);
                obj2[introduced5] = pair.Value;
            }
            jobject_0.RemoveAll();
            foreach (KeyValuePair<string, JToken> pair2 in obj2)
            {
                jobject_0[pair2.Key] = pair2.Value;
            }
        }

        public static string returnDes(string string_0, JObject jobject_0)
        {
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                string_0 = string_0.Replace(pair.Key, pair.Value.ToString());
            }
            return string_0;
        }

        public static JArray ReverseArray(JArray jarray_0)
        {
            JArray array = new JArray();
            for (int i = jarray_0.Count - 1; i > -1; i--)
            {
                array.Add(jarray_0[i]);
            }
            return array;
        }

        public static JArray ReverseArray(string[] string_0)
        {
            JArray array = new JArray();
            for (int i = string_0.Length - 1; i > -1; i--)
            {
                array.Add(string_0[i]);
            }
            return array;
        }

        public static string setValue(string string_0)
        {
            return string_0;
        }

        public static int strSimilar(string string_0, string string_1)
        {
            double num;
            string_0 = string_0.Trim().ToLower();
            string_1 = string_1.Trim().ToLower();
            double length = string_0.Length;
            double num3 = string_1.Length;
            if (length > num3)
            {
                num = length;
            }
            else
            {
                num = num3;
            }
            if (num == 0.0)
            {
                num = 1.0;
            }
            double num4 = 1.0 - (((double) LD(string_0, string_1)) / num);
            return (int) (num4 * 100.0);
        }

        public static int strSimilar2(string string_0, string string_1)
        {
            double num;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in 转成Dic(正则获取所有(string_0, "([a-z0-9]+)", ""), 0, 2))
            {
                dictionary[pair.Key.ToLower()] = "";
            }
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair2 in 转成Dic(正则获取所有(string_1, "([a-z0-9]+)", ""), 0, 2))
            {
                dictionary2[pair2.Key.ToLower()] = "";
            }
            double count = dictionary.Count;
            double num3 = dictionary2.Count;
            if (count > num3)
            {
                num = count;
            }
            else
            {
                num = num3;
            }
            if (num == 0.0)
            {
                num = 1.0;
            }
            double num4 = ((double) LD2(dictionary, dictionary2)) / num;
            return (int) (num4 * 100.0);
        }

        public static void strToDic(Dictionary<string, string> dictionary_0, string string_0, string string_1)
        {
            if (string_0 != null)
            {
                MatchCollection matchs = Regex.Matches(string_0, string_1);
                if (matchs.Count > 0)
                {
                    foreach (Match match in matchs)
                    {
                        dictionary_0[match.Result("$1").Trim()] = match.Result("$2");
                    }
                }
                dictionary_0.Remove("Path");
                dictionary_0.Remove("Domain");
                dictionary_0.Remove("Expires");
            }
        }

        public static string UnicodeToGB(string string_0)
        {
            MatchCollection matchs = Regex.Matches(string_0, @"\\u([\w]{4})");
            if ((matchs != null) && (matchs.Count > 0))
            {
                foreach (Match match in matchs)
                {
                    string oldValue = match.Value;
                    string str2 = oldValue.Substring(2);
                    byte[] bytes = new byte[2];
                    int num = Convert.ToInt32(str2.Substring(0, 2), 0x10);
                    int num2 = Convert.ToInt32(str2.Substring(2), 0x10);
                    bytes[0] = (byte) num2;
                    bytes[1] = (byte) num;
                    string_0 = string_0.Replace(oldValue, Encoding.Unicode.GetString(bytes));
                }
            }
            return string_0;
        }

        public static string UnZip(string string_0)
        {
            byte[] buffer = new byte[string_0.Length];
            int num = 0;
            foreach (char ch in string_0.ToCharArray())
            {
                buffer[num++] = (byte) ch;
            }
            MemoryStream stream = new MemoryStream(buffer);
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
            buffer = new byte[buffer.Length];
            int capacity = stream2.Read(buffer, 0, buffer.Length);
            StringBuilder builder = new StringBuilder(capacity);
            for (int i = 0; i < capacity; i++)
            {
                builder.Append((char) buffer[i]);
            }
            stream2.Close();
            stream.Close();
            stream2.Dispose();
            stream.Dispose();
            return builder.ToString();
        }

        public static bool UnZip(string string_0, string string_1)
        {
            try
            {
                ZipEntry entry;
                if (!File.Exists(string_0))
                {
                    return false;
                }
                string_1 = string_1.Replace("/", @"\");
                if (!string_1.EndsWith(@"\"))
                {
                    string_1 = string_1 + @"\";
                }
                if (!Directory.Exists(string_1))
                {
                    Directory.CreateDirectory(string_1);
                }
                ZipInputStream stream = new ZipInputStream(File.OpenRead(string_0));
                while ((entry = stream.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);
                    if (directoryName != string.Empty)
                    {
                        Directory.CreateDirectory(string_1 + directoryName);
                    }
                    if (!(fileName != string.Empty))
                    {
                        continue;
                    }
                    FileStream stream2 = File.Create(string_1 + entry.Name);
                    int count = 0x800;
                    byte[] buffer = new byte[0x800];
                    goto Label_00CF;
                Label_00C3:
                    stream2.Write(buffer, 0, count);
                Label_00CF:
                    count = stream.Read(buffer, 0, buffer.Length);
                    if (count > 0)
                    {
                        goto Label_00C3;
                    }
                    stream2.Close();
                }
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void writeLog(Exception exception_0)
        {
            string str = "";
            string str2 = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            if (exception_0 != null)
            {
                str = string.Format(str2 + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n", exception_0.GetType().Name, exception_0.Message, exception_0.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", exception_0);
            }
            if (exception_0.Data.Contains("url"))
            {
                ERRypu.submitQuestion(exception_0.Data["url"].ToString(), "", "", str);
            }
            else
            {
                ERRypu.submitQuestion("", "", "", str);
            }
        }

        public static void writeTxt(string string_0, string string_1)
        {
            FileStream stream = new FileStream(string_0, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(string_1);
            writer.Flush();
            writer.Dispose();
            stream.Dispose();
        }

        public static string Zip(string string_0)
        {
            byte[] buffer = new byte[string_0.Length];
            int num = 0;
            foreach (char ch in string_0.ToCharArray())
            {
                buffer[num++] = (byte) ch;
            }
            MemoryStream stream = new MemoryStream();
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.Close();
            buffer = stream.ToArray();
            StringBuilder builder = new StringBuilder(buffer.Length);
            foreach (byte num2 in buffer)
            {
                builder.Append((char) num2);
            }
            stream.Close();
            stream2.Dispose();
            stream.Dispose();
            return builder.ToString();
        }

        public static string 获取图片完整路径(string string_0, string string_1)
        {
            if (string_0 == "")
            {
                return "";
            }
            string str = string_0.Substring(0, 1);
            string str2 = string_0.Substring(1, 1);
            switch (str2)
            {
                case ".":
                case "":
                    str2 = str;
                    break;
            }
            string path = PzFuEX.Jo4304 + @"cc\" + string_1 + @"\img\" + str + @"\" + str2;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return (path + @"\" + string_0);
        }

        public static string 前后截取(string string_0, string string_1, string string_2)
        {
            try
            {
                int index = string_0.IndexOf(string_1);
                if (index < 0)
                {
                    return "";
                }
                int startIndex = index + string_1.Length;
                int length = string_0.IndexOf(string_2, startIndex) - startIndex;
                return string_0.Substring(startIndex, length);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string 正则获取(string string_0, string string_1)
        {
            try
            {
                JArray array = 正则获取(string_0, string_1, "");
                return ((array.Count > 0) ? array[0].ToString() : "");
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static JArray 正则获取(string string_0, string string_1, string string_2)
        {
            JArray array = new JArray();
            try
            {
                Regex regex = new Regex(string_1, RegexOptions.IgnoreCase);
                foreach (Group group in regex.Match(string_0, string_0.IndexOf(string_2)).Groups)
                {
                    array.Add(group.Value.Trim());
                }
                array.RemoveAt(0);
            }
            catch (Exception)
            {
            }
            return array;
        }

        public static JArray 正则获取所有(string string_0, string string_1, string string_2)
        {
            JArray array = new JArray();
            try
            {
                Regex regex = new Regex(string_1, RegexOptions.IgnoreCase);
                foreach (Match match in regex.Matches(string_0, string_0.IndexOf(string_2)))
                {
                    JArray item = new JArray();
                    foreach (Group group in match.Groups)
                    {
                        item.Add(group.Value.Trim());
                    }
                    item.RemoveAt(0);
                    array.Add(item);
                }
            }
            catch (Exception)
            {
            }
            return array;
        }

        public static string 正则截取(string string_0, string string_1, string string_2)
        {
            try
            {
                Regex regex = new Regex(string_1, RegexOptions.IgnoreCase);
                string str = regex.Match(string_0).Value;
                int startIndex = string_0.IndexOf(str) + str.Length;
                Regex regex2 = new Regex(string_2, RegexOptions.IgnoreCase);
                int length = string_0.IndexOf(regex2.Match(string_0, startIndex).Value, startIndex) - startIndex;
                return string_0.Substring(startIndex, length);
            }
            catch
            {
                return "";
            }
        }

        public static bool 正则匹配(string string_0, string string_1, string string_2)
        {
            bool flag = false;
            try
            {
                flag = new Regex(string_1, RegexOptions.IgnoreCase).IsMatch(string_0, string_0.IndexOf(string_2));
            }
            catch
            {
            }
            return flag;
        }

        public static string 正则替换(string string_0, string string_1, string string_2)
        {
            string str = string_0;
            try
            {
                str = new Regex(string_1, RegexOptions.IgnoreCase).Replace(string_0, string_2);
            }
            catch
            {
            }
            return str;
        }

        public static string 正则替换(string string_0, string string_1, string string_2, int int_0, int int_1)
        {
            string str = string_0;
            try
            {
                str = new Regex(string_1, RegexOptions.IgnoreCase).Replace(string_0, string_2, int_0, int_1);
            }
            catch
            {
            }
            return str;
        }

        public static Dictionary<string, string> 转成Dic(JArray jarray_0, int int_0, int int_1)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                if (jarray_0.Count <= 0)
                {
                    return dictionary;
                }
                if (((JArray) jarray_0[0]).Count > int_1)
                {
                    foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                    {
                        dictionary[token[int_0].ToString()] = token[int_1].ToString();
                    }
                    return dictionary;
                }
                foreach (JToken token2 in (IEnumerable<JToken>) jarray_0)
                {
                    dictionary[token2[int_0].ToString()] = "";
                }
            }
            catch (Exception)
            {
            }
            return dictionary;
        }

        public static JObject 转成JObject(JArray jarray_0, int int_0, int int_1)
        {
            JObject obj2 = new JObject();
            try
            {
                if (jarray_0.Count <= 0)
                {
                    return obj2;
                }
                if (((JArray) jarray_0[0]).Count > int_1)
                {
                    foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                    {
                        obj2[token[int_0].ToString()] = token[int_1].ToString();
                    }
                    return obj2;
                }
                foreach (JToken token2 in (IEnumerable<JToken>) jarray_0)
                {
                    obj2[token2[int_0].ToString()] = "";
                }
            }
            catch (Exception)
            {
            }
            return obj2;
        }

        public static JArray 转成一维JArray(JArray jarray_0, int int_0)
        {
            JArray array = new JArray();
            try
            {
                foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                {
                    array.Add(token[int_0]);
                }
            }
            catch (Exception)
            {
            }
            return array;
        }

        public static JArray 转成一维JArray(JObject jobject_0, int int_0)
        {
            JArray array = new JArray();
            try
            {
                foreach (KeyValuePair<string, JToken> pair in jobject_0)
                {
                    if (int_0 == 0)
                    {
                        array.Add(pair.Key);
                    }
                    else
                    {
                        array.Add(pair.Value);
                    }
                }
            }
            catch (Exception)
            {
            }
            return array;
        }

        public static List<string> 转成一维List(JArray jarray_0, int int_0)
        {
            List<string> list = new List<string>();
            try
            {
                foreach (JToken token in (IEnumerable<JToken>) jarray_0)
                {
                    list.Add(token[int_0].ToString());
                }
            }
            catch (Exception)
            {
            }
            return list;
        }
    }
}


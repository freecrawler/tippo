namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BaseOwn
    {
        public static List<cate> getCatesByFvalueFromBaseData(string string_0, string string_1, string string_2)
        {
            List<cate> list = new List<cate>();
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\" + string_2 + @"\cate.jpg";
            string str = "";
            if (string_0.Length > 0)
            {
                str = "select * from cate where f_value='" + string_0 + "'";
            }
            else
            {
                str = "select * from cate";
            }
            DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                string str2 = (string_1 == "zh-cn") ? "text_cn" : "text_en";
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    cate item = new cate();
                    item.value.id = row["value"].ToString().Trim();
                    item.value.text["zh-cn"] = row["text_cn"].ToString().Trim();
                    item.value.text["en"] = row["text_en"].ToString().Trim();
                    item.value.text[string_1] = row[str2].ToString().Trim();
                    item.isLeaf = Convert.ToBoolean(row["is_leaf"]);
                    item.fvalue = row["f_value"].ToString().Trim();
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<cate> getCatesByStr(string string_0)
        {
            List<cate> list = new List<cate>();
            foreach (JObject obj2 in (IEnumerable<JToken>) JArray.Parse(string_0))
            {
                list.Add(cate.getValueFromJObject(obj2));
            }
            return list;
        }

        public static bool getCatIDbypubUpDic(List<cate> list_0, string string_0, Dictionary<string, string> dictionary_0)
        {
            bool flag = false;
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                flag = false;
                if (list_0[list_0.Count - 1].value.text[string_0].Trim() == pair.Value.Trim())
                {
                    List<cate> list = getCatesByStr(pair.Key);
                    for (int i = 0; i < list_0.Count; i++)
                    {
                        flag = false;
                        using (List<cate>.Enumerator enumerator2 = list.GetEnumerator())
                        {
                            cate current;
                            while (enumerator2.MoveNext())
                            {
                                current = enumerator2.Current;
                                if (list_0[i].value.text[string_0].Trim() == current.value.text[string_0].Trim())
                                {
                                    if (i == 0)
                                    {
                                        flag = true;
                                    }
                                    else if (list_0[i - 1].value.id == current.fvalue)
                                    {
                                        flag = true;
                                    }
                                    if (flag)
                                    {
                                        goto Label_00EF;
                                    }
                                }
                            }
                            continue;
                        Label_00EF:
                            list_0[i].value.id = current.value.id;
                        }
                    }
                    if (flag)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        public static cate getMapCate(string string_0, string string_1, List<cate> list_0, bool bool_0, int int_0)
        {
            int num = 0;
            int num2 = -1;
            cate cate = new cate();
            foreach (cate cate2 in list_0)
            {
                num = func.strSimilar(cate2.value.text[string_1], string_0);
                if (num > num2)
                {
                    cate = cate2;
                    num2 = num;
                }
            }
            if (!bool_0 && (num2 < int_0))
            {
                cate = null;
            }
            return cate;
        }
    }
}


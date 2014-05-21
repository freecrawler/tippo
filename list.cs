namespace client
{
    using csExWB;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Threading;

    public class list : fatherObj
    {
        public List<int> ids = new List<int>();
        private int int_0;
        public bool isAllSelect;
        public bool isCSV;
        public string searchName = "";
        public string selectpid = "";
        public string ser = " where state !=0 ";

        public event myEventEventHandler handleEdit;

        public list(cEXWB cEXWB_0)
        {
            base.webbrowser = cEXWB_0;
            base.defaultLanguage = "zh-cn";
            base.editType = "list";
        }

        public void delete()
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.storeId, @"\pro.jpg" });
            StringBuilder builder = new StringBuilder();
            foreach (int num in this.ids)
            {
                builder.Append(num + ",");
            }
            SQLiteHelper.ExecuteNonQuery("delete from product where rowid in (" + builder.ToString().TrimEnd(new char[] { ',' }) + ") ", CommandType.Text);
            this.ids = new List<int>();
            this.isAllSelect = false;
        }

        public string getgridheader()
        {
            JArray array = new JArray();
            foreach (KeyValuePair<string, baseP> pair in base.attrs)
            {
                JObject item = new JObject();
                item["field"] = pair.Value.key;
                item["title"] = pair.Value.name;
                item["width"] = pair.Value.listWidth;
                if ((pair.Key == "pics") || (pair.Key == "proimg"))
                {
                    item["field"] = "err";
                    item["title"] = "错误信息";
                    item["width"] = 100;
                }
                array.Add(item);
            }
            return ("[" + JsonConvert.SerializeObject(array) + "]");
        }

        public void getProductCount()
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.storeId, @"\pro.jpg" });
            DataSet set = SQLiteHelper.ExecuteDataSet("select count(rowid) from product " + this.ser, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                this.int_0 = text.toInt(set.Tables[0].Rows[0][0].ToString());
            }
        }

        public void getSearch()
        {
            this.ser = "where";
            switch (base.state)
            {
                case 1:
                    this.ser = this.ser + " state=1 ";
                    break;

                case 2:
                    this.ser = this.ser + " state=2 ";
                    break;

                case 3:
                    this.ser = this.ser + " state=3 ";
                    break;

                case 4:
                    this.ser = this.ser + " state!=0 ";
                    break;
            }
            if ((this.searchName != "") && (this.searchName != "请输入产品名称"))
            {
                this.ser = this.ser + "and f32 like '%" + this.searchName.Replace("'", "''") + "%' ";
            }
        }

        public void handleUserChange(string string_0)
        {
            if (string_0.StartsWith("allselect"))
            {
                this.ids = new List<int>();
                if (!Convert.ToBoolean(string_0.Replace("allselect", "")))
                {
                    this.isAllSelect = false;
                }
                else
                {
                    this.isAllSelect = true;
                    SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.storeId, @"\pro.jpg" });
                    DataSet set = SQLiteHelper.ExecuteDataSet("select rowid from product " + this.ser, CommandType.Text);
                    if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                    {
                        foreach (DataRow row in set.Tables[0].Rows)
                        {
                            this.ids.Add(text.toInt(row[0].ToString()));
                        }
                    }
                }
            }
            else if (string_0.StartsWith("check"))
            {
                int item = Convert.ToInt32(string_0.Replace("check", ""));
                if (!this.ids.Contains(item))
                {
                    this.ids.Add(item);
                }
            }
            else if (string_0.StartsWith("uncheck"))
            {
                int num2 = Convert.ToInt32(string_0.Replace("uncheck", ""));
                if (this.ids.Contains(num2))
                {
                    this.ids.Remove(num2);
                }
            }
            else if (string_0.StartsWith("editpro"))
            {
                int num3 = Convert.ToInt32(string_0.Replace("editpro", ""));
                myEventArgs e = new myEventArgs();
                e.productId = num3;
                this.myEventEventHandler_0(this, e);
            }
            else if (string_0.StartsWith("lookoldpro"))
            {
                PzFuEX.openUrl(string_0.Replace("lookoldpro", ""));
            }
        }

        public string showList(int int_1, int int_2)
        {
            if (base.storeId == 0)
            {
                return "";
            }
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.storeId, @"\pro.jpg" });
            this.getProductCount();
            JObject obj2 = new JObject();
            JArray array = new JArray();
            obj2["total"] = this.int_0;
            obj2["rows"] = array;
            StringBuilder builder = new StringBuilder();
            if (this.int_0 > 0)
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("select rowid,url,err,state");
                foreach (KeyValuePair<string, baseP> pair in base.attrs)
                {
                    builder2.Append("," + pair.Value.dataBaseFieldForList);
                }
                builder2.Append(string.Concat(new object[] { " from product ", this.ser, " order by rowid desc limit ", (int_1 - 1) * int_2, ",", int_2 }));
                DataSet set = SQLiteHelper.ExecuteDataSet(builder2.ToString(), CommandType.Text);
                if (set.Tables.Count > 0)
                {
                    int num = 0;
                    foreach (DataRow row in set.Tables[0].Rows)
                    {
                        if (this.ids.Contains(text.toInt(row["rowid"].ToString())))
                        {
                            builder.Append(num.ToString() + ",");
                        }
                        JObject item = new JObject();
                        item["id"] = row["rowid"].ToString();
                        string str = "";
                        string str3 = row["state"].ToString();
                        if (str3 != null)
                        {
                            if (str3 != "1")
                            {
                                if (!(str3 == "2"))
                                {
                                    if (str3 == "3")
                                    {
                                        str = this.isCSV ? "导出失败" : "上传失败";
                                    }
                                }
                                else
                                {
                                    str = this.isCSV ? "导出成功" : "上传成功";
                                }
                            }
                            else
                            {
                                str = this.isCSV ? "未导出" : "未上传";
                            }
                        }
                        item["state"] = str;
                        item["url"] = row["url"].ToString();
                        item["err"] = row["err"].ToString();
                        foreach (KeyValuePair<string, baseP> pair2 in base.attrs)
                        {
                            if ((pair2.Key != "pics") && (pair2.Key != "proimg"))
                            {
                                item[pair2.Value.key] = row[pair2.Value.dataBaseFieldForList].ToString();
                            }
                            else
                            {
                                string str2 = row[pair2.Value.dataBaseFieldForList].ToString().Replace("$a1b2c3$", PzFuEX.Jo4304);
                                if (str2 == "")
                                {
                                    str2 = PzFuEX.Jo4304 + @"cc\no.gif";
                                }
                                item["pics"] = func.getUrl(str2);
                            }
                        }
                        array.Add(item);
                        num++;
                    }
                }
            }
            this.selectpid = builder.ToString().TrimEnd(new char[] { ',' });
            return JsonConvert.SerializeObject(obj2);
        }
    }
}


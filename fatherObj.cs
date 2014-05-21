namespace client
{
    using csExWB;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class fatherObj
    {
        public string AcceptLanguage = "us-en";
        public List<string> allowReg = new List<string>();
        public Dictionary<string, string> allPicHtml;
        public Dictionary<string, string> allPics = new Dictionary<string, string>();
        public Dictionary<string, client.postData> allUpPics = new Dictionary<string, client.postData>();
        public Dictionary<string, baseP> attrs = new Dictionary<string, baseP>();
        public List<string> banReg = new List<string>();
        public cateP cate;
        public cateAttrP cateAttr;
        public CSV csv;
        public string currencyUnit = "美元";
        public customAttrP customAttr;
        public string defaultLanguage = "en";
        public desP des;
        public int edIndex;
        public string editType = "";
        public string encode = "";
        public string fromSiteId = "";
        public groupP group;
        public string htmlCode = "";
        public http Http;
        public HttpVer httpVer;
        public int id;
        public bool isThreding;
        public bool isWater;
        public keywordsP keywords;
        public string listHtmlCode = "";
        public string loginSuccess = "";
        public string loginUrl = "";
        public string loginVeryUrl = "";
        public nameP name;
        public packageP package;
        public picsP pics;
        public string picSavePath = "";
        public string pid = "";
        public client.postData postData;
        public Dictionary<string, bool> postErr = new Dictionary<string, bool>();
        public priceP price;
        public Dictionary<string, string> pubHtml = new Dictionary<string, string>();
        public Dictionary<string, string> pubUpDic = new Dictionary<string, string>();
        public saleAttrP saleAttr;
        public saleMethodP saleMethod;
        public string secondLanguage = "";
        public shipP ship;
        public shipTempP shipTemp;
        public string siteId = "";
        public int state;
        public store Store;
        public int storeId;
        public summaryP summary;
        public string toLanguage = "";
        public string toSecondLanguage = "";
        public string toSiteId = "";
        public Dictionary<string, string> translateText = new Dictionary<string, string>();
        public unitP unit;
        public string url = "";
        public string urlInfo = "";
        public validityDayP validityDay;
        public bool waterIsCenter;
        public bool waterIsDown;
        public string waterStr = "";
        public cEXWB webbrowser;
        public weightP weight;
        public ExcelHelper xls;

        public void getAllPics()
        {
            if (this.editType != "batch")
            {
                this.allPics = new Dictionary<string, string>();
            }
            if (ERRypu.isDownPic)
            {
                foreach (string str in this.pics.value.allPic)
                {
                    if (str != "")
                    {
                        this.allPics[str] = str;
                    }
                }
            }
            if (ERRypu.isDownImg && this.attrs.ContainsKey("saleAttr"))
            {
                foreach (option option in this.saleAttr.value)
                {
                    foreach (optionValue value2 in option.optionValues)
                    {
                        if (value2.pic != "")
                        {
                            this.allPics[value2.pic] = value2.pic;
                        }
                    }
                }
            }
            if (ERRypu.isDowmdes)
            {
                this.des.getPics();
                foreach (KeyValuePair<string, string> pair in this.des.AllPics)
                {
                    this.allPics[pair.Key] = pair.Key;
                }
            }
        }

        public void getDownAllPics()
        {
            this.allPics = new Dictionary<string, string>();
            if (ERRypu.isDownPic)
            {
                foreach (string str in this.pics.value.allPic)
                {
                    if (str != "")
                    {
                        this.allPics[str] = "";
                    }
                }
            }
            if (ERRypu.isDownImg && this.attrs.ContainsKey("saleAttr"))
            {
                foreach (option option in this.saleAttr.value)
                {
                    foreach (optionValue value2 in option.optionValues)
                    {
                        if (value2.pic != "")
                        {
                            this.allPics[value2.pic] = "";
                        }
                    }
                }
            }
            if (ERRypu.isDowmdes)
            {
                this.des.getPics();
                foreach (KeyValuePair<string, string> pair in this.des.AllPics)
                {
                    this.allPics[pair.Key] = "";
                }
            }
        }

        public virtual void getReg()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + this.siteId + @"\attr.jpg";
            DataSet set = SQLiteHelper.ExecuteDataSet("select data from attr where linkid='" + this.cate.getLastCateID + "'", CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if (row["data"].ToString() != "")
                    {
                        try
                        {
                            JObject obj2 = JObject.Parse(row["data"].ToString());
                            this.banReg = func.getListFromJArray((JArray) obj2["banReg"]);
                            this.allowReg = func.getListFromJArray((JArray) obj2["allowReg"]);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public void loadFromDataBase()
        {
            if (this.id > 0)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", this.storeId, @"\pro.jpg" });
                string str = "";
                if (this.editType == "edit")
                {
                    str = "select * from product where rowid=" + this.id;
                }
                else if (this.editType == "batch")
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("select rowid,currencyunit,state,fromlanguage,fromSiteId");
                    foreach (KeyValuePair<string, baseP> pair in this.attrs)
                    {
                        if (pair.Value.isBatch)
                        {
                            builder.Append("," + pair.Value.dataBaseFieldForSave);
                        }
                    }
                    builder.Append(" from product where rowid=" + this.id);
                    str = builder.ToString();
                }
                else
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("select rowid,currencyunit,fromlanguage,state,fromSiteId");
                    foreach (KeyValuePair<string, baseP> pair2 in this.attrs)
                    {
                        builder2.Append("," + pair2.Value.dataBaseFieldForSave);
                    }
                    builder2.Append(" from product where rowid=" + this.id);
                    str = builder2.ToString();
                }
                DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    JObject obj2 = new JObject();
                    obj2["1"] = PzFuEX.Jo4304;
                    string newValue = JsonConvert.SerializeObject(obj2).Replace("{\"1\":\"", "").Replace("\"}", "");
                    DataRow row = set.Tables[0].Rows[0];
                    this.state = text.toInt(row["state"].ToString().Trim());
                    this.fromSiteId = row["fromSiteId"].ToString().Trim();
                    if (this.editType == "map")
                    {
                        this.defaultLanguage = row["fromlanguage"].ToString().Trim();
                        this.currencyUnit = row["currencyunit"].ToString().Trim();
                    }
                    if (this.editType == "edit")
                    {
                        this.pid = row["pid"].ToString().Trim();
                        this.url = row["url"].ToString().Trim();
                    }
                    foreach (KeyValuePair<string, baseP> pair3 in this.attrs)
                    {
                        if (((this.editType != "batch") || ((this.editType == "batch") && pair3.Value.isBatch)) && (row[pair3.Value.dataBaseFieldForSave].ToString() != ""))
                        {
                            string str3 = row[pair3.Value.dataBaseFieldForSave].ToString();
                            if ((pair3.Key == "pics") || (pair3.Key == "saleAttr"))
                            {
                                str3 = str3.Replace("$a1b2c3$", newValue);
                            }
                            if (pair3.Key == "des")
                            {
                                str3 = str3.Replace("$a1b2c3$", PzFuEX.Jo4304);
                            }
                            pair3.Value.getValueFromDataBase(str3);
                        }
                    }
                }
            }
        }

        public bool loginDown()
        {
            if (PzFuEX.allDownCookies.ContainsKey(this.siteId) && (PzFuEX.allDownCookies[this.siteId] != null))
            {
                this.Http = new http();
                this.Http.Cookies = PzFuEX.allDownCookies[this.siteId];
                this.Http.Encode = this.encode;
                new Uri(this.loginUrl);
                this.Http.url = this.loginVeryUrl;
                if (func.正则匹配(this.Http.DownHtml(), this.loginSuccess, ""))
                {
                    return true;
                }
            }
            if (myCookie.getCookie(this.loginUrl, this.loginSuccess, "", ""))
            {
                PzFuEX.allDownCookies[this.siteId] = myCookie.cookies;
                return true;
            }
            PzFuEX.allDownCookies[this.siteId] = null;
            return false;
        }

        public bool loginUp()
        {
            this.Http = new http();
            if (this.httpVer == HttpVer.ver0)
            {
                this.Http.Continue = false;
            }
            if (PzFuEX.allCookies.ContainsKey(this.storeId) && (PzFuEX.allCookies[this.storeId] != null))
            {
                this.Http.Cookies = PzFuEX.allCookies[this.storeId];
                this.Http.Encode = this.encode;
                this.Http.AcceptLanguage = this.AcceptLanguage;
                if ((PzFuEX._4YIz1F[this.siteId].isOwn && (this.siteId != "128")) && ((this.siteId != "197") && (this.siteId != "198")))
                {
                    this.Http.url = func.getUrlPath(this.loginUrl) + this.loginVeryUrl;
                }
                else
                {
                    this.Http.url = this.loginVeryUrl;
                }
                if (func.正则匹配(this.Http.DownHtml(), this.loginSuccess, ""))
                {
                    return true;
                }
            }
            if (!this.isThreding && myCookie.getCookie(this.loginUrl, this.loginSuccess, this.Store.userName, this.Store.siteId))
            {
                PzFuEX.allCookies[this.storeId] = myCookie.cookies;
                this.Http.Cookies = PzFuEX.allCookies[this.storeId];
                if ((this.Store.userName.Length == 0) && (myCookie.userName.Length > 0))
                {
                    this.Store.userName = myCookie.userName;
                    this.Store.saveDataBase();
                }
                return true;
            }
            PzFuEX.allCookies[this.storeId] = null;
            return false;
        }

        public void saveToDataBase()
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", this.storeId, @"\pro.jpg" });
            StringBuilder builder = new StringBuilder();
            JObject obj2 = new JObject();
            obj2["1"] = PzFuEX.Jo4304;
            string oldValue = JsonConvert.SerializeObject(obj2).Replace("{\"1\":\"", "").Replace("\"}", "");
            if (this.id == 0)
            {
                builder.Append("insert into product (url,state,fromsiteid,currencyunit,fromlanguage");
                foreach (KeyValuePair<string, baseP> pair in this.attrs)
                {
                    builder.Append("," + pair.Value.dataBaseFieldForSave);
                    if (pair.Value.dataBaseFieldForList != pair.Value.dataBaseFieldForSave)
                    {
                        builder.Append("," + pair.Value.dataBaseFieldForList);
                    }
                    if ((pair.Value.dataBaseFieldForSearch != pair.Value.dataBaseFieldForSave) && (pair.Value.dataBaseFieldForSearch != pair.Value.dataBaseFieldForList))
                    {
                        builder.Append("," + pair.Value.dataBaseFieldForSearch);
                    }
                }
                builder.Append(string.Concat(new object[] { ") values ('", this.url, "',", this.state, ",'", this.fromSiteId, "','", this.currencyUnit, "','", this.defaultLanguage, "'" }));
                foreach (KeyValuePair<string, baseP> pair2 in this.attrs)
                {
                    string str2 = pair2.Value.getDataBaseFromValue();
                    if ((pair2.Key == "pics") || (pair2.Key == "saleAttr"))
                    {
                        str2 = str2.Replace(oldValue, "$a1b2c3$");
                    }
                    if (pair2.Key == "des")
                    {
                        str2 = str2.Replace(PzFuEX.Jo4304, "$a1b2c3$");
                    }
                    builder.Append(",'" + str2.Replace("'", "''").Replace("\r", "").Replace("\n", "") + "'");
                    if (pair2.Value.dataBaseFieldForList != pair2.Value.dataBaseFieldForSave)
                    {
                        str2 = pair2.Value.getListCacheFromValue();
                        if (((pair2.Key == "pics") || (pair2.Key == "saleAttr")) || (pair2.Key == "des"))
                        {
                            str2 = str2.Replace(PzFuEX.Jo4304, "$a1b2c3$");
                        }
                        builder.Append(",'" + str2.Replace("'", "''").Replace("\r", "").Replace("\n", "") + "'");
                    }
                    if ((pair2.Value.dataBaseFieldForSearch != pair2.Value.dataBaseFieldForSave) && (pair2.Value.dataBaseFieldForSearch != pair2.Value.dataBaseFieldForList))
                    {
                        builder.Append(",'" + pair2.Value.getSearchCacheFromValue().Replace("'", "''").Replace("\r", "").Replace("\n", "") + "'");
                    }
                }
                builder.Append(")");
            }
            else
            {
                builder.Append("UPDATE product  SET state=" + this.state + ", ");
                int num = 0;
                foreach (KeyValuePair<string, baseP> pair3 in this.attrs)
                {
                    if ((this.editType != "batch") || ((this.editType == "batch") && pair3.Value.isBatch))
                    {
                        string str3 = ",";
                        if (num == 0)
                        {
                            str3 = "";
                        }
                        string str4 = pair3.Value.getDataBaseFromValue();
                        if ((pair3.Key == "pics") || (pair3.Key == "saleAttr"))
                        {
                            str4 = str4.Replace(oldValue, "$a1b2c3$");
                        }
                        if (pair3.Key == "des")
                        {
                            str4 = str4.Replace(PzFuEX.Jo4304, "$a1b2c3$");
                        }
                        builder.Append(str3 + pair3.Value.dataBaseFieldForSave + "='" + str4.Replace("'", "''").Replace("\r", "").Replace("\n", "") + "'");
                        num++;
                        if (pair3.Value.dataBaseFieldForList != pair3.Value.dataBaseFieldForSave)
                        {
                            str4 = pair3.Value.getListCacheFromValue();
                            if (((pair3.Key == "pics") || (pair3.Key == "saleAttr")) || (pair3.Key == "des"))
                            {
                                str4 = str4.Replace(PzFuEX.Jo4304, "$a1b2c3$");
                            }
                            builder.Append("," + pair3.Value.dataBaseFieldForList + "='" + str4.Replace("'", "''").Replace("\r", "").Replace("\n", "") + "'");
                        }
                        if ((pair3.Value.dataBaseFieldForSearch != pair3.Value.dataBaseFieldForSave) && (pair3.Value.dataBaseFieldForSearch != pair3.Value.dataBaseFieldForList))
                        {
                            builder.Append("," + pair3.Value.dataBaseFieldForSearch + "='" + pair3.Value.getSearchCacheFromValue().Replace("'", "''").Replace("\r", "").Replace("\n", "") + "'");
                        }
                    }
                }
                builder.Append(" where rowid=" + this.id);
            }
            SQLiteHelper.ExecuteNonQuery(builder.ToString(), CommandType.Text);
        }
    }
}


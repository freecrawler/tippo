namespace client
{
    using csExWB;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    public class down
    {
        public Dictionary<string, string> errUrls = new Dictionary<string, string>();
        public int i;
        public int ii;
        public Dictionary<string, string> inputUrls = new Dictionary<string, string>();
        public bool isOk;
        public bool isReDown;
        public int j = 1;
        public int si;
        public store sto;
        public int storeID;
        public string toLanguage = "";
        public int totallPic;
        public Dictionary<string, string> urls = new Dictionary<string, string>();
        public cEXWB webbrowser;

        private void method_0()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\url.jpg";
            string str = "delete from url";
            SQLiteHelper.ExecuteNonQuery(str, CommandType.Text);
        }

        private int method_1(string string_0)
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", this.storeID, @"\pro.jpg" });
            DataSet set = SQLiteHelper.ExecuteDataSet("select rowid from product where url ='" + string_0 + "'", CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                return text.toInt(set.Tables[0].Rows[0]["rowid"].ToString());
            }
            return 0;
        }

        private void method_2(ref string string_0, ref string string_1)
        {
            string_0 = "";
            string_1 = "";
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\url.jpg";
            string str = "select url,data from url where state=0 limit 1";
            DataTable table = SQLiteHelper.ExecuteDataSet(str, 1).Tables[0];
            if (table.Rows.Count > 0)
            {
                string_0 = table.Rows[0][0].ToString();
                string_1 = table.Rows[0][1].ToString();
            }
        }

        private void method_3(string string_0)
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\url.jpg";
            SQLiteHelper.ExecuteNonQuery("UPDATE url SET state=true WHERE url='" + string_0.Replace("'", "''") + "' ", CommandType.Text);
        }

        public void saveUrls()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\url.jpg";
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in this.urls)
            {
                builder.Append(("INSERT INTO url (url,data) VALUES ('" + pair.Key.Trim().Replace("'", "''") + "','" + pair.Value.Trim().Replace("'", "''") + "')") + ";");
            }
            SQLiteHelper.ExecuteNonQuery(builder.ToString(), CommandType.Text);
        }

        public void start()
        {
            string str = "";
            downSite site = null;
            ERRypu.JBipq1 = false;
            int num = ERRypu.DDGXY0 ? 5 : 0xc350;
            if (ERRypu.Jm1ydu(this.sto.userName, this.sto.siteId))
            {
                num = 0xc350;
            }
            foreach (KeyValuePair<string, string> pair in this.inputUrls)
            {
                this.i++;
                string str2 = func.getDomain(pair.Key);
                if (str2 == "")
                {
                    this.errUrls[pair.Key] = "网址无效";
                }
                else
                {
                    if (str != str2)
                    {
                        Assembly assembly = PzFuEX._39DU7p(str2);
                        if (assembly == null)
                        {
                            if (PzFuEX._02akcf.ContainsKey(str2) && (PzFuEX._02akcf[str2] == ERRypu.F8wJst))
                            {
                                this.errUrls[pair.Key] = "敦煌不能搬到敦煌";
                            }
                            else
                            {
                                this.errUrls[pair.Key] = "不支持此网址";
                            }
                            goto Label_01A1;
                        }
                        site = (downSite) Activator.CreateInstance(assembly.GetType("down" + PzFuEX._02akcf[str2] + ".downSite"));
                        site.Http.Encode = site.encode;
                        site.Http.url = pair.Key;
                        site.Http.isDown = true;
                        site.isTry = ERRypu.DDGXY0;
                        str = str2;
                    }
                    site.grapUrl(pair.Key, this.urls, this.errUrls);
                Label_01A1:;
                }
            }
            this.i = 0;
            this.j = 2;
            this.si = 0;
            product product = null;
            str = "";
            using (Dictionary<string, string>.Enumerator enumerator2 = this.urls.GetEnumerator())
            {
            Label_01E9:
                if (!enumerator2.MoveNext())
                {
                    goto Label_0719;
                }
                KeyValuePair<string, string> current = enumerator2.Current;
                this.i++;
                if (current.Key.Trim() == "")
                {
                    goto Label_01E9;
                }
                int num2 = this.method_1(current.Key);
                if (!this.isReDown && (num2 > 0))
                {
                    goto Label_01E9;
                }
                string str3 = func.getDomain(current.Key);
                if (str3 == "")
                {
                    this.errUrls[current.Key] = "网址无效";
                    goto Label_01E9;
                }
                if (str != str3)
                {
                    Assembly assembly2 = PzFuEX._39DU7p(str3);
                    if (assembly2 == null)
                    {
                        this.errUrls[current.Key] = "不支持此网址";
                        goto Label_01E9;
                    }
                    site = (downSite) Activator.CreateInstance(assembly2.GetType("down" + PzFuEX._02akcf[str3] + ".downSite"));
                    product = (product) Activator.CreateInstance(assembly2.GetType("down" + PzFuEX._02akcf[str3] + ".product"), new object[] { this.webbrowser });
                    product.Http = new http();
                    product.Http.isDown = true;
                    product.Http.Encode = site.encode;
                    product.storeId = this.storeID;
                    product.defaultLanguage = site.defaultLanguage;
                    product.currencyUnit = site.currencyUnit;
                    product.toLanguage = this.toLanguage;
                    product.siteId = site.siteId;
                    str = str3;
                }
                if ((PzFuEX._02akcf[str3] == "123") && (ERRypu.u2szIG == "2049"))
                {
                    product.Http.Cookies = PzFuEX.allDownCookies[PzFuEX._02akcf[str3]];
                }
                else if ((ERRypu.u2szIG == "22034") && ((PzFuEX._02akcf[str3] == "1") || (PzFuEX._02akcf[str3] == "5")))
                {
                    product.Http.Cookies = PzFuEX.allDownCookies[PzFuEX._02akcf[str3]];
                }
                else if (PzFuEX._02akcf[str3] == "32")
                {
                    product.Http.Cookies = PzFuEX.allDownCookies[PzFuEX._02akcf[str3]];
                }
                else if (PzFuEX._02akcf[str3] == "57")
                {
                    product.Http.Cookies = PzFuEX.allDownCookies[PzFuEX._02akcf[str3]];
                }
                else
                {
                    product.Http.Cookies = new Dictionary<string, string>();
                }
                product.Http.url = current.Key;
                if (site.isNeedDownHtml)
                {
                    product.htmlCode = product.Http.DownHtml();
                    if (product.htmlCode.Length == 0)
                    {
                        this.errUrls[current.Key] = "下载失败";
                        goto Label_01E9;
                    }
                }
                product.listHtmlCode = current.Value;
                product.url = current.Key;
                product.urlInfo = current.Value;
                product.id = num2;
                try
                {
                    while (site.isBanIp(product.htmlCode, product.url, product.Http.Referer))
                    {
                        site.handleIp(product.htmlCode, product.url, product.Http.Referer);
                        product.htmlCode = product.Http.DownHtml();
                    }
                }
                catch
                {
                }
                goto Label_06EF;
            Label_05C3:
                foreach (KeyValuePair<string, baseP> pair3 in product.attrs)
                {
                    try
                    {
                        pair3.Value.getValueFromWeb();
                    }
                    catch
                    {
                    }
                }
                product.pubHtml = new Dictionary<string, string>();
                product.translatePro();
                this.ii = 0;
                product.getDownAllPics();
                this.totallPic = product.getTotalPic();
                product.downPic(ref this.ii);
                product.returnAllPics();
                product.state = 0;
                product.fromSiteId = "down" + product.siteId;
                product.saveToDataBase();
                foreach (KeyValuePair<string, baseP> pair4 in product.attrs)
                {
                    pair4.Value.initValue();
                }
            Label_06AF:
                this.si++;
                func.getNum(1);
                if (this.si >= num)
                {
                    goto Label_0703;
                }
                if (site.intervalTime > 0)
                {
                    Thread.Sleep((int) (site.intervalTime * 0x3e8));
                }
                goto Label_01E9;
            Label_06EF:
                if (!product.downHtml(this.errUrls))
                {
                    goto Label_06AF;
                }
                goto Label_05C3;
            Label_0703:
                ERRypu.JBipq1 = true;
            }
        Label_0719:
            this.isOk = true;
        }
    }
}


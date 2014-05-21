namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    [Serializable]
    public class store : upSite
    {
        public static int cusid;
        public static int defStoID;
        public bool isDef;
        public string name = "";
        public int storeId;
        public string userName = "";
        public string userPwd = "";

        public static void delete(int int_0)
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            SQLiteHelper.ExecuteNonQuery("delete from store where rowid=" + int_0, CommandType.Text);
        }

        public void getDefStore()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            DataSet set = SQLiteHelper.ExecuteDataSet("select rowid,siteid from store where isdef='True' and (loginname='" + ERRypu.bQDosh.Replace("'", "''") + "' or loginname='') limit 1 ", CommandType.Text);
            if (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && PzFuEX._4YIz1F.ContainsKey(set.Tables[0].Rows[0][1].ToString()))
            {
                this.storeId = text.toInt(set.Tables[0].Rows[0][0].ToString());
                this.loadFromDataBase();
            }
            else
            {
                set = SQLiteHelper.ExecuteDataSet("select rowid from store where loginname='" + ERRypu.bQDosh.Replace("'", "''") + "' or loginname='' limit 1 ", CommandType.Text);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    this.storeId = text.toInt(set.Tables[0].Rows[0][0].ToString());
                    cusid = this.storeId;
                    this.loadFromDataBase();
                }
            }
        }

        public string getEditStore()
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            string str2 = "";
            if (base.siteId != "")
            {
                str = "disabled=\"disabled\"";
            }
            if (this.userName != "")
            {
                str2 = "disabled=\"disabled\"";
            }
            string str3 = "";
            if (this.isDef)
            {
                str3 = "checked='checked'";
            }
            builder.Append("<table  style=\" margin-top:20px;\" width=\"586\" height=\"180\" >  <tr>    <td width=\"154\" align=\"right\" valign=\"top\">是否设为默认店铺：</td><td valign=\"top\"><input  type='checkbox' " + str3 + "  id='defchec'/>是   </td>  </tr>   <tr>    <td width=\"154\" align=\"right\" valign=\"top\">登录名：</td>    <td valign=\"top\"><input " + str2 + " type=\"text\" id=\"username\" value=\"" + func.quotToHtml(this.userName) + "\" />&nbsp;&nbsp;&nbsp;您的网店后台登录名，设置好后不能修改    </td>  </tr> <tr>    <td align=\"right\" valign=\"top\">平&nbsp;&nbsp;&nbsp;台：</td><td valign=\"top\"><select " + str + " id='siteid' onchange=\"userchange(this.id);\">");
            foreach (KeyValuePair<string, easyupsite> pair in PzFuEX._4YIz1F)
            {
                string str4 = "";
                if (base.siteId == pair.Key)
                {
                    str4 = "selected=\"selected\"";
                }
                builder.Append("<option " + str4 + " value=\"" + pair.Key + "\">" + pair.Value.siteName + "</option>");
            }
            string str5 = "none";
            if ((base.siteId != "") && PzFuEX._4YIz1F[base.siteId].isOwn)
            {
                str5 = "block";
            }
            string str6 = "";
            string str7 = "";
            string str8 = "";
            if (base.encode == "gbk")
            {
                str8 = "selected=\"selected\"";
            }
            else if (base.encode == "gb2312")
            {
                str7 = "selected=\"selected\"";
            }
            else
            {
                str6 = "selected=\"selected\"";
            }
            string str9 = "";
            string str10 = "";
            if (base.currencyUnit == "人民币元")
            {
                str9 = "selected=\"selected\"";
            }
            else
            {
                str10 = "selected=\"selected\"";
            }
            builder.Append("</select>&nbsp;设置好后不能修改<br><br>即将支持更多平台，敬请关注。</td></tr></table><table width=\"586\" height=\"156\" id='own' style=\"display:" + str5 + "\">  <tr>    <td width=\"154\" align=\"right\">语&nbsp;&nbsp;&nbsp;言：</td>    <td ><select id='yy'>" + this.getLanguage() + "</select>   </td>  </tr>   <tr>    <td align=\"right\">编&nbsp;&nbsp;&nbsp;码：</td>    <td><select id='bm'><option " + str6 + " value=\"utf-8\">utf-8</option><option " + str7 + " value=\"gb2312\">gb2312</option><option " + str8 + " value=\"gbk\">gbk</option></select></td>  </tr>   <tr>    <td align=\"right\">货币单位：</td>    <td><select id='hb'><option " + str10 + " value=\"美元\">美元</option><option " + str9 + " value=\"人民币元\">人民币元</option></select></td>  </tr>   <tr>    <td align=\"right\">后台登录地址：</td>    <td><input type=\"text\" id=\"dz\" value=\"" + func.quotToHtml(base.loginUrl) + "\" size=\"60\" /></td>  </tr></table><input id='save' type=\"button\" value=\"  保存  \" onclick=\"userchange(this.id);\" style=\"height:30px; margin-top:20px;margin-left:160px\" />");
            return builder.ToString();
        }

        public string getLanguage()
        {
            string str = "<option value=sq>阿尔巴尼亚语</option><option value=ar>阿拉伯语</option><option value=az>阿塞拜疆语</option><option value=ga>爱尔兰语</option><option value=et>爱沙尼亚语</option><option value=eu>巴斯克语</option><option value=be>白俄罗斯语</option><option value=bg>保加利亚语</option><option value=is>冰岛语</option><option value=pl>波兰语</option><option value=bs>波斯尼亚语</option><option value=fa>波斯语</option><option value=af>布尔语(南非荷兰语)</option><option value=da>丹麦语</option><option value=de>德语</option><option value=ru>俄语</option><option value=fr>法语</option><option value=tl>菲律宾语</option><option value=fi>芬兰语</option><option value=km>高棉语</option><option value=ka>格鲁吉亚语</option><option value=gu>古吉拉特语</option><option value=ht>海地克里奥尔语</option><option value=ko>韩语</option><option value=nl>荷兰语</option><option value=gl>加利西亚语</option><option value=ca>加泰罗尼亚语</option><option value=cs>捷克语</option><option value=kn>卡纳达语</option><option value=hr>克罗地亚语</option><option value=la>拉丁语</option><option value=lv>拉脱维亚语</option><option value=lo>老挝语</option><option value=lt>立陶宛语</option><option value=ro>罗马尼亚语</option><option value=mt>马耳他语</option><option value=mr>马拉地语</option><option value=ms>马来语</option><option value=mk>马其顿语</option><option value=bn>孟加拉语</option><option value=hmn>苗语</option><option value=no>挪威语</option><option value=pt>葡萄牙语</option><option value=ja>日语</option><option value=sv>瑞典语</option><option value=sr>塞尔维亚语</option><option value=eo>世界语</option><option value=sk>斯洛伐克语</option><option value=sl>斯洛文尼亚语</option><option value=sw>斯瓦希里语</option><option value=ceb>宿务语</option><option value=te>泰卢固语</option><option value=ta>泰米尔语</option><option value=th>泰语</option><option value=tr>土耳其语</option><option value=cy>威尔士语</option><option value=ur>乌尔都语</option><option value=uk>乌克兰语</option><option value=iw>希伯来语</option><option value=el>希腊语</option><option value=es>西班牙语</option><option value=hu>匈牙利语</option><option value=hy>亚美尼亚语</option><option value=it>意大利语</option><option value=yi>意第绪语</option><option value=hi>印地语</option><option value=id>印尼语</option><option value=jw>印尼爪哇语</option><option value=en>英语</option><option value=vi>越南语</option><option value=zh-cn>中文</option>";
            StringBuilder builder = new StringBuilder();
            foreach (JArray array2 in (IEnumerable<JToken>) func.正则获取所有(str, "<option value=(.*?)>(.*?)</option>", ""))
            {
                string str2 = "";
                if (base.defaultLanguage.ToLower() == array2[0].ToString().ToLower())
                {
                    str2 = "selected=\"selected\"";
                }
                builder.Append("<option " + str2 + " value=\"" + array2[0].ToString() + "\">" + array2[1].ToString() + "</option>");
            }
            return builder.ToString();
        }

        public static string getStores()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            JArray array = new JArray();
            foreach (KeyValuePair<string, easyupsite> pair in PzFuEX._4YIz1F)
            {
                string siteName = func.正则截取(pair.Value.siteName, "", "-");
                if (siteName.Trim().Length == 0)
                {
                    siteName = pair.Value.siteName;
                }
                JObject obj2 = new JObject();
                obj2["id"] = "addstore" + pair.Key;
                obj2["name"] = siteName;
                obj2["state"] = "open";
                obj2["iconCls"] = "icon-" + pair.Key;
                JArray array2 = new JArray();
                obj2["children"] = array2;
                array.Add(obj2);
                foreach (DataRow row in SQLiteHelper.ExecuteDataSet("select rowid,userName,siteid,isdef from store where (loginname='" + ERRypu.bQDosh.Replace("'", "''") + "' or loginname='') and siteid='" + pair.Key.Replace("'", "''") + "'", 1).Tables[0].Rows)
                {
                    string str3 = row["userName"].ToString();
                    if (str3.Trim().Length == 0)
                    {
                        str3 = "默认店铺";
                    }
                    JObject obj3 = new JObject();
                    obj3["id"] = row["rowid"].ToString();
                    obj3["name"] = str3;
                    array2.Add(obj3);
                    if (row["isdef"].ToString() == "True")
                    {
                        defStoID = text.toInt(row["rowid"].ToString());
                    }
                }
            }
            JObject item = new JObject();
            item["id"] = "addstore";
            item["name"] = "独立网站(联系我们定制)";
            item["state"] = "open";
            item["iconCls"] = "icon-tip";
            JArray array3 = new JArray();
            item["children"] = array3;
            if (!File.Exists(PzFuEX.Jo4304 + "co.txt"))
            {
                array.Add(item);
            }
            return JsonConvert.SerializeObject(array);
        }

        public static string getStoresList(store store_0)
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            DataSet set = SQLiteHelper.ExecuteDataSet("select rowid,name from store where loginname='" + ERRypu.bQDosh.Replace("'", "''") + "' or loginname=''", CommandType.Text);
            StringBuilder builder = new StringBuilder();
            if (set.Tables.Count > 0)
            {
                builder.Append("<div style='padding:30px;'>将所选产品复制到哪个店铺：<select id=\"stto\"> ");
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    string str2 = "";
                    if (store_0.storeId.ToString() == row[0].ToString().Trim())
                    {
                        str2 = "selected=\"selected\"";
                    }
                    builder.Append(" <option " + str2 + " value=\"" + row[0].ToString().Trim() + "\">" + row[1].ToString().Trim() + "</option>");
                }
                builder.Append("</select>");
            }
            builder.Append("&nbsp;&nbsp;<input id=\"adds\" type=\"button\" value=\"  新增店铺  \" onclick=\"userchange(this.id);\" style=''/></div>");
            return builder.ToString();
        }

        public static string getStoresList2(store store_0)
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            DataSet set = SQLiteHelper.ExecuteDataSet("select rowid,name from store where loginname='" + ERRypu.bQDosh.Replace("'", "''") + "' or loginname=''", CommandType.Text);
            StringBuilder builder = new StringBuilder();
            if (set.Tables.Count > 0)
            {
                builder.Append("<div style='padding:10px;'>将产品采集到哪个店铺：<select id=\"stto\"> ");
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    string str2 = "";
                    if (store_0.storeId.ToString() == row[0].ToString().Trim())
                    {
                        str2 = "selected=\"selected\"";
                    }
                    builder.Append(" <option " + str2 + " value=\"" + row[0].ToString().Trim() + "\">" + row[1].ToString().Trim() + "</option>");
                }
                builder.Append("</select>");
            }
            builder.Append("&nbsp;&nbsp;<input id=\"adds\" type=\"button\" value=\"  新增店铺  \" onclick=\"userchange(this.id);\" style=''/></div>");
            return builder.ToString();
        }

        public static int getStoresNum()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            DataSet set = SQLiteHelper.ExecuteDataSet("select rowid,name from store where loginname='" + ERRypu.bQDosh.Replace("'", "''") + "' or loginname=''", CommandType.Text);
            if (set.Tables.Count > 0)
            {
                return set.Tables[0].Rows.Count;
            }
            return 0;
        }

        public void loadFromDataBase()
        {
            if (this.storeId > 0)
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
                DataSet set = SQLiteHelper.ExecuteDataSet("select * from store where rowid=" + this.storeId, CommandType.Text);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    base.siteId = set.Tables[0].Rows[0]["siteid"].ToString();
                    base.siteName = PzFuEX._4YIz1F[base.siteId].siteName;
                    this.name = set.Tables[0].Rows[0]["name"].ToString();
                    this.userName = set.Tables[0].Rows[0]["username"].ToString();
                    this.userPwd = set.Tables[0].Rows[0]["userpwd"].ToString();
                    base.isOwn = PzFuEX._4YIz1F[base.siteId].isOwn;
                    Type type = PzFuEX.qWFNHj(base.siteId).GetType("up" + base.siteId + ".upsite");
                    while (type == null)
                    {
                        type = PzFuEX.qWFNHj(base.siteId).GetType("up" + base.siteId + ".upsite");
                    }
                    upSite site = (upSite) Activator.CreateInstance(type);
                    base.isCSV = site.isCSV;
                    base.isLogin = site.isLogin;
                    base.loginSuccess = site.loginSuccess;
                    base.loginVeryUrl = site.loginVeryUrl;
                    base.csvFileName = site.csvFileName;
                    base.csvImgFloder = site.csvImgFloder;
                    base.csvPicExt = site.csvPicExt;
                    base.csvSplit = site.csvSplit;
                    base.csvHeader = site.csvHeader;
                    base.csvEncode = site.csvEncode;
                    base.intervalTime = site.intervalTime;
                    base.isShowExp = site.isShowExp;
                    base.isSaleAttrPic = site.isSaleAttrPic;
                    base.isXls = site.isXls;
                    base.isExtPic = site.isExtPic;
                    base.isFast = site.isFast;
                    base.xlsHeader = site.xlsHeader;
                    base.xlsImgFloder = site.xlsImgFloder;
                    base.xlsPicExt = site.xlsPicExt;
                    base.isFreezePanes = site.isFreezePanes;
                    base.xlsFileName = site.xlsFileName;
                    base.waterIsXD = site.waterIsXD;
                    base.waterPos = site.waterPos;
                    base.waterX = site.waterX;
                    base.waterY = site.waterY;
                    if (base.isOwn)
                    {
                        base.loginUrl = set.Tables[0].Rows[0]["loginurl"].ToString();
                        base.encode = set.Tables[0].Rows[0]["encode"].ToString();
                        base.defaultLanguage = set.Tables[0].Rows[0]["language"].ToString();
                        base.currencyUnit = set.Tables[0].Rows[0]["currencyunit"].ToString();
                    }
                    else
                    {
                        base.loginUrl = site.loginUrl;
                        base.encode = site.encode;
                        base.defaultLanguage = site.defaultLanguage;
                        base.currencyUnit = site.currencyUnit;
                        base.secondLanguage = site.secondLanguage;
                    }
                    this.isDef = Convert.ToBoolean(set.Tables[0].Rows[0]["isdef"].ToString());
                }
            }
        }

        public void saveDataBase()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            string str = "";
            if (this.isDef)
            {
                str = "UPDATE store SET isdef='False' ";
                SQLiteHelper.ExecuteNonQuery(str, CommandType.Text);
            }
            if (this.storeId == 0)
            {
                SQLiteHelper.ExecuteNonQuery(string.Concat(new object[] { 
                    "INSERT INTO store (siteid,sitename,name,username,userpwd,loginurl,encode,language,language2,currencyunit,isdef,loginname) VALUES ('", base.siteId.Replace("'", "''"), "','", base.siteName.Replace("'", "''"), "','", this.name.Replace("'", "''"), "','", this.userName.Replace("'", "''"), "','", this.userPwd.Replace("'", "''"), "','", base.loginUrl.Replace("'", "''"), "','", base.encode.Replace("'", "''"), "','", base.defaultLanguage.Replace("'", "''"), 
                    "','", base.secondLanguage.Replace("'", "''"), "','", base.currencyUnit.Replace("'", "''"), "','", this.isDef, "','", ERRypu.bQDosh.Replace("'", "''"), "')"
                 }), CommandType.Text);
                DataSet set = SQLiteHelper.ExecuteDataSet("SELECT max(rowid) from store", CommandType.Text);
                if (set.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                this.storeId = text.toInt(set.Tables[0].Rows[0][0].ToString());
                if (this.storeId <= 0)
                {
                    return;
                }
                string path = PzFuEX.Jo4304 + @"cc\" + this.storeId.ToString();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!Directory.Exists(path + @"\img"))
                {
                    Directory.CreateDirectory(path + @"\img");
                }
                File.Copy(PzFuEX.Jo4304 + @"cc\pro.jpg", path + @"\pro.jpg", true);
                File.Copy(PzFuEX.Jo4304 + @"cc\attr.jpg", path + @"\attr.jpg", true);
                File.Copy(PzFuEX.Jo4304 + @"cc\base.jpg", path + @"\base.jpg", true);
                File.Copy(PzFuEX.Jo4304 + @"cc\cate.jpg", path + @"\cate.jpg", true);
                if (base.isOwn)
                {
                    return;
                }
                int verBase = 0;
                int verCate = 0;
                int verAttr = 0;
                string str3 = PzFuEX.ekcg10.Replace("new/newnn.aspx", "") + "a/" + base.siteId;
                if (!File.Exists(PzFuEX.Jo4304 + @"a\" + base.siteId + @"\cate.jpg"))
                {
                    if (!Directory.Exists(PzFuEX.Jo4304 + @"a\" + base.siteId))
                    {
                        Directory.CreateDirectory(PzFuEX.Jo4304 + @"a\" + base.siteId);
                    }
                    File.Copy(PzFuEX.Jo4304 + @"a\attr.jpg", PzFuEX.Jo4304 + @"a\" + base.siteId + @"\attr.jpg", true);
                    if (!File.Exists(PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg"))
                    {
                        File.Copy(PzFuEX.Jo4304 + @"a\map.jpg", PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg", true);
                    }
                    verBase = PzFuEX._4YIz1F[base.siteId].verBase;
                    if (!ERRypu._3CFCHP(str3 + "/base.zip", base.siteId))
                    {
                        File.Copy(PzFuEX.Jo4304 + @"a\base.jpg", PzFuEX.Jo4304 + @"a\" + base.siteId + @"\base.jpg", true);
                        verBase = 0;
                    }
                    verCate = PzFuEX._4YIz1F[base.siteId].verCate;
                    if (!ERRypu._3CFCHP(str3 + "/cate.zip", base.siteId))
                    {
                        File.Copy(PzFuEX.Jo4304 + @"a\cate.jpg", PzFuEX.Jo4304 + @"a\" + base.siteId + @"\cate.jpg", true);
                        verCate = 0;
                    }
                    verAttr = PzFuEX._4YIz1F[base.siteId].verAttr;
                }
                using (DataSet set2 = SQLiteHelper.ExecuteDataSet("select siteid from ver where siteid=" + base.siteId, CommandType.Text))
                {
                    if ((set2.Tables.Count > 0) && (set2.Tables[0].Rows.Count == 0))
                    {
                        SQLiteHelper.ExecuteNonQuery(string.Concat(new object[] { "INSERT INTO `ver`(`siteid`, `attr`, `base`, `cate`) VALUES (", base.siteId, ",", verAttr, ",", verBase, ",", verCate, ") " }), CommandType.Text);
                    }
                    return;
                }
            }
            SQLiteHelper.ExecuteNonQuery(string.Concat(new object[] { 
                "UPDATE store SET siteID='", base.siteId.Replace("'", "''"), "',sitename='", base.siteName.Replace("'", "''"), "',name='", this.name.Replace("'", "''"), "',username='", this.userName.Replace("'", "''"), "',userpwd='", this.userPwd.Replace("'", "''"), "',loginurl='", base.loginUrl.Replace("'", "''"), "',encode='", base.encode.Replace("'", "''"), "',language='", base.defaultLanguage.Replace("'", "''"), 
                "',currencyunit='", base.currencyUnit.Replace("'", "''"), "',isdef='", this.isDef, "'  WHERE rowid=", this.storeId
             }), CommandType.Text);
        }
    }
}


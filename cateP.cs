namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    public class cateP : baseP
    {
        public List<cate> baseData = new List<cate>();
        private bool bool_0;
        public string catestr = "";
        public List<cate> defalu = new List<cate>();
        private List<cate> list_0;
        public List<cate> value = new List<cate>();

        public cateP(fatherObj fatherObj_0)
        {
            base.key = "cate";
            base.name = "类目";
            base.dataBaseFieldForSave = "f8";
            base.dataBaseFieldForList = "f9";
            base.dataBaseFieldForSearch = "f10";
            base.listWidth = 200;
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void batchEdit()
        {
            if (this.list_0 != null)
            {
                this.value = this.list_0;
            }
        }

        public override void forceEdit()
        {
            if (this.verifyEdit() != "")
            {
                this.value = this.defalu;
            }
        }

        public List<cate> getBaseData(string string_0)
        {
            List<cate> list = new List<cate>();
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            DataSet set = SQLiteHelper.ExecuteDataSet("select * from cate where f_value='" + string_0 + "'", CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                string str2 = (base.fatherObj.defaultLanguage == "zh-cn") ? "text_cn" : "text_en";
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    cate item = new cate();
                    item.value.id = row["value"].ToString().Trim();
                    item.value.text["zh-cn"] = row["text_cn"].ToString().Trim();
                    item.value.text["en"] = row["text_en"].ToString().Trim();
                    item.value.text[base.fatherObj.defaultLanguage] = row[str2].ToString().Trim();
                    item.isLeaf = Convert.ToBoolean(row["is_leaf"]);
                    list.Add(item);
                }
            }
            return list;
        }

        public void getCateJPG(string string_0)
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"cc\pub.jpg";
            int verBase = 0;
            int verCate = 0;
            int verAttr = 0;
            string str = PzFuEX.ekcg10.Replace("new/newnn.aspx", "") + "a/" + string_0;
            if (!File.Exists(PzFuEX.Jo4304 + @"a\" + string_0 + @"\cate.jpg"))
            {
                if (!Directory.Exists(PzFuEX.Jo4304 + @"a\" + string_0))
                {
                    Directory.CreateDirectory(PzFuEX.Jo4304 + @"a\" + string_0);
                }
                File.Copy(PzFuEX.Jo4304 + @"a\attr.jpg", PzFuEX.Jo4304 + @"a\" + string_0 + @"\attr.jpg", true);
                if (!File.Exists(PzFuEX.Jo4304 + @"a\" + string_0 + @"\map.jpg"))
                {
                    File.Copy(PzFuEX.Jo4304 + @"a\map.jpg", PzFuEX.Jo4304 + @"a\" + string_0 + @"\map.jpg", true);
                }
                verBase = PzFuEX._4YIz1F[string_0].verBase;
                if (!ERRypu._3CFCHP(str + "/base.zip", string_0))
                {
                    File.Copy(PzFuEX.Jo4304 + @"a\base.jpg", PzFuEX.Jo4304 + @"a\" + string_0 + @"\base.jpg", true);
                    verBase = 0;
                }
                verCate = PzFuEX._4YIz1F[string_0].verCate;
                if (!ERRypu._3CFCHP(str + "/cate.zip", string_0))
                {
                    File.Copy(PzFuEX.Jo4304 + @"a\cate.jpg", PzFuEX.Jo4304 + @"a\" + string_0 + @"\cate.jpg", true);
                    verCate = 0;
                }
                verAttr = PzFuEX._4YIz1F[string_0].verAttr;
            }
            using (DataSet set = SQLiteHelper.ExecuteDataSet("select siteid from ver where siteid=" + string_0, CommandType.Text))
            {
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count == 0))
                {
                    SQLiteHelper.ExecuteNonQuery(string.Concat(new object[] { "INSERT INTO `ver`(`siteid`, `attr`, `base`, `cate`) VALUES (", string_0, ",", verAttr, ",", verBase, ",", verCate, ") " }), CommandType.Text);
                }
            }
        }

        public override string getDataBaseFromValue()
        {
            JArray array = new JArray();
            foreach (cate cate in this.value)
            {
                array.Add(cate.getJObject);
            }
            return JsonConvert.SerializeObject(array);
        }

        public Dictionary<string, string> getFullPathText(string string_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            string str = "select * from cate where is_Leaf='True'";
            DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if (string_0 == "zh-cn")
                    {
                        dictionary[row["text_cn_path"].ToString()] = row["value"].ToString().Trim();
                    }
                    else
                    {
                        dictionary[row["text_en_path"].ToString()] = row["value"].ToString().Trim();
                    }
                }
            }
            return dictionary;
        }

        public Dictionary<string, string> getFullPathTextByCids(string string_0, Dictionary<string, string> dictionary_0)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                builder.Append("'" + pair.Value + "',");
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary[""] = "";
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            DataSet set = SQLiteHelper.ExecuteDataSet("select * from cate where is_Leaf='True' and value in (" + builder.ToString().TrimEnd(new char[] { ',' }) + ")", CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if (string_0 == "zh-cn")
                    {
                        dictionary[row["value"].ToString().Trim()] = row["text_cn_path"].ToString();
                    }
                    else
                    {
                        dictionary[row["value"].ToString().Trim()] = row["text_en_path"].ToString();
                    }
                }
            }
            return dictionary;
        }

        public override string getListCacheFromValue()
        {
            return this.fullName;
        }

        public override string getSearchCacheFromValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (cate cate in this.value)
            {
                builder.Append(cate.value.id + ",");
            }
            if (builder.ToString() != "")
            {
                return ("," + builder.ToString());
            }
            return "";
        }

        public override string getSearchCondition()
        {
            string str = this.getSearchCacheFromValue();
            if (str != "")
            {
                return (" (" + base.dataBaseFieldForSearch + " like '%" + str.Replace("'", "''") + "%') ");
            }
            return "";
        }

        public override void getTranslate()
        {
            foreach (cate cate in this.value)
            {
                if (cate.value.text[base.fatherObj.defaultLanguage] != "")
                {
                    base.fatherObj.translateText[cate.value.text[base.fatherObj.defaultLanguage]] = "";
                }
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = new List<cate>();
            foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(string_0))
            {
                this.value.Add(cate.getValueFromJObject((JObject) token));
            }
            if (base.fatherObj.state != 0)
            {
                base.fatherObj.getReg();
                if (base.fatherObj.attrs.ContainsKey("cateAttr"))
                {
                    base.fatherObj.cateAttr.baseDataLinkId = this.getLastCateID;
                    base.fatherObj.cateAttr.getBaseDataFromDataBase();
                }
                if (base.fatherObj.attrs.ContainsKey("saleAttr"))
                {
                    base.fatherObj.saleAttr.baseDataLinkId = this.getLastCateID;
                    base.fatherObj.saleAttr.getBaseDataFromDataBase();
                }
            }
        }

        public override void getValueFromEditC()
        {
        }

        public override object getValueFromSearchC()
        {
            return null;
        }

        public override void handleBatchUserChange(string string_0)
        {
        }

        public override void handleBatchValueChange(string string_0)
        {
            this.handleEditValueChange(string_0);
            this.list_0 = new List<cate>();
            foreach (cate cate in this.value)
            {
                this.list_0.Add(cate);
            }
        }

        public override void handleEditUserChange(string string_0)
        {
            if (!base.fatherObj.Store.isOwn && string_0.EndsWith("ok"))
            {
                base.fatherObj.getReg();
                if (base.fatherObj.attrs.ContainsKey("cateAttr"))
                {
                    base.fatherObj.cateAttr.baseDataLinkId = this.getLastCateID;
                    base.fatherObj.cateAttr.getBaseDataFromDataBase();
                    base.fatherObj.cateAttr.value = new List<attr>();
                    base.fatherObj.cateAttr.setEditControl();
                }
                if (base.fatherObj.attrs.ContainsKey("saleAttr"))
                {
                    base.fatherObj.saleAttr.baseDataLinkId = this.getLastCateID;
                    base.fatherObj.saleAttr.getBaseDataFromDataBase();
                    base.fatherObj.saleAttr.value = new List<option>();
                    base.fatherObj.saleAttr.setEditControl();
                    base.fatherObj.saleAttr.handleEditValueChange("");
                }
            }
        }

        public override void handleEditValueChange(string string_0)
        {
            if (string_0.EndsWith("c"))
            {
                string str = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
                this.bool_0 = true;
                for (int i = this.value.Count - 1; i >= text.toInt(string_0.Replace("c", "")); i--)
                {
                    this.value.RemoveAt(i);
                }
                string[] strArray = func.getAttrByElem(base.elem(string_0), "value").Split(new char[] { '-' });
                string_0 = string_0 + strArray[1];
                cate item = new cate();
                item.value.id = strArray[0];
                item.value.text[str] = func.getAttrByElem(base.elem(string_0), "txt");
                item.isLeaf = func.getAttrByElem(base.elem(string_0), "islast").ToLower() == "true";
                this.value.Add(item);
            }
            if (base.fatherObj.editType != "map")
            {
                this.setEditControl();
            }
            else
            {
                this.setMapControl();
            }
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
            this.handleEditValueChange(string_0);
        }

        public override void initValue()
        {
            this.value = new List<cate>();
        }

        private Dictionary<string, string> method_0(string string_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["zh-cn"] = "";
            dictionary["en"] = "";
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            DataSet set = SQLiteHelper.ExecuteDataSet("select * from cate where value='" + string_0 + "'", CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                if (set.Tables[0].Rows[0]["f_value"].ToString().Trim() != "0")
                {
                    dictionary = this.method_0(set.Tables[0].Rows[0]["f_value"].ToString().Trim());
                }
                dictionary["zh-cn"] = dictionary["zh-cn"] + set.Tables[0].Rows[0]["text_cn"].ToString().Trim() + " > ";
                dictionary["en"] = dictionary["en"] + set.Tables[0].Rows[0]["text_en"].ToString().Trim() + " > ";
            }
            return dictionary;
        }

        public override void saveBaseData()
        {
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
                SQLiteHelper.ExecuteNonQuery("delete from cate ", CommandType.Text);
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            StringBuilder builder = new StringBuilder();
            foreach (cate cate in this.baseData)
            {
                string str = "";
                string str2 = "";
                if (cate.value.text.ContainsKey("zh-cn"))
                {
                    str = cate.value.text["zh-cn"].Replace(@"\/", "/");
                }
                if (cate.value.text.ContainsKey("en"))
                {
                    str2 = cate.value.text["en"].Replace(@"\/", "/");
                }
                builder.Append(string.Concat(new object[] { "INSERT INTO cate (value,text_cn,is_Leaf,f_value,text_en) VALUES ('", cate.value.id.Replace("'", "''"), "','", str.Replace("'", "''"), "','", cate.isLeaf, "','", cate.fvalue, "','", str2.Replace("'", "''"), "');" }));
            }
            SQLiteHelper.ExecuteNonQuery(builder.ToString(), CommandType.Text);
        }

        public override void setBatchControl()
        {
            this.setEditControl();
        }

        public override void setEditControl()
        {
            string id;
            StringBuilder builder = new StringBuilder();
            string str = "0";
            string str2 = "display:none";
            if (this.bool_0)
            {
                str2 = "";
            }
            builder.Append("<input id='catefull' size='60' type='text' disabled='disabled' value=\"" + func.quotToHtml(this.fullName) + "\"/><input type='button' value='选择类目' onclick=\"document.getElementById('catecc').style.display='block';\" /><div id='catecc' style='" + str2 + "'>");
            int num = 0;
        Label_0285:
            id = "";
            if (this.value.Count > num)
            {
                id = this.value[num].value.id;
            }
            builder.Append(string.Concat(new object[] { "<select id='", base.KeyId, num, "c' size='10' style='margin:5px' onchange='valuechange(this.id);' >" }));
            int num2 = 0;
            foreach (cate cate in this.getBaseData(str))
            {
                string str4 = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
                string str5 = "";
                if (id == cate.value.id)
                {
                    str5 = "selected='selected'";
                }
                builder.Append(string.Concat(new object[] { 
                    "<option id='", base.KeyId, num, "c", num2, "' value='", cate.value.id, "-", num2, "' ", str5, " islast='", cate.isLeaf, "' txt=\"", func.quotToHtml(cate.value.text[str4]), "\" >", 
                    cate.value.GetFullText, "</option>"
                 }));
                num2++;
            }
            builder.Append("</select>");
            if ((this.value.Count <= num) || !this.value[num].isLeaf)
            {
                str = id;
                num++;
                if (str != "")
                {
                    goto Label_0285;
                }
            }
            string str6 = "disabled='disabled'";
            if ((this.value.Count > 0) && this.value[this.value.Count - 1].isLeaf)
            {
                str6 = "";
            }
            builder.Append("<div style='float:left;padding:5px'><input id='" + base.KeyId + "ok' " + str6 + " type='button' value='  确 定  ' onclick=\"document.getElementById('catecc').style.display='none';userchange(this.id);\" /></div></div>");
            base.control.innerHTML = builder.ToString();
        }

        public void setFullPathText()
        {
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            string str = "select * from cate where is_Leaf='True'";
            DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    Dictionary<string, string> dictionary = this.method_0(row["f_value"].ToString());
                    string str2 = dictionary["zh-cn"] + row["text_cn"].ToString();
                    string str3 = dictionary["en"] + row["text_en"].ToString();
                    builder.Append("update cate set text_cn_path='" + str2.Replace("'", "''") + "',text_en_path='" + str3.Replace("'", "''") + "' where value='" + row["value"].ToString() + "' and  f_value='" + row["f_value"].ToString() + "';");
                }
                SQLiteHelper.ExecuteNonQuery(builder.ToString(), CommandType.Text);
            }
        }

        public void setListCateFromLaefId(string string_0)
        {
            if ((string_0 != "0") && (string_0 != ""))
            {
                if (base.baseDataIsOwn)
                {
                    SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
                }
                else
                {
                    string path = PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
                    if (!File.Exists(path))
                    {
                        try
                        {
                            this.getCateJPG(base.fatherObj.siteId);
                        }
                        catch
                        {
                        }
                    }
                    SQLiteHelper.Conn = "Data Source=" + path;
                }
                DataSet set = SQLiteHelper.ExecuteDataSet("select * from cate where value='" + string_0 + "'", CommandType.Text);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    cate item = new cate();
                    item.value.id = string_0;
                    item.isLeaf = Convert.ToBoolean(set.Tables[0].Rows[0]["is_Leaf"]);
                    item.value.text["zh-cn"] = set.Tables[0].Rows[0]["text_cn"].ToString();
                    item.value.text["en"] = set.Tables[0].Rows[0]["text_en"].ToString();
                    this.value.Insert(0, item);
                    item.fvalue = set.Tables[0].Rows[0]["f_value"].ToString();
                    this.setListCateFromLaefId(item.fvalue);
                }
            }
        }

        public void setMapControl()
        {
            string id;
            StringBuilder builder = new StringBuilder();
            string str = "0";
            string str2 = "display:block";
            if (this.bool_0)
            {
                str2 = "";
            }
            builder.Append("<input style='display:none' id='catefull' size='60' type='text' disabled='disabled' value=\"" + func.quotToHtml(this.fullName) + "\"/><input style='display:none' type='button' value='选择类目' onclick=\"document.getElementById('catecc').style.display='block';\" /><div id='catecc' style='" + str2 + "'>");
            int num = 0;
        Label_0253:
            id = "";
            if (this.value.Count > num)
            {
                id = this.value[num].value.id;
            }
            builder.Append(string.Concat(new object[] { "<select id='", base.KeyId, num, "c' size='10' style='margin:5px' onchange='valuechange(this.id);' >" }));
            int num2 = 0;
            foreach (cate cate in this.getBaseData(str))
            {
                string str4 = "";
                if (id == cate.value.id)
                {
                    str4 = "selected='selected'";
                }
                builder.Append(string.Concat(new object[] { 
                    "<option id='", base.KeyId, num, "c", num2, "' value='", cate.value.id, "-", num2, "' ", str4, " islast='", cate.isLeaf, "' txt=\"", func.quotToHtml(cate.value.GetFullText), "\" >", 
                    cate.value.GetFullText, "</option>"
                 }));
                num2++;
            }
            builder.Append("</select>");
            str = id;
            if ((this.value.Count <= num) || !this.value[num].isLeaf)
            {
                num++;
                if (str != "")
                {
                    goto Label_0253;
                }
            }
            string str5 = "disabled='disabled'";
            if ((this.value.Count > 0) && this.value[this.value.Count - 1].isLeaf)
            {
                str5 = "";
            }
            builder.Append("<div style='float:left;padding:5px'><input id=\"chcates" + this.catestr.Replace("\"", "&quot;") + "\" " + str5 + " type='button' value='  确 定  ' onclick=\"userchange(this.id);\" /></div></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            this.setEditControl();
        }

        public override void setTranslate()
        {
            foreach (cate cate in this.value)
            {
                string str = base.fatherObj.translateText[cate.value.text[base.fatherObj.defaultLanguage]];
                if (str != "")
                {
                    cate.value.text[base.fatherObj.toLanguage] = str;
                }
                else
                {
                    cate.value.text[base.fatherObj.toLanguage] = cate.value.text[base.fatherObj.defaultLanguage];
                }
            }
        }

        public override void setValueFromMap()
        {
            if (base.fatherObj.Store.isOwn)
            {
                this.value = base.oldPro.cate.value;
            }
            else
            {
                this.value = new List<cate>();
                string str = base.mapObj.cates[base.oldPro.cate.fullName];
                this.setListCateFromLaefId(str);
            }
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            if (base.isMust && (this.value.Count <= 0))
            {
                return "请选择类目";
            }
            if ((this.value.Count > 0) && !this.value[this.value.Count - 1].isLeaf)
            {
                return "请选择最后一级类目";
            }
            return "";
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }

        public string fullName
        {
            get
            {
                string str = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
                string str2 = "";
                foreach (cate cate in this.value)
                {
                    if (str2 == "")
                    {
                        str2 = cate.value.text[str];
                    }
                    else
                    {
                        str2 = str2 + " > " + cate.value.text[str];
                    }
                }
                return str2.Trim();
            }
        }

        public string fullNameToLang
        {
            get
            {
                string str = "";
                foreach (cate cate in this.value)
                {
                    if (str == "")
                    {
                        str = cate.value.text[base.fatherObj.toLanguage];
                    }
                    else
                    {
                        str = str + " > " + cate.value.text[base.fatherObj.toLanguage];
                    }
                }
                return str.Trim();
            }
        }

        public string getLastCateID
        {
            get
            {
                if (this.value.Count > 0)
                {
                    return this.value[this.value.Count - 1].value.id;
                }
                return "";
            }
        }
    }
}


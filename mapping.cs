namespace client
{
    using csExWB;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    public class mapping : fatherObj
    {
        public Dictionary<string, Dictionary<string, string>> cateattrnames = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> cateattrvalues = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        public Dictionary<string, string> cates = new Dictionary<string, string>();
        public Dictionary<string, double> currencyRate = new Dictionary<string, double>();
        public Dictionary<int, string> idli = new Dictionary<int, string>();
        public Dictionary<string, Dictionary<string, string>> saleattrnames = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> saleattrvalues = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        public bool saveOk;
        public string toCurrencyUnit = "人民币元";
        public Dictionary<string, string> transDic = new Dictionary<string, string>();
        public Dictionary<string, string> units = new Dictionary<string, string>();

        public mapping(cEXWB cEXWB_0)
        {
            base.webbrowser = cEXWB_0;
            base.defaultLanguage = "zh-cn";
            base.editType = "map";
            base.name = new nameP(this);
            base.cate = new cateP(this);
            base.cateAttr = new cateAttrP(this);
            base.saleAttr = new saleAttrP(this);
            base.unit = new unitP(this);
            base.customAttr = new customAttrP(this);
        }

        public void handleuserchange(string string_0)
        {
            if (string_0.StartsWith("gglm"))
            {
                foreach (KeyValuePair<string, string> pair in this.cates)
                {
                    base.webbrowser.GetElementByID(true, "cate" + pair.Key).style.display = "none";
                    base.webbrowser.GetElementByID(true, "cate" + pair.Key).innerHTML = "";
                }
                base.webbrowser.GetElementByID(true, string_0.Replace("gglm", "cate")).style.display = "block";
                base.webbrowser.GetElementByID(true, string_0.Replace("gglm", "cate")).innerHTML = "<div id=\"cate>control\"></div>";
                base.cate.value = new List<cate>();
                base.cate.catestr = string_0.Replace("gglm", "");
                base.cate.setMapControl();
            }
            else if (string_0.StartsWith("chcates"))
            {
                this.method_18(string_0);
            }
            else if (string_0.StartsWith("dysx"))
            {
                this.method_15(string_0);
            }
            else if (string_0.StartsWith("saleattrname"))
            {
                this.method_20(string_0);
            }
            else if (string_0.StartsWith("chsaleattrname"))
            {
                this.method_17(string_0);
            }
            else if (string_0.StartsWith("cateattrname"))
            {
                this.method_19(string_0);
            }
            else if (string_0.StartsWith("chcateattrname"))
            {
                this.method_16(string_0);
            }
            else if (string_0.StartsWith("saleattrvalue"))
            {
                string[] strArray = string_0.Replace("saleattrvalue", "").Replace("rrrnnn", "\r").Split(new char[] { '\r' });
                string str2 = strArray[0];
                string str3 = strArray[1];
                this.saleattrvalues[str2][str3][strArray[2]] = func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value");
            }
            else if (string_0.StartsWith("cateattrvalue"))
            {
                string[] strArray2 = string_0.Replace("cateattrvalue", "").Replace("rrrnnn", "\r").Split(new char[] { '\r' });
                string str5 = strArray2[0];
                string str6 = strArray2[1];
                this.cateattrvalues[str5][str6][strArray2[2]] = func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value");
            }
            else if (string_0.StartsWith("unit"))
            {
                this.units[string_0.Replace("unit", "")] = func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value");
            }
            else if (string_0.StartsWith("savemap"))
            {
                this.saveOk = false;
                if (string_0.EndsWith("2"))
                {
                    this.saveOk = true;
                }
                else if (MessageBox.Show("确定要保存吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (KeyValuePair<string, double> pair2 in func.getNewDic2(this.currencyRate))
                    {
                        if (pair2.Key != this.toCurrencyUnit)
                        {
                            this.currencyRate[pair2.Key] = text.toDouble(func.getAttrByElem(base.webbrowser.GetElementByID(true, "currencyRate" + pair2.Key), "value"));
                            if (this.currencyRate[pair2.Key] <= 0.0)
                            {
                                this.currencyRate[pair2.Key] = 1.0;
                            }
                        }
                    }
                    this.method_14();
                }
            }
        }

        public void handlevaluechange(string string_0)
        {
            base.cate.handleEditValueChange(string_0.Replace("cate>", ""));
        }

        public void loadData()
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.storeId, @"\pro.jpg" });
            string str = "select rowid,fromsiteid from product where state=0";
            DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    this.idli[text.toInt(row[0].ToString())] = row[1].ToString().Trim();
                }
            }
            Dictionary<string, string> dictionary = base.cate.getFullPathText(base.toLanguage);
            base.unit.getBaseDataFromDataBase();
            foreach (KeyValuePair<int, string> pair in this.idli)
            {
                base.id = pair.Key;
                base.loadFromDataBase();
                this.currencyRate[base.currencyUnit] = 1.0;
                string fullName = base.cate.fullName;
                this.cates[fullName] = "";
                this.transDic[fullName] = base.cate.fullNameToLang;
                if (!this.saleattrnames.ContainsKey(fullName))
                {
                    this.saleattrnames[fullName] = new Dictionary<string, string>();
                }
                if (!this.cateattrnames.ContainsKey(fullName))
                {
                    this.cateattrnames[fullName] = new Dictionary<string, string>();
                }
                if (!this.saleattrvalues.ContainsKey(fullName))
                {
                    this.saleattrvalues[fullName] = new Dictionary<string, Dictionary<string, string>>();
                }
                if (!this.cateattrvalues.ContainsKey(fullName))
                {
                    this.cateattrvalues[fullName] = new Dictionary<string, Dictionary<string, string>>();
                }
                string getLastCateID = base.cate.getLastCateID;
                base.saleAttr.baseData = new List<option>();
                base.cateAttr.baseData = new List<attr>();
                if (((base.siteId == base.toSiteId) && (getLastCateID != "")) && dictionary.ContainsValue(getLastCateID))
                {
                    this.cates[fullName] = getLastCateID;
                    base.saleAttr.baseDataLinkId = getLastCateID;
                    if (!base.saleAttr.baseDataIsOwn)
                    {
                        base.saleAttr.getBaseDataFromDataBase();
                    }
                    base.cateAttr.baseDataLinkId = getLastCateID;
                    if (!base.cateAttr.baseDataIsOwn)
                    {
                        base.cateAttr.getBaseDataFromDataBase();
                    }
                }
                this.units[base.unit.value.text[base.defaultLanguage]] = "";
                this.transDic[base.unit.value.text[base.defaultLanguage]] = base.unit.value.text[base.toLanguage];
                if (((base.siteId == base.toSiteId) && (base.unit.value.id != "")) && base.unit.getMapData(base.toLanguage).ContainsValue(base.unit.value.id))
                {
                    this.units[base.unit.value.text[base.defaultLanguage]] = base.unit.value.id;
                }
                foreach (option option in base.saleAttr.value)
                {
                    this.saleattrnames[fullName][option.name.text[base.defaultLanguage]] = "";
                    this.transDic[option.name.text[base.defaultLanguage]] = option.name.text[base.toLanguage];
                    if (!this.saleattrvalues[fullName].ContainsKey(option.name.text[base.defaultLanguage]))
                    {
                        this.saleattrvalues[fullName][option.name.text[base.defaultLanguage]] = new Dictionary<string, string>();
                    }
                    if (((base.siteId == base.toSiteId) && (option.name.id != "")) && base.saleAttr.getMapName(base.toLanguage).ContainsValue(option.name.id))
                    {
                        this.saleattrnames[fullName][option.name.text[base.defaultLanguage]] = option.name.id;
                    }
                    foreach (optionValue value2 in option.optionValues)
                    {
                        this.saleattrvalues[fullName][option.name.text[base.defaultLanguage]][value2.value.text[base.defaultLanguage]] = "";
                        this.transDic[value2.value.text[base.defaultLanguage]] = value2.value.text[base.toLanguage];
                        if (((base.siteId == base.toSiteId) && (value2.value.id != "")) && base.saleAttr.getMapValue(base.toLanguage, option.name.id).ContainsValue(value2.value.id))
                        {
                            this.saleattrvalues[fullName][option.name.text[base.defaultLanguage]][value2.value.text[base.defaultLanguage]] = value2.value.id;
                        }
                    }
                }
                this.method_1(base.cateAttr.value, fullName, "", pair.Value);
            }
        }

        private void method_0(Dictionary<string, string> dictionary_0)
        {
            foreach (KeyValuePair<string, string> pair in func.getNewDic(dictionary_0))
            {
                dictionary_0[pair.Key] = "";
            }
        }

        private void method_1(List<attr> list_0, string string_0, string string_1, string string_2)
        {
            foreach (attr attr in list_0)
            {
                string id = "";
                id = attr.name.id;
                if (string_1 != "")
                {
                    id = string_1 + "-" + attr.name.id;
                }
                this.cateattrnames[string_0][attr.name.text[base.defaultLanguage]] = "";
                this.transDic[attr.name.text[base.defaultLanguage]] = attr.name.text[base.toLanguage];
                if (!this.cateattrvalues[string_0].ContainsKey(attr.name.text[base.defaultLanguage]))
                {
                    this.cateattrvalues[string_0][attr.name.text[base.defaultLanguage]] = new Dictionary<string, string>();
                }
                if (((base.siteId == base.toSiteId) && (id != "")) && base.cateAttr.getMapName(base.toLanguage).ContainsValue(id))
                {
                    this.cateattrnames[string_0][attr.name.text[base.defaultLanguage]] = id;
                }
                foreach (attrValue value2 in attr.value)
                {
                    if ((!string_2.StartsWith("up") || (!attr.isRadio && !attr.isMult)) || value2.isSelect)
                    {
                        string str2 = "";
                        str2 = id + "-" + value2.value.id;
                        this.cateattrvalues[string_0][attr.name.text[base.defaultLanguage]][value2.value.text[base.defaultLanguage]] = "";
                        this.transDic[value2.value.text[base.defaultLanguage]] = value2.value.text[base.toLanguage];
                        if (((base.siteId == base.toSiteId) && (value2.value.id != "")) && base.cateAttr.getMapValue(base.toLanguage, id).ContainsValue(value2.value.id))
                        {
                            this.cateattrvalues[string_0][attr.name.text[base.defaultLanguage]][value2.value.text[base.defaultLanguage]] = value2.value.id;
                        }
                        if (value2.attrs.Count > 0)
                        {
                            this.method_1(value2.attrs, string_0, str2, string_2);
                        }
                    }
                }
            }
        }

        private void method_10(string string_0)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in this.saleattrnames)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    string str = this.cates[pair.Key];
                    base.saleAttr.baseDataLinkId = str;
                    if (!base.saleAttr.baseDataIsOwn)
                    {
                        base.saleAttr.getBaseDataFromDataBase();
                    }
                    Dictionary<string, string> dictionary = base.saleAttr.getMapName(base.toLanguage);
                    foreach (KeyValuePair<string, string> pair2 in func.getNewDic(pair.Value))
                    {
                        if (pair.Value[pair2.Key] == "")
                        {
                            pair.Value[pair2.Key] = text.map(this.transDic[pair2.Key], dictionary, false, 60);
                        }
                    }
                }
            }
        }

        private void method_11(string string_0, string string_1)
        {
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair in this.saleattrvalues)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    string str = this.cates[pair.Key];
                    base.saleAttr.baseDataLinkId = str;
                    if (!base.saleAttr.baseDataIsOwn)
                    {
                        base.saleAttr.getBaseDataFromDataBase();
                    }
                    foreach (KeyValuePair<string, Dictionary<string, string>> pair2 in pair.Value)
                    {
                        if ((string_1 == "") || (string_1 == pair2.Key))
                        {
                            string str2 = this.saleattrnames[pair.Key][pair2.Key];
                            if (str2 != "")
                            {
                                Dictionary<string, string> dictionary = base.saleAttr.getMapValue(base.toLanguage, str2);
                                foreach (KeyValuePair<string, string> pair3 in func.getNewDic(pair2.Value))
                                {
                                    if (pair2.Value[pair3.Key] == "")
                                    {
                                        pair2.Value[pair3.Key] = text.map(this.transDic[pair3.Key], dictionary, false, 60);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_12(string string_0)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in this.cateattrnames)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    string str = this.cates[pair.Key];
                    base.cateAttr.baseDataLinkId = str;
                    if (!base.cateAttr.baseDataIsOwn)
                    {
                        base.cateAttr.getBaseDataFromDataBase();
                    }
                    Dictionary<string, string> dictionary = base.cateAttr.getMapName(base.toLanguage);
                    foreach (KeyValuePair<string, string> pair2 in func.getNewDic(pair.Value))
                    {
                        if (pair.Value[pair2.Key] == "")
                        {
                            pair.Value[pair2.Key] = text.map(this.transDic[pair2.Key], dictionary, false, 60);
                        }
                    }
                }
            }
        }

        private void method_13(string string_0, string string_1)
        {
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair in this.cateattrvalues)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    string str = this.cates[pair.Key];
                    base.cateAttr.baseDataLinkId = str;
                    if (!base.cateAttr.baseDataIsOwn)
                    {
                        base.cateAttr.getBaseDataFromDataBase();
                    }
                    foreach (KeyValuePair<string, Dictionary<string, string>> pair2 in pair.Value)
                    {
                        if ((string_1 == "") || (string_1 == pair2.Key))
                        {
                            string str2 = this.cateattrnames[pair.Key][pair2.Key];
                            if (str2 != "")
                            {
                                Dictionary<string, string> dictionary = base.cateAttr.getMapValue(base.toLanguage, str2);
                                foreach (KeyValuePair<string, string> pair3 in func.getNewDic(pair2.Value))
                                {
                                    if (pair2.Value[pair3.Key] == "")
                                    {
                                        pair2.Value[pair3.Key] = text.map(this.transDic[pair3.Key], dictionary, false, 60);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_14()
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in this.units)
            {
                if (pair.Value != "")
                {
                    builder.Append("delete from map where text='" + pair.Key.Replace("'", "''") + "' and key='unit';");
                    builder2.Append("insert into map (text,key,id) values ('" + pair.Key.Replace("'", "''") + "','unit','" + pair.Value.Replace("'", "''") + "') ;");
                }
            }
            foreach (KeyValuePair<string, string> pair2 in this.cates)
            {
                if (pair2.Value != "")
                {
                    builder.Append("delete from map where text='" + pair2.Key.Replace("'", "''") + "' and key='cate';");
                    builder2.Append("insert into map (text,key,id) values ('" + pair2.Key.Replace("'", "''") + "','cate','" + pair2.Value.Replace("'", "''") + "') ;");
                }
            }
            foreach (KeyValuePair<string, Dictionary<string, string>> pair3 in this.saleattrnames)
            {
                foreach (KeyValuePair<string, string> pair4 in pair3.Value)
                {
                    if (pair4.Value != "")
                    {
                        builder.Append("delete from map where text='" + pair4.Key.Replace("'", "''") + "' and key='saleattrnames' and cate='" + pair3.Key.Replace("'", "''") + "';");
                        builder2.Append("insert into map (text,key,id,cate) values ('" + pair4.Key.Replace("'", "''") + "','saleattrnames','" + pair4.Value.Replace("'", "''") + "','" + pair3.Key.Replace("'", "''") + "') ;");
                    }
                }
            }
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair5 in this.saleattrvalues)
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> pair6 in pair5.Value)
                {
                    foreach (KeyValuePair<string, string> pair7 in pair6.Value)
                    {
                        if (pair7.Value != "")
                        {
                            builder.Append("delete from map where text='" + pair7.Key.Replace("'", "''") + "' and key='saleattrvalues' and cate='" + pair5.Key.Replace("'", "''") + "' and attrname='" + pair6.Key.Replace("'", "''") + "';");
                            builder2.Append("insert into map (text,key,id,cate,attrname) values ('" + pair7.Key.Replace("'", "''") + "','saleattrvalues','" + pair7.Value.Replace("'", "''") + "','" + pair5.Key.Replace("'", "''") + "','" + pair6.Key.Replace("'", "''") + "') ;");
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, Dictionary<string, string>> pair8 in this.cateattrnames)
            {
                foreach (KeyValuePair<string, string> pair9 in pair8.Value)
                {
                    if (pair9.Value != "")
                    {
                        builder.Append("delete from map where text='" + pair9.Key.Replace("'", "''") + "' and key='cateattrnames' and cate='" + pair8.Key.Replace("'", "''") + "';");
                        builder2.Append("insert into map (text,key,id,cate) values ('" + pair9.Key.Replace("'", "''") + "','cateattrnames','" + pair9.Value.Replace("'", "''") + "','" + pair8.Key.Replace("'", "''") + "') ;");
                    }
                }
            }
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair10 in this.cateattrvalues)
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> pair11 in pair10.Value)
                {
                    foreach (KeyValuePair<string, string> pair12 in pair11.Value)
                    {
                        if (pair12.Value != "")
                        {
                            builder.Append("delete from map where text='" + pair12.Key.Replace("'", "''") + "' and key='cateattrvalues' and cate='" + pair10.Key.Replace("'", "''") + "' and attrname='" + pair11.Key.Replace("'", "''") + "';");
                            builder2.Append("insert into map (text,key,id,cate,attrname) values ('" + pair12.Key.Replace("'", "''") + "','cateattrvalues','" + pair12.Value.Replace("'", "''") + "','" + pair10.Key.Replace("'", "''") + "','" + pair11.Key.Replace("'", "''") + "') ;");
                        }
                    }
                }
            }
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
            if (builder.ToString() != "")
            {
                SQLiteHelper.ExecuteNonQuery(builder.ToString(), CommandType.Text);
            }
            if (builder2.ToString() != "")
            {
                SQLiteHelper.ExecuteNonQuery(builder2.ToString(), CommandType.Text);
            }
            this.saveOk = true;
        }

        private void method_15(string string_0)
        {
            foreach (KeyValuePair<string, string> pair in this.cates)
            {
                base.webbrowser.GetElementByID(true, "cate" + pair.Key).style.display = "none";
                base.webbrowser.GetElementByID(true, "cate" + pair.Key).innerHTML = "";
            }
            base.webbrowser.GetElementByID(true, string_0.Replace("dysx", "cate")).style.display = "block";
            string str = string_0.Replace("dysx", "");
            StringBuilder builder = new StringBuilder();
            base.saleAttr.baseDataLinkId = this.cates[str];
            if (!base.saleAttr.baseDataIsOwn)
            {
                base.saleAttr.getBaseDataFromDataBase();
            }
            if ((this.saleattrnames[str].Count > 0) && (base.saleAttr.baseData.Count > 0))
            {
                builder.Append("<div><div style=\"float:left; width:250px;padding:5px;\"><b>原销售属性</b></div><div style=\"float:left;padding:5px;\"><b>对应后的销售属性</b></div></div>");
                foreach (KeyValuePair<string, string> pair2 in this.saleattrnames[str])
                {
                    builder.Append("<div style=\"clear:both;\" ><div style=\"float:left; width:250px;padding:5px;\">" + func.quotToHtml(pair2.Key) + "</div><div style=\"float:left;padding:5px;\"> <select id=\"saleattrname" + func.quotToHtml(str) + "rrrnnn" + func.quotToHtml(pair2.Key) + "\" onchange='userchange(this.id);' > <option value=''>请选择销售属性</option>");
                    string str2 = "none";
                    foreach (KeyValuePair<string, string> pair3 in base.saleAttr.getMapName(""))
                    {
                        if (pair3.Value == pair2.Value)
                        {
                            builder.Append("<option value=\"" + pair3.Value + "\" selected=\"selected\">" + func.quotToHtml(pair3.Key) + "</option>");
                            str2 = "block";
                        }
                        else
                        {
                            builder.Append("<option value=\"" + pair3.Value + "\">" + func.quotToHtml(pair3.Key) + "</option>");
                        }
                    }
                    builder.Append("</select></div><div style=\"float:left; padding:2px;\"><input style='display:" + str2 + ";' id=\"chsaleattrname" + func.quotToHtml(str) + "rrrnnn" + func.quotToHtml(pair2.Key) + "\" onclick='userchange(this.id);' type=\"button\" value=\"对应属性值\"  /></div></div>");
                    builder.Append("<div style=\"clear:both\"></div><div id=\"divattrname" + func.quotToHtml(str) + "rrrnnn" + func.quotToHtml(pair2.Key) + "\" style=\"display:none; background-color:#ffffff;border: 1px solid #cccccc; float:left; padding:10px;\" ></div>");
                }
            }
            base.cateAttr.baseDataLinkId = this.cates[str];
            if (!base.cateAttr.baseDataIsOwn)
            {
                base.cateAttr.getBaseDataFromDataBase();
            }
            if ((this.cateattrnames[str].Count > 0) && (base.cateAttr.baseData.Count > 0))
            {
                builder.Append("<div style=\"clear:both;\" ></div><div><div style=\"float:left; width:250px;padding:5px;\"><b>原类目属性</b></div><div style=\"float:left;padding:5px;\"><b>对应后的类目属性</b></div></div>");
                foreach (KeyValuePair<string, string> pair4 in this.cateattrnames[str])
                {
                    builder.Append("<div style=\"clear:both;\" ><div style=\"float:left; width:250px;padding:5px;\">" + func.quotToHtml(pair4.Key) + "</div><div style=\"float:left;padding:5px;\"> <select id=\"cateattrname" + func.quotToHtml(str) + "rrrnnn" + func.quotToHtml(pair4.Key) + "\" onchange='userchange(this.id);' > <option value=''>请选择类目属性</option>");
                    string str3 = "none";
                    foreach (KeyValuePair<string, string> pair5 in base.cateAttr.getMapName(""))
                    {
                        if (pair5.Value.Replace("isinput", "") == pair4.Value)
                        {
                            builder.Append("<option value=\"" + pair5.Value + "\" selected=\"selected\">" + func.quotToHtml(pair5.Key) + "</option>");
                            if (pair5.Value.Contains("isinput"))
                            {
                                str3 = "none";
                            }
                            else
                            {
                                str3 = "block";
                            }
                        }
                        else
                        {
                            builder.Append("<option value=\"" + pair5.Value + "\">" + func.quotToHtml(pair5.Key) + "</option>");
                        }
                    }
                    builder.Append("</select></div><div style=\"float:left; padding:2px;\"><input style='display:" + str3 + ";' id=\"chcateattrname" + func.quotToHtml(str) + "rrrnnn" + func.quotToHtml(pair4.Key) + "\" onclick='userchange(this.id);' type=\"button\" value=\"对应属性值\"  /></div></div>");
                    builder.Append("<div style=\"clear:both\"></div><div id=\"divattrname" + func.quotToHtml(str) + "rrrnnn" + func.quotToHtml(pair4.Key) + "\" style=\"display:none; background-color:#ffffff;border: 1px solid #cccccc; float:left; padding:10px;\" ></div>");
                }
            }
            if (builder.ToString() == "")
            {
                builder.Append("该类目无属性对应");
            }
            builder.Append("<div style=\"clear:both; text-align:right; padding-top:20px;\" ><input type=\"button\" value=\" 确定 \" onclick=\"document.getElementById('cate" + str.Replace("\"", "&quot;").Replace(@"\", @"\\").Replace("'", @"\'") + "').style.display='none';\" /></div>");
            base.webbrowser.GetElementByID(true, string_0.Replace("dysx", "cate")).innerHTML = builder.ToString();
        }

        private void method_16(string string_0)
        {
            string[] strArray = string_0.Replace("chcateattrname", "").Replace("rrrnnn", "\r").Split(new char[] { '\r' });
            string str2 = strArray[0];
            string str3 = strArray[1];
            foreach (KeyValuePair<string, string> pair in this.cateattrnames[str2])
            {
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).style.display = "none";
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).innerHTML = "";
            }
            base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + str3).style.display = "block";
            StringBuilder builder = new StringBuilder();
            builder.Append("<div><div style=\"float:left; width:150px;padding:5px;\"><b>原属性值</b></div><div style=\"float:left;padding:5px;\"><b>对应后的属性值</b></div></div>");
            base.cateAttr.baseDataLinkId = this.cates[str2];
            if (!base.cateAttr.baseDataIsOwn)
            {
                base.cateAttr.getBaseDataFromDataBase();
            }
            foreach (KeyValuePair<string, string> pair2 in this.cateattrvalues[str2][str3])
            {
                builder.Append("<div style=\"clear:both;\" ><div style=\"float:left; width:150px;padding:5px;\">" + pair2.Key + "</div><div style=\"float:left;padding:5px;\"> <select id=\"cateattrvalue" + func.quotToHtml(str2) + "rrrnnn" + func.quotToHtml(str3) + "rrrnnn" + func.quotToHtml(pair2.Key) + "\" onchange='userchange(this.id);' > <option value=''>请选择属性值</option>");
                foreach (KeyValuePair<string, string> pair3 in base.cateAttr.getMapValue("", this.cateattrnames[str2][str3]))
                {
                    if (pair3.Value == pair2.Value)
                    {
                        builder.Append("<option value=\"" + pair3.Value + "\" selected=\"selected\">" + pair3.Key + "</option>");
                    }
                    else
                    {
                        builder.Append("<option value=\"" + pair3.Value + "\">" + pair3.Key + "</option>");
                    }
                }
                builder.Append("</select></div>");
            }
            builder.Append("<div style=\"clear:both; text-align:right; padding-top:20px;\" ><input type=\"button\" value=\" 确定 \" onclick=\"document.getElementById('divattrname" + str2.Replace("\"", "&quot;").Replace(@"\", @"\\").Replace("'", @"\'") + "rrrnnn" + str3.Replace(@"\", @"\\").Replace("'", @"\'").Replace("\"", "&quot;") + "').style.display='none';\" /></div>");
            base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + str3).innerHTML = builder.ToString();
        }

        private void method_17(string string_0)
        {
            string[] strArray = string_0.Replace("chsaleattrname", "").Replace("rrrnnn", "\r").Split(new char[] { '\r' });
            string str2 = strArray[0];
            string str3 = strArray[1];
            foreach (KeyValuePair<string, string> pair in this.saleattrnames[str2])
            {
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).style.display = "none";
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).innerHTML = "";
            }
            base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + str3).style.display = "block";
            StringBuilder builder = new StringBuilder();
            builder.Append("<div><div style=\"float:left; width:150px;padding:5px;\"><b>原属性值</b></div><div style=\"float:left;padding:5px;\"><b>对应后的属性值</b></div></div>");
            base.saleAttr.baseDataLinkId = this.cates[str2];
            if (!base.saleAttr.baseDataIsOwn)
            {
                base.saleAttr.getBaseDataFromDataBase();
            }
            foreach (KeyValuePair<string, string> pair2 in this.saleattrvalues[str2][str3])
            {
                builder.Append("<div style=\"clear:both;\" ><div style=\"float:left; width:150px;padding:5px;\">" + pair2.Key + "</div><div style=\"float:left;padding:5px;\"> <select id=\"saleattrvalue" + func.quotToHtml(str2) + "rrrnnn" + func.quotToHtml(str3) + "rrrnnn" + func.quotToHtml(pair2.Key) + "\" onchange='userchange(this.id);' > <option value=''>请选择属性值</option>");
                foreach (KeyValuePair<string, string> pair3 in base.saleAttr.getMapValue("", this.saleattrnames[str2][str3]))
                {
                    if (pair3.Value == pair2.Value)
                    {
                        builder.Append("<option value=\"" + pair3.Value + "\" selected=\"selected\">" + pair3.Key + "</option>");
                    }
                    else
                    {
                        builder.Append("<option value=\"" + pair3.Value + "\">" + pair3.Key + "</option>");
                    }
                }
                builder.Append("</select></div>");
            }
            builder.Append("<div style=\"clear:both; text-align:right; padding-top:20px;\" ><input type=\"button\" value=\" 确定 \" onclick=\"document.getElementById('divattrname" + str2.Replace("\"", "&quot;").Replace(@"\", @"\\").Replace("'", @"\'") + "rrrnnn" + str3.Replace("\"", "&quot;").Replace(@"\", @"\\").Replace("'", @"\'") + "').style.display='none';\" /></div>");
            base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + str3).innerHTML = builder.ToString();
        }

        private void method_18(string string_0)
        {
            base.webbrowser.GetElementByID(true, string_0.Replace("chcates", "cate")).style.display = "none";
            base.webbrowser.GetElementByID(true, string_0.Replace("chcates", "cate")).innerHTML = "";
            string str = string_0.Replace("chcates", "");
            if (this.cates[str] != base.cate.getLastCateID)
            {
                this.cates[str] = base.cate.getLastCateID;
                base.webbrowser.GetElementByID(true, string_0.Replace("chcates", "catetext")).setAttribute("value", base.cate.fullName, 0);
                this.method_4(str);
                this.method_5(str, "");
                this.method_6(str);
                this.method_7(str, "");
                this.method_10(str);
                this.method_11(str, "");
                this.method_12(str);
                this.method_13(str, "");
            }
        }

        private void method_19(string string_0)
        {
            string[] strArray = string_0.Replace("cateattrname", "").Replace("rrrnnn", "\r").Split(new char[] { '\r' });
            string str2 = strArray[0];
            string str3 = strArray[1];
            foreach (KeyValuePair<string, string> pair in this.cateattrnames[str2])
            {
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).style.display = "none";
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).innerHTML = "";
            }
            if (this.cateattrnames[str2][str3] != func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value").Replace("isinput", ""))
            {
                if (!func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value").Contains("isinput") && !(func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value") == ""))
                {
                    base.webbrowser.GetElementByID(true, "ch" + string_0).style.display = "block";
                }
                else
                {
                    base.webbrowser.GetElementByID(true, "ch" + string_0).style.display = "none";
                }
                this.cateattrnames[str2][str3] = func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value").Replace("isinput", "");
                this.method_7(str2, str3);
                this.method_13(str2, str3);
            }
        }

        private void method_2()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
            string str = "select * from map where key='unit'";
            DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if ((this.units.ContainsKey(row["text"].ToString().Trim()) && (this.units[row["text"].ToString()] == "")) && base.unit.getMapData(base.toLanguage).ContainsValue(row["id"].ToString().Trim()))
                    {
                        this.units[row["text"].ToString()] = row["id"].ToString().Trim();
                    }
                }
            }
        }

        private void method_20(string string_0)
        {
            string[] strArray = string_0.Replace("saleattrname", "").Replace("rrrnnn", "\r").Split(new char[] { '\r' });
            string str2 = strArray[0];
            string str3 = strArray[1];
            foreach (KeyValuePair<string, string> pair in this.saleattrnames[str2])
            {
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).style.display = "none";
                base.webbrowser.GetElementByID(true, "divattrname" + str2 + "rrrnnn" + pair.Key).innerHTML = "";
            }
            if (this.saleattrnames[str2][str3] != func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value"))
            {
                if (func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value") == "")
                {
                    base.webbrowser.GetElementByID(true, "ch" + string_0).style.display = "none";
                }
                else
                {
                    base.webbrowser.GetElementByID(true, "ch" + string_0).style.display = "block";
                }
                this.saleattrnames[str2][str3] = func.getAttrByElem(base.webbrowser.GetElementByID(true, string_0), "value");
                this.method_5(str2, str3);
                this.method_11(str2, str3);
            }
        }

        private void method_3()
        {
            SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
            string str = "select * from map where key='cate'";
            DataSet set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                Dictionary<string, string> dictionary = base.cate.getFullPathText(base.toLanguage);
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if ((this.cates.ContainsKey(row["text"].ToString()) && (this.cates[row["text"].ToString()] == "")) && dictionary.ContainsValue(row["id"].ToString().Trim()))
                    {
                        this.cates[row["text"].ToString()] = row["id"].ToString().Trim();
                    }
                }
            }
        }

        private void method_4(string string_0)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in this.saleattrnames)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    if ((string_0 != "") && (string_0 == pair.Key))
                    {
                        this.method_0(this.saleattrnames[pair.Key]);
                    }
                    string str = this.cates[pair.Key];
                    if (str != "")
                    {
                        base.saleAttr.baseDataLinkId = str;
                        if (!base.saleAttr.baseDataIsOwn)
                        {
                            base.saleAttr.getBaseDataFromDataBase();
                        }
                        SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
                        DataSet set = SQLiteHelper.ExecuteDataSet("select * from map where key='saleattrnames' and cate='" + pair.Key.Replace("'", "''") + "'", CommandType.Text);
                        if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                        {
                            foreach (DataRow row in set.Tables[0].Rows)
                            {
                                if ((this.saleattrnames[pair.Key].ContainsKey(row["text"].ToString()) && (this.saleattrnames[pair.Key][row["text"].ToString()] == "")) && base.saleAttr.getMapName(base.toLanguage).ContainsValue(row["id"].ToString().Trim()))
                                {
                                    this.saleattrnames[pair.Key][row["text"].ToString()] = row["id"].ToString().Trim();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_5(string string_0, string string_1)
        {
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair in this.saleattrvalues)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    string str = this.cates[pair.Key];
                    if (str != "")
                    {
                        base.saleAttr.baseDataLinkId = str;
                        if (!base.saleAttr.baseDataIsOwn)
                        {
                            base.saleAttr.getBaseDataFromDataBase();
                        }
                        SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
                        foreach (KeyValuePair<string, Dictionary<string, string>> pair2 in this.saleattrvalues[pair.Key])
                        {
                            if ((string_1 == "") || (string_1 == pair2.Key))
                            {
                                if ((string_1 != "") && (string_1 == pair2.Key))
                                {
                                    this.method_0(this.saleattrvalues[pair.Key][pair2.Key]);
                                }
                                DataSet set = SQLiteHelper.ExecuteDataSet("select * from map where key='saleattrvalues' and cate='" + pair.Key.Replace("'", "''") + "' and attrname='" + pair2.Key.Replace("'", "''") + "'", CommandType.Text);
                                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                                {
                                    foreach (DataRow row in set.Tables[0].Rows)
                                    {
                                        if (this.saleattrvalues[pair.Key][pair2.Key].ContainsKey(row["text"].ToString()))
                                        {
                                            string str3 = this.saleattrnames[pair.Key][pair2.Key];
                                            if (((str3 != "") && (this.saleattrvalues[pair.Key][pair2.Key][row["text"].ToString()] == "")) && base.saleAttr.getMapValue(base.toLanguage, str3).ContainsValue(row["id"].ToString().Trim()))
                                            {
                                                this.saleattrvalues[pair.Key][pair2.Key][row["text"].ToString()] = row["id"].ToString().Trim();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_6(string string_0)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in this.cateattrnames)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    if ((string_0 != "") && (string_0 == pair.Key))
                    {
                        this.method_0(this.cateattrnames[pair.Key]);
                    }
                    string str = this.cates[pair.Key];
                    if (str != "")
                    {
                        base.cateAttr.baseDataLinkId = str;
                        if (!base.cateAttr.baseDataIsOwn)
                        {
                            base.cateAttr.getBaseDataFromDataBase();
                        }
                        SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
                        DataSet set = SQLiteHelper.ExecuteDataSet("select * from map where key='cateattrnames' and cate='" + pair.Key.Replace("'", "''") + "'", CommandType.Text);
                        if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                        {
                            foreach (DataRow row in set.Tables[0].Rows)
                            {
                                if ((this.cateattrnames[pair.Key].ContainsKey(row["text"].ToString()) && (this.cateattrnames[pair.Key][row["text"].ToString()] == "")) && base.cateAttr.getMapName(base.toLanguage).ContainsValue(row["id"].ToString().Trim()))
                                {
                                    this.cateattrnames[pair.Key][row["text"].ToString()] = row["id"].ToString().Trim();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_7(string string_0, string string_1)
        {
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair in this.cateattrvalues)
            {
                if ((string_0 == "") || (string_0 == pair.Key))
                {
                    string str = this.cates[pair.Key];
                    if (str != "")
                    {
                        base.cateAttr.baseDataLinkId = str;
                        if (!base.cateAttr.baseDataIsOwn)
                        {
                            base.cateAttr.getBaseDataFromDataBase();
                        }
                        SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.siteId + @"\map.jpg";
                        foreach (KeyValuePair<string, Dictionary<string, string>> pair2 in this.cateattrvalues[pair.Key])
                        {
                            if ((string_1 == "") || (string_1 == pair2.Key))
                            {
                                if ((string_1 != "") && (string_1 == pair2.Key))
                                {
                                    this.method_0(this.cateattrvalues[pair.Key][pair2.Key]);
                                }
                                DataSet set = SQLiteHelper.ExecuteDataSet("select * from map where key='cateattrvalues' and cate='" + pair.Key.Replace("'", "''") + "' and attrname='" + pair2.Key.Replace("'", "''") + "'", CommandType.Text);
                                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                                {
                                    foreach (DataRow row in set.Tables[0].Rows)
                                    {
                                        if (this.cateattrvalues[pair.Key][pair2.Key].ContainsKey(row["text"].ToString()))
                                        {
                                            string str3 = this.cateattrnames[pair.Key][pair2.Key];
                                            if (((str3 != "") && (this.cateattrvalues[pair.Key][pair2.Key][row["text"].ToString()] == "")) && base.cateAttr.getMapValue(base.toLanguage, str3).ContainsValue(row["id"].ToString().Trim()))
                                            {
                                                this.cateattrvalues[pair.Key][pair2.Key][row["text"].ToString()] = row["id"].ToString().Trim();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_8()
        {
            Dictionary<string, string> dictionary = base.cate.getFullPathText(base.toLanguage);
            foreach (KeyValuePair<string, string> pair in func.getNewDic(this.cates))
            {
                if (this.cates[pair.Key] == "")
                {
                    this.cates[pair.Key] = text.map(this.transDic[pair.Key], dictionary, true, 0);
                }
            }
        }

        private void method_9()
        {
            Dictionary<string, string> dictionary = base.unit.getMapData(base.toLanguage);
            foreach (KeyValuePair<string, string> pair in func.getNewDic(this.units))
            {
                if ((this.units[pair.Key] == "") && (pair.Key.Trim() != ""))
                {
                    this.units[pair.Key] = text.map(this.transDic[pair.Key], dictionary, false, 60);
                }
            }
        }

        public void show()
        {
            this.loadData();
            this.method_2();
            this.method_3();
            this.method_4("");
            this.method_5("", "");
            this.method_6("");
            this.method_7("", "");
            this.method_8();
            this.method_9();
            this.method_10("");
            this.method_11("", "");
            this.method_12("");
            this.method_13("", "");
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style=\"padding:20px;\"><div style='color:#ff0000;font-size:20px;'>以下类目及属性是由软件自动建立的对应关系，准确度不会太高，为了提高准确度，请您仔细检查这些对应关系，设置好的对应关系将会保存，下次自动调用。</div>");
            StringBuilder builder2 = new StringBuilder();
            foreach (KeyValuePair<string, double> pair in this.currencyRate)
            {
                if (pair.Key != this.toCurrencyUnit)
                {
                    builder2.Append("<div><b>1" + pair.Key + " = <input id=\"currencyRate" + func.quotToHtml(pair.Key) + "\" type=\"text\" value=\"\" size=\"8\" />" + this.toCurrencyUnit + "</b></div>");
                }
            }
            if (builder2.ToString().Length > 0)
            {
                builder.Append("<div style=\"clear:both; padding-top:20px;\"><h2>设置汇率</h2></div>");
                builder.Append(builder2.ToString());
            }
            builder.Append("<div style=\"clear:both; padding-top:20px;\"><h2>对应类目及属性</h2></div><div><div style=\"float:left; width:400px;padding:5px; \"><b>原类目</b></div><div style=\"float:left;padding:5px; padding-left:0;\"><b>对应后的类目</b></div></div>");
            Dictionary<string, string> dictionary = base.cate.getFullPathTextByCids(base.toLanguage, this.cates);
            foreach (KeyValuePair<string, string> pair2 in this.cates)
            {
                builder.Append("<div style=\"clear:both;\" ><div style=\"float:left; width:400px;padding:5px;\">" + func.quotToHtml(pair2.Key) + "</div><div style=\"float:left; padding:0px; \">");
                builder.Append("<input id=\"catetext" + func.quotToHtml(pair2.Key) + "\" type=\"text\" readonly=\"readonly\" value=\"" + func.quotToHtml(dictionary[pair2.Value]) + "\" size=\"60\" />&nbsp;<input id=\"gglm" + func.quotToHtml(pair2.Key) + "\" type=\"button\" value=\"更改类目\" onclick='userchange(this.id);' /><input id=\"dysx" + pair2.Key.Replace("\"", "&quot;") + "\" onclick='userchange(this.id);' type=\"button\" value=\"对应属性\" /></div></div>");
                builder.Append("<div style=\"clear:both\"></div><div id=\"cate" + func.quotToHtml(pair2.Key) + "\" style=\" display:none; background-color:#FFFAE0; border: 1px solid #F1D38B;float:left; padding:10px;\"></div>");
            }
            if ((base.unit.baseData.Count > 0) && (this.units.Count > 0))
            {
                StringBuilder builder3 = new StringBuilder();
                foreach (KeyValuePair<string, string> pair3 in this.units)
                {
                    if (pair3.Key != "")
                    {
                        builder3.Append("<div style=\"clear:both;\" ><div style=\"float:left; width:150px;padding:5px;\">" + func.quotToHtml(pair3.Key) + "</div><div style=\"float:left; padding:5px;\"><select id=\"unit" + func.quotToHtml(pair3.Key) + "\" onchange='userchange(this.id);'><option value=\"\">请选择计量单位</option>");
                        foreach (KeyValuePair<string, string> pair4 in base.unit.getMapData(""))
                        {
                            if (pair4.Value == pair3.Value)
                            {
                                builder3.Append("<option value=\"" + func.quotToHtml(pair4.Value) + "\" selected=\"selected\">" + func.quotToHtml(pair4.Key) + "</option>");
                            }
                            else
                            {
                                builder3.Append("<option value=\"" + func.quotToHtml(pair4.Value) + "\">" + func.quotToHtml(pair4.Key) + "</option>");
                            }
                        }
                        builder3.Append("</select> </div></div>");
                    }
                }
                if (builder3.ToString().Length > 0)
                {
                    builder.Append("<div style=\"clear:both; padding-top:20px;\"><h2>对应计量单位</h2></div><div><div style=\"float:left; width:150px;padding:5px; \"><b>原计量单位</b></div><div style=\"float:left;padding:5px;\"><b>对应后的计量单位</b></div></div>");
                    builder.Append(builder3.ToString());
                    builder3 = null;
                }
            }
            builder.Append("</div>");
            base.webbrowser.GetActiveDocument().body.innerHTML = builder.ToString();
        }
    }
}


namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class cateAttrP : baseP
    {
        public Dictionary<string, string> allBatchCates = new Dictionary<string, string>();
        public List<attr> baseData = new List<attr>();
        private Dictionary<string, List<attr>> dictionary_0 = new Dictionary<string, List<attr>>();
        private string string_0 = "";
        public List<attr> value = new List<attr>();

        public cateAttrP(fatherObj fatherObj_0)
        {
            base.key = "cateAttr";
            base.name = "类目属性";
            base.dataBaseFieldForSave = "f11";
            base.dataBaseFieldForList = "f12";
            base.dataBaseFieldForSearch = "f13";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.tipTextForBatch = "请勾选要批量修改的属性";
        }

        public override void batchEdit()
        {
            if (this.dictionary_0.ContainsKey(base.fatherObj.cate.getLastCateID))
            {
                foreach (attr attr in this.dictionary_0[base.fatherObj.cate.getLastCateID])
                {
                    if (attr.isBatch)
                    {
                        this.method_5(attr, new List<string>());
                    }
                }
            }
        }

        public override void forceEdit()
        {
            this.method_4(this.value);
        }

        public void getAllBatchCate(List<int> list_0)
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\pro.jpg" });
            List<string> list = new List<string>();
            DataSet set = SQLiteHelper.ExecuteDataSet("SELECT distinct(" + base.fatherObj.cate.dataBaseFieldForList + ") FROM product where rowid in(" + func.listToStr2(list_0, ",", "") + ")", CommandType.Text);
            if (set.Tables.Count > 0)
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    list.Add(row[0].ToString());
                }
            }
            if (base.fatherObj.cate.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\cate.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\cate.jpg";
            }
            foreach (string str2 in list)
            {
                string str;
                if (base.fatherObj.defaultLanguage == "zh-cn")
                {
                    str = "select value from cate where text_cn_path ='" + str2.Replace("'", "''") + "' and is_Leaf='True'";
                }
                else
                {
                    str = "select value from cate where text_en_path ='" + str2.Replace("'", "''") + "' and is_Leaf='True'";
                }
                set = SQLiteHelper.ExecuteDataSet(str, CommandType.Text);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    this.allBatchCates[set.Tables[0].Rows[0][0].ToString().Trim()] = str2;
                }
            }
        }

        public override void getBaseDataFromDataBase()
        {
            this.baseData = new List<attr>();
            if (base.baseDataLinkId != "")
            {
                if (base.baseDataIsOwn)
                {
                    SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\attr.jpg" });
                }
                else
                {
                    SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\attr.jpg";
                }
                DataSet set = SQLiteHelper.ExecuteDataSet("select cateattr from attr where linkid='" + base.baseDataLinkId + "'", CommandType.Text);
                if (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && (set.Tables[0].Rows[0]["cateattr"].ToString() != ""))
                {
                    foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(set.Tables[0].Rows[0]["cateattr"].ToString()))
                    {
                        this.baseData.Add(attr.getValueFromJObject((JObject) token));
                    }
                }
                if (this.baseData.Count == 0)
                {
                    this.getBaseDataFromWeb();
                }
            }
        }

        public override void getBaseDataFromWeb()
        {
            JObject obj2 = new JObject();
            obj2["siteid"] = base.fatherObj.siteId;
            obj2["cateid"] = base.baseDataLinkId;
            JObject obj3 = ERRypu.server("attr", obj2);
            if (obj3["r"].ToString() == "")
            {
                try
                {
                    string str = obj3["d"]["data"].ToString();
                    string str2 = obj3["d"]["cateattr"].ToString();
                    string str3 = obj3["d"]["saleattr"].ToString();
                    this.saveBaseData(str, str2, str3);
                    this.baseData = new List<attr>();
                    if (str2 != "")
                    {
                        foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(str2))
                        {
                            this.baseData.Add(attr.getValueFromJObject((JObject) token));
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public override string getDataBaseFromValue()
        {
            JArray array = new JArray();
            foreach (attr attr in this.value)
            {
                array.Add(attr.getJObject);
            }
            return JsonConvert.SerializeObject(array);
        }

        public List<at> getListFromValue()
        {
            List<at> list = new List<at>();
            this.method_0(list, this.value);
            return list;
        }

        public Dictionary<string, string> getMapName(string string_1)
        {
            if ((string_1 != "zh-cn") && (string_1 != "en"))
            {
                string_1 = "";
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            this.method_8(string_1, this.baseData, dictionary, "");
            return dictionary;
        }

        public Dictionary<string, string> getMapValue(string string_1, string string_2)
        {
            if ((string_1 != "zh-cn") && (string_1 != "en"))
            {
                string_1 = "";
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (string_2 != "")
            {
                List<string> list = new List<string>();
                foreach (string str in string_2.Split(new char[] { '-' }))
                {
                    list.Add(str);
                }
                foreach (attrValue value2 in attr.getAttrByIds(this.baseData, list).value)
                {
                    if (value2.value.id != "")
                    {
                        if (string_1 == "")
                        {
                            dictionary[value2.value.GetFullText] = value2.value.id;
                        }
                        else
                        {
                            dictionary[value2.value.text[string_1]] = value2.value.id;
                        }
                    }
                }
            }
            return dictionary;
        }

        public override void getTranslate()
        {
            this.method_6(this.value);
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_1)
        {
            List<attr> list = new List<attr>();
            foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(string_1))
            {
                list.Add(attr.getValueFromJObject((JObject) token));
            }
            if ((base.fatherObj.state != 0) && !base.fatherObj.Store.isOwn)
            {
                this.value = this.baseData;
                foreach (attr attr in list)
                {
                    this.method_2(attr, new List<string>());
                }
            }
            else
            {
                this.value = list;
            }
        }

        public override void getValueFromEditC()
        {
        }

        public override object getValueFromSearchC()
        {
            return null;
        }

        public override void handleBatchUserChange(string string_1)
        {
        }

        public override void handleBatchValueChange(string string_1)
        {
            if (string_1.EndsWith("cate"))
            {
                if (func.getAttrByElem(base.elem(string_1), "value") == "")
                {
                    this.string_0 = "";
                    this.value = new List<attr>();
                    this.setBatchControl();
                }
                else
                {
                    if (this.string_0 != "")
                    {
                        this.dictionary_0[this.string_0] = this.value;
                    }
                    this.string_0 = func.getAttrByElem(base.elem(string_1), "value");
                    if (this.dictionary_0.ContainsKey(this.string_0))
                    {
                        this.value = this.dictionary_0[this.string_0];
                    }
                    else
                    {
                        base.baseDataLinkId = this.string_0;
                        this.getBaseDataFromDataBase();
                        this.value = this.baseData;
                        this.dictionary_0[this.string_0] = this.value;
                    }
                    this.setBatchControl();
                }
            }
            else
            {
                this.handleEditValueChange(string_1);
            }
        }

        public override void handleEditUserChange(string string_1)
        {
        }

        public override void handleEditValueChange(string string_1)
        {
            string[] strArray = func.正则替换(string_1, "[irmc]", "").Split(new char[] { '-' });
            attr attr = new attr();
            attrValue value2 = new attrValue();
            value2.attrs = this.value;
            int a = 0;
            foreach (string str in strArray)
            {
                if (str != "")
                {
                    int num2;
                    Math.DivRem(a, 2, out num2);
                    int num3 = text.toInt(str);
                    if (num2 == 0)
                    {
                        attr = value2.attrs[num3];
                    }
                    else
                    {
                        value2 = attr.value[num3];
                    }
                    a++;
                }
            }
            if (string_1.EndsWith("c"))
            {
                attr.isBatch = (bool) base.elem(string_1).getAttribute("checked", 0);
            }
            else
            {
                if (string_1.EndsWith("i"))
                {
                    attr.value[0].value.text[base.fatherObj.defaultLanguage] = func.getAttrByElem(base.elem(string_1), "value");
                }
                else if (string_1.EndsWith("r"))
                {
                    int num4 = 0;
                    foreach (attrValue value3 in attr.value)
                    {
                        if (text.toInt(func.getAttrByElem(base.elem(string_1), "value")) == num4)
                        {
                            value3.isSelect = true;
                        }
                        else
                        {
                            value3.isSelect = false;
                        }
                        num4++;
                    }
                }
                else if (string_1.EndsWith("m"))
                {
                    value2.isSelect = (bool) base.elem(string_1).getAttribute("checked", 0);
                }
                if (base.fatherObj.editType == "edit")
                {
                    this.setEditControl();
                }
                else if (base.fatherObj.editType == "batch")
                {
                    this.setBatchControl();
                }
            }
        }

        public override void handleSearchUserChange(string string_1)
        {
        }

        public override void handleSearchValueChange(string string_1)
        {
        }

        public override void initValue()
        {
            this.value = new List<attr>();
        }

        private void method_0(List<at> list_0, List<attr> list_1)
        {
            foreach (attr attr in list_1)
            {
                at item = new at();
                item.p.id = attr.name.id;
                item.p.text = attr.name.text[base.fatherObj.defaultLanguage];
                foreach (attrValue value2 in attr.value)
                {
                    vt vt = new vt();
                    if (attr.isInput)
                    {
                        if (value2.value.text[base.fatherObj.defaultLanguage].Length > 0)
                        {
                            vt.id = value2.value.id;
                            vt.text = value2.value.text[base.fatherObj.defaultLanguage];
                            item.v.Add(vt);
                        }
                    }
                    else if (value2.isSelect)
                    {
                        vt.id = value2.value.id;
                        vt.text = value2.value.text[base.fatherObj.defaultLanguage];
                        item.v.Add(vt);
                    }
                }
                if (item.v.Count > 0)
                {
                    list_0.Add(item);
                    foreach (attrValue value3 in attr.value)
                    {
                        if (attr.isInput)
                        {
                            if (value3.value.text[base.fatherObj.defaultLanguage].Length > 0)
                            {
                                this.method_0(list_0, value3.attrs);
                            }
                        }
                        else if (value3.isSelect)
                        {
                            this.method_0(list_0, value3.attrs);
                        }
                    }
                }
            }
        }

        private void method_1(StringBuilder stringBuilder_0, List<attr> list_0, string string_1, bool bool_0)
        {
            int num = 0;
            int num2 = 0;
            foreach (attr attr in list_0)
            {
                string str = "";
                if (attr.isMust && (attr.name.id != ""))
                {
                    str = "<font color='#FF0000'>*&nbsp;</font>";
                }
                string str2 = string.Concat(new object[] { base.KeyId, string_1, "-", num });
                string str3 = string_1 + "-" + num;
                string str4 = "";
                string str5 = "";
                if (attr.isBatch)
                {
                    str5 = "checked='checked'";
                }
                if (bool_0)
                {
                    str4 = "<input style='display:inline;' id='" + str2 + "c' type='checkbox' onclick='valuechange(this.id);' " + str5 + " /><font color='#660066' style='font-size:13px;display:inline;'>是否修改此属性&nbsp;&nbsp;</font>";
                }
                else
                {
                    str4 = "<input style='display:none;' id='" + str2 + "c' type='checkbox'/><font color='#660066' style='font-size:13px;display:none;'>是否修改此属性&nbsp;&nbsp;</font>";
                }
                if ((attr.name.id == "") && !base.fatherObj.Store.isOwn)
                {
                    stringBuilder_0.Append("<div style='float:left;padding:2px;padding-top:14px; '>" + str4 + "</div><div style='float:left;padding-top:10px;'>");
                }
                else
                {
                    string str6 = "200px;text-align:right;";
                    if (bool_0)
                    {
                        str6 = "300px;";
                    }
                    stringBuilder_0.Append("<div style='clear:both'></div><div style='float:left;padding-top:14px;padding-right:15px; width:" + str6 + "'>" + str4 + str + attr.name.GetFullText + "</div><div style='float:left; padding-top:10px; overflow:auto;'>");
                }
                num2 = 0;
                if (!attr.isInput && !base.fatherObj.Store.isOwn)
                {
                    if (attr.isRadio)
                    {
                        string str8 = "<option value=''>请选择</option>";
                        if (attr.name.id == "")
                        {
                            str8 = "";
                        }
                        stringBuilder_0.Append("<select id='" + str2 + "r' onchange='valuechange(this.id);'>" + str8);
                        foreach (attrValue value2 in attr.value)
                        {
                            string str9 = "";
                            if (value2.isSelect)
                            {
                                str9 = "selected='selected'";
                            }
                            stringBuilder_0.Append(string.Concat(new object[] { "<option value='", num2, "' ", str9, " >", value2.value.GetFullText, "</option>" }));
                            num2++;
                        }
                        stringBuilder_0.Append("</select>");
                    }
                    else if (attr.isMult)
                    {
                        foreach (attrValue value3 in attr.value)
                        {
                            string str10 = "";
                            if (value3.isSelect)
                            {
                                str10 = "checked='checked'";
                            }
                            stringBuilder_0.Append(string.Concat(new object[] { "<input id='", str2, "-", num2, "m' type='checkbox' value='", num2, "' ", str10, " onclick='valuechange(this.id);' />", value3.value.GetFullText, "&nbsp;" }));
                            num2++;
                        }
                    }
                }
                else
                {
                    string str7 = "";
                    if (attr.isDigi)
                    {
                        str7 = "size='8'";
                    }
                    stringBuilder_0.Append("<input id='" + str2 + "i' type='text' value=\"" + func.quotToHtml(attr.value[0].value.text[base.fatherObj.defaultLanguage]) + "\" " + str7 + " onchange='valuechange(this.id);' />");
                }
                stringBuilder_0.Append("</div>");
                num2 = 0;
                foreach (attrValue value4 in attr.value)
                {
                    if (value4.isSelect || attr.isInput)
                    {
                        string str11 = str3 + "-" + num2;
                        this.method_1(stringBuilder_0, value4.attrs, str11, bool_0);
                    }
                    num2++;
                }
                num++;
            }
        }

        private void method_2(attr attr_0, List<string> list_0)
        {
            List<string> list = new List<string>();
            foreach (string str in list_0)
            {
                list.Add(str);
            }
            list.Add(attr_0.name.id);
            attr attr = attr.getAttrByIds(this.value, list);
            foreach (attrValue value2 in attr_0.value)
            {
                foreach (attrValue value3 in attr.value)
                {
                    if ((value3.value.id != "") && (value2.value.id == value3.value.id))
                    {
                        value3.isSelect = value2.isSelect;
                        value3.value = value2.value;
                    }
                    else if (attr.isInput)
                    {
                        value3.value = value2.value;
                    }
                    else if ((value3.value.id == "") && (value3.value.text[base.fatherObj.defaultLanguage] == value2.value.text[base.fatherObj.defaultLanguage]))
                    {
                        value3.isSelect = value2.isSelect;
                    }
                }
            }
            foreach (attrValue value4 in attr_0.value)
            {
                list.Add(value4.value.id);
                foreach (attr attr2 in value4.attrs)
                {
                    this.method_2(attr2, list);
                }
                list.RemoveAt(list.Count - 1);
            }
        }

        private void method_3(List<attr> list_0, ref string string_1)
        {
            foreach (attr attr in list_0)
            {
                if (attr.isInput)
                {
                    if (attr.value[0].value.text[base.fatherObj.defaultLanguage].Length == 0)
                    {
                        if (attr.isMust)
                        {
                            string_1 = string_1 + "请输入" + attr.name.GetFullText + "<br/>";
                        }
                    }
                    else
                    {
                        if (attr.isDigi)
                        {
                            string str = text.check(text.toDouble(attr.value[0].value.text[base.fatherObj.defaultLanguage]), attr.isMust, attr.minValue, attr.maxValue, attr.num);
                            if (str != "")
                            {
                                string_1 = string_1 + str + "<br/>";
                            }
                        }
                        else
                        {
                            string str2 = text.check(attr.value[0].value.text[base.fatherObj.defaultLanguage], attr.minLength, attr.maxLength, attr.isMust, attr.allowZH, attr.banReg);
                            if (str2 != "")
                            {
                                string_1 = string_1 + str2 + "<br/>";
                            }
                        }
                        this.method_3(attr.value[0].attrs, ref string_1);
                    }
                }
                else
                {
                    int num = 0;
                    foreach (attrValue value2 in attr.value)
                    {
                        if (value2.isSelect)
                        {
                            num++;
                            this.method_3(value2.attrs, ref string_1);
                        }
                    }
                    if ((num == 0) && attr.isMust)
                    {
                        string_1 = string_1 + "请选择" + attr.name.GetFullText + "<br/>";
                    }
                }
            }
        }

        private void method_4(List<attr> list_0)
        {
            foreach (attr attr in list_0)
            {
                if (attr.isInput)
                {
                    if (attr.value[0].value.text[base.fatherObj.defaultLanguage].Length == 0)
                    {
                        if (attr.isMust)
                        {
                            attr.value[0].value.text[base.fatherObj.defaultLanguage] = "0";
                            this.method_4(attr.value[0].attrs);
                        }
                    }
                    else
                    {
                        if (attr.isDigi)
                        {
                            attr.minValue = 1.0;
                            attr.value[0].value.text[base.fatherObj.defaultLanguage] = text.change(attr.value[0].value.text[base.fatherObj.defaultLanguage], attr.isMust, attr.minValue, attr.maxValue, attr.num, 0.0).ToString();
                        }
                        else
                        {
                            attr.value[0].value.text[base.fatherObj.defaultLanguage] = text.change(attr.value[0].value.text[base.fatherObj.defaultLanguage], attr.minLength, attr.maxLength, attr.isMust, attr.allowZH, attr.banReg, "0");
                        }
                        this.method_4(attr.value[0].attrs);
                    }
                }
                else
                {
                    int num = 0;
                    foreach (attrValue value2 in attr.value)
                    {
                        if (value2.isSelect)
                        {
                            num++;
                            if (attr.isRadio && (num > 1))
                            {
                                value2.isSelect = false;
                            }
                            else
                            {
                                this.method_4(value2.attrs);
                            }
                        }
                    }
                    if (((num == 0) && attr.isMust) && (attr.value.Count > 0))
                    {
                        attr.value[0].isSelect = true;
                        this.method_4(attr.value[0].attrs);
                    }
                }
            }
        }

        private void method_5(attr attr_0, List<string> list_0)
        {
            List<string> list = new List<string>();
            foreach (string str in list_0)
            {
                list.Add(str);
            }
            list.Add(attr_0.name.id);
            attr attr = attr.getAttrByIds(this.value, list);
            foreach (attrValue value2 in attr_0.value)
            {
                foreach (attrValue value3 in attr.value)
                {
                    if (value2.value.id == value3.value.id)
                    {
                        value3.isSelect = value2.isSelect;
                        value3.value = value2.value;
                    }
                }
            }
            foreach (attrValue value4 in attr_0.value)
            {
                list.Add(value4.value.id);
                foreach (attr attr2 in value4.attrs)
                {
                    if (attr2.isBatch)
                    {
                        this.method_5(attr2, list);
                    }
                }
                list.RemoveAt(list.Count - 1);
            }
        }

        private void method_6(List<attr> list_0)
        {
            foreach (attr attr in list_0)
            {
                if (attr.name.text[base.fatherObj.defaultLanguage] != "")
                {
                    base.fatherObj.translateText[attr.name.text[base.fatherObj.defaultLanguage]] = "";
                }
                foreach (attrValue value2 in attr.value)
                {
                    if (value2.value.text[base.fatherObj.defaultLanguage] != "")
                    {
                        base.fatherObj.translateText[value2.value.text[base.fatherObj.defaultLanguage]] = "";
                    }
                    if (value2.attrs.Count > 0)
                    {
                        this.method_6(value2.attrs);
                    }
                }
            }
        }

        private void method_7(List<attr> list_0)
        {
            string str = "";
            foreach (attr attr in list_0)
            {
                str = base.fatherObj.translateText[attr.name.text[base.fatherObj.defaultLanguage]];
                if (str != "")
                {
                    attr.name.text[base.fatherObj.toLanguage] = str;
                    str = "";
                }
                else
                {
                    attr.name.text[base.fatherObj.toLanguage] = attr.name.text[base.fatherObj.defaultLanguage];
                }
                foreach (attrValue value2 in attr.value)
                {
                    str = base.fatherObj.translateText[value2.value.text[base.fatherObj.defaultLanguage]];
                    if (str != "")
                    {
                        value2.value.text[base.fatherObj.toLanguage] = str;
                        str = "";
                    }
                    else
                    {
                        value2.value.text[base.fatherObj.toLanguage] = value2.value.text[base.fatherObj.defaultLanguage];
                    }
                    if (value2.attrs.Count > 0)
                    {
                        this.method_7(value2.attrs);
                    }
                }
            }
        }

        private void method_8(string string_1, List<attr> list_0, Dictionary<string, string> dictionary_1, string string_2)
        {
            foreach (attr attr in list_0)
            {
                if (attr.name.id != "")
                {
                    string id = attr.name.id;
                    if (string_2 != "")
                    {
                        id = string_2 + "-" + id;
                    }
                    if (string_1 == "")
                    {
                        if (attr.isInput)
                        {
                            dictionary_1[attr.name.GetFullText] = id + "isinput";
                        }
                        else
                        {
                            dictionary_1[attr.name.GetFullText] = id;
                        }
                    }
                    else
                    {
                        dictionary_1[attr.name.text[string_1]] = id;
                    }
                    foreach (attrValue value2 in attr.value)
                    {
                        if (!(value2.value.id == ""))
                        {
                            string str2 = id + "-" + value2.value.id;
                            if (value2.attrs.Count > 0)
                            {
                                this.method_8(string_1, value2.attrs, dictionary_1, str2);
                            }
                        }
                    }
                }
            }
        }

        private List<string> method_9(string string_1)
        {
            List<string> list = new List<string>();
            foreach (string str in string_1.Split(new char[] { '-' }))
            {
                list.Add(str);
            }
            return list;
        }

        public override void saveBaseData()
        {
            JArray array = new JArray();
            foreach (attr attr in this.baseData)
            {
                array.Add(attr.getJObject);
            }
            this.saveBaseData("", JsonConvert.SerializeObject(array), "");
        }

        public void saveBaseData(string string_1, string string_2, string string_3)
        {
            if (((string_1 != "") || (string_2 != "")) || (string_3 != ""))
            {
                string str;
                if (base.baseDataIsOwn)
                {
                    SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\attr.jpg" });
                }
                else
                {
                    SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\attr.jpg";
                }
                if (SQLiteHelper.ExecuteDataSet("select count(rowid) from attr where linkid='" + base.baseDataLinkId + "'", 1).Tables[0].Rows[0][0].ToString() == "0")
                {
                    str = "insert into attr (linkid,data,cateattr,saleattr) values ('" + base.baseDataLinkId + "','" + string_1.Replace("'", "''") + "','" + string_2.Replace("'", "''") + "','" + string_3.Replace("'", "''") + "')";
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("update attr set ");
                    if (string_1 != "")
                    {
                        builder.Append("data='" + string_1.Replace("'", "''") + "',");
                    }
                    if (string_2 != "")
                    {
                        builder.Append("cateattr='" + string_2.Replace("'", "''") + "',");
                    }
                    if (string_3 != "")
                    {
                        builder.Append("saleattr='" + string_3.Replace("'", "''") + "',");
                    }
                    str = builder.ToString().TrimEnd(new char[] { ',' }) + " where linkid='" + base.baseDataLinkId + "'";
                }
                SQLiteHelper.ExecuteNonQuery(str, CommandType.Text);
            }
        }

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style='padding-bottom:20px'>请选择要批量修改属性的类目：<select id='" + base.KeyId + "cate' onchange='valuechange(this.id);'><option value=''>请选择类目</option>");
            foreach (KeyValuePair<string, string> pair in this.allBatchCates)
            {
                string str = "";
                if (pair.Key == this.string_0)
                {
                    str = "selected='selected'";
                }
                builder.Append("<option " + str + " value='" + pair.Key + "'>" + pair.Value + "</option>");
            }
            builder.Append("</select></div>");
            this.method_1(builder, this.value, "", true);
            builder.Append("<div style='clear:both'></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            if (this.value.Count == 0)
            {
                this.value = this.baseData;
            }
            StringBuilder builder = new StringBuilder();
            this.method_1(builder, this.value, "", false);
            builder.Append("<div style='clear:both'></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
        }

        public override void setTranslate()
        {
            this.method_7(this.value);
        }

        public override void setValueFromMap()
        {
            if (base.fatherObj.Store.isOwn)
            {
                this.value = base.oldPro.cateAttr.value;
            }
            else
            {
                base.baseDataLinkId = base.fatherObj.cate.getLastCateID;
                this.getBaseDataFromDataBase();
                this.value = this.baseData;
                foreach (attr attr in base.oldPro.cateAttr.value)
                {
                    string str = base.mapObj.cateattrnames[base.oldPro.cate.fullName][attr.name.text[base.oldPro.defaultLanguage]];
                    if (str != "")
                    {
                        string str2 = "";
                        foreach (attrValue value2 in attr.value)
                        {
                            if ((!base.fatherObj.fromSiteId.StartsWith("up") || (!attr.isRadio && !attr.isMult)) || value2.isSelect)
                            {
                                string str3 = base.mapObj.cateattrvalues[base.oldPro.cate.fullName][attr.name.text[base.oldPro.defaultLanguage]][value2.value.text[base.oldPro.defaultLanguage]];
                                if (str3 != "")
                                {
                                    attr.getAttrValueByIds(this.value, this.method_9(str + "-" + str3)).isSelect = true;
                                }
                                else
                                {
                                    str2 = str2 + value2.value.text[base.fatherObj.defaultLanguage] + ",";
                                }
                            }
                        }
                        if (str2 != "")
                        {
                            attr attr2 = attr.getAttrByIds(this.value, this.method_9(str));
                            if (attr2.isInput)
                            {
                                attr2.value[0].value.text[base.fatherObj.defaultLanguage] = str2.TrimEnd(new char[] { ',' });
                            }
                            else
                            {
                                base.fatherObj.customAttr.value[attr.name.text[base.fatherObj.defaultLanguage]] = str2.TrimEnd(new char[] { ',' });
                            }
                        }
                    }
                    else
                    {
                        string str4 = "";
                        foreach (attrValue value4 in attr.value)
                        {
                            if (!attr.isRadio && !attr.isMult)
                            {
                                if (value4.value.text[base.fatherObj.defaultLanguage] != "")
                                {
                                    str4 = str4 + value4.value.text[base.fatherObj.defaultLanguage] + ",";
                                }
                            }
                            else if (!base.fatherObj.fromSiteId.StartsWith("up") || value4.isSelect)
                            {
                                str4 = str4 + value4.value.text[base.fatherObj.defaultLanguage] + ",";
                            }
                        }
                        if (str4.Length > 0)
                        {
                            base.fatherObj.customAttr.value[attr.name.text[base.fatherObj.defaultLanguage]] = str4.TrimEnd(new char[] { ',' });
                        }
                    }
                }
            }
        }

        public override string verifyBatch(object object_0)
        {
            return this.verifyEdit();
        }

        public override string verifyEdit()
        {
            string str = "";
            this.method_3(this.value, ref str);
            return str;
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


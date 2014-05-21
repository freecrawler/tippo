namespace client
{
    using IfacesEnumsStructsClasses;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    public class saleAttrP : baseP
    {
        public bool allowZH;
        public List<string> banReg = new List<string>();
        public List<option> baseData = new List<option>();
        public List<option> defalu = new List<option>();
        public int maxLength;
        public int minLength;
        public List<option> value = new List<option>();

        public saleAttrP(fatherObj fatherObj_0)
        {
            base.key = "saleAttr";
            base.name = "销售属性";
            base.dataBaseFieldForSave = "f2";
            base.dataBaseFieldForList = "f3";
            base.dataBaseFieldForSearch = "f4";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void forceEdit()
        {
            foreach (option option in this.baseData)
            {
                if (!option.isMust)
                {
                    continue;
                }
                bool flag = false;
                using (List<option>.Enumerator enumerator2 = this.value.GetEnumerator())
                {
                    option current;
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (current.name.id == option.name.id)
                        {
                            goto Label_0068;
                        }
                    }
                    goto Label_00AC;
                Label_0068:
                    if ((current.optionValues.Count == 0) && (option.optionValues.Count > 0))
                    {
                        current.optionValues.Add(option.optionValues[0]);
                    }
                    flag = true;
                }
            Label_00AC:
                if (!flag)
                {
                    option item = new option();
                    item.name = option.name;
                    if (option.optionValues.Count > 0)
                    {
                        item.optionValues.Add(option.optionValues[0]);
                        this.value.Add(item);
                    }
                }
            }
            if ((this.value.Count <= 0) || !base.fatherObj.price.isHaveOption)
            {
                goto Label_01B0;
            }
            bool flag2 = false;
            using (List<option>.Enumerator enumerator3 = this.value.GetEnumerator())
            {
                while (enumerator3.MoveNext())
                {
                    option option4 = enumerator3.Current;
                    if (option4.optionValues.Count > 0)
                    {
                        goto Label_0167;
                    }
                }
                goto Label_017A;
            Label_0167:
                flag2 = true;
            }
        Label_017A:
            if (flag2)
            {
                List<sku> list = this.getNewPrice();
                this.method_0(base.fatherObj.price.value, list);
                base.fatherObj.price.value = list;
            }
        Label_01B0:
            foreach (option option5 in this.value)
            {
                foreach (optionValue value2 in option5.optionValues)
                {
                    if (value2.aliasName != "")
                    {
                        value2.aliasName = text.change(value2.aliasName, this.minLength, this.maxLength, false, this.allowZH, this.banReg, "");
                    }
                    if (value2.pic != "")
                    {
                        value2.pic = base.fatherObj.pics.forcePic(value2.pic);
                    }
                }
            }
        }

        public override void getBaseDataFromDataBase()
        {
            this.baseData = new List<option>();
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
                DataSet set = SQLiteHelper.ExecuteDataSet("select saleattr from attr where linkid='" + base.baseDataLinkId + "'", CommandType.Text);
                if (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && (set.Tables[0].Rows[0]["saleattr"].ToString() != ""))
                {
                    foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(set.Tables[0].Rows[0]["saleattr"].ToString()))
                    {
                        this.baseData.Add(option.getValueFromJObject((JObject) token));
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
                    base.fatherObj.cateAttr.baseDataLinkId = base.baseDataLinkId;
                    base.fatherObj.cateAttr.saveBaseData(str, str2, str3);
                    this.baseData = new List<option>();
                    if (str3 != "")
                    {
                        foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(str3))
                        {
                            this.baseData.Add(option.getValueFromJObject((JObject) token));
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
            foreach (option option in this.value)
            {
                array.Add(option.getJObject);
            }
            return JsonConvert.SerializeObject(array);
        }

        public Dictionary<string, string> getMapName(string string_0)
        {
            if ((string_0 != "zh-cn") && (string_0 != "en"))
            {
                string_0 = "";
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (option option in this.baseData)
            {
                if (string_0 == "")
                {
                    dictionary[option.name.GetFullText] = option.name.id;
                }
                else
                {
                    dictionary[option.name.text[string_0]] = option.name.id;
                }
            }
            return dictionary;
        }

        public Dictionary<string, string> getMapValue(string string_0, string string_1)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (string_1 != "")
            {
                foreach (option option in this.baseData)
                {
                    if (option.name.id == string_1)
                    {
                        foreach (optionValue value2 in option.optionValues)
                        {
                            if (value2.value.id != "")
                            {
                                if (string_0 == "")
                                {
                                    dictionary[value2.value.GetFullText] = value2.value.id;
                                }
                                else
                                {
                                    dictionary[value2.value.text[string_0]] = value2.value.id;
                                }
                            }
                        }
                    }
                }
            }
            return dictionary;
        }

        public List<sku> getNewPrice()
        {
            List<sku> list = new List<sku>();
            foreach (option option in this.value)
            {
                list = this.method_2(list, option);
            }
            return list;
        }

        public override void getTranslate()
        {
            foreach (option option in this.value)
            {
                if (option.name.text[base.fatherObj.defaultLanguage] != "")
                {
                    base.fatherObj.translateText[option.name.text[base.fatherObj.defaultLanguage]] = "";
                }
                foreach (optionValue value2 in option.optionValues)
                {
                    if (value2.value.text[base.fatherObj.defaultLanguage] != "")
                    {
                        base.fatherObj.translateText[value2.value.text[base.fatherObj.defaultLanguage]] = "";
                    }
                }
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = new List<option>();
            foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(string_0))
            {
                this.value.Add(option.getValueFromJObject((JObject) token));
            }
        }

        public override void getValueFromEditC()
        {
            int count;
            if (this.baseData.Count > 0)
            {
                count = this.baseData.Count;
            }
            else
            {
                count = this.value.Count;
            }
            this.value = new List<option>();
            for (int i = 0; i < count; i++)
            {
                IHTMLElementCollection elementsByName = base.webbrowser.GetElementsByName(true, base.KeyId + i + "option");
                option item = new option();
                if (elementsByName.length > 0)
                {
                    IHTMLElement element = (IHTMLElement) elementsByName.item(base.KeyId + i + "option", 0);
                    item.name.id = element.getAttribute("pid", 0).ToString().Trim();
                    item.name.text[base.fatherObj.defaultLanguage] = element.getAttribute("ptxt", 0).ToString().Trim();
                    if (func.getAttrByElem(element, "lp").ToLower() == "true")
                    {
                        item.isLinkPrice = true;
                    }
                    else
                    {
                        item.isLinkPrice = false;
                    }
                    if (func.getAttrByElem(element, "lpic").ToLower() == "true")
                    {
                        item.isEditPic = true;
                    }
                }
                foreach (IHTMLElement element2 in elementsByName)
                {
                    if ((bool) element2.getAttribute("checked", 0))
                    {
                        optionValue value2 = new optionValue();
                        value2.value.id = element2.getAttribute("vid", 0).ToString().Trim();
                        value2.value.text[base.fatherObj.defaultLanguage] = element2.getAttribute("vtxt", 0).ToString().Trim();
                        if ((this.baseData.Count > 0) && base.fatherObj.Store.isSaleAttrPic)
                        {
                            value2.pic = func.getLocalPath(func.getAttrByElem(base.elem2(element2.id + "pic"), "src"));
                            if (!value2.pic.EndsWith(".jpg") && !value2.pic.EndsWith(".gif"))
                            {
                                value2.pic = "";
                            }
                        }
                        item.optionValues.Add(value2);
                    }
                }
                this.value.Add(item);
            }
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
        }

        public override void handleEditUserChange(string string_0)
        {
            if (string_0.EndsWith("pic"))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = base.fatherObj.pics.picType;
                dialog.ShowDialog();
                string fileName = dialog.FileName;
                dialog.Dispose();
                if (fileName == "")
                {
                    return;
                }
                string str2 = base.fatherObj.pics.verifyPic(fileName);
                if (str2 != "")
                {
                    func.Message(str2);
                    return;
                }
                fileName = base.fatherObj.pics.saveLocalPic(fileName);
                base.elem(string_0).setAttribute("src", fileName, 0);
            }
            if (string_0.EndsWith("pic-del"))
            {
                base.elem(string_0.Replace("-del", "")).setAttribute("src", "", 0);
            }
        }

        public override void handleEditValueChange(string string_0)
        {
            base.handleEditValueChange(string_0);
            if ((base.fatherObj.editType == "edit") && base.fatherObj.price.isHaveOption)
            {
                List<sku> list = this.getNewPrice();
                base.fatherObj.price.getValueFromEditC();
                this.method_0(base.fatherObj.price.value, list);
                base.fatherObj.price.value = list;
                base.fatherObj.price.setEditControl();
            }
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = new List<option>();
        }

        private void method_0(List<sku> list_0, List<sku> list_1)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            int num = 0;
            foreach (sku sku in list_0)
            {
                dictionary[sku.getName] = num;
                num++;
            }
            foreach (sku sku2 in list_1)
            {
                num = this.method_1(sku2.getName, dictionary);
                if (num >= 0)
                {
                    sku2.isWholesale = list_0[num].isWholesale;
                    sku2.minOrderNum = list_0[num].minOrderNum;
                    sku2.off = list_0[num].off;
                    sku2.proNum = list_0[num].proNum;
                    sku2.skuCode = list_0[num].skuCode;
                    sku2.value = list_0[num].value;
                }
                else if (list_0.Count > 0)
                {
                    sku2.isWholesale = list_0[0].isWholesale;
                    sku2.minOrderNum = list_0[0].minOrderNum;
                    sku2.off = list_0[0].off;
                    sku2.proNum = list_0[0].proNum;
                    sku2.skuCode = list_0[0].skuCode;
                    sku2.value = list_0[0].value;
                }
            }
        }

        private int method_1(string string_0, Dictionary<string, int> dictionary_0)
        {
            foreach (KeyValuePair<string, int> pair in dictionary_0)
            {
                if (string_0 == pair.Key)
                {
                    return pair.Value;
                }
            }
            return -1;
        }

        private List<sku> method_2(List<sku> list_0, option option_0)
        {
            if (option_0.optionValues.Count == 0)
            {
                return list_0;
            }
            if (list_0.Count == 0)
            {
                foreach (optionValue value2 in option_0.optionValues)
                {
                    sku item = new sku();
                    intervalPrice price = new intervalPrice();
                    item.value.Add(price);
                    item.name[option_0.name.text[base.fatherObj.defaultLanguage]] = value2.value.text[base.fatherObj.defaultLanguage];
                    list_0.Add(item);
                }
                return list_0;
            }
            List<sku> list = new List<sku>();
            foreach (sku sku2 in list_0)
            {
                foreach (optionValue value3 in option_0.optionValues)
                {
                    sku sku3 = new sku();
                    intervalPrice price2 = new intervalPrice();
                    sku3.value.Add(price2);
                    foreach (KeyValuePair<string, string> pair in sku2.name)
                    {
                        sku3.name[pair.Key] = pair.Value;
                    }
                    sku3.name[option_0.name.text[base.fatherObj.defaultLanguage]] = value3.value.text[base.fatherObj.defaultLanguage];
                    list.Add(sku3);
                }
            }
            return list;
        }

        public void replacePics(Dictionary<string, string> dictionary_0)
        {
            if (ERRypu.isDownImg)
            {
                foreach (option option in this.value)
                {
                    foreach (optionValue value2 in option.optionValues)
                    {
                        if ((value2.pic != "") && (dictionary_0[value2.pic] != ""))
                        {
                            value2.pic = dictionary_0[value2.pic];
                        }
                        else
                        {
                            value2.pic = "";
                        }
                    }
                }
            }
        }

        public override void saveBaseData()
        {
            JArray array = new JArray();
            foreach (option option in this.baseData)
            {
                array.Add(option.getJObject);
            }
            base.fatherObj.cateAttr.saveBaseData("", "", JsonConvert.SerializeObject(array));
        }

        public override void setBatchControl()
        {
        }

        public override void setEditControl()
        {
            string str = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
            if (this.baseData.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                int num = 0;
                foreach (option option in this.baseData)
                {
                    builder.Append("<div style='clear:both;padding-bottom:10px;'>");
                    builder.Append("<div style='float:left;text-align:center;'>" + option.name.GetFullText + "&nbsp;</div>");
                    foreach (optionValue value2 in option.optionValues)
                    {
                        string str2 = "";
                        string str3 = "";
                        string str4 = base.KeyId + option.name.id + "_" + func.quotToHtml(option.name.text[str]) + "_" + value2.value.id + "_" + func.quotToHtml(value2.value.text[str]);
                        if (option.isEditPic)
                        {
                            str3 = "if(this.checked){document.getElementById(\"" + str4 + "pic\").style.display=\"inline\";}else{document.getElementById(\"" + str4 + "pic\").style.display=\"none\";};";
                        }
                        builder.Append(string.Concat(new object[] { 
                            "<div style='float:left;text-align:center;padding-bottom:10px;'><input onclick='valuechange(this.id);", str2, str3, "' name=\"", base.KeyId, num, "option\" type=\"checkbox\" id=\"", str4, "\" pid='", option.name.id, " ' vid='", value2.value.id, " ' lp='", option.isLinkPrice, "' lpic='", option.isEditPic, 
                            "' ptxt=\"", func.quotToHtml(option.name.text[str]), " \" vtxt=\"", func.quotToHtml(value2.value.text[str]), " \" />", value2.value.GetFullText
                         }));
                        if (option.isEditPic && base.fatherObj.Store.isSaleAttrPic)
                        {
                            builder.Append("<img style='margin:0px; padding:0px; border:1px solid #006666;' alt='\r\n\r\n单击增加图片\r\n\r\n双击删除图片\r\n \r\n ' ondblclick=\"userchange(this.id+'-del');\" onclick='userchange(this.id);' id=\"" + str4 + "pic\" src='" + func.getUrl(value2.pic) + "'  align='texttop' height='24px' width='24px' border=0 >&nbsp;");
                        }
                        builder.Append("</div>");
                    }
                    builder.Append("</div>");
                    num++;
                }
                base.control.innerHTML = builder.ToString();
                foreach (option option2 in this.value)
                {
                    foreach (optionValue value3 in option2.optionValues)
                    {
                        string str5 = option2.name.id + "_" + option2.name.text[str] + "_" + value3.value.id + "_" + value3.value.text[str];
                        if (base.elem(str5) != null)
                        {
                            base.elem(str5).setAttribute("checked", "checked", 0);
                            if (option2.isEditPic && base.fatherObj.Store.isSaleAttrPic)
                            {
                                base.elem(str5 + "pic").style.display = "inline";
                                base.elem(str5 + "pic").setAttribute("src", value3.pic, 0);
                            }
                        }
                    }
                }
            }
            else
            {
                StringBuilder builder2 = new StringBuilder();
                int num2 = 0;
                foreach (option option3 in this.value)
                {
                    builder2.Append("<div style='clear:both;padding-bottom:10px'>");
                    builder2.Append(option3.name.GetFullText + ":&nbsp;");
                    foreach (optionValue value4 in option3.optionValues)
                    {
                        builder2.Append(string.Concat(new object[] { 
                            "<input onclick='valuechange(this.id);' name=\"", base.KeyId, num2, "option\" type=\"checkbox\" checked='checked' id=\"", base.KeyId, option3.name.id, "_", func.quotToHtml(option3.name.text[str]), "_", value4.value.id, "_", func.quotToHtml(value4.value.text[str]), "\" pid='", option3.name.id, " ' vid='", value4.value.id, 
                            " ' ptxt=\"", func.quotToHtml(option3.name.text[str]), " \" vtxt=\"", func.quotToHtml(value4.value.text[str]), " \" />", value4.value.GetFullText, "&nbsp;&nbsp;"
                         }));
                    }
                    builder2.Append("</div>");
                    num2++;
                }
                base.control.innerHTML = builder2.ToString();
            }
        }

        public override void setSearchControl()
        {
        }

        public override void setTranslate()
        {
            string str = "";
            foreach (option option in this.value)
            {
                str = base.fatherObj.translateText[option.name.text[base.fatherObj.defaultLanguage]];
                if (str != "")
                {
                    option.name.text[base.fatherObj.toLanguage] = str;
                    str = "";
                }
                else
                {
                    option.name.text[base.fatherObj.toLanguage] = option.name.text[base.fatherObj.defaultLanguage];
                }
                foreach (optionValue value2 in option.optionValues)
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
                }
            }
        }

        public override void setValueFromMap()
        {
            if (base.fatherObj.Store.isOwn)
            {
                this.value = base.oldPro.saleAttr.value;
            }
            else
            {
                base.baseDataLinkId = base.fatherObj.cate.getLastCateID;
                this.getBaseDataFromDataBase();
                this.value = new List<option>();
                foreach (option option in base.oldPro.saleAttr.value)
                {
                    foreach (option option2 in this.baseData)
                    {
                        if (base.mapObj.saleattrnames[base.oldPro.cate.fullName][option.name.text[base.oldPro.defaultLanguage]] == option2.name.id)
                        {
                            option item = new option();
                            item.name = option2.name;
                            item.isEditAlias = option2.isEditAlias;
                            item.isEditPic = option2.isEditPic;
                            item.isLinkPrice = option2.isLinkPrice;
                            foreach (optionValue value2 in option.optionValues)
                            {
                                string str = base.mapObj.saleattrvalues[base.oldPro.cate.fullName][option.name.text[base.oldPro.defaultLanguage]][value2.value.text[base.oldPro.defaultLanguage]];
                                foreach (optionValue value3 in option2.optionValues)
                                {
                                    if (str == value3.value.id)
                                    {
                                        item.optionValues.Add(value3);
                                        value3.aliasName = value2.aliasName;
                                        value3.pic = value2.pic;
                                    }
                                }
                            }
                            this.value.Add(item);
                        }
                    }
                }
            }
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            string str = "";
            foreach (option option in this.value)
            {
                foreach (optionValue value2 in option.optionValues)
                {
                    if (value2.aliasName != "")
                    {
                        string str2 = text.check(value2.aliasName, this.minLength, this.maxLength, false, this.allowZH, this.banReg);
                        if (str2 != "")
                        {
                            str = str + str2 + "<br/>";
                        }
                    }
                    if (value2.pic != "")
                    {
                        string str3 = base.fatherObj.pics.verifyPic(value2.pic);
                        if (str3 != "")
                        {
                            str = str + str3 + "<br/>";
                        }
                    }
                }
            }
            return str;
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


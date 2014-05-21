namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class priceP : baseP
    {
        public List<sku> defalu = new List<sku>();
        public int intervalNum = 5;
        public bool isEditDay;
        public bool isEditInterval;
        public bool isEditItemCode;
        public bool isEditOption;
        public bool isEditProNum;
        public bool isEditWholesale;
        public bool isHaveOption;
        public bool isInputWholesaleOff;
        public int skuNum = 6;
        public List<sku> value = new List<sku>();

        public priceP(fatherObj fatherObj_0)
        {
            base.key = "price";
            base.name = "价格";
            base.searchName = "价格范围";
            base.dataBaseFieldForSave = "f5";
            base.dataBaseFieldForList = "f6";
            base.dataBaseFieldForSearch = "f7";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["off"] = new rule();
            base.rules["minOrderNum"] = new rule();
            base.rules["proNum"] = new rule();
            base.rules["skuCode"] = new rule();
            base.rules["min"] = new rule();
            base.rules["max"] = new rule();
            base.rules["price"] = new rule();
            base.rules["day"] = new rule();
            sku item = new sku();
            item.value.Add(new intervalPrice());
            item.value[0].min = 1;
            item.value[0].max = 0x270f;
            item.value[0].price = 999.0;
            this.defalu.Add(item);
        }

        public override void batchEdit()
        {
            if ((bool) base.elem("cc").getAttribute("checked", 0))
            {
                foreach (sku sku in this.value)
                {
                    foreach (intervalPrice price in sku.value)
                    {
                        price.price = this.method_0(price.price, func.getAttrByElem(base.elem("cz"), "value"), func.getAttrByElem(base.elem("v"), "value"));
                    }
                }
            }
            else if ((bool) base.elem("nn").getAttribute("checked", 0))
            {
                double num = text.toDouble(func.getAttrByElem(base.elem("nz"), "value"));
                if (num >= 0.0)
                {
                    foreach (sku sku2 in this.value)
                    {
                        foreach (intervalPrice price2 in sku2.value)
                        {
                            price2.price = num;
                        }
                    }
                }
            }
            else if ((base.fatherObj.siteId == "25") && ((bool) base.elem("jt").getAttribute("checked", 0)))
            {
                try
                {
                    string[] strArray = base.elem("jtnz").innerHTML.Trim().Split(new char[] { '\r' });
                    List<intervalPrice> list = new List<intervalPrice>();
                    foreach (string str2 in strArray)
                    {
                        string[] strArray2 = str2.Split(new char[] { ',' });
                        intervalPrice item = new intervalPrice();
                        string str3 = strArray2[2].Replace("p", this.value[0].value[0].price.ToString());
                        NEval eval = new NEval();
                        item.min = Convert.ToInt32(strArray2[0]);
                        item.max = Convert.ToInt32(strArray2[1]);
                        item.price = eval.Eval(str3);
                        item.day = Convert.ToInt32(strArray2[3]);
                        list.Add(item);
                    }
                    foreach (sku sku3 in this.value)
                    {
                        sku3.value = list;
                    }
                }
                catch
                {
                }
            }
            if (this.isEditDay)
            {
                if ((bool) base.elem("cc2").getAttribute("checked", 0))
                {
                    foreach (sku sku4 in this.value)
                    {
                        foreach (intervalPrice price4 in sku4.value)
                        {
                            int result = 0;
                            int.TryParse(this.method_0((double) price4.day, func.getAttrByElem(base.elem("cz2"), "value"), func.getAttrByElem(base.elem("v2"), "value")).ToString(), out result);
                            if (result > 0)
                            {
                                price4.day = result;
                            }
                        }
                    }
                }
                else if ((bool) base.elem("nn2").getAttribute("checked", 0))
                {
                    int num3 = text.toInt(func.getAttrByElem(base.elem("nz2"), "value"));
                    if (num3 > 0)
                    {
                        foreach (sku sku5 in this.value)
                        {
                            foreach (intervalPrice price5 in sku5.value)
                            {
                                price5.day = num3;
                            }
                        }
                    }
                }
            }
        }

        public override void forceEdit()
        {
            if (this.isEditOption)
            {
                while (this.value.Count > this.skuNum)
                {
                    this.value.RemoveAt(this.skuNum);
                }
            }
            if (this.value.Count <= 0)
            {
                this.value = this.defalu;
            }
            else
            {
                int num = 1;
                foreach (sku sku in this.value)
                {
                    if (sku.isWholesale && (num == 1))
                    {
                        sku.off = text.change(sku.off, base.rules["off"].isMust, base.rules["off"].minValue, base.rules["off"].maxValue, base.rules["off"].num, base.rules["off"].defaluD);
                        sku.minOrderNum = text.change(sku.minOrderNum, base.rules["minOrderNum"].isMust, base.rules["minOrderNum"].minValue, base.rules["minOrderNum"].maxValue, base.rules["minOrderNum"].num, base.rules["minOrderNum"].defaluD);
                    }
                    sku.proNum = text.change(sku.proNum, base.rules["proNum"].isMust, base.rules["proNum"].minValue, base.rules["proNum"].maxValue, base.rules["proNum"].num, base.rules["proNum"].defaluD);
                    sku.skuCode = text.change(sku.skuCode, base.rules["skuCode"].minLength, base.rules["skuCode"].maxLength, base.rules["skuCode"].isMust, base.rules["skuCode"].allowZH, base.rules["skuCode"].banReg, base.rules["skuCode"].defaluT);
                    if (this.isEditInterval)
                    {
                        while (sku.value.Count > this.intervalNum)
                        {
                            sku.value.RemoveAt(this.intervalNum);
                        }
                    }
                    int max = 0;
                    foreach (intervalPrice price in sku.value)
                    {
                        price.min = text.change(price.min, base.rules["min"].isMust, (double) (max + 1), base.rules["min"].maxValue, base.rules["min"].num, base.rules["min"].defaluD);
                        price.max = text.change(price.max, base.rules["max"].isMust, (double) (price.min + 1), base.rules["max"].maxValue, base.rules["max"].num, base.rules["max"].defaluD);
                        max = price.max;
                        price.price = text.change(price.price, base.rules["price"].isMust, base.rules["price"].minValue, base.rules["price"].maxValue, base.rules["price"].num, base.rules["price"].defaluD);
                        price.day = text.change(price.day, base.rules["day"].isMust, base.rules["day"].minValue, base.rules["day"].maxValue, base.rules["day"].num, base.rules["day"].defaluD);
                    }
                    num++;
                }
            }
        }

        public override string getDataBaseFromValue()
        {
            JArray array = new JArray();
            foreach (sku sku in this.value)
            {
                array.Add(sku.getJObject);
            }
            return JsonConvert.SerializeObject(array);
        }

        public override string getListCacheFromValue()
        {
            return this.getSearchCacheFromValue();
        }

        public Dictionary<string, string> getOptionNameId(Dictionary<string, string> dictionary_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in dictionary_0)
            {
                using (List<option>.Enumerator enumerator2 = base.fatherObj.saleAttr.baseData.GetEnumerator())
                {
                    option current;
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (pair.Key == current.name.text[base.fatherObj.defaultLanguage])
                        {
                            goto Label_0072;
                        }
                    }
                    continue;
                Label_0072:
                    using (List<optionValue>.Enumerator enumerator3 = current.optionValues.GetEnumerator())
                    {
                        optionValue value2;
                        while (enumerator3.MoveNext())
                        {
                            value2 = enumerator3.Current;
                            if (pair.Value == value2.value.text[base.fatherObj.defaultLanguage])
                            {
                                goto Label_00BE;
                            }
                        }
                        continue;
                    Label_00BE:
                        dictionary[current.name.id] = value2.value.id;
                    }
                }
            }
            return dictionary;
        }

        public pv getPidByName(KeyValuePair<string, string> keyValuePair_0)
        {
            foreach (option option in base.fatherObj.saleAttr.value)
            {
                if (option.name.text[base.fatherObj.defaultLanguage] == keyValuePair_0.Key)
                {
                    using (List<optionValue>.Enumerator enumerator2 = option.optionValues.GetEnumerator())
                    {
                        optionValue current;
                        pv pv;
                        while (enumerator2.MoveNext())
                        {
                            current = enumerator2.Current;
                            if (current.value.text[base.fatherObj.defaultLanguage] == keyValuePair_0.Value)
                            {
                                goto Label_009A;
                            }
                        }
                        continue;
                    Label_009A:
                        pv = new pv();
                        pv.p = option.name;
                        pv.v = current.value;
                        return pv;
                    }
                }
            }
            return null;
        }

        public override string getSearchCacheFromValue()
        {
            if ((this.value.Count > 0) && (this.value[0].value.Count > 0))
            {
                return Math.Round(this.value[0].value[0].price, 2).ToString();
            }
            return "";
        }

        public override string getSearchCondition()
        {
            string str = func.getAttrByElem(base.elem("from"), "value");
            string str2 = func.getAttrByElem(base.elem("to"), "value");
            string str3 = "";
            if (str.Length > 0)
            {
                str3 = base.dataBaseFieldForSearch + ">=" + text.toDouble(str);
            }
            if (str2.Length > 0)
            {
                if (str3 == "")
                {
                    str3 = base.dataBaseFieldForSearch + "<=" + text.toDouble(str2);
                }
                else
                {
                    str3 = string.Concat(new object[] { str3, " and ", base.dataBaseFieldForSearch, "<=", text.toDouble(str2) });
                }
            }
            if (str3 != "")
            {
                return (" (" + str3.Replace(base.dataBaseFieldForSearch, "round(" + base.dataBaseFieldForSearch + ",3)") + ") ");
            }
            return "";
        }

        public override void getTranslate()
        {
            foreach (sku sku in this.value)
            {
                foreach (KeyValuePair<string, string> pair in sku.name)
                {
                    if (pair.Key != "")
                    {
                        base.fatherObj.translateText[pair.Key] = "";
                    }
                    if (pair.Value != "")
                    {
                        base.fatherObj.translateText[pair.Value] = "";
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
            this.value = new List<sku>();
            foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(string_0))
            {
                this.value.Add(sku.getValueFromJObject((JObject) token));
            }
        }

        public override void getValueFromEditC()
        {
            try
            {
                int num = 0;
                int num2 = 0;
                foreach (sku sku in this.value)
                {
                    num2 = 0;
                    if (this.isEditOption)
                    {
                        sku.name[""] = func.getAttrByElem(base.elem(num + "name"), "value");
                    }
                    sku.isWholesale = (bool) base.elem("ispf").getAttribute("checked", 0);
                    sku.off = text.toInt(func.getAttrByElem(base.elem("off"), "value"));
                    sku.minOrderNum = text.toInt(func.getAttrByElem(base.elem("minordernum"), "value"));
                    sku.proNum = text.toInt(func.getAttrByElem(base.elem(num + "num"), "value"));
                    sku.skuCode = func.getAttrByElem(base.elem(num + "code"), "value");
                    foreach (intervalPrice price in sku.value)
                    {
                        price.min = text.toInt(func.getAttrByElem(base.elem(string.Concat(new object[] { num, "-", num2, "min" })), "value"));
                        price.max = text.toInt(func.getAttrByElem(base.elem(string.Concat(new object[] { num, "-", num2, "max" })), "value"));
                        price.price = text.toDouble(func.getAttrByElem(base.elem(string.Concat(new object[] { num, "-", num2, "pri" })), "value"));
                        price.day = text.toInt(func.getAttrByElem(base.elem(string.Concat(new object[] { num, "-", num2, "day" })), "value"));
                        num2++;
                    }
                    num++;
                }
            }
            catch (Exception)
            {
                func.Message("输入数字的地方不能输入字母或中文");
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
            this.getValueFromEditC();
            if (string_0.EndsWith("no"))
            {
                if (this.value.Count < this.skuNum)
                {
                    sku item = new sku();
                    intervalPrice price = new intervalPrice();
                    item.value.Add(price);
                    this.value.Add(item);
                }
                else
                {
                    func.Message("最多允许" + this.skuNum + "个销售选项");
                }
            }
            else if (string_0.EndsWith("do"))
            {
                int index = text.toInt(string_0.Replace("do", ""));
                this.value.RemoveAt(index);
            }
            if (string_0.EndsWith("ni"))
            {
                int num2 = text.toInt(string_0.Replace("ni", ""));
                if (this.value[num2].value.Count < this.intervalNum)
                {
                    intervalPrice price2 = new intervalPrice();
                    this.value[num2].value.Add(price2);
                }
                else
                {
                    func.Message("每个销售选项最多允许" + this.intervalNum + "个价格区间");
                }
            }
            if (string_0.EndsWith("di"))
            {
                string[] strArray = string_0.Replace("di", "").Split(new char[] { '-' });
                int num3 = text.toInt(strArray[0]);
                int num4 = text.toInt(strArray[1]);
                this.value[num3].value.RemoveAt(num4);
            }
            this.setEditControl();
        }

        public override void handleEditValueChange(string string_0)
        {
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = new List<sku>();
        }

        private double method_0(double double_0, string string_0, string string_1)
        {
            string str;
            double num = text.toDouble(string_1);
            if ((num > 0.0) && ((str = string_0) != null))
            {
                if (str != "+")
                {
                    if (str != "-")
                    {
                        if (!(str == "*"))
                        {
                            if (str == "/")
                            {
                                double_0 /= num;
                            }
                            return double_0;
                        }
                        double_0 *= num;
                        return double_0;
                    }
                    double_0 -= num;
                    return double_0;
                }
                double_0 += num;
            }
            return double_0;
        }

        private Dictionary<string, string> method_1(string string_0, string string_1)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["v"] = string_1;
            dictionary["k"] = string_0;
            List<string> list = func.getKeysByValue(base.mapObj.transDic, string_0);
            List<string> list2 = new List<string>();
            if (list.Count > 0)
            {
                list2 = func.getKeysByValue(base.mapObj.transDic, string_1);
            }
            if ((list.Count != 0) && (list2.Count != 0))
            {
                foreach (option option in base.fatherObj.saleAttr.baseData)
                {
                    if (this.method_2(base.mapObj.saleattrnames[base.oldPro.cate.fullName], list, option.name.id, ref string_0))
                    {
                        dictionary["k"] = option.name.text[base.fatherObj.defaultLanguage];
                        using (List<optionValue>.Enumerator enumerator2 = option.optionValues.GetEnumerator())
                        {
                            optionValue current;
                            while (enumerator2.MoveNext())
                            {
                                current = enumerator2.Current;
                                if (this.method_2(base.mapObj.saleattrvalues[base.oldPro.cate.fullName][string_0], list2, current.value.id, ref string_1))
                                {
                                    goto Label_014C;
                                }
                            }
                            continue;
                        Label_014C:
                            dictionary["v"] = current.value.text[base.fatherObj.defaultLanguage];
                        }
                    }
                }
            }
            return dictionary;
        }

        private bool method_2(Dictionary<string, string> dictionary_0, List<string> list_0, string string_0, ref string string_1)
        {
            using (List<string>.Enumerator enumerator = list_0.GetEnumerator())
            {
                string current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (dictionary_0.ContainsKey(current) && (dictionary_0[current] == string_0))
                    {
                        goto Label_0035;
                    }
                }
                goto Label_004B;
            Label_0035:
                string_1 = current;
                return true;
            }
        Label_004B:
            return false;
        }

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            if (this.isEditDay)
            {
                builder.Append("<div style='clear:both;'><h5>批量修改价格</h5></div>");
            }
            builder.Append("<div style='clear:both; padding:10px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "cc'/>在原值基础上&nbsp;<select id='" + base.KeyId + "cz'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
            builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v' type='text' size='8' /></div><div style='clear:both;padding:10px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "nn' />全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz' /></div>");
            if (base.fatherObj.siteId == "25")
            {
                builder.Append("<div style='clear:both;padding:10px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "jt' />批量设置阶梯价格&nbsp;<br/>一行一条阶梯价，每个阶梯价的表现形式是\"1,2,p*1.1+2,3\"，其中1表示最小数量，2表示最大数量，<br/>p*1.1+2表示价格(其中p表示原价,+-*/分别表示加减乘除)，3表示交货期，它们之间用英文逗号隔开.<br/><textarea  cols=\"70\" rows=\"5\" id='" + base.KeyId + "jtnz'></textarea></div>");
            }
            if (this.isEditDay)
            {
                builder.Append("<div style='clear:both;'><h5>批量修改交货期</h5></div>");
                builder.Append("<div style='clear:both; padding:10px'><input name='" + base.KeyId + "sel2' type='radio' value='' id='" + base.KeyId + "cc2'/>在原值基础上&nbsp;<select id='" + base.KeyId + "cz2'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
                builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v2' type='text' size='8' /></div><div style='clear:both;padding:10px'><input name='" + base.KeyId + "sel2' type='radio' value='' id='" + base.KeyId + "nn2'/>全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz2' /></div>");
            }
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            if (this.value.Count == 0)
            {
                sku item = new sku();
                intervalPrice price = new intervalPrice();
                item.value.Add(price);
                this.value.Add(item);
            }
            string str = "style='display:none'";
            string str2 = "style='display:none'";
            string str3 = "disabled='disabled'";
            string str4 = "style='display:none'";
            string str5 = "style='display:none'";
            string str6 = "style='display:none'";
            string str7 = "style='display:none'";
            string str8 = "style='display:none'";
            string str9 = "";
            if (this.isHaveOption)
            {
                str = "style='display:block'";
            }
            if (this.isEditOption)
            {
                str2 = "style='display:block'";
                str3 = "";
                str = "style='display:block'";
            }
            if (this.isEditProNum)
            {
                str4 = "style='display:block'";
            }
            if (this.isEditItemCode)
            {
                str5 = "style='display:block'";
            }
            if (this.isEditInterval)
            {
                str6 = "style='display:block'";
            }
            if (this.isEditDay)
            {
                str7 = "style='display:block'";
            }
            if (this.isEditWholesale)
            {
                str8 = "style='display:block'";
            }
            if (this.value[0].isWholesale)
            {
                str9 = "checked='checked'";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<table border='0' cellpadding='5' cellspacing='1' style='background-color: #b9d8f3;'><tr bgcolor='#ffffff' " + str + "><td " + str + "><div align='center'>销售属性</div></td><td><div align='center'>价格</div></td><td " + str4 + "><div align='center'>库存</div></td><td " + str5 + "><div align='center'>编码</div></td><td  " + str2 + ">&nbsp;</td></tr>");
            int num = 0;
            int num2 = 0;
            foreach (sku sku2 in this.value)
            {
                builder.Append(string.Concat(new object[] { "<tr bgcolor='#ffffff'><td ", str, "><input ", str3, " type='text' value=\"", func.quotToHtml(sku2.getName), "\" id='", base.KeyId, num, "name' size='25' onchange='valuechange(this.id);' /></td><td><table border='0' cellpadding='5' cellspacing='1' style='background-color: #F4FAFF;'><tr bgcolor='#ffffff' ", str6, "><td>最小数量</td><td>最大数量</td><td>销售价</td> <td ", str7, ">交货期(天)</td> <td ", str6, ">&nbsp;</td></tr>" }));
                num2 = 0;
                foreach (intervalPrice price2 in sku2.value)
                {
                    string str10 = "&nbsp;";
                    if (num2 > 0)
                    {
                        str10 = string.Concat(new object[] { "<input type='button' id='", base.KeyId, num, "-", num2, "di' value='删除价格区间' onclick='userchange(this.id);' />" });
                    }
                    builder.Append(string.Concat(new object[] { 
                        "<tr bgcolor='#ffffff'><td ", str6, "><input id='", base.KeyId, num, "-", num2, "min' type='text' size='5' value='", price2.min, "' onchange='valuechange(this.id);' /></td><td ", str6, "><input id='", base.KeyId, num, "-", num2, 
                        "max' type='text' size='5' value='", price2.max, "' onchange='valuechange(this.id);' /></td><td><input id='", base.KeyId, num, "-", num2, "pri' type='text' size='12' value='", Math.Round(price2.price, 2).ToString(), "' onchange='valuechange(this.id);' /></td><td ", str7, "><div align='center'><input id='", base.KeyId, num, "-", num2, 
                        "day' type='text' size='5'  value='", price2.day, "' onchange='valuechange(this.id);' /></div></td> <td ", str6, ">", str10, "</td></tr>"
                     }));
                    num2++;
                    if ((num2 == 1) && !this.isEditInterval)
                    {
                        break;
                    }
                }
                string str11 = "&nbsp;";
                if (num > 0)
                {
                    str11 = string.Concat(new object[] { "<input type='button' id='", base.KeyId, num, "do' value='删除销售属性' onclick='userchange(this.id);' />" });
                }
                builder.Append(string.Concat(new object[] { 
                    "<tr bgcolor='#ffffff' ", str6, "><td colspan='5'><div align='right'><input type='button' id='", base.KeyId, num, "ni' value='新增价格区间' onclick='userchange(this.id);' /></div></td></tr></table></td><td ", str4, "><input type='text' value=\"", sku2.proNum, "\" id='", base.KeyId, num, "num' size='8' onchange='valuechange(this.id);' /></td><td ", str5, "><input value=\"", func.quotToHtml(sku2.skuCode), 
                    "\" id='", base.KeyId, num, "code' type='text' onchange='valuechange(this.id);' /></td><td  ", str2, ">", str11, "</td></tr>"
                 }));
                num++;
                if (!this.isEditOption && !this.isHaveOption)
                {
                    break;
                }
            }
            builder.Append(string.Concat(new object[] { 
                "<tr bgcolor='#ffffff' ", str2, "><td colspan='5'><div align='right'><input type='button' id='", base.KeyId, "no' value='新增销售属性' onclick='userchange(this.id);' /></div></td></tr><tr bgcolor='#ffffff' ", str8, "><td colspan='5' height='50px'><input id='", base.KeyId, "ispf' type='checkbox' ", str9, " value='' onclick='valuechange(this.id);' />支持批发价&nbsp;&nbsp;折扣(多少折)：<input id='", base.KeyId, "off' type='text' value='", this.value[0].off, "' size='6' onchange='valuechange(this.id);' />&nbsp;&nbsp;最小起订量：<input id='", base.KeyId, 
                "minordernum' type='text' value='", this.value[0].minOrderNum, "' size='10' onchange='valuechange(this.id);' /><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入1-99之间的数字，填99表示原价乘以0.99</td></tr></table>"
             }));
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<input id='" + base.KeyId + "from' type='text' size='10' />&nbsp;-&nbsp;<input id='" + base.KeyId + "to' type='text' size='10' />";
        }

        public override void setTranslate()
        {
            foreach (sku sku in this.value)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> pair in sku.name)
                {
                    string key = base.fatherObj.translateText[pair.Key];
                    string str2 = base.fatherObj.translateText[pair.Value];
                    if (key == "")
                    {
                        key = pair.Key;
                    }
                    if (str2 == "")
                    {
                        str2 = pair.Value;
                    }
                    dictionary[key] = str2;
                }
                sku.name = dictionary;
            }
        }

        public override void setValueFromMap()
        {
            foreach (sku sku in base.oldPro.price.value)
            {
                foreach (intervalPrice price in sku.value)
                {
                    price.price *= base.mapObj.currencyRate[base.oldPro.currencyUnit];
                }
            }
            if (this.isHaveOption && !this.isEditOption)
            {
                this.value = base.fatherObj.saleAttr.getNewPrice();
                if (this.value.Count > 0)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    int num = 0;
                    foreach (sku sku2 in base.oldPro.price.value)
                    {
                        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                        foreach (KeyValuePair<string, string> pair in sku2.name)
                        {
                            Dictionary<string, string> dictionary3 = this.method_1(pair.Key, pair.Value);
                            dictionary2[dictionary3["k"]] = dictionary3["v"];
                        }
                        sku2.name = dictionary2;
                        dictionary[sku2.getName] = num.ToString();
                        num++;
                    }
                    foreach (sku sku3 in this.value)
                    {
                        num = text.toInt(text.map(sku3.getName, dictionary, true, 0));
                        sku3.isWholesale = base.oldPro.price.value[num].isWholesale;
                        sku3.minOrderNum = base.oldPro.price.value[num].minOrderNum;
                        sku3.off = base.oldPro.price.value[num].off;
                        sku3.proNum = base.oldPro.price.value[num].proNum;
                        sku3.skuCode = base.oldPro.price.value[num].skuCode;
                        sku3.value = base.oldPro.price.value[num].value;
                    }
                }
                else
                {
                    this.value = new List<sku>();
                    using (List<sku>.Enumerator enumerator6 = base.oldPro.price.value.GetEnumerator())
                    {
                        if (enumerator6.MoveNext())
                        {
                            sku current = enumerator6.Current;
                            sku item = new sku();
                            item.value = current.value;
                            item.minOrderNum = current.minOrderNum;
                            item.isWholesale = current.isWholesale;
                            item.off = current.off;
                            item.proNum = current.proNum;
                            item.skuCode = current.skuCode;
                            this.value.Add(item);
                        }
                    }
                }
            }
            else
            {
                this.value = base.oldPro.price.value;
            }
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            if (this.value.Count <= 0)
            {
                return "请设置价格";
            }
            string str = "";
            if (this.isEditOption && (this.value.Count > this.skuNum))
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "价格选项数量不能大于", this.skuNum, "个<br/>" });
            }
            int num = 1;
            foreach (sku sku in this.value)
            {
                string str2;
                if (this.isEditInterval && (sku.value.Count > this.intervalNum))
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, " 第", num, "个选项的价格区间数量不能大于", this.intervalNum, "个<br/>" });
                }
                if (sku.isWholesale && (num == 1))
                {
                    str2 = text.check((double) sku.off, base.rules["off"].isMust, base.rules["off"].minValue, base.rules["off"].maxValue, base.rules["off"].num);
                    if (str2 != "")
                    {
                        str = str + "折扣" + str2 + "<br/>";
                    }
                    str2 = text.check((double) sku.minOrderNum, base.rules["minOrderNum"].isMust, base.rules["minOrderNum"].minValue, base.rules["minOrderNum"].maxValue, base.rules["minOrderNum"].num);
                    if (str2 != "")
                    {
                        str = str + "最小起订量" + str2 + "<br/>";
                    }
                }
                str2 = text.check((double) sku.proNum, base.rules["proNum"].isMust, base.rules["proNum"].minValue, base.rules["proNum"].maxValue, base.rules["proNum"].num);
                if (str2 != "")
                {
                    object obj4 = str;
                    str = string.Concat(new object[] { obj4, "第", num, "个选项的库存", str2, "<br/>" });
                }
                str2 = text.check(sku.skuCode, base.rules["skuCode"].minLength, base.rules["skuCode"].maxLength, base.rules["skuCode"].isMust, base.rules["skuCode"].allowZH, base.rules["skuCode"].banReg);
                if (str2 != "")
                {
                    object obj5 = str;
                    str = string.Concat(new object[] { obj5, "第", num, "个选项的商家编码", str2, "<br/>" });
                }
                int max = 0;
                int num3 = 1;
                foreach (intervalPrice price in sku.value)
                {
                    if (this.isEditInterval)
                    {
                        str2 = text.check((double) price.min, base.rules["min"].isMust, (double) (max + 1), base.rules["min"].maxValue, base.rules["min"].num);
                        if (str2 != "")
                        {
                            object obj6 = str;
                            str = string.Concat(new object[] { obj6, "第", num, "个选项的第", num3, "个区间的最小数量", str2, "<br/>" });
                        }
                        str2 = text.check((double) price.max, base.rules["max"].isMust, (double) (price.min + 1), base.rules["max"].maxValue, base.rules["max"].num);
                        if (str2 != "")
                        {
                            object obj7 = str;
                            str = string.Concat(new object[] { obj7, "第", num, "个选项的第", num3, "个区间的最大数量", str2, "<br/>" });
                        }
                        max = price.max;
                    }
                    str2 = text.check(price.price, base.rules["price"].isMust, base.rules["price"].minValue, base.rules["price"].maxValue, 10);
                    if (str2 != "")
                    {
                        object obj8 = str;
                        str = string.Concat(new object[] { obj8, "第", num, "个选项的第", num3, "个区间的价格", str2, "<br/>" });
                    }
                    if (this.isEditDay)
                    {
                        str2 = text.check((double) price.day, base.rules["day"].isMust, base.rules["day"].minValue, base.rules["day"].maxValue, base.rules["day"].num);
                        if (str2 != "")
                        {
                            object obj9 = str;
                            str = string.Concat(new object[] { obj9, "第", num, "个选项的第", num3, "个区间的交货期", str2, "<br/>" });
                        }
                    }
                    num3++;
                }
                num++;
            }
            return str;
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


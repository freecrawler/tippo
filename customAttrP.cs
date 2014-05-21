namespace client
{
    using IfacesEnumsStructsClasses;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class customAttrP : baseP
    {
        public Dictionary<string, string> defalu = new Dictionary<string, string>();
        public int maxNum = 10;
        public Dictionary<string, string> value = new Dictionary<string, string>();

        public customAttrP(fatherObj fatherObj_0)
        {
            base.key = "customAttr";
            base.name = "自定义属性";
            base.dataBaseFieldForSave = "f29";
            base.dataBaseFieldForList = "f30";
            base.dataBaseFieldForSearch = "f31";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["customAttrName"] = new rule();
            base.rules["customAttrValue"] = new rule();
        }

        public override void forceEdit()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (KeyValuePair<string, string> pair in this.value)
            {
                if ((pair.Key.Trim() != "") && (pair.Value.Trim() != ""))
                {
                    num++;
                    if (num <= this.maxNum)
                    {
                        string introduced5 = text.change(pair.Key, base.rules["customAttrName"].minLength, base.rules["customAttrName"].maxLength, base.rules["customAttrName"].isMust, base.rules["customAttrName"].allowZH, base.rules["customAttrName"].banReg, base.rules["customAttrName"].defaluT);
                        dictionary[introduced5] = text.change(pair.Value, base.rules["customAttrValue"].minLength, base.rules["customAttrValue"].maxLength, base.rules["customAttrValue"].isMust, base.rules["customAttrValue"].allowZH, base.rules["customAttrValue"].banReg, base.rules["customAttrValue"].defaluT);
                    }
                    else
                    {
                        builder.Append(pair.Key + ":" + pair.Value + "<br/>");
                    }
                }
            }
            this.value = dictionary;
            base.fatherObj.des.value = builder.ToString() + base.fatherObj.des.value;
        }

        public override string getDataBaseFromValue()
        {
            return JsonConvert.SerializeObject(func.getJObjectFromDic(this.value));
        }

        public override void getTranslate()
        {
            foreach (KeyValuePair<string, string> pair in this.value)
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

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = func.getDicFromJObject(JObject.Parse(string_0));
        }

        public override void getValueFromEditC()
        {
            this.method_0(-1);
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
            if (string_0.EndsWith("n"))
            {
                if (this.value.Count > this.maxNum)
                {
                    func.Message("最多允许" + this.maxNum + "个属性");
                    return;
                }
                this.getValueFromEditC();
                this.value.Add("", "");
            }
            else if (string_0.EndsWith("d"))
            {
                int num = text.toInt(string_0.Replace("d", ""));
                this.method_0(num);
            }
            this.setEditControl();
        }

        public override void handleEditValueChange(string string_0)
        {
            this.getValueFromEditC();
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = new Dictionary<string, string>();
        }

        private void method_0(int int_0)
        {
            IHTMLElementCollection elementsByName = base.webbrowser.GetElementsByName(true, base.KeyId + "name");
            IHTMLElementCollection elements2 = base.webbrowser.GetElementsByName(true, base.KeyId + "value");
            int index = 0;
            this.value = new Dictionary<string, string>();
            foreach (IHTMLElement element in elementsByName)
            {
                if (int_0 != index)
                {
                    this.value[func.getAttrByElem(element, "value").Trim()] = func.getAttrByElem((IHTMLElement) elements2.item(base.KeyId + "value", index), "value").Trim();
                }
                index++;
            }
        }

        public override void setBatchControl()
        {
        }

        public override void setEditControl()
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (KeyValuePair<string, string> pair in this.value)
            {
                builder.Append(string.Concat(new object[] { "<div style='padding:5px'><input name='", base.KeyId, "name' id='", base.KeyId, "name' type='text' value=\"", func.quotToHtml(pair.Key), "\" onchange='valuechange(this.id);' />&nbsp;:&nbsp;<input name='", base.KeyId, "value' id='", base.KeyId, "value' type='text' value=\"", func.quotToHtml(pair.Value), "\" onchange='valuechange(this.id);' />&nbsp;&nbsp;<input id='", base.KeyId, num, "d' type='button' value='删除属性' onclick='userchange(this.id);' /></div>" }));
                num++;
            }
            builder.Append("<div style='padding:5px'><input id='" + base.KeyId + "n' type='button' value='新增属性' onclick='userchange(this.id);' /></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
        }

        public override void setTranslate()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in this.value)
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
            this.value = dictionary;
        }

        public override void setValueFromMap()
        {
            foreach (KeyValuePair<string, string> pair in base.oldPro.customAttr.value)
            {
                this.value[pair.Key] = pair.Value;
            }
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            string str = "";
            if (this.value.Count > this.maxNum)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "自定义属性不能大于", this.maxNum, "个<br/>" });
            }
            foreach (KeyValuePair<string, string> pair in this.value)
            {
                bool flag = pair.Value != "";
                string str2 = text.check(pair.Key, base.rules["customAttrName"].minLength, base.rules["customAttrName"].maxLength, flag, base.rules["customAttrName"].allowZH, base.rules["customAttrName"].banReg);
                if (str2 != "")
                {
                    str = str + "属性名" + str2 + ",";
                }
                flag = pair.Key != "";
                str2 = text.check(pair.Value, base.rules["customAttrValue"].minLength, base.rules["customAttrValue"].maxLength, flag, base.rules["customAttrValue"].allowZH, base.rules["customAttrValue"].banReg);
                if (str2 != "")
                {
                    str = str + "属性值" + str2 + "<br/>";
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


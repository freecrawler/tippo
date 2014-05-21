namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class keywordsP : baseP
    {
        public List<string> defalu = new List<string>();
        public int maxNum = 3;
        public List<string> value = new List<string>();

        public keywordsP(fatherObj fatherObj_0)
        {
            base.key = "keywords";
            base.name = "关键词";
            base.tipTextForBatch = "设置新的关键词";
            base.dataBaseFieldForSave = "f17";
            base.dataBaseFieldForList = "f18";
            base.dataBaseFieldForSearch = "f19";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["keywords1"] = new rule();
            base.rules["keywords"] = new rule();
        }

        public override void batchEdit()
        {
            this.getValueFromEditC();
        }

        public override void forceEdit()
        {
            if (this.value.Count <= 0)
            {
                if (base.isMust && (base.fatherObj.cate.value.Count > 0))
                {
                    this.value.Add(base.fatherObj.cate.value[base.fatherObj.cate.value.Count - 1].value.text[base.fatherObj.defaultLanguage]);
                }
            }
            else
            {
                List<string> list = new List<string>();
                int num = 0;
                foreach (string str in this.value)
                {
                    if (num == 0)
                    {
                        list.Add(text.change(str, base.rules["keywords1"].minLength, base.rules["keywords1"].maxLength, base.rules["keywords1"].isMust, base.rules["keywords1"].allowZH, base.rules["keywords1"].banReg, base.rules["keywords1"].defaluT));
                    }
                    else
                    {
                        list.Add(text.change(str, base.rules["keywords"].minLength, base.rules["keywords"].maxLength, base.rules["keywords"].isMust, base.rules["keywords"].allowZH, base.rules["keywords"].banReg, base.rules["keywords"].defaluT));
                    }
                    num++;
                }
                while (list.Count > this.maxNum)
                {
                    list.RemoveAt(this.maxNum);
                }
                this.value = list;
            }
        }

        public override string getDataBaseFromValue()
        {
            JArray array = new JArray();
            foreach (string str in this.value)
            {
                array.Add(str);
            }
            return JsonConvert.SerializeObject(array);
        }

        public override string getListCacheFromValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.value)
            {
                builder.Append(str + ",");
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        public override string getSearchCacheFromValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.value)
            {
                builder.Append(str);
            }
            return builder.ToString();
        }

        public override string getSearchCondition()
        {
            string str = func.getAttrByElem(base.elem("v"), "value");
            if (str != "")
            {
                return (" (" + base.dataBaseFieldForSearch + " like '%" + str.Replace("'", "''") + "%') ");
            }
            return "";
        }

        public override void getTranslate()
        {
            foreach (string str in this.value)
            {
                if (str != "")
                {
                    base.fatherObj.translateText[str] = "";
                }
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = new List<string>();
            foreach (string str in (IEnumerable<JToken>) JArray.Parse(string_0))
            {
                this.value.Add(str);
            }
        }

        public override void getValueFromEditC()
        {
            this.value = new List<string>();
            for (int i = 0; i < this.maxNum; i++)
            {
                string item = func.getAttrByElem(base.elem(i + "k"), "value").Trim();
                if (item != "")
                {
                    this.value.Add(item);
                }
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
            this.handleEditValueChange(string_0);
        }

        public override void handleEditUserChange(string string_0)
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
            this.value = new List<string>();
        }

        public override void setBatchControl()
        {
            this.setEditControl();
        }

        public override void setEditControl()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.maxNum; i++)
            {
                string str = "";
                if (i < this.value.Count)
                {
                    str = this.value[i];
                }
                builder.Append(string.Concat(new object[] { "<input id='", base.KeyId, i, "k' type='text' value=\"", str, "\" onchange='valuechange(this.id);' />&nbsp;" }));
            }
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<input size=\"50\" type=\"text\" id=\"" + base.KeyId + "v\" value=\"\" onchange=\"valuechange(this.id)\" />";
        }

        public override void setTranslate()
        {
            List<string> list = new List<string>();
            foreach (string str in this.value)
            {
                string item = base.fatherObj.translateText[str];
                if (item != "")
                {
                    list.Add(item);
                }
                else
                {
                    list.Add(str);
                }
            }
            if (list.Count > 0)
            {
                this.value = list;
            }
        }

        public override void setValueFromMap()
        {
            this.value = base.oldPro.keywords.value;
            if ((this.value.Count == 0) && (base.fatherObj.cate.value.Count > 0))
            {
                for (int i = base.fatherObj.cate.value.Count - 1; i >= 0; i--)
                {
                    this.value.Add(base.fatherObj.cate.value[i].value.text[base.fatherObj.defaultLanguage]);
                }
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
                return "不能为空";
            }
            string str = "";
            if (this.value.Count > this.maxNum)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "关键词数量不能大于", this.maxNum, "个<br/>" });
            }
            int num = 0;
            foreach (string str2 in this.value)
            {
                string str3;
                if (num == 0)
                {
                    str3 = text.check(str2, base.rules["keywords1"].minLength, base.rules["keywords1"].maxLength, base.rules["keywords1"].isMust, base.rules["keywords1"].allowZH, base.rules["keywords1"].banReg);
                }
                else
                {
                    str3 = text.check(str2, base.rules["keywords"].minLength, base.rules["keywords"].maxLength, base.rules["keywords"].isMust, base.rules["keywords"].allowZH, base.rules["keywords"].banReg);
                }
                if (str3 != "")
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, "第", num + 1, "关键词", str3, "<br/>" });
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


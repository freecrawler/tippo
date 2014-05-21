namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class enumP : baseP
    {
        public bool allowZH;
        public List<string> banReg = new List<string>();
        public List<textID> baseData = new List<textID>();
        public textID defalu = new textID();
        public bool isInput;
        public int maxLength;
        public int maxValue;
        public int minLength;
        public int minValue;
        public int num = 10;
        private textID textID_0;
        public textID value = new textID();

        public override void batchEdit()
        {
            if (this.textID_0 != null)
            {
                this.value = this.textID_0;
            }
        }

        public override void forceEdit()
        {
            if (base.isMust)
            {
                if ((this.value.id == "") && (this.baseData.Count > 0))
                {
                    this.value = this.baseData[0];
                }
            }
            else if (this.value.id == "other")
            {
                this.value.text[base.fatherObj.defaultLanguage] = text.change(this.value.text[base.fatherObj.defaultLanguage], this.minLength, this.maxLength, base.isMust, this.allowZH, this.banReg, this.defalu.text[base.fatherObj.defaultLanguage]);
            }
        }

        public override void getBaseDataFromDataBase()
        {
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\base.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\base.jpg";
            }
            this.baseData = new List<textID>();
            DataSet set = SQLiteHelper.ExecuteDataSet("select data from base where linkid='" + base.baseDataLinkId + "' and key='" + base.key + "'", CommandType.Text);
            if (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && (set.Tables[0].Rows[0]["data"].ToString() != ""))
            {
                foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(set.Tables[0].Rows[0]["data"].ToString()))
                {
                    this.baseData.Add(textID.getValueFromJObject((JObject) token));
                }
            }
        }

        public override string getDataBaseFromValue()
        {
            return JsonConvert.SerializeObject(this.value.getJObject);
        }

        public void getHtmlFromValue(StringBuilder stringBuilder_0, List<textID> list_0)
        {
            foreach (textID tid in list_0)
            {
                string str = "";
                if (tid.id == this.value.id)
                {
                    str = "selected='selected'";
                }
                if (tid.childList.Count > 0)
                {
                    stringBuilder_0.Append("<optgroup label='" + tid.GetFullText + "'>");
                    this.getHtmlFromValue(stringBuilder_0, tid.childList);
                    stringBuilder_0.Append("</optgroup>");
                }
                else
                {
                    stringBuilder_0.Append("<option value='" + tid.id + "'" + str + " >" + tid.GetFullText + "</option>");
                }
            }
        }

        public override string getListCacheFromValue()
        {
            string defaultLanguage = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
            if (this.value.text.ContainsKey(base.fatherObj.defaultLanguage))
            {
                defaultLanguage = base.fatherObj.defaultLanguage;
            }
            return this.value.text[defaultLanguage];
        }

        public override string getSearchCacheFromValue()
        {
            string defaultLanguage = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
            if (this.value.text.ContainsKey(base.fatherObj.defaultLanguage))
            {
                defaultLanguage = base.fatherObj.defaultLanguage;
            }
            return this.value.text[defaultLanguage];
        }

        public override string getSearchCondition()
        {
            this.getValueFromEditC();
            if (this.value.text[base.fatherObj.defaultLanguage] != "")
            {
                return (" (" + base.dataBaseFieldForSearch + " = '" + this.value.text[base.fatherObj.defaultLanguage].Replace("'", "''") + "') ");
            }
            return "";
        }

        public override void getTranslate()
        {
            if (this.value.text[base.fatherObj.defaultLanguage] != "")
            {
                base.fatherObj.translateText[this.value.text[base.fatherObj.defaultLanguage]] = "";
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = textID.getValueFromJObject(JObject.Parse(string_0));
        }

        public override void getValueFromEditC()
        {
            this.value.id = func.getAttrByElem(base.elem("v"), "value");
            if (this.value.id == "other")
            {
                this.value.text[base.fatherObj.defaultLanguage] = func.getAttrByElem(base.elem("t"), "value");
            }
            else
            {
                this.value.text[base.fatherObj.defaultLanguage] = this.method_0(this.value.id);
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
            this.textID_0 = this.value;
        }

        public override void handleEditUserChange(string string_0)
        {
        }

        public override void handleEditValueChange(string string_0)
        {
            this.getValueFromEditC();
            this.setEditControl();
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = new textID();
        }

        private string method_0(string string_0)
        {
            using (List<textID>.Enumerator enumerator = this.baseData.GetEnumerator())
            {
                textID current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (current.id == string_0)
                    {
                        goto Label_0030;
                    }
                }
                goto Label_0082;
            Label_0030:
                if (current.text.ContainsKey(base.fatherObj.defaultLanguage))
                {
                    return current.text[base.fatherObj.defaultLanguage];
                }
                return current.text["en"];
            }
        Label_0082:
            return "";
        }

        public override void saveBaseData()
        {
            string str2;
            JArray array = new JArray();
            foreach (textID tid in this.baseData)
            {
                array.Add(tid.getJObject);
            }
            string str = JsonConvert.SerializeObject(array);
            if (base.baseDataIsOwn)
            {
                SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.fatherObj.storeId, @"\base.jpg" });
            }
            else
            {
                SQLiteHelper.Conn = "Data Source=" + PzFuEX.Jo4304 + @"a\" + base.fatherObj.siteId + @"\base.jpg";
            }
            if (SQLiteHelper.ExecuteDataSet("select count(rowid) from base where linkid='" + base.baseDataLinkId + "' and key='" + base.key + "'", 1).Tables[0].Rows[0][0].ToString() == "0")
            {
                str2 = "insert into base (linkid,data,key) values ('" + base.baseDataLinkId + "','" + str.Replace("'", "''") + "','" + base.key + "')";
            }
            else
            {
                str2 = "update base set data='" + str.Replace("'", "''") + "' where linkid='" + base.baseDataLinkId + "' and key='" + base.key + "'";
            }
            SQLiteHelper.ExecuteNonQuery(str2, CommandType.Text);
        }

        public override void setBatchControl()
        {
            this.setEditControl();
        }

        public override void setEditControl()
        {
            string str = (base.fatherObj.defaultLanguage == "zh-cn") ? base.fatherObj.defaultLanguage : "en";
            StringBuilder builder = new StringBuilder();
            builder.Append("<select id='" + base.KeyId + "v' onchange='valuechange(this.id);'><option value=''>请选择</option>");
            this.getHtmlFromValue(builder, this.baseData);
            builder.Append("</select>&nbsp;");
            if (this.value.id == "other")
            {
                builder.Append("<input id='" + base.KeyId + "t' type='text' value=\"" + func.quotToHtml(this.value.text[str]) + "\" onchange='valuechange(this.id);' />");
            }
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            this.setEditControl();
        }

        public override void setTranslate()
        {
            string str = base.fatherObj.translateText[this.value.text[base.fatherObj.defaultLanguage]];
            if (str != "")
            {
                this.value.text[base.fatherObj.toLanguage] = str;
            }
            else
            {
                this.value.text[base.fatherObj.toLanguage] = this.value.text[base.fatherObj.defaultLanguage];
            }
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            if (base.isMust && (this.value.id == ""))
            {
                return "请选择";
            }
            if (this.value.id == "other")
            {
                return text.check(this.value.text[base.fatherObj.defaultLanguage], this.minLength, this.maxLength, base.isMust, this.allowZH, this.banReg);
            }
            return "";
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


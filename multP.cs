namespace client
{
    using IfacesEnumsStructsClasses;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class multP : baseP
    {
        public bool allowZH;
        public List<string> banReg = new List<string>();
        public List<textID> baseData = new List<textID>();
        public List<textID> defalu = new List<textID>();
        public bool isInput;
        private List<textID> list_0;
        public int maxLength;
        public int maxValue;
        public int minLength;
        public int minValue;
        public int num = 10;
        public List<textID> value = new List<textID>();

        public override void batchEdit()
        {
            if (this.list_0 != null)
            {
                this.value = this.list_0;
            }
        }

        public override void forceEdit()
        {
            if ((base.isMust && (this.value.Count <= 0)) && (this.baseData.Count > 0))
            {
                this.value.Add(this.baseData[0]);
            }
            foreach (textID tid in this.value)
            {
                if (tid.id == "other")
                {
                    tid.text[base.fatherObj.defaultLanguage] = text.change(tid.text[base.fatherObj.defaultLanguage], this.minLength, this.maxLength, base.isMust, this.allowZH, this.banReg, "_");
                }
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
            JArray array = new JArray();
            foreach (textID tid in this.value)
            {
                array.Add(tid.getJObject);
            }
            return JsonConvert.SerializeObject(array);
        }

        public override string getListCacheFromValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (textID tid in this.value)
            {
                builder.Append(tid.text[base.fatherObj.defaultLanguage] + "<br/>");
            }
            return builder.ToString();
        }

        public override string getSearchCacheFromValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (textID tid in this.value)
            {
                builder.Append(tid.text[base.fatherObj.defaultLanguage]);
            }
            return builder.ToString();
        }

        public override string getSearchCondition()
        {
            StringBuilder builder = new StringBuilder();
            foreach (textID tid in this.value)
            {
                if (tid.text.ContainsKey(base.fatherObj.defaultLanguage))
                {
                    builder.Append(tid.text[base.fatherObj.defaultLanguage]);
                }
                else
                {
                    builder.Append(tid.text["en"]);
                }
            }
            if (builder.ToString() != "")
            {
                return (" (" + base.dataBaseFieldForSearch + " = '" + builder.ToString().Replace("'", "''") + "') ");
            }
            return "";
        }

        public override void getTranslate()
        {
            foreach (textID tid in this.value)
            {
                if (tid.text[base.fatherObj.defaultLanguage] != "")
                {
                    base.fatherObj.translateText[tid.text[base.fatherObj.defaultLanguage]] = "";
                }
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = new List<textID>();
            foreach (JToken token in (IEnumerable<JToken>) JArray.Parse(string_0))
            {
                this.value.Add(textID.getValueFromJObject((JObject) token));
            }
        }

        public override void getValueFromEditC()
        {
            IHTMLElementCollection elementsByName = base.webbrowser.GetElementsByName(true, base.KeyId + "v");
            this.value = new List<textID>();
            foreach (IHTMLElement element in elementsByName)
            {
                if ((bool) element.getAttribute("checked", 0))
                {
                    textID item = new textID();
                    item.id = func.getAttrByElem(element, "value");
                    if (item.id == "other")
                    {
                        item.text[base.fatherObj.defaultLanguage] = func.getAttrByElem(base.elem("t"), "value");
                    }
                    else
                    {
                        item.text[base.fatherObj.defaultLanguage] = func.getAttrByElem(element, "txt");
                    }
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
            this.getValueFromEditC();
            this.setEditControl();
            this.list_0 = this.value;
        }

        public override void handleBatchValueChange(string string_0)
        {
            this.handleEditValueChange(string_0);
            this.list_0 = this.value;
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
            this.value = new List<textID>();
        }

        private bool method_0(string string_0)
        {
            foreach (textID tid in this.value)
            {
                if (tid.id == string_0)
                {
                    return true;
                }
            }
            return false;
        }

        private textID method_1(string string_0)
        {
            foreach (textID tid in this.value)
            {
                if (tid.id == string_0)
                {
                    return tid;
                }
            }
            return new textID();
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
            foreach (textID tid in this.baseData)
            {
                string str2 = "";
                if (this.method_0(tid.id))
                {
                    str2 = "checked='checked'";
                }
                builder.Append("<input id='" + base.KeyId + "v' name='" + base.KeyId + "v' type='checkbox' value='" + tid.id + "' txt=\"" + func.quotToHtml(tid.text[str]) + "\" " + str2 + " onclick='valuechange(this.id);' />" + tid.GetFullText + "&nbsp;");
            }
            if (this.method_0("other"))
            {
                builder.Append("<input id='" + base.KeyId + "t' type='text' value=\"" + func.quotToHtml(this.method_1("other").text[str]) + "\" onchange='valuechange(this.id);' />");
            }
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            this.setEditControl();
        }

        public override void setTranslate()
        {
            foreach (textID tid in this.value)
            {
                string str = base.fatherObj.translateText[tid.text[base.fatherObj.defaultLanguage]];
                if (str != "")
                {
                    tid.text[base.fatherObj.toLanguage] = str;
                }
                else
                {
                    tid.text[base.fatherObj.toLanguage] = tid.text[base.fatherObj.defaultLanguage];
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
                return "请选择一项";
            }
            foreach (textID tid in this.value)
            {
                if (tid.id == "other")
                {
                    return text.check(tid.text[base.fatherObj.defaultLanguage], this.minLength, this.maxLength, base.isMust, this.allowZH, this.banReg);
                }
            }
            return "";
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


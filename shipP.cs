namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class shipP : baseP
    {
        public List<shipMethodFee> defalu = new List<shipMethodFee>();
        private Dictionary<string, shipMethodFee> dictionary_0;
        public bool isFee;
        public Dictionary<string, string> shipCompany = new Dictionary<string, string>();
        public Dictionary<string, shipMethodFee> value = new Dictionary<string, shipMethodFee>();

        public shipP(fatherObj fatherObj_0)
        {
            base.key = "ship";
            base.name = "运费设置";
            base.dataBaseFieldForSave = "f20";
            base.dataBaseFieldForList = "f21";
            base.dataBaseFieldForSearch = "f22";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["fee"] = new rule();
            base.rules["off"] = new rule();
        }

        public override void batchEdit()
        {
            this.value = this.dictionary_0;
        }

        public override void forceEdit()
        {
            foreach (KeyValuePair<string, shipMethodFee> pair in this.value)
            {
                if (pair.Value.isSelect)
                {
                    if (this.isFee)
                    {
                        pair.Value.fee = text.change(pair.Value.fee, base.rules["fee"].isMust, base.rules["fee"].minValue, base.rules["fee"].maxValue, base.rules["fee"].num, base.rules["fee"].defaluD);
                    }
                    else
                    {
                        pair.Value.off = text.change(pair.Value.off, base.rules["fee"].isMust, base.rules["fee"].minValue, base.rules["fee"].maxValue, base.rules["fee"].num, base.rules["fee"].defaluD);
                    }
                }
            }
        }

        public override string getDataBaseFromValue()
        {
            JObject obj2 = new JObject();
            foreach (KeyValuePair<string, shipMethodFee> pair in this.value)
            {
                obj2[pair.Key] = pair.Value.getJObject;
            }
            return JsonConvert.SerializeObject(obj2);
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = new Dictionary<string, shipMethodFee>();
            foreach (KeyValuePair<string, JToken> pair in JObject.Parse(string_0))
            {
                this.value[pair.Key] = shipMethodFee.getValueFromJObject((JObject) pair.Value);
            }
        }

        public override void getValueFromEditC()
        {
            foreach (KeyValuePair<string, shipMethodFee> pair in this.value)
            {
                pair.Value.isSelect = (bool) base.elem(pair.Key + "-").getAttribute("checked", 0);
                if (this.isFee)
                {
                    pair.Value.fee = text.toDouble(func.getAttrByElem(base.elem(pair.Key + "v"), "value"));
                }
                else
                {
                    pair.Value.off = text.toInt(func.getAttrByElem(base.elem(pair.Key + "v"), "value"));
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
            this.dictionary_0 = this.value;
        }

        public override void handleEditUserChange(string string_0)
        {
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = new Dictionary<string, shipMethodFee>();
        }

        public override void setBatchControl()
        {
            this.setEditControl();
        }

        public override void setEditControl()
        {
            if (this.value.Count == 0)
            {
                foreach (KeyValuePair<string, string> pair in this.shipCompany)
                {
                    this.value[pair.Key] = new shipMethodFee();
                }
            }
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, shipMethodFee> pair2 in this.value)
            {
                string str = "";
                if (this.isFee)
                {
                    str = string.Concat(new object[] { "运费&nbsp;<input id='", base.KeyId, pair2.Key, "v' type='text' value='", pair2.Value.fee, "'  onchange='valuechange(this.id);' />" });
                }
                else
                {
                    str = string.Concat(new object[] { "折扣&nbsp;<input id='", base.KeyId, pair2.Key, "v' type='text' value='", pair2.Value.off, "'  onchange='valuechange(this.id);' />&nbsp;(填写0至100的整数，0表示免运费，100表示无折扣)" });
                }
                string str2 = "";
                if (pair2.Value.isSelect)
                {
                    str2 = "checked='checked'";
                }
                builder.Append("<div style='padding:5px'><input id='" + base.KeyId + pair2.Key + "-' type='checkbox' " + str2 + " onchange='valuechange(this.id);' />" + this.shipCompany[pair2.Key].PadRight(8, '@').Replace("@", "&nbsp;&nbsp;") + "&nbsp;&nbsp;" + str + "</div>");
            }
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
        }

        public override void setValueFromMap()
        {
            foreach (KeyValuePair<string, shipMethodFee> pair in base.oldPro.ship.value)
            {
                if (this.shipCompany.ContainsKey(pair.Key))
                {
                    this.value[pair.Key] = pair.Value;
                }
            }
        }

        public override string verifyEdit()
        {
            string str = "";
            foreach (KeyValuePair<string, shipMethodFee> pair in this.value)
            {
                if (pair.Value.isSelect)
                {
                    if (this.isFee)
                    {
                        string str2 = text.check(pair.Value.fee, base.rules["fee"].isMust, base.rules["fee"].minValue, base.rules["fee"].maxValue, base.rules["fee"].num);
                        if (str2 != "")
                        {
                            string str4 = str;
                            str = str4 + this.shipCompany[pair.Key] + "的运费" + str2 + "<br/>";
                        }
                    }
                    else
                    {
                        string str3 = text.check((double) pair.Value.off, base.rules["off"].isMust, base.rules["off"].minValue, base.rules["off"].maxValue, base.rules["off"].num);
                        if (str3 != "")
                        {
                            string str5 = str;
                            str = str5 + this.shipCompany[pair.Key] + "的运费折扣" + str3 + "<br/>";
                        }
                    }
                }
            }
            return str;
        }
    }
}


namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Text;

    public class weightP : baseP
    {
        public weight defalu = new weight();
        public bool isEditCustom;
        public weight value = new weight();

        public weightP(fatherObj fatherObj_0)
        {
            base.key = "weight";
            base.name = "重量";
            base.searchName = "重量范围";
            base.dataBaseFieldForSave = "f23";
            base.dataBaseFieldForList = "f24";
            base.dataBaseFieldForSearch = "f25";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["weigh"] = new rule();
            base.rules["itemNum"] = new rule();
            base.rules["addNum"] = new rule();
            base.rules["addWeight"] = new rule();
        }

        public override void batchEdit()
        {
            if ((bool) base.elem("cc").getAttribute("checked", 0))
            {
                string str;
                double num = text.toDouble(func.getAttrByElem(base.elem("v"), "value"));
                if ((num > 0.0) && ((str = func.getAttrByElem(base.elem("cz"), "value")) != null))
                {
                    if (str == "+")
                    {
                        this.value.weigh += num;
                    }
                    else if (str == "-")
                    {
                        this.value.weigh -= num;
                    }
                    else if (str == "*")
                    {
                        this.value.weigh *= num;
                    }
                    else if (str == "/")
                    {
                        this.value.weigh /= num;
                    }
                }
            }
            else if ((bool) base.elem("nn").getAttribute("checked", 0))
            {
                double num2 = text.toDouble(func.getAttrByElem(base.elem("nz"), "value"));
                if (num2 >= 0.0)
                {
                    this.value.weigh = num2;
                }
            }
        }

        public override void forceEdit()
        {
            this.value.weigh = text.change(this.value.weigh, base.rules["weigh"].isMust, base.rules["weigh"].minValue, base.rules["weigh"].maxValue, base.rules["weigh"].num, base.rules["weigh"].defaluD);
            if (this.value.isCustom)
            {
                this.value.itemNum = text.change(this.value.itemNum, base.rules["itemNum"].isMust, base.rules["itemNum"].minValue, base.rules["itemNum"].maxValue, base.rules["itemNum"].num, base.rules["itemNum"].defaluD);
                this.value.addNum = text.change(this.value.addNum, base.rules["addNum"].isMust, base.rules["addNum"].minValue, base.rules["addNum"].maxValue, base.rules["addNum"].num, base.rules["addNum"].defaluD);
                this.value.addWeight = text.change(this.value.addWeight, base.rules["addWeight"].isMust, base.rules["addWeight"].minValue, base.rules["addWeight"].maxValue, base.rules["addWeight"].num, base.rules["addWeight"].defaluD);
            }
        }

        public override string getDataBaseFromValue()
        {
            JObject obj2 = new JObject();
            obj2["addNum"] = this.value.addNum;
            obj2["isCustom"] = this.value.isCustom;
            obj2["weigh"] = this.value.weigh;
            obj2["itemNum"] = this.value.itemNum;
            obj2["addWeight"] = this.value.addWeight;
            return JsonConvert.SerializeObject(obj2);
        }

        public override string getListCacheFromValue()
        {
            return this.getSearchCacheFromValue();
        }

        public override string getSearchCacheFromValue()
        {
            return Math.Round(this.value.weigh, 2).ToString();
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

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            JObject obj2 = JObject.Parse(string_0);
            this.value.addNum = (int) obj2["addNum"];
            this.value.isCustom = (bool) obj2["isCustom"];
            this.value.weigh = (double) obj2["weigh"];
            this.value.itemNum = (int) obj2["itemNum"];
            this.value.addWeight = (double) obj2["addWeight"];
        }

        public override void getValueFromEditC()
        {
            this.value.weigh = text.toDouble(func.getAttrByElem(base.elem("w"), "value"));
            if (this.isEditCustom)
            {
                this.value.isCustom = (bool) base.elem("c").getAttribute("checked", 0);
                this.value.itemNum = text.toInt(func.getAttrByElem(base.elem("n"), "value"));
                this.value.addNum = text.toInt(func.getAttrByElem(base.elem("m"), "value"));
                this.value.addWeight = text.toDouble(func.getAttrByElem(base.elem("a"), "value"));
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
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = new weight();
        }

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style='clear:both; padding:20px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "cc'/>在原值基础上&nbsp;<select id='" + base.KeyId + "cz'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
            builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v' type='text' size='8' /></div><div style='clear:both;padding:20px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "nn'/>全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz' /></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            string str2 = "display:none;";
            if (this.isEditCustom && this.value.isCustom)
            {
                str = "checked='checked'";
                str2 = "display:block;";
            }
            builder.Append("<div>重&nbsp;<input type='text' size='8' id='" + base.KeyId + "w' value='" + Math.Round(this.value.weigh, 3).ToString() + "' onchange='valuechange(this.id);' />kg&nbsp;&nbsp;");
            if (this.isEditCustom)
            {
                builder.Append("<input " + str + " id='" + base.KeyId + "c' type='checkbox' onclick=\"if(this.checked){document.getElementById('weightcus').style.display='block';}else{document.getElementById('weightcus').style.display='none';}\" onchange='valuechange(this.id);' />自定义计重");
            }
            builder.Append(string.Concat(new object[] { "</div><div id='weightcus' style='padding:3px;background-color: #FFFAE0;border: 1px solid #F1D38B;", str2, "'><p>买家购买<input type='text' size='8' value='", this.value.itemNum, "' id='", base.KeyId, "n' onchange='valuechange(this.id);' />件以内，按单件产品重量计算运费。</p><p>在此基础上，买家每多买<input type='text' size='8' id='", base.KeyId, "m' onchange='valuechange(this.id);' value='", this.value.addNum, "' />件，重量增加<input id='", base.KeyId, "a' onchange='valuechange(this.id);' value='", this.value.addWeight, "' type='text' size='8' />kg。</p></div>" }));
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<input id='" + base.KeyId + "from' type='text' size='10' />&nbsp;-&nbsp;<input id='" + base.KeyId + "to' type='text' size='10' />";
        }

        public override void setValueFromMap()
        {
            this.value = base.oldPro.weight.value;
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            string str = "";
            string str2 = text.check(this.value.weigh, base.rules["weigh"].isMust, base.rules["weigh"].minValue, base.rules["weigh"].maxValue, base.rules["weigh"].num);
            if (str2 != "")
            {
                str = str + "重量" + str2 + ",";
            }
            if (this.value.isCustom)
            {
                str2 = text.check((double) this.value.itemNum, base.rules["itemNum"].isMust, base.rules["itemNum"].minValue, base.rules["itemNum"].maxValue, base.rules["itemNum"].num);
                if (str2 != "")
                {
                    str = str + "多少件以内" + str2 + ",";
                }
                str2 = text.check((double) this.value.addNum, base.rules["addNum"].isMust, base.rules["addNum"].minValue, base.rules["addNum"].maxValue, base.rules["addNum"].num);
                if (str2 != "")
                {
                    str = str + "每增加数量" + str2 + ",";
                }
                str2 = text.check(this.value.addWeight, base.rules["addWeight"].isMust, base.rules["addWeight"].minValue, base.rules["addWeight"].maxValue, base.rules["addWeight"].num);
                if (str2 != "")
                {
                    str = str + "每增加重量" + str2 + ",";
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


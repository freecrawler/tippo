namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Text;

    public class saleMethodP : baseP
    {
        public saleMethod defalu = new saleMethod();
        private saleMethod saleMethod_0;
        public saleMethod value = new saleMethod();

        public saleMethodP(fatherObj fatherObj_0)
        {
            base.key = "saleMethod";
            base.name = "销售方式";
            base.dataBaseFieldForSave = "f37";
            base.dataBaseFieldForList = "f38";
            base.dataBaseFieldForSearch = "f39";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["lotNum"] = new rule();
        }

        public override void batchEdit()
        {
            this.getValueFromEditC();
        }

        public override void forceEdit()
        {
            if (this.value.isLot)
            {
                this.value.lotNum = text.change(this.value.lotNum, base.rules["lotNum"].isMust, base.rules["lotNum"].minValue, base.rules["lotNum"].maxValue, base.rules["lotNum"].num, base.rules["lotNum"].defaluD);
            }
        }

        public override string getDataBaseFromValue()
        {
            JObject obj2 = new JObject();
            obj2["isLot"] = this.value.isLot;
            obj2["lotNum"] = this.value.lotNum;
            return JsonConvert.SerializeObject(obj2);
        }

        public override string getListCacheFromValue()
        {
            if (this.value.isLot)
            {
                return ("打包销售(" + this.value.lotNum + "个)");
            }
            return "单个销售";
        }

        public override string getSearchCacheFromValue()
        {
            if (this.value.isLot)
            {
                return "1";
            }
            return "0";
        }

        public override string getSearchCondition()
        {
            this.value.isLot = (bool) base.elem("b").getAttribute("checked", 0);
            if (this.value.isLot)
            {
                return (" (" + base.dataBaseFieldForSearch + " = '1') ");
            }
            if ((bool) base.elem("d").getAttribute("checked", 0))
            {
                return (" (" + base.dataBaseFieldForSearch + " = '0') ");
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
            this.value.isLot = (bool) obj2["isLot"];
            this.value.lotNum = (int) obj2["lotNum"];
        }

        public override void getValueFromEditC()
        {
            this.value.isLot = (bool) base.elem("b").getAttribute("checked", 0);
            this.value.lotNum = text.toInt(func.getAttrByElem(base.elem("v"), "value"));
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
            this.saleMethod_0 = this.value;
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
            this.value = new saleMethod();
        }

        public override void setBatchControl()
        {
            this.setEditControl();
        }

        public override void setEditControl()
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            string str2 = "";
            string str3 = "display:none";
            if (this.value.isLot)
            {
                str2 = "checked='checked'";
                str3 = "display:block;";
            }
            else
            {
                str = "checked='checked'";
            }
            builder.Append(string.Concat(new object[] { "<div style='float:left'><input name='salemethodq' type='radio' id='", base.KeyId, "d' onclick=\"if(this.checked){document.getElementById('salemethodnum').style.display='none';};valuechange(this.id);\" ", str, " />单件销售&nbsp;&nbsp;<input name='salemethodq' type='radio' id='", base.KeyId, "b' onclick=\"if(this.checked){document.getElementById('salemethodnum').style.display='block';};valuechange(this.id);\" ", str2, " />打包销售&nbsp;</div><div id='salemethodnum' style='float:left;", str3, "'>每包数量<input type='text' size='8' onchange='valuechange(this.id);' value='", this.value.lotNum, "' id='", base.KeyId, "v' /></div>" }));
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<input name='salemethodq' type='radio' id='" + base.KeyId + "d' />单件销售&nbsp;&nbsp;<input name='salemethodq' type='radio' id='" + base.KeyId + "b' />打包销售";
        }

        public override void setValueFromMap()
        {
            this.value = base.oldPro.saleMethod.value;
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            if (this.value.isLot)
            {
                return text.check((double) this.value.lotNum, base.rules["lotNum"].isMust, base.rules["lotNum"].minValue, base.rules["lotNum"].maxValue, base.rules["lotNum"].num);
            }
            return "";
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


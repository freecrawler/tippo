namespace client
{
    using System;
    using System.Text;

    public class inputDigi : baseP
    {
        public double defalu;
        public double maxValue;
        public double minValue;
        public int num = 10;
        public double value;

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
                        this.value += num;
                    }
                    else if (str == "-")
                    {
                        this.value -= num;
                    }
                    else if (str == "*")
                    {
                        this.value *= num;
                    }
                    else if (str == "/")
                    {
                        this.value /= num;
                    }
                }
            }
            else if ((bool) base.elem("nn").getAttribute("checked", 0))
            {
                double num2 = text.toDouble(func.getAttrByElem(base.elem("nz"), "value"));
                if (num2 >= 0.0)
                {
                    this.value = num2;
                }
            }
        }

        public override void forceEdit()
        {
            this.value = text.change(this.value, base.isMust, this.minValue, this.maxValue, this.num, this.defalu);
        }

        public override string getDataBaseFromValue()
        {
            return this.value.ToString();
        }

        public override string getListCacheFromValue()
        {
            return this.value.ToString();
        }

        public override string getSearchCacheFromValue()
        {
            return this.value.ToString();
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
            this.value = text.toDouble(string_0);
        }

        public override void getValueFromEditC()
        {
            this.value = text.toDouble(func.getAttrByElem(base.elem("v"), "value"));
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

        public override void handleEditValueChange(string string_0)
        {
            base.handleEditValueChange(string_0);
        }

        public override void handleSearchUserChange(string string_0)
        {
        }

        public override void handleSearchValueChange(string string_0)
        {
        }

        public override void initValue()
        {
            this.value = 0.0;
        }

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style='clear:both; padding:20px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "cc' />在原值基础上&nbsp;<select id='" + base.KeyId + "cz'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
            builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v' type='text' size='8' /></div><div style='clear:both;padding:20px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "nn'/>全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz' /></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            base.control.innerHTML = "<input size=\"8\" type=\"text\" id=\"" + base.KeyId + "v\" value=\"" + Math.Round(this.value, 2).ToString() + "\" onchange=\"valuechange(this.id)\" />";
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<input id='" + base.KeyId + "from' type='text' size='10' />&nbsp;-&nbsp;<input id='" + base.KeyId + "to' type='text' size='10' />";
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            return text.check(this.value, base.isMust, this.minValue, this.maxValue, this.num);
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


namespace client
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class inputText : baseP
    {
        public bool allowZH;
        public List<string> banReg = new List<string>();
        public string defalu = "";
        public bool isMultRows;
        public int maxLength;
        public int minLength;
        public string value = "";

        public override void batchEdit()
        {
            if ((bool) base.elem("add").getAttribute("checked", 0))
            {
                this.value = func.getAttrByElem(base.elem("qz"), "value") + this.value + func.getAttrByElem(base.elem("hz"), "value");
            }
            else if ((bool) base.elem("ser").getAttribute("checked", 0))
            {
                string str = func.getAttrByElem(base.elem("lz"), "value");
                if (str != "")
                {
                    str = Regex.Escape(str);
                    this.value = func.正则替换(this.value, str, func.getAttrByElem(base.elem("xz"), "value"));
                }
            }
            else if ((bool) base.elem("rep").getAttribute("checked", 0))
            {
                string str2 = func.getAttrByElem(base.elem("nz"), "value");
                if (str2 != "")
                {
                    this.value = str2;
                }
            }
        }

        public override void forceEdit()
        {
            List<string> list = new List<string>();
            list.AddRange(this.banReg);
            list.AddRange(base.fatherObj.banReg);
            this.value = text.change(this.value, this.minLength, this.maxLength, base.isMust, this.allowZH, list, base.fatherObj.allowReg, this.defalu);
        }

        public override string getDataBaseFromValue()
        {
            return this.value;
        }

        public override string getListCacheFromValue()
        {
            return this.value;
        }

        public override string getSearchCacheFromValue()
        {
            return this.value;
        }

        public override string getSearchCondition()
        {
            this.value = func.getAttrByElem(base.elem("v"), "value");
            if (this.value != "")
            {
                return (" (" + base.dataBaseFieldForSearch + " like '%" + this.value.Replace("'", "''") + "%') ");
            }
            return "";
        }

        public override void getTranslate()
        {
            if (this.value != "")
            {
                base.fatherObj.translateText[this.value] = "";
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            this.value = string_0;
        }

        public override void getValueFromEditC()
        {
            if (this.isMultRows)
            {
                try
                {
                    this.value = base.elem("v").innerHTML.Trim();
                }
                catch
                {
                    this.value = "";
                }
            }
            else
            {
                this.value = func.getAttrByElem(base.elem("v"), "value");
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
            this.value = "";
        }

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style='clear:both; padding:20px'><div style='width:150px; float:left'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "add' />增加</div><div style='float:left'>前缀&nbsp;<input id='" + base.KeyId + "qz' type='text' />&nbsp;后缀&nbsp;<input id='" + base.KeyId + "hz' type='text' /></div></div>");
            builder.Append("<div style='clear:both;padding:20px'><div style='width:150px; float:left'><input name='" + base.KeyId + "sel' type='radio' value=''  id='" + base.KeyId + "ser' />查找并替换</div><div style='float:left'>将&nbsp;<input id='" + base.KeyId + "lz' type='text' />&nbsp;替换为&nbsp;<input id='" + base.KeyId + "xz' type='text' /></div></div>");
            builder.Append("<div style='clear:both;padding:20px'><div style='width:150px; float:left'><input name='" + base.KeyId + "sel' type='radio' value=''  id='" + base.KeyId + "rep'  />全部更换为</div><div style='float:left'><input id='" + base.KeyId + "nz' type='text' size='54' /></div></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            if (this.isMultRows)
            {
                base.control.innerHTML = string.Concat(new object[] { "<div style='float:left'><textarea cols=\"70\" rows=\"5\" id=\"", base.KeyId, "v\" onchange=\"valuechange(this.id)\" onkeyup=\" document.getElementById('", base.KeyId, "cc').innerHTML=this.value.length;\">", func.quotToHtml(this.value), "</textarea></div><div style='float:left'>&nbsp;<font id=\"", base.KeyId, "cc\">", this.value.Length, "</font>/", this.maxLength, "</div>" });
            }
            else
            {
                base.control.innerHTML = string.Concat(new object[] { "<div style='float:left'><input size=\"70\" type=\"text\" id=\"", base.KeyId, "v\" value=\"", func.quotToHtml(this.value), "\" onchange=\"valuechange(this.id)\" onkeyup=\" document.getElementById('", base.KeyId, "cc').innerHTML=this.value.length;\" /></div><div style='float:left'>&nbsp;<font id=\"", base.KeyId, "cc\">", this.value.Length, "</font>/", this.maxLength, "</div>" });
            }
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<input size=\"50\" type=\"text\" id=\"" + base.KeyId + "v\" value=\"\" onchange=\"valuechange(this.id)\" />";
        }

        public override void setTranslate()
        {
            string str = base.fatherObj.translateText[this.value];
            if (str != "")
            {
                this.value = str;
            }
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            List<string> list = new List<string>();
            list.AddRange(this.banReg);
            list.AddRange(base.fatherObj.banReg);
            string str = text.check(this.value, this.minLength, this.maxLength, base.isMust, this.allowZH, list, base.fatherObj.allowReg);
            if (str != "")
            {
                return (base.name + str);
            }
            return "";
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


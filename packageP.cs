namespace client
{
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Text;

    public class packageP : baseP
    {
        public package defalu = new package();
        public package value = new package();

        public packageP(fatherObj fatherObj_0)
        {
            base.key = "package";
            base.name = "包装尺寸";
            base.searchName = "包装尺寸范围";
            base.dataBaseFieldForSave = "f26";
            base.dataBaseFieldForList = "f27";
            base.dataBaseFieldForSearch = "f28";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
            base.rules["length"] = new rule();
            base.rules["height"] = new rule();
            base.rules["width"] = new rule();
        }

        public override void batchEdit()
        {
            if ((bool) base.elem("cc").getAttribute("checked", 0))
            {
                this.value.length = this.method_0(this.value.length, func.getAttrByElem(base.elem("cz"), "value"), func.getAttrByElem(base.elem("v"), "value"));
            }
            else if ((bool) base.elem("nn").getAttribute("checked", 0))
            {
                double num = text.toDouble(func.getAttrByElem(base.elem("nz"), "value"));
                if (num >= 0.0)
                {
                    this.value.length = num;
                }
            }
            if ((bool) base.elem("cc2").getAttribute("checked", 0))
            {
                this.value.width = this.method_0(this.value.width, func.getAttrByElem(base.elem("cz2"), "value"), func.getAttrByElem(base.elem("v2"), "value"));
            }
            else if ((bool) base.elem("nn2").getAttribute("checked", 0))
            {
                double num2 = text.toDouble(func.getAttrByElem(base.elem("nz2"), "value"));
                if (num2 >= 0.0)
                {
                    this.value.width = num2;
                }
            }
            if ((bool) base.elem("cc3").getAttribute("checked", 0))
            {
                this.value.height = this.method_0(this.value.height, func.getAttrByElem(base.elem("cz3"), "value"), func.getAttrByElem(base.elem("v3"), "value"));
            }
            else if ((bool) base.elem("nn3").getAttribute("checked", 0))
            {
                double num3 = text.toDouble(func.getAttrByElem(base.elem("nz3"), "value"));
                if (num3 >= 0.0)
                {
                    this.value.height = num3;
                }
            }
        }

        public override void forceEdit()
        {
            this.value.length = text.change(this.value.length, base.rules["length"].isMust, base.rules["length"].minValue, base.rules["length"].maxValue, base.rules["length"].num, base.rules["length"].defaluD);
            this.value.height = text.change(this.value.height, base.rules["height"].isMust, base.rules["height"].minValue, base.rules["height"].maxValue, base.rules["height"].num, base.rules["height"].defaluD);
            this.value.width = text.change(this.value.width, base.rules["width"].isMust, base.rules["width"].minValue, base.rules["width"].maxValue, base.rules["width"].num, base.rules["width"].defaluD);
            double length = this.value.length;
            double width = this.value.width;
            double height = this.value.height;
            double num4 = 0.0;
            double num5 = 0.0;
            if (length > width)
            {
                num4 = length;
            }
            else
            {
                num4 = width;
                width = length;
            }
            if (num4 > height)
            {
                num5 = num4 + (2.0 * (width + height));
            }
            else
            {
                num5 = height + (2.0 * (width + num4));
            }
            if (num5 > 419.0)
            {
                this.value.length = 1.0;
            }
        }

        public override string getDataBaseFromValue()
        {
            JObject obj2 = new JObject();
            obj2["length"] = this.value.length;
            obj2["height"] = this.value.height;
            obj2["width"] = this.value.width;
            return JsonConvert.SerializeObject(obj2);
        }

        public override string getListCacheFromValue()
        {
            return (Math.Round(this.value.length, 2).ToString() + " * " + Math.Round(this.value.width, 2).ToString() + " * " + Math.Round(this.value.height, 2).ToString());
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromDataBase(string string_0)
        {
            JObject obj2 = JObject.Parse(string_0);
            this.value.length = (double) obj2["length"];
            this.value.height = (double) obj2["height"];
            this.value.width = (double) obj2["width"];
        }

        public override void getValueFromEditC()
        {
            this.value.length = text.toDouble(func.getAttrByElem(base.elem("l"), "value"));
            this.value.width = text.toDouble(func.getAttrByElem(base.elem("w"), "value"));
            this.value.height = text.toDouble(func.getAttrByElem(base.elem("h"), "value"));
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
            this.value = new package();
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

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style='clear:both;'><h5>批量修改长</h5></div>");
            builder.Append("<div style='clear:both; padding:10px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "cc' />在原值基础上&nbsp;<select id='" + base.KeyId + "cz'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
            builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v' type='text' size='8' /></div><div style='clear:both;padding:10px'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "nn'/>全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz' /></div>");
            builder.Append("<div style='clear:both;'><h5>批量修改宽</h5></div>");
            builder.Append("<div style='clear:both; padding:10px'><input name='" + base.KeyId + "sel2' type='radio' value=''id='" + base.KeyId + "cc2' />在原值基础上&nbsp;<select id='" + base.KeyId + "cz2'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
            builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v2' type='text' size='8' /></div><div style='clear:both;padding:10px'><input name='" + base.KeyId + "sel2' type='radio' value='' id='" + base.KeyId + "nn2'/>全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz2' /></div>");
            builder.Append("<div style='clear:both;'><h5>批量修改高</h5></div>");
            builder.Append("<div style='clear:both; padding:10px'><input name='" + base.KeyId + "sel3' type='radio' value='' id='" + base.KeyId + "cc3'/>在原值基础上&nbsp;<select id='" + base.KeyId + "cz3'><option value='+'>加上</option><option value='-'>减去</option><option value='*'>乘以</option><option value='/'>");
            builder.Append("除以</option></select>&nbsp;<input id='" + base.KeyId + "v3' type='text' size='8' /></div><div style='clear:both;padding:10px'><input name='" + base.KeyId + "sel3' type='radio' value='' id='" + base.KeyId + "nn3'/>全部设为新值&nbsp;<input type='text' size='8' id='" + base.KeyId + "nz3' /></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "长&nbsp;<input type='text' size='8' id='", base.KeyId, "l' value='", Math.Round(this.value.length, 2), "'  onchange='valuechange(this.id);' />cm&nbsp;宽&nbsp;<input type='text' size='8' id='", base.KeyId, "w' value='", Math.Round(this.value.width, 2), "' onchange='valuechange(this.id);' />cm&nbsp;高&nbsp;<input type='text' size='8' id='", base.KeyId, "h' value='", Math.Round(this.value.height, 2), "' onchange='valuechange(this.id);' />cm&nbsp;" }));
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("长<input id='" + base.KeyId + "froml' type='text' size='8' />-<input id='" + base.KeyId + "tol' type='text' size='8' />&nbsp;");
            builder.Append("宽<input id='" + base.KeyId + "fromw' type='text' size='8' />-<input id='" + base.KeyId + "tow' type='text' size='8' />&nbsp;");
            builder.Append("高<input id='" + base.KeyId + "fromh' type='text' size='8' />-<input id='" + base.KeyId + "toh' type='text' size='8' />&nbsp;");
            base.control.innerHTML = builder.ToString();
        }

        public override void setValueFromMap()
        {
            this.value = base.oldPro.package.value;
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            if (base.isMust && (((this.value.length <= 0.0) || (this.value.height <= 0.0)) || (this.value.width <= 0.0)))
            {
                return "不能为空";
            }
            string str = text.check(this.value.length, base.rules["length"].isMust, base.rules["length"].minValue, base.rules["length"].maxValue, base.rules["length"].num);
            string str2 = text.check(this.value.height, base.rules["height"].isMust, base.rules["height"].minValue, base.rules["height"].maxValue, base.rules["height"].num);
            string str3 = text.check(this.value.width, base.rules["width"].isMust, base.rules["width"].minValue, base.rules["width"].maxValue, base.rules["width"].num);
            if ((str + str2 + str3) == "")
            {
                double length = this.value.length;
                double width = this.value.width;
                double height = this.value.height;
                double num4 = 0.0;
                double num5 = 0.0;
                if (length > width)
                {
                    num4 = length;
                }
                else
                {
                    num4 = width;
                    width = length;
                }
                if (num4 > height)
                {
                    num5 = num4 + (2.0 * (width + height));
                }
                else
                {
                    num5 = height + (2.0 * (width + num4));
                }
                if (num5 > 419.0)
                {
                    return "产品包装尺寸的最大值+2\x00d7（第二大值+第三大值）不能超过419厘米";
                }
                return "";
            }
            string str4 = "";
            if (str != "")
            {
                str4 = str4 + "长" + str + ",";
            }
            if (str2 != "")
            {
                str4 = str4 + "宽" + str2 + ",";
            }
            if (str3 != "")
            {
                str4 = str4 + "高" + str3 + ",";
            }
            return str4;
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


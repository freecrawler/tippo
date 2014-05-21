namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class desP : inputText
    {
        public Dictionary<string, string> AllPics = new Dictionary<string, string>();
        private Dictionary<string, string> dictionary_0 = new Dictionary<string, string>();
        private Dictionary<string, string> dictionary_1 = new Dictionary<string, string>();
        public bool isSetOK;
        public int maxHeight;
        public int maxWidth;
        public int minHeight;
        public int minWidth;
        public int size;
        private string[] string_0;

        public desP(fatherObj fatherObj_0)
        {
            base.key = "des";
            base.name = "详细描述";
            base.dataBaseFieldForSave = "f1";
            base.dataBaseFieldForList = "f1";
            base.dataBaseFieldForSearch = "f1";
            base.isMust = true;
            base.tipTextForSearch = "请输入包含的源代码。";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void batchEdit()
        {
            if ((bool) base.elem("add").getAttribute("checked", 0))
            {
                base.value = base.elem("qz").innerText + base.value + base.elem("hz").innerText;
            }
            else if ((bool) base.elem("ser").getAttribute("checked", 0))
            {
                string innerText = base.elem("lz").innerText;
                if (innerText != "")
                {
                    innerText = Regex.Escape(innerText);
                    base.value = func.正则替换(base.value, innerText, base.elem("xz").innerText);
                }
            }
            else if ((bool) base.elem("rep").getAttribute("checked", 0))
            {
                string str2 = base.elem("nz").innerText;
                if (str2 != "")
                {
                    base.value = str2;
                }
            }
        }

        public void copyPicToLocal()
        {
            foreach (KeyValuePair<string, string> pair in func.getNewDic(this.AllPics))
            {
                if (ERRypu.isDowmdes)
                {
                    if (!pair.Key.StartsWith(PzFuEX.Jo4304))
                    {
                        if (pair.Key.StartsWith("http"))
                        {
                            http http = new http();
                            http.url = pair.Key;
                            this.AllPics[pair.Key] = http.DownFile(PzFuEX.Jo4304 + @"cc\" + base.fatherObj.storeId.ToString() + @"\img");
                            http = null;
                        }
                        else
                        {
                            this.AllPics[pair.Key] = this.saveLocalPic(pair.Key);
                        }
                    }
                }
                else if (!pair.Key.StartsWith("http"))
                {
                    this.AllPics[pair.Key] = "";
                }
            }
        }

        public override void forceEdit()
        {
            base.value = base.value.Replace(PzFuEX.Jo4304, "$mysoftpath$");
            base.forceEdit();
            base.value = base.value.Replace("$mysoftpath$", PzFuEX.Jo4304);
            if (ERRypu.isDowmdes && (base.fatherObj.pics != null))
            {
                this.getPics();
                foreach (KeyValuePair<string, string> pair in func.getNewDic(this.AllPics))
                {
                    this.AllPics[pair.Key] = base.fatherObj.pics.forcePic(pair.Key);
                }
                this.replacePics(this.AllPics);
            }
            foreach (JArray array in (IEnumerable<JToken>) func.正则获取所有(base.value, "(<img [^>]*>)", ""))
            {
                if (!array[0].ToString().Contains(" src"))
                {
                    base.value = base.value.Replace(array[0].ToString(), "");
                }
            }
        }

        public override string getDataBaseFromValue()
        {
            if (base.fatherObj.state > 0)
            {
                this.getPics();
                this.copyPicToLocal();
                this.replacePics(this.AllPics);
            }
            base.value = func.正则替换(base.value, "<script[^>]*>.*?</script>", "");
            return func.Html标签过滤(base.value, "noscript,iframe,marquee");
        }

        public void getPics()
        {
            this.AllPics = new Dictionary<string, string>();
            this.dictionary_0 = func.转成Dic(func.正则获取所有(base.value, "(<img[^>]* src *= *['\"] *([^'\"]*) *['\"][^>]*>)", ""), 0, 1);
            foreach (KeyValuePair<string, string> pair in this.dictionary_0)
            {
                if (pair.Value == "")
                {
                    base.value = func.正则替换(base.value, pair.Key, "");
                }
                else
                {
                    string introduced4 = pair.Value.Trim();
                    this.AllPics[introduced4] = pair.Value.Trim();
                }
            }
            this.dictionary_1 = func.转成Dic(func.正则获取所有(base.value, "(<[^>]*background *= *['\"] *([^'\"]*) *['\"][^>]*>)", ""), 0, 1);
            foreach (KeyValuePair<string, string> pair2 in this.dictionary_1)
            {
                if (pair2.Value != "")
                {
                    string introduced5 = pair2.Value.Trim();
                    this.AllPics[introduced5] = pair2.Value.Trim();
                }
            }
        }

        public override void getTranslate()
        {
            string str = base.value;
            foreach (JArray array in (IEnumerable<JToken>) func.正则获取所有(base.value, "(<[^>]+>)", ""))
            {
                if (array[0].ToString() != "")
                {
                    str = str.Replace(array[0].ToString(), "\r");
                }
            }
            this.string_0 = str.Split(new char[] { '\r' });
            foreach (string str2 in this.string_0)
            {
                if (str2 != "")
                {
                    base.fatherObj.translateText[str2] = "";
                }
            }
        }

        public override object getValueFromBatchC()
        {
            return null;
        }

        public override void getValueFromEditC()
        {
            if (this.isSetOK)
            {
                base.value = (string) base.webbrowser.InvokeScript("getdes", null);
            }
        }

        public override object getValueFromSearchC()
        {
            return null;
        }

        public override void handleBatchUserChange(string string_1)
        {
            if (string_1.Contains("html"))
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary["html"] = base.elem(string_1.Replace("html", "")).innerText;
                new wb(func.getUrl(PzFuEX.Jo4304 + @"editor\window.html"), dictionary).ShowDialog();
                base.value = dictionary["html"];
                this.getPics();
                this.copyPicToLocal();
                this.replacePics(this.AllPics);
                base.elem(string_1.Replace("html", "")).innerText = base.value;
            }
        }

        public override void handleBatchValueChange(string string_1)
        {
        }

        public override void handleEditUserChange(string string_1)
        {
        }

        public override void handleSearchUserChange(string string_1)
        {
        }

        public override void handleSearchValueChange(string string_1)
        {
        }

        public void replacePics(Dictionary<string, string> dictionary_2)
        {
            foreach (KeyValuePair<string, string> pair in this.dictionary_0)
            {
                if (pair.Value != "")
                {
                    if (dictionary_2.ContainsKey(pair.Value) && (dictionary_2[pair.Value] != ""))
                    {
                        if (dictionary_2[pair.Value] != pair.Value)
                        {
                            string newValue = pair.Key.Replace(pair.Value, dictionary_2[pair.Value]);
                            base.value = base.value.Replace(pair.Key, newValue);
                        }
                    }
                    else
                    {
                        base.value = base.value.Replace(pair.Key, "");
                    }
                }
            }
            foreach (KeyValuePair<string, string> pair2 in this.dictionary_1)
            {
                if (pair2.Value != "")
                {
                    if (dictionary_2.ContainsKey(pair2.Value) && (dictionary_2[pair2.Value] != ""))
                    {
                        if (dictionary_2[pair2.Value] != pair2.Value)
                        {
                            string str2 = pair2.Key.Replace(pair2.Value, dictionary_2[pair2.Value]);
                            base.value = base.value.Replace(pair2.Key, str2);
                        }
                    }
                    else
                    {
                        base.value = base.value.Replace(pair2.Key, pair2.Key.Replace(pair2.Value, ""));
                    }
                }
            }
        }

        public string saveLocalPic(string string_1)
        {
            try
            {
                string destFileName = func.md5_hash(new StreamReader(string_1).BaseStream);
                if (destFileName != "")
                {
                    destFileName = func.获取图片完整路径(destFileName + Path.GetExtension(string_1), base.fatherObj.storeId.ToString());
                    File.Copy(string_1, destFileName, true);
                }
                return destFileName;
            }
            catch
            {
                return "";
            }
        }

        public override void setBatchControl()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div style='clear:both; padding:20px'><div style='width:150px; float:left'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "add' />首尾增加</div><div style='float:left;' >首部&nbsp;</div> ");
            builder.Append("<div style='float:left;' ><textarea rows='5' id='" + base.KeyId + "qz'></textarea><br/><input id='" + base.KeyId + "htmlqz' onclick='userchange(this.id);' type='button' value='使用Html编辑器' /></div><div style='float:left;' >&nbsp;尾部&nbsp;&nbsp;&nbsp;</div><div style='float:left;' ><textarea rows='5' id='" + base.KeyId + "hz'></textarea><br/><input id='" + base.KeyId + "htmlhz' onclick='userchange(this.id);' type='button' value='使用Html编辑器' /></div></div>");
            builder.Append("<div style='clear:both;padding:20px'><div style='width:150px; float:left'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "ser'/>查找并替换</div><div style='float:left'>将&nbsp;&nbsp;&nbsp;</div> <div style='float:left;' >");
            builder.Append("<textarea rows='5' id='" + base.KeyId + "lz'></textarea><br/><input style='display:none' type='button' value='查看源代码' /></div><div style='float:left;' >&nbsp;替换为&nbsp;</div><div style='float:left;' ><textarea rows='5' id='" + base.KeyId + "xz'></textarea><br/><input id='" + base.KeyId + "htmlxz' onclick='userchange(this.id);' type='button' value='使用Html编辑器' /></div></div></div>");
            builder.Append("<div style='clear:both;padding:20px'><div style='width:150px; float:left'><input name='" + base.KeyId + "sel' type='radio' value='' id='" + base.KeyId + "rep'/>全部更换为</div>");
            builder.Append("<div style='float:left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<textarea cols='55' rows='5' id='" + base.KeyId + "nz'></textarea><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id='" + base.KeyId + "htmlnz' onclick='userchange(this.id);' type='button' value='使用Html编辑器' /></div></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            base.webbrowser.execScript(true, "setdes(\"" + base.value.Replace(@"\", @"\\").Replace("\"", "\\\"") + "\")", "javascript");
        }

        public override void setSearchControl()
        {
            base.control.innerHTML = "<textarea cols=\"50\" rows=\"5\" id=\"" + base.KeyId + "v\" ></textarea>";
        }

        public override void setTranslate()
        {
            foreach (string str in this.string_0)
            {
                if (str != "")
                {
                    string str2 = base.fatherObj.translateText[str];
                    if (str2 != "")
                    {
                        int index = base.value.IndexOf(str);
                        base.value = base.value.Remove(index, str.Length);
                        base.value = base.value.Insert(index, str2);
                    }
                }
            }
            this.string_0 = null;
        }

        public override void setValueFromMap()
        {
            base.value = base.oldPro.des.value;
        }

        public override string verifyEdit()
        {
            return "";
        }
    }
}


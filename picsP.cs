namespace client
{
    using Microsoft.VisualBasic;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class picsP : baseP
    {
        public Dictionary<string, string> allBatchPics = new Dictionary<string, string>();
        private double double_0 = 50.0;
        private int int_0 = 1;
        public bool isWater;
        public int maxHeight;
        public int maxNum = 6;
        public int maxWidth;
        public int minHeight;
        public int minWidth;
        public string picType = "*.jpg|*.jpg|*.gif|*.gif";
        public int size;
        public pics value = new pics();

        public picsP(fatherObj fatherObj_0)
        {
            base.key = "pics";
            base.name = "图片";
            base.dataBaseFieldForSave = "f14";
            base.dataBaseFieldForList = "f15";
            base.dataBaseFieldForSearch = "f16";
            base.fatherObj = fatherObj_0;
            base.fatherObj.attrs.Add(base.key, this);
            base.webbrowser = base.fatherObj.webbrowser;
        }

        public override void forceEdit()
        {
            List<string> list = new List<string>();
            foreach (string str in this.value.allPic)
            {
                string item = this.forcePic(str);
                if (item != "")
                {
                    list.Add(item);
                }
            }
            this.value.allPic = list;
            while (this.value.allPic.Count > this.maxNum)
            {
                if (ERRypu.isDowmdes)
                {
                    base.fatherObj.des.value = base.fatherObj.des.value + "<img border='0' src='" + this.value.allPic[this.maxNum] + "' />";
                }
                this.value.allPic.RemoveAt(this.maxNum);
            }
        }

        public string forcePic(string string_0)
        {
            if (ERRypu.isDownPic)
            {
                try
                {
                    return handPicture.handle_img(string_0, this.size, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight, this.picType);
                }
                catch
                {
                    return "";
                }
            }
            if (string_0.StartsWith("http"))
            {
                return string_0;
            }
            return "";
        }

        public override string getDataBaseFromValue()
        {
            JObject obj2 = new JObject();
            obj2["allPic"] = func.getJArrayFromList(this.value.allPic);
            obj2["isAddWater"] = this.value.isAddWater;
            obj2["waterText"] = this.value.waterText;
            return JsonConvert.SerializeObject(obj2);
        }

        public override string getListCacheFromValue()
        {
            if (this.value.allPic.Count > 0)
            {
                return this.value.allPic[0];
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
            this.value.allPic = func.getListFromJArray((JArray) obj2["allPic"]);
            this.value.isAddWater = (bool) obj2["isAddWater"];
            this.value.waterText = obj2["waterText"].ToString();
        }

        public override void getValueFromEditC()
        {
        }

        public override object getValueFromSearchC()
        {
            return null;
        }

        public override void handleBatchUserChange(string string_0)
        {
            if (string_0.EndsWith("dc"))
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择存放的文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    if (!Directory.Exists(selectedPath + @"\导出的图片"))
                    {
                        Directory.CreateDirectory(selectedPath + @"\导出的图片");
                    }
                    foreach (KeyValuePair<string, string> pair in this.allBatchPics)
                    {
                        if ((pair.Value != "") && pair.Value.StartsWith(PzFuEX.Jo4304))
                        {
                            File.Copy(pair.Value, selectedPath + @"\导出的图片\" + Path.GetFileName(pair.Value), true);
                        }
                    }
                    func.Message("导出成功");
                }
            }
            else if (string_0.EndsWith("dr"))
            {
                FolderBrowserDialog dialog2 = new FolderBrowserDialog();
                dialog2.Description = "请选择图片文件夹";
                if (dialog2.ShowDialog() == DialogResult.OK)
                {
                    foreach (string str3 in Directory.GetFiles(dialog2.SelectedPath))
                    {
                        File.Copy(str3, func.获取图片完整路径(Path.GetFileName(str3), base.fatherObj.storeId.ToString()), true);
                    }
                    func.Message("导入成功");
                    this.setBatchControl();
                }
            }
            else if (string_0.EndsWith("d"))
            {
                string sourceFileName = string_0.TrimEnd(new char[] { 'd' });
                FolderBrowserDialog dialog3 = new FolderBrowserDialog();
                dialog3.Description = "请选择存放的文件夹";
                if (dialog3.ShowDialog() == DialogResult.OK)
                {
                    string str5 = dialog3.SelectedPath;
                    if (!Directory.Exists(str5 + @"\导出的图片"))
                    {
                        Directory.CreateDirectory(str5 + @"\导出的图片");
                    }
                    File.Copy(sourceFileName, str5 + @"\导出的图片\" + Path.GetFileName(sourceFileName), true);
                    func.Message("导出成功");
                }
            }
            else if (string_0.EndsWith("s"))
            {
                if (MessageBox.Show("确定要删除此图片吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    string str6 = string_0.TrimEnd(new char[] { 's' });
                    this.allBatchPics[str6] = "";
                    base.elem(str6 + "img").setAttribute("src", func.getUrl(PzFuEX.Jo4304 + @"cc\no.gif"), 0);
                }
            }
            else if (string_0.EndsWith("t"))
            {
                string str7 = string_0.TrimEnd(new char[] { 't' });
                OpenFileDialog dialog4 = new OpenFileDialog();
                dialog4.Filter = this.picType;
                dialog4.ShowDialog();
                string fileName = dialog4.FileName;
                dialog4.Dispose();
                if (fileName != "")
                {
                    string str9 = this.verifyPic(fileName);
                    if (str9 != "")
                    {
                        func.Message(str9);
                    }
                    else
                    {
                        fileName = this.saveLocalPic(fileName);
                        this.allBatchPics[str7] = fileName;
                        base.elem(str7 + "img").setAttribute("src", func.getUrl(fileName), 0);
                    }
                }
            }
            else if (string_0.EndsWith("cou"))
            {
                this.double_0 = text.toDouble(func.getAttrByElem(base.elem("cou"), "value"));
                this.setBatchControl();
            }
            else if (string_0.EndsWith("sy"))
            {
                this.int_0 = 1;
                this.setBatchControl();
            }
            else if (string_0.EndsWith("syy"))
            {
                this.int_0--;
                this.setBatchControl();
            }
            else if (string_0.EndsWith("xyy"))
            {
                this.int_0++;
                this.setBatchControl();
            }
            else if (string_0.EndsWith("my"))
            {
                this.int_0 = (int) Math.Ceiling((double) (((double) this.allBatchPics.Count) / this.double_0));
                this.setBatchControl();
            }
            else if (string_0.EndsWith("img"))
            {
                wb wb = new wb(func.getAttrByElem(base.elem(string_0), "src"), new Dictionary<string, string>());
                wb.isBigPic = true;
                wb.Size = new Size(800, 550);
                wb.WindowState = FormWindowState.Normal;
                wb.ShowDialog();
            }
        }

        public override void handleBatchValueChange(string string_0)
        {
        }

        public override void handleEditUserChange(string string_0)
        {
            if (string_0.EndsWith("da"))
            {
                if (MessageBox.Show("确定要全部删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.value.allPic = new List<string>();
                }
            }
            else if (string_0.EndsWith("a"))
            {
                if (!ERRypu.isDownPic)
                {
                    string str = Interaction.InputBox("", "请输入远程图片地址", "http://", 100, 100);
                    if ((str.Length > 0) && func.RemoteFileExists(str))
                    {
                        this.value.allPic.Add(str);
                    }
                }
                else
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = this.picType;
                    dialog.ShowDialog();
                    string fileName = dialog.FileName;
                    dialog.Dispose();
                    if (fileName == "")
                    {
                        return;
                    }
                    string str3 = this.verifyPic(fileName);
                    if (str3 != "")
                    {
                        func.Message(str3);
                        return;
                    }
                    fileName = this.saveLocalPic(fileName);
                    this.value.allPic.Add(fileName);
                }
            }
            else if (string_0.EndsWith("d"))
            {
                if (MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.value.allPic.RemoveAt(Convert.ToInt32(string_0.Replace("d", "")));
                }
            }
            else if (string_0.EndsWith("w"))
            {
                this.value.isAddWater = (bool) base.elem("w").getAttribute("checked", 0);
            }
            this.setEditControl();
        }

        public override void handleEditValueChange(string string_0)
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
            this.value = new pics();
        }

        public void replacePics(Dictionary<string, string> dictionary_0)
        {
            if (ERRypu.isDownPic)
            {
                List<string> list = new List<string>();
                foreach (string str in this.value.allPic)
                {
                    if (((str != "") && dictionary_0.ContainsKey(str)) && (dictionary_0[str] != ""))
                    {
                        list.Add(dictionary_0[str]);
                    }
                }
                this.value.allPic = list;
            }
            else
            {
                List<string> list2 = new List<string>();
                foreach (string str2 in this.value.allPic)
                {
                    if (((str2 != "") && dictionary_0.ContainsKey(str2)) && dictionary_0[str2].StartsWith("http"))
                    {
                        list2.Add(dictionary_0[str2]);
                    }
                }
                this.value.allPic = list2;
            }
        }

        public string saveLocalPic(string string_0)
        {
            try
            {
                string destFileName = func.md5_hash(new StreamReader(string_0).BaseStream);
                if (destFileName != "")
                {
                    destFileName = func.获取图片完整路径(destFileName + Path.GetExtension(string_0), base.fatherObj.storeId.ToString());
                    File.Copy(string_0, destFileName, true);
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
            int num = 0;
            if (this.double_0 == 0.0)
            {
                this.double_0 = this.allBatchPics.Count;
            }
            int num2 = (int) Math.Ceiling((double) (((double) this.allBatchPics.Count) / this.double_0));
            if (this.int_0 < 1)
            {
                this.int_0 = 1;
            }
            else if (this.int_0 > num2)
            {
                this.int_0 = num2;
            }
            foreach (KeyValuePair<string, string> pair in this.allBatchPics)
            {
                if (pair.Value != "")
                {
                    num++;
                    if ((num > (this.double_0 * (this.int_0 - 1))) && (num <= (this.double_0 * this.int_0)))
                    {
                        string str = base.KeyId + pair.Key;
                        builder.Append("<div class='batchpic'><img onclick=\"userchange(this.id);\" id='" + str + "img'  height='140px' width='140px' border='0' src='" + func.getUrl(pair.Value) + "' /><br /><a id='" + str + "d' href='#' onclick='userchange(this.id);return false;'>导出</a>&nbsp;<a id='" + str + "s' href='#' onclick='userchange(this.id);return false;'>删除</a>&nbsp;<a id='" + str + "t' href='#' onclick='userchange(this.id);return false;'>替换</a></div>");
                    }
                }
            }
            builder.Append(string.Concat(new object[] { "</div><div style='text-align:center; padding:15px; clear:both'>第", this.int_0, "页&nbsp;&nbsp;共", num2, "页&nbsp;&nbsp;共", this.allBatchPics.Count, "张图片&nbsp;&nbsp;每页显示&nbsp;<select id='", base.KeyId, "cou' onchange='userchange(this.id);'>" }));
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (this.double_0 == 50.0)
            {
                str2 = "selected='selected'";
            }
            else if (this.double_0 == 200.0)
            {
                str3 = "selected='selected'";
            }
            else
            {
                str4 = "selected='selected'";
            }
            builder.Append("<option value='50' " + str2 + ">50</option><option value='200' " + str3 + ">200</option><option value='0' " + str4 + ">全部</option>");
            builder.Append("</select>&nbsp;张图片&nbsp;&nbsp;<a id='" + base.KeyId + "sy' href='#' onclick='userchange(this.id);return false;' >首页</a>&nbsp;&nbsp;<a id='" + base.KeyId + "syy' href='#' onclick='userchange(this.id);return false;'>上一页</a>&nbsp;&nbsp;<a id='" + base.KeyId + "xyy' href='#' onclick='userchange(this.id);return false;'>下一页</a>&nbsp;&nbsp;<a id='" + base.KeyId + "my' href='#' onclick='userchange(this.id);return false;'>末页</a></div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setEditControl()
        {
            StringBuilder builder = new StringBuilder();
            string str = "display:inline;";
            if (this.value.allPic.Count >= this.maxNum)
            {
                str = "display:none;";
            }
            builder.Append("<div style='padding:5px;'><input style='" + str + "' id='" + base.KeyId + "a' type='button' value=' 添加图片 ' onclick='userchange(this.id);' />&nbsp;&nbsp;&nbsp;&nbsp;<input id='" + base.KeyId + "da' type='button' value='删除全部图片'  onclick='userchange(this.id);' /></div><div>");
            int num = 0;
            foreach (string str2 in this.value.allPic)
            {
                builder.Append(string.Concat(new object[] { "<div style='float:left;padding:5px; width:126px; text-align:center'><img src='", func.getUrl(str2), "' height='120' width='120' border='0' /><a id='", base.KeyId, num, "d' href='#' onclick='userchange(this.id);return false;'>删除</a></div>" }));
                num++;
            }
            string str3 = "";
            if (this.value.isAddWater)
            {
                str3 = "checked='checked'";
            }
            string str4 = "display:none;";
            if (this.isWater)
            {
                str4 = "display:block;";
            }
            builder.Append("</div><div style='padding:5px; clear:both;" + str4 + "'><input id='" + base.KeyId + "w' type='checkbox' " + str3 + " onclick='userchange(this.id);' />给图片添加水印</div>");
            base.control.innerHTML = builder.ToString();
        }

        public override void setSearchControl()
        {
        }

        public override void setValueFromMap()
        {
            this.value.allPic = base.oldPro.pics.value.allPic;
        }

        public override string verifyBatch(object object_0)
        {
            return "";
        }

        public override string verifyEdit()
        {
            string str = "";
            if (this.value.allPic.Count > this.maxNum)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "图片数量不能大于", this.maxNum, "张<br/>" });
            }
            int num = 1;
            foreach (string str2 in this.value.allPic)
            {
                string str3 = this.verifyPic(str2);
                if (str3 != "")
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, "第", num, "张图片", str3 });
                }
                num++;
            }
            return str;
        }

        public string verifyPic(string string_0)
        {
            if (ERRypu.isDownPic)
            {
                return handPicture.check_img(string_0, this.size, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight, this.picType);
            }
            if (string_0.StartsWith("http"))
            {
                return "";
            }
            return "不能使用本地图片";
        }

        public override string verifySearch(object object_0)
        {
            return "";
        }
    }
}


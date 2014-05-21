namespace client
{
    using csExWB;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Text;

    public class product : fatherObj
    {
        public string upLoadUrl = "";

        public product(cEXWB cEXWB_0)
        {
            base.webbrowser = cEXWB_0;
            base.defaultLanguage = "zh-cn";
            base.editType = "edit";
        }

        public void copyPicToLocal()
        {
            base.getDownAllPics();
            foreach (KeyValuePair<string, string> pair in func.getNewDic(base.allPics))
            {
                if (!pair.Key.StartsWith(PzFuEX.Jo4304 + @"cc\" + this.storeId.ToString() + @"\"))
                {
                    if (pair.Key.StartsWith("http"))
                    {
                        http http = new http();
                        http.url = pair.Key;
                        base.allPics[pair.Key] = http.DownFile(PzFuEX.Jo4304 + @"cc\" + this.storeId.ToString() + @"\img");
                        http = null;
                    }
                    else
                    {
                        base.allPics[pair.Key] = base.des.saveLocalPic(pair.Key);
                    }
                }
                else
                {
                    base.allPics[pair.Key] = pair.Key;
                }
            }
            this.returnAllPics();
        }

        public virtual bool downHtml(Dictionary<string, string> dictionary_0)
        {
            return true;
        }

        public void downPic(ref int int_0)
        {
            client.downPic pic = new client.downPic();
            pic.refer = base.url;
            pic.allPic = base.allPics;
            pic.savePath = PzFuEX.Jo4304 + @"cc\" + this.storeId.ToString() + @"\img";
            pic.startDownPic(ref int_0);
            pic = null;
        }

        public virtual postData getPicPostData(string string_0)
        {
            return new postData(ContentType.dataFrom, base.encode);
        }

        public int getTotalPic()
        {
            return base.allPics.Count;
        }

        public void getUpAllPics(bool bool_0, string string_0, Font font_0, Color color_0)
        {
            base.allUpPics = new Dictionary<string, postData>();
            if (ERRypu.isDownPic)
            {
                foreach (string str in base.pics.value.allPic)
                {
                    if (str != "")
                    {
                        string str2 = str;
                        if (bool_0)
                        {
                            str2 = handPicture.addWater(str2, string_0, font_0, color_0, base.Store.waterIsXD, base.Store.waterX, base.Store.waterY, base.Store.waterPos);
                        }
                        base.allUpPics[str] = this.getPicPostData(str2);
                    }
                }
            }
            if (ERRypu.isDownImg && base.attrs.ContainsKey("saleAttr"))
            {
                foreach (option option in base.saleAttr.value)
                {
                    foreach (optionValue value2 in option.optionValues)
                    {
                        if (value2.pic != "")
                        {
                            string pic = value2.pic;
                            if (bool_0)
                            {
                                pic = handPicture.addWater(pic, string_0, font_0, color_0, base.Store.waterIsXD, base.Store.waterX, base.Store.waterY, base.Store.waterPos);
                            }
                            base.allUpPics[value2.pic] = this.getPicPostData(pic);
                        }
                    }
                }
            }
            if (ERRypu.isDowmdes)
            {
                base.des.getPics();
                foreach (KeyValuePair<string, string> pair in base.des.AllPics)
                {
                    string key = pair.Key;
                    if (bool_0)
                    {
                        key = handPicture.addWater(key, string_0, font_0, color_0, base.Store.waterIsXD, base.Store.waterX, base.Store.waterY, base.Store.waterPos);
                    }
                    base.allUpPics[pair.Key] = this.getPicPostData(key);
                }
            }
        }

        public virtual string post()
        {
            return "";
        }

        public virtual bool prePost()
        {
            return true;
        }

        public void returnAllPics()
        {
            if (ERRypu.isDownImg && base.attrs.ContainsKey("saleAttr"))
            {
                base.saleAttr.replacePics(base.allPics);
            }
            if (ERRypu.isDownPic)
            {
                base.pics.replacePics(base.allPics);
            }
            if (ERRypu.isDowmdes)
            {
                base.des.replacePics(base.allPics);
            }
        }

        public void translatePro()
        {
            if (base.defaultLanguage != base.toLanguage)
            {
                foreach (KeyValuePair<string, baseP> pair in base.attrs)
                {
                    pair.Value.getTranslate();
                }
                StringBuilder builder = new StringBuilder();
                int num = 0;
                foreach (KeyValuePair<string, string> pair2 in base.translateText)
                {
                    if (pair2.Key.Trim() != "")
                    {
                        builder.Append(pair2.Key.Trim() + "\r\n");
                        num++;
                    }
                }
                int num2 = 3;
                if (ERRypu.u2szIG == "13766")
                {
                    num2 = 6;
                }
                int num3 = 0;
                while (true)
                {
                    string[] strArray = translate.trans(builder.ToString(), base.defaultLanguage, base.toLanguage).Replace("\n", "").Split(new char[] { '\r' });
                    if (strArray.Length != num)
                    {
                        num3++;
                    }
                    else
                    {
                        int index = 0;
                        foreach (KeyValuePair<string, string> pair3 in func.getNewDic(base.translateText))
                        {
                            if (pair3.Key.Trim() != "")
                            {
                                base.translateText[pair3.Key] = strArray[index].Trim();
                                index++;
                            }
                        }
                        num3 = num2;
                    }
                    if (num3 >= num2)
                    {
                        base.translateText[""] = "";
                        foreach (KeyValuePair<string, baseP> pair4 in base.attrs)
                        {
                            pair4.Value.setTranslate();
                        }
                        return;
                    }
                }
            }
        }

        public void updateUp(string string_0, string string_1)
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", base.storeId, @"\pro.jpg" });
            string str = "";
            if (string_0 != "")
            {
                str = string.Concat(new object[] { "UPDATE product SET state=2,err='',pid='", string_0.Replace("'", "''"), "' where rowid=", base.id });
            }
            else
            {
                str = string.Concat(new object[] { "UPDATE product SET state=3,err='", string_1.Replace("'", "''"), "' where rowid=", base.id });
            }
            SQLiteHelper.ExecuteNonQuery(str, CommandType.Text);
        }

        public void upPic(ref int int_0)
        {
            if (base.Http != null)
            {
                uploadPic pic = new uploadPic();
                pic.ver = base.httpVer;
                pic.refer = base.Http.url;
                pic.allPic = base.allUpPics;
                pic.upLoadUrl = this.upLoadUrl;
                pic.cookies = base.Http.Cookies;
                pic.startUpPic(ref int_0);
                base.allPicHtml = pic.allPicHtml;
                pic = null;
                base.allUpPics = null;
            }
        }
    }
}


namespace client
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Threading;

    public class upPro
    {
        public int errNum;
        public int i;
        public bool isOk;
        public bool isReUp;
        public bool isWater;
        public string keyErr = "";
        public int kipNum;
        public list li;
        public int p;
        public product pro;
        public store sto;
        public int succNum;
        public int totalPic;
        public Color waterColor = Color.White;
        public Font waterFont = new Font("Arial Black", 20f, FontStyle.Bold);
        public bool waterIsCenter = true;
        public bool waterIsDown = true;
        public string waterStr = "";

        private bool method_0(int int_0)
        {
            SQLiteHelper.Conn = string.Concat(new object[] { "Data Source=", PzFuEX.Jo4304, @"cc\", this.sto.storeId, @"\pro.jpg" });
            DataSet set = SQLiteHelper.ExecuteDataSet("select state from product where rowid =" + int_0, CommandType.Text);
            return (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && (set.Tables[0].Rows[0][0].ToString() == "2"));
        }

        public void startUp()
        {
            bool flag;
            if (!(flag = ERRypu.Jm1ydu(this.sto.userName, this.sto.siteId)) && !ERRypu.mTPtNa())
            {
                this.keyErr = "试用会员每次只能上传5个产品，累计只能上传20个产品，请登录官网购买";
                this.isOk = true;
                return;
            }
            if (!flag && (ERRypu._81ESCx > 50L))
            {
                this.keyErr = "遇到问题，请重启软件。";
                this.isOk = true;
                return;
            }
            if (this.sto.isCSV)
            {
                if (this.sto.csvHeader != "")
                {
                    this.pro.csv.addString(this.sto.csvHeader);
                }
                if ((this.sto.csvImgFloder != "?") && !Directory.Exists(this.pro.csv.path + this.sto.csvImgFloder))
                {
                    Directory.CreateDirectory(this.pro.csv.path + this.sto.csvImgFloder);
                }
            }
            else if (this.sto.isXls)
            {
                if (this.sto.xlsHeader.Count > 0)
                {
                    this.pro.xls.indxe++;
                    if (this.sto.isFast)
                    {
                        this.pro.xls.showTitleFast(this.sto.xlsHeader);
                    }
                    else
                    {
                        this.pro.xls.showTitle(this.sto.xlsHeader);
                    }
                }
                if ((this.sto.isExtPic && !string.IsNullOrEmpty(this.sto.xlsImgFloder)) && !Directory.Exists(this.pro.xls.path + this.sto.xlsImgFloder))
                {
                    Directory.CreateDirectory(this.pro.xls.path + this.sto.xlsImgFloder);
                }
            }
            this.pro.pubUpDic = new Dictionary<string, string>();
            using (List<int>.Enumerator enumerator = this.li.ids.GetEnumerator())
            {
                StringBuilder builder;
                string str5;
            Label_0219:
                if (!enumerator.MoveNext())
                {
                    goto Label_09B6;
                }
                int current = enumerator.Current;
                if (!this.isReUp && this.method_0(current))
                {
                    this.kipNum++;
                    goto Label_0219;
                }
                this.p++;
                this.pro.postErr = new Dictionary<string, bool>();
                this.pro.allPicHtml = new Dictionary<string, string>();
                this.pro.id = current;
                this.pro.loadFromDataBase();
                if (!this.pro.prePost())
                {
                    goto Label_0900;
                }
                foreach (KeyValuePair<string, baseP> pair2 in this.pro.attrs)
                {
                    pair2.Value.getBaseDataFromDataBase();
                }
                foreach (KeyValuePair<string, baseP> pair3 in this.pro.attrs)
                {
                    pair3.Value.forceEdit();
                }
                this.pro.saveToDataBase();
                this.i = 0;
                if (this.sto.isCSV)
                {
                    if (this.sto.csvImgFloder != "?")
                    {
                        this.pro.getDownAllPics();
                        this.totalPic = this.pro.allPics.Count;
                        string str = this.pro.csv.path + this.sto.csvImgFloder;
                        foreach (KeyValuePair<string, string> pair4 in this.pro.allPics)
                        {
                            string key = pair4.Key;
                            if (this.isWater)
                            {
                                key = handPicture.addWater(key, this.waterStr, this.waterFont, this.waterColor, this.sto.waterIsXD, this.sto.waterX, this.sto.waterY, this.sto.waterPos);
                            }
                            if (this.sto.csvPicExt != "")
                            {
                                File.Copy(key, str + @"\" + Path.GetFileNameWithoutExtension(pair4.Key) + this.sto.csvPicExt, true);
                            }
                            else
                            {
                                File.Copy(key, str + @"\" + Path.GetFileName(pair4.Key), true);
                            }
                            this.i++;
                        }
                    }
                }
                else if (this.sto.isXls && this.sto.isExtPic)
                {
                    this.pro.getDownAllPics();
                    this.totalPic = this.pro.allPics.Count;
                    string str3 = this.pro.xls.path + this.sto.xlsImgFloder;
                    foreach (KeyValuePair<string, string> pair5 in this.pro.allPics)
                    {
                        string str4 = pair5.Key;
                        if (this.isWater)
                        {
                            str4 = handPicture.addWater(str4, this.waterStr, this.waterFont, this.waterColor, this.sto.waterIsXD, this.sto.waterX, this.sto.waterY, this.sto.waterPos);
                        }
                        if (!string.IsNullOrEmpty(this.sto.xlsPicExt))
                        {
                            File.Copy(str4, str3 + @"\" + Path.GetFileNameWithoutExtension(pair5.Key) + this.sto.csvPicExt, true);
                        }
                        else
                        {
                            File.Copy(str4, str3 + @"\" + Path.GetFileName(pair5.Key), true);
                        }
                        this.i++;
                    }
                }
                else if (this.sto.isLogin)
                {
                    if (this.sto.siteId == "23")
                    {
                        this.pro.isWater = this.isWater;
                        this.pro.waterStr = this.waterStr;
                        this.pro.waterIsCenter = this.waterIsCenter;
                        this.pro.waterIsDown = this.waterIsDown;
                        this.pro.getUpAllPics(false, this.waterStr, this.waterFont, this.waterColor);
                    }
                    else
                    {
                        this.pro.getUpAllPics(this.isWater, this.waterStr, this.waterFont, this.waterColor);
                    }
                    this.totalPic = this.pro.allUpPics.Count;
                    this.pro.upPic(ref this.i);
                }
                goto Label_08F4;
            Label_06F4:
                try
                {
                    str5 = this.pro.post();
                }
                catch
                {
                }
                foreach (KeyValuePair<string, baseP> pair6 in this.pro.attrs)
                {
                    pair6.Value.initValue();
                }
                if (str5 == "")
                {
                    this.errNum++;
                    if (this.pro.pubUpDic.ContainsKey("err"))
                    {
                        ERRypu.submitQuestion(this.pro.url, this.sto.siteId, this.pro.pubUpDic["err"], this.pro.htmlCode);
                    }
                }
                else
                {
                    this.succNum++;
                }
                StringBuilder builder2 = new StringBuilder();
                bool flag2 = true;
                foreach (KeyValuePair<string, bool> pair7 in this.pro.postErr)
                {
                    builder2.Append(pair7.Key + ",");
                    if (pair7.Value)
                    {
                        flag2 = false;
                        this.keyErr = pair7.Key;
                    }
                }
                this.pro.updateUp(str5, builder2.ToString().Trim(new char[] { ',' }));
                if (!flag2)
                {
                    goto Label_09B6;
                }
                if (!(this.sto.siteId == "23"))
                {
                }
                if (((str5 != "") && !flag) && !ERRypu.n5kEDK(""))
                {
                    goto Label_0981;
                }
                if (!flag && (ERRypu._81ESCx > 50L))
                {
                    goto Label_098E;
                }
                if ((!flag && ERRypu.DDGXY0) && (this.succNum >= 5))
                {
                    goto Label_099B;
                }
                if (this.sto.intervalTime > 0)
                {
                    Thread.Sleep((int) (this.sto.intervalTime * 0x3e8));
                }
                goto Label_0219;
            Label_08F4:
                str5 = "";
                goto Label_06F4;
            Label_0900:
                builder = new StringBuilder();
                foreach (KeyValuePair<string, bool> pair in this.pro.postErr)
                {
                    builder.Append(pair.Key + ",");
                }
                this.keyErr = builder.ToString().TrimEnd(new char[] { ',' });
                this.isOk = true;
                return;
            Label_0981:
                this.keyErr = "试用会员每次只能上传5个产品，累计只能上传20个产品，请登录官网购买";
                goto Label_09B6;
            Label_098E:
                this.keyErr = "遇到问题，请重启软件。";
                goto Label_09B6;
            Label_099B:
                this.keyErr = "试用会员每次只能上传5个产品，累计只能上传20个产品，请登录官网购买";
            }
        Label_09B6:
            if (!(this.sto.siteId == "23"))
            {
            }
            if (!flag)
            {
                ERRypu._9FwJVg("");
            }
            if (this.sto.isCSV)
            {
                this.pro.csv.Save();
            }
            else if (this.sto.isXls)
            {
                if (this.sto.isFreezePanes)
                {
                    this.pro.xls.FreezePanes();
                }
                this.pro.xls.SaveFile();
                this.pro.xls.Dispose();
            }
            this.isOk = true;
        }
    }
}


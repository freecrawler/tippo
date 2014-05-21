namespace client
{
    using csExWB;
    using My.Json;
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;

    public class downCSV
    {
        public string defaultLanguage = "en";
        public int i;
        public int ii;
        public Dictionary<string, string> inputUrls = new Dictionary<string, string>();
        public bool isOk;
        public bool isReDown;
        public int j = 1;
        public int si;
        public store sto;
        public int storeID;
        public string toLanguage = "";
        public int totallPic;
        public DataTable urls = new DataTable();
        public cEXWB webbrowser;

        public void getProByJoRow(product product_0, DataRow dataRow_0, JObject jobject_0, List<textID> list_0)
        {
            if (product_0.price.value.Count == 0)
            {
                product_0.price.value.Add(new sku());
            }
            foreach (KeyValuePair<string, JToken> pair in jobject_0)
            {
                string str;
                string str4;
                string str6;
                string str7;
                string str8;
                string str9;
                string str11;
                int num;
                string str12;
                string[] strArray15;
                int num3;
                string[] strArray16;
                int num4;
                string[] strArray18;
                int num6;
                string[] strArray19;
                int num7;
                string[] strArray20;
                int num8;
                string[] strArray21;
                if (pair.Value.ToString().Length > 0)
                {
                    switch (pair.Key)
                    {
                        case "cate":
                            strArray15 = dataRow_0[pair.Value.ToString()].ToString().Split(new char[] { '\\' });
                            num3 = 0;
                            goto Label_02F1;

                        case "name":
                            product_0.name.value = dataRow_0[pair.Value.ToString()].ToString();
                            break;

                        case "attr":
                            if (!pair.Value.ToString().Contains(","))
                            {
                                break;
                            }
                            strArray16 = pair.Value.ToString().Split(new char[] { ',' });
                            num4 = 0;
                            goto Label_0404;

                        case "keywords":
                            strArray18 = pair.Value.ToString().Split(new char[] { ',' });
                            num6 = 0;
                            goto Label_046A;

                        case "pic":
                            strArray19 = pair.Value.ToString().Split(new char[] { ',' });
                            num7 = 0;
                            goto Label_04D2;

                        case "summary":
                            strArray20 = pair.Value.ToString().Split(new char[] { ',' });
                            num8 = 0;
                            goto Label_0572;

                        case "des":
                            strArray21 = pair.Value.ToString().Split(new char[] { ',' });
                            num3 = 0;
                            goto Label_0667;

                        case "unit":
                            goto Label_0677;

                        case "method":
                            goto Label_06E0;

                        case "weight":
                            product_0.weight.value.weigh = text.toDouble(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "weightis":
                            if (!(dataRow_0[pair.Value.ToString()].ToString() == "1"))
                            {
                                goto Label_07AB;
                            }
                            product_0.weight.value.isCustom = true;
                            break;

                        case "weightnum":
                            product_0.weight.value.itemNum = text.toInt(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "weightaddnum":
                            product_0.weight.value.addNum = text.toInt(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "weightaddwei":
                            product_0.weight.value.addWeight = text.toDouble(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "len":
                            product_0.package.value.length = text.toDouble(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "wid":
                            product_0.package.value.width = text.toDouble(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "hei":
                            product_0.package.value.height = text.toDouble(dataRow_0[pair.Value.ToString()].ToString());
                            break;

                        case "group":
                            product_0.group.value.text[product_0.defaultLanguage] = dataRow_0[pair.Value.ToString()].ToString();
                            break;

                        case "days":
                            goto Label_091E;

                        case "price1":
                            if (dataRow_0[pair.Value.ToString()].ToString().Length > 0)
                            {
                                string[] strArray10 = dataRow_0[pair.Value.ToString()].ToString().Split(new char[] { ',' });
                                intervalPrice price = new intervalPrice();
                                price.min = text.toInt(strArray10[0]);
                                price.max = text.toInt(strArray10[1]);
                                price.price = text.toDouble(strArray10[2]);
                                price.day = text.toInt(strArray10[3]);
                                product_0.price.value[0].value.Add(price);
                            }
                            break;

                        case "price2":
                            if (dataRow_0[pair.Value.ToString()].ToString().Length > 0)
                            {
                                string[] strArray11 = dataRow_0[pair.Value.ToString()].ToString().Split(new char[] { ',' });
                                intervalPrice price2 = new intervalPrice();
                                price2.min = text.toInt(strArray11[0]);
                                price2.max = text.toInt(strArray11[1]);
                                price2.price = text.toDouble(strArray11[2]);
                                price2.day = text.toInt(strArray11[3]);
                                product_0.price.value[0].value.Add(price2);
                            }
                            break;

                        case "price3":
                            if (dataRow_0[pair.Value.ToString()].ToString().Length > 0)
                            {
                                string[] strArray12 = dataRow_0[pair.Value.ToString()].ToString().Split(new char[] { ',' });
                                intervalPrice price3 = new intervalPrice();
                                price3.min = text.toInt(strArray12[0]);
                                price3.max = text.toInt(strArray12[1]);
                                price3.price = text.toDouble(strArray12[2]);
                                price3.day = text.toInt(strArray12[3]);
                                product_0.price.value[0].value.Add(price3);
                            }
                            break;

                        case "price4":
                            if (dataRow_0[pair.Value.ToString()].ToString().Length > 0)
                            {
                                string[] strArray13 = dataRow_0[pair.Value.ToString()].ToString().Split(new char[] { ',' });
                                intervalPrice price4 = new intervalPrice();
                                price4.min = text.toInt(strArray13[0]);
                                price4.max = text.toInt(strArray13[1]);
                                price4.price = text.toDouble(strArray13[2]);
                                price4.day = text.toInt(strArray13[3]);
                                product_0.price.value[0].value.Add(price4);
                            }
                            break;

                        case "price5":
                            if (dataRow_0[pair.Value.ToString()].ToString().Length > 0)
                            {
                                string[] strArray14 = dataRow_0[pair.Value.ToString()].ToString().Split(new char[] { ',' });
                                intervalPrice price5 = new intervalPrice();
                                price5.min = text.toInt(strArray14[0]);
                                price5.max = text.toInt(strArray14[1]);
                                price5.price = text.toDouble(strArray14[2]);
                                price5.day = text.toInt(strArray14[3]);
                                product_0.price.value[0].value.Add(price5);
                            }
                            break;
                    }
                }
                continue;
            Label_0265:
                str = strArray15[num3];
                string[] strArray2 = str.Split(new char[] { ':' });
                string str2 = "";
                string str3 = "";
                if (strArray2.Length > 1)
                {
                    str2 = strArray2[0];
                    str3 = strArray2[1];
                }
                else
                {
                    str3 = str;
                }
                textID tid = new textID();
                tid.id = str2;
                tid.text[product_0.defaultLanguage] = str3;
                cate item = new cate();
                item.value = tid;
                product_0.cate.value.Add(item);
                num3++;
            Label_02F1:
                if (num3 < strArray15.Length)
                {
                    goto Label_0265;
                }
                continue;
            Label_0372:
                str4 = strArray16[num4];
                foreach (string str5 in dataRow_0[str4].ToString().Split(new char[] { ';' }))
                {
                    string[] strArray5 = str5.Split(new char[] { ':' });
                    if (strArray5.Length > 1)
                    {
                        product_0.cateAttr.value.Add(downfunc.getAttr(strArray5[0], strArray5[1], product_0.defaultLanguage));
                    }
                }
                num4++;
            Label_0404:
                if (num4 < strArray16.Length)
                {
                    goto Label_0372;
                }
                continue;
            Label_0440:
                str6 = strArray18[num6];
                product_0.keywords.value.Add(dataRow_0[str6].ToString());
                num6++;
            Label_046A:
                if (num6 < strArray18.Length)
                {
                    goto Label_0440;
                }
                continue;
            Label_04A3:
                str7 = strArray19[num7];
                product_0.pics.value.allPic.Add(dataRow_0[str7].ToString());
                num7++;
            Label_04D2:
                if (num7 < strArray19.Length)
                {
                    goto Label_04A3;
                }
                continue;
            Label_050B:
                str8 = strArray20[num8];
                if (product_0.summary.value.Length <= 0)
                {
                    product_0.summary.value = dataRow_0[str8].ToString();
                }
                else
                {
                    product_0.summary.value = product_0.summary.value + "\r\n" + dataRow_0[str8].ToString();
                }
                num8++;
            Label_0572:
                if (num8 < strArray20.Length)
                {
                    goto Label_050B;
                }
                continue;
            Label_05AE:
                str9 = strArray21[num3];
                string str10 = dataRow_0[str9].ToString();
                if ((str10.EndsWith(".jpg") || str10.EndsWith(".png")) || ((str10.EndsWith(".bmp") || str10.EndsWith(".jpeg")) || str10.EndsWith(".gif")))
                {
                    str10 = "<img border='0' src='" + str10 + "' />";
                }
                if (product_0.des.value.Length <= 0)
                {
                    product_0.des.value = str10;
                }
                else
                {
                    product_0.des.value = product_0.des.value + "\r\n" + str10;
                }
                num3++;
            Label_0667:
                if (num3 < strArray21.Length)
                {
                    goto Label_05AE;
                }
                continue;
            Label_0677:
                str11 = dataRow_0[pair.Value.ToString()].ToString();
                using (List<textID>.Enumerator enumerator2 = list_0.GetEnumerator())
                {
                    textID current;
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (current.id == str11)
                        {
                            goto Label_06C0;
                        }
                    }
                    continue;
                Label_06C0:
                    product_0.unit.value = current;
                    continue;
                }
            Label_06E0:
                num = text.toInt(dataRow_0[pair.Value.ToString()].ToString());
                if (num > 1)
                {
                    product_0.saleMethod.value.isLot = true;
                    product_0.saleMethod.value.lotNum = num;
                }
                else
                {
                    product_0.saleMethod.value.isLot = false;
                }
                continue;
            Label_07AB:
                product_0.weight.value.isCustom = false;
                continue;
            Label_091E:
                str12 = dataRow_0[pair.Value.ToString()].ToString();
                if (!str12.Contains("day"))
                {
                    str12 = str12 + " day";
                }
                product_0.validityDay.value.text[product_0.defaultLanguage] = str12;
            }
        }

        public void start()
        {
            this.i = 0;
            this.j = 2;
            this.si = 0;
            product product = null;
            Assembly assembly = PzFuEX._8lqO1O("99");
            JObject obj2 = new JObject();
            List<textID> list = new List<textID>();
            if ((ERRypu.u2szIG == "13352") || (ERRypu.u2szIG == "7"))
            {
                string json = func.Dept(func.readTxt(PzFuEX.Jo4304 + "incsv.txt"), "20130528").TrimEnd(new char[0]);
                try
                {
                    obj2 = JObject.Parse(json);
                }
                catch
                {
                    func.Message("读取配置失败");
                    this.isOk = true;
                    return;
                }
                list = func.getTxtByBaseData(false, "23", "unit");
            }
            foreach (DataRow row in this.urls.Rows)
            {
                this.i++;
                downSite site = (downSite) Activator.CreateInstance(assembly.GetType("down99.downSite"));
                product = (product) Activator.CreateInstance(assembly.GetType("down99.product"), new object[] { this.webbrowser });
                product.Http = new http();
                product.Http.isDown = true;
                product.Http.Encode = site.encode;
                product.storeId = this.storeID;
                product.currencyUnit = site.currencyUnit;
                product.toLanguage = this.toLanguage;
                product.siteId = site.siteId;
                product.defaultLanguage = this.defaultLanguage;
                product.url = "";
                try
                {
                    if ((!(ERRypu.u2szIG == "13156") && !(ERRypu.u2szIG == "7")) && !(ERRypu.u2szIG == "10092"))
                    {
                        this.getProByJoRow(product, row, obj2, list);
                    }
                    else
                    {
                        product.Http.url = "";
                        JArray array = new JArray();
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            array.Add(row[i].ToString());
                        }
                        product.htmlCode = JsonConvert.SerializeObject(array);
                        product.listHtmlCode = "";
                        product.url = "";
                        product.urlInfo = "";
                        product.id = 0;
                        if (!product.downHtml(new Dictionary<string, string>()))
                        {
                            continue;
                        }
                    }
                }
                catch
                {
                    continue;
                }
                product.pubHtml = new Dictionary<string, string>();
                product.translatePro();
                this.ii = 0;
                product.getDownAllPics();
                this.totallPic = product.getTotalPic();
                product.downPic(ref this.ii);
                product.returnAllPics();
                product.state = 0;
                product.fromSiteId = "down" + product.siteId;
                product.saveToDataBase();
                foreach (KeyValuePair<string, baseP> pair in product.attrs)
                {
                    pair.Value.initValue();
                }
            }
            this.isOk = true;
        }
    }
}


namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class sku
    {
        public bool isWholesale;
        public int minOrderNum;
        public Dictionary<string, string> name = new Dictionary<string, string>();
        public int off;
        public int proNum = 0x270f;
        public string skuCode = "";
        public List<intervalPrice> value = new List<intervalPrice>();

        public static sku getValueFromJObject(JObject jobject_0)
        {
            sku sku = new sku();
            sku.name = func.getDicFromJObject((JObject) jobject_0["name"]);
            foreach (JToken token in (IEnumerable<JToken>) ((JArray) jobject_0["value"]))
            {
                sku.value.Add(intervalPrice.getValueFromJObject((JObject) token));
            }
            sku.skuCode = jobject_0["skuCode"].ToString();
            sku.proNum = (int) jobject_0["proNum"];
            sku.isWholesale = (bool) jobject_0["isWholesale"];
            sku.off = (int) jobject_0["off"];
            sku.minOrderNum = (int) jobject_0["minOrderNum"];
            return sku;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["name"] = func.getJObjectFromDic(this.name);
                JArray array = new JArray();
                foreach (intervalPrice price in this.value)
                {
                    array.Add(price.getJObject);
                }
                obj2["value"] = array;
                obj2["skuCode"] = this.skuCode;
                obj2["proNum"] = this.proNum;
                obj2["isWholesale"] = this.isWholesale;
                obj2["off"] = this.off;
                obj2["minOrderNum"] = this.minOrderNum;
                return obj2;
            }
        }

        public string getName
        {
            get
            {
                string str = "";
                foreach (KeyValuePair<string, string> pair in this.name)
                {
                    if (str == "")
                    {
                        if (pair.Key == "")
                        {
                            str = pair.Value;
                        }
                        else
                        {
                            str = pair.Key + ":" + pair.Value;
                        }
                    }
                    else if (pair.Key == "")
                    {
                        str = str + "," + pair.Value;
                    }
                    else
                    {
                        str = str + "," + pair.Key + ":" + pair.Value;
                    }
                }
                return str;
            }
        }
    }
}


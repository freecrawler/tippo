namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class attr
    {
        public bool allowZH;
        public List<string> banReg = new List<string>();
        public Dictionary<string, string> extraData = new Dictionary<string, string>();
        public bool isBatch;
        public bool isDigi;
        public bool isInput;
        public bool isMult;
        public bool isMust;
        public bool isRadio;
        public int maxLength;
        public double maxValue;
        public int minLength;
        public double minValue = 1.0;
        public textID name = new textID();
        public int num = 10;
        public List<attrValue> value = new List<attrValue>();

        public static attr getAttrByIds(List<attr> list_0, List<string> list_1)
        {
            return (attr) getObjByIds(list_0, list_1);
        }

        public static attrValue getAttrValueByIds(List<attr> list_0, List<string> list_1)
        {
            return (attrValue) getObjByIds(list_0, list_1);
        }

        private static object getObjByIds(List<attr> list_0, List<string> list_1)
        {
            int num3;
            attr attr = new attr();
            attrValue value2 = new attrValue();
            value2.attrs = list_0;
            int a = 0;
            foreach (string str in list_1)
            {
                int num2;
                Math.DivRem(a, 2, out num2);
                if (num2 == 0)
                {
                    attr = new attr();
                    foreach (attr attr2 in value2.attrs)
                    {
                        if (str == attr2.name.id)
                        {
                            attr = attr2;
                        }
                    }
                }
                else
                {
                    value2 = new attrValue();
                    foreach (attrValue value3 in attr.value)
                    {
                        if (str == value3.value.id)
                        {
                            value2 = value3;
                        }
                    }
                }
                a++;
            }
            Math.DivRem(list_1.Count, 2, out num3);
            if (num3 == 1)
            {
                return attr;
            }
            return value2;
        }

        public static attr getValueFromJObject(JObject jobject_0)
        {
            attr attr = new attr();
            attr.name = textID.getValueFromJObject((JObject) jobject_0["name"]);
            attr.isRadio = (bool) jobject_0["isRadio"];
            attr.isInput = (bool) jobject_0["isInput"];
            attr.isDigi = (bool) jobject_0["isDigi"];
            attr.minValue = (double) jobject_0["minValue"];
            attr.maxValue = (double) jobject_0["maxValue"];
            attr.num = (int) jobject_0["num"];
            attr.minLength = (int) jobject_0["minLength"];
            attr.maxLength = (int) jobject_0["maxLength"];
            attr.allowZH = (bool) jobject_0["allowZH"];
            attr.isMust = (bool) jobject_0["isMust"];
            attr.isMult = (bool) jobject_0["isMult"];
            attr.extraData = func.getDicFromJObject((JObject) jobject_0["extraData"]);
            foreach (JToken token in (IEnumerable<JToken>) ((JArray) jobject_0["value"]))
            {
                attr.value.Add(attrValue.getValueFromJObject((JObject) token));
            }
            foreach (string str in (IEnumerable<JToken>) ((JArray) jobject_0["banReg"]))
            {
                attr.banReg.Add(str);
            }
            return attr;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["name"] = this.name.getJObject;
                obj2["isRadio"] = this.isRadio;
                obj2["isInput"] = this.isInput;
                obj2["isDigi"] = this.isDigi;
                obj2["minValue"] = this.minValue;
                obj2["maxValue"] = this.maxValue;
                obj2["num"] = this.num;
                obj2["minLength"] = this.minLength;
                obj2["maxLength"] = this.maxLength;
                obj2["allowZH"] = this.allowZH;
                obj2["isMust"] = this.isMust;
                obj2["isMult"] = this.isMult;
                obj2["extraData"] = func.getJObjectFromDic(this.extraData);
                JArray array = new JArray();
                foreach (attrValue value2 in this.value)
                {
                    array.Add(value2.getJObject);
                }
                obj2["value"] = array;
                JArray array2 = new JArray();
                foreach (string str in this.banReg)
                {
                    array2.Add(str);
                }
                obj2["banReg"] = array2;
                return obj2;
            }
        }
    }
}


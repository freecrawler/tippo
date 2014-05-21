namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class attrValue
    {
        public List<attr> attrs = new List<attr>();
        public bool isSelect;
        public textID value = new textID();

        public static attrValue getValueFromJObject(JObject jobject_0)
        {
            attrValue value2 = new attrValue();
            value2.value = textID.getValueFromJObject((JObject) jobject_0["value"]);
            value2.isSelect = (bool) jobject_0["isSelect"];
            foreach (JToken token in (IEnumerable<JToken>) ((JArray) jobject_0["attrs"]))
            {
                value2.attrs.Add(attr.getValueFromJObject((JObject) token));
            }
            return value2;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["value"] = this.value.getJObject;
                obj2["isSelect"] = this.isSelect;
                JArray array = new JArray();
                foreach (attr attr in this.attrs)
                {
                    array.Add(attr.getJObject);
                }
                obj2["attrs"] = array;
                return obj2;
            }
        }
    }
}


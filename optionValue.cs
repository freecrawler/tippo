namespace client
{
    using My.Json.Linq;
    using System;

    [Serializable]
    public class optionValue
    {
        public string aliasName = "";
        public string pic = "";
        public textID value = new textID();

        public static optionValue getValueFromJObject(JObject jobject_0)
        {
            optionValue value2 = new optionValue();
            value2.value = textID.getValueFromJObject((JObject) jobject_0["value"]);
            value2.pic = jobject_0["pic"].ToString();
            value2.aliasName = jobject_0["aliasName"].ToString();
            return value2;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["value"] = this.value.getJObject;
                obj2["pic"] = this.pic;
                obj2["aliasName"] = this.aliasName;
                return obj2;
            }
        }
    }
}


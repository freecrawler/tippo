namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class cate
    {
        public Dictionary<string, string> extraData = new Dictionary<string, string>();
        public string fvalue = "";
        public bool isLeaf;
        public textID value = new textID();

        public static cate getValueFromJObject(JObject jobject_0)
        {
            cate cate = new cate();
            cate.value = textID.getValueFromJObject((JObject) jobject_0["value"]);
            cate.isLeaf = (bool) jobject_0["isLeaf"];
            cate.extraData = func.getDicFromJObject((JObject) jobject_0["extraData"]);
            cate.fvalue = jobject_0["fvalue"].ToString();
            return cate;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["value"] = this.value.getJObject;
                obj2["isLeaf"] = this.isLeaf;
                obj2["extraData"] = func.getJObjectFromDic(this.extraData);
                obj2["fvalue"] = this.fvalue;
                return obj2;
            }
        }
    }
}


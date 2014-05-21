namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class textID
    {
        public List<textID> childList = new List<textID>();
        public string id = "";
        public Dictionary<string, string> text = new Dictionary<string, string>();

        public textID()
        {
            this.text["zh-cn"] = "";
            this.text["en"] = "";
        }

        public static textID getValueFromJObject(JObject jobject_0)
        {
            textID tid = new textID();
            tid.id = jobject_0["id"].ToString();
            if (func.haveKey("childList", jobject_0))
            {
                foreach (JObject obj2 in (IEnumerable<JToken>) jobject_0["childList"])
                {
                    tid.childList.Add(getValueFromJObject(obj2));
                }
            }
            else
            {
                tid.childList = new List<textID>();
            }
            tid.text = func.getDicFromJObject((JObject) jobject_0["text"]);
            return tid;
        }

        public string GetFullText
        {
            get
            {
                bool flag = true;
                StringBuilder builder = new StringBuilder();
                foreach (KeyValuePair<string, string> pair in this.text)
                {
                    if (pair.Value != "")
                    {
                        if (flag)
                        {
                            builder.Append(pair.Value.ToString());
                            flag = false;
                        }
                        else if (pair.Value.ToString() != builder.ToString())
                        {
                            builder.Append("(" + pair.Value.ToString() + ")");
                        }
                    }
                }
                return builder.ToString();
            }
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["id"] = this.id;
                obj2["text"] = new JObject();
                obj2["text"] = func.getJObjectFromDic(this.text);
                JArray array = new JArray();
                foreach (textID tid in this.childList)
                {
                    array.Add(tid.getJObject);
                }
                obj2["childList"] = array;
                return obj2;
            }
        }
    }
}


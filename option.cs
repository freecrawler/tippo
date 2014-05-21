namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class option
    {
        public bool isEditAlias;
        public bool isEditPic;
        public bool isLinkPrice = true;
        public bool isMust;
        public textID name = new textID();
        public List<optionValue> optionValues = new List<optionValue>();

        public static option getValueFromJObject(JObject jobject_0)
        {
            option option = new option();
            option.name = textID.getValueFromJObject((JObject) jobject_0["name"]);
            foreach (JToken token in (IEnumerable<JToken>) ((JArray) jobject_0["optionValues"]))
            {
                option.optionValues.Add(optionValue.getValueFromJObject((JObject) token));
            }
            option.isEditAlias = (bool) jobject_0["isEditAlias"];
            option.isEditPic = (bool) jobject_0["isEditPic"];
            option.isLinkPrice = (bool) jobject_0["isLinkPrice"];
            try
            {
                option.isMust = (bool) jobject_0["isMust"];
            }
            catch
            {
            }
            return option;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["name"] = this.name.getJObject;
                JArray array = new JArray();
                foreach (optionValue value2 in this.optionValues)
                {
                    array.Add(value2.getJObject);
                }
                obj2["optionValues"] = array;
                obj2["isLinkPrice"] = this.isLinkPrice;
                obj2["isEditAlias"] = this.isEditAlias;
                obj2["isEditPic"] = this.isEditPic;
                obj2["isMust"] = this.isMust;
                return obj2;
            }
        }
    }
}


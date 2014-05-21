namespace client
{
    using My.Json.Linq;
    using System;

    [Serializable]
    public class shipMethodFee
    {
        public double fee;
        public bool isSelect;
        public int off = 100;

        public static shipMethodFee getValueFromJObject(JObject jobject_0)
        {
            shipMethodFee fee = new shipMethodFee();
            fee.fee = (double) jobject_0["fee"];
            fee.off = (int) jobject_0["off"];
            fee.isSelect = (bool) jobject_0["isSelect"];
            return fee;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["fee"] = this.fee;
                obj2["off"] = this.off;
                obj2["isSelect"] = this.isSelect;
                return obj2;
            }
        }
    }
}


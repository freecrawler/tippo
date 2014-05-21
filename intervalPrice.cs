namespace client
{
    using My.Json.Linq;
    using System;

    [Serializable]
    public class intervalPrice
    {
        public int day = 3;
        public int max = 0x270f;
        public int min = 1;
        public double price;

        public static intervalPrice getValueFromJObject(JObject jobject_0)
        {
            intervalPrice price = new intervalPrice();
            price.min = (int) jobject_0["min"];
            price.max = (int) jobject_0["max"];
            price.price = (double) jobject_0["price"];
            price.day = (int) jobject_0["day"];
            return price;
        }

        public JObject getJObject
        {
            get
            {
                JObject obj2 = new JObject();
                obj2["min"] = this.min;
                obj2["max"] = this.max;
                obj2["price"] = this.price;
                obj2["day"] = this.day;
                return obj2;
            }
        }
    }
}


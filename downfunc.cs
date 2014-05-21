namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class downfunc
    {
        public static string dealPrice(string string_0)
        {
            if (string_0.Contains(","))
            {
                StringBuilder builder = new StringBuilder(string_0);
                builder.Replace(",", ".", string_0.LastIndexOf(','), 1);
                string_0 = builder.ToString().Replace(",", "");
            }
            return string_0;
        }

        public static attr getAttr(JArray jarray_0, string string_0)
        {
            attr attr = new attr();
            if (jarray_0.Count >= 2)
            {
                textID tid = new textID();
                tid.text[string_0] = jarray_0[0].ToString();
                attr.name = tid;
                List<attrValue> list = new List<attrValue>();
                attrValue item = new attrValue();
                tid = new textID();
                item.value.text[string_0] = jarray_0[1].ToString();
                list.Add(item);
                attr.value = list;
            }
            return attr;
        }

        public static attr getAttr(JArray jarray_0, string string_0, string string_1)
        {
            attr attr = new attr();
            if (jarray_0.Count >= 2)
            {
                textID tid = new textID();
                tid.text[string_1] = jarray_0[0].ToString();
                attr.name = tid;
                List<attrValue> list = new List<attrValue>();
                attrValue item = new attrValue();
                tid = new textID();
                item.value.text[string_1] = jarray_0[1].ToString();
                item.value.id = string_0;
                list.Add(item);
                attr.value = list;
            }
            return attr;
        }

        public static attr getAttr(string string_0, string string_1, string string_2)
        {
            attr attr = new attr();
            textID tid = new textID();
            tid.text[string_2] = string_0;
            attr.name = tid;
            List<attrValue> list = new List<attrValue>();
            attrValue item = new attrValue();
            tid = new textID();
            item.value.text[string_2] = string_1;
            list.Add(item);
            attr.value = list;
            return attr;
        }

        public static textID getListValue(string string_0, string string_1)
        {
            textID tid = new textID();
            tid.text[string_0] = string_1;
            return tid;
        }

        public static textID getListValue(string string_0, string string_1, string string_2)
        {
            textID tid = new textID();
            tid.id = string_0;
            tid.text[string_1] = string_2;
            return tid;
        }

        public static option getOption(string string_0, JArray jarray_0, string string_1, int int_0, bool bool_0)
        {
            option option = new option();
            textID tid = new textID();
            tid.text[string_1] = string_0;
            option.name = tid;
            option.isLinkPrice = bool_0;
            optionValue item = null;
            List<optionValue> list = new List<optionValue>();
            foreach (JArray array in (IEnumerable<JToken>) jarray_0)
            {
                item = new optionValue();
                new textID();
                item.value.text[string_1] = array[int_0].ToString();
                list.Add(item);
            }
            option.optionValues = list;
            return option;
        }

        public static option getOption(string string_0, JArray jarray_0, string string_1, int int_0, int int_1, bool bool_0)
        {
            option option = new option();
            textID tid = new textID();
            tid.text[string_1] = string_0;
            option.name = tid;
            option.isLinkPrice = bool_0;
            optionValue item = null;
            List<optionValue> list = new List<optionValue>();
            foreach (JArray array in (IEnumerable<JToken>) jarray_0)
            {
                item = new optionValue();
                new textID();
                item.value.text[string_1] = array[int_0].ToString();
                item.value.id = array[int_1].ToString();
                list.Add(item);
            }
            option.optionValues = list;
            return option;
        }

        public static option getOption(string string_0, string string_1, JArray jarray_0, string string_2, int int_0, bool bool_0)
        {
            option option = new option();
            textID tid = new textID();
            tid.id = string_1;
            tid.text[string_2] = string_0;
            option.name = tid;
            option.isLinkPrice = bool_0;
            optionValue item = null;
            List<optionValue> list = new List<optionValue>();
            foreach (JArray array in (IEnumerable<JToken>) jarray_0)
            {
                item = new optionValue();
                new textID();
                item.value.text[string_2] = array[int_0].ToString();
                list.Add(item);
            }
            option.optionValues = list;
            return option;
        }

        public static List<client.intervalPrice> getPriceTable(char char_0, string string_0, JArray jarray_0, int int_0)
        {
            List<client.intervalPrice> list = new List<client.intervalPrice>();
            client.intervalPrice item = null;
            foreach (JArray array in (IEnumerable<JToken>) jarray_0)
            {
                item = new client.intervalPrice();
                if (array[0].ToString().Contains(char_0.ToString()))
                {
                    string str = string.Format(@"([^{0}]+){0}[^\d]*(\d+)", char_0);
                    JArray array2 = func.正则获取(array[0].ToString(), str, "");
                    item.min = Convert.ToInt32(array2[0].ToString());
                    item.max = Convert.ToInt32(array2[1].ToString());
                }
                else
                {
                    item.min = Convert.ToInt32(array[0].ToString());
                    item.max = Convert.ToInt32(array[0].ToString());
                }
                if (array[1].ToString().Contains(string_0))
                {
                    item.price = Convert.ToSingle(func.正则获取(array[1].ToString(), @"~[^\d]+([\d\.]+)"));
                }
                else
                {
                    item.price = Convert.ToSingle(func.正则获取(array[1].ToString(), @"([\d\.]+)"));
                }
                if (int_0 != 0)
                {
                    item.day = int_0;
                }
                list.Add(item);
            }
            return list;
        }

        public static List<client.intervalPrice> getPriceTable(char char_0, bool bool_0, string string_0, JArray jarray_0, bool bool_1)
        {
            List<client.intervalPrice> list = new List<client.intervalPrice>();
            client.intervalPrice item = null;
            foreach (JArray array in (IEnumerable<JToken>) jarray_0)
            {
                item = new client.intervalPrice();
                if (array[0].ToString().Contains(char_0.ToString()))
                {
                    string str = string.Format(@"([^{0}]+){0}[^\d]*(\d+)", char_0);
                    JArray array2 = func.正则获取(array[0].ToString(), str, "");
                    item.min = Convert.ToInt32(array2[0].ToString());
                    item.max = Convert.ToInt32(array2[1].ToString());
                }
                else
                {
                    item.min = Convert.ToInt32(array[0].ToString());
                    item.max = Convert.ToInt32(array[0].ToString());
                }
                if (bool_0)
                {
                    if (array[1].ToString().Contains(string_0))
                    {
                        item.price = Convert.ToSingle(func.正则获取(array[1].ToString(), @"~[^\d]+([\d\.]+)"));
                    }
                    else
                    {
                        item.price = Convert.ToSingle(func.正则获取(array[1].ToString(), @"([\d\.]+)"));
                    }
                }
                else
                {
                    item.price = Convert.ToSingle(array[1].ToString());
                }
                if (bool_1)
                {
                    item.day = Convert.ToInt32(array[2].ToString());
                }
                list.Add(item);
            }
            return list;
        }

        public static List<client.intervalPrice> intervalPrice(string string_0, int int_0)
        {
            if (string_0 == "")
            {
                string_0 = "999.00";
            }
            List<client.intervalPrice> list = new List<client.intervalPrice>();
            client.intervalPrice item = new client.intervalPrice();
            item.min = 1;
            item.max = 1;
            item.price = Convert.ToSingle(string_0);
            if (int_0 != 0)
            {
                item.day = int_0;
            }
            list.Add(item);
            return list;
        }

        private List<attrValue> method_0(JArray jarray_0, string string_0, bool bool_0)
        {
            List<attrValue> list = new List<attrValue>();
            attrValue item = null;
            if (bool_0)
            {
                foreach (JArray array in (IEnumerable<JToken>) jarray_0)
                {
                    item = new attrValue();
                    new textID();
                    item.value.text[string_0] = array[1].ToString();
                    item.value.id = array[0].ToString();
                    list.Add(item);
                }
                return list;
            }
            foreach (JArray array2 in (IEnumerable<JToken>) jarray_0)
            {
                JArray array3 = array2;
                item = new attrValue();
                new textID();
                item.value.text[string_0] = array3[1].ToString();
                item.value.id = array3[0].ToString();
                list.Add(item);
            }
            return list;
        }
    }
}


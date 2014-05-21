namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;

    public class translate
    {
        private static string googleTrans(string string_0, string string_1, string string_2)
        {
            http http = new http();
            http.ContentType = ContentType.urlEncoded;
            http.url = "http://translate.google.cn/translate_a/t";
            http.PostData.addString("client", "t");
            http.PostData.addString("text", string_0);
            http.PostData.addString("hl", string_2);
            http.PostData.addString("sl", string_1);
            http.PostData.addString("tl", string_2);
            http.PostData.addString("multires", "1");
            http.PostData.addString("ssel", "0");
            http.PostData.addString("tsel", "0");
            http.PostData.addString("sc", "1");
            http.isGzip = false;
            string json = http.DownHtml();
            string str2 = "";
            try
            {
                foreach (JArray array2 in (IEnumerable<JToken>) JArray.Parse(json)[0])
                {
                    str2 = str2 + array2[0];
                }
            }
            catch (Exception)
            {
            }
            if (string_2 == "en")
            {
                str2 = str2.Replace("包邮", " free shipping ");
            }
            return str2;
        }

        public static string trans(string string_0, string string_1, string string_2)
        {
            return googleTrans(string_0, string_1, string_2);
        }

        public static string trans(string string_0, string string_1, string string_2, string string_3)
        {
            switch (string_3)
            {
                case "google":
                    return googleTrans(string_0, string_1, string_2);

                case "yahoo":
                    return yahooTrans(string_0, string_1, string_2);
            }
            return googleTrans(string_0, string_1, string_2);
        }

        private static string yahooTrans(string string_0, string string_1, string string_2)
        {
            return "";
        }
    }
}


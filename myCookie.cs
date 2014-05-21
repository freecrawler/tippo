namespace client
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class myCookie
    {
        public static CookieContainer cookieContainer;
        public static Dictionary<string, string> cookies;
        public static bool isSucc;
        public static bool isUseUpsite;
        public static string loginUrl;
        public static string siteId;
        public static string succ;
        public static string succUrl;
        public static string UserAgnet;
        public static string userName;

        static myCookie()
        {
            old_acctor_mc();
        }

        public static bool getCookie(string string_0, string string_1, string string_2, string string_3)
        {
            cookies = new Dictionary<string, string>();
            sitelogin sitelogin = new sitelogin();
            loginUrl = string_0;
            succ = string_1;
            userName = string_2;
            siteId = string_3;
            sitelogin.ShowDialog();
            return isSucc;
        }

        private static void old_acctor_mc()
        {
            cookies = new Dictionary<string, string>();
            cookieContainer = new CookieContainer();
            loginUrl = "";
            succ = "";
            isSucc = false;
            userName = "";
            siteId = "";
            isUseUpsite = true;
            UserAgnet = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
            succUrl = "";
        }
    }
}


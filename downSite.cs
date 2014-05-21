namespace client
{
    using System;
    using System.Collections.Generic;

    public class downSite : site
    {
        public http Http = new http();
        public bool isNeedDownHtml = true;
        public bool isTry;

        public virtual void grapUrl(string string_0, Dictionary<string, string> dictionary_0, Dictionary<string, string> dictionary_1)
        {
        }

        public virtual void handleIp(string string_0, string string_1, string string_2)
        {
        }

        public virtual bool isBanIp(string string_0, string string_1, string string_2)
        {
            return false;
        }
    }
}


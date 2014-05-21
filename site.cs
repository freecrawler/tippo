namespace client
{
    using System;
    using System.Collections.Generic;

    public class site
    {
        public string currencyUnit = "美元";
        public string defaultLanguage = "en";
        public string encode = "utf-8";
        public int intervalTime;
        public bool isExtPic;
        public bool isFast;
        public bool isFreezePanes = true;
        public bool isLogin;
        public bool isShowExp;
        public bool isToDh;
        public bool isXls;
        public string loginSuccess = "";
        public string loginUrl = "";
        public string loginVeryUrl = "";
        public string siteId = "";
        public string siteName = "";
        public string xlsFileName = "product.xls";
        public Dictionary<string, int> xlsHeader = new Dictionary<string, int>();
        public string xlsImgFloder;
        public string xlsPicExt = "";
    }
}


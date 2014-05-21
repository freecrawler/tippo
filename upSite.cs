namespace client
{
    using csExWB;
    using IfacesEnumsStructsClasses;
    using System;

    public class upSite : site
    {
        public string csvEncode = "";
        public string csvFileName = "product.csv";
        public string csvHeader = "";
        public string csvImgFloder = "pic";
        public string csvPicExt = "";
        public string csvSplit = ",";
        public bool isCSV;
        public bool isOwn;
        public bool isSaleAttrPic;
        public string secondLanguage = "";
        public bool waterIsXD = true;
        public int waterPos = 4;
        public float waterX;
        public float waterY;

        public virtual IHTMLElement getUserNameInput(cEXWB cEXWB_0)
        {
            return null;
        }
    }
}


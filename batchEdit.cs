namespace client
{
    using csExWB;
    using System;

    public class batchEdit : fatherObj
    {
        public batchEdit(cEXWB cEXWB_0)
        {
            base.webbrowser = cEXWB_0;
            base.defaultLanguage = "zh-cn";
            base.editType = "batch";
        }
    }
}


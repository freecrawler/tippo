namespace client
{
    using csExWB;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class search : fatherObj
    {
        public search(cEXWB cEXWB_0)
        {
            base.webbrowser = cEXWB_0;
            base.defaultLanguage = "zh-cn";
            base.editType = "search";
        }

        public string getSearch()
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            switch (base.state)
            {
                case 1:
                    str = " state=1 ";
                    break;

                case 2:
                    str = " state=2 ";
                    break;

                case 3:
                    str = " state=3 ";
                    break;

                case 4:
                    str = " state!=0 ";
                    break;
            }
            builder.Append("where" + str);
            foreach (KeyValuePair<string, baseP> pair in base.attrs)
            {
                string str2 = pair.Value.getSearchCondition();
                if (str2 != "")
                {
                    builder.Append("and" + str2);
                }
            }
            return builder.ToString();
        }
    }
}


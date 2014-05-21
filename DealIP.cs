namespace client
{
    using My.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Xml;

    public class DealIP
    {
        public static string ADSLname;
        public static bool isADSL;
        public static bool isHandle;
        public static bool isProxy;
        public static bool isVcode;
        public static JArray JaProxy;
        public static string picUrl;
        public static string proxyHost;
        public static int Proxyindex;
        public static int proxyPort;
        public static string RetrunVcode;
        public static string username;
        public static string userpass;

        static DealIP()
        {
            old_acctor_mc();
        }

        public static void handle(http http_0)
        {
            if (isHandle)
            {
                Thread.Sleep(0x1388);
            }
            else
            {
                isHandle = true;
                string str = CommandDo.Execute(" ");
                if (func.正则匹配(str, @".+\n.+\n.+", ""))
                {
                    string str2 = func.前后截取(str, "\n", "\n");
                    CommandDo.Execute(str2 + " /DISCONNECT");
                    isADSL = false;
                    if (ADSLname == str2)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(ADSLname + " " + username + " " + userpass);
                        isADSL = func.正则匹配(CommandDo.Execute(builder.ToString()), @".+\n.+\n.+", "");
                    }
                    if (!isADSL)
                    {
                        ASDLForm form = new ASDLForm();
                        form.name = str2;
                        form.ShowDialog();
                    }
                }
                else if (!isProxy)
                {
                    if (picUrl != "")
                    {
                        IPPictuer pictuer = new IPPictuer();
                        pictuer.hp = http_0;
                        pictuer.ShowDialog();
                        isVcode = true;
                    }
                    else
                    {
                        new HttpForm().ShowDialog();
                        if (isProxy)
                        {
                            string[] strArray2 = JaProxy[Proxyindex].ToString().Split(new char[] { ':', ' ' });
                            http_0.proxyHost = strArray2[0];
                            http_0.proxyPort = text.toInt(strArray2[1]);
                            proxyHost = http_0.proxyHost;
                            proxyPort = http_0.proxyPort;
                        }
                        else
                        {
                            http_0.proxyHost = "";
                            proxyHost = http_0.proxyHost;
                            proxyPort = http_0.proxyPort;
                        }
                    }
                }
                else if (JaProxy.Count > 0)
                {
                    Proxyindex++;
                    while (Proxyindex < JaProxy.Count)
                    {
                        if (!JaProxy[Proxyindex].ToString().Contains("已用"))
                        {
                            JaProxy[Proxyindex] = JaProxy[Proxyindex] + " 已用";
                            break;
                        }
                        Proxyindex++;
                    }
                    if (Proxyindex >= JaProxy.Count)
                    {
                        new HttpForm().ShowDialog();
                    }
                    if (isProxy)
                    {
                        string[] strArray = JaProxy[Proxyindex].ToString().Split(new char[] { ':', ' ' });
                        http_0.proxyHost = strArray[0];
                        http_0.proxyPort = text.toInt(strArray[1]);
                        proxyHost = http_0.proxyHost;
                        proxyPort = http_0.proxyPort;
                    }
                    else
                    {
                        http_0.proxyHost = "";
                        proxyHost = http_0.proxyHost;
                        proxyPort = http_0.proxyPort;
                    }
                }
                isHandle = false;
            }
        }

        public static void handleVcode(http http_0)
        {
            new IPPictuer().ShowDialog();
            isVcode = true;
        }

        public static void init()
        {
            isProxy = false;
            Proxyindex = 0;
            picUrl = "";
            isHandle = false;
            RetrunVcode = "";
            isADSL = false;
            isVcode = false;
            proxyHost = "";
            proxyPort = 0;
        }

        public static bool isConnect(string string_0)
        {
            http http = new http();
            string_0 = (string_0 == "") ? "http://www.baidu.com/" : string_0;
            http.url = string_0;
            return ((http.GetStream2() != null) || isConnect2(""));
        }

        public static bool isConnect2(string string_0)
        {
            http http = new http();
            string_0 = (string_0 == "") ? "http://www.soso.com/" : string_0;
            http.url = string_0;
            return ((http.GetStream2() != null) || isConnect3(""));
        }

        public static bool isConnect3(string string_0)
        {
            http http = new http();
            string_0 = (string_0 == "") ? "http://www.baidu.com/" : string_0;
            http.url = string_0;
            return ((http.GetStream2() != null) || isConnect4(""));
        }

        public static bool isConnect4(string string_0)
        {
            http http = new http();
            string_0 = (string_0 == "") ? "http://www.soso.com/" : string_0;
            http.url = string_0;
            return (http.GetStream2() != null);
        }

        private static void old_acctor_mc()
        {
            JaProxy = new JArray();
            Proxyindex = 0;
            isProxy = false;
            isHandle = false;
            isADSL = false;
            ADSLname = "";
            username = "";
            userpass = "";
            isVcode = false;
            picUrl = "";
            RetrunVcode = "";
            proxyHost = "";
        }

        public static bool proxyIsOK(string string_0, int int_0, string string_1)
        {
            http http = new http();
            string_1 = (string_1 == "") ? "http://www.baidu.com/" : string_1;
            http.proxyHost = string_0;
            http.proxyPort = int_0;
            http.url = string_1;
            return ((http.GetStream2() != null) || proxyIsOK2(string_0, int_0, ""));
        }

        public static bool proxyIsOK2(string string_0, int int_0, string string_1)
        {
            http http = new http();
            string_1 = (string_1 == "") ? "http://www.soso.com/" : string_1;
            http.proxyHost = string_0;
            http.proxyPort = int_0;
            http.url = string_1;
            return (http.GetStream2() != null);
        }

        public static string[] Readuser()
        {
            string[] strArray = new string[] { "", "DiamondBlue" };
            string path = Environment.CurrentDirectory + @"\nnnn.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode node in document.SelectSingleNode("nnnuser").ChildNodes)
                {
                    if (node.Name == "user1111")
                    {
                        strArray[0] = node.InnerText.Trim();
                    }
                    else if (node.Name == "skinName")
                    {
                        strArray[1] = node.InnerText.Trim();
                    }
                }
            }
            return strArray;
        }

        public static void ReadXML()
        {
            string path = Environment.CurrentDirectory + @"\add123.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                try
                {
                    foreach (XmlNode node in document.SelectSingleNode("ddddd").ChildNodes)
                    {
                        if (node.Name == "name")
                        {
                            ADSLname = node.InnerText.Trim();
                        }
                        else if (node.Name == "Account")
                        {
                            username = node.InnerText.Trim();
                        }
                        else if (node.Name == "ps")
                        {
                            userpass = node.InnerText.Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            path = Environment.CurrentDirectory + "/Pr12345.xml";
            if (File.Exists(path))
            {
                try
                {
                    XmlDocument document2 = new XmlDocument();
                    document2.Load(path);
                    foreach (XmlElement element in document2.SelectNodes("/prList/List"))
                    {
                        JaProxy.Add(element.GetAttribute("hopo"));
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static void wirte(string string_0, string string_1)
        {
            string path = Environment.CurrentDirectory + @"\nnnn.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNodeList childNodes = document.SelectSingleNode("nnnuser").ChildNodes;
                try
                {
                    foreach (XmlNode node in childNodes)
                    {
                        XmlElement element = (XmlElement) node;
                        if (node.Name == "user1111")
                        {
                            element.InnerText = string_0;
                        }
                        else if (node.Name == "skinName")
                        {
                            element.InnerText = string_1;
                        }
                    }
                    document.Save("nnnn.xml");
                }
                catch (Exception)
                {
                }
            }
        }

        public static void wirteXML()
        {
            XmlDocument document = new XmlDocument();
            string filename = Environment.CurrentDirectory + "/Pr12345.xml";
            document.Load(filename);
            document.SelectSingleNode("prList").RemoveAll();
            document.Save(filename);
            document.Load(filename);
            XmlNode node = document.SelectSingleNode("prList");
            foreach (string str2 in (IEnumerable<JToken>) JaProxy)
            {
                XmlElement newChild = document.CreateElement("List");
                newChild.SetAttribute("hopo", str2.Replace(" 已用", ""));
                node.AppendChild(newChild);
            }
            document.Save(filename);
        }

        public static void wirteXMLADSL()
        {
            string filename = Environment.CurrentDirectory + @"\add123.xml";
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            XmlNodeList childNodes = document.SelectSingleNode("ddddd").ChildNodes;
            try
            {
                foreach (XmlNode node in childNodes)
                {
                    XmlElement element = (XmlElement) node;
                    if (node.Name == "name")
                    {
                        element.InnerText = ADSLname;
                    }
                    else if (node.Name == "Account")
                    {
                        element.InnerText = username;
                    }
                    else if (node.Name == "ps")
                    {
                        element.InnerText = userpass;
                    }
                }
                document.Save("add123.xml");
            }
            catch (Exception)
            {
            }
        }
    }
}


namespace client
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class XmlHelper
    {
        public static void Delete(string string_0, string string_1, string string_2)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(string_0);
                XmlNode oldChild = document.SelectSingleNode(string_1);
                XmlElement element = (XmlElement) oldChild;
                if (string_2.Equals(""))
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                else
                {
                    element.RemoveAttribute(string_2);
                }
                document.Save(string_0);
            }
            catch
            {
            }
        }

        public static void Delete(XmlDocument xmlDocument_0, string string_0, string string_1)
        {
            try
            {
                XmlNode oldChild = xmlDocument_0.SelectSingleNode(string_0);
                XmlElement element = (XmlElement) oldChild;
                if (string_1.Equals(""))
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                else
                {
                    element.RemoveAttribute(string_1);
                }
            }
            catch
            {
            }
        }

        public static void Insert(string string_0, string string_1, string string_2, string string_3, string string_4)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(string_0);
                XmlNode node = document.SelectSingleNode(string_1);
                if (string_2.Equals(""))
                {
                    if (!string_3.Equals(""))
                    {
                        ((XmlElement) node).SetAttribute(string_3, string_4);
                    }
                }
                else
                {
                    XmlElement newChild = document.CreateElement(string_2);
                    if (string_3.Equals(""))
                    {
                        newChild.InnerText = string_4;
                    }
                    else
                    {
                        newChild.SetAttribute(string_3, string_4);
                    }
                    node.AppendChild(newChild);
                }
                document.Save(string_0);
            }
            catch
            {
            }
        }

        public static void Insert(XmlDocument xmlDocument_0, string string_0, string string_1, string string_2, string string_3)
        {
            try
            {
                XmlNode node = xmlDocument_0.SelectSingleNode(string_0);
                if (string_1.Equals(""))
                {
                    if (!string_2.Equals(""))
                    {
                        ((XmlElement) node).SetAttribute(string_2, string_3);
                    }
                }
                else
                {
                    XmlElement newChild = xmlDocument_0.CreateElement(string_1);
                    if (string_2.Equals(""))
                    {
                        newChild.InnerText = string_3;
                    }
                    else
                    {
                        newChild.SetAttribute(string_2, string_3);
                    }
                    node.AppendChild(newChild);
                }
            }
            catch
            {
            }
        }

        public static string Read(string string_0, string string_1, string string_2)
        {
            string str = "";
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(string_0);
                XmlNode node = document.SelectSingleNode(string_1);
                str = string_2.Equals("") ? node.InnerText : node.Attributes[string_2].Value;
            }
            catch
            {
            }
            return str;
        }

        public static void Update(string string_0, string string_1, string string_2, string string_3)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(string_0);
                XmlNode oldChild = document.SelectSingleNode(string_1);
                if (string_3.ToString().Length <= 0)
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                else
                {
                    XmlElement element = (XmlElement) oldChild;
                    if (string_2.Equals(""))
                    {
                        element.InnerText = string_3;
                    }
                    else
                    {
                        element.SetAttribute(string_2, string_3);
                    }
                }
                document.Save(string_0);
            }
            catch
            {
            }
        }

        public static void Update(XmlDocument xmlDocument_0, string string_0, string string_1, string string_2)
        {
            try
            {
                XmlElement element = (XmlElement) xmlDocument_0.SelectSingleNode(string_0);
                if (string_1.Equals(""))
                {
                    element.InnerText = string_2;
                }
                else
                {
                    element.SetAttribute(string_1, string_2);
                }
            }
            catch
            {
            }
        }

        public static void Update(string string_0, string string_1, string string_2, List<string> list_0, bool bool_0)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(string_0);
                XmlNodeList list = document.SelectNodes(string_1);
                for (int i = 0; i < list_0.Count; i++)
                {
                    list[i].InnerText = list_0[i];
                }
                if ((list.Count > list_0.Count) && bool_0)
                {
                    for (int j = list.Count - 1; j >= list_0.Count; j--)
                    {
                        list[j].ParentNode.RemoveChild(list[j]);
                    }
                }
                else if (string_2 != "")
                {
                    XmlNodeList list2 = document.SelectNodes(string_2);
                    for (int k = list2.Count - 1; k >= list_0.Count; k--)
                    {
                        list2[k].ParentNode.RemoveChild(list2[k]);
                    }
                }
                document.Save(string_0);
            }
            catch
            {
            }
        }
    }
}


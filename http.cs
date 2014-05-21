namespace client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    public class http
    {
        public string AcceptLanguage = "us-en";
        private bool bool_0;
        public client.ContentType ContentType;
        public bool Continue = true;
        public CookieContainer cookieContainer = new CookieContainer();
        public Dictionary<string, string> Cookies = new Dictionary<string, string>();
        public byte[] Data;
        public string Encode = "utf-8";
        public bool isDown;
        public bool isGzip = true;
        public bool isRedirect = true;
        public string Language = "zh-CN";
        public postData PostData = new postData();
        public string proxyHost = "";
        public int proxyPort;
        public string Referer = "";
        public int reTime = 3;
        private string string_0 = "";
        private string string_1 = "";
        private string string_2 = "";
        public int TimeOut = 0x7530;
        public string url = "";
        public bool useCookieContainer;
        public bool useData;

        public string DownbaseFile(string string_3)
        {
            Stream stream = null;
            stream = this.GetStream();
            if (stream == null)
            {
                return "";
            }
            string_3 = string_3 + Path.GetFileName(this.url);
            byte[] buffer = new byte[0x400];
            Stream stream2 = null;
            try
            {
                int num;
                if (File.Exists(string_3))
                {
                    File.Delete(string_3);
                }
                stream2 = File.Create(string_3);
                goto Label_005A;
            Label_0049:
                if (num > 0)
                {
                    goto Label_005A;
                }
                return string_3;
            Label_004F:
                stream2.Write(buffer, 0, num);
                goto Label_0049;
            Label_005A:
                num = stream.Read(buffer, 0, buffer.Length);
                if (num <= 0)
                {
                    goto Label_0049;
                }
                goto Label_004F;
            }
            catch
            {
                string_3 = "";
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return string_3;
        }

        public string DownFile(string string_3)
        {
            Stream stream = null;
            stream = this.GetStream();
            if (stream == null)
            {
                return "";
            }
            string extension = ".";
            if (this.string_2.StartsWith("image/"))
            {
                extension = "." + this.string_2.Replace("image/", "");
            }
            if (extension == ".")
            {
                try
                {
                    extension = Path.GetExtension(this.Referer);
                }
                catch
                {
                    return "";
                }
                if (extension.Contains("?"))
                {
                    extension = extension.Split(new char[] { '?' })[0];
                }
                extension = extension.ToLower();
                if (((!(extension == ".jpg") && !(extension == ".jpeg")) && (!(extension == ".gif") && !(extension == ".png"))) && !(extension == ".bmp"))
                {
                    return "";
                }
            }
            if (extension == ".jpeg")
            {
                extension = ".jpg";
            }
            Stream stream2 = func.copyStream(stream);
            string str = func.md5_hash(stream2);
            if (str == "")
            {
                return "";
            }
            stream2.Position = 0L;
            string str3 = str.Substring(0, 1);
            string str4 = str.Substring(1, 1);
            string path = string_3 + @"\" + str3 + @"\" + str4;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string_3 = path + @"\" + str + extension;
            byte[] buffer = new byte[0x400];
            Stream stream3 = null;
            try
            {
                int num;
                stream3 = File.Create(string_3);
                goto Label_01C1;
            Label_01AC:
                if (num > 0)
                {
                    goto Label_01C1;
                }
                return string_3;
            Label_01B3:
                stream3.Write(buffer, 0, num);
                goto Label_01AC;
            Label_01C1:
                num = stream2.Read(buffer, 0, buffer.Length);
                if (num <= 0)
                {
                    goto Label_01AC;
                }
                goto Label_01B3;
            }
            catch
            {
                try
                {
                    int num2;
                    if (File.Exists(string_3))
                    {
                        return (str + extension);
                    }
                    stream3 = File.Create(string_3);
                    goto Label_020D;
                Label_01F8:
                    if (num2 > 0)
                    {
                        goto Label_020D;
                    }
                    return string_3;
                Label_01FF:
                    stream3.Write(buffer, 0, num2);
                    goto Label_01F8;
                Label_020D:
                    num2 = stream2.Read(buffer, 0, buffer.Length);
                    if (num2 <= 0)
                    {
                        goto Label_01F8;
                    }
                    goto Label_01FF;
                }
                catch
                {
                    string_3 = "";
                }
                return string_3;
            }
            finally
            {
                if (stream3 != null)
                {
                    stream3.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (stream2 != null)
                {
                    stream2.Close();
                }
            }
            return string_3;
        }

        public string DownHtml()
        {
            return func.handleHtml(this.DownHtmlNotHandle2());
        }

        public string DownHtml(string string_3)
        {
            this.string_1 = string_3;
            return (this.string_0 + this.DownHtml());
        }

        public string DownHtmlByWebClient()
        {
            try
            {
                if (this.url == "")
                {
                    return "";
                }
                this.url = this.url.Replace("https:", "http:");
                if (!this.url.StartsWith("http"))
                {
                    this.url = "http://" + this.url;
                }
                byte[] bytes = new WebClient().DownloadData(this.url);
                return Encoding.GetEncoding(this.Encode).GetString(bytes);
            }
            catch
            {
                return "";
            }
        }

        public string DownHtmlNotHandle()
        {
            return func.handleHtml2(this.DownHtmlNotHandle2());
        }

        public string DownHtmlNotHandle2()
        {
            Stream stream = this.GetStream();
            if (stream == null)
            {
                return "";
            }
            string str = "";
            try
            {
                StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(this.Encode));
                str = reader.ReadToEnd();
                reader.Dispose();
            }
            catch
            {
            }
            return str;
        }

        public Stream GetStream()
        {
            Stream stream = this.GetStream2();
            this.bool_0 = true;
            if (!this.bool_0)
            {
                if (!DealIP.isConnect(""))
                {
                    if (WebMsg.isShow)
                    {
                        do
                        {
                            Thread.Sleep(0x7d0);
                        }
                        while (!WebMsg.isClose);
                    }
                    else
                    {
                        WebMsg.isShow = true;
                        WebMsg.isClose = false;
                        WebMsg msg = new WebMsg();
                        msg.ShowDialog();
                        msg.Dispose();
                        WebMsg.isShow = false;
                    }
                    Stream stream2 = this.GetStream2();
                    this.PostData.li = new List<byte>();
                    return stream2;
                }
                WebMsg.isClose = true;
                if (this.proxyHost != "")
                {
                    if (!DealIP.proxyIsOK(this.proxyHost, this.proxyPort, ""))
                    {
                        DealIP.JaProxy.RemoveAt(DealIP.Proxyindex);
                        DealIP.wirteXML();
                        if (DealIP.Proxyindex >= DealIP.JaProxy.Count)
                        {
                            new HttpForm().ShowDialog();
                        }
                        if (DealIP.isProxy)
                        {
                            string[] strArray = DealIP.JaProxy[DealIP.Proxyindex].ToString().Split(new char[] { ':', ' ' });
                            this.proxyHost = strArray[0];
                            this.proxyPort = text.toInt(strArray[1]);
                            DealIP.proxyHost = this.proxyHost;
                            DealIP.proxyPort = this.proxyPort;
                            DealIP.JaProxy[DealIP.Proxyindex] = DealIP.JaProxy[DealIP.Proxyindex] + " 已用";
                        }
                        else
                        {
                            this.proxyHost = "";
                            DealIP.proxyHost = this.proxyHost;
                            DealIP.proxyPort = this.proxyPort;
                        }
                        Stream stream3 = this.GetStream();
                        this.PostData.li = new List<byte>();
                        return stream3;
                    }
                    this.PostData.li = new List<byte>();
                    return stream;
                }
                this.PostData.li = new List<byte>();
                return stream;
            }
            WebMsg.isClose = true;
            this.PostData.li = new List<byte>();
            return stream;
        }

        public Stream GetStream2()
        {
            byte[] data;
            HttpWebRequest request;
            this.bool_0 = false;
            if (this.url == "")
            {
                return null;
            }
            if (this.useData)
            {
                data = this.Data;
            }
            else
            {
                data = this.PostData.getData2();
            }
            HttpWebResponse response = null;
            int reTime = 0;
            Stream responseStream = null;
            do
            {
                try
                {
                    this.url = this.url.Replace("https:", "http:");
                    if (!this.url.StartsWith("http"))
                    {
                        this.url = "http://" + this.url;
                    }
                    request = WebRequest.Create(this.url) as HttpWebRequest;
                    request.ServicePoint.Expect100Continue = this.Continue;
                    request.UserAgent = myCookie.UserAgnet;
                    request.Accept = "*/*";
                    try
                    {
                        request.Referer = this.Referer;
                    }
                    catch
                    {
                    }
                    if (data != null)
                    {
                        request.ContentLength = data.LongLength;
                    }
                    if (this.proxyHost != "")
                    {
                        WebProxy proxy = new WebProxy(this.proxyHost, this.proxyPort);
                        request.Proxy = proxy;
                    }
                    if ((data != null) && (data.Length > 0))
                    {
                        request.Method = "post";
                        this.Encode = this.PostData.Encode;
                        this.ContentType = this.PostData.Contenttype;
                        if (this.ContentType == client.ContentType.urlEncoded)
                        {
                            request.ContentType = "application/x-www-form-urlencoded; charset=" + this.Encode;
                        }
                        else if (this.ContentType == client.ContentType.json)
                        {
                            request.ContentType = "application/json; charset=" + this.Encode;
                        }
                        else
                        {
                            request.ContentType = "multipart/form-data; boundary=----------gL6ae0Ef1ae0KM7ae0KM7GI3Ef1Ij5";
                        }
                    }
                    else
                    {
                        request.Method = "get";
                    }
                    request.Headers.Add(HttpRequestHeader.AcceptLanguage, this.AcceptLanguage);
                    request.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
                    request.Expect = "";
                    StringBuilder builder = new StringBuilder();
                    foreach (KeyValuePair<string, string> pair in this.Cookies)
                    {
                        builder.Append(pair.Key + "=");
                        builder.Append(pair.Value + ";");
                    }
                    if (this.useCookieContainer)
                    {
                        request.CookieContainer = this.cookieContainer;
                    }
                    else if (this.Cookies.Count > 0)
                    {
                        request.Headers.Add(HttpRequestHeader.Cookie, builder.ToString());
                    }
                    request.Timeout = this.TimeOut;
                    if (this.isGzip)
                    {
                        request.AutomaticDecompression = DecompressionMethods.GZip;
                    }
                    request.AllowAutoRedirect = this.isRedirect;
                    if (request.Method.ToLower() == "post")
                    {
                        request.GetRequestStream().Write(data, 0, data.Length);
                    }
                    response = (HttpWebResponse) request.GetResponse();
                    string responseHeader = "";
                    try
                    {
                        responseHeader = response.GetResponseHeader("Location");
                    }
                    catch
                    {
                    }
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        responseStream = response.GetResponseStream();
                        if (response.Headers["Set-Cookie"] != null)
                        {
                            func.strToDic(this.Cookies, response.Headers["Set-Cookie"], "([^=;, ]+)=([^;,]*)");
                            this.Cookies.Remove("path");
                            this.Cookies.Remove("domain");
                            this.Cookies.Remove("expires");
                        }
                        this.Referer = response.ResponseUri.ToString();
                        this.string_0 = response.GetResponseHeader(this.string_1);
                        this.string_2 = response.ContentType;
                        reTime = this.reTime;
                        this.bool_0 = true;
                    }
                    else if (((response.StatusCode != HttpStatusCode.MovedPermanently) && (response.StatusCode != HttpStatusCode.MovedPermanently)) && ((response.StatusCode != HttpStatusCode.Found) && (responseHeader.Length <= 0)))
                    {
                        if ((response.StatusCode != HttpStatusCode.NotFound) && (response.StatusCode != HttpStatusCode.NoContent))
                        {
                            Thread.Sleep(100);
                        }
                        else
                        {
                            reTime = this.reTime;
                            this.bool_0 = true;
                        }
                    }
                    else
                    {
                        if (response.Headers["Set-Cookie"] != null)
                        {
                            func.strToDic(this.Cookies, response.Headers["Set-Cookie"], "([^=;, ]+)=([^;,]*)");
                            this.Cookies.Remove("path");
                            this.Cookies.Remove("domain");
                            this.Cookies.Remove("expires");
                        }
                        this.url = responseHeader;
                        this.Data = null;
                        this.useData = false;
                        this.Referer = response.ResponseUri.ToString();
                        this.string_0 = response.GetResponseHeader(this.string_1);
                        this.string_2 = response.ContentType;
                        responseStream = this.GetStream2();
                        reTime = this.reTime;
                        this.bool_0 = true;
                    }
                }
                catch (Exception exception)
                {
                    if (this.isDown)
                    {
                        reTime = this.reTime;
                        this.bool_0 = false;
                    }
                    if ((exception.Message.Contains("404") || exception.Message.Contains("204")) || exception.Message.Contains("无法解析"))
                    {
                        this.bool_0 = true;
                    }
                }
                reTime++;
            }
            while (reTime < this.reTime);
            request = null;
            this.Data = null;
            return responseStream;
        }

        private void method_0(CookieCollection cookieCollection_0)
        {
            foreach (Cookie cookie in cookieCollection_0)
            {
                this.Cookies[cookie.Name] = cookie.Value;
            }
        }
    }
}


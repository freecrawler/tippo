namespace client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class uploadPic
    {
        public Dictionary<string, postData> allPic = new Dictionary<string, postData>();
        public Dictionary<string, string> allPicHtml = new Dictionary<string, string>();
        public Dictionary<string, string> cookies;
        private Dictionary<string, postData> dictionary_0 = new Dictionary<string, postData>();
        private Dictionary<string, postData> dictionary_1 = new Dictionary<string, postData>();
        private Dictionary<string, postData> dictionary_2 = new Dictionary<string, postData>();
        private int int_0;
        public static bool isOne;
        public string refer = "";
        public static int sleepTime;
        public string upLoadUrl = "";
        public HttpVer ver;

        static uploadPic()
        {
            old_acctor_mc();
        }

        private void method_0()
        {
            http http = new http();
            if (this.ver == HttpVer.ver0)
            {
                http.Continue = false;
            }
            http.url = this.upLoadUrl;
            http.Referer = this.refer;
            http.Cookies = this.cookies;
            http.TimeOut = 0xea60;
            foreach (KeyValuePair<string, postData> pair in this.dictionary_0)
            {
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                http.PostData = pair.Value;
                string str = http.DownHtmlNotHandle();
                this.allPicHtml[pair.Key] = str;
                this.int_0++;
            }
            http = null;
        }

        private void method_1()
        {
            http http = new http();
            if (this.ver == HttpVer.ver0)
            {
                http.Continue = false;
            }
            http.Referer = this.refer;
            http.url = this.upLoadUrl;
            http.Cookies = this.cookies;
            http.TimeOut = 0xea60;
            foreach (KeyValuePair<string, postData> pair in this.dictionary_1)
            {
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                http.PostData = pair.Value;
                string str = http.DownHtmlNotHandle();
                this.allPicHtml[pair.Key] = str;
                this.int_0++;
            }
            http = null;
        }

        private void method_2()
        {
            http http = new http();
            if (this.ver == HttpVer.ver0)
            {
                http.Continue = false;
            }
            http.url = this.upLoadUrl;
            http.Referer = this.refer;
            http.Cookies = this.cookies;
            http.TimeOut = 0xea60;
            foreach (KeyValuePair<string, postData> pair in this.dictionary_2)
            {
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                http.PostData = pair.Value;
                string str = http.DownHtmlNotHandle();
                this.allPicHtml[pair.Key] = str;
                this.int_0++;
            }
            http = null;
        }

        private static void old_acctor_mc()
        {
        }

        public void startUpPic(ref int int_1)
        {
            if (isOne)
            {
                foreach (KeyValuePair<string, postData> pair in this.allPic)
                {
                    if (pair.Value.getData2().Length <= 0)
                    {
                        this.allPicHtml[pair.Key] = "";
                        this.int_0++;
                    }
                    else
                    {
                        this.dictionary_0[pair.Key] = pair.Value;
                    }
                }
            }
            else
            {
                int num = 0;
                foreach (KeyValuePair<string, postData> pair2 in this.allPic)
                {
                    if (pair2.Value.getData2().Length <= 0)
                    {
                        this.allPicHtml[pair2.Key] = "";
                        this.int_0++;
                    }
                    else
                    {
                        switch (num)
                        {
                            case 0:
                                this.dictionary_0[pair2.Key] = pair2.Value;
                                num = 1;
                                break;

                            case 1:
                                this.dictionary_1[pair2.Key] = pair2.Value;
                                num = 2;
                                break;

                            case 2:
                                this.dictionary_2[pair2.Key] = pair2.Value;
                                num = 0;
                                break;
                        }
                    }
                }
            }
            Thread thread = new Thread(new ThreadStart(this.method_0));
            thread.IsBackground = true;
            thread.Start();
            Thread thread2 = new Thread(new ThreadStart(this.method_1));
            thread2.IsBackground = true;
            thread2.Start();
            Thread thread3 = new Thread(new ThreadStart(this.method_2));
            thread3.IsBackground = true;
            thread3.Start();
            while (thread.IsAlive || (thread2.IsAlive || thread3.IsAlive))
            {
                Thread.Sleep(10);
                int_1 = this.int_0;
            }
            thread = null;
            thread2 = null;
            thread3 = null;
        }
    }
}


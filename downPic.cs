namespace client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    public class downPic
    {
        public Dictionary<string, string> allPic = new Dictionary<string, string>();
        private Dictionary<string, string> dictionary_0 = new Dictionary<string, string>();
        private Dictionary<string, string> dictionary_1 = new Dictionary<string, string>();
        private Dictionary<string, string> dictionary_2 = new Dictionary<string, string>();
        private int int_0;
        public string refer = "";
        public string savePath = "";

        private void method_0()
        {
            http http = new http();
            http.isDown = true;
            foreach (KeyValuePair<string, string> pair in this.dictionary_0)
            {
                http.url = pair.Key;
                http.Referer = this.refer;
                string str = http.DownFile(this.savePath);
                this.allPic[pair.Key] = str;
                this.int_0++;
            }
            http = null;
        }

        private void method_1()
        {
            http http = new http();
            http.isDown = true;
            http.Referer = this.refer;
            foreach (KeyValuePair<string, string> pair in this.dictionary_1)
            {
                http.url = pair.Key;
                http.Referer = this.refer;
                string str = http.DownFile(this.savePath);
                this.allPic[pair.Key] = str;
                this.int_0++;
            }
            http = null;
        }

        private void method_2()
        {
            http http = new http();
            http.isDown = true;
            http.Referer = this.refer;
            foreach (KeyValuePair<string, string> pair in this.dictionary_2)
            {
                http.url = pair.Key;
                http.Referer = this.refer;
                string str = http.DownFile(this.savePath);
                this.allPic[pair.Key] = str;
                this.int_0++;
            }
            http = null;
        }

        public void startDownPic(ref int int_1)
        {
            int num = 0;
            foreach (KeyValuePair<string, string> pair in func.getNewDic(this.allPic))
            {
                if (File.Exists(pair.Key))
                {
                    string destFileName = func.md5_hash(new StreamReader(pair.Key).BaseStream);
                    string str2 = func.正则获取(this.savePath, @"cc\(\d+)\img");
                    if (destFileName != "")
                    {
                        destFileName = func.获取图片完整路径(destFileName + Path.GetExtension(pair.Key), str2);
                        File.Copy(pair.Key, destFileName, true);
                    }
                    this.allPic[pair.Key] = destFileName;
                }
                else
                {
                    switch (num)
                    {
                        case 0:
                            this.dictionary_0[pair.Key] = pair.Value;
                            num = 1;
                            break;

                        case 1:
                            this.dictionary_1[pair.Key] = pair.Value;
                            num = 2;
                            break;

                        case 2:
                            this.dictionary_2[pair.Key] = pair.Value;
                            num = 0;
                            break;
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


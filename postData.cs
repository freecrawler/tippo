namespace client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;

    public class postData
    {
        private bool bool_0;
        private ContentType contentType_0;
        public List<byte> li;
        private string string_0;
        private string string_1;

        public postData()
        {
            this.li = new List<byte>();
            this.string_0 = "";
            this.string_1 = "------------gL6ae0Ef1ae0KM7ae0KM7GI3Ef1Ij5";
            this.contentType_0 = ContentType.urlEncoded;
            this.string_0 = "utf-8";
        }

        public postData(ContentType contentType_1)
        {
            this.li = new List<byte>();
            this.string_0 = "";
            this.string_1 = "------------gL6ae0Ef1ae0KM7ae0KM7GI3Ef1Ij5";
            this.contentType_0 = contentType_1;
            this.string_0 = "utf-8";
        }

        public postData(ContentType contentType_1, string string_2)
        {
            this.li = new List<byte>();
            this.string_0 = "";
            this.string_1 = "------------gL6ae0Ef1ae0KM7ae0KM7GI3Ef1Ij5";
            this.contentType_0 = contentType_1;
            this.string_0 = string_2;
        }

        public void addFile(string string_2)
        {
            Stream stream = new FileStream(string_2, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);
            this.li.InsertRange(this.li.Count, buffer);
        }

        public void addFile(string string_2, string string_3)
        {
            this.addFile(string_2, string_3, "application/octet-stream");
        }

        public void addFile(string string_2, string string_3, string string_4)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.string_1 + "\r\n");
            builder.Append("Content-Disposition: form-data; name=\"" + string_2 + "\"; filename=\"" + Path.GetFileName(string_3) + "\"");
            builder.Append("\r\nContent-Type: " + string_4);
            builder.Append("\r\n\r\n");
            byte[] bytes = Encoding.GetEncoding(this.string_0).GetBytes(builder.ToString());
            this.li.InsertRange(this.li.Count, bytes);
            try
            {
                Stream stream = new FileStream(string_3, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int) stream.Length);
                this.li.InsertRange(this.li.Count, buffer);
            }
            catch (Exception)
            {
            }
            string s = "\r\n";
            byte[] collection = Encoding.GetEncoding(this.string_0).GetBytes(s);
            this.li.InsertRange(this.li.Count, collection);
        }

        public void addImg(string string_2, string string_3)
        {
            this.addFile(string_2, string_3, "image/jpeg");
        }

        public void addString(string string_2)
        {
            byte[] bytes = Encoding.GetEncoding(this.string_0).GetBytes(string_2);
            this.li.InsertRange(this.li.Count, bytes);
        }

        public void addString(string string_2, string string_3)
        {
            if (this.contentType_0 == ContentType.dataFrom)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.string_1 + "\r\n");
                builder.Append("Content-Disposition: form-data; name=\"" + string_2 + "\"");
                builder.Append("\r\n\r\n");
                builder.Append(string_3 + "\r\n");
                byte[] bytes = Encoding.GetEncoding(this.string_0).GetBytes(builder.ToString());
                this.li.InsertRange(this.li.Count, bytes);
            }
            else
            {
                StringBuilder builder2 = new StringBuilder();
                if (this.li.Count > 0)
                {
                    builder2.Append("&" + string_2 + "=" + HttpUtility.UrlEncode(string_3, Encoding.GetEncoding(this.string_0)));
                }
                else
                {
                    builder2.Append(string_2 + "=" + HttpUtility.UrlEncode(string_3, Encoding.GetEncoding(this.string_0)));
                }
                byte[] collection = Encoding.GetEncoding(this.string_0).GetBytes(builder2.ToString());
                this.li.InsertRange(this.li.Count, collection);
            }
        }

        public byte[] getData()
        {
            byte[] buffer = this.getData2();
            this.li = new List<byte>();
            return buffer;
        }

        public byte[] getData2()
        {
            if (((this.li.Count > 0) && (this.contentType_0 == ContentType.dataFrom)) && !this.bool_0)
            {
                byte[] bytes = Encoding.Default.GetBytes(this.string_1 + "--\r\n");
                this.li.InsertRange(this.li.Count, bytes);
                this.bool_0 = true;
            }
            return this.li.ToArray();
        }

        public ContentType Contenttype
        {
            get
            {
                return this.contentType_0;
            }
        }

        public string Encode
        {
            get
            {
                return this.string_0;
            }
        }
    }
}


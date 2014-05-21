namespace client
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    public class handPicture
    {
        private static PixelFormat[] indexedPixelFormats;

        static handPicture()
        {
            old_acctor_mc();
        }

        public static string addWater(string string_0, string string_1, Font font_0, Color color_0, bool bool_0, float float_0, float float_1, int int_0)
        {
            string str = string_0;
            try
            {
                Image image = Image.FromFile(string_0);
                if (IsPixelFormatIndexed(image.PixelFormat))
                {
                    Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(image, 0, 0);
                    image.Save(string_0 + "1");
                    image.Dispose();
                    image = null;
                    graphics.Dispose();
                    File.Delete(string_0);
                    File.Move(string_0 + "1", string_0);
                    image = Image.FromFile(string_0);
                }
                Graphics graphics2 = Graphics.FromImage(image);
                if (bool_0)
                {
                    float_0 = (float_0 / 100f) * image.Width;
                    float_1 = (float_1 / 100f) * image.Height;
                }
                SizeF ef = graphics2.MeasureString(string_1, font_0);
                float height = ef.Height;
                float width = ef.Width;
                float num3 = 0f;
                float num4 = 0f;
                switch (int_0)
                {
                    case 0:
                        num3 = 0f;
                        num4 = 0f;
                        break;

                    case 1:
                        num3 = 0f;
                        num4 = (image.Height - height) / 2f;
                        break;

                    case 2:
                        num3 = 0f;
                        num4 = image.Height - height;
                        break;

                    case 3:
                        num3 = (image.Width - width) / 2f;
                        num4 = 0f;
                        break;

                    case 4:
                        num3 = (image.Width - width) / 2f;
                        num4 = (image.Height - height) / 2f;
                        break;

                    case 5:
                        num3 = (image.Width - width) / 2f;
                        num4 = image.Height - height;
                        break;

                    case 6:
                        num3 = image.Width - width;
                        num4 = 0f;
                        break;

                    case 7:
                        num3 = image.Width - width;
                        num4 = (image.Height - height) / 2f;
                        break;

                    case 8:
                        num3 = image.Width - width;
                        num4 = image.Height - height;
                        break;
                }
                graphics2.DrawString(string_1, font_0, new SolidBrush(color_0), (float) (num3 + float_0), (float) (num4 + float_1));
                string_0 = Path.GetDirectoryName(string_0) + @"\water" + Path.GetFileName(string_0);
                image.Save(string_0);
                image.Dispose();
                graphics2.Dispose();
                str = string_0;
            }
            catch
            {
            }
            return str;
        }

        public static string check_img(string string_0, int int_0, int int_1, int int_2, int int_3, int int_4, string string_1)
        {
            string str = "";
            if (!File.Exists(string_0))
            {
                return "图片不存在";
            }
            using (Image image = Image.FromFile(string_0))
            {
                if ((int_2 != 0) && (image.Height < int_2))
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, "图片高度必须大于", int_2, "像素," });
                }
                if ((int_1 != 0) && (image.Width < int_1))
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, "图片宽度必须大于", int_1, "像素," });
                }
                if ((int_4 != 0) && (image.Height > int_4))
                {
                    object obj4 = str;
                    str = string.Concat(new object[] { obj4, "图片高度必须小于", int_4, "像素," });
                }
                if ((int_3 != 0) && (image.Width > int_3))
                {
                    object obj5 = str;
                    str = string.Concat(new object[] { obj5, "图片宽度必须小于", int_3, "像素," });
                }
            }
            if (int_0 != 0)
            {
                using (FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
                {
                    long num = int_0 * 0x400L;
                    if (num < stream.Length)
                    {
                        object obj6 = str;
                        str = string.Concat(new object[] { obj6, "图片不能大于", int_0, "KB," });
                    }
                }
            }
            if (!string_1.ToLower().Contains("*" + Path.GetExtension(string_0).ToLower()))
            {
                str = str + "不支持图片类型,";
            }
            return str;
        }

        public static void Compress(Bitmap bitmap_0, Stream stream_0, long long_0)
        {
            ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
            Encoder quality = Encoder.Quality;
            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter parameter = new EncoderParameter(quality, long_0);
            encoderParams.Param[0] = parameter;
            bitmap_0.Save(stream_0, encoderInfo, encoderParams);
        }

        public static void Compress(Bitmap bitmap_0, string string_0, long long_0)
        {
            Stream stream = new FileStream(string_0, FileMode.Create, FileAccess.ReadWrite);
            Compress(bitmap_0, stream, long_0);
            stream.Close();
        }

        public static void Compress(Image image_0, string string_0, long long_0)
        {
            Bitmap bitmap = new Bitmap(image_0);
            Compress(bitmap, string_0, long_0);
            bitmap.Dispose();
        }

        public static void Compress(Stream stream_0, string string_0, long long_0)
        {
            Bitmap bitmap = new Bitmap(stream_0);
            Compress(bitmap, string_0, long_0);
            bitmap.Dispose();
        }

        public static void Compress(string string_0, string string_1, long long_0)
        {
            Bitmap bitmap = new Bitmap(string_0);
            Compress(bitmap, string_1, long_0);
            bitmap.Dispose();
        }

        private static ImageCodecInfo GetEncoderInfo(string string_0)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType == string_0)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }

        public static string getjpg(string string_0, string string_1)
        {
            string_1 = string_1.ToLower();
            if (!string_1.Contains("*" + Path.GetExtension(string_0).ToLower()))
            {
                string str = string_0.Replace(Path.GetExtension(string_0), ".jpg");
                Compress(string_0, str, 100L);
                string_0 = str;
            }
            return string_0;
        }

        public static string handle_img(string string_0, int int_0, int int_1, int int_2, int int_3, int int_4, string string_1)
        {
            if (!File.Exists(string_0))
            {
                return "";
            }
            string_0 = getjpg(string_0, string_1);
            int width = 0;
            int height = 0;
            Image image = Image.FromFile(string_0);
            if ((int_2 != 0) && (image.Height < int_2))
            {
                height = int_2;
            }
            else
            {
                height = image.Height;
            }
            if ((int_1 != 0) && (image.Width < int_1))
            {
                width = int_1;
            }
            else
            {
                width = image.Width;
            }
            if ((int_4 != 0) && (image.Height > int_4))
            {
                height = int_4;
            }
            if ((int_3 != 0) && (image.Width > int_3))
            {
                width = int_3;
            }
            if ((height == image.Height) && (width == image.Width))
            {
                image.Dispose();
            }
            else
            {
                MakeThumbnail(string_0, width, height);
                image.Dispose();
                File.Delete(string_0);
                File.Move(string_0 + "1", string_0);
            }
            if (int_0 != 0)
            {
                FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
                long num3 = int_0 * 0x400L;
                if (num3 < stream.Length)
                {
                    Compress(stream, string_0 + "1", 80L);
                    stream.Dispose();
                    File.Delete(string_0);
                    File.Move(string_0 + "1", string_0);
                    stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
                    if (num3 < stream.Length)
                    {
                        Compress(stream, string_0 + "1", 60L);
                        stream.Dispose();
                        File.Delete(string_0);
                        File.Move(string_0 + "1", string_0);
                    }
                    stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
                    if (num3 < stream.Length)
                    {
                        Compress(stream, string_0 + "1", 40L);
                        stream.Dispose();
                        File.Delete(string_0);
                        File.Move(string_0 + "1", string_0);
                    }
                    stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
                    if (num3 < stream.Length)
                    {
                        Compress(stream, string_0 + "1", 10L);
                        stream.Dispose();
                        File.Delete(string_0);
                        File.Move(string_0 + "1", string_0);
                    }
                    return string_0;
                }
                stream.Dispose();
            }
            return string_0;
        }

        public static string ImageTypeConversion(string string_0, string string_1)
        {
            using (FileStream stream = new FileStream(string_0, FileMode.Open))
            {
                Bitmap bitmap = new Bitmap(stream);
                string str2 = string_1;
                if (str2 != null)
                {
                    if (str2 == ".gif")
                    {
                        bitmap.Save(string_0 + "1", ImageFormat.Gif);
                    }
                    else if (str2 == ".jpg")
                    {
                        bitmap.Save(string_0 + "1", ImageFormat.Jpeg);
                    }
                    else if (!(str2 == ".bmp"))
                    {
                        if (!(str2 == ".png"))
                        {
                            goto Label_00AB;
                        }
                        bitmap.Save(string_0 + "1", ImageFormat.Png);
                    }
                    else
                    {
                        bitmap.Save(string_0 + "1", ImageFormat.Bmp);
                    }
                    goto Label_00C1;
                }
            Label_00AB:
                bitmap.Save(string_0 + "1", ImageFormat.Jpeg);
            Label_00C1:
                stream.Dispose();
                bitmap.Dispose();
            }
            File.Delete(string_0);
            string destFileName = string_0;
            if (!string_0.EndsWith(".jpg"))
            {
                destFileName = string_0.Replace(Path.GetExtension(string_0), ".jpg");
            }
            File.Move(string_0 + "1", destFileName);
            return destFileName;
        }

        private static bool IsPixelFormatIndexed(PixelFormat pixelFormat_0)
        {
            foreach (PixelFormat format in indexedPixelFormats)
            {
                if (format.Equals(pixelFormat_0))
                {
                    return true;
                }
            }
            return false;
        }

        public static void MakeThumbnail(string string_0, int int_0, int int_1)
        {
            Image image = Image.FromFile(string_0);
            int width = int_0;
            int height = int_1;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            int num7 = 0;
            int num8 = 0;
            int num9 = width;
            int num10 = height;
            double num11 = 0.0;
            if (image.Width >= image.Height)
            {
                num11 = ((double) image.Width) / ((double) int_0);
            }
            else
            {
                num11 = ((double) image.Height) / ((double) int_1);
            }
            if ((num5 <= int_0) && (num6 <= int_1))
            {
                num9 = image.Width;
                num10 = image.Height;
                num7 = Convert.ToInt32((double) ((width - num5) / 2.0));
                num8 = Convert.ToInt32((double) ((height - num6) / 2.0));
            }
            else
            {
                num9 = Convert.ToInt32((double) (((double) image.Width) / num11));
                num10 = Convert.ToInt32((double) (((double) image.Height) / num11));
                num8 = Convert.ToInt32((double) ((int_1 - num10) / 2.0));
                num7 = Convert.ToInt32((double) ((int_0 - num9) / 2.0));
            }
            Image image2 = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(ColorTranslator.FromHtml("#ffffff"));
            graphics.DrawImage(image, new Rectangle(num7, num8, num9, num10), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                string str2 = Path.GetExtension(string_0).ToLower();
                if (str2 != null)
                {
                    if (str2 == ".gif")
                    {
                        image2.Save(string_0 + "1", ImageFormat.Gif);
                    }
                    else if (str2 == ".jpg")
                    {
                        image2.Save(string_0 + "1", ImageFormat.Jpeg);
                    }
                    else if (!(str2 == ".bmp"))
                    {
                        if (!(str2 == ".png"))
                        {
                            goto Label_0201;
                        }
                        image2.Save(string_0 + "1", ImageFormat.Png);
                    }
                    else
                    {
                        image2.Save(string_0 + "1", ImageFormat.Bmp);
                    }
                    return;
                }
            Label_0201:
                image2.Save(string_0, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                exception.ToString();
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }

        private static void old_acctor_mc()
        {
            PixelFormat[] formatArray = new PixelFormat[6];
            formatArray[2] = PixelFormat.Format16bppArgb1555;
            formatArray[3] = PixelFormat.Format1bppIndexed;
            formatArray[4] = PixelFormat.Format4bppIndexed;
            formatArray[5] = PixelFormat.Format8bppIndexed;
            indexedPixelFormats = formatArray;
        }
    }
}


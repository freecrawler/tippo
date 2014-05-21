namespace client
{
    using csExWB;
    using IfacesEnumsStructsClasses;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Net;
    using System.Reflection;
    using System.Windows.Forms;

    public class sitelogin : Form
    {
        private bool bool_0;
        private bool bool_1 = true;
        private http http_0 = new http();
        private IContainer icontainer_0;
        private string string_0 = "";
        public Timer timer1;
        private upSite upSite_0;
        private cEXWB webBrowser1;

        public sitelogin()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private static List<Cookie> GetAllCookies(CookieContainer cookieContainer_0)
        {
            List<Cookie> list = new List<Cookie>();
            Hashtable hashtable = (Hashtable) cookieContainer_0.GetType().InvokeMember("m_domainTable", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, cookieContainer_0, new object[0]);
            foreach (object obj2 in hashtable.Values)
            {
                SortedList list2 = (SortedList) obj2.GetType().InvokeMember("m_list", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, obj2, new object[0]);
                foreach (CookieCollection cookies in list2.Values)
                {
                    foreach (Cookie cookie in cookies)
                    {
                        list.Add(cookie);
                    }
                }
            }
            return list;
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(sitelogin));
            this.timer1 = new Timer(this.icontainer_0);
            this.webBrowser1 = new cEXWB();
            base.SuspendLayout();
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.webBrowser1.Border3DEnabled = false;
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.DocumentSource = "<HTML><HEAD></HEAD>\r\n<BODY></BODY></HTML>";
            this.webBrowser1.DocumentTitle = "";
            this.webBrowser1.DownloadActiveX = true;
            this.webBrowser1.DownloadFrames = true;
            this.webBrowser1.DownloadImages = true;
            this.webBrowser1.DownloadJava = true;
            this.webBrowser1.DownloadScripts = true;
            this.webBrowser1.DownloadSounds = true;
            this.webBrowser1.DownloadVideo = true;
            this.webBrowser1.FileDownloadDirectory = @"C:\Users\admin\Documents\";
            this.webBrowser1.IsLeftMouseClicked = false;
            this.webBrowser1.Location = new Point(0, 0);
            this.webBrowser1.LocationUrl = "about:blank";
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ObjectForScripting = null;
            this.webBrowser1.OffLine = false;
            this.webBrowser1.RegisterAsBrowser = false;
            this.webBrowser1.RegisterAsDropTarget = false;
            this.webBrowser1.RegisterForInternalDragDrop = true;
            this.webBrowser1.ScrollBarsEnabled = true;
            this.webBrowser1.SendSourceOnDocumentCompleteWBEx = false;
            this.webBrowser1.Silent = true;
            this.webBrowser1.Size = new Size(0x1a8, 300);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.TextSize = TextSizeWB.Medium;
            this.webBrowser1.UseInternalDownloadManager = true;
            this.webBrowser1.WBDOCDOWNLOADCTLFLAG = 0x70;
            this.webBrowser1.WBDOCHOSTUIDBLCLK = DOCHOSTUIDBLCLK.DEFAULT;
            this.webBrowser1.WBDOCHOSTUIFLAG = 0x40084;
            this.webBrowser1.DocumentComplete += new DocumentCompleteEventHandler(this.webBrowser1_DocumentComplete);
            this.webBrowser1.BeforeNavigate2 += new BeforeNavigate2EventHandler(this.webBrowser1_BeforeNavigate2);
            this.webBrowser1.ProtocolHandlerOnResponse += new ProtocolHandlerOnResponseEventHandler(this.webBrowser1_ProtocolHandlerOnResponse);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a8, 300);
            base.Controls.Add(this.webBrowser1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "sitelogin";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "请登录";
            base.WindowState = FormWindowState.Maximized;
            base.FormClosing += new FormClosingEventHandler(this.sitelogin_FormClosing);
            base.FormClosed += new FormClosedEventHandler(this.sitelogin_FormClosed);
            base.Load += new EventHandler(this.sitelogin_Load);
            base.ResumeLayout(false);
        }

        private bool method_0()
        {
            try
            {
                return func.正则匹配(this.webBrowser1.GetActiveDocument().body.innerHTML, myCookie.succ, "");
            }
            catch
            {
                return false;
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://work.china.alibaba.com/home/page/sale.htm?tracelog=work_channel_sale&t=1351225987276");
        }

        private void method_2(object sender, EventArgs e)
        {
            this.bool_0 = true;
            base.Close();
        }

        private void sitelogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.webBrowser1.StopHTTPAPP();
            this.webBrowser1.StopHTTPSAPP();
            this.webBrowser1.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void sitelogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.bool_0)
            {
                if (MessageBox.Show("您确定要退出吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.webBrowser1.StopHTTPAPP();
                    this.webBrowser1.StopHTTPSAPP();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void sitelogin_Load(object sender, EventArgs e)
        {
            this.webBrowser1.RegisterAsBrowser = true;
            this.webBrowser1.Silent = true;
            this.webBrowser1.NavToBlank();
            myCookie.UserAgnet = this.webBrowser1.UserAgnet();
            this.timer1.Start();
            if (myCookie.isUseUpsite)
            {
                Type type = PzFuEX.qWFNHj(myCookie.siteId).GetType("up" + myCookie.siteId + ".upsite");
                this.upSite_0 = (upSite) Activator.CreateInstance(type);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.webBrowser1.StartHTTPAPP();
            this.webBrowser1.StartHTTPSAPP();
            this.webBrowser1.Navigate(myCookie.loginUrl);
        }

        private void webBrowser1_BeforeNavigate2(object sender, BeforeNavigate2EventArgs e)
        {
            if (((e.url.IndexOf("javascript") > -1) && (this.upSite_0 != null)) && (this.upSite_0.siteId == "25"))
            {
                e.Cancel = true;
            }
            if (this.upSite_0 != null)
            {
                string str = func.getAttrByElem(this.upSite_0.getUserNameInput(this.webBrowser1), "value");
                if (str.Trim().Length > 0)
                {
                    this.string_0 = str;
                }
            }
        }

        private void webBrowser1_DocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            if (e.url.Contains("blank") && this.bool_1)
            {
                this.webBrowser1.GetActiveDocument().body.innerHTML = "<center><br><br><br><br><br><br><font style='padding-top:100px;font-size:24px; ' id='run'>正在加载登录页面，如果长时间未出现登录页面，请按F5刷新。</font></center>";
                this.bool_1 = false;
            }
            if ((myCookie.userName.Length > 0) && (this.upSite_0 != null))
            {
                try
                {
                    this.upSite_0.getUserNameInput(this.webBrowser1).setAttribute("value", myCookie.userName, 0);
                    this.upSite_0.getUserNameInput(this.webBrowser1).setAttribute("readonly", "readonly", 0);
                }
                catch
                {
                }
            }
            if ((this.upSite_0 != null) && (this.upSite_0.siteId == "197"))
            {
                foreach (IHTMLElement element in this.webBrowser1.GetElementsByTagName(false, "A"))
                {
                    if (element.getAttribute("href", 0) != null)
                    {
                        element.getAttribute("href", 0).ToString();
                        if (element.getAttribute("href", 0).ToString().Contains("php_check.aspx?domain=sunboy2013"))
                        {
                            element.setAttribute("target", "", 0);
                        }
                    }
                }
            }
            string cookie = this.webBrowser1.GetActiveDocument().cookie;
            func.strToDic(myCookie.cookies, cookie, "([^=;]+)=([^;]*)");
            string cookieInternal = FullWebBrowserCookie.GetCookieInternal(new Uri(this.webBrowser1.LocationUrl), true);
            func.strToDic(myCookie.cookies, cookieInternal, "([^=;]+)=([^;]*)");
            if (this.method_0())
            {
                myCookie.succUrl = e.url;
                myCookie.isSucc = true;
                myCookie.userName = this.string_0;
                this.webBrowser1.StopHTTPAPP();
                this.webBrowser1.StopHTTPSAPP();
                this.bool_0 = true;
                base.Close();
            }
        }

        private void webBrowser1_ProtocolHandlerOnResponse(object sender, ProtocolHandlerOnResponseEventArgs e)
        {
            func.strToDic(myCookie.cookies, e.m_ResponseHeaders, @"Set-Cookie: ([^=;]+)=([^;\r\n]*)");
            func.strToDic(myCookie.cookies, e.m_RedirectHeaders, @"Set-Cookie: ([^=;]+)=([^;\r\n]*)");
        }
    }
}


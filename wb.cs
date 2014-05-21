namespace client
{
    using csExWB;
    using IfacesEnumsStructsClasses;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class wb : Form
    {
        private Dictionary<string, string> dictionary_0;
        private IContainer icontainer_0;
        public bool isBigPic;
        private string string_0 = "";
        private Timer timer_0;
        public cEXWB webBrowser1;

        public wb(string string_1, Dictionary<string, string> dictionary_1)
        {
            this.InitializeComponent();
            this.string_0 = string_1;
            this.dictionary_0 = dictionary_1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(wb));
            this.timer_0 = new Timer(this.icontainer_0);
            this.webBrowser1 = new cEXWB();
            base.SuspendLayout();
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
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
            this.webBrowser1.RegisterAsBrowser = true;
            this.webBrowser1.RegisterAsDropTarget = false;
            this.webBrowser1.RegisterForInternalDragDrop = true;
            this.webBrowser1.ScrollBarsEnabled = true;
            this.webBrowser1.SendSourceOnDocumentCompleteWBEx = false;
            this.webBrowser1.Silent = false;
            this.webBrowser1.Size = new Size(0x220, 0x1b4);
            this.webBrowser1.TabIndex = 11;
            this.webBrowser1.TextSize = TextSizeWB.Medium;
            this.webBrowser1.UseInternalDownloadManager = true;
            this.webBrowser1.WBDOCDOWNLOADCTLFLAG = 0x70;
            this.webBrowser1.WBDOCHOSTUIDBLCLK = DOCHOSTUIDBLCLK.DEFAULT;
            this.webBrowser1.WBDOCHOSTUIFLAG = 0x40084;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x220, 0x1b4);
            base.Controls.Add(this.webBrowser1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "wb";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "浏览器";
            base.WindowState = FormWindowState.Maximized;
            base.FormClosed += new FormClosedEventHandler(this.wb_FormClosed);
            base.Load += new EventHandler(this.wb_Load);
            base.ResumeLayout(false);
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            try
            {
                this.webBrowser1.execScript(true, "setdes(\"" + this.dictionary_0["html"].Replace(@"\", @"\\").Replace("\"", "\\\"") + "\")", "javascript");
                this.timer_0.Stop();
            }
            catch
            {
            }
        }

        private void wb_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.dictionary_0.ContainsKey("html"))
                {
                    this.dictionary_0["html"] = (string) this.webBrowser1.InvokeScript("getdes", null);
                }
            }
            catch
            {
            }
            this.timer_0.Stop();
            this.webBrowser1.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void wb_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.string_0);
            if (this.dictionary_0.ContainsKey("html") && (this.dictionary_0["html"] != ""))
            {
                this.timer_0.Start();
            }
        }
    }
}


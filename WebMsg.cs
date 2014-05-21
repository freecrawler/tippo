namespace client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WebMsg : Form
    {
        private Button button1;
        private Button button2;
        private IContainer icontainer_0;
        public static bool isClose;
        public static bool isShow;
        private Label label1;
        private Label label2;
        private Timer timer_0;
        private Timer timer_1;

        static WebMsg()
        {
            old_acctor_mc();
        }

        public WebMsg()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.label1.Text = "正在连接......";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要退出吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
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
            this.label1 = new Label();
            this.button1 = new Button();
            this.timer_0 = new Timer(this.icontainer_0);
            this.timer_1 = new Timer(this.icontainer_0);
            this.label2 = new Label();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x1d, 0x21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa1, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "对不起，您的网络出现故障。";
            this.button1.Location = new Point(0x3d, 110);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x5f, 0x24);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.timer_0.Enabled = true;
            this.timer_0.Interval = 0x7d0;
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
            this.timer_1.Enabled = true;
            this.timer_1.Tick += new EventHandler(this.timer_1_Tick);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(240, 0x7a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 12);
            this.label2.TabIndex = 2;
            this.button2.Location = new Point(0xb0, 110);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x5f, 0x23);
            this.button2.TabIndex = 3;
            this.button2.Text = "退出系统";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x151, 180);
            base.ControlBox = false;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WebMsg";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "网络无法连接";
            base.Load += new EventHandler(this.WebMsg_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private static void old_acctor_mc()
        {
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            if (DealIP.isConnect(""))
            {
                isClose = true;
            }
        }

        private void timer_1_Tick(object sender, EventArgs e)
        {
            if (isClose)
            {
                base.Close();
            }
        }

        private void WebMsg_Load(object sender, EventArgs e)
        {
            isClose = false;
        }
    }
}


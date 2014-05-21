namespace client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class IPPictuer : Form
    {
        private Button button1;
        private Button button2;
        public http hp;
        private IContainer icontainer_0;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
        private TextBox textBox1;

        public IPPictuer()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().Length == 0)
            {
                func.Message("请输入的验证码！");
                this.textBox1.Focus();
            }
            else
            {
                DealIP.RetrunVcode = this.textBox1.Text.Trim();
                base.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.method_0();
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
            this.button1 = new Button();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.pictureBox1 = new PictureBox();
            this.button2 = new Button();
            this.label1 = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.button1.Location = new Point(0x6a, 0xbd);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4a, 0x21);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.textBox1.Font = new Font("宋体", 12f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0xa3, 0x8e);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x70, 0x1a);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "hhhh";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x68, 0x97);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "验证码：";
            this.pictureBox1.Location = new Point(0xa3, 0x53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x70, 0x2f);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.button2.Location = new Point(0xc9, 0xbd);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4a, 0x21);
            this.button2.TabIndex = 7;
            this.button2.Text = "更换验证码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x16d, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "由于频繁请求，该网站暂时拒绝访问，如需继续访问，请输验证码。";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18e, 0x103);
            base.ControlBox = false;
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.button1);
            this.Cursor = Cursors.Default;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "IPPictuer";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "请输入验证码";
            base.Load += new EventHandler(this.IPPictuer_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void IPPictuer_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            this.hp.url = DealIP.picUrl;
            this.pictureBox1.Image = Image.FromStream(this.hp.GetStream());
        }
    }
}


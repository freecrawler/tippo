namespace client
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class ASDLForm : Form
    {
        private Button btnok;
        private Button button1;
        private GroupBox groupBox1;
        private IContainer icontainer_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        public string name = "";
        private Timer timer_0;
        private TextBox txtname;
        private TextBox txtpass;
        private TextBox txtuser;

        public ASDLForm()
        {
            this.InitializeComponent();
        }

        private void ASDLForm_Load(object sender, EventArgs e)
        {
            this.txtname.Text = this.name;
            this.txtuser.Text = DealIP.username;
            this.txtpass.Text = DealIP.userpass;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if ((this.txtuser.Text.Trim().Length > 0) && (this.txtpass.Text.Trim().Length > 0))
            {
                CommandDo.Execute(this.name + " /DISCONNECT");
                StringBuilder builder = new StringBuilder();
                builder.Append(this.name + " " + this.txtuser.Text.Trim() + " " + this.txtpass.Text.Trim());
                if (func.正则匹配(CommandDo.Execute(builder.ToString()), @".+\n.+\n.+", ""))
                {
                    DealIP.ADSLname = this.name;
                    DealIP.username = this.txtuser.Text.Trim();
                    DealIP.userpass = this.txtpass.Text.Trim();
                    DealIP.wirteXMLADSL();
                    base.Close();
                }
                else
                {
                    func.Message("用户名或密码错误,请重新输入！");
                }
            }
            else if (this.txtuser.Text.Trim().Length == 0)
            {
                func.Message("请填写宽带用户名!");
                this.txtuser.Focus();
            }
            else if (this.txtpass.Text.Length == 0)
            {
                func.Message("请填写宽带用户密码!");
                this.txtpass.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer_0.Enabled = true;
            this.label5.Visible = true;
            try
            {
                Process.Start("rasphone.exe", "-d " + this.name);
            }
            catch (Exception)
            {
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
            this.btnok = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtuser = new TextBox();
            this.txtpass = new TextBox();
            this.label3 = new Label();
            this.txtname = new TextBox();
            this.button1 = new Button();
            this.label4 = new Label();
            this.label5 = new Label();
            this.timer_0 = new Timer(this.icontainer_0);
            this.label6 = new Label();
            this.groupBox1 = new GroupBox();
            this.label7 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnok.Location = new Point(0xe1, 0xa8);
            this.btnok.Name = "btnok";
            this.btnok.Size = new Size(0x6c, 0x23);
            this.btnok.TabIndex = 0;
            this.btnok.Text = "保存";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new EventHandler(this.btnok_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x4f, 0x8d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x4f, 0xb7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密  码：";
            this.txtuser.Location = new Point(0xb1, 0x84);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new Size(0xa3, 0x15);
            this.txtuser.TabIndex = 3;
            this.txtpass.Location = new Point(0xb1, 0xae);
            this.txtpass.Name = "txtpass";
            this.txtpass.PasswordChar = '*';
            this.txtpass.Size = new Size(0xa3, 0x15);
            this.txtpass.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x4b, 0x5f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "宽带连接名称：";
            this.txtname.Location = new Point(0xb1, 90);
            this.txtname.Name = "txtname";
            this.txtname.ReadOnly = true;
            this.txtname.Size = new Size(0xa3, 0x15);
            this.txtname.TabIndex = 7;
            this.button1.Location = new Point(0x10c, 0x135);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x6c, 0x23);
            this.button1.TabIndex = 8;
            this.button1.Text = "打开宽带连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.ForeColor = Color.Red;
            this.label4.Location = new Point(0x6f, 0x16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(14, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = " ";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label5.Location = new Point(230, 0x165);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xbf, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "如果网络连接上,本窗口自动关闭！";
            this.label5.Visible = false;
            this.timer_0.Interval = 0x3e8;
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x29, 0x16);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x185, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "由于频繁请求，该网站暂时拒绝访问，如需继续访问，请重新连接宽带。\r\n";
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Location = new Point(0x2b, 0x3a);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x163, 230);
            this.groupBox1.TabIndex = 0x10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置下列信息，以便程序自动进行宽带连接";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x29, 320);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0xc5, 12);
            this.label7.TabIndex = 0x11;
            this.label7.Text = "如果您不输入宽带信息，请手动连接";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1bf, 0x191);
            base.ControlBox = false;
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.txtname);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtpass);
            base.Controls.Add(this.txtuser);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ASDLForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "宽带连接";
            base.Load += new EventHandler(this.ASDLForm_Load);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            if (DealIP.isConnect(""))
            {
                base.Close();
            }
        }
    }
}


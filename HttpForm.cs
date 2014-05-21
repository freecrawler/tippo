namespace client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class HttpForm : Form
    {
        private Button bntadd;
        private Button btndelete;
        private Button btnOK;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private IContainer icontainer_0;
        private int int_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listViewProxy;
        private TextBox txthost;
        private TextBox txtport;

        public HttpForm()
        {
            this.InitializeComponent();
        }

        private void bntadd_Click(object sender, EventArgs e)
        {
            if (this.txthost.Text.Trim().Length == 0)
            {
                func.Message("请输入中的代理服务器地址!");
                this.txthost.Focus();
            }
            else if (this.txtport.Text.Trim().Length == 0)
            {
                func.Message("请输入中的代理服务器端口!");
                this.txtport.Focus();
            }
            else if (!func.正则匹配(this.txtport.Text.Trim(), @"\d+", ""))
            {
                func.Message("请输入正确的端口!");
                this.txtport.Focus();
            }
            else
            {
                string str = this.txthost.Text.Trim() + ":" + this.txtport.Text.Trim();
                ListViewItem item = new ListViewItem(new string[] { str });
                this.listViewProxy.Items.Add(item);
                this.txthost.Text = "";
                DealIP.JaProxy.Add(str);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (this.listViewProxy.SelectedItems.Count > 0)
            {
                DealIP.JaProxy.RemoveAt(this.int_0);
                this.listViewProxy.Items.RemoveAt(this.int_0);
            }
            else
            {
                func.Message("请选择一项！");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DealIP.isProxy = false;
            if (!this.checkBox1.Checked)
            {
                DealIP.wirteXML();
                base.Close();
            }
            else if (DealIP.JaProxy.Count <= 0)
            {
                func.Message("请添加新的代理服务器地址和端口！");
            }
            else
            {
                for (int i = 0; i < DealIP.JaProxy.Count; i++)
                {
                    if (!DealIP.JaProxy[i].ToString().Contains("已用"))
                    {
                        DealIP.Proxyindex = i;
                        DealIP.isProxy = true;
                        DealIP.JaProxy[i] = DealIP.JaProxy[i] + " 已用";
                        DealIP.wirteXML();
                        base.Close();
                        return;
                    }
                }
                func.Message("请添加新的代理服务器地址和端口！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listViewProxy.Items.Clear();
            DealIP.JaProxy.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "退出系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void HttpForm_Load(object sender, EventArgs e)
        {
            DealIP.isProxy = false;
            DealIP.Proxyindex = 0;
            this.method_0();
            this.method_1();
        }

        private void InitializeComponent()
        {
            this.btnOK = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txthost = new TextBox();
            this.txtport = new TextBox();
            this.checkBox1 = new CheckBox();
            this.listViewProxy = new ListView();
            this.bntadd = new Button();
            this.btndelete = new Button();
            this.button1 = new Button();
            this.button2 = new Button();
            this.label3 = new Label();
            this.label4 = new Label();
            base.SuspendLayout();
            this.btnOK.Location = new Point(0x19, 0x1de);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x6c, 0x22);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xfb, 0x86);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "地址";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xfb, 0xb2);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            this.txthost.Location = new Point(0x137, 0x83);
            this.txthost.Name = "txthost";
            this.txthost.Size = new Size(0x6c, 0x15);
            this.txthost.TabIndex = 3;
            this.txtport.Location = new Point(0x137, 0xa9);
            this.txtport.Name = "txtport";
            this.txtport.Size = new Size(0x6c, 0x15);
            this.txtport.TabIndex = 4;
            this.txtport.Text = "80";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x19, 0x19b);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x6c, 0x10);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "启用代理服务器";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.listViewProxy.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listViewProxy.GridLines = true;
            this.listViewProxy.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewProxy.Location = new Point(0x19, 0x63);
            this.listViewProxy.MultiSelect = false;
            this.listViewProxy.Name = "listViewProxy";
            this.listViewProxy.Size = new Size(0xd1, 0x11a);
            this.listViewProxy.TabIndex = 7;
            this.listViewProxy.UseCompatibleStateImageBehavior = false;
            this.listViewProxy.View = View.Details;
            this.listViewProxy.SelectedIndexChanged += new EventHandler(this.listViewProxy_SelectedIndexChanged);
            this.bntadd.Location = new Point(0x101, 0xd8);
            this.bntadd.Name = "bntadd";
            this.bntadd.Size = new Size(0x4e, 30);
            this.bntadd.TabIndex = 8;
            this.bntadd.Text = "新增";
            this.bntadd.UseVisualStyleBackColor = true;
            this.bntadd.Click += new EventHandler(this.bntadd_Click);
            this.btndelete.Location = new Point(0x101, 0x11b);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new Size(80, 0x20);
            this.btndelete.TabIndex = 9;
            this.btndelete.Text = "删除";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new EventHandler(this.btndelete_Click);
            this.button1.Location = new Point(0x101, 0x15d);
            this.button1.Name = "button1";
            this.button1.Size = new Size(80, 0x20);
            this.button1.TabIndex = 11;
            this.button1.Text = "全部删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0xb6, 0x1de);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x62, 0x23);
            this.button2.TabIndex = 12;
            this.button2.Text = "退出系统";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x17, 540);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x16d, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "如果您要手动更换IP(比如重启路由器)，换掉IP后直接点确定即可。";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x17, 20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xf5, 0x24);
            this.label4.TabIndex = 14;
            this.label4.Text = "由于频繁请求，该网站暂时拒绝访问，\r\n\r\n如需继续访问，请设置代理服务器或更换IP。\r\n";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1cf, 0x242);
            base.ControlBox = false;
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btndelete);
            base.Controls.Add(this.bntadd);
            base.Controls.Add(this.listViewProxy);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.txtport);
            base.Controls.Add(this.txthost);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnOK);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HttpForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "代理服务器配置";
            base.Load += new EventHandler(this.HttpForm_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listViewProxy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewProxy.SelectedItems.Count > 0)
            {
                this.int_0 = this.listViewProxy.SelectedItems[0].Index;
            }
        }

        private void method_0()
        {
            this.listViewProxy.GridLines = true;
            this.listViewProxy.FullRowSelect = true;
            this.listViewProxy.View = View.Details;
            this.listViewProxy.Scrollable = true;
            this.listViewProxy.MultiSelect = false;
            this.listViewProxy.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewProxy.Columns.Add("地址:端口", 200, HorizontalAlignment.Center);
        }

        private void method_1()
        {
            foreach (string str in (IEnumerable<JToken>) DealIP.JaProxy)
            {
                if (!(str == ""))
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = str;
                    this.listViewProxy.Items.Add(item);
                }
            }
        }
    }
}


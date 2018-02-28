using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;//为了使用正则表达式

using StusContactE;
using StusContactB;

namespace 个人通讯录
{
    public partial class UserLogin_From : Form
    {
        //当用户输入用户名的时候检测是否存在该用户。当文本框一失去焦点的时候验证。
        //当用户输入密码的时候检测用户密码是否大于5位
        //验证用户两次输入的密码是否一致
        private Image img;
        public string imgPath;
        public UserLogin_From()
        {
            InitializeComponent();
            //初始化控件背景颜色使之透明化
            label1.BackColor = Color.FromArgb(222, 241, 255);
            label2.BackColor = Color.FromArgb(222, 241, 255);
            label3.BackColor = Color.FromArgb(222, 241, 255);
            label4.BackColor = Color.FromArgb(222, 241, 255);
            checkBox1.BackColor = Color.FromArgb(222, 241, 255);
            checkBox2.BackColor = Color.FromArgb(222, 241, 255);
            pictureBox1.BackColor = Color.FromArgb(222, 241, 255);
            pictureBox4.BackColor = Color.FromArgb(222, 241, 255);
            pictureBox5.BackColor = Color.FromArgb(222, 241, 255);
            pictureBox6.BackColor = Color.FromArgb(222, 241, 255);

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            登录 dl = new 登录();
            this.Hide();
            dl.Show();
        }

        //开始注册用户，即操作数据库。
        private void btnLogin_Click(object sender, EventArgs e)
        {

            ///////加上用户输入验证
            if (txtUserName.Text == "")
                MessageBox.Show("请输入用户名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtUserPwd_1.Text == "")
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtUserPwd_2.Text == "")
                MessageBox.Show("请输入密码确认", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                UserInfoE ue = new UserInfoE();
                ue.UserName = txtUserName.Text.Trim();
                ue.UserPwd = txtUserPwd_1.Text.Trim();
                ue.UserImg1 = img;
                StuContact_B scb = new StuContact_B();
                if (scb.addUser(ue, imgPath))
                {
                    MessageBox.Show("恭喜您，注册成功，请使用当前注册的用户登录系统（请使用“普通用户”权限登录）", "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    登录 dl = new 登录();
                    dl.Show();
                }
                else
                {
                    MessageBox.Show("注册失败！", "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void userLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            登录 dl = new 登录();
            this.Hide();
            dl.Show();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            lbUserName.Text = "";
            #region 正则表达式来检测用户注册的名称有没有汉字
            //正则表达式来检测用户注册的名称有没有汉字
            Boolean bl = false;
            Regex rg = new Regex("^[\u4E00-\u9FA5]{0,}$");//正则表达式来匹配是否包含汉字
            for (int i = 0; i < txtUserName.Text.Length; i++)
            {
                bl = rg.IsMatch(txtUserName.Text[i].ToString());
            }
            if (bl)
            {
                lbUserName.ForeColor = Color.Red;
                lbUserName.Text = "不能有汉字";
                pictureBox4.Image = imageList1.Images[0];
            }
            else
                lbUserName.ForeColor = Color.FromArgb(13, 195, 33);

            #endregion
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtUserPwd_1.PasswordChar = new char();
            else
                txtUserPwd_1.PasswordChar = '*';
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                txtUserPwd_2.PasswordChar = new char();
            else
                txtUserPwd_2.PasswordChar = '*';
        }

        private void txtUserPwd_2_TextChanged(object sender, EventArgs e)
        {
            if (txtUserPwd_1.Text.Trim() != txtUserPwd_2.Text.Trim())
            {
                lbUserPwd_2.ForeColor = Color.Red;
                lbUserPwd_2.Visible = true;
                lbUserPwd_2.Text = "两次密码输入不一致";
                pictureBox6.Image = imageList1.Images[0];
            }
            else
            {
                lbUserPwd_2.Visible = true;
                lbUserPwd_2.ForeColor = Color.FromArgb(13, 195, 33);
                lbUserPwd_2.Text = "密码通过,可以使用";
                pictureBox6.Image = imageList1.Images[1];
            }
        }

        private void txtUserPwd_1_TextChanged(object sender, EventArgs e)
        {
            if (txtUserPwd_1.Text.Length < 5)
            {
                lbUserPwd_1.ForeColor = Color.Red;
                lbUserPwd_1.Visible = true;
                lbUserPwd_1.Text = "密码不能小于五位";
                pictureBox5.Image = imageList1.Images[0];
            }
            else
            {
                lbUserPwd_1.Visible = false;
                lbUserPwd_1.ForeColor = Color.FromArgb(13, 195, 33);
                lbUserPwd_1.Text = "密码可以使用";
                pictureBox5.Image = imageList1.Images[1];
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图片|*.jpg;*.jpeg;*.bmp;*.gif;*.png";
            openFileDialog1.Title = "选择头像";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(openFileDialog1.FileName);
                imgPath = openFileDialog1.FileName;
                pictureBox1.Image = img;
            }
        }

        #region 开始动态验证用户是否在数据库已经存在
        private void timer1_Tick(object sender, EventArgs e)
        {
            //如果用户名输入框失去焦点之后则开始验证
            if (!txtUserName.Focused)
            {
                UserInfoE ue = new UserInfoE();
                ue.UserName = txtUserName.Text.Trim();
                StuContact_B scb = new StuContact_B();

                if (txtUserName.Text.Trim() != "")
                {
                    lbUserName.Visible = true;
                    if (scb.userIsExit_B(ue))
                    {
                        lbUserName.ForeColor = Color.Red;
                        lbUserName.Text = "该用户名已经存在，请重新输入";
                        pictureBox4.Image = imageList1.Images[0];
                    }
                    else if (txtUserName.Text.Equals("system") || txtUserName.Text.Equals("sa") || txtUserName.Text.Equals("root") || txtUserName.Text.Equals("SYSTEM") || txtUserName.Text.Equals("SA") || txtUserName.Text.Equals("ROOT"))
                    {
                        lbUserName.ForeColor = Color.Red;
                        lbUserName.Text = "对不起，无法使用保留名称";
                        pictureBox4.Image = imageList1.Images[0];
                    }
                    else
                    {
                        lbUserName.ForeColor = Color.FromArgb(13, 195, 33);
                        lbUserName.Text = "该用户名可以使用";
                        pictureBox4.Image = imageList1.Images[1];
                    }
                }
                else
                {
                    lbUserName.Text = "";
                    lbUserName.Visible = false;
                }

                //实时检测用户所有输入是否合法，只有合法之后才会显示‘用户选择头像’
                if (lbUserName.Text == "该用户名可以使用" && lbUserPwd_1.Text != "密码不能小于五位" && lbUserPwd_2.Text == "密码通过,可以使用")
                {
                    label4.Visible = true;
                    pictureBox1.Visible = true;
                }
                else
                {
                    label4.Visible = false;
                    pictureBox1.Visible = false;
                }
            }
        }
        #endregion

        #region 移动窗体
        Boolean isMove = false;
        int x0, y0;
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
                x0 = e.X;
                y0 = e.Y;
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMove)
            {
                int dx = e.X - x0;
                int dy = e.Y - y0;
                this.Location = new Point(this.Left + dx, this.Top + dy);
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }
        #endregion
    }
}

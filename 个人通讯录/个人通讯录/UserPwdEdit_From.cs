using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StusContactB;

        
namespace 个人通讯录
{
    public partial class UserPwdEdit_From : Form
    {
        public UserPwdEdit_From()
        {
            InitializeComponent();
        }

        MainFormTools_B mftb = new MainFormTools_B();
        public  string open_pwd;//记录数据库启动密码，即老密码
        public  string new_pwd;//记录新密码

        private void button1_Click(object sender, EventArgs e)
        {
            string currentUser = 登录.currentUser;

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入原密码","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入新密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                open_pwd = textBox1.Text.Trim();
                new_pwd = textBox2.Text.Trim();
                if (mftb.editUserPwd_B(currentUser, open_pwd, new_pwd) == 0)
                {
                    MessageBox.Show("密码修改失败！");
                    this.Hide();
                }
                else if (mftb.editUserPwd_B(currentUser, open_pwd, new_pwd) == 1)
                {
                    MessageBox.Show("密码修改成功,下次请使用该密码登录软件", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
                //else
                //{
                //    MessageBox.Show("输入的原密码不正确，请重新输入原密码", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    textBox1.Text = "";
                //    textBox2.Text = "";
                //}
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}

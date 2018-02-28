using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StusTools;


namespace 个人通讯录
{
    public partial class InputDBPwd_Form : Form
    {
        public InputDBPwd_Form()
        {
            InitializeComponent();
        }
        public static Boolean flag = false;
        public static string open_pwd;//记录数据库启动密码，即老密码
        public static string new_pwd;//记录新密码

        private void button1_Click(object sender, EventArgs e)
        {
            open_pwd = textBox1.Text.Trim();
            new_pwd = textBox2.Text.Trim();
            this.Hide();
            flag = true;

            string old_password = InputDBPwd_Form.open_pwd;
            string new_password = InputDBPwd_Form.new_pwd;



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                //修改配置文件，标志数据库密码被修改过
                if (checkBox1.Checked)
                {
                    config.write_DBEdited(false, textBox1.Text.Trim());
                }
                else
                {
                    config.write_DBEdited(true, textBox1.Text.Trim());
                }
            }
            else
            {
                checkBox1.Checked = false;
                MessageBox.Show("请先将密码密码输入完成之后再勾选", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            flag = false;
        }

        private void inputDBPwd_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            this.BackgroundImage = imageList1.Images[0];
            if (Main_Form.flag_pwd == 0)
            {
                label3.Visible = false;
                textBox2.Visible = false;
                checkBox1.Visible = true;
            }
            else
            {
                label3.Visible = true;
                textBox2.Visible = true;
                checkBox1.Visible = false;
            }
            this.Show();
            textBox1.Focus();
        }


    }
}

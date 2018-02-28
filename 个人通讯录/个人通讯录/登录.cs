using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;//读取ini用

using StusContactB;
using StusContactE;
using System.Data.OleDb;
using System.IO;
using StusTools;

namespace 个人通讯录
{
    public partial class 登录 : Form
    {
        public static string currentUser;//记录当前登录账户
        StuContact_B scb = new StuContact_B();
        DialogResult result;

        

        public 登录()
        {
            InitializeComponent();
            label1.BackColor = Color.FromArgb(222, 241, 255);
            label2.BackColor = Color.FromArgb(222, 241, 255);
            label3.BackColor = Color.FromArgb(222, 241, 255);
            ckAutoLogin.BackColor = Color.FromArgb(222, 241, 255);
            ckBoxRembPwd.BackColor = Color.FromArgb(222, 241, 255);
            this.Height = 0;
            this.Width = 411;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void 登录_Load(object sender, EventArgs e)
        {
            #region 检测必要文件的完整性
            int flag = scb.checkFile_B();
            if (flag == 1)
            {
                result = MessageBox.Show("检测到img文件夹下的图片有部分丢失或者文件名改变，这将会导致部分没有头像的联系人的默认头像丢失，是否继续？", "异常提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                    Application.Exit();
            }
            else if (flag == 0)
            {
                result = MessageBox.Show("检测到img文件夹下的图片丢失或者文件名改变，这将会导致没有头像的联系人的默认头像丢失，是否继续？", "异常提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                    Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + @"\source\StuContact.mdb"))
            {
                result = MessageBox.Show("系统检测到数据库已经丢失，无法启动软件。请点击“确定”来重建数据库", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    CreateDB cdb = new CreateDB();
                    cdb.creatDatabase();
                }
                else
                {
                    Application.Exit();
                }
            }
            #endregion
            
            this.Show();
            combUserName.Focus();
            this.Opacity = 0;
            timer1.Interval = Convert.ToInt32(config.readConfig_ReadPwd_dlDH());
            timer2.Interval = Convert.ToInt32(config.readConfig_ReadPwd_dlDH()) ;
            timer1.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (combUserName.Text.Trim() == "")
            {
                MessageBox.Show("请将用户名输入完整！", "登陆提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                combUserName.Focus();
            }
            else if (txtUserPwd.Text == "")
            {
                MessageBox.Show("请将密码输入完整！", "登陆提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserPwd.Focus();
            }
            else if (comboBox1.Text == "选择登录权限")
                MessageBox.Show("选择登录身份！", "登陆提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            
            else
            {
                UserInfoE ue = new UserInfoE();
                ue.UserName = combUserName.Text.Trim();
                ue.UserPwd = txtUserPwd.Text.Trim();
                if (scb.userExit(ue))
                {
                    currentUser = combUserName.Text;//记录当前登录用户，用于在主窗体显示
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    Loding_Form ldf = new Loding_Form();
                    ldf.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("对不起，登录失败！没有此用户或者密码不正确，请重新登录！", "登陆提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    combUserName.Text = "";
                    txtUserPwd.Text = "";
                    combUserName.Focus();
                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            UserLogin_From ul = new UserLogin_From();
            this.Hide();
            ul.Show();
        }

        private void 登录_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            Application.Exit();
        }


        private void comboBox1_MouseMove(object sender, MouseEventArgs e)
        {
            comboBox1.ForeColor = Color.Black;
        }

        //自动检测数据库里面是否有该用户的头像，有的话则自动加载
        string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());//config.readConfig_ReadPwd()读取加密的配置文件中的密码
        private void combUserName_TextUpdate(object sender, EventArgs e)
        {

            UserInfoE ue = new UserInfoE();
            ue.UserName = combUserName.Text.Trim();

            if (scb.UserIsImg_B(ue))
            {
                byte[] imagebytes = null;
                using (OleDbConnection olconn = new OleDbConnection(connStr))
                {
                    olconn.Open();
                    string sql = string.Format("select User_Img from UserInfo where User_Name='{0}'", combUserName.Text.Trim());
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                    OleDbDataReader oldr = olcmd.ExecuteReader();
                    while (oldr.Read())
                    {
                        imagebytes = (byte[])oldr.GetValue(0);
                    }
                    if (imagebytes != null)
                    {
                        MemoryStream ms = new MemoryStream(imagebytes);
                        pictureBox1.Image = new Bitmap(ms);

                    }
                }
            }
            else
                pictureBox1.Image = null;
        }

        #region 启动渐显过渡动画
        double _opacity = 0.05;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += _opacity;
            timer2.Start();
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }
        //405, 277
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Height < 277)
            {
                this.Height += 30;
            }
            else
            {
                timer2.Stop();
            }
        }
        #endregion

        //读写配置文件
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //WritePrivateProfileString(sect.Text, key.Text, val.Text, Application.StartupPath + "\\peizhi.ini");
        //StringBuilder stringBud = new StringBuilder(50);
        //GetPrivateProfileString("JXCDB", "name", "没有数据", stringBud, 50, Application.StartupPath + "\\peizhi.ini");
        //此时所读取的server键对应的值已被保存在stringBud中，只需:
        //val.Text=stringBud.ToString();
        private void ckBoxRembPwd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (combUserName.Text != "" || txtUserPwd.Text != "" || comboBox1.Text != "选择登录权限")
            {

            }
            else
            {

            }
        }

        #region 移动无标题栏的窗体
        Boolean isMove = false;
        int x0, y0;
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
                x0 = e.X;
                y0 = e.Y;
            }
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMove)
            {
                int dx = e.X - x0;//想一下为什么是用“-”而不是用“+”？很简单，就是为了计算出鼠标移动的距离
                //首先x0记录了鼠标第一次点击时的位置，e.X，e.Y是鼠标目前的位置，利用e.X和
                //e.Y减去原来的x0,y0就是鼠标移动过的距离，然后将这个距离应用到窗体的left和top属性即可
                int dy = e.Y - y0;
                this.Location = new Point(this.Left + dx, this.Top + dy);
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }
        #endregion
    }
}

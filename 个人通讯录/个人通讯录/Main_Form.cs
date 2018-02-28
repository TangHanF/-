using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;
using StusContactB;
using StusContactD;
using StusTools;
using StusContactE;
using System.Runtime.InteropServices;
using Paway.Windows.Forms;//API函数用到
namespace 个人通讯录
{
    public partial class Main_Form : _360Form
    {
        #region 窗体动画用到
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        public const Int32 AW_CENTER = 0x00000010;
        public const Int32 AW_HIDE = 0x00010000;
        public const Int32 AW_ACTIVATE = 0x00020000;
        public const Int32 AW_SLIDE = 0x00040000;
        public const Int32 AW_BLEND = 0x00080000;
        //调用API函数
        [DllImportAttribute("user32.dll")]
        //参数1：窗体句柄  参数2：持续时间   参数3：:动画类型
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        #endregion

        private string userSelectedItem;//在搜索时记录用户选择的搜索依据
        StuContact_B scb = new StuContact_B();
        ContactUsers cus = new ContactUsers();
        StuContact_D scd = new StuContact_D();
        MainFormTools_B mftb = new MainFormTools_B();
        MainFormTools_D mftd = new MainFormTools_D();
        
        config cfg = new config();

        private Image img;


        public Main_Form()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);//开启双缓冲，在绘制界面时防止闪烁
            if (config.readConfig_zjmOpenEffect() == "on")//从配置文件里面读取配置，即读取用户是否开启了动画效果
            {
                int time;
                time = Convert.ToInt32(config.readConfig_ReadPwd_zjmDH()) * 10;
                AnimateWindow(this.Handle, time, AW_SLIDE + AW_VER_NEGATIVE);//开始窗体动画
            }

        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (config.readConfig_zjmCloseEffect() == "on")
                AnimateWindow(this.Handle, 1000, AW_SLIDE + AW_VER_NEGATIVE + AW_HIDE);//结束窗体动画
            Application.Exit();
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void mainForm_Load(object sender, EventArgs e)
        {
            //初始化背景图
            this.BackgroundImage = imageList1.Images[2];
            groupBox1.BackgroundImage = imageList1.Images[2];
            groupBox2.BackgroundImage = imageList1.Images[2];
            groupBox4.BackgroundImage = imageList1.Images[2];
            tabPage1.BackgroundImage = imageList1.Images[2];
            tabPage2.BackgroundImage = imageList1.Images[2];
            panel1.BackgroundImage = imageList1.Images[2];
            menuStrip1.BackgroundImage = imageList1.Images[2];
            toolStrip1.BackgroundImage = imageList1.Images[2];
            statusStrip1.BackgroundImage = imageList1.Images[2];

            //状态栏显示数据库大小
            mftb.refresh_DB(toolStripStatusLabel7);
            //接下来开始读取数据库的组到列表框。初始化
            scb.initComboBox_B(comboxGroup);

            //计算出联系人数
            toolStripStatusLabel1.Text = "当前联系人个数：" + scb.countUsers_B().ToString() + "人" + "       ";

            //显示当前登录用户
            toolStripStatusLabel5.Text = "      当前登录用户:" + 登录.currentUser + "     ";

            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(128, 126);

            

            //锁定复选框选中
            cbLock.Checked = true;
        }

        private void 编辑分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGroup_Form ag = new AddGroup_Form();
            ag.ShowDialog();
        }

        //新增联系人按钮
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddUsers_Form eu = new AddUsers_Form();
            eu.ShowDialog();
            mftb.refresh_DB(toolStripStatusLabel7);//刷新数据库大小显示
        }

        //状态栏显示时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            toolStripStatusLabel3.Text = dt.ToString();

        }

        InputDBPwd_Form pwdForm = new InputDBPwd_Form();//密码输入窗体

        public static int flag_pwd;//用来标志是压缩数据库的时候出现密码输入框还是修改数据库密码的时候出现输入框。
        private void 压缩数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag_pwd = 0;
            pwdForm.ShowDialog();
            if (InputDBPwd_Form.flag)
                mftb.compressDB_B(toolStripStatusLabel7, InputDBPwd_Form.open_pwd);
        }
        private void 修改数据库密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag_pwd = 1;
            pwdForm.ShowDialog();
            mftb.editDBPwd_B(InputDBPwd_Form.open_pwd, InputDBPwd_Form.new_pwd);

        }

        //选择分组展开分组联系人
        private void comboxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //读取配置文件数据，判断数据库密码被修改过，修改过则提示用户输入密码
            if (config.read_DBEdited() == "yes")
            {
                flag_pwd = 0;//标识要显示的窗体
                pwdForm.ShowDialog();
                scb.addListUsers_B(listBox1, comboxGroup.SelectedItem.ToString(), true, InputDBPwd_Form.open_pwd);////出现的问题：inputDBPwd.new_pwd获取不到值-----已解决。不能是inputDBPwd.new_pwd,而应该是inputDBPwd.open_pwd
            }
            else//否则直接使用原来的密码
                scb.addListUsers_B(listBox1, comboxGroup.SelectedItem.ToString(), false, config.readConfig_ReadPwd());


            if (listBox1.Items.Count == 0)
            {
                toolStripButton2.Enabled = false;//设置 修改 按钮不可用
                toolStripButton3.Enabled = false;//设置 删除 按钮不可用
                pictureBox1.Image = null;

                lbSelectImg.Visible = false;//“选择头像”标签不可见
                txtName.Text = ""; txtAge.Text = ""; txtBirth.Text = ""; txtPhoneNum.Text = ""; txtQQ.Text = ""; txtCompany.Text = ""; txtAddress.Text = ""; txtRemark.Text = "";
            }
            else
            {
                listBox1.SelectedIndex = 0;//设置默认选中第一个联系人
                toolStripButton3.Enabled = true;//设置 删除 按钮可用
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //展开联系人详细信息

            MainFormTools_D mft = new MainFormTools_D();
            if (tabControl1.SelectedIndex == 0)
            {
                mft.UserDetail_D(listBox1.SelectedItem.ToString(), txtAddress, txtAge, txtBirth, txtBoxFind, txtCompany, txtName, txtPhoneNum, txtQQ, txtRemark, radioButtonSex1, radioButtonSex2, imageList1, pictureBox1);
            }
            else
            {
                mft.UserDetail_D(listBoxUsers.SelectedItem.ToString(), txtAddress, txtAge, txtBirth, txtBoxFind, txtCompany, txtName, txtPhoneNum, txtQQ, txtRemark, radioButtonSex1, radioButtonSex2, imageList1, pictureBox1);
            }
            try
            {
                if (listBox1.SelectedItem.ToString() != "")
                {
                    toolStripButton2.Enabled = true;
                }
                else
                    toolStripButton2.Enabled = false;
            }
            catch (Exception)
            {

            }
        }

        private void 关于软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutSoftware_From().Show();
        }


        private void comboxGroup_MouseEnter(object sender, EventArgs e)
        {
            comboxGroup.ForeColor = Color.Black;
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            comboBox1.ForeColor = Color.Black;
        }

        //搜索用户
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxUsers.Items.Clear();
            if (txtBoxFind.Text != "")
            {
                userSelectedItem = comboBox1.SelectedItem.ToString();
                MainFormTools_D mft = new MainFormTools_D();
                mft.findUsers(userSelectedItem, txtBoxFind.Text.Trim(), listBoxUsers);
            }
        }

        private void txtBoxFind_TextChanged(object sender, EventArgs e)
        {
            listBoxUsers.Items.Clear();
            if (comboBox1.Text != "选择依据...")
            {
                userSelectedItem = comboBox1.SelectedItem.ToString();
                MainFormTools_D mft = new MainFormTools_D();
                mft.findUsers(userSelectedItem, txtBoxFind.Text.Trim(), listBoxUsers);
            }
            else
                listBoxUsers.Items.Clear();
        }

        private void 清空列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxUsers.Items.Clear();
            listBox1.Items.Clear();
            toolStripButton2.Enabled = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolFangQi.Visible = true;
            if (toolStripButton2.Text == "修改")
            {
                toolStripButton2.Text = "确定";
                lbSelectImg.Visible = true;
                //控件可用性、可编辑性打开
                txtName.ReadOnly = false;
                radioButtonSex1.Enabled = false;
                radioButtonSex2.Enabled = false;
                //txtRemark.Enabled = true;
                txtAge.ReadOnly = false;
                txtBirth.ReadOnly = false;
                txtPhoneNum.ReadOnly = false;
                txtQQ.ReadOnly = false;
                txtCompany.ReadOnly = false;
                txtAddress.ReadOnly = false;
                txtRemark.ReadOnly = false;
                cbLock.Checked = false;
            }
            else//相当于保存的作用
            {
                toolStripButton2.Text = "修改";
                lbSelectImg.Visible = false;
                toolFangQi.Visible = false;


                //开始更新信息
                cus.User_name = txtName.Text.Trim();
                string id = scd.getUserID(listBox1.SelectedItem.ToString());//获取被修改联系人的ID编号
                if (radioButtonSex1.Checked)
                    cus.User_sex = "男";
                else
                    cus.User_sex = "女";

                cus.User_age = txtAge.Text.Trim();
                cus.User_birth = txtBirth.Text.Trim();
                cus.User_phone = txtPhoneNum.Text.Trim();
                cus.User_qq = txtQQ.Text.Trim();
                cus.User_address = txtAddress.Text.Trim();
                cus.User_company = txtCompany.Text.Trim();
                cus.User_remark = txtRemark.Text.Trim();

                mftb.addUserorUpdateUser_B(cus, id);//执行修改

                lbSelectImg.Visible = false;
                txtName.ReadOnly = true;
                radioButtonSex1.Enabled = false;
                radioButtonSex2.Enabled = false;
                txtAge.ReadOnly = true;
                txtBirth.ReadOnly = true; ;
                txtPhoneNum.ReadOnly = true;
                txtQQ.ReadOnly = true;
                txtCompany.ReadOnly = true;
                txtAddress.ReadOnly = true;
                txtRemark.ReadOnly = true;
                cbLock.Checked = true;

                mftb.refresh_DB(toolStripStatusLabel7);//刷新数据库大小显示
            }

        }


        private void 重构数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("您确定要重构数据库吗?此操作将会清空数据库的所有数据！数据无价，谨慎操作！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                CreateDB cdb = new CreateDB();
                cdb.creatDatabase();
            }
        }

        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            登录 dl = new 登录();
            this.Hide();
            dl.Show();
        }

        private void 默认导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag_pwd = 0;
            if (mftb.db_Default_Import())
            {
                MessageBox.Show("导入成功！因为您是导入的原来的数据库，密码可能与现在的数据库密码不一致，所以请您先输入导入的该数据库的密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InputDBPwd_Form pwd = new InputDBPwd_Form();
                pwd.ShowDialog();
            }
            else
                MessageBox.Show("导入失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void 自定义导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mftb.db_Define_Import())
            {
                MessageBox.Show("导入成功！因为您是导入的原来的数据库，密码可能与现在的数据库密码不一致，所以请您先输入导入的该数据库的密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //提示用户输入导入的该数据库的密码
                InputDBPwd_Form pwd = new InputDBPwd_Form();
                pwd.ShowDialog();
            }
            else
                MessageBox.Show("导入失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void lbSelectImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "图片|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = Image.FromFile(ofd.FileName);
                img = Image.FromFile(ofd.FileName);
                pictureBox1.Image = img;
                cus.User_img = img;
                string id = scd.getUserID(txtName.Text.Trim());

                if (mftb.addImg_B(cus, id))//往数据库里面保存头像
                {
                    MessageBox.Show("头像修改完成");
                }
                else
                    MessageBox.Show("头像修改失败");
            }
        }

        private void cbLock_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLock.Checked)
                txtRemark.ReadOnly = true;
            else
                txtRemark.ReadOnly = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() != "")
            {
                if (mftb.deleteUser_B(listBox1.SelectedItem.ToString()))
                {
                    //MessageBox.Show("删除成功","提示");
                    //刷新一下,重新加载数据到列表框
                    scb.addListUsers_B(listBox1, comboxGroup.SelectedItem.ToString(), false, config.readConfig_ReadPwd());
                    //刷新完之后默认选中第一项
                    if (listBox1.Items.Count > 0)
                        listBox1.SelectedIndex = 0;

                }
            }




            mftb.refresh_DB(toolStripStatusLabel7);//刷新数据库大小显示
        }

        private void 默认备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mftb.backUpDB())
                MessageBox.Show("备份成功！", "备份提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("数据库不存在！", "备份提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void 自定义导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mftb.backUpDB_Define())
                MessageBox.Show("导出数据库成功！", "导出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("数据库未导出！", "导出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 导入数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolFangQi_Click(object sender, EventArgs e)
        {
            toolStripButton2.Text = "修改";
            toolFangQi.Visible = false;
        }

        private void 关于作者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lx lx = new Lx();
            lx.ShowDialog();
        }

        private void 修改登录密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //首先应该确定当前用户是谁
            //然后获取新密码
            flag_pwd = 1;
            UserPwdEdit_From upe = new UserPwdEdit_From();
            upe.ShowDialog();

        }

        private void 刷新列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboxGroup.Items.Clear();
            mftd.initCommBox(comboxGroup);//刷新
            listBox1.Items.Clear();
            mftd.initCommBox(listBox1);
            //同时也刷新显示状态栏当前联系人个数
            toolStripStatusLabel1.Text = "当前联系人个数：" + scb.countUsers_B().ToString() + "人" + "       ";
            comboxGroup.SelectedIndex = 0;
        }

        private void mainForm_Activated(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.刷新1;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.刷新2;
        }


        #region 鼠标经过效果

        private void txtName_MouseEnter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Font f = new Font(Font.FontFamily, 12, FontStyle.Bold);
            tb.Font = f;
            //label10.Font = f;
        }

        private void txtName_MouseLeave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Font f = new Font(Font.FontFamily, Font.Size, Font.Style);
            tb.Font = f;
            //label10.Font = f;
        }

        private void lbSelectImg_MouseLeave(object sender, EventArgs e)
        {
            Font f = new Font(Font.FontFamily, Font.Size, Font.Style);
            lbSelectImg.Font = f;
        }

        private void lbSelectImg_MouseEnter(object sender, EventArgs e)
        {
            Font f = new Font(Font.FontFamily, 12, Font.Style);
            lbSelectImg.Font = f;
        }
        #endregion

        //鼠标经过工具栏效果
        private void toolStripLabel1_MouseEnter(object sender, EventArgs e)
        {
            ToolStripLabel tsl = (ToolStripLabel)sender;
            Font f = new Font("楷体", 14, Font.Style);
            tsl.Font = f;
        }

        private void toolStripLabel1_MouseLeave(object sender, EventArgs e)
        {
            ToolStripLabel tsl = (ToolStripLabel)sender;
            Font f = new Font("楷体", 12, Font.Style);
            tsl.Font = f;
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SoftOption_From sf = new SoftOption_From();
            sf.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                toolStripButton2.Enabled = true;//设置工具栏的 修改 按钮可用
                toolStripButton3.Enabled = false;//设置工具栏的 删除 按钮不可用
            }
            else
            {
                toolStripButton3.Enabled = true;
            }
                
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("注意！您真的要清空所有联系人吗?清空之后数据不可找回！", "特别注意",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (mftb.deleteAllUsers_B())
                {
                    MessageBox.Show("联系人已经清空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    listBox1.Items.Clear();
                }
            }
        }

    }
}

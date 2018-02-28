using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StusContactB;
using StusContactE;
using StusContactD;


namespace 个人通讯录
{
    public partial class AddUsers_Form : Form
    {
        public AddUsers_Form()
        {
            InitializeComponent();

        }

        StuContact_B scb = new StuContact_B();
        MainFormTools_D mftd = new MainFormTools_D();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AddGroup_Form ag = new AddGroup_Form();
            ag.ShowDialog();
        }


        private void addUsers_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = imageList1.Images[0];
            tabPage1.BackgroundImage = imageList1.Images[0];
            groupBox1.BackgroundImage = imageList1.Images[0];
            txtRemark.BackColor = Color.FromArgb(237, 233, 114);

            //初始化分组
            scb.initComboBox_B(comGroup);
        }
        Boolean flag = false;//标识用户是否选择了头像
        private void button1_Click(object sender, EventArgs e)
        {
            //姓名、所属分组不能为空
            if (txtName.Text == "")
                MessageBox.Show("还没有输入姓名！最少要输入姓名、手机号和所属分组", "操作提示");
            #region
            //else if (txtAge.Text == "")
            //    MessageBox.Show("请输入年龄！");
            //else if (txtBirth.Text == "")
            //    MessageBox.Show("请输入生日！");
            //else if (txtPhone.Text == "")
            //    MessageBox.Show("请输入手机号码！");
            //else if (txtQQ.Text == "")
            //    MessageBox.Show("请输入qq号码！");
            //else if (txtAddress.Text == "")
            //    MessageBox.Show("请输入地址！");
            #endregion
            else if (txtPhone.Text.Trim() == "")
                MessageBox.Show("输入联系人号码！", "操作提示");
            else if (radioSex_1.Checked == false && radioSex_2.Checked == false)
                MessageBox.Show("请选择性别！", "操作提示");
            //else if (radioSex_2.Checked == false)
            //    MessageBox.Show("请选择性别！", "操作提示");
            else if (comGroup.Text == "")
                MessageBox.Show("请选择该联系人所属分组！", "操作提示");
            else
            {
                //开始添加保存
                ContactUsers cu = new ContactUsers();
                cu.User_name = txtName.Text.Trim();
                if (flag)
                    cu.User_img = pictureBox1.Image;
                else
                    cu.User_img = null;

                if (radioSex_1.Checked)
                    cu.User_sex = "男";
                if (radioSex_2.Checked)
                    cu.User_sex = "女";
                cu.User_age = txtAge.Text.Trim();
                cu.User_birth = txtBirth.Text.Trim();
                cu.User_phone = txtPhone.Text.Trim();
                cu.User_qq = txtQQ.Text.Trim();
                cu.User_company = txtCompany.Text.Trim();
                cu.User_address = txtAddress.Text.Trim();
                if (txtRemark.Text == "在这里面写上你对该联系人的一些描述，是您对该联系人的情况了解更多.....")
                    cu.User_remark = "";
                else
                    cu.User_remark = txtRemark.Text.Trim();

                //首先应该获取所选分组的ID
                cu.User_belonggroup = mftd.getUserGroupID_D(comGroup.SelectedItem.ToString());

                addUsersForm_B aufb = new addUsersForm_B();
                if (aufb.addUsers_B(cu, flag))//flag代表是否有头像，方便判断
                {
                    if (MessageBox.Show("添加成功！是否继续添加?", "添加提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                        this.Hide();
                    else
                    {
                        txtAddress.Text = ""; txtAge.Text = ""; txtBirth.Text = ""; txtCompany.Text = "";
                        txtName.Text = ""; txtPhone.Text = ""; txtQQ.Text = ""; txtRemark.Text = "";
                    }
                }
                else
                    MessageBox.Show("添加失败", "添加提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lbSelectImage_Click(object sender, EventArgs e)
        {
            //对用户是否选择了头像进行判断，可以设置一个标志 flag
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Title = "选择头像";
            ofd.Filter = "图片文件|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                flag = true;
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
            else
                flag = false;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.ShowAlways = true;
            tip.SetToolTip(pictureBox2, "点击刷新一下");
            pictureBox2.Image = Properties.Resources.刷新2;
        }


        #region 移动窗体
        Boolean isMove = false;
        int x0, y0;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
                x0 = e.X;
                y0 = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
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

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }
        #endregion

        //刷新
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comGroup.Items.Clear();
            scb.initComboBox_B(comGroup);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.刷新1;
        }

        private void lbSelectImage_MouseEnter(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            Font f = new Font(Font.FontFamily, 11, Font.Style);
            if (lb.Text == "选择头像或者不选使用默认")
            {
                lb.Font = f;
            }
            if (lb.Text == "对现有分组不满意?点击修改分组")
            {
                lb.Font = f;
            }
        }

        private void lbSelectImage_MouseLeave(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            Font f = new Font(Font.FontFamily, Font.Size, Font.Style);
            if (lb.Text == "选择头像或者不选使用默认")
            {
                lb.Font = f;
            }
            if (lb.Text == "对现有分组不满意?点击修改分组")
            {
                lb.Font = f;
            }
        }

        private void txtRemark_Click(object sender, EventArgs e)
        {
            if (txtRemark.Text == "在这里面写上你对该联系人的一些描述，是您对该联系人的情况了解更多.....")
            {
                txtRemark.Text = "";
                txtRemark.ForeColor = Color.Black;
            }
        }

        
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length == 12)
            {
                txtPhone.Text = txtPhone.Text.Substring(0,11);
                txtPhone.SelectionStart = txtPhone.Text.Length;//限制用户输入个数为11个
                
            }
            
        }
    }
}

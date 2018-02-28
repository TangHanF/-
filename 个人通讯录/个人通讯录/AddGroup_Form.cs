using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StusContactB;
using StusContactD;


namespace 个人通讯录
{
    public partial class AddGroup_Form : Form
    {
        public AddGroup_Form()
        {
            InitializeComponent();
        }
        //mainForm mf = new mainForm();
        MainFormTools_D mftd = new MainFormTools_D();
        addGroupFrom_B agfb = new addGroupFrom_B();

        private void addGroup_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = imageList1.Images[0];
            textBox1.BackColor = Color.FromArgb(207, 216, 63);

            //初始化列表框分组信息，既是初始化，又是刷新
            mftd.initCommBox(listBox1);
        }





        Boolean isMove = false;
        int x0, y0;
        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
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

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
                x0 = e.X;
                y0 = e.Y;
            }
        }


        private void tabControl1_Click(object sender, EventArgs e)
        {
            //是文本框获得焦点
            if (qqTabControl1.SelectedIndex == 1)
                textBox1.Focus();
        }

        private void 包括组和组联系人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            if ((listBox1.SelectedItem.ToString() != "家人" && listBox1.SelectedItem.ToString() != "朋友" && listBox1.SelectedItem.ToString() != "同学" && listBox1.SelectedItem.ToString() != "同事" && listBox1.SelectedItem.ToString() != "老师"))
            {
                if (tsm.Text == "包括组和组联系人")
                {
                    if (agfb.deleteAll_B(listBox1.SelectedItem.ToString()))
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //mf.listBox1.Items.Clear();
                        listBox1.Items.Clear();
                        mftd.initCommBox(listBox1);//刷新
                        //mftd.initCommBox(mf.listBox1);
                        //mftd.initCommBox(mf.comboxGroup);

                    }
                }
                else if (tsm.Text == "仅删除组")
                {
                    listBox1.Items.Clear();
                    mftd.initCommBox(listBox1);
                }
                else//删除
                {
                    if (agfb.deleteAll_B(listBox1.SelectedItem.ToString()))
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listBox1.Items.Clear();
                        mftd.initCommBox(listBox1);//刷新

                    }
                }
            }
            else
                MessageBox.Show("系统保留分组，不能删除！", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void qqButton1_Click(object sender, EventArgs e)
        {
            string groupName = textBox1.Text.Trim();
            if (textBox1.Text.Trim() != "")
            {

                if (agfb.addGroup_B(listBox1, groupName))
                {
                    MessageBox.Show("分组添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    qqTabControl1.SelectedTab = tabPage3;
                }
                else
                {
                    MessageBox.Show("分组添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                }
                listBox1.Items.Clear();//先清空，在刷新之后添加
                mftd.initCommBox(listBox1);//确定添加之后自动刷新分组，即时显示新添加的分组
                //this.Hide();
            }
        }

        private void qqButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 删除选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((listBox1.SelectedItem.ToString() != "家人" && listBox1.SelectedItem.ToString() != "朋友" && listBox1.SelectedItem.ToString() != "同学" && listBox1.SelectedItem.ToString() != "同事" && listBox1.SelectedItem.ToString() != "老师"))
            {
                if (agfb.deleteAll_B(listBox1.SelectedItem.ToString()))
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listBox1.Items.Clear();
                    mftd.initCommBox(listBox1);//刷新
                }
                else {
                    MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


    }
}

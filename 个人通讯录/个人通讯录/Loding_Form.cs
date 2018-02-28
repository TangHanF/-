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
    public partial class Loding_Form : Form
    {
        public Loding_Form()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.加载中;//这样的话加载速度会稍微快一点，因为这里最先被初始化（构造）
        }

        private void lodingForm_Load(object sender, EventArgs e)
        {
            this.timer1.Start();//启动计时器
            this.timer1.Interval =Convert.ToInt32( config.readConfig_ReadPwd_gdDH())*100;//设置启动窗体停留时间
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            Main_Form mf = new Main_Form();
            mf.Show();//主窗体显示
            timer1.Enabled = false;//计时器关闭
        }

        private void lodingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Application.Exit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StusTools;
using Paway.Windows.Forms;

namespace 个人通讯录
{
    public partial class SoftOption_From : _360Form
    {
        public SoftOption_From()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true); 
            this.BackgroundImage = Properties.Resources.未标题_11;
        }
        //软件启动后应该也要读取配置文件，用来初始化滑动块的位置

        //登录界面动画
        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            config.writeConfig_dlDH(hScrollBar2.Value.ToString());
            label6.Text = (hScrollBar2.Value / 10).ToString() + "." + (hScrollBar2.Value% 10).ToString()+"秒";
        }

        //过度界面动画
        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            config.writeConfig_gdDH(hScrollBar3.Value.ToString());
            label7.Text = (hScrollBar3.Value / 10).ToString() + "." + (hScrollBar3.Value % 10).ToString() + "秒";
        }

        //主界面动画
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            config.writeConfig_zjmDH(hScrollBar1.Value.ToString());
            label8.Text = (hScrollBar1.Value / 10).ToString() + "." + (hScrollBar1.Value % 10).ToString() ;
        }

        private void softSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void softSet_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true); 
            hScrollBar1.Value = Convert.ToInt32(config.readConfig_ReadPwd_zjmDH());
            hScrollBar2.Value = Convert.ToInt32(config.readConfig_ReadPwd_dlDH());
            hScrollBar3.Value = Convert.ToInt32(config.readConfig_ReadPwd_gdDH());
            getTime();
            if (config.readConfig_zjmOpenEffect() == "on")
                qqRadioButton1.Checked = true;
            else
                qqRadioButton2.Checked = true;

            if (config.readConfig_zjmCloseEffect() == "on")
                qqRadioButton3.Checked = true;
            else
                qqRadioButton4.Checked = false;
        }


        public void getTime()
        {
            label6.Text = (hScrollBar2.Value / 10).ToString() + "." + (hScrollBar2.Value % 10).ToString() + "秒";
            label7.Text = (hScrollBar3.Value / 10).ToString() + "." + (hScrollBar3.Value % 10).ToString() + "秒";
            label8.Text = (hScrollBar1.Value / 10).ToString() + "." + (hScrollBar1.Value % 10).ToString();
        }

        private void qqRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            QQRadioButton qrb = (QQRadioButton)sender;
            if(qrb.Text=="开启")
                config.writeConfig_zjmOpenEffect(true);
            else
                config.writeConfig_zjmOpenEffect(false);

        }

        private void qqRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            QQRadioButton qrb = (QQRadioButton)sender;
            if (qrb.Text == "开启")
                config.writeConfig_zjmCloseEffect(true);
            else
                config.writeConfig_zjmCloseEffect(false);
        }
    }
}

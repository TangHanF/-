using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 个人通讯录
{
    public partial class AboutSoftware_From : Form
    {
        public AboutSoftware_From()
        {
            InitializeComponent();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Dispose();
        }
        int r, g, b;
        Random rand = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {
            r = rand.Next(255);
            g = rand.Next(255);
            b = rand.Next(255);
            label1.ForeColor= Color.FromArgb(r,g,b);
        }
    }
}

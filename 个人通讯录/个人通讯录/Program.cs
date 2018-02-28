using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 个人通讯录
{
    static class Program
    {
        private static System.Threading.Mutex mutex;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //防止程序重复运行
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                //Application.Run(new userLogin());//用户注册界面
                //Application.Run(new softSet()); //软件设置界面
                Application.Run(new 登录());
                //Application.Run(new addGroup());
                //Application.Run(new Main_Form());//主界面

                //Application.Run(new Lx());//联系作者界面
                //Application.Run(new AboutSoftware_From());
                //Application.Run(new lodingForm());//登陆界面和主界面之间的过渡动画界面
                //Application.Run(new inputDBPwd());//密码输入框
            }
            else
            {
                MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace StusTools
{
    public class config
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        //写入配置-----写入加密后的数据
        //数据库密码是否修改过
        public static void writeConfig_IsEditDBandAddPwd(Boolean flag, string new_Pwd)
        {
            //对“new_Pwd”进行加密，之后再写入配置文件
            string new_Pwd_After = new Encrypt().ToEncrypt(new_Pwd);
            if (flag)//如果修改过密码
            {
                //先判断路径是否存在
                if (Directory.Exists(Application.StartupPath + @"\config"))
                {
                    WritePrivateProfileString("infomation", "DB_PWEdited", "yes", Application.StartupPath + @"\config\config.ini");
                    WritePrivateProfileString("infomation", "DB_Pwd", new_Pwd_After, Application.StartupPath + @"\config\config.ini");
                }
                else
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\config");//创建文件夹
                    WritePrivateProfileString("infomation", "is_Edited", "yes", Application.StartupPath + @"\config\config.ini");
                }
            }
            else
            {
                WritePrivateProfileString("infomation", "is_Edited", "no", Application.StartupPath + @"\config\config.ini");
            }
        }
        //读取配置
        public static string readConfig_ReadPwd()
        {

            StringBuilder stringBud = new StringBuilder(50);
            //读取密码
            GetPrivateProfileString("infomation", "DB_Pwd", "密码数据不存在", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            //此时所读取的DB_Pwd键对应的值已被保存在stringBud中
            string jiemi = new Encrypt().ToDecrypt(stringBud.ToString());//解密加密的配置数据
            return jiemi;//返回解密的数据
        }



        //读取数据库密码是否被修改过
        public static string read_DBEdited()
        {
            StringBuilder stringBud = new StringBuilder(50);
            GetPrivateProfileString("infomation", "DB_PWEdited", "未修改", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            return stringBud.ToString();
        }

        public static void write_DBEdited(Boolean isXiuGaiGuoMa, string new_Pwd)
        {
            string new_Pwd_After = new Encrypt().ToEncrypt(new_Pwd);
            if (isXiuGaiGuoMa)
            {
                WritePrivateProfileString("infomation", "DB_Pwd", new_Pwd_After, Application.StartupPath + @"\config\config.ini");
                WritePrivateProfileString("infomation", "DB_PWEdited", "yes", Application.StartupPath + @"\config\config.ini");
            }
            else
            {
                WritePrivateProfileString("infomation", "DB_Pwd", new_Pwd_After, Application.StartupPath + @"\config\config.ini");
                WritePrivateProfileString("infomation", "DB_PWEdited", "no", Application.StartupPath + @"\config\config.ini");

            }

        }
        //写入动画时间设置
        public static void writeConfig_dlDH(string value)
        {
            WritePrivateProfileString("effect", "dlDH", value, Application.StartupPath + @"\config\config.ini");

        }
        public static void writeConfig_gdDH(string value)
        {
            WritePrivateProfileString("effect", "gdDH", value, Application.StartupPath + @"\config\config.ini");

        }
        public static void writeConfig_zjmDH(string value)
        {
            WritePrivateProfileString("effect", "zjmDH", value, Application.StartupPath + @"\config\config.ini");

        }
        //是否开启动画
        public static void writeConfig_zjmOpenEffect(Boolean flag)
        {
            if (flag)
                WritePrivateProfileString("effect", "zjmBeforeDH", "on", Application.StartupPath + @"\config\config.ini");
            else
                WritePrivateProfileString("effect", "zjmBeforeDH", "off", Application.StartupPath + @"\config\config.ini");

        }
        public static void writeConfig_zjmCloseEffect(Boolean flag)
        {
            if (flag)
                WritePrivateProfileString("effect", "zjmAfterDH", "on", Application.StartupPath + @"\config\config.ini");
            else
                WritePrivateProfileString("effect", "zjmAfterDH", "off", Application.StartupPath + @"\config\config.ini");

        }


        


        //读取动画时间
        public static string readConfig_ReadPwd_dlDH()//登陆界面动画
        {
            StringBuilder stringBud = new StringBuilder(50);
            GetPrivateProfileString("effect", "dlDH", "40", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            return stringBud.ToString();

        }
        public static string readConfig_ReadPwd_gdDH()//过渡动画
        {
            StringBuilder stringBud = new StringBuilder(50);
            GetPrivateProfileString("effect", "gdDH", "40", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            return stringBud.ToString();
        }
        public static string readConfig_ReadPwd_zjmDH()
        {
            StringBuilder stringBud = new StringBuilder(50);
            GetPrivateProfileString("effect", "zjmDH", "40", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            return stringBud.ToString();
        }

        public static string readConfig_zjmOpenEffect()
        {
            StringBuilder stringBud = new StringBuilder(50);
            GetPrivateProfileString("effect", "zjmBeforeDH", "40", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            return stringBud.ToString();
        }
        public static string readConfig_zjmCloseEffect()
        {
            StringBuilder stringBud = new StringBuilder(50);
            GetPrivateProfileString("effect", "zjmAfterDH", "40", stringBud, 50, Application.StartupPath + @"\config\config.ini");
            return stringBud.ToString();
        }

    }
}

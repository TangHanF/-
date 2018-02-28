using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using StusContactE;
using StusContactD;
using System.IO;
using StusTools;

namespace StusContactB
{
    public class StuContact_B
    {
        StuContact_D scd = new StuContact_D();
        config cfg = new config();

        public Boolean userExit(UserInfoE ue)
        {
            if (scd.login(ue) == 1)
                return true;
            else
                return false;
        }

        public Boolean userIsExit_B(UserInfoE ue)
        {
            if (scd.userIsExit_D(ue) == 1)//存在该用户返回真
                return true;
            else
                return false;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="ue">UserInfoE对象</param>
        /// <param name="imgpath">用户选择的头像路径</param>
        /// <returns></returns>
        public Boolean addUser(UserInfoE ue, string imgpath)
        {
            if (scd.addUserorUpdateUser_D(ue, imgpath))
                return true;
            else
                return false;
        }

        public Boolean UserIsImg_B(UserInfoE ue)
        {
            StuContact_D scd = new StuContact_D();
            if (scd.userIsImg_D(ue))
            {
                return true;
            }
            else
                return false;

        }

        //c初始化分组列表框
        MainFormTools_D mftd = new MainFormTools_D();
        public void initComboBox_B(ComboBox cb)
        {
            mftd.initCommBox(cb);
        }
        // 选择分组展开分组联系人
        public void addListUsers_B(ListBox lb, string selectedGroup,Boolean flag,string new_pwd)
        {
            if(flag)//如果数据库密码被修改过：
                mftd.addListUsers_D(lb, selectedGroup,true,new_pwd);
            else
                mftd.addListUsers_D(lb, selectedGroup, false, new_pwd);
                
        }

        //计算出联系人数
        public int countUsers_B()
        {
            //mft.countUsers_D();
            return mftd.countUsers_D();
        }

        public int checkFile_B()
        {
            if (!(File.Exists(Application.StartupPath + @"\img\0.jpg"))|| !(File.Exists(Application.StartupPath + @"\img\1.jpg")))
                return 1;
            else if ((File.Exists(Application.StartupPath + @"\img\0.jpg") && File.Exists(Application.StartupPath + @"\img\1.jpg")))
                return 2;
            else
                return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StusContactE
{
    public class UserInfoE
    {
        private string userName;
        private string userPwd;
        private Image UserImg;
        private int UserGroup;
        private int userIsImg;

        public int UserIsImg
        {
            get { return userIsImg; }
            set { userIsImg = value; }
        }
        public int UserGroup1
        {
            get { return UserGroup; }
            set { UserGroup = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string UserPwd
        {
            get { return userPwd; }
            set { userPwd = value; }
        }

        public Image UserImg1
        {
            get { return UserImg; }
            set { UserImg = value; }
        }
    }
}

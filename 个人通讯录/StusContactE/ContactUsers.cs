using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace StusContactE
{
    public class ContactUsers
    {
        private string user_name;
        private string user_sex;
        private string user_age;
        private string user_birth;
        private string user_phone;
        private string user_qq;
        private string user_address;
        private string user_remark;
        private string user_belonggroup;
        private string user_company;
        private Image user_img;

        public Image User_img
        {
            get { return user_img; }
            set { user_img = value; }
        }
        public string User_company
        {
            get { return user_company; }
            set { user_company = value; }
        }

        public string User_name
        {
            get { return user_name; }
            set { user_name = value; }
        }

        public string User_sex
        {
            get { return user_sex; }
            set { user_sex = value; }
        }

        public string User_age
        {
            get { return user_age; }
            set { user_age = value; }
        }

        public string User_birth
        {
            get { return user_birth; }
            set { user_birth = value; }
        }

        public string User_phone
        {
            get { return user_phone; }
            set { user_phone = value; }
        }

        public string User_qq
        {
            get { return user_qq; }
            set { user_qq = value; }
        }

        public string User_address
        {
            get { return user_address; }
            set { user_address = value; }
        }

        public string User_remark
        {
            get { return user_remark; }
            set { user_remark = value; }
        }

        public string User_belonggroup
        {
            get { return user_belonggroup; }
            set { user_belonggroup = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using StusTools;
using StusContactE;

namespace StusContactD
{

    public class MainFormTools_D
    {
        private int count = 0;
        //string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());


        config cfg = new config();
        string connStr;


        public void initCommBox(ComboBox cb)
        {
            //if (config.read_DBEdited() == "yes")
            //{
            //    MessageBox.Show("检测到您原来修改过数据库密码并使用了导入数据库功能，所以为了正常打开数据库请您点击“确定”后输入该数据库密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //}
            //else
            //因为数据库可能修改过密码，所以不能将connStr设置成全局变量，否则读取的还是原来的旧密码，导致数据库打不开
            connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = "select Group_Name from UserGroup";
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                while (oldr.Read())
                {
                    cb.Items.Add(oldr["Group_Name"]);
                }
            }
        }
        //利用重构
        public void initCommBox(ListBox lb)
        {
            connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = "select Group_Name from UserGroup";
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                while (oldr.Read())
                {
                    lb.Items.Add(oldr["Group_Name"]);
                }
            }
        }



        // 选择分组展开分组联系人

        public void addListUsers_D(ListBox lb, string selectedGroup, Boolean flag, string new_pwd)
        {
            lb.Items.Clear();
            if (flag)//如果修改过数据库密码就使用刚才用户输入的密码
                connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", new_pwd);
            else
                connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());


            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                try
                {
                    olconn.Open();
                    //string sql = string.Format("select User_Name from Contact_Users where User_BelongGroup=(select Group_Id from UserGroup where Group_Name='{0}') ", selectedGroup);
                    string sql = string.Format("select User_Name from Contact_Users where User_BelongGroup=(select ID from UserGroup where Group_Name='{0}') ", selectedGroup);
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                    OleDbDataReader oldr = olcmd.ExecuteReader();
                    while (oldr.Read())
                    {
                        lb.Items.Add(oldr["User_Name"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出错了昂:" + ex.Message, "错误");
                }
            }
        }

        //计算出联系人数
        public int countUsers_D()
        {
            count = 0;
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = "select User_Name from Contact_Users ";
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                while (oldr.Read())
                {
                    count++;
                }
                return count;
            }
        }

        //展开联系人详细信息
        public void UserDetail_D(String selectedName, TextBox txtAddress, TextBox txtAge, TextBox txtBirth, TextBox txtBoxFind, TextBox txtCompany, TextBox txtName, TextBox txtPhoneNum, TextBox txtQQ, TextBox txtRemark, RadioButton radioButtonSex1, RadioButton radioButtonSex2, ImageList myImageList, PictureBox picture)
        {
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            byte[] imagebytes = null;
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = string.Format("select * from Contact_Users where User_Name='{0}'", selectedName);
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                oldr.Read();

                try
                {
                    txtName.Text = oldr["User_Name"].ToString();
                    if (oldr["User_Sex"].ToString() == "男")
                        radioButtonSex1.Checked = true;
                    else
                        radioButtonSex2.Checked = true;
                    if (oldr["User_Img"].ToString() == "" && radioButtonSex1.Checked)
                    {
                        //还需要添加一个文件存在性验证，防止图片文件丢失导致程序崩溃
                        if (File.Exists(Application.StartupPath + @"\img\0.jpg"))
                            picture.Image = Image.FromFile(Application.StartupPath + @"\img\0.jpg");
                        else
                            picture.Image = null;
                    }
                    else if (oldr["User_Img"].ToString() == "" && radioButtonSex2.Checked)
                    {
                        if (File.Exists(Application.StartupPath + @"\img\1.jpg"))
                            picture.Image = Image.FromFile(Application.StartupPath + @"\img\1.jpg");
                        else
                            picture.Image = null;
                    }
                    else
                    {
                        ////////读取图片
                        imagebytes = (byte[])oldr.GetValue(2);
                        if (imagebytes != null)
                        {
                            MemoryStream ms = new MemoryStream(imagebytes);
                            Bitmap bmpt = new Bitmap(ms);
                            picture.Image = bmpt;
                        }
                    }

                    txtAge.Text = oldr["User_Age"].ToString();
                    txtBirth.Text = oldr["User_Birth"].ToString();
                    txtPhoneNum.Text = oldr["User_Phone"].ToString();
                    txtQQ.Text = oldr["User_Qq"].ToString();
                    txtAddress.Text = oldr["User_Address"].ToString();
                    txtCompany.Text = oldr["User_Company"].ToString();
                    txtRemark.Text = oldr["User_Remark"].ToString();
                }
                catch (Exception)
                {

                }
            }
        }
        string sql;
        public void findUsers(string selectItem, string findItem, ListBox list)
        {
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            switch (selectItem)
            {
                case "姓名": sql = string.Format("select User_Name from Contact_Users where User_Name like '%{0}%'", findItem); break;
                case "QQ号": sql = string.Format("select User_Name from Contact_Users where User_Qq like '%{0}%'", findItem); break;
                case "手机号": sql = string.Format("select User_Name from Contact_Users where User_Phone like '%{0}%' ", findItem); break;
                case "居住地": sql = string.Format("select User_Name from Contact_Users where User_Address like '%{0}%' ", findItem); break;
                case "生日": sql = string.Format("select User_Name from Contact_Users where User_Birth like '%{0}%' ", findItem); break;
            }
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                while (oldr.Read())
                {
                    list.Items.Add(oldr["User_Name"]);
                }
            }
        }


        public int editUserPwd_D(string currentUser, string old_password, string new_password)
        {
            //返回值含义：   0：出错    1：成功   2：原密码不对
            int f = 0;
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            //还必须验证用户输入的原密码是否匹配
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                OleDbCommand olcmd = new OleDbCommand();
                olcmd.CommandText = string.Format("select * from UserInfo where User_Name='{0}'", currentUser);
                olcmd.Connection = olconn;

                OleDbDataReader oldr = olcmd.ExecuteReader();
                oldr.Read();
                //验证输入的原用户密码是否一致
                if (oldr["User_Pwd"].ToString() == old_password)//如果一致
                {
                    //开始执行update语句
                    oldr.Close();
                    olcmd.CommandText = string.Format("update UserInfo set User_Pwd='{0}' where User_Name='{1}'", new_password, currentUser);
                    olcmd.Connection = olconn;
                    f = olcmd.ExecuteNonQuery();
                    if (f == 1)
                        return 1;
                    else
                        return 2;
                }
                else if (oldr["User_Pwd"].ToString() == new_password)//如果不一致
                {
                    return 1;
                }
                else
                    return 0;
            }
        }

        //删除联系人
        public Boolean deleteUser_D(string userName)
        {
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = string.Format("delete from Contact_Users where User_Name='{0}'", userName);
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                count = olcmd.ExecuteNonQuery();
                if (count == 1)
                    return true;
                else
                    return false;
            }
        }


        //添加联系人
        public Boolean addUser_D(ContactUsers cu, Boolean isImg)
        {
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            if (isImg)
            {
                int count = 0;
                byte[] imagebytes = null;
                Image photo = new Bitmap(cu.User_img);
                MemoryStream ms = new MemoryStream();
                photo.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                imagebytes = ms.GetBuffer();

                using (OleDbConnection olconn = new OleDbConnection(connStr))
                {
                    olconn.Open();
                    string sql = string.Format("insert into Contact_Users(User_Name,User_Img,User_Sex,User_Age,User_Birth,User_Phone,User_Qq,User_Company,User_Address,User_Remark,User_BelongGroup) values(@User_Name,@User_Img,@User_Sex,@User_Age,@User_Birth,@User_Phone,@User_Qq,@User_Company,@User_Address,@User_Remark,@User_BelongGroup)");
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);

                    olcmd.Parameters.Add("User_Name", OleDbType.VarChar);
                    olcmd.Parameters["User_Name"].Value = cu.User_name;

                    olcmd.Parameters.Add("User_Img", OleDbType.Binary);
                    olcmd.Parameters["User_Img"].Value = imagebytes;

                    olcmd.Parameters.Add("User_Sex", OleDbType.VarChar);
                    olcmd.Parameters["User_Sex"].Value = cu.User_sex;

                    olcmd.Parameters.Add("User_Age", OleDbType.VarChar);
                    olcmd.Parameters["User_Age"].Value = cu.User_age;

                    olcmd.Parameters.Add("User_Birth", OleDbType.VarChar);
                    olcmd.Parameters["User_Birth"].Value = cu.User_birth;

                    olcmd.Parameters.Add("User_Phone", OleDbType.VarChar);
                    olcmd.Parameters["User_Phone"].Value = cu.User_phone;

                    olcmd.Parameters.Add("User_Qq", OleDbType.VarChar);
                    olcmd.Parameters["User_Qq"].Value = cu.User_qq;

                    olcmd.Parameters.Add("User_Company", OleDbType.VarChar);
                    olcmd.Parameters["User_Company"].Value = cu.User_company;

                    olcmd.Parameters.Add("User_Address", OleDbType.VarChar);
                    olcmd.Parameters["User_Address"].Value = cu.User_address;

                    olcmd.Parameters.Add("User_Remark", OleDbType.VarChar);
                    olcmd.Parameters["User_Remark"].Value = cu.User_remark;

                    olcmd.Parameters.Add("User_BelongGroup", OleDbType.VarChar);
                    olcmd.Parameters["User_BelongGroup"].Value = cu.User_belonggroup;

                    count = olcmd.ExecuteNonQuery();
                    if (count == 1)
                        return true;
                    else
                        return false;
                }
            }
            else//不保存图片
            {
                using (OleDbConnection olconn = new OleDbConnection(connStr))
                {
                    olconn.Open();
                    string sql = string.Format("insert into Contact_Users(User_Name,User_Sex,User_Age,User_Birth,User_Phone,User_Qq,User_Company,User_Address,User_Remark,User_BelongGroup) values(@User_Name,@User_Sex,@User_Age,@User_Birth,@User_Phone,@User_Qq,@User_Company,@User_Address,@User_Remark,@User_BelongGroup)");
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);

                    olcmd.Parameters.Add("User_Name", OleDbType.VarChar);
                    olcmd.Parameters["User_Name"].Value = cu.User_name;

                    olcmd.Parameters.Add("User_Sex", OleDbType.VarChar);
                    olcmd.Parameters["User_Sex"].Value = cu.User_sex;

                    olcmd.Parameters.Add("User_Age", OleDbType.VarChar);
                    olcmd.Parameters["User_Age"].Value = cu.User_age;

                    olcmd.Parameters.Add("User_Birth", OleDbType.VarChar);
                    olcmd.Parameters["User_Birth"].Value = cu.User_birth;

                    olcmd.Parameters.Add("User_Phone", OleDbType.VarChar);
                    olcmd.Parameters["User_Phone"].Value = cu.User_phone;

                    olcmd.Parameters.Add("User_Qq", OleDbType.VarChar);
                    olcmd.Parameters["User_Qq"].Value = cu.User_qq;

                    olcmd.Parameters.Add("User_Company", OleDbType.VarChar);
                    olcmd.Parameters["User_Company"].Value = cu.User_company;

                    olcmd.Parameters.Add("User_Address", OleDbType.VarChar);
                    olcmd.Parameters["User_Address"].Value = cu.User_address;

                    olcmd.Parameters.Add("User_Remark", OleDbType.VarChar);
                    olcmd.Parameters["User_Remark"].Value = cu.User_remark;

                    olcmd.Parameters.Add("User_BelongGroup", OleDbType.VarChar);
                    olcmd.Parameters["User_BelongGroup"].Value = cu.User_belonggroup;

                    count = olcmd.ExecuteNonQuery();
                    if (count >= 1)
                        return true;
                    else
                        return false;
                }
            }
        }



        //获取分组的ID
        public string getUserGroupID_D(string groupName)
        {
            string groupId = "";
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = string.Format("select ID from UserGroup where Group_Name='{0}'", groupName);
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                oldr.Read();
                groupId = oldr["ID"].ToString();
                return groupId;
            }

        }

        //删除所有的联系人
        public bool deleteAllUsers_D()
        {
            string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = "delete * from Contact_Users";
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
               int count= olcmd.ExecuteNonQuery();
               if (count >= 1)
                   return true;
               else
                   return false;
            }
        }
    }
}

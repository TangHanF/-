using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using StusContactE;
using System.Data;
using System.IO;
using System.Drawing;
using StusTools;

namespace StusContactD
{
    public class StuContact_D
    {
        string connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password='{0}'", config.readConfig_ReadPwd());
        public int login(UserInfoE ue)
        {
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = string.Format("select count(*) from UserInfo where User_Name='{0}' and User_Pwd='{1}'", ue.UserName, ue.UserPwd);
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                int count = Convert.ToInt32(olcmd.ExecuteScalar());
                return count;
            }
        }

        //在注册的时候验证用户是否已经存在
        public int userIsExit_D(UserInfoE ue)
        {
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = string.Format("select count(*) from UserInfo where User_Name='{0}'", ue.UserName);
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                int count = Convert.ToInt32(olcmd.ExecuteScalar());
                return count;
            }
        }

        #region 方法的重载----注册用户或者更新联系人信息
        //更新联系人信息
        public Boolean addUserorUpdateUser_D(ContactUsers cus, string id)
        {
            try
            {
                using (OleDbConnection olconn = new OleDbConnection(connStr))
                {
                    olconn.Open();
                    string sql = string.Format("update  Contact_Users set User_Name='{0}',User_Sex='{1}',User_Age='{2}',User_Birth='{3}',User_Phone='{4}',User_Qq='{5}',User_Company='{6}',User_Address='{7}',User_Remark='{8}' where ID={9}", cus.User_name, cus.User_sex, cus.User_age, cus.User_birth, cus.User_phone, cus.User_qq, cus.User_company, cus.User_address, cus.User_remark, id);
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                    int count = olcmd.ExecuteNonQuery();
                    if (count == 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+@"--------------------建议：尽量不要使用英文单引号：''", "特殊字符转义异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               return false; 
            }
            
        }
        //注册用户 
        public Boolean addUserorUpdateUser_D(UserInfoE ue, string imgpath)
        {
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                #region

                //string fullpath = imgpath;//文件路径
                //FileStream fs = new FileStream(fullpath, FileMode.Open);
                //byte[] imagebytes = new byte[fs.Length];
                //BinaryReader br = new BinaryReader(fs);
                //imagebytes = br.ReadBytes(Convert.ToInt32(fs.Length));
                #endregion
                byte[] imagebytes = null;

                int count = 0;
                olconn.Open();

                //如果用户未选择头像，则：
                if (imgpath == null)
                {
                    string sql = string.Format("insert into UserInfo(User_Name,User_Pwd,User_Group,User_IsImg)  values('{0}','{1}','0','0')", ue.UserName, ue.UserPwd);
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                    count = olcmd.ExecuteNonQuery();
                }
                else//如果用户选择了头像，则：
                {
                    //将图片转换成字节数组
                    Image photo = new Bitmap(ue.UserImg1);
                    MemoryStream ms = new MemoryStream();
                    photo.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    imagebytes = ms.GetBuffer();

                    string sql = string.Format("insert into UserInfo(User_Name,User_Pwd,User_Img,User_Group,User_IsImg)  values(@User_Name,@User_Pwd,@User_Img,@User_Group,'1')");
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                    olcmd.Parameters.Add("User_Name", SqlDbType.Text);
                    olcmd.Parameters["User_Name"].Value = ue.UserName;

                    olcmd.Parameters.Add("User_Pwd", SqlDbType.Text);
                    olcmd.Parameters["User_Pwd"].Value = ue.UserPwd;

                    olcmd.Parameters.Add("User_Img", SqlDbType.Image);
                    olcmd.Parameters["User_Img"].Value = imagebytes;

                    olcmd.Parameters.Add("User_Group", SqlDbType.Image);
                    olcmd.Parameters["User_Group"].Value = "0";
                    count = olcmd.ExecuteNonQuery();
                }
                if (count == 1)
                    return true;
                else
                    return false;
            }
        }
        #endregion


        #region 检查用户头像是否存在
        public Boolean userIsImg_D(UserInfoE ue)
        {
            if (userIsExit_D(ue) == 1)
            {
                using (OleDbConnection olconn = new OleDbConnection(connStr))
                {
                    olconn.Open();
                    string sql = string.Format("select * from UserInfo where User_Name='{0}'", ue.UserName);
                    OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                    OleDbDataReader oldr = olcmd.ExecuteReader();
                    oldr.Read();
                    if (Convert.ToInt32(oldr["User_IsImg"]) == 1)
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;
        }
        #endregion

        //获取用户的ID
        public string getUserID(string userName)
        {
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                string sql = string.Format("select ID from Contact_Users where (User_Name='{0}')", userName);
                OleDbCommand olcmd = new OleDbCommand(sql, olconn);
                OleDbDataReader oldr = olcmd.ExecuteReader();
                oldr.Read();
                string id = oldr["ID"].ToString();
                return id;
            }
        }

        public Boolean addImg_D(ContactUsers cu, string id)
        {
            int count = 0;
            byte[] imagebytes = null;
            Image photo = new Bitmap(cu.User_img);
            MemoryStream ms = new MemoryStream();
            photo.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            imagebytes = ms.GetBuffer();

            //打开数据库
            using (OleDbConnection olconn = new OleDbConnection(connStr))
            {
                olconn.Open();
                OleDbCommand olcmd = new OleDbCommand(string.Format("update Contact_Users set User_Img=@User_Img where ID={0}", id), olconn);
                olcmd.Parameters.Add("User_Img", SqlDbType.Image);
                olcmd.Parameters["User_Img"].Value = imagebytes;
                count = olcmd.ExecuteNonQuery();
                if (count == 1)
                    return true;
                else
                    return false;
            }
        }
    }
}

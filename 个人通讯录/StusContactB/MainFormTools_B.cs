using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using JRO;//压缩数据库的时候用到

using StusTools;
using StusContactD;
using StusContactE;


namespace StusContactB
{
    public class MainFormTools_B
    {
        StuContact_D scd = new StuContact_D();
        MainFormTools_D mftd = new MainFormTools_D();

        //备份数据库到默认位置
        public Boolean backUpDB()
        {
            string DB_Path = Application.StartupPath + @"\source\StuContact.mdb";
            if (File.Exists(DB_Path))
            {
                if (Directory.Exists(@"D:\ContactBackup"))
                {
                    if (File.Exists(@"D:\ContactBackup\StuContact.mdb"))
                        File.Delete(@"D:\ContactBackup\StuContact.mdb");
                    File.Copy(Application.StartupPath + @"\source\StuContact.mdb", @"D:\ContactBackup\StuContact.mdb");
                    return true;
                }
                else
                {
                    Directory.CreateDirectory(@"D:\ContactBackup");
                    File.Copy(Application.StartupPath + @"\source\StuContact.mdb", @"D:\ContactBackup\StuContact.mdb");
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        //自定义导出位置
        public Boolean backUpDB_Define()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult res = fbd.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (File.Exists(fbd.SelectedPath + @"\StuContact.mdb"))
                {
                    DialogResult result;
                    result = MessageBox.Show("该位置已经有原来备份的数据库，是否覆盖", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        File.Delete(fbd.SelectedPath + @"\StuContact.mdb");
                        File.Copy(Application.StartupPath + @"\source\StuContact.mdb", fbd.SelectedPath + @"\StuContact.mdb");
                        return true;
                    }
                    else
                        return false;
                }
                else 
                {
                    File.Copy(Application.StartupPath + @"\source\StuContact.mdb", fbd.SelectedPath + @"\StuContact.mdb");
                    return true;
                }
                
            }
            else
                return false;

        }

        //从默认位置默认导入
        public Boolean db_Default_Import()
        {
            if (File.Exists(@"D:\ContactBackup\StuContact.mdb"))
            {
                if (File.Exists(Application.StartupPath + @"\source\StuContact.mdb"))
                {
                    File.Delete(Application.StartupPath + @"\source\StuContact.mdb");
                }
                File.Copy(@"D:\ContactBackup\StuContact.mdb", Application.StartupPath + @"\source\StuContact.mdb");
                return true;
            }
            else
                return false;
        }

        //从自定义位置导入数据库
        public Boolean db_Define_Import()
        {
            string DB_Path;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Title = "选择要导入的数据库...";
            ofd.Filter = "Access数据库|*.mdb";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DB_Path = ofd.FileName;
                if (File.Exists(Application.StartupPath + @"\source\StuContact.mdb"))
                {
                    File.Delete(Application.StartupPath + @"\source\StuContact.mdb");
                }
                File.Copy(DB_Path, Application.StartupPath + @"\source\StuContact.mdb");
                return true;
            }
            else
                return false;
        }

        //重新刷新数据库大小
        public void refresh_DB(ToolStripStatusLabel label)
        {
            FileInfo fi = new FileInfo(Application.StartupPath + @"\source\StuContact.mdb");
            label.Text = "当前数据库大小：" + fi.Length / 1024 + "KB";
        }

        //压缩数据库
        public void compressDB_B(ToolStripStatusLabel label, string db_Pwd)
        {
            try
            {
                JetEngine x = new JetEngine();
                string connstr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\source\StuContact.mdb;Jet OLEDB:Database Password='{1}'", Application.StartupPath, db_Pwd);
                string connstr1 = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\source\StuContact1.mdb;Jet OLEDB:Database Password='{1}'", Application.StartupPath, db_Pwd);
                x.CompactDatabase(connstr, connstr1);
                File.Delete(Application.StartupPath + "\\source\\StuContact.mdb");
                File.Move(Application.StartupPath + "\\source\\StuContact1.mdb", Application.StartupPath + "\\source\\StuContact.mdb");
                MessageBox.Show("数据库压缩成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh_DB(label);//调用方法刷新数据库大小显示
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //修改数据库密码
        public void editDBPwd_B(string old_password, string new_password)
        {
            try
            {
                string fileName = Application.StartupPath + @"\source\StuContact.mdb";//数据库路径
                string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Jet OLEDB:Database password=" + old_password;
                string sql = "ALTER DATABASE PASSWORD " + new_password + " " + old_password;

                ADODB.Connection cn = new ADODB.Connection();
                cn.Mode = ADODB.ConnectModeEnum.adModeShareExclusive;//以独占模式打开数据库，不然不能修改密码
                cn.Open(conn, null, null, -1);
                // 执行 SQL 语句以更改密码
                object num;
                cn.Execute(sql, out num, -1);
                cn.Close();

                //开始修改配置文件
                config.writeConfig_IsEditDBandAddPwd(true, new_password);
                MessageBox.Show("密码修改成功，请牢记您的密码", "修改提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库密码未修改,因为:"+ex.Message, "提示", MessageBoxButtons.OK);
            }
        }

        //修改用户数据
        public Boolean addUserorUpdateUser_B(ContactUsers cus, string id)
        {
            if (scd.addUserorUpdateUser_D(cus, id))
                return true;
            else
                return false;
        }

        //添加头像
        public Boolean addImg_B(ContactUsers cu, string id)
        {
            if (scd.addImg_D(cu, id))
                return true;
            else
                return false;
        }

        public int editUserPwd_B(string currentUser, string old_password, string new_password)
        {
            if (mftd.editUserPwd_D(currentUser, old_password, new_password) == 0)
                return 0;
            if (mftd.editUserPwd_D(currentUser, old_password, new_password) == 1)
                return 1;
            else
                return 2;

        }


        //删除联系人
        public Boolean deleteUser_B(string userName)
        {
            if (mftd.deleteUser_D(userName))
                return true;
            else
                return false;
        }

        public bool deleteAllUsers_B()
        {
            if (mftd.deleteAllUsers_D())
                return true;
            else
                return false;
        }
    }
}

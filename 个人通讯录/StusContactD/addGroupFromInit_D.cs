using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using StusTools;

namespace StusContactD
{
   public class addGroupFromInit_D
    {
       string connStr;
       public void addGroupFromInit_B(ListBox listBox1)
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
                    //cb.Items.Add(oldr["Group_Name"]);
                }
            }
        }

       public Boolean addGroup_D(ListBox listBox1, string groupName)
       {
           connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());
           using (OleDbConnection olconn = new OleDbConnection(connStr))
           {
               olconn.Open();
               string sql =string.Format("insert into UserGroup(Group_Name) values('{0}')",groupName);
               OleDbCommand olcmd = new OleDbCommand(sql, olconn);
               int count = olcmd.ExecuteNonQuery();
               if (count == 1)
                   return true;
               else
                   return false;
           }
       }


       //删除组和组下面的联系人
       public Boolean deleteAll_D(string groupName)
       {
           int count1=0, count2 = 0;
           connStr = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;Jet OLEDB:Database Password={0}", config.readConfig_ReadPwd());

           using (OleDbConnection olconn = new OleDbConnection(connStr))
           {
               olconn.Open();
               OleDbCommand olcmd = new OleDbCommand();
               //利用跨表联合查询实现删除分组下的联系人
               olcmd.CommandText = string.Format("delete from Contact_Users where User_BelongGroup=(select ID from UserGroup where Group_Name='{0}')", groupName);
               olcmd.Connection = olconn;
               count1=olcmd.ExecuteNonQuery();
               //定义删除分组的语句
               olcmd.CommandText = string.Format("delete from UserGroup where Group_Name='{0}'",groupName);
               count2 = olcmd.ExecuteNonQuery();

               if ((count1 + count2) >= 2 || (count1 + count2) == 1)
                   return true;
               else
                   return false;
           }
       }
    }
}

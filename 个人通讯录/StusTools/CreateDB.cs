using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;
using ADODB;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;


/////重建数据库之后已经添加一个管理员账号
namespace StusTools
{
    public class CreateDB
    {
        public void creatDatabase()
        {
            //路径没有需要新建
            if (Directory.Exists(Application.StartupPath + @"\source"))
            {
                creat_DB();
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + @"\source");//创建文件夹
                creat_DB();
            }
        }

        //创建带有密码的Access数据库，默认密码：jiemi
        string connStr = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + Application.StartupPath + @"\source\StuContact.mdb;jet oledb:database password=jiemi";
        public void creat_DB()
        {
            try
            {
                string dbName = Application.StartupPath + @"\source\StuContact.mdb";//注意扩展名必须为mdb,否则不能插入表
                ADOX.CatalogClass cat = new ADOX.CatalogClass();
                cat.Create(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Jet OLEDB:Database Password=jiemi;Jet OLEDB:Engine Type=5", dbName));


                //新建表
                ADOX.TableClass tbl = new ADOX.TableClass();
                tbl.ParentCatalog = cat;
                tbl.Name = "Contact_Users";

                ADOX.TableClass tb2 = new ADOX.TableClass();
                tb2.ParentCatalog = cat;
                tb2.Name = "UserGroup";

                ADOX.TableClass tb3 = new ADOX.TableClass();
                tb3.ParentCatalog = cat;
                tb3.Name = "UserInfo";


                #region 表一：
                //给各个表增加自动增长的字段
                ADOX.ColumnClass tb_one_col1 = new ADOX.ColumnClass();
                tb_one_col1.ParentCatalog = cat;
                tb_one_col1.Type = ADOX.DataTypeEnum.adInteger; //必须先设置字段类型
                tb_one_col1.Name = "ID";
                tb_one_col1.Properties["Jet OLEDB:Allow Zero Length"].Value = false;//是否允许为空
                tb_one_col1.Properties["AutoIncrement"].Value = true;//自增长
                tbl.Columns.Append(tb_one_col1, ADOX.DataTypeEnum.adInteger, 0);//添加字段


                //增加文本字段
                ADOX.ColumnClass col2 = new ADOX.ColumnClass();//注意col序号
                col2.ParentCatalog = cat;
                col2.Name = "User_Name";
                col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;

                tbl.Columns.Append(col2, ADOX.DataTypeEnum.adVarChar, 50);

                ADOX.ColumnClass col3 = new ADOX.ColumnClass();
                col3.ParentCatalog = cat;
                col3.Name = "User_Img";
                col3.Type = ADOX.DataTypeEnum.adLongVarBinary;////////////OLE类型设置,用来存储图片
                col3.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col3, ADOX.DataTypeEnum.adLongVarBinary, 0);

                ADOX.ColumnClass col4 = new ADOX.ColumnClass();
                col4.ParentCatalog = cat;
                col4.Name = "User_Sex";
                col4.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col4, ADOX.DataTypeEnum.adVarChar, 4);

                ADOX.ColumnClass col5 = new ADOX.ColumnClass();
                col5.ParentCatalog = cat;
                col5.Name = "User_Age";
                col5.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col5, ADOX.DataTypeEnum.adInteger, 200);

                ADOX.ColumnClass col6 = new ADOX.ColumnClass();
                col6.ParentCatalog = cat;
                col6.Name = "User_Birth";
                col6.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col6, ADOX.DataTypeEnum.adVarChar, 50);

                ADOX.ColumnClass col7 = new ADOX.ColumnClass();
                col7.ParentCatalog = cat;
                col7.Name = "User_Phone";
                col7.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col7, ADOX.DataTypeEnum.adVarChar, 15);

                ADOX.ColumnClass col8 = new ADOX.ColumnClass();
                col8.ParentCatalog = cat;
                col8.Name = "User_Qq";
                col8.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col8, ADOX.DataTypeEnum.adVarChar, 25);

                ADOX.ColumnClass col9 = new ADOX.ColumnClass();
                col9.ParentCatalog = cat;
                col9.Name = "User_Company";
                col9.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col9, ADOX.DataTypeEnum.adVarChar, 50);

                ADOX.ColumnClass col10 = new ADOX.ColumnClass();
                col10.ParentCatalog = cat;
                col10.Name = "User_Address";
                //col10.Type
                col10.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col10, ADOX.DataTypeEnum.adVarChar, 100);

                ADOX.ColumnClass col11 = new ADOX.ColumnClass();
                col11.Type = ADOX.DataTypeEnum.adLongVarWChar;//长文本
                col11.ParentCatalog = cat;
                col11.Name = "User_Remark";
                col11.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col11, ADOX.DataTypeEnum.adLongVarChar, 16);

                ADOX.ColumnClass col12 = new ADOX.ColumnClass();
                col12.ParentCatalog = cat;
                col12.Name = "User_BelongGroup";
                col12.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col12, ADOX.DataTypeEnum.adInteger, 200);

                cat.Tables.Append(tbl); //把表加入数据库(非常重要)
                #endregion
                #region 表二：
                //给各增加自动增长的字段
                ADOX.ColumnClass tb_two_col1 = new ADOX.ColumnClass();//代表表的第一列
                tb_two_col1.ParentCatalog = cat;
                tb_two_col1.Type = ADOX.DataTypeEnum.adInteger; //必须先设置字段类型
                tb_two_col1.Name = "ID";
                tb_two_col1.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb_two_col1.Properties["AutoIncrement"].Value = true;
                tb2.Columns.Append(tb_two_col1, ADOX.DataTypeEnum.adInteger, 0);


                //增加文本字段
                ADOX.ColumnClass tb_two_col2 = new ADOX.ColumnClass();//注意col序号
                tb_two_col2.ParentCatalog = cat;
                tb_two_col2.Name = "Group_Id";
                tb_two_col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb2.Columns.Append(tb_two_col2, ADOX.DataTypeEnum.adVarChar, 50);

                ADOX.ColumnClass tb_two_col3 = new ADOX.ColumnClass();
                tb_two_col3.ParentCatalog = cat;
                tb_two_col3.Name = "Group_Name";
                tb_two_col3.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb2.Columns.Append(tb_two_col3, ADOX.DataTypeEnum.adVarChar, 50);


                cat.Tables.Append(tb2);
                #endregion

                #region 表三：
                ADOX.ColumnClass tb_three_col1 = new ADOX.ColumnClass();//代表表的第一列
                tb_three_col1.ParentCatalog = cat;
                tb_three_col1.Type = ADOX.DataTypeEnum.adInteger; //必须先设置字段类型
                tb_three_col1.Name = "ID";
                tb_three_col1.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb_three_col1.Properties["AutoIncrement"].Value = true;
                tb3.Columns.Append(tb_three_col1, ADOX.DataTypeEnum.adInteger, 0);


                //增加文本字段
                ADOX.ColumnClass tb_three_col2 = new ADOX.ColumnClass();
                tb_three_col2.ParentCatalog = cat;
                tb_three_col2.Name = "User_Name";
                tb_three_col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb3.Columns.Append(tb_three_col2, ADOX.DataTypeEnum.adVarChar, 50);

                ADOX.ColumnClass tb_three_col3 = new ADOX.ColumnClass();
                tb_three_col3.ParentCatalog = cat;
                tb_three_col3.Name = "User_Pwd";
                tb_three_col3.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb3.Columns.Append(tb_three_col3, ADOX.DataTypeEnum.adVarChar, 50);

                ADOX.ColumnClass tb_three_col4 = new ADOX.ColumnClass();
                tb_three_col4.ParentCatalog = cat;
                tb_three_col4.Name = "User_Img";
                tb_three_col4.Type = ADOX.DataTypeEnum.adLongVarBinary;
                tb_three_col4.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tb3.Columns.Append(tb_three_col4, ADOX.DataTypeEnum.adLongVarBinary, 0);

                ADOX.ColumnClass tb_three_col5 = new ADOX.ColumnClass();
                tb_three_col5.ParentCatalog = cat;
                tb_three_col5.Name = "User_Group";
                tb_three_col5.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb3.Columns.Append(tb_three_col5, ADOX.DataTypeEnum.adInteger, 200);

                ADOX.ColumnClass tb_three_col6 = new ADOX.ColumnClass();
                tb_three_col6.ParentCatalog = cat;
                tb_three_col6.Name = "User_IsImg";
                tb_three_col6.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tb3.Columns.Append(tb_three_col6, ADOX.DataTypeEnum.adInteger, 2);

                cat.Tables.Append(tb3);
                #endregion
                //转换为ADO连接,并关闭
                (cat.ActiveConnection as ADODB.Connection).Close();
                cat.ActiveConnection = null;
                cat = null;

                //创建完数据库之后自动初始化必要数据
                using (OleDbConnection olconn = new OleDbConnection(connStr))
                {
                    olconn.Open();
                    OleDbCommand olcmd = new OleDbCommand();
                    olcmd.Connection = olconn;
                    olcmd.CommandText = "insert into UserInfo(User_Name,User_Pwd,User_Group,User_IsImg) values('admin','admin','1','0')";
                    olcmd.ExecuteNonQuery();

                    olcmd.CommandText = "insert into UserGroup(Group_Id,Group_Name) values('0','家人')";
                    olcmd.ExecuteNonQuery();

                    olcmd.CommandText = "insert into UserGroup(Group_Id,Group_Name) values('1','朋友')";
                    olcmd.ExecuteNonQuery();

                    olcmd.CommandText = "insert into UserGroup(Group_Id,Group_Name) values('2','同学')";
                    olcmd.ExecuteNonQuery();

                    olcmd.CommandText = "insert into UserGroup(Group_Id,Group_Name) values('3','同事')";
                    olcmd.ExecuteNonQuery();

                    olcmd.CommandText = "insert into UserGroup(Group_Id,Group_Name) values('4','老师')";
                    olcmd.ExecuteNonQuery();
                }

                //开始将数据库的密码加密后写入配置文件
                config.writeConfig_IsEditDBandAddPwd(true, "jiemi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现了如下问题："+ex.Message,"异常提示");
            }


        }


    }
}


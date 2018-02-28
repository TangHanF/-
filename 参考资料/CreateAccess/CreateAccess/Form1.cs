using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


//添加以下引用
using System.IO;
using System.Data.OleDb;        //连接Access数据库
using ADOX;                     //引用COM：Microsoft ADO Ext. 2.8 for DDL and Security 还要引用ADODB 2.8

                                //添加引用：Microsoft ActioveX Data Objects 2.8 Library

namespace CreateAccess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtFilePath, "可以拖动Access数据库文件到这里！");
        }

        private void txtFilePath_Click(object sender, EventArgs e)
        {
            txtFilePath.SelectAll();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "*.mdb|*.mdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {            
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        //把Form1的AllowDrop设置成True
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            txtFilePath.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            

        }



        #region 创建数据库并添加数据
        //创建数据库
        public void CreateData(string fileName)
        {
            if (File.Exists(fileName))
            {
                string delFile = fileName;
                File.Delete(delFile);
            }

            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName;
            
            //创建数据库
            ADOX.Catalog catalog = new Catalog();
            try
            {
                catalog.Create(conn); 
            }
            catch
            {}


            //连接数据库
            ADODB.Connection cn = new ADODB.Connection();
            cn.Open(conn, null, null, -1);                       
            catalog.ActiveConnection = cn;

            //新建表
            ADOX.Table table = new ADOX.Table();
            table.Name = "AdPlayList";

            ADOX.Column column = new ADOX.Column();
            column.ParentCatalog = catalog;
            column.Type = ADOX.DataTypeEnum.adInteger; // 必须先设置字段类型
            column.Name = "ID";
            column.DefinedSize = 9;
            column.Properties["AutoIncrement"].Value = true;
            table.Columns.Append(column, DataTypeEnum.adInteger, 0);
            //设置主键
            table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "ID", "", "");

            table.Columns.Append("FileName", DataTypeEnum.adVarWChar, 50);
            table.Columns.Append("FileDate", DataTypeEnum.adDate, 0);
            table.Columns.Append("FileSize", DataTypeEnum.adInteger, 9);
            table.Columns.Append("OrderID", DataTypeEnum.adInteger, 9);
            table.Columns.Append("Sha1", DataTypeEnum.adVarWChar, 50);
            
            try
            {
                catalog.Tables.Append(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //此处一定要关闭连接，否则添加数据时候会出错

            table = null;
            catalog = null;
            Application.DoEvents();
            cn.Close();
        }


        public void AddData(string fileName)
        {
            string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Persist Security Info=False";

            OleDbConnection conn = new OleDbConnection(strConnection);
            string strSql = "select * from AdPlayList";
            OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                da.Fill(ds, "AdPlayList");
                DataRow dr = ds.Tables["AdPlayList"].NewRow();
                dr["FileName"] = "A.wmv";
                dr["FileDate"] = DateTime.Now.ToShortDateString();
                dr["FileSize"] = 25;
                dr["OrderID"] = 1;
                dr["Sha1"] = "2q34lkadsflaoiulkj34";
                ds.Tables["AdPlayList"].Rows.Add(dr);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.Update(ds, "AdPlayList");
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                da.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }

        #endregion





        #region 创建数据库并添加数据
        //创建数据库
        public void CreateData(string fileName, string Pwd)
        {
            if (File.Exists(fileName))
            {
                string delFile = fileName;
                File.Delete(delFile);
            }

            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Jet OLEDB:Database Password=" + Pwd + ";Jet OLEDB:Engine Type=5";            
            //创建数据库
            ADOX.Catalog catalog = new Catalog();
            try
            {
                catalog.Create(conn);
            }
            catch
            {    }


            //连接数据库
            ADODB.Connection cn = new ADODB.Connection();
            cn.Open(conn, null, null, -1);            
            catalog.ActiveConnection = cn;

            //新建表
            ADOX.Table table = new ADOX.Table();
            table.Name = "AdPlayList";

            ADOX.Column column = new ADOX.Column();
            column.ParentCatalog = catalog;
            column.Type = ADOX.DataTypeEnum.adInteger; // 必须先设置字段类型
            column.Name = "ID";
            column.DefinedSize = 9;
            column.Properties["AutoIncrement"].Value = true;
            table.Columns.Append(column, DataTypeEnum.adInteger, 0);
            //设置主键
            table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "ID", "", "");

            table.Columns.Append("FileName", DataTypeEnum.adVarWChar, 50);
            table.Columns.Append("FileDate", DataTypeEnum.adDate, 0);
            table.Columns.Append("FileSize", DataTypeEnum.adInteger, 9);
            table.Columns.Append("OrderID", DataTypeEnum.adInteger, 9);
            table.Columns.Append("Sha1", DataTypeEnum.adVarWChar, 50);

            try
            {
                catalog.Tables.Append(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //此处一定要关闭连接，否则添加数据时候会出错

            table = null;
            catalog = null;
            Application.DoEvents();
            cn.Close();
        }


        public void AddData(string fileName, string pwd)
        {
            string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Persist Security Info=False;Jet OLEDB:Database password=" + pwd;

            OleDbConnection conn = new OleDbConnection(strConnection);
            string strSql = "select * from AdPlayList";
            OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds, "AdPlayList");
                DataRow dr = ds.Tables["AdPlayList"].NewRow();
                dr["FileName"] = "A.wmv";
                dr["FileDate"] = DateTime.Now.ToShortDateString();
                dr["FileSize"] = 25;
                dr["OrderID"] = 1;
                dr["Sha1"] = "2q34lkadsflaoiulkj34";
                ds.Tables["AdPlayList"].Rows.Add(dr);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.Update(ds, "AdPlayList");
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                da.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        #endregion

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            string fileName = txtFilePath.Text.Trim();
            string openpwd = txtOldPwd.Text.Trim();
            string newpwd = txtNewPwd.Text.Trim();
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Jet OLEDB:Database password=" + openpwd;
            string sql = "ALTER DATABASE PASSWORD " + newpwd + " " + openpwd;
            ADODB.Connection cn = new ADODB.Connection();
            cn.Mode = ADODB.ConnectModeEnum.adModeShareExclusive;
            cn.Open(conn, null, null, -1);


            // 执行 SQL 语句以更改密码。
            object num;
            cn.Execute(sql, out num, -1);

            cn.Close();

            radioButton3.Checked = true;
        }


        private void ViewData(string fileName,string pwd)
        {
            string strConnection;
            if (pwd == null)
            {
                strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Persist Security Info=False";
            }
            else
            {
                strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Jet OLEDB:Database password=" + pwd;
            }
            
            OleDbConnection conn = new OleDbConnection(strConnection);             
            string strSql = "select * From AdPlayList";
            OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();               

                da.Fill(ds, "AdPlayList");

                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            da.Dispose();
            conn.Dispose();
            conn.Close();

        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ViewData(txtFilePath.Text, null);
                return;
            }


            if (radioButton2.Checked)
            {
                ViewData(txtFilePath.Text, txtOldPwd.Text.Trim());
                return;
            }


            if (radioButton3.Checked)
            {
                ViewData(txtFilePath.Text, txtNewPwd.Text.Trim());
                return;
            }

        }

        private void btnAccess_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Application.StartupPath, txtFileName.Text);

            CreateData(fileName);

            AddData(fileName);

            txtFilePath.Text = fileName;
        }

        private void btnAccessPwd_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Application.StartupPath, txtFileName.Text);

            CreateData(fileName, txtPwd.Text.Trim());

            AddData(fileName, txtPwd.Text.Trim());

            txtFilePath.Text = fileName;

            txtOldPwd.Text = txtPwd.Text;
            radioButton2.Checked = true;

        }

        private void txtOldPwd_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void txtNewPwd_Click(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
        }
    }
}

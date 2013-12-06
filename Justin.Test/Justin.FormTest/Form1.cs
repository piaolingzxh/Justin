using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justin.FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private DataSet ds = new DataSet();
        private SQLiteConnection conn = null;
        private SQLiteDataAdapter da = null;
        private const string DRIVER = @"Data Source=D:\System\Document\JStock\JStock.db3;Version=3;Pooling=true;Max Pool Size=100;";
        private const string sql_select = "select * from CheckHistory";

        /**/
        /**
     * 此方法为将数据库northwind中的region表的数据查询出来并放入DataSet中 
    **/
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SQLiteConnection(DRIVER);
            da = new SQLiteDataAdapter(sql_select, conn);
            da.Fill(ds, "table");
            this.dataGridView1.DataSource = ds.Tables["table"].DefaultView;
        }

        private bool BtnInsert() //此方法作用于添加
        {
            da.InsertCommand = conn.CreateCommand();
            da.InsertCommand.CommandText = "INSERT INTO CheckHistory (CheckType, Bank,Amt,CheckTime) " +
    "VALUES (@CheckType, @Bank, @Amt,datetime('now'))";
            da.InsertCommand.Parameters.Add("@CheckType", DbType.String, 255, "CheckType");
            da.InsertCommand.Parameters.Add("@Bank", DbType.String, 255, "Bank");
            da.InsertCommand.Parameters.Add("@Amt", DbType.Decimal, 16, "Amt");

            int count = da.Update(ds);
            bool result = count > 0 ? true : false;
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.BtnInsert())//调用此方法
            {
                MessageBox.Show("添加成功!");
            }
            else
            {
                MessageBox.Show("添加失败!");
            }
        }


        private bool BtnDelect() //此方法作用于删除
        {
            SQLiteParameter sp = new SQLiteParameter();
            da.DeleteCommand = conn.CreateCommand();
            da.DeleteCommand.CommandText = "delete region where regionid=@id";
            sp = da.DeleteCommand.Parameters.Add("@id", DbType.Int16, 4, "regionid");
            sp.SourceVersion = DataRowVersion.Original;
            ds.Tables["table"].Rows[this.dataGridView1.CurrentRow.Index].Delete();
            int count = da.Update(ds);
            bool result = count > 0 ? true : false;
            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.BtnDelect())//调用删除方法
            {
                MessageBox.Show("删除成功!");
            }
            else
            {
                MessageBox.Show("删除失败!");
            }
        }


        private bool BtnUpdate() //此方法作用于修改
        {
            SQLiteParameter sp = new SQLiteParameter();
            da.UpdateCommand = conn.CreateCommand();
            da.UpdateCommand.CommandText = "update region set regionid=@id,regiondescription=@ption where regionid=@oldid";

            da.InsertCommand.Parameters.Add("@CheckType", DbType.String, 255, "CheckType");
            da.InsertCommand.Parameters.Add("@Bank", DbType.String, 255, "Bank");
            da.InsertCommand.Parameters.Add("@Amt", DbType.Decimal, 16, "Amt");


            int count = da.Update(ds);
            bool result = count > 0 ? true : false;
            return result;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.BtnUpdate())//调用修改方法
            {
                MessageBox.Show("修改成功!");
            }
            else
            {
                MessageBox.Show("修改失败!");
            }
        }

    }
}

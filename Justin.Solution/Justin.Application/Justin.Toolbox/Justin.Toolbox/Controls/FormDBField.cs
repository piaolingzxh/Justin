using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Justin.Controls.TestDataGenerator.Entities;

namespace Justin.Toolbox
{
    public delegate void AddFieldDelegate(JField field);
    public partial class FormDBField : Form
    {
        public FormDBField()
        {
            InitializeComponent();
        }
         
        public AddFieldDelegate AddFieldDelegate;

        private void FormDBField_Load(object sender, EventArgs e)
        {
            comboBoxSQLDBType.Items.Clear();

            foreach (var item in Enum.GetNames(typeof(SqlDbType)))
            {
                comboBoxSQLDBType.Items.Add(item);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DBColumn dbColumn = new DBColumn()
            {
                ColumnName = txtColumnName.Text,
                AllowNull = checkBoxAllowNull.Checked,
                DbType = SqlDbType.NVarChar,
            };
            int length = 0;
            if (!string.IsNullOrEmpty(txtLength.Text))
            {
                int.TryParse(txtLength.Text, out  length);
                dbColumn.Length = length;
            }

            if (!string.IsNullOrEmpty(comboBoxSQLDBType.Text))
            {
                SqlDbType dbType;
                if (Enum.TryParse<SqlDbType>(comboBoxSQLDBType.Text, out dbType))
                {
                    dbColumn.DbType = dbType;
                }
            }

            JField field = new JField(dbColumn);

            if (AddFieldDelegate != null)
            {
                AddFieldDelegate(field);
            }
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Extensions;
using Justin.Controls.TestDataGenerator.Entities;

namespace Justin.Controls.TestDataGenerator
{
    public partial class OperandCtrl : UserControl
    {
        public OperandCtrl()
        {
            InitializeComponent();
        }
        private string FieldName { get; set; }
        private JOperateNum OperateNum { get; set; }
        public void PreLoad(IEnumerable<string> fields)
        {
            this.cBoxOtherFieldName.DataSource = fields.ToList();
        }

        public JOperateNum GetOperateNum()
        {
            JValueCategroy valueCategroy;
            bool success = Enum.TryParse<JValueCategroy>(cBoxValueCategroy.Text, true, out valueCategroy);
            if (success)
            {
                JOperateNum num = OperateNum;
                if (num == null)
                    num = new JOperateNum(this.FieldName, JValueType.Numeric);

                num.ValueCategroy = valueCategroy;
                //Range或者Sequence
                num.MinValue = txtMinValue.Text;
                num.MaxValue = txtMaxValue.Text;
                num.Step = txtSeed.Text;
                num.Format = txtFormat.Text;

                //List
                if (string.IsNullOrEmpty(txtSourceList.Text))
                {
                    num.Values = new List<object>();
                }
                else
                {
                    string sourceString = txtSourceList.Text.Trim().TrimEnd(',');
                    num.Values = sourceString.Split(',').Where(row => !string.IsNullOrEmpty(row.Trim())).Select(row => (object)row.Trim()).ToList();
                }
                //引用其他表
                num.ReferenceTableName = txtRefTableName.Text;
                num.ReferenceColumnName = txtRefFieldName.Text;
                num.RefFilter = txtFilter.Text;

                //引用其他字段值
                num.OtherFiledName = cBoxOtherFieldName.Text;
                return num;
            }

            return null;
        }

        public void LoadJOperateNum(JOperateNum tempNum, string fieldName)
        {
            this.FieldName = fieldName;

            if (tempNum == null)
                return;

            SelectTbSourceIndex(tempNum.ValueCategroy);
            cBoxValueCategroy.Text = tempNum.ValueCategroy.ToJString();
            //Range或Sequence
            txtMinValue.Text = tempNum.MinValue.ToJString();
            txtMaxValue.Text = tempNum.MaxValue.ToJString();
            txtSeed.Text = tempNum.Step.ToJString();
            txtFormat.Text = tempNum.Format.ToJString();

            //List
            string sourceStr = "";
            foreach (var item in tempNum.Values)
            {
                sourceStr += item.ToString() + "," + Environment.NewLine;
            }
            if (sourceStr.Length > 0)
            {
                txtSourceList.Text = sourceStr.Remove(sourceStr.Length - 1, 1);
            }
            else
            {
                txtSourceList.Clear();
            }
            //引用其他表字段
            txtRefTableName.Text = tempNum.ReferenceTableName;
            txtRefFieldName.Text = tempNum.ReferenceColumnName;
            txtFilter.Text = tempNum.RefFilter;

            //引用其他字段的值
            cBoxOtherFieldName.Text = tempNum.OtherFiledName;
            this.OperateNum = tempNum;
        }


        private void SelectTbSourceIndex(JValueCategroy sourceValueCategroy)
        {
            switch (sourceValueCategroy)
            {
                case JValueCategroy.List: tbSourceInfo.SelectedIndex = 0; break;
                case JValueCategroy.Range: tbSourceInfo.SelectedIndex = 1; break;
                case JValueCategroy.Sequence: tbSourceInfo.SelectedIndex = 1; break;
                case JValueCategroy.FromTable: tbSourceInfo.SelectedIndex = 2; break;
                case JValueCategroy.OtherField: tbSourceInfo.SelectedIndex = 3; break;
            }
        }

        private void cBoxValueCategroy_SelectedIndexChanged(object sender, EventArgs e)
        {
            JValueCategroy sourceValueCategroy = (JValueCategroy)Enum.Parse(typeof(JValueCategroy), cBoxValueCategroy.Text, true);
            SelectTbSourceIndex(sourceValueCategroy);
        }
    }
}

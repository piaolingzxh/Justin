using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.Controls.TestDataGenerator.Entities;
using Justin.Controls.TestDataGenerator.Utility;
using System.IO;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Extensions;
using ICSharpCode.TextEditor.Document;
using Justin.FrameWork.WinForm.FormUI;
using Justin.FrameWork.WinForm.Models;
using Justin.FrameWork.WinForm.Helper;
using Justin.FrameWork.Settings;

namespace Justin.Controls.TestDataGenerator
{
    //public delegate void ConnStrChangDelegate(string oldConnStr, string newConnStr);
    public partial class TableConfigCtrl : JUserControl, IFile
    {
        #region 变量

        public static string tableConfigFolder = "";
        public static string configFileExtensions = ".config";
        public static string sqlFileExtensions = ".Sql";

        public JTable TableSetting { get; set; }

        #endregion

        public TableConfigCtrl()
        {
            InitializeComponent();
            this.LoadAction = (fileName) =>
            {
                TableSetting = SerializeHelper.XmlDeserializeFromFile<JTable>(fileName);
                BindTableToTree();
            };
            this.SaveAction = (fileName) =>
            {
                if (TableSetting != null)
                {
                    this.TableSetting.SaveSettings(fileName);
                    TableSetting.Modified = false;
                }
            };
        }

        #region 窗体 按钮事件

        private void TableConfigCtrl_Load(object sender, EventArgs e)
        {
            if (this.TableSetting != null)
            {
                BindTableToTree();
            }
            JTable.SqlProcess = (StringBuilder sqlBuilder, JTable table) =>
            {
                string fileName = GetFullFileName(table.TableName, sqlFileExtensions);
                File.AppendAllText(fileName, sqlBuilder.ToString());
            };

            ToolTip tips = new ToolTip();
            this.SetToolTipsForButton(tips);
        }
        private void TableConfigCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TableSetting.Modified)
            {
                TableSetting.Modified = false;
                DialogResult result = MessageBox.Show("是否保存设置？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    TableSetting.SaveSettings(this.FileName);
                    this.ShowMessage(string.Format("表【{0}】配置保存成功!", TableSetting.TableName));
                }
            }
        }

        private void btnSaveFieldInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvDst.SelectedNode == null || tvDst.SelectedNode.ImageIndex == 0)
                {
                    this.ShowMessage("请在左侧树中选择字段！");
                    return;
                }
                string fieldName = tvDst.SelectedNode.Tag.ToString();
                string tableName = tvDst.SelectedNode.Parent.Tag.ToString();
                JField field = TableSetting.Fields.Where(f => f.FieldName == fieldName).FirstOrDefault();
                field.ValueType = (JValueType)Enum.Parse(typeof(JValueType), cBoxValueType.Text, true);
                field.SetVisible(bool.Parse(cBoxVisible.Text));

                field.FirstOperand = operandCtrl1.GetOperateNum();
                field.SecondOperand = operandCtrl2.GetOperateNum();
                field.Operator = cBoxOperator.Text;

                if (field.FirstOperand.ValueCategroy == JValueCategroy.OtherField)
                {
                    field.Order = TableSetting.Order;
                    TableSetting.Order = TableSetting.Order + 1;
                }

                if (TableSetting.DataCount == 0)
                {
                    tabControl1.SelectedIndex = 1;
                }
                tvDst.SelectedNode.ForeColor = Color.Green;
                this.ShowMessage(string.Format("表【{0}】字段【{1}】配置信息保存成功!", tableName, fieldName));
                TableSetting.Modified = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "设置生成规则失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void btnSaveTable_Click(object sender, EventArgs e)
        {
            if (tvDst.SelectedNode != null)
            {
                TableSetting.DataCount = int.Parse(txtDataCount.Text);
                TableSetting.BeforeSQL = txtBeforeSQL.Text;
                TableSetting.AfterSQL = txtAfterSQL.Text;
                TableSetting.Modified = true;
            }
            this.ShowMessage(string.Format("表【{0}】Table配置信息保存成功!", TableSetting.TableName));
        }
        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            base.SaveFile(this.FileName);
        }
        private void btnGenerateData_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                if (!CheckTableSetting())
                {
                    return;
                }
                string fileName = GetFullFileName(TableSetting.TableName, sqlFileExtensions);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                TableSetting.Process(this.ConnStr);
                this.ShowMessage(string.Format("表【{0}】SQL【{1}】生成成功!", TableSetting.TableName, fileName));
            });
        }
        private void btnExecuteTableSQL_Click(object sender, EventArgs e)
        {
            this.CheckConnStringAssigned(() =>
            {
                string fileName = GetFullFileName(TableSetting.TableName, sqlFileExtensions);

                using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                {
                    string sql = sr.ReadToEnd();
                    SqlHelper.ExecuteNonQuery(this.ConnStr, CommandType.Text, sql, null);

                }
                this.ShowMessage(string.Format("表【{0}】SQL【{1}】执行成功!", TableSetting.TableName, fileName));
            });
        }

        #endregion

        #region 树、树的右键菜单 事件

        private void tvDst_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvDst.SelectedNode = e.Node;
        }
        private void visualeMenuItemOfDst_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvDst.SelectedNode.Parent != null)
                {
                    string fieldName = tvDst.SelectedNode.Tag.ToString();
                    JField field = TableSetting.Fields.Where(f => f.FieldName == fieldName).FirstOrDefault();
                    field.SetVisible(!field.Visible);
                    BindFieldInfo(field);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "设置是否生成该列数据", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tvDst_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvDst.SelectedNode.Parent != null)
            {
                string fieldName = tvDst.SelectedNode.Tag.ToString();
                IEnumerable<string> allFieldNames = TableSetting.Fields.Select(row => row.FieldName);
                operandCtrl1.PreLoad(allFieldNames);
                operandCtrl2.PreLoad(allFieldNames);
                JField field = TableSetting.Fields.Where(f => f.FieldName == fieldName).FirstOrDefault();
                BindFieldInfo(field);
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                BindTableInfo();
                tabControl1.SelectedIndex = 1;
            }
        }

        private void addFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormDBField formDBField = new FormDBField();
            //formDBField.AddFieldDelegate = (field) =>
            //{
            //    TreeNode fieldNode = PrepareNode(field);
            //    tvDst.Nodes[0].Nodes.Add(fieldNode);
            //    this.TableSetting.Fields.Add(field);
            //    this.TableSetting.Modified = true;
            //    this.ShowMessage(string.Format("已添加字段{0}", field.FieldName));
            //};
            //formDBField.StartPosition = FormStartPosition.CenterParent;
            //formDBField.ShowDialog();
        }
        private void deleteFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvDst.SelectedNode.ImageIndex != 0)
            {
                tvDst.Nodes[0].Nodes.Remove(this.tvDst.SelectedNode);
                string fieldName = this.tvDst.SelectedNode.Tag as string;
                this.TableSetting.Fields.Remove(this.TableSetting.Fields.FirstOrDefault(row => row.FieldName == fieldName));
                this.TableSetting.Modified = true;
            }
        }

        #endregion

        #region 辅助函数

        private void BindTableToTree()
        {
            tvDst.Nodes.Clear();
            tvDst.ImageList = imageList1;
            var tableNode = new TreeNode(TableSetting.TableName);
            tableNode.Tag = TableSetting.TableName;
            tableNode.ImageIndex = tableNode.SelectedImageIndex = 0;
            foreach (var field in TableSetting.Fields.OrderBy(row => row.FirstOperand.ValueCategroy).ThenBy(row => row.FieldName))
            {
                TreeNode fieldNode = PrepareNode(field);
                tableNode.Nodes.Add(fieldNode);
            }
            tvDst.Nodes.Add(tableNode);
            tvDst.ExpandAll();
            tvDst.SelectedNode = tvDst.Nodes[0];
            BindTableInfo();
        }
        public TreeNode PrepareNode(JField field)
        {
            var fieldNode = new TreeNode(field.FieldName);
            fieldNode.Tag = field.FieldName;
            if (field.ValueType == JValueType.DateTime)
            {
                fieldNode.ImageIndex = fieldNode.SelectedImageIndex = 4;
            }
            else if (field.ValueType == JValueType.Numeric)
            {
                fieldNode.ImageIndex = fieldNode.SelectedImageIndex = 5;
            }
            else
            {
                fieldNode.ImageIndex = fieldNode.SelectedImageIndex = 1;
            }
            if (!field.Visible)
            {
                if (!field.AllowNull)
                {
                    fieldNode.ForeColor = Color.Red;
                }
                else
                {
                    fieldNode.ForeColor = Color.OrangeRed;
                }
            }

            return fieldNode;
        }
        private void BindFieldInfo(JField field)
        {
            if (field == null)
            {
                return;
            }
            lbFieldName.Text = field.FieldName;
            cBoxValueType.Text = field.ValueType.ToJString();
            cBoxVisible.Text = field.Visible.ToJString("True");

            cBoxOperator.Text = field.Operator;
            operandCtrl1.LoadJOperateNum(field.FirstOperand, field.FieldName);
            operandCtrl2.LoadJOperateNum(field.SecondOperand, field.FieldName);
        }
        private void BindTableInfo()
        {
            txtDataCount.Text = TableSetting.DataCount.ToJString();
            txtBeforeSQL.Encoding = System.Text.Encoding.Default;
            txtBeforeSQL.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtAfterSQL.Encoding = System.Text.Encoding.Default;
            txtAfterSQL.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            txtBeforeSQL.Text = TableSetting.BeforeSQL;
            txtAfterSQL.Text = TableSetting.AfterSQL;
        }

        private bool CheckTableSetting()
        {
            string messageStr = "";

            if (TableSetting.DataCount > 0)
            {
                foreach (var field in TableSetting.Fields)
                {
                    if (field.FirstOperand == null)
                    {
                        messageStr += string.Format("{0}:{1}需设置SourceValueCategroy", TableSetting.TableName, field.FieldName) + Environment.NewLine;
                    }
                }
            }
            if (!string.IsNullOrEmpty(messageStr))
            {
                this.ShowMessage(messageStr);
            }
            return string.IsNullOrEmpty(messageStr);
        }


        public static string GetFullFileName(string tableName, string fileExtension)
        {
            string fullFileName = Path.Combine(tableConfigFolder, string.Format("{0}{1}", tableName, fileExtension));
            return fullFileName;
        }
        public string Extension
        {
            get { return ".config"; }
        }
        #endregion


    }
}

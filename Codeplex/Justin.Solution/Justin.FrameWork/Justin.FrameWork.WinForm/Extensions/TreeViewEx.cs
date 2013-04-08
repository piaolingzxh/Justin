using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Extensions;
namespace Justin.FrameWork.WinForm.Extensions
{
    public static class TreeViewEx
    {
        public static TreeView BuildTree(this TreeView treeView,string connStr, string sql)
        {
            DataTable table = SqlHelper.ExecuteDataTable(connStr, CommandType.Text, sql, null);
            List<JNodeData> datas = new List<JNodeData>();
            foreach (var item in table.Rows.Cast<DataRow>())
            {
                string id = item["ID"].Value<string>();
                string pid = item["PID"].Value<string>();
                string name = item["Name"].Value<string>();
                JNodeData node = new JNodeData(id, pid,name, item);
                datas.Add(node);
            }
            treeView.Nodes.Add(treeView.BuildChildNode("0", datas));
            //treeView.Nodes.Add();    
            return treeView;
        }
        public static TreeNode BuildChildNode(this TreeView treeView, string id, List<JNodeData> datas)
        {
            TreeNode rootNode = new TreeNode(id);
            JNodeData rootData = datas.FirstOrDefault(row => row.Id == id);
            if (rootData != null)
            {
                rootNode.ToolTipText = rootData.Text;
                rootNode.Tag = rootData.Tag;
            }
            var childrenNodeData = datas.Where(row => row.PId == id);
            if (childrenNodeData == null || childrenNodeData.Count() == 0)
            {
                return rootNode;
            }
            foreach (JNodeData childNodeData in childrenNodeData)
            {
                TreeNode childNode = treeView.BuildChildNode(childNodeData.Id, datas);
                if (childNode != null)
                {
                    // childNode.ParentNode = rootNode;
                    rootNode.Nodes.Add(childNode);
                }
            }
            return rootNode;
        }
        public class JNodeData
        {
            public JNodeData(string id, string pId,string text ,object tag = null)
            {
                this.Id = id;
                this.Text = text;
                this.PId = pId;
                this.Tag = tag;
            }

            public string Id { get; set; }
            public string Text { get; set; }
            public object Tag { get; set; }
            public string PId { get; set; }
        }

    }
}

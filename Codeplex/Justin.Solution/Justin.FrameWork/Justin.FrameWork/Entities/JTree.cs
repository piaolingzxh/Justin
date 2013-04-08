using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.Entities
{
    public class JTreeView
    {
        public JTreeView(List<JNodeData> datas )
        {
            RootNode = BuildChildNode("0", datas);
        }
        public JNode RootNode { get; set; }
        public object RootId { get; private set; }
        public static JNode BuildChildNode(string id, List<JNodeData> datas)
        {
            JNode rootNode = new JNode(id);

            var childrenNodeData = datas.Where(row => row.PId == id);
            if (childrenNodeData == null || childrenNodeData.Count() == 0)
            {
                return rootNode;
            }
            foreach (JNodeData childNodeData in childrenNodeData)
            {
                JNode childNode = BuildChildNode(childNodeData.Id, datas);
                if (childNode != null)
                {
                    childNode.ParentNode = rootNode;
                    rootNode.ChildrenNodes.Add(childNode);
                }
            }
            return rootNode;
        }
    }

    public class JNodeData
    {
        public JNodeData(string id, string pId, object tag = null)
        {
            this.Id = id;
            this.PId = pId;
            this.Tag = tag;
        }
        public string Id { get; set; }
        public string PId { get; set; }
        public object Tag { get; set; }
    }
    public class JNode
    {
        public JNode(string id, object tag = null)
        {
            this.Id = id;
            this.Tag = tag;
            this.ChildrenNodes = new List<JNode>();
        }
        public string Id { get; set; }
        public string Text { get; set; }
        public List<JNode> ChildrenNodes { get; set; }
        public JNode ParentNode { get; set; }
        public string Path
        {
            get
            {
                string path = Id;
                JNode parentNode = this.ParentNode;
                while (parentNode != null)
                {
                    path = parentNode.Id + "-" + path;
                    parentNode = parentNode.ParentNode;
                }
                return path;
            }
        }
        public object Tag { get; set; }

        public List<JNodeData> Data
        {
            get
            {
                List<JNodeData> datas = new List<JNodeData>();

                if (ParentNode != null)
                {
                    datas.Add(new JNodeData(Id, this.ParentNode.Id));
                }
                else
                {
                    datas.Add(new JNodeData(Id, "1"));
                }
                if (this.ChildrenNodes != null && this.ChildrenNodes.Count > 0)
                {
                    foreach (var childNode in this.ChildrenNodes)
                    {
                        List<JNodeData> childDatas = childNode.Data;
                        if (childDatas != null)
                        {
                            datas.AddRange(childDatas);
                        }
                    }
                }
                return datas;

            }
        }
    }
}

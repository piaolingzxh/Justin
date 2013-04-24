using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.IO;
using Newtonsoft.Json;
using ICSharpCode.TextEditor.Document;
using System.Threading;
using Justin.Log;
using Justin.FrameWork.Settings;
using Justin.FrameWork.WinForm.Models;
using Justin.FrameWork.WinForm.FormUI;

namespace Justin.Controls.JsonView
{
    public partial class JsonViewCtrl : JUserControl,IFile
    {
        private PluginsManager _pluginsManager = new PluginsManager();
        public JsonViewCtrl()
        {
            InitializeComponent();
            this.LoadAction = (fileName) =>
            {
                txtJson.LoadFile(fileName);
            };
            this.SaveAction = (fileName) =>
            {
                txtJson.SaveFile(fileName);
            };
        }

        #region 工具按钮事件


        private void btnFormat_Click(object sender, EventArgs e)
        {
            JsonFormat(txtJson);
        }
        private void btnStripToSqr_Click(object sender, EventArgs e)
        {
            StripTextTo('[', ']');
        }
        private void btnStripToCurly_Click(object sender, EventArgs e)
        {
            StripTextTo('{', '}');
        }
        private void removeSpecialCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = txtJson.Text;
            text = text.Replace(@"\""", @"""");
            txtJson.SetText(text);
        }
        private void removeNewLineMenuItem_Click(object sender, EventArgs e)
        {
            StripFromText('\n', '\r');
        }

        #endregion

        #region 树视图

        private void tvJson_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvJson.SelectedNode = e.Node;
        }
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            JsonViewerTreeNode node = (JsonViewerTreeNode)tvJson.SelectedNode;
            if (node != null)
            {
                Clipboard.SetText(node.Text);
            }
        }
        private void mnuCopyValue_Click(object sender, EventArgs e)
        {
            JsonViewerTreeNode node = (JsonViewerTreeNode)tvJson.SelectedNode;
            if (node != null && node.JsonObject.Value != null)
            {
                Clipboard.SetText(node.JsonObject.Value.ToString());
            }
        }
        private void mnuExpandAll_Click(object sender, EventArgs e)
        {
            tvJson.BeginUpdate();
            try
            {
                if (tvJson.SelectedNode != null)
                {
                    TreeNode topNode = tvJson.TopNode;
                    tvJson.SelectedNode.ExpandAll();
                    tvJson.TopNode = topNode;
                }
            }
            finally
            {
                tvJson.EndUpdate();
            }
        }

        #endregion

        private void btnShow_Click(object sender, EventArgs e)
        {
            string json = GetJson(txtJson);
            try
            {
                tvJson.BeginUpdate();
                try
                {
                    tvJson.Nodes.Clear();
                    if (!String.IsNullOrEmpty(json))
                    {
                        JsonObjectTree tree = JsonObjectTree.Parse(json);
                        AddNode(tvJson.Nodes, tree.Root);
                        JsonViewerTreeNode node = null;
                        if (tvJson.Nodes.Count > 0)
                            node = (JsonViewerTreeNode)tvJson.Nodes[0];
                        InitVisualizers(node);
                        node.Expand();
                        tvJson.SelectedNode = node;
                    }
                }
                finally
                {
                    tvJson.EndUpdate();
                }
            }
            catch (JsonParseError ex)
            {
                this.ShowMessage(ex);
                //GetParseErrorDetails(ex);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindNext(txtSearch.Text, true);
        }

        private void JsonViewCtrl_Load(object sender, EventArgs e)
        {
            txtJson.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtJson.Encoding = Encoding.Default;
        }

        public bool FindNext(string text, bool includeSelected)
        {
            TreeNode startNode = tvJson.SelectedNode;
            if (startNode == null && tvJson.Nodes.Count > 0)
                startNode = (JsonViewerTreeNode)tvJson.Nodes[0];
            if (startNode != null)
            {
                startNode = FindNext(startNode, text, includeSelected);
                if (startNode != null)
                {
                    tvJson.SelectedNode = startNode;
                    return true;
                }
            }
            return false;
        }

        private void StripTextTo(char sChr, char eChr)
        {
            string text = txtJson.Text;
            int start = text.IndexOf(sChr);
            int end = text.LastIndexOf(eChr);
            int newLen = end - start + 1;
            if (newLen > 1)
            {
                txtJson.SetText(text.Substring(start, newLen));
            }
        }
        private void StripFromText(params char[] chars)
        {
            string text = txtJson.Text;
            foreach (char ch in chars)
            {
                text = text.Replace(ch.ToString(), "");
            }
            txtJson.SetText(text);
        }

        public TreeNode FindNext(TreeNode startNode, string text, bool includeSelected)
        {
            if (text == String.Empty)
                return startNode;

            if (includeSelected && IsMatchingNode(startNode, text))
                return startNode;

            TreeNode originalStartNode = startNode;
            startNode = GetNextNode(startNode);
            text = text.ToLower();
            while (startNode != originalStartNode)
            {
                if (IsMatchingNode(startNode, text))
                    return startNode;
                startNode = GetNextNode(startNode);
            }

            return null;
        }
        private bool IsMatchingNode(TreeNode startNode, string text)
        {
            return (startNode.Text.ToLower().Contains(text));
        }
        private void AddNode(TreeNodeCollection nodes, JsonObject jsonObject)
        {
            JsonViewerTreeNode newNode = new JsonViewerTreeNode(jsonObject);
            nodes.Add(newNode);
            newNode.Text = jsonObject.Text;
            newNode.Tag = jsonObject;
            newNode.ImageIndex = (int)jsonObject.JsonType;
            newNode.SelectedImageIndex = newNode.ImageIndex;

            foreach (JsonObject field in jsonObject.Fields)
            {
                AddNode(newNode.Nodes, field);
            }
        }
        private void InitVisualizers(JsonViewerTreeNode node)
        {
            if (!node.Initialized)
            {
                node.Initialized = true;
                JsonObject jsonObject = node.JsonObject;
                foreach (ICustomTextProvider textVis in _pluginsManager.TextVisualizers)
                {
                    if (textVis.CanVisualize(jsonObject))
                        node.TextVisualizers.Add(textVis);
                }

                node.RefreshText();

                foreach (IJsonVisualizer visualizer in _pluginsManager.Visualizers)
                {
                    if (visualizer.CanVisualize(jsonObject))
                        node.Visualizers.Add(visualizer);
                }
            }
        }
        private TreeNode GetNextNode(TreeNode startNode)
        {
            TreeNode next = startNode.FirstNode ?? startNode.NextNode;
            if (next == null)
            {
                while (startNode != null && next == null)
                {
                    startNode = startNode.Parent;
                    if (startNode != null)
                        next = startNode.NextNode;
                }
                if (next == null)
                {
                    next = tvJson.Nodes[0];
                    //FlashControl(txtFind, Color.Cyan);
                }
            }
            return next;
        }
        private void FlashControl(Control control, Color color)
        {
            Color prevColor = control.BackColor;
            try
            {
                control.BackColor = color;
                control.Refresh();
                Thread.Sleep(25);
            }
            finally
            {
                control.BackColor = prevColor;
                control.Refresh();
            }
        }

        public string GetJson(TextEditorControl editor)
        {
            string json = editor.ActiveTextAreaControl.SelectionManager.SelectedText.Trim();
            if (!string.IsNullOrEmpty(json))
                return json;

            int startNum = 0;
            StringBuilder builder = new StringBuilder();
            string line;

            for (int i = 0; i < editor.Document.TotalNumberOfLines; i++)
            {
                line = editor.Document.GetText(editor.Document.GetLineSegment(i));

                if (line.Contains(Constants.SQLParagraphStartFlag))   //当遇到开始符时，则设置已开始标识
                {
                    startNum++;
                    continue;
                }
                if (startNum > 0 && line.Contains(Constants.SQLParagraphEndFlag))
                {
                    startNum--;
                    continue;
                }

                if (startNum == 0)
                {
                    builder.Append(line).AppendLine();
                }
            }

            return builder.ToString();
        }
        public void JsonFormat(TextEditorControl editor)
        {
            try
            {
                string json = GetJson(editor);
                JsonSerializer s = new JsonSerializer();
                JsonReader reader = new JsonReader(new StringReader(json));
                Object jsonObject = s.Deserialize(reader);
                if (jsonObject != null)
                {
                    StringWriter sWriter = new StringWriter();
                    JsonWriter writer = new JsonWriter(sWriter);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 4;
                    writer.IndentChar = ' ';
                    s.Serialize(writer, jsonObject);
                    editor.Text = sWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }
        }

        public string Extension
        {
            get { return ".json"; }
        }
 
    }
}

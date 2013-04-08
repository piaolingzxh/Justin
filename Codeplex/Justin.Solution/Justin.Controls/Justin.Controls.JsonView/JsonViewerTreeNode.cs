using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  Justin.Controls.JsonView
{
    public class JsonViewerTreeNode : TreeNode
    {
        JsonObject _jsonObject;
        List<ICustomTextProvider> _textVisualizers = new List<ICustomTextProvider>();
        List<IJsonVisualizer> _visualizers = new List<IJsonVisualizer>();
        private bool _init;
        private IJsonVisualizer _lastVisualizer;

        public JsonViewerTreeNode(JsonObject jsonObject)
        {
            _jsonObject = jsonObject;
        }

        public List<ICustomTextProvider> TextVisualizers
        {
            get
            {
                return _textVisualizers;
            }
        }

        public List<IJsonVisualizer> Visualizers
        {
            get
            {
                return _visualizers;
            }
        }

        public JsonObject JsonObject
        {
            get
            {
                return _jsonObject;
            }
        }

        internal bool Initialized
        {
            get
            {
                return _init;
            }
            set
            {
                _init = value;
            }
        }

        internal void RefreshText()
        {
            StringBuilder sb = new StringBuilder(_jsonObject.Text);
            foreach (ICustomTextProvider textVisualizer in _textVisualizers)
            {
                try
                {
                    string customText = textVisualizer.GetText(_jsonObject);
                    sb.Append(" (" + customText + ")");
                }
                catch
                {
                    //silently ignore
                }
            }
            string text = sb.ToString();
            if (text != this.Text)
                this.Text = text;
        }

        public IJsonVisualizer LastVisualizer
        {
            get
            {
                return _lastVisualizer;
            }
            set
            {
                _lastVisualizer = value;
            }
        }
    }
}

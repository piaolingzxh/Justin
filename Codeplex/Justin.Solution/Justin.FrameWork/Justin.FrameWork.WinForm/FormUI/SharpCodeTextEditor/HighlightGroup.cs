using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace Justin.FrameWork.WinForm.FormUI.SharpCodeTextEditor
{
    public class HighlightGroup : IDisposable
    {
        List<TextMarker> _markers = new List<TextMarker>();
        TextEditorControl _editor;
        IDocument _document;
        public HighlightGroup(TextEditorControl editor)
        {
            _editor = editor;
            _document = editor.Document;
        }
        public void AddMarker(TextMarker marker)
        {
            _markers.Add(marker);
            _document.MarkerStrategy.AddMarker(marker);
        }
        public void ClearMarkers()
        {
            foreach (TextMarker m in _markers)
                _document.MarkerStrategy.RemoveMarker(m);
            _markers.Clear();
            _editor.Refresh();
        }
        public void Dispose() { ClearMarkers(); GC.SuppressFinalize(this); }
        ~HighlightGroup() { Dispose(); }

        public IList<TextMarker> Markers { get { return _markers.AsReadOnly(); } }
    }
}

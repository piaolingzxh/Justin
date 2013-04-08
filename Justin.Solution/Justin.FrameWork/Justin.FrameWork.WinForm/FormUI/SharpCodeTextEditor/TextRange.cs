using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.TextEditor.Document;

namespace Justin.FrameWork.WinForm.FormUI.SharpCodeTextEditor
{
    public class TextRange : AbstractSegment
    {
        IDocument _document;
        public TextRange(IDocument document, int offset, int length)
        {
            _document = document;
            this.offset = offset;
            this.length = length;
        }
    }
}

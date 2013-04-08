using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSharpCode.TextEditor.Document
{
    public static class TextEditorControlEx
    {
        public static void SetText(this TextEditorControl instance, string text)
        {
            instance.Text = text;
            instance.Refresh();
        }
    }
}

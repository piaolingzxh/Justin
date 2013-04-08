using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssociationManager
{
    public class ApplicationIcon
    {
        private readonly string _iconLibraryPath;
        private readonly int? _iconIndex;

        public ApplicationIcon(string iconlibrarypath)
        {
            _iconLibraryPath = iconlibrarypath;
            _iconIndex = null;
        }

        public ApplicationIcon(string iconlibrarypath, int iconindex)
        {
            _iconLibraryPath = iconlibrarypath;
            _iconIndex = iconindex;
        }

        public string IconLibraryPath
        {
            get { return _iconLibraryPath; }
        }

        public int? IconIndex
        {
            get { return _iconIndex; }
        }
    }
}

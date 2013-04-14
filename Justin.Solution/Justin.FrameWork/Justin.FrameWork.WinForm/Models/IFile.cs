using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.WinForm.Models
{
    public interface IFile
    {
        void LoadFile(string fileName);
        void SaveFile(string fileName);
    }
}

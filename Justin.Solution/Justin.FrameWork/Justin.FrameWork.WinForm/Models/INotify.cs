using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Justin.FrameWork.WinForm.Models
{
    public interface INotify
    {
        double Opacity { get; set; }
        void Show(string msg, string title = "", string tips = "");
        void Show(string msgFormat, params object[] args);
        void Show(string msgFormat, string detailMsg = "", params object[] msgArgs);
    }
}

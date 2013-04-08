using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Justin.FrameWork.Helper;
using Justin.Stock.DAL;
using Justin.Stock.Service.Models;
using Justin.Stock.Service.Entities;

namespace Justin.Stock.Controls.Entities
{
    public class JSettings
    {
        public JSettings()
        {
            StartPosition = new StartPosition();
        }
        public string DBPath { get; set; }
        public decimal Balance { get; set; }
        public string DeskDisplayFormat { get; set; }

        public bool ShowWarn { get; set; }
        public bool CheckTime { get; set; }
        public StartPosition StartPosition { get; set; }
        public ServiceProvider MonitorSite { get; set; }
    }
    public class StartPosition
    {
        public StartPosition()
        {
        }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

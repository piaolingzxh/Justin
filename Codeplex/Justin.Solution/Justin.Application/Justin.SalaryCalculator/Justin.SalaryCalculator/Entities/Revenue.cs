using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.SalaryCalculator.Entities
{
    public class RevenuePolicy
    {
        [XmlElement("Before")]
        public RevenueInfo RevenuePolicyBefore { get; set; }
        [XmlElement("After")]
        public RevenueInfo RevenuePolicyAfter { get; set; }
    }
    public class RevenueInfo
    {
        [XmlAttribute("Base")]
        public double RevenueBase { get; set; }
        [XmlArray(), XmlArrayItem("Level")]
        public List<RevenueLevel> Leveles { get; set; }
    }
    [XmlRoot("Level")]
    public class RevenueLevel
    {
        [XmlAttribute("value")]
        public int Level { get; set; }
        [XmlAttribute()]
        public double Max { get; set; }
        [XmlAttribute()]
        public double Min { get; set; }
        [XmlAttribute()]
        public double Add { get; set; }
        [XmlAttribute()]
        public double Percent { get; set; }
    }
}

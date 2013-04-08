using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Justin.BI.DBLibrary.TestDataGenerate
{
    public enum FileType
    {
        [FileInfoAttribute("JTDTC", "测试数据表配置", new string[] { "CONFIG", "XML" })]
        [Description("记录表各个字段生成测试数据SQL规则的文件")]
        TableConfig,


        [FileInfoAttribute("JSQL", "JSQL", new string[] { "SQL" })]
        [Description("JSQL文件")]
        SQL       ,

        [FileInfoAttribute("MDX", "MDX", new string[] { "MDX" })]
        [Description("MDX文件")]
        MDX,
    }
}

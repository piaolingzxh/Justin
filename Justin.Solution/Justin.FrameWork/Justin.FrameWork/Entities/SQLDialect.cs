using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Justin.FrameWork.Entities
{
    public enum SQLDialect
    {
        [XmlEnum("generic")]
        Generic,
        [XmlEnum("access")]
        Access,
        [XmlEnum("db2")]
        Db2,
        [XmlEnum("derby")]
        Derby,
        [XmlEnum("firebird")]
        Firebird,
        [XmlEnum("hsqldb")]
        Hsqldb,
        [XmlEnum("mssql")]
        Mssql,
        [XmlEnum("mysql")]
        Mysql,
        [XmlEnum("oracle")]
        Oracle,
        [XmlEnum("postgres")]
        Postgres,
        [XmlEnum("sysbase")]
        Sysbase,
        [XmlEnum("teradata")]
        Teradata,
        [XmlEnum("ingres")]
        Ingres,
        [XmlEnum("infobright")]
        Infobright,
        [XmlEnum("luciddb")]
        Luciddb,
        [XmlEnum("vertica")]
        Vertica,
        [XmlEnum("neoview")]
        Neoview,
        [XmlEnum("greenplum")]
        Greenplum,
        [XmlEnum("vectorwise")]
        Vectorwise,
        [XmlEnum("hive")]
        Hive,
    }
}

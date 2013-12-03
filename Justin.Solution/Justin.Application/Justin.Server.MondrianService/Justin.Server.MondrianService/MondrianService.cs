using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Justin.FrameWork.Entities;

namespace Justin.Server.MondrianService
{
    public class MondrianService
    {
        //tomcat
        private static string _appName = "Justin_Mondrian";
        private static int _port = 8894;

        private static string defaultDataSourceInfoFormat = "Provider=mondrian;JdbcDrivers={0};Jdbc=jdbc:{1};Catalog={2};";
 
        private static string OracleJdbcDrivers = "oracle.jdbc.driver.OracleDriver";
        private static string OracleJdbcConnStringValueFormat = "oracle:thin:@//{0}:{4}/{1};JdbcUser={2};JdbcPassword={3}";
        private static string MSSQLJdbcDrivers = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
        private static string MSSQLJdbcConnStringValueFormat = "sqlserver://{0};jdbc.databaseName={1};jdbc.username={2};jdbc.password={3}";


        public void Start(string tomcatRootPath, string jreExecuteFileName, string mondrianRootPath, int port)
        {
            _port = port;
            SetTomcatPort(tomcatRootPath);
            SetMondrianDataSourceConfig(mondrianRootPath);

            StartTomcat(tomcatRootPath, jreExecuteFileName);
        }

        #region mondrian

        private void SetMondrianDataSourceConfig(string mondrianRootPath)
        {
            bool needSave = false;
            string datasourceXMLPath = Path.Combine(mondrianRootPath, @"WEB-INF\datasources.xml");
            XmlDocument datasourceXMLdoc = LoadXML(datasourceXMLPath);
            string jdbcValue = "";
            string jdbcDrivers = "";
       
            string oledbConnString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            ReadDataSource(oledbConnString, out jdbcValue, out jdbcDrivers);

            var categologs = datasourceXMLdoc.GetElementsByTagName("Catalog").Cast<XmlElement>().ToList();
            foreach (XmlElement catalogElement in categologs)
            {
                string dataSourceInfoFormat = defaultDataSourceInfoFormat;
                if (catalogElement.ChildNodes.Count > 2)
                {
                    if (string.IsNullOrEmpty(catalogElement.ChildNodes[2].InnerText))
                    {
                        catalogElement.ChildNodes[2].InnerText = defaultDataSourceInfoFormat;
                        needSave = true;
                    }
                    else
                    {
                        dataSourceInfoFormat = catalogElement.ChildNodes[2].InnerText;
                    }
                }
                string javaMondrianConnStr = string.Format(dataSourceInfoFormat, jdbcDrivers, jdbcValue, catalogElement.ChildNodes[1].InnerText);
                if (!catalogElement.ChildNodes[0].InnerText.Equals(javaMondrianConnStr))
                {
                    catalogElement.ChildNodes[0].InnerText = javaMondrianConnStr;
                    needSave = true;
                }
            }
            if (needSave)
                datasourceXMLdoc.Save(datasourceXMLPath);
        }

        private void ReadDataSource(string oledbConnectionString, out string jdbcValue, out string jdbcDrivers)
        {
            OleDbConnectionStringBuilder sb = new OleDbConnectionStringBuilder(oledbConnectionString);

            string provider = sb.Provider;
            SQLDialect dialect = SQLDialect.Generic;
            if (provider == "sqloledb")
            {
                dialect = SQLDialect.Mssql;
            }
            else if (provider == "oraoledb" || provider == "msdaora")
            {
                dialect = SQLDialect.Oracle;
            }
            else
            {
                throw new NotSupportedException(string.Format("不支持此数据库", dialect.ToString()));
            }

            string dataSourceValue = sb.DataSource;

            string serverName = "";
            string database = "";
            int port = 1521;
            jdbcValue = "";
            jdbcDrivers = "";

            string userName = sb["User ID"].ToString();
            string UserPwd = sb["Password"].ToString();
            if (dialect == SQLDialect.Mssql)
            {
                string initialCatalog = sb["Initial Catalog"].ToString();
                serverName = dataSourceValue;
                database = initialCatalog;
                port = 1433;
                jdbcValue = string.Format(MSSQLJdbcConnStringValueFormat
              , serverName
              , database
              , userName
              , UserPwd
              , port
              );
                jdbcDrivers = MSSQLJdbcDrivers;
            }
            else if (dialect == SQLDialect.Oracle)
            {
                string[] serverInfo1 = dataSourceValue.Split('/');
                string[] serverInfo2 = serverInfo1[0].Split(':');

                serverName = serverInfo2[0];
                database = serverInfo1[1];
                port = serverInfo2.Length < 2 || string.IsNullOrEmpty(serverInfo2[1]) ? 1521 : int.Parse(serverInfo2[1]);
                jdbcValue = string.Format(OracleJdbcConnStringValueFormat
                 , serverName
                 , database
                 , userName
                 , UserPwd
                 , port
                 );
                jdbcDrivers = OracleJdbcDrivers;
            }


        }

        #endregion

        #region tomcat

        private void SetTomcatPort(string tomcatRootPath)
        {
            string tomcatServerConfigFileName = Path.Combine(tomcatRootPath, @"conf\server.xml");
            XmlDocument serverXML = LoadXML(tomcatServerConfigFileName);
            XmlNodeList list = serverXML.GetElementsByTagName("Connector");
            bool portChanged = false;
            if (list != null)
            {
                foreach (XmlElement item in list)
                {
                    if (item.Attributes["port"].Value != _port.ToString())
                    {
                        item.SetAttribute("port", _port.ToString());
                        if (!portChanged)
                            portChanged = true;
                    }
                }
            }
            XmlElement contextEl = (serverXML.GetElementsByTagName("Context")[0] as XmlElement);
            bool docBaseChanged = false;
            if (contextEl != null)
            {
                if (contextEl.Attributes["docBase"].Value != _appName)
                {
                    contextEl.SetAttribute("docBase", _appName);
                    docBaseChanged = true;
                }
            }
            if (docBaseChanged || portChanged)
                SaveXML(tomcatServerConfigFileName, serverXML);
        }
        private void StartTomcat(string tomcatRootPath, string jreExecuteFileName)
        {
            string _min = "1024";
            string _max = "1024";
            //润乾技术说加上后面这组参数可以解决内在溢出的问题：-XX:MaxNewSize=256m -XX:MaxPermSize=256m 没有进行验证。
            string xms = string.Format(" -Xms{0}m -Xmx{1}m  ", _min, _max);
            string args = string.Format("{0}-Djava.util.logging.config.file=\"{1}\\conf\\logging.properties\" -Djava.util.logging.manager=org.apache.juli.ClassLoaderLogManager -Djava.endorsed.dirs=\"{1}\\endorsed\" -classpath \"{1}\\bin\\bootstrap.jar;{1}\\bin\\tomcat-juli.jar\" -Dcatalina.base=\"{1}\" -Dcatalina.home=\"{1}\" -Djava.io.tmpdir=\"{1}\\temp\" org.apache.catalina.startup.Bootstrap  start", xms, tomcatRootPath);
            string command = jreExecuteFileName;// Path.Combine(basepath + @"\tools\jre\bin\", _processName + ".exe");
            Process p = new Process();
            p.StartInfo.FileName = command;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = tomcatRootPath;
            p.Start();
        }

        #endregion

        #region xml

        private static void SaveXML(string file, XmlDocument xmlDoc)
        {
            //开始格式化代码
            System.Xml.XmlWriterSettings set = new XmlWriterSettings();
            set.Indent = true;
            set.IndentChars = "\t";
            set.Encoding = System.Text.Encoding.UTF8;
            using (System.Xml.XmlWriter writer = XmlWriter.Create(file, set))
            {
                xmlDoc.Save(writer);
                writer.Flush();
            }
        }
        private static XmlDocument LoadXML(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception(string.Format("Mondrian_OLAP服务启动失败，读取文件[{0}]更新服务配置时出错。文件不存在。", fileName));
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                return xmlDoc;
            }
        }

        #endregion


    }


}

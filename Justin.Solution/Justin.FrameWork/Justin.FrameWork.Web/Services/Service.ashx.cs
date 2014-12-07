using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Justin.FrameWork.Web.Extensions;
using GTP.BI.WebIntegration.AjaxServices;
using Justin.FrameWork.Web.Helper;

namespace GTP.BI.WebIntegration
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    public class Service : IHttpHandler, IRequiresSessionState
    {
        private string cmd { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            Stopwatch watch = Stopwatch.StartNew();

            try
            {
                //BIPermission.RegisterAuthority();
                cmd = context.Request.GetValue<string>("cmd");
                if (string.IsNullOrWhiteSpace(cmd))
                    throw new Exception("Ajax请求必须指定命令“cmd”参数");

                switch (cmd)
                {
                    case "ExportSolutionQueryResult": ExcelDownLoadProcess(context); break;
                    default: CommonRequestProcess(context); break;
                }
            }
            catch (Exception ex)
            {
                //GLog.Default.Write(LogMode.Error, "GTP.BI.WebIntegration.Service.ProcessRequest执行出现异常，cmd：{0}，异常信息：{1}", cmd, ex);
                throw;
            }
            finally
            {
                watch.Stop();
                //GLog.Default.Write(LogMode.Debug, "Service cmd：{0}，耗时：{1}秒", cmd, watch.Elapsed.TotalSeconds);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void CommonRequestProcess(HttpContext context)
        {
            object result = ExecuteRequest(context);
            string jsonResult = GetJson(result);
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonResult);
        }
        private void ExcelDownLoadProcess(HttpContext context)
        {

            object result = ExecuteRequest(context);
            Byte[] btyes = result as Byte[];
            if (!context.Response.Buffer)
            {
                context.Response.Buffer = true;
                context.Response.Clear();
            }
            string fileNameTemp = string.Empty;
            string fileName = "excel.xls";
            if (context.Request.Browser.Browser == "IE")
                fileNameTemp = System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            else
                fileNameTemp = fileName;
            context.Response.AddHeader("Content-Length", btyes.Length.ToString());
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", string.Format("{0};filename={1}", "attachment", fileNameTemp));
            context.Response.Flush();
            context.Response.BinaryWrite(btyes);
            context.Response.End();
        }
        private object ExecuteRequest(HttpContext context)
        {

            object result = null;

         ServiceBase service = null;
            switch (cmd)
            {
 
                case "GetDimStructs":
                    service = new DeleteSolution();
                    break;
                 
                default:
                    throw new Exception("不可识别的Cmd参数：" + cmd);
            }
            service.Context = HttpContext.Current;
            service.Initialize();
            result = service.Execute();
            return result;
        }

        private string GetJson(object o)
        {
            //if (o is string)
            //    return o.ToString();
            //if (o == null)
            //    return null;
            //return new JavaScriptSerializer().Serialize(o);//Newtonsoft.Json.JsonConvert.SerializeObject(o);
            return SerializeHelper.JsonSerialize(o);
        }

    }
}
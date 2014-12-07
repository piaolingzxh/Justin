using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Justin.FrameWork.Web.Extensions;

namespace GLM.Mobile.Web
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class MobileService : IHttpHandler
    {
        string chartType;
        string cmd;
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            context.Response.ContentType = "text/plain";

            cmd = Request.GetValue<String>("cmd");
            chartType = Request.GetValue<String>("chartType");
            switch (cmd)
            {
                case "company":
                    GetCompany(context);//按照月份统计数据
                    break;
                case "labors":
                    Getlabors(context);//按照月份统计数据
                    break;
                default:
                    break;
            }
        }
        private void GetCompany(HttpContext context)
        {
            string returnvalue = @"
{
options:{
     title: { text: '分公司现场人数', x: 'center', y: 'top' },
     tooltip: {   trigger: 'axis'        },
     calculable: true,
     yAxis: [{ type: 'value'
            , boundaryGap: [0, 0.01]
     }],
     xAxis: [{
             type: 'category',
             data: ['1001', '1002', '1003', '1004', '1005', '1006', '1007']
            ,axisLabel:{
            show: true,
            formatter: formatCompany,
            textStyle: {
                color: 'auto'
            }}
     }],
     series: [{
         name: '2012.7.8',
         type: '" + chartType + @"',
         data: [58, 32, 15, 6, 24, 19, 36]
     }] 
 }
,labels:{1001:'第一分公司',1002:'第二分公司', 1003:'第三分公司',1004: '第四分公司', 1005:'第五分公司',1006: '第六分公司',1007: '第七分公司'}
}
";
            context.Response.Write(returnvalue);
            context.Response.End();

        }
        private void Getlabors(HttpContext context)
        {
            string returnvalue = @"
{
options:{
     title: { text: '劳务公司现场人数', x: 'center', y: 'top' },
    tooltip: {trigger: 'axis'},
    calculable: true,
    yAxis: [{ type: 'value', boundaryGap: [0, 0.01]}],
    xAxis: [{
         type: 'category',
         data: ['第一劳务公司', '第二劳务公司', '第三劳务公司', '第四劳务公司', '第五劳务公司', '第六劳务公司', '第七劳务公司']
    }],
    series: [{
        name: '2012.7.8',
        type: '" + chartType + @"',
        data: [58, 32, 15, 6, 24, 19, 36]
    }]
}
}
";
            context.Response.Write(returnvalue);
            context.Response.End();

        }
        

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
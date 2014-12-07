/************************************************************************************************
 *
 * 创建：zhangxh-a 2012-8-27
 * 功能：服务功能实现基类
 *
 * Copyright (c) 1998-2012 Glodon Corporation
 *
 *************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Justin.FrameWork.Web.Extensions;
using Justin.FrameWork.Web.Helper;
 
namespace GTP.BI.WebIntegration.AjaxServices
{
    public abstract class ServiceBase
    {
        public ServiceBase()
        {
        }

        public HttpContext Context { get; set; }
        public HttpRequest Request
        {
            get
            {
                return Context.Request;
            }
        }
        public HttpSessionState Session
        {
            get
            {
                return Context.Session;
            }
        }
         
        public T Deserialize<T>(string key)
        {
            try
            {
                string o = Request.GetValue<string>(key);
                if (o != null)
                {

                    //T t = new JavaScriptSerializer().Deserialize<T>(o);
                    T t = SerializeHelper.JsonDeserialize<T>(o);
                    return t;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.ToString();
            }
            return default(T);
        }
       
        public abstract void Initialize();

        public abstract object Execute();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Justin.FrameWork.Extensions;
namespace Justin.FrameWork.Web.Extensions
{
    public static class RequestEx
    {

        #region User information

        ///// <summary>
        ///// 从Session中获取当前登录用户信息
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="instance"></param>
        ///// <returns></returns>
        //public static T GetCurrentUser<T>(this HttpRequest instance)
        //{
        //    return (T)HttpContext.Current.Session[SessionKeys.CurrentUser];
        //}

        ///// <summary>
        ///// 验证服务器端Session，看当前用户是否已经登录
        ///// </summary>
        //internal static bool CheckLoginInServer(this HttpRequest instance)
        //{
        //    return HttpContext.Current.Session[SessionKeys.CurrentUser] != null;
        //}

        ///// <summary>
        ///// 保存当前用户信息到Session
        ///// </summary>
        ///// <param name="currentUser"></param>
        //internal static void SaveCurrentUser(this HttpRequest instance, object currentUser)
        //{
        //    HttpContext.Current.Session[SessionKeys.CurrentUser] = currentUser;
        //}

        ///// <summary>
        ///// 设置HttpContext.Current.User.Identity.Name为当前登录用户Id，说明用户已登录
        ///// </summary>
        ///// <param name="userName"></param>
        //internal static void LogUserWithFormAuthoration(this HttpRequest instance, string userId)
        //{
        //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(userId, false, Convert.ToInt32(FormsAuthentication.Timeout.TotalMinutes));

        //    string encTicket = FormsAuthentication.Encrypt(ticket);
        //    //set HttpContext.Current.User.Identity.Name
        //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //}

        ///// <summary>
        ///// 将当前登录用户的UserId写入Cookie，过期时间为10天
        ///// </summary>
        ///// <param name="userId"></param>
        //internal static void SaveUserInfoToCookie(this HttpRequest instance, string userId, string password)
        //{
        //    //设置cookie

        //    HttpCookie cookie = new HttpCookie(CookieKeys.SavedUser);
        //    string cookiePwdValue = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        //    string cookieUserValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(userId));

        //    cookie.Expires = DateTime.Now.AddDays(7);
        //    cookie.Values.Add(CookieKeys.SavedUserId, cookieUserValue);
        //    cookie.Values.Add(CookieKeys.SavedUserPwd, cookiePwdValue);

        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //}

        ///// <summary>
        ///// 从Cookie中获取登录用户的Id
        ///// </summary>
        ///// <returns></returns>
        //internal static string[] GetUserIdFromCookie(this HttpRequest instance)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieKeys.SavedUser];

        //    if (cookie == null || cookie.Values.Count < 2)
        //    {
        //        throw new Exception("当前用户没有登录");
        //    }
        //    string userId = Encoding.UTF8.GetString(Convert.FromBase64String(cookie.Values[CookieKeys.SavedUserId])).Trim();
        //    string password = cookie.Values[CookieKeys.SavedUserPwd];
        //    return new string[] { userId, password };
        //}

        #endregion

        #region FormValue

        public static T GetFormValue<T>(this HttpRequest instance, string formKey, T defaultValue)
        {
            if (instance.Form.AllKeys.Contains(formKey))
            {
                string formValue = instance.Form[formKey];
                return formValue.Value<T>(defaultValue);
            }
            else return defaultValue;
        }
        public static T GetFormValue<T>(this HttpRequest instance, string formKey)
        {
            if (instance.Form.AllKeys.Contains(formKey))
            {
                string formValue = instance.Form[formKey];
                return formValue.Value<T>();
            }
            throw new Exception("key " + formKey + " not found");
        }

        #endregion

        #region QueryStringValue

        public static T GetQueryValue<T>(this HttpRequest instance, string queryKey, T defaultValue)
        {
            if (instance.QueryString.AllKeys.Contains(queryKey))
            {
                string queryValue = instance.QueryString[queryKey];
                return queryValue.Value<T>(defaultValue);
            }
            else
                return defaultValue;
        }
        public static T GetQueryValue<T>(this HttpRequest instance, string queryKey)
        {
            if (instance.QueryString.AllKeys.Contains(queryKey))
            {
                string queryValue = instance.QueryString[queryKey];
                return queryValue.Value<T>();
            }
            throw new Exception("key " + queryKey + " not found");
        }

        #endregion

    }
}

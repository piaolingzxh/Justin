using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Justin.FrameWork.Web.Helper
{
    public class CacheHelper
    {
        private static DateTime _timeToExpire;

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetFromCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        /// <summary>
        /// 插入缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objectToCache"></param>
        /// <param name="minutesTillExpiration"></param>
        public static void InsertToCache(string key, object objectToCache, int minutesTillExpiration)
        {

            //if (System.Web.Configuration.WebConfigurationManager.AppSettings["Environment"].ToUpper() != "PROD")
            //    minutesTillExpiration = 1;

            HttpRuntime.Cache.Insert(key, objectToCache, null, DateTime.Now.AddMinutes(minutesTillExpiration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// 移除缓存数据
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveFromCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 插入缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objectToCache"></param>
        public static void InsertToCache(string key, object objectToCache)
        {
            if (HttpContext.Current.Application["CacheExpirationTime"] != null)
            {
                if ((System.DateTime.Parse(HttpContext.Current.Application["CacheExpirationTime"].ToString()) < System.DateTime.Now))
                {
                    DateTime yesterdayTimeToExpire = System.DateTime.Parse(HttpContext.Current.Application["CacheExpirationTime"].ToString());
                    _timeToExpire = yesterdayTimeToExpire.AddDays(1);
                }
                else
                {
                    _timeToExpire = System.DateTime.Parse(HttpContext.Current.Application["CacheExpirationTime"].ToString());

                }

                HttpRuntime.Cache.Insert(key, objectToCache, null, _timeToExpire, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            else
            {
                HttpContext.Current.Application["CacheExpirationTime"] = DateTime.Now.AddDays(1);

                InsertToCache(key, objectToCache);
            }
        }

        /// <summary>
        /// 插入对象到缓存里,文件被监视，一旦有任何修改，即清空缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objectToCache"></param>
        /// <param name="filePath"></param>
        public static void InsertToCache(string key, object objectToCache, string filePath)
        {
            CacheDependency dep = new CacheDependency(filePath);

            HttpRuntime.Cache.Insert(key, objectToCache, dep);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Justin.FrameWork.Extensions;

namespace Justin.FrameWork.Settings
{
    public class JSetting
    {
        private static Dictionary<string, object> Settings = new Dictionary<string, object>();

        public static void Set(string key, object value)
        {
            if (!Settings.ContainsKey(key.ToLower()))
            {
                Settings.Add(key.ToLower(), value);
            }
            else
            {
                Settings[key.ToLower()] = value;
            }
        }

        public static void SetUseAppSetting(string key, string appSettingKey)
        {
            Set(key, ConfigurationManager.AppSettings[appSettingKey]);
        }


        public static T Get<T>(string key)
        {
            T t = default(T);
            if (Settings.ContainsKey(key.ToLower()))
            {
                try
                {
                    t = Settings[key.ToLower()].Value<T>();
                }
                catch { }
            }
            return t;
        }
        public static string Get(string key)
        {
            return Get<String>(key);
        }

        public static string ReadAppSetting(string appSettingKey)
        {
            SetUseAppSetting(appSettingKey, appSettingKey);
            return Get(appSettingKey);
        }
        public static T ReadAppSetting<T>(string appSettingKey)
        {
            SetUseAppSetting(appSettingKey, appSettingKey);
            return Get<T>(appSettingKey);
        }
    }
}

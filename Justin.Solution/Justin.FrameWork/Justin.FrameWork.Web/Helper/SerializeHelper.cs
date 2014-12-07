using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Justin.FrameWork.Web.Helper
{
    public class SerializeHelper
    {
        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    return default(T);
                }
                T t = JSSerializer.Deserialize<T>(jsonString);
                return t;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.ToString();
                return default(T);
            }

        }
        public static string JsonSerialize(object o)
        {
            if (o == null)
                return null;
            return JSSerializer.Serialize(o);
        }

        private static JavaScriptSerializer _javaScriptSerializer;
        public static JavaScriptSerializer JSSerializer
        {
            get
            {
                if (_javaScriptSerializer == null)
                {
                    _javaScriptSerializer = new JavaScriptSerializer();
                    _javaScriptSerializer.MaxJsonLength = 524288000;

                }
                return _javaScriptSerializer;
            }
        }
    }
}

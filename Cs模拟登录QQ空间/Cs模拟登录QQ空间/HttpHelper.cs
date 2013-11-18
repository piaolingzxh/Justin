using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;


class HttpHelper
{
    public static string GetResponse(string url, CookieContainer cookie)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "Get";
        request.CookieContainer = cookie;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        string resString = sr.ReadToEnd();
        return resString;       
    }
    public static object GetResponse(string objectType,string url, CookieContainer cookie)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "Get";
        request.CookieContainer = cookie;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        if (objectType == "Bitmap") return new System.Drawing.Bitmap(response.GetResponseStream());
        return null;
    }
}


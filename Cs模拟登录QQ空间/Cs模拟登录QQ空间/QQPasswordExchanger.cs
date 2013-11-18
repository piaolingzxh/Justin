using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections;

class QQPasswordExchanger
{
    public static string binl2hex(byte[] buffer)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < buffer.Length; i++)
        {
            builder.Append(buffer[i].ToString("x2"));
        }
        return builder.ToString();
    }
    public static string md5_3(string input)
    {
        MD5 md = MD5.Create();
        byte[] buffer = md.ComputeHash(Encoding.Default.GetBytes(input));
        buffer = md.ComputeHash(buffer);
        buffer = md.ComputeHash(buffer);
        return binl2hex(buffer);
    }

    public static string md5(string input)
    {
        byte[] buffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
        return binl2hex(buffer);
    }

    public static string getPassword(string password, string verifycode)
    {
        return md5(md5_3(password).ToUpper() + verifycode.ToUpper()).ToUpper();
    }


}


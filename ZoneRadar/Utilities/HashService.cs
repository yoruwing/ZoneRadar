﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ZoneRadar.Utilities
{
    public static class HashService
    {
        //加密是不會提供還原密碼API的
        public static string MD5Hash(this string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;
            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]陣列(因為是對每個字元做編碼)
                byte[] byteArrar = Encoding.UTF8.GetBytes(rawString);

                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArrar);

                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    //2, 8, 10, 16進位
                    //hexadecimal - 十六進位(x2參數可變十六進位)
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString();
        }
        public static string MD5HashBase64(this string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);
                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArray);
                result = Convert.ToBase64String(encryption);
            }

            return result;
        }

        //SHA1演算法雜湊大小是160 位元
        public static string SHA1Hash(this string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA1 sha1 = SHA1.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);
                byte[] encryption = sha1.ComputeHash(byteArray);
                sb = new StringBuilder();
                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }



        //SHA256演算法雜湊大小是256位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha256?view=netframework-4.8

        //SHA384演算法雜湊大小是256位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha384?view=netframework-4.8

        //SHA512演算法雜湊大小為512位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha512?view=netframework-4.8
        public static string SHA512Hash(this string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;
            using (SHA512 sha512 = SHA512.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);
                byte[] encryption = sha512.ComputeHash(byteArray);
                sb = new StringBuilder();
                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
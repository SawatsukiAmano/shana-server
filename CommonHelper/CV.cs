using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class CV
    {

        /// <summary>
        /// 大驼峰转蛇形
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PascalUp2Snake(string str)
        {
            var result = "";
            int index = 0;
            for (int i = 0; i < str.Length; i++)
            {
                bool isUpper = char.IsUpper(str[i]);
                bool latestIsUpper = false;
                if (i > 0 && char.IsUpper(str[i - 1]))
                {
                    latestIsUpper = true;
                    if (i < str.Length - 1 && char.IsLower(str[i + 1])) latestIsUpper = false;
                }

                if (index < i && isUpper && !latestIsUpper) result += '_';

                result += str[i].ToString().ToLower();
                if (isUpper) index = i;
            }
            return result;
        }

        /// <summary>
        /// 生成md5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ComputeMD5(string s)
        {
            StringBuilder sb = new StringBuilder();

            // Initialize a MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }
            return sb.ToString();
        }
    }
}

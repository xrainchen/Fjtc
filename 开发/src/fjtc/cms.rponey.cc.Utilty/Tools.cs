using System;
using System.Web;

namespace cms.rponey.cc.Utilty
{
    public class Tools
    {
        /// <summary>
        /// 取客户端Ip
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            if (HttpContext.Current == null) return "";

            var request = HttpContext.Current.Request;

            try
            {
                string returnValue = "";
                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    returnValue = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        returnValue = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = HttpContext.Current.Request.UserHostAddress;
                }
                return returnValue;
            }
            catch
            {
                try
                {
                    return request.UserHostAddress;
                }
                catch { return "0.0.0.0"; }
            }
        }

        /// <summary>
        /// 根据指定的字符串产生指定长度的随机字符串
        /// </summary>
        /// <param name="sourceString">指定的字符串</param>
        /// <param name="length">产生的随机字符串的长度</param>
        /// <param name="seed">随机数种子，并发使用时必须传入不同的种子以避免生成重复的随机字符串</param>
        /// <returns></returns>
        public static string GetRandom(string sourceString, int length, int seed = 0)
        {
            var output = string.Empty;
            var arr = sourceString.ToCharArray();
            var ran = seed == 0 ? new Random() : new Random(seed);
            while (output.Length < length)
            {
                output += arr[ran.Next(0, arr.Length)];
            }

            return output;
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}

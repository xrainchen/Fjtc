using System;

namespace fjtc.com.Common
{
    public static class OrderHelper
    {
        private static long identity = 0;
        /// <summary>
        /// 获取商户订单号
        /// </summary>
        /// <param name="machId"></param>
        /// <returns></returns>
        public static string GetMachBillno(string machId)
        {
            return machId + DateTime.Now.ToString("yyyyMMdd")+ Identity();
        }

        public static string Identity()
        {
            identity++;
            if (identity > 999999999)
            {
                identity = 0;
            }
            var result = identity.ToString().PadLeft(10, '0');
            return result.Substring(result.Length - 10, 10);
        }
    }
}
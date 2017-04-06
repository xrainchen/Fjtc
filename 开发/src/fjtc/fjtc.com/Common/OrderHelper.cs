using System;
using cms.rponey.cc.Utilty;

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
            return machId + DateTime.Now.ToString("yyyyMMdd") + Identity();
        }

        public static string Identity()
        {
            identity++;
            var result = identity.ToString().PadLeft(5, '0');
            return Tools.GetRandom("0123456789", 2, System.Threading.Thread.GetDomainID())+ DateTime.Now.Millisecond.ToString().PadLeft(3,'0') + result.Substring(result.Length - 5, 5);
        }
    }
}
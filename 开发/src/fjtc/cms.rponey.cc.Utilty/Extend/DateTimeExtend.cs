using System;

namespace cms.rponey.cc.Utilty.Extend
{
    public static class DateTimeExtend
    {
        /// <summary>
        /// 获取给定时间的本地时间戳（1970秒级）
        /// </summary>
        /// <param name="dateTime">要转换的时间</param>
        /// <returns></returns>
        public static long ToLocalTimeStamp(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToLocalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        /// <summary>
        /// 从时间戳获取时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromTimeStamp(long timeStamp)
        {
            return new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(timeStamp));
        }
    }
}

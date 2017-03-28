using System.Collections.Generic;

namespace fjtc.com.Areas.Admin.Models
{
    /// <summary>
    /// 红包模型
    /// </summary>
    public class RedPackModel
    {
        public IList<long> MpUserIds { get; set; }

        public decimal RedPackTotalAmount { get; set; }

        /// <summary>
        /// 发送红包方式  1 平均，2 随机
        /// </summary>
        public int SendRedPackType { get; set; }

        public string Remark { get; set; }

    }

    public class RedPackItemModel
    {
        public long MpUserId { get; set; }
        public int RedPackAmount { get; set; }
    }
}
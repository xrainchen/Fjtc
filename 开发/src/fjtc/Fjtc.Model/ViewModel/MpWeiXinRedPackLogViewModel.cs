using System;

namespace Fjtc.Model.ViewModel
{
    public class MpWeiXinRedPackLogViewModel
    {
        public long Id { get; set; }

        public long ProductUserId { get; set; }

        public string ProductUserName { get; set; }
        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal RedPackAmount { get; set; }
        public string OpenId { get; set; }
        public string MpUserNick { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        public string Remark { get; set; }
    }
}

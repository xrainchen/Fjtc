using System;

namespace Fjtc.Model.Entity
{

    [Serializable]
    public class MpWeiXinRedPackLog : IIdentityEntity
    {
        public long Id { get; set; }

        public long ProductUserId { get; set; }
        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal RedPackAmount { get; set; }
        public string OpenId { get; set; }
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

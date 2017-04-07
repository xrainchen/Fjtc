using cms.rponey.cc.Utilty.Extend;
using System.Collections.Generic;
using Senparc.Weixin.MP;

namespace fjtc.com.Areas.Admin.Models
{
    /// <summary>
    /// 红包模型
    /// </summary>
    public class RedPackModel
    {
        public string MpUserIds { get; set; }

        public decimal? RedPackTotalAmount { get; set; }

        /// <summary>
        /// 发送红包方式  1 平均，2 随机
        /// </summary>
        public int SendRedPackType { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 使用场景
        /// </summary>
        public RedPack_Scene? SceneIdType { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        public string Wishing { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActionName { get; set; }
    }

    public class RedPackItemModel
    {
        public long MpUserId { get; set; }
        public int RedPackAmount { get; set; }
    }

    /// <summary>
    /// 使用场景
    /// </summary>
    public enum SceneIdTypeEnum
    {
        /// <summary>
        /// 商品促销
        /// </summary>
        [Remark("商品促销")]
        PRODUCT_1,
        /// <summary>
        /// 抽奖
        /// </summary>
        [Remark("抽奖")]
        PRODUCT_2,
        /// <summary>
        /// 虚拟物品兑换
        /// </summary>
        [Remark("虚拟物品兑换")]
        PRODUCT_3,
        /// <summary>
        /// 企业内部福利
        /// </summary>
        [Remark("企业内部福利")]
        PRODUCT_4,
        /// <summary>
        /// 渠道分润
        /// </summary>
        [Remark("渠道分润")]
        PRODUCT_5,
        /// <summary>
        /// 保险回馈
        /// </summary>
        [Remark("保险回馈")]
        PRODUCT_6,
        /// <summary>
        /// 彩票派奖
        /// </summary>
        [Remark("彩票派奖")]
        PRODUCT_7,
        /// <summary>
        /// 税务刮奖
        /// </summary>
        [Remark("税务刮奖")]
        PRODUCT_8,
    }
}
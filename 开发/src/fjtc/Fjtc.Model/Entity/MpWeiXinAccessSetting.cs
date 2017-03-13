using System;

namespace Fjtc.Model.Entity
{
    /// <summary>
    /// 微信接入配置
    /// </summary>
    [Serializable]
    public class MpWeiXinAccessSetting
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// AppSecret 
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
    }
}

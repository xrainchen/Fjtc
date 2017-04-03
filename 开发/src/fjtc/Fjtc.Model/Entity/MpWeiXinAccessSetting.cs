using System;
using System.Text;
using Fjtc.Common.Encrypt;
using RPoney;

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
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public string MachId { get; set; }
        /// <summary>
        /// API密钥
        /// </summary>
        public string ApiKey { get; set; }


        public MpWeiXinAccessSetting Decrypt()
        {
            //this.AppId = string.IsNullOrWhiteSpace(this.AppId) ? "" : Encoding.UTF8.GetString(this.AppId.DesDecrypt().GetBytes());
            //this.AppSecret = string.IsNullOrWhiteSpace(this.AppSecret) ? "" : Encoding.UTF8.GetString(this.AppSecret.DesDecrypt().GetBytes());
            //this.Token = string.IsNullOrWhiteSpace(this.Token) ? "" : Encoding.UTF8.GetString(this.Token.DesDecrypt().GetBytes());
            //this.MachId = string.IsNullOrWhiteSpace(this.MachId) ? "" : Encoding.UTF8.GetString(this.MachId.DesDecrypt().GetBytes());
            //this.ApiKey = string.IsNullOrWhiteSpace(this.ApiKey) ? "" : Encoding.UTF8.GetString(this.ApiKey.DesDecrypt().GetBytes());
            return this;
        }

        public MpWeiXinAccessSetting Encrypt()
        {
            //this.AppId = string.IsNullOrWhiteSpace(this.AppId) ? "" : Encoding.UTF8.GetBytes(this.AppId).GetHexString().DesEncrypt();
            //this.AppSecret = string.IsNullOrWhiteSpace(this.AppSecret) ? "" : Encoding.UTF8.GetBytes(this.AppSecret).GetHexString().DesEncrypt();
            //this.Token = string.IsNullOrWhiteSpace(this.Token) ? "" : Encoding.UTF8.GetBytes(this.Token).GetHexString().DesEncrypt();
            //this.MachId = string.IsNullOrWhiteSpace(this.MachId) ? "" : Encoding.UTF8.GetBytes(this.MachId).GetHexString().DesEncrypt();
            //this.ApiKey = string.IsNullOrWhiteSpace(this.ApiKey) ? "" : Encoding.UTF8.GetBytes(this.ApiKey).GetHexString().DesEncrypt();
            return this;
        }
    }
}

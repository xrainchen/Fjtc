namespace admin.fjtc.com.Areas.MpWeiXin.Models
{
    /// <summary>
    /// 微信公众平台授权模型
    /// </summary>
    public class MpWeiXinAccessModel
    {

        public string Signature { get; set; }

        public string Timestamp { get; set; }

        public string Nonce { get; set; }

        public string Echostr { get; set; }
    }
}
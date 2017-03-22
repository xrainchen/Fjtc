namespace Fjtc.Model
{
    public class ReturnJsonResultModel
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int ReturnCode { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 回调类型
        /// </summary>
        public string CallBackType { get; set; }
        /// <summary>
        /// 重定向地址
        /// </summary>
        public string RedirectUrl { get; set; }
        /// <summary>
        /// 返回数据 
        /// </summary>
        public dynamic ReturnData { get; set; }
    }
}

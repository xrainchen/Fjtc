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
        /// 返回数据 
        /// </summary>
        public dynamic ReturnData { get; set; }
    }
}

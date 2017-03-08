namespace admin.fjtc.com.Auth
{
    /// <summary>
    /// 票据存储服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITicketStorage<T>
    {
        /// <summary>
        /// 取消
        /// </summary>
        void Cancellation();
        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns></returns>
        T GetTicket();
        /// <summary>
        /// 设置票据
        /// </summary>
        /// <param name="tick"></param>
        void SetTicket(T tick);
    }
}

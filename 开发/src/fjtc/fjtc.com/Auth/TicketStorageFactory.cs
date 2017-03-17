namespace fjtc.com.Auth
{
    /// <summary>
    /// 票据存储工厂
    /// </summary>
    public class TicketStorageFactory
    {
        public static ITicketStorage<T> InstanceTicketStorage<T>()
        {
            return (ITicketStorage<T>)new CookieTickStorage();
        }
    }
}
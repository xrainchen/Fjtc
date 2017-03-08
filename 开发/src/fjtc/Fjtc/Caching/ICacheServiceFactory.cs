namespace Fjtc.Caching
{
    public interface ICacheServiceFactory
    {
        ICacheService CreateCacheService(CacheTypeEnum cacheType);
    }
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheTypeEnum
    {
        /// <summary>
        /// 本地缓存
        /// </summary>
        Local = 1
    }
}

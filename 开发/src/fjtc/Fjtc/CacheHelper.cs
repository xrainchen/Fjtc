using Fjtc.Caching;
using Fjtc.Caching.Imp;

namespace Fjtc
{
    public class CacheHelper
    {
        private static readonly ICacheServiceFactory _cacheServiceFactory;
        static CacheHelper()
        {
            _cacheServiceFactory = new CacheServiceFactory();
        }
        public static ICacheService GetCache(CacheTypeEnum cacheType)
        {
            return _cacheServiceFactory.CreateCacheService(cacheType);
        }
    }
}

using System;

namespace Fjtc.Caching.Imp
{
    public class CacheServiceFactory : ICacheServiceFactory
    {
        public ICacheService CreateCacheService(CacheTypeEnum cacheType)
        {
            switch (cacheType)
            {
                case CacheTypeEnum.Local:
                    return new LocalCacheService();
            }
            throw new NotImplementedException();
        }
    }
}

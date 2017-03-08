using System;
using System.Linq;
using System.Runtime.Caching;

namespace Fjtc.Caching.Imp
{
    /// <summary>
    /// 本地缓存
    /// </summary>
    public class LocalCacheService : ICacheService
    {
        protected ObjectCache LocalCache => MemoryCache.Default;
        public void Add<T>(T obj, string key)
        {
            if (obj == null)
                return;
            var policy = new CacheItemPolicy { Priority = CacheItemPriority.NotRemovable };
            //永久缓存
            LocalCache.Add(new CacheItem(key, obj), policy);
        }

        public void Add<T>(T obj, string key, TimeSpan timeSpan)
        {
            if (obj == null)
                return;
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + timeSpan };
            LocalCache.Add(new CacheItem(key, obj), policy);
        }

        public void Add<T>(T obj, string key, int expireSeconds)
        {
            Add<T>(obj, key, TimeSpan.FromSeconds(expireSeconds));
        }

        public T GetCacheOjbect<T>(string key)
        {
            if (null == LocalCache[key]) return default(T);
            return (T)LocalCache[key];
        }

        public void RemoveCache(params string[] key)
        {
            if (!key.Any()) return;
            foreach (var k in key)
            {
                LocalCache.Remove(k);
            }
        }
    }
}

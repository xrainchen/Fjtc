using System;

namespace Fjtc.Caching
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        void Add<T>(T obj, string key);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="expireSeconds"></param>
        void Add<T>(T obj, string key, int expireSeconds);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        void Add<T>(T obj, string key, TimeSpan timeSpan);
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCacheOjbect<T>(string key);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void RemoveCache(params string[] key);
    }
}

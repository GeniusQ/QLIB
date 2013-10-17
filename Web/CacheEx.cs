using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;
using System.Web.UI;

namespace QLIB.Web
{
    public static class CacheEx
    {
        /// <summary>
        /// 创建定时过期缓存
        /// </summary>
        /// <typeparam name="T">缓存的数据类型</typeparam>
        /// <param name="cache">httpcontext的缓存对象</param>
        /// <param name="key">缓存名称</param>
        /// <param name="getDataFunc">缓存不存在时，获取缓存所调用的方法</param>
        /// <param name="expireTime">缓存过期时间</param>
        /// <returns>缓存的数据</returns>
        public static T Create<T>(this Cache cache, string key, Func<T> getDataFunc, DateTime expireTime)
        {
            T data = default(T);
            if (cache[key] == null)
            {
                cache.Insert(key, getDataFunc(), null, expireTime, TimeSpan.Zero);                
            }

            data = (T)(cache[key] ?? default(T));
            return data;
        }

        /// <summary>
        /// 创建滑动过期缓存
        /// </summary>
        /// <typeparam name="T">缓存的数据类型</typeparam>
        /// <param name="cache">httpcontext的缓存对象</param>
        /// <param name="key">缓存名称</param>
        /// <param name="getDataFunc">缓存不存在时，获取缓存所调用的方法</param>
        /// <param name="expireMinute">设置过期分钟</param>
        /// <returns>缓存的数据</returns>
        public static T Create<T>(this Cache cache, string key, Func<T> getDataFunc, double expireMinute)
        {
            T data = default(T);
            if (cache[key] == null)
            {
                cache.Insert(key, getDataFunc(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(expireMinute));
            }
            data = (T)cache[key];

            return data;
        }

    }
}

﻿using System;
using System.Threading.Tasks;

namespace LazySetup.RedisCache
{
    public interface IRedisCache
    {
        Task<T> GetAsync<T>(string key);
        Task<T> GetSetAsync<T>(string key, Func<T> func, TimeSpan? expiry = null);
        Task<bool> SetAsync(string key, object data, TimeSpan? expiry = null);
        Task<bool> SetAsync<T>(string key, Func<T> func, TimeSpan? expiry = null);
    }
}
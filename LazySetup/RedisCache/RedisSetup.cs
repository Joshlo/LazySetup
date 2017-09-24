using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace LazySetup.RedisCache
{
    public static class RedisSetup
    {
        public static void AddRedis(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IRedisCache>(provider => new RedisCache(connectionString));
        }
    }
}

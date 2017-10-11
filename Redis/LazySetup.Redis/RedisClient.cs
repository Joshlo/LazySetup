using System;
using System.Threading.Tasks;
using LazySetup.HelpersAndExtensions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LazySetup.Redis
{
    public class RedisClient : IRedisClient
    {
        public static string ConnectionString { get; internal set; }
        public RedisClient(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = ConnectionMultiplexer.Connect(ConnectionString);
        }

        private readonly IConnectionMultiplexer Connection;

        public async Task<T> GetAsync<T>(string key)
        {
            var db = Connection.GetDatabase();

            var data = await db.StringGetAsync(key);

            return data.HasValue ? JsonConvert.DeserializeObject<T>(data) : default(T);
        }

        public async Task<T> GetSetAsync<T>(string key, Func<T> func, TimeSpan? expiry = null)
        {
            var db = Connection.GetDatabase();

            var data = await db.StringGetAsync(key);

            if (data.HasValue)
                return JsonConvert.DeserializeObject<T>(data);

            var result = func();
            await db.StringSetAsync(key, result.ToJson(), expiry);
            return result;
        }

        public Task<bool> SetAsync(string key, object data, TimeSpan? expiry = null)
        {
            var db = Connection.GetDatabase();
            return db.StringSetAsync(key, data.ToJson(), expiry);
        }

        public Task<bool> SetAsync<T>(string key, Func<T> func, TimeSpan? expiry = null)
        {
            var db = Connection.GetDatabase();
            var data = func();
            return db.StringSetAsync(key, data.ToJson(), expiry);
        }
    }
}

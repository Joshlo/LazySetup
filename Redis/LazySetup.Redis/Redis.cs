using System;
using System.Threading.Tasks;
using LazySetup.HelpersAndExtensions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LazySetup.Redis
{
    public class Redis : IRedis
    {
        public static string ConnectionString { get; internal set; }
        public static IConnectionMultiplexer Connection { get; private set; }
        public Redis(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = LazyConnection.Value;
        }

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConnectionString));

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

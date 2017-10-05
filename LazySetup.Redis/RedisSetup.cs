using Microsoft.Extensions.DependencyInjection;

namespace LazySetup.Redis
{
    public static class RedisSetup
    {
        public static void AddRedis(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IRedis>(provider => new Redis(connectionString));
        }
    }
}

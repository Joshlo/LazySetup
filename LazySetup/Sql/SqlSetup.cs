using Microsoft.Extensions.DependencyInjection;

namespace LazySetup.Sql
{
    public static class SqlSetup
    {
        public static void AddSql(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<ISqlProvider>(provider => new SqlProvider(connectionString));
        }
    }
}

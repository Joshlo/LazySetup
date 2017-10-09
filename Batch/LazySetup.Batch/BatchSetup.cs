using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LazySetup.Batch
{
    public static class BatchSetup
    {
        public static IApplicationBuilder UseBatchRequest(this IApplicationBuilder app, string path = "/batch")
        {
            var factory = app.ApplicationServices.GetRequiredService<IHttpContextFactory>();
            app.UseMiddleware<BatchMiddleware>(factory, path);
            return app;
        }
    }
}

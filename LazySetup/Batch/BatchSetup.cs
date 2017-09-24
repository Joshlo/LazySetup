using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LazySetup.Batch
{
    public static class BatchSetup
    {
        public static IServiceCollection AddBatchRequest(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseBatchRequest(this IApplicationBuilder app)
        {
            var factory = app.ApplicationServices.GetRequiredService<IHttpContextFactory>();
            app.UseMiddleware<BatchRequestProvider>(factory, new BatchRequestOptions {Host = new Uri("http://localhost:57575") });
            return app;
        }
    }
}

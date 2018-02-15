using System;
using Microsoft.AspNetCore.Builder;

namespace LazySetup.RequireHttps
{
    public static class RequireHttpsSetup
    {
        public static IApplicationBuilder UseRequireHttps(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequireHttpsMiddleware>();
            return app;
        }
    }
}

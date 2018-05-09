using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace LazySetup.Ip
{
    public static class IpSetup
    {
        public static IApplicationBuilder UseIpBlocking(this IApplicationBuilder app, IEnumerable<string> ips)
        {
            app.UseForwardedHeaders();
            app.UseMiddleware<IpBlockingMiddleware>(ips);
            return app;
        }

        public static IApplicationBuilder UseIpRestriction(this IApplicationBuilder app, IEnumerable<string> ips)
        {
            app.UseForwardedHeaders();
            app.UseMiddleware<IpRestrictionMiddleware>(ips);
            return app;
        }
    }
}

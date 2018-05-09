using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace LazySetup.Ip
{
    public static class IpSetup
    {
        public static IApplicationBuilder UseIpBlocking(this IApplicationBuilder app, IEnumerable<string> ips)
        {
            app.UseMiddleware<IpBlockingMiddleware>(ips);
            return app;
        }

        public static IApplicationBuilder UseIpRestriction(this IApplicationBuilder app, IEnumerable<string> ips)
        {
            app.UseMiddleware<IpRestrictionMiddleware>(ips);
            return app;
        }
    }
}

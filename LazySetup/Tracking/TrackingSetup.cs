using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace LazySetup.Tracking
{
    public static class TrackingSetup
    {
        public static IApplicationBuilder UseTracking(this IApplicationBuilder app, TrackingOptions options)
        {
            app.UseMiddleware<TrackingMiddleware>(options);
            return app;
        }
    }
}

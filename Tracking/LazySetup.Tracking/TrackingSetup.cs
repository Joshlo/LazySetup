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

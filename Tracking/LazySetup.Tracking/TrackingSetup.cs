using Microsoft.AspNetCore.Builder;

namespace LazySetup.Tracking
{
    public static class TrackingSetup
    {
        public static IApplicationBuilder UseTracking(this IApplicationBuilder app, TrackingTypes trackingType)
        {
            app.UseMiddleware<TrackingMiddleware>(trackingType);
            return app;
        }
    }
}

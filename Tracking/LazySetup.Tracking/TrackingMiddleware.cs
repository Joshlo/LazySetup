using System.Diagnostics;
using System.Threading.Tasks;
using LazySetup.HelpersAndExtensions;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Tracking
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITrackingHandler _trackingHandler;
        private readonly TrackingTypes _type;

        public TrackingMiddleware(RequestDelegate next, ITrackingHandler trackingHandler, TrackingTypes type)
        {
            _next = next;
            _trackingHandler = trackingHandler;
            _type = type;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = new Stopwatch();
            timer.Start();
            await _next(context);
            timer.Stop();

            var model = new TrackingModel
            {
                Request = context.Request,
                ExecutionTime = timer.Elapsed
            };

            if (_type == TrackingTypes.All)
                model.Response = context.Response;

            if (_type == TrackingTypes.Success && context.Response.IsSuccessStatusCode())
                model.Response = context.Response;

            if (_type == TrackingTypes.Errors && !context.Response.IsSuccessStatusCode())
                model.Response = context.Response;

            await _trackingHandler.SaveTracking(model);
        }
    }
}

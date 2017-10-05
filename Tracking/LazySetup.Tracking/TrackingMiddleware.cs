using System.Diagnostics;
using System.Threading.Tasks;
using LazySetup.HelpersAndExtensions;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Tracking
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TrackingOptions _options;

        public TrackingMiddleware(RequestDelegate next, TrackingOptions options)
        {
            _next = next;
            _options = options;
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

            if (_options.TrackingType == TrackingTypes.All)
                model.Response = context.Response;

            if (_options.TrackingType == TrackingTypes.Success && context.Response.IsSuccessStatusCode())
                model.Response = context.Response;

            if (_options.TrackingType == TrackingTypes.Errors && !context.Response.IsSuccessStatusCode())
                model.Response = context.Response;

            _options.Execute(model);
        }
    }
}

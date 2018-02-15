using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LazySetup.RequireHttps
{
    public class RequireHttpsMiddleware
    {
        private readonly RequestDelegate _next;

        public RequireHttpsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("HTTPS required.");
                return;
            }

            await _next(context);
        }
    }
}
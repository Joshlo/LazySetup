using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Ip
{
    public class IpBlockingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEnumerable<string> _ips;

        public IpBlockingMiddleware(RequestDelegate next, IEnumerable<string> ips)
        {
            _next = next;
            _ips = ips;
        }

        public Task Invoke(HttpContext context)
        {
            if(_ips.All(x => x != context.Connection.RemoteIpAddress.ToString()))
                return _next(context);

            return Task.CompletedTask;
        }
    }
}
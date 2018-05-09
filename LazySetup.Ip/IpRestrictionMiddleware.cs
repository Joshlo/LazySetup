using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Ip
{
    public class IpRestrictionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEnumerable<string> _ips;

        public IpRestrictionMiddleware(RequestDelegate next, IEnumerable<string> ips)
        {
            _next = next;
            _ips = ips;
        }

        public Task Invoke(HttpContext context)
        {
            if (_ips.Any(x => x == context.Connection.RemoteIpAddress.ToString()))
                return _next(context);
            return Task.CompletedTask;
        }
    }
}
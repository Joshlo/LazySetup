using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionOptions _options;

        public ExceptionMiddleware(RequestDelegate next, ExceptionOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if(context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                return;

            if (_options.LogWhen >= (ExceptionType)context.Response.StatusCode)
            {
                
            } 
        }
    }
}

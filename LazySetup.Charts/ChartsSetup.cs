using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace LazySetup.Charts
{
    public static class ChartsSetup
    {
        public static void AddCharts(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.FileProviders.Add(new EmbeddedFileProvider(typeof(ChartsSetup).Assembly));
            });
        }
    }
}

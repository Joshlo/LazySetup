using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace LazySetup.Token
{
    public static class TokenSetup
    {
        public static IApplicationBuilder UseTokenAuthorize(this IApplicationBuilder app, TokenProviderOptions options)
        {
            app.UseMiddleware<TokenProvider>(options);
            app.UseAuthentication();
            return app;
        }

        public static IServiceCollection AddTokenAuthorize(this IServiceCollection services, TokenValidationParameters parameters)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = parameters;
                });

            return services;
        }
    }
}

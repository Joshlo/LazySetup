using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public static IApplicationBuilder UseTokenAuthorize(this IApplicationBuilder app, TokenOptions options)
        {
            var handler = app.ApplicationServices.GetRequiredService<ITokenHandler>();
            app.UseAuthentication();
            app.UseMiddleware<TokenMiddleware>(handler, options);
            return app;
        }

        public static IServiceCollection AddTokenAuthorize(this IServiceCollection services, TokenValidationParameters parameters, Action<AuthorizationOptions> authOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = parameters;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/authorize/login";
            });

            services.AddAuthorization(authOptions);

            return services;
        }
    }
}

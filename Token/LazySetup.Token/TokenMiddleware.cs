﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LazySetup.Token
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenOptions _options;
        private readonly ITokenValidation _tokenValidator;

        public TokenMiddleware(RequestDelegate next, ITokenValidation tokenValidator, TokenOptions options)
        {
            _tokenValidator = tokenValidator;
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                await _next(context);
                return;
            }
            if (context.Request.Method.Equals("POST"))
            {
                context.Request.EnableRewind();

                var bodyStream = new StreamReader(context.Request.Body);
                var json = await bodyStream.ReadToEndAsync();

                var model = JsonConvert.DeserializeObject<TokenRequest>(json);
                
                TokenClaims identity;

                if (model.Grant_type.ToLower() == "refresh")
                {
                    identity = await _tokenValidator.ValidateAsync(model.Refresh_token);

                    if (identity == null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync($"Invalid refresh_token: {model.Refresh_token}");
                        return;
                    }
                }
                else
                {
                    identity = await _tokenValidator.ValidateAsync(model.Identifier, model.Password);

                    if (identity == null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid identifier or password");
                        return;
                    }

                    model.Refresh_token = Guid.NewGuid().ToString().Replace("-", "");
                    await _tokenValidator.StoreRefreshTokenAsync(model.Identifier, model.Refresh_token);
                }
                
                var now = DateTime.UtcNow;
                var claims = new List<Claim>();

                var type = identity.GetType();

                foreach (var prop in type.GetProperties())
                {
                    if (prop.GetValue(identity) != null)
                        claims.Add(new Claim(prop.Name, prop.GetValue(identity).ToString(), prop.PropertyType.ToString()));
                }

                var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(_options.Expiration),
                    signingCredentials: _options.SigningCredentials
                    );

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)_options.Expiration.TotalSeconds,
                    refresh_token = model.Refresh_token
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response,
                    new JsonSerializerSettings { Formatting = Formatting.Indented }));
                return;
            }

            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Bad request.");
        }
    }
}

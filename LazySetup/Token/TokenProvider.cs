using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LazySetup.Token
{
    public class TokenProvider
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;

        public TokenProvider(RequestDelegate next, TokenProviderOptions options)
        {
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
                var username = "";
                var password = "";
                if (context.Request.HasFormContentType)
                {
                    username = context.Request.Form["identifier"];
                    password = context.Request.Form["password"];
                }
                else
                {
                    context.Request.EnableRewind();

                    var bodyStream = new StreamReader(context.Request.Body);
                    var json = await bodyStream.ReadToEndAsync();

                    var obj = JsonConvert.DeserializeObject<dynamic>(json);

                    password = obj.password;
                    username = obj.identifier;
                }

                TokenClaims identity;

                if (_options.ValidateAsync != null)
                    identity = await _options.ValidateAsync(username, password);
                else
                    identity = _options.Validate(username, password);

                if (identity == null)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"Invalid {_options.Identifier} or password");
                    return;
                }

                var now = DateTime.UtcNow;
                var claims = new List<Claim>();

                var type = identity.GetType();

                foreach (var prop in type.GetProperties())
                {
                    if(prop.GetValue(identity) != null)
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
                    expires_in = (int)_options.Expiration.TotalSeconds
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

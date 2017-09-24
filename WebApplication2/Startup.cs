using System;
using System.Text;
using LazySetup.Batch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LazySetup.Token;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTokenAuthorize(new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "LazySetup",

                ValidateAudience = true,
                ValidAudience = "http://localhost",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NotSoSecure12345")),

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseTokenAuthorize(new TokenProviderOptions("email")
            {
                Validate = (email, password) =>
                {
                    var result = new TokenClaims();

                    if (email == password)
                    {
                        result.Sub = email;
                    }

                    return result;
                }
            });

            app.UseBatchRequest();

            app.UseMvc();
        }
    }
}

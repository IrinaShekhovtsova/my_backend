using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Interfaces;
using Shekhovtsova_backend.Services;

namespace Shekhovtsova_backend
{
    public class Startup
    {
        public ICountry CountryService { get; private set; }
        public IEnergyCard EnergyCardService { get; private set; }

        public AuthContext context { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CountryService = new CountryService(context);
            EnergyCardService = new EnergyCardService(context) ;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = AuthOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.SigningKey,

                    ValidateLifetime = true,
                };
            }
            );
            services.AddControllers();
            services.AddDbContext<AuthContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("AuthContext")));

            services.AddScoped(typeof(ICountry), typeof(CountryService));
            services.AddScoped(typeof(IEnergyCard), typeof(EnergyCardService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

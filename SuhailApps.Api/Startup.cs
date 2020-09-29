using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

using SuhailApps.Core.Classes;
using SuhailApps.Core.Data;
using SuhailApps.Core.Interfaces;
using SuhailApps.Core.Mapping;
using SuhailApps.Core.Services;

namespace SuhailApps.Api
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
            //services
            //    .AddMvc()
            //    .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver
            //        = new DefaultContractResolver());

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/fir-demo-d30e0";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/fir-demo-d30e0",
                        ValidateAudience = true,
                        ValidAudience = "fir-demo-d30e0",
                        ValidateLifetime = true
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<SMSoptions>(Configuration);
            services.AddAutoMapper(typeof(MapperProfile));
            AddDbContext(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {

                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("SuhailApps.Core"));
                }

            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.OpenApi.Models;

namespace ServerFTM
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
            services.AddControllers().AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.IgnoreNullValues = true;
               });

            services.AddSwaggerGen(serviceAction => {
                serviceAction.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Flight Ticket Restful API",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOption = new SwaggerOption();
            Configuration.GetSection(nameof(SwaggerOption)).Bind(swaggerOption);

            app.UseSwagger(option => {
                option.RouteTemplate = swaggerOption.JsonRoute;
            });

            app.UseSwaggerUI(option => {
                option.SwaggerEndpoint(swaggerOption.UIEndpoint, swaggerOption.Description);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

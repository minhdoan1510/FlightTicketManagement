using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using ServerFTM.JwtSetting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;

namespace ServerFTM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.IgnoreNullValues = true;
               });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            var jwtSettings = new JwtSettings();
            Configuration.Bind(key: nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(configureOptions: x =>
             {
                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
            .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
            services.AddSwaggerGen(serviceAction => {
                serviceAction.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Flight Ticket Restful API",
                    Version = "v1"
                });
                serviceAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                serviceAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },
                      Scheme = "Bearer",
                      Name = "Bearer",
                      In = ParameterLocation.Header,

                    },
                new List<string>()
          }
        });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Configurations;
using Domain.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartLock.WebAPI.Hubs;
using SmartLock.WebAPI.Models;
using SmartLock.WebAPI.Services;
using SmartLock.WebAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;

namespace SmartLock.WebAPI
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
            DbConfiguration.DbConnectionString = Configuration.GetConnectionString("LocalDb");

            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(DbConfiguration.DbConnectionString));
            services.AddSingleton<ITokenService, TokenService>(x => new TokenService(jwtSettings.ValidIssuer,
                jwtSettings.ValidAudience, jwtSettings.IssuerSigningKey, jwtSettings.TokenLifeTime));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // will the lifetime be validated
                        ValidateLifetime = jwtSettings.ValidateLifetime,

                        // specifies whether the publisher will validate when validating the token
                        ValidateIssuer = jwtSettings.ValidateIssuer,

                        // a string representing the publisher
                        ValidIssuer = jwtSettings.ValidIssuer,

                        // setting the token consumer
                        ValidAudience = jwtSettings.ValidAudience,

                        // Will the token consumer be validated
                        ValidateAudience = jwtSettings.ValidateAudience,

                        // validate the security key
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,

                        // set the security key
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.IssuerSigningKey))
                    };
                });

            services.AddAuthorization();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ILocksService, LocksService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "SmartLock",
                    Description = "Swagger Core API documentation",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseSignalR(routes =>
            {
                routes.MapHub<LockHub>("/Locks");
            });
            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
                c.RoutePrefix = String.Empty;
            });
        }
    }
}

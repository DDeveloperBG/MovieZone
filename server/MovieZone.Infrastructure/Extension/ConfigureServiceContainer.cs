namespace MovieZone.Infrastructure.Extension
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    using MovieZone.Common;
    using MovieZone.Data;

    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("MovieZoneDb"));
            }
            else
            {
                serviceCollection.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString(GlobalConstants.AppSettings.DatabaseConnectionKey)));
            }
        }

        public static void AddFirebaseAuth(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            string firebaseAppId = configuration.GetValue<string>("FIREBASE-APP-ID");
            serviceCollection
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://securetoken.google.com/{firebaseAppId}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{firebaseAppId}",
                        ValidateAudience = true,
                        ValidAudience = firebaseAppId,
                        ValidateLifetime = true,
                    };
                });
        }

        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = "MovieZone",
                        Version = "1",
                    });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = $"Input your Bearer token in this format - Bearer token to access this API",
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });
            });
        }

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson();
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddHealthCheck(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>(name: "Application DB Context", failureStatus: HealthStatus.Degraded)
                .AddUrlGroup(new Uri(configuration.GetValue<string>("applicationUrl")), name: "MovieZone", failureStatus: HealthStatus.Degraded)
                .AddSqlServer(configuration.GetConnectionString("OnionArchConn"));

            serviceCollection.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Basic Health Check", $"/healthz");
            }).AddInMemoryStorage();
        }
    }
}

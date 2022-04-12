namespace MovieZone.Infrastructure.Extension
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.OpenApi.Models;
    using MovieZone.Data;

    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        // public static void AddTransientServices(this IServiceCollection serviceCollection)
        // {
        // }
        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Onion Architecture WebAPI",
                        Version = "1",
                        Description = "Through this API you can access customer details",
                        Contact = new OpenApiContact()
                        {
                            Email = "amit.naik8103@gmail.com",
                            Name = "Amit Naik",
                            Url = new Uri("https://amitpnk.github.io/"),
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT"),
                        },
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
                .AddUrlGroup(new Uri("https://localhost:44356/"), name: "My personal website", failureStatus: HealthStatus.Degraded)
                .AddSqlServer(configuration.GetConnectionString("OnionArchConn"));

            serviceCollection.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Basic Health Check", $"/healthz");
            }).AddInMemoryStorage();
        }
    }
}

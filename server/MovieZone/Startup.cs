namespace MovieZone
{
    using System.Reflection;

    using HealthChecks.UI.Client;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.FeatureManagement;

    using MovieZone.Common;
    using MovieZone.DTOs;
    using MovieZone.Infrastructure.Extension;
    using MovieZone.Infrastructure.Seeding;
    using MovieZone.Persistence;
    using MovieZone.Service;
    using MovieZone.Service.Mapping;

    using Serilog;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddController();

            services.AddDbContext(this.configuration);

            services.AddSwaggerOpenAPI();

            services.AddScopedServices();
            services.AddTransientServices();
            services.AddServicesConfigurationKeysObjects(this.configuration);

            services.AddVersion();

            services.AddHealthCheck(this.configuration);

            services.AddFirebaseAuth(this.configuration);

            services.AddFeatureManagement();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            AutoMapperConfig.RegisterMappings(typeof(RootDto).GetTypeInfo().Assembly);
            Globals.AppSettings.ApplicationUrl = this.configuration["ApplicationUrl"];

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options =>
                 options.WithOrigins("http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            app.ConfigureCustomExceptionMiddleware();

            log.AddSerilog();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureSwagger();

            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
            }).UseHealthChecksUI(setup =>
             {
                 setup.ApiPath = "/healthcheck";
                 setup.UIPath = "/healthcheck-ui";
             });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

namespace MovieZone
{
    using System;
    using System.IO;

    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.FeatureManagement;
    using MovieZone.Domain.Settings;
    using MovieZone.Infrastructure.Extension;
    using MovieZone.Persistence;
    using MovieZone.Service;
    using Serilog;

    public class Startup
    {
        private readonly IConfigurationRoot configRoot;

        private readonly AppSettings appSettings;

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            this.Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            this.configRoot = builder.Build();

            this.appSettings = new AppSettings();
            this.Configuration.Bind(this.appSettings);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddController();

            services.AddDbContext(this.Configuration, this.configRoot);

            services.AddIdentityService(this.Configuration);

            services.AddAutoMapper();

            services.AddScopedServices();

            services.AddTransientServices();

            services.AddSwaggerOpenAPI();

            services.AddMailSetting(this.Configuration);

            services.AddServiceLayer();

            services.AddVersion();

            // services.AddHealthCheck(AppSettings, Configuration);
            services.AddFeatureManagement();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
                 options.WithOrigins("http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            app.ConfigureCustomExceptionMiddleware();

            log.AddSerilog();

            // app.ConfigureHealthCheck();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.ConfigureSwagger();

            // app.UseHealthChecks("/healthz", new HealthCheckOptions
            // {
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            //    ResultStatusCodes =
            //    {
            //        [HealthStatus.Healthy] = StatusCodes.Status200OK,
            //        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
            //        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            //    },
            // }).UseHealthChecksUI(setup =>
            //  {
            //      setup.ApiPath = "/healthcheck";
            //      setup.UIPath = "/healthcheck-ui";
            //      //setup.AddCustomStylesheet("Customization/custom.css");
            //  });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

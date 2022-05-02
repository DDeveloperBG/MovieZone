namespace MovieZone.Infrastructure.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MovieZone.Persistence;
    using MovieZone.Service.User;

    public class AdminSeeder : ISeeder
    {
        private const string AdminDisplayNamePath = "Admin:DisplayName";
        private const string AdminEmailPath = "Admin:Email";
        private const string AdminPasswordPath = "Admin:Password";

        public Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            var adminDisplayName = configuration[AdminDisplayNamePath];
            var adminEmail = configuration[AdminEmailPath];
            var adminPassword = configuration[AdminPasswordPath];

            var userService = serviceProvider.GetService<IUserService>();
            return userService.SeedAdminAsync(adminDisplayName, adminEmail, adminPassword);
        }
    }
}

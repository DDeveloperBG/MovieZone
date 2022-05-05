namespace MovieZone.Infrastructure.Seeding
{
    using System;
    using System.Threading.Tasks;

    using MovieZone.Persistence;

    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}

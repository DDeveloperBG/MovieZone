namespace MovieZone.Service
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MovieZone.Data;
    using MovieZone.Data.Common;
    using MovieZone.Data.Common.Repositories;
    using MovieZone.Data.Repositories;
    using MovieZone.Service.FirebaseIntegration;
    using MovieZone.Service.User;

    public static class DependencyInjection
    {
        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            serviceCollection.AddScoped<IDbQueryRunner, DbQueryRunner>();
        }

        public static void AddTransientServices(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddTransient<IFirebaseService>(_ =>
                new FirebaseService(configuration));
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}

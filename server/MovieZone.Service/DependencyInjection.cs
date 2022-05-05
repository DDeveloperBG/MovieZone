namespace MovieZone.Service
{
    using Amazon.S3;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MovieZone.Persistence;
    using MovieZone.Persistence.Common;
    using MovieZone.Persistence.Common.Repositories;
    using MovieZone.Persistence.Repositories;
    using MovieZone.Service.Actor;
    using MovieZone.Service.AWS.Storage.MovieStorage;
    using MovieZone.Service.Firebase;
    using MovieZone.Service.Movie;
    using MovieZone.Service.MoviesCategory;
    using MovieZone.Service.Pagination;
    using MovieZone.Service.Time;
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
            serviceCollection.AddTransient<IMovieStorageService>(_ =>
                new MovieStorageService(configuration));

            serviceCollection.AddTransient<IPaginationService, PaginationService>();

            serviceCollection.AddTransient<ITimeService, TimeService>();

            serviceCollection.AddTransient<IActorService, ActorService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IMovieService, MovieService>();
            serviceCollection.AddTransient<IMoviesCategoryService, MoviesCategoryService>();
        }
    }
}

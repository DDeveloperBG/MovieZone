namespace MovieZone.Service
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MovieZone.Common;

    using MovieZone.Persistence;

    using MovieZone.Persistence.Common;
    using MovieZone.Persistence.Common.Repositories;
    using MovieZone.Persistence.Repositories;

    using MovieZone.Service.Actor;
    using MovieZone.Service.AWS.Storage.MovieStorage;
    using MovieZone.Service.AWS.Storage.PublicImageStorage;
    using MovieZone.Service.DTOs.AWS.Storage;
    using MovieZone.Service.DTOs.Firebase;
    using MovieZone.Service.DTOs.Twilio;
    using MovieZone.Service.Firebase;
    using MovieZone.Service.Movie;
    using MovieZone.Service.MoviesCategory;
    using MovieZone.Service.Pagination;
    using MovieZone.Service.Time;
    using MovieZone.Service.TwilioVideoChat;
    using MovieZone.Service.User;
    using MovieZone.Service.VideoChatConversation;

    public static class DependencyInjection
    {
        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            serviceCollection.AddScoped<IDbQueryRunner, DbQueryRunner>();
        }

        public static void AddServicesConfigurationKeysObjects(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddSingleton<TwilioSettingsDTO>(_ =>
            {
                var configSettings = new TwilioSettingsDTO();
                configuration.Bind(Globals.VideoChat.ConfigKeysPath, configSettings);
                return configSettings;
            });

            serviceCollection.AddSingleton<FirebaseConfigKeys>(_ =>
            {
                FirebaseConfigKeys configKeys = new FirebaseConfigKeys();
                configuration.Bind(Globals.Firebase.ConfigKeysPath, configKeys);
                return configKeys;
            });

            serviceCollection.AddSingleton<AWSStorageConfigKeys>(_ =>
            {
                AWSStorageConfigKeys configKeys = new AWSStorageConfigKeys();
                configuration.Bind(Globals.AWS.Storage.ConfigKeysPath, configKeys);
                return configKeys;
            });
        }

        public static void AddTransientServices(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFirebaseService, FirebaseService>();
            serviceCollection.AddTransient<IMovieStorageService, MovieStorageService>();
            serviceCollection.AddTransient<IPublicImageStorageService, PublicImageStorageService>();
            serviceCollection.AddTransient<IVideoChat, VideoChat>();

            serviceCollection.AddTransient<IPaginationService, PaginationService>();

            serviceCollection.AddTransient<ITimeService, TimeService>();

            serviceCollection.AddTransient<IActorService, ActorService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IMovieService, MovieService>();
            serviceCollection.AddTransient<IMoviesCategoryService, MoviesCategoryService>();
            serviceCollection.AddTransient<IVideoChatConversationService, VideoChatConversationService>();
        }
    }
}

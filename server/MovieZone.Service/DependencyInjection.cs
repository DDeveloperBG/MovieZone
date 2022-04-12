namespace MovieZone.Service
{
    using Microsoft.Extensions.DependencyInjection;
    using MovieZone.Data;
    using MovieZone.Data.Common;
    using MovieZone.Data.Common.Repositories;
    using MovieZone.Data.Repositories;

    public static class DependencyInjection
    {
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
        }
    }
}

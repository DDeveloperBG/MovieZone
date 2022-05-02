namespace MovieZone.Service.Actor
{
    using System.Linq;

    using MovieZone.Domain.Entities;
    using MovieZone.Persistence.Common.Repositories;

    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> actorRepository;

        public ActorService(IRepository<Actor> actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        public Actor GetByName(string name)
        {
            return this.actorRepository
                .AllAsNoTracking()
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }
    }
}

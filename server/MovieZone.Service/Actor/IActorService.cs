namespace MovieZone.Service.Actor
{
    using MovieZone.Domain.Entities;

    public interface IActorService
    {
        Actor GetByName(string name);
    }
}

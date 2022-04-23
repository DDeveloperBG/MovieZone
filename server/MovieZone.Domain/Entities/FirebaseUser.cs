namespace MovieZone.Domain.Entities
{
    public class FirebaseUser
    {
        public FirebaseUser(string id)
        {
            this.Id = id;
        }

        public string Id { get; init; }
    }
}

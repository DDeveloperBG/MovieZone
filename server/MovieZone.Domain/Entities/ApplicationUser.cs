namespace MovieZone.Persistence.Models
{
    using MovieZone.Persistence.Common.Models;

    public class ApplicationUser : BaseDeletableModel<string>
    {
        public ApplicationUser(string id)
        {
            this.Id = id;
        }

        public string Email { get; set; }

        public string UserName { get; set; }
    }
}

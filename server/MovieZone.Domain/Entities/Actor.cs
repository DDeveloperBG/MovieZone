namespace MovieZone.Domain.Entities
{
    using System.Collections.Generic;

    using MovieZone.Data.Common.Models;

    public class Actor : BaseModel<int>
    {
        public Actor()
        {
            this.Movies = new HashSet<Movie>();
        }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}

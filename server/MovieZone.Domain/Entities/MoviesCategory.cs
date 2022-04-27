namespace MovieZone.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using MovieZone.Data.Common.Models;

    public class MoviesCategory : BaseDeletableModel<string>
    {
        public MoviesCategory()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Movies = new HashSet<Movie>();
        }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}

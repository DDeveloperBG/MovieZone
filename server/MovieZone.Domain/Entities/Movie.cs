namespace MovieZone.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using MovieZone.Data.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();

            this.MoviesCategories = new HashSet<MoviesCategory>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<MoviesCategory> MoviesCategories { get; set; }
    }
}

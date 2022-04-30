namespace MovieZone.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MovieZone.Data.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();

            this.MoviesCategories = new HashSet<MoviesCategory>();
            this.Actors = new HashSet<Actor>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(40)]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string ListingImgUrl { get; set; }

        [Required]
        public string DetailsImgUrl { get; set; }

        public int YearOfPublishing { get; set; }

        public int AgeRestriction { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<MoviesCategory> MoviesCategories { get; set; }

        public ICollection<Actor> Actors { get; set; }
    }
}

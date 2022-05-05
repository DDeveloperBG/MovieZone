namespace MovieZone.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MovieZone.Common;
    using MovieZone.Persistence.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();

            this.MoviesCategories = new HashSet<MoviesCategory>();
            this.Actors = new HashSet<Actor>();
        }

        [Required]
        [MinLength(ValidationConstants.Movie.NameMinLength)]
        [MaxLength(ValidationConstants.Movie.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ValidationConstants.Movie.DescriptionMinLength)]
        [MaxLength(ValidationConstants.Movie.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ListingImgUrl { get; set; }

        [Required]
        public string DetailsImgUrl { get; set; }

        public ushort YearOfPublishing { get; set; }

        public byte AgeRestriction { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<MoviesCategory> MovieCategories { get; set; }

        public ICollection<Actor> Actors { get; set; }
    }
}

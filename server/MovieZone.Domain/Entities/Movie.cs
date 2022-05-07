namespace MovieZone.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MovieZone.Common;
    using MovieZone.Persistence.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();

            this.MovieCategories = new HashSet<MoviesCategory>();
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
        public string ListingImgName { get; set; }

        [NotMapped]
        public string ListingImgUrl
            => $"{Globals.AppSettings.ApplicationUrl}api/file/getPublicImage?imageName={this.ListingImgName}";

        [Required]
        public string DetailsImgName { get; set; }

        [NotMapped]
        public string DetailsImgUrl
            => $"{Globals.AppSettings.ApplicationUrl}api/file/getPublicImage?imageName={this.DetailsImgName}";

        public ushort YearOfPublishing { get; set; }

        [Range(
            ValidationConstants.Movie.AgeRestrictionMinValue,
            ValidationConstants.Movie.AgeRestrictionMaxValue)]
        public byte AgeRestriction { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<MoviesCategory> MovieCategories { get; set; }

        public ICollection<Actor> Actors { get; set; }
    }
}

namespace MovieZone.Service.DTOs.Movie
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using MovieZone.Common;
    using MovieZone.Domain.Entities;
    using MovieZone.Service.Mapping;

    public class AddMovieInputDTO : IMapTo<Movie>
    {
        [Required]
        public IFormFile MovieFile { get; set; }

        [Required]
        [MinLength(ValidationConstants.Movie.NameMinLength)]
        [MaxLength(ValidationConstants.Movie.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ValidationConstants.Movie.DescriptionMinLength)]
        [MaxLength(ValidationConstants.Movie.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public IFormFile ListingImg { get; set; }

        [Required]
        public IFormFile DetailsImg { get; set; }

        [Required]
        public ushort YearOfPublishing { get; set; }

        [Required]
        [Range(
            ValidationConstants.Movie.AgeRestrictionMinValue,
            ValidationConstants.Movie.AgeRestrictionMaxValue)]
        public byte AgeRestriction { get; set; }

        [Required]
        [Range(0, 12)]
        public byte HoursDuration { get; set; } = 0;

        [Required]
        [Range(0, 60)]
        public byte MinutesDuration { get; set; } = 0;

        public IEnumerable<string> MoviesCategoriesNames { get; set; }

        public IEnumerable<string> ActorsNames { get; set; }
    }
}

namespace MovieZone.Service.DTOs.Movie
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using MovieZone.Domain.Entities;
    using MovieZone.Services.Mapping;

    public class GetMovieDetailsDTO : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string DetailsImgUrl { get; set; }

        public int YearOfPublishing { get; set; }

        public int AgeRestriction { get; set; }

        public int HoursDuration { get; set; }

        public int MinutesDuration { get; set; }

        public IEnumerable<string> ActorsNames { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Movie, GetMovieDetailsDTO>()
                .ForMember(
                    x => x.ActorsNames,
                    y => y.MapFrom(x => x.Actors.Select(x => x.Name)))
                .ForMember(
                    x => x.HoursDuration,
                    y => y.MapFrom(x => x.Duration.Hours))
                .ForMember(
                    x => x.MinutesDuration,
                    y => y.MapFrom(x => x.Duration.Minutes));
        }
    }
}

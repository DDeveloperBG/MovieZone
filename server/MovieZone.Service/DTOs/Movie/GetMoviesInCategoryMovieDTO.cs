namespace MovieZone.Service.DTOs.Movie
{
    using AutoMapper;

    using MovieZone.Domain.Entities;
    using MovieZone.Service.Mapping;

    public class GetMoviesInCategoryMovieDTO : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ListingImgUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Movie, GetMoviesInCategoryMovieDTO>()
                .ForMember(
                    x => x.Description,
                    y => y.MapFrom(x => x.Description.Substring(0, 65) + "..."));
        }
    }
}

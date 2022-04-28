namespace MovieZone.Service.DTOs.Movie
{
    using MovieZone.Domain.Entities;
    using MovieZone.Services.Mapping;

    public class GetMoviesInCategoryMovieDTO : IMapFrom<Movie>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }
    }
}

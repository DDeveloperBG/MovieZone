namespace MovieZone.Service.Movie
{
    using System.Collections.Generic;
    using System.Linq;

    using MovieZone.Data.Common.Repositories;
    using MovieZone.Domain.Entities;
    using MovieZone.Service.DTOs.Movie;
    using MovieZone.Services.Mapping;

    public class MovieService : IMovieService
    {
        private readonly IDeletableEntityRepository<Movie> movieRepository;

        public MovieService(IDeletableEntityRepository<Movie> movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public IEnumerable<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(string categoryId)
        {
            return this.movieRepository
                .AllAsNoTracking()
                .Where(x => x.MoviesCategories.Any(x => x.Id == categoryId))
                .To<GetMoviesInCategoryMovieDTO>()
                .ToList();
        }
    }
}

namespace MovieZone.Service.Movie
{
    using System.Collections.Generic;

    using MovieZone.Service.DTOs.Movie;

    public interface IMovieService
    {
        public IEnumerable<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(string categoryId);
    }
}

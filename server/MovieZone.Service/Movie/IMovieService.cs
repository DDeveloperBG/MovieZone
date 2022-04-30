namespace MovieZone.Service.Movie
{
    using MovieZone.Service.DTOs.Movie;
    using MovieZone.Service.DTOs.Pagination;

    public interface IMovieService
    {
        public PaginationDTO<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(string categoryId, int page);
    }
}

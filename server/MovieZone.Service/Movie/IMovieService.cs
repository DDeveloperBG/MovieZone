namespace MovieZone.Service.Movie
{
    using MovieZone.Service.DTOs.Movie;
    using MovieZone.Service.DTOs.Pagination;

    public interface IMovieService
    {
        PaginationDTO<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(string categoryId, int page);

        GetMovieDetailsDTO GetMovieDetails(string id);
    }
}

namespace MovieZone.Service.Movie
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using MovieZone.Common;
    using MovieZone.Data.Common.Repositories;
    using MovieZone.Domain.Entities;
    using MovieZone.Service.DTOs.Movie;
    using MovieZone.Service.DTOs.Pagination;
    using MovieZone.Service.Pagination;
    using MovieZone.Services.Mapping;

    public class MovieService : IMovieService
    {
        private const string DefaultMoviesCategoryName = GlobalConstants.CategoryMovies.DefaultMoviesCategoryName;
        private const int PageSize = GlobalConstants.CategoryMovies.PageSize;

        private readonly IDeletableEntityRepository<Movie> movieRepository;
        private readonly IPaginationService paginationService;

        public MovieService(
            IDeletableEntityRepository<Movie> movieRepository,
            IPaginationService paginationService)
        {
            this.movieRepository = movieRepository;
            this.paginationService = paginationService;
        }

        [HttpGet]
        public PaginationDTO<GetMoviesInCategoryMovieDTO> GetMoviesInCategory(
            string categoryId,
            int currentPage)
        {
            var query = this.movieRepository
                .AllAsNoTracking();

            if (categoryId != "default")
            {
                query = query.Where(x => x.MoviesCategories
                    .Any(x => x.Id == categoryId));
            }
            else
            {
                query = query.Where(x => x.MoviesCategories
                    .Any(x => x.Name == DefaultMoviesCategoryName));
            }

            var moviesAsIQuerable = query.To<GetMoviesInCategoryMovieDTO>();

            return this.paginationService.GetPaged(moviesAsIQuerable, currentPage, PageSize);
        }
    }
}

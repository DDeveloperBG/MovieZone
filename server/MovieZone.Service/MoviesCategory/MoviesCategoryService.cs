namespace MovieZone.Service.MoviesCategory
{
    using System.Collections.Generic;
    using System.Linq;

    using MovieZone.Data.Common.Repositories;
    using MovieZone.Domain.Entities;
    using MovieZone.Service.DTOs.MoviesCategory;
    using MovieZone.Services.Mapping;

    public class MoviesCategoryService : IMoviesCategoryService
    {
        private readonly IDeletableEntityRepository<MoviesCategory> moviesCategoryRepository;

        public MoviesCategoryService(IDeletableEntityRepository<MoviesCategory> moviesCategoryRepository)
        {
            this.moviesCategoryRepository = moviesCategoryRepository;
        }

        public IEnumerable<GetAllCategoriesMoviesCategoryDTO> GetAllCategories()
        {
            return this.moviesCategoryRepository
                .AllAsNoTracking()
                .To<GetAllCategoriesMoviesCategoryDTO>()
                .ToList();
        }
    }
}

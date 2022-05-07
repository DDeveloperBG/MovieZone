namespace MovieZone.Service.MoviesCategory
{
    using System.Collections.Generic;
    using System.Linq;

    using MovieZone.Domain.Entities;
    using MovieZone.Persistence.Common.Repositories;
    using MovieZone.Service.DTOs.MoviesCategory;
    using MovieZone.Service.Mapping;

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

        public MoviesCategory GetByName(string categoryName)
        {
            return this.moviesCategoryRepository
                .AllAsNoTracking()
                .Where(x => x.Name == categoryName)
                .FirstOrDefault();
        }
    }
}

namespace MovieZone.Service.MoviesCategory
{
    using System.Collections.Generic;

    using MovieZone.Service.DTOs.MoviesCategory;

    public interface IMoviesCategoryService
    {
        public IEnumerable<GetAllCategoriesMoviesCategoryDTO> GetAllCategories();
    }
}

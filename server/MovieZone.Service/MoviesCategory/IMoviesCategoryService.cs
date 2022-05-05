namespace MovieZone.Service.MoviesCategory
{
    using System.Collections.Generic;

    using MovieZone.Domain.Entities;
    using MovieZone.Service.DTOs.MoviesCategory;

    public interface IMoviesCategoryService
    {
        IEnumerable<GetAllCategoriesMoviesCategoryDTO> GetAllCategories();

        MoviesCategory GetByName(string categoryName);
    }
}

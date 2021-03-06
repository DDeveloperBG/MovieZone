namespace MovieZone.Service.DTOs.MoviesCategory
{
    using MovieZone.Domain.Entities;
    using MovieZone.Service.Mapping;

    public class GetAllCategoriesMoviesCategoryDTO : IMapFrom<MoviesCategory>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

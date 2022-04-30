namespace MovieZone.ApiControllers
{
    using Microsoft.AspNetCore.Mvc;

    using MovieZone.Service.MoviesCategory;

    [ApiController]
    [Route("api/[controller]")]
    public class MoviesCategoryController : ControllerBase
    {
        private readonly IMoviesCategoryService moviesCategoryService;

        public MoviesCategoryController(IMoviesCategoryService moviesCategoryService)
        {
            this.moviesCategoryService = moviesCategoryService;
        }

        [HttpGet]
        public IActionResult GetMoviesCategories()
        {
            var responce = this.moviesCategoryService.GetAllCategories();

            return this.Ok(responce);
        }
    }
}

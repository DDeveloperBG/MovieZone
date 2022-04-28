namespace MovieZone.ApiControllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieZone.Service.Movie;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService moviesService;

        public MovieController(IMovieService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet]
        public IActionResult GetMoviesInCategory([FromQuery] string categoryId, int page = 1)
        {
            var responce = this.moviesService.GetMoviesInCategory(categoryId);

            return this.Ok(responce);
        }
    }
}

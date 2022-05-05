namespace MovieZone.ApiControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MovieZone.Common;
    using MovieZone.Service.DTOs.Movie;
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
            var responce = this.moviesService.GetMoviesInCategory(categoryId, page);

            return this.Ok(responce);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMovieDetails([FromQuery] string movieId)
        {
            var responce = this.moviesService.GetMovieDetails(movieId);

            return this.Ok(responce);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AppRoles.AdminRoleName)]
        public async Task<IActionResult> AddMovie([FromForm] AddMovieInputDTO input)
        {
            await this.moviesService.AddMovieAsync(input);

            return this.Ok();
        }
    }
}

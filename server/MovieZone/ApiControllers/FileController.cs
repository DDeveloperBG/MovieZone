namespace MovieZone.ApiControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MovieZone.Service.AWS.Storage.MovieStorage;
    using MovieZone.Service.AWS.Storage.PublicImageStorage;
    using MovieZone.Service.User;

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMovieStorageService movieStorageService;
        private readonly IPublicImageStorageService publicImageStorageService;

        public FileController(
            IUserService userService,
            IMovieStorageService movieStorageService,
            IPublicImageStorageService publicImageStorageService)
        {
            this.movieStorageService = movieStorageService;
            this.publicImageStorageService = publicImageStorageService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieFile(
            [FromQuery]
            string movieId,
            string userIdToken)
        {
            bool isUserAuthorized = await this.userService.ValidateIsUserAuthorizedAsync(userIdToken);
            if (!isUserAuthorized)
            {
                return this.BadRequest("User has no access to watch this movie!");
            }

            var responce = await this.movieStorageService.GetFileByKeyAsync(movieId);

            return this.File(responce.FileStream, responce.FileType);
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicImage(
          [FromQuery]
          string imageName)
        {
            var responce = await this.publicImageStorageService.GetFileByKeyAsync(imageName);

            return this.File(responce.FileStream, responce.FileType);
        }
    }
}

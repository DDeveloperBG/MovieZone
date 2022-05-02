namespace MovieZone.ApiControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using MovieZone.DTOs.Auth;
    using MovieZone.Service.User;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult CheckIsUsernameUsed([FromQuery] string username)
        {
            bool isUsernameUsed = this.userService.CheckIsUsernameUsed(username);
            var responce = new { isUsernameUsed };

            return this.Ok(responce);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserInputDTO input)
        {
            await this.userService.RegisterAsync(input.IdToken, input.Username);

            return this.Ok();
        }
    }
}

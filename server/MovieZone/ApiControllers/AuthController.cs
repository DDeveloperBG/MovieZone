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

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserInput input)
        {
            await this.userService.RegisterAsync(input.IdToken, input.Username);

            return this.Ok();
        }
    }
}

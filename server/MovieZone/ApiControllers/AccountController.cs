namespace MovieZone.ApiControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MovieZone.Domain.Auth;
    using MovieZone.Service.Contract;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return this.Ok(await this.accountService.AuthenticateAsync(request, this.GenerateIPAddress()));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = this.Request.Headers["origin"];
            return this.Ok(await this.accountService.RegisterAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = this.Request.Headers["origin"];
            return this.Ok(await this.accountService.ConfirmEmailAsync(userId, code));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await this.accountService.ForgotPassword(model, this.Request.Headers["origin"]);
            return this.Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return this.Ok(await this.accountService.ResetPassword(model));
        }

        private string GenerateIPAddress()
        {
            if (this.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return this.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return this.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}

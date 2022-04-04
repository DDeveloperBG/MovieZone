namespace MovieZone.ApiControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MovieZone.Domain.Settings;
    using MovieZone.Service.Contract;

    [ApiController]
    [Route("api/v{version:apiVersion}/Mail")]
    [ApiVersion("1.0")]
    public class MailController : ControllerBase
    {
        private readonly IEmailService mailService;

        public MailController(IEmailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            await this.mailService.SendEmailAsync(request);
            return this.Ok();
        }
    }
}

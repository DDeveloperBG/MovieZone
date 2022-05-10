namespace MovieZone.ApiControllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MovieZone.Service.DTOs.Twilio;
    using MovieZone.Service.TwilioVideoChat;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VideoChatController : ControllerBase
    {
        private readonly IVideoChat videoChat;

        public VideoChatController(IVideoChat videoChat)
        {
            this.videoChat = videoChat;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCall([FromQuery] CreateCallInputDTO input)
        {
            input.CurrentUserId = this.User.Identity.Name;
            var responce = this.videoChat.CreateCall(input);

            return this.Ok(responce);
        }
    }
}

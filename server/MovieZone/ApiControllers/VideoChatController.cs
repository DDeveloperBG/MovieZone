namespace MovieZone.ApiControllers
{
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

        [HttpPost]
        public IActionResult GetTwilioToken([FromBody] GetTwilioTokenInputDTO input)
        {
            var jwt = this.videoChat.GetTwilioJwt(input);

            return this.Ok(new { token = jwt });
        }
    }
}

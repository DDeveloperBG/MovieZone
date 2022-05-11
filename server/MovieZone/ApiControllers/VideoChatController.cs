namespace MovieZone.ApiControllers
{
    using System.Linq;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateOrJoinConversationAsync(
            [FromQuery]
            CreateOrJoinConversationInputDTO input)
        {
            input.UserId = this.User.Claims.ToList().First(x => x.Type == "user_id").Value;
            input.UserName = this.User.Identity.Name;
            var responce = await this.videoChat.CreateOrJoinConversationAsync(input);

            return this.Ok(responce);
        }
    }
}

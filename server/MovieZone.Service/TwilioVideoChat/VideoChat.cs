namespace MovieZone.Service.TwilioVideoChat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieZone.Service.DTOs.Twilio;
    using MovieZone.Service.VideoChatConversation;

    using Twilio;
    using Twilio.Jwt.AccessToken;

    public class VideoChat : IVideoChat
    {
        private readonly TwilioSettingsDTO twilioSettingsDTO;

        private readonly IVideoChatConversationService videoChatConversationService;

        public VideoChat(
            TwilioSettingsDTO configSettings,
            IVideoChatConversationService videoChatConversationService)
        {
            this.twilioSettingsDTO = configSettings;

            TwilioClient.Init(this.twilioSettingsDTO.ApiKey, this.twilioSettingsDTO.ApiSecret);

            this.videoChatConversationService = videoChatConversationService;
        }

        // TODO: Notify called user
        public async Task<CreateOrJoinConversationResultDTO> CreateOrJoinConversationAsync(
            CreateOrJoinConversationInputDTO input)
        {
            var participants = new string[] { input.UserId, input.CalledUserId };

            string roomId = this.videoChatConversationService.GetConversationId(participants);
            if (string.IsNullOrEmpty(roomId))
            {
                roomId = await this.videoChatConversationService.CreateConversationAsync(participants);
            }

            string accessToken = this.GetAccessToken(input.UserId);

            return new CreateOrJoinConversationResultDTO
            {
                RoomId = roomId,
                AccessToken = accessToken,
            };
        }

        private string GetAccessToken(string identity)
        {
            var accessToken = new Token(
                    this.twilioSettingsDTO.AccountSid,
                    this.twilioSettingsDTO.ApiKey,
                    this.twilioSettingsDTO.ApiSecret,
                    identity,
                    grants: new HashSet<IGrant> { new VideoGrant() }).ToJwt();

            return accessToken;
        }
    }
}

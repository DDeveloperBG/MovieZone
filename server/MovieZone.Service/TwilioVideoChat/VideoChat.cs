namespace MovieZone.Service.TwilioVideoChat
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Configuration;

    using MovieZone.Common;
    using MovieZone.Service.DTOs.Twilio;

    using Twilio;
    using Twilio.Jwt.AccessToken;

    public class VideoChat : IVideoChat
    {
        private readonly TwilioSettingsDTO twilioSettingsDTO;

        public VideoChat(IConfiguration configuration)
        {
            this.twilioSettingsDTO = new TwilioSettingsDTO();
            configuration.Bind(Globals.VideoChat.ConfigKeys, this.twilioSettingsDTO);

            TwilioClient.Init(this.twilioSettingsDTO.ApiKey, this.twilioSettingsDTO.ApiSecret);
        }

        public CreateCallResultDTO CreateCall(CreateCallInputDTO input)
        {
            // TODO: Notify called user
            string roomId = Guid.NewGuid().ToString();

            string accessToken = new Token(
                   this.twilioSettingsDTO.AccountSid,
                   this.twilioSettingsDTO.ApiKey,
                   this.twilioSettingsDTO.ApiSecret,
                   input.CurrentUserId,
                   grants: new HashSet<IGrant> { new VideoGrant() }).ToJwt();

            return new CreateCallResultDTO
            {
                RoomId = roomId,
                AccessToken = accessToken,
            };
        }
    }
}

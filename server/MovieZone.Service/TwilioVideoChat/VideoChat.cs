namespace MovieZone.Service.TwilioVideoChat
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Configuration;

    using MovieZone.Service.DTOs.Twilio;

    using Twilio;
    using Twilio.Jwt.AccessToken;

    public class VideoChat : IVideoChat
    {
        private readonly TwilioSettingsDTO twilioSettingsDTO;

        public VideoChat(IConfiguration configuration)
        {
            this.twilioSettingsDTO = new TwilioSettingsDTO();
            configuration.Bind("TwilioConfigKeys", this.twilioSettingsDTO);

            TwilioClient.Init(this.twilioSettingsDTO.ApiKey, this.twilioSettingsDTO.ApiSecret);
        }

        public string GetTwilioJwt(GetTwilioTokenInputDTO input)
        {
            return new Token(
                   this.twilioSettingsDTO.AccountSid,
                   this.twilioSettingsDTO.ApiKey,
                   this.twilioSettingsDTO.ApiSecret,
                   input.Identity ?? Guid.NewGuid().ToString(),
                   grants: new HashSet<IGrant> { new VideoGrant() }).ToJwt();
        }
    }
}

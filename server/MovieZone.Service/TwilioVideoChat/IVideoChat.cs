namespace MovieZone.Service.TwilioVideoChat
{
    using MovieZone.Service.DTOs.Twilio;

    public interface IVideoChat
    {
        string GetTwilioJwt(GetTwilioTokenInputDTO input);
    }
}

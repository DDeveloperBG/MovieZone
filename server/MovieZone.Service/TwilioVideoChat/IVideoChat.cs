namespace MovieZone.Service.TwilioVideoChat
{
    using MovieZone.Service.DTOs.Twilio;

    public interface IVideoChat
    {
        CreateCallResultDTO CreateCall(CreateCallInputDTO input);
    }
}

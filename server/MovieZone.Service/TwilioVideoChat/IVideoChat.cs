namespace MovieZone.Service.TwilioVideoChat
{
    using System.Threading.Tasks;

    using MovieZone.Service.DTOs.Twilio;

    public interface IVideoChat
    {
        Task<CreateOrJoinConversationResultDTO> CreateOrJoinConversationAsync(CreateOrJoinConversationInputDTO input);
    }
}

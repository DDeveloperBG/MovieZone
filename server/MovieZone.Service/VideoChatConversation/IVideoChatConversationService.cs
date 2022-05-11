namespace MovieZone.Service.VideoChatConversation
{
    using System.Threading.Tasks;

    public interface IVideoChatConversationService
    {
        string GetConversationId(string[] participants);

        Task<string> CreateConversationAsync(string[] participantsId);
    }
}

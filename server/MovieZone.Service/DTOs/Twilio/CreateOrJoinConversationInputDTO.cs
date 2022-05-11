namespace MovieZone.Service.DTOs.Twilio
{
    public class CreateOrJoinConversationInputDTO
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string CalledUserId { get; set; }
    }
}

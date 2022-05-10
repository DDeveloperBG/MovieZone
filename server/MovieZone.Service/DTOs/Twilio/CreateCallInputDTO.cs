namespace MovieZone.Service.DTOs.Twilio
{
    public class CreateCallInputDTO
    {
        public string CalledUserId { get; set; }

        public string CurrentUserId { get; set; }
    }
}

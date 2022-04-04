namespace MovieZone.Service.Contract
{
    using System.Threading.Tasks;

    using MovieZone.Domain.Settings;

    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

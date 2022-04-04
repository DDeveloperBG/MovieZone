namespace MovieZone.Service.Implementation
{
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using MailKit.Security;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using MimeKit;

    using MovieZone.Domain.Settings;
    using MovieZone.Service.Contract;
    using MovieZone.Service.Exceptions;

    public class MailService : IEmailService
    {
        private readonly MailSettings mailSettings;

        private readonly ILogger<MailService> logger;

        public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
        {
            this.mailSettings = mailSettings.Value;
            this.logger = logger;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(mailRequest.From ?? this.mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(this.mailSettings.SmtpHost, this.mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(this.mailSettings.SmtpUser, this.mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}

using MailSentNotification.Helper;

namespace MailSentNotification.Respos.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest request);
    }
}

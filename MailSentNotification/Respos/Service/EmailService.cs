using MailKit.Net.Smtp;
using MailKit.Security;
using MailSentNotification.Helper;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailSentNotification.Respos.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _setting;

        public EmailService(IOptions<EmailSetting> setting)
        {
            _setting = setting.Value;
        }

        public async Task SendEmailAsync(MailRequest request)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_setting.Email);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_setting.Email, _setting.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}

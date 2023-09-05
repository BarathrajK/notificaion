using MailSentNotification.Helper;
using MailSentNotification.Respos.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailSentNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService  = emailService;
        }

        [HttpPost("sendmail")]

        public async Task<IActionResult> SendMail()
        {
            try
            {
                MailRequest mailRequest = new MailRequest();

                mailRequest.ToEmail = "nanthinitp23@gmail.com";
                mailRequest.Subject = "welcome to Barath Tech Services";
                mailRequest.Body = "Thanks for you accepted my request...";
                await _emailService.SendEmailAsync(mailRequest);
                return Ok(mailRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

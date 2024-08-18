using System.Net.Mail;
using System.Net;

namespace BrightStarPhase2AssessmentTestEventManagement.Notification
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_configuration["Email:SmtpServer"])
            {
                Port = int.Parse(_configuration["Email:Port"]),
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]),
                EnableSsl = true,
            };

            await client.SendMailAsync(new MailMessage(_configuration["Email:From"], email, subject, message));
        }
    }

}

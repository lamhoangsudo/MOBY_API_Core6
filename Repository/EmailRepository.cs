using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmai(Email emailTo)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailHost").Value));
            email.To.Add(MailboxAddress.Parse(emailTo.To));
            email.Subject = emailTo.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailTo.Body };
            var smtp = new SmtpClient();

            string emailUsername = _configuration.GetSection("EmailUserName").Value;
            string emailPassword = _configuration.GetSection("EmailPassword").Value;

            await smtp.ConnectAsync(_configuration.GetSection("EmailHost").Value, int.Parse(_configuration.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailUsername, emailPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

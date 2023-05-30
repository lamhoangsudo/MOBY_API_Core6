using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmai(Email emailTo)
        {
            var email = new MimeMessage();
            BodyBuilder bodyBuilder = new();
            StreamReader streamReader = File.OpenText("EmailFormat\\order.html");
            bodyBuilder.HtmlBody = streamReader.ReadToEnd();
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[user-name]", emailTo.UserName);
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[obj]", emailTo.Obj);
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[date-create]", emailTo.Link);
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailHost").Value));
            email.To.Add(MailboxAddress.Parse(emailTo.To));
            email.Subject = emailTo.Subject;
            email.Body = bodyBuilder.ToMessageBody();
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

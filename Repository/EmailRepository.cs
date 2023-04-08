using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MOBY_API_Core6.Models;
using MailKit.Net.Smtp;

namespace MOBY_API_Core6.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmai(Email emailTo)
        {
            var email = new MimeMessage();
            BodyBuilder bodyBuilder = new();
            StreamReader streamReader = File.OpenText("EmailFormat\\order.html");
            bodyBuilder.HtmlBody = streamReader.ReadToEnd();
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[user-name]", "a");
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[order-id]", "a");
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[date-create]", "a");
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[item-name]", "a");
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[number]", "a");
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[price]", "a");
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[total-price]", "a");
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailHost").Value));
            email.To.Add(MailboxAddress.Parse(emailTo.To));
            email.Subject = emailTo.Subject;
            //email.Body = bodyBuilder.ToMessageBody();
            email.Body = new TextPart(TextFormat.Html) { Text = emailTo.Body };
            var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, int.Parse(_configuration.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

using MimeKit;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Models
{
    public class EmailHTML
    {
        public void acb()
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            StreamReader streamReader = System.IO.File.OpenText("EmailFormat\\index.html");
            bodyBuilder.HtmlBody = streamReader.ReadToEnd();
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("[asacsac]", "a");
            //Email email = new("","","");
            //email.Body = bodyBuilder.HtmlBody;
        }
    }
}

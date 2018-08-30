using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyCarApp.Web.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions options;

        public EmailSender(IOptions<EmailOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = this.options.SendEmailApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(this.options.SenderEmail, this.options.SenderMessage);
            var to = new EmailAddress(email,email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);
            var statusCode = response.StatusCode;
        }
    }
}

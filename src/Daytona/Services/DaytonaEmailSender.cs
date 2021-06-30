using Daytona.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Daytona.Services
{
    public class DaytonaEmailSender : IDaytonaEmailSender
    {
        public AuthMessageSenderOptions Options { get; set; }

        public DaytonaEmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string toEmailAddress, string subject, string message, string maskedName)
        {
            return Execute(Options.Key, Options.User, subject, message, toEmailAddress, maskedName);
        }

        private async Task Execute(string apiKey, string fromAddress, string subject, string message, string toEmailAddress, string maskedName)
        {
            var client = new SendGridClient(new SendGridClientOptions { ApiKey = apiKey, HttpErrorAsException = true });
            var from = new EmailAddress(fromAddress, maskedName);
            var to = new EmailAddress(toEmailAddress);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, message);
            await client.SendEmailAsync(msg);
        }
    }
}

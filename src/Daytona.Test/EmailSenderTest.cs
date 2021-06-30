using Daytona.Options;
using Daytona.Services;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Daytona.Test
{
    public class EmailSenderTest
    {
        readonly IDaytonaEmailSender _sender;

        public EmailSenderTest()
        {
            var options = new Mock<IOptions<AuthMessageSenderOptions>>();
            options.SetupGet(x => x.Value)
                .Returns(new AuthMessageSenderOptions
                {
                    User = "noreply@khakpouri.net",
                    Key = "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"
                });
            _sender = new DaytonaEmailSender(options.Object);
        }

        [Fact]
        public async Task Send_Email_Test()
        {
            await _sender.SendEmailAsync("ali.khakpouri@gmail.com", "Testing email", "hello! this is your father.", "Daytona Test");
        }
    }
}

using System.Threading.Tasks;

namespace Daytona.Services
{
    public interface IDaytonaEmailSender
    {
        public Task SendEmailAsync(string emailAddress, string subject, string message, string maskedName);
    }
}

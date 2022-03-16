using System.Threading.Tasks;

namespace JobPortal.Service.Notifications
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string message);
    }
}

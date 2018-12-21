using System.Net.Mail;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailHistoryService
    {
        bool CreateAndSend(MailMessage mailMessage);
        Task<bool> CreateAndSendAsync(MailMessage mailMessage);
    }
}
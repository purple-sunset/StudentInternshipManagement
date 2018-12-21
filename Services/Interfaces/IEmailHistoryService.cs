using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Services.Interfaces
{
    public interface IEmailHistoryService: IIdentityMessageService
    {
        bool CreateAndSend(MailMessage mailMessage);
        Task<bool> CreateAndSendAsync(MailMessage mailMessage);
    }
}
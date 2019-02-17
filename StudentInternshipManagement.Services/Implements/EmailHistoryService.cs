using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;
using StudentInternshipManagement.Utilities;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IEmailHistoryService
    {
        bool CreateAndSend(MailMessage mailMessage);
        Task<bool> CreateAndSendAsync(MailMessage mailMessage);
    }

    #endregion

    #region Class

    public class EmailHistoryService : GenericService<EmailHistory>, IEmailHistoryService
    {
        public EmailHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool CreateAndSend(MailMessage mailMessage)
        {
            var emailHistory = new EmailHistory
            {
                Title = mailMessage.Subject,
                Body = mailMessage.Body,
                To = mailMessage.To.ToString(),
                CC = mailMessage.CC.ToString(),
                Attachments = mailMessage.Attachments.Select(x => x.Name).Aggregate((a, b) => a + ", " + b)
            };
            bool result = Add(emailHistory);
            if (result)
            {
                result = EmailSender.Send(mailMessage);
                emailHistory.IsDeleted = result;
                Update(emailHistory);
            }

            return result;
        }

        public async Task<bool> CreateAndSendAsync(MailMessage mailMessage)
        {
            var emailHistory = new EmailHistory
            {
                Title = mailMessage.Subject,
                Body = mailMessage.Body,
                To = mailMessage.To.ToString(),
                CC = mailMessage.CC.ToString(),
                Attachments = mailMessage.Attachments.Select(x => x.Name).Aggregate((a, b) => a + ", " + b)
            };
            bool result = await AddAsync(emailHistory);
            if (result)
            {
                result = await EmailSender.SendAsync(mailMessage);
                emailHistory.IsDeleted = result;
                await UpdateAsync(emailHistory);
            }

            return result;
        }
    }

    #endregion

}
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using Utilities;

namespace Services.Implements
{
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
            var result = Add(emailHistory);
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
            var result = await AddAsync(emailHistory);
            if (result)
            {
                result = await EmailSender.SendAsync(mailMessage);
                emailHistory.IsDeleted = result;
                await UpdateAsync(emailHistory);
            }

            return result;
        }
    }
}
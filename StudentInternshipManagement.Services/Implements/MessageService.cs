using System.Linq;
using StudentInternshipManagement.Models.Constants;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IMessageService : IGenericService<Message>
    {
        IQueryable<Message> GetByUserId(string userId);
        IQueryable<Message> GetDraftEmail(string userId);
        IQueryable<Message> GetReceivedEmail(string userId);
        IQueryable<Message> GetSentEmail(string userId);
    }

    #endregion

    #region Class

    public class MessageService : GenericService<Message>, IMessageService
    {
        public MessageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Message> GetByUserId(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking
                .Where(m => m.SenderId == userId || m.ReceiverId == userId).OrderByDescending(m => m.CreatedAt);
        }

        public IQueryable<Message> GetReceivedEmail(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking
                .Where(m => m.ReceiverId == userId && m.Status != MessageStatus.Draft)
                .OrderByDescending(m => m.CreatedAt);
        }

        public IQueryable<Message> GetSentEmail(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking
                .Where(m => m.SenderId == userId && m.Status != MessageStatus.Draft)
                .OrderByDescending(m => m.CreatedAt);
        }

        public IQueryable<Message> GetDraftEmail(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking
                .Where(m => m.SenderId == userId && m.Status == MessageStatus.Draft)
                .OrderByDescending(m => m.CreatedAt);
        }
    }

    #endregion

}
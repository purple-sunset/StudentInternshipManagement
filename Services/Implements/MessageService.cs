using System;
using System.Linq;
using Models.Constants;
using Models.Entities;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class MessageService : GenericService<Message>, IMessageService
    {
        public MessageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Message> GetByUserId(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking.Where(m => m.SenderId == userId || m.ReceiverId == userId).OrderByDescending(m => m.CreatedAt);
        }

        public IQueryable<Message> GetReceivedEmail(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking.Where(m => m.ReceiverId == userId && m.Status != MessageStatus.Draft).OrderByDescending(m => m.CreatedAt);
        }

        public IQueryable<Message> GetSentEmail(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking.Where(m => m.SenderId == userId && m.Status != MessageStatus.Draft).OrderByDescending(m => m.CreatedAt);
        }

        public IQueryable<Message> GetDraftEmail(string userId)
        {
            return UnitOfWork.Repository<Message>().TableNoTracking.Where(m => m.SenderId == userId && m.Status == MessageStatus.Draft).OrderByDescending(m => m.CreatedAt);
        }
        
    }
}

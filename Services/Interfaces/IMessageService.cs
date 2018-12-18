using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        IQueryable<Message> GetByUserId(string userId);
        IQueryable<Message> GetDraftEmail(string userId);
        IQueryable<Message> GetReceivedEmail(string userId);
        IQueryable<Message> GetSentEmail(string userId);
    }
}
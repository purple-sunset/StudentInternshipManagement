using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface INotificationService : IGenericService<Notification>
    {
    }

    #endregion

    #region Class

    public class NotificationService : GenericService<Notification>, INotificationService
    {
        public NotificationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface INewsService : IGenericService<News>
    {
    }

    #endregion

    #region Class

    public class NewsService : GenericService<News>, INewsService
    {
        public NewsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}
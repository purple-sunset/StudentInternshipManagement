using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IStatisticService : IGenericService<Statistic>
    {
    }

    #endregion

    #region Class

    public class StatisticService : GenericService<Statistic>, IStatisticService
    {
        public StatisticService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}
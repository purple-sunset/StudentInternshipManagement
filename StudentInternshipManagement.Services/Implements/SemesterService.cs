using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface ISemesterService : IGenericService<Semester>
    {
        Semester GetLatest();
        Task<Semester> GetLatestAsync();
    }

    #endregion

    #region Class

    public class SemesterService : GenericService<Semester>, ISemesterService
    {
        public SemesterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Semester GetLatest()
        {
            return UnitOfWork.Repository<Semester>().TableNoTracking.OrderByDescending(s => s.Id).FirstOrDefault();
        }

        public async Task<Semester> GetLatestAsync()
        {
            return await UnitOfWork.Repository<Semester>().TableNoTracking.OrderByDescending(s => s.Id)
                .FirstOrDefaultAsync();
        }
    }

    #endregion

}
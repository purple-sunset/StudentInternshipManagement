using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface ICompanyTrainingMajorService : IGenericService<CompanyTrainingMajor>
    {
        IQueryable<CompanyTrainingMajor> GetByCompany(int companyId);
        CompanyTrainingMajor GetById(int companyId, int trainingMajorId);
        Task<CompanyTrainingMajor> GetByIdAsync(int companyId, int trainingMajorId);
        IQueryable<CompanyTrainingMajor> GetByTrainingMajor(int trainingMajorId);
    }

    #endregion

    #region Class

    public class CompanyTrainingMajorService : GenericService<CompanyTrainingMajor>, ICompanyTrainingMajorService
    {
        public CompanyTrainingMajorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CompanyTrainingMajor GetById(int companyId, int trainingMajorId)
        {
            return UnitOfWork.Repository<CompanyTrainingMajor>().Table
                .FirstOrDefault(x => x.CompanyId == companyId && x.TrainingMajorId == trainingMajorId);
        }

        public async Task<CompanyTrainingMajor> GetByIdAsync(int companyId, int trainingMajorId)
        {
            return await UnitOfWork.Repository<CompanyTrainingMajor>().Table
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.TrainingMajorId == trainingMajorId);
        }

        public IQueryable<CompanyTrainingMajor> GetByCompany(int companyId)
        {
            return UnitOfWork.Repository<CompanyTrainingMajor>().TableNoTracking.Where(x => x.CompanyId == companyId);
        }

        public IQueryable<CompanyTrainingMajor> GetByTrainingMajor(int trainingMajorId)
        {
            return UnitOfWork.Repository<CompanyTrainingMajor>().TableNoTracking
                .Where(x => x.TrainingMajorId == trainingMajorId);
        }
    }

    #endregion

}
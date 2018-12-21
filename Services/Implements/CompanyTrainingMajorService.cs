using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
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
}
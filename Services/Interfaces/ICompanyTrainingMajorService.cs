using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ICompanyTrainingMajorService:IGenericService<CompanyTrainingMajor>
    {
        IQueryable<CompanyTrainingMajor> GetByCompany(int companyId);
        CompanyTrainingMajor GetById(int companyId, int trainingMajorId);
        Task<CompanyTrainingMajor> GetByIdAsync(int companyId, int trainingMajorId);
        IQueryable<CompanyTrainingMajor> GetByTrainingMajor(int trainingMajorId);
        
    }
}
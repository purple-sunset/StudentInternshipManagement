using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ITrainingMajorService : IGenericService<TrainingMajor>
    {
        IQueryable<Company> GetCompanyList(int companyId);
    }
}
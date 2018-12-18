using System.Linq;
using Models;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ICompanyService : IGenericService<Company>
    {
        IQueryable<TrainingMajor> GetMajorList(int companyId);
    }
}
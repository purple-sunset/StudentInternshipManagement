using System.Linq;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface ICompanyService : IGenericService<Company>
    {
        IQueryable<TrainingMajor> GetMajorList(int companyId);
    }

    #endregion

    #region Class

    public class CompanyService : GenericService<Company>, ICompanyService
    {
        private readonly ICompanyTrainingMajorService _companyTrainingMajorService;

        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CompanyService(IUnitOfWork unitOfWork, ICompanyTrainingMajorService companyTrainingMajorService) :
            base(unitOfWork)
        {
            _companyTrainingMajorService = companyTrainingMajorService;
        }

        public IQueryable<TrainingMajor> GetMajorList(int companyId)
        {
            return _companyTrainingMajorService.GetByCompany(companyId).Select(m => m.TrainingMajor);
        }
    }

    #endregion

}
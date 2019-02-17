using System.Linq;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface ITrainingMajorService : IGenericService<TrainingMajor>
    {
        IQueryable<Company> GetCompanyList(int companyId);
    }

    #endregion

    #region Class

    public class TrainingMajorService : GenericService<TrainingMajor>, ITrainingMajorService
    {
        private readonly ICompanyTrainingMajorService _companyTrainingMajorService;

        public TrainingMajorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TrainingMajorService(IUnitOfWork unitOfWork, CompanyTrainingMajorService companyTrainingMajorService) :
            base(unitOfWork)
        {
            _companyTrainingMajorService = companyTrainingMajorService;
        }

        public IQueryable<Company> GetCompanyList(int companyId)
        {
            return _companyTrainingMajorService.GetByTrainingMajor(companyId).Select(x => x.Company);
        }
    }

    #endregion

}
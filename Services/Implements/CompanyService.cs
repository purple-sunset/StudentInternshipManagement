using System.Linq;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
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
}
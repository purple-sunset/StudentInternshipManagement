using System;
using System.Linq;
using Models;
using Models.Entities;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class TrainingMajorService : GenericService<TrainingMajor>, ITrainingMajorService
    {
        private readonly ICompanyTrainingMajorService _companyTrainingMajorService;
        public TrainingMajorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TrainingMajorService(IUnitOfWork unitOfWork, CompanyTrainingMajorService companyTrainingMajorService) : base(unitOfWork)
        {
            _companyTrainingMajorService = companyTrainingMajorService;
        }

        public IQueryable<Company> GetCompanyList(int companyId)
        {
            return _companyTrainingMajorService.GetByTrainingMajor(companyId).Select(x => x.Company);
        }
    }
}

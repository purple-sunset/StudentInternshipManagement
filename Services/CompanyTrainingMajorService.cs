using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class CompanyTrainingMajorService : IDisposable
    {
        private readonly CompanyTrainingMajorRepository _repository=new CompanyTrainingMajorRepository();

        public IQueryable<CompanyTrainingMajor> GetAll()
        {
            return _repository.GetAll();
        }
        public CompanyTrainingMajor GetById(int companyId, int trainingMajorId)
        {
            return _repository.GetById(companyId,trainingMajorId);
        }

        public IQueryable<CompanyTrainingMajor> GetByCompany(int companyId)
        {
            return _repository.GetByCompany(companyId);
        }

        public IQueryable<CompanyTrainingMajor> GetByTrainingMajor(int trainingMajorId)
        {
            return _repository.GetByTrainingMajor(trainingMajorId);
        }

        public bool Add(CompanyTrainingMajor companyTrainingMajor)
        {
            return _repository.Add(companyTrainingMajor);
        }

        public bool Update(CompanyTrainingMajor companyTrainingMajor)
        {
            return _repository.Update(companyTrainingMajor);
        }

        public bool Delete(CompanyTrainingMajor companyTrainingMajor)
        {
            return _repository.Delete(companyTrainingMajor);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class TrainingMajorService : IDisposable
    {
        private readonly TrainingMajorRepository _repository=new TrainingMajorRepository();

        public IQueryable<TrainingMajor> GetAll()
        {
            return _repository.GetAll();
        }
        public TrainingMajor GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Company> GetCompanyList(int id)
        {
            return _repository.GetCompanyList(id);
        }

        public bool Add(TrainingMajor trainingMajor)
        {
            return _repository.Add(trainingMajor);
        }

        public bool Update(TrainingMajor trainingMajor)
        {
            return _repository.Update(trainingMajor);
        }

        public bool Delete(TrainingMajor trainingMajor)
        {
            return _repository.Delete(trainingMajor);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

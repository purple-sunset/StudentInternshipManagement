using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class SemesterService : IDisposable
    {
        private readonly SemesterRepository _repository=new SemesterRepository();

        public IQueryable<Semester> GetAll()
        {
            return _repository.GetAll();
        }
        public Semester GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Semester semester)
        {
            return _repository.Add(semester);
        }

        public bool Update(Semester semester)
        {
            return _repository.Update(semester);
        }

        public bool Delete(Semester semester)
        {
            return _repository.Delete(semester);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

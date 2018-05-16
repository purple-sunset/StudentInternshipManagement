using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class SubjectService : IDisposable
    {
        private readonly SubjectRepository _repository=new SubjectRepository();

        public IQueryable<Subject> GetAll()
        {
            return _repository.GetAll();
        }
        public Subject GetById(string id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Subject subject)
        {
            return _repository.Add(subject);
        }

        public bool Update(Subject subject)
        {
            return _repository.Update(subject);
        }

        public bool Delete(Subject subject)
        {
            return _repository.Delete(subject);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

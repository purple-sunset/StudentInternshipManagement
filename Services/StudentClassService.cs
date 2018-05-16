using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class StudentClassService : IDisposable
    {
        private readonly StudentClassRepository _repository=new StudentClassRepository();

        public IQueryable<StudentClass> GetAll()
        {
            return _repository.GetAll();
        }

        public StudentClass GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(StudentClass studentClass)
        {
            return _repository.Add(studentClass);
        }

        public bool Update(StudentClass studentClass)
        {
            return _repository.Update(studentClass);
        }

        public bool Delete(StudentClass studentClass)
        {
            return _repository.Delete(studentClass);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

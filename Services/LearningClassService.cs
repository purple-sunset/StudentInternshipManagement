using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class LearningClassService : IDisposable
    {
        private readonly LearningClassRepository _repository=new LearningClassRepository();

        public IQueryable<LearningClass> GetAll()
        {
            return _repository.GetAll();
        }

        public LearningClass GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Student> GetStudentList(int id)
        {
            return _repository.GetStudentList(id);
        }

        public bool Add(LearningClass learningClass)
        {
            return _repository.Add(learningClass);
        }

        public bool Update(LearningClass learningClass)
        {
            return _repository.Update(learningClass);
        }

        public bool Delete(LearningClass learningClass)
        {
            return _repository.Delete(learningClass);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

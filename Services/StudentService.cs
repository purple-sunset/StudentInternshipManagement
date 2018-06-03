using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class StudentService : IDisposable
    {
        private readonly StudentRepository _repository=new StudentRepository();

        public IQueryable<Student> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Student> GetByStudentClass(int classId)
        {
            return _repository.GetByStudentClass(classId);
        }

        public Student GetById(string id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<LearningClass> GetLearningClassList(string id)
        {
            return _repository.GetLearningClassList(id);
        }

        public IQueryable<LearningClass> GetLearningClassBySemesterList(string id)
        {
            using (var semesterRepository = new SemesterRepository())
            {
                var semesterId = semesterRepository.GetLatest().SemesterId;
                return _repository.GetLearningClassList(id).Where(c=>c.SemesterId==semesterId);
            }
        }

        public bool Add(Student student)
        {
            return _repository.Add(student);
        }

        public bool Update(Student student)
        {
            return _repository.Update(student);
        }

        public bool Delete(Student student)
        {
            return _repository.Delete(student);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class LearningClassStudentService : IDisposable
    {
        private readonly LearningClassStudentRepository _repository=new LearningClassStudentRepository();

        public IQueryable<LearningClassStudent> GetAll()
        {
            return _repository.GetAll();
        }
        public LearningClassStudent GetById(int classId, string studentId)
        {
            return _repository.GetById(classId,studentId);
        }

        public IQueryable<LearningClassStudent> GetByClass(int classId)
        {
            return _repository.GetByClass(classId);
        }

        public IQueryable<LearningClassStudent> GetByStudent(string studentId)
        {
            return _repository.GetByStudent(studentId);
        }

        public IQueryable<LearningClassStudent> GetByTeacher(string teacherId)
        {
            return _repository.GetByTeacher(teacherId);
        }

        public bool Add(LearningClassStudent learningClassStudent)
        {
            return _repository.Add(learningClassStudent);
        }

        public bool Update(LearningClassStudent learningClassStudent)
        {
            return _repository.Update(learningClassStudent);
        }

        public bool Delete(LearningClassStudent learningClassStudent)
        {
            return _repository.Delete(learningClassStudent);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

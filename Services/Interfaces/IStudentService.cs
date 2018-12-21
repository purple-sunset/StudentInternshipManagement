using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IStudentService
    {
        IQueryable<Student> GetByStudentClass(int classId);
        IQueryable<LearningClass> GetLearningClassBySemesterList(int studentId);
        IQueryable<LearningClass> GetLearningClassList(int studentId);
    }
}
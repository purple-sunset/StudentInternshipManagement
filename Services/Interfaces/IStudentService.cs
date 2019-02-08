using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IStudentService : IGenericService<Student>
    {
        IQueryable<Student> GetByStudentClass(int classId);
        IQueryable<LearningClass> GetLearningClassBySemesterList(string studentCode);
        IQueryable<LearningClass> GetLearningClassList(string studentCode);
        Student GetByStudentCode(string studentCode);
    }
}
using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IStudentService:IGenericService<Student>
    {
        IQueryable<Student> GetByStudentClass(int classId);
        IQueryable<LearningClass> GetLearningClassBySemesterList(int studentId);
        IQueryable<LearningClass> GetLearningClassList(int studentId);
    }
}
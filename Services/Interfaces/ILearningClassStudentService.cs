using System.Linq;
using Models;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ILearningClassStudentService:IGenericService<LearningClassStudent>
    {
        IQueryable<LearningClassStudent> GetByClass(int classId);
        LearningClassStudent GetById(int classId, int studentId);
        IQueryable<LearningClassStudent> GetByStudent(int studentId);
        IQueryable<LearningClassStudent> GetByTeacher(int teacherId);
    }
}
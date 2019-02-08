using System.Linq;
using System.Threading.Tasks;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ILearningClassStudentService : IGenericService<LearningClassStudent>
    {
        IQueryable<LearningClassStudent> GetByClass(int classId);
        LearningClassStudent GetById(int classId, int studentId);
        Task<LearningClassStudent> GetByIdAsync(int classId, int studentId);
        IQueryable<LearningClassStudent> GetByStudent(string studentCode);
        IQueryable<LearningClassStudent> GetByTeacher(int teacherId);
    }
}
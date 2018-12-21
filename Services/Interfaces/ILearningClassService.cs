using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ILearningClassService : IGenericService<LearningClass>
    {
        IQueryable<Student> GetStudentList(int classId);
    }
}
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IGroupService : IGenericService<Group>
    {
        Group GetByInternship(Internship internship);
        Task<Group> GetByInternshipAsync(Internship internship);
        IQueryable<Group> GetByStudent(string studentCode);
        IQueryable<Group> GetByTeacher(string teacherCode);
        IQueryable<Group> GetBySemester(int semesterId);
    }
}
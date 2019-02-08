using System.Linq;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IInternshipService : IGenericService<Internship>
    {
        void AssignInternship();
        void CreateGroup();
        IQueryable<Internship> GetByLatestSemester();
        IQueryable<Internship> GetBySemester(int semesterId);
        void ProcessRegistration();
        IQueryable<Internship> GetByStudent(string studentCode);
    }
}
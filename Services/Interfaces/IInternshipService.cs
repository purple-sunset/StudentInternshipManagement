using System.Linq;
using Models;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IInternshipService
    {
        void AssignInternship();
        void CreateGroup();
        IQueryable<Internship> GetByLatestSemester();
        IQueryable<Internship> GetBySemester(int semesterId);
        void ProcessRegistration();
    }
}
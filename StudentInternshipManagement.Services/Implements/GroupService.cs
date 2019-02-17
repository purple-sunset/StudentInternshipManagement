using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IGroupService : IGenericService<Group>
    {
        Group GetByInternship(Internship internship);
        Task<Group> GetByInternshipAsync(Internship internship);
        IQueryable<Group> GetByStudent(int studentId);
        IQueryable<Group> GetByTeacher(int teacherId);
        IQueryable<Group> GetBySemester(int semesterId);
    }

    #endregion

    #region Class

    public class GroupService : GenericService<Group>, IGroupService
    {
        public GroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Group> GetByStudent(int studentId)
        {
            return UnitOfWork.Repository<Group>().TableNoTracking
                .Where(g => g.Members.Select(s => s.Id).Contains(studentId));
        }

        public Group GetByInternship(Internship internship)
        {
            return UnitOfWork.Repository<Group>().Table.FirstOrDefault(g =>
                g.ClassId == internship.ClassId && g.Members.Select(s => s.Id).Contains(internship.StudentId));
        }

        public async Task<Group> GetByInternshipAsync(Internship internship)
        {
            return await UnitOfWork.Repository<Group>().Table.FirstOrDefaultAsync(g =>
                g.ClassId == internship.ClassId && g.Members.Select(s => s.Id).Contains(internship.StudentId));
        }

        public IQueryable<Group> GetByTeacher(int teacherId)
        {
            return UnitOfWork.Repository<Group>().TableNoTracking.Where(g => g.TeacherId == teacherId);
        }

        public IQueryable<Group> GetBySemester(int semesterId)
        {
            return UnitOfWork.Repository<Group>().TableNoTracking.Where(i => i.Class.SemesterId == semesterId);
        }
    }

    #endregion

}
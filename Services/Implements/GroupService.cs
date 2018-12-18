using System.Linq;
using Models;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class GroupService : GenericService<Group>, IGroupService
    {
        public GroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Group> GetByStudent(int studentId)
        {
            return UnitOfWork.Repository<Group>().TableNoTracking.Where(g => g.Members.Select(s => s.Id).Contains(studentId));
        }

        public Group GetByInternship(Internship internship)
        {
            return UnitOfWork.Repository<Group>().Table.FirstOrDefault(g =>
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
}

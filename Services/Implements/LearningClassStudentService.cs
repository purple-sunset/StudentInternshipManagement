using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class LearningClassStudentService : GenericService<LearningClassStudent>, ILearningClassStudentService
    {
        private readonly IGroupService _groupService;

        public LearningClassStudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public LearningClassStudentService(IUnitOfWork unitOfWork, IGroupService groupService) : base(unitOfWork)
        {
            _groupService = groupService;
        }

        public LearningClassStudent GetById(int classId, int studentId)
        {
            return UnitOfWork.Repository<LearningClassStudent>().TableNoTracking
                .FirstOrDefault(s => s.ClassId == classId && s.StudentId == studentId);
        }

        public async Task<LearningClassStudent> GetByIdAsync(int classId, int studentId)
        {
            return await UnitOfWork.Repository<LearningClassStudent>().TableNoTracking
                .FirstOrDefaultAsync(s => s.ClassId == classId && s.StudentId == studentId);
        }

        public IQueryable<LearningClassStudent> GetByClass(int classId)
        {
            return UnitOfWork.Repository<LearningClassStudent>().TableNoTracking.Where(s => s.ClassId == classId);
        }

        public IQueryable<LearningClassStudent> GetByStudent(string studentCode)
        {
            return UnitOfWork.Repository<LearningClassStudent>().TableNoTracking.Where(s => s.Student.StudentCode == studentCode);
        }

        public IQueryable<LearningClassStudent> GetByTeacher(string teacherCode)
        {
            IQueryable<Group> groups = _groupService.GetByTeacher(teacherCode);
            IQueryable<int> students = groups.SelectMany(g => g.Members).Select(s => s.Id);
            return GetAll().Where(l => students.Contains(l.StudentId));
        }
    }
}
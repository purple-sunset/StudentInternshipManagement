using System.Linq;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class TeacherService : GenericService<Teacher>, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Teacher GetByTeacherCode(string teacherCode)
        {
            return UnitOfWork.Repository<Teacher>().Table.FirstOrDefault(x => x.TeacherCode == teacherCode);
        }
    }
}
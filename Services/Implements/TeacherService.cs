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
    }
}
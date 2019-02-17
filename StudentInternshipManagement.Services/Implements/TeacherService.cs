using System.Linq;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface ITeacherService : IGenericService<Teacher>
    {
        Teacher GetByUserName(string userName);
    }

    #endregion

    #region Class

    public class TeacherService : GenericService<Teacher>, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Teacher GetByUserName(string userName)
        {
            return UnitOfWork.Repository<Teacher>().Table.FirstOrDefault(x => x.User.UserName == userName);
        }
    }

    #endregion

}
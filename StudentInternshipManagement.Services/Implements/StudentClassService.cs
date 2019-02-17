using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IStudentClassService : IGenericService<StudentClass>
    {
    }

    #endregion

    #region Class

    public class StudentClassService : GenericService<StudentClass>, IStudentClassService
    {
        public StudentClassService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}
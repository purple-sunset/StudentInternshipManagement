using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface ISubjectService : IGenericService<Subject>
    {
    }

    #endregion

    #region Class

    public class SubjectService : GenericService<Subject>, ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IDepartmentService : IGenericService<Department>
    {
    }

    #endregion

    #region Class

    public class DepartmentService : GenericService<Department>, IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}
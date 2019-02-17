using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IAdminService : IGenericService<Admin>
    {
        Admin GetByUserName(string userName);
        Task<Admin> GetByUserNameAsync(string userName);
    }

    #endregion

    #region Class

    public class AdminService : GenericService<Admin>, IAdminService
    {
        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Admin GetByUserName(string userName)
        {
            return UnitOfWork.Repository<Admin>().Table.FirstOrDefault(x => x.User.UserName == userName);
        }

        public async Task<Admin> GetByUserNameAsync(string userName)
        {
            return await UnitOfWork.Repository<Admin>().Table.FirstOrDefaultAsync(x => x.UserId == userName);
        }
    }

    #endregion

}
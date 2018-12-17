using System.Linq;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class AdminService : GenericService<Admin>, IAdminService
    {
        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Admin GetByTeacherCode(string code)
        {
            return UnitOfWork.Repository<Admin>().TableNoTracking.FirstOrDefault(x => x.AdminCode == code);
        }

    }
}

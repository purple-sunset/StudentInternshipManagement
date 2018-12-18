using System.Linq;
using Models;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class AdminService : GenericService<Admin>, IAdminService
    {
        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Admin GetByTeacherCode(string teacherCode)
        {
            return UnitOfWork.Repository<Admin>().Table.FirstOrDefault(x => x.AdminCode == teacherCode);
        }

    }
}

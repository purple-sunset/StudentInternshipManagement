using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Admin> GetByTeacherCodeAsync(string teacherCode)
        {
            return await UnitOfWork.Repository<Admin>().Table.FirstOrDefaultAsync(x => x.AdminCode == teacherCode);
        }
    }
}
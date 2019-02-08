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

        public Admin GetByAdminCode(string adminCode)
        {
            return UnitOfWork.Repository<Admin>().Table.FirstOrDefault(x => x.AdminCode == adminCode);
        }

        public async Task<Admin> GetByAdminCodeAsync(string adminCode)
        {
            return await UnitOfWork.Repository<Admin>().Table.FirstOrDefaultAsync(x => x.AdminCode == adminCode);
        }
    }
}
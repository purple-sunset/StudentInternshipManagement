using System.Threading.Tasks;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IAdminService : IGenericService<Admin>
    {
        Admin GetByAdminCode(string adminCode);
        Task<Admin> GetByAdminCodeAsync(string adminCode);
    }
}
using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Entities;

namespace Services.Interfaces
{
    public interface IUserService
    {
        bool Add(ApplicationUser user, string role);
        Task<bool> AddAsync(ApplicationUser user, string role);
        bool Delete(string id);
        Task<bool> DeleteAsync(string id);
        bool Delete(ApplicationUser user);
        Task<bool> DeleteAsync(ApplicationUser user);
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        ApplicationUser GetByUserName(string userName);
        bool Update(ApplicationUser user);
        Task<bool> UpdateAsync(ApplicationUser user);
    }
}
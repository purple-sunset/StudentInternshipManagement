using System.Linq;
using System.Threading.Tasks;
using Models.Entities;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> Table { get; }

        IQueryable<ApplicationUser> TableNoTracking { get; }
        ApplicationUser GetById(string id);
        Task<ApplicationUser> GetByIdAsync(string id);

        ApplicationUser GetByUserName(string userName);
        Task<ApplicationUser> GetByUserNameAsync(string userName);

        ApplicationUser GetByEmail(string email);
        Task<ApplicationUser> GetByEmailAsync(string email);

        void Add(ApplicationUser user, string role);

        void Update(ApplicationUser user);

        void Delete(string id);

        void Delete(ApplicationUser user);
    }
}
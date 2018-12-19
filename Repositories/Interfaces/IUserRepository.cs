using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Entities;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser GetById(string id);
        Task<ApplicationUser> GetByIdAsync(string id);

        ApplicationUser GetByUserName(string userName);
        Task<ApplicationUser> GetByUserNameAsync(string userName);

        void Add(ApplicationUser user, string role);

        void Update(ApplicationUser user);

        void Delete(string id);

        void Delete(ApplicationUser user);

        IQueryable<ApplicationUser> Table { get; }

        IQueryable<ApplicationUser> TableNoTracking { get; }
        
    }
}

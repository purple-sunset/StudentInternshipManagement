using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace Repositories
{
    public class UserRepository:IDisposable
    {
        private readonly ApplicationDbContext _context =new ApplicationDbContext();

        public ApplicationUser GetUser(string userName)
        {
            return _context.Set<ApplicationUser>().FirstOrDefault(u => u.UserName == userName);
        }

        public string GetEmail(string userName)
        {
            return GetUser(userName).Email;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

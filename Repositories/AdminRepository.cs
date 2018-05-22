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
    public class AdminRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<Admin> GetAll()
        {
            try
            {
                return _context.Admins;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Admin GetById(string id)
        {
            try
            {
                return _context.Admins.FirstOrDefault(s => s.AdminId.Equals(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Admin admin)
        {
            try
            {
                _context.Admins.Add(admin);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Admin admin)
        {
            try
            {
                _context.Entry(admin).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Admin admin)
        {
            var curr = GetById(admin.AdminId);
            if (curr == null)
                return false;

            try
            {
                _context.Admins.Remove(curr);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

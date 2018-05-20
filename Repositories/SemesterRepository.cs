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
    public class SemesterRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<Semester> GetAll()
        {
            try
            {
                return _context.Semesters;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Semester GetById(int id)
        {
            try
            {
                return _context.Semesters.FirstOrDefault(s => s.SemesterId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Semester semester)
        {
            try
            {
                _context.Semesters.Add(semester);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Semester semester)
        {
            try
            {
                _context.Entry(semester).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Semester semester)
        {
            var curr = GetById(semester.SemesterId);
            if (curr == null)
                return false;

            try
            {
                _context.Semesters.Remove(curr);
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

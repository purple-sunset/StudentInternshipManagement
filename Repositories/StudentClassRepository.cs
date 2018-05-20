using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace Repositories
{
    public class StudentClassRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<StudentClass> GetAll()
        {
            try
            {
                return _context.StudentClasses;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public StudentClass GetById(int id)
        {
            try
            {
                return _context.StudentClasses.FirstOrDefault(s => s.ClassId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(StudentClass studentClass)
        {
            try
            {
                _context.StudentClasses.Add(studentClass);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(StudentClass studentClass)
        {
            try
            {
                _context.Entry(studentClass).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(StudentClass studentClass)
        {
            var curr = GetById(studentClass.ClassId);
            if (curr == null)
                return false;

            try
            {
                _context.StudentClasses.Remove(curr);
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

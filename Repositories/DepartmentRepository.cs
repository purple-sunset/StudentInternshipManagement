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
    public class DepartmentRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<Department> GetAll()
        {
            return _context.Departments;
        }
        public Department GetById(int id)
        {
            try
            {
                return _context.Departments.FirstOrDefault(s => s.DepartmentId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Department department)
        {
            try
            {
                _context.Entry(department).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Department department)
        {
            var curr = GetById(department.DepartmentId);
            if (curr == null)
                return false;

            try
            {
                _context.Departments.Remove(curr);
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

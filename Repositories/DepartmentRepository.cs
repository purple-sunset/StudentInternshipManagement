using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

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
            return _context.Departments.FirstOrDefault(s => s.DepartmentId==id);
        }

        public bool Add(Department department)
        {
            _context.Entry(department).State = EntityState.Added;
            return _context.SaveChanges() > 0;
        }

        public bool Update(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Department department)
        {
            _context.Entry(department).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

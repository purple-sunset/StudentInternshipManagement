using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class StudentClassRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<StudentClass> GetAll()
        {
            return _context.StudentClasses;
        }

        public StudentClass GetById(int id)
        {
            return _context.StudentClasses.FirstOrDefault(s => s.ClassId == id);
        }

        public bool Add(StudentClass studentClass)
        {
            _context.Entry(studentClass).State = EntityState.Added;
            return _context.SaveChanges() > 0;
        }

        public bool Update(StudentClass studentClass)
        {
            _context.Entry(studentClass).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(StudentClass studentClass)
        {
            _context.Entry(studentClass).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

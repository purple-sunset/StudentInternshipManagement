using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class StudentClassRepository
    {
        private readonly WebContext _context=new WebContext();

        public StudentClass GetById(string id)
        {
            return _context.StudentClasses.FirstOrDefault(s => s.ClassId.Equals(id));
        }

        public bool Add(StudentClass studentClass)
        {
            _context.Entry(studentClass).State = EntityState.Added;
            return _context.SaveChanges() > 0;
        }

        public bool Update(StudentClass StudentClass)
        {
            _context.Entry(StudentClass).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(StudentClass StudentClass)
        {
            _context.Entry(StudentClass).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
    }
}

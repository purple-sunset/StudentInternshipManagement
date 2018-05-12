using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class StudentRepository
    {
        private readonly WebContext _context=new WebContext();

        public Student GetById(string id)
        {
            return _context.Students.Include("Class").FirstOrDefault(s => s.StudentId.Equals(id));
        }

        public bool Add(Student student)
        {
            _context.Entry(student).State = EntityState.Added;
            return _context.SaveChanges() > 0;
        }

        public bool Update(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Student student)
        {
            _context.Entry(student).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
    }
}
